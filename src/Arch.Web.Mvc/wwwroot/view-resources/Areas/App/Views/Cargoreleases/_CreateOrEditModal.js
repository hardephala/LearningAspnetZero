(function ($) {
  app.modals.CreateOrEditCargoreleaseModal = function () {
    var _cargoreleasesService = abp.services.app.cargoreleases;

    var _modalManager;
    var _$cargoreleaseInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$cargoreleaseInformationForm = _modalManager.getModal().find('form[name=CargoreleaseInformationsForm]');
      _$cargoreleaseInformationForm.validate();
    };

    this.save = function () {
      if (!_$cargoreleaseInformationForm.valid()) {
        return;
      }

      var cargorelease = _$cargoreleaseInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _cargoreleasesService
        .createOrEdit(cargorelease)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditCargoreleaseModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
