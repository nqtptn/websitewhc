<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HT_PQNhom.aspx.cs" Inherits="Admin_Modules_F001_HT_PQNhom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Permission</title>
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
        var savePQ = function () {
            var IdNhom = tplNhomND.getSelectionModel().getSelectedNode().id.substring(3);
            var s = "<img src='../../resources/loading.gif'><font color='orange'>&nbsp &nbsp &nbsp &nbsp Savinh....</font>";
            lblLoading.setText(s, false);
            gplList.stopEditing();
            /*: v.05.08.2011
            var nR = gplList.getStore().data.length;
            for (var i = 0; i < nR; i++) {                
            var record = gplList.getStore().getAt(i);                
            Ext.net.DirectMethods.Save(i, nR, IdNhom, record.get('IDChucNang'), record.get('Xem'), record.get('Them'), record.get('Sua'), record.get('Xoa'), record.get('Tim'), record.get('Duyet'));
            }
            */
            var nR = gplList.getStore().data.length;
            if (nR > 0) {
                var rows = [];
                for (var r = 0; r < nR; r++) {
                    var record = gplList.getStore().getAt(r);
                    rows.push(record.data);
                }
                rows = Ext.encode(rows);
                Ext.net.DirectMethods.SavePhanQuyen(IdNhom, rows);
            }
        };
        var ChonTatCa = function () {
            if (chbChonTatCa.getValue() == true) {
                var jlen = gplList.getStore().data.length;
                for (var j = 0; j < jlen; j++) {
                    var record = gplList.getStore().getAt(j);
                    record.set('Read', true);
                }
            }
            else {
                var jlen = gplList.getStore().data.length;
                for (var j = 0; j < jlen; j++) {
                    var record = gplList.getStore().getAt(j);
                    record.set('Read', false);
                }
            }
        };
        var savePQV1 = function () {
            var IdNhom = hidNhom.getValue();
            Ext.net.DirectMethods.Delete(IdNhom);
            var records = gplList.store.getModifiedRecords();
            var nR = records.length;
            if (nR == 0)
                return;
            var s = "<img src='../../UserFiles/spinner.gif'><font color='orange'>Saving....</font>";
            lblLoading.setText(s, false);
            gplList.stopEditing();
            for (var i = 0; i < nR; i++) // for 1
            {
                Ext.net.DirectMethods.Save(i, nR, IdNhom, records[i].get('ID'), records[i].get('Read'), records[i].get('Add'), records[i].get('Edit'), records[i].get('Delete'), records[i].get('Search'), records[i].get('Browse'));
            } // End for 1            
        };
         
        
    </script>
</head>
<body>    
 <form id="form1" runat="server">
<ext:ResourceManager ID="ResourceManager1" runat="server" />
<ext:Store ID="stList" runat="server">            
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="funtionsname" />
                <ext:RecordField Name="Read" />
                <ext:RecordField Name="Add" />
                <ext:RecordField Name="Edit" />
                <ext:RecordField Name="Delete" />
                <ext:RecordField Name="Search" />
                <ext:RecordField Name="Browse" />                
            </Fields>
        </ext:JsonReader>                
    </Reader>
</ext:Store>
<ext:Hidden ID="hidNhom" runat="server" Text="-1"></ext:Hidden> 
                                                                          
