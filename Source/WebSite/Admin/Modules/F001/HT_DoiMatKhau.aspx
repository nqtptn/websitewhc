<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HT_DoiMatKhau.aspx.cs" Inherits="Admin_Modules_F001_HT_DoiMatKhau" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change password</title>
    <script type="text/javascript">
        var cancel = function () {
            txfMatKhau.setValue("");
            txfReMatKhau.setValue("");
        }
    </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <form id="form1" runat="server">
    <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
        <Items>
            <ext:Panel ID="Panel2" runat="server" Flayout="Fit" AutoScroll="true" Padding="5">
                <Items>    
                    <ext:Toolbar runat="server" ID="tbSaveItem">            
                        <Items>
                            <ext:Button ID="btnChangeMK" runat="server" Text="Save" Icon="Disk">
                                <Listeners>
                                    <Click Handler="Ext.net.DirectMethods.changeMK();" />                  
                                </Listeners>
                            </ext:Button>                            
                            <ext:Button ID="btnCancel" runat="server" Text="Cancel" Icon="Cancel">                    
                                <Listeners>                        
                                    <Click Handler="cancel();" />
                                </Listeners>
                            </ext:Button>               
                        </Items>
                    </ext:Toolbar>
                    <ext:CompositeField ID="CompositeField1" FieldLabel="Fullname" LabelWidth="140" runat="server"  CombineErrors="false"  >
                        <Items>                                                            
                            <ext:TextField ID="txfTenHienThi" runat="server" Disabled="true" Width="200"/> 
                        </Items>
                    </ext:CompositeField>                                                           
                    <ext:CompositeField ID="CompositeField2" FieldLabel="Username" LabelWidth="140" runat="server"  CombineErrors="false"  >
                        <Items>                                                            
                            <ext:TextField ID="txfTenDangNhap" runat="server" Disabled="true" Width="200"/>
                        </Items>
                    </ext:CompositeField>                                           
                    <ext:CompositeField ID="CompositeField3" FieldLabel="New password" LabelWidth="140" runat="server"  CombineErrors="false"  >
                        <Items>                                                            
                            <ext:TextField ID="txfMatKhau" runat="server"  Width="200" InputType="Password" ></ext:TextField>   
                        </Items>
                    </ext:CompositeField>           
                    <ext:CompositeField ID="CompositeField4" FieldLabel="Re-new password" LabelWidth="140" runat="server"  CombineErrors="false"  >
                        <Items>                                                            
                            <ext:TextField ID="txfReMatKhau" runat="server"  Width="200" InputType="Password" IsRemoteValidation="true" >
                                <RemoteValidation OnValidation="CheckMatKhau" /> 
                            </ext:TextField>  
                        </Items>
                    </ext:CompositeField>                                                                 
                </Items>
            </ext:Panel> 
        </Items>
    </ext:Viewport>          
    </form>
</body>
</html>
