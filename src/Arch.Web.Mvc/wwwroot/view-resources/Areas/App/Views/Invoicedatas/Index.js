(function () {
  $(function () {
    var _$invoicedatasTable = $('#InvoicedatasTable');
    var _invoicedatasService = abp.services.app.invoicedatas;
    var _entityTypeFullName = 'Arch.Invoicedatas.Invoicedata';

    var $selectedDate = {
      startDate: null,
      endDate: null,
    };

    $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
      $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('.startDate')
      .daterangepicker(
        {
          autoUpdateInput: false,
          singleDatePicker: true,
          locale: abp.localization.currentLanguage.name,
          format: 'L',
        },
        (date) => {
          $selectedDate.startDate = date;
        }
      )
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
      });

    $('.endDate')
      .daterangepicker(
        {
          autoUpdateInput: false,
          singleDatePicker: true,
          locale: abp.localization.currentLanguage.name,
          format: 'L',
        },
        (date) => {
          $selectedDate.endDate = date;
        }
      )
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.Invoicedatas.Create'),
      edit: abp.auth.hasPermission('Pages.Invoicedatas.Edit'),
      delete: abp.auth.hasPermission('Pages.Invoicedatas.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Invoicedatas/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Invoicedatas/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditInvoicedataModal',
    });

    var _viewInvoicedataModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Invoicedatas/ViewinvoicedataModal',
      modalClass: 'ViewInvoicedataModal',
    });

    var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();
    function entityHistoryIsEnabled() {
      return (
        abp.auth.hasPermission('Pages.Administration.AuditLogs') &&
        abp.custom.EntityHistory &&
        abp.custom.EntityHistory.IsEnabled &&
        _.filter(abp.custom.EntityHistory.EnabledEntities, (entityType) => entityType === _entityTypeFullName)
          .length === 1
      );
    }

    var getDateFilter = function (element) {
      if ($selectedDate.startDate == null) {
        return null;
      }
      return $selectedDate.startDate.format('YYYY-MM-DDT00:00:00Z');
    };

    var getMaxDateFilter = function (element) {
      if ($selectedDate.endDate == null) {
        return null;
      }
      return $selectedDate.endDate.format('YYYY-MM-DDT23:59:59Z');
    };

    var dataTable = _$invoicedatasTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _invoicedatasService.getAll,
        inputFilter: function () {
          return {
            filter: $('#InvoicedatasTableFilter').val(),
            blnoFilter: $('#blnoFilterId').val(),
            minvaliditydateFilter: getDateFilter($('#MinvaliditydateFilterId')),
            maxvaliditydateFilter: getMaxDateFilter($('#MaxvaliditydateFilterId')),
            amountFilter: $('#amountFilterId').val(),
            amountdueFilter: $('#amountdueFilterId').val(),
            statusFilter: $('#statusFilterId').val(),
            mininvpaiddateFilter: getDateFilter($('#MininvpaiddateFilterId')),
            maxinvpaiddateFilter: getMaxDateFilter($('#MaxinvpaiddateFilterId')),
            useridFilter: $('#useridFilterId').val(),
            waiverFilter: $('#waiverFilterId').val(),
            waivedamountFilter: $('#waivedamountFilterId').val(),
            waivedbyFilter: $('#waivedbyFilterId').val(),
            waivecommentFilter: $('#waivecommentFilterId').val(),
            mindatewaivedFilter: getDateFilter($('#MindatewaivedFilterId')),
            maxdatewaivedFilter: getMaxDateFilter($('#MaxdatewaivedFilterId')),
            minCreationTimeFilter: getDateFilter($('#MinCreationTimeFilterId')),
            maxCreationTimeFilter: getMaxDateFilter($('#MaxCreationTimeFilterId')),
            minLastModificationTimeFilter: getDateFilter($('#MinLastModificationTimeFilterId')),
            maxLastModificationTimeFilter: getMaxDateFilter($('#MaxLastModificationTimeFilterId')),
            billofladingblnoFilter: $('#BillofladingblnoFilterId').val(),
          };
        },
      },
      columnDefs: [
        {
          className: 'control responsive',
          orderable: false,
          render: function () {
            return '';
          },
          targets: 0,
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
                  _viewInvoicedataModal.open({ id: data.record.invoicedata.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.invoicedata.id });
                },
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
                    entityId: data.record.invoicedata.id,
                  });
                },
              },
              {
                text: app.localize('Delete'),
                iconStyle: 'far fa-trash-alt mr-2',
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteInvoicedata(data.record.invoicedata);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'invoicedata.blno',
          name: 'blno',
        },
        {
          targets: 3,
          data: 'invoicedata.validitydate',
          name: 'validitydate',
          render: function (validitydate) {
            if (validitydate) {
              return moment(validitydate).format('L');
            }
            return '';
          },
        },
        {
          targets: 4,
          data: 'invoicedata.amount',
          name: 'amount',
        },
        {
          targets: 5,
          data: 'invoicedata.amountdue',
          name: 'amountdue',
        },
        {
          targets: 6,
          data: 'invoicedata.status',
          name: 'status',
        },
        {
          targets: 7,
          data: 'invoicedata.invpaiddate',
          name: 'invpaiddate',
          render: function (invpaiddate) {
            if (invpaiddate) {
              return moment(invpaiddate).format('L');
            }
            return '';
          },
        },
        {
          targets: 8,
          data: 'invoicedata.userid',
          name: 'userid',
        },
        {
          targets: 9,
          data: 'invoicedata.waiver',
          name: 'waiver',
        },
        {
          targets: 10,
          data: 'invoicedata.waivedamount',
          name: 'waivedamount',
        },
        {
          targets: 11,
          data: 'invoicedata.waivedby',
          name: 'waivedby',
        },
        {
          targets: 12,
          data: 'invoicedata.waivecomment',
          name: 'waivecomment',
        },
        {
          targets: 13,
          data: 'invoicedata.datewaived',
          name: 'datewaived',
          render: function (datewaived) {
            if (datewaived) {
              return moment(datewaived).format('L');
            }
            return '';
          },
        },
        {
          targets: 14,
          data: 'invoicedata.creationTime',
          name: 'creationTime',
          render: function (creationTime) {
            if (creationTime) {
              return moment(creationTime).format('L');
            }
            return '';
          },
        },
        {
          targets: 15,
          data: 'invoicedata.lastModificationTime',
          name: 'lastModificationTime',
          render: function (lastModificationTime) {
            if (lastModificationTime) {
              return moment(lastModificationTime).format('L');
            }
            return '';
          },
        },
        {
          targets: 16,
          data: 'billofladingblno',
          name: 'billofladingFk.blno',
        },
      ],
    });

    function getInvoicedatas() {
      dataTable.ajax.reload();
    }

    function deleteInvoicedata(invoicedata) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _invoicedatasService
            .delete({
              id: invoicedata.id,
            })
            .done(function () {
              getInvoicedatas(true);
              abp.notify.success(app.localize('SuccessfullyDeleted'));
            });
        }
      });
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

    $('#CreateNewInvoicedataButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _invoicedatasService
        .getInvoicedatasToExcel({
          filter: $('#InvoicedatasTableFilter').val(),
          blnoFilter: $('#blnoFilterId').val(),
          minvaliditydateFilter: getDateFilter($('#MinvaliditydateFilterId')),
          maxvaliditydateFilter: getMaxDateFilter($('#MaxvaliditydateFilterId')),
          amountFilter: $('#amountFilterId').val(),
          amountdueFilter: $('#amountdueFilterId').val(),
          statusFilter: $('#statusFilterId').val(),
          mininvpaiddateFilter: getDateFilter($('#MininvpaiddateFilterId')),
          maxinvpaiddateFilter: getMaxDateFilter($('#MaxinvpaiddateFilterId')),
          useridFilter: $('#useridFilterId').val(),
          waiverFilter: $('#waiverFilterId').val(),
          waivedamountFilter: $('#waivedamountFilterId').val(),
          waivedbyFilter: $('#waivedbyFilterId').val(),
          waivecommentFilter: $('#waivecommentFilterId').val(),
          mindatewaivedFilter: getDateFilter($('#MindatewaivedFilterId')),
          maxdatewaivedFilter: getMaxDateFilter($('#MaxdatewaivedFilterId')),
          minCreationTimeFilter: getDateFilter($('#MinCreationTimeFilterId')),
          maxCreationTimeFilter: getMaxDateFilter($('#MaxCreationTimeFilterId')),
          minLastModificationTimeFilter: getDateFilter($('#MinLastModificationTimeFilterId')),
          maxLastModificationTimeFilter: getMaxDateFilter($('#MaxLastModificationTimeFilterId')),
          billofladingblnoFilter: $('#BillofladingblnoFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditInvoicedataModalSaved', function () {
      getInvoicedatas();
    });

    $('#GetInvoicedatasButton').click(function (e) {
      e.preventDefault();
      getInvoicedatas();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getInvoicedatas();
      }
    });
  });
})();