<ext:Viewport ID="Viewport1" runat="server" Layout="border">
    <Items>
       <ext:Panel ID="Panel1" 
                runat="server" 
                Title="Permission" 
                Region="West"
                Layout="accordion"
                Width="250" 
                MinWidth="200" 
                MaxWidth="400" 
                Split="true" 
                Collapsible="true">
                <Items>
                    <ext:TreePanel 
                        ID="tplNhomND"
                        runat="server"                     
                        Width="250"                    
                        Height="550"                    
                        AutoScroll="true"                                        
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
                        <LoadMask ShowMask="true" Msg="Đang tải..." MsgCls="Đang tải dữ liệu..." />                                   
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
        <ext:Panel ID="Panel4" 
            runat="server" 
            Title="Danh sách chức năng" 
            Region="Center" AutoScroll="true">            
            <Items>
                <ext:FieldSet ID="FieldSet1" 
                    runat="server"
                    Title="Tìm kiếm"
                    Collapsible="true"
                    Layout="form">       
                        <Content>
                            <ext:CompositeField ID="CompositeField1" runat="server"  AnchorHorizontal="100%" FieldLabel="Key">
                            <Items>                                                                        
                                <ext:TextField ID="txtSearch" runat="server" Width="300"/>           
                                <ext:ComboBox ID="cbSearch" runat="server" />
                            </Items>
                            </ext:CompositeField>                                                                
                        </Content>                                                                                                     
                </ext:FieldSet>                                                
                <ext:Toolbar runat="server" ID="tbSaveItem" Flat="true">
                    <Items>
                        <ext:Button ID="btnSave" runat="server" Text="Save Icon="Disk">
                            <Listeners>
                                <Click Handler="savePQ();" />                  
                            </Listeners>
                        </ext:Button>   
                        <ext:Label ID="lblLoading" runat="server" StyleSpec="font-weight:bold; color:blue;" />                          
                    </Items>
                </ext:Toolbar>                                                                    
                <ext:GridPanel ID="gplList" 
                    runat="server" 
                    StoreID="stList" 
                    Title="" Height="490"                         
                    Flayout="Fit" AutoScroll="true" ClicksToEdit="1">     
                    <TopBar>
                        <ext:Toolbar ID="Toolbar11" runat="server">
                            <Content>
                                <ext:Checkbox runat="server" ID="chbChonTatCa" Checked="false" BoxLabel="Check all">
                                    <Listeners>
                                        <Check Fn="ChonTatCa" />
                                    </Listeners>
                                </ext:Checkbox>
                            </Content>
                        </ext:Toolbar>
                    </TopBar>                                                         
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>                                                                                         
                            <ext:Column DataIndex="funtionsname" Header="funtionsname" Width="200"/>                                                                                                                                      
                            <ext:CheckColumn DataIndex="Read" Header="Read" Width="50" Editable="true">   
                                <Editor>
                                    <ext:Checkbox ID="Checkbox1" runat="server" />
                                </Editor>
                            </ext:CheckColumn>
                            <%--<ext:CheckColumn DataIndex="Add" Header="Add" Width="70" Editable="true">
                                <Editor>
                                    <ext:Checkbox ID="Checkbox2" runat="server" />
                                </Editor>
                            </ext:CheckColumn>
                            <ext:CheckColumn DataIndex="Edit" Header="Edit" Width="50" Editable="true">                                                            
                                <Editor>
                                    <ext:Checkbox ID="Checkbox6" runat="server" />
                                </Editor>
                            </ext:CheckColumn>
                            <ext:CheckColumn DataIndex="Delete" Header="Delete" Width="50" Editable="true">   
                                <Editor>
                                    <ext:Checkbox ID="Checkbox3" runat="server" />
                                </Editor>
                            </ext:CheckColumn>
                            <ext:CheckColumn DataIndex="Search" Header="Search" Width="70" Editable="true">   
                                <Editor>
                                    <ext:Checkbox ID="Checkbox4" runat="server" />
                                </Editor>
                            </ext:CheckColumn>
                            <ext:CheckColumn DataIndex="Browse" Header="Browse" Width="50" Editable="true">   
                                <Editor>
                                    <ext:Checkbox ID="Checkbox5" runat="server" />
                                </Editor>
                            </ext:CheckColumn>--%>
                        </Columns>
                    </ColumnModel>                    
                    <Plugins>
                        <ext:GridFilters runat="server" ID="gvFilters" Local="true">
                            <Filters>                                
                                <ext:StringFilter DataIndex="funtionsname" />                                                  
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
