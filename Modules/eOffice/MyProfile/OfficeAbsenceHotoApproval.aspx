<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceHotoApproval.aspx.cs" Inherits="Emtm_OfficeAbsenceHotoApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Handover Note</title> 
    <link href="../style.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:250px;">
        
        <div style="text-align:left; background-color: white; padding:5px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" >
            <tr>
                <td style="background-color: #5C8AE6; height: 30px; font-size: 14px; font-weight: bold; text-align: center; color: White">
                    &nbsp;<asp:Label ID="lblTitle" Font-Size="14px" runat="server" Text="Handover Note "></asp:Label>
                </td>
            </tr>
        </table> 
        <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="all">
        <tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Purpose :
            </td>
            <td><asp:Label ID="lblPurpose" runat="server"></asp:Label>
            </td>
            <td style="text-align: right; ">Planned Period :</td>
            <td style="text-align: left; "><asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                    
            </td>
          
        </tr>
        <tr>
            <td style="text-align: right;"> Handover To: </td>
            <td>
                <asp:Label ID="ddlHandover" runat="server"  ></asp:Label>
            </td>
            <td style="text-align: right;"> Backup Handover To: </td>
            <td>
                <asp:Label ID="ddlBackup" runat="server"  ></asp:Label>
            </td>
        </tr>
        </table>

        <div style="width:100%; text-align:center; padding:4px;">
            <asp:Label ID="lblMsgStatus" style="font-weight:bold; color:Blue"  runat="server" ></asp:Label>
        </div>
        </div>
        
        <div style="text-align:left; background-color: white; height:445px;">
        <asp:Panel runat="server" ID="pnlHandover" Visible="true" style="padding:5px;">
                <div style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 29px; text-align: center;">
                    <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                    <col width="20px" />
                    <col  />
                    <tr style=" background-color:#5C8AE6; height:25px; color:White; font-weight:bold;" >
                        <td></td>
                        <td>Handover List</td>
                    </tr>
                </table>
                </div>
                <div runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 385px; text-align: center;border-bottom:solid 1px gray;">
                <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows" style="border-collapse:collapse;border:none;text-align:left; border-color:gray;">
                    <col width="20px" />
                    <col  />
                    <asp:Repeater ID="rptHotoDetails" runat="server">
                            
                        <ItemTemplate>
                            <tr id="Tr1" runat="server" visible='<%#CompareVSl(Eval("VESSELCODE"))%>'>
                                <td colspan="3" style="background-color:#FFEBCC">
                                    <img src="../../Images/arrow_blue.gif" style="float:left; margin-top:3px;" /> <span style="font-size:13px; color:Black; font-weight:bold;"><%#Eval("VESSELCODE")%></span>
                                </td>
                            </tr>
                            <tr >
                                <td style="text-align:center">
                                     <asp:ImageButton ID="btnView" OnClick="btnViewFleetNotes_OnClick" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass1.gif" CommandArgument='<%#Eval("TID")%>' />   
                                </td>
                                <td> 
                                
                                <div style=" height:15px; overflow:hidden;"> 
                                       <span style="color:Red"> ( <%#Common.ToDateString(Eval("CreatedOn"))%> )</span>
                                       <span style="color:Blue"><i><%#Eval("CreatedByName")%></i></span>
                                       <span><%#Eval("TOPICDETAILS")%></span>
                                    </div>
                                
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
        </asp:Panel>
           
        </div>
        
       
        </td>
        </tr>
        </table>


        <div style="width:100%; text-align:center;">
            <asp:Button ID="btnApprove" runat="server" Text=" Accept " OnClick="btnApprove_Onclick" CssClass="btnstlnew"  OnClientClick="return confirm('Click OK to Accept?');" style="padding:3px;margin:2px;background-color:#088A68;font-weight:bold;border-color:#21610B;"/>
            <asp:Button ID="btnReject" runat="server" Text=" Reject " OnClick="btnReject_Onclick" CssClass="btnstlnew"   OnClientClick="return confirm('Are you sure to Reject?');" style="padding:3px;margin:2px;background-color:#F78181;font-weight:bold;border-color:#FA5858;"/>
        </div>
        <div style="width:100%; text-align:center;">
            <asp:Label ID="lblMsg" runat="server" Text="" style="color:Red;font-weight:bold;margin:2px;"></asp:Label>
        </div>

          <%--------------  Show Notes  ------------  --%>

        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvViewNotes" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:400px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:150;top:80px;opacity:1;filter:alpha(opacity=100)">
             <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col width="150px" />  <col  />
                        <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold;height:30px;">
                            <td  style="text-align:left">
                                
                            </td> 
                            <td>
                                
                            </td>
                            <td>
                            <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/delete_button.jpg" ID="ImageButton2" OnClick="btnCloseViewFleetPopup_OnClick"  style="float :right;" ToolTip="Close this Window."/>
                            </td>
                        </tr>
            </table>
            <asp:TextBox ID="txtViewComments" runat="server" TextMode="MultiLine"  Width="100%" Height="354px" ></asp:TextBox>
            </div>
            </center>
        </div>

         </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
