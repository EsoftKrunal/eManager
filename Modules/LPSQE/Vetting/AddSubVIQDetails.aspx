<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSubVIQDetails.aspx.cs" Inherits="VIMS_AddSubVIQDetails" %>
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
    function ConfirmDisable(ctl) {
            if (confirm('Are you sure to lock this?')) {
                DisableMe(ctl);
            }
            else {
                return false;
            }
        }
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
        <b>VIQ Preparation</b>
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
        <td style="text-align: center; vertical-align:top; width:550px;">
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 2px">
        <tr>
        <td>
           <div style="height:30px; overflow-y:scroll; overflow-x:hidden;">
           <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
           <thead>
           <tr>
           <td style="width:50px">Select</td>
           <td style="width:50px">Ques#</td>
           <td>Question</td>
           <td style="width:150px">Selected</td>
           <td style="width:30px">&nbsp;</td>
           </tr>
           </table>
           </div>
           <div style=" height:620px; overflow-x:hidden;overflow-y:scroll">
           <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
           <tbody>
           <asp:Repeater runat="server" ID="rpt_Chapters">
           <ItemTemplate>
           <tr>
                <td style="width:50px; text-align:center; "><input type="radio" class="radselect" name="radselect" pviqid='<%#Eval("PVIQId")%>' viqid='<%#Eval("VIQId")%>' chapterid='<%#Eval("ChapterId")%>' vslcode='<%#Eval("VesselCode")%>' /></td>
                <td style="width:50px; text-align:center;"><%#Eval("CHAPTERNO")%></td>
                <td style="text-align:left;"><%#Eval("ChapterName")%></td>
                <td style="text-align:left;">
                    <div style="width:150px; background-color:White;  height:14px;   border:solid 1px Green;float:right; position:relative; cursor:pointer;">
                        <div style="position:relative; z-index:100; text-align:center; color:Red; margin-top:-4px;"><%#Eval("DONEQ")%> of <%#Eval("NOQ")%></div>
                        <div style='position:absolute;  top:0px ;width:<%#Eval("PERDONE")%>%' class='bar'></div>
                    </div>
                </td>
                <td style="width:30px">&nbsp;</td>
           </tr>
           </ItemTemplate>
           </asp:Repeater>
           </tbody>
           </table>
       </div>
       </td>
       </tr>
        </table>
        </td>
        <td valign="top" style="vertical-align:top;">
        <iframe width="100%" id='frmDet' height="650px" frameborder="no" scrolling="no" src=""></iframe>
        </td>
         </tr>
        </table>
       </td>
      </tr>
      <tr runat="server" id="tr_Create" visible="false">
      <td style="padding:5px; border:solid 1px grey; vertical-align:top; background-color:#E0F5FF" valign="middle">
      <center>
        <div style="display:inline;vertical-align:top;">
            Target Date : <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ValidationGroup="g1"></asp:TextBox>
            <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> 
            <asp:RequiredFieldValidator runat="server" ID="rf1" ControlToValidate="txtFromDate" ErrorMessage="*" ValidationGroup="g1"></asp:RequiredFieldValidator>
        </div>
        <asp:Button runat="server" ID="btnCreateQ" Text="Create Questionnaire for Ship" CssClass="Btn1" ValidationGroup="g1" OnClick="btnCreateQ_Click"  />
        <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
        </center>
      </td>
      </tr>
      <tr runat="server" id="tr_Edit" visible="false">
      <td style="padding:5px; border:solid 1px grey; vertical-align:top; background-color:#E0F5FF" valign="middle">
        <asp:Button runat="server" ID="btnLock" Text=" Lock for Editing " CausesValidation="false" CssClass="Btn1" ValidationGroup="g1" OnClick="btnLock_Click" OnClientClick="return ConfirmDisable(this);" style="float:right;"/>
      </td>
      </tr>
     </table>
   </div>
    </form>
    <script type="text/javascript">
        function OpenPopUP(id) {
            window.open('VIQDetails.aspx?VIQId=' + id, '');
        }
        $(document).ready(function () {
            $(".radselect").change(function () {
                var vslcode = $(".radselect").filter(":checked").attr('vslcode');
                var chid = $(".radselect").filter(":checked").attr('chapterid');
                var pviqid = $(".radselect").filter(":checked").attr('pviqid');
                var md=<%=VIQId%>;
                if(md>0)
                {
                    $("#frmDet").attr("src", "EditVIQChapterDetails.aspx?VSL=" + vslcode + "&PVIQId=" + pviqid + "&VIQId=" + md + "&ChapterId=" + chid);
                }
                else
                {
                    $("#frmDet").attr("src", "AddVIQChapterDetails.aspx?VSL=" + vslcode + "&PVIQId=" + pviqid + "&ChapterId=" + chid);
                }
            });
        });

    </script>
</body>
</html>


