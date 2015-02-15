$(document).ready(function() {
    var categoryDropdownElements = $('#categoryDropDown li a');
    var statusDropdownElements = $('#statusDropDown li a');

    var categorySelection = $('#categorySelection');
    var statusSelection = $('#statusSelection');

    var categoryDescription = $('#categoryDescription');
    var statusDescription = $('#statusDescription');

    categoryDropdownElements.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        categorySelection.text(selection);
        categoryDescription.val(selection);
    });

    statusDropdownElements.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        statusSelection.text(selection);
        statusDescription.val(selection);
    });
   
});