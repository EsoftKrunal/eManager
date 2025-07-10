<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VesselReports.aspx.cs" Inherits="LPSQE_VesselReports" Title="EMANAGER" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />  
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />    
<script language="javascript" type="text/javascript">
//    function OpenVesselWiseDetailWindow(vslid)
//    {
//        var VesselId=vslid;
//        var FollowUpCategories=document.getElementById("ctl00_ContentPlaceHolder1_HiddenField_FollowUpCat").value;
//        var FromDate=document.getElementById("ctl00_ContentPlaceHolder1_HiddenField_FromDate").value;
//        var ToDate=document.getElementById("ctl00_ContentPlaceHolder1_HiddenField_ToDate").value;
//        window.open('..\\FormReporting\\VesselWiseFollowUpList.aspx?FPVesselId='+VesselId+'&FPCatID='+FollowUpCategories+'&FPFrmDate='+FromDate+'&FPToDate='+ToDate,'tyu','title=no,toolbars=no,scrollbars=yes,width=1000,height=560,left=150,top=100,addressbar=no');
//    }

    function ShowProgress(ctl) 
    {
        ctl.setAttribute("value", "Loading...");
    }
    function CallPostBack(val) {
        var pid = '<%=txt_key.ClientID%>';
        var bid = '<%=btnDownloadFile.ClientID%>';
        document.getElementById(pid).setAttribute("value", val);
        document.getElementById(bid).click();
    }

    <%--function OpenVesselReport(reportId, year, period) {
        var ReportYear = '<%=hdnReportYear.ClientID%>';
        var Report = '<%=hdnReportId.ClientID%>';
        var ReportPeriod = '<%=hdnPeriod.ClientID%>';
        var bid = '<%=btn_AddNew.ClientID%>';
        //alert(ReportYear);
        //alert(Report);
        //alert(ReportPeriod);
        //document.getElementById(ReportYear).setAttribute("value", year);
        //document.getElementById(ReportId).setAttribute("value", reportId);
        //document.getElementById(ReportPeriod).setAttribute("value", period);
        document.getElementById(ReportYear).value = year;
        document.getElementById(Report).value = reportId;
        document.getElementById(ReportPeriod).value = period;
        document.getElementById(bid).click();
    }--%>
    function VerifyVesselReport(Reportyear, ReportGroup, ReportFrequency, Reportperiod) {
        var ExpRepYr = '<%=hdnExpRptYr.ClientID%>';
        var ExpRptGrpId = '<%=hdnExpRptGrpId.ClientID%>';
        var ExpRptFreq = '<%=hdnExpRptFreq.ClientID%>';   
        var ExpRptPeriod = '<%=hdnExpRptPeriod.ClientID%>';
        var bid = '<%=btnVerify.ClientID%>';
         //alert(ReportYear);
         //alert(Report);
         //alert(ReportPeriod);
         //document.getElementById(ReportYear).setAttribute("value", year);
         //document.getElementById(ReportId).setAttribute("value", reportId);
         //document.getElementById(ReportPeriod).setAttribute("value", period);
        document.getElementById(ExpRepYr).value = Reportyear;
        document.getElementById(ExpRptGrpId).value = ReportGroup;
        document.getElementById(ExpRptFreq).value = ReportFrequency;
        document.getElementById(ExpRptPeriod).value = Reportperiod;
        document.getElementById(bid).click();
     }
</script>
<style type="text/css">
.btn1
{
    padding:2px;
    background-color:#4371a5;
    color:White;
    border:solid 1px #D9E3ED;
    padding-left:15px;
    padding-right:15px;
}
a img
{
    border:none;
}
</style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<div style='display:none;'>
    <asp:TextBox id="txt_key" runat="server" style="display:none;" />
    <asp:HiddenField ID="hdnReportId" runat="server" Value="" />
    <asp:HiddenField ID="hdnPeriod" runat="server" Value="" />
    <asp:HiddenField ID="hdnReportYear" runat="server" Value="" />
    <asp:HiddenField ID="hdnExpRptYr"  runat="server" Value="" />
    <asp:HiddenField ID="hdnExpRptGrpId" runat="server" Value="" />
    <asp:HiddenField ID="hdnExpRptFreq" runat="server" Value="" />
    <asp:HiddenField ID="hdnExpRptPeriod" runat="server" Value="" />
    <asp:LinkButton runat="server" ID="btnDownloadFile" OnClick="btnDownloadFile_Click" Text="a" ></asp:LinkButton>
