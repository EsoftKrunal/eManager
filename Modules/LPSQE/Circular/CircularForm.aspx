<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CircularForm.aspx.cs" Inherits="Circular_CircularForm" Title="Circular Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseThisWindow()
        {
            this.close();
        }
        function RefereshParentPage()
        {
            window.opener.Reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1"  runat="server" >
        <ContentTemplate>
            <div style="font-family:Arial;font-size:12px;">
            <table cellpadding="2" cellspacing="2" border="0" width="750px" rules="none" style="margin:auto;">
                <colgroup>
                    <col width="160px" />
                    <col width="230px" />
                    <col width="99px" />
                    <col />
                    <tr>
                        <td class="text headerband" colspan="4" style="height: 23px;text-align :center; font-size:15px;">New Circular </td>
                    </tr>
                    <tr>
                        <td><b>Circular Date :</b> </td>
                        <td>
                            <asp:TextBox ID="txtCirDate" runat="server" CssClass="input_box" Enabled="false" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txtCirDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td><b>Category :</b> </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="input_box">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Source :</b> </td>
                        <td>
                            <asp:TextBox ID="lblSource" runat="server" CssClass="input_box" MaxLength="70" Width="400px"></asp:TextBox>
                            <asp:TextBox ID="txtSuperSedes" runat="server" CssClass="input_box" Visible="false" Width="200px"></asp:TextBox>
                        </td>
                        <td><b>Type :</b> </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="input_box">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Internal" Value="I"></asp:ListItem>
                                <asp:ListItem Text="External" Value="E"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;"><b>Circular Topic:</b> </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCirCularTopic" runat="server" CssClass="input_box" Height="40px" MaxLength="50" TextMode="MultiLine" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;"><b>Circular Details :</b> </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCircularDetails" runat="server" CssClass="input_box" Height="380px" TextMode="MultiLine" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Next Review Date :</b> </td>
                        <td>
                            <asp:TextBox ID="txtNextReviewDate" runat="server" CssClass="input_box" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtNextReviewDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><b>Attach Document : </b></td>
                        <td colspan="3">
                            <asp:FileUpload ID="fuAddFile" runat="server" CssClass="input_box" Width="300px" />
                            <span style="font-size:9px; font-style:italic;color:Maroon;">( Only PDF is allowed.)</span> <a id="aFile" runat="server" target="_blank" visible="false">
                            <img src="../../HRD/Images/paperclipx12.png" style="border:none;" />
                            </a>
                            <br />
                            <span style="font-size:11px; font-style:italic;color:Maroon;"><b>Remark :</b> Please note that the attached pdf will be merged into one circular document. </span></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:right;">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_OnClick" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancel_OnClick" Text="Cancel" />
                            <asp:Button ID="Button2" runat="server" CssClass="btn" OnClick="sdfdfdfdf" style="display:none;" Text="Close" />
                        </td>
                    </tr>
                    <tr id="trApprovalSection" runat="server" visible="false">
                        <td colspan="4">
                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                <colgroup>
                                    <col width="200px" />
                                    <col />
                                    <col />
                                    <col />
                                    <tr>
                                        <td class="text headerband" colspan="4" style=" height: 23px;text-align :center; font-size:15px;">Submit For Approval </td>
                                    </tr>
                                    <tr>
                                        <td><b>Submit for Approval To: </b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubAppTo" runat="server" CssClass="input_box" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><b><%--Submitted for Approval On : --%></b></td>
                                        <td><%--<asp:TextBox ID="txtSubAppOn" runat="server" CssClass="input_box" Width="80px"  ></asp:TextBox>
                                <asp:ImageButton id="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> 
                                <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtSubAppOn"> 
                                </ajaxToolkit:CalendarExtender>--%></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align:right;">
                                            <asp:Label ID="lblApprovalMsg" runat="server" ForeColor="Red"></asp:Label>
                                            <asp:Button ID="btnSubmitSave" runat="server" CssClass="btn" OnClick="btnApprovalSave_OnClick" Text="Submit" />
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                        </td>
                    </tr>
                    <tr id="trViewApprovedComm" runat="server" visible="false">
                        <td colspan="4">
                            <table border="0" cellpadding="0" cellspacing="2" rules="none" width="100%">
                                <colgroup>
                                    <col width="170px" />
                                    <col width="180px" />
                                    <col />
                                    <col />
                                    <tr>
                                        <td class="text headerband" colspan="4" style=" height: 23px;text-align :center; font-size:15px;">Submited For Approval </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; padding-right:5px;"><b>Requested By : </b></td>
                                        <td>
                                            <asp:Label ID="lblRequestedBy" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align:right; padding-right:5px;"><b>Requested On : </b></td>
                                        <td>
                                            <asp:Label ID="lblRequestedOn" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; padding-right:5px;"><b>Submit for Approval To : </b></td>
                                        <td>
                                            <asp:Label ID="lblSubmitedForApp" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align:right;padding-right:5px;"><b>Submitted for Approval On : </b></td>
                                        <td>
                                            <asp:Label ID="lblSubmitedAppOn" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;padding-right:5px;"><b>
                                            <asp:Label ID="lblAppRejByText" runat="server"></asp:Label>
                                            </b></td>
                                        <td>
                                            <asp:Label ID="lblApprovedBy" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align:right;padding-right:5px;"><b>
                                            <asp:Label ID="lblAppRejOnText" runat="server"></asp:Label>
                                            </b></td>
                                        <td>
                                            <asp:Label ID="lblApprovedOn" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trApprovalComments" runat="server" visible="false">
                                        <td colspan="4">
                                            <asp:Label ID="lblApprovalCommentsText" runat="server" style="font-weight:bold"></asp:Label>
                                            <br />
                                            <div style="width:99%; height:100px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                                                <asp:Label ID="lblApprovalComments" runat="server" Width="450px"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                        </td>
                    </tr>
                </colgroup>
        </table>
            </div>        
        </ContentTemplate>
     <Triggers>
     <asp:PostBackTrigger ControlID="btnSave" />  
     </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
