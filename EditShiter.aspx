<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EditShiter.aspx.cs" Inherits="EditShiter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="page_headers">
        Edit Shitter
    </h2>
    <div class="col-sm-4 col-sm-offset-4">
        <div class="form-group">
            <label>
                Shitter Name:</label>
            <input type="text" class="form-control" id="txtName" runat="server" />
        </div>
        <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" 
            onclick="btnUpdate_Click" />
    </div>
        
</asp:Content>
