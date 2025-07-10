<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Peap_EmpTrainingNeed.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Peap_EmpTrainingNeed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Need</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function refreshparent() {
            window.opener.RefreshRemark();
        }
     
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
        <div style="border:1px solid #9abcd7;">
        <table cellpadding="2" cellspacing="0" style="width: 100%;background-color: #f9f9f9">
        <tr>
            <td style="background-color: #4371a5; text-align: center; height: 23px; font-size: 15px;" cssclass="text">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <colgroup>
                        <col width="310px" />
                        <col />
                        <col width="200px" />
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="font-size: 16px; font-weight: bold; color: White; text-align: center; font-family:Verdana;">
                                Identify Training Needs
                            </td>
                            <td style="font-size: 10px; color: White; vertical-align: top;">
                                &nbsp;
                            </td>
                        </tr>
                    </colgroup>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding: 10px">
                <span style="font-size: Large; font-weight: bold; color: #336699;">[
                    <asp:Label ID="txtFirstName" runat="server" Style="font-size: Large; font-weight: bold;"
                        CssClass="input_box"></asp:Label>
                    <asp:Label ID="txtLastName" runat="server" Style="font-size: Large; font-weight: bold;"
                        CssClass="input_box"></asp:Label>
                    &nbsp;/
                    <asp:Label ID="lblPeapLevel" runat="server" Style="font-size: Large; font-weight: bold;"
                        CssClass="input_box"></asp:Label>
                    &nbsp;] </span><span style="font-size: Large; font-weight: bold; color: #6600CC;">(
                        <asp:Label ID="txtOccasion" Style="font-size: Large; font-weight: bold;" runat="server"
                            CssClass="input_box"></asp:Label>
                        &nbsp;) </span><span style="font-size: Large; font-weight: bold; color: #0000FF;">
                            <asp:Label ID="lblAppraiserName" Style="font-size: Large; font-weight: bold;" runat="server"
                                CssClass="input_box"></asp:Label>
                        </span>
                <br />
                <div style="padding: 5px; background-color: #f2f2f2;">
                    <asp:Label ID="lblPeapStatus" runat="server" CssClass="input_box" Font-Bold="True"
                        Font-Size="Large" ForeColor="#993300"></asp:Label>
                </div>
                <hr />
            </td>
        </tr>
        <tr>
           <td>
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                   <tr>
                       <td style="text-align:center; background-color:#d2d2d2; font-weight:bold; font-family:Verdana; font-style:italic; font-size: 16px; ">
                             Training List
                       </td>
                   </tr>
                   <tr>
                       <td>
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:90%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:25px;" />                                     
                                    <col  />
                                    <%--<col style="width:110px;" />--%>
                                    <col style="width:90px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader" >                                        
                                        <td></td>
                                        <td style="text-align:left">Training Title</td>
                                        <%--<td>Last Done Date</td>--%>
                                        <td>Due Date</td>
                                        <td></td>
                                    </tr>
                                </colgroup>
                            </table>
                        <div id="Div2" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px; text-align:left;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:90%;border-collapse:collapse;">
                               <colgroup>
                                    <col style="width:25px;" />                                    
                                    <col  />
                                    <col style="width:90px;" />
                                    <col style="width:17px;" />
                               </colgroup>
                                <asp:Repeater ID="rptPlannedTrainings" runat="server">
                                    <ItemTemplate>                                          
                                          <tr class="row" >
                                          <td colspan="4">
                                              <table cellpadding="2" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="left" colspan="4">
                                                    <asp:Label ID="lblGroup" Text='<%#Eval("TrainingGroupName")%>' Font-Bold="true" runat="server"></asp:Label>
                                                   </td>
                                                </tr>
                                                <tr>
                                                   <td align="left" style="width:30px">
                                                        <asp:CheckBox ID="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" />
                                                        <asp:HiddenField ID="hfTrainingId" Value='<%#Eval("TrainingId")%>' runat="server" />
                                                    </td>
                                                    <td style="text-align:left">
                                                        <%--<a href='#' title="View Training Details" onclick='OpenPlanningDetails("<%#Eval("TrainingPlanningId").ToString() %> ")'><%#Eval("TrainingName")%></a>--%>
                                                        <%#Eval("TrainingName")%>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox ID="txtDueDt"  MaxLength="12" Width="80px" Enabled="false" runat="server" ></asp:TextBox >
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDueDt" TargetControlID="txtDueDt" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                                    </td>
                                            
                                                </tr>
                                              </table>
                                          </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                       
                       </td>
                   </tr>
                   <tr>
                      <td style="text-align:right; padding-right:5px;">
                        <asp:Label ID="lblMsg" style="color:Red;" runat="server"></asp:Label> 
                         <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" onclick="btnSave_Click" />
                         <input type="button" value="Close" class="btn"  onclick="javascript:self.close();" />
                         
                      </td>
                   </tr>
                </table>
           </td>
        </tr>
    </table>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
