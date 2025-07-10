<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NearestAirport.aspx.cs" Inherits="Registers_NearestAirport" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
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
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
             <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Nearest Airport "></asp:Label></td>
    </tr> 
      <tr>
    <td>
     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-bottom:10px">
                    <legend><strong>Search</strong></legend>
                    <table width="100%" style="padding-top: 3px" cellpadding="0" cellspacing="0"><tr>
                    <td style=" text-align:left; width: 144px; height: 19px;">
                        &nbsp;Airport Name :</td>
                    <td style=" text-align:left; width: 794px; height: 19px;"><asp:TextBox ID="txt_Airport" runat="server" MaxLength="30" CssClass="input_box" Width="306px" onkeydown="javascript:if(event.keyCode==13){document.getElementById('ctl00$ContentPlaceHolder1$btn_Search').focus();}"></asp:TextBox> </td>
                    <td style="width: 86px; height: 19px"><asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="input_box" OnClick="btn_Search_Click" Width="62px" /></td>
                    </tr></table>
                </fieldset>
    
    </td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Nearest Airport List</strong></legend>
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">  
                       <asp:GridView ID="GridView_NearestAirport" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="NearestAirportId" OnSelectedIndexChanged="GridView_NearestAirport_SelectedIndexChanged" OnRowEditing="GridView_NearestAirport_Row_Editing" OnRowDeleting="GridView_NearestAirport_Row_Deleting" OnPreRender="GridView_NearestAirport_PreRender" OnDataBound="GridView_NearestAirport_DataBound" OnRowCommand="GridView_NearestAirport_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="38px" /></asp:CommandField>
                             <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditNearestAirport" CausesValidation="false" OnClick="btnEditNearestAirport_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("NearestAirportId")%>' />
                                                <asp:HiddenField ID="hdnNearestAirportId" runat="server" Value='<%#Eval("NearestAirportId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Country"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblcountryname" runat="server" Text='<%#Eval("CountryName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenNearestAirportId" runat="server" Value='<%#Eval("NearestAirportId")%>' />
                                   <asp:HiddenField ID="HiddenNearestAirportName" runat="server" Value='<%#Eval("NearestAirportName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="NearestAirportName" HeaderText="Nearest Airport">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                <%--               <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
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
                        <asp:Label ID="lbl_GridView_NearestAirport" runat="server" Text=""></asp:Label>
                  <asp:Label ID="lbl_NearestAirport_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
            <asp:Panel ID="pnl_NearestAirport" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Nearest Airport Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenNearestAirport" runat="server" />
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
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 18px;">
                                                                    Country:</td>
                                                                <td style="text-align: left; height: 18px;">
                                                                    <asp:DropDownList ID="ddlCountryName" runat="server" CssClass="required_box" Width="205px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 18px;">
                                                                    Nearest Airport:</td>
                                                                <td style="text-align: left; height: 18px;">
                                                                    <asp:TextBox ID="txtNearestAirportName" runat="server" CssClass="required_box" Width="200px" TabIndex="2" MaxLength="49"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlCountryName"
                                  ErrorMessage="Required" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNearestAirportName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_NearestAirport" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_NearestAirport" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy_NearestAirport" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_NearestAirport" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
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
                              <asp:DropDownList ID="ddlStatus_NearestAirport" runat="server" CssClass="input_box" Width="205px" TabIndex="3">
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
                       <asp:Button ID="btn_Add_NearestAirport" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_NearestAirport_Click" CausesValidation="False" TabIndex="4" />
                            <asp:Button ID="btn_Save_NearestAirport" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_NearestAirport_Click" TabIndex="5" />
                            <asp:Button ID="btn_Cancel_NearestAirport" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_NearestAirport_Click" CausesValidation="False" TabIndex="6" />
                            <asp:Button ID="btn_Print_NearestAirport" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_NearestAirport_Click" TabIndex="7" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_NearestAirport');" Visible="False" />                                
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
