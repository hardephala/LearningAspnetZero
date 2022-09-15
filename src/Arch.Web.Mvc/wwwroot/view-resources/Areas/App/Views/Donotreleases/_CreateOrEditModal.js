(function ($) {
  app.modals.CreateOrEditDonotreleaseModal = function () {
    var _donotreleasesService = abp.services.app.donotreleases;

    var _modalManager;
    var _$donotreleaseInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$donotreleaseInformationForm = _modalManager.getModal().find('form[name=DonotreleaseInformationsForm]');
      _$donotreleaseInformationForm.validate();
    };

    this.save = function () {
      if (!_$donotreleaseInformationForm.valid()) {
        return;
      }

      var donotrelease = _$donotreleaseInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _donotreleasesService
        .createOrEdit(donotrelease)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditDonotreleaseModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
