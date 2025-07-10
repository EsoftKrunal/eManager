<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office_Home.aspx.cs" Inherits="Office_Home" MasterPageFile="~/MasterPage.master" %>
<%--<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <%--<tr><td>
        <hm:HMenu runat="server" ID="menu2" />
        </td></tr>--%>
            <tr>
                <td  class="text headerband">
                    Dashboard
                </td>
            </tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top; padding-left:20px;" > 
        <div style="width:99%; height:452px; border:#4371a5 1px solid;  overflow:auto; overflow-y:hidden ; vertical-align:top; background-color:#fafafa" >
   <%-- <center>--%>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="up1">
    <ProgressTemplate>
    <%--<center>
    <div>
    <img src="Images/loading1.gif" alt="Loading.." style="position:relative;top:0px; " />
    </div>
    </center>--%>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
  <%--  <center >--%>
    <table width="85%" style="font-size:17px; text-align:center; ">
    <tr>
        <td style="width:25%;"> 
            <div onclick="window.open('OfficeVerifyJobs.aspx');" style="cursor:pointer;color:white; padding:10px; background-color:#33ccff;  margin:0 auto;border:solid 1px #f1f1f1"><asp:Label runat="server" ID="lblVCount"></asp:Label></div>
        </td>
        <td style="width:25%"> 
            <div onclick="window.open('OfficeVerifyJobs_Postpone.aspx');"  style="cursor:pointer;color:white; padding:10px; background-color:#df80ff;  margin:0 auto;border:solid 1px #f1f1f1"><asp:Label runat="server" ID="lblPCount"></asp:Label></div>
        </td>
        <td style="width:25%"> 
            <div onclick="window.open('CriticalComponentShutdownRequest.aspx');" style="cursor:pointer;color:white; padding:10px; background-color:#ffad33;  margin:0 auto;border:solid 1px #f1f1f1">
                <asp:Label ID="lblCriticalComponentShutdownRequest" runat="server" ></asp:Label>
            </div>
        </td>
        <td>
            <div  onclick="window.open('DefectswithoutOfficeComments.aspx');" style="cursor:pointer;color:white; padding:10px; background-color:rgba(179, 167, 0, 1);  margin:0 auto;border:solid 1px #f1f1f1">
            <asp:Label ID="lblDefectsWithoutOfficeComments" runat="server" ></asp:Label>
            </div>
        </td>
    </tr>
    </table>
 <%--   </center>--%>
    <div style="padding-left:10px;text-align:center;">
    <table width="85%" cellspacing="20">
        <tr>
         <td style="text-align:right" ><b>Select Fleet : </b>&nbsp;&nbsp;</td>
         <td style="text-align:left;"><asp:DropDownList ID="ddlFleet" runat="server" Width="250px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList>
         </td>
         <td style="text-align:right"><b>Select Vessel : </b>&nbsp;&nbsp;</td>
         <td style="text-align:left;"><asp:DropDownList ID="ddlVessels" runat="server" Width="250px" AutoPostBack="true" onselectedindexchanged="ddlVessels_SelectedIndexChanged" ></asp:DropDownList>
         </td>
     </tr>
        <tr>
        <td colspan="2">
        <div style="width :98%">
        <div style="width:550px;border:solid 1px gray; background-color : #FA5882; font-size :14px; font-weight:bold; padding:3px">Critical Jobs</div>
                <div style="width:550px;border:solid 1px gray; text-align:left;font-size :12px;padding:3px">
                
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
                        </table>
               
                </div>
        </div>
         <br /> <br />
        <div style="width :98%">
                <div style="width:550px;border:solid 1px gray; background-color : #2EFE64; font-size :14px; font-weight:bold; padding:3px">Quick View of Maintenance Jobs</div>
                <div style="width:550px;border:solid 1px gray; text-align:left;font-size :13px;padding:3px">
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
        </td>
        <td colspan="2">  
           <div style="width:550px;border:solid 1px gray; background-color : #2EFE64; font-size :14px; font-weight:bold; padding:3px">NON-Critical Jobs</div>
                <div style="width:550px;border:solid 1px gray; text-align:left;font-size :12px;padding:3px">
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
        </td>
     </tr>
     </table> 
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
 <%--   </center> --%>
    </div>
    </td> 
    </tr>
    </table>
     </div>

    </asp:Content>
