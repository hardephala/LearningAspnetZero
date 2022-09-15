(function ($) {
  app.modals.CreateOrEditLongstandingModal = function () {
    var _longstandingsService = abp.services.app.longstandings;

    var _modalManager;
    var _$longstandingInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$longstandingInformationForm = _modalManager.getModal().find('form[name=LongstandingInformationsForm]');
      _$longstandingInformationForm.validate();
    };

    this.save = function () {
      if (!_$longstandingInformationForm.valid()) {
        return;
      }

      var longstanding = _$longstandingInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _longstandingsService
        .createOrEdit(longstanding)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditLongstandingModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
