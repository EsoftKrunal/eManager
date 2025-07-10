<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inv_AMC.aspx.cs" Inherits="emtm_Inventory_Emtm_Inv_AMC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AMC</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
      function openitemwin(ItemID,MidCat)
      {
         window.open('Emtm_Inv_AMCEntry.aspx?Mode=Edit&&ITEMId='+ ItemID+'&MidCat='+MidCat,'','');
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
    <form id="form1" runat="server">
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
                                            Items List - [ AMC ]
                                        </td>
                                    </tr>
                                    <tr><td style="text-align :center ">
                                    <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-size: 14px;"></asp:Label>
                                    </td></tr>
                                    
                                    <tr>
                                        <td style="vertical-align: top; padding-top: 2px;">
                                         <fieldset>
                                            <legend style="font-size:12px;">Search Items</legend> 
                                            <table cellpadding="1" cellspacing="0" width="100%" rules="all">
                                                <tr class="blueheader">
                                                    <td style="text-align: center">
                                                        &nbsp;Contract#
                                                    </td>
                                                    <td style="text-align: center">
                                                        &nbsp;Min Cat
                                                    </td>
                                                    <td style="text-align: center">
                                                        &nbsp;Contract Vendor 
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtContNo" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:DropDownList ID="ddlMinCat" runat="server" Width="200px"></asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtVendor" runat="server" AutoPostBack="true" Width="200px"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtVendor" ServicePath="~/WebService.asmx" ServiceMethod="GetVendorAMC" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList"  CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH"> </ajaxToolkit:AutoCompleteExtender>
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
                                                        <col width="150px" />
                                                        <col width="150px" />
                                                        <col width="100px" />
                                                        <col width="100px" />
                                                        <col />
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
                                                            &nbsp;<asp:LinkButton ID="lbtnContNo" Text="Contract#" OnClick="lbtnContNo_Click" runat="server"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;Mid Category
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;<asp:LinkButton ID="lbtnStartDt" Text="Start Date" OnClick="lbtnStartDt_Click" runat="server"></asp:LinkButton>
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;<asp:LinkButton ID="lbtnEndDt" Text="End Date" OnClick="lbtnEndDt_Click" runat="server"></asp:LinkButton>
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;<asp:LinkButton ID="lbtnContVendr" Text="Contract Vendor" OnClick="lbtnContVendr_Click" runat="server"></asp:LinkButton>
                                                        </td>                                                        
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                            </div>
                                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;
                                                height: 220px; text-align: center;">
                                                <table cellpadding="2" cellspacing="2" width="100%" rules="all">
                                                    <colgroup>
                                                        <col width="50px" />
                                                        <col width="50px" />
                                                        <col width="150px" />
                                                        <col width="150px" />
                                                        <col width="100px" />
                                                        <col width="100px" />
                                                        <col />                                                        
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptITHardware" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgEdit" runat="server" CommandArgument='<%# Eval("ITEMID").ToString() %>'
                                                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEdit_OnClick" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%#Eval("ITEMID")%>'
                                                                        ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete ?')" />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("CONTRACTNO")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("MinCatName")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("STARTDATE")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("ENDDATE")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("CONTRACT_VENDOR")%>
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
