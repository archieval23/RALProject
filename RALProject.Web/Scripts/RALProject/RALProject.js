
$(document).ready(function () {
    $("#idmodalStore").on("click", function (e) {
        e.preventDefault();
        LoadStorePartialView();
    });

    $("#idmodalVendor").on("click", function (e) {
        e.preventDefault();
        LoadVendorPartialView();
    });

    $("#idmodalPO").on("click", function (e) {
        e.preventDefault();
        LoadPurchaseOrderPartialView();
    });

    $("#id-po-vendor-button").on("click", function (e) {
        e.preventDefault();
        LoadVendorPartialView();
    });

    $('#id-text-storeNumbet-at').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            LoadStorePartialView();
        }
        event.stopPropagation();
    });

    $('#id-text-vendor-at').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            LoadVendorPartialView();
        }
        event.stopPropagation();
    });

    $('#id-text-PO-at').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            LoadPurchaseOrderPartialView();
        }
        event.stopPropagation();
    });

    $("#id-Accept-button").on("click", function (e) {
        e.preventDefault();
        if (($('#idFromDate').val() == '')
            && ($('#idToDate').val() == '')
            && ($('#id-text-storeNumber').val() == '')
            && ($('#id-text-Vendor').val() == '')
            && ($('#id-text-PO').val() == '')
        ) {
            $('#id-entry-div-message').removeClass('d-none');
            $('#id-entry-span-massage').text("Entry is required.");
            return false;
        }
        else {
            if ($('#id-text-PO').val() != '') {
                if (($('#idFromDate').val() != '') || ($('#idToDate').val() != '')) {
                    $('#id-datefromTo-div-message').removeClass('d-none');
                    $('#id-datefromTo-span-massage').text("If PO Number is selected, Dates Should be zero.");
                }
                if ($('#id-text-storeNumber').val() != '') {
                    $('#id-storenumber-div-message').removeClass('d-none');
                    $('#id-storenumber-span-massage').text("If PO Number is selected, Store number Should be zero.");
                }
                if ($('#id-text-Vendor').val() != '') {
                    $('#id-vendor-div-message').removeClass('d-none');
                    $('#id-vendor-span-massage').text("If PO Number is selected, Vendor Should be zero.");
                }
                CreateReports();
            }
            else {
                if (($('#id-text-storeNumber').val() != '') || ($('#id-text-Vendor').val() != '')) {
                    if (($('#idFromDate').val() != '') && ($('#idToDate').val() != '')) {
                        CreateReports();
                    }
                    else {
                        $('#id-datefromTo-div-message').removeClass('d-none');
                        $('#id-datefromTo-span-massage').text("Dates must be required.");
                    }
                }
                else {
                    $('#id-datefromTo-div-message').removeClass('d-none');
                    $('#id-datefromTo-span-massage').text("Store Number or vendor must be required.");
                }
            }
        }  
    });
});

function CreateReports() {
    var report = {
        pONumber: $("#id-text-PO").val(),
        storeNumber: $("#id-text-storeNumber").val(),
        vendorCode: $("#id-text-Vendor").val(),
        receivingDate: $("#idFromDate").val(),
        cancelDate: $("#idToDate").val(),
    };

    $.ajax({
        type: "POST",
        url: $("#divReportGeneration").data("request-url"),
        data: JSON.stringify({ reportModel: report }),
        contentType: 'application/json',
        async: false,
        success: function (data) {
            //var url = window.location.href;
            //window.location.href = url;
            //$('#idModalEditRoles').modal('hide');
            //$("#loading").addClass("d-none");
            $('#id-vendor-div-message').removeClass('d-none');
            $('#id-vendor-span-massage').text("Successfully processed report..");
            alert("test");
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

$('#id-dateFrom-div-message .close').click(function () {
    $('#id-dateFrom-div-message').addClass('d-none');
    $('#idFromDate').val("");
});

$('#id-dateTo-div-message .close').click(function () {
    $('#id-dateTo-div-message').addClass('d-none');
    $('#idToDate').val("");
});

$('#id-entry-div-message .close').click(function () {
    $('#id-entry-div-message').addClass('d-none');
    $('#idToDate').val("");
});

$('#id-storenumber-div-message .close').click(function () {
    $('#id-storenumber-div-message').addClass('d-none');
    $('#id-text-storeNumber').val("");
    $('#id-spand-storeName').val("");
});

$('#id-vendor-div-message .close').click(function () {
    $('#id-vendor-div-message').addClass('d-none');
    $('#id-text-Vendor').val("");
    $('#id-spand-vendorName').val("");
});

$('#id-datefromTo-div-message .close').click(function () {
    $('#id-datefromTo-div-message').addClass('d-none');
    $('#idFromDate').val("");
    $('#idToDate').val("");
});

function LoadStorePartialView() {
    //PageLoader();
    $.ajax({
        type: "GET",
        dataType: 'html',
        url: $("#divGetPartialStore").data("request-url"),
        data: { filter: $('#idsearchFormControlSelect').val(), value: $('#id-text-storeNumbet-at').val() },
        contentType: false,
        success: function (data) {
            $("#DivStoreList").html(data);
            $('#idModalStoreView').modal('show');
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function LoadVendorPartialView() {
    //PageLoader();
    $.ajax({
        type: "GET",
        dataType: 'html',
        url: $("#divGetPartialVendor").data("request-url"),
        data: { value: $('#id-text-vendor-at').val() },
        contentType: false,
        success: function (data) {
            $("#DiVendorList").html(data);
            $('#idModalVendorView').modal('show');
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function LoadPurchaseOrderPartialView() {
    //PageLoader();
    $.ajax({
        type: "GET",
        dataType: 'html',
        url: $("#divGetPartialPurchaseOrder").data("request-url"),
        data: { value: $('#id-text-PO-at').val() },
        contentType: false,
        success: function (data) {
            $("#DivPOList").html(data);
            $('#idModalPOView').modal('show');
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}
