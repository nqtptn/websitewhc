<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentUC.ascx.cs" Inherits="Admin_Modules_F002_CommentUC" %>
<script type="text/javascript">
    var checkChanged = function () {
        var changed = document.getElementById('<%=hidChanged.ClientID%>').value;
        if (changed == 1)
            parent.stList.reload();
    }
</script>
<ext:Store ID="stDm" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="ProductID" />
                <ext:RecordField Name="Name" />
                <ext:RecordField Name="Email" />
                <ext:RecordField Name="Phone" />
                <ext:RecordField Name="Comment" />
                <ext:RecordField Name="DateCreated" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="winItemInfo" runat="server" Icon="Information" Title="Funtions" Width="800"
    AutoShow="false" Modal="true" Hidden="true" Padding="0" AutoHeight="true" Layout="Form"
    Collapsible="true" >
    <Items>
        <ext:Toolbar runat="server" ID="tbSaveItem">
            <Items>
                <ext:Button ID="btnSave" runat="server" Text="Save" Icon="Disk">
                    <Listeners>
                        <Click Handler="#{DirectMethods}.Delete();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="btnCancel" runat="server" Text="Cancel" Icon="Cancel">
                    <Listeners>
                        <Click Handler="#{winItemInfo}.hide(null);" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:Toolbar>
        <ext:Hidden ID="hidChanged" runat="server" Text="0">
        </ext:Hidden>
        <ext:Hidden ID="hidId" runat="server" Text="">
        </ext:Hidden>
        <ext:Panel ID="Panel2" runat="server" Flayout="Fit" BodyCssClass="pnlUD" AutoScroll="true"
            Height="550">
            <Content>
                <ext:CompositeField ID="CompositeField3" FieldLabel="Bài viết" LabelWidth="100" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txtProduct" runat="server" Width="150">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField4" runat="server" Text="Tên" Width="100">
                        </ext:DisplayField>
                        <ext:TextField ID="txtName" runat="server" Width="150">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField1" FieldLabel="Email" LabelWidth="100" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txtEmail" runat="server" Width="150">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField1" runat="server" Text="Phone" Width="100">
                        </ext:DisplayField>
                        <ext:TextField ID="txtPhone" runat="server" Width="150">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField8" FieldLabel="Comment" Height="170" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:HtmlEditor ID="txfComment" runat="server" Height="170">
                        </ext:HtmlEditor>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField2" FieldLabel="Ngày viết" LabelWidth="100"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txtDate" runat="server" Width="150">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
            </Content>
        </ext:Panel>
    </Items>
    <Listeners>
        <Hide Fn="checkChanged" />
    </Listeners>
</ext:Window>
