<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InspectionReport.aspx.cs" Inherits="Modules_Inspection_InpectionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <%-- <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
  <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
   <%--  <link href="Styles/tabs.css" rel="stylesheet" type="text/css" />--%>
      <script language="javascript" type="text/javascript">
        function CallPrint(strid)
        {
         var prtContent = document.getElementById(strid);
         var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
         WinPrint.document.write(prtContent.innerHTML);
         WinPrint.document.close();
         WinPrint.focus();
         WinPrint.print();
         WinPrint.close();
        // prtContent.innerHTML=strOldOne;
        }
      </script>
    <script type="text/javascript">
        CallAfterRefresh();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="text-align: center">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
          <table style="width :100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
       
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9 " width="100%">
            <tr style=" background-color:#E2E2E2">
                  <td align="center" style="height: 23px; text-align :center" >
                   <div style=' font-size:16px; margin-top:5px; font-family:Arial;' class="text headerband" >Inspection Reports
                   </div>
                   </td>
            </tr>
            <tr>
                <td style="height: 485px; background-color:#f9f9f9" valign="top">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:10px; padding-left:11px;">
                                <br />
                                <div id="header"><%--tabs--%>
                                    <table width="100%">
                                        <tr style="height:30px;padding:3px;">
 <td>
     <a href="Reports/InspMisc_Report.aspx" target="_blank"><span>Inspection Miscellaneous</span></a>
                                        </td>
                                        <td>
                                            <a href="Reports/OperatorReporting_Report.aspx"  target="_blank"><span>Operator Reporting</span></a>
                                        </td>
                                            <td>
                                                <a href="Reports/Inspection_Count_Report.aspx" target="_blank"><span>Inspection Count Report</span></a>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr style="height:30px;padding:3px;">
                                            <td><a href="Reports/ATA.aspx" target="_blank"><span>Audit Trend Analysis</span></a></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                       
                                    </table>
                                  <ul>
                                    <li> </li>
                                    <%--<li><a href="InspHistory_Report.aspx"><span>Inspection Status</span></a></li>--%>
                                    <li></li>
                                    <%--<li><a href="ObservationReporting_Report.aspx"><span>Observation Report</span></a></li>
                                    <li><a href="OperatorReportingSummary_Report.aspx"><span>Vetting Status Report</span></a></li>--%>
                                    <%--<li><a href="Reports/FIR_Report.aspx" target="_blank"><span>FIR Report</span></a></li>--%>
                                   <%-- <li><a href="Reports/Motor_Report.aspx" target="_blank"><span>MOTOR</span></a></li>--%>
                                    <li></li>
                                  <%--  <li style='<%=((Session["loginid"].ToString()=="1")?"":"display:none")%>'><a href="Reports/AuditTrendAnalysisReport.aspx" target="_blank"><span>Audit Trend Analysis-Old</span></a></li>--%>
                                    <li></li>
                                  </ul>
                                </div>
                                <script>
                                    function tabing() {
                                        var strHref = window.location.href;
                                        var strQueryString = strHref.split(".aspx");
                                        var aQueryString = strQueryString[0].split("/");
                                        var curpage = unescape(aQueryString[aQueryString.length - 1]).toLowerCase();
                                        //alert(curpage); 
                                        //alert(document.getElementById('navigation1').getElementsByTagName('li').length);
                                        var menunum = document.getElementById('header').getElementsByTagName('li').length;
                                        var menu = document.getElementById('header').getElementsByTagName('li')
                                        //	                                 if(curpage=='inspection'){
                                        //	                                     menu[1].className='current';
                                        //	                                 }else{
                                        for (x = 0; x < menunum; x++) {
                                            //alert(unescape(menu[x].childNodes[0]).toLowerCase());

                                            if (unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1) {
                                                //alert(menu[x].childNodes[0])
                                                menu[x].className = 'current';
                                            }

                                        }
                                        //	                                 }
                                    }
                                    tabing();
                                </script>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 10px; height:10px;">
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table> 
        </td> 
       </tr> 
       </table> 
     </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainFoot" Runat="Server">
</asp:Content>

