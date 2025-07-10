<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspectionSettings.aspx.cs" Inherits="Registers_InspectionSettings"
     %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function CheckMail(Field) {
            if (Field.indexOf("@") <= 0 || Field.indexOf("@@") >= 0 || Field.indexOf(".") <= 0 || Field.indexOf("..") >= 0 || Field.indexOf(" ") != -1 || Field.length <= 6 || Field.indexOf("'") >= 0 || Field.indexOf('"') >= 0 || Field.indexOf("~") >= 0 || Field.indexOf("!") >= 0 || Field.indexOf("#") >= 0 || Field.indexOf("$") >= 0 || Field.indexOf("%") >= 0 || Field.indexOf("^") >= 0 || Field.indexOf("&") >= 0 || Field.indexOf("*") >= 0 || Field.indexOf("(") >= 0 || Field.indexOf(")") >= 0 || Field.indexOf("--") >= 0 || Field.indexOf("+") >= 0 || Field.indexOf("=") >= 0 || Field.indexOf("|") >= 0 || Field.indexOf("/") >= 0 || Field.indexOf("?") >= 0 || Field.indexOf(">") >= 0 || Field.indexOf("<") >= 0 || Field.indexOf(",") >= 0 || Field.indexOf(";") >= 0 || Field.indexOf(":") >= 0 || Field.indexOf("{") >= 0 || Field.indexOf("}") >= 0 || Field.indexOf("[") >= 0 || Field.indexOf("]") >= 0 || Field.indexOf("`") >= 0 || Field.indexOf("\\") >= 0) {
                alert("" + Field + " MailId is not Correct!");
                return false;
            }
        }
        function ValidateMails() {
            var str = document.getElementById('txtMailTo').value.split(",");
            var i = 0;
            for (i = 0; i < str.length; i++) {
                if (CheckMail(str[i]) == false) {
                    return false;
                }
            }

        }
    </script>
     </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    

    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
        <triggers><asp:PostBackTrigger ControlID="btn_Save_InspectionSettings"/></triggers>
        <contenttemplate>
