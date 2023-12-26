$(function () {
    initial_work();
    PR_Code();
    set_drop_down_list('cboClient', 'Select Client', '', 'Acc.ClientsInformation', 'ClientCode', 'ClientName', 'ClientCode', '00015');
    set_drop_down_list('cboPaperSupplier', 'Select Supplier', '', 'prd.PaperSupplierInfo', 'PaperSupplierCode', 'PaperSupplierName', '', '');
    set_drop_down_list('cboPaper', 'Select Paper', '', 'prd.PaperInfo', 'PaperCode', 'PaperName', '', '');
    //---------------Show Date-------------//
    $(".show_datepicker").datepicker({
        dateFormat: 'dd/mm/yy',
        changeMonth: true,
        changeYear: true
    }).datepicker("setDate", "0");

    $(".select2").select2();
});
//--------------Load Details Part----------------//
function initial_work() {
    $("#tbl_details tbody tr").remove();

    var rows = '<tr class="input_fields_box">'
    + '<td><input class="form-control" type="text" id="txtTrackNo" name="txtTrackNo"/></td>'
    + '<td><input class="form-control" type="text" onkeyup="CheckNumer(this.id)" id="txtRollQtyInKG" name="txtRollQtyInKG"/></td>'
    + '<td><input class="form-control" type="text" onkeyup="TotalBills(this.id)" id="txtQty" name="txtQty"/></td>'
    + '<td><input class="form-control" type="text" onkeyup="TotalBills(this.id)" id="txUnitPrice" name="txtUnitPrice"/></td>'
    + '<td><input class="form-control TotalPrice" type="text" id="txTotalBill" name="txtTotalBill"/></td>'
    + '<td><span id="btn_temp_add1" class="glyphicon glyphicon-plus add_field_button" style="cursor:pointer"></span></td>'
    + '<td><span class="glyphicon glyphicon-trash remove_field" style="cursor:pointer"></span></td>'
    + '</tr>';

    $('#tbl_details tbody').append(rows);

}

function CheckNumer(ElementID) {

    if (!(/^[-+]?\d*\.?\d*$/.test(document.getElementById(ElementID).value))) {
        alert('Please enter only numbers')
        $('#' + ElementID).val(0);
    }

}

function TotalBills(ElementID) {
    
    if (!!ElementID) {

        if (!(/^[-+]?\d*\.?\d*$/.test(document.getElementById(ElementID).value))) {
            alert('Please enter only numbers')
            $('#' + ElementID).val(0);
        }
        else {
            var suffix = ElementID.match(/\d+/);
            suffix = (!!suffix) ? suffix : "";

            //if (!!suffix) {
            var qty = $('#txtQty' + suffix).val();
            var price = $('#txUnitPrice' + suffix).val();

            qty = (!!qty) ? qty : 0;
            price = (!!price) ? price : 0;

            var total_price = parseInt(qty) * parseFloat(price);
            $('#txTotalBill' + suffix).val(total_price);
            //}
            //-----------------------------------------//
            var sum = 0;
            $('.TotalPrice').each(function () {
                sum += parseFloat(this.value);
            });
            $('#txtTotalAmount').val(sum);
        }
    }
}

function LoadClientAddress()
{
    var clientCode = $('#cboClient').val();
    $.ajax({
        type: "POST",
        url: "default.aspx/LoadClientAddress",
        data: "{clientCode: '" + clientCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var Details = JSON.parse(response.d);
           
            if (Details != "") {
                $.each(Details, function (index, item) {
                    $('#txtClientAddress').val(item.ClientAddress);
                });
            }
        }
    });
}

function LoadPaperDetails()
{
    var cboPaper = $('#cboPaper').val();
   
    $.ajax({
        type: "POST",
        url: "default.aspx/LoadPaperDetails",
        data: "{cboPaper: '" + cboPaper + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var Details = JSON.parse(response.d);
           
            if (Details != "") {
                $.each(Details, function (index, item) {
                    $('#txtPaperDetail').val(item.PaperDetail);
                });
            }
        }
    });
}

//--------------Doropdown List------------------//
function set_drop_down_list_Product(element_id, deafult_text, selected_item, db_table, col_value, col_text, condition_field, condition, condition_field1, condition1) {

    $.ajax({
        type: "POST",
        url: "default.aspx/set_drop_down_list_Product",
        data: "{ 'db_table': '" + db_table + "','col_value': '" + col_value + "','col_text': '" + col_text + "','condition_field': '" + condition_field + "','condition': '" + condition + "','condition_field1': '" + condition_field1 + "','condition1': '" + condition1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (deafult_text == "")
                deafult_text = "Please select";
            //var ddl_element = $("[id*=ddl_course]");
            var ddl_element = $("#" + element_id);
            ddl_element.empty().append('<option value="0">' + deafult_text + '</option>');
            $.each(r.d, function () {
                if (selected_item == this['Value'])
                    ddl_element.append($('<option selected="selected"></option>').val(this['Value']).html(this['Text']));
                else
                    ddl_element.append($('<option></option>').val(this['Value']).html(this['Text']));
            });
        }
    });
}

