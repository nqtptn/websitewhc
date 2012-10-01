<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductUC.ascx.cs" Inherits="Admin_Modules_F002_ProductUC" %>
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
                <ext:RecordField Name="CategoryID" />
                <ext:RecordField Name="Name" />
                <ext:RecordField Name="Image" />
                <ext:RecordField Name="ShortDescription" />
                <ext:RecordField Name="FullDescription" />
                <ext:RecordField Name="Tag" />
                <ext:RecordField Name="Viewer" />
                <ext:RecordField Name="isComment" Type="Boolean" />
                <ext:RecordField Name="DateCreated" />
                <ext:RecordField Name="CreatedBy" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="winItemInfo" runat="server" Icon="Information" Title="Funtions" Width="800"
    AutoShow="false" Modal="true" Hidden="true" Padding="0" AutoHeight="true" Layout="Form"
    Collapsible="true">
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
                <ext:CompositeField ID="CompositeField3" FieldLabel="Danh mục cha" LabelWidth="100"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:ComboBox ID="cbbThuocDanhmuc" StoreID="stDm" DisplayField="Name" ValueField="ID"
                            runat="server" Width="150">
                        </ext:ComboBox>
                        <ext:DisplayField ID="DisplayField4" runat="server" Text="Tên" Width="100">
                        </ext:DisplayField>
                        <ext:TextField ID="txtName" runat="server" Width="150">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField5" runat="server" Text="Gửi bài viết" Width="100">
                        </ext:DisplayField>
                        <ext:Checkbox ID="ckbComment" runat="server" Checked="true">
                        </ext:Checkbox>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField1" FieldLabel="Hình ảnh" LabelWidth="100" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:FileUploadField ID="FileUploadField1" runat="server" Width="150">
                            <%--<DirectEvents>
                                <FileSelected OnEvent="FileUploadField_FileSelected" IsUpload="true" />
                            </DirectEvents>--%>
                        </ext:FileUploadField>
                        <ext:DisplayField ID="DisplayField2" runat="server" Text="Hình ảnh:" Width="100">
                        </ext:DisplayField>
                        <ext:Image ID="imgPhoto" runat="server" Width="100" Height="150">
                        </ext:Image>
                        <ext:TextField ID="txfPhoto" runat="server" Hidden="true" Width="200">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField3" runat="server" Text="Tag" Width="100">
                        </ext:DisplayField>
                        <ext:NumberField ID="txtTag" runat="server" Width="100">
                        </ext:NumberField>
                        <ext:DisplayField ID="DisplayField6" runat="server" Text="Viewer" Width="100">
                        </ext:DisplayField>
                        <ext:NumberField ID="txtViewer" runat="server" Width="100">
                        </ext:NumberField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField2" FieldLabel="Vắng tắt" Height="170" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextArea ID="txtShort" runat="server" Hidden="true" Width="200" Height="50px">
                        </ext:TextArea>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField8" FieldLabel="Chi tiết" Height="170" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:HtmlEditor ID="txfFull" runat="server" Height="170">
                        </ext:HtmlEditor>
                    </Items>
                </ext:CompositeField>
            </Content>
        </ext:Panel>
    </Items>
    <Listeners>
        <Hide Fn="checkChanged" />
    </Listeners>
</ext:Window>
