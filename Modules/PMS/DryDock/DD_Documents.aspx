<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_Documents.aspx.cs" Inherits="Docket_DD_Documents" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>eMANAGER</title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script> 
    <link href="../../../css/app_style.css" rel="stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />

</head>
<body>
    <div id="dvMain"  style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40); display:none; "><div style="font-size:25px; font-weight:bold; text-align:center; vertical-align:middle; color:White; top:400px; padding:200px 0px;">Loading.......</div></div> 
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style=" text-align:center;  font-size:15px; " class="text headerband"><b>DOCUMENTS</b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr>
        <td>
        <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; width:"><b>Docket #</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label></td>
            <td style="text-align:right"><b>Vessel</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right"><b>Type</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td>
                <asp:Button runat="server" ID="btnAddJob" Text="Add New Document" OnClick="btnAddDoc_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:150px;"  />
            </td>
            </tr>
            </table>
        </div>
        </td>
        </tr>
        <tr>
        <td>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 25px ; text-align:center; background-color:#CCEBF5;" class="scrollbox">
            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px;" bordercolor="white">
                    <colgroup>
                    <col style="width:2%;" />
                    <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:74%;" />
                    <col style="width:10%;" />
                    <col style="width:2%;" />
                     </colgroup>

                    <tr class= "headerstylegrid">
                    <td style="text-align:center;width:2%;"></td>
                    <td style="text-align:center;width:4%;"><b>SR# </b></td>
                    <td style="text-align:center;width:4%;"><b>Edit </b></td>
                    <td style="text-align:center;width:4%;"><b>Delete </b></td>
                    <td style="text-align:left;width:74%;"><b>Document Name </b></td>
                    <td style="text-align:center;width:10%;"><b>Attachment</b></td>                                    
                    <td style="width:2%;">&nbsp;</td>
                </tr>
                </table>
             </div>
             <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 385px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                            <col style="width:2%;" />
                    <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:74%;" />
                    <col style="width:10%;" />
                    <col style="width:2%;" />
                             </colgroup>
                                <asp:Repeater ID="rptDocuments" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                                <td style="width:2%;">&nbsp;</td>
                                                <td style="text-align:center;width:4%;"><%#Eval("SNo")%></td>
                                                <td align="center" style="width:4%;"><asp:ImageButton runat="server" ToolTip="Edit" OnClick="btnEdit_Click" CommandArgument='<%#Eval("DocumentId")%>' ImageUrl="~/Modules/PMS/Images/12-em-pencil.png" /></td>
                                                <td align="center" style="width:4%;"><asp:ImageButton runat="server" ToolTip="Delete" OnClick="btnDelete_Click" CommandArgument='<%#Eval("DocumentId")%>' ImageUrl="~/Modules/PMS/Images/cancel.png" style="height:12px;" OnClientClick="return confirm('Are you sure to delete ?.');"/></td>
                                                <td align="left" style="width:74%;"> <%#Eval("documentname")%></td>
                                                <td align="center" style="width:10%;"><asp:ImageButton runat="server" ToolTip="Select" ID="btnDocumentDownload" OnClick="btnDocumentDownload_Click" CommandArgument='<%#Eval("DocumentId")%>' ImageUrl="~/Modules/PMS/Images/paperclip.gif"  /></td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>
            </div>
        </td>
        </tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
         <div style="text-align:right; padding:3px;">
            <div style="text-align:left;float:left; padding-top:5px;">
                 <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div style="text-align:left;float:right; padding:3px;">
                
            </div>
         </div>
        </td> 
     </tr>
     </table>
     </div>

    <%-- Add Sub Jobs --%>

    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_A_SubJobs" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 800px; height: 160px; padding: 3px; text-align: center;background: white; z-index: 150; top: 150px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel411">
                                <ContentTemplate>
                                 <asp:Panel runat="server" id="pnlNew">
                                   <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                   <tr>
                                        <td colspan="2" style="text-align: center;  font-size:14px; " class="text headerband" >
                                            Upload New Document</td>
                                   </tr>
                                   <tr>
                                        <td style="text-align:right; width:120px;">Document Name :</td>
                                        <td style="text-align:left;">
                                            <asp:TextBox ID="txt_DocName" runat="server" Width="99%"></asp:TextBox>
                                        </td>                  
                                   </tr>
                                   <tr >
                                            <td style="text-align:right;">Attachment :</td>
                                            <td style="text-align:left;">
                                            <asp:FileUpload runat="server" id="ftp_A_Upload" />
                                            </td>                  
                                         </tr>
                                   </table>
                                 </asp:Panel>
                                 <div style="text-align:center; padding:5px;">
                                            <div style="text-align:left;float:left; padding-top:5px;">
                                                <asp:Label ID="lbl_A_MsgSubJob" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:left;float:right">
                                                <asp:Button runat="server" ID="btn_SaveDoc" Text="Save" OnClick="btn_SaveDoc_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btn_CloseDoc" Text="Close" OnClick="btn_CloseDoc_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            </div>
                                </div>

                                </ContentTemplate>
                                <Triggers>
                                <asp:PostBackTrigger ControlID="btn_SaveDoc" />
                                  <asp:PostBackTrigger ControlID="btn_CloseDoc" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
    </div>

    </form>
</body>
</html>
