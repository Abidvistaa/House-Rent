﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Flat/GetAll"

        },
        "columns": [
            { "data": "floor" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Flat/Edit/${data}" class="btn btn-success text-white style" style="cursor:pointer">
                                    <i class="far fa-edit"></i> 
                                </a>
                                
                                <a onclick=Delete("/Admin/Flat/Delete/${data}") class="btn btn-danger text-white style" style="cursor:pointer">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "60%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure want to delete?",
        text: "You will not be able to restore the data",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "POST",
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

