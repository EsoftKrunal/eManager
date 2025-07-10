<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReliefDueForNextSpecifiedDays.aspx.cs" Inherits="Reporting_ReliefDueForNextSpecifiedDays" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <style type="text/css" >  
    .fixedbar
    {
        position:fixed;
        margin:73px 0px 0px 135px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style> 
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show">
<div>
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
           <tr>
           <td align="center" class="text headerband" style="width: 100%; ">
            Relief Due Days Wise&nbsp;</td>
           </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
             </tr>
             <tr>
                 <td >
                     <table cellpadding="0" cellspacing="0" style="width: 100%">
                       <tr>
                                                 <td style="text-align:right" class="style6">
                                                     &nbsp;</td>
                                                 <td style="text-align:right" class="style7">
                                                    Fleet :
                                                 </td>
                                                 <td style="width: 286px; height: 13px; text-align :left">
                                                      <asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" 
                                   TabIndex="15" Width="90px" AutoPostBack="True" 
                                                          onselectedindexchanged="ddlFleet_SelectedIndexChanged">
                               </asp:DropDownList>
                                                 </td>
                                                 <td style="height: 13px; width: 100px; text-align: right;">
                                                     &nbsp;</td>
                                                 <td style="text-align: right;" align="left" class="style2">
                                                     Days :</td>
                                                 <td style="width: 187px; height: 13px; text-align: left">
                                                     <asp:TextBox ID="txt_days" runat="server" CssClass="required_box" MaxLength="3"
                                                         Width="67px"></asp:TextBox></td>
                                                 <td style="text-align: center;" class="style4">
                                                     Rank : </td>
                                                 <td style="width: 99px;" align="left" rowspan="4">
                                                 <asp:ListBox ID="chkrank" runat="server" CssClass="input_box" Width="150px" 
                                                         SelectionMode="Multiple" Height="70px"></asp:ListBox>
                                                                                </td>
                                                 <td style="text-align: left;" class="style3">&nbsp;</td>
                                                 <td align="left" class="style5">&nbsp;<asp:Button ID="btn_show" runat="server" CssClass="btn"
                                                         Text="Show Report" OnClick="btn_show_Click" /></td>
                                                 <td align="left" style="width: 35px; height: 13px">
                                                     &nbsp;</td>
                                                 <td style="height: 13px; text-align: center">
                                                     </td>
                                             </tr>
                                             <tr>
                                                 <td style="text-align:right" class="style6">
                                                     &nbsp;</td>
                                                 <td style="text-align:right" class="style7">
                                                     Vessel List :</td>
                                                 <td style="width: 86px; height: 13px; text-align :left ">
                                                     <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" 
                                                          Width="251px">
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td style="height: 13px; width: 100px; text-align: right;">
                                                     &nbsp;</td>
                                                 <td style="text-align: right;" align="left" class="style2">
                                                     Rank Group :</td>
                                                 <td style="width: 187px; height: 13px; text-align: left">
                                                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input_box" 
                                                         Width="110px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                                                     <asp:ListItem Value="A">All</asp:ListItem>
                                                     <asp:ListItem Value="O">Officers</asp:ListItem>
                                                     <asp:ListItem Value="R">Rating</asp:ListItem>
                                                 </asp:DropDownList></td>
                                                 <td style="text-align: right;" class="style4">
                                                     &nbsp;</td>
                                                 <td style="text-align: left;" class="style3">&nbsp;</td>
                                                 <td align="left" class="style5">
                                                     <asp:CheckBox ID="CheckBox1" runat="server" Text="Planning Not Done" />
                                                 </td>
                                                 <td align="left" style="width: 35px; height: 13px">
                                                     &nbsp;</td>
                                                 <td style="height: 13px; text-align: center">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td style="text-align:right" class="style6">
                                                     &nbsp;</td>
                                                 <td style="text-align:left" class="style1" colspan="5">
                                                 
                                                     </td>
                                                 <td style="text-align: right;" class="style4">
                                                     &nbsp;</td>
                                                 <td style="text-align: left;" class="style3">&nbsp;</td>
                                                 <td align="left" class="style5">&nbsp;</td>
                                                 <td align="left" style="width: 35px; height: 13px">
                                                     &nbsp;</td>
                                                 <td style="height: 13px; text-align: center">
                                                     &nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td style="text-align:right" class="style6">
                                                     &nbsp;</td>
                                                 <td style="text-align:right" class="style7">
                                                     &nbsp;</td>
                                                 <td style="width: 86px; height: 13px; text-align :left ">
                                                     &nbsp;&nbsp;</td>
                                                 <td style="height: 13px; width: 100px; text-align: right;">
                                                     &nbsp;</td>
                                                 <td style="text-align: right;" align="left" class="style2">
                                                     &nbsp;</td>
                                                 <td style="width: 187px; height: 13px; text-align: left">
                                                     &nbsp;</td>
                                                 <td style="text-align: right;" class="style4">
                                                     &nbsp;</td>
                                                 <td style="text-align: left;" class="style3">&nbsp;</td>
                                                 <td align="left"  style=" text-align :left">
                                                 <asp:Label runat="server" ID="lblMsg" ForeColor="Red" ></asp:Label> 
                                                 </td>
                                                 <td align="left" style="width: 35px; height: 13px">
                                                     &nbsp;</td>
                                                 <td style="height: 13px; text-align: center">
                                                     &nbsp;</td>
                                             </tr>
                         </table>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                         FilterType="Numbers" TargetControlID="txt_days">
                     </ajaxToolkit:FilteredTextBoxExtender>
                 </td>
             </tr>
          <tr>
           <td>
          <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:435px; overflow:auto"></iframe>
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
