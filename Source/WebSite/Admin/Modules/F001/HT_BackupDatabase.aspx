<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HT_BackupDatabase.aspx.cs" Inherits="Admin_Modules_F001_HT_BackupDatabase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content-right">
        <fieldset>
            <legend>
                <asp:Label ID="lblMessage" runat="Server" EnableViewState="False" ForeColor="Red"></asp:Label>
            </legend>
            <div class="content">
                <div class="search">
                    <div class="row">
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#FF6600"></asp:Label>
                        <asp:ImageButton ID="imbAdd" runat="server" ImageUrl="~/Admin/Modules/F001/images/btnBackup.png"
                            OnClick="imbAdd_Click" AlternateText="Thêm" />
                    </div>
                    <div style="height: 20px; position: absolute; left: 47%; top: 47%">
                    </div>
                    <asp:GridView ID="gvBackups" runat="server" AutoGenerateColumns="False" Width="780px"
                        CssClass="grid-view" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvBackups_PageIndexChanging"
                        AllowSorting="True" onrowdeleting="gvBackups_RowDeleting">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" Mode="NumericFirstLast"
                            Position="TopAndBottom" />
                        <Columns>
                            <asp:TemplateField HeaderText="Name"
                                ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Server.HtmlEncode(Eval("FileName").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# GetFileSizeInfo( (long)(Eval("FileSize")))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="DownloadButton" runat="server" CssClass="adminButton" CommandName="Download"
                                        Text="Download" CommandArgument='<%#Server.HtmlEncode(Eval("FileName").ToString())%>' OnCommand="DownloadButton_OnCommand"
                                        CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Restore" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="RestoreButton" runat="server" CssClass="adminButton" CommandName="Restore"
                                        Text="Restore" CommandArgument='<%#Server.HtmlEncode(Eval("FileName").ToString())%>' OnCommand="RestoreButton_OnCommand"
                                        CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="DeleteButton" runat="server" CssClass="adminButton" CommandName="Delete"
                                        Text="Delete" CommandArgument='<%#Eval("FullFileName")%>'
                                        OnCommand="DeleteButton_OnCommand" CausesValidation="false" ToolTip="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle HorizontalAlign="Left" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="Empty"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="spacer">
                </div>
            </div>
            </fieldset>
    </div>
    </form>
</body>
</html>
