<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselSafeManning.ascx.cs" Inherits="VesselRecord_VesselSafeManning" %>
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<center>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:Label ID="lbl_message_manning" runat="server" Text="Record Successfully Saved." Visible="False" ForeColor="#C00000"></asp:Label>
<table width="100%" cellpadding="0" cellspacing="0" border ="0">
    <tr>
        <td style=" background-color :#e2e2e2;padding:5px;" >
            <table style="width:100%" cellpadding="0" cellspacing="0">
                <col width="70px" />
                <col width="300px" />
                <col width="150px" />
                <col />
                <col width="200px" />
                
                <tr>
                    <td style="text-align:right;">
                        Rank:&nbsp;
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="dp_Rank" runat="server" CssClass="required_box" Width="98px">
                        </asp:DropDownList></td>
                    <td style="text-align:right;">
                        Safe Manning:&nbsp;
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txt_budget" runat="server" CssClass="required_box" Width="42px" MaxLength="3"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btn_Add" runat="server" CssClass="btn" OnClick="btn_Add_Click" Text="Add" Width="54px" Height="26px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;                    </td>
                    <td>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dp_Rank" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                    <td></td>
                    <td><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txt_budget"></asp:RequiredFieldValidator></td>
                    <td>
                          
                    </td>
                    <td>
                        &nbsp;</td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
           <ajaxToolkit:FilteredTextBoxExtender ID="f3" runat="server" FilterType="Numbers" TargetControlID="txt_budget"></ajaxToolkit:FilteredTextBoxExtender>
    </td>
    </tr>
            
   
    <tr>
        <td>
           <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Safe Manning</strong></legend>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" colspan="4" style="height: 15px; padding-bottom:5px;padding-top:4px; text-align: center" id="tdgrid" runat="server">
                                        <div id="div-datagrid" style="width:100%; height:205px;  "  >
                                            <asp:GridView ID="gv_VDoc" OnRowDeleting="Row_Deleting" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                        <ItemStyle Width="25px" HorizontalAlign="center" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_OnClick"  ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" CommandArgument='<%#Eval("VesselID") %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                    <ItemStyle Width="25px" HorizontalAlign="center" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />

                                                            <asp:HiddenField ID="hfManningGradeId" runat="server" Value='<%#Eval("ManningGradeId") %>' />
                                                            <asp:HiddenField ID="hfSafeManning" runat="server" Value='<%#Eval("SafeManning") %>'  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GradeName" HeaderText="Grade Name" />
                                                    <asp:BoundField DataField="SafeManning" HeaderText="Safe Mannning" />
                                                    <asp:BoundField DataField="ActualManning" HeaderText="Actual Mannning" />
                                                    <asp:BoundField DataField="ActualNationality" HeaderText="Actual Nationality" />
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
                            </fieldset>
        </td>
    </tr>
    <tr>
        <td align="left" style="height: 12px">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="height: 12px;font-weight:bold ">
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;"><legend><strong>Summary</strong></legend>
        <table width="100%" >
        <col style="text-align :right"  />
        <col style="text-align :left"  />
        <col style="text-align :right"  />
        <col style="text-align :left"  />
        <tr><td>
                    Total Safe Manning:</td>
            <td style ="text-align : left " >
                    (<asp:Label ID="lbl_Total_Safe" runat="server"></asp:Label>) Crew</td>
            <td>
                    Total Actual Manning:</td>
            <td style ="text-align : left ">
                    (<asp:Label ID="lbl_Total_Actual" runat="server"></asp:Label>) Crew</td>
        </tr>
        </table>
        </fieldset>
            </td>
    </tr>
    <tr>
        <td style="height: 19px; text-align: right; " >
         <asp:HiddenField ID="hvesselminingid" runat="server"/>
         <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save" CssClass="btn" Width="59px" Visible="False" />
         
         <asp:Button ID="btn_Print" runat="server" CssClass="btn"  Text="Print" OnClientClick="javascript:ShowPrint();return false;" CausesValidation="False" Width="59px" style="margin-top:2px;"/>
       </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
</table> 
    
</center>

