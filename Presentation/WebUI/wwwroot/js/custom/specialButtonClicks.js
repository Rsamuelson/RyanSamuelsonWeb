"use strict";

$(function () {
    function AddButtonClick() {
        $.ajax({
            type: "POST",
            url: "Interactables/AddButtonClick",
            success: function (result) {
                $("#SpecialButtonClicks").html(result);
            }
        });
    }

    $("#SpecialButtonId").click(function ()
    {
        AddButtonClick()
    });
})