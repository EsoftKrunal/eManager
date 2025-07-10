<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewList.aspx.cs" Inherits="CrewList"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

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
<div style="vertical-align :middle;  text-align :left; padding-left :0px;">
<table cellpadding="4" cellspacing="0" border="0" id="tblSearchPanel" runat="server" bordercolor="red" width="100%" style="background-color:#dddbdb">
<tr>
<td><asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" Width="100px"  ></asp:DropDownList></td>
<td>
    <asp:DropDownList ID="ddlVesselType" runat="server" CssClass="input_box" Width="190px"  ></asp:DropDownList>
</td>
<td>From :</td>
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
<td>To :</td>
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
    <td><asp:DropDownList ID="ddlReason" runat="server" CssClass="input_box" Width="150px"  ></asp:DropDownList></td>
    <td><asp:CheckBox runat="server" ID="chkUVNC" Text="Unverified NC" /></td>
<td style="text-align:right">
<asp:Button runat="server" ID="btnShow" Text=" Show " OnClick="Show_Click" CssClass="btn" /> 
<asp:Button runat="server" ID="btnClear" Text=" Clear " OnClick="btnClear_Click" CssClass="btn" /> 
<input type="button" class="btn" value="Rest Hour NC Mgmt"  onclick="window.open('RestHourReport.aspx', '');" />
</td>
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
            <col width="40px" />
            <col width="180px" />
            <col width="17px" />
            <tr>
                <td style="text-align:center;">&nbsp;</td>
                <td style="text-align:center;">Sr#</td>
                <td>Vessel</td>
                <td style="text-align:center;">Mail</td>
                <td>Last Update Received On</td>                
                <td></td>
            </tr>           
        </table>
    </div>
        <div style="overflow-x:hidden;overflow-y:scroll; height:468px;font-size:13px;color:#4e4e4e" >        
        <table cellpadding="3" cellspacing="0" border="0" width="100%" class="borderd" >
            <col width="40px" />
            <col width="40px" />
            <col />
            <col width="40px" />
            <col width="180px" />
            <%--<col width="200px" />
            <col width="170px" />
            <col width="160px" />
            <col width="160px" />--%>


            <%--<col width="70px" />
            <col width="70px" />
            <col width="70px" />--%>
            <col width="17px" />
            <asp:Repeater ID="rptVesselList" runat="server" >
                <ItemTemplate>
                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td style="text-align:center;">
                            <asp:ImageButton ID="LinkButton1" ImageUrl="~/Modules/HRD/Images/HourGlass.png" runat="server" ToolTip='<%#Eval("VesselName")%>' Text='<%#Eval("VesselName")%>' OnClick="lnlVessel_OnClick" CommandArgument='<%#Eval("VesselID")%>' style='<%#"color:" + Eval("VColor").ToString()%>'></asp:ImageButton>
                        </td>
                        <td style="text-align:center;"><%#Eval("Row")%></td>
                        <td style="text-align:left;"><%#Eval("VesselName")%></td>
                        <td style="text-align:center;">
                        <asp:ImageButton runat="server" id="btnMail" ImageUrl="~/Modules/HRD/Images/mail.gif" OnClick="btnMail_Click" CommandArgument='<%#Eval("VesselID")%>' OnClientClick="this.src='../Images/inprocss.gif';" />
                        </td>
                        <td><%#Common.ToDateString(Eval("LASTUPDATE"))%></td>
                        
                        <%--<td><a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"NCType1");'> <%# getCountNC(Eval("VesselID").ToString(), 1)%></a></td>
                        <td><a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"NCType2");'> <%# getCountNC(Eval("VesselID").ToString(), 2)%></a></td>
                        <td><a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"NCType3");'> <%# getCountNC(Eval("VesselID").ToString(), 3)%></a></td>
                        <td><a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"NCType4");'> <%# getCountNC(Eval("VesselID").ToString(), 4)%></a></td>--%>





                        <%--<td> <a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"UnVerified");'> <%# getCountUnVerified(Eval("VesselID").ToString())%></a> </td>
                        <td> <a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"Verified");'> <%# getCountVerified(Eval("VesselID").ToString())%> </a>  </td>
                        <td> <a href="#" onclick='ShowPending(<%#Eval("VesselID")%>,"Total");'> <%# getCountTotal(Eval("VesselID").ToString())%> </a> </td>--%>
                        <td></td>
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
                    <asp:TextBox runat="server" id="txtDate" MaxLength="15" style="width:90px; margin-top:2px; text-align:center;" CssClass="required_box" ></asp:TextBox>

                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                    <asp:Button ID="btnShow11" runat="server" style=" margin:2px;" Text="Show" Width="70px" CssClass="btn"  OnClick="btnShow11_OnClick" />
                    <asp:Button ID="btnBack" runat="server" style="float:right; margin:2px;" Text="Back" Width="70px" CssClass="btn"  OnClick="btnBack_OnClick" />
                    <asp:Button ID="btnSetup" runat="server" style="float:right; margin:2px;" Text="Setup" Width="70px" CssClass="btn"  OnClientClick="OpenSetup()" />
                </td>
            </tr>
        </table>
        <div  style="overflow-x:hidden;overflow-y:scroll; height:60px;" >        
    <table  cellpadding="5" cellspacing="0" border="0" width="100%" class="borderd header" style="height:60px;" >
        <col width="70px" />
        <col />
        <col width="80px" />
        <col width="110px" />
        <col width="120px" />
        <col width="115px" />
        <col width="100px" />
        <col width="120px" />
        <col width="105px" />
        
        <tr >
        
            <td>Crew #</td>
            <td>Name</td>
            <td>Rank</td>
            <td>Sign on Date</td>
            <td>Sign off Date</td>
            <td></td>
            <td colspan="2">Entries</td>
            <td>Alert</td>
        </tr>
        <tr >        
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>Days On Board</td>
            <td >This Month</td>
            <td>Since S-On Dt</td>
            <td>(<asp:Label runat="Server" ID="lblMn"></asp:Label> )</td>
        </tr>
        </table>
    </div>
        <div  style="overflow-x:hidden;overflow-y:scroll; height:440px;font-size:13px;" >        
        <table  cellpadding="5" cellspacing="0" border="0" width="100%" class="borderd" >
           <col width="70px" />
        <col />
        <col width="80px" />
        <col width="110px" />
        <col width="120px" />
        <col width="115px" />
        <col width="100px" />
        <col width="120px" />
        <col width="105px" />
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
                    <%--<td align="center" style='cursor:pointer;background-color:<%#Eval("CSSTM")%>' onclick="OpenNCReport(<%#Eval("NCCOUNTThisMonth")%>,<%#Eval("CrewId") %>)" ><%#Eval("NCCOUNTThisMonth")%></td>
                    <td align="center" style='cursor:pointer; background-color:<%#Eval("CSS")%>' onclick="OpenNCReport2(<%#Eval("NCCOUNT")%>,<%#Eval("CrewId") %>,<%#Eval("ContractId") %>)"><%#Eval("NCCOUNT")%></td>--%>
                    <td align="center"><%#GetCountNewNC(Eval("CrewId"), Eval("VesselId"))%></td>
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