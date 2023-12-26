<%@ Page Title="" Language="C#" MasterPageFile="~/CDB/System/Common/Layout/Master/Panel.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BPL.Data.INV.LoadingAndUnloadingLaborBill.Add._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../../../../CDB/System/Assets/script/library/jquery-1.7.2.min.js"></script>
    <script src="../../../../../BPL/System/Assets/function/dropdown/dropdown.js"></script>
    <script type="text/javascript">
        var jQuery_1_7_2 = $.noConflict(true);

        function goto_list() {
            window.open("../../../../../BPL/Data/INV/LoadingAndUnloadingLaborBill/List/", "_self");
        }        
    </script>

    <script src="default.js"></script>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper" style="padding: 0; margin: 0;">
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <div id="msg"></div>
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Loading & Unloading Labor Bill</h3>
                            <input id="hdn_master_id" type="hidden" />
                            <input id="hdn_count_element" type="hidden" value="1" />
                            <input id="hdn_remove_all_id" type="hidden" />
                            <input id="hdn_total_rec" type="hidden" value="0" />
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Name">Unloading No</label>
                                        <input type="text" class="form-control" id="txtNo" name="txtNo" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Country">Supplier SL</label>
                                        <input type="text" class="form-control" id="txtSupplierSL" name="txtSupplierSL" placeholder="Supplier SL" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Country">Bill Date</label>
                                        <input type="text" class="form-control show_datepicker" id="txtBillDate" name="txtBillDate" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Country">Unloading/Loading Date</label>
                                        <input type="text" class="form-control show_datepicker" id="txtUnloadingLoadingDate" name="txtUnloadingLoadingDate" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Chalan No</label>
                                        <input type="text" class="form-control" id="txtChalanNo" name="txtChalanNo" placeholder="Chalan No" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Unloading/Loading Time</label>
                                        <input type="text" class="form-control" id="txtUnloadingLoadingTime" name="txtUnloadingLoadingTime" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Client's Name</label>
                                        <select class="form-control required select2" id="cboClient" onchange="LoadClientAddress()" name="cboClient"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Client's Address</label>
                                        <input type="text" class="form-control" id="txtClientAddress" name="txtClientAddress" placeholder="Client Address" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Place of Unloading</label>
                                        <input type="text" class="form-control required" id="txtPlaceofUnloading" name="txtPlaceofUnloading" placeholder="Place of Unloading" />
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Paper Supplier</label>
                                        <select class="form-control required select2" id="cboPaperSupplier" name="cboPaperSupplier"></select>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Paper Code</label>
                                        <select class="form-control required select2" id="cboPaper" onchange="LoadPaperDetails()" name="cboPaper"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label for="Website">Paper Detail</label>
                                        <input type="text" class="form-control" id="txtPaperDetail" name="txtPaperDetail" disabled placeholder="Paper Detail" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <table class="table table-hover table-bordered" id="tbl_details" style="min-width: 800px;">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 20%">Track No</th>
                                                        <th style="width: 20%">Roll Qty in KG</th>
                                                        <th style="width: 15%">Qty</th>
                                                        <th style="width: 15%">Unit Price</th>
                                                        <th style="width: 20%">Total Bill</th>
                                                        <th colspan="2" style="width: 5%; text-align: center; margin: 0 auto;" align="center"><span class="glyphicon glyphicon-cog"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>

                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group" style="float: right">
                                                    <label for="Website">Total Amount</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4" style="float: right">
                                                <div class="form-group">
                                                    <input type="text" class="form-control" id="txtTotalAmount" name="txtTotalAmount" disabled />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <a href="#" class="btn btn-default" onclick="goto_list()">Back</a>
                            <button id="btnsave" type="button" onclick="save()" class="btn btn-primary">Save</button>
                            <br />
                        </div>
                    </div>
                </div>
                <!-- /.box -->
            </div>
        </section>
    </div>
</asp:Content>
