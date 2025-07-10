<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="VesselSearch.aspx.cs" Inherits="VesselRecord_VesselSearch" %>
<%@ Register TagName="menu" Src="~/Modules/HRD/UserControls/VesselMenu.ascx" TagPrefix="mtm"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/> 
    <%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server"> 
    <div style="text-align: left">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr >
            <td colspan ="2" class="text headerband">
            <img runat="server" id="imgHelp" moduleid="4" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
            Vessel Master</td>
        </tr>
        <tr>
        <td style="width:150px; text-align :left; vertical-align : top;">
        <mtm:menu runat="server" ID="menu2" />  
        </td>
        <td style=" text-align :right; vertical-align : top;" >
            <table border="0" cellpadding="0" cellspacing="0" style="background-color:#f9f9f9; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;width:100%;"   >
           
            <tr>
                <td style="width: 100%;padding :10px">
                 <asp:Button runat="server" ID="btnback" PostBackUrl="~/crewrecord/crewsearch.aspx" Text="Back" Width="80px"  CssClass="btn" CausesValidation="false"  /> &nbsp;&nbsp;&nbsp;
                   <fieldset style="display:none;border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                       <legend><strong>Search</strong></legend>
                       <table style="width: 100%" cellpadding="5" cellspacing="0">
                           <tr>
                               <td style="width: 152px; text-align: right">
                                   Vessel Type:</td>
                               <td style="width: 152px; text-align: left">
                                   <asp:DropDownList ID="ddlVesselTypeName" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselTypeName_SelectedIndexChanged" Width="190px">
                                   </asp:DropDownList></td>
                               <td style="width: 152px; text-align: right">
                                   Status:</td>
                               <td style="width: 152px; text-align: left">
                                   <asp:DropDownList ID="ddlVesselStatus" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselTypeName_SelectedIndexChanged" Width="190px">
                                   </asp:DropDownList></td>
                               <td style="width: 152px; text-align: right">
                                   Mgmt Type:</td>
                               <td style="width: 100px; text-align: left; padding-bottom:5px">
                                   <asp:DropDownList ID="ddlMgmtType" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselTypeName_SelectedIndexChanged" Width="96px">
                                   </asp:DropDownList></td>
                           </tr>
                           <tr>
                               <td style="width: 152px; text-align: right;padding-bottom:10px">
                                   Owner:</td>
                               <td style="width: 152px; text-align: left;padding-bottom:10px">
                                   <asp:DropDownList ID="ddlOwner" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselTypeName_SelectedIndexChanged" Width="190px">
                                   </asp:DropDownList></td>
                               <td style="width: 152px; text-align: right;padding-bottom:10px">
                                   Owner Pool:</td>
                               <td style="width: 152px; text-align: left;padding-bottom:10px">
                                   <asp:DropDownList ID="ddlOwnerPool" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddlVesselTypeName_SelectedIndexChanged" Width="190px">
                                   </asp:DropDownList></td>
                               <td style="width: 152px; text-align: right;padding-bottom:10px">
                               </td>
                               <td style="padding-bottom: 5px; width: 100px; text-align: left;padding-bottom:10px">
                               </td>
                           </tr>
                          
                       </table>
                   </fieldset>
                  </td>
             </tr>
             <tr style ="display:none">
                <td colspan="2" style="padding :10px; padding-top :0px;">
                 <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                  <legend><strong>Vessel</strong></legend>
                   <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                     <td style="padding-right: 10px;">
                      <asp:Label ID="lbl_GridView_VesselSearch" runat="server" Text=""></asp:Label>
                      <div style="overflow-x:hidden;overflow-y:scroll ; width: 100%; height: 290px">
                        <asp:GridView ID="GridView_VesselSearch"  runat="server" AutoGenerateColumns="False" Width="98%" OnSelectedIndexChanged="GridView_VesselSearch_SelectedIndexChanged" OnPreRender="GridView_VesselSearch_PreRender" OnDataBound="GridView_VesselSearch_DataBound" OnRowEditing="GridView_VesselSearch_Row_Editing" OnRowDeleting="GridView_VesselSearch_Row_Deleting" AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" DataKeyNames="VesselId" GridLines="Horizontal">
                         <Columns>
                           <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                            <ItemStyle Width="35px" /></asp:CommandField>
                           <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                            <ItemStyle Width="40px" /></asp:CommandField>
                           <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                             <ItemTemplate>
                               <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"  ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="VesselFullName" HeaderText="VSL" SortExpression="VesselFullName">
                             <ItemStyle HorizontalAlign="Left" Width="180px" />
                           </asp:BoundField>
                           <asp:TemplateField HeaderText="Vessel Type" SortExpression="VesselTypeName"><ItemStyle HorizontalAlign="Left" />
                           <ItemStyle HorizontalAlign="Left" Width="120px" />
                             <ItemTemplate>  
                               <asp:Label ID="lblvesseltypename" runat="server" Text='<%#Eval("VesselTypeName")%>'></asp:Label>
                                 <asp:HiddenField ID="HiddenVesselId" runat="server" Value='<%#Eval("VesselId")%>' />
                             </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="OwnerShortName" HeaderText="Owner" SortExpression="OwnerShortName">
                             <ItemStyle HorizontalAlign="Left" Width="100px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="OwnerPoolName" HeaderText="Owner Pool" SortExpression="OwnerPoolName">
                             <ItemStyle HorizontalAlign="Left" Width="120px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="VesselManagementName" HeaderText="Mgmt Type" SortExpression="VesselManagementName">
                             <ItemStyle HorizontalAlign="Left" Width="80px" />
                           </asp:BoundField>
                          </Columns>
                             <SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstylefixedheadergrid" HorizontalAlign="Left" />
                             <RowStyle CssClass="rowstyle" />
                     </asp:GridView>
                    </div>
                  </td>
                 </tr>
                </table>        
           </fieldset>
          </td>
         </tr>
            <tr style ="display:none">
          <td style="text-align: right; padding-right: 10px;">
             <asp:Button ID="btn_Add_VesselSearch" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Add_VesselSearch_Click" Text="Add Vessel" Width="80px" /><br />
              <br />
          </td>
         </tr>
        </table> 
        </td> 
        </tr> 
        </table> 
      </div>
   </asp:Content>
