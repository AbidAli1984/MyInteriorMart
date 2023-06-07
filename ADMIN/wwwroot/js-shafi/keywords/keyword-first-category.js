// Shafi: Bind keyword field with url field and convert to upper case by removing space
$(document).ready(function () {
    // Bind keyword, url and description input together
    $("#keyword").keyup(function (event) {

        // Force text to be typed in lowercase in Keyword text field
        $(this).val($(this).val().toLowerCase());

        // Get value of keyword text field
        var keyword = $(this).val();

        // Tranform value of keyword to Title Case
        function ToTitleCase(keyword) {
            return keyword.replace(/(?:^|\s)\w/g, function (match) {
                return match.toUpperCase();
            });
        }

        // Call ToTitleCase function replace space by - hyphen through and convert to title case
        var url = ToTitleCase(keyword).replace(/\s+/g, "-");

        // Set final value of input with id's url, title and description
        $("#url").val(url);
        $("#title").val(ToTitleCase(keyword));
        $("#description").val(ToTitleCase(keyword));
    });
    // End:

    // Prevent user manually typing text in url input
    $("#url").keypress(function (e) {
        e.preventDefault();
    });
    // End:
});

// Begin: On CreateAjaxCall click invoke Modal with id "ModalCreate"
$(document).ready(function () {
    $("#CreateAjaxCall").click(function (event) {
        $('#ModalCreate').modal('show');
    });
})
// End:

// Begin: On button click with id "CreateKeywordFirstCategory" execute
$(document).ready(function () {
    $("#warning").hide();
    $("#spinner").hide();
    $("#error").hide();
    $("#ModalSuccess").hide();
    $("#CreateKeywordFirstCategory").click(function (event) {
        // Begin: Get values of form parameters
        var FirstCategoryID = $("#firstCategoryId").val();
        var Keyword = $("#keyword").val();
        var URL = $("#url").val();
        var Title = $("#title").val();
        var Description = $("#description").val();
        // End:

        if (FirstCategoryID != "" && Keyword != "" && URL != "" && Title != "" && Description != "") {
            // Hide: CreateFirstCategoryForm form
            $("#FormFields").hide();
            // Show: Pinner
            $("#spinner").show();
            // Begin: Execute if all fields are not empty
            $.ajax({
                type: 'POST',
                url: '/AllKeywords/KeywordsFirstCategory/Create',
                type: 'POST',
                async: true,
                dataType: 'json',
                data: {
                    FirstCategoryID: FirstCategoryID,
                    Keyword: Keyword,
                    URL: URL,
                    Title: Title,
                    Description: Description
                },
                success: function (data) {
                    // Begin: If data is equal to success then execute
                    if (data === "success") {
                        // Hide: ModalCreate
                        $('#ModalCreate').modal('toggle');
                        // Empty: Values of all form fields
                        Keyword.value = "";
                        URL.value = "";
                        Title.value = "";
                        Description.value = "";
                        // Invoke: success modal
                        $("#ModalSuccess").show();
                        $("#ModalSuccess").modal('toggle');
                        location.reload();
                    }
                    // End:
                    // Begin: If data is equal to error then execute
                    else {
                        // Hide: spinner
                        $("#spinner").hide();
                        // Set: value of error message
                        $("#error").html(data);
                        // Show: error message
                        $("#error").show();
                        // Show: form fields
                        $("#FormFields").show();
                    }
                },
                error: function (ex) {
                    alert(ex.responseText);
                }
            });
            // End:
        }
        else {
            // Begin: Show warning of any value is empty
            $("#warning").show();
            // End:
        }
    });
})
// End: