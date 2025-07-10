<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inve_MainCategory.aspx.cs" Inherits="Emtm_Inve_MainCategory"%>
<%@ Register src="~/Modules/eOffice/Inventory/Emtm_Inv_HeaderMenu.ascx"  tagname="Emtm_Inv_HeaderMenu" tagprefix="uc2" %>
<%@ Register src="~/Modules/eOffice/Inventory/Emtm_Inv_Category.ascx"  tagname="Emtm_Inv_CategoryMenu" tagprefix="uc4" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function Show_Image_Large(obj)
    {
    window.open(obj.src,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
    }
    function Show_Image_Large1(path)
    {
    window.open(path,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
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
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                Inventory Management                                  
                            </div>
                             <div class="dottedscrollbox">
                                <uc2:Emtm_Inv_HeaderMenu ID="UCHeader" runat="server" />
                             </div> 
                             <div class="dottedscrollbox">
                                    <uc4:Emtm_Inv_CategoryMenu ID="UCCategoryMenu" runat="server" />
                             </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%" >
                                    <tr>
                                        <td class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                            Main Category
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align:top;" >
                                            <table cellpadding="2" cellspacing="2" border="0px" style="border-collapse:collapse;" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                        <table cellpadding="2" cellspacing="1" width="100%" rules="all" height="26px">
                                                            <colgroup>
                                                                <col width="50px" />
                                                                <col width="60px" />
                                                                <col />
                                                                <col width="25px" />
                                                                <tr class="blueheader">
                                                                    <td>
                                                                        Edit</td>
                                                                    <td>
                                                                        Delete</td>
                                                                    <td style="text-align:left">
                                                                        Category</td>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </colgroup>
                                                        </table>
                                                        </div>
                                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px; text-align:center;">
                                                            <table cellpadding="2" cellspacing="2" width="100%" rules="all">
                                                                <colgroup>
                                                                    <col width="50px" />
                                                                    <col width="60px" />
                                                                    <col />
                                                                    <col width="25px" />
                                                                </colgroup>                                                       
                                                            <asp:Repeater ID="rptCategory" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgEdit" runat="server" 
                                                                                CommandArgument='<%#Eval("MainCatID")%>' ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                                                OnClick="imgEdit_OnClick" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgDel" runat="server" 
                                                                                CommandArgument='<%#Eval("MainCatID")%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                                                OnClick="imgDel_OnClick" 
                                                                                OnClientClick="return confirm('Are you sure to delete ?')" />
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("MainCatName")%>'></asp:Label>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                          </table>  
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <fieldset style="padding :10px;">
                                                            <legend style=" color:Black; font-size:12px;font-family:Arial;" >Category</legend>
                                                            <div id="divAdd" runat="server" style="text-align:right; padding-right:5px;">
                                                               <asp:Button ID="btnAdd" Text="Add Category" CssClass="btn" runat="server" 
                                                                    Width="98px" onclick="btnAdd_Click" />
                                                            </div>
                                                            <table id="tblAddEdit" visible="false" runat="server" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="text-align:right">Main Category :&nbsp;</td>
                                                                    <td style="text-align:left">
                                                                        <asp:TextBox ID="txtMainCategory" runat="server" required="yes" Width="350px" MaxLength="50"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" Width="80px" OnClick="btnSave_OnClick" />
                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                                                                            OnClick="btnCancel_OnClick" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-size: 12px;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
