<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" Async="true" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <script type="text/javascript" src="JS/jquery_v1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
   
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
            Text="Button" />
        
   
    </div>
    </form>
</body>
</html>
