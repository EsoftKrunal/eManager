<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Communication.aspx.cs" Inherits="Communication" %>
<%@ Register src="~/Modules/LPSQE/Procedures/SMSADMIN/SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="~/Modules/LPSQE/Procedures/SMSADMIN/SMSAdminSubTab.ascx" tagname="SMSAdminSubTab" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>EMANAGER</title>
    <link rel="stylesheet" type="text/css" href="../../../HRD/Styles/StyleSheet.css" />
    <link rel="Stylesheet" href="../CSS/style.css" />
</head>
<body >
    <form id="form1" runat="server">
        <div style="font-family:Arial;font-size:12px;"> 
     <uc3:SMSManualMenu ID="SMSManualMenu1" runat="server" />
      <%-- <uc4:SMSAdminSubTab ID="ManualMenu2" runat="server" />
         <div class="text headerband"> Communication </div>--%>
    <div>
        <center>
            <asp:RadioButtonList runat="server" ID="radCommtype" RepeatDirection="Horizontal" onselectedindexchanged="radCommtype_SelectedIndexChanged" AutoPostBack="true">
                
                <%--<asp:ListItem Text="&nbsp;Manual Wise Communication" Value="MW" style="border:none" ></asp:ListItem>
                <asp:ListItem Text="&nbsp;Ship Wise Communication" Value="SW" style="border:none"></asp:ListItem>--%>
                <asp:ListItem Text="&nbsp;Form & Rank Communication" Value="FW" style="border:none"></asp:ListItem>
                <asp:ListItem Text="&nbsp;Manual Communication" Value="MW1" style="border:none" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>  
        </center>
    </div>
    <div style=" height:490px; border:solid 1px gray;">
        <iframe width="100%" runat="server" src="MWCommunication1.aspx" id="frm_Comm" height="490px;">
        
        </iframe>
    </div>
             </div>
    </form>
</body>

</html>
