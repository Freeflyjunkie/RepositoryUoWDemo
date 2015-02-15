var surveyId = 0;

$(document).ready(function () {
    WireEvents();
    surveyId = $('input[name="txtSurvey"]').val();
});

function WireEvents() {

    $('.row[data-hover="highlight"]').hover(
        function () {
            $(this).toggleClass("Background-highlight");
        }
    );

    $('.link[data-hover="link"]')
        .hover( function () { $(this).toggleClass("link-class"); })
        .on("click", function () { ShowSurveysWithChoiceId($(this).data("choiceid")); });

    $('.link[data-hover="sale"]')
        .hover(function () { $(this).toggleClass("link-class"); })
        .on("click", function () { ShowSurvey($(this).data("saleid")); });
}

function ShowSurveysWithChoiceId(choiceId)
{
    var sDate = $('input[name="startDate"]').val();
    var eDate = $('input[name="endDate"]').val();

    var url = "ReportDetail?choiceId=" + choiceId + "&startDate=" + sDate + "&endDate=" + eDate;
    window.open(url, "_parent");
}

function ShowSurvey(saleid) {

    var url = "ViewSurvey?saleId=" + saleid;
    window.open(url, "_parent");
}

function ResetQuestion(questionId)
{
    $('input[data-questionid="' + questionId + '"]').each(function () {
        if ($(this).prop('checked'))
        {
            DeleteSurveyAnswer(surveyId, $(this).val());
            try {
                var matchingTextBox = $('input[name="trb_' + $(this).val() + '"]');
                $(matchingTextBox).val("");
            }
            catch (e) { }
            try {
                var matchingTextBox = $('input[name="tch_' + $(this).val() + '"]');
                $(matchingTextBox).val("");
            }
            catch (e) { }

            $(this).prop('checked', false);
        }
        else
        {
            if ( $(this).data("type") == "fillin" )
            {
                var choiceId = $(this).data("choiceid");
                DeleteSurveyAnswer(surveyId, choiceId);
                try {
                    var matchingTextBox = $('input[name="trb_' + choiceId + '"]');
                    $(matchingTextBox).val("");
                }
                catch (e) { }
                try {
                    var matchingTextBox = $('input[name="tch_' + choiceId + '"]');
                    $(matchingTextBox).val("");
                }
                catch (e) { }
            }
        }

    });
}

function SaveCheckBoxValue(surveyChoiceId) {
    var isChecked = $('input[name="chb_' + surveyChoiceId + '"]').prop('checked');
    if (isChecked) {
        SaveSurveyAnswer(surveyId, surveyChoiceId);
    }
    else {
        DeleteSurveyAnswer(surveyId, surveyChoiceId);
    }
}

function SaveRadioButtonValue(questionId, surveyChoiceId) {
    $('input[name="rdb_' + questionId + '"]').each(function ()
    {
        if ($(this).prop('checked'))
        {
            SaveSurveyAnswer(surveyId, surveyChoiceId);
        }
        else
        {
            DeleteSurveyAnswer(surveyId, $(this).val());
                try {
                    var matchingTextBox = $('input[name="trb_' + $(this).val() + '"]');
                    $(matchingTextBox).val("");
                }
                catch (e) { }
            }
    });
}

function SaveCheckBoxValueAndSetText(surveyChoiceId)
{
    var isChecked = $('input[name="cht_' + surveyChoiceId + '"]').prop('checked');
    var matchingTextBox = $('input[name="tch_' + surveyChoiceId + '"]');

    if (isChecked) {
        SaveSurveyAnswer(surveyId, surveyChoiceId);
        $(matchingTextBox).prop( "disabled", false );   
    }
    else {
        DeleteSurveyAnswer(surveyId, surveyChoiceId);

        $(matchingTextBox).val("");
        $(matchingTextBox).prop("disabled", true);
    }
}

function SaveTextBoxValue(surveyChoiceId)
{
    var matchingTextBox = $('input[name="tch_' + surveyChoiceId + '"]');

    var value = $(matchingTextBox).val().length;
    if (value > 0)
    {
        SaveSurveyAnswerText(surveyId, surveyChoiceId, $(matchingTextBox).val());
            try {
                var matchingCheckBox = $('input[name="cht_' + surveyChoiceId + '"]');
                $(matchingCheckBox).attr('checked', true);
            }
            catch (e) { alert(e.message); }
        }
    else
    {
        DeleteSurveyAnswer(surveyId, surveyChoiceId);
        try
        {
            var matchingCheckBox = $('input[name="cht_' + surveyChoiceId + '"]');
            $(matchingCheckBox).attr('checked', false);
        }
        catch(e) { }
    }
}

function SaveTextBoxValueForRadioButton(surveyChoiceId) {
    var matchingTextBox = $('input[name="trb_' + surveyChoiceId + '"]');

    SaveSurveyAnswerText(surveyId, surveyChoiceId, $(matchingTextBox).val());
}

function SaveSurveyAnswer(surveyId, choiceId) {
    var answerData = '{ surveyId: "' + surveyId + '", choiceId: "' + choiceId + '"}';
    $.ajax({
        type: "POST",
        url: SaveSurveyAnswerUrl,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: answerData,
        success: function (data, status, xhr) {
        },
        error: function (xhr, status, error) {
            alert(error.data);
            // alert("There was an error adding that answer; please refresh the page and try again.");
        }
    });
}

function SaveSurveyAnswerText(surveyId, choiceId, surveyAnswerText) {
    var answerData = '{ surveyId: "' + surveyId + '", choiceId: "' + choiceId + '", surveyAnswerText: "' + surveyAnswerText + '"}';
    $.ajax({
        type: "POST",
        url: SaveSurveyAnswerTextUrl,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: answerData,
        success: function (data, status, xhr) {
        },
        error: function (xhr, status, error) {
            alert("There was an error adding that answer; please refresh the page and try again.");
        }
    });
}

function DeleteSurveyAnswer(surveyId, choiceId) 
{
    var intId = parseInt(choiceId);
    if (intId > 0)
    {
        var answerData = '{ surveyId: "' + surveyId + '", choiceId: "' + choiceId + '"}';
        $.ajax({
            type: "POST",
            async: true,
            url: DeleteSurveyAnswerUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: answerData,
            success: function (data, status, xhr) {
            },
            error: function (xhr, status, error) {
                alert("There was an error removing answer " + choiceId + "; please refresh the page and try again.");
            }
        });
    }
}

