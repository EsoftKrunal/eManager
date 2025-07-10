<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewList_N.aspx.cs" Inherits="CrewList_N" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

    <link rel="stylesheet" href="../../../css/app_style.css" />
    <%--<link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />--%>
    <link href="../../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />

    <title> </title>
    <style type="text/css">
        body {
            font-size:14px;
            font-family:Calibri; 
            margin:0px;        
        }

        

    table{border-collapse:collapse;}
    .borderd tr td{
            border:solid 1px #dddbdb;
            color:#333;
    }
    .header tr td{
        background-color:#4e4e4e;
        color:white;
    }
</style>    
</head>
<body >
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    
<table style="width :100%" cellpadding="0" cellspacing="0">
<tr>
<td >
<center>
<asp:HiddenField runat="server" ID="hfdCrew" />
<div style="vertical-align :middle; text-align :left;">
<table cellpadding="4" cellspacing="0" border="0" id="tblSearchPanel" runat="server" bordercolor="red" width="100%" style="background-color:#dddbdb">
<tr>
<td style="width:110px"><asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" Width="100px"  ></asp:DropDownList></td>
<td style="width:200px">
    
</td>
<td style="text-align:right">
<asp:Button runat="server" ID="btnShow" Text=" Show " OnClick="Show_Click" CssClass="btn" /> 
<asp:Button runat="server" ID="btnClear" Text=" Clear " OnClick="btnClear_Click" CssClass="btn" /> 
<input type="button" class="btn" value="Rest Hour NC Mgmt" onclick="window.open('RestHourReport_N.aspx', '');" />
</td>
</tr>
</table>
<table style="display:none;">    
<tr >
    <td>&nbsp;&nbsp;<b>From :</b>&nbsp;</td>
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
<td>&nbsp;&nbsp;<b>To :</b>&nbsp;</td>
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
<td>&nbsp;&nbsp;&nbsp; &nbsp;</td>
<td>&nbsp;Reason : &nbsp;&nbsp;</td>
<td>
    
    <asp:DropDownList ID="ddlReason" runat="server" CssClass="input_box" Width="150px"  ></asp:DropDownList>
    </td>

<td>Unverified NC :</td>
<td>
<asp:CheckBox runat="server" ID="chkUVNC" />
    </td>
<td></td>
<td>
    
    &nbsp;</td>

<td>&nbsp;</td>
<td>
    &nbsp;</td>
<td colspan="3" style="text-align:center">
    <b> <a href="RestHourReport.aspx" target="_blank" >Rest Hour NC Mgmt</a> </b></td>
