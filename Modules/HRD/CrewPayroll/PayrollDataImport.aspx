<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollDataImport.aspx.cs" Inherits="Modules_HRD_CrewPayroll_PayrollDataImport" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <%-- <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <div style="text-align: center;font-family:Arial;font-size:12px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="text headerband">
            Import Ship Data</td>
            </tr>
        <tr>
        <td style=" text-align :center; vertical-align : top;border-left:solid 1px white;display:flex;justify-content:center;padding-top:10px;">
             <asp:FileUpload ID="FileUpload1" runat="server" /> <asp:Button Text="Upload" OnClick="Upload" runat="server" CssClass="btn" /> 
            </td>
            </tr>
            </table>
         </div>
   </asp:Content>
