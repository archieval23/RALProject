Authenticate = function () {
    ClearErrorMessage();
    $.get(UrlHelp('/Security/Authenticate'), {
        username: $('#uname').val(),
        password: $('#pass').val()
    }, function (res) {
        $('.login').data("isactivedir", false);
        switch (res.responseText) {
            case "Success":
                res.responseText = "Welcome!" + $('#uname').val();
                window.location.href = res.redirectToUrl;
                break;
            case "ForRegistration":
                SwitchDisplay('.validateEmployee', '.login');
                $('.login').data("isactivedir", true);
                DisplayAlerMessage(res = {
                    responseText: "Employee number validation.",
                    description: "Your User name is associated with an active directory account. Please enter your Employee number to proceed with the activation."
                }, "alert-info");
                break;
            case "RegistryForApproval":
                DisplayAlerMessage(res = {
                    responseText: "Registration for approval.",
                    description: "Please follow up your signatory for registration approval."
                }, "alert-info");
                break;
            case "RegistryDenied":
                res.responseText = "Registration denied!";
                res.description = "Please try to register again.";
                DisplayAlerMessage(res, "alert-danger");
                break;
            case "Failed":
                res.responseText = "User name or Password is incorrect!";
                DisplayAlerMessage(res, "alert-danger");
                break;
            case "NotFound":
                res.responseText = "User name or Password is incorrect!";
                res.description = "Please register if your Employee number is not yet enrolled.";
                DisplayAlerMessage(res, "alert-danger");
                break;
            //case "NotRegistered":
            //    res.responseText = "User name not registered!";
            //    break;
            default:
                break;
        };
    }).fail(function (res) {
        DisplayAlerMessage(res, "alert-danger");
    });
}

ClearErrorMessage = function () {
    $(".alertPlaceHolder").empty();
}

DisplayAlerMessage = function (error, type) {
    ClearErrorMessage();
    var alertTitle = error.responseText;
    var alertDescription = "";
    if (error == undefined) {
        alertTitle = "Application has encountered a problem.";
        alertDescription = "The operation you requested has been terminated by the system. Contact us @ ALLISDAppDevGroup@retaildomain.com.ph Thank you!";
    }
    if (error.description != undefined) {
        alertDescription = error.description;
    }

    $(".alertPlaceHolder").empty();
    $(".alertPlaceHolder").append("<div class='alert " + type + " alert-dismissible fade show' role='alert'><strong>"
        + alertTitle + "</strong> "
        + alertDescription + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
        + "<span aria-hidden='true'>&times;</span></button></div>");
}

UrlHelp = function (controllerAction) {
    return controllerAction;
}

Logout = function () {
    $.post(UrlHelp('/Security/Logout'), {
    }, function (res) {
        alert(res.responseText);
        window.location.href = res.redirectToUrl;
    }).fail(function (res) {
        DisplayAlerMessage(res, "alert-danger");
    });
}