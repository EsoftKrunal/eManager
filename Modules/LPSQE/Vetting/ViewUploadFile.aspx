<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewUploadFile.aspx.cs" Inherits="VIMS_ViewUploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link href="VIMSStyle.css" rel="stylesheet" type="text/css" />
 </head>
<body style="margin:3px 3px 3px 3px; font-family:Arial; font-size:12px;">
    <%--<form id="form1" runat="server">--%>
    <div>
     <form runat="server" method="post" name="fmain" id="fmain" action="UploadFile.aspx" enctype="multipart/form-data">
        <div>
        <asp:Repeater runat="server" ID="rptFiles">
        <ItemTemplate>
        <div style="padding:4px; background-color:#EBFFEB; border:solid 1px gray; margin:2px;">
          <b><%#Eval("SNO")%>.</b>&nbsp;&nbsp;<a href='./Attachments/<%#Eval("FileName")%>' target="_blank"><%#Eval("FileName")%></a>
         <span style="color:Red;"><em>[ <%#Eval("Description")%> ] </em></span>
          </div>
        </ItemTemplate>
        </asp:Repeater>
        </div>
    </form>
    </div>
    <%--</form>--%>
</body>
</html>
