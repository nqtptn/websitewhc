<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HT_NhomNDUC.ascx.cs" Inherits="Admin_Modules_F001_HT_NhomNDUC" %>
<script type="text/javascript">
    var checkChanged = function () {
        var changed = document.getElementById('<%=hidChanged.ClientID%>').value;
        if (changed == 1)
            parent.stList.reload();
    }
</script>
<ext:Window 
    ID="winItemInfo" 
    runat="server" 
    Icon="Information" 
    Title="UserGroup"
    Width="800"     
    AutoShow="false" 
    Modal="true" 
    Hidden="true"     
    Padding="0" AutoHeight="true"    
    Layout="Form">      
    <Items>
        <ext:Toolbar runat="server" ID="tbSaveItem">            
            <Items>
                <ext:Button ID="btnSave" runat="server" Text="Save" Icon="Disk">
                    <Listeners>
                        <Click Handler="#{DirectMethods}.SaveItem();" />                  
                    </Listeners>
                </ext:Button>
                <ext:Button ID="btnCancel" runat="server" Text="Cancel" Icon="Cancel">                    
                    <Listeners>                        
                        <Click Handler="#{winItemInfo}.hide(null);" />
                    </Listeners>
                </ext:Button>               
            </Items>
        </ext:Toolbar>
        <ext:Hidden ID="hidChanged" runat="server" Text="0"></ext:Hidden> 
        <ext:Hidden ID="hidId" runat="server" Text=""></ext:Hidden>        
        <ext:Panel ID="Panel2" runat="server" Flayout="Fit" BodyCssClass="pnlUD" AutoScroll="true" Height="550">
            <Content>          
                <ext:CompositeField ID="CompositeField1" FieldLabel="Name" LabelWidth="100" runat="server"  CombineErrors="false"  >
                    <Items>                                    
                        <ext:TextField ID="txfTenNhomND" runat="server"  Width="550"></ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField2" FieldLabel="Notes" LabelWidth="100" runat="server"  CombineErrors="false" >
                    <Items>                                    
                        <ext:TextArea ID="txaMoTa" runat="server"  Width="550" Height="50"></ext:TextArea>
                    </Items>
                </ext:CompositeField>              
            </Content>
        </ext:Panel>    
    </Items>   
    <Listeners>
        <Hide Fn="checkChanged" />
    </Listeners>
</ext:Window>

