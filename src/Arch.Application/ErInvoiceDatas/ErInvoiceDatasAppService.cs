using Arch.Billofladings;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.ErInvoiceDatas.Exporting;
using Arch.ErInvoiceDatas.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.ErInvoiceDatas
{
    [AbpAuthorize(AppPermissions.Pages_ErInvoiceDatas)]
    public class ErInvoiceDatasAppService : ArchAppServiceBase, IErInvoiceDatasAppService
    {
        private readonly IRepository<ErInvoiceData> _erInvoiceDataRepository;
        private readonly IErInvoiceDatasExcelExporter _erInvoiceDatasExcelExporter;
        private readonly IRepository<Billoflading, long> _lookup_billofladingRepository;

        public ErInvoiceDatasAppService(IRepository<ErInvoiceData> erInvoiceDataRepository, IErInvoiceDatasExcelExporter erInvoiceDatasExcelExporter, IRepository<Billoflading, long> lookup_billofladingRepository)
        {
            _erInvoiceDataRepository = erInvoiceDataRepository;
            _erInvoiceDatasExcelExporter = erInvoiceDatasExcelExporter;
            _lookup_billofladingRepository = lookup_billofladingRepository;

        }

        public async Task<PagedResultDto<GetErInvoiceDataForViewDto>> GetAll(GetAllErInvoiceDatasInput input)
        {

            var filteredErInvoiceDatas = _erInvoiceDataRepository.GetAll()
                        .Include(e => e.BillofladingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.amount.Contains(input.Filter) || e.amountdue.Contains(input.Filter) || e.status.Contains(input.Filter))
                        .WhereIf(input.MinvalidityDateFilter != null, e => e.validityDate >= input.MinvalidityDateFilter)
                        .WhereIf(input.MaxvalidityDateFilter != null, e => e.validityDate <= input.MaxvalidityDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountFilter), e => e.amount == input.amountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountdueFilter), e => e.amountdue == input.amountdueFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BillofladingblnoFilter), e => e.BillofladingFk != null && e.BillofladingFk.blno == input.BillofladingblnoFilter);

            var pagedAndFilteredErInvoiceDatas = filteredErInvoiceDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var erInvoiceDatas = from o in pagedAndFilteredErInvoiceDatas
                                 join o1 in _lookup_billofladingRepository.GetAll() on o.BillofladingId equals o1.Id into j1
                                 from s1 in j1.DefaultIfEmpty()

                                 select new
                                 {

                                     o.validityDate,
                                     o.amount,
                                     o.amountdue,
                                     o.status,
                                     Id = o.Id,
                                     Billofladingblno = s1 == null || s1.blno == null ? "" : s1.blno.ToString()
                                 };

            var totalCount = await filteredErInvoiceDatas.CountAsync();

            var dbList = await erInvoiceDatas.ToListAsync();
            var results = new List<GetErInvoiceDataForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetErInvoiceDataForViewDto()
                {
                    ErInvoiceData = new ErInvoiceDataDto
                    {

                        validityDate = o.validityDate,
                        amount = o.amount,
                        amountdue = o.amountdue,
                        status = o.status,
                        Id = o.Id,
                    },
                    Billofladingblno = o.Billofladingblno
                };

                results.Add(res);
            }

            return new PagedResultDto<GetErInvoiceDataForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetErInvoiceDataForViewDto> GetErInvoiceDataForView(int id)
        {
            var erInvoiceData = await _erInvoiceDataRepository.GetAsync(id);

            var output = new GetErInvoiceDataForViewDto { ErInvoiceData = ObjectMapper.Map<ErInvoiceDataDto>(erInvoiceData) };

            if (output.ErInvoiceData.BillofladingId != null)
            {
                var _lookupBilloflading = await _lookup_billofladingRepository.FirstOrDefaultAsync((long)output.ErInvoiceData.BillofladingId);
                output.Billofladingblno = _lookupBilloflading?.blno?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ErInvoiceDatas_Edit)]
        public async Task<GetErInvoiceDataForEditOutput> GetErInvoiceDataForEdit(EntityDto input)
        {
            var erInvoiceData = await _erInvoiceDataRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetErInvoiceDataForEditOutput { ErInvoiceData = ObjectMapper.Map<CreateOrEditErInvoiceDataDto>(erInvoiceData) };

            if (output.ErInvoiceData.BillofladingId != null)
            {
                var _lookupBilloflading = await _lookup_billofladingRepository.FirstOrDefaultAsync((long)output.ErInvoiceData.BillofladingId);
                output.Billofladingblno = _lookupBilloflading?.blno?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditErInvoiceDataDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ErInvoiceDatas_Create)]
        protected virtual async Task Create(CreateOrEditErInvoiceDataDto input)
        {
            var erInvoiceData = ObjectMapper.Map<ErInvoiceData>(input);

            if (AbpSession.TenantId != null)
            {
                erInvoiceData.TenantId = (int?)AbpSession.TenantId;
            }

            await _erInvoiceDataRepository.InsertAsync(erInvoiceData);

        }

        [AbpAuthorize(AppPermissions.Pages_ErInvoiceDatas_Edit)]
        protected virtual async Task Update(CreateOrEditErInvoiceDataDto input)
        {
            var erInvoiceData = await _erInvoiceDataRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, erInvoiceData);

        }

        [AbpAuthorize(AppPermissions.Pages_ErInvoiceDatas_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _erInvoiceDataRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetErInvoiceDatasToExcel(GetAllErInvoiceDatasForExcelInput input)
        {

            var filteredErInvoiceDatas = _erInvoiceDataRepository.GetAll()
                        .Include(e => e.BillofladingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.amount.Contains(input.Filter) || e.amountdue.Contains(input.Filter) || e.status.Contains(input.Filter))
                        .WhereIf(input.MinvalidityDateFilter != null, e => e.validityDate >= input.MinvalidityDateFilter)
                        .WhereIf(input.MaxvalidityDateFilter != null, e => e.validityDate <= input.MaxvalidityDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountFilter), e => e.amount == input.amountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountdueFilter), e => e.amountdue == input.amountdueFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BillofladingblnoFilter), e => e.BillofladingFk != null && e.BillofladingFk.blno == input.BillofladingblnoFilter);

            var query = (from o in filteredErInvoiceDatas
                         join o1 in _lookup_billofladingRepository.GetAll() on o.BillofladingId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetErInvoiceDataForViewDto()
                         {
                             ErInvoiceData = new ErInvoiceDataDto
                             {
                                 validityDate = o.validityDate,
                                 amount = o.amount,
                                 amountdue = o.amountdue,
                                 status = o.status,
                                 Id = o.Id
                             },
                             Billofladingblno = s1 == null || s1.blno == null ? "" : s1.blno.ToString()
                         });

            var erInvoiceDataListDtos = await query.ToListAsync();

            return _erInvoiceDatasExcelExporter.ExportToFile(erInvoiceDataListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_ErInvoiceDatas)]
        public async Task<PagedResultDto<ErInvoiceDataBillofladingLookupTableDto>> GetAllBillofladingForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_billofladingRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.blno != null && e.blno.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var billofladingList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ErInvoiceDataBillofladingLookupTableDto>();
            foreach (var billoflading in billofladingList)
            {
                lookupTableDtoList.Add(new ErInvoiceDataBillofladingLookupTableDto
                {
                    Id = billoflading.Id,
                    DisplayName = billoflading.blno?.ToString()
                });
            }

            return new PagedResultDto<ErInvoiceDataBillofladingLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}