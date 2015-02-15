$(document).ready(function () {

    var filterByDropDown = $('#filterByDropDown li a');
    var filterSelection = $('#filterBySelection');

    filterByDropDown.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        filterSelection.text(selection);

        var statusid = $(this).attr('data-printjob-status-id');        
        var printJobItemStatusId = $('#printJobItemStatusId');
        printJobItemStatusId.val(statusid);
    });  
});

