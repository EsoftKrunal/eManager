<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountHead.aspx.cs" Inherits="Registers_AccountHead" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
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
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Account Head"></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Account Head List</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                <td  style="padding:10px;">
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-bottom:10px">
                    <legend><strong>Search</strong></legend>
                    <table width="100%" style="padding-top: 3px" cellpadding="0" cellspacing="0"><tr>
                    <td style=" text-align:left; width: 192px; height: 19px;">
                        &nbsp;Account Head Name :</td>
                    <td  style=" text-align:left; width: 794px; height: 19px;">
                    <asp:TextBox ID="txt_Licence" runat="server" onkeydown="javascript:if(event.keyCode==13){document.getElementById('ctl00$ContentPlaceHolder1$btn_Search').focus();}" MaxLength="30" CssClass="input_box"></asp:TextBox> </td>
                    <td style="width: 86px; height: 19px"><asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="input_box" OnClick="btn_Search_Click" Width="62px" /></td>
                    </tr></table>
                </fieldset>
                </td>
            </tr>
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow: auto; width: 100%; height: 150px">
                    <%--<div id="div-datagrid">--%>
                       <asp:GridView ID="GridView_AccountHead" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="AccountHeadId" OnSelectedIndexChanged="GridView_AccountHead_SelectedIndexChanged" OnRowEditing="GridView_AccountHead_Row_Editing" OnRowDeleting="GridView_AccountHead_Row_Deleting" OnPreRender="GridView_AccountHead_PreRender" OnDataBound="GridView_AccountHead_DataBound" GridLines="Horizontal" OnRowCancelingEdit="GridView_AccountHead_RowCancelingEdit" OnRowCommand="GridView_AccountHead_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="30px"/></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="27px" /></asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditAccountHead" CausesValidation="false" OnClick="btnEditAccountHead_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("AccountHeadId")%>' />
                                                <asp:HiddenField ID="hdnAccountHeadId" runat="server" Value='<%#Eval("AccountHeadId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="35px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                              <%-- <asp:BoundField DataField="CostCentreGroupName" HeaderText="Cost Centre Group">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>--%>
                                  <asp:BoundField DataField="AccountHeadName" HeaderText="Name"  ItemStyle-Width="240px">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>
                               <asp:TemplateField HeaderText="A/C No.(CLS)"><ItemStyle HorizontalAlign="Left" Width="70px" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblcostcentrename" runat="server" Text='<%#Eval("AccountHeadnumberCLS")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenAccountHeadId" runat="server" Value='<%#Eval("AccountHeadId")%>' />
                                   <asp:HiddenField ID="HiddenAccountHeadname" runat="server" Value='<%#Eval("AccountHeadName")%>' />
                                  </ItemTemplate>
                                  <HeaderStyle Width="80px" />
                               </asp:TemplateField>
                              
                                <asp:BoundField DataField="AccountHeadnumberCostPlus" HeaderText="A/C No.(CostPlus)" HeaderStyle-Width="100px">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                              
                                <asp:BoundField DataField="AccountHeadType" HeaderText="Type">
                                 <ItemStyle HorizontalAlign="Left" Width="45px"/>
                               </asp:BoundField>
                               <%-- <asp:BoundField DataField="IncludeInBudgets" HeaderText="Include In Budgets">
                                 <ItemStyle HorizontalAlign="Left" Width="110px"/>
                               </asp:BoundField>--%>
                               <asp:BoundField DataField="RecoverableCost" HeaderText="Recoverable Cost" ItemStyle-Width= "110px" >
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                              <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Center" Width="90px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Center" Width="90px" />
                               </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="60px" />
                               </asp:BoundField>
                         </Columns>
                            <%-- <SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstyle" HorizontalAlign="Left" />
                             <AlternatingRowStyle CssClass="alternatingrowstyle" />--%>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_AccountHead" runat="server" Text=""></asp:Label></td>
                 </tr>
                </table>        
                <asp:Label ID="lbl_AccountHead_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                &nbsp;<br />
            <asp:Panel ID="pnl_AccountHead" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Account Head Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenAccountHead" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 202px;">
                          </td>
                          <td style="height: 13px; text-align: left; width: 170px;">
                          </td>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 223px;">
                          </td>
                          <td style="height: 13px; text-align: left; width: 193px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 202px;">
                              Account Head Number(CLS):</td>
                          <td style="height: 13px; text-align: left; width: 170px;">
                              <asp:TextBox ID="txt_cls" runat="server" CssClass="input_box" MaxLength="19" TabIndex="3"
                                  Width="200px"></asp:TextBox></td>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 223px;">
                              Account  Head Number(CostPlus):</td>
                          <td style="height: 13px; text-align: left; width: 193px;">
                              <asp:TextBox ID="txtAccountHeadNumber" runat="server" CssClass="input_box" MaxLength="19"
                                  TabIndex="3" Width="190px"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px; width: 202px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 170px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px; width: 223px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 193px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px; width: 202px;">
                              Account Head:</td>
                          <td style="text-align: left; height: 19px; width: 170px;">
                              <asp:TextBox ID="txtAccountHeadName" runat="server" CssClass="required_box" MaxLength="49"
                                  Width="200px" TabIndex="4"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px; width: 223px;">
                              Account Head Type:</td>
                          <td style="text-align: left; height: 19px; width: 193px;">
                              <asp:DropDownList ID="ddlAccountHeadType" runat="server" CssClass="input_box" Width="195px" TabIndex="5">
                              </asp:DropDownList></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 16px; width: 202px;">
                          </td>
                          <td style="text-align: left; height: 16px; width: 170px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountHeadName" runat="server"
                                  ControlToValidate="txtAccountHeadName" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 16px; width: 223px;">
                          </td>
                          <td style="text-align: left; height: 16px; width: 193px;">
                              &nbsp;
                              </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 20px; width: 202px;">
                              Include In Budget:</td>
                          <td style="text-align: left; height: 20px; width: 170px;">
                              <asp:CheckBox ID="chkIncludeInBudget" runat="server" TabIndex="6" /></td>
                          <td align="right" style="text-align: right; padding-right:15px; height: 20px; width: 223px;">
                              Recoverable Cost:</td>
                          <td style="text-align: left; height: 20px; width: 193px;"><asp:CheckBox ID="chkrecoverablecost" runat="server" TabIndex="6" /></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 202px;">
                          </td>
                          <td style="text-align: left; width: 170px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 223px;">
                          </td>
                          <td style="text-align: left; width: 193px;">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px; width: 202px;">
                                                                    Created By:</td>
                                                                <td style="text-align: left; width: 170px;">
                                                                    <asp:TextBox ID="txtCreatedBy_AccountHead" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px; width: 223px;">
                                                                    Created On:</td>
                                                                <td style="text-align: left; width: 193px;">
                                                                    <asp:TextBox ID="txtCreatedOn_AccountHead" runat="server" CssClass="input_box" ReadOnly="True" Width="190px" TabIndex="-2"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 202px;">
                          </td>
                          <td style="text-align: left; width: 170px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 223px;">
                          </td>
                          <td style="text-align: left; width: 193px;">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px; width: 202px;">
                                                                    Modified By:</td>
                                                                <td style="text-align: left; height: 19px; width: 170px;">
                                                                    <asp:TextBox ID="txtModifiedBy_AccountHead" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-3"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px; width: 223px;">
                                                                    Modified On:</td>
                                                                <td style=" text-align: left; height: 19px; width: 193px;">
                                                                    <asp:TextBox ID="txtModifiedOn_AccountHead" runat="server" CssClass="input_box" ReadOnly="True" Width="190px" TabIndex="-4"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 202px;">
                          </td>
                          <td style="text-align: left; width: 170px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 223px;">
                          </td>
                          <td style="text-align: left; width: 193px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 202px;">
                              Status:</td>
                          <td style="text-align: left; width: 170px;">
                              <asp:DropDownList ID="ddlStatus_AccountHead" runat="server" CssClass="input_box" Width="205px" TabIndex="7">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right; width: 223px;">
                          </td>
                          <td style="text-align: left; width: 193px;">
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
                       <asp:Button ID="btn_Add_AccountHead" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_AccountHead_Click" CausesValidation="False" TabIndex="8" />
                <asp:Button ID="btn_Save_AccountHead" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_AccountHead_Click" TabIndex="9" />
                <asp:Button ID="btn_Cancel_AccountHead" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_AccountHead_Click" CausesValidation="False" TabIndex="10" />
                <asp:Button ID="btn_Print_AccountHead" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_AccountHead_Click" TabIndex="11" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_AccountHead');" Visible="False" />
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
   <%-- </form>
</body>
</html>--%>
