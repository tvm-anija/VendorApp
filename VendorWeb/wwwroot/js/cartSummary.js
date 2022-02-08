var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Product/CartSummary",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "productName", "width": "50%" },
            { "data": "cost", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a class='btn btn-success text-white'
                    style='cursor:pointer;'><i class='fas fa-eye'></i></a></div>`;
                }, "width": "30%"
            }
        ]
    });
}