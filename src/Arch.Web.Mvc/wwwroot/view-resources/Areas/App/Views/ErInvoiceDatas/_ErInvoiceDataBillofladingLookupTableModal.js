(function ($) {
  app.modals.BillofladingLookupTableModal = function () {
    var _modalManager;

    var _erInvoiceDatasService = abp.services.app.erInvoiceDatas;
    var _$billofladingTable = $('#BillofladingTable');

    this.init = function (modalManager) {
      _modalManager = modalManager;
    };

    var dataTable = _$billofladingTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _erInvoiceDatasService.getAllBillofladingForLookupTable,
        inputFilter: function () {
          return {
            filter: $('#BillofladingTableFilter').val(),
          };
        },
      },
      columnDefs: [
        {
          targets: 0,
          data: null,
          orderable: false,
          autoWidth: false,
          defaultContent:
            "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" +
            app.localize('Select') +
            "' /></div>",
        },
        {
          autoWidth: false,
          orderable: false,
          targets: 1,
          data: 'displayName',
        },
      ],
    });

    $('#BillofladingTable tbody').on('click', '[id*=selectbtn]', function () {
      var data = dataTable.row($(this).parents('tr')).data();
      _modalManager.setResult(data);
      _modalManager.close();
    });

    function getBilloflading() {
      dataTable.ajax.reload();
    }

    $('#GetBillofladingButton').click(function (e) {
      e.preventDefault();
      getBilloflading();
    });

    $('#SelectButton').click(function (e) {
      e.preventDefault();
    });

    $(document).keypress(function (e) {
      if (e.which === 13 && e.target.tagName.toLocaleLowerCase() != 'textarea') {
        getBilloflading();
      }
    });
  };
})(jQuery);
