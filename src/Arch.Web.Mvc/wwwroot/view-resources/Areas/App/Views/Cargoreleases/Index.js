(function () {
  $(function () {
    var _$cargoreleasesTable = $('#CargoreleasesTable');
    var _cargoreleasesService = abp.services.app.cargoreleases;

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
      create: abp.auth.hasPermission('Pages.Cargoreleases.Create'),
      edit: abp.auth.hasPermission('Pages.Cargoreleases.Edit'),
      delete: abp.auth.hasPermission('Pages.Cargoreleases.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Cargoreleases/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Cargoreleases/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditCargoreleaseModal',
    });

    var _viewCargoreleaseModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Cargoreleases/ViewcargoreleaseModal',
      modalClass: 'ViewCargoreleaseModal',
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

    var dataTable = _$cargoreleasesTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _cargoreleasesService.getAll,
        inputFilter: function () {
          return {
            filter: $('#CargoreleasesTableFilter').val(),
            priorityFilter: $('#priorityFilterId').val(),
            blnoFilter: $('#blnoFilterId').val(),
            mininvoicevalidityFilter: getDateFilter($('#MininvoicevalidityFilterId')),
            maxinvoicevalidityFilter: getMaxDateFilter($('#MaxinvoicevalidityFilterId')),
            terminalFilter: $('#terminalFilterId').val(),
            deliveryordernoFilter: $('#deliveryordernoFilterId').val(),
            customercodeFilter: $('#customercodeFilterId').val(),
            agencycodeFilter: $('#agencycodeFilterId').val(),
            agentcodeFilter: $('#agentcodeFilterId').val(),
            entrybyrepcodeFilter: $('#entrybyrepcodeFilterId').val(),
            entrymodeFilter: $('#entrymodeFilterId').val(),
            minentrydateFilter: getDateFilter($('#MinentrydateFilterId')),
            maxentrydateFilter: getMaxDateFilter($('#MaxentrydateFilterId')),
            approvebyFilter: $('#approvebyFilterId').val(),
            approvecommentFilter: $('#approvecommentFilterId').val(),
            minapprovedateFilter: getDateFilter($('#MinapprovedateFilterId')),
            maxapprovedateFilter: getMaxDateFilter($('#MaxapprovedateFilterId')),
            updatedbyFilter: $('#updatedbyFilterId').val(),
            updatecommentFilter: $('#updatecommentFilterId').val(),
            minupdatedateFilter: getDateFilter($('#MinupdatedateFilterId')),
            maxupdatedateFilter: getMaxDateFilter($('#MaxupdatedateFilterId')),
            releasebyFilter: $('#releasebyFilterId').val(),
            releasestatusFilter: $('#releasestatusFilterId').val(),
            releasecommentFilter: $('#releasecommentFilterId').val(),
            minreleasedateFilter: getDateFilter($('#MinreleasedateFilterId')),
            maxreleasedateFilter: getMaxDateFilter($('#MaxreleasedateFilterId')),
            statusFilter: $('#statusFilterId').val(),
            ipaddrFilter: $('#ipaddrFilterId').val(),
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
                  _viewCargoreleaseModal.open({ id: data.record.cargorelease.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.cargorelease.id });
                },
              },
              {
                text: app.localize('Delete'),
                iconStyle: 'far fa-trash-alt mr-2',
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteCargorelease(data.record.cargorelease);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'cargorelease.priority',
          name: 'priority',
        },
        {
          targets: 3,
          data: 'cargorelease.blno',
          name: 'blno',
        },
        {
          targets: 4,
          data: 'cargorelease.invoicevalidity',
          name: 'invoicevalidity',
          render: function (invoicevalidity) {
            if (invoicevalidity) {
              return moment(invoicevalidity).format('L');
            }
            return '';
          },
        },
        {
          targets: 5,
          data: 'cargorelease.terminal',
          name: 'terminal',
        },
        {
          targets: 6,
          data: 'cargorelease.deliveryorderno',
          name: 'deliveryorderno',
        },
        {
          targets: 7,
          data: 'cargorelease.customercode',
          name: 'customercode',
        },
        {
          targets: 8,
          data: 'cargorelease.agencycode',
          name: 'agencycode',
        },
        {
          targets: 9,
          data: 'cargorelease.agentcode',
          name: 'agentcode',
        },
        {
          targets: 10,
          data: 'cargorelease.entrybyrepcode',
          name: 'entrybyrepcode',
        },
        {
          targets: 11,
          data: 'cargorelease.entrymode',
          name: 'entrymode',
        },
        {
          targets: 12,
          data: 'cargorelease.entrydate',
          name: 'entrydate',
          render: function (entrydate) {
            if (entrydate) {
              return moment(entrydate).format('L');
            }
            return '';
          },
        },
        {
          targets: 13,
          data: 'cargorelease.approveby',
          name: 'approveby',
        },
        {
          targets: 14,
          data: 'cargorelease.approvecomment',
          name: 'approvecomment',
        },
        {
          targets: 15,
          data: 'cargorelease.approvedate',
          name: 'approvedate',
          render: function (approvedate) {
            if (approvedate) {
              return moment(approvedate).format('L');
            }
            return '';
          },
        },
        {
          targets: 16,
          data: 'cargorelease.updatedby',
          name: 'updatedby',
        },
        {
          targets: 17,
          data: 'cargorelease.updatecomment',
          name: 'updatecomment',
        },
        {
          targets: 18,
          data: 'cargorelease.updatedate',
          name: 'updatedate',
          render: function (updatedate) {
            if (updatedate) {
              return moment(updatedate).format('L');
            }
            return '';
          },
        },
        {
          targets: 19,
          data: 'cargorelease.releaseby',
          name: 'releaseby',
        },
        {
          targets: 20,
          data: 'cargorelease.releasestatus',
          name: 'releasestatus',
        },
        {
          targets: 21,
          data: 'cargorelease.releasecomment',
          name: 'releasecomment',
        },
        {
          targets: 22,
          data: 'cargorelease.releasedate',
          name: 'releasedate',
          render: function (releasedate) {
            if (releasedate) {
              return moment(releasedate).format('L');
            }
            return '';
          },
        },
        {
          targets: 23,
          data: 'cargorelease.status',
          name: 'status',
        },
        {
          targets: 24,
          data: 'cargorelease.ipaddr',
          name: 'ipaddr',
        },
      ],
    });

    function getCargoreleases() {
      dataTable.ajax.reload();
    }

    function deleteCargorelease(cargorelease) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _cargoreleasesService
            .delete({
              id: cargorelease.id,
            })
            .done(function () {
              getCargoreleases(true);
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

    $('#CreateNewCargoreleaseButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _cargoreleasesService
        .getCargoreleasesToExcel({
          filter: $('#CargoreleasesTableFilter').val(),
          priorityFilter: $('#priorityFilterId').val(),
          blnoFilter: $('#blnoFilterId').val(),
          mininvoicevalidityFilter: getDateFilter($('#MininvoicevalidityFilterId')),
          maxinvoicevalidityFilter: getMaxDateFilter($('#MaxinvoicevalidityFilterId')),
          terminalFilter: $('#terminalFilterId').val(),
          deliveryordernoFilter: $('#deliveryordernoFilterId').val(),
          customercodeFilter: $('#customercodeFilterId').val(),
          agencycodeFilter: $('#agencycodeFilterId').val(),
          agentcodeFilter: $('#agentcodeFilterId').val(),
          entrybyrepcodeFilter: $('#entrybyrepcodeFilterId').val(),
          entrymodeFilter: $('#entrymodeFilterId').val(),
          minentrydateFilter: getDateFilter($('#MinentrydateFilterId')),
          maxentrydateFilter: getMaxDateFilter($('#MaxentrydateFilterId')),
          approvebyFilter: $('#approvebyFilterId').val(),
          approvecommentFilter: $('#approvecommentFilterId').val(),
          minapprovedateFilter: getDateFilter($('#MinapprovedateFilterId')),
          maxapprovedateFilter: getMaxDateFilter($('#MaxapprovedateFilterId')),
          updatedbyFilter: $('#updatedbyFilterId').val(),
          updatecommentFilter: $('#updatecommentFilterId').val(),
          minupdatedateFilter: getDateFilter($('#MinupdatedateFilterId')),
          maxupdatedateFilter: getMaxDateFilter($('#MaxupdatedateFilterId')),
          releasebyFilter: $('#releasebyFilterId').val(),
          releasestatusFilter: $('#releasestatusFilterId').val(),
          releasecommentFilter: $('#releasecommentFilterId').val(),
          minreleasedateFilter: getDateFilter($('#MinreleasedateFilterId')),
          maxreleasedateFilter: getMaxDateFilter($('#MaxreleasedateFilterId')),
          statusFilter: $('#statusFilterId').val(),
          ipaddrFilter: $('#ipaddrFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditCargoreleaseModalSaved', function () {
      getCargoreleases();
    });

    $('#GetCargoreleasesButton').click(function (e) {
      e.preventDefault();
      getCargoreleases();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getCargoreleases();
      }
    });
  });
})();
