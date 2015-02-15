$(document).ready(function () {
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

    // Wire Agent Office Dropdown
    var detailButtons = $(".agent-office-details");
    detailButtons.click(function (e) {
        e.preventDefault();
        var personNumber = $(this).attr("data-person-number");
        var tableid = "#table" + personNumber;
        $(tableid).toggle();
    });

    // Property Fields
    var address = $('#address1');
    var city = $('#city');
    var state = $('#state');
    var zip = $('#zip');

    // Property (USPS)
    var uspsTable = $('#uspsTable');
    var uAddress = $('#uspsAddress1');
    var uCity = $('#uspsCity');
    var uState = $('#uspsState');
    var uZip = $('#uspsZip');
    var propertyUspsButton = $('#propertyUspsButton');
    var uspsTableUseButton = $('#uspsTableUseButton');

    // Property (BING)
    var suggestionTable = $('#bingTable');
    var sAddress = $('#bingAddress1');
    var sCity = $('#bingCity');
    var sState = $('#bingState');
    var sZip = $('#bingZip');
    var propertyBingButton = $('#propertyBingButton');
    var bingTableUseButton = $('#bingTableUseButton');

    propertyUspsButton.click(function () {
        var addressData = '{ addressLine1: "' + address.val() + '", city: "' + city.val() + '", state: "' + state.val() + '", zip: "' + zip.val() + '"}';

        $.ajax({
            type: "POST",
            url: ValidatePropertyUsingUspsUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: addressData,
            beforeSend: function () {
                $('#askingUSPS').show();
            },
            success: function (data, status, xhr) {                                
                var uspsControls = $('#uspsTable div');                
                var uspsValidatedAddressTable = $('#uspsValidatedAddressTable');
                var uspsUnknown = $('#uspsUnknown');              
                var uspsMessage = $('#uspsMessage');
                var validatedValue = "";
                
                uspsMessage.text("");
                uspsTable.show();
                uspsControls.removeClass();
                uspsValidatedAddressTable.hide();

                if (data.Validated == "0") {
                    validatedValue = "No";
                    uspsControls.addClass("inValidPropertyInformation");
                }

                if (data.Validated == "1") {
                    validatedValue = "Yes";
                    uspsControls.addClass("validPropertyInformation");
                }

                if (data.Validated == "2") {
                    validatedValue = "Kinda...";
                    uspsControls.addClass("quaziValidPropertyInformation");
                }

                if (data.Address1 === "" || data.City === "" || data.State === "" || data.Zip === "") {
                    uspsValidatedAddressTable.hide();
                    uspsUnknown.show();                    
                    uspsTableUseButton.attr("disabled", "disabled");
                }
                else {
                    uAddress.text(data.Address1);
                    uCity.text(data.City);
                    uState.text(data.State);
                    uZip.text(data.Zip);
                    uspsValidatedAddressTable.show();
                    uspsUnknown.hide();
                    uspsTableUseButton.removeAttr("disabled");
                }

                if (data.ErrorCode !== "")
                    uspsMessage.append("<strong>Error : </strong>" + data.ErrorCode + "<br/>");

                if (data.DpvNotesDescription !== "")
                    uspsMessage.append("<strong>Delivery Point Notes : </strong>" + data.DpvNotesDescription + ". <br/>");

                if (data.GeocodeLevelDescription !== "")
                    uspsMessage.append("<strong>Geocode : </strong>" + data.GeocodeLevelDescription + "<br/>");

                if (data.CorrectionDescription !== "")
                    uspsMessage.append("<strong>Corrections : </strong>" + data.CorrectionDescription + "<br/>");

                uspsMessage.append("<strong>Validated : </strong>" + validatedValue);
            },
            error: function (xhr, status, error) {
                alert(status);
                alert(error);
            },
            complete: function () {
                $('#askingUSPS').hide();
            }
        });
    });

    uspsTableUseButton.click(function () {
        var transactionId = $('#transactionid');
        var addressUspsData = '{ transactionId: "' + transactionId.val() + '", addressLine1: "' + uAddress.text() + '", city: "' + uCity.text() + '", state: "' + uState.text() + '", zip: "' + uZip.text() + '"}';
        $.ajax({
            type: "POST",
            url: SaveTransactionPropertyAjaxUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: addressUspsData,
            beforeSend: function () {
                $('#usingUsps').show();
            },
            success: function (data, status, xhr) {                
                window.location.href = QualityAssuranceEditByTransactionId + "?transactionId=" + transactionId.val();
            },
            error: function (xhr, status, error) {                
                alert(status);
                alert(error);
            },
            complete: function () {
                $('#usingUsps').hide();
            }
        });
    });      
   
    propertyBingButton.click(function () {
        var addressData = '{ addressLine1: "' + address.val() + '", city: "' + city.val() + '", state: "' + state.val() + '", zip: "' + zip.val() + '"}';

        $.ajax({
            type: "POST",
            url: FindLocationByAddressUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: addressData,
            beforeSend: function () {
                $('#askingBing').show();
            },
            success: function (data, status, xhr) {
                //var bingControls = $('#bingTable div');                

                var bingValidatedAddressTable = $('#bingValidatedAddressTable');
                var bingUnknown = $('#bingUnknown');
                var bingMessage = $('#bingMessage');
                var locationIndex = $('#locationIndex');
                var index = parseInt(locationIndex.val(), 10);                
                

                if (parseInt(index, 10) >= data.length) {
                    index = 0;
                    locationIndex.val(index);
                } else {
                    locationIndex.val(parseInt(index, 10) + 1);
                }
               
                var confidence = data[index].Confidence;
                //var validatedValue = "";
                bingMessage.text("");
                suggestionTable.show();
                //bingControls.removeClass();
                bingValidatedAddressTable.hide();

                switch (confidence) {
                    case "HIGH":
                        //bingControls.addClass("validPropertyInformation");
                        sAddress.text(data[index].Address);
                        sCity.text(data[index].City);
                        sState.text(data[index].State);
                        sZip.text(data[index].Zip);
                        //validatedValue = "Yes";
                        break;
                    case "MEDIUM":
                        //bingControls.addClass("quaziValidPropertyInformation");
                        sAddress.text(data[index].Address);
                        sCity.text(data[index].City);
                        sState.text(data[index].State);
                        sZip.text(data[index].Zip);
                        //validatedValue = "Kinda...";
                        break;
                    case "LOW":
                        //bingControls.addClass("inValidPropertyInformation");
                        //validatedValue = "No";
                        break;
                    default:
                }

                if (data[index].Address === null || data[index].City === null || data[index].State === null || data[index].Zip === null) {
                    bingValidatedAddressTable.hide();
                    bingUnknown.show();
                    //bingControls.addClass("inValidPropertyInformation");
                    //validatedValue = "No";
                    bingTableUseButton.attr("disabled", "disabled");
                }
                else {
                    sAddress.text(data[index].Address);
                    sCity.text(data[index].City);
                    sState.text(data[index].State);
                    sZip.text(data[index].Zip);
                    bingValidatedAddressTable.show();
                    bingUnknown.hide();
                    bingTableUseButton.removeAttr("disabled");
                }

                bingMessage.append("<strong>Entity Type : </strong>" + data[index].EntityType + "<br/>");
                bingMessage.append("<strong>Suggestion Confidence : </strong>" + confidence + "<br/>");
            },
            error: function (xhr, status, error) {
                alert(status);
                alert(error);
            },
            complete: function () {
                $('#askingBing').hide();
            }
        });
    });

    bingTableUseButton.click(function () {
        var transactionId = $('#transactionid');
        var addressBingData = '{ transactionId: "' + transactionId.val() + '", addressLine1: "' + sAddress.text() + '", city: "' + sCity.text() + '", state: "' + sState.text() + '", zip: "' + sZip.text() + '"}';

        $.ajax({
            type: "POST",
            url: SaveTransactionPropertyAjaxUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: addressBingData,
            beforeSend: function () {
                $('#usingBing').show();
            },
            success: function (data, status, xhr) {
                window.location.href = QualityAssuranceEditByTransactionId + "?transactionId=" + transactionId.val();
            },
            error: function (xhr, status, error) {
                alert(status);
                alert(error);
            },
            complete: function () {
                $('#usingBing').hide();
            }
        });
    });
});

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

        var agentPersonNumber = $('#addedPersonNumber');
        var personNumber = $(this).attr("data-personNumber");
        $(agentPersonNumber).val(personNumber);

        var agentRelationshipNumber = $('#addedRelationshipNumber');
        var relationshipNumber = $(this).attr("data-relationshipNumber");
        $(agentRelationshipNumber).val(relationshipNumber);        

        var agentOfficeId = $('#addedOfficeId');
        var officeId = $(this).attr("data-officeId");
        $(agentOfficeId).val(officeId);
    });
}

//function CommentUpdateSuccess() {
//    alert('Comments sent to agent.');
//    $('#comments').val('');
//}

//function CommentUpdateFailure() {
//    alert('Tried to send comments, but failed.');
//}

function EnterSurvey(saleId)
{
    alert(saleId);
}