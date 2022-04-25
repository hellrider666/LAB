function onProductChange() {
    var selectedProduct = $('#products').find('option:selected').val();
    console.log(selectedProduct);
    $.get('../Ingredients/GetByProduct?productId=' + selectedProduct).then(function (resp) {
        var ingridients = JSON.parse(resp);
        var ingridientsLabel = $('#ingridients');
        var str = '';
        $(ingridients).each(function (index, item) {
            str += item.Name + ' : ' + item.Quantity;
            if (index < ingridients.length - 1) {
                str += ', ';
            }
        })
        ingridientsLabel.text(str);
    }).catch(function (err) {
        console.log(err);
    });
}
$(document).ready(function () {
    onProductChange();
});