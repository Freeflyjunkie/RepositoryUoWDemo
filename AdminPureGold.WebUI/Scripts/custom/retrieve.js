$(document).ready(function () {   
    // Agent Search Hiding And Wiring
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
                url: GetActiveAndInactiveAgentsByNameUrl,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: '{ search: "' + agentSelection.val() + '"}',
                beforeSend: function () {
                    $('#loadingAgents').show();
                },
                success: function (data, status, xhr) {
                    PopulateAgentDropdown(data, agentDropdown);
                    WireAgentDropDownElements($('#agentDropDown li a'), agentSelection);
                    agentDropdown.show();
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

    // Agent Transaction Reassignment Hiding And Wiring   
    var reassignToAgentSelection = $('#reassignToAgentSelection');
    var reassignToAgentDropDown = $('#reassignToAgentDropDown');
    reassignToAgentDropDown.mouseleave(function () {
        reassignToAgentDropDown.toggle();
    });
    reassignToAgentSelection.keyup(function () {
        var dropSearchLength = reassignToAgentSelection.val().length;
        if (dropSearchLength > 3) {
            $.ajax({
                type: "POST",
                url: GetActiveAgentsByNameUrl,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: '{ search: "' + reassignToAgentSelection.val() + '"}',
                beforeSend: function () {
                    $('#loadingReassignToAgent').show();
                },
                success: function (data, status, xhr) {
                    PopulateAgentDropdown(data, reassignToAgentDropDown);
                    WireAgentReassignDropDownElements($('#reassignToAgentDropDown li a'), reassignToAgentSelection);
                    reassignToAgentDropDown.show();
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText);
                },
                complete: function () {
                    $('#loadingReassignToAgent').hide();
                }
            });
        }
    });

    var reassignWindow = $('#reassignWindow');

    // Bulk Reassignment Window
    $('#reassignAllBtn').on('click', function (e) {
        e.preventDefault();

        // Gather TransactionIds
        var transactionIds = "";
        var checkboxes = $('.bulk-reassign-checkbox');
        if (checkboxes.length > 0) {
            checkboxes.each(function (index) {
                var checkbox = $(this);
                if (checkbox.is(':checked')) {
                    transactionIds = transactionIds + $(this).attr('data-transactionid') + ",";
                }
            });

            // Set TransactionIds 
            reassignWindow.find('#reassignToAgentForTransactionId').val(transactionIds.substring(0, transactionIds.length - 1));

            // Show Bulk Reassignment Window
            reassignWindow.modal('show');
        }        
    });

    // Transaction Reassignment Window
    $('.reassignBtn').on('click', function (e) {
        e.preventDefault();

        // Get TransactionId Selected For Reassign
        var transactionId = $(this).attr('data-transactionid');
        reassignWindow.find('#reassignToAgentForTransactionId').val(transactionId);

        // Show Reassignment Window
        reassignWindow.modal('show');
    });

});

// Agent Search and Tranaction Reassignment DropDown Population
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

        // Set Selected 
        var selection = $(this).text();
        agentSelection.val(selection);

        // Set PersonNumber For Form POST
        var selectedPersonNumber = $('#selectedPersonNumber');
        var personNumber = $(this).attr("data-personNumber");
        $(selectedPersonNumber).val(personNumber);

        // Enable Get Button
        var reassignBtn = $('#getHistoryBtn');
        reassignBtn.removeAttr('disabled');
    });
}

// Agent Tranaction Reassignment Dropdown Wiring
function WireAgentReassignDropDownElements(agentDropdownNewElements, agentSelection) {
    agentDropdownNewElements.on('click', function (e) {
        e.preventDefault();

        // Set Selected 
        var selection = $(this).text();
        agentSelection.val(selection);

        // Set Values For Form POST
        var reassignToAgentPersonNumber = $('#reassignToAgentPersonNumber');
        var reassignToAgentRelationshipNumber = $('#reassignToAgentRelationshipNumber');
        var reassignToAgentOfficeId = $('#reassignToAgentOfficeId');

        var personNumber = $(this).attr("data-personNumber");
        var relationshipNumber = $(this).attr("data-relationshipNumber");
        var officeId = $(this).attr("data-officeId");

        $(reassignToAgentPersonNumber).val(personNumber);
        $(reassignToAgentRelationshipNumber).val(relationshipNumber);
        $(reassignToAgentOfficeId).val(officeId);

        // Enable Reassign Button
        var reassignBtn = $('#reassignBtn');
        reassignBtn.removeAttr('disabled');
    });
}