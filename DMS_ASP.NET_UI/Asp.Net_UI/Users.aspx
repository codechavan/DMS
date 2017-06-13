<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="DMS.UI.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/grid-paging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:panel runat="server" id="pnlUserList">
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
    </asp:panel>
    <asp:panel runat="server" id="pnlUpdateUser">
        
			
            <div class="form-group">
				<label for="txtUserName" class="cols-sm-2 control-label">User Name</label>
					<div class="input-group">
						<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
						<input type="text" class="form-control" name="name" id="txtUserName" runat="server"  placeholder="Enter user Full Name"/>
					</div>
			</div>

			<div class="form-group">
				<label for="email" class="cols-sm-2 control-label">Email</label>
				<div class="cols-sm-10">
					<div class="input-group">
						<span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
						<input type="text" class="form-control" name="email" id="txtEmail" runat="server" placeholder="Enter user Email"/>
					</div>
				</div>
			</div>

            <div class="form-group">
				<label for="txtUserId" class="cols-sm-2 control-label">User ID</label>
					<div class="input-group">
						<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
						<input type="text" class="form-control" name="name" id="txtUserId" runat="server" placeholder="Enter unique User Id"/>
					</div>
			</div>

			<div class="form-group">
				<label for="txtPassword" class="cols-sm-2 control-label">Password</label>
				<div class="cols-sm-10">
					<div class="input-group">
						<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
						<input type="password" class="form-control" name="password" id="txtPassword" runat="server"  placeholder="Enter Password for user"/>
					</div>
				</div>
			</div>

			<div class="form-group">
				<label for="confirm" class="cols-sm-2 control-label">Confirm Password</label>
				<div class="cols-sm-10">
					<div class="input-group">
						<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
						<input type="password" class="form-control" name="confirm" id="txtConfirmPassword" runat="server" placeholder="Confirm Password"/>
					</div>
				</div>
			</div>

			<div class="form-group">
				<%--<a href="http://deepak646.blogspot.in" target="_blank" type="button" id="button" class="btn btn-primary btn-lg btn-block login-button">Register</a>--%>
                <button runat="server" id="btnCancelSave" class="btn btn-warning" onserverclick="btnCancelSave_ServerClick">Cancel</button>
                <button runat="server" id="btnAddUser" class="btn btn-primary" onserverclick="btnAddUser_ServerClick">Add</button>
			</div>
		
    </asp:panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
