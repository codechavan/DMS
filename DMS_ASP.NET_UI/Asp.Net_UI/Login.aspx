<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DMS.UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <link href="Content/login.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="text-center" style="padding: 50px 0">
            <div class="logo">Login</div>
            <!-- Main Form -->
            <div class="login-form-1 text-left">
                <div class="login-form-main-message"></div>
                <div class="main-login-form">
                    <div class="login-group">
                        <div class="form-group">
                            <label for="TxtUserName" class="sr-only">Username</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="TxtUserName" placeholder="Username"/>
                        </div>
                        <div class="form-group">
                            <label for="TxtPassword" class="sr-only">Password</label>
                            <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="TxtPassword" placeholder="Password"/>
                        </div>
                        <div class="form-group">
                            <label for="DdlSystems" class="sr-only">System</label>
                            <asp:DropDownList runat="server" ID="DdlSystems" CssClass="form-control">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group login-group-checkbox">
                            <asp:CheckBox Text="Remember" ID="ChkRemember" runat="server" />
                        </div>
                    </div>
                    <button type="submit" class="login-button" runat="server" id="BtnLogin" onserverclick="BtnLogin_Click"><i class="fa fa-chevron-right"></i></button>
                </div>
                <div class="etc-login-form">
                    <p>forgot your password? <a href="ForgotPassword.aspx">click here</a></p>
                    <p>new user? <a href="Register.aspx">Register here</a></p>
                </div>
            </div>
            <!-- end:Main Form -->
        </div>
    </form>
</body>
</html>
