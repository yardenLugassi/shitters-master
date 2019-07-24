<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NewShiter.aspx.cs" Inherits="NewShiter" %>

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
                Shitter name:</label>
            <input type="text" class="form-control" id="txtName" runat="server" />
        </div>
        
        <asp:Button ID="btnUpdate" CssClass="Save" runat="server" Text="Create" 
            onclick="btnUpdate_Click" />
    </div>
</asp:Content>
