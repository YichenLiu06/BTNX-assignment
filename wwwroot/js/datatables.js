function initializeDataTable(table) {
    if ($.fn.dataTable.isDataTable(table)) {
        $(table).DataTable().destroy();  // Destroy and clear the table
    }
    $(document).ready(function () {
        $(table).DataTable({
            paging: true,
            searching: true,
            ordering: true,
            responsive: true
        });
    });
}

function destroyDataTable(table) {
    $(document).ready(function () {
        $(table).DataTable().destroy();
    });
}

function clearDataTable(table){
    $(document).ready(function () {
        $(table).DataTable().clear();
    });
}
