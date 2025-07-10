<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipDocManagement.aspx.cs" Inherits="ShipDocManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
   <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
    window.resizeTo(800,600);            
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
        <table cellpadding="2" cellspacing="" border="0" width="100%">
            <tr >
                <td class="text headerband">
                    Attachments
                </td>
            </tr>
            <tr>
                <td>
                <table id="tblAddDocs" runat="server"  cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>Description : </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="60px" Width="99%" ></asp:TextBox></td>
                </tr>
                <tr>
                <td>
                    <asp:FileUpload ID="fupFile" runat="server" Width="300px" />
                    <asp:Button ID="btnAddFiles" runat="server" Text="Upload Attachment" CssClass="btn" OnClick="btnAddFiles_OnClick" Width="150px" />
                    <asp:Label ID="lblMSG" runat="server" ForeColor="Red" ></asp:Label>
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
                    <table cellpadding="2" cellspacing="" border="1" width="100%" style=" background-color:#c2c2c2; border-collapse:collapse" >
                        <tr class= "headerstylegrid">
                            <td style="width:30px; text-align:center">Sr#</td>
                            <td style="width:30px;"></td>
                            <td>Description</td>
                            <td style="width:150px;">File Name</td>
                            <td style="width:12px;">&nbsp;</td>
                        </tr>
                    </table>
                    <div style="width:100%;height:200px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                    <table cellpadding="2" cellspacing="" border="1" width="100%" style="border-collapse:collapse" >
                        <asp:Repeater ID="rptFiles" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                    <td style="width:30px; text-align:center">   
                                        <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" CommandArgument='<%#Eval("TableID")%>' Height="12px" Visible='<%#(Eval("VesselCode").ToString()!="")%>' />
                                        <asp:HiddenField ID="hfCompJobID" runat="server" Value='<%#Eval("CompJobID")%>' />
                                    </td>
                                    <td>&nbsp;<%#Eval("Descr")%></td>
                                    <td style="width:150px;">
                                        <a href='UploadFiles/<%#((Eval("VesselCode").ToString().Trim()=="")?"AttachmentForm/": VessCode+"/")+ Eval("UpFileName").ToString()%>' target="_blank" ><%#Eval("UpFileName")%> </a>
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
