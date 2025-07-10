<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewReliefPlanning.aspx.cs" Inherits="Reporting_CrewReliefPlanning" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <style type="text/css">
        .style1
        {
            height: 13px;
            }
        .style2
        {
            height: 13px;
            width: 113px;
        }
        .style3
        {
            height: 13px;
            width: 91px;
        }
        .style4
        {
            height: 13px;
            width: 81px;
        }
        .style5
        {
            height: 13px;
            width: 154px;
        }
        .style6
        {
            height: 13px;
            width: 33px;
        }
        .style7
        {
            height: 13px;
            width: 103px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1" >
<div style="text-align: center">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
 FilterMode="validChars" FilterType="Custom" TargetControlID="txtdays" ValidChars="0123456789">
</ajaxToolkit:FilteredTextBoxExtender>
 
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
<table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
<tr>
    <td align="center" class="text headerband" style="width: 100%; "> Relief Planning</td>
</tr>
<tr>
    <td style="width: 100%">
        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
            <tr>
                <td style="width: 100%;text-align: left">
                     <table cellpadding="0" cellspacing="0" width="100%">
                         <tr>
                             <td colspan="2" style="padding: 3px;border-bottom: #4371a5 1px solid;">
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tbody>
                                             <tr>
                                                 <td style="text-align:right; width:120px;" class="style6">
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
                                                     <asp:TextBox ID="txtdays" runat="server" CssClass="required_box" MaxLength="3"
                                                         Width="67px"></asp:TextBox></td>
                                                 <td style="text-align: center;" class="style4">
                                                     Rank : </td>
                                                 <td style="width: 99px;" align="left" rowspan="4">
                                                 <asp:ListBox ID="chkrank" runat="server" CssClass="input_box" Width="150px" 
                                                         SelectionMode="Multiple" Height="70px"></asp:ListBox>
                                                                                </td>
                                                 <td style="text-align: left;" class="style3">&nbsp;</td>
                                                 <td align="left" class="style5">&nbsp;<asp:Button ID="Button1" runat="server" CssClass="btn"
                                                         Text="Show Report" OnClick="Button1_Click" /></td>
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
                                                     <asp:DropDownList ID="ddl_vessel" runat="server" CssClass="input_box" 
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
                                                 <td style="text-align:right" class="style1" colspan="2">
                                                        Recruiting Office :
                                                     </td>
                                                     <td colspan="4">
                                                          <asp:DropDownList ID="ddlRecruitingOff" runat="server" Width="150px" CssClass="input_box"  ></asp:DropDownList>
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
                                         </tbody>
                                     </table>
                             </td>
                         </tr>
                        <tr><td colspan="2">
                        <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:380px; overflow:auto"></iframe>
                            </td></tr>
                    </table>
                </td>
            </tr>
        </table>
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
