(function () {
  $(function () {
    var _$donotreleasesTable = $('#DonotreleasesTable');
    var _donotreleasesService = abp.services.app.donotreleases;

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
      create: abp.auth.hasPermission('Pages.Donotreleases.Create'),
      edit: abp.auth.hasPermission('Pages.Donotreleases.Edit'),
      delete: abp.auth.hasPermission('Pages.Donotreleases.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Donotreleases/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Donotreleases/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditDonotreleaseModal',
    });

    var _viewDonotreleaseModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Donotreleases/ViewdonotreleaseModal',
      modalClass: 'ViewDonotreleaseModal',
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

    var dataTable = _$donotreleasesTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _donotreleasesService.getAll,
        inputFilter: function () {
          return {
            filter: $('#DonotreleasesTableFilter').val(),
            blnoFilter: $('#blnoFilterId').val(),
            statusFilter: $('#statusFilterId').val(),
            releasedbyFilter: $('#releasedbyFilterId').val(),
            releasecommentFilter: $('#releasecommentFilterId').val(),
            blockedbyFilter: $('#blockedbyFilterId').val(),
            blockedcommentFilter: $('#blockedcommentFilterId').val(),
            minblockeddateFilter: getDateFilter($('#MinblockeddateFilterId')),
            maxblockeddateFilter: getMaxDateFilter($('#MaxblockeddateFilterId')),
            blockedreferenceFilter: $('#blockedreferenceFilterId').val(),
            blcommentFilter: $('#blcommentFilterId').val(),
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
                  _viewDonotreleaseModal.open({ id: data.record.donotrelease.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.donotrelease.id });
                },
              },
              {
                text: app.localize('Delete'),
                iconStyle: 'far fa-trash-alt mr-2',
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteDonotrelease(data.record.donotrelease);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'donotrelease.blno',
          name: 'blno',
        },
        {
          targets: 3,
          data: 'donotrelease.status',
          name: 'status',
        },
        {
          targets: 4,
          data: 'donotrelease.releasedby',
          name: 'releasedby',
        },
        {
          targets: 5,
          data: 'donotrelease.releasecomment',
          name: 'releasecomment',
        },
        {
          targets: 6,
          data: 'donotrelease.blockedby',
          name: 'blockedby',
        },
        {
          targets: 7,
          data: 'donotrelease.blockedcomment',
          name: 'blockedcomment',
        },
        {
          targets: 8,
          data: 'donotrelease.blockeddate',
          name: 'blockeddate',
          render: function (blockeddate) {
            if (blockeddate) {
              return moment(blockeddate).format('L');
            }
            return '';
          },
        },
        {
          targets: 9,
          data: 'donotrelease.blockedreference',
          name: 'blockedreference',
        },
        {
          targets: 10,
          data: 'donotrelease.blcomment',
          name: 'blcomment',
        },
      ],
    });

    function getDonotreleases() {
      dataTable.ajax.reload();
    }

    function deleteDonotrelease(donotrelease) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _donotreleasesService
            .delete({
              id: donotrelease.id,
            })
            .done(function () {
              getDonotreleases(true);
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

    $('#CreateNewDonotreleaseButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _donotreleasesService
        .getDonotreleasesToExcel({
          filter: $('#DonotreleasesTableFilter').val(),
          blnoFilter: $('#blnoFilterId').val(),
          statusFilter: $('#statusFilterId').val(),
          releasedbyFilter: $('#releasedbyFilterId').val(),
          releasecommentFilter: $('#releasecommentFilterId').val(),
          blockedbyFilter: $('#blockedbyFilterId').val(),
          blockedcommentFilter: $('#blockedcommentFilterId').val(),
          minblockeddateFilter: getDateFilter($('#MinblockeddateFilterId')),
          maxblockeddateFilter: getMaxDateFilter($('#MaxblockeddateFilterId')),
          blockedreferenceFilter: $('#blockedreferenceFilterId').val(),
          blcommentFilter: $('#blcommentFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditDonotreleaseModalSaved', function () {
      getDonotreleases();
    });

    $('#GetDonotreleasesButton').click(function (e) {
      e.preventDefault();
      getDonotreleases();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getDonotreleases();
      }
    });
  });
})();
