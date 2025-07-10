<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCV.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="Reporting_PrintCV" Title="Report : Print CV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   <%-- <link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: left"> <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager></div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 149px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
                        <tr >
                            <td align="center"class="text headerband">Print CV</td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9" width="100%">
                                    <tr>
                                        <td style="padding:2px;text-align:right;width:100px;">
                                            Crew # : 
                                         </td>
                                        <td style="text-align:left;width:250px;">
                                            <asp:TextBox ID="txt_Emp_number" runat="server" style="text-transform:uppercase" CssClass="required_box" MaxLength="6" TabIndex="1" Width="141px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Emp_number" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align:left;width:150px;">
                                             <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" TabIndex="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="3">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="left" colspan="2" >
                                                             <iframe runat="server" height="432px" id="IFRAME1" frameborder="1" style="width: 100%; overflow:auto"></iframe>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterMode="validChars" FilterType="Custom" TargetControlID="txt_Emp_number" ValidChars="0123456789sSyY"></ajaxToolkit:FilteredTextBoxExtender>
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
    <script language="javascript"  type="text/javascript">
    var i=0;
    for (i=0;i<=document.all.length-1;i++)
    {
        if (document.all(i).nodeName=="IMG")
        {
            if(document.all(i).height=="67" && document.all(i).width=="60")
            {
             document.all(i).width="80";
             document.all(i).height="110";
            } 
        }
    } 
    
    </script>
    </asp:Content>



