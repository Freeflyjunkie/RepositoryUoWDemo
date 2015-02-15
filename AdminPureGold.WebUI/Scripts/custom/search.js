$(document).ready(function () {

    // wire Model dropdown
    WireSelectButton($('#typeDropdown li a'), $('#typeSelection'));   
});

function WireSelectButton(dropdownElements, selectControl) {
    dropdownElements.on('click', function (e) {

        e.preventDefault();

        // set selection text
        var selection = $(this).text();
        selectControl.text(selection);

        var placeholderText = $(this).attr("data-placeholder-text");
        $('#search').attr("placeholder", placeholderText);

        var searchType = $(this).attr("data-search-type");
        $('#searchType').val(searchType);
    });
}