</tr>
</table>
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" ></asp:Label>
</div>
    <div id="divVesselList" runat="server" >
    <div  style="overflow-x:hidden;overflow-y:scroll; height:30px;" >        
        <table cellpadding="5" cellspacing="0" border="1" width="100%" class="borderd header" height="30px" >
            <col width="40px" />
            <col width="40px" />
            <col />
            <%--<col width="80px" /> --%>
            <col width="180px" />            
            <tr class= "headerstylegrid">
                <td>&nbsp;</td>
                <td style="text-align:center;"">Sr#</td>
                <td>Vessel</td>
                <%--<td style="text-align:center;">Mail</td>--%>
                <td>Last Update Received On</td>                
            </tr>
        </table>
    </div>
    <div style="overflow-x:hidden;overflow-y:scroll; height:468px;font-size:13px;color:#4e4e4e;font-size:13px;" >        
        <table cellpadding="3" cellspacing="0" border="0" width="100%" class="borderd" >
            <colgroup>
            <col width="40px" />
            <col width="40px" />
            <col />
            <%--<col width="80px" /> --%>
            <col width="180px" />
                </colgroup>
            <asp:Repeater ID="rptVesselList" runat="server" >
                <ItemTemplate>
                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td style="text-align:center;">
                            <asp:ImageButton ID="LinkButton1" ImageUrl="~/Modules/HRD/Images/HourGlass.png" runat="server" ToolTip='<%#Eval("VesselName")%>' Text='<%#Eval("VesselName")%>' OnClick="lnlVessel_OnClick" CommandArgument='<%#Eval("VesselID")%>' style='<%#"color:" + Eval("VColor").ToString()%>'></asp:ImageButton>
                        </td>
                        <td style="text-align:center;"><%#Eval("Row")%></td>
                        <td style="text-align:left;"><%#Eval("VesselName")%>
                            <asp:HiddenField ID="hfVesselCode" runat="server" Value='<%#Eval("VesselCode")%>' />
                        </td>
                       <%-- <td style="text-align:center;">
                        <asp:ImageButton runat="server" id="btnMail" ImageUrl="~/Modules/HRD/Images/mail.gif" OnClick="btnMail_Click" CommandArgument='<%#Eval("VesselID")%>' OnClientClick="this.src='../Images/inprocss.gif';" />
                        </td>--%>
                        <td style="text-align:left;"><%#Common.ToDateString(Eval("LASTUPDATE_New"))%></td>                        
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>    
    </div>
    <div id="divCrewPortalUser" runat="server" visible="false">
         <table cellpadding="1" cellspacing="0" border="0" width="100%" style="background-color:#dddbdb">
                <tr>
                     <td style="padding-left:5px; text-align:left;">
                    <asp:Label ID="lblSelVessel" runat="server" style="font-weight:bold;margin:2px; padding:2px;" ></asp:Label>
                    &nbsp;:&nbsp;&nbsp;
                           <asp:DropDownList ID="ddlMonth" runat="server" CssClass="input_box" Width="50px" >
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

                         <asp:DropDownList ID="ddlYear" runat="server" CssClass="input_box" Width="55px" ></asp:DropDownList>
                    <asp:TextBox runat="server" id="txtDate" MaxLength="15" style="width:90px; margin-top:2px; text-align:center;" CssClass="required_box" Visible="false" ></asp:TextBox>

                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                        <asp:Button ID="btnShow11" runat="server" style="margin:2px;" Text="Show" Width="70px" CssClass="btn"  OnClick="btnShow11_OnClick" />
                    </td>
                    <td style="width:400px;text-align:right;">
                        <asp:Button ID="btnStatus" runat="server" style="margin:2px;" Text="Rest Hour Status" Width="130px" CssClass="btn"  OnClick="btnRHStatus_OnClick" />
                        <%--<asp:Button ID="btnSetup" runat="server" style="margin:2px;" Text="Setup" Width="70px" CssClass="btn"  OnClientClick="OpenSetup()" />--%>
                        <asp:Button ID="btnBack" runat="server" style="margin:2px;" Text="Back" Width="70px" CssClass="btn"  OnClick="btnBack_OnClick" />
                        
                        
                    </td>
                </tr>
        </table>
            

        <div  style="overflow-x:hidden;overflow-y:scroll; height:60px;" >
       <table  cellpadding="5" cellspacing="0" border="0" width="100%" class="borderd header" style="height:60px;" >
        <col width="70px" />
        <col />
        <col width="80px" />
        <col width="100px" />
        <col width="110px" />
        <%--<col width="120px" />--%>
        <col width="100px" />
        <col width="100px" />
        <col width="100px" />
        
        <tr style="text-align :left;" class= "headerstylegrid">
        
            <td>Crew #</td>
            <td>Name</td>
            <td style="text-align:center">Rank</td>
            <td style="text-align:center">Sign on Dt.</td>
            <td style="text-align:center">Sign off Dt.</td>
            <%--<td></td>--%>
            <td  style="text-align:center" colspan="2">Entries</td>            
            <%--<td colspan="2">NC </td>--%>
            <td  style="text-align:center">Alert</td>
            
        </tr>
        <tr style="text-align :center;" class="hd">
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <%--<td>Days On Board</td>--%>
            <td >Updated</td>
            <td>Missing</td>
            <td>( <asp:Label runat="Server" ID="lblMn"></asp:Label>  )</td>
        </tr>
        </table>
        </div>
        <div  style="overflow-x:hidden;overflow-y:scroll; height:440px;font-size:13px;" >        
        <table  cellpadding="5" cellspacing="0" border="0" width="100%" class="borderd" >
            <colgroup>
            <col width="70px" />
        <col />
        <col width="80px" />
        <col width="100px" />
        <col width="110px" />
        <%--<col width="120px" />--%>
        <col width="100px" />
        <col width="100px" />
        <col width="100px" />
        </colgroup>
        <asp:Repeater ID="rptCrewPortalUser" runat="server" >
            <ItemTemplate>
                <tr style="text-align :center;" valign="top">
                    <td>
                        <asp:LinkButton ID="lnkCrewNumber" runat="server" CssClass='<%#Eval("ContractId") %>' Text='<%#Eval("CrewNumber") %>' OnClick="lnkCrewNumber_OnClick" CommandArgument='<%#Eval("CrewId") %>' vessel='<%#Eval("VesselId") %>'></asp:LinkButton>
                    </td>
                    <td style="text-align:left">  <%#Eval("CrewName")%> </td>
                    <td align="center"><%#Eval("RankName")%></td>
                    <td align="center"><%# Eval("sSignOnDate")%></td>
                    <td align="center">
                        <asp:Label ID="lblSignOffDate" runat="server" Text='<%# Eval("sSignOffDate")%>'></asp:Label>                        
                    </td>
                    <%--<td align="center"><%#Eval("DaysOnBoard")%></td>--%>
                    <td align="center"><%# Eval("ME")%></td>
                    <td align="center">  <%# Convert.ToString(Common.CastAsInt32(Eval("DaysRequired")) - Common.CastAsInt32(Eval("ME")))  %> </td>
                    <td align="center">
                        <img src="../Images/Bell.png" style='display:<%#Eval("nc_cnt")%>' />
                    </td>
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

        <script type ="text/javascript">
