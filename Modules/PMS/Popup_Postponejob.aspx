<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Postponejob.aspx.cs" Inherits="Popup_Postponejob" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
      function refreshparent()
      {
         window.opener.refresh();
      }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >                     
                    Postpone Job&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:390px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>                       
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <asp:Panel ID="plPostpone" runat="server" >
                              <table border="0" cellpadding="4" cellspacing="0" style="width:100%;border-collapse:collapse;">
                               
                               <tr>
                                            <td style=" font-size :14px; color:blue; background-color :tan;">
                                                  <table border="0" cellpadding="4" cellspacing="0" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:left;width:130px;  font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblPostponeComponent" runat="server"></asp:Label> </td>
                                                            <td style="font-weight:bold" class="style2">Job :&nbsp;</td>
                                                            <td style="text-align:left;"><b>[ <asp:Label ID="lblPostponeInterval" ForeColor="Red" runat="server"></asp:Label>
                                                                &nbsp;]&nbsp; </b>
                                                                <asp:Label ID="lblPostponeJob" runat="server"></asp:Label></td>
                                                        </tr>
                                                 </table>
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td style="color:Red;font-weight:bold;">
                                                <center>
                                                    <asp:Label ID="lblCriticalMSG" runat="server" Text="Critical Component require Office Permission to Postpone the jobs .Please refer your SMS manual for guidance."></asp:Label> 
                                                </center>
                                            </td>
                                        </tr>
                               <tr>
                                 <td>
                                   <table cellpadding="2" cellspacing="0" style="width:100%;">
                                          <tr>
                                              <td style="text-align:right; width:200px;">Postpone Reason :&nbsp;</td>
                                              <td style="text-align:left"><asp:DropDownList ID="ddlPpReason" required='yes' runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPpReason_OnSelectedIndexChanged">
                                                 <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                 <asp:ListItem Text="Performance Based" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Waiting for spares" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="Dry docking" Value="3"></asp:ListItem>
                                                 <asp:ListItem Text="Suitable Opportunity not available" Value="4"></asp:ListItem>
                                                 
                                                 
                                                 </asp:DropDownList> 
                                              </td>
                                              <td style="text-align:right; width:120px;">Requested By :&nbsp;</td>
                                              <td style="text-align:left"><asp:DropDownList required='yes'  ID="ddlPRank" runat="server" ></asp:DropDownList></td>
                                          </tr>
                                          <tr>
                                          <td colspan="4">
                                            <asp:Label runat="server" ForeColor="Red" ID="lblmsg1" Font-Size="Larger" Text="Enter detailed condition report in Remarks." Font-Bold="true" Visible="false"></asp:Label>
                                          </td>
                                          </tr>
                                        <tr>
                                           <td style="text-align:right; width:200px;">Emp. No :&nbsp;</td>
                                           <td style="text-align:left"><asp:TextBox ID="txtPEmpCode" required='yes' MaxLength="6" Width="75px" runat="server" ></asp:TextBox></td>
                                           <td style="text-align:right; width:120px;">Emp. Name :&nbsp;</td>
                                           <td style="text-align:left"><asp:TextBox ID="txtPEmpname" required='yes' MaxLength="50" Width="235px" runat="server" ></asp:TextBox></td>
                                        </tr>
                                          <tr>
                                              <td style="text-align:right; width:200px;">Postpone Remarks :&nbsp;</td>
                                              <td colspan="3" style="text-align:left">
                                                   <asp:TextBox ID="txtPostponeRemarks" TextMode="MultiLine" required='yes' MaxLength="500" runat="server" Height="185px" Width="370px" ></asp:TextBox>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td style="text-align:right; width:200px;">Postpone Till Date :&nbsp;</td>
                                              <td style="text-align:left"><asp:TextBox ID="txtPostponedate" required='yes' onfocus="showCalendar('',this,this,'','holder1',5,-180,1)" MaxLength="11" Width="75px" runat="server" ></asp:TextBox></td>
                                              <td style="text-align:left"></td>
                                              <td style="text-align:left"></td>
                                             </tr>
                                             <tr>
                                                 <td colspan="3" style="text-align:left;">
                                                     <div style="padding:0px; float:left">
                                                         <uc1:MessageBox ID="mbPostPone" runat="server" />
                                                     </div>
                                                </td>
                                                 <td style=" float:right"><asp:Button ID="btnPostpone" CssClass="btnorange" Text="Save" OnClick="btnPostpone_Click" runat="server" /></td>
                                             </tr>
                                             
                                   </table>
                                 </td>
                            </tr>
                        </table> 
                            
                            </asp:Panel>
                          </td>
                        </tr>
                        </table>                       
                        </ContentTemplate>
                        </asp:UpdatePanel> 
                        </div>
                        </td>
                        </tr>
                    </table>
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
