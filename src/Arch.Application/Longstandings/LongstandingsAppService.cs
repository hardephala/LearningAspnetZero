using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.Longstandings.Exporting;
using Arch.Longstandings.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.Longstandings
{
    [AbpAuthorize(AppPermissions.Pages_Longstandings)]
    public class LongstandingsAppService : ArchAppServiceBase, ILongstandingsAppService
    {
        private readonly IRepository<Longstanding> _longstandingRepository;
        private readonly ILongstandingsExcelExporter _longstandingsExcelExporter;

        public LongstandingsAppService(IRepository<Longstanding> longstandingRepository, ILongstandingsExcelExporter longstandingsExcelExporter)
        {
            _longstandingRepository = longstandingRepository;
            _longstandingsExcelExporter = longstandingsExcelExporter;

        }

        public async Task<PagedResultDto<GetLongstandingForViewDto>> GetAll(GetAllLongstandingsInput input)
        {

            var filteredLongstandings = _longstandingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.customercode.Contains(input.Filter) || e.blno.Contains(input.Filter) || e.containerno.Contains(input.Filter) || e.containertype.Contains(input.Filter) || e.freetext.Contains(input.Filter) || e.location.Contains(input.Filter) || e.lastmove.Contains(input.Filter) || e.status.Contains(input.Filter) || e.releasedby.Contains(input.Filter) || e.releasedreason.Contains(input.Filter) || e.releasecomment.Contains(input.Filter) || e.shipoperator.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customercodeFilter), e => e.customercode == input.customercodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.containernoFilter), e => e.containerno == input.containernoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.containertypeFilter), e => e.containertype == input.containertypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.freetextFilter), e => e.freetext == input.freetextFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.locationFilter), e => e.location == input.locationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.lastmoveFilter), e => e.lastmove == input.lastmoveFilter)
                        .WhereIf(input.MindaysFilter != null, e => e.days >= input.MindaysFilter)
                        .WhereIf(input.MaxdaysFilter != null, e => e.days <= input.MaxdaysFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasedbyFilter), e => e.releasedby == input.releasedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasedreasonFilter), e => e.releasedreason == input.releasedreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasecommentFilter), e => e.releasecomment == input.releasecommentFilter)
                        .WhereIf(input.MinvaliditydateFilter != null, e => e.validitydate >= input.MinvaliditydateFilter)
                        .WhereIf(input.MaxvaliditydateFilter != null, e => e.validitydate <= input.MaxvaliditydateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipoperatorFilter), e => e.shipoperator == input.shipoperatorFilter);

            var pagedAndFilteredLongstandings = filteredLongstandings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var longstandings = from o in pagedAndFilteredLongstandings
                                select new
                                {

                                    o.customercode,
                                    o.blno,
                                    o.containerno,
                                    o.containertype,
                                    o.freetext,
                                    o.location,
                                    o.lastmove,
                                    o.days,
                                    o.status,
                                    o.releasedby,
                                    o.releasedreason,
                                    o.releasecomment,
                                    o.validitydate,
                                    o.shipoperator,
                                    Id = o.Id
                                };

            var totalCount = await filteredLongstandings.CountAsync();

            var dbList = await longstandings.ToListAsync();
            var results = new List<GetLongstandingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetLongstandingForViewDto()
                {
                    Longstanding = new LongstandingDto
                    {

                        customercode = o.customercode,
                        blno = o.blno,
                        containerno = o.containerno,
                        containertype = o.containertype,
                        freetext = o.freetext,
                        location = o.location,
                        lastmove = o.lastmove,
                        days = o.days,
                        status = o.status,
                        releasedby = o.releasedby,
                        releasedreason = o.releasedreason,
                        releasecomment = o.releasecomment,
                        validitydate = o.validitydate,
                        shipoperator = o.shipoperator,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetLongstandingForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetLongstandingForViewDto> GetLongstandingForView(int id)
        {
            var longstanding = await _longstandingRepository.GetAsync(id);

            var output = new GetLongstandingForViewDto { Longstanding = ObjectMapper.Map<LongstandingDto>(longstanding) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Longstandings_Edit)]
        public async Task<GetLongstandingForEditOutput> GetLongstandingForEdit(EntityDto input)
        {
            var longstanding = await _longstandingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLongstandingForEditOutput { Longstanding = ObjectMapper.Map<CreateOrEditLongstandingDto>(longstanding) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLongstandingDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Longstandings_Create)]
        protected virtual async Task Create(CreateOrEditLongstandingDto input)
        {
            var longstanding = ObjectMapper.Map<Longstanding>(input);

            if (AbpSession.TenantId != null)
            {
                longstanding.TenantId = (int?)AbpSession.TenantId;
            }

            await _longstandingRepository.InsertAsync(longstanding);

        }

        [AbpAuthorize(AppPermissions.Pages_Longstandings_Edit)]
        protected virtual async Task Update(CreateOrEditLongstandingDto input)
        {
            var longstanding = await _longstandingRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, longstanding);

        }

        [AbpAuthorize(AppPermissions.Pages_Longstandings_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _longstandingRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetLongstandingsToExcel(GetAllLongstandingsForExcelInput input)
        {

            var filteredLongstandings = _longstandingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.customercode.Contains(input.Filter) || e.blno.Contains(input.Filter) || e.containerno.Contains(input.Filter) || e.containertype.Contains(input.Filter) || e.freetext.Contains(input.Filter) || e.location.Contains(input.Filter) || e.lastmove.Contains(input.Filter) || e.status.Contains(input.Filter) || e.releasedby.Contains(input.Filter) || e.releasedreason.Contains(input.Filter) || e.releasecomment.Contains(input.Filter) || e.shipoperator.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customercodeFilter), e => e.customercode == input.customercodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.containernoFilter), e => e.containerno == input.containernoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.containertypeFilter), e => e.containertype == input.containertypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.freetextFilter), e => e.freetext == input.freetextFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.locationFilter), e => e.location == input.locationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.lastmoveFilter), e => e.lastmove == input.lastmoveFilter)
                        .WhereIf(input.MindaysFilter != null, e => e.days >= input.MindaysFilter)
                        .WhereIf(input.MaxdaysFilter != null, e => e.days <= input.MaxdaysFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasedbyFilter), e => e.releasedby == input.releasedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasedreasonFilter), e => e.releasedreason == input.releasedreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasecommentFilter), e => e.releasecomment == input.releasecommentFilter)
                        .WhereIf(input.MinvaliditydateFilter != null, e => e.validitydate >= input.MinvaliditydateFilter)
                        .WhereIf(input.MaxvaliditydateFilter != null, e => e.validitydate <= input.MaxvaliditydateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipoperatorFilter), e => e.shipoperator == input.shipoperatorFilter);

            var query = (from o in filteredLongstandings
                         select new GetLongstandingForViewDto()
                         {
                             Longstanding = new LongstandingDto
                             {
                                 customercode = o.customercode,
                                 blno = o.blno,
                                 containerno = o.containerno,
                                 containertype = o.containertype,
                                 freetext = o.freetext,
                                 location = o.location,
                                 lastmove = o.lastmove,
                                 days = o.days,
                                 status = o.status,
                                 releasedby = o.releasedby,
                                 releasedreason = o.releasedreason,
                                 releasecomment = o.releasecomment,
                                 validitydate = o.validitydate,
                                 shipoperator = o.shipoperator,
                                 Id = o.Id
                             }
                         });

            var longstandingListDtos = await query.ToListAsync();

            return _longstandingsExcelExporter.ExportToFile(longstandingListDtos);
        }

    }
}