<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FamilyMembersReport.aspx.cs" MasterPageFile="~/MasterPage.master" Title="Family Member Report" Inherits="Reporting_FamilyMembersReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
             <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >  
         <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5"> Family Member Reports</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: center">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;
                                             padding-top: 10px; border-bottom: #4371a5 1px solid; height: 10px">
                                             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top: 5px; padding-bottom: 5px; text-align: center;">
       
                                             <table cellpadding="3" cellspacing="0" width="100%">
                                                 <tr>
                                                     <td style="height: 13px; padding-right: 10px;" align="right">
                                                         Age:</td>
                                                     <td style="height: 13px" align="left">
                                                         From: &nbsp; &nbsp;<asp:TextBox ID="txt_Exp_From" runat="server" CssClass="input_box"
                                                             MaxLength="2" TabIndex="1" Width="16px"></asp:TextBox>&nbsp; &nbsp; &nbsp; To:
                                                         &nbsp;
                                                         <asp:TextBox ID="txt_Exp_To" runat="server" CssClass="input_box" MaxLength="2" TabIndex="2"
                                                             Width="16px"></asp:TextBox></td>
                                                     <td style="height: 13px; padding-right: 10px;" align="right">
                                                         Relation:</td>
                                                     <td style="height: 13px" align="left">
                                                         <asp:DropDownList ID="ddl_Relation" runat="server" CssClass="input_box" TabIndex="3"
                                                             Width="140px">
                                                             <asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem>
                                                             <asp:ListItem Value="1">Father</asp:ListItem>
                                                             <asp:ListItem Value="2">Mother</asp:ListItem>
                                                             <asp:ListItem Value="3">Wife</asp:ListItem>
                                                             <asp:ListItem Value="4">Husband</asp:ListItem>
                                                             <asp:ListItem Value="5">Child</asp:ListItem>
                                                             <asp:ListItem Value="6">Brother</asp:ListItem>
                                                             <asp:ListItem Value="7">Sister</asp:ListItem>
                                                         </asp:DropDownList>&nbsp;</td>
                                                     <td style="height: 13px; padding-right: 10px;" align="right">
                                                         &nbsp;</td>
                                                     <td style="height: 13px" align="left">
                                                         <asp:Button ID="Button1" runat="server" CssClass="btn" Text="Show Report" Width="94px" OnClick="Button1_Click" TabIndex="8"/></td>
                                                     <td style="height: 13px" align="left">
                                                         &nbsp;</td>
                                                 </tr>
                                                 <tr>
                                                     <td align="right" style="padding-right: 10px; height: 13px">
                                                         Crew Status:</td>
                                                     <td align="left" style="height: 13px">
                                                         <asp:DropDownList ID="ddCrewStatus" runat="server" CssClass="input_box" TabIndex="4" Width="170px">
                                                             
                                                             
                                                         </asp:DropDownList></td>
                                                     <td align="right" style="padding-right: 10px; height: 13px">
                                                         Gender:</td>
                                                     <td align="left" style="height: 13px">
                                                         <asp:DropDownList ID="ddGender" runat="server" CssClass="input_box" TabIndex="4"
                                                             Width="145px">
                                                             <asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem>
                                                             <asp:ListItem Value="1">Male</asp:ListItem>
                                                             <asp:ListItem Value="2">FeMale</asp:ListItem>
                                                         </asp:DropDownList></td>
                                                     <td align="right" style="padding-right: 10px; height: 13px">
                                                         &nbsp;</td>
                                                     <td align="left" style="height: 13px">
                                                         &nbsp;</td>
                                                     <td align="left" style="height: 13px">
                                                         &nbsp;</td>
                                                 </tr>
                                                 <tr>
                                                     <td align="right" style="padding-right: 10px; height: 13px">
                                                         Country:</td>
                                                     <td align="left" style="height: 13px">
                                                         <asp:DropDownList ID="ddCountry" runat="server" CssClass="input_box"
                                                             TabIndex="5" Width="170px">
                                                         </asp:DropDownList></td>
                                                     <td align="right" style="padding-right: 10px; height: 13px">
                                                         Area Code:</td>
                                                     <td align="left" style="height: 13px">
                                                         <asp:TextBox ID="txt_P_Area_Code_Tel" runat="server" CssClass="input_box" MaxLength="5"
                                                             meta:resourcekey="txt_P_Area_Code_TelResource1" TabIndex="6" Width="135px"></asp:TextBox></td>
                                                     <td align="right" style="padding-right: 10px; height: 13px">
                                                         NOK:</td>
                                                     <td align="left" style="height: 13px">
                                                         <asp:CheckBox ID="CheckBox1" runat="server" TabIndex="7" /></td>
                                                     <td align="left" style="height: 13px">
                                                         &nbsp;</td>
                                                 </tr>
                                                 </table>
                                             </fieldset>
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                 FilterType="Numbers" TargetControlID="txt_Exp_From">
                                             </ajaxToolkit:FilteredTextBoxExtender>
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                 FilterType="Numbers" TargetControlID="txt_Exp_To">
                                             </ajaxToolkit:FilteredTextBoxExtender>
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                 FilterType="Numbers" TargetControlID="txt_P_Area_Code_Tel">
                                             </ajaxToolkit:FilteredTextBoxExtender>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td colspan="2">
                                           <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:380px;overflow-x:hidden;overflow-y:scroll" ></iframe>
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
        </td></tr> </table> 
        </div>
 </asp:Content>
