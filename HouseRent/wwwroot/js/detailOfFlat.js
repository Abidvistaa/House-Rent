var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/DetailofFlat/GetAll"

        },
        "columns": [
            { "data": "flatName" },
            { "data": "flat.floor" },
            { "data": "roomNumber" },
            { "data": "bathNumber" },
            { "data": "carParking" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/DetailofFlat/Edit/${data}" class="btn-success text-white style" style="cursor:pointer">
                                    <i class="far fa-edit"></i> 
                                </a>&nbsp
                                <a href="/Admin/SpecialTag/Details/${data}" class="btn-primary text-white style" style="cursor:pointer">
                                    <i class="fas fa-list"></i>
                                </a>&nbsp
                                <a onclick=Delete("/Admin/DetailofFlat/Delete/${data}") class=" btn-danger text-white style" style="cursor:pointer">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "20%"
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

