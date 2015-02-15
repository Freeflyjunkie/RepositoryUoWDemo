$(document).ready(function ()
{
    WireEvents();
});

function WireEvents()
{
    var printTypeDropDown = $('#printTypeDropDown li a');
    var printTypeSelection = $('#printTypeSelection');
    var printTypeDescription = $('#printType');

    printTypeDropDown.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        printTypeSelection.text(selection);
        printTypeDescription.val(selection);

        if (selection == "Anniversary/Follow up")
        {
            $('div[data-do-toggle="yes"]').each(function () {
                $(this).toggleClass("hidden", false);
            });
        }
        else
        {
            $('div[data-do-toggle="yes"]').each(function () {
                $(this).toggleClass("hidden", true);
            });
        }
    });

    var month1DropDown = $('#month1DropDown li a');
    var month1Selection = $('#month1Selection');
    var month1Hidden = $('#month1');

    month1DropDown.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        month1Selection.text(selection);
        month1Hidden.val(selection);

        if (selection == "Anniversary/Follow up") {
            alert("Its an anniversary!!");
        }
        else {

        }
    });

    var year1DropDown = $('#year1DropDown li a');
    var year1Selection = $('#year1Selection');
    var year1Hidden = $('#year1');

    year1DropDown.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        year1Selection.text(selection);
        year1Hidden.val(selection);

        if (selection == "Anniversary/Follow up") {
            alert("Its an anniversary!!");
        }
        else {

        }
    });


    var month2DropDown = $('#month2DropDown li a');
    var month2Selection = $('#month2Selection');
    var month2Hidden = $('#month2');

    month2DropDown.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        month2Selection.text(selection);
        month2Hidden.val(selection);

        if (selection == "Anniversary/Follow up") {
            alert("Its an anniversary!!");
        }
        else {

        }
    });

    var year2DropDown = $('#year2DropDown li a');
    var year2Selection = $('#year2Selection');
    var year2Hidden = $('#year2');

    year2DropDown.on('click', function (e) {
        e.preventDefault();
        var selection = $(this).text();
        year2Selection.text(selection);
        year2Hidden.val(selection);

        if (selection == "Anniversary/Follow up") {
            alert("Its an anniversary!!");
        }
        else {

        }
    });

    var datepickers = $('.datepicker');

    datepickers.datepicker({ format: "mm/dd/yyyy" }).on('changeDate', function (e) {
        datepickers.datepicker('hide');
    });

    var sendEmailButton = $("#sendEmails");
    
    sendEmailButton.on("click", function () {
        var dueDate = $("#dueDate").val();

        if (dueDate.length > 1)
        {
            SetDueDateAndSendEmailFlag(dueDate);
            $('div[data-toggle="true"]').toggleClass("hidden", false);
            UpdateDisplayTags();
        }
        else
        {
            alert("Please select a due date");
        }
    })


    var $h2 = $('h2[data-heading="header"]').html();

    if ($h2 == "Email Reminders - Emails Are Sending")
    {
        $('div[data-toggle="true"]').toggleClass("hidden", false);
        $('section[data-name="datapicker"]').toggleClass("hidden", true);
        
        setInterval(function () { UpdateDisplayTags(); }, 5000);
    }

}

function SetDueDateAndSendEmailFlag(dueDate) {
    var dateData = '{ duedate: "' + dueDate + '"}';
    var success = false;

    $.ajax({
        type: "POST",
        async: false,
        url: SaveDueDateAndSetEmailFlagUrl,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: dateData,
        success: function (data, status, xhr) {
            alert("Email Sending Process has started");
            success = true;
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText)
            alert("There was an error setting; please refresh the page and try again.");
        },
        complete: function () {
            if (success == true)
            {
                setInterval(function () { UpdateDisplayTags(); }, 5000);
            }
        }
    });
}

function UpdateDisplayTags()
{
    $.ajax({
        type: "GET",
        async: false,
        url: SendingEmail_StatusNumbersUrl,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data, status, xhr) {
        },
        error: function (xhr, status, error) {
            alert("error getting updates");
        },
        complete: function (xhr, status) {
            handleData(xhr);
        }
    });
}

function handleData(data)
{
    var $response = data.responseText;
    var array = $response.split(":");

    var sent = array[0].replace("\"", "");
    var unsent = array[1].replace("\"", "");
    var d = new Date()

    $("#divUnsent").html("Emails to be sent: " + unsent);
    $("#divSent").html("Emails sent: " + sent);
    $("#lastCheck").html("Last Check: " + d);

    if (unsent == "0")
    {
        $('#divReset').toggleClass("hidden", false);
    }
    
}
