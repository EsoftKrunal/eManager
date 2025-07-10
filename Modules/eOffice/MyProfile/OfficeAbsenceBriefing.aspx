<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceBriefing.aspx.cs" Inherits="Emtm_OfficeAbsenceBriefing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Briefing / Debriefing</title>
    <script type="text/javascript" src="../../JS/jquery.min.js"></script>
    <link href="../style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/Common.js"></script>
    <script type="text/javascript" language="javascript">
            function openwindow(id) {

                  window.open("Emtm_PopupAttachment.aspx?expid=" + id, "att", "");
              }
              function openreport(id) {

                  // window.open("../../Reporting/OfficeAbsence_Expense.aspx?id=" + id, "report", "");
                  window.open('../../Reporting/OfficeAbsenceExpense.aspx?id=' + id, 'asdf', '');
              }
              function CheckVSL(check) {
                  var Vsl = check.getAttribute('vsl');
                  var X= document.getElementsByName(Vsl);
                  var i = 0;
                  for (i = 0; i <= X.length - 1; i++) {
                      X[i].getElementsByTagName("input")[0].checked = check.checked;
                  }
              }
              function CheckAll(Obj) {
                  var X = document.getElementsByTagName('input');
                  for (i = 0; i <= X.length - 1; i++) {
                      X[i].checked = Obj.checked;
                  }
              }
   </script>
        <style type="text/css">
            .btn11sel_1
    {
        font-size:14px;
        background-color:#FFEBE0;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #FFEBE0;
        padding:5px;
    }
    .btn11sel
    {
        font-size:14px;
        background-color:#C2E0FF;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #C2E0FF;
        padding:5px;
    }
    .btn11
    {
        font-size:14px;
        background-color:#e2e2e2;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #c2c2c2;
        padding:5px;
    }
    .btnstlnew
    {
        background-color:#E6B2FF; 
        border:solid 1px #2e2e4c; 
        padding:3px; 
        color:Black;
    }
    .csslnkclose
    {
        text-decoration:none;
        color:Blue;
    }
    .csslnkclose:hover
    {
        text-decoration:underline;
        color:Red;
    }
    .csslnkclose:active
    {
        text-decoration:none;
        color:Blue;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <table width="100%">
        <tr>
        <td valign="top">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <div runat="server" id="dvBreifing">
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Briefing</div>
          <table id="Table1" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation" Font-Size="Large" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose" Font-Size="Large" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay" Font-Size="Large" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                  <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblNotifiedon" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblTakeOverOn" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                <tr>
                <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
                    <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                </td>
                </tr>
        </table>        
          <asp:Panel runat="server" ID="pnlBriefing" Visible="true" style="padding:5px;">
                 <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="font-weight:bold; font-size:12px;"> Briefing Remarks </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBriefingRemarks" runat="server" Width="99%" TextMode="MultiLine" Height="200px" ReadOnly="true" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;" >
                            <asp:Label ID="lblBriefingByOn" runat="server" style="font-weight:bold;font-size:14px;color:#66CC00;"></asp:Label>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td style="text-align:right;">
                            <asp:Label ID="lblMsgBriefing" runat="server" style="color:Red; font-weight:bold;"></asp:Label>
                            <asp:Button ID="btnSaveBriefing" runat="server" Text=" Save " OnClick="btnSaveBriefing_OnClick" CssClass="btnstlnew" Width="60px" />
                        </td>
                    </tr>--%>
                 </table>
            </asp:Panel>            
        </div>
        <div runat="server" id="dvDebriefing">
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Debriefing</div>
            <table id="Table2" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation1" Font-Size="Large" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose1" Font-Size="Large" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod1" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay1" Font-Size="Large" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName1" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections1" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                  <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblNotifiedon1" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblTakeOverOn1" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                <tr>
                <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
                    <asp:Label runat="server" ID="lblRemarks1"></asp:Label>
                </td>
                </tr>
        </table>
            <asp:Panel runat="server" ID="pnlDeBriefing" style="padding:5px;">
               <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="font-weight:bold; font-size:12px;"> De-Briefing remarks by Suptd. </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDebriefingRemarks" runat="server" Width="100%" TextMode="MultiLine" Height="200px" ReadOnly="true" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;" >
                            <asp:Label ID="lblDebriefingByOn" runat="server" style="font-weight:bold;font-size:14px;color:#66CC00;"></asp:Label>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="text-align:right;">
                            <asp:Label ID="lblMsgDebriefing" runat="server" style="color:Red; font-weight:bold;"></asp:Label>
                            <asp:Button ID="btnSaveDebriefing" runat="server" Text=" Save " OnClick="btnSaveDebriefing_OnClick" CssClass="btnstlnew"  Width="60px"/>            
                        </td>
                    </tr>--%>
                 </table>
            </asp:Panel>
        </div>
        <div style="padding:5px; text-align:right;">
           <asp:Button ID="btnClose" Text="Close" CssClass="btn" style="background-color:Red" runat="server" OnClientClick="window.parent.HideFrame();"  Width="80px" />
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
