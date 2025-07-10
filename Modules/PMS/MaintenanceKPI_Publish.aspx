<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceKPI_Publish.aspx.cs" Inherits="Reports_MaintenanceKPI_Publish" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
      <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="./CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="./JS/Calender.js" type="text/javascript"></script>
    <script src="./JS/Common.js" type="text/javascript"></script>
    <style type="text/css">
        td{
            vertical-align:middle;
        }
        select{
            line-height:25px;
            height:25px;
            font-size:13px;
        }
        .bordered tr td{
            border:solid 1px #e5dddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="position:fixed;top:0px;left:0px;width:100%;z-index:2;">
    <div style="text-align :center; padding-top :8px;" class="text headerband" visible="false" >
          <b>Maintenance KPI</b>
     </div>
    <div style="padding:0px; background-color:#e5dddd">
        <table border="0" cellpadding="5" cellspacing="0" style="text-align:center">
        <tr>
            <td  style="text-align: right; padding-left :20px;width:150px;">
    <span id="Span1" runat="server">
    <b>Select Fleet :</b>&nbsp;
    </span>
    </td>  
            <td style="text-align: left;width:200px;">
                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" ></asp:DropDownList>
            </td>
            <td style="text-align: right; padding-left :20px;width:150px;">
                 <span id="tdVessel" runat="server">
    <b>Select Vessel :</b>&nbsp;  </span>
            </td>
    <td style="text-align: left;width:200px;">
   <asp:DropDownList ID="ddlVessels" runat="server" ></asp:DropDownList>
   
    </td>             
    <td style="text-align :right;width:150px;"><b>Month & Year :</b>&nbsp;</td>
    <td style="text-align: left;width:200px;">
       <asp:Label runat="server" ID="lblpubmonyear"></asp:Label>
    </td>
    <td>
        <asp:Button ID="btnViewReport" CssClass="btn" Text="Show" runat="server" Width="100px" onclick="btnViewReport_Click" /> &nbsp;
        <asp:Button ID="Button1" CssClass="btn" Text="Open Report" runat="server" Width="100px" onclick="btOpenReport_Click" />
    </td>
</tr>
        </table>
    </div>
          <table border="0" cellpadding="5" cellspacing="0" style="width:100%;border-collapse:collapse; height:60px;" class="bordered">
                 <tr align="left" class="PageHeader" style="font-size:11px;padding:3px;font-weight:bold;">
                   <td style="text-align:left;">&nbsp;</td>
                   <td colspan="5"><asp:Label runat="server" ID="lbllastmonth"></asp:Label></td>
                   <td colspan="2"><asp:Label runat="server" ID="lblcurrmonth"></asp:Label></td>
                     <td>&nbsp;</td>
               </tr> 
               <tr align="left" class="PageHeader" style="font-size:11px;padding:3px;font-weight:bold;">
                   <td style="text-align:left;">Vessel</td>
                   <td style="width:100px">Due </td>
                   <td style="width:100px">Over Due </td>
                    <td style="width:100px">Total Jobs</td>
                  <%-- <td style="width:120px">Postponed Other </td>
                   <td style="width:140px">Postponed Docking  </td> --%>                 
                   <td style="width:130px">Outstanding Jobs</td>
                   <td style="width:80px">KPI (%)</td>
                   <td style="width:100px">Due</td>
                   <td style="width:100px">Over Due </td>
                   <td style="width:100px">Publish</td>
               </tr>
            </table>
    </div>
        <div style="position:absolute;top:123px;left:0px;width:100%;z-index:1;">         
               <table border="0" cellpadding="5" cellspacing="0"  style="width:100%;border-collapse:collapse;" class="bordered">
                  
               <asp:Repeater ID="rptMaintenanceKPIData" runat="server">
                   <ItemTemplate>
                       <tr>
                           <td ><%#Eval("VesselName")%></td>
                           <td style="text-align:right;width:100px"><%#Eval("DueJobs") %> </td>
                           <td style="text-align:right;width:100px"><%#Eval("OverDueJobs") %> </td>
                           <td style="text-align:right;width:100px"><%#Eval("TotalJobs") %> </td>
                           <%--<td style="text-align:right;width:120px"><%#Eval("POSTPONEOTHER") %> </td>
                           <td style="text-align:right;width:140px"><%#Eval("POSTPONEdocking") %> </td>  --%>                         
                           <td style="text-align:right;width:130px"><%#Eval("Outstanding") %> </td>
                           <td style="text-align:right;width:80px"><%#Common.CastAsDecimal( Eval("OutstandingPercent")).ToString("0.00") %> % </td>          
                           <td style="text-align:right;width:100px"><%#Eval("DUENEXTMONTH") %> </td>
                           <td style="text-align:right;width:100px"><%#Eval("OVERDUENEXTMONTH") %> </td>  
                            <td style="text-align:center;width:100px">
                                <asp:HiddenField runat="server" ID="hfdvsl" Value='<%#Eval("VesselCode") %>'/>
                                <asp:HiddenField runat="server" ID="hfdmonth" Value='<%#Eval("Month") %>' />
                                <asp:HiddenField runat="server" ID="hfdyear" Value='<%#Eval("Year") %>' />

                                <asp:HiddenField runat="server" ID="hfdDue"  Value='<%#Eval("DueJobs") %>'/>
                                <asp:HiddenField runat="server" ID="hfdOverDue"  Value='<%#Eval("OverDueJobs") %>'/>
                                <asp:HiddenField runat="server" ID="hfdTotal"  Value='<%#Eval("TotalJobs") %>'/>
                                <asp:HiddenField runat="server" ID="hfdPP" Value='<%#Eval("POSTPONEOTHER") %>' />
                                <asp:HiddenField runat="server" ID="hfdPPD"  Value='<%#Eval("POSTPONEdocking") %>'/>
                                <asp:HiddenField runat="server" ID="hfdOut"  Value='<%#Eval("Outstanding") %>'/>
                                <asp:HiddenField runat="server" ID="hfdDueNext"  Value='<%#Eval("DUENEXTMONTH") %>'/>
                                <asp:HiddenField runat="server" ID="hfdODueNext"  Value='<%#Eval("OVERDUENEXTMONTH") %>'/>
                               <asp:Button runat="server" ID="btnPublish" OnClick="btnPublish_Click" Text="Publish"  OnClientClick="return confirm('Are you sure to publish this record.');" style='<%# "display:" + Eval("PublishDisplay")%>'/>
                           </td>               
                       </tr>
                   </ItemTemplate>
               </asp:Repeater>
           </table>
          </div>
   </div>
    



    </form>
</body>
</html>
