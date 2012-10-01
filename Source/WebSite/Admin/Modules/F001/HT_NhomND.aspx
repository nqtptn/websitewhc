<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HT_NhomND.aspx.cs" Inherits="Admin_Modules_F001_HT_NhomND" %>

<%@ Register Src="~/Admin/Modules/F001/HT_NhomNDUC.ascx" TagName="HT_NhomNDUD" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UserGroup</title>
    <script type="text/javascript">
        var deleteItem = function () {
            Ext.Msg.confirm('Thông báo', 'Bạn có chắc là muốn xóa?', function (btn) {
                if (btn == "yes") {
                    Ext.net.DirectMethods.Delete();
                }
            });
        };
        //        var AddNew = function () {
        //            hidKey.setValue('');
        //            cbbDonVi.setValue('');
        //            txfTenNhomND.setValue('');
        //            txaMoTa.setValue('');
        //        };
        //        var Edit = function () {
        //            var sm = gplList.getSelectionModel().getSelections();
        //            if (sm.length > 0) {
        //                hidKey.setValue(sm[0].get('ID'));
        //                //cbbDonVi.setValue(sm[0].get('Madv'));
        //                txfTenNhomND.setValue(sm[0].get('TenNhomND'));
        //                txaMoTa.setValue(sm[0].get('MoTa'));
        //            }
        //        };
    </script>
</head>
<body>
    <form id="frmList" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Store ID="stList" runat="server" OnRefreshData="stList_RefreshData">
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
    <uc1:HT_NhomNDUD ID="HT_NhomNDUD" runat="server" />
    <%--    <ext:Hidden ID="hidKey" runat="server" />
    <ext:Hidden ID="hidMadv" runat="server" />--%>
    <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Height="600" Title="UserGroup" Padding="0"
                MonitorResize="true">
                <Items>
                   <%-- <ext:FieldSet ID="FieldSet1" runat="server" Title="Search" Collapsible="true" Layout="form">
                        <Content>
                            <ext:CompositeField ID="CompositeField2" runat="server" FieldLabel="Key" LabelWidth="100">
                                <Items>
                                    <ext:TextField ID="txtSearch" runat="server" Width="150" />
                                    <ext:ComboBox ID="cbSearch" runat="server" Width="250" />
                                </Items>
                            </ext:CompositeField>
                        </Content>
                    </ext:FieldSet>--%>
                    <ext:GridPanel ID="gplList" runat="server" StoreID="stList" Title="" Height="400"
                        AutoScroll="true" Anchor="100%">
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:Button runat="server" ID="btnView" Text="Reload" Icon="ApplicationViewList">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.View();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="btnAddNew" Text="Add" Icon="ApplicationAdd">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.AddNew();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="btnEdit" Text="Edit" Icon="ApplicationEdit">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.Edit();" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="btnDelete" Text="Delete" Icon="ApplicationDelete">
                                        <Listeners>
                                            <Click Handler="deleteItem();" />
                                        </Listeners>
                                    </ext:Button>
                                    <%--<ext:Button runat="server" ID="btnSearch" Text="Search" Icon="ApplicationGo">
                                        <Listeners>
                                            <Click Handler="Ext.net.DirectMethods.Search();" />
                                        </Listeners>
                                    </ext:Button>--%>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn runat="server" Width="35" />
                                <ext:Column DataIndex="userlevelname" Header="Name" Width="300" />
                                <ext:Column DataIndex="Note" Header="Mô tả" Width="200" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                        </SelectionModel>
                        <Plugins>
                            <ext:GridFilters runat="server" ID="gvFilters" Local="true">
                                <Filters>
                                    <ext:StringFilter DataIndex="userlevelname" />
                                    <ext:StringFilter DataIndex="Note" />
                                </Filters>
                            </ext:GridFilters>
                        </Plugins>
                        <LoadMask ShowMask="true" Msg="Đang tải..." MsgCls="Đang tải dữ liệu..." />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" DisplayInfo="true"
                                DisplayMsg="Dòng {0} - {1}/{2}" EmptyMsg="No data" StoreID="stList" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
