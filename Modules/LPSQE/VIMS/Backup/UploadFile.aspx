<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="VIMS_UploadFile" %>

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
        <Center>
        
        <div style=" background-color:#F5F5FF; padding:4px; border-bottom:solid 1px gray; position:fixed; top:0px; left:0px; width:100%; text-align:left;" runat="server" id="dvUpload">
        Description :
        <asp:TextBox runat="server" ID="txtDescr" MaxLength="255" Width="650px"></asp:TextBox>
        <asp:FileUpload ID="dlpUpload" runat="server" style="width:70px" /> 
        <asp:Button runat="server" ID="btnUpload" Text="Upload" CssClass="Btn1" style="padding:1px" onclick="btnUpload_Click" />
        </div>
        </Center>   
        <div runat="server" id="dvmargin" style="height:32px;">&nbsp;</div>
        <div>
        <asp:Repeater runat="server" ID="rptFiles">
        <ItemTemplate>
        <div style="padding:4px; background-color:#EBFFEB; border:solid 1px gray; margin:2px;">
          <b><%#Eval("SNO")%>.</b>&nbsp;&nbsp;<a href='./Attachments/<%#Eval("FileName")%>' target="_blank"><%#Eval("FileName")%></a>
         <span style="color:Red;"><em>[ <%#Eval("Description")%> ] </em></span>
         <span style='float:right;'>
         <span style='<%=AllowUploadText%>'>
         <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/Images/error.png" CssClass='<%#Eval("FileName")%>' CommandArgument='<%#Eval("AttachmentId")%>' OnClick="imgDelete_Click" OnClientClick="return window.confirm('Are you sure to remove this?');" />
         </span>
         </span>
          </div>
        </ItemTemplate>
        </asp:Repeater>
        </div>
    </form>
    </div>
    <%--</form>--%>
</body>
</html>
