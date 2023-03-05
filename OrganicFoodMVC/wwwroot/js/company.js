var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "email", "width": "10%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "streetAddress", "width": "10%" },
            { "data": "village", "width": "10%" },
            { "data": "district", "width": "10%" },
            { "data": "city", "width": "10%" },
            {
                "data": "isAuthorizedCompany",
                "width": "10%",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Company/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "15%"
            }
        ],
        dom: 'Plfrtip',
        language: {
            "emptyTable": "Không có dữ liệu",
            "search": "Tìm kiếm:",
            "paginate": {
                "first": "Trang đầu",
                "last": "Trang cuối",
                "next": "Trang sau",
                "previous": "Trang trước"
            },
            "info": "Hiển thị _START_ đến _END_ sản phẩm / Tổng _TOTAL_ sản phẩm",
            "infoEmpty": "",
            "lengthMenu": "Hiện _MENU_ sản phẩm",
        },
    });
}

function Delete(url) {
    swal({
        title: "Bạn chắc chắn muốn xóa?",
        text: "Không thể hồi phục khi xóa.",
        icon: "warning",
        buttons: true,
        
        dangerMode: true,
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