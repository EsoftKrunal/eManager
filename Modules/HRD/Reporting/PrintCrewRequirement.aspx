<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCrewRequirement.aspx.cs" Inherits="Reporting_PrintCrewRequirement" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style>
    *
    {
    	font-family :Verdana ;
    	font-size :11px;
    }
    </style> 
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="dv_Rank" visible="false">
    <table width="98%" border="1" cellspacing="0" >
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
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <col style ="text-align :center" />
            <tr style=" background-color : Gray ; height :20px; color :White;" >
            <th>S.No.</th>
            <th>Rank</th>
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
            <th >Total</th>
            </tr>
            <asp:Repeater runat="server" ID="rptByRank">
            <ItemTemplate>
            <tr class="data">
            <td><%#Eval("SNo")%></td>
            <td><%#Eval("Rank")%></td>
            <td ><%#Eval("Mon1")%></td>
            <td ><%#Eval("Mon2")%></td>
            <td ><%#Eval("Mon3")%></td>
            <td ><%#Eval("Mon4")%></td>
            <td ><%#Eval("Mon5")%></td>
            <td ><%#Eval("Mon6")%></td>
            <td ><%#Eval("Mon7")%></td>
            <td ><%#Eval("Mon8")%></td>
            <td ><%#Eval("Mon9")%></td>
            <td ><%#Eval("Mon10")%></td>
            <td ><%#Eval("Mon11")%></td>
            <td ><%#Eval("Mon12")%></td>
            <td ><%#Eval("Total")%></td>
            </tr>
            </ItemTemplate> 
            </asp:Repeater>
            <tr style=" background-color : Gray ; height :20px; color :White;" >
            <th>Total</th>
            <th>&nbsp;</th>
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
            <th ><%=MonthSum[12].ToString()%></th>
            </tr>
            </table>
    </div>
    <div runat="server" id="dv_Vessel" visible="false">
    <asp:Repeater runat="server" ID="rptVessels" OnItemDataBound="ItemDataBound">
        <ItemTemplate >
            <span style="  font-weight : bold; font-size :12px; " >
            <br />
            Vessel : <asp:Label runat="server" ID="lblVSL" Text="<%#Container.DataItem.ToString()%>"></asp:Label> 
            </span>
            <br /><br />
            <table width="98%" border="1" cellspacing="0" >
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :left" />
            <col style ="text-align :center; width:100px; " />
            <col style ="text-align :center; width:100px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <col style ="text-align :center; width:50px; " />
            <tr style=" background-color : Gray ; height :20px; color :White; position :relative ;top:-3px; " >
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
            <asp:Repeater runat="server" ID="rptByVessel">
            <ItemTemplate>
            <tr class="data">
            <td><%#Eval("CrewNumber")%></td>
            <td><%#Eval("CrewName")%></td>
            <td><%#Eval("RankName")%></td>
            <td><%#Eval("Nationality")%></td> <!-- Nationality Code -->
            <td><%#Eval("SignOnDate")%></td> <!-- Nationality Code -->
            <td <%#getPlanned(Eval("Relievers").ToString())%> title='<%#Eval("Details").ToString().Replace("<br/>", "\n")%>'><%#Eval("SignOffDate")%></td> <!-- Nationality Code -->
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
            <th ><asp:Label runat="server" ID="lblSum1" Text="<%=MonthSum[0].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum2" Text="<%=MonthSum[1].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum3" Text="<%=MonthSum[2].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum4" Text="<%=MonthSum[3].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum5" Text="<%=MonthSum[4].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum6" Text="<%=MonthSum[5].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum7" Text="<%=MonthSum[6].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum8" Text="<%=MonthSum[7].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum9" Text="<%=MonthSum[8].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum10" Text="<%=MonthSum[9].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum11" Text="<%=MonthSum[10].ToString()%>"></asp:Label></th>
            <th ><asp:Label runat="server" ID="lblSum12" Text="<%=MonthSum[11].ToString()%>"></asp:Label></th>
            </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    </div> 
    </form>
</body>
</html>
