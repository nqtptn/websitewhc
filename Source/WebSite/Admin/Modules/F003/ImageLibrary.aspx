<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageLibrary.aspx.cs" Inherits="Admin_Modules_F003_ImageLibrary" %>

<%@ Register Src="~/Admin/Modules/F003/ImageLibraryUC.ascx" TagName="ImageLibraryUC" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bài viết</title>
    <script type="text/javascript">
        var filterTree = function (el, e) {
            var tree = tplNhomND,
                text = this.getRawValue();

            tree.clearFilter();

            if (Ext.isEmpty(text, false)) {
                return;
            }

            if (e.getKey() === Ext.EventObject.ESC) {
                clearFilter();
            } else {
                var re = new RegExp(".*" + text + ".*", "i");

                tree.filterBy(function (node) {
                    return re.test(node.text);
                });
            }
        };

        var clearFilter = function () {
            var field = TriggerField1,
                tree = tplNhomND;
            field.setValue("");
            tree.clearFilter();
            tree.getRootNode().collapseChildNodes(true);
            tree.getRootNode().ensureVisible();
        };
        //
        var deleteItem = function () {
            Ext.Msg.confirm('Thông báo', 'Bạn có chắc là muốn xóa?', function (btn) {
                if (btn == "yes") {
                    Ext.net.DirectMethods.Delete();
                }
            });
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
                <ext:RecordField Name="CategoryID" />
                <ext:RecordField Name="Name" />
                <ext:RecordField Name="Image" />
                <ext:RecordField Name="ShortDescription" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Store ID="stNhomND" runat="server">
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
    <ext:Hidden ID="hidNhom" runat="server" Text="-1">
    </ext:Hidden>
    <ext:Hidden ID="hidId" runat="server" Text="">
    </ext:Hidden>
    <ext:Hidden ID="hidChanged" runat="server" Text="0">
    </ext:Hidden>
    <uc1:ImageLibraryUC ID="ImageLibraryUC" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Title="UserGroup" Region="West" Layout="accordion"
                Width="250" MinWidth="200" MaxWidth="400" Split="true" Collapsible="true">
                <Items>
                    <ext:TreePanel ID="tplNhomND" runat="server" Width="250" Height="550" AutoScroll="true"
                        ContainerScroll="true">
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="Search:" />
                                    <ext:ToolbarSpacer />
                                    <ext:TriggerField ID="TriggerField1" runat="server" EnableKeyEvents="true">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyUp Fn="filterTree" Buffer="250" />
                                            <TriggerClick Handler="clearFilter();" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Root>
                        </Root>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel4" runat="server" Title="Users" Region="Center" AutoScroll="true">
                <Items>
                    <ext:FieldSet ID="FieldSet1" runat="server" Title="Search" Collapsible="true" Layout="form">
                        <Content>
                            <ext:CompositeField ID="CompositeField2" runat="server" AnchorHorizontal="100%" FieldLabel="">
                                <Items>
                                    <ext:DisplayField ID="DisplayField1" runat="server" Text="Key" />
                                    <ext:TextField ID="txtSearch" runat="server" Width="300" />
                                    <ext:ComboBox ID="cbSearch" runat="server" />
                                </Items>
                            </ext:CompositeField>
                        </Content>
                    </ext:FieldSet>
                    <ext:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <ext:Button runat="server" ID="btnAddNew" Text="Add" Icon="ApplicationAdd">
                                <Listeners>
                                    <Click Handler="Ext.net.DirectMethods.addItem();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnEdit" Text="Edit" Icon="ApplicationEdit">
                                <Listeners>
                                    <Click Handler="Ext.net.DirectMethods.editItem();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnDelete" Text="Delete" Icon="ApplicationDelete">
                                <Listeners>
                                    <Click Handler="deleteItem();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Icon="ApplicationGo">
                                <Listeners>
                                    <Click Handler="Ext.net.DirectMethods.Search();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                    <ext:GridPanel ID="gplList" runat="server" StoreID="stList" Title="" Height="490"
                        Flayout="Fit" AutoScroll="true">
                        
                        <TopBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="15" DisplayInfo="true"
                                DisplayMsg="Dòng {0} - {1}/{2}" EmptyMsg="No data" StoreID="stList" />
                        </TopBar>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn runat="server" Width="35" />
                                <ext:Column DataIndex="ID" Header="ID" Hidden="true" />
                                <ext:Column DataIndex="Name" Sortable="true" Header="Tên bài viết" Width="200" />
                                
                                <ext:TemplateColumn DataIndex="Image" Header="Image">
                                    <Template ID="Template1" runat="server">
                                        <Html>
                                            <img style="width: 60px; height: 45px;" src="../../../Ad/{Image}"></img>
                                        </Html>
                                    </Template>
                                </ext:TemplateColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                        </SelectionModel>
                        <Plugins>
                            <ext:GridFilters runat="server" ID="gvFilters" Local="true">
                                <Filters>
                                    <ext:StringFilter DataIndex="Name" />
                                    <ext:StringFilter DataIndex="CreatedBy" />
                                </Filters>
                            </ext:GridFilters>
                        </Plugins>
                        <LoadMask ShowMask="true" Msg="Đang tải..." MsgCls="Đang tải dữ liệu..." />
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