</div>
<div class="txt headerband">
    Vessel Reports 
</div>         
<table id="filter" width="100%" style ="text-align :center" cellpadding="2" cellspacing="1" style="border:solid 1px #4371a5;" >
    <tr  class= "headerstylegrid">
        <td style="width:100px;color:White; text-align:center">Year</td>
        <td style="width:200px;color:White; text-align:center">Vessel</td>
        <td style="width:200px;color:White; text-align:center">Report Group</td>
        <td style="width:200px;color:White; text-align:center">Report Frequency</td>
        <td style="color:White; text-align:center"></td>
    </tr>
    <tr>
        <td style="width:100px; text-align:center">
            <asp:DropDownList runat="server"  ID="ddlYear" CssClass="input_box" Width="80px" ></asp:DropDownList> 
        </td>
        
        <td style="width:200px; text-align:center">
            <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged" ></asp:DropDownList> 
           <%--<asp:Label ID="lblVessel" runat="server" Font-Bold="true"> </asp:Label>--%> <asp:HiddenField ID="hdnVesselId" runat="server" /> 
        </td>
        <td style="width:200px; text-align:center">
            <asp:DropDownList runat="server"  ID="ddlGroup" CssClass="input_box" Width="160px"></asp:DropDownList> 
        </td>
        <td style="width:200px; text-align:center">
            <asp:DropDownList runat="server"  ID="ddlFrequency" CssClass="input_box" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlFrequency_SelectedIndexChanged"></asp:DropDownList> 
        </td>
        <%--<td style="text-align:center">
            From 
            <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>
            <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> 
            &nbsp;&nbsp; TO
            <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="80px"  ></asp:TextBox>
            <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton>
            <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender> 
        </td>--%>
        <td style='text-align:center' >
            <asp:Button runat="server" CssClass="btn" ID="btnShow" Text="Show" Width="80px" onclick="btn_Show_Click" OnClientClick='ShowProgress(this)'/> 
            &nbsp;<asp:Button runat="server" CssClass="btn" ID="btnClear" Width="80px" Text="Clear" OnClick="btnClear_OnClick" /> 
            &nbsp;<%--<asp:Button id="btn_AddNew" runat="server" CssClass="btn" Width="120px" Text="Upload Reports" OnClick="btn_AddNew_Click" ></asp:Button>--%> &nbsp; <asp:Button id="btnVerify" runat="server" CssClass="btn" Width="120px" Text="Verify" OnClick="btnVerify_Click" style="display:none;" OnClientClick="return confirm('Are you sure to Verify Vessel Reports?');"></asp:Button>
        </td>
    </tr>
</table>
<div>
    <asp:Literal runat="server" ID="lit_Data"></asp:Literal>
</div>
<div id="DivAdhoc" runat="server" visible="false">
        <div style="overflow-y:scroll;overflow-x:hidden;background-color:White" id="histmessagebox" class="auto-style1">
                        <table cellpadding="4" cellspacing="0" border="1" style="border-collapse:collapse; width:70%;" >
                            <tr class= "headerstylegrid">
                                <td style="width:50px;text-align:left">Sr#</td>
                                <td style="width:250px;text-align:left;padding-left:5px; ">Adhoc Report Name</td>
                                <td style="width:50px;"></td>
                     
                            </tr>
                       
                          <asp:Repeater runat="server" ID="rptAdhocReportlist">
                              <ItemTemplate>
                               <tr>
                                <td style="text-align:left"><%#Eval("SrNo")%></td>
                                <td style="text-align:left;padding-left:5px;"><%#Eval("ReportName")%>
                                    <asp:HiddenField ID="hdnReportId" runat="server" Value='<%#Eval("ReportId")%>' />
                                </td>
                               
                                    <td style="text-align:center">
                                     <asp:ImageButton ID="btnViewAdhocReport"  GroupId='<%#Eval("GroupId")%>' Frequency='<%#Eval("Frequency")%>' ImageUrl= "~/Modules/HRD/Images/search_magnifier_12.png" CommandArgument='<%#Eval("ReportId")%>' OnClick="btnViewAdhocReport_Click" runat="server"  />
                                </td>
                              
                            </tr>
                            </ItemTemplate>
                          </asp:Repeater>
                             </table>
                    </div>
    </div>
