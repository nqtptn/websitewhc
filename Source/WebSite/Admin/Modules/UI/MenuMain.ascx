<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuMain.ascx.cs" Inherits="Admin_Modules_UI_MenuMain" %>

<ext:Toolbar ID="tbMenu" runat="server">
    <Items>
        <ext:Button ID="Button1" runat="server" Text="" Flat="true" Flex="1" Cls="x-button"/>        
    </Items>
</ext:Toolbar>
<%--<ext:Menu ID="mnuMain"
   runat="server" 
    Hidden="false" 
    ShowSeparator="false" 
    EnableScrolling="false"
    Cls="x-menu-horizontal" 
    Floating="false" 
    SubMenuAlign="tl-bl?">  
</ext:Menu>--%>
<ext:Hidden runat="server" ID="hidIdNhomND" Text ="" />