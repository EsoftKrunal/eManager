<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DockingProgressComments.aspx.cs" Inherits="DryDock_DockingProgressComments" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>InspectionObservation</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../CSS/StyleSheet.css" />
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
                window.open('..\\Reports\\SafetyInsp_Report.aspx?InspId='+ InspDueId);
            }
            return false;
        }
        function PrintSafetyInspRPTWithoutImage()
        {
            var InspDueId = document.getElementById('hfd_INSPID').value;
            if(!(parseInt(InspDueId)==0 || InspDueId==""))
            {
                window.open('..\\Reports\\SafetyInsp_Report_WithoutImage.aspx?InspId='+ InspDueId);
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
        function PublishSafetyInspRPT() {

            var InspDueId = document.getElementById('hfd_INSPID').value;
            var MTMPublish=1;
            var MTMvslId=document.getElementById('HiddenField_VslId').value;
            var MTMInspName=document.getElementById('HiddenField_InspName').value;
            var MTMDoneDt=document.getElementById('HiddenField_DoneDt').value;
            var MTMPortDone = document.getElementById('HiddenField_PortDone').value;
            if (!(parseInt(InspDueId) == 0 || InspDueId == "")) {
                window.open('..\\Reports\\SafetyInsp_Report.aspx?InspId='+ InspDueId+'&MTMPub='+MTMPublish+'&MTMVesselId='+MTMvslId+'&MTMInsp='+MTMInspName+'&MTMDnDt='+MTMDoneDt+'&MTMPrDone='+MTMPortDone,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
            }
        }
    </script>
    </head> 
<body style="text-align: center; margin:0px 0px 0px 0px; font-family:Georgia">
    <form id="form1" runat="server">
     <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>
     <table border="0" cellSpacing="0" cellPadding="0" width="100%" >
        <tr>
            <td style="background-color:#0066FF; padding:10px;  text-align :center; font-size:14px; color:#fff;" class="text">
                Dry Dock Report - Progress Comments</td>
        </tr>
        <tr>
            <td style="TEXT-ALIGN: left;">
            <div style="border:solid 0px gray; float:right; width:100%; float:left;">
                    <asp:Repeater ID="rptJobs" runat="server">
                        <ItemTemplate>
                                    <div style="border:solid 5px #CCE0FF; margin:5px; border-radius:5px;" runat="server" visible='<%# ( Common.CastAsInt32(CountComments(Eval("DocketJobId"))) > 0 ) %>'>
                                    <div style="overflow:hidden; margin-bottom:0px;font-size:13;background-color:#CCE0FF; color:#143D66; padding:10px; padding-top:0px;"><b style='text-transform:uppercase;'><%#Eval("JOBCODE").ToString() + " </b> : " + Eval("JOBNAME").ToString()%></div>
                                     <asp:Repeater ID="rptJobs" runat="server" DataSource='<%#BindComments(Eval("DocketJobId"))%>'>
                                     <ItemTemplate>
                                     <div style=" padding:3px;font-size:11px;">
                                        <span style="font-size:11px; color:#003399; text-transform:uppercase;"><%#Common.ToDateString(Eval("FOR_DATE"))%></span> / 
                                        <span style="font-size:11px; color:#993D3D"><%#Eval("PER")%> % </span> /
                                        <span style="font-size:11px; color:#993D3D; font-style:italic; line-height:1.5"><%#Eval("REMARK")%></span>
                                     </div>
                                     </ItemTemplate>
                                     </asp:Repeater>
                                    </div>
                        </ItemTemplate>
                    </asp:Repeater>
            </div>
            </td>
        </tr>
    </table>
    <div style="padding:3px">
        <asp:Button ID="btnClose1" runat="server" CssClass="btn" Text="Close" OnClientClick="window.close();return false;" Width="100px" style=" background-color:Red; border:none; color:White; border:solid 1px red;height: 25px; font-size:11px;cursor: pointer;	font-family: Arial, Helvetica, sans-serifl;"/>
    </div>
  
</form>
</body>
</html>