<div style="position:absolute;top:0px;left:0px; height :250px; width:100%;" id="dvAddReport" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:700px;  height:250px;padding :5px; text-align :center;background : white; z-index:150;top:75px; border:solid 0px black;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <center >
            <div class="txt headerband"><b>Add / Edit Vessel Report </b></div>
            <div style=" margin-top:5px;">
            <table cellpadding="2" cellspacing="0" border="0" width='100%'>
            <tr>
            <td style='text-align:right; width:130px;'> Vessel :</td>
            <td>
                <asp:Label ID="lblVessel1" runat="server" Font-Bold="true"></asp:Label>
              <%--  <asp:DropDownList runat="server" id="ddlVessel1" Width='255px' OnSelectedIndexChanged="ddlVessel1_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>    
                <asp:CompareValidator runat="server"  ID="CompareValidator2" ControlToValidate="ddlVessel1" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Required."></asp:CompareValidator>--%>
            </td>
            </tr>
            <tr>
            <td style='text-align:right; width:130px;'> Year :</td>
            <td>
                <asp:DropDownList runat="server" id="ddlYear1" Width='255px'></asp:DropDownList>    
            </td>
            </tr>
            <tr>
            <td style='text-align:right; width:130px;'> Report :</td>
            <td>
                <asp:DropDownList runat="server" id="ddlReport" Width='255px' OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>    
                <asp:CompareValidator runat="server"  ID="CompareValidator1" ControlToValidate="ddlReport" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Required."></asp:CompareValidator>
            </td>
            </tr>
            <tr runat="server" id="tr_Period" visible="false">
            <td style='text-align:right; width:130px;'> Period :</td>
            <td>
                <asp:DropDownList runat="server" id="ddlPeriod" Width='255px' AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged"></asp:DropDownList>
                &nbsp;
                <asp:LinkButton runat="server" ID="lnkDownloadFile" OnClick="lnkDownloadFile_Click"></asp:LinkButton>
            </td>
            </tr>

            <tr>
            <td style='text-align:right; width:130px;'> Attachement :</td>
            <td>
                 <asp:FileUpload runat="server" ID="upl_File" Width="455px"/>
            </td>
            </tr>
            </table>
            </div>
                <div style="height:50px; margin-top:5px;text-align:center;">
                    <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" style="border:none; color:White; width:100px; " CssClass="btn" />  &nbsp;
                    <asp:Button runat="server" ID="btnClose" Text="Close" onclick="btnClose_Click"  CausesValidation="false" ToolTip='Close this Window !' CssClass="btn" style="border:none; color:White; width:100px; "/>   
                </div>
           
             </center>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDownloadFile" />
        <asp:PostBackTrigger ControlID="lnkDownloadFile" />
        <asp:PostBackTrigger ControlID="btnClose" />
        </Triggers>
        </asp:UpdatePanel>
         </div>
         </center>
        
