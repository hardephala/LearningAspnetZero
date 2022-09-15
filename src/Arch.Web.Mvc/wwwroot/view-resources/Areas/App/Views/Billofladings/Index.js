(function () {
  $(function () {
    var _$billofladingsTable = $('#BillofladingsTable');
    var _billofladingsService = abp.services.app.billofladings;

    $('.date-picker').datetimepicker({
      locale: abp.localization.currentLanguage.name,
      format: 'L',
    });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.Billofladings.Create'),
      edit: abp.auth.hasPermission('Pages.Billofladings.Edit'),
      delete: abp.auth.hasPermission('Pages.Billofladings.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Billofladings/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Billofladings/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditBillofladingModal',
    });

    var _viewBillofladingModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Billofladings/ViewbillofladingModal',
      modalClass: 'ViewBillofladingModal',
    });

    var getDateFilter = function (element) {
      if (element.data('DateTimePicker').date() == null) {
        return null;
      }
      return element.data('DateTimePicker').date().format('YYYY-MM-DDT00:00:00Z');
    };

    var getMaxDateFilter = function (element) {
      if (element.data('DateTimePicker').date() == null) {
        return null;
      }
      return element.data('DateTimePicker').date().format('YYYY-MM-DDT23:59:59Z');
    };

    var dataTable = _$billofladingsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _billofladingsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#BillofladingsTableFilter').val(),
            shipmentnoFilter: $('#shipmentnoFilterId').val(),
            blnoFilter: $('#blnoFilterId').val(),
            equipmentnoFilter: $('#equipmentnoFilterId').val(),
            equipmenttypeFilter: $('#equipmenttypeFilterId').val(),
            equipmentsizeFilter: $('#equipmentsizeFilterId').val(),
            shipperownedFilter: $('#shipperownedFilterId').val(),
            shipoperatorFilter: $('#shipoperatorFilterId').val(),
            servicecontractFilter: $('#servicecontractFilterId').val(),
            spotbookingFilter: $('#spotbookingFilterId').val(),
            consigneecodeFilter: $('#consigneecodeFilterId').val(),
            dischargeportcodeFilter: $('#dischargeportcodeFilterId').val(),
            dischargeportnameFilter: $('#dischargeportnameFilterId').val(),
            placeofdeliverycodeFilter: $('#placeofdeliverycodeFilterId').val(),
            placeofdeliverynameFilter: $('#placeofdeliverynameFilterId').val(),
            finalvesselcodeFilter: $('#finalvesselcodeFilterId').val(),
            finalvesselnameFilter: $('#finalvesselnameFilterId').val(),
            finalvesselvoyageFilter: $('#finalvesselvoyageFilterId').val(),
            minfinalvesseletaFilter: getDateFilter($('#MinfinalvesseletaFilterId')),
            maxfinalvesseletaFilter: getMaxDateFilter($('#MaxfinalvesseletaFilterId')),
            partpartstatusFilter: $('#partpartstatusFilterId').val(),
            partpartrefFilter: $('#partpartrefFilterId').val(),
            depositpayableFilter: $('#depositpayableFilterId').val(),
            mindepositdueamountFilter: $('#MindepositdueamountFilterId').val(),
            maxdepositdueamountFilter: $('#MaxdepositdueamountFilterId').val(),
            depositwaivedamountFilter: $('#depositwaivedamountFilterId').val(),
            depositwaivedreasonFilter: $('#depositwaivedreasonFilterId').val(),
            depositwaivedbyFilter: $('#depositwaivedbyFilterId').val(),
            depositpaymentstatusFilter: $('#depositpaymentstatusFilterId').val(),
            releaseoutstandingstatusFilter: $('#releaseoutstandingstatusFilterId').val(),
            releaseoutstandingreasonFilter: $('#releaseoutstandingreasonFilterId').val(),
            releaseoutstandingbyFilter: $('#releaseoutstandingbyFilterId').val(),
            releaselongstandingstatusFilter: $('#releaselongstandingstatusFilterId').val(),
            releaselongstandingreasonFilter: $('#releaselongstandingreasonFilterId').val(),
            releaselongstandingbyFilter: $('#releaselongstandingbyFilterId').val(),
            blnotypeFilter: $('#blnotypeFilterId').val(),
            blnosubmitstatusFilter: $('#blnosubmitstatusFilterId').val(),
            blnosubmittedtoFilter: $('#blnosubmittedtoFilterId').val(),
            blnosubmittedbyFilter: $('#blnosubmittedbyFilterId').val(),
            blnosubmitrefFilter: $('#blnosubmitrefFilterId').val(),
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
                  _viewBillofladingModal.open({ id: data.record.billoflading.id });
                },
              },
              {
                text: app.localize('Edit'),
                iconStyle: 'far fa-edit mr-2',
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.billoflading.id });
                },
              },
              {
                text: app.localize('Delete'),
                iconStyle: 'far fa-trash-alt mr-2',
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteBilloflading(data.record.billoflading);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'billoflading.shipmentno',
          name: 'shipmentno',
        },
        {
          targets: 3,
          data: 'billoflading.blno',
          name: 'blno',
        },
        {
          targets: 4,
          data: 'billoflading.equipmentno',
          name: 'equipmentno',
        },
        {
          targets: 5,
          data: 'billoflading.equipmenttype',
          name: 'equipmenttype',
        },
        {
          targets: 6,
          data: 'billoflading.equipmentsize',
          name: 'equipmentsize',
        },
        {
          targets: 7,
          data: 'billoflading.shipperowned',
          name: 'shipperowned',
        },
        {
          targets: 8,
          data: 'billoflading.shipoperator',
          name: 'shipoperator',
        },
        {
          targets: 9,
          data: 'billoflading.servicecontract',
          name: 'servicecontract',
        },
        {
          targets: 10,
          data: 'billoflading.spotbooking',
          name: 'spotbooking',
        },
        {
          targets: 11,
          data: 'billoflading.consigneecode',
          name: 'consigneecode',
        },
        {
          targets: 12,
          data: 'billoflading.dischargeportcode',
          name: 'dischargeportcode',
        },
        {
          targets: 13,
          data: 'billoflading.dischargeportname',
          name: 'dischargeportname',
        },
        {
          targets: 14,
          data: 'billoflading.placeofdeliverycode',
          name: 'placeofdeliverycode',
        },
        {
          targets: 15,
          data: 'billoflading.placeofdeliveryname',
          name: 'placeofdeliveryname',
        },
        {
          targets: 16,
          data: 'billoflading.finalvesselcode',
          name: 'finalvesselcode',
        },
        {
          targets: 17,
          data: 'billoflading.finalvesselname',
          name: 'finalvesselname',
        },
        {
          targets: 18,
          data: 'billoflading.finalvesselvoyage',
          name: 'finalvesselvoyage',
        },
        {
          targets: 19,
          data: 'billoflading.finalvesseleta',
          name: 'finalvesseleta',
          render: function (finalvesseleta) {
            if (finalvesseleta) {
              return moment(finalvesseleta).format('L');
            }
            return '';
          },
        },
        {
          targets: 20,
          data: 'billoflading.partpartstatus',
          name: 'partpartstatus',
        },
        {
          targets: 21,
          data: 'billoflading.partpartref',
          name: 'partpartref',
        },
        {
          targets: 22,
          data: 'billoflading.depositpayable',
          name: 'depositpayable',
        },
        {
          targets: 23,
          data: 'billoflading.depositdueamount',
          name: 'depositdueamount',
        },
        {
          targets: 24,
          data: 'billoflading.depositwaivedamount',
          name: 'depositwaivedamount',
        },
        {
          targets: 25,
          data: 'billoflading.depositwaivedreason',
          name: 'depositwaivedreason',
        },
        {
          targets: 26,
          data: 'billoflading.depositwaivedby',
          name: 'depositwaivedby',
        },
        {
          targets: 27,
          data: 'billoflading.depositpaymentstatus',
          name: 'depositpaymentstatus',
        },
        {
          targets: 28,
          data: 'billoflading.releaseoutstandingstatus',
          name: 'releaseoutstandingstatus',
        },
        {
          targets: 29,
          data: 'billoflading.releaseoutstandingreason',
          name: 'releaseoutstandingreason',
        },
        {
          targets: 30,
          data: 'billoflading.releaseoutstandingby',
          name: 'releaseoutstandingby',
        },
        {
          targets: 31,
          data: 'billoflading.releaselongstandingstatus',
          name: 'releaselongstandingstatus',
        },
        {
          targets: 32,
          data: 'billoflading.releaselongstandingreason',
          name: 'releaselongstandingreason',
        },
        {
          targets: 33,
          data: 'billoflading.releaselongstandingby',
          name: 'releaselongstandingby',
        },
        {
          targets: 34,
          data: 'billoflading.blnotype',
          name: 'blnotype',
        },
        {
          targets: 35,
          data: 'billoflading.blnosubmitstatus',
          name: 'blnosubmitstatus',
        },
        {
          targets: 36,
          data: 'billoflading.blnosubmittedto',
          name: 'blnosubmittedto',
        },
        {
          targets: 37,
          data: 'billoflading.blnosubmittedby',
          name: 'blnosubmittedby',
        },
        {
          targets: 38,
          data: 'billoflading.blnosubmitref',
          name: 'blnosubmitref',
        },
      ],
    });

    function getBillofladings() {
      dataTable.ajax.reload();
    }

    function deleteBilloflading(billoflading) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _billofladingsService
            .delete({
              id: billoflading.id,
            })
            .done(function () {
              getBillofladings(true);
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

    $('#CreateNewBillofladingButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _billofladingsService
        .getBillofladingsToExcel({
          filter: $('#BillofladingsTableFilter').val(),
          shipmentnoFilter: $('#shipmentnoFilterId').val(),
          blnoFilter: $('#blnoFilterId').val(),
          equipmentnoFilter: $('#equipmentnoFilterId').val(),
          equipmenttypeFilter: $('#equipmenttypeFilterId').val(),
          equipmentsizeFilter: $('#equipmentsizeFilterId').val(),
          shipperownedFilter: $('#shipperownedFilterId').val(),
          shipoperatorFilter: $('#shipoperatorFilterId').val(),
          servicecontractFilter: $('#servicecontractFilterId').val(),
          spotbookingFilter: $('#spotbookingFilterId').val(),
          consigneecodeFilter: $('#consigneecodeFilterId').val(),
          dischargeportcodeFilter: $('#dischargeportcodeFilterId').val(),
          dischargeportnameFilter: $('#dischargeportnameFilterId').val(),
          placeofdeliverycodeFilter: $('#placeofdeliverycodeFilterId').val(),
          placeofdeliverynameFilter: $('#placeofdeliverynameFilterId').val(),
          finalvesselcodeFilter: $('#finalvesselcodeFilterId').val(),
          finalvesselnameFilter: $('#finalvesselnameFilterId').val(),
          finalvesselvoyageFilter: $('#finalvesselvoyageFilterId').val(),
          minfinalvesseletaFilter: getDateFilter($('#MinfinalvesseletaFilterId')),
          maxfinalvesseletaFilter: getMaxDateFilter($('#MaxfinalvesseletaFilterId')),
          partpartstatusFilter: $('#partpartstatusFilterId').val(),
          partpartrefFilter: $('#partpartrefFilterId').val(),
          depositpayableFilter: $('#depositpayableFilterId').val(),
          mindepositdueamountFilter: $('#MindepositdueamountFilterId').val(),
          maxdepositdueamountFilter: $('#MaxdepositdueamountFilterId').val(),
          depositwaivedamountFilter: $('#depositwaivedamountFilterId').val(),
          depositwaivedreasonFilter: $('#depositwaivedreasonFilterId').val(),
          depositwaivedbyFilter: $('#depositwaivedbyFilterId').val(),
          depositpaymentstatusFilter: $('#depositpaymentstatusFilterId').val(),
          releaseoutstandingstatusFilter: $('#releaseoutstandingstatusFilterId').val(),
          releaseoutstandingreasonFilter: $('#releaseoutstandingreasonFilterId').val(),
          releaseoutstandingbyFilter: $('#releaseoutstandingbyFilterId').val(),
          releaselongstandingstatusFilter: $('#releaselongstandingstatusFilterId').val(),
          releaselongstandingreasonFilter: $('#releaselongstandingreasonFilterId').val(),
          releaselongstandingbyFilter: $('#releaselongstandingbyFilterId').val(),
          blnotypeFilter: $('#blnotypeFilterId').val(),
          blnosubmitstatusFilter: $('#blnosubmitstatusFilterId').val(),
          blnosubmittedtoFilter: $('#blnosubmittedtoFilterId').val(),
          blnosubmittedbyFilter: $('#blnosubmittedbyFilterId').val(),
          blnosubmitrefFilter: $('#blnosubmitrefFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditBillofladingModalSaved', function () {
      getBillofladings();
    });

    $('#GetBillofladingsButton').click(function (e) {
      e.preventDefault();
      getBillofladings();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getBillofladings();
      }
    });
  });
})();
