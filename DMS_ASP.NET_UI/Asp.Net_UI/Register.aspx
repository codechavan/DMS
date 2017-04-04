<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DMS.UI.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
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
            <div class="logo">Register</div>
            <!-- Main Form -->
            <div class="login-form-1 text-left">
                <div class="login-form-main-message"></div>
                <div class="main-login-form">
                    <div class="login-group">
                        <div class="form-group">
                            <label for="TxtSystemName" class="">System Name</label>
                            <input type="text" runat="server" class="form-control" id="TxtSystemName" maxlength="50" placeholder="System Name" />
                        </div>
                        <div class="form-group">
                            <label for="TxtUserId" class="">User ID</label>
                            <input type="text" class="form-control" id="TxtUserId" value="Administrator" placeholder="UserName" readonly="readonly" />
                        </div>
                        <div class="form-group">
                            <label for="TxtUserName" class="">User Name</label>
                            <input type="text" class="form-control" id="TxtUserName" value="Administrator" placeholder="User Name" readonly="readonly" />
                        </div>
                        <div class="form-group">
                            <label for="TxtPassword" class="">Password</label>
                            <input type="password" class="form-control" id="TxtPassword" maxlength="50" placeholder="Password" />
                        </div>
                        <div class="form-group">
                            <label for="TxtPasswordConfirm" class="">Confirm Password</label>
                            <input type="password" class="form-control" id="TxtPasswordConfirm" maxlength="50" placeholder="Confirm Password" />
                        </div>
                        <div class="form-group">
                            <label for="reg_email" class="">Email</label>
                            <input type="text" runat="server" class="form-control" id="TxtEmailId" placeholder="Email Id" />
                        </div>
                        <div class="form-group login-group-checkbox">
                            <input type="checkbox" class="" id="ChkTermAndConditions" runat="server" />
                            <label for="ChkTermAndConditions">i agree with <a href="#">terms</a></label>
                        </div>
                    </div>
                    <button type="submit" class="login-button" id="BtnRegister" onserverclick="BtnRegister_Click" runat="server"><i class="fa fa-chevron-right"></i></button>
                </div>
                <div class="etc-login-form">
                    <p>already have an account? <a href="Login.aspx">login here</a></p>
                </div>
            </div>
            <!-- end:Main Form -->
        </div>
    </form>
</body>
</html>
