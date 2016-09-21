<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Cashier.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SWim School Login</title>
    <link href="styles/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="space16">
        </div>
        <div class="logincontainer">
            <div class="logintext" >
                Swim School
            </div>
            <div class="hrlight"></div>
            <div style="min-height: 15%; width: 100%;"></div>

            <div class="loginbox">

                <div class="dialogtitlebar">
                    <div class="h1">LOGIN</div>
                </div>

                <div style="padding: 16px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="margin: 0px auto;">
                                <asp:TextBox ID="txtUsername" runat="server" Placeholder="User Name" CssClass="textbox" 
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin: 0px auto;">
                                <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" CssClass="textbox" TextMode="Password"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <div class="space32"></div>

                    <div style="text-align: right; padding-right: 16px;">
                        <asp:Button ID="btnLogin" runat="server" CssClass="buttonnav" Text="Login" Width="120px" OnClick="btnLogin_Click" />
                    </div>

                </div>


            </div>

        </div>
        <asp:HiddenField ID="hdfStaff" runat="server" />
        <asp:HiddenField ID="hdfBranch" runat="server" />
        <asp:HiddenField ID="hdfUser" runat="server" />
    </form>
</body>
</html>
