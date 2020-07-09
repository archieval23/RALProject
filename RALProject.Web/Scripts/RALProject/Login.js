$(document).ready(function () {
    function Authenticate() {
        if ($("#serverName").val() == null) {
            $(".spanMessage").text("Server Name is required");
            $(".messageLoginAlert").css('display', 'block');
            $(".loginLoader").css('display', 'none');
            $(".loginButton").css('display', 'block');
        }
        else if ($("#uname").val() == "") {
            $(".spanMessage").text("User name is required");
            $(".messageLoginAlert").css('display', 'block');
            $(".loginLoader").css('display', 'none');
            $(".loginButton").css('display', 'block');
        }
        else if ($("#pass").val() == "") {
            $(".spanMessage").text("Password is required");
            $(".messageLoginAlert").css('display', 'block');
            $(".loginLoader").css('display', 'none');
            $(".loginButton").css('display', 'block');
        }
        else {
            $(".loginLoader").css('display', 'block');
            $(".loginButton").css('display', 'none');

            $.ajax({
                type: "GET",
                dataType: "json",
                url: $("#divLogin").data("request-url"),
                data: { servername: $("#serverName").val(), username: $("#uname").val(), password: $("#pass").val(), lastlogin: $("#lastloginCount").val()},
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    switch (data.responseText) {
                        case "Success":
                            data.responseText = "Welcome!" + $('#uname').val();
                            window.location.href = data.redirectToUrl;
                            break;
                        case "Failed":
                            $(".spanMessage").text("Invalid UserName or Password.")
                            $(".messageLoginAlert").css('display', 'block');
                            $(".loginButton").css('display', 'block');
                            $(".loginLoader").css('display', 'none');
                            break;
                        default:
                            break;
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
    };
    $("#uname").blur(function () {
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: $("#divCheckLastLogin").data("request-url"),
            data: { username: $('#uname').val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#lastloginCount").val(0);
                }
                else {
                    var servername = data[0].jda_connection;
                    var serverid = data[0].jda_connection_id;
                    $("#serverName").val(serverid);
                    $("#optionName").text(servername);
                    $("#lastloginCount").val(1);
                }
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    });
    $(document).on('keydown', function (e) {
        if (e.which == 13) {
            Authenticate();
        }
    });
});

