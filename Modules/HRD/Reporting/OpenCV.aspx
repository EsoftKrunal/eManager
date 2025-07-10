<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenCV.aspx.cs" Inherits="Reporting_OpenPrintCV" Title="Print CV" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager></div>
        <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 149px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
                        <tr>
                            <td align="center" class="text headerband" style="width: 100%; ">Print CV</td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9" width="100%">
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                         </td>
                                    </tr>
                                    <tr>
                                       <tr>
                                        <td style="text-align: left;">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="left" colspan="2" >
                                                             <iframe runat="server" height="432px" id="IFRAME1" frameborder="1" style="width: 100%; overflow:auto"></iframe>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td> 
        </tr> 
        </table> 
    </form>
    <script language="javascript"  type="text/javascript">
    var i=0;
    for (i=0;i<=document.all.length-1;i++)
    {
        if (document.all(i).nodeName=="IMG")
        {
            if(document.all(i).height=="67" && document.all(i).width=="60")
            {
             document.all(i).width="80";
             document.all(i).height="110";
            } 
        }
    } 
    
    </script>
</body>
</html>


