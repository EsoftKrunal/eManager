<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alerts.aspx.cs" Inherits="emtm_Emtm_Alerts" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emtm - Alerts</title>
    <style type="text/css">
    .od
    {
        background-color:#FFC2B2;
    }
    </style>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
    <div>
    <div >
    <div style="text-align:center; padding:10px; background-color:#4371A5; font-size:18px; color:White;">
    <asp:Label runat="server" ID="lblAlertType"></asp:Label>
    </div>
    <div style="padding:5px; background-color:#dddddd; font-size:15px;">
      <div style="float:left; width:30%;" >
       <div id="dv_Fleet" runat="server" visible="false" style="float:left;"><b>Fleet : </b>&nbsp;<asp:DropDownList ID="ddlFleet" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged" runat="server" Width="100px"></asp:DropDownList></div>
       <div style="float:right;" runat="server" id="dv_Vessel">
        <b>Vessel : </b>&nbsp;
        <asp:DropDownList ID="ddlVessel" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged" runat="server"></asp:DropDownList>
        </div>
        <div id="dv_Office" runat="server" visible="false" style="float:left;"><b>Office : </b>&nbsp;<asp:DropDownList ID="ddlOffice" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" runat="server" Width="150px"></asp:DropDownList></div>
        <div style="clear:both;"></div>
        </div>
        <div style="float:right;"><asp:Label ID="lblRecCount" ForeColor="Blue" runat="server"></asp:Label> </div>
        <div style="clear:both;"></div>
    </div>
    </div>
    <div id="dv_MWUC" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:200px;" />
            <col style="width:200px;" />
            <col />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Received Date</td>
                <td>Remarks</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:200px;" />
            <col style="width:200px;" />
            <col />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlerts_MWUC" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("DATE_FIELD"))%>'>
                    <td align="left"><%#Eval("VESSELNAME")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DATE_FIELD"))%></td>
                    <td align="left">Office comment not entered.</td>
                    <td align="left">&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>
    <div id="dv_Followup" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:200px;" />
            <col />
            <col style="width:200px;" />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Description</td>
                <td>Target Closure Date</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:200px;" />
            <col />
            <col style="width:200px;" />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlerts_Fup" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("DATE_FIELD"))%>'>
                    <td align="left"><%#Eval("VESSELNAME")%></td>
                    <td align="left"><%#Eval("FollowupText")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DATE_FIELD"))%></td>
                    <td >&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>
    <div id="dv_Motor" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:200px;" />
            <col style="width:200px;" />
            <col style="width:110px;" />
            <col />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Motor #</td>
                <td>Due Date</td>
                <td>Remarks</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            
            <col style="width:200px;" />
            <col style="width:200px;" />
            <col style="width:110px;" />
            <col />
            <col style="width:30px;" /> 
            <asp:Repeater ID="rptAlerts_Motor" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("Due Date"))%>'>
                    <td align="left"><%#Eval("VESSELNAME")%></td>
                    <td align="left"><%#Eval("REFNUMBER")%></td>
                    <td align="left"><%#Eval("Due Date")%></td>
                    <td align="left">Motor report Incomplete.</td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div> 
    <div id="dv_Defects" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            
            <col style="width:200px;" />
            <col style="width:100px;" />
            <col />
            <col style="width:110px;" />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Defect #</td>
                <td>Description</td>
                <td>Due Date</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:200px;" />
            <col style="width:100px;" />
            <col />
            <col style="width:110px;" />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlert_Defects" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("DATE_FIELD"))%>'>
                    <td align="left"><%#Eval("VESSELNAME")%></td>
                    <td align="left"><%#Eval("REFNUMBER")%></td>
                    <td align="left"><%#Eval("Defects")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DATE_FIELD"))%></td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>                    
    <div id="dv_DrillTraining" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:200px;" />
            <col style="width:100px;" />
            <col />
            <col style="width:100px;" />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Type</td>
                <td>Drill/ Training Name</td>
                <td>Due Date</td>
                <td>&nbsp;</td>
            </tr>
          </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:200px;" />
            <col style="width:100px;" />
            <col />
            <col style="width:100px;" />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlerts_DT" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("DATE_FIELD"))%>'>
                    <td align="left"><%#Eval("VESSELNAME")%></td>
                    <td align="left"><%#(Eval("TYPE").ToString() == "D" ? "Drill" : "Training")%></td>
                    <td align="left"><%#Eval("DRILLNAME")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DATE_FIELD"))%></td>
                    <td>&nbsp;</td>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </div>
    <div id="dv_VI" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col />
            <col style="width:200px;" />
            <col style="width:100px;" />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Inspection #</td>
                <td>Validity Date</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col />
            <col style="width:200px;" />
            <col style="width:100px;" />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlerts_VI" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("DATE_FIELD"))%>'>
                    <td align="left"><%#Eval("VESSELNAME")%></td>                    
                    <td align="left"><%#Eval("REFNUMBER")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DATE_FIELD"))%></td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div> 
    <div id="dv_PVI" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:180px;" />
            <col style="width:120px;" />
            <col style="width:70px;" />
            <col style="width:170px;" />
            <col />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Inspection #</td>
                <td>Plan Date</td>
                <td>Superintendent Name</td>
                <td>Plan Remarks</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:180px;" />
            <col style="width:120px;" />
            <col style="width:70px;" />
            <col style="width:170px;" />
            <col />
            <col style="width:30px;" />  
            <asp:Repeater ID="rptAlerts_PVI" runat="server" >
            <ItemTemplate>
                <tr>
                    <td align="left"><%#Eval("VESSELNAME")%></td>                    
                    <td align="left"><%#Eval("REFNUMBER")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DATE_FIELD"))%></td>
                    <td align="left"><%#Eval("SupName")%></td>
                    <td align="left"><%#Eval("PlanRemark")%></td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>                    
    <div id="dv_6" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col />
            <col style="width:100px;" />
            <col style="width:150px;" />
            <col style="width:100px;" />
            <col style="width:100px;" />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Vessel</td> 
                <td>Sire_Cdi</td>
                <td>Last Insp. Name</td>
                <td>Last Done</td>
                <td>Next Due Date</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col />
            <col style="width:100px;" />
            <col style="width:150px;" />
            <col style="width:100px;" />
            <col style="width:100px;" />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlerts_6" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("NEXTDUE"))%>'>
                    <td align="left"><%#Eval("VNAME2")%></td>                    
                    <td align="left"><%#Eval("SIRE_CDI")%></td>
                    <td align="left"><%#Eval("LASTINSPNAME")%></td>
                    <td align="left"><%#Eval("LASTDONE")%></td>
                    <td align="left"><%#Eval("NEXTDUE")%></td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div> 
    <div id="dv_Emtm_LR" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:60px;" />
            <col style="width:250px;" />
            <col style="width:70px;" />
            <col style="width:70px;" />
            <col style="width:200px;" />
            <col style="width:80px;" />
            <col style="width:70px;" />
            <col />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Emp Code</td> 
                <td>Emp Name</td>
                <td>Leave From</td>
                <td>Leave To</td>
                <td>Forwarded To</td>
                <td>Forwarded On</td>
                <td>Office</td>
                <td>Reason</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:60px;" />
            <col style="width:250px;" />
            <col style="width:70px;" />
            <col style="width:70px;" />
            <col style="width:200px;" />
            <col style="width:80px;" />
            <col style="width:70px;" />
            <col />
            <col style="width:30px;" />
            <asp:Repeater ID="rptAlerts_LR" runat="server" >
            <ItemTemplate>
                <tr>
                    <td align="left"><%#Eval("EmpCode")%></td>                    
                    <td align="left"><%#Eval("EmployeeName")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("LeaveFrom"))%></td>
                    <td align="left"><%#Common.ToDateString(Eval("LeaveTo"))%></td>
                    <td align="left"><%#Eval("ForwardedTo")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("ForwardedOn"))%></td>
                    <td align="left"><%#Eval("Office")%></td>
                    <td align="left"><%#Eval("Reason")%></td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>                    
    <div id="dv_15" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:120px;" />
            <col style="width:70px;" />
            <col style="width:70px;" />
            <col style="width:150px;" />
            <col />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Company Code</td> 
                <td>Month</td>
                <td>Year</td>
                <td>Budget Published On</td>
                <td>Remarks</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:120px;" />
            <col style="width:70px;" />
            <col style="width:70px;" />
            <col style="width:150px;" />
            <col />
            <col style="width:30px;" />  
            <asp:Repeater ID="rptAlerts_15" runat="server" >
            <ItemTemplate>
                <tr class='<%#GetClass(Eval("NextDate"))%>'>
                    <td align="left"><%#Eval("CoCode")%></td>                    
                    <td align="left"><%#Eval("Month")%></td>
                    <td align="left"><%#Eval("year")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("SendOn"))%></td>
                    <td align="left">Budget variance is published for this company. Comment is due by <%#Common.ToDateString(Eval("NextDate"))%></td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>                    
    <div id="dv_16" runat="server" visible="false">
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
         <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; background-color:#555555; color:White;height:26px;">
            <col style="width:120px;" />
            <col style="width:70px;" />
            <col style="width:70px;" />
            <col style="width:150px;" />
            <col />
            <col style="width:30px;" />                   
            <tr align="left" class="blueheader">
                <td>Company Code</td> 
                <td>Month</td>
                <td>Year</td>
                <td>Budget Published On</td>
                <td>Remarks</td>
                <td>&nbsp;</td>
            </tr>
    </table> 
    </div>
    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px ; text-align:center;">
           <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <col style="width:120px;" />
            <col style="width:70px;" />
            <col style="width:70px;" />
            <col style="width:150px;" />
            <col />
            <col style="width:30px;" />  
            <asp:Repeater ID="rptAlerts_16" runat="server" >
            <ItemTemplate>
                <tr >
                    <td align="left"><%#Eval("CoCode")%></td>                    
                    <td align="left"><%#Eval("Month")%></td>
                    <td align="left"><%#Eval("year")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("SendOn"))%></td>
                    <td align="left">Budget comment is entered for this comppay. Publish is Due.</td>
                    <td>&nbsp;</td>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>                    

    <%--<asp:GridView ID="gvAlerts" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" ForeColor="Black" GridLines="Vertical" Width="100%">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>--%>
    </div>
    </form>
</body>
</html>
