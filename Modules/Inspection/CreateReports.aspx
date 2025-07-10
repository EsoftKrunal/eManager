<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateReports.aspx.cs" Inherits="Transactions_CreateReports" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
   
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
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
            var InspDueId=document.getElementById('hfd_INSPID').value;
            if(!(parseInt(InspDueId)==0 || InspDueId==""))
            {
                window.open('..\\Inspection\\Reports\\SafetyInsp_Report.aspx?InspId='+ InspDueId);
            }
            return false;
        }
        function PrintSafetyInspRPTWithoutImage()
        {
            var InspDueId = document.getElementById('hfd_INSPID').value;
            if(!(parseInt(InspDueId)==0 || InspDueId==""))
            {
                window.open('..\\Inspection\\Reports\\SafetyInsp_Report_WithoutImage.aspx?InspId='+ InspDueId);
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
            var ImgUrl="../Inspection/UserUploadedDocuments/Transaction_Reports/"+imgPath;
            var InspDueId=document.getElementById('HiddenField_InspId').value;
            var SrNum=document.getElementById('txt_SrNo').value;
            
            window.open('..\\Inspection\\SingleImageViewer.aspx?InspId='+ InspDueId + '&SrNumb='+ SrNum + '&ImgUrl='+ ImgUrl,null,'title=no,toolbars=no,scrollbars=yes,width=700,height=500,left=20,top=20,addressbar=no',false);
        }
        function PublishSafetyInspRPT() {

            var InspDueId = document.getElementById('hfd_INSPID').value;
            var MTMPublish=1;
            var MTMvslId=document.getElementById('HiddenField_VslId').value;
            var MTMInspName=document.getElementById('HiddenField_InspName').value;
            var MTMDoneDt=document.getElementById('HiddenField_DoneDt').value;
            var MTMPortDone = document.getElementById('HiddenField_PortDone').value;
            if (!(parseInt(InspDueId) == 0 || InspDueId == "")) {
                window.open('..\\Inspection\\Reports\\SafetyInsp_Report.aspx?InspId='+ InspDueId+'&MTMPub='+MTMPublish+'&MTMVesselId='+MTMvslId+'&MTMInsp='+MTMInspName+'&MTMDnDt='+MTMDoneDt+'&MTMPrDone='+MTMPortDone,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
            }
        }
    </script>
    <style type="text/css">
         .dv
        {
             padding-top:2px;
             padding-bottom:2px;
             cursor:pointer;
        }
        .dv_sel
        {
            padding-top:2px;
            padding-bottom:2px;
            background-color:yellow;
            cursor:pointer;
        }
        .newbtn{
            background-color:#4371a5;
            color:white;
            padding:5px;
            border:none;
        }
    </style>
    </head> 
<body style="text-align: center;margin:0px;">
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="hfd_INSPID" />
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
  <ContentTemplate>
<asp:Panel id="pnl_InspResponse" runat="server">
         <div style=" padding:6px; text-align :center; " class="text headerband">
                    Technical Inspection Report
         </div>
         <table cellSpacing="0" cellPadding="3" width="100%" style=" background-color:#FFF0E0" >
                        <tr>
                            <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right; font-size:13px;" width="10%">Insp#:</td>
                            <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                                <asp:Label id="txtinspno" runat="server" Width="158px" ReadOnly="True" Font-Size="Larger" ></asp:Label>
                            </td>
                            <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right; font-size:13px;" width="10%">Done Date:</td>
                            <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                                <asp:Label id="txtlastdone" runat="server" Width="158px" ReadOnly="True"  Font-Size="Larger"></asp:Label>
                            </td>
                            <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right; font-size:13px;" width="10%">Port Done:</td>
                             <td style="TEXT-ALIGN: left">
                                <asp:Label id="txtplannedport" runat="server" Width="158px" ReadOnly="True" Font-Size="Larger"></asp:Label>
                            </td>
                             <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right; font-size:13px;" width="10%">Suptd. Name:</td>
                             <td style="TEXT-ALIGN: left">
                                <asp:Label id="txtSupName" runat="server" Width="158px" ReadOnly="True"  Font-Size="Larger"></asp:Label>
                            </td>
                        </tr>
                    </TABLE>
         <div>
             <table width="100%" border="0" cellpadding="0" cellspacing="0">
                 <tr style="background-color:#2cbdc2">
                     <td style="width:400px;height:30px;">
                        &nbsp;Report Heading
                     </td>
                     <td style=";height:30px;">
                         &nbsp;Description
                     </td>
                     <td style="width:170px;height:30px;">
                         &nbsp;Pictures
                     </td>
                 </tr>
             </table>
             <table width="100%" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                     <td style="width:400px;vertical-align:top">
                          <table cellpadding="2" cellspacing="0" width="100%" style="border-collapse:collapse" border="1">
                            <asp:Repeater runat="server" ID="rptQuestions">
                            <ItemTemplate>
                            <tr>
                            <tr class='<%#(Eval("Srno").ToString().Trim()==SNO)?"dv_sel":"dv"%>'>
                            <td style="width:25px; text-align:center;">
                                <asp:LinkButton runat="server" ID="lnkRow" Text='<%#Eval("Srno")%>' CommandArgument='<%#Eval("Srno")%>' OnClick='Select_Row' >  </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="LinkButton1" Text='<%#Eval("ContentHeading")%>' CommandArgument='<%#Eval("Srno")%>' OnClick='Select_Row'></asp:LinkButton><span style='color:Red'><%#(Eval("ContentText").ToString().Trim()=="")?"*":""%></span>
                            </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                        </table>
                     </td>
                     <td style="vertical-align:top">
                          <asp:TextBox runat="server" ID="txtDescr" TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1" style="height:515px; width:100%;"></asp:TextBox>
                     </td>
                     <td style="width:170px;vertical-align:top">
                        <asp:UpdatePanel runat="server" ID="up2">
                        <ContentTemplate>
                        <asp:Button runat="server" ID="btnPost" onclick="btnPost_Click" style="display:none " /> 
                        <div style=" height :513px; width:170px; overflow-y :scroll;overflow-x :hidden" class="input_box"  >
                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" CellSpacing="5"  >
                        <ItemTemplate>
                        <div style=" text-align :center; border:dotted 1px blue;" >
                        <div style=" position :relative; vertical-align:top;">
                        <asp:ImageButton runat="server" ID="btnDel" ToolTip="Delete this Image.." OnClientClick="return confirm('Are you sure to remove this Image.');" filename='<%#Eval("FilePath")%>' style="position:absolute;left:112px; cursor:pointer;" ImageUrl ="~/Modules/HRD/Images/DeleteSquare.png" CommandArgument='<%#Eval("TableId")%>' OnClick="btnDel_Click" />
                        <a target="_blank" href='<%# "..\\Inspection\\ImageUpdate.aspx?TableId=" + Eval("TableId")%>'>
                            <img style=" border:none" src='<%# "..\\Inspection\\UserUploadedDocuments\\Transaction_Reports\\" + Eval("FilePath")%>' title='<%#Eval("PicCaption") %>' width="130"  />
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
             <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                    <td style="width:400px;vertical-align:top"> </td>
                    <td style="vertical-align:top; text-align:center;padding-top:5px;padding-bottom:5px;">
                        <asp:Button ID="btn_Save" runat="server" CssClass="btn" onclick="btn_Save_Click" Text="Save" Width="59px" Enabled="false"/>
                    </td>
                    <td style="width:170px;vertical-align:top;text-align:center;padding-top:5px;padding-bottom:5px;">
                        <asp:Button ID="btnUploadMore" runat="server" CssClass="btn" Text="Upload Picture" OnClick="btnUploadMore_Click" Enabled="false" />
                    </td>
                    </tr>
               </table>
        </div>
     <table width="100%" border="0" cellpadding="5" cellspacing="0">
                 <tr style="background-color:#2cbdc2">
                     <td style="text-align:left">
                           <asp:Label ID="lblNotify" runat="server"></asp:Label>
                            <asp:Label id="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                     </td>
                     <td style="text-align:right; width:400px;">
                          <asp:HiddenField ID="HiddenField_ImgUrl" runat="server" />
                            <asp:HiddenField id="HiddenField_TotalSrNo" runat="server"></asp:HiddenField> 
                            <asp:HiddenField id="HiddenField_InspId" runat="server"></asp:HiddenField> 
                            <asp:HiddenField ID="HiddenField_VslId" runat="server" />
                            <asp:HiddenField ID="HiddenField_InspName" runat="server" />
                            <asp:HiddenField ID="HiddenField_DoneDt" runat="server" />
                            <asp:HiddenField ID="HiddenField_PortDone" runat="server" />
                            <asp:HiddenField ID="HiddenField_StartDate" runat="server" />
                            
                            <asp:Button id="btn_Print" runat="server" Width="102px" CssClass="btn" Text="Print with Image" OnClientClick="return PrintSafetyInspRPT();" style="display:none;">
                            </asp:Button>&nbsp;<asp:Button ID="btn_PrintwoImage" runat="server" CssClass="btn" OnClientClick="return PrintSafetyInspRPTWithoutImage();" Text="Print without Image" Width="125px" style="display:none;"/>

                            <asp:Button ID="btn_Publish" runat="server" CssClass="btn" OnClick="btn_Publish_OnClick"  Text="Publish" Visible="false" />
                            <asp:Button ID="btnNotify" runat="server" CssClass="btn" OnClick="btnNotify_Click"  Text="Notify" Visible="false"/>
                            <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print" OnClick="btnPrint_OnClick" />
                            <asp:Button ID="Button1" runat="server" CssClass="btn" Text="Print Without Images" OnClick='btnPrintWithOutImage_OnClick' />
                     </td>
                 </tr>
             </table>

    <div style="position:absolute;top:0px;left:0px; height :500px; width:100%;" id="dvUploadFiles" runat="server" visible="false">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :670px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:900px; height:500px;text-align :center;background : white; z-index:150;top:50px; border:solid 10px gray;">
         <center >
         <iframe runat="server" id="frmUpload" width='890px' height='500px'>
         </iframe>
            <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="[X] Close Window " OnClick="btnClose_OnClick" style="margin-top:4px; padding:2px; font-size:13px; " />
         </center>
         </div>
     </center>
     </div>
</asp:Panel>
</ContentTemplate>
    <triggers>
        <asp:PostBackTrigger ControlID="btn_Save" />
    </Triggers>
</asp:UpdatePanel>
</form>
</body>
</html>

