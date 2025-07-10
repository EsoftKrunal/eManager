<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceHotoViewOnly.aspx.cs" Inherits="Emtm_OfficeAbsenceHotoViewOnly" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Handover Note</title> 
    <link href="../style.css" rel="stylesheet" type="text/css" />
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
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%" >
            <tr>
                <td style="background-color: #4371a5; height: 30px; font-size: 14px; font-weight: bold; text-align: center; color: White">
                    &nbsp;<asp:Label ID="lblTitle" Font-Size="14px" runat="server" Text="Handover / TakeOver "></asp:Label>
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
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Travel Start Date :
            </td>
            <td>
                <asp:Label ID="txtActLeaveFrom" runat="server" MaxLength="11" Width="90px" ></asp:Label>
                <asp:Label ID="ddlEtHr" runat="server"  style="font-weight:bold;"></asp:Label>  &nbsp;(Hrs.)&nbsp;
                <asp:Label ID="ddlEtMin" runat="server"  style="font-weight:bold;"></asp:Label>&nbsp;(Min.)&nbsp;
            </td>
            <td style="text-align: right; ">Travel End Date : </td>
            <td style="text-align: left;">
                <asp:Label ID="txtEndDt" runat="server" MaxLength="11" Width="90px" ></asp:Label>
                <asp:Label ID="ddlEndHr" runat="server" style="font-weight:bold;" ></asp:Label>
                <asp:Label ID="ddlEndMin" runat="server"  style="font-weight:bold;"></asp:Label>
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

        

        <br />
        <div style="text-align:left;">
            <asp:Button runat="server" Text="Handover" ID="btnCash" onclick="btnHandover_Click"  CssClass="btn11sel" />
            <asp:Button runat="server" Text="TakeOver" ID="btnExp" onclick="btnTakeover_Click"  CssClass="btn11" />
        </div>
        <div style="text-align:left; background-color:#C2E0FF; height:450px; border:solid 1px black;border-top:solid 0px black; padding:10px 5px 5px 5px;">
            
            <div style="text-align:left; background-color: white; height:445px;">
            <asp:Panel runat="server" ID="pnlHandover" Visible="true" style="padding:5px;">
            <div style="background-color:White; padding-bottom:3px;">
                
            </div>
            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 29px; text-align: center;">
                  <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col  />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="100px" /> 
                        <tr style=" background-color:#5C8AE6; height:25px; color:White; font-weight:bold;" >
                            <td>Handover List</td>
                            <td>Due Date</td>
                            <td>Closed On</td>
                            <td>Status</td>
                        </tr>
                   </table>
                   </div>
            <div runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 340px; text-align: center;">
                    <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows" style="border-collapse:collapse;border:none;text-align:left; border-color:gray;">
                        <col  />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="100px" /> 
                        <asp:Repeater ID="rptHotoDetails" runat="server">
                            
                            <ItemTemplate>
                                <tr id="Tr1" runat="server" visible='<%#CompareVSl(Eval("VESSELCODE"))%>'>
                                    <td colspan="4" style="background-color:#FFEBCC">
                                       <img src="../../Images/arrow_blue.gif" style="float:left; margin-top:3px;" /> <span style="font-size:13px; color:Black; font-weight:bold;"><%#Eval("VESSELCODE")%></span>
                                    </td>
                                </tr>
                                <tr >
                                    <td> &nbsp;&nbsp;&nbsp; <%#Eval("TOPICDETAILS")%></td>
                                    <td style="text-align:center"> &nbsp; <%#Common.ToDateString(Eval("DueDate"))%></td>
                                    <td style="text-align:center"> &nbsp; <%# Common.ToDateString( Eval("ClosedOn"))%></td>
                                    <td style="text-align:center"><asp:Label ID="lblClosed" runat="server"  Visible='<%# (Eval("ClosedBy").ToString()!="") %>' Text=" Closed"></asp:Label></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                   </div>
                   <hr />
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align:left">
            <col width="230px" /> <col  /> 
            <tr>
                <td >
                </td>
                <td align="left">
                   <b> &nbsp; <asp:Label ID="lblUMsg" ForeColor="Red" runat="server" Font-Size="Large"  ></asp:Label></b>
                </td>
            </tr>
            </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlTakeover" Visible="false" style="padding:5px;">
            <div style="background-color:White; padding-bottom:3px;">
                    
                </div>
            <div id="Div2" runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;text-align: center;">
                      <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col  />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="100px" /> 
                        <tr style=" background-color:#5C8AE6; height:25px; color:White; font-weight:bold;" >
                            <td>Handover List</td>
                            <td>Due Date</td>
                            <td>Closed On</td>
                            <td>Status</td>
                        </tr>
                   </table>
                </div>
            <div id="Div1" runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 340px; text-align: center;">
                    <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows" style="border-collapse:collapse;border:none;text-align:left;">
                        <col  />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="100px" />
                        <asp:Repeater ID="rptTakeover" runat="server">
                              <ItemTemplate>
                                <tr id="Tr1" runat="server" visible='<%#CompareVSl(Eval("VESSELCODE"))%>'>
                                    <td colspan="4" style="background-color:#FFEBCC">
                                       <img src="../../Images/arrow_blue.gif" style="float:left; margin-top:3px;" /> <span style="font-size:13px; color:Black; font-weight:bold;"><%#Eval("VESSELCODE")%></span>
                                    </td>
                                </tr>
                                <tr >
                                    <td> &nbsp;&nbsp;&nbsp; <%#Eval("TOPICDETAILS")%></td>
                                    <td style="text-align:center"> &nbsp; <%#Common.ToDateString(Eval("DueDate"))%></td>
                                    <td style="text-align:center"> &nbsp; <%# Common.ToDateString( Eval("ClosedOn"))%></td>
                                    <td style="text-align:center">                                        
                                        <asp:Label ID="lblClosed" runat="server"  Visible='<%# (Eval("ClosedBy").ToString()!="") %>' Text=" Closed"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
            <hr />
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align:left">
            <col width="230px" /> <col  /> 
            <tr>
                <td  >

                </td>
                <td align="left">
                   <b> &nbsp; <asp:Label ID="lblUMsg1" ForeColor="Red" runat="server" Font-Size="Large"  ></asp:Label></b>
                </td>
            </tr>
            </table>
            </asp:Panel>
            </div>
        </div>

      
        </td>
        </tr>
        </table>


        
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
