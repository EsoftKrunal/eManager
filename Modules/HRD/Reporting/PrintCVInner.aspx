<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCVInner.aspx.cs" Inherits="PrintCVInner" Title="Print CV" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
     <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" >
    <div  style="text-align: left">
        &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"   />
</div>
    </form>
    <script language="javascript"  type="text/javascript">
    var i=0;
    for (i=0;i<=document.all.length-1;i++)
    {
        if (document.all(i).nodeName=="IMG")
        {
            if(document.all(i).height=="67" && document.all(i).width=="60")
            {
             document.all(i).width="80";
             document.all(i).height="110";
            } 
        }
    } 
    
    </script>
</body>
</html>


