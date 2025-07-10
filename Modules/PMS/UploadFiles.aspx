<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFiles.aspx.cs" Inherits="UploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Upload Files </title>
    <link rel="Stylesheet" type="text/css" href="JQ_Css/uploadify.css" />
    <script type="text/javascript" src="JQ_Scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="JQ_Scripts/jquery.uploadify.js"></script>
    <style type="text/css">
        *
        {
            box-sizing:border-box;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style='width:855px; font-family:Verdana; font-size:11px;'>
    <center>
    <div style=' padding:4px; background:#E6F0FF; position:fixed; top:0px; left:0px; width:100%; z-index:100;'>
        <asp:Button ID="btnClose" runat="server" Text="Show Uploaded Files" OnClick="btnShow_OnClick" style='padding:4px; font-size:13px; border:none; background-color:#2E8AE6; color:White;width:150px;' />
        <asp:Button ID="btnDeleteAll" runat="server" Text="Clear All Files" OnClick="btnDeleteAll_OnClick" style='padding:4px; font-size:13px; border:none; background-color:#2E8AE6; color:White;width:100px;' />
        <asp:Button ID="btnSave" runat="server" Text="Save Files" OnClick="btnSave_OnClick" style='padding:4px; font-size:13px; border:none; background-color:#19A319; color:White; width:100px;' />
    </div>
    <div style = "padding:5px;position:fixed; top:30px; left:0px; width:100%; background-color:White; border-bottom:solid 1px blue; z-index:100;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
    </center>
    <div>
    <div style='vertical-align:top; padding-top:60px;'>
        <asp:DataList runat="server" ID="rptTempFiles" RepeatColumns="2">
            <ItemTemplate>
            <div style=' width:400px;  border:solid 1px #d2d2d2; margin:10px; padding:5px;box-shadow: 5px 5px 5px #888888; '>
                  <div style="border:solid 1px #d2d2d2;position:relative; ">
                    <img src='<%# "..\\PMS\\Temp\\" + System.IO.Path.GetFileName(Container.DataItem.ToString())%>' width='384px' height='250px;'/>
                    <asp:ImageButton runat="server" ID="btnDelImg" ImageUrl="~/Images/DeleteSquare.png"  style="position:absolute; top:0px; left:368px;" OnClick="DeleteImage" ToolTip="Delete This Image.." OnClientClick="return confirm('Are you sure to delete this file ?');" CommandArgument='<%# System.IO.Path.GetFileName(Container.DataItem.ToString())%>'/>
                  </div>
                  <div style="padding-top:2px;">
                  <div style="padding:5px;">
                   Enter File Description :
                  </div>
                  <asp:TextBox runat="server" ID="txtCaption" style='border:solid 1px #e2e2e2; width:388px; background-color:#FFFFE0; font-size:12px; padding:5px;' Text='<%# System.IO.Path.GetFileNameWithoutExtension(Container.DataItem.ToString()).Substring(18)%>'></asp:TextBox>
                  </div>
            </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </div>
    </form>
</body>
</html>
<script type = "text/javascript">
    $(window).load(
    function () {
        $("#<%=FileUpload1.ClientID %>").fileUpload({
            'uploader': 'JQ_Scripts/uploader.swf',
            'cancelImg': 'images/cancel.png',
            'buttonText': 'Browse Files',
            'script': 'Upload.ashx',
            'folder': 'Temp',
            'fileDesc': 'Image Files',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
            'multi': true,
            'auto': true
        });
    }
);
</script>