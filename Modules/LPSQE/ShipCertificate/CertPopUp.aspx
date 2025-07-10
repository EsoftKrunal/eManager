<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertPopUp.aspx.cs" Inherits="Transactions_CertPopUp" %>
<%@ Register src="VesselCertificates.ascx" tagname="VesselCertificates" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deficiency</title>
    <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" ></ajaxToolkit:ToolkitScriptManager>   
    <div>
        <br />
        <br />
        <center>
         <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9;font-family:Arial;font-size:12px;">
            <tr>
                <td colspan="4" style=" height:23px; text-align :center;font-family:Arial; font-size:14px;" class="text headerband">Add/Edit Vessel Certificate</td>
            </tr>
            <tr><td>
            <uc1:VesselCertificates ID="VesselCertificates1" runat="server" />
            </td></tr>
            </table> 
        </center>
    </div>
    </form>
</body>
</html>
