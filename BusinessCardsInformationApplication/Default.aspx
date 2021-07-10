<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BusinessCardsInformationApplication._Default"%>

<html>
    <head>
        <script type="text/javascript" src="/Scripts/Login-Script.js"></script>
        <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet">
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
        <link rel="stylesheet" href="/Content/Login-template.css" />
        <title>Login Page</title>
    </head>
    <body>
           
        <div id="login">
        <h3 class="text-center text-white pt-5">Login form</h3>
        <div class="container">
            <div id="login-row" class="row justify-content-center align-items-center">
                <div id="login-column" class="col-md-6">
                    <div id="login-box" class="col-md-12">
                        <form id="form1" runat="server" class="form" action="" method="post">
                            <h3 class="text-center text-info">Login</h3>
                            <div class="form-group">
                                <label for="username" class="text-info">Username:</label><br>
                                <asp:TextBox ID="txtBxUserName" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="password" class="text-info">Password:</label><br>
                                <asp:TextBox ID="txtBxPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">                               
                                <asp:Button ID="btnLogin" runat="server" OnClick="BtnLoginClicked" OnClientClick="BtnLoginClicked();" Text="Login" class="btn btn-info btn-md" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
</html>

