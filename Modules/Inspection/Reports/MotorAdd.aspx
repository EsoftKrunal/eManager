<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MotorAdd.aspx.cs" Inherits="MotorAdd" Title="Monthly Owner’s Technical & Operating Report (MOTOR) " %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>InspectionObservation</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css" >
    .btn1
    {
    	background-color :#d2d2d2;
    	border:solid 1px black;
    	height:30px;  
    }
    .btnSel
    {
    	background-color :Gray;
    	border :solid 1px black;
    	height:30px;  
    }
    </style> 
    <script language="javascript">
    var nfiles=1;
    function Expand()
    { 
        nfiles++ ;
        document.getElementById("txtFileNumber").value = nfiles;
        if (nfiles>8){return false;}
        var adh = '+<br/><div><label>Image File '+nfiles+' :</label><label>&nbsp;<input type="file" name="File'+nfiles+'" id="File'+nfiles+'" class="input_box" onkeydown="return false;"></label></div>';
        adh =   adh + '<div  class="tabular" style="display:none;"><label class="name1">Description '+nfiles+' :</label><label class="name2"><Input Name="Desc-File'+nfiles+'" Size=20 value="" class="inputText" id="Desc-File'+nfiles+'"></label></div>';
        alert(adh);
        document.getElementById('files' + nfiles).innerHTML=adh;
        document.getElementById('files' + nfiles).style.display="block";
        return false;
    }
    function PrintSafetyInspRPT()
    {
        var MID=document.getElementById('HiddenField_InspId').value;
        if(!(parseInt(MID)==0 || MID==""))
        {
            window.open('..\\Reports\\SafetyInsp_Report.aspx?MID='+ MID);
        }
        return false;
    }
    function PrintSafetyInspRPTWithoutImage()
    {
        var MID=document.getElementById('HiddenField_InspId').value;
        if(!(parseInt(MID)==0 || MID==""))
        {
            window.open('..\\Reports\\SafetyInsp_Report_WithoutImage.aspx?MID='+ MID);
        }
        return false;
    }
    function GetFileName(val)
    {
        var i = val.lastIndexOf("\\");
        var k = val.lastIndexOf(".");
        return val.substring(i+1,k);
    }
    function openMoreImages(imgPath)
    {
        var ImgUrl ="../EMANAGERBLOB/Inspection/Transaction_Reports/"+imgPath;
        var InspDueId=document.getElementById('HiddenField_InspId').value;
        var SrNum=document.getElementById('txt_SrNo').value;
        
        window.open('..\\Transactions\\SingleImageViewer.aspx?InspId='+ InspDueId + '&SrNumb='+ SrNum + '&ImgUrl='+ ImgUrl,null,'title=no,toolbars=no,scrollbars=yes,width=700,height=500,left=20,top=20,addressbar=no',false);
    }
    function PublishSafetyInspRPT()
    {
        var InspDueId=document.getElementById('HiddenField_InspId').value;
        var MTMPublish=1;
        var MTMvslId=document.getElementById('HiddenField_VslId').value;
        var MTMInspName=document.getElementById('HiddenField_InspName').value;
        var MTMDoneDt=document.getElementById('HiddenField_DoneDt').value;
        var MTMPortDone=document.getElementById('HiddenField_PortDone').value;
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Reports\\SafetyInsp_Report.aspx?InspId='+ InspDueId+'&MTMPub='+MTMPublish+'&MTMVesselId='+MTMvslId+'&MTMInsp='+MTMInspName+'&MTMDnDt='+MTMDoneDt+'&MTMPrDone='+MTMPortDone,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    function RefreshParent()
    {
       window.opener.Refresh();
    }
    
    </script>
    </head> 
<body style="text-align: center">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
  <ContentTemplate>
<asp:Panel id="pnl_InspResponse" runat="server">
    <TABLE style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 10px; BORDER-TOP: #8fafdb 0px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 5px; VERTICAL-ALIGN: top; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 5px; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right" cellSpacing=0 cellPadding=0 width="100%" border=0>
         <TR>
            <TD style="PADDING-RIGHT: 10px; height:" colspan="8" style="background-color:#4371a5; height:23px; text-align :center " class="text"><span lang="en-us" >
            Monthly Owner’s Technical & Operating Report (MOTOR)</span>
            </TD>
        </TR>
        <TR>
            <TD style="padding:5px; background-color :#d2d2d2; font-weight:bold; font-size:14px;">
                    <TABLE cellSpacing="0" cellPadding="2" width="900px">
                        <TR>
                            <td style="text-align:right">&nbsp;&nbsp;&nbsp;<span style="Font-Size:14px">Vessel:</span></td>
                            <td style="text-align :left">
                                <asp:DropDownList style="display :none" ID="ddl_Vessel" runat="server" CssClass="input_box" Width="175px" Enabled="true"></asp:DropDownList>   
                                <asp:Label id="lblVesselName" runat="server" Width="100%" Font-Size="14px" ></asp:Label>
                            </td>
                            <TD style="TEXT-ALIGN: right" ><span style="Font-Size:14px">Report#:</span></TD>
                            <TD style="TEXT-ALIGN: left" >
                                <asp:Label id="txtReportNo" runat="server" Width="100%"  Font-Size="14px"></asp:Label>
                            </TD>
                            <TD style="TEXT-ALIGN: right" ><span style="Font-Size:14px">Prepared On:</span></TD>
                            <TD style="TEXT-ALIGN: left">
                                <asp:Label id="txtReportDate" runat="server" Width="100%" Font-Size="14px"></asp:Label>
                            </TD>
                        </TR>
                        <tr>
                            <td colspan="6" style="text-align:right;">
                                <asp:Label ID="lblMasterMsg" runat="server" style="text-align:right;color:Red;" ></asp:Label>
                            </td>
                        </tr>
                    </TABLE>
            </TD>
        </TR>
        <tr>
        <td style=" text-align :center; padding-bottom:0px;" >
                    <div style="float:left;">
                        <asp:Button id="btnTechnical" Font-Bold="true" onclick="btnTechnical_Click" runat="server" CssClass="btn1" Text="Technical"></asp:Button>
                        <asp:Button id="btnOperations" Font-Bold="true" onclick="btnOperations_Click" runat="server" CssClass="btn1" Text="Operations"></asp:Button>
                        <asp:Button id="btnCrewing" Font-Bold="true" onclick="btnCrewing_Click" runat="server" CssClass="btn1" Text="Crewing"></asp:Button>
                        <asp:Button id="btnVetting" Font-Bold="true" onclick="btnVetting_Click" runat="server" CssClass="btn1" Text="Vetting"></asp:Button>
                        <asp:Button id="btnOthers" Font-Bold="true" onclick="btnOthers_Click" runat="server" CssClass="btn1" Text="HSSQE"></asp:Button>
                    </div>
        </td>
        </tr>
        <TR>
            <TD style="padding-top:0px;">
                <FIELDSET style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; TEXT-ALIGN: center">
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                <td style ="text-align:left ">
                <TABLE cellSpacing="0" cellPadding="2" border="0" width="100%"  >
                        <TR>
                            <TD width="80px"></TD>
                            <TD style="TEXT-ALIGN: left"></TD>
                        </TR>
                        <TR>
                            <TD style="PADDING-LEFT: 10px; TEXT-ALIGN: left">Sr#</TD>
                            <TD style="TEXT-ALIGN: left">Title &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                            </TD>
                        </TR>
                        <TR>
                            <TD style="PADDING-LEFT: 10px; TEXT-ALIGN: left">
                                <asp:TextBox  id="txt_SrNo" runat="server" Width="64px" CssClass="input_box" MaxLength="49" ReadOnly="true" ></asp:TextBox>
                            </TD>
                            <TD style="TEXT-ALIGN: left">
                                <asp:TextBox id="txt_ContentHeading" runat="server" Width="100%" CssClass="input_box" MaxLength="254"  ReadOnly="true"></asp:TextBox>
                            </TD>
                        </TR>
                        <TR>
                            <TD vAlign=top style=" text-align :right" >Description :</TD>
      
                            <TD style="TEXT-ALIGN: left">
                                <asp:TextBox  id="txt_Content" runat="server" Width="100%" 
                        CssClass="input_box" TextMode="MultiLine" Height="250px"></asp:TextBox>
                            </TD>
                        </TR>
                        </TABLE>
                <td width="180px" style="vertical-align:bottom; padding-bottom:2px;" >
                <asp:UpdatePanel runat="server" ID="up2">
                        <ContentTemplate>
                        <asp:Button runat="server" ID="btnPost" onclick="btnPost_Click" style="display:none " /> 
                        <div style=" height :275px; width:170px; overflow-y :scroll;overflow-x :hidden" class="input_box"  >
                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" CellSpacing="5"  >
                        <ItemTemplate>
                        <div style=" text-align :center; border:dotted 1px blue;" >
                        <div style=" position :relative; vertical-align:top;">
                        <asp:ImageButton runat="server" ID="btnDel" ToolTip="Delete this Image.." OnClientClick="return confirm('Are you sure to remove this Image.');" filename='<%#Eval("FilePath")%>' style="position:absolute;left:112px; cursor:pointer;" ImageUrl ="~/Images/DeleteSquare.png" CommandArgument='<%#Eval("MPDId")%>' OnClick="btnDel_Click" />
                        <a target="_blank" href='<%# "..\\Transactions\\ImageUpdateMortor.aspx?MPDID=" + Eval("MPDId")%>'>
                            <img style=" border:none" src='<%# "..\\EMANAGERBLOB/Inspection\\Mortor\\" + Eval("FilePath")%>' title='<%#Eval("PicCaption") %>' width="130"  />
                        </a>
                        </div>
                        </div>
                        </ItemTemplate>
                        </asp:DataList>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
                </table> 
                </FIELDSET>
            </TD>
        </TR>
        <TR>
            <TD style="TEXT-ALIGN: left">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td valign="top" style="padding-right: 5px" width="60%" runat="server" visible="false" >
                            <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: left">
                                <legend>View Pictures</legend>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            No. of pictures :
                                                                                              <asp:Label ID="lbl_NoofPics" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ScrollBars="vertical" Height="140px" runat="server" ID="Panel1">
                                            <asp:Repeater id="Repeater1" runat="Server">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <%--<TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right"></TD>--%>
                                                        <%--<TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right"><asp:Label ID="lbl_PicNum" runat="server" Text='<%# Eval("PicNumber") %>'></asp:Label></TD>--%>
                                                        <%--<TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: left"><asp:FileUpload runat="server" ID="flpupld" CssClass="input_box" Width="186px" Enabled="false" /></TD> --%>
                                                        <TD style="TEXT-ALIGN: right"><asp:Label ID="lbl_CapNum" runat="server" Text='<%# Eval("CapNumber") %>'></asp:Label>&nbsp;<asp:TextBox id="txt_Caption1" runat="server" CssClass="input_box" Width="480px" Text='<%# Eval("PicCaption") %>' ReadOnly="true"></asp:TextBox></TD> 
                                                        <td style="text-align: left"><img src="../Images/paperclip.gif" onclick='<%# "return openMoreImages(\"" + Eval("FilePath") + "\");" %>' style="cursor: hand" title="View Image." /></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate> 
                                            </asp:Repeater>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                   
                                </table>
                            </FIELDSET>
                        </td>
                        <td valign="top" width="40%">
                            <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: left">
                                <legend>Upload Pictures</legend>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Please enter number of pictures :
                                            <asp:TextBox id="txt_Nos" runat="Server" Width="42px" CssClass="input_box"></asp:TextBox>&nbsp;<asp:Button id="btnFiles" onclick="btnFiles_Click" runat="Server" CssClass="btn" Text="Enter" Width="73px"></asp:Button></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ScrollBars="vertical" Height="140px" runat="server" ID="pnl" Width="100%">
                                            <asp:Repeater id="rpt_Images" runat="Server" OnItemDataBound="rpt_OnDataBound">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right"></TD>
                                                        <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Picture <%# ((int)Container.DataItem)%> :</TD>         
                                                        <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: left"><asp:FileUpload runat="server" ID="flpupld" CssClass="input_box" Width="56px" /></TD> 
                                                        <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right; padding-left: 5px">Caption <%# ((int)Container.DataItem)%> :&nbsp;&nbsp;<asp:TextBox id="txt_Caption2" runat="server" CssClass="input_box" Width="300px"></asp:TextBox></TD> 
                                                    </tr>
                                                </table>
                                            </ItemTemplate> 
                                            </asp:Repeater> 
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </FIELDSET>
                        </td>
                    </tr>
                   
                </table>
            </TD>
        </TR>
        <tr>
            <td style="PADDING-TOP: 5px">
                <table style="WIDTH: 100%" cellSpacing=0 cellPadding=0>
                    <tr>
                        <td colspan="1" style="padding-left: 10px; text-align: left" valign="bottom">
                            <asp:Label id="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
                        <td style="PADDING-RIGHT: 8px; HEIGHT: 10px; TEXT-ALIGN: right" colSpan=9>
                            <asp:HiddenField ID="HiddenField_ImgUrl" runat="server" />
                            <asp:HiddenField id="HiddenField_TotalSrNo" runat="server"></asp:HiddenField> 
                            <asp:HiddenField id="HiddenField_InspId" runat="server"></asp:HiddenField> 
                            <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_SrNo" FilterType="Numbers,Custom" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender> 
                            <asp:HiddenField ID="HiddenField_VslId" runat="server" />
                            <asp:HiddenField ID="HiddenField_InspName" runat="server" />
                            <asp:HiddenField ID="HiddenField_DoneDt" runat="server" />
                            <asp:HiddenField ID="HiddenField_PortDone" runat="server" />
                            <asp:HiddenField ID="hfd_HDID" runat="server" />
                            <asp:Button id="btn_Save" onclick="btn_Save_Click" runat="server" Width="59px" CssClass="btn" Text="Save" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data'"></asp:Button>&nbsp;
                            <asp:Button ID="btn_Print" runat="server" CssClass="btn" onclick="PrintNow" Text="Print"  Width="59px" />
                            <asp:Button ID="Button1" runat="server" CssClass="btn" onclick="PublishNow" Text="Publish"  Width="59px" style="display:none;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </TABLE>
</asp:Panel>
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_Save" />
    </Triggers>
</asp:UpdatePanel>
</form>
</body>
</html>

