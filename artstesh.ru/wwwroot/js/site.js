// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ScrollToAnchor(id) {
    $('html, body').animate({
        scrollTop: $(`#${id}`).offset().top-100
    }, 500);
}

function SwitchTab(id, hide){
    $("#box-"+hide).hide();
    $("#box-"+id).show();
    $("#tab-"+hide).removeClass("tab-active");
    $("#tab-"+id).addClass("tab-active");
}