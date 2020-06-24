
$(document).ready(function () {
    $("#idmodalStore").on("click", function (e) {
        e.preventDefault();
        $('#idmodalStore').prop('disabled', true);
        $("#divStoreLoader").show();
        LoadStorePartialView();
    });

    $("#idmodalVendor").on("click", function (e) {
        e.preventDefault();
        $('#idmodalVendor').prop('disabled', true);
        $("#divVendorLoader").show();
        LoadVendorPartialView();
    });

    $("#idmodalPO").on("click", function (e) {
        e.preventDefault();
        $('#idmodalPO').prop('disabled', true);
        $("#divPOLoader").show();
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


    //Accept Button Validations
    $("#id-Accept-button").on("click", function (e) {
        e.preventDefault();
        //No Entry
        //alert('Accept Button is Clicked');
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
            //PO is Selected
            if ($('#id-text-PO').val() != '') {
                //Date Must be Null
                if (($('#idFromDate').val() != '') || ($('#idToDate').val() != '')) {
                    $('#id-datefromTo-div-message').removeClass('d-none');
                    $('#id-datefromTo-span-massage').text("If PO Number is selected, Dates Should be zero.");
                }
                //Store and Vendor Must be Zero or Empty
                else if ($('#id-text-storeNumber').val() != '' || $('#id-text-Vendor').val() != '') {
                    if ($('#id-text-storeNumber').val() == '0' && $('#id-text-Vendor').val() == '0') {
                        CreateReports();
                    }
                    else if ($('#id-text-storeNumber').val() == '0' && $('#id-text-Vendor').val() == '') {
                        CreateReports();
                    }
                    else if ($('#id-text-storeNumber').val() == '' && $('#id-text-Vendor').val() == '0') {
                        CreateReports();
                    }
                    else {
                        $('#id-storenumber-div-message').removeClass('d-none');
                        $('#id-storenumber-span-massage').text("If PO Number is selected, Store number and Vendor number Should be zero.");
                    }
                }
                //Create Report
                else {
                    CreateReports();
                }
            }
            //ReceiptDate is Selected
            else {
                if (($('#id-text-storeNumber').val() != '') || ($('#id-text-Vendor').val() != '')) {
                    if (($('#idFromDate').val() != '') && ($('#idToDate').val() != '')) {
                        CreateReports();
                    }
                    else {
                        $('#id-datefromTo-div-message').removeClass('d-none');
                        $('#id-datefromTo-span-massage').text("Check for Required Dates");
                    }
                }
                else {
                    $('#id-datefromTo-div-message').removeClass('d-none');
                    $('#id-datefromTo-span-massage').text("Store Number or Vendor must be required.");
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
        success: function () {
            CreatePDF();
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function CreatePDF() {
    //<div id="divPDFGeneration" data-request-url="@Url.Action(" CreatePdf", "Report")" ></div >
    //var pdfAction = '@Url.Action("CreatePdf","Report")';
    $.ajax({
        url: $("#divPDFGeneration").data("request-url"),
        contentType: "application/json; charset=utf-8",
        success: function () {
            ClearFields();
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function ClearFields() {

    $('#id-success-div-message').removeClass('d-none');
    $('#id-success-span-massage').text("Successfully processed report..");

    $('#id-dateFrom-div-message').addClass('d-none');
    $('#idFromDate').val("");

    $('#id-dateTo-div-message').addClass('d-none');
    $('#idToDate').val("");

    $('#id-entry-div-message').addClass('d-none');
    $('#idToDate').val("");

    $('#id-storenumber-div-message').addClass('d-none');
    $('#id-text-storeNumber').val("");
    $('#id-spand-storeName').val("");

    $('#id-vendor-div-message').addClass('d-none');
    $('#id-text-Vendor').val("");
    $('#id-spand-vendorName').val("");

    $('#id-po-div-message').addClass('d-none');
    $('#id-text-PO').val("");
    $('#id-spand-description').val("");

    $('#id-datefromTo-div-message').addClass('d-none');
    $('#idFromDate').val("");
    $('#idToDate').val("");
}

$('#id-success-div-message .close').click(function () {
    $('#id-success-div-message').addClass('d-none');
});

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
            $("#divStoreLoader").hide();
            $('#idmodalStore').prop('disabled', false);
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
            $("#divVendorLoader").hide(); 
            $('#idmodalVendor').prop('disabled', false);
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
            $("#divPOLoader").hide();
            $('#idmodalPO').prop('disabled', false);
            $("#DivPOList").html(data);
            $('#idModalPOView').modal('show');
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}
