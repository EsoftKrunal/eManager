<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipHome.aspx.cs" Inherits="ShipHome" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>Planned Maintenance System : Ship Home Page</title>
    <script language="javascript" type="text/javascript">

        function openreport(Mode, Param) {
            window.open('Reports/HomeReport.aspx?Mode=' + Mode + '&Days=' + Param, '', '');
        }

        function OpenAttachment() {
            window.open('PMSDocuments.aspx','');
        }
        function OpenPostPone() {
            window.open('PostPoneJobs.aspx','');
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr><td>
        <hm:HMenu runat="server" ID="menu2" />
        </td></tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
        <div style="width:99%; height:452px; border:#4371a5 1px solid;  overflow:auto; overflow-y:hidden" >
    <center>
     <h1> &nbsp;</h1>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
    <ProgressTemplate>
    <center>
    <div>
    <img src="Images/loading1.gif" alt="Loading.." style="position:relative;top:0px; " />
    </div>
    </center>
    </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdatePanel runat="server" ID="up1">
     <ContentTemplate>
     <table width="95%" align="center" cellspacing="20">
     <tr>
        <td>
         <div style="width :98%;">
                <div style="width:550px;border:solid 1px gray; background-color : #FA5882; font-size :14px; font-weight:bold; padding:3px">Critical Jobs</div>
                <div style="width:550px;border:solid 1px gray; text-align:left;font-size :12px;padding:3px">
               
                        <table width="95%" cellpadding="4" border="0">
                        <tr>
                            <td >&nbsp;</td>
                            <td >1.</td>
                            <td >
                                Due in Next&nbsp;
                            </td>
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
                <br /> <br />
                <div style="width:550px;border:solid 1px gray; background-color : #A9D0F5; font-size :14px; font-weight:bold; padding:3px">Jobs Monitoring</div>
                <div style="width:550px;border:solid 1px gray; text-align:left;font-size :13px;padding:3px">
                        <table width="95%" cellpadding="4" border="0">
                        <tr>
                            <td >&nbsp;</td>
                            <td >1. </td>
                            <td >
                                Jobs Pending for Verification</td>
                                <td style="text-align:right; width:70px">
                                
                                <asp:LinkButton ID="lnk_Verify" Text="" runat="server" onclick="lnk_Verify_Click"></asp:LinkButton>
                                </td>
                        </tr>    
                        <tr>
                            <td >&nbsp;</td>
                            <td >2. </td>
                            <td >Export PMS Documents to Office</td>
                                <td style="text-align:right; width:70px">
                                    <asp:LinkButton ID="lnk_Documents" Text="" runat="server" onclick="lnk_Documents_Click"></asp:LinkButton>
                                </td>
                        </tr>                  
                        <tr>
                            <td >&nbsp;</td>
                            <td >3. </td>
                            <td >Postpone Request pending for approval</td>
                                <td style="text-align:right; width:70px">
                                    <asp:LinkButton ID="lnk_Postpone" Text="" runat="server" onclick="lnk_Postpone_Click"></asp:LinkButton>
                                </td>
                        </tr>
                         <tr>
                            <td >&nbsp;</td>
                            <td >4. </td>
                            <td >Critical component shut down request</td>
                                <td style="text-align:right; width:70px">
                                    <asp:LinkButton ID="lnk_CriticalShutdown" Text="" runat="server" onclick="lnk_CriticalShutdown_Click"></asp:LinkButton>
                                </td>
                        </tr>
                        
                        </table>
                </div>
         </div>
        </td>
         <td>  
         <div style="width :98%">
                <div style="width:550px;border:solid 1px gray; background-color : #2EFE64; font-size :14px; font-weight:bold; padding:3px">NON-Critical Jobs</div>
                <div style="width:550px;border:solid 1px gray; text-align:left;font-size :12px;padding:3px">
                        <table width="95%" cellpadding="4" border="0">
                        <tr>
                            <td >&nbsp;</td>
                            <td >1.</td>
                            <td >
                                Due for Next&nbsp;
                            </td>
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">&nbsp;</td>
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
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
                            <td style="text-align:left; width:80px">
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
     </tr>
     </table> 
     </ContentTemplate>
    </asp:UpdatePanel>
    </center> 
    </div>
    </td> 
    </tr>
    </table>
     </div>
    <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
