<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_BreakDown.aspx.cs"
    Inherits="Popup_BreakDown" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
      <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function openaddsparewindow(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
        function openremarkswindow(DN) {

            window.open('DefectRemarks.aspx?DN=' + DN, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
        function refreshparent() {
            window.opener.reloadunits();
        }
        function reloadunits() {
            __doPostBack('btnRefresh', '');
        }
        function OpenReport() {
            //window.open('~/Reports/Office_BreakdownDefectReport.aspx?DN=''&fm=', '', '')
        }
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
            background-color: #5CAEFF;
        }
        .border_table td
        {
            border: solid 1px #e5e5e5;
        }
        
       /* .btn
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
        }*/
        
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
            width: 143px;
            text-align: right;
        }
        .style2
        {
            width: 111px;
        }
        .bordered tr td
        {
            border: solid 1px #e5e5e5;
        }
        
        .auto-style1 {
            height: 20px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="text headerband">
            Defect No. :&nbsp;<asp:Label ID="lblNo" runat="server"></asp:Label></div>
        <ul class="tab" runat="Server" visible="false">
            <li><asp:Button runat="server" CssClass="btnsel" Text="Defect Details" ID="btntab1" OnClick="btntab1_OnClick" Width="150px" /></li>
            <li><asp:Button runat="server" CssClass="btn" Text="Spare Consumption" ID="btntab2" OnClick="btntab2_OnClick" Width="150px" /></li>
            <li><asp:Button runat="server" CssClass="btn" Text="Attachments" ID="btntab3" OnClick="btntab3_OnClick" Width="150px" /></li>
        </ul>
        <div style="background-color: #F95A15; height: 4px">
            &nbsp;</div>
        <div id="divTab1" runat="server">
        <table width="100%">
        <tr>
        
        <td>
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
                                <td class="style3" style="text-align: left; font-weight: bold;;width: 10px;">:</td>
                                <td style="text-align: left; font-weight: bold;">&nbsp;<asp:Label ID="lblJob" runat="server"></asp:Label></td>
                            </tr>
                            </table>
                            </td></tr>
                            </table>
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
                                    <asp:TextBox ID="txtReportDt" runat="server" MaxLength="15" Style="text-align: center" Width="80px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                        PopupButtonID="txtReportDt" PopupPosition="TopLeft" TargetControlID="txtReportDt">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                          
                           
                           
                            <tr>
                                <td style="text-align: left">
                                    <strong>Defect Details  </strong>
                                </td>
                                <td style="text-align: left">
                                 :
                                </td>
                                <td style="text-align: left">
                                <asp:TextBox ID="txtDefectdetails" runat="server" Height="240px" MaxLength="1000"
                            TextMode="MultiLine" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                              <tr id="trCompStatus" runat="server">
                                <td style="text-align: left; font-weight: bold;">
                                    <b>Component Status</b>
                                </td>
                                <td style="text-align: left; font-weight: bold;">
                                    :
                                </td>
                                <td style="text-align: left; font-weight: bold;">
                                    <asp:DropDownList ID="ddlCompStatus" runat="server" Width="250px">
                                        <asp:ListItem Text="&lt;Select&gt;" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                           <tr>
                                <td style="text-align: left;">
                                    <strong>RESPONSIBILITY</strong>
                                </td>
                                <td style="text-align: left;">
                                    :
                                </td>
                                <td style="text-align: left;">
                                 <table cellpadding="3" cellspacing="0" border="1" style="width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td></td>
                                        <td colspan="2" style="text-align:center"><b>OFFICE</b></td>
                                        <td><asp:CheckBox ID="ChkDrydock" runat="server" Text="Drydock" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:CheckBox ID="chkVessel" runat="server" Text="Vessel" /></td>
                                        <td><asp:CheckBox ID="chkSpares" runat="server" Text="MTM-Spares"  /></td>
                                        <td><asp:CheckBox ID="chkShAssist" runat="server" Text="MTM-Shore Assistance"   /></td>
                                        <td><asp:CheckBox ID="chkGuarantee" runat="server" Text="Guarantee"  /></td>
                                    </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <strong>Target Closure Date</strong>
                                </td>
                                <td style="text-align: left;">:</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtTargetDt" runat="server" MaxLength="15" Style="text-align: center"
                                        Width="80px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                        PopupButtonID="txtTargetDt" PopupPosition="TopLeft" TargetControlID="txtTargetDt">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            </table>
                            <%--1111111111--%>
                            
                             
                             <table cellpadding="3" cellspacing="0" style="width: 100%; border-collapse: collapse;" border="0">
                            <tr>
                                <td style="text-align: left; font-weight: bold;width:180px;">
                                    Chief Engineer
                                </td>
                                <td class="style3" style="text-align: left; font-weight: bold;">:</td>
                                <td style="text-align: left; font-weight: bold;">
                                    <asp:TextBox ID="txtCE" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <strong>Chief Officer</strong>
                                </td>
                                <td class="style3" style="text-align: left;">:</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtCO" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <strong>Supdt. Name</strong>
                                </td>
                                <td class="style3" style="text-align: left;">:</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtSupdt" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                            </tr>



                              </table>
                            
                    </td>
                    <td style="text-align: left; ">
                    
                   <table width="100%" >
                    <tr>
                    <td style="text-align: left">
                    <div class="myheading1">
                               Repairs Carried Out :
                            </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtRepairsCarriedout" runat="server" Height="100px" MaxLength="1000"
                            TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            
             <table cellpadding="3" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                             <tr>
                                <td style="text-align: left; width:130px;">
                                    
                                </td>
                                <td style="text-align: left;width:10px;">
                                    
                                </td>
                                <td style="text-align: left;">
                                    
                                </td>
                                <td style="width:100px; text-align:right"><b>Closure Date :</b></td>
                             <td style="text-align:left"> <asp:Label ID="lblCompDt" runat="server"></asp:Label> </td>
                            </tr>
                            
                        </table>

               <%--2222222222--%>
                        <div id="divTab2" runat="server" visible="true" style="border:solid 0px red;">
            <div class="myheading1" >Spare </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse">
                        <tr>
                            <td style="text-align: left;">
                                <table style="width: 100%; border-collapse: collapse" border="1">
                                    <tr>
                                        <td style="width:120px" >&nbsp;<b>Spares Required ?</b>&nbsp;
                                            </td>
                                         <td >
                                            <asp:DropDownList ID="ddlSparesReqd" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlSparesReqd_SelectedIndexChanged" Width="100px">
                                                <asp:ListItem Selected="True" Text="&lt; SELECT &gt;" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       
                                        <td style="text-align: left; width:150px;">
                                            <strong>Available On Board :</strong></td>
                                        <td >
                                            <asp:DropDownList ID="ddlSOB" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlSOB_SelectedIndexChanged" Width="100px">
                                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                       
                                        <td style="text-align: left; font-weight: bold;">Reqn No. :&nbsp;</td>
                                        <td style="text-align: left; "><asp:TextBox ID="txtRqnNo" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                        <td style="text-align: left; width: 80px; font-weight: bold">Reqn Date :&nbsp;</td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtRqnDt" runat="server" MaxLength="15" Width="80px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                Format="dd-MMM-yyyy" PopupButtonID="txtRqnDt" PopupPosition="TopLeft" 
                                                TargetControlID="txtRqnDt">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td >
                            <div class="myheading1">Spare Consumption</div>
                                <table id="tblSpares" runat="server" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" visible="false" width="100%">
                                    <tr>
                                        <td style="background-color:#e5e5e5;" >
                                            <asp:UpdatePanel ID="upSpares" runat="server">
                                                <ContentTemplate>
                                                    <table id="taddspates" runat="server" cellpadding="2" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="text-align: right; vertical-align: middle; width:100px;">Select Spare :&nbsp;</td>
                                                            <td style="text-align: left; vertical-align: middle;">
                                                                <asp:DropDownList ID="ddlSparesList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSparesList_SelectedIndexChanged" Width="95%"></asp:DropDownList>
                                                            </td>
                                                            <td style="text-align:left; width:40px">
                                                            <asp:ImageButton ID="imgAddSpare" runat="server" ImageUrl="~/Images/add.png" OnClick="imgAddSpare_Click" />
                                                            </td>
                                                            <td style="text-align: right; vertical-align: middle;width:110px;">
                                                                Qty(Consumed) :&nbsp;
                                                            </td>
                                                            <td style="text-align: left; vertical-align: middle;width:70px;">
                                                                <asp:TextBox ID="txtQtyCon" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)"
                                                                    Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; vertical-align: middle; width:70px;">
                                                                Qty(ROB) :&nbsp;
                                                            </td>
                                                            <td style="text-align: left; vertical-align: middle;width:70px;">
                                                                <asp:TextBox ID="txtQtyRob" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)"
                                                                    Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td style="vertical-align: middle;width:120px;">
                                                                <asp:Button ID="btnAddSpare" runat="server" CssClass="btnorange" OnClick="btnAddSpare_Click" style="width: 110px; float: right;" Text=" + Add Spare" />
                                                                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Style="display: none" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                    <col style="width: 50px;" />
                                                    <col />
                                                    <col style="width: 200px;" />
                                                    <col style="width: 100px" />
                                                    <col style="width: 90px;" />
                                                    <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td>Delete</td>
                                                        <td style="text-align:left">
                                                           Spare Name
                                                        </td>
                                                        <td>
                                                            Part#
                                                        </td>
                                                        <td>
                                                            Qty(Cons.)
                                                        </td>
                                                        <td>
                                                            Qty(ROB)
                                                        </td>
                                                        
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <div id="dvSpares" class="scrollbox" onscroll="SetScrollPos(this)" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 150px; text-align: center;">
                                            
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                    <colgroup>
                                                        <col style="width: 50px;" />
                                                        <col />
                                                        <col style="width: 200px;" />
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptComponentSpares" runat="server">
                                                        <ItemTemplate>
                                                            <tr visible='<%#Eval("SpareId").ToString() != "" %>'>
                                                                 <td align="center">
                                                                        <%if (Request.QueryString["DN"]!= null && Session["UserType"].ToString() == "S" && (ModifySpare=="Y" || txtCompletionDt.Text.Trim()=="" ))  %>
                                                                        <%{ %>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%#Eval("RowId") %>' Height="12px" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDeleteSpare_OnClick" OnClientClick="javascript:return confirm('Are you sure to delete?');" />
                                                                        <%} %>
                                                                        <% if (Request.QueryString["DN"] == null)
                                                                           { %>
                                                                            <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%#Eval("RowId") %>' Height="12px" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="javascript:return confirm('Are you sure to delete?');" />
                                                                        <%} %>
                                                                </td>
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
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
              <div id="divTab3" runat="server" visible="true">
            <div class="myheading1">
                Attachments</div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table cellpadding="2" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: right; vertical-align: middle; width:100px;">
                                Description :&nbsp;
                            </td>
                            <td style="text-align: left; vertical-align: middle;">
                                <asp:TextBox ID="txtAttachmentText" runat="server" MaxLength="200" Width="95%"></asp:TextBox>
                            </td>
                            <td style="text-align: right; vertical-align: middle; width:100px;">
                                Attachment :&nbsp;
                            </td>
                            <td style="text-align: left; vertical-align: middle; width:110px;">
                                <asp:FileUpload ID="flAttachDocs" runat="server" Width="100px" />
                            </td>
                            <td style="vertical-align: middle; text-align:right;width:110px;">
                                <asp:Button ID="btnSaveAttachment" runat="server" OnClick="btnSaveAttachment_Click" Text="Upload Attachment" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveAttachment" />
                </Triggers>
            </asp:UpdatePanel>
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                <colgroup>
                    <col />
                    <col style="width: 30px;" />
                    <%if (Session["UserType"].ToString().Trim() == "S")
                      {%>
                    <col style="width: 30px;" />
                    <% }%>
                    <col style="width: 17px;" />
                </colgroup>
                    <tr align="left" class="gridheader">
                        <td style="text-align:left">Attachments</td>
                        <td></td>
                        <%if (Session["UserType"].ToString().Trim() == "S")
                          {%>
                        <td>
                        </td>
                        <% }%>
                        <td>
                        </td>
                    </tr>
            </table>
            <div id="trAttachments" visible="false" runat="server">
                <div id="divAttachment" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll;overflow-x: hidden;height: 100px; text-align: center;">
                    <table border="1" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;">
                        <colgroup>
                            <col />
                            <col style="width: 30px;" />
                            <%if (Session["UserType"].ToString().Trim() == "S")
                              {%>
                            <col style="width: 30px;" />
                            <% }%>
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
                                    <%if (Session["UserType"].ToString().Trim() == "S")
                                      {%>
                                    <td>
                                        <asp:ImageButton runat="server" ID="btnDelAttachment" ImageUrl="~/Images/delete.png" Height="12px" OnClick="DeleteAttachment_OnClick" title='Delete Attachment' CommandArgument='<%#Eval("AttachmentId")%>'                                            OnClientClick="javascript:confirm('Are you sure to remove this attachment?');" />
                                    </td>
                                    <% }%>
                                  <td style="width:17px">
                                </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="myheading1">Office Comments :</div>
            <div>
             <table border="1" cellpadding="4" style="width: 100%; border-collapse: collapse;font-size:12px;">
             <asp:Repeater ID="rptOfficeComments" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%>.</td>
                        <td style="text-align:left;"><%#Eval("Remarks")%></td>
                        <td style="width:175px;"><%#Eval("EnteredByON")%></td>    
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;</table>
            </div>
        </div>
                    </td>
                </tr>
            </table>
            
                    
            
            <table width="100%" id="trClosure" runat="server">
            <tr>
                
                </tr>
            </table>
            
            
            
        </div>
        
        
    </div>
        <div style="background-color: #F95A15; height: 4px">&nbsp;</div>
    <div style="padding:5px; text-align:center;">
                <asp:Button ID="btnAddRemarks" runat="server" CssClass="btn" OnClick="btnAddRemarks_Click" Text="Add Remarks" Width="100px" />
                
                <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save Defect" Width="100px" />
                <asp:Button ID="btnOpenPopupCloseDefect" runat="server" CssClass="btn" OnClick="btnOpenPopupCloseDefect_Click" Text="Close Defect" Width="100px" />

                <asp:Button ID="btnExportAttachments" runat="server" CssClass="btn" OnClick="btnSendAttachment_OnClick" Text="Export Attachments" Width="139px" Visible="false"/>

                <asp:Button ID="btnPrint" runat="server" CssClass="btn" OnClick="btnPrint_Click" Text="Print" Visible="false" Width="100px" />
            </div>


            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100; " runat="server" id="dvCloseDefect" visible="false" >
            <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                <div style="position :relative; width:1100px;padding :20px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                    <table cellpadding="0" cellspacing="0" border="1">
                        <tr>
                            <td>
                                <table width="100%" id="trClosurePopup" runat="server" border="0">
                        <col width="100px" />
                        <col />
                    <tr>
                        <td style="text-align:left;"> <b>Component Working : </b></td>
                        <td style="text-align:left;">
                             <asp:DropDownList ID="ddlCurrentComponentStatus" runat="server">    
                            <asp:ListItem Value="" Text=" Select " ></asp:ListItem>
                            <asp:ListItem Value="1" Text="Yes" ></asp:ListItem>
                            <asp:ListItem Value="2" Text="No" ></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;"><b>Closure Date : </b></td>
                        <td style="text-align:left;">
                              <asp:TextBox ID="txtCompletionDt" runat="server" MaxLength="15" Style="text-align: center" Width="90px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                    PopupButtonID="txtCompletionDt" PopupPosition="TopLeft" TargetControlID="txtCompletionDt">
                                </asp:CalendarExtender>
                        </td>
                    </tr>
                        <tr>
                            <td colspan="2"  style="text-align:left;">
                                <strong> Repairs Carried Out : </strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"  style="text-align:left;">
                                <asp:TextBox ID="txtRepairsCarriedout_CloseDefect" runat="server" Height="100px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                    <tr>
                         <td style="text-align:center" colspan="2">
                            &nbsp;
                         </td>
                    </tr>
                    <tr>
                         <td style="text-align:center" colspan="2">

                        <asp:Button ID="btnClosureSave" runat="server" CssClass="btn" OnClick="btnClosureSave_Click" Text="Close Defect" Width="100px" />
                        <asp:Button ID="btnCloseDefect" Text="Close" CssClass="btn" OnClick="btnCloseDefect_Click" runat="server" />
                    </td>
                    </tr>
            </table>
                            </td>
                            <td id="tdSpareConsumptionPopup" runat="server" visible="false">
                                  <div class="myheading1">Spare Consumption</div>
                                <table id="tblSpares_Pop" runat="server" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
                                    <tr>
                                        <td style="background-color:#e5e5e5;" >
                                            <%--<asp:UpdatePanel ID="upSpares_Pop" runat="server">
                                                <ContentTemplate>--%>
                                                    <table id="taddspates_Pop" runat="server" cellpadding="2" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="text-align: right; vertical-align: middle; width:100px;">Select Spare :&nbsp;</td>
                                                            <td style="text-align: left; vertical-align: middle;">
                                                                <asp:DropDownList ID="ddlSparesList_Pop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSparesList_Pop_SelectedIndexChanged" Width="95%"></asp:DropDownList>
                                                            </td>
                                                            <td style="text-align:left; width:40px">
                                                            <asp:ImageButton ID="imgAddSpare_Pop" runat="server" ImageUrl="~/Modules/HRD/Images/add.png" OnClick="imgAddSpare_Pop_Click" />
                                                            </td>
                                                            <td style="text-align: right; vertical-align: middle;width:110px;">
                                                                Qty(Consumed) :&nbsp;
                                                            </td>
                                                            <td style="text-align: left; vertical-align: middle;width:70px;">
                                                                <asp:TextBox ID="txtQtyCon_Pop" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)"
                                                                    Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; vertical-align: middle; width:70px;">
                                                                Qty(ROB) :&nbsp;
                                                            </td>
                                                            <td style="text-align: left; vertical-align: middle;width:70px;">
                                                                <asp:TextBox ID="txtQtyRob_Pop" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)"
                                                                    Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td style="vertical-align: middle;width:120px;">
                                                                <asp:Button ID="btnAddSpare_Pop" runat="server" CssClass="btn" OnClick="btnAddSpare_Pop_Click" style="width: 110px; float: right;" Text=" + Add Spare" />
                                                                <asp:Button ID="btnRefresh_Pop" runat="server" OnClick="btnRefresh_Pop_Click" Style="display: none" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                    <col />
                                                    <col style="width: 200px;" />
                                                    <col style="width: 100px" />
                                                    <col style="width: 90px;" />
                                                    <col style="width: 30px;" />
                                                    <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td style="text-align:left">
                                                           Spare Name
                                                        </td>
                                                        <td>
                                                            Part#
                                                        </td>
                                                        <td>
                                                            Qty(Cons.)
                                                        </td>
                                                        <td>
                                                            Qty(ROB)
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <div id="dvSpares_Pop" class="scrollbox" onscroll="SetScrollPos(this)" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 150px; text-align: center;">
                                            
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                    <colgroup>
                                                        <col />
                                                        <col style="width: 200px;" />
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptComponentSpares_Pop" runat="server">
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
                                                                    <td>
                                                                        <% if (Request.QueryString["DN"] == null)
                                                                           { %>
                                                                        <asp:ImageButton ID="imgDel" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                                                            Height="12px" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" OnClientClick="javascript:return confirm('Are you sure to delete?');" />
                                                                        <%} %>
                                                                    </td>
                                                                    <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    
                </div>
            </center>
            </div>
    </form>
</body>
</html>










