<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageUpdate.aspx.cs" Inherits="Transactions_ImageUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>View / Update Image</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../CSS/StyleSheet.css" />
    <script type="text/javascript">
    function RefreshParent()
    {
        window.opener.document.getElementById("btnPost").click();      
    }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellSpacing="0" cellPadding="0" width="100%" >
        <tr>
            <td style="background-color:#0066FF; padding:10px;  text-align :center; font-size:14px; color:#fff;" class="text">Update Image</td>
        </tr>
        <tr>
            <td style="TEXT-ALIGN: left"><asp:Image ID="Image1" runat="server" Width="500px"/></td>
        </tr>
        <tr>
            <td style=" text-align :center" >
                <asp:Label  runat="server" ID="lblCaption" Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="#666666"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td style=" text-align :center" > Caption :  
                <asp:TextBox runat="server" ID="txtCap" CssClass="input_box" Width="400px" /> 
            </td>
        </tr> 
        <tr>
            <td style=" text-align :center" > New File:  
                <asp:FileUpload runat="server" ID="FileUpload1" CssClass="input_box" Width="400px" /> 
            </td>
        </tr> 
        <tr>
            <td style=" text-align :center" > 
                <asp:Button runat="server" ID="btnUpload" CssClass="btn" Text="Upload Image" Width="100px" onclick="btnUpload_Click" /> 
            </td>
        </tr>    
        </table>
    </div>
    </form>
</body>
</html>
