<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorDocuments.aspx.cs" Inherits="VendorDocuments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    body
    {
        font-family:Calibri;
        font-size:14px;
        margin:0px;
        padding:0px;
    }
    *
    {
        box-sizing:border-box;
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        color:#555;
    }
     h1
    {
        font-size:22px;
        background-color: #35356f;
        border-bottom: 5px solid #00001f;
        color:#fff;
        padding:10px;
        margin:0px;
    }
    
    h2 {
      background-color: #00b359;
      color: white;
      font-size: 17px;
      margin: 0;
      padding: 10px;
      text-align: left;
    }

    .center
    {
        text-align:center;
    }
    .center div
    {
        margin:0 auto;
    }
    .left
    {
        text-align:left;
    }
    .right
    {
        text-align:left;
    }
   
   .activecell  h2 
   {
      background-color: #7cb02c;
      color:white;
      font-size: 17px;
      margin: 0;
      padding: 10px;
      text-align: left;
    }
    .activecell
    {
        background-color:#f1f7e9;
    }
     h2.active:after {
      content:"action im progress";
    }
    
.label
{display:inline-block; text-align:left; float:left; width:400px; color:#222 }
.label:after
{content:":"; text-align:right; width:100px; padding-right:5px; display:inline; float:right; position:absolute; right:5px;top:0px;}
.controlarea
{display:inline; text-align:left;  empty-cells:show; }

.row
{
    padding:4px;
    text-align:left;
    border-bottom:solid 1px #eee;
}
.control
{
    border:solid 1px #ddd;
    padding:5px;
    line-height:14px;
}
  
.actionbox
{
    text-align:center;
    padding:5px;
    position:fixed;
    bottom:0px;
    background-color:#eeeeee;
    width:100%;
    border-top:solid 1px #999;
}

.info
{
    color:Maroon;
    font-style:italic;
    font-size:11px;
}
    .bold
    {
    font-weight:800;
    
    }
.circle
{
    border-radius:8px;
    width:18px;
    height:18px;
    line-height:18px;
    color:#7cb02c;
    font-size:12px;
    display:inline-block;
    float:left;
    background-color:#fff;
    margin-right:10px;
}
.bgmodal
{
    background-color:Black;
    opacity: 0.6;
    filter: alpha(opacity=60);
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:5;
}
.modalframe
{
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:6;
    text-align:center;
    margin:0px auto;
    padding-top:5%;    
}
.modalborder
{
    background:rgba(0,0,0,0.3);
    width:80%;
    margin:0px auto;
    padding:0px;
    border:solid 10px grey;
}
.modalcontainer
{
    height:100%;
    background-color:White;
    padding:10px;
}

/*.btn {
  background-color: #66C266;
  border: 1px solid #f0f0f0;
  border-radius: 4px;
  color: #fff;
  text-align:center;
  width:150px;
  padding: 5px 0px 7px 0px;
}*/
.btnpn {
  background-color: #eee;
  border: 1px solid green;
  border-radius: 4px;
  color: green;
  text-align:center;
  width:150px;
  padding: 3px 0px 3px 0px;
}
.close
{
    background-color:#FF6262;
    border: 1px solid #FF6262;
    color:#fff;
}
.btn:hover, .btn:focus {
  background-color: #5CAF5C;
  border-color: #35356f;
  color: #fff;
}
.close:hover,.close:focus
{
   background-color:#FF7373;
   border: 1px solid #FF7373;
}
hr
{
    margin-bottom:0px;
    padding-bottom:0px;
}

.alternate_table 
{
    border:solid 1px #f1f1f1;
}
.alternate_table tr:nth-child(even)
{
    background-color:#f1f1f1;
}
.alternate_table tr:nth-child(odd)
{
    background-color:white;
}

.msgbox
{
    padding:5px 5px 7px 5px;
    border:solid 1px #eee;
    text-align:center;
    margin-left:10px;
    margin-right:10px;
}

</style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="padding:5px;background-color:#e4dddd;">
                <table border="0" width="100%">
                <col />
                <col width="22%"  />
                <col width="2%"  />
                <col width="5%"  />
                <tr>
                    <td>Description </td>
                    <td>Attachment </td>   
                    <td></td>              
                    <td></td>                                     
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Width="95%" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:FileUpload ID="fpAttachment" runat="server" />
                        
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fpAttachment" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="Save"  CssClass="btn"/>
                    </td>
                </tr>
                    
            </table>
            </div>
            <asp:Label id="lblMsg" runat="server" ForeColor="Red"></asp:Label>                            
        <div style="padding:0px;margin:0px;border:solid 1px #c2c2c2;margin-top:5px;background-color:#c2c2c2;">
            <table cellpadding="3" cellspacing="0" border="0" width="100%" rules="all">
                <col />
                <col width="20%" />
                <col width="4%" />
                <tr style="font-weight:bold;" class= "headerstylegrid" >
                    <td>Description</td>
                    <td>File Name</td>
                    <td></td>
                </tr>
            </table>
        </div>

        <div style="padding:0px;margin:0px;border:solid 1px #c2c2c2;">
            <table cellpadding="3" cellspacing="0" border="0" width="100%" rules="all" class="alternate_table">
                <col />
                <col width="20%" />
                <col width="4%" />
                    <asp:Repeater ID="rptAttachments"  runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                     <%#Eval("AttachmentDescription") %>
                                </td>
                                <td>
                                        <asp:LinkButton ID="lnkFileName" runat="server" Text='<%#Eval("Attachmentname") %>' OnClick="lnkFileName_OnClick" CommandArgument='<%#Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                     
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnDeleteFile" runat="server" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="btnDeleteFile_OnClick" CommandArgument='<%#Eval("id") %>' CausesValidation="false" OnClientClick="return confirm('Are you sure to delete the file?');" ToolTip="Delete" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
             </table>
        </div>
    </div>
    </form>
</body>
</html>
