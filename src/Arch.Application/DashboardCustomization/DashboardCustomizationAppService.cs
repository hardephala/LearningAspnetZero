﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.UI;
using Arch.Configuration;
using Arch.DashboardCustomization.Definitions;
using Arch.DashboardCustomization.Dto;
using Newtonsoft.Json;

namespace Arch.DashboardCustomization
{
    [AbpAuthorize]
    public class DashboardCustomizationAppService : ArchAppServiceBase, IDashboardCustomizationAppService
    {
        private readonly DashboardConfiguration _dashboardConfiguration;

        public DashboardCustomizationAppService(DashboardConfiguration dashboardConfiguration)
        {
            _dashboardConfiguration = dashboardConfiguration;
        }

        public async Task<Dashboard> GetUserDashboard(GetDashboardInput input)
        {
            return GetDashboard(await GetDashboardsFromSettings(input.Application), input.DashboardName);
        }

        public async Task SavePage(SavePageInput input)
        {
            var dashboards = await GetDashboardsFromSettings(input.Application);
            var dashboard = GetDashboard(dashboards, input.DashboardName);

            foreach (var inputPage in input.Pages)
            {
                var page = dashboard.Pages.FirstOrDefault(p => p.Id == inputPage.Id);
                var pageIndex = dashboard.Pages.IndexOf(page);

                dashboard.Pages.RemoveAt(pageIndex);

                if (page != null)
                {
                    inputPage.Name = page.Name;
                    dashboard.Pages.Insert(pageIndex, inputPage);
                }
            }

            await SaveSetting(input.Application, dashboards);
        }

        public async Task RenamePage(RenamePageInput input)
        {
            var dashboards = await GetDashboardsFromSettings(input.Application);
            var dashboard = GetDashboard(dashboards, input.DashboardName);

            var page = dashboard.Pages.FirstOrDefault(p => p.Id == input.Id);
            if (page == null)
            {
                return;
            }

            page.Name = input.Name;

            await SaveSetting(input.Application, dashboards);
        }

        public async Task<AddNewPageOutput> AddNewPage(AddNewPageInput input)
        {
            var dashboards = await GetDashboardsFromSettings(input.Application);
            var dashboard = GetDashboard(dashboards, input.DashboardName);

            var page = new Page
            {
                Name = input.Name,
                Widgets = new List<Widget>(),
            };

            dashboard.Pages.Add(page);
            await SaveSetting(input.Application, dashboards);

            return new AddNewPageOutput { PageId = page.Id };
        }

        public async Task DeletePage(DeletePageInput input)
        {
            var dashboards = await GetDashboardsFromSettings(input.Application);
            var dashboard = GetDashboard(dashboards, input.DashboardName);

            dashboard.Pages.RemoveAll(p => p.Id == input.Id);

            if (dashboard.Pages.Count == 0) // return to default
            {
                var defaultDashboard = (await GetDefaultDashboardValue(input.Application)).FirstOrDefault(d => d.DashboardName == input.DashboardName);

                dashboards.Remove(dashboard);
                dashboards.Add(defaultDashboard);
            }

            await SaveSetting(input.Application, dashboards);
        }

        public async Task<Widget> AddWidget(AddWidgetInput input)
        {
            var widgetDefinition = _dashboardConfiguration.WidgetDefinitions.FirstOrDefault(w => w.Id == input.WidgetId);
            if (widgetDefinition == null)
            {
                throw new UserFriendlyException(L("WidgetNotFound"));
            }

            var dashboards = await GetDashboardsFromSettings(input.Application);
            var dashboard = GetDashboard(dashboards, input.DashboardName);

            var page = dashboard.Pages.Single(p => p.Id == input.PageId);

            if (!widgetDefinition.AllowMultipleInstanceInSamePage && page.Widgets.Any(w => w.WidgetId == widgetDefinition.Id))
            {
                throw new UserFriendlyException(L("WidgetCanNotBePlacedMoreThanOnceInAPageWarning"));
            }

            var widget = new Widget
            {
                WidgetId = input.WidgetId,
                Height = input.Height,
                Width = input.Width,
                PositionX = 0,
                PositionY = CalculatePositionY(page.Widgets)
            };

            page.Widgets.Add(widget);

            await SaveSetting(input.Application, dashboards);
            return widget;
        }

        public DashboardOutput GetDashboardDefinition(GetDashboardInput input)
        {
            var dashboardDefinition = _dashboardConfiguration.DashboardDefinitions.FirstOrDefault(d => d.Name == input.DashboardName);
            if (dashboardDefinition == null)
            {
                throw new UserFriendlyException(L("UnknownDashboard", input.DashboardName));
            }

            //widgets which used in that dashboard
            var usedWidgetDefinitions = GetFilteredWidgets(dashboardDefinition);

            return new DashboardOutput(
                dashboardDefinition.Name,
                usedWidgetDefinitions
                    .Select(widget => new WidgetOutput(
                    widget.Id,
                    widget.Name,
                    widget.Description,
                    filters: GetNeededWidgetFiltersOutput(widget))
                    ).ToList()
            );
        }
        
        public List<WidgetOutput> GetAllWidgetDefinitions(GetDashboardInput input)
        {
            var dashboardDefinition = _dashboardConfiguration.DashboardDefinitions.FirstOrDefault(d => d.Name == input.DashboardName);
            if (dashboardDefinition == null)
            {
                throw new UserFriendlyException(L("UnknownDashboard", input.DashboardName));
            }

            return GetFilteredWidgets(dashboardDefinition)
                .Select(widget => new WidgetOutput(widget.Id, widget.Name, widget.Description)).ToList();
        }

