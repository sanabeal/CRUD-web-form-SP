<%@ Page Title="" Language="C#" MasterPageFile="~/CDB/System/Common/Layout/Master/Panel.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BPL.Data.INV.LoadingAndUnloadingLaborBill.List._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="default.js"></script>
    <div class="content-wrapper" style="padding: 0; margin: 0;">
        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Loading & Unloading Labor Bill List</h3>
                        <a href="../../../../../BPL/Data/INV/LoadingAndUnloadingLaborBill/Add/" style="float: right" target="_self" class="btn btn-primary">New</a>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="table-responsive">
                            <table class="table" id="table_data">
                                <thead>
                                    <tr>
                                        <th style="width: 5%">SL</th>
                                        <th style="width: 10%">Bill No</th>
                                        <th style="width: 10%">Bil Date</th>
                                        <th style="width: 20%">Client Name</th>
                                        <th style="width: 20%">Supplier Name</th>
                                        <th style="width: 15%">Delivery Date</th> 
                                        <th style="width: 15%">TotalAmount</th>
                                        <th style="width: 5%">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
