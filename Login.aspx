<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="col-sm-4 col-sm-offset-4">
        <div class="form-group">
            <label for="email">
                Email address:</label>
            <input runat="server" type="text" class="form-control" id="email"/>
        </div>
        <div class="form-group">
            <label for="pwd">
                Password:</label>
            <input type="password" class="form-control" id="pwd" runat="server"/>
        </div>
        <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" Text="Login" 
            onclick="Button1_Click" />
        
    </div>
</asp:Content>
