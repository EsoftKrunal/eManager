<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobUpdateHistory.aspx.cs" Inherits="JobUpdateHistory" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>eMANAGER</title>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
       function opendetails(RP,HID,VC,UT)
       {
         if(RP == 'REPORT')
         {
             RP = 'R';
             UT = '<%=UserType%>';
             if (UT == "S")
                                            {
                 window.open('JobCard.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
             }
             else {
                 window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
             }
         }
         if(RP == 'POSTPONE')
         {
            RP = 'P';
            window.open('PopupHistoryJobDetails.aspx?VC='+ VC +'&&HID='+ HID + '&&RP='+ RP,'','');
         }
          
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                          <tr style=" padding-top:2px;">
                            <td colspan="2" style="padding-right: 5px; padding-left: 2px;">
                            
                            <DIV style="width:100%; height:425px; border:0px solid #000;  overflow:auto; overflow-y:hidden" ><%--height:460px;--%>
                            
                    <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <tr class= "text headerband">
                                       <td >Job History</td>
                                    </tr>
                                     <tr>
                            <td style=" font-size :14px; color:blue; background-color :tan;">
                               <b>Vessel </b> - <asp:Label runat="server" ID="lblJhVessel"></asp:Label>
                            </td>
                            </tr>
                                    <tr>
                                            <td style=" font-size :14px; color:blue; background-color :tan;">
                                                  <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:left;width:100px;  font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblJhComponent" runat="server"></asp:Label> </td>
                                                            <td style="font-weight:bold" class="style2">Job :&nbsp;</td>
                                                            <td style="text-align:left;"><b>[ <asp:Label ID="lblJhInterval" ForeColor="Red" runat="server"></asp:Label>
                                                                &nbsp;]&nbsp; </b>
                                                                <asp:Label ID="lblJhJob" runat="server"></asp:Label></td>
                                                        </tr>
                                                 </table>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                   <colgroup>
                                                       <col style="width:90px;" />
                                                       <col style="width:90px;" />
                                                       <col style="width:90px;" />
                                                       <col style="width:80px;" />
                                                       <col style="width:80px;" />
                                                       <col style="width:80px;" />
                                                       <col />
                                                       <col style="width:70px;" />
                                                       <col style="width:250px;" />
                                                       <col style="width:17px;" />
                                                   </colgroup>
                                                   <tr align="left" class= "headerstylegrid">
                                                       <td>Last Due Date</td>
                                                       <td>Done Date</td>
                                                       <td>Next Due Dt.</td>
                                                       <td>Due Hr.</td>
                                                       <td>Done Hr.</td>
                                                       <td>Done By</td>
                                                       <td>Crew Name</td>
                                                       <td>Action</td>
                                                       <td>Equip. Condn</td>                                                  
                                                       <td></td>
                                                   </tr>
                                           </table>
                                           
                                           <div id="dvHistory" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 325px ; text-align:center;">
                                        <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                           <colgroup>
                                                   <col style="width:90px;" />
                                                   <col style="width:90px;" />
                                                   <col style="width:90px;" />
                                                   <col style="width:80px;" />
                                                   <col style="width:80px;" />
                                                   <col style="width:80px;" />
                                                   <col />
                                                   <col style="width:70px;" />
                                                   <col style="width:250px;" />
                                                   <col style="width:17px;" />
                                           </colgroup>
                                           <asp:Repeater ID="rptJobHistory" runat="server">
                                              <ItemTemplate>
                                                  <tr title="Click to view details" onclick="opendetails('<%#Eval("Action")%>','<%#Eval("HistoryId")%>','<%#Eval("VesselCode")%>');" class="row" style="cursor:pointer">
                                                       <td align="left"><%#Eval("DueDate")%></td>
                                                       <td align="left"><%#Eval("ACTIONDATE")%></td>
                                                       <td align="left"><%#Common.ToDateString(Eval("NextDueDate"))%></td>
                                                       <td align="center"><%# Eval("DueHour").ToString() == "0" ? "" : Eval("DueHour").ToString()%></td>
                                                       <td align="center"><%# Eval("DoneHour").ToString() == "0" ? "" : Eval("DoneHour").ToString()%></td>
                                                       <td align="left" style=" text-align:center"><%#Eval("DoneBy")%></td>
                                                       <td align="left"><%#Eval("EmpNo")%>-<%#Eval("EmpName")%></td>
                                                       <td align="left"><%#Eval("Action")%></td>
                                                       <td align="left"><%#Eval("ConditionAfter")%></td>
                                                       <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                   </tr>
                                               </ItemTemplate>
                                              </asp:Repeater>
                                          </table>
                                       </div>
                                            
                                        </td>
                                    </tr>
                        <tr>
                        <td>
                        <table>
                        <tr>
                        <td style="width:950px; text-align:left;">
                             <div style="padding:0px; float:left">
                                 <uc1:MessageBox ID="MessageBox1" runat="server" />
                             </div>
                        </td>
                        
                        <td align="right" style=" padding-top:3px; padding-right:2px; padding-bottom:3px; width:205px; text-align:right">
                            
                          
                                </td>
                                </tr></table>
                                </td>
                                </tr>
                        </table> 
                                </DIV>
                            
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
