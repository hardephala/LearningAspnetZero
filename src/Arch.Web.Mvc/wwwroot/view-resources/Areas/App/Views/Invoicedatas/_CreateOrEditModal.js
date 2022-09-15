(function ($) {
  app.modals.CreateOrEditInvoicedataModal = function () {
    var _invoicedatasService = abp.services.app.invoicedatas;

    var _modalManager;
    var _$invoicedataInformationForm = null;

    var _InvoicedatabillofladingLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/Invoicedatas/BillofladingLookupTableModal',
      scriptUrl:
        abp.appPath + 'view-resources/Areas/App/Views/Invoicedatas/_InvoicedataBillofladingLookupTableModal.js',
      modalClass: 'BillofladingLookupTableModal',
    });

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$invoicedataInformationForm = _modalManager.getModal().find('form[name=InvoicedataInformationsForm]');
      _$invoicedataInformationForm.validate();
    };

    $('#OpenBillofladingLookupTableButton').click(function () {
      var invoicedata = _$invoicedataInformationForm.serializeFormToObject();

      _InvoicedatabillofladingLookupTableModal.open(
        { id: invoicedata.billofladingId, displayName: invoicedata.billofladingblno },
        function (data) {
          _$invoicedataInformationForm.find('input[name=billofladingblno]').val(data.displayName);
          _$invoicedataInformationForm.find('input[name=billofladingId]').val(data.id);
        }
      );
    });

    $('#ClearBillofladingblnoButton').click(function () {
      _$invoicedataInformationForm.find('input[name=billofladingblno]').val('');
      _$invoicedataInformationForm.find('input[name=billofladingId]').val('');
    });

    this.save = function () {
      if (!_$invoicedataInformationForm.valid()) {
        return;
      }
      if ($('#Invoicedata_BillofladingId').prop('required') && $('#Invoicedata_BillofladingId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('Billoflading')));
        return;
      }

      var invoicedata = _$invoicedataInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _invoicedatasService
        .createOrEdit(invoicedata)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditInvoicedataModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
