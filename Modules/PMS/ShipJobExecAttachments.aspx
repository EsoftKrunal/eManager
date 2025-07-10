<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipJobExecAttachments.aspx.cs" Inherits="ShipJobExecAttachments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    
    <script type="text/javascript"></script>
    <style type="text/css">
        .bordered 
        {
            border-collapse:collapse;
        }
        .bordered tr td 
        {
            border:solid 1px #c2c2c2;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body style="font-size:14px; ">
    <form id="form1" runat="server">
    <div style="font-size:12px;font-family:Arial;" >
        <div class="text headerband"> Attachments List Required to Execute this Job</div>
        <table cellpadding="5" cellspacing="0" border="0" width="100%" style="text-align:left;font-size:14px;" class="bordered">
            <tr > 
                <td style="width:80px" >Job Code :</td>
                <td ><asp:Label runat="server" ID="lbljobcode"></asp:Label></td>
                <td style="width:100px" >Job Details :</td>                
                <td>                    
                    <asp:Label runat="server" ID="lbljobdetails"></asp:Label>
                </td>
            </tr>
        </table>

        <div style="padding:10px;color:gray; text-align:center;font-weight:bold;"> Required Attachments </div>
        <table cellpadding="2" cellspacing="" border="0" width="100%">
        <tr>
        <td>
            <div style="text-align:center">
                <asp:Label ID="lblMSG" runat="server" ForeColor="Red" ></asp:Label>
            </div>
        <table id="tblAddDocs" runat="server" cellpadding="4" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width:150px;height:23px;vertical-align:middle;   ">Attachment Details : </td>
            <td><asp:TextBox ID="txtDescription" runat="server" Width="99%" style="padding:5px;font-size:11px;" ></asp:TextBox></td>
            <td style="width:155px;">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_OnClick" Width="150px" />            
            </td>
        </tr>
        </table>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse" class="bordered" >
                        <tr class= "headerstylegrid">
                            <td style="width:30px; text-align:center">Sr#</td>
                            <td style="width:30px;<%=((Session["UserType"].ToString() == "O")?"":"display:none")%>"></td>
                            <td style="width:30px;<%=((Session["UserType"].ToString() == "O")?"":"display:none")%>"></td>
                            <td>Attachment Details</td>
                            <td style="width:10px;">&nbsp;</td>
                        </tr>
                    </table>
                    <div style="width:100%;height:400px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                    <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse" class="bordered" >
                        <asp:Repeater ID="rptFiles" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("srno")%></td>
                                    <td style="width:30px; text-align:center" runat="server" visible='<%#(Session["UserType"].ToString() == "O")%>'>   
                                        <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEdit_OnClick" CommandArgument='<%#Eval("AttachmentId")%>'/>
                                    </td>
                                    <td style="width:30px; text-align:center" runat="server" visible='<%#(Session["UserType"].ToString() == "O")%>'>   
                                        <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" CommandArgument='<%#Eval("AttachmentId")%>'/>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDetails" Text='<%#Eval("AttachmentDetails")%>'></asp:Label>
                                        </td>                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
