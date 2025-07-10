<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Modules_HRD_HRDDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <%-- <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="table-responsive">
       <table style="width: 100%; text-align: center; ">  
            <tr class="text headerband">
                <td colspan="2" style="text-align:center;font-size: 14px;" class="text headerband" >
                Dashboard</td>
            </tr>
            <tr>  
                <td align="center" style="padding-top:25px;" >  
                    <asp:Label ID="lblMsg" runat ="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                    <br />
                    <asp:Panel ID = "Panel1" runat="server">
                     </asp:Panel>
                </td>  
            </tr>  
        </table>
        </div>
</asp:Content>


