<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewShitterOperation.aspx.cs" Inherits="NewShitterOperation" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .Save
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <h2 class="page_headers">
        Edit Operation
    </h2>
    <div class="col-sm-4 col-sm-offset-4">
        <div class="form-group">
            <label for="">
                Shitter number:</label>
            <asp:DropDownList CssClass="form-control" ID="ddlShiters" runat="server">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label >
                Papaer type:</label>
            <input type="text" class="form-control" id="TxtpaperType" runat="server" />
        </div>
        <div class="form-group">
            <label >
                Amount:</label>
            <input type="text" class="form-control" id="TxtAmount" runat="server" />
        </div>
        <div class="form-group">
            <label >
                Cover type:</label>
            <input type="text" class="form-control" id="TxtCoverType" runat="server" />
        </div>
        <div class="form-group">
            <label>Date:</label>
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        </div>
        <div class="form-group">
            <label >
                Customer:</label>
                <input type="text" class="form-control" id="txtCustomer" runat="server" />
              
            
        </div>
        <div class="form-group">
            <label >
                SetUp:</label>
                <input type="checkbox" class="form-control" id="chkSetupReq" runat="server" />
            
        </div>
        <asp:Button ID="btnUpdate" CssClass="Save" runat="server" Text="Create" 
            onclick="btnUpdate_Click" />
    </div>
</asp:Content>

