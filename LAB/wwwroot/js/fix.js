$(document).ready(function (){  
        var sum = priceValue = parseFloat(document.getElementById("sum").innerHTML);
        document.getElementById("sum").innerHTML = Math.round(sum * 100) / 100;    
});