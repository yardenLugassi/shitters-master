<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Shiters.aspx.cs" Inherits="Shiters" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <h2 class="page-header">
            Shitters
        </h2>
        <div class="col-sm-12">
            <a class="btn btn-primary" href="NewShiter.aspx">And New</a>
        </div>
        <div class="col-sm-12">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>
                            Id
                        </th>
                        <th>
                            Name
                        </th>
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (DataRow row in shittersDt.Rows)
                       {%>
                    <tr>
                        <td>
                            <%=row["id"].ToString() %>
                        </td>
                        <td>
                            <%=row["name"].ToString() %>
                        </td>
                        <td>
                            <a class="btn btn-success" href="EditShiter.aspx?id=<%=row["id"].ToString() %>">Edit</a>
                            <a class="btn btn-danger" href="delete.aspx?type=shitter&id=<%=row["id"].ToString() %>">
                                Delete</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
