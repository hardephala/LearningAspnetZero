using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.Donotreleases.Exporting;
using Arch.Donotreleases.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.Donotreleases
{
    [AbpAuthorize(AppPermissions.Pages_Donotreleases)]
    public class DonotreleasesAppService : ArchAppServiceBase, IDonotreleasesAppService
    {
        private readonly IRepository<Donotrelease> _donotreleaseRepository;
        private readonly IDonotreleasesExcelExporter _donotreleasesExcelExporter;

        public DonotreleasesAppService(IRepository<Donotrelease> donotreleaseRepository, IDonotreleasesExcelExporter donotreleasesExcelExporter)
        {
            _donotreleaseRepository = donotreleaseRepository;
            _donotreleasesExcelExporter = donotreleasesExcelExporter;

        }

        public async Task<PagedResultDto<GetDonotreleaseForViewDto>> GetAll(GetAllDonotreleasesInput input)
        {

            var filteredDonotreleases = _donotreleaseRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.blno.Contains(input.Filter) || e.status.Contains(input.Filter) || e.releasedby.Contains(input.Filter) || e.releasecomment.Contains(input.Filter) || e.blockedby.Contains(input.Filter) || e.blockedcomment.Contains(input.Filter) || e.blockedreference.Contains(input.Filter) || e.blcomment.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasedbyFilter), e => e.releasedby == input.releasedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasecommentFilter), e => e.releasecomment == input.releasecommentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blockedbyFilter), e => e.blockedby == input.blockedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blockedcommentFilter), e => e.blockedcomment == input.blockedcommentFilter)
                        .WhereIf(input.MinblockeddateFilter != null, e => e.blockeddate >= input.MinblockeddateFilter)
                        .WhereIf(input.MaxblockeddateFilter != null, e => e.blockeddate <= input.MaxblockeddateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blockedreferenceFilter), e => e.blockedreference == input.blockedreferenceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blcommentFilter), e => e.blcomment == input.blcommentFilter);

            var pagedAndFilteredDonotreleases = filteredDonotreleases
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var donotreleases = from o in pagedAndFilteredDonotreleases
                                select new
                                {

                                    o.blno,
                                    o.status,
                                    o.releasedby,
                                    o.releasecomment,
                                    o.blockedby,
                                    o.blockedcomment,
                                    o.blockeddate,
                                    o.blockedreference,
                                    o.blcomment,
                                    Id = o.Id
                                };

            var totalCount = await filteredDonotreleases.CountAsync();

            var dbList = await donotreleases.ToListAsync();
            var results = new List<GetDonotreleaseForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetDonotreleaseForViewDto()
                {
                    Donotrelease = new DonotreleaseDto
                    {

                        blno = o.blno,
                        status = o.status,
                        releasedby = o.releasedby,
                        releasecomment = o.releasecomment,
                        blockedby = o.blockedby,
                        blockedcomment = o.blockedcomment,
                        blockeddate = o.blockeddate,
                        blockedreference = o.blockedreference,
                        blcomment = o.blcomment,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetDonotreleaseForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetDonotreleaseForViewDto> GetDonotreleaseForView(int id)
        {
            var donotrelease = await _donotreleaseRepository.GetAsync(id);

            var output = new GetDonotreleaseForViewDto { Donotrelease = ObjectMapper.Map<DonotreleaseDto>(donotrelease) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Donotreleases_Edit)]
        public async Task<GetDonotreleaseForEditOutput> GetDonotreleaseForEdit(EntityDto input)
        {
            var donotrelease = await _donotreleaseRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDonotreleaseForEditOutput { Donotrelease = ObjectMapper.Map<CreateOrEditDonotreleaseDto>(donotrelease) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDonotreleaseDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Donotreleases_Create)]
        protected virtual async Task Create(CreateOrEditDonotreleaseDto input)
        {
            var donotrelease = ObjectMapper.Map<Donotrelease>(input);

            if (AbpSession.TenantId != null)
            {
                donotrelease.TenantId = (int?)AbpSession.TenantId;
            }

            await _donotreleaseRepository.InsertAsync(donotrelease);

        }

        [AbpAuthorize(AppPermissions.Pages_Donotreleases_Edit)]
        protected virtual async Task Update(CreateOrEditDonotreleaseDto input)
        {
            var donotrelease = await _donotreleaseRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, donotrelease);

        }

        [AbpAuthorize(AppPermissions.Pages_Donotreleases_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _donotreleaseRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDonotreleasesToExcel(GetAllDonotreleasesForExcelInput input)
        {

            var filteredDonotreleases = _donotreleaseRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.blno.Contains(input.Filter) || e.status.Contains(input.Filter) || e.releasedby.Contains(input.Filter) || e.releasecomment.Contains(input.Filter) || e.blockedby.Contains(input.Filter) || e.blockedcomment.Contains(input.Filter) || e.blockedreference.Contains(input.Filter) || e.blcomment.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasedbyFilter), e => e.releasedby == input.releasedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releasecommentFilter), e => e.releasecomment == input.releasecommentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blockedbyFilter), e => e.blockedby == input.blockedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blockedcommentFilter), e => e.blockedcomment == input.blockedcommentFilter)
                        .WhereIf(input.MinblockeddateFilter != null, e => e.blockeddate >= input.MinblockeddateFilter)
                        .WhereIf(input.MaxblockeddateFilter != null, e => e.blockeddate <= input.MaxblockeddateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blockedreferenceFilter), e => e.blockedreference == input.blockedreferenceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blcommentFilter), e => e.blcomment == input.blcommentFilter);

            var query = (from o in filteredDonotreleases
                         select new GetDonotreleaseForViewDto()
                         {
                             Donotrelease = new DonotreleaseDto
                             {
                                 blno = o.blno,
                                 status = o.status,
                                 releasedby = o.releasedby,
                                 releasecomment = o.releasecomment,
                                 blockedby = o.blockedby,
                                 blockedcomment = o.blockedcomment,
                                 blockeddate = o.blockeddate,
                                 blockedreference = o.blockedreference,
                                 blcomment = o.blcomment,
                                 Id = o.Id
                             }
                         });

            var donotreleaseListDtos = await query.ToListAsync();

            return _donotreleasesExcelExporter.ExportToFile(donotreleaseListDtos);
        }

    }
}