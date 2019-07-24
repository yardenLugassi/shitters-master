<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ShiterDayOperations.aspx.cs" Inherits="ShiterDayOperations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <style type="text/css">
        ul li
        {
            list-style-type: none;
            text-align: center;
        }
        li.simulation-slot
        {
            display: inline-block;
            width: 200px;
            height: 200px;
            margin-right: 10px;
        }
        #sortable
        {
            display: flex;
        }
    </style>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        var reordered;
        var sortableString;
        $(function () {
            var originalList = [];
            var diff_paper = <%=PaperCost %>;
            var diff_cover = <%=CoverCost %>;

            //$("#sortable").disableSelection();

            //create date piker
            $("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
            function getSortableString(reordered) {
                var counter = 0;
                var str = "";
                reordered.forEach(function (d) {
                    counter++;
                    if (d.type) {
                        str += '<li class="ui-state-default unsortable simulation-slot" style="color:#fff;background-color:' + d.color + ' ">' +
                            '<span class="ui-icon ui-icon-arrowthick-2-n-s"></span>' +
                            d.price / 1000 + 'k' +

                            '</li>';
                    } else {
                        d.order = counter;
                        str += '<li class="ui-state-default simulation-slot" o_n="' + d.Id + '"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>' +
                            '<div>Operation:' + d.order + '</div>' +
                            '<div>Paper:' + d.PaperType + '</div>' +
                            '<div>Cover:' + d.CoverType + '</div>' +
                            '<div>Amount:' + d.Amount + '</div>' +
                            '</li>';
                    }

                });
                return str;
            }
            function getReorderedOperations(operations) {
                var lst = [];
                for (var i = 0; i < operations.length - 1; i++) {
                    if (operations[i].CoverType != operations[i + 1].CoverType &&
                        operations[i].PaperType != operations[i + 1].PaperType) {
                        lst.push(operations[i]);
                        lst.push({
                            type: "setup",
                            color: 'red',
                            price: (105 * (diff_cover + diff_paper)) + (diff_cover + diff_paper * 3900)
                        });

                    }
                    else if (
                        operations[i].CoverType != operations[i + 1].CoverType &&
                            operations[i].PaperType == operations[i + 1].PaperType) {
                        lst.push(operations[i]);
                        lst.push({
                            type: "setup",
                            color: 'green',
                            price: (105 * (diff_cover)) + (diff_cover * 3900)
                        });
                    }
                    else if (operations[i].CoverType == operations[i + 1].CoverType &&
                        operations[i].PaperType != operations[i + 1].PaperType) {
                        lst.push(operations[i]);
                        lst.push({
                            type: "setup",
                            color: 'blue',
                            price: (105 * (diff_paper)) + (diff_paper * 3900)
                        });
                    } else {
                        lst.push(operations[i]);
                    }
                }
                lst.push(operations[operations.length - 1]);
                return lst;
            }

            $('#btnSave').click(function () {
                var shitterId = $('#<%=ddlShiters.ClientID %>').val();
                var date = $("#datepicker").datepicker('getDate');
                var opStr = reordered.filter(function (i) {
                    return i.Id != null;
                }).map(function (i) { return i.Id }).join(',');
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: 'WebService.asmx/SaveSimulation',
                    data: JSON.stringify({ shitterId: shitterId, date: date, operations: opStr }),
                    dataType: "json",
                    success: function (res) {
                        alert('success');
                    }

                });
            });

            $('#btnGet').click(function () {
                var shitterId = $('#<%=ddlShiters.ClientID %>').val();
                var date = $("#datepicker").datepicker('getDate');
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: 'WebService.asmx/GetOperationByDay',
                    data: JSON.stringify({ shitterId: shitterId, date: date }),
                    dataType: "json",
                    success: function (res) {
                        var data = res.d;
                        if (data.length == 0) {
                            alert('No operation found');
                            $('#btnSave').hide();
                            return false;
                            
                        }
                        $('#btnSave').show();
                        originalList = data;

                        reordered = getReorderedOperations(data);
                        sortableString = getSortableString(reordered);
                        $("#sortable").empty();
                        $("#sortable").append(sortableString);
                        setTimeout(function () {
                            $("#sortable").sortable({
                                items: "li:not(.unsortable)",
                                axis: "x",
                                stop: function (event, ui) {
                                    var updatedLis = $("#sortable li:not(.unsortable)");

                                    var updated = [];
                                    updatedLis.each(function () {
                                        var id = $(this).attr('o_n');
                                        var founded = originalList.find(function (e) {
                                            return e.Id == id;
                                        });
                                        updated.push(founded);
                                    });

                                    reordered = getReorderedOperations(updated);
                                    sortableString = getSortableString(reordered);
                                    $("#sortable").empty();
                                    $("#sortable").append(sortableString);
                                    setTimeout(function () {
                                        $("#sortable").sortable({ axis: "x" });
                                    }, 2000);
                                }
                            });
                            $("#sortable").disableSelection();
                        }, 2000);


                    }
                });
            });
            
            $('#<%=ddlShiters.ClientID %>').val($('#<%=ddlShiters.ClientID %>').find('option').eq(1).val());
            $("#datepicker").datepicker().datepicker("setDate", new Date());
            $('#btnGet').click();

        });
    </script>
    <div class="row">
        <h2 class="page-header">
            Operation by day
        </h2>
        <div class="col-sm-3" style="background-color: ">
            <label>
                Select shitter</label>
            <asp:DropDownList CssClass="form-control" ID="ddlShiters" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <label>
                Select Day</label>
            <input type="text" id="datepicker" class="form-control" value=" " />
        </div>
        <div class="col-sm-3">
            <label style="color: #fff">
                AAA</label>
            <div>
                <input type="button" class="btn btn-primary" id="btnGet" value="Get Data" />
            </div>
        </div>
        <div class="col-sm-3">
        </div>
    </div>
    <div class="row" style="margin-top: 20px;">
        <div class="col-sm-12">
            <div class="panel panel-success" style="min-height: 300px">
                <div class="panel-heading">
                    Shitter Operation</div>
                <div class="panel-body" style="position: relative;">
                    <ul id="sortable">
                    </ul>
                    <div style="text-align: right">
                        <a class="btn btn-primary" id="btnSave">Save</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
