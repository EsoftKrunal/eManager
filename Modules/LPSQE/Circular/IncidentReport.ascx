<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IncidentReport.ascx.cs" Inherits="ucIncidentReport" %>
<script language="javascript" type="text/javascript">
    function OpenLTIReport()
    {
        window.open('..\\Reports\\LTI_Report.aspx','ltirpt','title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');        
    }
    function OpenNMReport()
    {
        window.open('..\\Reports\\NM_Report.aspx','ltirpt','title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');        
    }
</script>
<li style="display:inline"><a href="#" title="Click here to open LTI-M Report" onclick="return OpenLTIReport();">1. LTI-M Report</a>&nbsp;&nbsp;</li>
<li style="display:inline"><a href="#" title="Click here to open NM Report" onclick="return OpenNMReport();">2. NM Report</a>&nbsp;&nbsp;</li>
<li style="display:inline"><a href="../Reports/NMAnalysis.aspx" target="_blank" title="Click here to open NM Analysis" >3. NM Analysis</a></li>
