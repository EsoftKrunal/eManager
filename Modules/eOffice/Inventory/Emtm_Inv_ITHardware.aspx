<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inv_ITHardware.aspx.cs" Inherits="emtm_Inventory_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT Hardware</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
      function openitemwin(ItemID,MidCat)
      {
         window.open('Emtm_Inv_ITHardwareEntry.aspx?Mode=Edit&&ITEMId='+ ItemID+"&MidCat="+MidCat+"",'','');
      }
      function Refresh()
      {
//         var btn = document.getElementById('btnRefresh');
//         btn.click();
      
         __doPostBack('btnRefresh','');
      }
    </script>
    
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
    <table width="100%">
    
                    <tr>
                        <td valign="top" style="border:solid 1px #4371a5;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%" >
                                    <tr>
                                        <td class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                            Items List - [ IT -> Hardware ]
                                        </td>
                                    </tr>
                                    <tr><td style="text-align :center ">
                                    <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-size: 14px;"></asp:Label>
                                    </td></tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 2px;">
                                         <fieldset>
                                            <legend style="font-size:12px;">Search Items</legend> 
                                            <table cellpadding="2" cellspacing="1" width="100%" rules="all">
                                                <tr class="blueheader">
                                                    <td style="text-align: center">
                                                        &nbsp;Asset Code
                                                    </td>
                                                    <td>
                                                        Min Category
                                                    </td>
                                                    <td style="text-align: center">
                                                        &nbsp;Sr#
                                                    </td>
                                                    <td style="text-align: center">
                                                        &nbsp;Vendor Name
                                                    </td>
                                                    <td style="text-align: center">
                                                        &nbsp;Maker Name
                                                    </td>
                                                    <td style="text-align: center">
                                                        &nbsp;Warrenty Expiry
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtAssetCode" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMinCatID" runat="server" ></asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtSrNo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtVendorName" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtVendorName" ServicePath="~/WebService.asmx" ServiceMethod="GetVendorHardware" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList"  CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH"></ajaxToolkit:AutoCompleteExtender>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtMakerName" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" TargetControlID="txtMakerName" ServicePath="~/WebService.asmx" ServiceMethod="GetMakerHardware" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList"  CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH"></ajaxToolkit:AutoCompleteExtender>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtWarrantyExpiry" MaxLength="12" ReadOnly="false" runat="server" Width="100px" ></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                        CausesValidation="false" />                                                                        
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton1" TargetControlID="txtWarrantyExpiry" PopupPosition="BottomRight">
                                                                    </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnSearch" CssClass="btn" Text="Search" runat="server" onclick="btnSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                         </fieldset>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top:2px;">
                                          <fieldset>
                                            <legend style="font-size:12px;">Item List</legend> 
                                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table cellpadding="2" cellspacing="1" width="100%" rules="all" height="26px">
                                                   <colgroup>
                                                        <col width="50px" />
                                                        <col width="50px" />
                                                        <col />
                                                        <col width="150px" />
                                                        <col width="150px" />                                                        
                                                        <col width="200px" />                                                        
                                                        <col width="150px" />                                                        
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <tr class="blueheader">
                                                        <td>
                                                            Edit
                                                        </td>
                                                        <td>
                                                            Delete
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;<asp:LinkButton ID="lbtnAssetCode" Text="Asset Code" OnClick="lbtnAssetCode_Click" runat="server"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            Min Category 
                                                        </td>
                                                        <td style="text-align: left">
                                                          &nbsp;<asp:LinkButton ID="lbtnSrNo" Text="Sr#" OnClick="lbtnSrNo_Click" runat="server"></asp:LinkButton>
                                                        </td>                                                        
                                                        <td>
                                                            Vendor Name 
                                                        </td>
                                                        <td>
                                                            Warranty Expiry 
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                            </div>
                                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;
                                                height: 218px; text-align: center;">
                                                <table cellpadding="2" cellspacing="2" width="100%" rules="all">
                                                    <colgroup>
                                                        <col width="50px" />
                                                        <col width="50px" />
                                                        <col />
                                                        <col width="150px" />
                                                        <col width="150px" />
                                                        <col width="200px" />
                                                        <col width="150px" />
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptITHardware" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# Eval("ItemId").ToString() %>'
                                                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEdit_OnClick" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%#Eval("ItemId")%>'
                                                                        ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete ?')" />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("AssetCode")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("MinCatName")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("SRNumber")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("VendorName")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("WarrantyExp")%>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 5px; padding-bottom:5px;">
                                            <div id="divAdd" runat="server" style="text-align: right; padding-right: 5px;">
                                                <asp:Button ID="btnAdd" Text="Add Item" CssClass="btn" runat="server" Width="100px"
                                                    OnClick="btnAdd_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnRefresh" Text="" style="display:none;" runat="server" onclick="btnRefresh_Click" />
                            </td>
                        </tr>
                        </table>  
                    </td>
                    </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
