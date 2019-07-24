<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ShitterOperations.aspx.cs" Inherits="ShitterOperations" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="page-header">
        Shitter Operations
    </h2>
    <div class="row">
        <div class="col-sm-3">
            <a class="btn btn-primary" href="NewShitterOperation.aspx">Add New</a> |
        </div>
        <div class="col-sm-5">
            <div style="float: left">
                <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" />
            </div>
            <div style="float: left">
                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary"
                    OnClick="btnUpload_Click" />
            </div>
            <div style="clear: both;">
            </div>
        </div>
        <div class="col-sm-3">
            <asp:DropDownList CssClass="form-control" ID="ddlShiters" runat="server" OnSelectedIndexChanged="ddlShiters_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>
                            Operation id
                        </th>
                        <th>
                            Shitter
                        </th>
                        <th>
                            Paper
                        </th>
                        <th>
                            Amount
                        </th>
                          <th>
                            Partial done %
                        </th>
                        <th>
                            Cover
                        </th>
                        <th>
                            Date
                        </th>
                        <th>
                            Customer
                        </th>
                        <th>
                            Setup
                        </th>
                      
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (ShitterOperation so in shitterOperations)
                       {%>
                    <tr>
                        <td>
                            <%=so.Id %>
                        </td>
                        <td>
                            <%=so.ShiterNumber %>
                        </td>
                        <td>
                            <%=so.PaperType %>
                        </td>
                        <td>
                            <%=so.Amount %>
                        </td>
                        <td>
                            <%=so.PartialAmountPercent+"%" %>
                        </td>
                        <td>
                            <%=so.CoverType %>
                        </td>
                        <td>
                            <%=so.OpDate.ToShortDateString() %>
                        </td>
                        <td>
                            <%=so.Customer %>
                        </td>
                        
                        <td>
                            <%=so.SetupReq?"Yes":"No" %>
                        </td>
                        <td>
                            <a class="btn btn-success" href="EditShitterOperation.aspx?id=<%=so.Id %>">Edit</a>
                            <a class="btn btn-danger" href="delete.aspx?type=shitterOperation&id=<%=so.Id %>">Delete</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
    <asp:Label ID="lblChangeIndex" runat="server" Text="" Visible="False"></asp:Label>
</asp:Content>
