<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="InspectionDocuments.aspx.cs" Inherits="Transactions_InspectionDocuments" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
<script language="javascript" type="text/javascript">
    function trimAll(sString) 
    {
        while (sString.substring(0,1) == ' ')
        {
            sString = sString.substring(1, sString.length);
        }
        while (sString.substring(sString.length-1, sString.length) ==' ')
        {
            sString = sString.substring(0,sString.length-1);
        }
        return sString;
    }
    function ValidateForm()
    {
   // alert(document.getElementById("ctl00_ContentPlaceHolder1_FileUpload_Doc").value)
     document.getElementById("HiddenField1").value=document.getElementById("FileUpload_Doc").value;
        if(trimAll(document.getElementById("ddl_DocType").value)=="0")
        {
            alert('Please Enter Document Type!');
            document.getElementById("ddl_DocType").focus();
            return false;
        }
        if(trimAll(document.getElementById("txt_DocName").value)=="")
        {
            alert('Please Enter Document Name!');
            document.getElementById("txt_DocName").focus();
            return false;
        }
    }
</script>z
    <style type="text/css">
        .btn
        {
            border: 1px solid #fe0034;
	font-family: arial;
	font-size: 12px;
	color: #fff;
	border-radius: 3px;
	-webkit-border-radius: 3px;
	-ms-border-radius: 3px;
	background: #fe0030;
	background: linear-gradient(#ff7c96, #fe0030);
	background: -webkit-linear-gradient(#ff7c96, #fe0030);
	background: -ms-linear-gradient(#ff7c96, #fe0030);
	padding: 4px 6px;
	cursor: pointer;
        }
        </style>
     </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true"><Triggers><asp:PostBackTrigger ControlID="btn_Children"/></Triggers><ContentTemplate>
<asp:Panel id="pnl_InspDocuments" runat="server">
    <div style="font-family:Arial;font-size:12px;">
        <center>
             <div style="background-color:#206020; width:95% ; height:3px;">
            </div>
            <br />
    <table style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 10px; BORDER-TOP: #8fafdb 0px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; BORDER-LEFT: #8fafdb 1px solid; WIDTH: 100%; PADDING-TOP: 10px; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right" cellSpacing="0" cellPadding="0" border="0">
        <tbody>
            <tr>
                <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: center"></td>

            </tr>
            <tr><td><fieldset style="BORDER-RIGHT: #8fafdb 0px solid; BORDER-TOP: #8fafdb 0px solid; BORDER-LEFT: #8fafdb 0px solid; BORDER-BOTTOM: #8fafdb 0px solid; TEXT-ALIGN: right"><%--<legend><strong>Document</strong></legend>--%>
                <table style="PADDING-RIGHT: 4px; TEXT-ALIGN: center" cellSpacing=0 cellPadding=0 width="100%" border=0>
                    <tbody>
                       <tr><td align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" vAlign=top rowSpan=1></td><td style="TEXT-ALIGN: right" class="tablepad" align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td><td style="TEXT-ALIGN: right" align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td><td style="PADDING-RIGHT: 5px; TEXT-ALIGN: left"></td></tr><TR><td align=right>Doc. Type :</td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" vAlign=top rowSpan=1><asp:DropDownList id="ddl_DocType" runat="server" CssClass="input_box" Width="124px" __designer:wfdid="w4"></asp:DropDownList></td><td style="TEXT-ALIGN: right" class="tablepad" align=right>Doc. Name :</td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" colSpan=3><asp:TextBox id="txt_DocName" runat="server" CssClass="input_box" Width="354px" __designer:wfdid="w3"></asp:TextBox></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: right">File :</td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"><asp:FileUpload id="FileUpload_Doc" runat="server" CssClass="input_box" Width="234px" __designer:wfdid="w11"></asp:FileUpload></td></TR><TR><td align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" vAlign=top rowSpan=1></td><td style="TEXT-ALIGN: right" class="tablepad" align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" colSpan=3></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: right"></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td></TR><TR><td align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" vAlign=top rowSpan=1></td><td style="TEXT-ALIGN: right" class="tablepad" align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" colSpan=3></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: right"></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: right"><asp:Button id="btn_Children" onclick="btn_Save_Click" runat="server" CssClass="btn" Width="59px" __designer:wfdid="w10" Text="Save" OnClientClick="return ValidateForm();"></asp:Button></td></TR><TR><td style="TEXT-ALIGN: left" align=right>&nbsp;</td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left" vAlign=top rowSpan=1></td><td style="TEXT-ALIGN: right" class="tablepad" align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td><td style="TEXT-ALIGN: right" align=right></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"></td><td style="PADDING-LEFT: 5px; TEXT-ALIGN: left"><asp:HiddenField id="HiddenFieldInspDocument" runat="server"></asp:HiddenField> <asp:HiddenField id="HiddenField_Doc" runat="server"></asp:HiddenField> <asp:HiddenField id="HiddenField1" runat="server"></asp:HiddenField> <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_Dtdone"
                                                    ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%></td></TR></tbody></table><%--<fieldset style="border-right: #8fafdb 1px solid; padding-right: 5px; border-top: #8fafdb 1px solid;
                                                        padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px;
                                                        border-bottom: #8fafdb 1px solid; text-align: center">
                                                        <legend><strong>Document List</strong></legend>--%><DIV style="PADDING-RIGHT: 5px; OVERFLOW-Y: auto; PADDING-LEFT: 5px; WIDTH: 98%; HEIGHT: 280px; TEXT-ALIGN: left"><asp:GridView style="TEXT-ALIGN: center" id="Grid_Document" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="true" PageSize="9" GridLines="Horizontal" OnRowDataBound="Grid_Document_RowDataBound" OnRowDeleting="Grid_Document_RowDeleting" OnRowEditing="Grid_Document_RowEditing" OnSelectedIndexChanged="Grid_Document_SelectedIndexChanged" OnPageIndexChanging="Grid_Document_PageIndexChanging" OnSorted="Grid_Document_Sorted" OnSorting="Grid_Document_Sorting">
<RowStyle CssClass="rowstyle"></RowStyle>
<Columns>
<asp:TemplateField HeaderText="Document Type"><ItemTemplate>
                                                                            <asp:Label ID="lbl_DocType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                                            <asp:HiddenField ID="Hidden_DocumentId" runat="server" Value='<%# Eval("Id") %>' />
                                                                            <asp:HiddenField ID="hfd_Document" runat="server" Value='<%# Eval("FilePath") %>' />
                                                                        
</ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="DocumentName" HeaderText="Document Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="View"><ItemTemplate>
                                                                            <a target="_blank" href='<%# GetPath(Eval("Filepath").ToString()).ToString() %>' style='display: <%# (Eval("Filepath").ToString().Trim()=="")?"None":"Block" %>; cursor: hand'><img src="../HRD/Images/paperclip.gif" border="0" /></a>
                                                                        
</ItemTemplate>

<ItemStyle Width="35px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Edit"><ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit" />
                                                                        
</ItemTemplate>

<ItemStyle Width="40px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
                                                                         <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                                                        
</ItemTemplate>

<ItemStyle Width="50px"></ItemStyle>
</asp:TemplateField>
</Columns>

<PagerStyle HorizontalAlign="Center"></PagerStyle>

<SelectedRowStyle CssClass="selectedtowstyle"></SelectedRowStyle>

<HeaderStyle CssClass="headerstylefixedheadergrid" ForeColor="#0E64A0"></HeaderStyle>
</asp:GridView> </DIV><%--</fieldset>--%><TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0><TBODY><TR><td style="HEIGHT: 5px"></td></TR><TR><td style="PADDING-RIGHT: 5px; PADDING-LEFT: 16px; TEXT-ALIGN: center"><TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0><TBODY><TR><td style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Created By :</td><td style="TEXT-ALIGN: left"><asp:TextBox id="txtCreatedBy_Document" tabIndex=-1 runat="server" CssClass="input_box" ReadOnly="True" Width="154px" BackColor="Gainsboro"></asp:TextBox></td><td style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Created On :</td><td style="TEXT-ALIGN: left"><asp:TextBox id="txtCreatedOn_Document" tabIndex=-2 runat="server" CssClass="input_box" ReadOnly="True" Width="82px" BackColor="Gainsboro"></asp:TextBox></td><td style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Modified By :</td><td style="TEXT-ALIGN: left"><asp:TextBox id="txtModifiedBy_Document" tabIndex=-3 runat="server" CssClass="input_box" ReadOnly="True" Width="154px" BackColor="Gainsboro"></asp:TextBox></td><td style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Modified On :</td><td style="WIDTH: 115px; TEXT-ALIGN: left"><asp:TextBox id="txtModifiedOn_Document" tabIndex=-4 runat="server" CssClass="input_box" ReadOnly="True" Width="82px" BackColor="Gainsboro"></asp:TextBox></td><td style="PADDING-RIGHT: 0px; TEXT-ALIGN: right"><asp:Button id="btn_Print" onclick="btn_Print_Click" runat="server" CssClass="btn" Width="59px" Text="Print" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_InspDocuments');"></asp:Button></td></TR></TBODY></TABLE></td></TR><TR><td style="PADDING-RIGHT: 7px; PADDING-LEFT: 16px; TEXT-ALIGN: left">&nbsp;</td></TR><TR><td style="PADDING-RIGHT: 7px; PADDING-LEFT: 16px; TEXT-ALIGN: left"><asp:Label id="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td></TR></TBODY></TABLE></fieldset> <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy"
                                    PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txt_Dtdone">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AutoComplete="false"
                                    ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
                                    TargetControlID="txt_Dtdone">
                                </ajaxToolkit:MaskedEditExtender>--%></td></tr></tbody></table>
            </center></div>
            </asp:Panel> 
</ContentTemplate></asp:UpdatePanel>
</form>
</body>
</html>
