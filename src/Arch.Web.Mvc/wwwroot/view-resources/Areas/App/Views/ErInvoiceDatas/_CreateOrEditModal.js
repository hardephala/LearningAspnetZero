(function ($) {
  app.modals.CreateOrEditErInvoiceDataModal = function () {
    var _erInvoiceDatasService = abp.services.app.erInvoiceDatas;

    var _modalManager;
    var _$erInvoiceDataInformationForm = null;

    var _ErInvoiceDatabillofladingLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'App/ErInvoiceDatas/BillofladingLookupTableModal',
      scriptUrl:
        abp.appPath + 'view-resources/Areas/App/Views/ErInvoiceDatas/_ErInvoiceDataBillofladingLookupTableModal.js',
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

      _$erInvoiceDataInformationForm = _modalManager.getModal().find('form[name=ErInvoiceDataInformationsForm]');
      _$erInvoiceDataInformationForm.validate();
    };

    $('#OpenBillofladingLookupTableButton').click(function () {
      var erInvoiceData = _$erInvoiceDataInformationForm.serializeFormToObject();

      _ErInvoiceDatabillofladingLookupTableModal.open(
        { id: erInvoiceData.billofladingId, displayName: erInvoiceData.billofladingblno },
        function (data) {
          _$erInvoiceDataInformationForm.find('input[name=billofladingblno]').val(data.displayName);
          _$erInvoiceDataInformationForm.find('input[name=billofladingId]').val(data.id);
        }
      );
    });

    $('#ClearBillofladingblnoButton').click(function () {
      _$erInvoiceDataInformationForm.find('input[name=billofladingblno]').val('');
      _$erInvoiceDataInformationForm.find('input[name=billofladingId]').val('');
    });

    this.save = function () {
      if (!_$erInvoiceDataInformationForm.valid()) {
        return;
      }
      if ($('#ErInvoiceData_BillofladingId').prop('required') && $('#ErInvoiceData_BillofladingId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('Billoflading')));
        return;
      }

      var erInvoiceData = _$erInvoiceDataInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _erInvoiceDatasService
        .createOrEdit(erInvoiceData)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditErInvoiceDataModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
