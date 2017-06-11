<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DMS.UI.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/grid-paging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    Dashboard
    <asp:GridView runat="server" ID="gvDocList" AutoGenerateColumns="true" Width="100%" ViewStateMode="Enabled" PagerSettings-Position="TopAndBottom" OnPageIndexChanging="gvDocList_PageIndexChanging">
        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <%--<script>
        $(document).ready(function () {
            $('#offcanvasleft').click(function () {
                $('#sidebarLeft').toggleClass('active');
            });
        });
    </script>--%>
</asp:Content>