        public async Task<List<WidgetOutput>> GetAllAvailableWidgetDefinitionsForPage(GetAvailableWidgetDefinitionsForPageInput input)
        {
            var dashboardDefinition =
                _dashboardConfiguration.DashboardDefinitions.FirstOrDefault(d => d.Name == input.DashboardName);
            if (dashboardDefinition == null)
            {
                throw new UserFriendlyException(L("UnknownDashboard", input.DashboardName));
            }

            var dashboard = await GetUserDashboard(new GetDashboardInput()
            {
                Application = input.Application,
                DashboardName = input.DashboardName
            });

            var page = dashboard.Pages.FirstOrDefault(p => p.Id == input.PageId);
            if (page == null)
            {
                throw new UserFriendlyException(L("UnknownPage"));
            }

            var widgetsAlreadyInPage = page.Widgets.Select(w => w.WidgetId).ToList();

            return GetFilteredWidgets(dashboardDefinition)
                .Where(widgetDefinition =>
                    widgetDefinition.AllowMultipleInstanceInSamePage ||
                    !widgetsAlreadyInPage.Contains(widgetDefinition.Id)
                )
                .Select(widget => new WidgetOutput(widget.Id, widget.Name, widget.Description)).ToList();
        }

        public string GetSettingName(string application)
        {
            return AppSettings.DashboardCustomization.Configuration + "." + application;
        }

        private Dashboard GetDashboard(List<Dashboard> dashboards, string dashboardName)
        {
            var dashboard = dashboards.FirstOrDefault(d => d.DashboardName == dashboardName);
            if (dashboard == null)
            {
                throw new UserFriendlyException(L("UnknownDashboard", dashboardName));
            }

            return dashboard;
        }

        private async Task<List<Dashboard>> GetDashboardsFromSettings(string application)
        {
            var dashboardConfigAsJsonString = await SettingManager.GetSettingValueAsync(GetSettingName(application));

            if (string.IsNullOrWhiteSpace(dashboardConfigAsJsonString))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Dashboard>>(dashboardConfigAsJsonString);
        }

        private async Task SaveSetting(string application, List<Dashboard> dashboards)
        {
            var value = JsonConvert.SerializeObject(dashboards);

            var currentUser = await GetCurrentUserAsync();
            await SettingManager.ChangeSettingForUserAsync(currentUser.ToUserIdentifier(), GetSettingName(application), value);
        }

        private byte CalculatePositionY(List<Widget> widgets)
        {
            if (widgets == null || !widgets.Any())
            {
                return 0;
            }

            return (byte)widgets.Max(w => w.PositionY + w.Height);
        }

        private async Task<List<Dashboard>> GetDefaultDashboardValue(string application)
        {
            string dashboardConfigAsJsonString;

            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                dashboardConfigAsJsonString = await SettingManager.GetSettingValueForApplicationAsync(GetSettingName(application));
            }
            else
            {
                dashboardConfigAsJsonString = await SettingManager.GetSettingValueForTenantAsync(GetSettingName(application), AbpSession.GetTenantId());
            }

            return string.IsNullOrWhiteSpace(dashboardConfigAsJsonString)
                ? null
                : JsonConvert.DeserializeObject<List<Dashboard>>(dashboardConfigAsJsonString);
        }

        private List<WidgetDefinition> GetFilteredWidgets(DashboardDefinition dashboardDefinition)
        {
            var dashboardWidgets = dashboardDefinition.AvailableWidgets ?? new List<string>();

            var widgetDefinitions = _dashboardConfiguration.WidgetDefinitions
                .Where(wd => dashboardWidgets.Contains(wd.Id)).ToList();

            return FilterWidgetsByPermissionAndMultiTenancySide(FilterWidgetsByMultiTenancySide(widgetDefinitions));
        }

        private List<WidgetDefinition> FilterWidgetsByPermissionAndMultiTenancySide(List<WidgetDefinition> widgets)
        {
            return FilterWidgetsByPermission(FilterWidgetsByMultiTenancySide(widgets));
        }

        private List<WidgetDefinition> FilterWidgetsByPermission(List<WidgetDefinition> widgets)
        {
            var filteredWidgets = new List<WidgetDefinition>();

            foreach (var widget in widgets)
            {
                if (widget.Permissions.All(p => PermissionChecker.IsGranted(p)))
                {
                    filteredWidgets.Add(widget);
                }
            }

            return filteredWidgets;
        }

        private List<WidgetDefinition> FilterWidgetsByMultiTenancySide(List<WidgetDefinition> widgets)
        {
            return widgets.Where(w => w.Side.HasFlag(AbpSession.MultiTenancySide)).ToList();
        }

        private List<WidgetFilterOutput> GetNeededWidgetFiltersOutput(WidgetDefinition widget)
        {
            if (widget.UsedWidgetFilters == null || !widget.UsedWidgetFilters.Any())
            {
                return new List<WidgetFilterOutput>();
            }

            var allNeededFilters = widget.UsedWidgetFilters.Distinct().ToList();

            return _dashboardConfiguration.WidgetFilterDefinitions
                .Where(definition => allNeededFilters.Contains(definition.Id))
                .Select(x => new WidgetFilterOutput(x.Id, x.Name))
                .ToList();
        }
    }
}