function ShowBox()
{
    $('#tblAddChangedDate').slideToggle();
}
</script>
<script type="text/javascript">
    function SetCheckBox(chk)
    {
        var inputs = document.getElementsByTagName("input"); //or document.forms[0].elements;  
        if(chk.checked)
        {
            for (var i = 0; i < inputs.length; i++) 
            {  
              if (inputs[i].type == "checkbox") 
              {  
                
                inputs[i].checked=true;
                  
              }  
            }  
        }
        else
        {
            for (var i = 0; i < inputs.length; i++) 
            {  
              if (inputs[i].type == "checkbox") 
              {  
                
                inputs[i].checked=false;
                  
              }  
            }
        }
    }
    
    function OpenSetup()
    {
        var VessID= '<%=VesselID %>';
        window.open('RestHourSetup.aspx?VesselID='+VessID,'','letf=0,top=0,width=600,height=450,toolbar=0,scrollbars=0,status=0');
    }
    function OpenNCReport(NC,CID)
    {
        if(NC>0)
        {
            var VessID= '<%=VesselID %>';
            var d = new Date();
            var y = d.getFullYear();
            var str = './Reports/NCList.aspx?v=' + VessID + '&c=' + CID + '&m=' + (parseInt(d.getMonth()) + 1) + '&y=' + y;
            window.open(str,'','letf=0,top=0,toolbar=0,scrollbars=1,status=0')
        }
    }
    function OpenNCReport2(NC,CID,ContractId)
    {
        if(NC>0)
        {
            var VessID= '<%=VesselID %>';
            var d = new Date();
            var str='./Reports/NCList.aspx?v='+VessID+'&c='+CID+'&cid=' + ContractId;
            window.open(str,'','letf=0,top=0,toolbar=0,scrollbars=1,status=0')
        }
    }
    //-----------------------
    function ShowPending(vsl,QueryTYpe) {

        var ddlFleet = document.getElementById("ctl00_contentPlaceHolder1_ddlFleet");
        var ddlVesselType = document.getElementById("ctl00_contentPlaceHolder1_ddlVesselType");
        var ddlReason = document.getElementById("ctl00_contentPlaceHolder1_ddlReason");
        var ddlMonthFrom = document.getElementById("ctl00_contentPlaceHolder1_ddlMonthFrom");
        var ddlMonthTO = document.getElementById("ctl00_contentPlaceHolder1_ddlMonthTO");
        var ddlYearFrom = document.getElementById("ctl00_contentPlaceHolder1_ddlYearFrom");
        var ddlYearTO = document.getElementById("ctl00_contentPlaceHolder1_ddlYearTO");
        var chkNC = document.getElementById("ctl00_contentPlaceHolder1_chkUVNC");
         
        var fleet = ddlFleet.options[ddlFleet.selectedIndex].value;
        var vesseltype = ddlVesselType.options[ddlVesselType.selectedIndex].value;
        var reason = ddlReason.options[ddlReason.selectedIndex].value;
        var FM = ddlMonthFrom.options[ddlMonthFrom.selectedIndex].value;
        var TM = ddlMonthTO.options[ddlMonthTO.selectedIndex].value;
        var FY = ddlYearFrom.options[ddlYearFrom.selectedIndex].value;
        var TY = ddlYearTO.options[ddlYearTO.selectedIndex].value;
        var VNC = chkNC.checked;

        window.open("VerifyWC.aspx?vesselid=" + vsl + "&QueryTYpe=" + QueryTYpe + "&fleet=" + fleet + "&vtype=" + vesseltype + "&reason=" + reason + "&fm=" + FM + "&tm=" + TM + "&fy=" + FY + "&ty=" + TY + "&VNC=" + VNC, "", "", ""); 
    }
    //-----------------------
    function ShowVerified() {
        var fleet = ddlFleet.options[ddlFleet.selectedIndex].value;
        var vesseltype = ddlVesselType.options[ddlVesselType.selectedIndex].value;
        var reason = ddlReason.options[ddlReason.selectedIndex].value;
        var from = ddlMonthFrom.options[ddlMonthFrom.selectedIndex].value + ddlYearFrom.options[ddlYearFrom.selectedIndex].value;
        var to = ddlMonthTO.options[ddlMonthTO.selectedIndex].value + ddlYearTO.options[ddlYearTO.selectedIndex].value;
        window.open("VerifiedWC.aspx?fleet=" + fleet + "&vtype=" + vesseltype + "&reason=" + reason + "&from=" + from + "&to=" + to, "", "", ""); 

    }
</script>

        </form>
        </body>
</html>