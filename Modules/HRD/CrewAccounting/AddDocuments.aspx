<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDocuments.aspx.cs" Inherits="CrewAccounting_AddDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Documents</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript">
    window.resizeTo(800,600);            
    </script>
    
    <script type="text/javascript">
        function OpenDocument(TableID) {
            window.open("ShowDocuments.aspx?TableID=" + TableID + "");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            
            <table cellpadding="2" cellspacing="2" width="100%" style="margin:auto">
                <col />
                <col width="68px" />
                <tr style="background-color:#c2c2c2; font-size:large; font-weight:bold; text-align:center;">
                    <td colspan="3">
                        File Upload
                    </td>
                </tr>
                </table>
                
                
                
                <table cellpadding="2" cellspacing="2" width="650px" style="margin:auto; font-size:13px; font-weight:bold; text-align:left;" rules="none" border="0">
                <col width="110px" />
                <col />
                <tr>
                    <td align="right" >Crew# :</td>
                    <td align="left" >
                        <asp:Label ID="lblCrewNo" runat="server" style="font-weight:normal;" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" >Crew Name :</td>
                    <td align="left" >
                        <asp:Label ID="lblCrewName" runat="server" style="font-weight:normal;"></asp:Label>
                    </td>
                </tr>
                </table>
                
                
            <table id="tblAddPanel" runat="server" cellpadding="2" cellspacing="2" width="650px" style="margin:auto; margin-top:25px;text-align:left;" border="0">
                <col width="110px" />
                <col />
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td align="right" >Attach File :</td>
                    <td >
                        <asp:FileUpload ID="FU" runat="server" Width="100%" CssClass="input_box" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Document Name :
                        
                    </td>
                    <td >
                        <asp:TextBox ID="txtDocName" runat="server" Width="369px" CssClass="input_box" 
                            MaxLength="50" Height="17px"></asp:TextBox>                        
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Width="68px"  OnClick="btnSave_OnClick" Text="Upload" style="float:right;" CssClass="btn" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;" >
                        <asp:Label ID="lblmsg" runat="server" style="color:Red; font-weight:bold;" ></asp:Label>
                    </td>
                </tr>
                </table>
                
                
                
                <table cellpadding="2" cellspacing="2" width="650px" style="margin:auto; margin-top:25px;text-align:left;" border="0">
                <tr>
                    <td colspan="2" >
                    
                        <table cellpadding="2" cellspacing="0" width="650px" style="margin:auto;" >
                        <col width="50px" /> <col /> <col width="100px" />
                        <tr class="headerstylegrid" style="font-weight:bold;">
                            <td>Delete</td>
                            <td>File Name</td>
                            <td>Attachment</td>
                        </tr>
                        <asp:Repeater ID="rptDocuments" runat="server" >
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/delete1.gif" Visible='<%#(Page.Request.QueryString["Mode"].ToString()=="E") %>' OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete the file?')" /> 
                                        <asp:HiddenField ID="hfTableID" runat="server" Value='<%#Eval("TableID")%> ' />
                                   </td>
                                    <td> <%#Eval("DocumentName")%> </td>
                                    <td> 
                                        <a onclick="OpenDocument(<%#Eval("TableID")%>)" style="cursor:pointer;"> 
                                        <img src="../Images/paperclip12.gif" /> 
                                        </a>
                                    </td>
                                </tr> 
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                        
                    </td>
                </tr>
                
            </table>
    </div>
    </form>
</body>
</html>
