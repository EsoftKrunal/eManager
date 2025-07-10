<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionReport.aspx.cs" Inherits="PositionReport_PositionReport" Title="EMANAGER" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>EMANAGER</title>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript">
        function OpenDetails(obj) {
            window.open('Details.aspx?VSLCode=' + obj.getAttribute('value') + '&VSLName=' + obj.getAttribute('title'));
        }
        function OpenDetails2(VSlCode, VSLName, VoyNo) {
            window.open('PositionReport.aspx?VSLCode=' + VSlCode + '&VSL=' + VSLName);
        }
        function ShowDetailsPopup(ctl, RepType) {
            window.open('Details2.aspx?ReportType=' + RepType + '&ReportId=' + ctl.getAttribute('value') + '&VSLCode=' + ctl.getAttribute('vslvalue'));
            return false;
        }
        function ShowReport(ob) {
            window.open('../Reports/PositionReport.aspx?ReportId=' + ob.getAttribute('value') + "&VSLCode=" + obj.getAttribute('vslvalue'));
            return false;
        }  
    </script>
    <style type="text/css">
        .pink
        {
            background-color: #FFCCE0;
            color: black;
        }
        .btn1
        {
            border: none;
            border-bottom: none;
            background-color: #B2D1FF;
            padding: 5px;
        }
    </style>
    </head>
  <body>
     <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
    <div class="text headerband">
        Position Report
    </div>
    <br />
    <table width="100%" border="1" style="border: solid 1px #c2c2c2;text-align: center; border-collapse: collapse;" cellpadding="3" cellspacing="1">
        <tr style="font-size: 13px; background-color:#404040;">
            <td style="text-align: center; height: 25px;;color:White;">
                Fleet
            </td>
            <td style=" text-align: center;color:White;">
                Vessel
            </td>
            <td style="text-align: center;color:White;">
                Inactive Vessels
            </td>
            <td style="text-align: center;color:White;">
                View Report for
            </td>
            <td style="width: 250px; text-align: center;color:White;">
                <asp:LinkButton runat="server" CssClass="input_box" ForeColor="White" ID="bgnGoback" style="border:none" Text="< Go Back" PostBackUrl="~/Home.aspx" Visible="False" />
            </td>
            <td style="text-align: center;color:White;">
                Fleet EEOI
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" ID="ddlFleet" CssClass="input_box" Width="100%"></asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="Update_Grid" ID="ddlVessel" CssClass="input_box" Width="100%"></asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:CheckBox runat="server" ID="chk_Inactive" Text="Include Inactive Vessels" OnCheckedChanged="chk_Inactive_OnCheckedChanged"
                    AutoPostBack="true" />
            </td>
            <td style="text-align: center">
                <asp:TextBox runat="server" ID="txtDate" OnTextChanged="Update_Grid" MaxLength="15"
                    CssClass="input_box" Width="80px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif">
                </asp:ImageButton>
                
            </td>
            <td>
                <asp:Button runat="server" CssClass="btn" ID="btn_Show" Text="Show" OnClick="btn_Show_Click" Width="108px" />
                <asp:Button runat="server" CssClass="btn" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
            </td>
            <td style="text-align:center">
                <asp:Button runat="server" CssClass="btn" ID="btnEEOI" Text="Download EEOI" OnClick="btnDEEOI_Click" />
            </td>
        </tr>
    </table>
  
    <div style="overflow-x: hidden; overflow-y: scroll; width: 100%; height: 30px;">
        <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width: 100%;border-collapse: collapse; height: 30px">
            <colgroup>
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="150px" />
           <%-- <col width="60px" />--%>
            <col width="100px" />
            <col width="180px" />
            <col width="90px" />
            <col width="250px" />
            <col width="17px" />
                </colgroup>
            <tr class= "headerstylegrid">
                <th scope="col">
                    Vessel Name
                </th>
                <th scope="col">
                    Voyage #
                </th>
                <th scope="col">
                    Report Date
                </th>
                <th scope="col">
                    Report Type
                </th>
               <%-- <th scope="col">
                    Weather
                </th>--%>
                <th scope="col">
                    Wind Force
                </th>
                <th scope="col">
                    Location
                </th>
                <th scope="col">
                    View Report
                </th>
                <th scope="col">
                    Next Activity
                </th>
                <th scope='col'>
                    &nbsp;
                </th>
            </tr>
        </table>
    </div>


    <div style="height: 365px; overflow-x: hidden; overflow-y: scroll; width: 100%;">
        <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width: 100%;
            border-collapse: collapse; border: solid 1px #4371a5;">
            <colgroup>
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="150px" />
           <%-- <col width="60px" />--%>
            <col width="100px" />
            <col width="180px" />
            <col width="90px" />
            <col width="250px" />
            <col width="17px" />
                </colgroup>
            <asp:Repeater runat="server" ID="grd_Data">
                <ItemTemplate>
                    <tr class="<%# Eval("RowCls")%>" onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#FFFF66';"
                        onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td style="text-align: left;">
                            <%--<a href="PositionReport_Popup.aspx?CurrentShip=<%# Eval("VESSELCODE") %>&VslName=<%# Eval("VesselName")%>" > <%# Eval("VesselName")%> </a>--%>
                            <a href="PositionReport_Popup.aspx?CurrentShip=<%# Eval("VESSELCODE") %>&VslName=<%# Eval("VesselName")%>" target="_blank"  > <%# Eval("VesselName")%> </a>
                          </td>
                        <td style="text-align: left;">
                            <%# Eval("VoyageNo") %>
                        </td>
                        <td style="text-align: center;">
                            <%# Common.ToDateString(Eval("ReportDate"))%>
                        </td>
                        <td style="text-align: left;">
                            <%# Eval("ReportTypeName")%>
                        </td>
                      <%--  <td style="text-align: center;">
                            <center runat="server" Visible='<%#(Eval("ACTIVITY_CODE").ToString()!="") %>' >
                                <img src="../../HRD/Images/VslRep.png" onclick="window.open('../PositionReport/SeaIcon.aspx?Mode=Page&ReportType=<%#Eval("ACTIVITY_CODE") %>&VSLCode=<%#Eval("VESSELCODE") %>&ReportId=<%#Eval("REP_KEY") %>')"/>
                            </center>
                        </td>--%>
                        <td style="text-align: center;">
                            <%#Eval("WindForce")%>
                        </td>
                        <td style="text-align: left;">
                            &nbsp;<%#Eval("Lattitude1").ToString() + "&#176 " + Eval("Lattitude2").ToString() + "&#96 " + GetLattitude(Eval("Lattitude3")) + " - " + Eval("Longitude1").ToString() + "&#176 " + Eval("Longitud2").ToString() + "&#96 " + GetLongitude(Eval("Longitud3"))%></td>
                        <td style="text-align: center;">
                            <center runat="server" Visible='<%#(Eval("ACTIVITY_CODE").ToString()!="") %>' >
                                <img src="../../HRD/Images/magnifier.png" onclick="window.open('NoonReport.aspx?Key=<%#Eval("Rep_Key")%>&VesselCode=<%#Eval("VESSELCODE")%>')"/>
                            </center>
                        </td>
                        <td>
                            <%#GetNextActivity(Eval("Location"), Eval("ArrivalPortETA"),Eval("ArrivalPortETAHrs"),Eval("ArrivalPortETAMin"),Eval("DepArrivalPort"))%>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
     <div ID="dv_Down" runat="server" style="position: absolute; top: 50px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:400px; padding :0px; text-align :center;background : white; z-index:150;top:80px; border:solid 1px black;">
        <center >
           <div class="text headerband">
               Download EEOI
           </div>
                
           
            <table width="100%" cellpadding="5">
                <tr>
                    <td>Fleet :</td>
                    <td><asp:DropDownList runat="server" ID="ddlFleet1" CssClass="input_box" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet1_OnSelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                 <tr>
                    <td>Vessel :</td>
                    <td><asp:DropDownList runat="server" ID="ddlVessel1" CssClass="input_box" Width="100%"></asp:DropDownList></td>
                </tr>
                 <tr>
                    <td>Year :</td>
                    <td><asp:DropDownList runat="server" ID="ddlYear" CssClass="input_box" Width="100%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button runat="server" CssClass="btn" ID="Button1" Text="Download EEOI" OnClick="btnDEEOIDown_Click" />
                        <asp:Button runat="server" CssClass="btn" ID="Button2" Text="Close" OnClick="btnDEEOI_Close_Click" />
                    </td>
                </tr>
            </table>
            <br />
        </center>
        </div>
        </center>
    </div>
        </div>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txtDate" ></ajaxToolkit:CalendarExtender>
         </form>
     </body>
</html>
