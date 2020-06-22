
$(document).ready(function () {
    $(".clsselectstore").on("click", function (e) {
        e.preventDefault();
        var thisElement = this;
        $("#id-text-storeNumber").val(thisElement.parentElement.parentElement.children[1].innerText.trim());
        $("#id-spand-storeName").text(thisElement.parentElement.parentElement.children[4].innerText.trim());
        $('#idModalStoreView').modal('hide');
    });

    $(".cls-select-vendor").on("click", function (e) {
        e.preventDefault();
        var thisElement = this;
        $("#id-text-Vendor").val(thisElement.parentElement.parentElement.children[2].innerText.trim());
        $("#id-spand-vendorName").text(thisElement.parentElement.parentElement.children[3].innerText.trim());
        $('#idModalVendorView').modal('hide');
    });

    $(".cls-select-po").on("click", function (e) {
        e.preventDefault();
        var thisElement = this;
        $("#id-text-PO").val(thisElement.parentElement.parentElement.children[2].innerText.trim());
        $('#idModalPOView').modal('hide');
    });
});