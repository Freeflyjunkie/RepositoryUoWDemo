$(document).ready(function() {
    for (var i = 0; i < 5; i++) {        
        $.ajax({
            type: "POST",
            url: SendEmailsUrl,
            dataType: 'json',
            data: '{delay: "' + i + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (data, status, xhr) {
                var newRow = '<tr><td>';
                newRow = newRow + data.RelateToName.FirstName + ' ' + data.RelateToName.LastName;
                newRow = newRow + '</td><td>';
                newRow = newRow +
                    '<div>' +
                    data.Office.FriendlyOfficeName +
                    '</div>' +
                    '<div>' +
                    data.Office.Address +
                    '</div>'+
                    '<div>' +
                    data.Office.City + ' ' + data.Office.State + ' ' + data.Office.ZipCode +
                    '</div>';
                newRow = newRow + '</td><td>';
                newRow = newRow + data.RelateToEmail.EmailAddress;
                newRow = newRow + '</td><td>';
                newRow = newRow + '<h3>Success</h3>';
                newRow = newRow  +'</td></tr>';
                $('#sendingEmailsTable tbody').append(newRow);
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    }    
});