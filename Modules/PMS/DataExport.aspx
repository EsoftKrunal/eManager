<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataExport.aspx.cs" Inherits="DataExport" Async="true" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <script type="text/javascript" src="JS/jquery_v1.10.2.min.js"></script>
  <link href="https://fonts.googleapis.com/css?family=M+PLUS+1p" rel="stylesheet">
    <style type="text/css">
        *{
            font-family: 'M PLUS 1p', sans-serif;
            padding:0px;
            margin:0px;
        }
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <div style="text-align:center;background-color:#ff6a00;color:white;padding:10px;">Export BreakDown Data</div>
        <%--<table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width:300px;">
                    <div style="text-align:center;background-color:#4f4f4e;color:white;padding:5px;">Tables</div>
                    <div>
                        <asp:Repeater runat="server" ID="rpttables">
                            <ItemTemplate>
                            <div>
                                <input type="radio" name="rad_"/>
                                <label for="rad_"> <%#Eval("tablename")%>    </label>
                            </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
                <td>


                </td>
            </tr>
        </table>
   --%>
        <div style="padding:10px;">
        <asp:DropDownList runat="server" ID="ddlVessel"></asp:DropDownList>
        <asp:Button ID="Button1" runat="server" onclick="btnexport_Click" Text="Export BreakDown Data For Ship" Width="300px" />
        </div>
        <textarea style="height:400px;padding:10px; margin:5px;padding:10px; width:100%;" rows="50"><asp:Literal runat="server" ID="littext"></asp:Literal></textarea>          
    </div>
    </form>
</body>
</html>
