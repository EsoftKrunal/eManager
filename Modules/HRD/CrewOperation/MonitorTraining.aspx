<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonitorTraining.aspx.cs" Inherits="CrewOperation_MonitorTraining" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />  
 <link href="~/Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <script type ="text/javascript">
        function ShowBox() {
            $('#tblAddChangedDate').slideToggle();
        }

        function ShowDetails(VslId, Mode) {
            var fm = document.getElementById("ddlMonthFrom");
            var fy = document.getElementById("ddlYearFrom");            
            var fd = fm.options[fm.selectedIndex].text + "-" + fy.options[fy.selectedIndex].text;

            var tm = document.getElementById("ddlMonthTO");
            var ty = document.getElementById("ddlYearTO");
            var td = tm.options[tm.selectedIndex].text + "-" + ty.options[ty.selectedIndex].text;
            window.open("popupMonitorTrainingDetails.aspx?Vsl=" + VslId + "&Mode=" + Mode + "&FD=" + fd + "&TD=" + td, "asdf", "", "");
        }
</script>
</head>
<body>
<form id="form1" runat="server">
<div style="font-family:Arial;font-size:12px;">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td >
<center>
<asp:HiddenField runat="server" ID="hfdCrew" />
<div style="vertical-align :middle; font-size:12px; text-align :left; padding-left :0px;">
<table cellpadding="4" cellspacing="0" border="0" id="tblSearchPanel" runat="server" bordercolor="red" width="100%" style="margin-top:2px;margin-bottom:2px;">
<tr>
<td style="text-align:right">Fleet :</td>
<td><asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" Width="100px"  ></asp:DropDownList>
    <asp:DropDownList ID="ddlOffice" runat="server" CssClass="input_box" Width="100px" Visible="false"  ></asp:DropDownList>
</td>


<td>&nbsp;</td>
<td>
    
    &nbsp;</td>

<td style="text-align:right">From :</td>
<td>
<asp:DropDownList ID="ddlMonthFrom" runat="server" CssClass="input_box" Width="50px" >
    <asp:ListItem Text="Jan" Value="1" ></asp:ListItem>  
    <asp:ListItem Text="Feb" Value="2" ></asp:ListItem>  
    <asp:ListItem Text="Mar" Value="3" ></asp:ListItem>  
    <asp:ListItem Text="Apr" Value="4" ></asp:ListItem>  
    <asp:ListItem Text="May" Value="5" ></asp:ListItem>  
    <asp:ListItem Text="Jun" Value="6" ></asp:ListItem>  
    <asp:ListItem Text="Jul" Value="7" ></asp:ListItem>  
    <asp:ListItem Text="Aug" Value="8" ></asp:ListItem>  
    <asp:ListItem Text="Sep" Value="9" ></asp:ListItem>  
    <asp:ListItem Text="Oct" Value="10" ></asp:ListItem>  
    <asp:ListItem Text="Nov" Value="11" ></asp:ListItem>  
    <asp:ListItem Text="Dec" Value="12" ></asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="ddlYearFrom" runat="server" CssClass="input_box" Width="55px" ></asp:DropDownList>
</td>
    <td style="text-align:right">To :</td>
<td>
        <asp:DropDownList ID="ddlMonthTO" runat="server" CssClass="input_box" Width="50px">
        <asp:ListItem Text="Jan" Value="1" ></asp:ListItem>  
        <asp:ListItem Text="Feb" Value="2" ></asp:ListItem>  
        <asp:ListItem Text="Mar" Value="3" ></asp:ListItem>  
        <asp:ListItem Text="Apr" Value="4" ></asp:ListItem>  
        <asp:ListItem Text="May" Value="5" ></asp:ListItem>  
        <asp:ListItem Text="Jun" Value="6" ></asp:ListItem>  
        <asp:ListItem Text="Jul" Value="7" ></asp:ListItem>  
        <asp:ListItem Text="Aug" Value="8" ></asp:ListItem>  
        <asp:ListItem Text="Sep" Value="9" ></asp:ListItem>  
        <asp:ListItem Text="Oct" Value="10" ></asp:ListItem>  
        <asp:ListItem Text="Nov" Value="11" ></asp:ListItem>  
        <asp:ListItem Text="Dec" Value="12" ></asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlYearTO" runat="server" CssClass="input_box" Width="55px" ></asp:DropDownList>
