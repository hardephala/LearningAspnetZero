(function () {
    $(function () {

        var _$customersTable = $('#CustomersTable');
        var _customersService = abp.services.app.customers;
		var _entityTypeFullName = 'Arch.Customers.Customer';
        
       var $selectedDate = {
            startDate: null,
            endDate: null,
        }

        $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('.startDate').daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L',
        }, (date) => {
            $selectedDate.startDate = date;
        }).on('cancel.daterangepicker', function (ev, picker) {
            $(this).val("");
            $selectedDate.startDate = null;
        });

        $('.endDate').daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: abp.localization.currentLanguage.name,
            format: 'L',
        }, (date) => {
            $selectedDate.endDate = date;
        }).on('cancel.daterangepicker', function (ev, picker) {
            $(this).val("");
            $selectedDate.endDate = null;
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.Customers.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.Customers.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.Customers.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Customers/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Customers/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditCustomerModal'
                });
                   

		 var _viewCustomerModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Customers/ViewcustomerModal',
            modalClass: 'ViewCustomerModal'
        });

		        var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();
		        function entityHistoryIsEnabled() {
            return abp.auth.hasPermission('Pages.Administration.AuditLogs') &&
                abp.custom.EntityHistory &&
                abp.custom.EntityHistory.IsEnabled &&
                _.filter(abp.custom.EntityHistory.EnabledEntities, entityType => entityType === _entityTypeFullName).length === 1;
        }

        var getDateFilter = function (element) {
            if ($selectedDate.startDate == null) {
                return null;
            }
            return $selectedDate.startDate.format("YYYY-MM-DDT00:00:00Z"); 
        }
        
        var getMaxDateFilter = function (element) {
            if ($selectedDate.endDate == null) {
                return null;
            }
            return $selectedDate.endDate.format("YYYY-MM-DDT23:59:59Z"); 
        }

        var dataTable = _$customersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _customersService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#CustomersTableFilter').val(),
					customerroleFilter: $('#customerroleFilterId').val(),
					customercodeFilter: $('#customercodeFilterId').val(),
					customernameFilter: $('#customernameFilterId').val(),
					customergroupcodeFilter: $('#customergroupcodeFilterId').val(),
					customergroupnameFilter: $('#customergroupnameFilterId').val(),
					primaryemailFilter: $('#primaryemailFilterId').val(),
					altemailFilter: $('#altemailFilterId').val(),
					phonenumberFilter: $('#phonenumberFilterId').val(),
					accounttypeFilter: $('#accounttypeFilterId').val(),
					linkedcodeFilter: $('#linkedcodeFilterId').val(),
					passwordFilter: $('#passwordFilterId').val(),
					statusFilter: $('#statusFilterId').val(),
					notesFilter: $('#notesFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                                text: app.localize('View'),
                                iconStyle: 'far fa-eye mr-2',
                                action: function (data) {
                                    _viewCustomerModal.open({ id: data.record.customer.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.customer.id });                                
                            }
                        },
                        {
                            text: app.localize('History'),
                            iconStyle: 'fas fa-history mr-2',
                            visible: function () {
                                return entityHistoryIsEnabled();
                            },
                            action: function (data) {
                                _entityTypeHistoryModal.open({
                                    entityTypeFullName: _entityTypeFullName,
                                    entityId: data.record.customer.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteCustomer(data.record.customer);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "customer.customerrole",
						 name: "customerrole"   
					},
					{
						targets: 3,
						 data: "customer.customercode",
						 name: "customercode"   
					},
					{
						targets: 4,
						 data: "customer.customername",
						 name: "customername"   
					},
					{
						targets: 5,
						 data: "customer.customergroupcode",
						 name: "customergroupcode"   
					},
					{
						targets: 6,
						 data: "customer.customergroupname",
						 name: "customergroupname"   
					},
					{
						targets: 7,
						 data: "customer.primaryemail",
						 name: "primaryemail"   
					},
					{
						targets: 8,
						 data: "customer.altemail",
						 name: "altemail"   
					},
					{
						targets: 9,
						 data: "customer.phonenumber",
						 name: "phonenumber"   
					},
					{
						targets: 10,
						 data: "customer.accounttype",
						 name: "accounttype"   
					},
					{
						targets: 11,
						 data: "customer.linkedcode",
						 name: "linkedcode"   
					},
					{
						targets: 12,
						 data: "customer.password",
						 name: "password"   
					},
					{
						targets: 13,
						 data: "customer.status",
						 name: "status"   
					},
					{
						targets: 14,
						 data: "customer.notes",
						 name: "notes"   
					}
            ]
        });

        function getCustomers() {
            dataTable.ajax.reload();
        }

        function deleteCustomer(customer) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _customersService.delete({
                            id: customer.id
                        }).done(function () {
                            getCustomers(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewCustomerButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _customersService
                .getCustomersToExcel({
				filter : $('#CustomersTableFilter').val(),
					customerroleFilter: $('#customerroleFilterId').val(),
					customercodeFilter: $('#customercodeFilterId').val(),
					customernameFilter: $('#customernameFilterId').val(),
					customergroupcodeFilter: $('#customergroupcodeFilterId').val(),
					customergroupnameFilter: $('#customergroupnameFilterId').val(),
					primaryemailFilter: $('#primaryemailFilterId').val(),
					altemailFilter: $('#altemailFilterId').val(),
					phonenumberFilter: $('#phonenumberFilterId').val(),
					accounttypeFilter: $('#accounttypeFilterId').val(),
					linkedcodeFilter: $('#linkedcodeFilterId').val(),
					passwordFilter: $('#passwordFilterId').val(),
					statusFilter: $('#statusFilterId').val(),
					notesFilter: $('#notesFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditCustomerModalSaved', function () {
            getCustomers();
        });

		$('#GetCustomersButton').click(function (e) {
            e.preventDefault();
            getCustomers();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getCustomers();
		  }
		});
		
		
		
    });
})();
