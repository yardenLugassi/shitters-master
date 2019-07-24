<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SubmitOperations.aspx.cs" Inherits="SubmitOperations" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function submitClick(id, amount, lastParialAmount) {
            var pamount = $('#txtAmount').val();
            if (isNaN(pamount)) {
                alert('Amount must be a number');
                return false;
            }
            window.location.href = "submit.aspx?id=" + id + "&pamount=" + pamount + "&amount=" + amount + "&lastpamount=" + lastParialAmount;
        }
    </script>
    <h2 class="page-header">
        Submit Operations
    </h2>
    <div class="row">
        <div class="col-sm-3">
            <asp:DropDownList CssClass="form-control" ID="ddlShiters" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search"
                OnClick="btnSearch_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-3">
            <input type="text" id="txtAmount" class="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <table class="table table-striped shitterOperations">
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
                            Partial Amount
                        </th>
                        <th>
                            Partial Amount(%)
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
                            <%=so.PartialAmount %>
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
                            <a class="btn btn-success submitBtn" onclick="submitClick(<%=so.Id %>, <%=so.Amount %>,<%=so.PartialAmount %>)">
                                Submit</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
