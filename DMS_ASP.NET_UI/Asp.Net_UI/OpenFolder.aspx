<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="OpenFolder.aspx.cs" Inherits="DMS.UI.OpenFolder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HiddenField runat="server" ID="hdnFolderId" />
    <button id="btnAddFolder" class="btn btn-default" type="button"><i class="fa fa-plus"></i>New</button>

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
                            <label for="TxtFolderName" class="sr-only">Folder Name</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="TxtFolderName" placeholder="Folder Name"/>
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
        });
    </script>
</asp:Content>
