<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselBudget.ascx.cs" Inherits="VesselBudget" %>
 <div style="text-align: center">
 <script type="text/javascript">
 function ShowPrint()
 {
    var str;
    str='<% Response.Write(this.Vesselid.ToString()); %>';
    str1='<% Response.Write(ddl_B_Year.SelectedValue); %>';
    window.open('../Reporting/Vessel_Budget_Report.aspx?VID=' + str + '&YEAR=' + str1);
 }
 </script>
     <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
 </div>
 <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
 <center>
<asp:Label ID="lbl_message_budget" runat="server" Text="Record Successfully Saved." Visible="False" ForeColor="#C00000"></asp:Label>
</center>
<table width="500px" cellpadding="0" cellspacing="0" border="1" style="font-family:Arial;font-size:12px;" >
   <tr>
        <td style="width: 100%; background-color :#e2e2e2">
           <table cellpadding="0" cellspacing="0" width="100%" border="0" style="height: 30px; padding-right :10px;">
              <tr>
                 <td style ="padding-left :30px; text-align :right " >Vessel Name:</td>
                 <td style=" text-align :left "><asp:TextBox ID="txtVesselName" ReadOnly="true" BackColor="#e2e2e2" runat="server" CssClass="input_box" MaxLength="49" Width="184px" TabIndex="1"></asp:TextBox></td>
                 <td style=" text-align :right ">Former Name:</td>
                 <td style=" text-align :left "><asp:TextBox ID="txtFormerVesselName" ReadOnly="true" BackColor="#e2e2e2" runat="server" CssClass="input_box" MaxLength="49" Width="122px"></asp:TextBox></td>
                 <td style=" text-align :right ">Flag:</td>
                 <td style=" text-align :left "><asp:DropDownList ID="ddlFlagStateName" Enabled="false" BackColor="#e2e2e2" runat="server" CssClass="input_box" Width="128px" TabIndex="2"></asp:DropDownList></td>
            </tr>
           </table>
        </td>
    </tr>          
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 508px;" valign="top">
                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Vessel Budget</strong></legend>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td align="right" colspan="6" style="width: 128px; height: 1px; text-align: right">
                                        &nbsp;</td>
                                    <td align="right" colspan="1" style="width: 122px; height: 1px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 113px; height: 1px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 77px; height: 1px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 61px; height: 1px; text-align: right">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="height: 5px; text-align: right; width: 128px;">
                                        Year:</td>
                                    <td align="right" colspan="1" style="width: 122px; height: 5px; text-align: left"><asp:DropDownList ID="ddl_B_Year" AutoPostBack="true" runat="server" CssClass="required_box" Width="111px" OnSelectedIndexChanged="ddl_B_Year_SelectedIndexChanged" TabIndex="1">
                                    </asp:DropDownList>&nbsp;</td>
                                    <td align="right" colspan="1" style="width: 113px; height: 5px; text-align: right">
                                        Budget Type:&nbsp;</td>
                                    <td align="right" colspan="1" style="width: 77px; height: 5px; text-align: right">
                                        <asp:DropDownList ID="ddlBudgetType" runat="server" CssClass="required_box" Width="112px" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlBudgetType_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td align="right" colspan="1" style="width: 61px; height: 5px; text-align: right">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="width: 128px; height: 1px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 122px; height: 1px; text-align: left">

                                    </td>
                                    <td align="right" colspan="1" style="width: 113px; height: 1px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 77px; height: 1px; text-align: left">
                                        <asp:RangeValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBudgetType" ErrorMessage="Required." MinimumValue="1" MaximumValue="9999" Type="Integer" ValidationGroup="SAVE"></asp:RangeValidator></td>
                                    <td align="right" colspan="1" style="width: 61px; height: 1px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="width: 128px; height: 5px; text-align: right">
                                        From Month:</td>
                                    <td align="right" colspan="1" style="width: 122px; height: 5px; text-align: left">
                                    <asp:DropDownList ID="ddl_FromMonth" runat="server" CssClass="required_box" Width="111px" TabIndex="1">
                                        <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList></td>
                                    <td align="right" colspan="1" style="width: 113px; height: 5px; text-align: right">
                                        To Month:&nbsp;
                                    </td>
                                    <td align="right" colspan="1" style="height: 5px; text-align: left">
                                    <asp:DropDownList ID="ddl_ToMonth" runat="server" CssClass="required_box" Width="111px" TabIndex="1">
                                    <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList></td>
                                    <td align="right" colspan="1" style="width: 61px; height: 5px; text-align: left">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="width: 128px; height: 5px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 122px; height: 5px; text-align: left">
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddl_FromMonth"
                                            ErrorMessage="Required." MaximumValue="9999" MinimumValue="1" Type="Integer"
                                            ValidationGroup="SAVE"></asp:RangeValidator></td>
                                    <td align="right" colspan="1" style="width: 113px; height: 5px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="height: 5px; text-align: left">
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_ToMonth"
                                            ErrorMessage="Required." MaximumValue="9999" MinimumValue="1" Type="Integer"
                                            ValidationGroup="SAVE"></asp:RangeValidator></td>
                                    <td align="right" colspan="1" style="width: 61px; height: 5px; text-align: left">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="height: 93px" valign="middle">
                        &nbsp;</td>
                    <td valign="top">
                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Copy Budget Details</strong></legend>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td align="right" colspan="6" style="width: 162px; height: 1px; text-align: right">
                                        &nbsp;</td>
                                    <td align="right" colspan="1" style="height: 1px; text-align: left; width: 251px;">
                                    </td>
                                    <td align="right" colspan="1" style="height: 1px; text-align: left; width: 110px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="height: 15px; text-align: right; width: 162px;">
                                        Vessel Name:&nbsp;</td>
                                    <td align="right" colspan="1" style="height: 15px; text-align: left; width: 251px;">
                                        <asp:DropDownList ID="ddlVessel" runat="server" CssClass="required_box" Width="161px" TabIndex="5">
                                        </asp:DropDownList></td>
                                    <td align="right" colspan="1" style="height: 15px; text-align: left; width: 110px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="width: 162px; height: 1px; text-align: right">
                                    </td>
                                    <td align="right" colspan="1" style="width: 251px; height: 1px; text-align: left">
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlVessel"
                                            ErrorMessage="Required." MaximumValue="9999" MinimumValue="1" Type="Integer"
                                            ValidationGroup="COPY"></asp:RangeValidator></td>
                                    <td align="right" colspan="1" style="height: 1px; text-align: right; width: 110px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="width: 162px; height: 6px; text-align: right">
                                        Year: &nbsp;</td>
                                    <td align="right" colspan="1" style="width: 251px; height: 6px; text-align: left">
                                        <asp:DropDownList ID="ddlVesselYear" runat="server" CssClass="input_box" Width="80px" TabIndex="6">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnCopy" runat="server" CssClass="btn" Text="Copy" Width="59px" OnClick="btnCopy_Click" ValidationGroup="COPY" TabIndex="7" /></td>
                                    <td align="right" colspan="1" style="height: 6px; text-align: center; width: 110px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6" style="width: 162px; text-align: right">
                                        &nbsp;</td>
                                    <td align="right" colspan="1" style="width: 251px; text-align: left">
                                    </td>
                                    <td align="right" colspan="1" style="text-align: center; width: 110px;">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right" colspan="6" style="height: 15px; text-align:center; width: 100%;">
                        <div style=" width:100%; overflow-y:scroll; overflow-x:hidden;height:220px">
                        <asp:GridView ID="gv_VDoc" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" OnDataBound="gv_VDoc_DataBound">
                            <Columns>
                            <asp:BoundField DataField="accountheadnumber" HeaderText="Account Code" >
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                                <asp:TemplateField HeaderText="Account Head">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AccountName" runat="server" Text=' <%#Eval("accountheadname")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenId" runat="server" Value=' <%#Eval("accountheadid")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Amoount" runat="server" Text='<%#Eval("Amount")%>' MaxLength="7" CssClass="input_box" style=" text-align:right"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txt_Amoount">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Total Annual Budget" DataField="Total">
                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        </asp:GridView>
                        </div>
                </td>
            </tr>
                </table>
        </td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right; padding-bottom: 5px; padding-top: 15px;">
        <span style=" float : left" >
        &nbsp;Total Annual Budget :<asp:Label ID="lbl_Total" runat="server"></asp:Label>
        </span>
         <asp:Button ID="btn_Refresh" runat="server" Text="Refresh" OnClick="btn_refresh_Click" CssClass="btn" Width="80px" TabIndex="8" />
         <asp:Button ID="btn_manip" runat="server" Text="Adjust Budget" OnClick="btn_manip_Click" CssClass="btn" Width="100px" TabIndex="8" />
         <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save" CssClass="btn" Width="59px" ValidationGroup="SAVE" TabIndex="8" />
         <asp:Button ID="btn_Print" runat="server" CssClass="btn" Text="Print" OnClientClick="javascript:ShowPrint();return false;" CausesValidation="False" Width="59px" TabIndex="9" /></td>
    </tr>
 </table>
