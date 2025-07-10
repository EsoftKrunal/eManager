<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadObservationDocs.aspx.cs" Inherits="Transactions_UploadObservationDocs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Upload Docs</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
      <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager  ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
        <center>
                <table cellpadding="2" cellspacing="0" border="0" width="99%">
                    <tr>
                        <td>
                         <table border="0" cellpadding="2" cellspacing="0" style="width:100%;border-collapse:collapse;">
                            <col />                                        
                            <col width="250px"/>
                            <col width="100px"/>
                                <tr style="font-weight:bold;">
                                <td style="text-align:left" >Description :</td>                                        
                                <td align="right">Attachment</td>
                                <td align="right"></td>
                                </tr>
                            <tr>
                                <td style="text-align:left" >
                                        <asp:TextBox ID="txt_Desc" CssClass="input_box" MaxLength="50" runat="server" Width="550px" ></asp:TextBox>
                                    </td>
                                <td style="text-align:left" >
                                        <asp:FileUpload ID="flAttachDocs" CssClass="input_box" Width="200px" runat="server" />
                                    </td>                                         
                                    <td>
                                    <asp:Button ID="btnSaveDoc" Text="Save Document" CssClass="btn" runat="server" 
                                            CausesValidation="true" onclick="btnSaveDoc_Click" />
                                    </td>
                            </tr>
                            </table>
                         <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;margin-top:10px;">
                                    <colgroup>
                                        <col style="width: 2%;" />
                                        <col style="width: 5%;" />
                                        <col style="width: 61%;"/>
                                        <col style="width: 15%;" />
                                        <col style="width: 10%;"/>
                                        <col style="width: 5%;"/>
                                        <col style="width: 2%;" />
                                        </colgroup>
                                        <tr align="center" class= "headerstylegrid" >                                            
                                            <td style="width: 2%;">Edit</td>
                                            <td style="width: 5%;">Delete</td>
                                            <td align="left" style="width: 61%;">
                                                Description
                                            </td>
                                            <td style="width: 15%;">
                                                Uploaded By
                                            </td>
                                            <td style="width: 10%;">
                                                Uploaded On
                                            </td>
                                            <td style="text-align:center;width: 5%;">    
                                                <img src="../Modules/HRD/Images/paperclip.gif" style="border:none"  />
                                            </td>
                                            <td style="width: 2%;"></td>
                                        </tr>
                                    
                                </table>
                         <div id="dvDocs" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 270px; text-align: center; border:solid 1px gray;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                        <colgroup>
                                            <col style="width: 2%;" />
                                        <col style="width: 5%;" />
                                        <col style="width: 61%;"/>
                                        <col style="width: 15%;" />
                                        <col style="width: 10%;"/>
                                        <col style="width: 5%;"/>
                                        <col style="width: 2%;" />
                                            
                                        </colgroup>
                                        <asp:Repeater ID="rptDocs" runat="server">
                                            <ItemTemplate>
                                                <tr class="row">
                                                    <td style="text-align:center;width: 2%;">    
                                                        <asp:ImageButton ID="imgEditDoc" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditDoc_OnClick" />  <%--Visible='<%#(Mode!="V") %>' --%>
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    </td>
                                                    <td style="text-align:center;width: 5%;">    
                                                        <asp:ImageButton ID="imgDelDoc" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelDoc_OnClick" OnClientClick="return confirm('Are you sure to delete?')" />  <%--Visible='<%#(Mode!="V") %>'--%>
                                                    </td>
                                                    <td style="width: 61%;">
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>                                                        
                                                    </td>
                                                    <td align="center" style="width: 15%;">
                                                      <%#Eval("UploadedBy")%>
                                                    </td>
                                                    <td align="right" style="width: 10%;"> 
                                                       <%#Eval("UploadedDate")%>
                                                    </td>
                                                    <td style="text-align:center;width: 5%;">    
                                                        <a runat="server" ID="ancdoc"  href='<%#"~/EMANAGERBLOB/Inspections\\Observation\\" + Eval("FileName").ToString() %>' target="_blank"  title="Show Doc" visible='<%#Eval("FileName")!=""%>' >
                                                         
                                                       <img src="../Images/paperclip.gif" style="border:none"  /></a>  
                                                    </td>
                                                    <td style="width:2%"></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                    </table>
                                </div>
                        </td>
                    </tr>
                    
                </table>
        </center>
    </div>
    </form>
</body>
</html>
