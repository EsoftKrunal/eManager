<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageUpdateMortor.aspx.cs" Inherits="Transactions_ImageUpdateMortor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>View / Update Image</title>
   <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
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
    <TABLE style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 10px; BORDER-TOP: #8fafdb 0px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 5px; VERTICAL-ALIGN: top; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 5px; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right" cellSpacing=0 cellPadding=2 width="500" border=0>
         <TR>
            <TD style=" height:23px; text-align :center; " class="text headerband">
            <b>
            Update Image
            </b>
            </TD>
         </TR>
        <TR>
            <TD style="TEXT-ALIGN: left">
                <asp:Image ID="Image1" runat="server" Width="500px"/>
            </TD>
        </TR>
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
        <asp:Button runat="server" ID="btnUpload" CssClass="btn" Text="Upload Image" 
                Width="100px" onclick="btnUpload_Click" /> 
        </td>
        </tr>    
        </TABLE>
    </div>
    </form>
</body>
</html>
