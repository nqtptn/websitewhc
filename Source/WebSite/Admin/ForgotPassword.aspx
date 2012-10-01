<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Admin_ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Restore password</title>
    <script type="text/javascript">
        var guiYeuCau = function () {
            Ext.net.DirectMethods.ResetPassToCTV(Username.getValue(), Email.getValue());
        };
        var boqua = function () {

            location.href = 'Login.aspx';
        };
    </script>
</head>
<body id="body" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <form id="form1" runat="server">        
        <ext:ViewPort ID="ViewPort1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel 
                    ID="frmResetPass" 
                    runat="server" 
                    Title="Restore passwork"                
                    MonitorValid="true" 
                    Padding="5"                                
                    Border="true" Layout="FormLayout" Flex="1" LabelWidth="100" Header="false">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="Toolbar1">
                            <Items>
                                <ext:Button ID="btnGuiYeuCau" runat="server" Text="Send request" Icon="Accept">
                                    <Listeners>
                                        <Click Handler="if (#{frmResetPass}.getForm().isValid()) {guiYeuCau();} else { Ext.Msg.show({icon: Ext.MessageBox.ERROR, msg: 'Yêu cầu nhập đầy đủ thông tin', buttons:Ext.Msg.OK});}" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button2" runat="server" Text="Cancel">
                                    <Listeners>
                                        <Click Handler="boqua();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:TextField ID="Username" runat="server" AllowBlank="false" FieldLabel="Username" Width="250">
                        </ext:TextField>
                        <ext:TextField ID="Email" runat="server" AllowBlank="false" FieldLabel="Email" Width="250">
                        </ext:TextField>
                    </Items>                               
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="25" />
                    </BottomBar>               
                </ext:FormPanel>
            </Items>
        </ext:ViewPort>
                
    </form>
</body>
</html>
