<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselCertificates.ascx.cs" Inherits="VesselCertificates" %>
<style type="text/css">
    .style1
    {
        height: 26px;
    }
    .style2
    {
        width: 135px;
        height: 26px;
    }
    .style3
    {
        width: 341px;
        height: 26px;
    }
    .style4
    {
        width: 341px;
    }
</style>
  <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table cellpadding="0" cellspacing="0" width="500px">
<tr>
                       <td style="text-align:center">
<script type="text/javascript">
 function ShowPrint()
 {
    var str;
    str='<% Response.Write(this.VesselId.ToString()); %>';
    window.open('../Reporting/VesselCrewDocuments.aspx?VID=' + str);
 }
 </script>
                        <asp:Label ID="lbl_message_documents" runat="server" 
                               Text="" Visible="true" ForeColor="#C00000" 
                               meta:resourcekey="lbl_message_documentsResource1"></asp:Label>
                       </td>
                      </tr>
 <tr><td> 
      <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
             <legend><strong>Vessel Details</strong></legend>
               <table cellpadding="0" cellspacing="0" width="500px" style="font-family:Arial;font-size:12px;">
                    <tr>
                      <td colspan="6">
                          &nbsp;</td>
                        <td colspan="1">
                        </td>
                    </tr>
                      <tr>
                    <td align="right" style="width: 17%; height: 15px; text-align: right">
                        Vessel Name:</td>
                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                              <asp:TextBox ID="txtVesselName" runat="server" CssClass="input_box" ReadOnly="True"
                                  Width="225px" MaxLength="49" meta:resourcekey="txtVesselNameResource1"></asp:TextBox></td>
                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                              Former Name:</td>
                    <td style="padding-left: 5px; width: 16%; height: 15px; text-align: left">
                        <asp:TextBox ID="txtFormerVesselName" runat="server" CssClass="input_box" ReadOnly="True"
                                  Width="198px" MaxLength="49" 
                            meta:resourcekey="txtFormerVesselNameResource1"></asp:TextBox></td>
                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px; text-align: right">
                              Flag:</td>
                    <td style="padding-left: 5px; width: 30%; height: 15px; text-align: left">
                             <asp:DropDownList ID="ddlFlagStateName" runat="server" CssClass="input_box" 
                                 Width="213px" meta:resourcekey="ddlFlagStateNameResource1"></asp:DropDownList></td>
                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                    </td>
                </tr>
                   <tr>
                       <td align="right" style="width: 17%; height: 15px; text-align: right">
                       </td>
                       <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                           &nbsp;</td>
                       <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                       </td>
                       <td style="padding-left: 5px; width: 16%; height: 15px; text-align: left">
                       </td>
                       <td align="right" style="padding-left: 5px; width: 12%; height: 15px; text-align: right">
                       </td>
                       <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                       </td>
                       <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                       </td>
                   </tr>
                  </table>
            </fieldset></td></tr>
                                   <tr><td>
                          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" 
                                           MaskType="Date" TargetControlID="txt_IssueDate" CultureAMPMPlaceholder="" 
                                           CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                           CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                           CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
            </ajaxToolkit:MaskedEditExtender>
                          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" 
                                           MaskType="Date" TargetControlID="txt_ExpDate" CultureAMPMPlaceholder="" 
                                           CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                           CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                           CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
            </ajaxToolkit:MaskedEditExtender>
                          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" 
                                           MaskType="Date" TargetControlID="txt_NSD" CultureAMPMPlaceholder="" 
                                           CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                           CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                           CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                PopupButtonID="ImageButton2" PopupPosition="TopLeft" TargetControlID="txt_IssueDate" Enabled="True">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy"
                PopupButtonID="ImageButton3" PopupPosition="TopLeft" TargetControlID="txt_ExpDate" Enabled="True">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy"
                PopupButtonID="ImageButton6" PopupPosition="TopLeft" TargetControlID="txt_NSD" Enabled="True">
            </ajaxToolkit:CalendarExtender>
                                       </td></tr>
    <tr>
    
        <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                <legend><strong>Certificates</strong></legend>
                <asp:Label ID="lbl_VesselDoc" runat="server" Text="Label" 
                    meta:resourcekey="lbl_VesselDocResource1"></asp:Label>
                <div style="padding-top:5px;overflow-y:scroll;overflow-x:hidden;height:200px; width:100%" >
                 <asp:GridView ID="gv_VDoc" runat="server" AutoGenerateColumns="False" 
                                                                    
                        OnPreRender="gv_VDoc_PreRender" OnRowDeleting="gv_VDoc_Row_Deleting" OnRowEditing="gv_VDoc_Row_Editing"
                                                                    
                        OnSelectedIndexChanged="gv_VDoc_SelectIndexChanged" Style="text-align: center"
                                                                    Width="97%" 
                        GridLines="Horizontal" meta:resourcekey="gv_VDocResource1">
                                                                    
                                                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                    <PagerStyle CssClass="pagerstyle" />
                                                                    <RowStyle CssClass="rowstyle" />
                                                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                            ShowSelectButton="True" meta:resourcekey="CommandFieldResource1">
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:CommandField>
                                                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                            ShowEditButton="True" meta:resourcekey="CommandFieldResource2">
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:CommandField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" 
                                                                            meta:resourcekey="TemplateFieldResource1">
                                                                            <ItemStyle Width="30px" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                    ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                    Text="Delete" meta:resourcekey="ImageButton1Resource1" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Certificate Name" 
                                                                            meta:resourcekey="TemplateFieldResource2">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Doc_Type" runat="server" 
                                                                                    Text='<%# Eval("CertificateName") %>' meta:resourcekey="lbl_Doc_TypeResource1"></asp:Label>
                                                                                <asp:HiddenField ID="HiddenId" runat="server" 
                                                                                    Value='<%# Eval("VesselCertId") %>'></asp:HiddenField>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="CertificateNumber" HeaderText="Certificate Number" 
                                                                            meta:resourcekey="BoundFieldResource1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" 
                                                                            meta:resourcekey="BoundFieldResource2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                        <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" 
                                                                            meta:resourcekey="BoundFieldResource3"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                </div>
            </fieldset>
            &nbsp;<br />
            <asp:Panel ID="pnl_Documents" runat="server" Width="100%" 
                meta:resourcekey="pnl_DocumentsResource1">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                <legend><strong>Certificates Details</strong></legend>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;<asp:HiddenField ID="HiddenDocId" runat="server" />
                        </td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td class="style4">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Certificate Name:&nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_Cert" runat="server" CssClass="required_box" 
                                MaxLength="255" Width="297px" ></asp:TextBox>
                        </td>
                        <td style="width: 135px; text-align: right;">
                            Certificate Number :</td>
                        <td style="text-align: left;" class="style4">
                            <asp:TextBox ID="txt_CertNumber" runat="server" Width="300px" MaxLength="255" 
                                CssClass="required_box" meta:resourcekey="txt_CertNumberResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left">
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txt_Cert" ErrorMessage="Required" 
                                meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td style="width: 135px">
                        </td>
                        <td style="text-align: left;" class="style4">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txt_CertNumber" ErrorMessage="Required" 
                                meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Issue Date :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_IssueDate" runat="server" CssClass="input_box" 
                                MaxLength="10" TabIndex="1" Width="111px" 
                                meta:resourcekey="txt_IssueDateResource1"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" 
                                meta:resourcekey="ImageButton2Resource1" />
                            &nbsp;
                        </td>
                        <td style="width: 135px; text-align: right;">
                            Expiry Date :</td>
                        <td style="text-align: left;" class="style4">
                            <asp:TextBox ID="txt_ExpDate" runat="server" CssClass="input_box" 
                                MaxLength="10" TabIndex="1" Width="111px" 
                                meta:resourcekey="txt_ExpDateResource1"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" 
                                meta:resourcekey="ImageButton3Resource1" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:CompareValidator ID="CompareValidator10" runat="server" 
                                ControlToValidate="txt_IssueDate" Display="Dynamic" 
                                ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date" 
                                meta:resourcekey="CompareValidator10Resource1"></asp:CompareValidator>
                        </td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td style="text-align: left;" class="style4">
                            <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                ControlToValidate="txt_ExpDate" Display="Dynamic" ErrorMessage="Invalid Date." 
                                Operator="DataTypeCheck" Type="Date" 
                                meta:resourcekey="CompareValidator11Resource1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Issued By :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_IssueBy" CssClass="input_box" runat="server" Width="291px" MaxLength="255" 
                                meta:resourcekey="txt_IssueByResource1"></asp:TextBox>
                        </td>
                        <td style="width: 135px; text-align: right;">
                            Place Issued:</td>
                        <td style="text-align: left;" class="style4">
                            <asp:TextBox ID="txt_PlaceIssued" CssClass="input_box" runat="server" 
                                Width="296px" MaxLength="50" 
                                meta:resourcekey="txt_PlaceIssuedResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            Cert. Type :</td>
                        <td style="text-align: left" class="style1">
                            <asp:DropDownList ID="ddl_CertType" runat="server" CssClass="input_box" 
                                Width="199px" meta:resourcekey="ddl_CertTypeResource1">
                            <asp:ListItem Text ="Provisional" Value ="P" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Text ="Interim" Value ="I" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            <asp:ListItem Text ="Fullterm" Value ="F" meta:resourcekey="ListItemResource3"></asp:ListItem>
                            <asp:ListItem Text ="Conditional" Value ="C" meta:resourcekey="ListItemResource4"></asp:ListItem>
                            <asp:ListItem Text ="Permanent" Value ="A" meta:resourcekey="ListItemResource5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;" class="style2">
                            Next Survey Interval :</td>
                        <td style="text-align: left;" class="style3">
                            <asp:TextBox ID="txt_NextSurInt" CssClass="input_box" runat="server" 
                                MaxLength="2" Width="36px" AutoPostBack="True"  
                                ontextchanged="txt_NextSurInt_TextChanged" 
                                meta:resourcekey="txt_NextSurIntResource1"></asp:TextBox>
                            &nbsp;(month)</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Next Survey Due :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_NSD" runat="server" CssClass="input_box" 
                                MaxLength="10" TabIndex="1" Width="111px" 
                                meta:resourcekey="txt_NSDResource1"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton6" runat="server" 
                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" 
                                meta:resourcekey="ImageButton6Resource1" />
                            &nbsp;
                        </td>
                        <td style="width: 135px; text-align: right;">
                            Alert for Survey :</td>
                        <td style="text-align: left;" class="style4">
                            <asp:TextBox ID="txt_AFS" runat="server" CssClass="input_box" 
                                MaxLength="3" TabIndex="1" Width="36px" 
                                meta:resourcekey="txt_AFSResource1"></asp:TextBox>
                            &nbsp; (days)</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            <asp:CompareValidator ID="CompareValidator12" runat="server" 
                                ControlToValidate="txt_NSD" Display="Dynamic" ErrorMessage="Invalid Date." 
                                Operator="DataTypeCheck" Type="Date" 
                                meta:resourcekey="CompareValidator12Resource1"></asp:CompareValidator>
                        </td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td style="text-align: left;" class="style4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Alert for Expiry :</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_AFE" runat="server" CssClass="input_box" 
                                MaxLength="3" TabIndex="1" Width="36px" 
                                meta:resourcekey="txt_AFEResource1"></asp:TextBox>
                            &nbsp; (days)</td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                    </tr>
                </table>
            </fieldset>
            </asp:Panel>
        </td>
    </tr>
    <tr><td>
    
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_NSI0" runat="server" 
            TargetControlID="txt_NextSurInt" FilterType="Custom, Numbers" 
            ValidChars="0123456789" Enabled="True" ></ajaxToolkit:FilteredTextBoxExtender>

                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_AFS" runat="server" 
            TargetControlID="txt_AFS" FilterType="Custom, Numbers" 
            ValidChars="0123456789" Enabled="True" ></ajaxToolkit:FilteredTextBoxExtender>

                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_AFE" runat="server" 
            TargetControlID="txt_AFE" FilterType="Custom, Numbers" 
            ValidChars="0123456789" Enabled="True" ></ajaxToolkit:FilteredTextBoxExtender>
            
            <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txt_Cert" ServicePath="../WebService.asmx" ServiceMethod="GetCertificates" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></ajaxToolkit:AutoCompleteExtender>
            
    </td></tr>
    <tr>
        <td style="height: 24px; text-align: right">
            <asp:Button ID="btn_Add" runat="server" CssClass="btn" Text="Add" Width="59px" 
                OnClick="btn_Add_Click" CausesValidation="False" 
                meta:resourcekey="btn_AddResource1" />
            <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" 
                Width="59px" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Cancel" 
                Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" 
                meta:resourcekey="btn_CancelResource1" />
            <asp:Button ID="btn_Print" runat="server" CausesValidation="False" 
                CssClass="btn" OnClientClick="javascript:ShowPrint();return false;"
                Text="Print" Width="59px" meta:resourcekey="btn_PrintResource1" /></td>
    </tr>
</table>
