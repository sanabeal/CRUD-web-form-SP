$(function () {
    show_data();
});
//----------Load Data--------------------//
function show_data() {
    $("#table_data tbody tr").remove();
    $.ajax({
        type: "POST",
        url: "default.aspx/show_list_data",
        data: "{ 'search_data': ''}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var Details = JSON.parse(response.d);
            var sl_no = 0;
            if (Details != "") {
                $.each(Details, function (index, item) {
                    var rows = '<tr>'
                    + '<td>' + ++sl_no + '</td>'
                    + '<td>' + item.BillNo + '</td>'
                    + '<td>' + item.BillDate + '</td>'
                    + '<td>' + item.ClientName + '</td>'
                    + '<td>' + item.PaperSupplierName + '</td>'
                    + '<td>' + item.DeliveryDate + '</td>'
                    + '<td>' + item.TotalAmount + '</td>'
                    + '<td><a onclick="set_value(\'' + item.BillNo + '\')"><i style="cursor: pointer;" class="glyphicon glyphicon-edit"></i></a></td>'
                    rows += '</td>'
                    + '</tr>';
                    $('#table_data tbody').append(rows);
                });
               // datagrid();
            }
        },
        error: function (data, success, error) {

        }
    });
}

//-------------Set value-----------------//
function set_value(BillNo) {
    sessionStorage.setItem("sentBillNo", BillNo);
    window.open("../../../../../BPL/Data/INV/LoadingAndUnloadingLaborBill/Edit/", "_self");
}

//------------Load DataTable---------------//
function datagrid() {
    $('#table_data').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "destroy": true
    });
}

//------------show_report---------------//
function show_report(No, Language, Topic) {
    $.ajax({
        type: "POST",
        url: "default.aspx/report_show",
        data: "{No: '" + No + "', Language: '" + Language + "', Topic: '" + Topic + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.open("../../../../../BPL/Data/HRD/IOM/Print", "_blank");
        }
    });
}