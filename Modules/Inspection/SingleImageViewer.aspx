<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SingleImageViewer.aspx.cs" Inherits="Transactions_SingleImageViewer" Title="View Image" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Image</title>
    <script>
        var a=1;
		function ChangeImage()
		{
			if(a>3)
			{a=0;}
			document.getElementById('imgImage').filters.item(0).rotation=a;
			a++;
			return false;
		}
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
        <asp:ImageButton ID="ImgDel" runat="server" ImageUrl="~/Images/DeleteSquare.png" ToolTip="Delete Image." OnClientClick="return confirm('Are You Sure You Want To Delete This Image?.');" OnClick="ImgDel_Click" /></td>
                <td valign="top">
        <img id="imgImage" runat="server" style="filter:progId:DXImageTransform.Microsoft.BasicImage(rotation=0); width: 480px; height: 412px;" /></td>
            </tr>
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
        <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return ChangeImage();" ImageUrl="~/Images/arrow_rotate_clockwise.png" ToolTip="Rotate image." /></td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
