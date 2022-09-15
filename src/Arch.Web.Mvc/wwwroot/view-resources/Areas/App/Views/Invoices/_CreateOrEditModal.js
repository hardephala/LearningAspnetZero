(function ($) {
  app.modals.CreateOrEditInvoiceModal = function () {
    var _invoicesService = abp.services.app.invoices;

    var _modalManager;
    var _$invoiceInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$invoiceInformationForm = _modalManager.getModal().find('form[name=InvoiceInformationsForm]');
      _$invoiceInformationForm.validate();
    };

    this.save = function () {
      if (!_$invoiceInformationForm.valid()) {
        return;
      }

      var invoice = _$invoiceInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _invoicesService
        .createOrEdit(invoice)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditInvoiceModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
