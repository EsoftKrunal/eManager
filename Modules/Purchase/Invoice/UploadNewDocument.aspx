<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadNewDocument.aspx.cs" Inherits="Invoice_UploadNewDocument" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <!-- Auto Complete -->

     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css" />
    <style type="text/css">
        body
        {
            font-family:Calibri;
            font-size:12px;
            margin:0px;
        }
    .error
    {
        color:Red;
    }
    .hide
    {
        display:none;
    }
        </style>
</head>
<body>
    <form id="form1" runat="server">
     
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><strong>Upload 
                 Additional Documents</strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:150px;">
             <div style="padding:3px"><asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label></div>
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                              Attachment: 
                            </td>
                          <td style="text-align: left;">
                              <asp:FileUpload ID="fuAttachment" runat="server" />&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="att" ControlToValidate="fuAttachment" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                             </td>
                              
                      </tr>
                      </table>
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style=" text-align:center;">
                              &nbsp; 
                              <asp:Button ID="Button1" runat="server" Text="Save" Width="80px" OnClick="btn_UpdateAttachment_Click" ValidationGroup="att" style=" border:none; padding:4px;" CssClass="btn" />
                          </td>
                        </tr>
                      </table>
             </div>
             </center>
    </form>
</body>
</html>
