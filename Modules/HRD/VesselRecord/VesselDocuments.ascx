<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselDocuments.ascx.cs" Inherits="VesselDocuments" %>
  <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<asp:Label ID="lbl_message_documents" runat="server" Text="Record Successfully Saved." Visible="False" ForeColor="#C00000"></asp:Label>
<script type="text/javascript">
 function ShowPrint()
 {
    var str;
    str='<% Response.Write(this.VesselId.ToString()); %>';
    window.open('../Reporting/VesselCrewDocuments.aspx?VID=' + str);
 }
 </script>
<table cellpadding="0" cellspacing="0" width="100%" border="1" style="font-family:Arial;font-size:12px;" >
<tr><td style=" background-color :#e2e2e2" > 
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
</td></tr>
<tr>
<td style="text-align: center;">
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
            <legend><strong>Documents</strong></legend>
            <asp:Label ID="lbl_VesselDoc" runat="server" Text="Label"></asp:Label>
            <div style="padding-top:5px;overflow-y:scroll;overflow-x:hidden;height:270px; width:100%" >
             <asp:GridView ID="gv_VDoc" runat="server" AutoGenerateColumns="False"  OnDataBound="gv_VDoc_DataBound"
                                                                OnPreRender="gv_VDoc_PreRender" OnRowDeleting="gv_VDoc_Row_Deleting" OnRowEditing="gv_VDoc_Row_Editing"
                                                                OnSelectedIndexChanged="gv_VDoc_SelectIndexChanged" Style="text-align: center"
                                                                Width="97%" GridLines="Horizontal">
                                                                
                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <RowStyle CssClass="rowstyle" />
                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                        ShowSelectButton="True">
                                                                        <ItemStyle Width="50px" />
                                                                    </asp:CommandField>
                                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                        ShowEditButton="True">
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:CommandField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                        <ItemStyle Width="30px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                Text="Delete" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Document Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Doc_Type" runat="server" Text='<%#Eval("VesselDocumentTypeName")%>'></asp:Label>
                                                                            <asp:HiddenField ID="HiddenId" runat="server" Value='<%#Eval("VesselDocumentId")%>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="HiddenDocumentTypeId" runat="server" Value='<%#Eval("VesselDocumentTypeId")%>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="HiddenDocumentNameId" runat="server" Value='<%#Eval("VesselDocumentNameId")%>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="HiddenRankId" runat="server" Value='<%#Eval("RankId")%>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="VesselDocumentName" HeaderText="Document Name"><ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="RankCode" HeaderText="Rank"><ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
            </div>
        </fieldset>
            &nbsp;<br />
            <asp:Panel ID="pnl_Documents" runat="server" Width="100%">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                <legend><strong>Documents Details</strong></legend>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;<asp:HiddenField ID="HiddenDocId" runat="server" />
                        </td>
                        <td style="width: 135px">
                            &nbsp;</td>
                        <td style="width: 463px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: right;">
                            Document Type:&nbsp;
                        </td>
                        <td style="height: 22px; text-align: left;">
                            <asp:DropDownList ID="ddl_VDocType" runat="server" CssClass="required_box" Width="199px" AutoPostBack="True" OnSelectedIndexChanged="ddl_VDocType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 22px; text-align: right; width: 135px;">
                            Document Name:&nbsp;
                        </td>
                        <td style="height: 22px; text-align: left; width: 463px;">
                            <asp:DropDownList ID="ddl_VDocName" runat="server" CssClass="input_box" Width="389px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_VDocType"
                                ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                        <td style="width: 135px">
                        </td>
                        <td style="width: 463px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Rank :&nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddl_Rank" runat="server" CssClass="required_box" Width="199px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Rank_SelectedIndexChanged" >
                            </asp:DropDownList></td>
                        <td style="width: 135px">
                        </td>
                        <td style="width: 463px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Rank"
                                ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                        <td style="width: 135px">
                        </td>
                        <td style="width: 463px">
                        </td>
                    </tr>
                </table>
            </fieldset>
            </asp:Panel>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td style="height: 24px; text-align: right">
            <asp:Button ID="btn_Add" runat="server" CssClass="btn" Text="Add" Width="59px" OnClick="btn_Add_Click" CausesValidation="False" />
            <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" />
            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" />
            <asp:Button ID="btn_Print" runat="server" CausesValidation="False" CssClass="btn" OnClientClick="javascript:ShowPrint();return false;"
                Text="Print" Width="59px" /></td>
    </tr>
</table>
