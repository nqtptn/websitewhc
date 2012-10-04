<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollectionImage.ascx.cs"
    Inherits="Admin_Modules_F003_CollectionImage" %>
<script type="text/javascript">
    var checkChanged = function () {
        if ('<%=hidChanged.ClientID%>'.getValue() == '1') {
            parent.stList.reload();
        }
    };
    var checkInput = function () {
        if ('<%=txfReMatKhau.ClientID%>'.getText() != '<%=txfMatKhau.ClientID%>'.getText()) {
            alert("Mật khẩu nhập lại không đúng");
            '<%=txfReMatKhau.ClientID%>'.focus();
        }
    };
    var UpdateUploadInfo = function (el, label) {

        debugger;
        var ret = true;

        if (Ext.isIE) {
            return;
        }
        var size = 0;
        var names = '';
        for (var num1 = 0; num1 < el.files.length; num1++) {
            var file = el.files[num1];
            names += file.name + '\r\n';
            //alert(file.name+" "+file.type+" "+file.size);
            size += file.size;
        }
        var txt = '';
        var fileSize = Ext.util.Format.fileSize(size);

        if (size > 31457280) {
            txt = String.format('You are trying to upload {0}. Max. allowed upload size is 30 MB', fileSize);
            ret = false;
        } else {
            txt = String.format('{0} file(s) of total size {1}', el.files.length, fileSize);
        }

        label.setText(txt);
        return ret;
    }

    var SetMultipleUpload = function (fileupload, label) {
        fileupload.fileInput.set({ multiple: 'multiple' });

        if (Ext.isIE) {
            label.setText('IE does not support multiple file upload, to use this feature use Firefox or Chrome');
        }
    }
</script>
<ext:Store ID="stNhomND" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="ImageLibraryID" />
                <ext:RecordField Name="Name" />
                <ext:RecordField Name="Title" />
                <ext:RecordField Name="Image" />
                <ext:RecordField Name="DateCreated" />
                <ext:RecordField Name="CreatedBy" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Store ID="stImageLibrary" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="userlevelname" />
                <ext:RecordField Name="Note" />
                <ext:RecordField Name="IsAdminGroup" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="winItemInfo" runat="server" Icon="Information" Title="Upload image"
    Width="800" AutoShow="false" Modal="true" Hidden="true" Padding="0" AutoHeight="true"
    Layout="FormLayout" ReloadOnEvent="true">
    <Items>
        <ext:Toolbar runat="server" ID="tbItem">
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
                                <ext:Button ID="Button2" runat="server" Text="Reset">
                    <Listeners>
                        <%--when resetting the form  make sure to call SetMultipleUpload() otherwise you won't be able to select multiple files, whithout rendering the control again--%>
                        <Click Handler="#{BasicForm}.getForm().reset(); SetMultipleUpload( #{FileUploadField1}, #{UpdateLabel});#{UpdateLabel}.setText('');" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:Toolbar>
        <ext:Hidden ID="hidIsLoadNhomND" runat="server" Text="0">
        </ext:Hidden>
        <ext:Hidden ID="hidId" runat="server" Text="">
        </ext:Hidden>
        <ext:Hidden ID="hidChanged" runat="server" Text="0">
        </ext:Hidden>
        <ext:Panel ID="Panel2" runat="server" Flayout="Fit" AutoScroll="true" Height="550"
            Padding="5">
            <Content>
                <ext:CompositeField ID="CompositeField3" FieldLabel="UserGroup" LabelWidth="150"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:ComboBox ID="cbbImageLibrary" runat="server" Width="550" StoreID="stImageLibrary"
                            ValueField="ID" DisplayField="Name" Mode="Local">
                        </ext:ComboBox>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField2" FieldLabel="Tên" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txfName" runat="server" Width="200">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField2" runat="server" Text="Tựa đề:" Width="140">
                        </ext:DisplayField>
                        <ext:TextField ID="txtTitle" runat="server" Width="200">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField8" FieldLabel="UploadImage" LabelWidth="150"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:FileUploadField ID="FileUploadField1" runat="server" ButtonText="Add Files"
                            Icon="Add" ButtonOnly="true" AllowBlank="false">
                            <Listeners>
                                <Render Handler="SetMultipleUpload( this, #{UpdateLabel}) ;" />
                                <FileSelected Handler="if(!UpdateUploadInfo(this.fileInput.dom, #{UpdateLabel})) {this.reset();SetMultipleUpload( this, #{UpdateLabel})}" />
                            </Listeners>
                        </ext:FileUploadField>
                        <ext:Label ID="UpdateLabel" runat="server">
                        </ext:Label>
                        <ext:DisplayField ID="DisplayField4" runat="server" Text="Hình ảnh:" Width="140">
                        </ext:DisplayField>
                        <ext:Image ID="imgPhoto" runat="server" Width="100" Height="150">
                        </ext:Image>
                        <ext:TextField ID="txfPhoto" runat="server" Hidden="true" Width="200">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
            </Content>
        </ext:Panel>
    </Items>
    <Listeners>
        <Hide Fn="checkChanged" />
        <Hide Handler="#{BasicForm}.getForm().reset(); SetMultipleUpload( #{FileUploadField1}, #{UpdateLabel});#{UpdateLabel}.setText('');" />
    </Listeners>
</ext:Window>
