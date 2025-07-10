<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ManageTickets.aspx.cs" Inherits="CrewOperation_ManageTickets" Title="EMANAGER" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>EMANAGER</title>
<link href="../../../css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
   <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="#4371a5" style="font-family:Arial;">
    <tr>
        <td  class="text headerband" colspan="2" >
            <strong> Crew Travel - Ticket Management </strong>
        </td>
    </tr>
    <tr>
    <td>
    <div style="padding:5px; background-color:Wheat;  ">
    <table width='100%' cellpadding="3" cellspacing="0" border="1" rules="all" style=' border-collapse:collapse; background-color:	#EBF0FF' bordercolor='Gray' >
    <tr style='font-size:13px; font-weight:bold;'>
    <td>From Date</td>
    <td>To Date</td>
    <td>Vessel</td>
    <td>Crew #</td>
    <td>PNR #</td>
    <td>Status</td>
    <td>Travel Agent</td>
    <td>&nbsp;</td>
    </tr>
    <tr>
    <td>
        <asp:TextBox runat="server" ID="txtFDate" Width='80px' style='text-align:center' CssClass="input_box"></asp:TextBox>
    </td>
    <td>
      <asp:TextBox runat="server" ID="txtTDate" Width='80px' style='text-align:center' CssClass="input_box"></asp:TextBox>
      </td>
    <td>
      <asp:DropDownList runat="server" ID="ddlVessel" CssClass='input_box' Width="160px"></asp:DropDownList>
        </td>
    <td>
      <asp:TextBox runat="server" ID="txtCrewNo" Width="50px" MaxLength="6"  style='text-align:center' CssClass="input_box"></asp:TextBox>
        </td>
    <td>
      <asp:TextBox runat="server" ID="txtPNRNo" Width="100px" MaxLength="50"  style='text-align:center' CssClass="input_box"></asp:TextBox>
        </td>
    <td>
      <asp:DropDownList runat="server" ID="ddlStatus" CssClass='input_box' Width="100px">
        <asp:ListItem Text="< ALL >" Value=""></asp:ListItem>
        <asp:ListItem Text=" Issued " Value="A"></asp:ListItem>
        <asp:ListItem Text=" Changed " Value="C"></asp:ListItem>
        <asp:ListItem Text=" Cancelled " Value="D"></asp:ListItem>
      </asp:DropDownList>
        </td>
    <td>
      <asp:DropDownList runat="server" ID="ddlTA" CssClass='input_box' Width="250px" ></asp:DropDownList>

        </td>
    <td style='width:200px; text-align:center '>
        <asp:Button runat="server" ID="bthShow" Text="Show" style="padding:5px;" CssClass="btn" OnClick="btnShow_Click" />&nbsp;
    </td>
    </tr>
    </table>

    <ajaxToolkit:CalendarExtender runat="server" ID="c1" TargetControlID="txtFDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtTDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
 
    </div>
    <table cellspacing="0" cellpadding="2" border="1" id="Table1" style="width:100%;border-collapse:collapse;">
		<tr class= "headerstylegrid">
            <th scope="col" style='width:80px'>Booking Dt.</th>
            <th scope="col" style='width:80px'>INV#</th>
            <th scope="col" style=''>Crew Name</th>
            <th scope="col" style='width:50px'>No(s) Tickts</th>
		    <th scope="col" style='width:50px'>Rank</th>
            <th scope="col" style='width:50px'>Vessel</th>
            <th scope="col" style='width:200px'>Origin > Destination </th>
            <th scope="col" style='width:80px'>Dep. Dt.</th>
            <th scope="col" style='width:70px'>PNR#</th>  
            <th scope="col" style='width:70px'>Status</th>
            <th scope="col" style='width:40px'>CUR</th>
            <th scope="col" style='width:60px'>Ticket Amt.</th>
            <th scope="col" style='width:60px'>Change Amt.</th>
            <th scope="col" style='width:60px'>Cancel Amt.</th>
            <th scope="col" style='width:60px'>Refund Amt.</th>
            <th scope="col" style='width:60px'>Total Amt.</th>
            <th scope="col" style='width:50px'>Action</th>
	    </tr>
    </table>
    <div style="overflow-x:hidden; overflow-y:scroll; height:400px">
    <table cellspacing="0" cellpadding="2" border="1" id="Table2" style="width:100%;border-collapse:collapse;">
        <asp:Repeater runat="server" ID="rpt_Data">
        <ItemTemplate>
        <tr style='<%# (Eval("CrewFlag").ToString()=="I")?"background-color:#99FFCC":"background-color:#FFCCCC" %>'>
            <td scope="col" style='width:80px'><%#Common.ToDateString(Eval("BookingDate"))%></td>
            <td scope="col" style='width:80px;text-align:center;'><%#Eval("INVNO")%>
            </td>
            <td scope="col" style='text-align:left;'><%# Eval("CrewName") %>
                <asp:HiddenField ID="HiddencrewIdsignoff" runat="server" Value='<%#Eval("CrewId")%>' />
                <asp:HiddenField ID="HfdCrewFlag" runat="server" Value='<%#Eval("CrewFlag")%>' />
            </td>
            
            <td scope="col" style='width:50px'><%# Eval("NO_TICS")%></td>
		    <td scope="col" style='width:50px'><%# Eval("RankName") %></td>
            <td scope="col" style='width:50px'><%# Eval("VesselCode") %></td>
            <td scope="col" style='width:200px; text-align:left;'><%#Eval("FromAirport")%> > <%#Eval("ToAirport")%></td>
            <td scope="col" style='width:80px'><%#Common.ToDateString(Eval("DeptDate"))%></td>
            <td scope="col" style='width:70px'><%#Eval("PNR")%></td>  
            <td scope="col" style='width:70px'><%#(Eval("TicketStatus").ToString() == "A") ? "<div style='background-color:green;padding:2px;color:white;'>Issued</div>" : ((Eval("TicketStatus").ToString() == "C") ? "<div style='background-color:yellow;padding:2px;'>Changed</div>" : "<div style='background-color:red;padding:2px;color:white;'>Cancelled</div>")%></td>
            <td scope="col" style='width:40px'><%#Eval("Currency")%></td>
            <td scope="col" style='width:60px; text-align:right;'><%#Eval("Booking_LC")%>&nbsp;</td>
            <td scope="col" style='width:60px; text-align:right;'><%#Eval("Change_LC")%>&nbsp;</td>
            <td scope="col" style='width:60px; text-align:right;'><%#Eval("Cancellation_LC")%>&nbsp;</td>
            <td scope="col" style='width:60px; text-align:right;'><%#Eval("Refund_LC")%>&nbsp;</td>
            <td scope="col" style='width:60px; text-align:right;'><%#Eval("Final_LC")%>&nbsp;</td>
            <td scope="col" style='width:50px; text-align:left;'>
            <asp:ImageButton runat="server" ID="btn_Show"  ImageUrl="~/Modules/HRD/Images/magnifier.png" CommandArgument='<%#Eval("CrewTravelId").ToString()%>' OnClick="btnView_OnClick" />
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    </td>
    </tr>
    </table>

    <div style="position:absolute;top:0px;left:0px; height :670px; width:100%;" runat="server" id="dvDetails" visible="false">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :650px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:800px; height:550px;padding :3px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px gray;">
         <center >
            <iframe runat="server" id="frmDetails" width="100%" frameborder="no" height="523"></iframe>
            <div>
            <div style="float:right">
            <asp:Button runat="server" id="btnClose" CssClass="btn" Text="Close" OnClick="btn_Close_Click" />
            </div>
            </div>
         </center>
         </div>
    </center>
    </div> 
    

    <div>
    <asp:Label runat="server" ID="lblMsg" Text="" ForeColor="Red" style="float:right" Font-Size="15px" ></asp:Label>
    </div>
    </form>
</body>
</html>


