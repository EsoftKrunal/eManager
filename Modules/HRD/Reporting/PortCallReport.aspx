<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallReport.aspx.cs" Inherits="Reporting_PortCallReport" MasterPageFile="~/Modules/HRD/Reporting/ActivityTracking.master" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css" >  
    .fixedbar
    {
        position:fixed;
        margin:95px 0px 0px 210px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
        width:100%;
    }
    </style> 
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; text-align: left;border-bottom: #4371a5 1px solid;" valign="top">
                                   <table style="width: 100%">
                                       <tr>
                                           <td style="width: 48px">
                                               User:</td>
                                           <td style="width: 100px">
                                               <asp:DropDownList ID="ddUser" runat="server" AutoPostBack="false" CssClass="input_box" Width="204px">
                                               </asp:DropDownList></td>
                                           <td style="width: 71px">
                                             From Date:</td>
                                           <td style="width: 100px">
                                               <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="81px"></asp:TextBox>
                                               <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="79" /></td>
                                           <td style="width: 100px">
                                           <asp:Label runat="server" ID="lblMess" ForeColor="Red"></asp:Label>    
                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdate" ErrorMessage="Required" Width="82px"></asp:RequiredFieldValidator></td>
                                           <td style="width: 71px">
                                             To Date:</td>
                                           <td style="width: 100px">
                                               <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="81px"></asp:TextBox>
                                               <asp:ImageButton ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="79" /></td>
                                           <td style="width: 100px">
                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttodate" ErrorMessage="Required" Width="82px"></asp:RequiredFieldValidator></td>
                                           <td style="width: 100px">
                                               <asp:Button ID="btnsearch" runat="server" CssClass="btn" OnClientClick="return DoPost();" Text="Show Report" OnClick="btnsearch_Click" Width="90px" /></td>
                                       </tr>
                                   </table>       
                            </td>
                        </tr>
                       <tr>
                       <td>
                        <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:450px;" > 
                           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" ToolbarStyle-CssClass="fixedbar" AutoDataBind="true"  />
                           </div> 
                       </td>
                   </tr>
               </table>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd-MMM-yyyy" OnClientShown="onCalendarShown" PopupButtonID="imgfrom" PopupPosition="TopRight"  TargetControlID="txtfromdate" > </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd-MMM-yyyy" OnClientShown="onCalendarShown" PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate" > </ajaxToolkit:CalendarExtender>
                    
                </td>
            </tr>
        </table>
</td></tr></table>
</asp:Content>
    <%--</form>
</body>
</html>--%>