<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HT_NguoiDungUC.ascx.cs"
    Inherits="Admin_Modules_F001_HT_NguoiDungUC" %>
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
</script>
<ext:Store ID="stNhomND" runat="server">
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
<ext:Window ID="winItemInfo" runat="server" Icon="Information" Title="Thông tin "
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
                        <ext:ComboBox ID="cbbNhomND" runat="server" Width="550" StoreID="stNhomND" ValueField="ID"
                            DisplayField="userlevelname" Mode="Local">
                        </ext:ComboBox>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField1" FieldLabel="Username" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txfTenDangNhap" runat="server" Width="550">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField2" FieldLabel="Password" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txfMatKhau" runat="server" Width="200" InputType="Password">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField2" runat="server" Text="Re-Password:" Width="140">
                        </ext:DisplayField>
                        <ext:TextField ID="txfReMatKhau" runat="server" Width="200" InputType="Password"
                            IsRemoteValidation="true">
                            <RemoteValidation OnValidation="CheckMatKhau" />
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField5" FieldLabel="Fullname" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txfTenHienThi" runat="server" Width="200">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField1" runat="server" Text="Email:" Width="140">
                        </ext:DisplayField>
                        <ext:TextField ID="txfEmail" runat="server" FieldLabel="Email" Width="200" Regex="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            RegexText="Invalid format">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField4" FieldLabel="Address" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:TextField ID="txfAddress" runat="server" Width="200">
                        </ext:TextField>
                        <ext:DisplayField ID="DisplayField3" runat="server" Text="PhoneHome:" Width="140">
                        </ext:DisplayField>
                        <ext:TextField ID="txfPhoneHome" runat="server" FieldLabel="PhoneHome" Width="200" Regex="^[0-9]{10,12}$" RegexText="Invalid format">
                        </ext:TextField>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="invalid"
                            ControlToValidate="txtPhone" ValidationExpression="^[0-9]{10,12}$"></asp:RegularExpressionValidator>--%>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField7" FieldLabel="Birthday" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:DateField ID="dtbBirthday" runat="server" Width="200">
                        </ext:DateField>
                        <ext:DisplayField ID="DisplayField5" runat="server" Text="Notes:" Width="140">
                        </ext:DisplayField>
                        <ext:TextField ID="txfNote" runat="server" FieldLabel="Note" Width="200">
                        </ext:TextField>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField8" FieldLabel="UploadImage" LabelWidth="150"
                    runat="server" CombineErrors="false">
                    <Items>
                        <ext:FileUploadField ID="FileUploadField1" runat="server" Width="200">
                            <%--<DirectEvents>
                                <FileSelected OnEvent="FileUploadField_FileSelected" IsUpload="true" />
                            </DirectEvents>--%>
                        </ext:FileUploadField>
                        <ext:DisplayField ID="DisplayField4" runat="server" Text="Hình ảnh:" Width="140">
                        </ext:DisplayField>
                        <ext:Image ID="imgPhoto" runat="server" Width="100" Height="150">
                        </ext:Image>
                    </Items>
                </ext:CompositeField>
                <ext:CompositeField ID="CompositeField6" FieldLabel="Active" LabelWidth="150" runat="server"
                    CombineErrors="false">
                    <Items>
                        <ext:Checkbox ID="cbxActive" runat="server" Width="290">
                        </ext:Checkbox>
                        <ext:TextField ID="txfPhoto" runat="server" Hidden="true" Width="200">
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
