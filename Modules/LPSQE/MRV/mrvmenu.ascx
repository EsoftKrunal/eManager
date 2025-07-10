<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mrvmenu.ascx.cs" Inherits="MRV_mrvmenu" %>
<ul class="menu">
    <li id="li_0"><a href="Home.aspx">Home</a></li>
    <li id="li_1"><a href="VesselSetup.aspx">Vessel SetUp</a></li>
    <li id="li_2"><a href="FuelTypes.aspx">Fuel Types</a></li>
    <li id="li_3"><a href="Activity.aspx">Activity</a></li>
    <li class="clear" style="display:none;"></li>
</ul>
<script type="text/javascript">
    document.getElementById('li_' + '<%=Session["MM"].ToString()%>').className = "active";
</script>