<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIQDetails.aspx.cs" Inherits="VIMS_VIQDetails" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ShipSoft-VIMS</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />

    <link href="VettingStyle.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="VettingScript.js" type="text/javascript"></script>

    <script type="text/javascript">
        function OpenPopUP(id) {
            window.open('VIQDetails.aspx?VIQId=' + id, '');
        }
        $(document).ready(function () {
            $(".radselect").change(function () {
                var vslcode = $(".radselect").filter(":checked").attr('vslcode');
                var chid = $(".radselect").filter(":checked").attr('chapterid');
                var viqid = $(".radselect").filter(":checked").attr('viqid');

                $("#frmDet").attr("src", "VIQChapterDetails.aspx?VSL=" + vslcode + "&VIQId=" + viqid + "&ChapterId=" + chid);
            });
        });

    </script>
    <style type="text/css">
    .bar
    {
        background-color:LightGreen; 
        height:14px;
    }
   
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
      
    <tr>
     <td style=" background-color:#007A99; color:White; padding:4px; font-size:14px; text-align:center;">
        <b>Vetting Preparation</b>
     </td>
     </tr>
     <tr>
    <td style="text-align:center; background-color:#5CB8E6; padding:5px ; color:White; ">
        <b>
            <asp:Label  runat="server" ID="lblheader"></asp:Label>
        </b>
    </td>
    </tr>
     <tr>
       <td style="vertical-align:top">
        <table cellpadding="0" cellspacing="0" width="100%" border="1">
        <tr>
        <td style="text-align: center; vertical-align:top; width:450px;">
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 2px">
        <tr>
        <td>
           <div style=" height:650px; overflow-x:hidden;overflow-y:scroll">
           <ul style="list-style-type:none; padding:0px; margin:0px;">
           <asp:Repeater runat="server" ID="rpt_Chapters">
           <ItemTemplate>
           <li style="padding:1px;color:Black;background-color:white; margin:1px;">
                <div style="text-align:left; border:none; padding:3px; border-bottom: solid 1px #C2F3FF; padding-bottom:7px;">
                    <div style="width:30px;float:left; ">
                        <input type="radio" class="radselect" name="radselect" viqid='<%#Eval("VIQId")%>' chapterid='<%#Eval("ChapterId")%>' vslcode='<%#Eval("VesselCode")%>' />
                    </div>
                    <div style="width:40px;float:left; ">
                    <%#Eval("CHAPTERNO")%>
                    </div>
                    <div style="width:150px; background-color:White;  height:14px;   border:solid 1px Green;float:right; position:relative; cursor:pointer;">
                        <div style="position:relative; z-index:100; text-align:center; color:Red; margin-top:-4px;"><%#Eval("DONEQ")%> of <%#Eval("NOQ")%></div>
                        <div style='position:absolute;  top:0px ;width:<%#Eval("PERDONE")%>%' class='bar'></div>
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
        </table>
        </td>
        <td valign="top" style="vertical-align:top">
        <iframe width="100%" id='frmDet' height="650px" frameborder="no" scrolling="no" src=""></iframe>
        </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
   </div>
    </form>
</body>
</html>


