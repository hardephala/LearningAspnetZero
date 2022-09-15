(function () {
  $(function () {
    var _$erInvoiceDatasTable = $('#ErInvoiceDatasTable');
    var _erInvoiceDatasService = abp.services.app.erInvoiceDatas;
    var _entityTypeFullName = 'Arch.ErInvoiceDatas.ErInvoiceData';

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
      create: abp.auth.hasPermission('Pages.ErInvoiceDatas.Create'),
      edit: abp.auth.hasPermission('Pages.ErInvoiceDatas.Edit'),
      delete: abp.auth.hasPermission('Pages.ErInvoiceDatas.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/ErInvoiceDatas/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ErInvoiceDatas/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditErInvoiceDataModal',
    });

    var _viewErInvoiceDataModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/ErInvoiceDatas/ViewerInvoiceDataModal',
      modalClass: 'ViewErInvoiceDataModal',
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

    var dataTable = _$erInvoiceDatasTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _erInvoiceDatasService.getAll,
        inputFilter: function () {
          return {
            filter: $('#ErInvoiceDatasTableFilter').val(),
            minvalidityDateFilter: getDateFilter($('#MinvalidityDateFilterId')),
            maxvalidityDateFilter: getMaxDateFilter($('#MaxvalidityDateFilterId')),
            amountFilter: $('#amountFilterId').val(),
            amountdueFilter: $('#amountdueFilterId').val(),
            statusFilter: $('#statusFilterId').val(),
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
                  _viewErInvoiceDataModal.open({ id: data.record.erInvoiceData.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.erInvoiceData.id });
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
                    entityId: data.record.erInvoiceData.id,
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
                  deleteErInvoiceData(data.record.erInvoiceData);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'erInvoiceData.validityDate',
          name: 'validityDate',
          render: function (validityDate) {
            if (validityDate) {
              return moment(validityDate).format('L');
            }
            return '';
          },
        },
        {
          targets: 3,
          data: 'erInvoiceData.amount',
          name: 'amount',
        },
        {
          targets: 4,
          data: 'erInvoiceData.amountdue',
          name: 'amountdue',
        },
        {
          targets: 5,
          data: 'erInvoiceData.status',
          name: 'status',
        },
        {
          targets: 6,
          data: 'billofladingblno',
          name: 'billofladingFk.blno',
        },
      ],
    });

    function getErInvoiceDatas() {
      dataTable.ajax.reload();
    }

    function deleteErInvoiceData(erInvoiceData) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _erInvoiceDatasService
            .delete({
              id: erInvoiceData.id,
            })
            .done(function () {
              getErInvoiceDatas(true);
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

    $('#CreateNewErInvoiceDataButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _erInvoiceDatasService
        .getErInvoiceDatasToExcel({
          filter: $('#ErInvoiceDatasTableFilter').val(),
          minvalidityDateFilter: getDateFilter($('#MinvalidityDateFilterId')),
          maxvalidityDateFilter: getMaxDateFilter($('#MaxvalidityDateFilterId')),
          amountFilter: $('#amountFilterId').val(),
          amountdueFilter: $('#amountdueFilterId').val(),
          statusFilter: $('#statusFilterId').val(),
          billofladingblnoFilter: $('#BillofladingblnoFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditErInvoiceDataModalSaved', function () {
      getErInvoiceDatas();
    });

    $('#GetErInvoiceDatasButton').click(function (e) {
      e.preventDefault();
      getErInvoiceDatas();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getErInvoiceDatas();
      }
    });
  });
})();
