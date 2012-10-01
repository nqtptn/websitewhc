<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<%@ Register src="Modules/UI/MenuMain.ascx" tagname="MenuMain" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administration</title>    
    <script type="text/javascript" src="resources/js/exporter.js"></script>
    <script type="text/javascript">
        var loadPage = function (tabPanel, id, title, href) {
            var tab = tabPanel.getItem(id);
            if (!tab) {
                tab = tabPanel.add({
                    id: id,
                    title: title,
                    closable: true,
                    autoLoad: {
                        showMask: true,
                        url: href,
                        mode: 'iframe',
                        maskMsg: 'Loading ' + href + '...'
                    },
                    listeners: {
                        update: {
                            fn: function (tab, cfg) {
                                cfg.iframe.setHeight(cfg.iframe.getSize().height - 20);
                            },
                            scope: this,
                            single: true
                        }
                    }
                });
            }
            tabPanel.setActiveTab(tab);
        }
        $(function () {
            setInterval(KeepSessionAlive, 30000);
        });

        function KeepSessionAlive() {
            $.post("/KeepSessionAlive.ashx", null, function () {
                $("#result").append("<p>Session is alive and kicking!<p/>");
            });
        }   
 </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <form id="form1" runat="server">               
   <ext:Viewport ID="Viewport1" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlTop" 
                runat="server"
                Title="" 
                Region="North"
                Split="true"
                Height="30"
                Padding="1"
                Html=""
                Collapsible="true" Header="false">            
                <Content>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    <uc1:MenuMain ID="MenuMain1" runat="server" />
                                </td>
                                <td>
                                    <ext:Label runat="server" ID="lblUser" Text="" Icon="User"></ext:Label>
                                </td>
                                <td>                                    
                                    <ext:LinkButton ID="btnLogOut" runat="server" Text="Log out" Icon="UserHome">
                                        <DirectEvents>
                                            <Click OnEvent="btnLogOut_Click"></Click>
                                        </DirectEvents>                                        
                                    </ext:LinkButton> 
                                </td>
                            </tr>
                        </tbody>
                    </table>                                                                               
                </Content>
            </ext:Panel>            
            <ext:TabPanel ID="tabPages" runat="server" Region="Center" AutoScroll="true">
                <Items>
                    <%--<ext:Panel ID="pnlHome" 
                        runat="server" 
                        Title="Trang chủ" 
                        Border="false" 
                        Padding="3"
                        Html="<h1></h1>"
                        />--%>                    
                </Items>
            </ext:TabPanel>            
            <ext:Panel ID="pnlBottom" 
                runat="server"
                Title=""
                Region="South"
                Split="true"
                Collapsible="false"
                Height="20"
                Padding="3"
                Html="" Header="false"
                />
        </Items>
    </ext:Viewport>
                   
    <%--<script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-19135912-3']);
        _gaq.push(['_setDomainName', '.ext.net']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>--%>
    
    </form>
</body>
</html>