//-----------Add Multiple Element FOR PPL---------------//
jQuery_1_7_2(window).load(function () {
    var x = 1; //initilal text box count
    var max_elements = 50; //maximum input elements allowed      
    //--------------------------//
    jQuery_1_7_2(".add_field_button").live('click', function () {
        var id = this.id;
        var ele_suffix = id.match(/\d+/);
        var x = document.getElementById('hdn_count_element').value;
        if (x <= max_elements) {
            var $tr = $(this).closest('.input_fields_box');
            //-------------------------//
            var div = $("#tbl_details tr");
            //find all select2 and destroy them   
            div.find(".select2").each(function (index) {
                if ($(this).data('select2')) {
                    $(this).select2('destroy');
                    //alert(this.id);
                }
            });
            //-------------------------//
            var $clone = $tr.clone();

            $clone.find(':text').val('');
            $clone.find('textarea').val('');

            $clone.find('.remove_field').attr("id", ''); //Remove Button ID Set Empty For New Clone Data Update.
            $clone.find('select option:first-child').attr("selected", "selected");
            $clone.find('*').each(function (index, element) {
                var ele_id = element.id;
                $('#' + ele_id).attr("id", ele_id + x);
            });
            $tr.after($clone);
            //--------------------------//
            $('.select2').select2();
            //--------------------------------------//
            $clone.find("input.show_datepicker")
            .removeClass('hasDatepicker')
            .removeData('datepicker')
            .unbind()
            .datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                yearRange: "-100:+0",
                changeYear: true,
                showButtonPanel: false,
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 99999999999999);

                    }, 0);
                }
            });
            //--------------------------------------//
            x++;
            document.getElementById('hdn_count_element').value = x;
            $('#Amount' + ele_suffix).val("0")
            summation_price_ppl_Main()
        }
    });
});

//------------------Remove Element-----------------//
var total_remove_element_id = "";
jQuery_1_7_2(window).load(function () {
    jQuery_1_7_2(".remove_field").live("click", function () {
        var x = document.getElementById('hdn_count_element').value;
        if (x > 1) {
            $(this).parents(".input_fields_box").remove();
            x--;
            document.getElementById('hdn_count_element').value = x;
            //---------------------------------//
            var remove_single_id = $(this).attr("id");
            total_remove_element_id += remove_single_id + "#";
            $('#hdn_remove_all_id').val(total_remove_element_id);
        }
    });
});

//-------------------Save Data-------------------//
var DetailsTable =
{
    getData: function (table) {
        var data = [];
        table.find('tr').not(':first').each(function (rowIndex, r) {
            var cols = [];
            $(this).find('td').each(function (colIndex, c) {
                if ($(this).children(':text,textarea,select,input[type="hidden"]').length > 0)
                    cols.push($(this).children('input,textarea,select,input[type="hidden"]').val().trim());
                    //if dropdown text is needed then uncomment it and remove SELECT from above IF condition//
                else if ($(this).children('select').length > 0)
                    cols.push($(this).find('option:selected').text());
                else if ($(this).children(':checkbox').length > 0)
                    cols.push($(this).children(':checkbox').is(':checked') ? 1 : 0);
                else {
                    //cols.push($(this).text().trim());
                    cols.push($(this).find('.remove_field').attr('id'));
                }
            });
            data.push(cols);
        });
        return data;
    }
}
//----------------- Save Master Data--------// 
function save() {
   
  
    var BillNo = $('#txtNo').val();
    var SupplierSerial = $('#txtSupplierSL').val();
    var BillDate = $('#txtBillDate').val();
    var DeliveryDate = $('#txtUnloadingLoadingDate').val();
    var ChalanNo = $('#txtChalanNo').val();
    var DeliveryTime = $('#txtUnloadingLoadingTime').val();
    var ClientCode = $('#cboClient').val();
    var ReceiverAddress = $('#txtPlaceofUnloading').val();
    var PaperSupplierCode = $('#cboPaperSupplier').val();
    var PaperCode = $('#cboPaper').val();

    if (BillNo != "" && SupplierSerial != "" && ChalanNo != "" && ClientCode != "" && ReceiverAddress != "" && PaperSupplierCode != "" && PaperCode != "") {
        var data = {
            BillNo,
            SupplierSerial,
            BillDate,
            DeliveryDate,
            ChalanNo,
            DeliveryTime,
            ClientCode,
            ReceiverAddress,
            PaperSupplierCode,
            PaperCode
        };

        var parameters = {};
        parameters.master = data;

        var Detaildata = DetailsTable.getData($('#tbl_details'));  // passing that table's ID //   
        var identify_data = [["master_id", '1']];
        parameters.array = Detaildata;
        parameters.array1 = identify_data;
        var request = $.ajax({
            url: "default.aspx/insertDt",
            data: JSON.stringify(parameters),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
        request.done(function (msg) {

            var box = msg.d;
            if (box == "SaveSuccess")
            {
                window.open("../../../../../BPL/Data/INV/LoadingAndUnloadingLaborBill/List/", "_self");
            }
            else
            {
                $('#msg').html('<div class="alert alert-warning"><a class="close" style="text-decoration: none" data-hide-closest=".alert">×</a><strong>Warning!</strong>Save Failed</div>');
            }
        });
    }
    else {
        $('#msg').html('<div class="alert alert-info"><a class="close" style="text-decoration: none" data-hide-closest=".alert">×</a>Please fill in required fields.</div>');
    }

}

//-----------------Button New----------------------------//
function new_work() {
    $(':text').val('');
    $('textarea').val('');
    $('select option:first-child').attr("selected", "selected");
    initial_work();
}

//----------------------Message Hide-----------------------//
$(document).on("click", "[data-hide-closest]", function (e) {
    e.preventDefault();
    var $this = $(this);
    $this.closest($this.attr("data-hide-closest")).hide();
});

//-------------------Show report-------------------//
function show_report() {
    $.ajax({
        type: "POST",
        url: "default.aspx/report_show",
        data: "{Code: '" + $('#txtNo').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.location.href = "../../../../../PBS/Data/INV/Chalan/Print";
        }
    });
}

///------Generate PR Code----///
function PR_Code() {
    $.ajax({
        type: "POST",
        url: "default.aspx/cs_code",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#txtNo').val(data.d);
        }
    });

}
//----------END------------------//