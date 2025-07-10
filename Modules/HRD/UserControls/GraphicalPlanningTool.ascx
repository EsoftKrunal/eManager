<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GraphicalPlanningTool.ascx.cs" Inherits="UserControls_GraphicalPlanningTool" %>
 <link rel="stylesheet" href="../../../css/app_style.css" />
 <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<style type="text/css">
    .cent
    {
    	text-align :center ;
    }
</style>
<script type="text/javascript">

function Print_ByVessel()
{
    window.open('../Reporting/PrintCrewRequirement.aspx?Mode=V');
    return false;
}

function Print_ByRank()
{
    
    window.open('../Reporting/PrintCrewRequirement.aspx?Mode=R');
    return false;
}
</script>
<asp:Label ID="lb_msg" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
<table cellpadding="0" cellspacing="0" border="1" width="100%" style="font-family:Arial;font-size:12px;"  >
<tr>
<td style=" background-color : #e2e2e2; padding : 0px 0px 0px 0px;" >
        <table cellpadding="0" cellspacing="0" border="1" width="100%" >
        <tr>
            <td style="text-align: left;">
            <table width="100%" cellspacing="0" cellpadding ="0" style="font-family:Arial;font-size:12px;" >
                    <tr>
                    <td><asp:CheckBox ID="chk_Owner" AutoPostBack="true" Text="Select All" runat="server" OnCheckedChanged="Chk_Owner_CheckedChanged" />[ Owner ]</td>
                    <td ><asp:CheckBox ID="Chk_vessel" AutoPostBack="true" Text="Select All" runat="server" OnCheckedChanged="Chk_vessel_CheckedChanged" />[ Vessel ]</td>
                    <td ><asp:CheckBox ID="Chk_all" Text="Select All" AutoPostBack=true runat="server" OnCheckedChanged="Chk_all_CheckedChanged" />[ Rank ]</td>
                    <td>
                    <table cellpadding ="0" cellspacing ="0" width="100%" style =" text-align :center; height :20px;" >
                        <tr>
                    <td style="background-color : Yellow">Planned</td>
                    <td style="background-color : green;display:inline">Actual Schedule</td>
                    <td style="background-color : Coral;display:inline">Overdue</td>
                    <td style="background-color : cornflowerblue;display:inline">Projected</td></tr>
                    </table>
                    </td>
                    </tr>
                    <tr>
                    <td rowspan="3"><asp:ListBox ID="chkOwner" runat="server" CssClass="input_box" Width="150px" SelectionMode="Multiple" Height="75px" AutoPostBack="true" OnSelectedIndexChanged="OwnerSelected"></asp:ListBox></td>
                    <td rowspan="3"><asp:ListBox ID="chkvessel" runat="server" CssClass="input_box" Width="250px" SelectionMode="Multiple" Height="75px"></asp:ListBox></td>
                    <td rowspan="3"><asp:ListBox ID="chkrank" runat="server" CssClass="input_box" Width="150px" SelectionMode="Multiple" Height="75px"></asp:ListBox></td>
                    <td style ="text-align :left ;font-weight :bold;">Year : <%=CurYear.ToString()%></td>
                    </tr>
                    <tr>
                        <td style ="width:450px;">
                        <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn" Width="60px" OnClick="Button2_Click" /> &nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Print By Vessel" CssClass="btn" Width="100px" OnClientClick="return Print_ByVessel();" /> &nbsp;
                        <asp:Button ID="Button3" runat="server" Text="Print By Rank" CssClass="btn" Width="100px" OnClientClick="return Print_ByRank();" />
                        </td>
                    </tr>
                    <tr>
                        <td  style =" padding-bottom:3px; ">
                            <table style="font-family:Arial;font-size:12px;" >
                                <tr>
                                    <td style="text-align:right;width:75px;">
                                        Sort by :
                                    </td>
                                    <td style="text-align:left;width:200px;">
                                          <asp:DropDownList runat="server" ID="ddlSort" OnSelectedIndexChanged="Sorting_Changed" CssClass="input_box" >
                        <asp:ListItem Text="VesselName" Value="VesselName"></asp:ListItem>
                        <asp:ListItem Text="CrewNumber" Value="CrewNumber"></asp:ListItem>
                        <asp:ListItem Text="CrewName" Value="CrewName"></asp:ListItem>
                        <asp:ListItem Text="RankName" Value="RankName"></asp:ListItem>
                        <asp:ListItem Text="Nationality" Value="Nationality"></asp:ListItem>
                        <asp:ListItem Text="SignOnDate" Value="SignOnDate"></asp:ListItem>
                        <asp:ListItem Text="SignOffDate" Value="SignOffDate"></asp:ListItem>
                        </asp:DropDownList> 
                                    </td>
                                    <td style="text-align:left;width:75px;">
                                        <asp:Label Font-Bold="true" runat="server" id="lblCount" ></asp:Label>
                                    </td>
                                </tr>
                               
                            </table>
                       
                       </td> 
                    </tr>
            </table>
            <div style="height:384px; overflow-y:scroll;overflow-x:hidden"  >
            <table width="98%" border="1" cellspacing="0" >
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :left" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <tr class= "headerstylegrid" >
            <th>Vessel</th>
            <th>Emp#</th>
            <th>Name</th>
            <th>Rank</th>
            <th>Nationality</th>
            <th>SignOnDate</th>
            <th>Rel.Due.Date</th>
            <th >Jan</th>
            <th >Feb</th>
            <th >Mar</th>
            <th >Apr</th>
            <th >May</th>
            <th >Jun</th>
            <th >Jul</th>
            <th >Aug</th>
            <th >Sep</th>
            <th >Oct</th>
            <th >Nov</th>
            <th >Dec</th>
            </tr>
            <asp:Repeater runat="server" ID="rptData">
            <ItemTemplate>
            <tr class="data">
            <td><%#Eval("VesselName")%></td>
            <td><%#Eval("CrewNumber")%></td>
            <td><%#Eval("CrewName")%></td>
            <td><%#Eval("RankName")%></td>
            <td><%#Eval("Nationality")%></td> <!-- Nationality Code -->
            <td><%#FormatDate(Eval("SignOnDate"))%></td> <!-- Nationality Code -->
            <td <%#getPlanned(Eval("Relievers").ToString())%> title='<%#Eval("Details").ToString().Replace("<br/>", "\n")%>'><%#FormatDate(Eval("SignOffDate"))%></td> <!-- Nationality Code -->
            <td style='background-color :<%#Eval("Mon1")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon2")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon3")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon4")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon5")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon6")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon7")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon8")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon9")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon10")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon11")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style='background-color :<%#Eval("Mon12")%>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            
            </tr>
            </ItemTemplate> 
            </asp:Repeater>
            <tr style=" background-color : Gray ; height :20px; color :White  " >
            <th>Total</th>
            <th></th>
            <th></th>
            <th></th>
            <th></th>
            <th></th>
            <th></th>
            <th ><%=MonthSum[0].ToString()%></th>
            <th ><%=MonthSum[1].ToString()%></th>
            <th ><%=MonthSum[2].ToString()%></th>
            <th ><%=MonthSum[3].ToString()%></th>
            <th ><%=MonthSum[4].ToString()%></th>
            <th ><%=MonthSum[5].ToString()%></th>
            <th ><%=MonthSum[6].ToString()%></th>
            <th ><%=MonthSum[7].ToString()%></th>
            <th ><%=MonthSum[8].ToString()%></th>
            <th ><%=MonthSum[9].ToString()%></th>
            <th ><%=MonthSum[10].ToString()%></th>
            <th ><%=MonthSum[11].ToString()%></th>
            </tr>
            </table>
            </div>
        </tr>
        </table>
</td>
</tr>
</table>
        