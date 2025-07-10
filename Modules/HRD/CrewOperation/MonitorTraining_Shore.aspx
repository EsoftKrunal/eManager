<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonitorTraining_Shore.aspx.cs" Inherits="CrewOperation_MonitorTraining_Shore"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
      <table style="width :100%" cellpadding="0" cellspacing="0">
<tr>
<td >
<center>
<asp:HiddenField runat="server" ID="hfdCrew" />
<div style="vertical-align :middle; font-size:12px; text-align :left; padding-left :0px;">
<table cellpadding="2" cellspacing="0" border="0" id="tblSearchPanel" runat="server" bordercolor="red" width="100%" style="margin-top:2px;margin-bottom:2px;font-family:Arial;font-size:12px;">
<tr>
<td>&nbsp;</td>
<td>&nbsp; <asp:Label ID="lblFleet_Office" Text="Office : " runat="server"></asp:Label></td>
<td>
    <asp:DropDownList ID="ddlOffice" runat="server" CssClass="input_box" Width="100px" Visible="true"  ></asp:DropDownList>
</td>


<td>&nbsp;</td>
<td>
    
    &nbsp;</td>

<td>&nbsp;&nbsp;From :&nbsp;</td>
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
<td>&nbsp;&nbsp;To :&nbsp;</td>
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
            <col width="100px" />            
            <col width="170px" />
            <col width="160px" />
            <col width="160px" />
            <col width="170px" />
            <col width="160px" />
            <col width="160px" />
            <tr class= "headerstylegrid">                
                <td><asp:Label ID="lblVessel_Office" Text="Vessel" runat="server" ></asp:Label></td>
                <td colspan="3">Training Due</td>
                <td colspan="3">Training Done</td>
            </tr>
            <tr class= "headerstylegrid" >
                <td style="text-align:left">Office Name</td>
                 <td>By Crew</td>
                <td>By Rank</td>
                <td>By Training Name</td>
                <td>By Crew</td>
                <td>By Rank</td>
                <td>By Training Name</td>
            </tr>
        </table>
    </div>    
    <div id="div_OfficewiseList" visible="false" runat="server" style="overflow-x:hidden;overflow-y:scroll; height:310px;" >        
        <table  cellpadding="2" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse" >
             <col />
            <col width="100px" />            
            <col width="170px" />
            <col width="160px" />
            <col width="160px" />
            <col width="170px" />
            <col width="160px" />
            <col width="160px" />
            <asp:Repeater ID="rptOfficewiseList" runat="server" >
                <ItemTemplate>
                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td style="text-align:left;"><%#Eval("RecruitingOfficeName")%>         </td>
                        <td style="text-align:center"><asp:LinkButton runat="server" ID="link_Click" CommandArgument='<%#Eval("RecruitingOfficeId")%>' CssClass="CDue" OnClick="btnLink_Click" Text='<%#Eval("ByCrew_Due")%>'></asp:LinkButton></td>
                        <td style="text-align:center"><asp:LinkButton runat="server" ID="LinkButton1" CommandArgument='<%#Eval("RecruitingOfficeId")%>' CssClass="RDue" OnClick="btnLink_Click" Text='<%#Eval("ByRank_Due")%>'></asp:LinkButton></td>
                        <td style="text-align:center"><asp:LinkButton runat="server" ID="LinkButton2" CommandArgument='<%#Eval("RecruitingOfficeId")%>' CssClass="TDue" OnClick="btnLink_Click" Text='<%#Eval("ByTrainingType_Due")%>'></asp:LinkButton></td>

                        <td style="text-align:center"><asp:LinkButton runat="server" ID="LinkButton3" CommandArgument='<%#Eval("RecruitingOfficeId")%>' CssClass="CDone" OnClick="btnLink_Click" Text='<%#Eval("ByCrew")%>'></asp:LinkButton> </td>
                        <td style="text-align:center"><asp:LinkButton runat="server" ID="LinkButton4" CommandArgument='<%#Eval("RecruitingOfficeId")%>' CssClass="RDone" OnClick="btnLink_Click" Text='<%#Eval("ByRank")%>'></asp:LinkButton></td>
                        <td style="text-align:center"><asp:LinkButton runat="server" ID="LinkButton5" CommandArgument='<%#Eval("RecruitingOfficeId")%>' CssClass="TDone" OnClick="btnLink_Click" Text='<%#Eval("ByTrainingType")%>'></asp:LinkButton></td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>    
    </div>
    <div id="divCrewPortalUser" runat="server" visible="false">
        <asp:Label ID="lblSelVessel" runat="server" style="font-weight:bold;float:left; margin:2px; padding:2px;" ></asp:Label>
        <asp:TextBox runat="server" id="txtDate" MaxLength="15" style="float:left;width:80px; margin-top:2px;" CssClass="required_box" ></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
        <asp:Button ID="btnShow11" runat="server" style="float:left; margin:2px;" Text="Show" Width="70px" CssClass="btn"  OnClick="btnShow11_OnClick" />

        <asp:Button ID="btnBack" runat="server" style="float:right; margin:2px;" Text="Back" Width="70px" CssClass="btn"  OnClick="btnBack_OnClick" />
        <asp:Button ID="btnSetup" runat="server" style="float:right; margin:2px;" Text="Setup" Width="70px" CssClass="btn"  OnClientClick="OpenSetup()" />
        <table  cellpadding="2" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse;font-family:Arial;font-size:12px;" >
            <col width="70px" />
            <col />
            <col width="50px" />
            <col width="90px" />
            <col width="90px" />
            <col width="100px" />
            <col width="80px" />
            <col width="90px" />
            <col width="80px" />
            <col width="90px" />
        
            <tr class= "headerstylegrid">
        
                <td>Crew #</td>
                <td>Name</td>
                <td>Rank</td>
                <td>Sign on Date</td>
                <td>Sign off Date</td>
                <td></td>
                <td colspan="2">Entries</td>
                <td colspan="2">NC </td>
            </tr>
            <tr class= "headerstylegrid">
        
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>Days On Board</td>
                <td >This Month</td>
                <td>Since S-On Dt</td>
                <td >This Month</td>
                <td>Since S-On Dt</td>
            </tr>
            <asp:Repeater ID="rptCrewPortalUser" runat="server" >
                <ItemTemplate>
                    <tr style="text-align :center;" valign="top">
                        <td>
                            <asp:LinkButton ID="lnkCrewNumber" runat="server" CssClass='<%#Eval("ContractId") %>' Text='<%#Eval("CrewNumber") %>' OnClick="lnkCrewNumber_OnClick" CommandArgument='<%#Eval("CrewId") %>' vessel='<%#Eval("VesselId") %>'></asp:LinkButton>
                        </td>
                        <td style="text-align:left">  <%#Eval("CrewName")%> </td>
                        <td align="center"><%#Eval("RankName")%></td>
                        <td align="center"><%#Eval("SignOnDate")%></td>
                        <td align="center">
                            <asp:Label ID="lblSignOffDate" runat="server" Text='<%#Eval("SignOffDate")%>'></asp:Label>                        
                        </td>
                        <td align="center"><%#Eval("DaysOnBoard")%></td>
                        <td align="center"><%#Eval("METM")%></td>
                        <td align="center"><%#Eval("ME")%></td>
                        <td align="center" style='cursor:pointer;background-color:<%#Eval("CSSTM")%>' onclick="OpenNCReport(<%#Eval("NCCOUNTThisMonth")%>,<%#Eval("CrewId") %>)" >
                            <%#Eval("NCCOUNTThisMonth")%>
                         </td>
                        <td align="center" style='cursor:pointer; background-color:<%#Eval("CSS")%>' onclick="OpenNCReport2(<%#Eval("NCCOUNT")%>,<%#Eval("CrewId") %>,<%#Eval("ContractId") %>)"><%#Eval("NCCOUNT")%></td>
                    
                    </tr>    
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div> 

</center>
</td>
</tr>
</table>
    </div>
    </form>
</body>
</html>
