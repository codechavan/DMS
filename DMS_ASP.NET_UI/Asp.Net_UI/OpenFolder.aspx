<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="OpenFolder.aspx.cs" Inherits="DMS.UI.OpenFolder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HiddenField runat="server" ID="hdnFolderId" />
    <button id="btnAddFolder" class="btn btn-default" type="button"><i class="fa fa-plus"></i>New Folder</button>

    <div style="display: inline-block" runat="server" id="pnlFileUpload">
        <button id="btnAddFile" class="btn btn-default" type="button"><i class="fa fa-plus"></i>New File</button>
        <asp:FileUpload runat="server" ID="FileUpload" ClientIDMode="Static" AllowMultiple="false" Style="display: none" onchange="OnFileSelected()" />
        <asp:Button runat="server" ID="btnSaveFile" OnClick="btnSaveFile_Click" Style="display: none" ClientIDMode="Static" />
    </div>

    <asp:GridView runat="server" ID="grdObjectList" ShowHeader="false" Width="100%" AllowCustomPaging="true" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdObjectList_PageIndexChanging" OnRowDataBound="grdObjectList_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a id="LnkObjectName" runat="server"></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
    </asp:GridView>




    <!-- Modal -->
    <div class="modal fade" id="modalFolder" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="gridSystemModalLabel">Add Folder</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">Directory Path</div>
                        <div class="col-md-4 col-md-offset-4">\</div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label for="TxtFolderName" class="label">Folder Name</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="TxtFolderName" placeholder="Folder Name" />
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-6 col-md-offset-3">.col-md-6 .col-md-offset-3</div>
                    </div>
                    <div class="row">
                        <div class="col-sm-9">
                            Level 1: .col-sm-9
                            <div class="row">
                                <div class="col-xs-8 col-sm-6">
                                    Level 2: .col-xs-8 .col-sm-6
                                </div>
                                <div class="col-xs-4 col-sm-6">
                                    Level 2: .col-xs-4 .col-sm-6
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close<i class="fa fa-close"></i></button>
                    <button runat="server" id="btnSaveFolder" type="button" class="btn btn-primary" onserverclick="btnSaveFolder_Click">Save<i class="fa fa-save"></i></button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script>
        $(document).ready(function () {
            $('#btnAddFolder').click(function () {
                $('#modalFolder').modal("show");
            });
            $('#btnAddFile').click(function () {
                $('#FileUpload').click();
            });
        });
        function OnFileSelected() {
            $('#btnSaveFile').click();
        }
    </script>
</asp:Content>
