<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inventory.aspx.cs" Inherits="Emtm_Inventory"%>

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
                             <div>
                                <uc2:Emtm_Inv_HeaderMenu ID="UCHeader" runat="server" />
                             </div> 
                             <div>
                                    <uc4:Emtm_Inv_CategoryMenu ID="UCCategoryMenu" runat="server" />
                             </div>
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
