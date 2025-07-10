<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreManagement.aspx.cs" Inherits="Purchase_Masters_StoreManagement" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/Purchase/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
        <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <script src="../JS/jquery_v1.10.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>
    <style type="text/css">
        .lnk
        {
            text-decoration:none;font-weight:bold;color:#727070;
        }
        .bordered
        {
            border-collapse:collapse;
        }
        .bordered tr td{
            border:solid 1px #f6f5f5;
            padding:4px;
        }
        .selRow
        {
            background-color:#f3de5d;
        }
        .auto-style1 {
            border-collapse: collapse;
            width: 100%;
        }
    </style>
              <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <uc2:Registers runat="server" ID="Registers1" />
    <div class="text headerband">
        Store Items Master
    </div>
     <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
      <div>
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress2">
        <ProgressTemplate>
        <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
        <center>
        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
        <img src="../Images/loading.gif" alt="loading"> Loading ...
        </div>
        </center>
        </div>
        </ProgressTemplate> 
        </asp:UpdateProgress> 
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>        
               <table style="border:#4371a5 1px solid; width: 100%" border="0" cellpadding="0" cellspacing="0">
                   <colgroup>
                       <col width="300px" />
                       <col width="400px" />
                       <col />
                       <tr>
                           <td style="border-right:solid 1px #e2d9d9">
                               <asp:UpdatePanel ID="up1" runat="server">
                                   <ContentTemplate>
                                       <table border="0" cellpadding="5" cellspacing="0" style="border:#4371a5 1px solid;width:100%;font-weight:bold;text-align:left;">
                                           <tr>
                                               <td style="padding-right:20px;height:16px;">Category
                                                   <asp:ImageButton ID="btnAddProduct" runat="server" ImageUrl="~/Modules/PMS/Images/add.png" OnClick="btnAddProduct_OnClick" style="float:right;" />
                                                   <asp:ImageButton ID="btnEditProduct" runat="server" ImageUrl="~/Modules/PMS/Images/AddPencil.gif" OnClick="btnEditProduct_OnClick" style="float:right;margin-right:5px;" Visible="false" />
                                                   <asp:HiddenField ID="hfdProductID" runat="server" />
                                                   <asp:HiddenField ID="hfdProductCode" runat="server" />
                                               </td>
                                           </tr>
                                       </table>
                                       <div style="height:428px;overflow-x:hidden;overflow-y:scroll;">
                                           <asp:TreeView ID="tvCategories" runat="server" onselectednodechanged="tvCategories_SelectedNodeChanged" OnTreeNodePopulate="tvCategories_TreeNodePopulate" ShowLines="true">
                                               <LevelStyles>
                                                   <asp:TreeNodeStyle Font-Underline="False" ForeColor="Purple" />
                                                   <asp:TreeNodeStyle Font-Underline="False" ForeColor="DarkGreen" />
                                                   <asp:TreeNodeStyle Font-Underline="False" />
                                                   <asp:TreeNodeStyle Font-Underline="False" ForeColor="Black" />
                                               </LevelStyles>
                                               <HoverNodeStyle CssClass="treehovernode" />
                                               <SelectedNodeStyle CssClass="treeselectednode" />
                                           </asp:TreeView>
                                           <asp:HiddenField ID="hfSelectedNodeID" runat="server" />
                                           <asp:HiddenField ID="hfSelectedNodeCode" runat="server" />
                                           <%--<asp:Button ID="btnSearchedCode" style="display:none" runat="server" onclick="btnSearchedCode_Click" />               --%>
                                       </div>
                                       <%-- Add Product-----------------------------------------%>
                                       <div id="divAddProduct" runat="server" style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" visible="false">
                                           <center>
                                               <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)">
                                               </div>
                                               <div style="position : relative; width: 650px; padding : 0px; text-align : center; border : solid 3px #4371a5; background : white; z-index: 150; top: 110px; opacity: 1; filter: alpha(opacity=100)">
                                                   <center>
                                                       <div class="text headerband" style="font-size:16px;font-weight:bold;padding:2px 0px 2px 0px; text-align:center;line-height:30px;">
                                                           <asp:ImageButton ID="btnClsoseProductPopup" runat="server" ImageUrl="~/Modules/PMS/Images/closewindow.png" OnClick="btnClsoseProductPopup_OnClick" style="float:right;width:24px;" />
                                                           <span style="font-size:18px;">
                                                           <asp:Label ID="lblHeadingPopup" runat="server"></asp:Label>
                                                           </span>
                                                       </div>
                                                       <div>
                                                           <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                               <tr>
                                                                   <td>
                                                           <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px; ">
                                                                       <asp:RadioButtonList ID="rdoCatSubCate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoCatSubCate_OnSelectedIndexChanged" RepeatDirection="Horizontal">
                                                                           <asp:ListItem Selected="True" Text="Category" Value="C"></asp:ListItem>
                                                                           <asp:ListItem Text="Sub Category" Value="S"></asp:ListItem>
                                                                       </asp:RadioButtonList>
                                                                   </td>
                                                               </tr>
                                                               <tr id="trSelectCategory" runat="server" visible="false" style="padding-left:10px;">
                                                                   <td style="padding:2px 0px 2px 10px; "><span style="display:block;">Select Parent Category </span>
                                                                       <asp:DropDownList ID="ddlCategoryAdd" runat="server">
                                                                       </asp:DropDownList>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px; " ><span style="display:block; ">Category Name</span>
                                                                       <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px; "><span style="display:block;">Status </span>
                                                                       <asp:DropDownList ID="ddlProductStatus" runat="server" Width="70px">
                                                                           <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                           <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                                       </asp:DropDownList>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                                       </td>
                                                                   <td style="width:250px;">
                                                                       <div style="padding:5px; background-color:#e2d9d9">Vessels (Not Applicable)</div>
                                                                       <div style="height:200px;overflow-y:scroll;overflow-x:hidden">
                                                                           <asp:CheckBoxList runat="server" ID="chkvessels"></asp:CheckBoxList>
                                                                       </div>

                                                                   </td>
                                                               </tr>
                                                               </table>
                                                       </div>
                                                       <div style="padding:5px;">
                                                           <asp:Button ID="btnSaveProduct" runat="server" OnClick="btnSaveProduct_OnClick" Text="Save" CssClass="btn" />
                                                       </div>
                                                       <div style="padding:5px;background-color:#e2d9d9;">
                                                           &nbsp;<asp:Label ID="lblMsgProduct" runat="server" CssClass="error_msg"></asp:Label>
                                                       </div>
                                                   </center>
                                               </div>
                                           </center>
                                       </div>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                           </td>
                           <td>
                               <asp:UpdatePanel ID="up3" runat="server">
                                   <ContentTemplate>
                                       <table border="0" cellpadding="5" cellspacing="0" style="border:#4371a5 1px solid;width:100%;font-weight:bold;text-align:left;">
                                           <tr>
                                               <td style="padding-right:20px;height:16px;">Products
                                                   <asp:ImageButton ID="btnAddProductLLPopup" runat="server" ImageUrl="~/Modules/PMS/Images/add.png" OnClick="btnAddProductLLPopup_OnClick" style="float:right;" Visible="false" />
                                                   <asp:ImageButton ID="btnEditProductLL" runat="server" ImageUrl="~/Modules/PMS/Images/AddPencil.gif" OnClick="btnEditProductLL_OnClick" style="float:right;margin-right:5px;" Visible="false" />
                                                   <asp:HiddenField ID="hfdProductIdLL" runat="server" />
                                                   <asp:HiddenField ID="hfdProductCodeLL" runat="server" />
                                                   <asp:Button ID="btnTempProductLL" runat="server" OnClick="btnTempProductLL_OnClick" style="display:none;" />
                                               </td>
                                           </tr>
                                       </table>
                                       <div style="overflow-x:hidden;overflow-y:scroll;">
                                           <table border="0" cellpadding="2" cellspacing="0" class="auto-style1">
                                               <tr>
                                                   <td>Code</td>
                                                   <td> :<asp:TextBox ID="txtProductCodeS" runat="server" Width="120px"></asp:TextBox></td>
                                                   <td>Name</td>
                                                   <td> :<asp:TextBox ID="txtProductNameS" runat="server" Width="180px"></asp:TextBox></td>
                                                   <td>IMPA#</td>
                                                   <td> :<asp:TextBox ID="txtIMPAS" runat="server" Width="80px"></asp:TextBox></td>
                                                   <td>Status</td>
                                                   <td> :<asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                       <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                       <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                         </asp:DropDownList></td>
                                                   <td><asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_OnClick" Text="Search"  /></td>
                                               </tr>
                                           </table>
                                       </div>
                                       <div style="overflow-x:hidden;overflow-y:scroll;">
                                           <table border="0" cellpadding="2" cellspacing="0" class="bordered" style=" width: 100%">
                                               <colgroup>
                                                   <col width="50px" />
                                                   <col width="120px" />
                                                   <col />
                                                   <col width="100px" />
                                                   <col width="100px" />
                                                   <col width="70px" />
                                                   <tr class="headerstylegrid">
                                                       <td></td>
                                                       <td>Product Code</td>
                                                       <td>Product Name</td>
                                                       <td>Unit</td>
                                                       <td>IMPA#</td>
                                                       <td>Status</td>
                                                   </tr>
                                               </colgroup>
                                           </table>
                                       </div>
                                       <div style="height:369px;overflow-x:hidden;overflow-y:scroll;">
                                           <table border="0" cellpadding="2" cellspacing="0" class="bordered" style=" width: 100%">
                                               <colgroup>
                                                   <col width="50px" />
                                                   <col width="120px" />
                                                   <col />
                                                   <col width="100px" />
                                                   <col width="100px" />
                                                   <col width="70px" />
                                               </colgroup>
                                         
                                           <asp:Repeater ID="rptProductLL" runat="server">
                                               <ItemTemplate>
                                                   <tr class='<%#((Common.CastAsInt32( Eval("PID"))==Common.CastAsInt32( hfdProductIdLL.Value))?"selRow":"")%>'>
                                                       <td style="width:30px">
                                                           <div runat="server" visible='<%# (Location=="O") %>'>
                                                               <input type="radio" name="productLL" <%#((Common.CastAsInt32( Eval("PID"))==Common.CastAsInt32( hfdProductIdLL.Value))?"checked":"")%>  onclick='SetProductIDLL(<%#Eval("PID")%>,"<%# Eval("PCode").ToString()%>")' />
                                                           </div>
                                                       </td>
                                                       <td align="left"><%# Eval("PCode") %></td>
                                                       <td align="left"><%# Eval("Pname") %></td>
                                                       <td align="left"><%#Eval("UnitName") %></td>
                                                       <td align="left"><%#Eval("impano") %></td>
                                                       <td align="left"><%#Eval("StatusName") %></td>
                                                   </tr>
                                               </ItemTemplate>
                                           </asp:Repeater>
                                          
                                           </table>
                                       </div>
                                       <%-- Add Product Last Level-----------------------------------------%>
                                       <div id="divAddProductLL" runat="server" style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" visible="false">
                                           <center>
                                               <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)">
                                               </div>
                                               <div style="position : relative; width: 500px; padding : 0px; text-align : center; border : solid 3px #4371a5; background : white; z-index: 150; top: 110px; opacity: 1; filter: alpha(opacity=100)">
                                                   <center>
                                                       <div class="text headerband" style="font-size:16px;font-weight:bold;padding:8px; text-align:center;line-height:30px;">
                                                           <asp:ImageButton ID="btnCloseProductLLPopup" runat="server" ImageUrl="~/Modules/PMS/Images/closewindow.png" OnClick="btnCloseProductLLPopup_OnClick" style="float:right;width:24px;" />
                                                           <span style="font-size:18px;">Add Product</span>
                                                       </div>
                                                       <div>
                                                           <div style="background-color:#e2d9d9;color:#2b2929;font-weight:bold;padding:2px 0px 2px 10px;">
                                                               <asp:Label ID="lblParentSubCategory" runat="server"></asp:Label>
                                                           </div>
                                                           <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px;"><span style="display:block;">Product Name</span>
                                                                       <asp:TextBox ID="txtProductNameLL" runat="server"></asp:TextBox>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px;"><span style="display:block;">Unit</span>
                                                                       <asp:DropDownList ID="ddlProductUnit" runat="server">
                                                                       </asp:DropDownList>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px;"><span style="display:block;">Status </span>
                                                                       <asp:DropDownList ID="ddlProductStatusLL" runat="server" Width="70px">
                                                                           <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                           <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                                       </asp:DropDownList>
                                                                   </td>
                                                               </tr>
                                                               <tr>
                                                                   <td style="padding:2px 0px 2px 10px;"><span style="display:block;">IMPA </span>
                                                                       <asp:TextBox ID="txtimpa" runat="server"></asp:TextBox>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </div>
                                                       <div style="padding:5px;">
                                                           <asp:Button ID="btnSaveProductLL" runat="server" OnClick="btnSaveProductLL_OnClick" Text="Save" CssClass="btn" />
                                                       </div>
                                                       <div style="padding:5px;background-color:#e2d9d9;">
                                                           &nbsp;<asp:Label ID="lblMSgProductLL" runat="server" CssClass="error_msg"></asp:Label>
                                                       </div>
                                                   </center>
                                               </div>
                                           </center>
                                       </div>
                                   </ContentTemplate>
                                   <Triggers>
                                      
                                   </Triggers>
                               </asp:UpdatePanel>
                           </td>
                       </tr>
                   </colgroup>
               </table> 
        </ContentTemplate>
        </asp:UpdatePanel>
      </div>

     </div>     
        <script type="text/javascript">
            function SetProductID(PID,PCode)
            {
                document.getElementById("ctl00_ContentMainMaster_hfdProductID").value = PID;
                document.getElementById("ctl00_ContentMainMaster_hfdProductCode").value = PCode;
                document.getElementById("ctl00_ContentMainMaster_TempProductClick").click();
                //$("#hfdProductID").val(PID);
                //$("#hfdProductCode").val(PCode);
                //$("#TempProductClick").click();
            }
            function SetSubProductID(PID,PCode)
            {
                document.getElementById("ctl00_ContentMainMaster_hfdSubPID").value = PID;
                document.getElementById("ctl00_ContentMainMaster_hfdSunPCode").value = PCode;
                document.getElementById("ctl00_ContentMainMaster_btnTempClickSubProduct").click();
                //$("#hfdSubPID").val(PID);
                //$("#hfdSunPCode").val(PCode);
                //$("#btnTempClickSubProduct").click();
            }
            function SetProductIDLL(PID,PCode)
            {
                document.getElementById("ctl00_ContentMainMaster_hfdProductIdLL").value = PID;
              //  alert(document.getElementById("ctl00_ContentMainMaster_hfdProductIdLL").value);
                document.getElementById("ctl00_ContentMainMaster_hfdProductCodeLL").value = PCode;
                document.getElementById("ctl00_ContentMainMaster_btnTempProductLL").click();
            }
            
            
        </script>
</asp:Content>

