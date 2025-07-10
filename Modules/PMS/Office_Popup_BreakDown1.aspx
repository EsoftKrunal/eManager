<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office_Popup_BreakDown1.aspx.cs"
    Inherits="Office_Popup_BreakDown1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function openremarkswindow(DN) {
            window.open('BreakdownRemarks.aspx?DN=' + DN, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');
        }
        function refreshparent() {
            window.opener.reloadunits();
        }
        function reloadunits() {
            __doPostBack('btnRefresh', '');
        }
        //function openattachmentwindow(DRI,VC) {
        //    window.open('PopupHistoryAttachment.aspx?DRI=' + DRI + '&VC=' + VC, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');
        //}
    </script>
   
    <style type="text/css">
        *
        {
            font-family: Calibri;
            font-size: 14px;
        }
        .pageheader, .pageheader span
        {
            text-align: center;
            padding: 8px;
            background-color: #4da6ff;
            color: White;
            font-size: 20px;
        }
        .myheading1
        {
            color: black;
            text-align: left;
            font-size: 16px;
            padding: 4px;
            border-bottom: solid 1px #ffcc80;
            background-color: #fff7e6;
            font-weight: bold;
        }
        .gridheader
        {
            background-color:rgb(136, 136, 136);
        }
        .border_table td
        {
            border: solid 1px #e5e5e5;
        }
        
        .btn
        {
            background-color: #888;
            padding: 0px;
            border: none;
            height: 30px;
            color: White;
        }
        .btnsel
        {
            background-color: #F95A15;
            padding: 0px;
            border: none;
            height: 30px;
            color: White;
        }
        
        input, select
        {
            padding: 3px;
        }
        input[type='checkbox']
        {
            padding: 0px;
            margin: 0px;
        }
        ul.tab
        {
            padding: 0px;
            margin: 0px;
            text-align: left;
            margin-top: 3px;
        }
        ul.tab li
        {
            padding: 0px;
            margin: 0px;
            display: inline-block;
        }
        .style1
        {
            width: 176px;
        }
        .bordered tr td
        {
            border:solid 1px #e9e9e9; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="pageheader">
            BreakDown Report No. :&nbsp;<asp:Label ID="lblNo" runat="server"></asp:Label></div>
        <ul class="tab" style="display:none">
            <li><asp:Button runat="server" CssClass="btnsel" Text="Breakdown Details" ID="btntab1" OnClick="btntab1_OnClick" Width="150px" /></li>
            <li><asp:Button runat="server" CssClass="btn" Text="Spare Consumption" ID="btntab2" OnClick="btntab2_OnClick" Width="150px" /></li>
            <li><asp:Button runat="server" CssClass="btn" Text="Attachments" ID="btntab3" OnClick="btntab3_OnClick" Width="150px" /></li>
        </ul>
        <div style="background-color: #F95A15; height: 4px">
            &nbsp;</div>
        <table cellpadding="3" cellspacing="0" style="width: 100%; border-collapse: collapse;" border="0" class="bordered">
            <tr>
                                <td style="text-align: left; font-weight: bold; width: 180px;">
                                    Component Code&nbsp; &amp; Name</td>
                                <td style="text-align: left; font-weight: bold;width: 10px;">
                                    :
                                </td>
                                <td style="text-align: left; font-weight: bold; width:500px;">
                                    &nbsp;<asp:Label ID="lblCompCode" runat="server"></asp:Label> /  <asp:Label ID="lblCompName" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 130px;">
                                    <strong>Component Category</strong>
                                </td>
                                <td style="text-align: left;width: 10px;">:</td>
                                <td style="text-align: left;">&nbsp;<asp:Label runat="server" ID="lblCStatus"></asp:Label></td>
                                <td style="text-align: left; font-weight: bold; width: 80px;"><strong>Job&nbsp;&nbsp;Details</strong></td>
                                <td class="style3" style="text-align: left; font-weight: bold;width: 10px;">:</td>
                                <td style="text-align: left; font-weight: bold;">&nbsp;<asp:Label ID="lblJob" runat="server"></asp:Label></td>
                            </tr>
        </table>
        <div id="divTab1" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%; text-align: left;">
                         <table cellpadding="3" cellspacing="0" style="width: 100%; border-collapse: collapse;" border="0">
                            <tr>
                                <td style="text-align: left; font-weight: bold; width:180px;">
                                    <b>Report Date</b>
                                </td>
                                <td class="style3" style="text-align: left; font-weight: bold;">
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="txtReportDt" runat="server" MaxLength="15" Style="text-align: center" ></asp:Label>
                                    <%--<asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                        PopupButtonID="txtReportDt" PopupPosition="TopLeft" TargetControlID="txtReportDt">
                                    </asp:CalendarExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <strong>Breakdown Details  </strong>
                                </td>
                                <td style="text-align: left">
                                 :
                                </td>
                                <td style="text-align: left">
                                <asp:Label ID="txtDefectdetails" runat="server"  TextMode="MultiLine" ></asp:Label>
                                </td>
                            </tr>
                              <tr id="trCompStatus" runat="server">
                                <td style="text-align: left; font-weight: bold;">
                                    <b>Component Status</b>
                                </td>
                                <td style="text-align: left; font-weight: bold;">
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblCompStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                           <tr>
                                <td style="text-align: left;">
                                    <strong>Repairs to be carried out by</strong>
                                </td>
                                <td style="text-align: left;">
                                    :
                                </td>
                                <td style="text-align: left;">
                                 
                                    <table cellpadding="3" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td></td>
                                        <td colspan="2" style="text-align:center"><b>Office</b></td>
                                        <td><asp:CheckBox ID="ChkDrydock" runat="server" Text="Drydock"  Enabled="false"/></td>
                                    </tr>
                                    <tr>
                                        <td><asp:CheckBox ID="chkVessel" runat="server" Text="Vessel" Enabled="false"  /></td>
                                        <td><asp:CheckBox ID="chkSpares" runat="server" Text="Spares"  Enabled="false"/></td>
                                        <td><asp:CheckBox ID="chkShAssist" runat="server" Text="Shore Assistance"  Enabled="false"/></td>
                                        <td><asp:CheckBox ID="chkGuarantee" runat="server" Text="Guarantee"  Enabled="false"/></td>
                                    </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                 <td  colspan="3">
                                     <div id="divTab2" runat="server">
            <div class="myheading1">
                Spare </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse">
                        <tr>
                            <td style="text-align: left; ">
                                <table style="width: 100%; border-collapse: collapse" border="1">
                                    <tr>
                                        <td style="width:140px" >Available On Board :</td>
                                        <td style="width:80px" ><strong><asp:Label ID="lblSOB" runat="server" ></asp:Label></strong></td>
                                        <td style="text-align: left; font-weight: bold; width:70px;">&nbsp;</td>
                                        <td style="text-align: left; width: 120px;">&nbsp;</td>
                                        
                                        <td style="text-align: left; width: 80px; font-weight: bold">&nbsp;</td>
                                        <td style="text-align: left;">                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Spares Required ?&nbsp; :</td>
                                        <td >
                                            <strong><asp:Label ID="lblSparesReqd" runat="server" ></asp:Label></strong>
                                        </td>
                                        <td style="text-align: left; ">
                                            Reqn No. :&nbsp;
                                        </td>
                                        <td style="text-align: left;font-weight: bold">
                                            <asp:Label ID="txtRqnNo" runat="server" MaxLength="20" ></asp:Label>
                                        </td>
                                        
                                        <td style="text-align: left;  ">
                                            Reqn Date :&nbsp;
                                        </td>
                                        <td style="text-align: left;font-weight: bold">
                                            <asp:Label ID="txtRqnDt" runat="server" MaxLength="15" ></asp:Label>                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="bordered gridheader">
                                                 <colgroup>
                                                        <col />
                                                        <col style="width: 200px;" />
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 30px;" />
                                                    </colgroup>
                                                    <tr align="left" >
                                                        <td style="text-align:left">
                                                           Spare Name
                                                        </td>
                                                        <td>
                                                            Part#
                                                        </td>
                                                        <td>
                                                            Qty ( Cons. )
                                                        </td>
                                                        <td>
                                                            Qty ( ROB )
                                                        </td>
                                                    </tr>
                                            </table>
                                            <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="bordered">
                                                    <colgroup>
                                                        <col />
                                                        <col style="width: 200px;" />
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 30px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptComponentSpares" runat="server">
                                                        <ItemTemplate>
                                                            <tr visible='<%#Eval("SpareId").ToString() != "" %>'>
                                                                <td align="left">
                                                                    <%#Eval("SpareName")%>
                                                                    <asp:HiddenField ID="hfdComponentId" runat="server" Value='<%#Eval("ComponentId") %>' />
                                                                    <asp:HiddenField ID="hfOffice_Ship" runat="server" Value='<%#Eval("Office_Ship") %>' />
                                                                    <asp:HiddenField ID="hfSpareId" runat="server" Value='<%#Eval("SpareId") %>' />
                                                                    <span style="font-size: 9px; color: blue;"><i>
                                                                        <%#Eval("Maker")%></i></span>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("PartNo")%>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblQtyCons" runat="server" Text='<%#Eval("QtyCons")%>'></asp:Label>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblQtyRob" runat="server" Text='<%#Eval("QtyRob")%>'></asp:Label>
                                                                    </td>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
                                 </td>
                             </tr>
                             <tr>
                                <td style="text-align: left; font-weight: bold;">
                                    Chief Engineer
                                </td>
                                <td class="style3" style="text-align: left;">
                                    :
                                </td>
                                <td style="text-align: left; font-weight: bold;">
                                    <asp:Label ID="txtCE" runat="server" MaxLength="50" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <strong>Chief Officer</strong>
                                </td>
                                <td class="style3" style="text-align: left;">
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="txtCO" runat="server" MaxLength="50" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <strong>Supdt. Name</strong>
                                </td>
                                <td class="style3" style="text-align: left;">
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="txtSupdt" runat="server" MaxLength="50" Width="250px"></asp:Label>
                                </td>
                            </tr>
                            </table>
                    </td>
                    <td>
                        <table width="100%">
                                <col width="150px" />
                            <col />
                                <tr>
                                    <td style="text-align: left; ">
                                        <strong>Repairs Carried Out :</strong>
                                    </td>
                                    <td style="text-align: left; ">
                                        <asp:Label ID="txtRepairsCarriedout" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                            <tr>
                                <td style="text-align: left; "><strong>Target Closure Date</strong></td>
                                <td style="text-align: left; ">
                                    <asp:Label ID="txtTargetDt" runat="server" MaxLength="15" Style="text-align: center" Width="80px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="divTab3" runat="server">
        
            <div class="myheading1">Attachments</div>
             <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="bordered">
                        <colgroup>
                            <col />
                            <col style="width: 30px;" />                           
                        </colgroup>
                        <asp:Repeater ID="rptAttachment" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="left">
                                        <%#Eval("AttachmentText")%>
                                    </td>
                                    <td>
                                        <a runat="server" id="ancFile" href='<%# ProjectCommon.getLinkFolder(DateTime.Parse(txtReportDt.Text.Trim())) + Eval("FileName").ToString()  %>'
                                            target="_blank" visible='<%# Eval("FileName").ToString() != "" %>' title="Attachment">
                                            <img src="Images/paperclip.gif" style="border: none" />
                                        </a>
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
        </div>
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="2">
                                        <div class="myheading1">Ship Breakdown Closure :</div>
            <table width="100%" id="trClosure" runat="server">
                <tr>
                    <Td style="width:100px; text-align:left">Closure Date :</Td>
                    <td style="text-align:left" class="style1">
                       <asp:Label ID="txtCompletionDt" runat="server" MaxLength="15" Style="text-align: center" Width="90px"></asp:Label>                                
                        <asp:Label ID="lblCompDt" runat="server"></asp:Label>                    
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
            </table>
           
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2">
                                         <div class="myheading1" style="overflow:auto">
                <span style="float:left; margin-top:3px;">Office Closure & RCA :</span>
                <span style="float:left">
                    <asp:ImageButton ID="btnAddRemarks" runat="server" CssClass="style5" OnClick="btnAddRemarks_Click" Text="Add Remarks" ImageUrl="~\Images\add.png" />                
                </span>
                    </div>
                     <table border="1" cellpadding="4" style="width: 100%; border-collapse: collapse;font-size:12px;" runat="server" id="pnlofficecomments" visible="false">
                             <tr>
                                 <td style="text-align:left;width:100px;">Classification :</td>
                                 <td style="text-align:left"><asp:Label runat="server" ID="lblclassification"></asp:Label></td>
                                 <td style="text-align:left;width:100px;">RCA Requried : </td>
                                 <td style="text-align:left"><asp:Label runat="server" ID="lblRCARequried"></asp:Label></td>
                             </tr>
                             <tr>
                                 <td style="text-align:left">RCA No. :</td>
                                 <td style="text-align:left"><asp:Label runat="server" ID="lblRCANo"></asp:Label></td>
                                 <td style="text-align:left">Remarks : </td>
                                 <td style="text-align:left"><asp:Label runat="server" ID="lblRemarks"></asp:Label></td>
                             </tr>
                         </table>
                                 </td>
                            </tr>
                            </table>
                    </td>
                </tr>
           </table>
            
        </div>
        
         
            
        
    </div>
       <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvOfficeComments" visible="false" >
            <div style="position:fixed;top:0px;left:0px; right:0px;bottom:0px; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; padding :3px; text-align :center;z-index:150;top:100px;">
            <center>
                <div style="padding :0px; text-align :center; width:800px; border:solid 5px #F95A15; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                    <div style="padding:10px; background-color:rgb(136, 136, 136);color:white">
                        Office Closure & RCA
                    </div>
                <table border="1" cellpadding="4" style="width: 100%; border-collapse: collapse;font-size:12px;">
                         <tr>
                             <td style="text-align:left;width:100px;">Classification :</td>
                             <td style="text-align:left">
                                  <asp:RadioButtonList ID="ddlclassification" runat="server" RepeatDirection="Horizontal">
                                                     <asp:ListItem Text="Minor" Value="1"></asp:ListItem>
                                                     <asp:ListItem Text="Major" Value="2"></asp:ListItem>
                                                     <asp:ListItem Text="Severe" Value="3"></asp:ListItem>
                                                 </asp:RadioButtonList>

                             </td>
                             <td style="text-align:left;width:100px;">RCA Requried : </td>
                             <td style="text-align:left"><asp:CheckBox runat="server" ID="chkrequried" /></td>
                         </tr>
                         <tr>
                             <td style="text-align:left;width:100px;">RCA No. :</td>
                             <td style="text-align:left"><asp:TextBox runat="server" ID="txtRCARequried"></asp:TextBox></td>
                             <td></td>
                             <td></td>
                         </tr>
                        <tr>
                            <td style="text-align:left;width:100px;">Remarks : </td>
                            <td style="text-align:left" colspan="3"><asp:TextBox runat="server" ID="txtremarks" TextMode="MultiLine" Rows="10" Width="95%"></asp:TextBox></td>
                        </tr>
                     </table>
                     <div style="padding:5px">
                         <asp:Button ID="btnsave" OnClick="btnOfficeClosure_Click" runat="server" Text="Save" Width="100px"/>
                         <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Text="Close" Width="100px"/>
                      </div>
                </div>
                </center>
               
            </div>
            
       </div>
        <div style="background-color: #F95A15; height: 4px">
            &nbsp;</div>
       <div style="padding:5px; text-align:center; display:none;">
                
                <asp:Button ID="btnPrint" runat="server" CssClass="style5" OnClick="btnPrint_Click" Text="Print" Visible="false" Width="100px" />
                <asp:Button ID="btnReduceImage" runat="server" CssClass="style5" OnClick="btn_Reduce_Image_Click" Text=" Reduce Image " Width="100px" />
           
            </div>
    </form>
</body>
</html>
