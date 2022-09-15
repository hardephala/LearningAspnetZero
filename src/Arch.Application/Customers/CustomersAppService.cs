using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Arch.Customers.Exporting;
using Arch.Customers.Dtos;
using Arch.Dto;
using Abp.Application.Services.Dto;
using Arch.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Arch.Storage;

namespace Arch.Customers
{
    [AbpAuthorize(AppPermissions.Pages_Customers)]
    public class CustomersAppService : ArchAppServiceBase, ICustomersAppService
    {
        private readonly IRepository<Customer, long> _customerRepository;
        private readonly ICustomersExcelExporter _customersExcelExporter;

        public CustomersAppService(IRepository<Customer, long> customerRepository, ICustomersExcelExporter customersExcelExporter)
        {
            _customerRepository = customerRepository;
            _customersExcelExporter = customersExcelExporter;

        }

        public async Task<PagedResultDto<GetCustomerForViewDto>> GetAll(GetAllCustomersInput input)
        {

            var filteredCustomers = _customerRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.customerrole.Contains(input.Filter) || e.customercode.Contains(input.Filter) || e.customername.Contains(input.Filter) || e.customergroupcode.Contains(input.Filter) || e.customergroupname.Contains(input.Filter) || e.primaryemail.Contains(input.Filter) || e.altemail.Contains(input.Filter) || e.phonenumber.Contains(input.Filter) || e.accounttype.Contains(input.Filter) || e.linkedcode.Contains(input.Filter) || e.password.Contains(input.Filter) || e.status.Contains(input.Filter) || e.notes.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customerroleFilter), e => e.customerrole == input.customerroleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customercodeFilter), e => e.customercode == input.customercodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customernameFilter), e => e.customername == input.customernameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customergroupcodeFilter), e => e.customergroupcode == input.customergroupcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customergroupnameFilter), e => e.customergroupname == input.customergroupnameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.primaryemailFilter), e => e.primaryemail == input.primaryemailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.altemailFilter), e => e.altemail == input.altemailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.phonenumberFilter), e => e.phonenumber == input.phonenumberFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.accounttypeFilter), e => e.accounttype == input.accounttypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.linkedcodeFilter), e => e.linkedcode == input.linkedcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.passwordFilter), e => e.password == input.passwordFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.notesFilter), e => e.notes == input.notesFilter);

            var pagedAndFilteredCustomers = filteredCustomers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var customers = from o in pagedAndFilteredCustomers
                            select new
                            {

                                o.customerrole,
                                o.customercode,
                                o.customername,
                                o.customergroupcode,
                                o.customergroupname,
                                o.primaryemail,
                                o.altemail,
                                o.phonenumber,
                                o.accounttype,
                                o.linkedcode,
                                o.password,
                                o.status,
                                o.notes,
                                Id = o.Id
                            };

            var totalCount = await filteredCustomers.CountAsync();

            var dbList = await customers.ToListAsync();
            var results = new List<GetCustomerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCustomerForViewDto()
                {
                    Customer = new CustomerDto
                    {

                        customerrole = o.customerrole,
                        customercode = o.customercode,
                        customername = o.customername,
                        customergroupcode = o.customergroupcode,
                        customergroupname = o.customergroupname,
                        primaryemail = o.primaryemail,
                        altemail = o.altemail,
                        phonenumber = o.phonenumber,
                        accounttype = o.accounttype,
                        linkedcode = o.linkedcode,
                        password = o.password,
                        status = o.status,
                        notes = o.notes,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCustomerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCustomerForViewDto> GetCustomerForView(long id)
        {
            var customer = await _customerRepository.GetAsync(id);

            var output = new GetCustomerForViewDto { Customer = ObjectMapper.Map<CustomerDto>(customer) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Edit)]
        public async Task<GetCustomerForEditOutput> GetCustomerForEdit(EntityDto<long> input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCustomerForEditOutput { Customer = ObjectMapper.Map<CreateOrEditCustomerDto>(customer) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCustomerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Create)]
        protected virtual async Task Create(CreateOrEditCustomerDto input)
        {
            var customer = ObjectMapper.Map<Customer>(input);

            if (AbpSession.TenantId != null)
            {
                customer.TenantId = (int?)AbpSession.TenantId;
            }

            await _customerRepository.InsertAsync(customer);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Edit)]
        protected virtual async Task Update(CreateOrEditCustomerDto input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, customer);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _customerRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCustomersToExcel(GetAllCustomersForExcelInput input)
        {

            var filteredCustomers = _customerRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.customerrole.Contains(input.Filter) || e.customercode.Contains(input.Filter) || e.customername.Contains(input.Filter) || e.customergroupcode.Contains(input.Filter) || e.customergroupname.Contains(input.Filter) || e.primaryemail.Contains(input.Filter) || e.altemail.Contains(input.Filter) || e.phonenumber.Contains(input.Filter) || e.accounttype.Contains(input.Filter) || e.linkedcode.Contains(input.Filter) || e.password.Contains(input.Filter) || e.status.Contains(input.Filter) || e.notes.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customerroleFilter), e => e.customerrole == input.customerroleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customercodeFilter), e => e.customercode == input.customercodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customernameFilter), e => e.customername == input.customernameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customergroupcodeFilter), e => e.customergroupcode == input.customergroupcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.customergroupnameFilter), e => e.customergroupname == input.customergroupnameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.primaryemailFilter), e => e.primaryemail == input.primaryemailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.altemailFilter), e => e.altemail == input.altemailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.phonenumberFilter), e => e.phonenumber == input.phonenumberFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.accounttypeFilter), e => e.accounttype == input.accounttypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.linkedcodeFilter), e => e.linkedcode == input.linkedcodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.passwordFilter), e => e.password == input.passwordFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.statusFilter), e => e.status == input.statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.notesFilter), e => e.notes == input.notesFilter);

            var query = (from o in filteredCustomers
                         select new GetCustomerForViewDto()
                         {
                             Customer = new CustomerDto
                             {
                                 customerrole = o.customerrole,
                                 customercode = o.customercode,
                                 customername = o.customername,
                                 customergroupcode = o.customergroupcode,
                                 customergroupname = o.customergroupname,
                                 primaryemail = o.primaryemail,
                                 altemail = o.altemail,
                                 phonenumber = o.phonenumber,
                                 accounttype = o.accounttype,
                                 linkedcode = o.linkedcode,
                                 password = o.password,
                                 status = o.status,
                                 notes = o.notes,
                                 Id = o.Id
                             }
                         });

            var customerListDtos = await query.ToListAsync();

            return _customersExcelExporter.ExportToFile(customerListDtos);
        }

    }
}