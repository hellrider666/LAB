// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var priceValue = 0;
$(document).ready(function () {
    priceValue = parseFloat(document.getElementById("currency").innerHTML);
});

function changeCurr(elem) {
    if (elem.value == 'Доллар') {
        priceValue = parseFloat(priceValue) / 95.24;
        document.getElementById("currency").innerHTML = Math.round(priceValue * 100) / 100;
        elem.value = "Сомы";
    }
    else {
        priceValue = parseFloat(priceValue) * 95.24;
        document.getElementById("currency").innerHTML = Math.round(priceValue);
        elem.value = "Доллар";
    }

}