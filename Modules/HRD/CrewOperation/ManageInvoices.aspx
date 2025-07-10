<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ManageInvoices.aspx.cs" Inherits="CrewOperation_ManageInvoices" Title="CMS : Crew Operation > Crew Travel ( Invoice Mangement ) " %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>CMS : Crew Operation > Crew Travel ( Ticket Mangement ) </title>
<link href="../../../css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script type="text/javascript">
    function SelectAll(ctl) {
        var chks = document.getElementById("d1").getElementsByTagName("input");
        for (i = 0; i <= chks.length - 1; i++) {
            chks[i].checked = ctl.checked;
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="#4371a5" style="font-family:Arial;">
    <tr>
        <td  class="text headerband" colspan="2" >
            <strong> Crew Travel - Invoice Management </strong>
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
      <asp:DropDownList runat="server" ID="ddlTA" CssClass='input_box' Width="250px" ></asp:DropDownList>

        </td>
    <td style='width:200px; text-align:center '>
        <asp:Button runat="server" ID="bthShow" Text="Show" style="padding:5px;" CssClass="btn" OnClick="btnShow_Click" />&nbsp;
        <asp:Button runat="server" ID="btnInv" Text="Rec Invoice" style="padding:5px;" CssClass="btn" OnClick="btnInv_Click" />
    </td>
    </tr>
    </table>

    <ajaxToolkit:CalendarExtender runat="server" ID="c1" TargetControlID="txtFDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtTDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
 
    </div>
    <table cellspacing="0" cellpadding="2" border="1" id="Table1" style="width:100%;border-collapse:collapse;">
		<tr class= "headerstylegrid">
            <th scope="col" style='width:80px'>INV#</th>
            <th scope="col" style='width:80px'>INV Dt.</th>
            <th scope="col" style='width:250px'>Vessel Name</th>
            <th scope="col" style=''>Travel Agent</th>
            <th scope="col" style='width:80px'>Recd. On</th>
            <th scope="col" style='width:100px'>Invoice Amt.</th>
            <th scope="col" style='width:50px'>Action</th>
	    </tr>
    </table>
    <div style="overflow-x:hidden; overflow-y:scroll; height:400px">
    <table cellspacing="0" cellpadding="2" border="1" id="Table2" style="width:100%;border-collapse:collapse;">
        <asp:Repeater runat="server" ID="rpt_Data">
        <ItemTemplate>
        <tr style="background-color:#F5FAFF">
            <td scope="col" style='width:80px;text-align:center;'><%#Eval("INVOICENO")%></td>
            <td scope="col" style='width:80px'><%#Common.ToDateString(Eval("InvoiceDate"))%></td>
            <td scope="col" style='width:250px; text-align:left'><%# Eval("VesselName") %></td>
            <td scope="col" style='; text-align:left'><%# Eval("Company") %></td>
            <td scope="col" style='width:80px'><%#Common.ToDateString(Eval("receivedon"))%></td>
            <td scope="col" style='width:100px; text-align:right;'><%#Eval("InvAmount")%>&nbsp;</td>
            <td scope="col" style='width:50px; text-align:left;'>
            <asp:ImageButton runat="server" ID="btn_Show"  ImageUrl="~/Modules/HRD/Images/magnifier.png" CommandArgument='<%#Eval("INVOICEID").ToString()%>' OnClick="btnView_OnClick" />
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    </td>
    </tr>
    </table>

    <div style="position:absolute;top:0px;left:0px; height :670px; width:100%;font-family:Arial;" runat="server" id="dvDetails" visible="false">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :650px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:1000px; height:550px;padding :3px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px gray;">
         <center >
            <div class="text headerband">
                <span >Receive New Invoice</span>
            </div>
         <table cellpadding="5" cellspacing="0" width="100%" style="text-align:left ; font-size:12px; border-collapse:collapse" border="1" bordercolor="wheat">
         <tr>
         <td style='text-align:right;width:150px' >Travel Agent : </td>
         <td style='text-align:left;width:280px'><asp:DropDownList runat="server" ID="ddlInvTravelAgents" CssClass="input_box" Width="250"></asp:DropDownList></td>
         <td style='text-align:right;width:200px'>Invoice #:</td>
         <td style='text-align:left;'><asp:TextBox runat="server" ID="txtINVNo" Width='80px' style='text-align:left' CssClass="input_box" MaxLength="50"></asp:TextBox></td>
         </tr>
         <tr>
         <td style='text-align:right;width:150px' >Invoice Date : </td>
         <td style='text-align:left;width:280px'><asp:TextBox runat="server" ID="txtInvDate" Width='80px' style='text-align:center' CssClass="input_box" MaxLength="15"></asp:TextBox>
             <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtInvDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
             </td>
         <td style='text-align:right;width:200px'>Invoice Amount :</td>
         <td style='text-align:left;'><asp:TextBox runat="server" ID="txtINVAmt" Width='80px' style='text-align:right' CssClass="input_box" MaxLength="50" ReadOnly="true"></asp:TextBox></td>
         </tr>
         </table>
         <div>
            <table cellspacing="0" cellpadding="2" border="1" id="Table3" style="width:100%;border-collapse:collapse;">
		<tr class= "headerstylegrid">
            
            <th scope="col" style='width:80px'>Booking Dt.</th>
            <th scope="col" style=''>Crew Name</th>
            <th scope="col" style='width:50px'>Rank</th>
            <th scope="col" style='width:50px'>Vessel</th>
            <th scope="col" style='width:200px'>Origin > Destination </th>
            <th scope="col" style='width:80px'>Dep. Dt.</th>
            <th scope="col" style='width:70px'>PNR#</th>  
            <th scope="col" style='width:70px'>Status</th>
            <th scope="col" style='width:40px'>CUR</th>
            <th scope="col" style='width:60px'>Total Amt.</th>
            <th scope="col" style='width:20px'>
            <input type="checkbox" ID="chkAll" onclick="SelectAll(this);" />
            </th>
            <th scope="col" style='width:60px'>Invoice Amt.</th>
            <th style='width:20px'>&nbsp;</th>
	    </tr>
    </table>
            <div style="overflow-x:hidden; overflow-y:scroll; height:400px" id="d1">
                <table cellspacing="0" cellpadding="2" border="1" id="Table5" style="width:100%;border-collapse:collapse;">
                <asp:Repeater runat="server" ID="Repeater2">
                <ItemTemplate>
                <tr style='<%# (Eval("CrewFlag").ToString()=="I")?"background-color:#99FFCC":"background-color:#FFCCCC" %>'>
                    
                    <td scope="col" style='width:80px'><%#Common.ToDateString(Eval("BookingDate"))%></td>
                    <td scope="col" style='text-align:left;'><%# Eval("CrewName") %>
                        <asp:HiddenField ID="HiddencrewIdsignoff" runat="server" Value='<%#Eval("CrewId")%>' />
                        <asp:HiddenField ID="HfdCrewFlag" runat="server" Value='<%#Eval("CrewFlag")%>' />
                    </td>
                    <td scope="col" style='width:50px'><%# Eval("RankName") %></td>
                    <td scope="col" style='width:50px'><%# Eval("VesselCode") %></td>
                    <td scope="col" style='width:200px; text-align:left;'><%#Eval("FromAirport")%> > <%#Eval("ToAirport")%></td>
                    <td scope="col" style='width:80px'><%#Common.ToDateString(Eval("DeptDate"))%></td>
                    <td scope="col" style='width:70px'><%#Eval("PNR")%></td>  
                    <td scope="col" style='width:70px'><%#(Eval("TicketStatus").ToString() == "A") ? "<div style='background-color:green;padding:2px;color:white;'>Issued</div>" : ((Eval("TicketStatus").ToString() == "C") ? "<div style='background-color:yellow;padding:2px;'>Changed</div>" : "<div style='background-color:red;padding:2px;color:white;'>Cancelled</div>")%></td>
                    <td scope="col" style='width:40px'><%#Eval("Currency")%></td>
                    <td scope="col" style='width:60px; text-align:right;'><%#Eval("Final_LC")%>&nbsp;</td>
                    <th scope="col" style='width:20px'><asp:CheckBox runat="server" id="chkSelect" /> </th>
                    <td scope="col" style='width:60px; text-align:right;'><asp:TextBox runat="server" Text='' MaxLength="15" CssClass='input_box' style='text-align:right;width:50px;' ></asp:TextBox></td>
                    <td style='width:20px'>&nbsp;</td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                </table>                                                                                                                                                                                                                                                    <table cellspacing="0" cellpadding="2" border="1" id="Table4" style="width:100%;border-collapse:collapse;">
        <asp:Repeater runat="server" ID="Repeater1">
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
         </div>       
        <div>
        <div style="float:right">
        <asp:Button runat="server" id="btnClose" CssClass="btn"  Text="Close" OnClick="btn_Close_Click" />
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


