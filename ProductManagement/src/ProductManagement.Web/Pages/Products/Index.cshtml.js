$(function () {
    var l = abp.localization.getResource("ProductMangement");
    var editModal = new abp.ModalManager(abp.appPath + "Products/EditProductModal");

    var dataTable = $("#ProductsTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(
                ProductManagement.products.product.getList),
            columnDefs: [{
                title: l("Actions"),
                rowAction: {
                    items: [
                        {
                            text: l("Edit"),
                            action: function (data) {
                                editModal.open({ id: data.record.id });
                            }
                        }, {
                            text: l("Delete"),
                            confirmMessage: function (data) {
                                return l("ProductDeletionConfirmationMessage");
                            },
                            action: function (data) {
                                ProductManagement.products.product
                                    .delete(data.record.id)
                                    .then(function () {
                                        abp.notify.info(l("SuccessfullyDeleted"));
                                        dataTable.ajax.reload();
                                    })
                            }
                        }
                    ]
                }
            },
            {
                title: l("Name"),
                data: "name"
            },
            {
                title: l("CategoryName"),
                data: "categoryName",
                orderable: true
            },
            {
                title: l("Price"),
                data: "price"
            },
            {
                title: l("StockState"),
                data: "stockState",
                render: function (data) {
                    return l("Enum.StockState:" + data);
                }
            },
            {
                title: l("CreationTime"),
                data: "creationTime",
                dataFormat: "date"
            }
            ]
        })
    );

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    var createModal = new abp.ModalManager(abp.appPath + "Products/CreateProductModal");
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

})