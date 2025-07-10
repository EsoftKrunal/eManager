<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Modules_PMS_Dashboard" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <title>Planned Maintenance System : Office Home Page</title>
    <script language="javascript" type="text/javascript">

        function openreport(Mode, Param) {
            window.open('Reports/HomeReport.aspx?Mode=' + Mode + '&Days=' + Param, '', '');
        }

        function openVerifyreport(VessCode, Param) {
            window.open('JobDoneBetweenPeriod_OfficeVerify.aspx?VessCode=' + VessCode + '&Days=' + Param, '', '', '');
        }

    </script>
        <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="text headerband">
        Dashboard
    </div>
    <center>
<table cellpadding="10" cellspacing="0" width="100%">
          <tr>
            <td style="text-align:center; vertical-align:top; font-size:17px;">
                <table width="90%" cellspacing="2">
        <tr>
         <td style="text-align:right" ><b>Select Fleet : </b>&nbsp;&nbsp;</td>
         <td style="text-align:left;"><asp:DropDownList ID="ddlFleet" runat="server" Width="250px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList>
         </td>
         <td style="text-align:right"><b>Select Vessel : </b>&nbsp;&nbsp;</td>
         <td style="text-align:left;"><asp:DropDownList ID="ddlVessels" runat="server" Width="250px" AutoPostBack="true" onselectedindexchanged="ddlVessels_SelectedIndexChanged" ></asp:DropDownList>
         </td>
     </tr>
    <%--                    <tr>
         <td style="text-align:right" ><b>Jobs : </b>&nbsp;&nbsp;</td>
         <td style="text-align:left;"><asp:DropDownList ID="ddlTypesOfJobs" runat="server" Width="250px" AutoPostBack="true" onselectedindexchanged="ddlTypesOfJobs_SelectedIndexChanged" >
            <asp:ListItem Text="< Select Job >" Value="0"  Selected="True" ></asp:ListItem>
            <asp:ListItem Text="Critical" Value="1" ></asp:ListItem>
            <asp:ListItem Text="NON-Critical" Value="2" ></asp:ListItem>
         <asp:ListItem Text="Quick View of Maintenance" Value="3" ></asp:ListItem>
             </asp:DropDownList>
         </td>
         <td style="text-align:right"><b> </b>&nbsp;&nbsp;</td>
         <td style="text-align:left;">
         </td>
     </tr>--%>
        <tr>
        <td colspan="2" style="padding-left:5px;" >
            <br />
        <div style="width :98%;" id="divCritical" runat="server" >
        <div style="width:550px;border:solid 1px gray; background-color :darksalmon; font-size :14px; font-weight:bold; padding:3px;">Critical Jobs</div>
                <div style="width:550px;border:solid 1px gray;font-size :12px;padding:3px;text-align:left;">
                
                        <table width="95%" cellpadding="4" border="0">
                        <tr>
                            <td >&nbsp;</td>
                            <td >1.</td>
                            <td >
                                Due in Next&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtCritical_Due" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_Critical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkCritical_Due" runat="server" 
                                    onclick="lnkCritical_Due_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >2. </td>
                            <td >
                                Planned For Next&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtCritical_Plan" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_Critical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkCritical_Plan" runat="server" 
                                    onclick="lnkCritical_Plan_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                          <tr>
                            <td >&nbsp;</td>
                            <td >3. </td>
                            <td >
                                Jobs Done in Last&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtCritical_Done" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_Critical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                              <td style="text-align:right; width:70px">
                                  <asp:LinkButton ID="lnkCritical_Done" runat="server" 
                                      onclick="lnkCritical_Done_Click" Text=""></asp:LinkButton>
                              </td>
                        </tr>
                            <tr>
                            <td >&nbsp;</td>
                            <td >4. </td>
                            <td >
                                OverDue&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                </td>
                              <td style="text-align:right; width:70px">
                                  <asp:LinkButton ID="lnkCritical_Overdue" runat="server" 
                                      onclick="lnkCritical_Overdue_Click" Text=""></asp:LinkButton>
                              </td>
                        </tr>
                        </table>
               
                </div>
        </div>
        <%-- <br /> <br />--%>
        <div style="width :98%" id="divMaintenance" runat="server" visible="false">
                <div style="width:550px;border:solid 1px gray; background-color : #2EFE64; font-size :14px; font-weight:bold; padding:3px">Quick View of Maintenance Jobs</div>
                <div style="width:550px;border:solid 1px gray; font-size :13px;padding:3px;text-align:left;">
                <table width="95%"  cellpadding="4">
                        <tr>
                            <td style="width:5px;">&nbsp;</td>
                            <td style="width:10px;">1. </td>
                            <td>Total Jobs done in last 
                            
                             </td>
                              <td style="text-align:left; width:100px">
                            <asp:TextBox ID="txtVerifyDays" style="text-align:center" runat="server" MaxLength="3" Width="30px" Text="30" OnTextChanged="Update_Verify" AutoPostBack="true"></asp:TextBox> &nbsp;Days  
                              </td>
                           <td style="text-align:right;width:70px; padding-right:0px;">
                                <asp:LinkButton ID="lnkUnVerified" Text="0" runat="server" OnClick="lnkUnVerified_OnClick" ></asp:LinkButton>
                           </td>
                        </tr>
                        
                        </table>
                </div>
         </div> 
            <br />
        <div style="width :98%" id="divNonCritical" runat="server" >
            <div style="width:550px;border:solid 1px gray; background-color : cadetblue; font-size :14px; font-weight:bold; padding:3px">Other Jobs</div>
                <div style="width:550px;border:solid 1px gray; font-size :12px;padding:3px;text-align:left;">
                <table width="95%" cellpadding="4" border="0">
                        <tr>
                            <td >&nbsp;</td>
                            <td >1.</td>
                            <td >
                                Due for Next&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_Due" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_NonCritical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkNoNCritical_Due" runat="server" 
                                    onclick="lnkNoNCritical_Due_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >2.</td>
                            <td >
                                OverDue </td>
                            <td style="text-align:left; width:100px">&nbsp;</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkNoNCritical_OverDue" runat="server" 
                                    onclick="lnkNoNCritical_OverDue_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >3. </td>
                            <td >
                                Planned For Next&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_Plan" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_NonCritical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkNoNCritical_Plan" runat="server" 
                                    onclick="lnkNoNCritical_Plan_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                         <tr>
                            <td >&nbsp;</td>
                            <td >4. </td>
                            <td >
                                Postponed in Last&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_Postponed" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_NonCritical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                             <td style="text-align:right; width:70px">
                                 <asp:LinkButton ID="lnkNoNCritical_Postponed" runat="server" 
                                     onclick="lnkNoNCritical_Postponed_Click" Text=""></asp:LinkButton>
                             </td>
                        </tr>
                          <tr runat="server" visible="false" >
                            <td >&nbsp;</td>
                            <td >5. </td>
                            <td >
                                Breakdown &amp; UnPlanned Jobs in Last&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_B_UP" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_NonCritical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                              <td style="text-align:right; width:70px">
                                  <asp:LinkButton ID="lnkNoNCritical_B_UP" runat="server" 
                                      onclick="lnkNoNCritical_B_UP_Click" Text=""></asp:LinkButton>
                              </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >5. </td>
                            <td >
                                Jobs Done in Last&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_Done" runat="server" AutoPostBack="true" 
                                    MaxLength="3" OnTextChanged="Update_NonCritical" style="text-align:center" 
                                    Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkNoNCritical_Done" runat="server" 
                                    onclick="lnkNoNCritical_Done_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >6. </td>
                            <td >
                                Jobs Done after DueDate in Last&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_DoneAfterDue" runat="server" 
                                    AutoPostBack="true" MaxLength="3" OnTextChanged="Update_NonCritical" 
                                    style="text-align:center" Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkNoNCritical_DoneAfterDue" runat="server" 
                                    onclick="lnkNoNCritical_DoneAfterDue_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >7. </td>
                            <td >
                                Defect Jobs Due in Next&nbsp;
                            </td>
                            <td style="text-align:left; width:100px">
                                <asp:TextBox ID="txtNoNCritical_DefectDue" runat="server" 
                                    AutoPostBack="true" MaxLength="3" OnTextChanged="Update_NonCritical" 
                                    style="text-align:center" Text="30" Width="30px"></asp:TextBox>
                                &nbsp;Days</td>
                            <td style="text-align:right; width:70px">
                                <asp:LinkButton ID="lnkNoNCritical_DefectDue" runat="server" 
                                    onclick="lnkNoNCritical_DefectDue_Click" Text=""></asp:LinkButton>
                            </td>
                        </tr>
                        </table>
                </div>
        </div>
        </td>
        <td colspan="2" >
            <br />
            <div id="divChart" runat="server" visible="false" >
                 <asp:Chart ID="Chart_HSQE009" runat="server" Width="500px" Height="400px" BackColor="#ecf0f5" >
            <Series>
               <%-- <asp:Series Name="New" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" Color="Orange"></asp:Series>--%>
                <asp:Series Name="% Outstanding" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" Color="darkgoldenrod"    ></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackColor="#ecf0f5"  >
                <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineDashStyle="NotSet" />
                    <LabelStyle Angle="-90" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
            </div> 
        </td>
     </tr>
     </table>
            </td>
            <td style="padding:5px">
            <div style="background-color:#206020; width:5px ; height:450px;">
            </div>
            </td>
            <td style="text-align:center; vertical-align:top; font-size:17px; width:300px;">
                <table cellpadding="5" cellspacing="5" width="100%"  border="0">
                <tr runat="server" id="tr_VIQ">
                <td style="text-align:left; width:10px;" ><img src="../HRD/Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                   <a href="OfficeVerifyJobs.aspx" target="_blank">Job Verification</a>
                </td>
                </tr>
                <tr runat="server" id="tr_VPR">
                <td style="text-align:left; " ><img src="../HRD/Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    <a href="OfficeVerifyJobs_Postpone.aspx" target="_blank">Postpone Request</a>
                </td>
                </tr>
                <tr runat="server" id="tr_VP">
                <td style="text-align:left; " ><img src="../HRD/Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    <a href="CriticalComponentShutdownRequest.aspx" target="_blank" >Critical component shutdown request</a>
                </td>
                </tr>
                <tr runat="server" id="tr_VR" visible="false">
                <td style="text-align:left" ><img src="../HRD/Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    <a href="DefectswithoutOfficeComments.aspx" target="_blank">Defect without office comments</a>
                    </td>
                </tr>
               
                </table>
            </td>
         </tr>
</table>
</center> 
</asp:Content>

