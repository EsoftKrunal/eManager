<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BreakDownRemarks.aspx.cs" Inherits="BreakDownRemarks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
   <%-- <script type="text/javascript" language="javascript">
        function openattachmentwindow(DRI,VC) {

            window.open('PopupHistoryAttachment.aspx?DRI=' + DRI + '&VC=' + VC, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
    </script>--%>
       <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <table cellpadding="2" cellspacing="" border="0" width="100%">
            <tr >
                <td class="pagename">
                  <b> BreakDown Remarks :&nbsp;<asp:Label ID="lblDefectNo" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td>
                <table id="tblAddDocs" runat="server"  cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>Remarks : </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="60px" Width="99%" ></asp:TextBox></td>
                </tr>
                <tr>
                <td>
                    <asp:FileUpload ID="fupFile" runat="server" Width="300px" />
                    <asp:Button ID="btnAddFiles" runat="server" Text="Save" CssClass="btn" OnClick="btnAddFiles_OnClick" Width="100px" />
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
                            <%--<td style="width:30px;"></td>--%>
                            <td width="400px" >Remarks</td>
                            <td style="width:25px;"></td>
                            <td style="width:175px;">Updated By/ On </td>
                            <td style="width:12px;">&nbsp;</td>
                        </tr>
                    </table>
                    <div style="width:100%;height:200px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                    <table cellpadding="2" cellspacing="" border="1" width="100%" style="border-collapse:collapse" >
                        <asp:Repeater ID="rptFiles" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                    <%--<td style="width:30px; text-align:center">   
                                        <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/PMS/Images/delete.png" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" CommandArgument='<%#Eval("DefectRemarkId")%>' Height="12px" />                                        
                                    </td>--%>
                                    <td width="400px"><%#Eval("Remarks")%></td>
                                    <td style="width:25px; text-align:center;">
                                        <%--<a href='UploadFiles/<%#((Eval("VesselCode").ToString().Trim()=="")?"AttachmentForm/": VessCode+"/")+ Eval("UpFileName").ToString()%>' target="_blank" ><%#Eval("UpFileName")%> </a>--%>
                                        <asp:ImageButton ID="btnAttachment" CommandArgument='<%#Eval("DefectRemarkId")%>' ToolTip="Show Attachment" ImageUrl="~/Modules/PMS/Images/paperclip.gif" OnClick="btnAttachment_Click" Visible='<%#Eval("FileName").ToString() != "" %>' runat="server" />
                                    </td>
                                    <td style="width:175px;">
                                        <%#Eval("EnteredByON")%>
                                    </td> 
                                    <td style="width:12px;">&nbsp;</td>
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
