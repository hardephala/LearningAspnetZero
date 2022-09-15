(function () {
  $(function () {
    var _$longstandingsTable = $('#LongstandingsTable');
    var _longstandingsService = abp.services.app.longstandings;

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
      create: abp.auth.hasPermission('Pages.Longstandings.Create'),
      edit: abp.auth.hasPermission('Pages.Longstandings.Edit'),
      delete: abp.auth.hasPermission('Pages.Longstandings.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Longstandings/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Longstandings/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditLongstandingModal',
    });

    var _viewLongstandingModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Longstandings/ViewlongstandingModal',
      modalClass: 'ViewLongstandingModal',
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

    var dataTable = _$longstandingsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _longstandingsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#LongstandingsTableFilter').val(),
            customercodeFilter: $('#customercodeFilterId').val(),
            blnoFilter: $('#blnoFilterId').val(),
            containernoFilter: $('#containernoFilterId').val(),
            containertypeFilter: $('#containertypeFilterId').val(),
            freetextFilter: $('#freetextFilterId').val(),
            locationFilter: $('#locationFilterId').val(),
            lastmoveFilter: $('#lastmoveFilterId').val(),
            mindaysFilter: $('#MindaysFilterId').val(),
            maxdaysFilter: $('#MaxdaysFilterId').val(),
            statusFilter: $('#statusFilterId').val(),
            releasedbyFilter: $('#releasedbyFilterId').val(),
            releasedreasonFilter: $('#releasedreasonFilterId').val(),
            releasecommentFilter: $('#releasecommentFilterId').val(),
            minvaliditydateFilter: getDateFilter($('#MinvaliditydateFilterId')),
            maxvaliditydateFilter: getMaxDateFilter($('#MaxvaliditydateFilterId')),
            shipoperatorFilter: $('#shipoperatorFilterId').val(),
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
                  _viewLongstandingModal.open({ id: data.record.longstanding.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.longstanding.id });
                },
              },
              {
                text: app.localize('Delete'),
                iconStyle: 'far fa-trash-alt mr-2',
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteLongstanding(data.record.longstanding);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'longstanding.customercode',
          name: 'customercode',
        },
        {
          targets: 3,
          data: 'longstanding.blno',
          name: 'blno',
        },
        {
          targets: 4,
          data: 'longstanding.containerno',
          name: 'containerno',
        },
        {
          targets: 5,
          data: 'longstanding.containertype',
          name: 'containertype',
        },
        {
          targets: 6,
          data: 'longstanding.freetext',
          name: 'freetext',
        },
        {
          targets: 7,
          data: 'longstanding.location',
          name: 'location',
        },
        {
          targets: 8,
          data: 'longstanding.lastmove',
          name: 'lastmove',
        },
        {
          targets: 9,
          data: 'longstanding.days',
          name: 'days',
        },
        {
          targets: 10,
          data: 'longstanding.status',
          name: 'status',
        },
        {
          targets: 11,
          data: 'longstanding.releasedby',
          name: 'releasedby',
        },
        {
          targets: 12,
          data: 'longstanding.releasedreason',
          name: 'releasedreason',
        },
        {
          targets: 13,
          data: 'longstanding.releasecomment',
          name: 'releasecomment',
        },
        {
          targets: 14,
          data: 'longstanding.validitydate',
          name: 'validitydate',
          render: function (validitydate) {
            if (validitydate) {
              return moment(validitydate).format('L');
            }
            return '';
          },
        },
        {
          targets: 15,
          data: 'longstanding.shipoperator',
          name: 'shipoperator',
        },
      ],
    });

    function getLongstandings() {
      dataTable.ajax.reload();
    }

    function deleteLongstanding(longstanding) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _longstandingsService
            .delete({
              id: longstanding.id,
            })
            .done(function () {
              getLongstandings(true);
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

    $('#CreateNewLongstandingButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _longstandingsService
        .getLongstandingsToExcel({
          filter: $('#LongstandingsTableFilter').val(),
          customercodeFilter: $('#customercodeFilterId').val(),
          blnoFilter: $('#blnoFilterId').val(),
          containernoFilter: $('#containernoFilterId').val(),
          containertypeFilter: $('#containertypeFilterId').val(),
          freetextFilter: $('#freetextFilterId').val(),
          locationFilter: $('#locationFilterId').val(),
          lastmoveFilter: $('#lastmoveFilterId').val(),
          mindaysFilter: $('#MindaysFilterId').val(),
          maxdaysFilter: $('#MaxdaysFilterId').val(),
          statusFilter: $('#statusFilterId').val(),
          releasedbyFilter: $('#releasedbyFilterId').val(),
          releasedreasonFilter: $('#releasedreasonFilterId').val(),
          releasecommentFilter: $('#releasecommentFilterId').val(),
          minvaliditydateFilter: getDateFilter($('#MinvaliditydateFilterId')),
          maxvaliditydateFilter: getMaxDateFilter($('#MaxvaliditydateFilterId')),
          shipoperatorFilter: $('#shipoperatorFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditLongstandingModalSaved', function () {
      getLongstandings();
    });

    $('#GetLongstandingsButton').click(function (e) {
      e.preventDefault();
      getLongstandings();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getLongstandings();
      }
    });
  });
})();
