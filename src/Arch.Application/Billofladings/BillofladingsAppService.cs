using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.Billofladings.Exporting;
using Arch.Billofladings.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.Billofladings
{
    [AbpAuthorize(AppPermissions.Pages_Billofladings)]
    public class BillofladingsAppService : ArchAppServiceBase, IBillofladingsAppService
    {
        private readonly IRepository<Billoflading, long> _billofladingRepository;
        private readonly IBillofladingsExcelExporter _billofladingsExcelExporter;

        public BillofladingsAppService(IRepository<Billoflading, long> billofladingRepository, IBillofladingsExcelExporter billofladingsExcelExporter)
        {
            _billofladingRepository = billofladingRepository;
            _billofladingsExcelExporter = billofladingsExcelExporter;

        }

        public async Task<PagedResultDto<GetBillofladingForViewDto>> GetAll(GetAllBillofladingsInput input)
        {

            var filteredBillofladings = _billofladingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.shipmentno.Contains(input.Filter) || e.blno.Contains(input.Filter) || e.equipmentno.Contains(input.Filter) || e.equipmenttype.Contains(input.Filter) || e.equipmentsize.Contains(input.Filter) || e.shipperowned.Contains(input.Filter) || e.shipoperator.Contains(input.Filter) || e.servicecontract.Contains(input.Filter) || e.spotbooking.Contains(input.Filter) || e.consigneecode.Contains(input.Filter) || e.dischargeportcode.Contains(input.Filter) || e.dischargeportname.Contains(input.Filter) || e.placeofdeliverycode.Contains(input.Filter) || e.placeofdeliveryname.Contains(input.Filter) || e.finalvesselcode.Contains(input.Filter) || e.finalvesselname.Contains(input.Filter) || e.finalvesselvoyage.Contains(input.Filter) || e.partpartstatus.Contains(input.Filter) || e.partpartref.Contains(input.Filter) || e.depositpayable.Contains(input.Filter) || e.depositwaivedamount.Contains(input.Filter) || e.depositwaivedreason.Contains(input.Filter) || e.depositwaivedby.Contains(input.Filter) || e.depositpaymentstatus.Contains(input.Filter) || e.releaseoutstandingstatus.Contains(input.Filter) || e.releaseoutstandingreason.Contains(input.Filter) || e.releaseoutstandingby.Contains(input.Filter) || e.releaselongstandingstatus.Contains(input.Filter) || e.releaselongstandingreason.Contains(input.Filter) || e.releaselongstandingby.Contains(input.Filter) || e.blnotype.Contains(input.Filter) || e.blnosubmitstatus.Contains(input.Filter) || e.blnosubmittedto.Contains(input.Filter) || e.blnosubmittedby.Contains(input.Filter) || e.blnosubmitref.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipmentnoFilter), e => e.shipmentno == input.shipmentnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.equipmentnoFilter), e => e.equipmentno == input.equipmentnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.equipmenttypeFilter), e => e.equipmenttype == input.equipmenttypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.equipmentsizeFilter), e => e.equipmentsize == input.equipmentsizeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipperownedFilter), e => e.shipperowned == input.shipperownedFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipoperatorFilter), e => e.shipoperator == input.shipoperatorFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.servicecontractFilter), e => e.servicecontract == input.servicecontractFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.spotbookingFilter), e => e.spotbooking == input.spotbookingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.consigneecodeFilter), e => e.consigneecode == input.consigneecodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.dischargeportcodeFilter), e => e.dischargeportcode == input.dischargeportcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.dischargeportnameFilter), e => e.dischargeportname == input.dischargeportnameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.placeofdeliverycodeFilter), e => e.placeofdeliverycode == input.placeofdeliverycodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.placeofdeliverynameFilter), e => e.placeofdeliveryname == input.placeofdeliverynameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.finalvesselcodeFilter), e => e.finalvesselcode == input.finalvesselcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.finalvesselnameFilter), e => e.finalvesselname == input.finalvesselnameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.finalvesselvoyageFilter), e => e.finalvesselvoyage == input.finalvesselvoyageFilter)
                        .WhereIf(input.MinfinalvesseletaFilter != null, e => e.finalvesseleta >= input.MinfinalvesseletaFilter)
                        .WhereIf(input.MaxfinalvesseletaFilter != null, e => e.finalvesseleta <= input.MaxfinalvesseletaFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.partpartstatusFilter), e => e.partpartstatus == input.partpartstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.partpartrefFilter), e => e.partpartref == input.partpartrefFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositpayableFilter), e => e.depositpayable == input.depositpayableFilter)
                        .WhereIf(input.MindepositdueamountFilter != null, e => e.depositdueamount >= input.MindepositdueamountFilter)
                        .WhereIf(input.MaxdepositdueamountFilter != null, e => e.depositdueamount <= input.MaxdepositdueamountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositwaivedamountFilter), e => e.depositwaivedamount == input.depositwaivedamountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositwaivedreasonFilter), e => e.depositwaivedreason == input.depositwaivedreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositwaivedbyFilter), e => e.depositwaivedby == input.depositwaivedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositpaymentstatusFilter), e => e.depositpaymentstatus == input.depositpaymentstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaseoutstandingstatusFilter), e => e.releaseoutstandingstatus == input.releaseoutstandingstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaseoutstandingreasonFilter), e => e.releaseoutstandingreason == input.releaseoutstandingreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaseoutstandingbyFilter), e => e.releaseoutstandingby == input.releaseoutstandingbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaselongstandingstatusFilter), e => e.releaselongstandingstatus == input.releaselongstandingstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaselongstandingreasonFilter), e => e.releaselongstandingreason == input.releaselongstandingreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaselongstandingbyFilter), e => e.releaselongstandingby == input.releaselongstandingbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnotypeFilter), e => e.blnotype == input.blnotypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmitstatusFilter), e => e.blnosubmitstatus == input.blnosubmitstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmittedtoFilter), e => e.blnosubmittedto == input.blnosubmittedtoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmittedbyFilter), e => e.blnosubmittedby == input.blnosubmittedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmitrefFilter), e => e.blnosubmitref == input.blnosubmitrefFilter);

            var pagedAndFilteredBillofladings = filteredBillofladings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var billofladings = from o in pagedAndFilteredBillofladings
                                select new
                                {

                                    o.shipmentno,
                                    o.blno,
                                    o.equipmentno,
                                    o.equipmenttype,
                                    o.equipmentsize,
                                    o.shipperowned,
                                    o.shipoperator,
                                    o.servicecontract,
                                    o.spotbooking,
                                    o.consigneecode,
                                    o.dischargeportcode,
                                    o.dischargeportname,
                                    o.placeofdeliverycode,
                                    o.placeofdeliveryname,
                                    o.finalvesselcode,
                                    o.finalvesselname,
                                    o.finalvesselvoyage,
                                    o.finalvesseleta,
                                    o.partpartstatus,
                                    o.partpartref,
                                    o.depositpayable,
                                    o.depositdueamount,
                                    o.depositwaivedamount,
                                    o.depositwaivedreason,
                                    o.depositwaivedby,
                                    o.depositpaymentstatus,
                                    o.releaseoutstandingstatus,
                                    o.releaseoutstandingreason,
                                    o.releaseoutstandingby,
                                    o.releaselongstandingstatus,
                                    o.releaselongstandingreason,
                                    o.releaselongstandingby,
                                    o.blnotype,
                                    o.blnosubmitstatus,
                                    o.blnosubmittedto,
                                    o.blnosubmittedby,
                                    o.blnosubmitref,
                                    Id = o.Id
                                };

            var totalCount = await filteredBillofladings.CountAsync();

            var dbList = await billofladings.ToListAsync();
            var results = new List<GetBillofladingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetBillofladingForViewDto()
                {
                    Billoflading = new BillofladingDto
                    {

                        shipmentno = o.shipmentno,
                        blno = o.blno,
                        equipmentno = o.equipmentno,
                        equipmenttype = o.equipmenttype,
                        equipmentsize = o.equipmentsize,
                        shipperowned = o.shipperowned,
                        shipoperator = o.shipoperator,
                        servicecontract = o.servicecontract,
                        spotbooking = o.spotbooking,
                        consigneecode = o.consigneecode,
                        dischargeportcode = o.dischargeportcode,
                        dischargeportname = o.dischargeportname,
                        placeofdeliverycode = o.placeofdeliverycode,
                        placeofdeliveryname = o.placeofdeliveryname,
                        finalvesselcode = o.finalvesselcode,
                        finalvesselname = o.finalvesselname,
                        finalvesselvoyage = o.finalvesselvoyage,
                        finalvesseleta = o.finalvesseleta,
                        partpartstatus = o.partpartstatus,
                        partpartref = o.partpartref,
                        depositpayable = o.depositpayable,
                        depositdueamount = o.depositdueamount,
                        depositwaivedamount = o.depositwaivedamount,
                        depositwaivedreason = o.depositwaivedreason,
                        depositwaivedby = o.depositwaivedby,
                        depositpaymentstatus = o.depositpaymentstatus,
                        releaseoutstandingstatus = o.releaseoutstandingstatus,
                        releaseoutstandingreason = o.releaseoutstandingreason,
                        releaseoutstandingby = o.releaseoutstandingby,
                        releaselongstandingstatus = o.releaselongstandingstatus,
                        releaselongstandingreason = o.releaselongstandingreason,
                        releaselongstandingby = o.releaselongstandingby,
                        blnotype = o.blnotype,
                        blnosubmitstatus = o.blnosubmitstatus,
                        blnosubmittedto = o.blnosubmittedto,
                        blnosubmittedby = o.blnosubmittedby,
                        blnosubmitref = o.blnosubmitref,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetBillofladingForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetBillofladingForViewDto> GetBillofladingForView(long id)
        {
            var billoflading = await _billofladingRepository.GetAsync(id);

            var output = new GetBillofladingForViewDto { Billoflading = ObjectMapper.Map<BillofladingDto>(billoflading) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Billofladings_Edit)]
        public async Task<GetBillofladingForEditOutput> GetBillofladingForEdit(EntityDto<long> input)
        {
            var billoflading = await _billofladingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBillofladingForEditOutput { Billoflading = ObjectMapper.Map<CreateOrEditBillofladingDto>(billoflading) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditBillofladingDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Billofladings_Create)]
        protected virtual async Task Create(CreateOrEditBillofladingDto input)
        {
            var billoflading = ObjectMapper.Map<Billoflading>(input);

            if (AbpSession.TenantId != null)
            {
                billoflading.TenantId = (int?)AbpSession.TenantId;
            }

            await _billofladingRepository.InsertAsync(billoflading);

        }

        [AbpAuthorize(AppPermissions.Pages_Billofladings_Edit)]
        protected virtual async Task Update(CreateOrEditBillofladingDto input)
        {
            var billoflading = await _billofladingRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, billoflading);

        }

        [AbpAuthorize(AppPermissions.Pages_Billofladings_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _billofladingRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetBillofladingsToExcel(GetAllBillofladingsForExcelInput input)
        {

            var filteredBillofladings = _billofladingRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.shipmentno.Contains(input.Filter) || e.blno.Contains(input.Filter) || e.equipmentno.Contains(input.Filter) || e.equipmenttype.Contains(input.Filter) || e.equipmentsize.Contains(input.Filter) || e.shipperowned.Contains(input.Filter) || e.shipoperator.Contains(input.Filter) || e.servicecontract.Contains(input.Filter) || e.spotbooking.Contains(input.Filter) || e.consigneecode.Contains(input.Filter) || e.dischargeportcode.Contains(input.Filter) || e.dischargeportname.Contains(input.Filter) || e.placeofdeliverycode.Contains(input.Filter) || e.placeofdeliveryname.Contains(input.Filter) || e.finalvesselcode.Contains(input.Filter) || e.finalvesselname.Contains(input.Filter) || e.finalvesselvoyage.Contains(input.Filter) || e.partpartstatus.Contains(input.Filter) || e.partpartref.Contains(input.Filter) || e.depositpayable.Contains(input.Filter) || e.depositwaivedamount.Contains(input.Filter) || e.depositwaivedreason.Contains(input.Filter) || e.depositwaivedby.Contains(input.Filter) || e.depositpaymentstatus.Contains(input.Filter) || e.releaseoutstandingstatus.Contains(input.Filter) || e.releaseoutstandingreason.Contains(input.Filter) || e.releaseoutstandingby.Contains(input.Filter) || e.releaselongstandingstatus.Contains(input.Filter) || e.releaselongstandingreason.Contains(input.Filter) || e.releaselongstandingby.Contains(input.Filter) || e.blnotype.Contains(input.Filter) || e.blnosubmitstatus.Contains(input.Filter) || e.blnosubmittedto.Contains(input.Filter) || e.blnosubmittedby.Contains(input.Filter) || e.blnosubmitref.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipmentnoFilter), e => e.shipmentno == input.shipmentnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnoFilter), e => e.blno == input.blnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.equipmentnoFilter), e => e.equipmentno == input.equipmentnoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.equipmenttypeFilter), e => e.equipmenttype == input.equipmenttypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.equipmentsizeFilter), e => e.equipmentsize == input.equipmentsizeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipperownedFilter), e => e.shipperowned == input.shipperownedFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.shipoperatorFilter), e => e.shipoperator == input.shipoperatorFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.servicecontractFilter), e => e.servicecontract == input.servicecontractFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.spotbookingFilter), e => e.spotbooking == input.spotbookingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.consigneecodeFilter), e => e.consigneecode == input.consigneecodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.dischargeportcodeFilter), e => e.dischargeportcode == input.dischargeportcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.dischargeportnameFilter), e => e.dischargeportname == input.dischargeportnameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.placeofdeliverycodeFilter), e => e.placeofdeliverycode == input.placeofdeliverycodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.placeofdeliverynameFilter), e => e.placeofdeliveryname == input.placeofdeliverynameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.finalvesselcodeFilter), e => e.finalvesselcode == input.finalvesselcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.finalvesselnameFilter), e => e.finalvesselname == input.finalvesselnameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.finalvesselvoyageFilter), e => e.finalvesselvoyage == input.finalvesselvoyageFilter)
                        .WhereIf(input.MinfinalvesseletaFilter != null, e => e.finalvesseleta >= input.MinfinalvesseletaFilter)
                        .WhereIf(input.MaxfinalvesseletaFilter != null, e => e.finalvesseleta <= input.MaxfinalvesseletaFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.partpartstatusFilter), e => e.partpartstatus == input.partpartstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.partpartrefFilter), e => e.partpartref == input.partpartrefFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositpayableFilter), e => e.depositpayable == input.depositpayableFilter)
                        .WhereIf(input.MindepositdueamountFilter != null, e => e.depositdueamount >= input.MindepositdueamountFilter)
                        .WhereIf(input.MaxdepositdueamountFilter != null, e => e.depositdueamount <= input.MaxdepositdueamountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositwaivedamountFilter), e => e.depositwaivedamount == input.depositwaivedamountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositwaivedreasonFilter), e => e.depositwaivedreason == input.depositwaivedreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositwaivedbyFilter), e => e.depositwaivedby == input.depositwaivedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.depositpaymentstatusFilter), e => e.depositpaymentstatus == input.depositpaymentstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaseoutstandingstatusFilter), e => e.releaseoutstandingstatus == input.releaseoutstandingstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaseoutstandingreasonFilter), e => e.releaseoutstandingreason == input.releaseoutstandingreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaseoutstandingbyFilter), e => e.releaseoutstandingby == input.releaseoutstandingbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaselongstandingstatusFilter), e => e.releaselongstandingstatus == input.releaselongstandingstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaselongstandingreasonFilter), e => e.releaselongstandingreason == input.releaselongstandingreasonFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.releaselongstandingbyFilter), e => e.releaselongstandingby == input.releaselongstandingbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnotypeFilter), e => e.blnotype == input.blnotypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmitstatusFilter), e => e.blnosubmitstatus == input.blnosubmitstatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmittedtoFilter), e => e.blnosubmittedto == input.blnosubmittedtoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmittedbyFilter), e => e.blnosubmittedby == input.blnosubmittedbyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.blnosubmitrefFilter), e => e.blnosubmitref == input.blnosubmitrefFilter);

            var query = (from o in filteredBillofladings
                         select new GetBillofladingForViewDto()
                         {
                             Billoflading = new BillofladingDto
                             {
                                 shipmentno = o.shipmentno,
                                 blno = o.blno,
                                 equipmentno = o.equipmentno,
                                 equipmenttype = o.equipmenttype,
                                 equipmentsize = o.equipmentsize,
                                 shipperowned = o.shipperowned,
                                 shipoperator = o.shipoperator,
                                 servicecontract = o.servicecontract,
                                 spotbooking = o.spotbooking,
                                 consigneecode = o.consigneecode,
                                 dischargeportcode = o.dischargeportcode,
                                 dischargeportname = o.dischargeportname,
                                 placeofdeliverycode = o.placeofdeliverycode,
                                 placeofdeliveryname = o.placeofdeliveryname,
                                 finalvesselcode = o.finalvesselcode,
                                 finalvesselname = o.finalvesselname,
                                 finalvesselvoyage = o.finalvesselvoyage,
                                 finalvesseleta = o.finalvesseleta,
                                 partpartstatus = o.partpartstatus,
                                 partpartref = o.partpartref,
                                 depositpayable = o.depositpayable,
                                 depositdueamount = o.depositdueamount,
                                 depositwaivedamount = o.depositwaivedamount,
                                 depositwaivedreason = o.depositwaivedreason,
                                 depositwaivedby = o.depositwaivedby,
                                 depositpaymentstatus = o.depositpaymentstatus,
                                 releaseoutstandingstatus = o.releaseoutstandingstatus,
                                 releaseoutstandingreason = o.releaseoutstandingreason,
                                 releaseoutstandingby = o.releaseoutstandingby,
                                 releaselongstandingstatus = o.releaselongstandingstatus,
                                 releaselongstandingreason = o.releaselongstandingreason,
                                 releaselongstandingby = o.releaselongstandingby,
                                 blnotype = o.blnotype,
                                 blnosubmitstatus = o.blnosubmitstatus,
                                 blnosubmittedto = o.blnosubmittedto,
                                 blnosubmittedby = o.blnosubmittedby,
                                 blnosubmitref = o.blnosubmitref,
                                 Id = o.Id
                             }
                         });

            var billofladingListDtos = await query.ToListAsync();

            return _billofladingsExcelExporter.ExportToFile(billofladingListDtos);
        }

    }
}