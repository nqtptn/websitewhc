<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HT_ChucNangUC.ascx.cs" Inherits="Admin_Modules_F001_HT_ChucNangUC" %>
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
                <ext:RecordField Name="symbol" />
                <ext:RecordField Name="funtionsname" />
                <ext:RecordField Name="level" />
                <ext:RecordField Name="IDRoot" />
                <ext:RecordField Name="folderCN" />
                <ext:RecordField Name="pageCN" />
                <ext:RecordField Name="sequence" />
                <ext:RecordField Name="use" Type="Boolean" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="winItemInfo" runat="server" Icon="Information" Title="Funtions"
    Width="800" AutoShow="false" Modal="true" Hidden="true" Padding="0" AutoHeight="true"
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
        <ext:Hidden ID="hidChanged" runat="server" Text="0">
        </ext:Hidden>
        <ext:Hidden ID="hidId" runat="server" Text="">
        </ext:Hidden>
        <ext:Panel ID="Panel2" runat="server" Flayout="Fit" BodyCssClass="pnlUD" AutoScroll="true"
            Height="550">
            <Content>
                <ext:CompositeField ID="CompositeField3" FieldLabel="Funtion" LabelWidth="120"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:ComboBox ID="cbbThuocChucnang" StoreID="stDm" DisplayField="TenHienThi" ValueField="ID"
                            runat="server" Width="550">
                        </ext:ComboBox>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField4" FieldLabel="Sequence" LabelWidth="120"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:NumberField ID="nfCaphienthi" runat="server" Width="150">
                        </ext:NumberField>
                        <ext:Checkbox ID="ckbSuDung" runat="server" Checked="true">
                        </ext:Checkbox>
                        <ext:DisplayField ID="DisplayField1" runat="server" Text="Use" Width="70">
                        </ext:DisplayField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField1" FieldLabel="No." LabelWidth="120" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txtSoTT" runat="server" Width="150">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField3" runat="server" Text="Symbol" Width="100">
                        </ext:DisplayField>
                        <ext:TextField ID="txtKyhieu" runat="server" Width="290">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField2" FieldLabel="Name" LabelWidth="120"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txtTenchucnang" runat="server" Width="550">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField6" FieldLabel="Folder" LabelWidth="120"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txtThumucchua" runat="server" Width="150">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField2" runat="server" Text="Page" Width="100">
                        </ext:DisplayField>
                        <ext:TextField ID="txtTrangchucnang" runat="server" Width="290">
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
