<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreManagementUncategorised.aspx.cs" Inherits="Purchase_Masters_StoreManagementUncategorised" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/Purchase/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" /> 
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <uc2:Registers runat="server" ID="Registers1" />
     <div class="text headerband">
        Uncategorised Store Items Master
    </div>
        <div style="text-align: center">
   
     
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
                   <tr>
                        <td>
                            <asp:UpdatePanel ID="up3" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="5" cellspacing="0" style="border:#4371a5 1px solid;width:100%;background-color:#4371a5;color:white;font-weight:bold;text-align:left;">
                                        <tr>
                                            <td style="padding-right:20px;height:16px;">Uncategorized Store Items<asp:HiddenField ID="hfdTableID" runat="server" /></td>
                                        </tr>
                                    </table>
                                    <div style="overflow-x:hidden;overflow-y:scroll;">
                                        <table border="0" cellpadding="0" cellspacing="0" class="bordered" style=" width: 100%">
                                            <col width="400px" />
                                            <col />
                                            <tr>
                                                <td align="left"> Name : <asp:TextBox ID="txtItemName_F" runat="server" Width="180px" ></asp:TextBox> </td>                                                
                                                <td align="left"> IMPA# : <asp:TextBox ID="txtIMPAS_F" runat="server" Width="80px" ></asp:TextBox> </td>
                                                <td align="right">
                                                      <asp:Button ID="btnSearch" Text="Search" CssClass="btnorange" runat="server" OnClick="btnSearch_Click" />                                              
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="overflow-x:hidden;overflow-y:scroll;">
                                        <table border="0" cellpadding="2" cellspacing="0" class="bordered" style=" width: 100%">
                                            <col width="50px" />
                                            <col width="50px" />
                                            <col />
                                            <col width="100px" />
                                            <tr style="background-color:#FFA366;color:#1e1c1c;font-weight:bold;text-align:left;">
                                                <td style="text-align:center">Delete</td>
                                                <td style="text-align:center">Move</td>
                                                <td>Product Name</td>
                                                <td>IMPA#</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="height:408px;overflow-x:hidden;overflow-y:scroll;">
                                        <table border="0" cellpadding="2" cellspacing="0" class="bordered" style=" width: 100%">
                                            <col width="50px" />  
                                            <col width="50px" />                                          
                                            <col />                                            
                                            <col width="100px" />
                                            <asp:Repeater ID="rptProductLL" runat="server">
                                                <ItemTemplate>
                                                    <tr class='<%#((Common.CastAsInt32( Eval("TableID"))==Common.CastAsInt32( hfdTableID.Value))?"selRow":"")%>'>
                                                        <td style="text-align:center;">
                                                            <asp:ImageButton ID="btnDeleteItem"  OnClientClick="return window.confirm('Are you sure to remove this item?');" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteItem_OnClick" CommandArgument='<%#Eval("TableID")%>' />
                                                        </td>
                                                        <td style="text-align:center;">
                                                            <asp:ImageButton ID="btnMoveItemPopup" ToolTip="Move uncategorized item to Store Item Master." runat="server" ImageUrl="~/Modules/HRD/Images/Move1.png" OnClick="btnMoveItemPopup_OnClick" CommandArgument='<%#Eval("TableID")%>' />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblGridItemName" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label>
                                                        </td>                                                        
                                                        <td align="left">
                                                            <asp:Label ID="lblGridISSAIMPA" runat="server" Text='<%#Eval("ISSAIMPA")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
               </table>        


            
                     <%-- Add Product Last Level-----------------------------------------%>
                    <div id="divMoveItem" runat="server" style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" visible="false">
                        <center>
                            <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)">
                            </div>
                            <div style="position : relative; width: 600px; padding : 0px; text-align : center; border : solid 3px #4371a5; background : white; z-index: 150; top: 110px; opacity: 1; filter: alpha(opacity=100)">
                                <center>
                                    <div class="text headerband" style="font-size:16px;font-weight:bold;padding:8px; text-align:left;line-height:30px;">
                                        <asp:ImageButton ID="btnClosePopupMoveItem" runat="server" ImageUrl="~/Modules/HRD/Images/closewindow.png" OnClick="btnClosePopupMoveItem_OnClick" style="float:right;width:24px;" />
                                        <span style="font-size:18px;">Create New Store Item</span>
                                    </div>
                                    <div>
                                        <table width="100%" cellpadding="5" cellspacing="2" border="0">
                                            <col width="150px" />
                                            <col />
                                            <tr>
                                                <td>Main Category</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Sub Category</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubCategory" runat="server" Width="100%"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Item Name</td>
                                                <td>
                                                    <asp:TextBox ID="txtItemName" runat="server" style="width:100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>IMPA#</td>
                                                <td>
                                                    <asp:TextBox ID="txtImpaNo" runat="server" style="width:100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Unit</td>                                                    
                                                <td>
                                                    <asp:DropDownList ID="ddlProductUnit" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                      
                                    </div>
                                    <div style="padding:5px;">
                                        <asp:Button ID="btnMoveItem" runat="server" OnClick="btnMoveItem_OnClick" Text="Save" CssClass="btn" />
                                    </div>
                                    <div style="padding:5px;background-color:#e2d9d9;">
                                        &nbsp;<asp:Label ID="lblMsgMoveItem" runat="server" CssClass="error_msg"></asp:Label>
                                    </div>
                                </center>
                            </div>
                        </center>
                    </div>
            
        </ContentTemplate>
        </asp:UpdatePanel>
      </div>

     </div>     
        <script type="text/javascript">
            function SetProductID(PID,PCode)
            {
                $("#hfdProductID").val(PID);
                $("#hfdProductCode").val(PCode);
                
                $("#TempProductClick").click();
            }
            function SetSubProductID(PID,PCode)
            {
                $("#hfdSubPID").val(PID);
                $("#hfdSunPCode").val(PCode);
                
                $("#btnTempClickSubProduct").click();
            }
            function SetProductIDLL(TableID)
            {
                $("#hfdTableID").val(TableID);
                $("#btnTempProductLL").click();
            }
            
            
        </script>
   </asp:Content>

