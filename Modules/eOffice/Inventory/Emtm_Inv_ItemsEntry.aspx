<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inv_ItemsEntry.aspx.cs" Inherits="emtm_Inventory_Emtm_Inv_ItemsEntry" %>
<%@ Register src="~/Modules/eOffice/Inventory/Emtm_Inv_HeaderMenu.ascx"  tagname="Emtm_Inv_HeaderMenu" tagprefix="uc2" %>

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
                             <div>
                                <uc2:Emtm_Inv_HeaderMenu ID="UCHeader" runat="server" />
                             </div>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <%--<table cellpadding="5" cellspacing="0" width="100%" >
                                    <tr>
                                        <td colspan="3" class="dottedscrollbox" 
                                            style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                            Items Entry
                                        </td>
                                    </tr>
                                </table>--%>
                                <table id="txtCategory" runat="server" cellpadding="2" cellspacing="0" width="100%" style="margin:auto;"  border="0">
                                    <tr>
                                        <td style="text-align: right; width:100px;">
                                            Main Category :&nbsp;
                                        </td>
                                        <td style="text-align: left; width:170px;">
                                            <asp:DropDownList ID="ddlMainCat" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCat_SelectedIndexChanged" runat="server" Width="150px" required="yes"></asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width:100px;">
                                            Mid Category :</td>
                                        <td style="text-align: left; width:220px;">
                                            <asp:DropDownList ID="ddlMidCat" runat="server" required="yes" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" Text=" Go " Width="80px" />
                                            <asp:Label ID="lblMsg" style="font-size:12px; color:Red;" runat="server"></asp:Label>
                                        </td>
                                    </tr>                                    
                                </table>
                                
                                <table id="tblItems" runat="server" runat="server" cellpadding="5" cellspacing="0" width="100%" >
                                    <tr>
                                        <td>
                                            <iframe id="iframItems" runat="server"  width="100%" frameborder="0" height="395px" scrolling="no" ></iframe>                                            
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
