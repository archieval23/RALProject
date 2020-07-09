$(document).ready(function () {
    function showPleaseWait() {
        if (document.querySelector("#pleaseWaitDialog") == null) {
            var modalLoading = '<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog">\
            <div class="modal-dialog">\
                <div class="modal-content">\
                    <div class="modal-header">\
                        <h4 class="modal-title">Creating the Report Please wait...</h4>\
                    </div>\
                    <div class="modal-body">\
                        <div class="progress">\
                          <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar"\
                          aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width:100%; height: 40px">\
                          </div>\
                        </div>\
                    </div>\
                </div>\
            </div>\
        </div>';
            $(document.body).append(modalLoading);
        }
        $("#pleaseWaitDialog").modal("show");
    }
    function hidePleaseWait() {
        $("#pleaseWaitDialog").modal("hide");
    }
    $("#idmodalStore").on("click", function (e) {
        e.preventDefault();
        InitializeStore();
        LoadStorePartialView();
    });

    $("#idmodalVendor").on("click", function (e) {
        e.preventDefault();
        InitializeVendor();
        LoadVendorPartialView();
    });

    $("#idmodalPO").on("click", function (e) {
        e.preventDefault();
        InitializePO();
        LoadPurchaseOrderPartialView();
    });

    function InitializeStore() {
        $('#idmodalStore').prop('disabled', true);
        $("#divStoreLoader").show();
        $("#id-spand-storeName").text('');
    }
    function InitializeVendor() {
        $('#idmodalVendor').prop('disabled', true);
        $("#divVendorLoader").show();
        $("#id-spand-vendorName").text('');
    }
    function InitializePO() {
        $('#idmodalPO').prop('disabled', true);
        $("#divPOLoader").show();
        $("#id-spand-description").text('');
    }

    //F3 and F7 Keyboard Events
    //Store
    $("#id-text-storeNumber").keydown(function (key) {
        if (key.which == 114) {
            InitializeStore();
            LoadStoreByID();
        }
    });
    //Vendor
    $("#id-text-Vendor").keydown(function (key) {
        if (key.which == 114) {
            InitializeVendor();
            LoadVendorByID();
        }
    });
    //PO
    $("#id-text-PO").keydown(function (key) {
        if (key.which == 114) {
            InitializePO();
            LoadPOByID();
        }
    });
    //Generate
    $(document).on('keydown', function (e) {
        if (e.which == 118) {
            generateRal();
        }
    });
    //F3 and F7 Keyboard Events End

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

    function generateRal() {
        //Empty Fields
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
                if (($('#idFromDate').val() != '') || ($('#idToDate').val() != '')) {
                    $('#id-datefromTo-div-message').removeClass('d-none');
                    $('#id-datefromTo-span-massage').text("If PO Number is selected, Dates Should be zero.");
                }
                else if ($('#id-text-storeNumber').val() != '') {
                    $('#id-storenumber-div-message').removeClass('d-none');
                    $('#id-storenumber-span-massage').text("If PO Number is selected, Store number Should be zero.");
                }
                else if ($('#id-text-Vendor').val() != '') {
                    $('#id-vendor-div-message').removeClass('d-none');
                    $('#id-vendor-span-massage').text("If PO Number is selected, Vendor Should be zero.");
                }
                else {
                    showPleaseWait();
                    CreateReports();
                }
            }
            //Date is Selected
            else {
                if (($('#id-text-storeNumber').val() != '') || ($('#id-text-Vendor').val() != '')) {
                    if (($('#idFromDate').val() != '') && ($('#idToDate').val() != '')) {
                        showPleaseWait();
                        CreateReports();
                    }
                    else {
                        $('#id-datefromTo-div-message').removeClass('d-none');
                        $('#id-datefromTo-span-massage').text("Dates must be required.");
                    }
                }
                else {
                    $('#id-datefromTo-div-message').removeClass('d-none');
                    $('#id-datefromTo-span-massage').text("Store Number or Vendor number must be valid and required.");
                }
            }
        }  

    }
    //Accept Button Validations
    $("#id-Accept-button").on("click", function (e) {
        e.preventDefault();
        generateRal();  
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
            dataType: 'json',
            url: $("#divReportGeneration").data("request-url"),
            data: JSON.stringify({ reportModel: report }),
            contentType: 'application/json',
            async:false,
            success: function (data) {
                if (data > 0) {
                    hidePleaseWait()
                    RALSuccess();
                }
                else {
                    hidePleaseWait();
                    $('#id-entry-div-message').removeClass('d-none');
                    $('#id-entry-span-massage').text("No Results Found, Report is not generated.");
                    $('#id-success-div-message').addClass('d-none');
                }
               
            },
            error: function (xhr, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    }

    function RALSuccess() {
        var reportUrl = "Reports/RALReport.aspx";
        window.open(reportUrl, '_blank');

        $('#id-success-div-message').removeClass('d-none');
        $('#id-success-span-massage').text("Successfully processed report..");
    }

    $('#id-success-div-message .close').click(function () {
        $('#id-success-div-message').addClass('d-none');
    });

    $('#id-dateFrom-div-message .close').click(function () {
        $('#id-dateFrom-div-message').addClass('d-none');
    });

    $('#id-dateTo-div-message .close').click(function () {
        $('#id-dateTo-div-message').addClass('d-none');
    });

    $('#id-entry-div-message .close').click(function () {
        $('#id-entry-div-message').addClass('d-none');
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


    function LoadStoreByID() {
        //PageLoader();
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: $("#divGetStoreByID").data("request-url"),
            data: { value: $('#id-text-storeNumber').val() },
            success: function (data) {
                $("#divStoreLoader").hide();
                $('#idmodalStore').prop('disabled', false);
                if (data.length == 0) {
                    $("#id-spand-storeName").text("No Store Found with ID " + $('#id-text-storeNumber').val());
                    $("#id-spand-storeName").css("color", "red");
                }
                else {
                    var storeNumber = data[0].store;
                    var storeDescription = data[0].description;
                    $("#id-spand-storeName").text(storeDescription);
                    $("#id-spand-storeName").css("color", "blueviolet");
                }

            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }

    function LoadVendorByID() {
        //PageLoader();
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: $("#divGetVendorByID").data("request-url"),
            data: { value: $('#id-text-Vendor').val() },
            success: function (data) {
                $("#divVendorLoader").hide();
                $('#idmodalVendor').prop('disabled', false);
                if (data.length == 0) {
                    $("#id-spand-vendorName").text("No Vendor Found with ID " + $('#id-text-Vendor').val());
                    $("#id-spand-vendorName").css("color", "red");
                }
                else {
                    var vendorNumber = data[0].vendorNumber;
                    var vendorName = data[0].vendorName;
                    $("#id-spand-vendorName").text(vendorName);
                    $("#id-spand-vendorName").css("color", "blueviolet");
                }
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }

    function LoadPOByID() {
        //PageLoader();
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: $("#divGetPOByID").data("request-url"),
            data: { value: $('#id-text-PO').val() },
            success: function (data) {
                $("#divPOLoader").hide();
                $('#idmodalPO').prop('disabled', false);
                if (data.length == 0) {
                    $("#id-spand-description").text("PO Number " + $('#id-text-PO').val() + " is Invalid");
                    $("#id-spand-description").css("color", "red");
                }
                else {
                    var description = "PO Number " + $('#id-text-PO').val() + " is Valid";
                    $("#id-spand-description").text(description);
                    $("#id-spand-description").css("color", "blueviolet");
                }
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
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
                loadedPartialStore();
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
    function loadedPartialStore(){
        $("#divStoreLoader").hide();
        $('#idmodalStore').prop('disabled', false);
        $('#idModalStoreView').modal('show');
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
                loadedPartialVendor();
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
    function loadedPartialVendor() {
        $("#divVendorLoader").hide();
        $('#idmodalVendor').prop('disabled', false);
        $('#idModalVendorView').modal('show');
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
                loadedPartialPO();
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
    function loadedPartialPO() {
        $("#divPOLoader").hide();
        $('#idmodalPO').prop('disabled', false);
        $('#idModalPOView').modal('show');
    }
});


