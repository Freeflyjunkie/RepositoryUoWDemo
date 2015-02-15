$(document).ready(function () {

    var typeSelector = $('#typeSelector');    
    var getProjectionBtn = $('#getProjectionBtn');
    var datepickers = $('.datepicker');
    var startDate = $('#startDate');
    var endDate = $('#endDate');
    var dateError = $('#dateError');

    dateError.hide();    
    getProjectionBtn.attr('disabled', 'disabled');

    datepickers.datepicker({ format: "mm/dd/yyyy" }).on('changeDate', function (e) {
        datepickers.datepicker('hide');
        showProjectionBtn();
    });

    typeSelector.change(function () {
        var selectedType = $('#typeSelector option:selected');
        var selectedText = selectedType.text();        
        $('#mailingTypeText').val(selectedText);

        showProjectionBtn();
    });

    function showProjectionBtn() {
        var selectedType = $('#typeSelector option:selected');
        var selectedValue = selectedType.attr("value");        
        if (startDate.val() !== '' && endDate.val() !== '' && selectedValue !== "") {
            var start = Date.parse(startDate.val());
            var end = Date.parse(endDate.val());
            if (start <= end) {
                dateError.hide();                
                getProjectionBtn.removeAttr('disabled');
            } else {
                dateError.show();
                getProjectionBtn.attr('disabled', 'disabled');
            }
        }
    }
});