</div>
    <div style="position:absolute;top:0px;left:0px; height :450px; width:100%;" id="dvAddAdhocReport" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:700px;  height:450px;padding :5px; text-align :center;background : white; z-index:150;top:75px; border:solid 0px black;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <center >
            <div class="txt headerband"><b>View Vessel Report (Adhoc)</b></div>
            <div style=" margin-top:5px;">
            <table cellpadding="2" cellspacing="0" border="0" width='100%'>
                <tr>
            <td style='text-align:center; ' colspan="2"> 
                <asp:Label ID="lblMessageAdhoc" runat="server" ForeColor="Red"></asp:Label>
            
            </td>
            </tr>
            <tr>
            <td style='text-align:right; width:130px;'> Vessel :</td>
            <td>
                <asp:Label ID="lblVesselName" runat="server" Font-Bold="true"></asp:Label>
              <%--  <asp:DropDownList runat="server" id="ddlVessel1" Width='255px' OnSelectedIndexChanged="ddlVessel1_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>    
                <asp:CompareValidator runat="server"  ID="CompareValidator2" ControlToValidate="ddlVessel1" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Required."></asp:CompareValidator>--%>
            </td>
            </tr>
            <tr>
            <td style='text-align:right; width:130px;'> Year :</td>
            <td>
                <asp:DropDownList runat="server" id="ddlAdhocReportYear" Width='255px' AutoPostBack="True" OnSelectedIndexChanged="ddlAdhocReportYear_SelectedIndexChanged"></asp:DropDownList>    
            </td>
            </tr>
            <tr>
            <td style='text-align:right; width:130px;'> Report :</td>
            <td>
                <asp:DropDownList runat="server" id="ddlAdhocReportList" Width='255px' OnSelectedIndexChanged="ddlAdhocReportList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>    
                <asp:CompareValidator runat="server"  ID="CompareValidator2" ControlToValidate="ddlAdhocReportList" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Required."></asp:CompareValidator>
            </td>
            </tr>
            </table>
            </div>
           
           <div style="overflow-y:scroll;overflow-x:hidden;background-color:White;height:250px;"  >
                <div style="text-align:left;padding:5px;font-weight:bold;font-size:14px;border-bottom:solid 1px #55b19c; background-color:lightgray;"> Adhoc Report List
                </div>
                <table cellpadding="4" cellspacing="0" border="1" style="border-collapse:collapse; width:100%;" >
                    <tr class= "headerstylegrid">
                        <td style="width:50px;text-align:left">Sr#</td>
                        <td style="width:250px;text-align:left;padding-left:5px; ">Attachment Name</td>
                        <td style="width:100px;"> Date </td>
                        <td style="width:50px;">  </td>
                        <%--<td style="width:50px;"> </td>--%>
                <%--          <td style="width:150px;">Imported By</td>--%>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAdhocVesselReportList">
                        <ItemTemplate>
                        <tr>
                        <td style="text-align:left"><%#Eval("SRNo")%></td>
                        <td style="text-align:left;padding-left:5px;"><%#Eval("AttachmentName")%>
                        </td>
                            <td style="text-align:left;padding-left:5px;"><%#Common.ToDateString(Eval("CreatedOn"))%>
                        </td>
                        <td style="text-align:center">
                        <asp:ImageButton ID="btnDownloadAdhocAttachment"    ImageUrl= "~/Modules/HRD/Images/paperclip12.gif" CommandArgument='<%#Eval("TableId")%>' OnClick="btnDownloadAdhocAttachment_Click" runat="server"  />
                        </td>
                       <%-- <td style="text-align:center">
                            <asp:ImageButton ID="btnDeleteAdhocReport"  ReportId='<%#Eval("ReportId")%>'  ImageUrl= "~/Images/Delete.png" CommandArgument='<%#Eval("TableId")%>' OnClick="btnDeleteAdhocReport_Click" runat="server"  />
                        </td>--%>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                </table>
           </div>
            <div style="height:30px; margin-top:5px;text-align:center;" >
              
                <asp:Button runat="server" ID="btnCloseAdhocReport" Text="Close" onclick="btnCloseAdhocReport_Click"  CausesValidation="false" ToolTip='Close this Window !' CssClass="btn" style="border:none; color:White; width:100px; "/>  &nbsp;
               
            </div>
             </center>
        </ContentTemplate>
        <Triggers>
        <%--<asp:PostBackTrigger ControlID="btnSaveAdhocReport" />--%>
        <asp:PostBackTrigger ControlID="btnCloseAdhocReport" />
        <asp:PostBackTrigger ControlID="rptAdhocVesselReportList" />
       
        </Triggers>
        </asp:UpdatePanel>
         </div>
         </center>
        
</div>
</asp:Content>