</td>
<td>
<asp:Button runat="server" ID="btnShow" Text=" Show " OnClick="Show_Click" CssClass="btn" /> 
<asp:Button runat="server" ID="btnClear" Text=" Clear " OnClick="btnClear_Click" CssClass="btn" /> 
</td>
</tr>
</table>
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" ></asp:Label>
</div>
    <div id="divVesselList" runat="server" >
    <div  style="overflow-x:hidden;overflow-y:scroll; height:50px;" >        
        <table  cellpadding="2" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse; height:50px;" >            
            <col />
            <col width="40px" />
            <col width="100px" />
            <col width="140px" />
            <%--<col width="140px" />--%>
            <col width="130px" />
            <col width="130px" />
            <col width="130px" />
            <col width="17px" />
            <tr class= "headerstylegrid">                
                <td>
                    <asp:Label ID="lblVessel_Office" Text="Vessel" runat="server"></asp:Label></td>
                <td>Mail</td>
                <td>Last Update</td>
                <td>Onboard</td>
             <%--   <td>&nbsp;</td>--%>
                <td colspan="3">Training Done</td>
                <td></td>
            </tr>
            <tr class= "headerstylegrid" >
                <%--<td></td>--%>
                <td><asp:Label runat="server" ID="l1"></asp:Label></td>
                <td></td>
                <td></td>
                <td>Training Status</td>
              <%--  <td>Training Due</td>--%>
                <td>By Crew</td>
                <td>By Training Name</td>
                <td>By Rank</td>
                <td></td>
            </tr>
        </table>
    </div>
    <div id="dv_VesselWiseList"  runat="server" style="overflow-x:hidden;overflow-y:scroll; height:406px;" >        
        <table  cellpadding="2" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse" >
            <%--<col width="40px" />--%>
            <col />
            <col width="40px" />
            <col width="100px" />
            <col width="140px" />
            <%--<col width="140px" />--%>
            <col width="130px" />
            <col width="130px" />
            <col width="130px" />
            <col width="17px" />
            <asp:Repeater ID="rptVesselList" runat="server" >
                <ItemTemplate>
                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <%--<td><%#Eval("Row")%></td>--%>
                        <td style="text-align:left;">
                            <%--<asp:LinkButton ID="lnlVessel" runat="server" Text='<%#Eval("VesselName")%>' OnClick="lnlVessel_OnClick" CommandArgument='<%#Eval("VesselID")%>' ></asp:LinkButton>--%>
                            <%#Eval("VesselName")%>
                        </td>
                        <td style="text-align:center;">
                        <asp:ImageButton runat="server" id="btnMail" ImageUrl="~/Modules/HRD/Images/icon-email.gif" OnClick="btnMail_Click" CommandArgument='<%#Eval("VesselID")%>' OnClientClick="this.src='../Images/inprocss.gif';" />
                        </td>
                        <td style="text-align:center;"><%#Common.ToDateString(Eval("LastUpdateRecdOn"))%></td>
                        <td style="text-align:center">
                        <asp:ImageButton runat="server" id="btnTrainingMatrix" ImageUrl="~/Modules/HRD/Images/training.png" ToolTip="Training Matrix" OnClick="btnTrainingMatrix_Click" CommandArgument='<%#Eval("VesselID")%>' OnClientClick="this.src='../Images/inprocss.gif';" />
                        </td>
                        <%--<td style="text-align:center;"><a href="#" onclick='ShowDetails(<%#Eval("VesselID")%>,1);'> <%#Eval("odcOUNT")%></a></td>--%>
                        <td style="text-align:center;"><a href="#" onclick='ShowDetails(<%#Eval("VesselID")%>,2);'> <%#Eval("ByCrew")%></a></td>
                        <td style="text-align:center;"><a href="#" onclick='ShowDetails(<%#Eval("VesselID")%>,3);'> <%#Eval("ByTrainingType")%></a></td>
                        <td style="text-align:center;"><a href="#" onclick='ShowDetails(<%#Eval("VesselID")%>,4);'> <%#Eval("ByRank")%></a></td>
                        <td></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div> 
        
    </div>
</center>
</td>
</tr>
</table>
    </div>
    </form>
</body>
</html>
