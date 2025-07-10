<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modules/HRD/Reporting/CrewExperienceSummaryMast.master" CodeFile="CrewsExperienceReport.aspx.cs" Inherits="Reporting_CrewsExperienceReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
<table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                    <tr>
                        <td style="width: 100%;text-align: left">
                            
                             <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td colspan="2">
                                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                             <tr>
                                                 <td style="height: 13px; width: 112px; padding-right: 20px; " align="right">Crew Member:</td>
                                                 <td align="left" style="padding-right: 20px; width: 38px; height: 13px">
                                                     <asp:TextBox ID="txt_Emp_number" runat="server" CssClass="input_box" style="text-transform:uppercase" MaxLength="6" TabIndex="1" Width="65px"></asp:TextBox></td>
                                                 <td align="left" style="padding-right: 20px; width: 408px; height: 13px">
                                                    <asp:RadioButtonList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" Width="100%" TabIndex="2">
                                                    <asp:ListItem Value="Office" Selected="True">Office</asp:ListItem>
                                                    <asp:ListItem Value="Full">Full</asp:ListItem>
                                                    <asp:ListItem Value="Owner">Owner</asp:ListItem>
                                                    <asp:ListItem Value="TotalRank">Total Rank</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                 <td align="left" style="padding-right: 20px; width: 408px; height: 13px">
                                                     <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="validChars" FilterType="Custom" TargetControlID="txt_Emp_number" ValidChars="0123456789sSyY"></ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                                 <td style="height: 13px; text-align: left; width :141px "><asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" ValidationGroup="aa" TabIndex="3" />&nbsp;&nbsp;&nbsp;</td>
                                             </tr>
                                             </table>
                                         </fieldset>
                                     </td>
                                 </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                          <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:389px; overflow:auto"></iframe>
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
</asp:Content>
