using Arch.Billofladings;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.Invoicedatas.Exporting;
using Arch.Invoicedatas.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.Invoicedatas
{
    [AbpAuthorize(AppPermissions.Pages_Invoicedatas)]
    public class InvoicedatasAppService : ArchAppServiceBase, IInvoicedatasAppService
    {
        private readonly IRepository<Invoicedata> _invoicedataRepository;
        private readonly IInvoicedatasExcelExporter _invoicedatasExcelExporter;
        private readonly IRepository<Billoflading, long> _lookup_billofladingRepository;

        public InvoicedatasAppService(IRepository<Invoicedata> invoicedataRepository, IInvoicedatasExcelExporter invoicedatasExcelExporter, IRepository<Billoflading, long> lookup_billofladingRepository)
        {
            _invoicedataRepository = invoicedataRepository;
            _invoicedatasExcelExporter = invoicedatasExcelExporter;
            _lookup_billofladingRepository = lookup_billofladingRepository;

        }

        public async Task<PagedResultDto<GetInvoicedataForViewDto>> GetAll(GetAllInvoicedatasInput input)
        {

            var filteredInvoicedatas = _invoicedataRepository.GetAll()
                        .Include(e => e.BillofladingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.blno.Contains(input.Filter) || e.amount.Contains(input.Filter) || e.amountdue.Contains(input.Filter) || e.status.Contains(input.Filter) || e.userid.Contains(input.Filter) || e.waiver.Contains(input.Filter) || e.waivedamount.Contains(input.Filter) || e.waivedby.Contains(input.Filter) || e.waivecomment.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(input.MinvaliditydateFilter != null, e => e.validitydate >= input.MinvaliditydateFilter)
                        .WhereIf(input.MaxvaliditydateFilter != null, e => e.validitydate <= input.MaxvaliditydateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountFilter), e => e.amount == input.amountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountdueFilter), e => e.amountdue == input.amountdueFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(input.MininvpaiddateFilter != null, e => e.invpaiddate >= input.MininvpaiddateFilter)
                        .WhereIf(input.MaxinvpaiddateFilter != null, e => e.invpaiddate <= input.MaxinvpaiddateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.useridFilter), e => e.userid == input.useridFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waiverFilter), e => e.waiver == input.waiverFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waivedamountFilter), e => e.waivedamount == input.waivedamountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waivedbyFilter), e => e.waivedby == input.waivedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waivecommentFilter), e => e.waivecomment == input.waivecommentFilter)
                        .WhereIf(input.MindatewaivedFilter != null, e => e.datewaived >= input.MindatewaivedFilter)
                        .WhereIf(input.MaxdatewaivedFilter != null, e => e.datewaived <= input.MaxdatewaivedFilter)
                        .WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
                        .WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
                        .WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
                        .WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BillofladingblnoFilter), e => e.BillofladingFk != null && e.BillofladingFk.blno == input.BillofladingblnoFilter);

            var pagedAndFilteredInvoicedatas = filteredInvoicedatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var invoicedatas = from o in pagedAndFilteredInvoicedatas
                               join o1 in _lookup_billofladingRepository.GetAll() on o.BillofladingId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               select new
                               {

                                   o.blno,
                                   o.validitydate,
                                   o.amount,
                                   o.amountdue,
                                   o.status,
                                   o.invpaiddate,
                                   o.userid,
                                   o.waiver,
                                   o.waivedamount,
                                   o.waivedby,
                                   o.waivecomment,
                                   o.datewaived,
                                   o.CreationTime,
                                   o.LastModificationTime,
                                   Id = o.Id,
                                   Billofladingblno = s1 == null || s1.blno == null ? "" : s1.blno.ToString()
                               };

            var totalCount = await filteredInvoicedatas.CountAsync();

            var dbList = await invoicedatas.ToListAsync();
            var results = new List<GetInvoicedataForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoicedataForViewDto()
                {
                    Invoicedata = new InvoicedataDto
                    {

                        blno = o.blno,
                        validitydate = o.validitydate,
                        amount = o.amount,
                        amountdue = o.amountdue,
                        status = o.status,
                        invpaiddate = o.invpaiddate,
                        userid = o.userid,
                        waiver = o.waiver,
                        waivedamount = o.waivedamount,
                        waivedby = o.waivedby,
                        waivecomment = o.waivecomment,
                        datewaived = o.datewaived,
                        CreationTime = o.CreationTime,
                        LastModificationTime = o.LastModificationTime,
                        Id = o.Id,
                    },
                    Billofladingblno = o.Billofladingblno
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoicedataForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetInvoicedataForViewDto> GetInvoicedataForView(int id)
        {
            var invoicedata = await _invoicedataRepository.GetAsync(id);

            var output = new GetInvoicedataForViewDto { Invoicedata = ObjectMapper.Map<InvoicedataDto>(invoicedata) };

            if (output.Invoicedata.BillofladingId != null)
            {
                var _lookupBilloflading = await _lookup_billofladingRepository.FirstOrDefaultAsync((long)output.Invoicedata.BillofladingId);
                output.Billofladingblno = _lookupBilloflading?.blno?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Invoicedatas_Edit)]
        public async Task<GetInvoicedataForEditOutput> GetInvoicedataForEdit(EntityDto input)
        {
            var invoicedata = await _invoicedataRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvoicedataForEditOutput { Invoicedata = ObjectMapper.Map<CreateOrEditInvoicedataDto>(invoicedata) };

            if (output.Invoicedata.BillofladingId != null)
            {
                var _lookupBilloflading = await _lookup_billofladingRepository.FirstOrDefaultAsync((long)output.Invoicedata.BillofladingId);
                output.Billofladingblno = _lookupBilloflading?.blno?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvoicedataDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Invoicedatas_Create)]
        protected virtual async Task Create(CreateOrEditInvoicedataDto input)
        {
            var invoicedata = ObjectMapper.Map<Invoicedata>(input);

            if (AbpSession.TenantId != null)
            {
                invoicedata.TenantId = (int?)AbpSession.TenantId;
            }

            await _invoicedataRepository.InsertAsync(invoicedata);

        }

        [AbpAuthorize(AppPermissions.Pages_Invoicedatas_Edit)]
        protected virtual async Task Update(CreateOrEditInvoicedataDto input)
        {
            var invoicedata = await _invoicedataRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, invoicedata);

        }

        [AbpAuthorize(AppPermissions.Pages_Invoicedatas_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _invoicedataRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetInvoicedatasToExcel(GetAllInvoicedatasForExcelInput input)
        {

            var filteredInvoicedatas = _invoicedataRepository.GetAll()
                        .Include(e => e.BillofladingFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.blno.Contains(input.Filter) || e.amount.Contains(input.Filter) || e.amountdue.Contains(input.Filter) || e.status.Contains(input.Filter) || e.userid.Contains(input.Filter) || e.waiver.Contains(input.Filter) || e.waivedamount.Contains(input.Filter) || e.waivedby.Contains(input.Filter) || e.waivecomment.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(input.MinvaliditydateFilter != null, e => e.validitydate >= input.MinvaliditydateFilter)
                        .WhereIf(input.MaxvaliditydateFilter != null, e => e.validitydate <= input.MaxvaliditydateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountFilter), e => e.amount == input.amountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.amountdueFilter), e => e.amountdue == input.amountdueFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(input.MininvpaiddateFilter != null, e => e.invpaiddate >= input.MininvpaiddateFilter)
                        .WhereIf(input.MaxinvpaiddateFilter != null, e => e.invpaiddate <= input.MaxinvpaiddateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.useridFilter), e => e.userid == input.useridFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waiverFilter), e => e.waiver == input.waiverFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waivedamountFilter), e => e.waivedamount == input.waivedamountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waivedbyFilter), e => e.waivedby == input.waivedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.waivecommentFilter), e => e.waivecomment == input.waivecommentFilter)
                        .WhereIf(input.MindatewaivedFilter != null, e => e.datewaived >= input.MindatewaivedFilter)
                        .WhereIf(input.MaxdatewaivedFilter != null, e => e.datewaived <= input.MaxdatewaivedFilter)
                        .WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
                        .WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
                        .WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
                        .WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BillofladingblnoFilter), e => e.BillofladingFk != null && e.BillofladingFk.blno == input.BillofladingblnoFilter);

            var query = (from o in filteredInvoicedatas
                         join o1 in _lookup_billofladingRepository.GetAll() on o.BillofladingId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetInvoicedataForViewDto()
                         {
                             Invoicedata = new InvoicedataDto
                             {
                                 blno = o.blno,
                                 validitydate = o.validitydate,
                                 amount = o.amount,
                                 amountdue = o.amountdue,
                                 status = o.status,
                                 invpaiddate = o.invpaiddate,
                                 userid = o.userid,
                                 waiver = o.waiver,
                                 waivedamount = o.waivedamount,
                                 waivedby = o.waivedby,
                                 waivecomment = o.waivecomment,
                                 datewaived = o.datewaived,
                                 CreationTime = o.CreationTime,
                                 LastModificationTime = o.LastModificationTime,
                                 Id = o.Id
                             },
                             Billofladingblno = s1 == null || s1.blno == null ? "" : s1.blno.ToString()
                         });

            var invoicedataListDtos = await query.ToListAsync();

            return _invoicedatasExcelExporter.ExportToFile(invoicedataListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Invoicedatas)]
        public async Task<List<InvoicedataBillofladingLookupTableDto>> GetAllBillofladingForTableDropdown()
        {
            return await _lookup_billofladingRepository.GetAll()
                .Select(billoflading => new InvoicedataBillofladingLookupTableDto
                {
                    Id = billoflading.Id,
                    DisplayName = billoflading == null || billoflading.blno == null ? "" : billoflading.blno.ToString()
                }).ToListAsync();
        }

    }
}