<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/Reporting/CrewExperienceSummaryMast.master" AutoEventWireup="true" CodeFile="CrewMatrixReportHeader.aspx.cs" Inherits="Reporting_CrewMatrixReportHeader" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding-right: 10px; padding-left: 10px;padding-bottom: 3px; padding-top: 3px; border-bottom: #4371a5 1px solid;">
                                         <table cellpadding="0" cellspacing="0" width="100%">
                                                 <tr>
                                                     <td style="height: 13px; width: 705px; padding-right: 20px;" align="right">Select Crew Member:</td>
                                                     <td align="left" style="padding-right: 20px; width: 430px; height: 13px">
                                                         <asp:TextBox ID="txt_Emp_number" runat="server" CssClass="required_box" style="text-transform:uppercase" MaxLength="6" TabIndex="1" Width="141px"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Emp_number"
                                                             ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                     <td align="left" style="padding-right: 20px; width: 430px; height: 13px">
                                                         <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                                         </td>
                                                         <td style="height: 13px; text-align: center; width: 180px;">
                                                         <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" TabIndex="2" /></td>
                                                 </tr>
                                                 </table>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                             FilterMode="validChars" FilterType="Custom" TargetControlID="txt_Emp_number"
                                             ValidChars="0123456789sSyY">
                                         </ajaxToolkit:FilteredTextBoxExtender>
                                         </td>
                                     </tr>
                                    <tr><td colspan="2" align="center">
                                        <iframe id="IFRAME1" runat="server" frameborder="1" style="overflow: auto;width: 100%; height: 400px"></iframe>
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

                  
</asp:Content>

