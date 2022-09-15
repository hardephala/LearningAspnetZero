﻿(function ($) {
    app.modals.CreateOrEditCustomerModal = function () {

        var _customersService = abp.services.app.customers;

        var _modalManager;
        var _$customerInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').daterangepicker({
                singleDatePicker: true,
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$customerInformationForm = _modalManager.getModal().find('form[name=CustomerInformationsForm]');
            _$customerInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$customerInformationForm.valid()) {
                return;
            }

            

            var customer = _$customerInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _customersService.createOrEdit(
				customer
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditCustomerModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);