var usernameArray = [];
$('document').ready(function () {
    debugger
    $.ajax({
        url: "Student/GetUsers",
        method: "Get",
        datatype: "json",
        success: function (data) {
            let parsedData = JSON.parse(data);
            for (let i = 0; i < parsedData.length; i++) {
                usernameArray.push(parsedData[i]["username"]);
            }
        },
        error: function (error) {
        }
    });
});
$('#username').on('keyup', function () {
    debugger
    let val = $('#username').val();
    if (val.length > 5) {
        if (!usernameArray.includes(val)) {
            $('#usernameValidate').text('');
        } else {
            $('#usernameValidate').text('Username already taken');
        }
    } else {
        $('#usernameValidate').text('Username length should be greater than 6 characters.');

    }

});