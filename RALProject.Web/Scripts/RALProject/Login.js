
Authenticate = function () {
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
                data: { servername: $("#serverName").val(), username: $("#uname").val(), password: $("#pass").val() },
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

