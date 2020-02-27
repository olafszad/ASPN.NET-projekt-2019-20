var dataTable;

$(document).ready(function () {
    loadDataTalbe();
});

function loadDataTalbe() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/warehouses/getall",
            "type": "GET",
            "datapyte": "json"
        },
        "columns": [
            { "data": "warehouseName", "width": "30%" },
            { "data": "companyID", "width": "30%" },
            {
                
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Warehouses/Update?id=${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer;width:100px;'
                            onclick=Delete('/Warehouses/Delete?id='+${data})>
                           Delete
                        </a>
                        </div> `;
                }, "width": "30%"
            },
            {

                "data": "warehouseName",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Products/Index?warehouseName=${data}" class='btn btn-info text-white' style='cursor:pointer;width:100px;'>
                            List
                        </a>
                       
                        </div> `;
                }, "width": "30%"
            }

        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"

    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}