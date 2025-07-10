<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mainmene.ascx.cs" Inherits="HSSQE_mainmene" %>
<ul class="menu">
    <li id="li_0"><a runat="Server" href="~/Drill/DrillPlanner.aspx">Drill & Trainings</a></li>
    <li id="li_4"><a runat="Server" href="~/eReports/Home.aspx"> Incident</a></li>
    <li id="li_3"><a runat="Server" href="~/eReports/G118/G118_List.aspx">NCR</a></li>
    <li id="li_5"><a runat="Server" href="~/VIMS/Regulation.aspx">Regulation</a></li>
    <li id="li_6"><a runat="Server" href="~/VIMS/RiskManagement.aspx">Risk Management</a></li>
    <li id="li_7"><a runat="Server" href="~/VIMS/LFI.aspx">LFI/LET</a></li>
    <li id="li_8"><a runat="Server" href="~/VIMS/FC.aspx">Focussed Campaign</a></li>
    <li id="li_9"><a runat="Server" href="~/VIMS/Circular.aspx">Circular</a></li>
    <li class="clear" style="display:none;"></li>
</ul>
<script type="text/javascript">
    document.getElementById('li_' + '<%=Session["MHSS"].ToString()%>').className = "active";
</script>