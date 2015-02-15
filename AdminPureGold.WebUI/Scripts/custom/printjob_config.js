$(document).ready(function () {        
    // Hide Show Sections    
    var templateSelector = $('#templates');
    var chooseWhatToInclude = $('#chooseWhatToInclude');
    var includeThankYouCards = $('#includeThankYouCards');
    var includeDatePicker = $('#includeDatePicker');
    var includePrintJob = $('#includePrintJob');

    chooseWhatToInclude.hide();
    includeThankYouCards.hide();
    includePrintJob.hide();    

    var addToPrintJobButton = $('#addToPrintJobButton');
    var currentPrintJobName = $('#currentPrintJobName');
    var enteredPrintJobName = $('#enteredPrintJobName');

    var sidebar = $('#sidebar');
    sidebar.hide();

    // Hide Show Buttons        
    var sendToMailMergeButton = $('#sendToMailMergeButton');
    var sendToExcelButton = $('#sendToExcelButton');
    sendToMailMergeButton.hide();
    sendToExcelButton.hide();

    // Date Range
    var datepickers = $('.datepicker');
    var startDate = $('#startDate');
    var endDate = $('#endDate');
    var dateError = $('#dateError');
    dateError.hide();

    // Get Template Selector and Listen For Change Event    
    templateSelector.change(function () {
        // Show Sections Based On Selected Template
        chooseWhatToInclude.show();
        // Get Selected Template
        var selectedTemplate = $('#templates option:selected');
        var selectedValue = selectedTemplate.attr("value");
        switch (selectedValue) {
            case "10": // Thank You Cards    
                includeThankYouCards.show();
                includeDatePicker.hide();
                includePrintJob.show();
                //includeMissedCustomers.hide();
                break;
            default:
                includeThankYouCards.hide();
                includeDatePicker.show();
                includePrintJob.hide();
                //includeMissedCustomers.hide();
                break;
        }
        
        startDate.val('');
        endDate.val('');
        var printjobName = 'Print Job ' + selectedTemplate.text();
        var today = new Date();
        var month = today.getMonth() + 1;
        printjobName = printjobName + ' ' + month + '-' + today.getDate() + '-' + today.getFullYear();
        enteredPrintJobName.val(printjobName);
    });

    datepickers.datepicker({ format: "mm/dd/yyyy" }).on('changeDate', function (e) {
        datepickers.datepicker('hide');
        if (startDate.val() !== '' && endDate.val() !== '') {
            var start = Date.parse(startDate.val());
            var end = Date.parse(endDate.val());
            if (start <= end) {
                dateError.hide();
                includePrintJob.show();                
            } else {
                dateError.show();
                includePrintJob.hide();                
            }
        }
    });

    addToPrintJobButton.on('click', function () {
        var selectedTemplate = $('#templates option:selected');
        var selectedValue = selectedTemplate.attr("value");
        
        if (startDate.val() === "" && endDate.val() === "") {
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth();            
            startDate.val('1/1/'+ year);
            endDate.val(month + '/1/' + year);
        }

        $.ajax({
            type: "POST",
            url: CreateUrl,
            dataType: 'json',
            data: '{type: "' + selectedValue + '", description: "' + enteredPrintJobName.val() + '", startDate: "' + startDate.val() + '" , endDate: "' + endDate.val() + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (data, status, xhr) {                
                // Set PrintJob Ids For POSTs
                var printJobIds = $('.printjob-ids');
                printJobIds.val(data.PrintJob.PrintJobId);                

                // Show Print Job Statistics
                sidebar.show();

                // Update Print Job Name
                currentPrintJobName.text(enteredPrintJobName.val());

                // Update QA Statistics
                var total = $('.total-customers-text');
                total.text(data.Total);
                var passed = $('.passed-qa-customers-text');
                passed.text(data.Passed);
                var failed = $('.failed-qa-customers-text');
                failed.text(data.Failed);

                // Disable Template Selector
                templateSelector.attr('disabled', 'disabled');

                // Update Create Print Job Button Text
                addToPrintJobButton.attr('disabled', 'disabled');

                // Hide Show Send Button                
                if (selectedValue == 13 || selectedValue == 14) {
                    sendToExcelButton.show();
                    sendToMailMergeButton.hide();
                } else {
                    sendToExcelButton.hide();
                    sendToMailMergeButton.show();
                }
            },
            error: function (xhr, status, error) {
                alert(error);                
            }
        });
    });

    //includeAllMissedButton.on('click', function () {
    //    var checkboxes = $('#includeMissedTable input[type=checkbox]');
    //    if (includeAllMissedButton.text() == 'Exclude All') {
    //        includeAllMissedButton.text('Include All');
    //        checkboxes.prop('checked', false);
    //    } else {
    //        includeAllMissedButton.text('Exclude All');
    //        checkboxes.prop('checked', true);
    //    }
    //});
});