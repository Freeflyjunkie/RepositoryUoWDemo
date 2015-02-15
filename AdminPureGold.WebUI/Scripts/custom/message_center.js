$(document).ready(function () {
    WireEvents();
});

function WireEvents() {

    // Agent Search
    var agentSelection = $('#agentSelection');
    var agentDropdown = $('#agentDropDown');

    agentDropdown.mouseleave(function () {
        agentDropdown.toggle();
    });

    agentSelection.keyup(function () {
        var dropSearchLength = agentSelection.val().length;
        if (dropSearchLength > 3) {
            $.ajax({
                type: "POST",
                url: GetAgentsByNameUrl,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: '{ search: "' + agentSelection.val() + '"}',
                beforeSend: function () {
                    $('#loadingAgents').show();
                },
                success: function (data, status, xhr) {
                    PopulateAgentDropdown(data, agentDropdown);
                    agentDropdown.show();
                    var agentDropdownNewElements = $('#agentDropDown li a');
                    WireAgentDropDownElements(agentDropdownNewElements, agentSelection);
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText);
                },
                complete: function () {
                    $('#loadingAgents').hide();
                }
            });
        }
    });
}

// Agent Search Population
function PopulateAgentDropdown(data, dropdown) {
    var html = '';
    for (var i = 0; i < data.length; i++) {
        var liBegin = "<li class='search-option'><a href='#' data-relationshipNumber='" + data[i].RelationshipNumber + "' data-personNumber='" + data[i].PersonNumber + "' data-officeId='" + data[i].OfficeId + "'>";
        var liText = data[i].LastName + ", " + data[i].FirstName;
        var liEnd = "</li></a>";
        html = html + liBegin + liText + liEnd;
    }
    $(dropdown).html(html);
}

// Agent Search Dropdown Wiring
function WireAgentDropDownElements(agentDropdownNewElements, agentSelection) {
    agentDropdownNewElements.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        agentSelection.val(selection);

        var agentPersonNumber = $('#personNumber');
        var personNumber = $(this).attr("data-personNumber");
        $(agentPersonNumber).val(personNumber);
    });
}
