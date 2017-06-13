<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="OpenFolder.aspx.cs" Inherits="DMS.UI.OpenFolder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/grid-paging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HiddenField runat="server" ID="hdnFolderId" />
    <div class="container">
        <div class="'row">
            <button id="btnAddFolder" class="btn btn-default" type="button"><i class="fa fa-plus"></i>New Folder</button>
            <div style="display: inline-block" runat="server" id="pnlFileUpload">
                <button id="btnAddFile" class="btn btn-default" type="button"><i class="fa fa-plus"></i>New File</button>
                <asp:FileUpload runat="server" ID="FileUpload" ClientIDMode="Static" AllowMultiple="false" Style="display: none" onchange="OnFileSelected()" />
                <asp:Button runat="server" ID="btnSaveFile" OnClick="btnSaveFile_Click" Style="display: none" ClientIDMode="Static" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6">
                <asp:GridView runat="server" ID="grdObjectList" ShowHeader="false" CellPadding="10" DataKeyNames="ObjectId" CellSpacing="2" Width="100%" AllowCustomPaging="true" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdObjectList_PageIndexChanging" OnRowDataBound="grdObjectList_RowDataBound" OnRowCommand="grdObjectList_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a id="LnkObjectName" runat="server"></a>
                                <asp:LinkButton runat="server" ID="LnkFileName" CommandName="OpenFile"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                </asp:GridView>
            </div>
            <div class="col-xs-6" id="pnlProperties" runat="server">
                <div class="row" id="pnlProperty1" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName1" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue1" />
                    </div>
                </div>
                <div class="row" id="pnlProperty2" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName2" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue2" />
                    </div>
                </div>
                <div class="row" id="pnlProperty3" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName3" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue3" />
                    </div>
                </div>
                <div class="row" id="pnlProperty4" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName4" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue4" />
                    </div>
                </div>
                <div class="row" id="pnlProperty5" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName5" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue5" />
                    </div>
                </div>
                <div class="row" id="pnlProperty6" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName6" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue6" />
                    </div>
                </div>
                <div class="row" id="pnlProperty7" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName7" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue7" />
                    </div>
                </div>
                <div class="row" id="pnlProperty8" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName8" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue8" />
                    </div>
                </div>
                <div class="row" id="pnlProperty9" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName9" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue9" />
                    </div>
                </div>
                <div class="row" id="pnlProperty10" runat="server">
                    <div class="col-xs-8 col-sm-6">
                        <asp:Label Text="" ID="lblPropertyName10" runat="server" />
                    </div>
                    <div class="col-xs-4 col-sm-6">
                        <asp:TextBox runat="server" ID="txtPropertyValue10" />
                    </div>
                </div>
                <div class="row">
                    <button runat="server" id="btnUpdateFileProperties" onserverclick="btnUpdateFileProperties_ServerClick"><i class="fa fa-save"></i>Update</button>
                </div>
            </div>
        </div>
    </div>
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
