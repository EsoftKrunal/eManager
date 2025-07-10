<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostCentre.aspx.cs" Inherits="Registers_CostCentre" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">--%>
    <div style="text-align: center">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Cost Center "></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Cost Centre List</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                      <asp:GridView ID="GridView_CostCentre" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="CostCentreId" OnSelectedIndexChanged="GridView_CostCentre_SelectedIndexChanged" OnRowEditing="GridView_CostCentre_Row_Editing" OnRowDeleting="GridView_CostCentre_Row_Deleting" OnPreRender="GridView_CostCentre_PreRender" OnDataBound="GridView_CostCentre_DataBound"  GridLines="Horizontal" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                              <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                              <%-- <asp:BoundField DataField="CostCentreName" HeaderText="Cost Centre">
                                 <ItemStyle HorizontalAlign="Left" Width="200px" />
                               </asp:BoundField>--%>
                               <asp:TemplateField HeaderText="Cost Centre"><ItemStyle HorizontalAlign="Left" Width="200px" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblcostcentrename" runat="server" Text='<%#Eval("CostCentreName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenCostCentreId" runat="server" Value='<%#Eval("CostCentreId")%>' />
                                    <asp:HiddenField ID="HiddenCostCentreName" runat="server" Value='<%#Eval("CostCentreName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               
                              <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Left" Width="100px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Left" Width="100px" />
                               </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                         </Columns>
                             <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                      </div>
                      <asp:Label ID="lbl_GridView_CostCentre" runat="server" Text=""></asp:Label>
                  </td>
                 </tr>
                </table>        
                <asp:Label ID="lbl_CostCentre_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                &nbsp;<br />
            <asp:Panel ID="pnl_CostCentre" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Cost Centre Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenCostCentre" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px;">
                                                                    Cost Centre:</td>
                                                                <td style="text-align: left; height: 19px;">
                                                                    <asp:TextBox ID="txtCostCentreName" runat="server" CssClass="required_box" Width="200px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                                                                    </td>
                                                                <td style="text-align: left; height: 19px;">
                                                                    </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right">
                          </td>
                          <td style="height: 13px; text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCostCentreName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right">
                          </td>
                          <td style="height: 13px; text-align: left">
                              </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_CostCentre" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_CostCentre" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedBy_CostCentre" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_CostCentre" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlStatus_CostCentre" runat="server" CssClass="input_box" Width="205px" TabIndex="3">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                        </table>
                  <br />
                                                    </fieldset>
                <br />
                                                    </asp:Panel>
           <table style="width: 100%">
               <tr>
                   <td style="text-align: right">
                       <asp:Button ID="btn_Add_CostCentre" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_CostCentre_Click" CausesValidation="False" TabIndex="4" />
                       <asp:Button ID="btn_Save_CostCentre" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_CostCentre_Click" TabIndex="5" />
                       <asp:Button ID="btn_Cancel_CostCentre" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_CostCentre_Click" CausesValidation="False" TabIndex="6" />
                       <asp:Button ID="btn_Print_CostCentre" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_CostCentre_Click" TabIndex="7" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_CostCentre');" Visible="False" />                   
                  </td>
               </tr>
           </table>
                
                                                    
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>    
    </div>
    </asp:Content>
    <%--</form>
</body>
</html>--%>