<TABLE cellSpacing=0 cellPadding=0 width="100%" align=center border=0><TBODY><TR><TD><asp:Panel id="pnl_InspectionSettings" runat="server" Width="100%"><TABLE cellSpacing=0 cellPadding=0 width="100%"><TBODY><TR><TD style="TEXT-ALIGN: center"><FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 0px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid" class=""><%--<legend><strong>Inspection Settings</strong></legend>--%><TABLE style="TEXT-ALIGN: center" cellSpacing=0 cellPadding=0 width="100%" border=0><TBODY><TR><TD style="HEIGHT: 15px" colSpan=6><asp:HiddenField id="HiddenInspectionSettings" runat="server"></asp:HiddenField> <asp:HiddenField id="HiddenFieldStatusIcon" runat="server"></asp:HiddenField> <asp:HiddenField id="HiddenFieldStatusName" runat="server" __designer:wfdid="w2"></asp:HiddenField> </TD></TR><TR><TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>Status Name :</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtStatusName" tabIndex=1 runat="server" Width="138px" Enabled="False" CssClass="input_box"></asp:TextBox> <%--<asp:DropDownList id="ddlStatusName" tabIndex=1 runat="server" Width="142px" Enabled="False" CssClass="input_box">
                    <asp:ListItem Value="Select">&lt;Select&gt;</asp:ListItem>
                    <asp:ListItem>Closed</asp:ListItem>
                    <asp:ListItem>Done</asp:ListItem>
                    <asp:ListItem>Due</asp:ListItem>
                    <asp:ListItem>Failed FollowUp</asp:ListItem>
                    <asp:ListItem>Inspection Request</asp:ListItem>
                    <asp:ListItem>Observation</asp:ListItem>
                    <asp:ListItem Value="Over Due">Over Due</asp:ListItem>
                    <asp:ListItem>Pass FollowUp</asp:ListItem>
                    <asp:ListItem>Planned</asp:ListItem>
                    <asp:ListItem>Response</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator id="CompareValidator1" runat="server" ValueToCompare="Select" Operator="NotEqual" ErrorMessage="Required" ControlToValidate="ddlStatusName"></asp:CompareValidator>--%></TD><TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>Status Color :</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtStatusColor" tabIndex=2 runat="server" Enabled="False" CssClass="input_box" MaxLength="6"></asp:TextBox> &nbsp; <asp:ImageButton id="ImageButton2" runat="server" Width="16px" Enabled="False" ImageUrl="~/Modules/HRD/Images/color.jpg" Height="13px"></asp:ImageButton></TD><TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Status Icon :</TD><TD style="PADDING-RIGHT: 9px; TEXT-ALIGN: right"><asp:FileUpload id="FileUpload_StatusIcon" tabIndex=3 runat="server" Width="250px" Enabled="False" CssClass="input_box"></asp:FileUpload> </TD></TR><TR><TD style="PADDING-RIGHT: 15px; HEIGHT: 3px; TEXT-ALIGN: right" align=right></TD><TD style="HEIGHT: 3px; TEXT-ALIGN: left"></TD><TD style="PADDING-RIGHT: 15px; HEIGHT: 3px; TEXT-ALIGN: right" align=right></TD><TD style="HEIGHT: 3px; TEXT-ALIGN: left"></TD><TD style="HEIGHT: 3px; TEXT-ALIGN: left"></TD><TD style="HEIGHT: 3px; TEXT-ALIGN: left"></TD></TR><TR><TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>Alert Period (Days) :</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtAlertPeriod" tabIndex=4 runat="server" Width="138px" Enabled="False" CssClass="input_box" MaxLength="4"></asp:TextBox></TD><TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>Mail To :</TD><TD style="TEXT-ALIGN: left" colSpan=2><asp:TextBox id="txtMailTo" tabIndex=5 runat="server" Width="366px" Enabled="False" CssClass="input_box" MaxLength="254"></asp:TextBox> <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" __designer:wfdid="w1" ErrorMessage="Required" ControlToValidate="txtMailTo"></asp:RequiredFieldValidator> <%--<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtMailTo" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%></TD><TD style="PADDING-RIGHT: 55px"><TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="PADDING-RIGHT: 17px; TEXT-ALIGN: left"><%--<DIV style="LEFT: 0px; POSITION: relative; TOP: 0px; TEXT-ALIGN: center" id="Div1" runat="server">&nbsp;</DIV>--%><asp:Button id="btn_Cancel_InspectionSettings" tabIndex=6 onclick="btn_Cancel_InspectionSettings_Click" runat="server" Width="59px" CssClass="btn" Visible="False" CausesValidation="False" Text="Cancel"></asp:Button> <asp:Button id="btn_New_InspectionSettings" tabIndex=7 onclick="btn_New_InspectionSettings_Click" runat="server" Width="59px" CssClass="btn" Visible="False" CausesValidation="False" Text="New"></asp:Button> <asp:Button id="btn_Save_InspectionSettings" tabIndex=8 onclick="btn_Save_InspectionSettings_Click" runat="server" Width="59px" Enabled="False" CssClass="btn" Text="Save" OnClientClick="return ValidateMails();"></asp:Button>&nbsp; </TD></TR></TBODY></TABLE></TD></TR><TR><TD style="PADDING-RIGHT: 15px; HEIGHT: 5px; TEXT-ALIGN: center" align=right colSpan=6><asp:Label id="lbl_InspectionSettings_Message" runat="server" __designer:wfdid="w6" ForeColor="#C00000"></asp:Label></TD></TR></TBODY></TABLE><TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0><TBODY><TR><TD></TD></TR><TR><TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px"><DIV style="WIDTH: 100%"><%-- height: 150px--%>
    <asp:GridView id="GridView_InspSet" runat="server" Width="98%" OnRowDeleting="GridView_InspSet_RowDeleting" OnRowDataBound="GridView_InspSet_RowDataBound" OnPreRender="GridView_InspSet_PreRender" GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView_InspSet_PageIndexChanging" OnRowCommand="GridView_InspSet_RowCommand">
                                                <RowStyle CssClass="rowstyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Status Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_StatusName" runat="server" Text='<%# Eval("InspDueStatus") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hidden_InspSettingsId" runat="server" Value='<%# Eval("Id") %>' />
                                                        <asp:HiddenField ID="Hidden_StatusIcon" runat="server" Value='<%# Eval("StatusIcon") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="StatusColor" HeaderText="Color" >
                                                    <ItemStyle HorizontalAlign="Left" Width="80px"/>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Icon">
                                                    <ItemTemplate>
                                                        <img src='<%# "../" + Eval("StatusIcon") %>' height="20px" width="20px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AlertPeriod" HeaderText="Alert Period (Days)" >
                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MailTo" HeaderText="Mail To" Visible="false" >
                                                    <ItemStyle HorizontalAlign="Left"/>
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div style="width:400px;overflow-y:hidden;overflow-x:hidden;"> <%#Eval("MailTo") %>... </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="45px" />
     <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditInsGroup" CausesValidation="false" OnClick="btnEditInsGroup_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("Id")%>' />
                                                                           
                                                                        </ItemTemplate>
                                                                            </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <%--<HeaderStyle CssClass="headerstylefixedheader" />--%>
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                         </asp:GridView> </DIV></TD></TR><TR><TD style="PADDING-RIGHT: 7px; PADDING-LEFT: 16px; TEXT-ALIGN: center">
                                             <TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0><TBODY><TR><TD style="HEIGHT: 5px" colSpan=9>&nbsp; </TD></TR><TR><TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: left"><asp:Label id="lbl_GridView_InspectionSettings" runat="server"></asp:Label> </TD><TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Created By:</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtCreatedBy_InspectionSettings" tabIndex=-1 runat="server" Width="154px" CssClass="input_box" BackColor="Gainsboro" ReadOnly="True"></asp:TextBox></TD><TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Created On:</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtCreatedOn_InspectionSettings" tabIndex=-2 runat="server" Width="72px" CssClass="input_box" BackColor="Gainsboro" ReadOnly="True"></asp:TextBox></TD><TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Modified By:</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtModifiedBy_InspectionSettings" tabIndex=-3 runat="server" Width="154px" CssClass="input_box" BackColor="Gainsboro" ReadOnly="True"></asp:TextBox></TD><TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: right">Modified On:</TD><TD style="TEXT-ALIGN: left"><asp:TextBox id="txtModifiedOn_InspectionSettings" tabIndex=-4 runat="server" Width="72px" CssClass="input_box" BackColor="Gainsboro" ReadOnly="True"></asp:TextBox></TD><TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: left"><asp:Button id="btn_Print_InspectionSettings" tabIndex=9 onclick="btn_Print_InspectionSettings_Click" runat="server" Width="59px" __designer:wfdid="w9" CssClass="btn" CausesValidation="False" Text="Print" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_InspectionSettings');"></asp:Button></TD></TR></TBODY></TABLE>&nbsp;</TD></TR></TBODY></TABLE></FIELDSET>
                                         
                                          <ajaxToolkit:ColorPickerExtender id="ColorPickerExtender1" runat="server" TargetControlID="txtStatusColor" PopupButtonID="ImageButton2" SampleControlID="txtStatusColor"></ajaxToolkit:ColorPickerExtender> <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAlertPeriod" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                          
                                           <asp:HiddenField id="HiddenFieldGridRowCount" runat="server" __designer:wfdid="w3"></asp:HiddenField> </TD></TR></TBODY></TABLE></asp:Panel> </TD></TR></TBODY></TABLE>&nbsp; 
</contenttemplate>
    </asp:UpdatePanel>
   </form>
</body>
</html>
