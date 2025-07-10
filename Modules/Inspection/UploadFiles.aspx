<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFiles.aspx.cs" Inherits="UploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> EMANAGER </title>
    <link rel="Stylesheet" type="text/css" href="JQ_Css/uploadify.css" />
     <link rel="Stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="JQ_Scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="JQ_Scripts/jquery.uploadify.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <div style='width:855px'>
    <center>
    <div style=' padding:4px; position:fixed; top:0px; left:0px; width:100%; z-index:100;'>
        <asp:Button ID="btnClose" runat="server" Text="Show Uploaded Files" OnClick="btnShow_OnClick" style='padding:4px; font-size:13px; border:none; ' CssClass="btn"/>
        <asp:Button ID="btnDeleteAll" runat="server" Text="Clear All Files" OnClick="btnDeleteAll_OnClick" style='padding:4px; font-size:13px; border:none; ' CssClass="btn" />
        <asp:Button ID="btnSave" runat="server" Text="Save Files" OnClick="btnSave_OnClick" style='padding:4px; font-size:13px; border:none; ' CssClass="btn" />
    </div>
    <div style = "padding:5px;position:fixed; top:30px; left:0px; width:100%; background-color:White; border-bottom:solid 1px blue; z-index:100;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
    </center>
    <div>
    <div style='vertical-align:top; padding-top:60px;'>
        <asp:DataList runat="server" ID="rptTempFiles" RepeatColumns="2">
            <ItemTemplate>
            <div style='position:relative; width:400px;'>
                  <img style="border:dotted 1px red; padding:5px; margin:5px;" src='<%# "..\\Inspection\\Temp\\" + System.IO.Path.GetFileName(Container.DataItem.ToString())%>' height="250" width="400" />
                  <asp:ImageButton runat="server" ID="btnDelImg" src="../HRD/Images/DeleteSquare.png" style="position:absolute; top:5px; left:400px;" OnClick="DeleteImage" CommandArgument='<%# System.IO.Path.GetFileName(Container.DataItem.ToString())%>'/>
                  <asp:TextBox runat="server" ID="txtCaption" style='border:solid 1px gray; width:400px; background-color:#FFFFE0; margin-left:10px;' Text='<%# System.IO.Path.GetFileNameWithoutExtension(Container.DataItem.ToString()).Substring(18)%>'></asp:TextBox>
            </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </div>
        </div>
    </form>
</body>
</html>
<script type = "text/javascript">
    $(window).load(
        function () {
           // alert('Hi');
        $("#<%=FileUpload1.ClientID %>").fileUpload({
            'uploader': 'JQ_Scripts/uploader.swf',
            'cancelImg': '../../HRD/images/cancel.png',
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