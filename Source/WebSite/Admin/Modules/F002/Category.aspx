<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Admin_Modules_F002_Category" %>

<%@ Register Src="~/Admin/Modules/F002/CategoryUC.ascx" TagName="CategoryUC" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Danh mục</title>
    <script type="text/javascript">
        var deleteItem = function () {
            Ext.Msg.confirm('Thông báo', 'Bạn có chắc là muốn xóa?', function (btn) {
                if (btn == "yes") {
                    Ext.net.DirectMethods.Delete();
                }
            });
        };
        var IDParent = function (value) {
            if (value != null || value != "")
                return Ext.net.DirectMethods.GetParent(value);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Store ID="stList" runat="server" OnRefreshData="stList_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="ID">
                <Fields>
                    <ext:RecordField Name="ID" />
                    <ext:RecordField Name="IDParent" />
                    <ext:RecordField Name="Name" />
                    <ext:RecordField Name="Image" />
                    <ext:RecordField Name="Link" />
                    <ext:RecordField Name="DisplayOrder" />
                    <ext:RecordField Name="Type" />
                    <ext:RecordField Name="ShowHomepage" Type="Boolean" />
                    <ext:RecordField Name="isLogin" Type="Boolean" />
                    <ext:RecordField Name="isFormPost" Type="Boolean" />
                    <ext:RecordField Name="Published" Type="Boolean" />
                    <ext:RecordField Name="MetaTitle" />
                    <ext:RecordField Name="MetaKeyWord" />
                    <ext:RecordField Name="MetaDescription" />
                    <ext:RecordField Name="DateCreated" />
                    <ext:RecordField Name="CreatedBy" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <uc1:CategoryUC ID="CategoryUC" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Height="900" Title="Funtions" Padding="0"
                MonitorResize="true" Collapsible="true">
                <Items>
                    <%--<ext:FieldSet ID="FieldSet1" runat="server" Title="Search" Collapsible="true" Layout="form">
                        <Content>
                            <ext:CompositeField ID="CompositeField2" runat="server" AnchorHorizontal="100%" FieldLabel="">
                                <Items>
                                    <ext:DisplayField ID="DisplayField1" runat="server" Text="Key" />
                                    <ext:TextField ID="txtSearch" runat="server" Width="300" />
                                    <ext:ComboBox ID="cbSearch" runat="server" />
                                </Items>
                            </ext:CompositeField>
                        </Content>
                    </ext:FieldSet>--%>
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
                            <%-- <ext:Button runat="server" ID="btnSearch" Text="Search" Icon="ApplicationGo">
                                <Listeners>
                                    <Click Handler="Ext.net.DirectMethods.Search();" />
                                </Listeners>
                            </ext:Button>--%>
                        </Items>
                    </ext:Toolbar>
                    <ext:GridPanel ID="gvList" runat="server" StoreID="stList" Title="" Height="400"
                        Flayout="Fit" AutoScroll="true" Collapsible=true>
                        <TopBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="15" DisplayInfo="true"
                                DisplayMsg="Dòng {0} - {1}/{2}" EmptyMsg="No data" StoreID="stList" />
                        </TopBar>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn runat="server" Width="35" />
                                <ext:Column DataIndex="ID" Header="ID" Hidden="true" />
                                <ext:Column DataIndex="Name" Header="Tên" />
                                <ext:Column DataIndex="IDParent" Header="Danh mục cha">
                                    <%--<Renderer fn="IDParent" />--%>
                                </ext:Column>
                                <%-- <ext:CheckColumn DataIndex="ShowHomepage" Header="Trang chủ">
                                </ext:CheckColumn>--%>
                                <ext:CheckColumn DataIndex="isLogin" Header="Đăng nhập">
                                </ext:CheckColumn>
                                <ext:CheckColumn DataIndex="isFormPost" Header="Đăng bài">
                                </ext:CheckColumn>
                                <ext:CheckColumn DataIndex="Published" Header="Công cộng">
                                </ext:CheckColumn>
                                
                                <ext:TemplateColumn DataIndex="Image" Header="Image" Width="180">
                                    <Template ID="Template1" runat="server">
                                        <Html>
                                            <a href="../../../Category/{Image}">{Image}</a>
                                        </Html>
                                    </Template>
                                </ext:TemplateColumn>
                                <ext:Column DataIndex="DisplayOrder" Header="Thứ tự" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                        </SelectionModel>
                        <Plugins>
                            <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                                <Filters>
                                    <ext:StringFilter DataIndex="MaReport" />
                                    <ext:StringFilter DataIndex="TenReport" />
                                </Filters>
                            </ext:GridFilters>
                        </Plugins>
                        <LoadMask ShowMask="true" Msg="Đang tải..." MsgCls="Đang tải dữ liệu..." />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="15" DisplayInfo="true"
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
