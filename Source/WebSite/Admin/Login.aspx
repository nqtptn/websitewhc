<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý website</title>
    <style type="text/css">
        .login-bg
        {
            background: url(images/bg_qlkhdt1.jpg);
            background-repeat: no-repeat;
            width: 800px; /* 800px*/
            height: 600px;
            margin: 0 auto;
        }
        .login-body
        {
            background-color: #003F87; /* #4D4DFF , BCD2EE, 3A66A7, 1B3F8B , 1874CD , 003F87	 */
            background-repeat: repeat;
        }
    </style>
    <script type="text/javascript">
        var btnLogin_Click = function () {
            Ext.net.DirectMethods.txtLogin();
        };
        var resetPass = function () {
            var win = winResetPass;
            win.setTitle('');
            win.show();
        };
    </script>
</head>
<body class="login-body">
    <form id="form1" runat="server">
    <div class="login-bg">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Window ID="winLogin" runat="server" Closable="false" Resizable="false" Height="150"
            Icon="Lock" Title="Sign in" Draggable="false" Width="350" Modal="true"
            Padding="5" Layout="Form">
            <Items>
                <ext:TextField ID="txtUsername" runat="server" ReadOnly="false" FieldLabel="Username"
                    AllowBlank="true" BlankText="Enter username." Text="" Width="200" />
                <ext:TextField ID="txtPassword" runat="server" ReadOnly="false" InputType="Password"
                    FieldLabel="Password" AllowBlank="true" BlankText="Enter Password." Text=""
                    Width="200" />
            </Items>
            <Buttons>
                <ext:Button ID="btnLogin" runat="server" Text="Đăng nhập" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="btnLogin_Click">
                            <EventMask ShowMask="true" Msg="Đang tải..." MinDelay="1000" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:LinkButton runat="server" ID="lnkResetPass" Icon="Help" Text="Forget username?">
                    <Listeners>
                        <Click Fn="resetPass" />
                    </Listeners>
                    <%--<DirectEvents>
                        <Click OnEvent="lnkResetPass_Click">
                            <EventMask ShowMask="true" Msg="Đang xử lý..." MinDelay="1000" />
                        </Click>
                    </DirectEvents>--%>
                </ext:LinkButton>
            </Buttons>
            <KeyMap>
                <ext:KeyBinding>
                    <Keys>
                        <ext:Key Code="ENTER" />
                    </Keys>
                    <Listeners>
                        <Event Fn="btnLogin_Click" />
                    </Listeners>
                </ext:KeyBinding>
            </KeyMap>
        </ext:Window>
        <ext:Window ID="winResetPass" runat="server" Icon="Report" Title="Reset password" Width="450"
            Height="250" AutoShow="false" Modal="true" Hidden="true" Layout="Form" Maximizable="true"
            AutoScroll="true">
            <AutoLoad Url="ResetPassword.aspx" Mode="IFrame" TriggerEvent="show" ReloadOnEvent="true"
                ShowMask="true" MaskMsg="Đang tải ...">
                <Params>
                    <ext:Parameter Name="madv" Value="" Mode="Value" />
                </Params>
            </AutoLoad>
        </ext:Window>
    </div>
    </form>
</body>
</html>
