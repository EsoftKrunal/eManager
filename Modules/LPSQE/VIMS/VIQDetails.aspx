<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIQDetails.aspx.cs" Inherits="VIMS_VIQDetails" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <script src="jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VIMSScript.js" type="text/javascript"></script>
    <link href="VIMSStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .bar
    {
        background-color:#00CC00; 
        height:14px;
    }
    </style>
    <script type="text/javascript">
        function OpenChapterDetails(vid, chapid) {
            window.open('VIQChapterDetails.aspx?VIQId=' + vid + '&ChapterId=' + chapid,'');
        }
    </script>
    </head>
<body style="margin:0px 0px 0px 0px ">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
    <table align="center" width="100%" border="1" cellpadding="0" cellspacing="0" style="border-collapse:collapse">    
    <tr>
       <td style=" background-color:#FFC266; color:White; padding:4px; font-size:14px; text-align:center;">
       <b><asp:Label runat="server" ID="lblheader"></asp:Label>&nbsp;</b>
       </td>
    </tr>
    <tr>
       <td>
       <div style=" height:28px; overflow-x:hidden;overflow-y:scroll">
       <ul style="list-style-type:none; padding:0px; margin:0px;">
       <li style="padding:1px;color:Black;background-color:white; margin:1px;">
            <div style="text-align:left; border:none; padding:3px; background-color:#749299; color:White; padding-bottom:7px; font-size:12px;">
                <div style="width:80px;float:left; ">
                    <b>Chapter#</b>
                </div>
                <div style="width:300px; height:14px; float:right; position:relative;">
                    <b>Progress</b>
                </div>
                    <b>Chapter Name</b>
            </div>
       </li>
       </ul>
       </div>
       <div style=" height:370px; overflow-x:hidden;overflow-y:scroll">
       <ul style="list-style-type:none; padding:0px; margin:0px;">
       <asp:Repeater runat="server" ID="rpt_Chapters">
       <ItemTemplate>
       <li style="padding:1px;color:Black;background-color:white; margin:1px;">
            <div style="text-align:left; border:none; padding:3px; border-bottom: solid 1px #C2F3FF; padding-bottom:7px;">
                <div style="width:80px;float:left; ">
                <%#Eval("CHAPTERNO")%>
                </div>
                <div style="width:300px; background-color:White; border:solid 1px green; height:14px; float:right; position:relative; cursor:pointer;" onclick="OpenChapterDetails(<%#Eval("VIQID")%>,<%#Eval("CHAPTERID")%>);">
                    <div style="position:relative; z-index:100; text-align:center; color:Red;"><%#Eval("DONEQ")%> of <%#Eval("NOQ")%></div>
                    <div style='position:absolute; top:0px ;width:<%#Eval("PERDONE")%>%' class='bar'></div>
                </div>
                <div style="float:left;">
                  <%#Eval("ChapterName")%>
                </div>
                <div style="clear:both"></div>
            </div>
       </li>
       </ItemTemplate>
       </asp:Repeater>
       </ul>
       </div>
       </td>
    </tr>
    <tr >
      <td valign="top" style="vertical-align:top">
                <div style=" height:30px; background-color:#FFF5F5; padding-top:4px; text-align:center;  border:solid 1px #c2c2c2;">
                      <asp:Button runat="server" ID="btnExport" Text="Export to Office" CssClass="Btn1" Width="150px" style="padding:1px" onclick="btnExport_Click" />
                </div>
             </td>
       </tr>
    </table>
    </div>
    </form>
</body>
</html>
