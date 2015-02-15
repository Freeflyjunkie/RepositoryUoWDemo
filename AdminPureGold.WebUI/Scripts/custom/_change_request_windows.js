$(document).ready(function () {
    // Comments Window
    $('.open-comment-window-button').on('click', function (e) {
        e.preventDefault();
        var changeRequestId = $(this).attr('data-changerequest-id');        

        $.ajax({
            type: "POST",
            url: GetChangeRequestCommentsUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: '{ changeRequestId: "' + changeRequestId + '"}',
            success: function (data, status, xhr) {
                var previousComments = $('#commentWindow #previousComments');
                var previousCommentsTitle = $('#commentWindow #previousCommentsTitle');

                if ($(data).length === 0) {
                    previousComments.hide();
                    previousCommentsTitle.html('<br/>');
                } else {
                    previousComments.show();
                    previousCommentsTitle.html('Previous Comments');
                    var html = "<table class='table table-condensed table-responsive table-no-margin'>";
                    html += '<tr><th style="border-top: none">Owner</th><th style="border-top: none">Comment</th></tr>';
                    $(data).each(function (idx, element) {
                        html += '<tr>';
                        html += '<td>';
                        var crdate = new Date(parseInt(element.ChangeRequestComment.CrDate.substr(6)));
                        html += element.AgentViewModel.RelateToName.LastName + ', ' + element.AgentViewModel.RelateToName.FirstName + '<br/>' + crdate.getMonth() + '/' + crdate.getDay() + '/' + crdate.getFullYear();
                        html += "</td>";
                        html += '<td>';
                        html += element.ChangeRequestComment.Comment;
                        html += "</td>";
                        html += "</tr>";
                    });
                    html += '</table>';
                    $(previousComments).html(html);
                }
            },
            error: function (xhr, status, error) {
                alert('Error occured retrieving Comments');
            }
        });

        var changeRequestInput = $('#commentWindow #changeRequestId');
        changeRequestInput.val(changeRequestId);
        $('#commentWindow').modal('show');
    });

    // Deny and Comment Window
    $('.open-deny-window-button').on('click', function (e) {
        e.preventDefault();
        var changeRequestId = $(this).attr('data-changerequest-id');
        $.ajax({
            type: "POST",
            url: GetChangeRequestCommentsUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: '{ changeRequestId: "' + changeRequestId + '"}',
            success: function (data, status, xhr) {
                var previousComments = $('#denyAndCommentWindow #previousComments');
                var previousCommentsTitle = $('#denyAndCommentWindow #previousCommentsTitle');

                if ($(data).length === 0) {
                    previousComments.hide();
                    previousCommentsTitle.html('&nbsp;');
                }
                else {
                    previousComments.show();
                    previousCommentsTitle.html('Previous Comments');

                    var html = "<table class='table table-condensed table-responsive table-no-margin'>";
                    html += '<tr><th style="border-top: none">Owner</th><th style="border-top: none">Comment</th></tr>';
                    $(data).each(function (idx, element) {
                        html += '<tr>';
                        html += '<td>';
                        var crdate = new Date(parseInt(element.ChangeRequestComment.CrDate.substr(6)));
                        html += element.AgentViewModel.RelateToName.LastName + ', ' + element.AgentViewModel.RelateToName.FirstName + '<br/>' + crdate.getMonth() + '/' + crdate.getDay() + '/' + crdate.getFullYear();
                        html += "</td>";
                        html += '<td>';
                        html += element.ChangeRequestComment.Comment;
                        html += "</td>";
                        html += "</tr>";
                    });
                    html += '</table>';
                    $(previousComments).html(html);
                }
            },
            error: function (xhr, status, error) {
                alert('Error occured retrieving Comments');
            }
        });

        var changeRequestInput = $('#denyAndCommentWindow #changeRequestId');
        changeRequestInput.val(changeRequestId);
        $('#denyAndCommentWindow').modal('show');
    });

    // Close and Comment Window
    $('.open-close-window-button').on('click', function (e) {
        e.preventDefault();
        var changeRequestId = $(this).attr('data-changerequest-id');
        $.ajax({
            type: "POST",
            url: GetChangeRequestCommentsUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: '{ changeRequestId: "' + changeRequestId + '"}',
            success: function (data, status, xhr) {
                var previousComments = $('#closeAndCommentWindow #previousComments');
                var previousCommentsTitle = $('#closeAndCommentWindow #previousCommentsTitle');

                if ($(data).length === 0) {
                    previousComments.hide();
                    previousCommentsTitle.html('&nbsp;');
                }
                else {
                    previousComments.show();
                    previousCommentsTitle.html('Previous Comments');

                    var html = "<table class='table table-condensed table-responsive table-no-margin'>";
                    html += '<tr><th style="border-top: none">Owner</th><th style="border-top: none">Comment</th></tr>';
                    $(data).each(function (idx, element) {
                        html += '<tr>';
                        html += '<td>';
                        var crdate = new Date(parseInt(element.ChangeRequestComment.CrDate.substr(6)));
                        html += element.AgentViewModel.RelateToName.LastName + ', ' + element.AgentViewModel.RelateToName.FirstName + '<br/>' + crdate.getMonth() + '/' + crdate.getDay() + '/' + crdate.getFullYear();
                        html += "</td>";
                        html += '<td>';
                        html += element.ChangeRequestComment.Comment;
                        html += "</td>";
                        html += "</tr>";
                    });
                    html += '</table>';
                    $(previousComments).html(html);
                }
            },
            error: function (xhr, status, error) {
                alert('Error occured retrieving Comments');
            }
        });

        var changeRequestInput = $('#closeAndCommentWindow #changeRequestId');
        changeRequestInput.val(changeRequestId);
        $('#closeAndCommentWindow').modal('show');
    });
});