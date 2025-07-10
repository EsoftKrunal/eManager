<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyCOCPopUp.aspx.cs" Inherits="FormReporting_ModifyCOCPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
   
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="1" cellspacing="0" 
            style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="4" style=" height:23px;text-align:center" class="text headerband">
                    Modify COC</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align :center " >
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style1">
                    COC Issued From :</td>
                <td style="text-align: left;" >
                    <asp:TextBox ID="txt_COCIssuedFrom" runat="server" CssClass="input_box" 
                        MaxLength="49" Width="250px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right" class="style1">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style1">
                    Vessel :</td>
                <td style="text-align: left" >
                    <asp:DropDownList ID="ddlvessel" runat="server" CssClass="input_box" 
                        Width="254px">
                    </asp:DropDownList></td>
                <td style="padding-right: 5px; text-align: right" class="style1">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    COC Ref # :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_COCNum" runat="server" CssClass="input_box" Width="249px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    COC Issue Date :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_IssueDt" runat="server" CssClass="input_box" Width="90px"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Place Issued :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_COCIssuedPlace" runat="server" CssClass="input_box" 
                        MaxLength="49" Width="248px" Height="16px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Target Closure Date :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_TargetCDate" runat="server" CssClass="input_box" Width="90px" MaxLength="11"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Survey Started :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_SurveyStDt" runat="server" CssClass="input_box" 
                        Width="90px"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Survey Completed :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_SurveyEndDt" runat="server" CssClass="input_box" 
                        Width="90px"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton5" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Surveyor&#39;s Name :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_COCSurveyor" runat="server" CssClass="input_box" 
                        MaxLength="49" Width="250px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top">
                    What's Wrong (desc.) :</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txt_Desc" runat="server" CssClass="input_box" Height="72px" MaxLength="49"
                        TextMode="MultiLine" Width="521px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top">
                    Corrective Actions (desc.) :</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txt_Desc0" runat="server" CssClass="input_box" MaxLength="49" 
                        Width="519px" Height="72px" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Upload COC :</td>
                <td colspan="2" style="text-align: left">
                    <asp:FileUpload ID="flp_COCUpload" runat="server" CssClass="input_box" Width="300px" /></td>
                    <td>
                                                &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    &nbsp;</td>
                <td colspan="2" style="text-align: left">
                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" />&nbsp;<asp:Button
                        ID="btn_Cancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btn_Cancel_Click" Width="59px" />
                    </td>
                    <td>
                        &nbsp;</td>
            </tr>
           <tr>
                <td style="padding-right: 5px; text-align: right">
                    Created By :</td>
                <td style="text-align: left">
                    <asp:Label ID="txt_CreatedBy" runat="server" ></asp:Label></td>
                <td style="padding-right: 5px; text-align: right">
                    Created On :</td>
                <td style="text-align: left">
                    <asp:Label ID="txt_CreatedOn" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Modified By :</td>
                <td style="text-align: left">
                    <asp:Label ID="txt_FModifiedBy" runat="server" ></asp:Label></td>
                <td style="padding-right: 5px; text-align: right">
                    Modified On :</td>
                <td style="text-align: left">
                    <asp:Label ID="txt_FModifiedOn" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4">
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_TargetCDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txt_IssueDt">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton3" PopupPosition="TopRight" 
                        TargetControlID="txt_SurveyStDt">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton5" PopupPosition="TopRight" 
                        TargetControlID="txt_SurveyEndDt">
                    </ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="HiddenField_File" runat="server" />
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
