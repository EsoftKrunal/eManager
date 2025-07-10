<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocManagement.aspx.cs" Inherits="DocManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Attachment</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    window.resizeTo(800,600);            
    </script>
      <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="2" cellspacing="" border="0" width="100%">
            <tr >
                <td class="text headerband">
                    Attachments
                </td>
            </tr>
            <tr>
                <td>
                <table cellpadding="2" cellspacing="" border="0" width="100%">
                <tr>
                    <td><b> Select Documents : </b> &nbsp;&nbsp;<asp:TextBox ID="txtFormNo" runat="server" Width="100px" MaxLength="50" ToolTip="Search Form No" OnTextChanged="txtFormNo_TextChanged" ></asp:TextBox></td></tr>
                <tr>
                    <td>
                        <div style="height:100px;width=500px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;" >
                       <%-- <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="60px" Width="99%" ></asp:TextBox>--%>
                         <asp:CheckBoxList ID="chkFormList" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="3" RepeatDirection="Vertical" CssClass="input_box" Width="1803px" Height="92px"></asp:CheckBoxList></div>
                    </td>
                </tr>
                <tr>
                <td>
                    <%--<asp:FileUpload ID="fupFile" runat="server" Width="300px" CssClass="btn"/>--%>
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
                                        <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/PMS/Images/delete.png" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete!')" CommandArgument='<%#Eval("TableID")%>' Height="12px" />
                                        <asp:HiddenField ID="hfcOMPjOBid" runat="server" Value='<%#Eval("CompJobID")%>' />
                                        
                                    </td>
                                    <td>&nbsp;<%#Eval("Descr")%></td>
                                    <td style="width:150px;">
                                        <%--<a href='UploadFiles/AttachmentForm/<%#Eval("UpFileName")%>' target="_blank" ><%#Eval("UpFileName")%> </a>--%>
                                         <asp:LinkButton ID="lnlViewVersion" runat="server" Text=" Download " OnClick="lnlViewVersion_OnClick" CommandArgument='<%#Eval("FORMID")%>' ToolTip='<%#Eval("FileName")%>'  ></asp:LinkButton>
                                        
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
