<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShitterPriceMaintenance.aspx.cs" Inherits="ShitterPriceMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <h2 class="page_headers">
         Shitter Price Maintenance
    </h2>
    <div class="col-sm-4 col-sm-offset-4">
        <div class="form-group">
            <label>
                Change Paper cost:</label>
            <input type="text" class="form-control" id="txtChangePaper" runat="server" />
        </div>
        <div class="form-group">
            <label>
                Change Cover cost:</label>
            <input type="text" class="form-control" id="txtChangeCover" runat="server" />
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
    </div>
</asp:Content>


