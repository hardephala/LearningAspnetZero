(function ($) {
  app.modals.CreateOrEditBillofladingModal = function () {
    var _billofladingsService = abp.services.app.billofladings;

    var _modalManager;
    var _$billofladingInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').datetimepicker({
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$billofladingInformationForm = _modalManager.getModal().find('form[name=BillofladingInformationsForm]');
      _$billofladingInformationForm.validate();
    };

    this.save = function () {
      if (!_$billofladingInformationForm.valid()) {
        return;
      }

      var billoflading = _$billofladingInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _billofladingsService
        .createOrEdit(billoflading)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditBillofladingModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
