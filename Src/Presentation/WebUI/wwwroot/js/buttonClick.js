"use strict";

$(function () {
    loadButtonClicks();

    $("#Click-Me-Buttton").click(function () {

        $.ajax({
            type: "POST",
            url: $("#Click-Me-Buttton").data("url")
        });

        loadButtonClicks()
    });

    function loadButtonClicks() {
        $.ajax({
            type: "POST",
            url: $("#Button-Count-Input").data("url"),
            success: function (response) {
                $("#Button-Count-Input").val(response.clicks)
            }
        });
    }
})
