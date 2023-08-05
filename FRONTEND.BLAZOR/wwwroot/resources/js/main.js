(function ($) {
    $(document).on('click', '.navbar-brand-btn', function () {
        $(this).toggleClass('active');
        $('.mim-HomeSideMenu').toggleClass('show');
    });

    $('.navbar-brand-offer-menu .navbar-brand-offer-btn').click(function () {
        $(this).toggleClass('active');
        $(this).closest('.category-list').next('.brand-category-list').find('.navbar-brand-offer-list').toggleClass('show');
    });

    $('.navbar-brand-offer-list .nav-link').click(function () {
        $('.navbar-brand-offer-menu .navbar-brand-offer-btn').click();
    });

    $('.mim-box-list a').click(function () {
        $('.navbar-brand-btn').click();
    });
})(window.jQuery);

$(document).ready(function () {
    $(document).on('click', "#show_hide_password a", function (event) {
        debugger;
        event.preventDefault();
        var $showHideDiv = $(this.closest('#show_hide_password'));
        var $input = $showHideDiv.find('input');
        var $inputAddonIcon = $showHideDiv.find('.input-group-addon i');

        if ($input.attr("type") == "text") {
            $input.attr('type', 'password');
            $inputAddonIcon.addClass("fa-eye-slash");
            $inputAddonIcon.removeClass("fa-eye");
        } else if ($input.attr("type") == "password") {
            $input.attr('type', 'text');
            $inputAddonIcon.removeClass("fa-eye-slash");
            $inputAddonIcon.addClass("fa-eye");
        }
    });



    $('#HideNumber').hide();
    $('#contactDetails').hide();

    $("#ViewNumber").click(function () {
        $("#ViewNumber").hide();
        $("#HideNumber").show();
        $(this).parent('.social-details').next('#contactDetails ').show();
    });

    $("#HideNumber").click(function () {
        $("#ViewNumber").show();
        $("#HideNumber").hide();
        $(this).parent('.social-details').next('#contactDetails ').hide();
    });
    $(".gander").select2({
        placeholder: "Gander",
        allowClear: true,
    });
    $(".turnover").select2({
        placeholder: "Turnover",
        allowClear: true,
    });
    $(".natureofbusiness").select2({
        placeholder: "Nature of Business",
        allowClear: true,
    });
    $(".designation").select2({
        placeholder: "Designation",
        allowClear: true,
    });
    $(".country").select2({
        placeholder: "Choose one",
        allowClear: true,
    });
    $(".state").select2({
        placeholder: "Choose one",
        allowClear: true,
    });
    $(".city").select2({
        placeholder: "Choose one",
        allowClear: true,
    });
    $(".pincode").select2({
        placeholder: "Choose one",
        allowClear: true,
    });

    $(".locality").select2({
        placeholder: "Choose one",
        allowClear: true,
    });

    $(".assembly").select2({
        placeholder: "Designation",
        allowClear: true,
    });
    $(".fcategory").select2({
        placeholder: "First Category",
        allowClear: true,
    });
    $(".scategory").select2({
        placeholder: "Second Category",
        allowClear: true,
    });

    $("#v-pills-tabContent form").on('submit', function (e) {
        console.log('test');
        e.preventDefault();
        var li_count = $('#v-pills-tab-listing a').length;
        var current_active = $('.# a.active').index();

        if (current_active < li_count) {
            $('#v-pills-tab-listing a.active').next('a.nav-link').attr('data-toggle', 'pill').tab('show');
        } else {
            alert('Last Step');
        }
    });

    var selectAllItems = "#select-all";
    var checkboxItem = ":checkbox";

    $(selectAllItems).click(function () {

        if (this.checked) {
            $(checkboxItem).each(function () {
                this.checked = true;
            });
        } else {
            $(checkboxItem).each(function () {
                this.checked = false;
            });
        }

    });

    // --------------------- Image Upload ---------------
    $(document).on('click', '#logo_img_btn', function () {
        $('#logo_upload_sec').toggleClass('show hide');
        $('#owner_upload_sec').removeClass('show').addClass('hide');
        $('#gallery_upload_sec').removeClass('show').addClass('hide');
    });

    $(document).on('click', '#owner_img_btn', function () {
        $('#logo_upload_sec').removeClass('show').addClass('hide');
        $('#owner_upload_sec').toggleClass('show hide');
        $('#gallery_upload_sec').removeClass('show').addClass('hide');
    });

    $(document).on('click', '#gallery_img_btn', function () {
        $('#logo_upload_sec').removeClass('show').addClass('hide');
        $('#owner_upload_sec').removeClass('show').addClass('hide');
        $('#gallery_upload_sec').toggleClass('show hide');
    });


    //$('.img-zoom').zoom();
});