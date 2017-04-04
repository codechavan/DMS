<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DMS.UI.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <link href="Content/register.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="text-center" style="padding: 50px 0">
            <div class="logo">Forgot Password</div>
            <!-- Main Form -->
            <div class="login-form-1">
                <div class="text-left">
                    <div class="etc-login-form">
                        <p>When you fill in your registered email address, you will be sent instructions on how to reset your password.</p>
                    </div>
                    <div class="login-form-main-message"></div>
                    <div class="main-login-form">
                        <div class="login-group">
                            <div class="form-group">
                                <label for="DdlSystems" class="">System Name</label>
                                <asp:DropDownList runat="server" ID="DdlSystems" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="TxtEmailId">Email address</label>
                                <input type="text" class="form-control" id="TxtEmailId" placeholder="Email Address" />
                            </div>
                        </div>
                        <button type="submit" class="login-button"><i class="fa fa-chevron-right"></i></button>
                    </div>
                    <div class="etc-login-form">
                        <p>already have an account? <a href="Login.aspx">login here</a></p>
                        <p>new user? <a href="Register.aspx">create new account</a></p>
                    </div>
                </div>
            </div>
            <!-- end:Main Form -->
        </div>
    </form>
</body>
</html>
