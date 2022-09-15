(function () {
  $(function () {
    var _$invoicesTable = $('#InvoicesTable');
    var _invoicesService = abp.services.app.invoices;

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
      create: abp.auth.hasPermission('Pages.Invoices.Create'),
      edit: abp.auth.hasPermission('Pages.Invoices.Edit'),
      delete: abp.auth.hasPermission('Pages.Invoices.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Invoices/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Invoices/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditInvoiceModal',
    });

    var _viewInvoiceModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Invoices/ViewinvoiceModal',
      modalClass: 'ViewInvoiceModal',
    });

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

    var dataTable = _$invoicesTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _invoicesService.getAll,
        inputFilter: function () {
          return {
            filter: $('#InvoicesTableFilter').val(),
            minvaliditydateFilter: getDateFilter($('#MinvaliditydateFilterId')),
            maxvaliditydateFilter: getMaxDateFilter($('#MaxvaliditydateFilterId')),
            blnoFilter: $('#blnoFilterId').val(),
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
                  _viewInvoiceModal.open({ id: data.record.invoice.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.invoice.id });
                },
              },
              {
                text: app.localize('Delete'),
                iconStyle: 'far fa-trash-alt mr-2',
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteInvoice(data.record.invoice);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'invoice.validitydate',
          name: 'validitydate',
          render: function (validitydate) {
            if (validitydate) {
              return moment(validitydate).format('L');
            }
            return '';
          },
        },
        {
          targets: 3,
          data: 'invoice.blno',
          name: 'blno',
        },
        {
          targets: 4,
          data: 'invoice.amount',
          name: 'amount',
        },
        {
          targets: 5,
          data: 'invoice.amountdue',
          name: 'amountdue',
        },
        {
          targets: 6,
          data: 'invoice.status',
          name: 'status',
        },
        {
          targets: 7,
          data: 'invoice.invpaiddate',
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
          data: 'invoice.userid',
          name: 'userid',
        },
        {
          targets: 9,
          data: 'invoice.waiver',
          name: 'waiver',
        },
        {
          targets: 10,
          data: 'invoice.waivedamount',
          name: 'waivedamount',
        },
        {
          targets: 11,
          data: 'invoice.waivedby',
          name: 'waivedby',
        },
        {
          targets: 12,
          data: 'invoice.waivecomment',
          name: 'waivecomment',
        },
        {
          targets: 13,
          data: 'invoice.datewaived',
          name: 'datewaived',
          render: function (datewaived) {
            if (datewaived) {
              return moment(datewaived).format('L');
            }
            return '';
          },
        },
      ],
    });

    function getInvoices() {
      dataTable.ajax.reload();
    }

    function deleteInvoice(invoice) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _invoicesService
            .delete({
              id: invoice.id,
            })
            .done(function () {
              getInvoices(true);
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

    $('#CreateNewInvoiceButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _invoicesService
        .getInvoicesToExcel({
          filter: $('#InvoicesTableFilter').val(),
          minvaliditydateFilter: getDateFilter($('#MinvaliditydateFilterId')),
          maxvaliditydateFilter: getMaxDateFilter($('#MaxvaliditydateFilterId')),
          blnoFilter: $('#blnoFilterId').val(),
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
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditInvoiceModalSaved', function () {
      getInvoices();
    });

    $('#GetInvoicesButton').click(function (e) {
      e.preventDefault();
      getInvoices();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getInvoices();
      }
    });
  });
})();
