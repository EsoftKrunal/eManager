<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadViewForms.aspx.cs" Inherits="ReadViewForms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Open Linked Forms</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body >
    <form id="form1" runat="server">
        <table style="width:100%; text-align:left" border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td style="text-align:left;" colspan="2">Attached Forms : <hr /></td>
            </tr>
        </table>
        <center>
        <table cellpadding="3" cellspacing="0" width="98%" border="0" style="text-align:center; border-collapse:collapse;">
        <col width="100%" />
        <tr>
            <td>
            <asp:Repeater runat="server" ID="rptLinkedForm">
                <ItemTemplate>
                <div style="height:15px; padding:5px; border-bottom:solid 1px #c2c2c2">
                    <div style="float:left"> 
                    <%#Eval("Sno")%>. 
                    <asp:LinkButton ID="lnkLink" runat="server" CommandArgument='<%#Eval("FormId")%>' Text='<%# Eval("FormNo")+ " ( "+ Eval("VersionNo") +" )"%>'  OnClick="btnDownLoadFile" ></asp:LinkButton>  
                    <asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("FileName")%>'/>
                    </div>
                    <span style="float:right"><%# Common.ToDateString(Eval("CreatedOn"))%></span>
                </div>
            </ItemTemplate>
            </asp:Repeater>
            </td>
        </tr>
        </table>
        </center>
    </form>
</body>
</html>
