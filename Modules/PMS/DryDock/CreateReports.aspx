<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateReports.aspx.cs" Inherits="DryDock_CreateReports" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
   <link href="../../../css/app_style.css" rel="stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <script src="../JS/KPIScript.js" type="text/javascript"></script>
    <style type="text/css">
    .dv_sel
    {
        background-color:#66CCFF;
    }
    .dv_sel a
    {
        color:White;
        
    }
    
    </style>
    </head> 
<body style="text-align: center; margin:0px 0px 0px 0px;">
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
  <ContentTemplate>
    <table border="0" cellSpacing="0" cellPadding="0" width="100%" >
        <tr>
            <td style=" padding:10px;  text-align :center; font-size:14px; " class="text headerband">Dry Dock Report
    <asp:HiddenField runat="server" ID="hfd_INSPID" />              
            </td>
        </tr>
        <tr>
            <td style="">
                <table border="0" cellSpacing="0" cellPadding="5" width="100%" >
                        <tr>
                            <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;font-size:12px;" width="10%">Docket#:</td>
                            <td style="TEXT-ALIGN: left" width="15%">
                                <asp:Label id="txtDocketNo" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;font-size:12px; " width="10%">Start Date:</td>
                            <td style="TEXT-ALIGN: left" width="15%">
                                <asp:Label id="txtStartDate" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;font-size:12px;" width="10%">End Date:</td>
                             <td style="TEXT-ALIGN: left">
                                <asp:Label id="txtEndDate" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                             <td style="PADDING-RIGHT: 10px;TEXT-ALIGN: right;font-size:12px;" width="10%">Vessel Name:</td>
                             <td style="TEXT-ALIGN: left">
                                <asp:Label id="txtVesselName" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                    </TABLE>
            </td>
        </tr>
        <tr>
            <td style="TEXT-ALIGN: left;">
            <div style="border:solid 0px gray; float:right; width:100%; float:left;">
            <table cellpadding="0" cellspacing="0" border="1" width='100%' style="border:solid 1px #c2c2c2; border-collapse:collapse;">
            <tr>
            <td style="width:450px;">
                <div style="height:25px; overflow-x:hidden; overflow-y:scroll;">
                <table cellpadding="4" cellspacing="0" border="1" width='100%' style="border:solid 1px white; background-color:#e2e2e2; border-collapse:collapse;">
                <tr>
                <td style="width:4%; text-align:center; font-size:12px; ">Sr#</td>
                <td style="text-align:left;font-size:12px;width:92%;">Category</td>
                <td style="width:4%; text-align:center ;font-size:12px;"></td>
                </tr>
                </table>
                </div>
                <div style="height:500px; overflow-x:hidden; overflow-y:scroll;">
                 <table cellpadding="4" cellspacing="0" border="1" width='100%' style="border:solid 1px #e2e2e2; border-collapse:collapse;">
                    <asp:Repeater runat="server" ID="rptQuestions">
                    <ItemTemplate>
                    <tr class='<%#(Eval("CatId").ToString().Trim()==CatId.ToString())?"dv_sel":"dv"%>'>
                    <td style="width:4%; text-align:center; font-size:12px;">
                        <asp:LinkButton runat="server" ID="lnkRow" Text='<%#Eval("Srno")%>' CommandArgument='<%#Eval("CatId")%>' OnClick='Select_Row' Font-Underline="false" >  </asp:LinkButton>

                    </td>
                    <td style="font-size:12px;width:92%;">
                        <div style="height:15px; overflow:hidden;">
                        <asp:LinkButton runat="server" ID="LinkButton1" Text='<%#Eval("CatCode").ToString() + " " +  Eval("CatName").ToString()%>' Font-Underline="false" CommandArgument='<%#Eval("CatId")%>' OnClick='Select_Row'></asp:LinkButton><span style='color:Red'><%#(Eval("CatName").ToString().Trim() == "") ? "*" : ""%></span>
                        </div>
                    </td>
                    <td style="width:4%; text-align:center; font-size:12px;">
                        <div runat="server" visible='<%#(Common.CastAsInt32(Eval("NOR"))>0)%>'>
                        <a href='DockingProgressComments.aspx?DocketId=<%#DocketId.ToString()%>&RFQId=&CatId=<%#Eval("CatId")%>' target="_blank" >
                        <img src="../Images/icon_comment.gif" title="View Comments" style="border:none" />
                        </a>
                        </div>
                    </td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
            </td>
            <td style="font-weight:bold; height:18px;">
                <table cellpadding="4" cellspacing="0" border="0" width='100%' style="border:solid 1px white; border-collapse:collapse;background-color:#e2e2e2; ">
                <tr>
                <td style="font-size:12px;">&nbsp;Description</td>
                </tr>
                </table>
                <asp:TextBox runat="server" ID="txtDescr" TextMode="MultiLine" BorderWidth="0" style="height:500px; width:100%;"></asp:TextBox>
                 
            </td>
            <td style="width:180px;">
            <table cellpadding="4" cellspacing="0" border="0" width='100%' style="border:solid 1px white; border-collapse:collapse;background-color:#e2e2e2; ">
                <tr>
                <td style="font-size:12px;">&nbsp;Pictures</td>
                </tr>
            </table>
                        <asp:UpdatePanel runat="server" ID="up2">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnPost" onclick="btnPost_Click" style="display:none " /> 
                            <div style=" height :500px; width:180px; overflow-y :scroll;overflow-x :hidden" class="input_box"  >
                            <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" CellSpacing="10"  >
                            <ItemTemplate>
                            <div style=" text-align :center; border:solid 1px #d2d2d2;box-shadow: 5px 5px 5px #999999;" >
                            <div style=" position :relative; vertical-align:top;">
                            <asp:ImageButton runat="server" ID="btnDel" ToolTip="Delete this Image.." OnClientClick="return confirm('Are you sure to remove this Image.');" filename='<%#Eval("FileName")%>' style="position:absolute;left:112px; cursor:pointer;" ImageUrl="~/Modules/PMS/Images/DeleteSquare.png" CommandArgument='<%#Eval("TableId")%>' OnClick="btnDel_Click" />
                            <a target="_blank" href='<%# "..\\DryDock\\ImageUpdate.aspx?TableId=" + Eval("TableId")%>' style="font-size:10px">
                                <img style=" border:none" src='<%# "..\\DryDock\\UploadFiles\\" + Eval("FileName")%>' title='<%#Eval("Caption") %>' width="130" style="font-size:10px"  />
                            </a>
                            </div>
                            </div>
                            </ItemTemplate>
                            </asp:DataList>
                            </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
            </td>
            </tr>
            </table>
            </div>
           
            </td>
        </tr>
    </table>
    <div style="padding:3px">

        <asp:Button ID="btn_Save" runat="server" CssClass="btn" onclick="btn_Save_Click" Text="Save" Enabled="false" Width="100px"/>
        <asp:Button ID="btnUploadMore" runat="server" CssClass="btn" Text="Upload Picture" OnClick="btnUploadMore_Click" Enabled="false" Width="150px"/>
        <asp:Button ID="btnClose1" runat="server" CssClass="btn" Text="Close" OnClientClick="window.close();return false;" Width="100px" />
        <asp:Button ID="btnNotify" runat="server" CssClass="btn" onclick="btn_Notify_Click" Text="Notify" Width="100px" OnClientClick="return ConfirmNotify(this);"  />
        <asp:Button ID="btnApprove" runat="server" CssClass="btn" onclick="btn_Approve_Click" Text="Approve" Width="100px" OnClientClick="return ConfirmApprove(this);" />
        <asp:Button ID="btn_Publish" runat="server" CssClass="btn" onclick="btn_Publish_Click" Text="Publish" Width="100px"/>
        <asp:Button ID="tbnDownload" runat="server" CssClass="btn" onclick="tbnDownload_Click" Text="Download" Width="100px"/>

        <script type="text/javascript">
            function ConfirmNotify(ctl) {
                if (window.confirm('Are you sure to notify to GM/DGM ?')) {
                    $(ctl).val('Processing..');
                    return true;
                }
                else
                    return false;
            }
            //----------------------------
            function ConfirmApprove(ctl) {
                if (window.confirm('Are you sure to approve ?')) {
                    $(ctl).val('Processing..');
                    return true;
                }
                else
                    return false;
            }
    </script>

    </div>

<asp:Label id="lblmessage" runat="server" ForeColor="#C00000" Font-Size="14px"></asp:Label>
    <div style="position:absolute;top:0px;left:0px; height:100%; width:100%; " id="dvUploadFiles" runat="server" visible="false">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:900px; height:510px;text-align :center;background : white; z-index:150;top:50px; border:solid 5px black;">
         <center >
         <iframe runat="server" id="frmUpload" width='890px' height='500px'></iframe>
            <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="[X] Close Window " OnClick="btnClose_OnClick" style="margin-top:10px; background-color:Red; border:solid 1px white; width:130px"/>
         </center>
         </div>
     </center>
     </div>
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_Save" />
        <asp:PostBackTrigger ControlID="tbnDownload" />
         <asp:PostBackTrigger ControlID="btnNotify" />
         <asp:PostBackTrigger ControlID="btnApprove" />
    </Triggers>
</asp:UpdatePanel>
</form>
</body>
</html>

