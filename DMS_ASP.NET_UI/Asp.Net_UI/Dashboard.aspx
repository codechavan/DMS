<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DMS.UI.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script>
        $(document).ready(function () {
            $('#offcanvasleft').click(function () {
                $('#sidebarLeft').toggleClass('active');
            });
        });
    </script>
</asp:Content>
