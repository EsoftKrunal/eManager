<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceReport.aspx.cs" Inherits="Reporting_InvoiceReport" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %> <asp:Content ContentPlaceHolderID="contentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
 function DoPost()
 {
 if(document.getElementById("<%=rpttype.ClientID%>").value=="0")
 {
 var s="";
 }
 else if(document.getElementById("<%=rpttype.ClientID%>").value=="5")
 {
 var s="InvoiceReport_Show.aspx?rbtype=" + document.getElementById("<%=rpttype.ClientID%>").value+"&invtype=&vendor=" + document.getElementById("<%=ddvendor.ClientID%>").value+"&vessel="+ document.getElementById("<%=ddvessel.ClientID%>").value+"&user="+ document.getElementById("<%=ddUser.ClientID%>").value+"&fromdate=&todate=&days=" + document.getElementById("<%=txtdays.ClientID%>").value + "&UnApp=" + document.getElementById("<%=chk_Unapp.ClientID%>").checked;
 }
 else if(document.getElementById("<%=rpttype.ClientID%>").value=="6")
 {
 var s="InvoiceReport_Show.aspx?rbtype=" + document.getElementById("<%=rpttype.ClientID%>").value+"&fromdate="+"&todate="+"&vendor="+"&vessel="+ document.getElementById("<%=ddvessel.ClientID%>").value+"&user="+ document.getElementById("<%=ddUser.ClientID%>").value+"&invtype=";
 }
 else if(document.getElementById("<%=rpttype.ClientID%>").value=="7")
 {
 var s="InvoiceReport_Show.aspx?rbtype=" + document.getElementById("<%=rpttype.ClientID%>").value+"&fromdate=" + document.getElementById("<%=txtfromdate.ClientID%>").value+"&todate=" + document.getElementById("<%=txttodate.ClientID%>").value+"&vendor=" + document.getElementById("<%=ddvendor.ClientID%>").value+"&vessel="+"&user="+"&invtype=";
 }
 else if(document.getElementById("<%=rpttype.ClientID%>").value=="8")
 {
 var s="InvoiceReport_Show.aspx?rbtype=" + document.getElementById("<%=rpttype.ClientID%>").value+"&fromdate=" + document.getElementById("<%=txtfromdate.ClientID%>").value+"&todate=" + document.getElementById("<%=txttodate.ClientID%>").value+"&vendor=" + document.getElementById("<%=ddvendor.ClientID%>").value+"&vessel="+ document.getElementById("<%=ddvessel.ClientID%>").value+"&user="+ document.getElementById("<%=ddUser.ClientID%>").value+"&invtype="+document.getElementById("ctl00_contentPlaceHolder1_ddlinvoicetype").value;
 }
 else if(document.getElementById("<%=rpttype.ClientID%>").value=="4")
 {
 var s="InvoiceReport_Show.aspx?rbtype=" + document.getElementById("<%=rpttype.ClientID%>").value+"&fromdate=" + document.getElementById("<%=txtfromdate.ClientID%>").value+"&todate=" + document.getElementById("<%=txtfromdate.ClientID%>").value+"&vendor=" + document.getElementById("<%=ddvendor.ClientID%>").value+"&vessel="+ document.getElementById("<%=ddvessel.ClientID%>").value+"&user="+ document.getElementById("<%=ddUser.ClientID%>").value+"&invtype=";
 }
 else
 {
 var s="InvoiceReport_Show.aspx?rbtype=" + document.getElementById("<%=rpttype.ClientID%>").value+"&fromdate=" + document.getElementById("<%=txtfromdate.ClientID%>").value+"&todate=" + document.getElementById("<%=txttodate.ClientID%>").value+"&vendor=" + document.getElementById("<%=ddvendor.ClientID%>").value+"&vessel="+ document.getElementById("<%=ddvessel.ClientID%>").value+"&user="+ document.getElementById("<%=ddUser.ClientID%>").value+"&invtype=";
 }
 document.getElementById("IFRAME1").setAttribute("src",s);
 return false;
 }
 </script>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table border="0" cellpadding="0" cellspacing="0" style="
            text-align: center" width="100%">
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;
                                width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%" style="padding-left: 5px">
                                    <tr>
                                        <td style="height: 17px; text-align: right;">
                                            Select :</td>
                                            <td style="height: 17px" colspan="2"><asp:DropDownList ID="rpttype" runat="server" CssClass="required_box" Width="253px" AutoPostBack="True" OnSelectedIndexChanged="rpttype_SelectedIndexChanged">
                                                <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                                <asp:ListItem Value="1">Total Received  Invoice By Date</asp:ListItem>
                                                <asp:ListItem Value="2">Total Approved Invoice By Date</asp:ListItem>
                                                <asp:ListItem Value="3">Total Paid Invoice By Date</asp:ListItem>
                                                <asp:ListItem Value="4" >Total Overdue Invoices By Date</asp:ListItem>
                                                <asp:ListItem Value="5" >Total  Unpaid invoice</asp:ListItem>
                                                <asp:ListItem Value="6" >Total UnApproved Invoice</asp:ListItem>
                                                <asp:ListItem Value="7" >Total Invoice By Vendor</asp:ListItem>
                                                <asp:ListItem Value="8" >Invoice Verify By Date </asp:ListItem>
                                            </asp:DropDownList></td>
                                            
                                        <td style="height: 17px">
                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Show Report" OnClientClick="DoPost(); return false;" /></td>
                                    </tr>
                                     <tr>
                                         <td style="height: 4px">
                                         </td>
                                         <td style="height: 4px; width: 266px;">
                                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rpttype"
                                                 ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                         <td style="height: 4px; width: 64px;">
                                         </td>
                                         <td style="height: 4px">
                                             &nbsp;</td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr id="trdate" runat="server">
                                         <td style="height: 4px; text-align: right;">
                                             <asp:Label ID="lblfrom" runat="server" Text="From Date:" Width="100%"></asp:Label></td>
                                         <td style="height: 4px; width: 266px;">
                                         <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box"></asp:TextBox>
                                             <asp:ImageButton
                                                    ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                    TabIndex="79" /></td>
                                         <td style="height: 4px;text-align: right;">
                                             <asp:Label ID="lblto" runat="server" Text="To Date:" Width="100%"></asp:Label></td>
                                         <td style="height: 4px">
                                         <asp:TextBox ID="txttodate" runat="server" CssClass="input_box"></asp:TextBox>
                                             <asp:ImageButton
                                                    ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                    TabIndex="79" /></td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="height: 2px">
                                         </td>
                                         <td style="height: 2px; width: 266px;">
                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                </td>
                                         <td style="height: 2px; width: 64px;">
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                             </td>
                                         <td style="height: 2px">&nbsp;</td>
                                     </tr>
                                     <tr id="truser" runat="server">
                                         <td style="height: 4px; text-align: right;">
                                             User :</td>
                                         <td style="height: 4px; width: 266px;">
                                             <asp:DropDownList ID="ddUser" runat="server" CssClass="input_box" Width="253px" AutoPostBack="false" OnSelectedIndexChanged="rpttype_SelectedIndexChanged">
                                                
                                             </asp:DropDownList></td>
                                         <td style="height: 4px; text-align: right;">
                                             Vessel :</td>
                                         <td style="height: 4px">
                                             <asp:DropDownList ID="ddvessel" runat="server" CssClass="input_box" Width="253px" AutoPostBack="false" OnSelectedIndexChanged="rpttype_SelectedIndexChanged">
                                                 
                                             </asp:DropDownList></td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="height: 4px">
                                         </td>
                                         <td style="height: 4px; width: 266px;">
                                             &nbsp;</td>
                                         <td style="height: 4px; width: 64px;">
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr id="trvendor" runat="server">
                                         <td style="height: 4px; text-align: right;">
                                             Vendor :</td>
                                         <td style="height: 4px" width="200px" >
                                             <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" Width="253px" AutoPostBack="false" OnSelectedIndexChanged="rpttype_SelectedIndexChanged"></asp:DropDownList></td>
                                             <td>
                                             <asp:Label ID="lblRange" runat="server" Text="Due (In Days) :" Width="100%" 
                                                     style="text-align: right"></asp:Label></td>
                                         <td style="height: 4px">
                                         <asp:TextBox runat="server" ID="txtdays" MaxLength="3" CssClass="input_box" Text="0"></asp:TextBox>   
                                         &nbsp;&nbsp;
                                         <asp:CheckBox runat="server" ID="chk_Unapp" Text="Exclude Un-Approved Invoices" />  
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr runat="server">
                                         <td style="height: 4px">
                                         </td>
                                         <td style="height: 4px; width: 266px;">
                                             &nbsp;</td>
                                         <td style="width: 64px; height: 4px">
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr runat="server" id="trverify">
                                         <td style="height: 4px; text-align: right;">
                                             Invoice Type:</td>
                                         <td style="height: 4px; width: 266px;">
                                             <asp:DropDownList ID="ddlinvoicetype" runat="server" CssClass="input_box" Width="253px">
                                                 <asp:ListItem Value="0">Invoice Verify For Approval</asp:ListItem>
                                                 <asp:ListItem Value="1">Invoice Verify For Payment</asp:ListItem>
                                             </asp:DropDownList></td>
                                         <td style="width: 64px; height: 4px; text-align :right ">
                                             &nbsp;</td>
                                         <td style="height: 4px">
                                             &nbsp;</td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                     <tr runat="server">
                                         <td style="height: 4px">
                                         </td>
                                         <td style="width: 266px; height: 4px">
                                             &nbsp;</td>
                                         <td style="width: 64px; height: 4px">
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                         <td style="height: 4px">
                                         </td>
                                     </tr>
                                    <tr><td colspan="5">
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
                                        <iframe id="IFRAME1" frameborder="1" style="width: 100%; height:500px; overflow:auto"></iframe>
                                    </td></tr>
                                </table>
                               <div id="divPrint">
                                    &nbsp;</div>
                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgto" PopupPosition="TopRight"  TargetControlID="txttodate">
                    </ajaxToolkit:CalendarExtender>
                   
                </td>
            </tr>
        </table>
</asp:Content>
