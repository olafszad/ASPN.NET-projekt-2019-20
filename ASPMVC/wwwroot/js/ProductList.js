

    var dataTable;

    $(document).ready(function () {
        loadDataTalbe();
    });

    function loadDataTalbe() {
        dataTable = $('#DT_load').DataTable({
            "ajax": {
                "url": "/products/getall",
                "type": "GET",
                "datapyte": "json"
            },
            "columns": [

                { "data": "productId", "width": "20%" },
                { "data": "productName", "width": "20%" },
                { "data": "quantity", "width": "20%" },

                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class="text-center">
                        <a href="/Products/Update?id=${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer;width:100px;'
                            onclick=Delete('/Products/Delete?id='+${data})>
                           Delete
                        </a>
                        </div> `;
                    }, "width": "20%"
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
