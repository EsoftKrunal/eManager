<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CountryDetails.aspx.cs" Inherits="Registers_CountryDetails" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>Country Details</title>
        <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
        <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    </head>
    <body>
        &nbsp;<form id="form1" runat="server">--%>
    <%--<div>--%>
    
      <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        
        
       <%-- <asp:UpdatePanel ID="up1" runat="server"><ContentTemplate>--%>
        <div style="text-align: center">
            <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Country"></asp:Label></td>
    </tr> 
                <tr>
                    <td style="text-align: center;">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Country Details</strong></legend>
                    <table width="100%"><tr><td style=" padding-top:5px;">
                       <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
    <asp:GridView ID="gvDocument" runat="server" DataKeyNames="CountryId" AutoGenerateColumns="False" Width="98%" OnSelectedIndexChanged="gvDocument_SelectedIndexChanged" GridLines="Horizontal" OnRowEditing="gvDocument_RowEditing" OnPreRender="gvDocument_PreRender" OnRowDataBound="gvDocument_RowDataBound" OnRowDeleting="gvDocument_RowDeleting" OnRowCommand="gvDocument_RowCommand" >
                        <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" HeaderText="View" >
                        <ItemStyle Width="40px" /></asp:CommandField>
                         
                         <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                                                                    <ItemStyle Width="38px" />
                                                                                </asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditCountry" CausesValidation="false" OnClick="btnEditCountry_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("CountryId")%>' />
                                    <asp:HiddenField ID="hdnCountryId" runat="server" Value='<%#Eval("CountryId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                    <ItemStyle Width="40px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField> 
                               <asp:BoundField DataField="CountryCode" HeaderText="Country Code"  >
                                   <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="CountryName" HeaderText="Country">
                                   <ItemStyle HorizontalAlign="Left" Width="200px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="NationalityCode" HeaderText="Nationality Code" >
                                   <ItemStyle HorizontalAlign="Left" Width="100px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="NationalityName" HeaderText="Nationality" >
                                   <ItemStyle HorizontalAlign="Left" Width="160px" />
                               </asp:BoundField>
                                <%--<asp:BoundField DataField="CreatedBy" HeaderText="Created By" >
                                   <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:MM/dd/yyyy}" >
                                   <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" >
                                   <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                                <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On" >
                                   <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>--%>
                               <asp:TemplateField HeaderText="Status">
                               <ItemTemplate>
                               <asp:Label ID="lblsname" runat="server" Text='<%#Eval("StatusName") %>'></asp:Label>
                               <asp:Label ID="lblsid" Visible="false" runat="server" text='<%#Eval("StatusId") %>'></asp:Label>
                               <asp:Label ID="lblcby" Visible="false" runat="server" text='<%#Eval("CreatedBy") %>'></asp:Label>
                               <asp:Label ID="lblcon" Visible="false" runat="server" text='<%#Eval("CreatedOn") %>'></asp:Label>
                               <asp:Label ID="lblmby" Visible="false" runat="server" text='<%#Eval("ModifiedBy") %>'></asp:Label>
                               <asp:Label ID="lblmon" Visible="false" runat="server" text='<%#Eval("ModifiedOn") %>'></asp:Label>
                                <asp:HiddenField ID="HiddenCountryName" runat="server" Value='<%#Eval("CountryName")%>' />
                                 <asp:HiddenField ID="HiddenCountryId" runat="server" Value='<%#Eval("CountryId")%>' />
                               </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="60px"/>
                               </asp:TemplateField>
                               
                              </Columns>
                        <%--<PagerTemplate>
                            Page 
                            <asp:TextBox ID="txtGoToPage" runat="server" AutoPostBack="true"  CssClass="gotopage" Width="17px" />
                            of
                            <asp:Label ID="lblTotalNumberOfPages" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" CommandName="Page" ToolTip="Previous Page" CommandArgument="Prev" CssClass="previous" CausesValidation="False" Text="" />
                            <asp:Button ID="Button2" runat="server" CommandName="Page" ToolTip="Next Page" CommandArgument="Next" CssClass="next" CausesValidation="False" Text="" />
                        </PagerTemplate>--%>
                    </asp:GridView>
                    </div>
                    <asp:Label ID="lblcountry" runat="server" Text=""></asp:Label></td></tr>
                    </table>
         <asp:Label ID="lbl_Country_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                       
         <asp:Panel id="pnl_country" runat="server">
         
             &nbsp;<table border="0" cellpadding="0" cellspacing="0" style="padding-left: 0px; padding-right: 0px;
                                        text-align: center" width="100%">
                                        <tr id="trfields" runat="server"><td style="width:100%">
                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:7px">
                    <legend><strong>Country &nbsp;Details</strong></legend>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                            </tr>
            <tr>
                <td align="right" style="text-align: right; padding-right:15px;">
                    Country Code:</td>
                <td style="padding-left: 5px; text-align: left">
                    <asp:TextBox ID="txtcountrycode" runat="server" CssClass="required_box" Width="200px" MaxLength="5" TabIndex="1"></asp:TextBox>
                    </td>
                <td align="right" style="text-align: right; padding-right:15px;">
                    Country:</td>
                <td style="padding-left: 5px; text-align: left">
                    <asp:TextBox ID="txtcountryname" runat="server" CssClass="required_box" Width="200px" MaxLength="50" TabIndex="2"></asp:TextBox>
                    </td>
               </tr>
                                            <tr>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcountrycode"
                        ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcountryname"
                        ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="text-align: right; padding-right:15px;">
                                                    Nationality:</td>
                                                <td style="padding-left: 5px;  text-align: left">
                    <asp:TextBox ID="txtnationalitycode" runat="server" CssClass="required_box" Width="200px" MaxLength="50" TabIndex="3"></asp:TextBox></td>
                                                <td align="right" style="text-align: right; padding-right:15px;">
                                                    Nationality Code:</td>
                                                <td style="padding-left: 5px; text-align: left">
                    <asp:TextBox ID="txtnationalityname" runat="server" CssClass="required_box" Width="200px" MaxLength="10" TabIndex="4"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtnationalitycode"
                        ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnationalityname"
                        ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                            </tr>
                    <tr>
                <td align="right" style="text-align: right; padding-right:15px;">
                    Created By:</td>
                <td style="padding-left: 5px; text-align: left">
                    <asp:TextBox ID="txtcreatedby" runat="server" CssClass="input_box" Width="200px"  ReadOnly="true" TabIndex="-1"></asp:TextBox></td>
                 <td align="right" style="text-align: right; padding-right:15px;">
                    Created On:</td>
                <td style="padding-left: 5px; text-align: left">
                    <asp:TextBox ID="txtcreatedon" runat="server" CssClass="input_box" Width="200px"  ReadOnly="true" TabIndex="-1"></asp:TextBox></td>
            </tr>
                                            <tr>
                                                <td align="right" style="padding-left: 5px; text-align: left;">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    &nbsp;
                    </td>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                            </tr>
             <tr>
                <td align="right" style="text-align: right; padding-right:15px;">
                    Modified By:</td>
                <td style="padding-left: 5px;  text-align: left">
                    <asp:TextBox ID="txtmodifiedby" runat="server" CssClass="input_box" Width="200px"  ReadOnly="true" TabIndex="-1"></asp:TextBox></td>
                <td align="right" style="text-align: right; padding-right:15px;">
                   Modified On:</td>
                <td style="padding-left: 5px; text-align: left">
                    <asp:TextBox ID="txtmodifiedon" runat="server" CssClass="input_box" Width="200px"  ReadOnly="true" TabIndex="-1" ></asp:TextBox></td>
            </tr>
                                            <tr>
                                                <td align="right" style="padding-left: 5px; text-align: left">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                            </tr>
                                            <tr style="padding-bottom:3px">
                                                <td align="right" style="text-align: right; padding-right:15px;">
                     Status:</td>
                                                <td style="padding-left: 5px; text-align: left">
                     <asp:DropDownList ID="ddlStatus_Country" runat="server" Width="205px" CssClass="input_box" TabIndex="5">
                     </asp:DropDownList></td>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                            </tr>
            </table>
             </fieldset>
                                            &nbsp;</td></tr>    </table>
       </asp:Panel>
        <table width="100%">
            <tr>
                <td align="center" colspan="6" style="text-align: right; padding-bottom:1px">
                    <asp:Button ID="btn_Add" runat="server" Width="59px" Text="Add" CssClass="btn" OnClick="btn_Add_Click" CausesValidation="False" TabIndex="6"  />
                    <asp:Button ID="btn_Save"
                        runat="server" Text="Save" CssClass="btn" Width="59px" OnClick="btn_Save_Click" TabIndex="7"  />
                         <asp:Button ID="btn_cancel"
                        runat="server" Text="Cancel" CssClass="btn" Width="59px" OnClick="btn_cancel_Click" CausesValidation="False" TabIndex="8"  />
                    <asp:Button ID="btn_Print"
                        runat="server" Text="Print" CssClass="btn" Width="59px" CausesValidation="False" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_country');" Visible="False" TabIndex="9"  />
                        <asp:HiddenField ID="hcid" runat="server" />
                        </td>
            </tr></table>
    
                    </td>
                </tr>
            </table>
        </div>
      <%--  </ContentTemplate></asp:UpdatePanel>--%>
        
        <%--<table style="width: 755px">
            <tr>
                <td style="width: 100px">--%>
        <%--</div>--%>
   </asp:Content>             
  <%--  </form>
</body>
</html>--%>
