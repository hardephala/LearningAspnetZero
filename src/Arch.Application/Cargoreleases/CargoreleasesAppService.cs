using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.Cargoreleases.Exporting;
using Arch.Cargoreleases.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.Cargoreleases
{
    [AbpAuthorize(AppPermissions.Pages_Cargoreleases)]
    public class CargoreleasesAppService : ArchAppServiceBase, ICargoreleasesAppService
    {
        private readonly IRepository<Cargorelease, long> _cargoreleaseRepository;
        private readonly ICargoreleasesExcelExporter _cargoreleasesExcelExporter;

        public CargoreleasesAppService(IRepository<Cargorelease, long> cargoreleaseRepository, ICargoreleasesExcelExporter cargoreleasesExcelExporter)
        {
            _cargoreleaseRepository = cargoreleaseRepository;
            _cargoreleasesExcelExporter = cargoreleasesExcelExporter;

        }

        public async Task<PagedResultDto<GetCargoreleaseForViewDto>> GetAll(GetAllCargoreleasesInput input)
        {

            var filteredCargoreleases = _cargoreleaseRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.priority.Contains(input.Filter) || e.blno.Contains(input.Filter) || e.terminal.Contains(input.Filter) || e.deliveryorderno.Contains(input.Filter) || e.customercode.Contains(input.Filter) || e.agencycode.Contains(input.Filter) || e.agentcode.Contains(input.Filter) || e.entrybyrepcode.Contains(input.Filter) || e.entrymode.Contains(input.Filter) || e.approveby.Contains(input.Filter) || e.approvecomment.Contains(input.Filter) || e.updatedby.Contains(input.Filter) || e.updatecomment.Contains(input.Filter) || e.releaseby.Contains(input.Filter) || e.releasestatus.Contains(input.Filter) || e.releasecomment.Contains(input.Filter) || e.status.Contains(input.Filter) || e.ipaddr.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.priorityFilter), e => e.priority == input.priorityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(input.MininvoicevalidityFilter != null, e => e.invoicevalidity >= input.MininvoicevalidityFilter)
                        .WhereIf(input.MaxinvoicevalidityFilter != null, e => e.invoicevalidity <= input.MaxinvoicevalidityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.terminalFilter), e => e.terminal == input.terminalFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.deliveryordernoFilter), e => e.deliveryorderno == input.deliveryordernoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customercodeFilter), e => e.customercode == input.customercodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.agencycodeFilter), e => e.agencycode == input.agencycodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.agentcodeFilter), e => e.agentcode == input.agentcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.entrybyrepcodeFilter), e => e.entrybyrepcode == input.entrybyrepcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.entrymodeFilter), e => e.entrymode == input.entrymodeFilter)
                        .WhereIf(input.MinentrydateFilter != null, e => e.entrydate >= input.MinentrydateFilter)
                        .WhereIf(input.MaxentrydateFilter != null, e => e.entrydate <= input.MaxentrydateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.approvebyFilter), e => e.approveby == input.approvebyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.approvecommentFilter), e => e.approvecomment == input.approvecommentFilter)
                        .WhereIf(input.MinapprovedateFilter != null, e => e.approvedate >= input.MinapprovedateFilter)
                        .WhereIf(input.MaxapprovedateFilter != null, e => e.approvedate <= input.MaxapprovedateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.updatedbyFilter), e => e.updatedby == input.updatedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.updatecommentFilter), e => e.updatecomment == input.updatecommentFilter)
                        .WhereIf(input.MinupdatedateFilter != null, e => e.updatedate >= input.MinupdatedateFilter)
                        .WhereIf(input.MaxupdatedateFilter != null, e => e.updatedate <= input.MaxupdatedateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasebyFilter), e => e.releaseby == input.releasebyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasestatusFilter), e => e.releasestatus == input.releasestatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasecommentFilter), e => e.releasecomment == input.releasecommentFilter)
                        .WhereIf(input.MinreleasedateFilter != null, e => e.releasedate >= input.MinreleasedateFilter)
                        .WhereIf(input.MaxreleasedateFilter != null, e => e.releasedate <= input.MaxreleasedateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ipaddrFilter), e => e.ipaddr == input.ipaddrFilter);

            var pagedAndFilteredCargoreleases = filteredCargoreleases
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var cargoreleases = from o in pagedAndFilteredCargoreleases
                                select new
                                {

                                    o.priority,
                                    o.blno,
                                    o.invoicevalidity,
                                    o.terminal,
                                    o.deliveryorderno,
                                    o.customercode,
                                    o.agencycode,
                                    o.agentcode,
                                    o.entrybyrepcode,
                                    o.entrymode,
                                    o.entrydate,
                                    o.approveby,
                                    o.approvecomment,
                                    o.approvedate,
                                    o.updatedby,
                                    o.updatecomment,
                                    o.updatedate,
                                    o.releaseby,
                                    o.releasestatus,
                                    o.releasecomment,
                                    o.releasedate,
                                    o.status,
                                    o.ipaddr,
                                    Id = o.Id
                                };

            var totalCount = await filteredCargoreleases.CountAsync();

            var dbList = await cargoreleases.ToListAsync();
            var results = new List<GetCargoreleaseForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCargoreleaseForViewDto()
                {
                    Cargorelease = new CargoreleaseDto
                    {

                        priority = o.priority,
                        blno = o.blno,
                        invoicevalidity = o.invoicevalidity,
                        terminal = o.terminal,
                        deliveryorderno = o.deliveryorderno,
                        customercode = o.customercode,
                        agencycode = o.agencycode,
                        agentcode = o.agentcode,
                        entrybyrepcode = o.entrybyrepcode,
                        entrymode = o.entrymode,
                        entrydate = o.entrydate,
                        approveby = o.approveby,
                        approvecomment = o.approvecomment,
                        approvedate = o.approvedate,
                        updatedby = o.updatedby,
                        updatecomment = o.updatecomment,
                        updatedate = o.updatedate,
                        releaseby = o.releaseby,
                        releasestatus = o.releasestatus,
                        releasecomment = o.releasecomment,
                        releasedate = o.releasedate,
                        status = o.status,
                        ipaddr = o.ipaddr,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCargoreleaseForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCargoreleaseForViewDto> GetCargoreleaseForView(long id)
        {
            var cargorelease = await _cargoreleaseRepository.GetAsync(id);

            var output = new GetCargoreleaseForViewDto { Cargorelease = ObjectMapper.Map<CargoreleaseDto>(cargorelease) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Cargoreleases_Edit)]
        public async Task<GetCargoreleaseForEditOutput> GetCargoreleaseForEdit(EntityDto<long> input)
        {
            var cargorelease = await _cargoreleaseRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCargoreleaseForEditOutput { Cargorelease = ObjectMapper.Map<CreateOrEditCargoreleaseDto>(cargorelease) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCargoreleaseDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Cargoreleases_Create)]
        protected virtual async Task Create(CreateOrEditCargoreleaseDto input)
        {
            var cargorelease = ObjectMapper.Map<Cargorelease>(input);

            if (AbpSession.TenantId != null)
            {
                cargorelease.TenantId = (int?)AbpSession.TenantId;
            }

            await _cargoreleaseRepository.InsertAsync(cargorelease);

        }

        [AbpAuthorize(AppPermissions.Pages_Cargoreleases_Edit)]
        protected virtual async Task Update(CreateOrEditCargoreleaseDto input)
        {
            var cargorelease = await _cargoreleaseRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, cargorelease);

        }

        [AbpAuthorize(AppPermissions.Pages_Cargoreleases_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _cargoreleaseRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCargoreleasesToExcel(GetAllCargoreleasesForExcelInput input)
        {

            var filteredCargoreleases = _cargoreleaseRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.priority.Contains(input.Filter) || e.blno.Contains(input.Filter) || e.terminal.Contains(input.Filter) || e.deliveryorderno.Contains(input.Filter) || e.customercode.Contains(input.Filter) || e.agencycode.Contains(input.Filter) || e.agentcode.Contains(input.Filter) || e.entrybyrepcode.Contains(input.Filter) || e.entrymode.Contains(input.Filter) || e.approveby.Contains(input.Filter) || e.approvecomment.Contains(input.Filter) || e.updatedby.Contains(input.Filter) || e.updatecomment.Contains(input.Filter) || e.releaseby.Contains(input.Filter) || e.releasestatus.Contains(input.Filter) || e.releasecomment.Contains(input.Filter) || e.status.Contains(input.Filter) || e.ipaddr.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.priorityFilter), e => e.priority == input.priorityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(input.MininvoicevalidityFilter != null, e => e.invoicevalidity >= input.MininvoicevalidityFilter)
                        .WhereIf(input.MaxinvoicevalidityFilter != null, e => e.invoicevalidity <= input.MaxinvoicevalidityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.terminalFilter), e => e.terminal == input.terminalFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.deliveryordernoFilter), e => e.deliveryorderno == input.deliveryordernoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customercodeFilter), e => e.customercode == input.customercodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.agencycodeFilter), e => e.agencycode == input.agencycodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.agentcodeFilter), e => e.agentcode == input.agentcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.entrybyrepcodeFilter), e => e.entrybyrepcode == input.entrybyrepcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.entrymodeFilter), e => e.entrymode == input.entrymodeFilter)
                        .WhereIf(input.MinentrydateFilter != null, e => e.entrydate >= input.MinentrydateFilter)
                        .WhereIf(input.MaxentrydateFilter != null, e => e.entrydate <= input.MaxentrydateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.approvebyFilter), e => e.approveby == input.approvebyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.approvecommentFilter), e => e.approvecomment == input.approvecommentFilter)
                        .WhereIf(input.MinapprovedateFilter != null, e => e.approvedate >= input.MinapprovedateFilter)
                        .WhereIf(input.MaxapprovedateFilter != null, e => e.approvedate <= input.MaxapprovedateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.updatedbyFilter), e => e.updatedby == input.updatedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.updatecommentFilter), e => e.updatecomment == input.updatecommentFilter)
                        .WhereIf(input.MinupdatedateFilter != null, e => e.updatedate >= input.MinupdatedateFilter)
                        .WhereIf(input.MaxupdatedateFilter != null, e => e.updatedate <= input.MaxupdatedateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasebyFilter), e => e.releaseby == input.releasebyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasestatusFilter), e => e.releasestatus == input.releasestatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasecommentFilter), e => e.releasecomment == input.releasecommentFilter)
                        .WhereIf(input.MinreleasedateFilter != null, e => e.releasedate >= input.MinreleasedateFilter)
                        .WhereIf(input.MaxreleasedateFilter != null, e => e.releasedate <= input.MaxreleasedateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ipaddrFilter), e => e.ipaddr == input.ipaddrFilter);

            var query = (from o in filteredCargoreleases
                         select new GetCargoreleaseForViewDto()
                         {
                             Cargorelease = new CargoreleaseDto
                             {
                                 priority = o.priority,
                                 blno = o.blno,
                                 invoicevalidity = o.invoicevalidity,
                                 terminal = o.terminal,
                                 deliveryorderno = o.deliveryorderno,
                                 customercode = o.customercode,
                                 agencycode = o.agencycode,
                                 agentcode = o.agentcode,
                                 entrybyrepcode = o.entrybyrepcode,
                                 entrymode = o.entrymode,
                                 entrydate = o.entrydate,
                                 approveby = o.approveby,
                                 approvecomment = o.approvecomment,
                                 approvedate = o.approvedate,
                                 updatedby = o.updatedby,
                                 updatecomment = o.updatecomment,
                                 updatedate = o.updatedate,
                                 releaseby = o.releaseby,
                                 releasestatus = o.releasestatus,
                                 releasecomment = o.releasecomment,
                                 releasedate = o.releasedate,
                                 status = o.status,
                                 ipaddr = o.ipaddr,
                                 Id = o.Id
                             }
                         });

            var cargoreleaseListDtos = await query.ToListAsync();

            return _cargoreleasesExcelExporter.ExportToFile(cargoreleaseListDtos);
        }

    }
}