﻿using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Notifications;
using Arch.Web.Controllers;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class NotificationsController : ArchControllerBase
    {
        private readonly INotificationAppService _notificationApp;

        public NotificationsController(INotificationAppService notificationApp)
        {
            _notificationApp = notificationApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> SettingsModal()
        {
            var notificationSettings = await _notificationApp.GetNotificationSettings();
            return PartialView("_SettingsModal", notificationSettings);
        }
    }
}