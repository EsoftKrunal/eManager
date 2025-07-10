<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionReport_Popup.aspx.cs" Inherits="PositionReport_Popup"
    Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Position Report</title>
    <script type="text/javascript" src="../JQ_Scripts/jquery.min.js"></script>
    <script src="../js/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/KPIScript.js"></script>
    <link rel="stylesheet" href="../js/JS/AutoComplete/jquery-ui.css" />
    <script src="../js/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            margin: 0px;
            font-family: Helvetica;
            font-size: 14px;
            color: #2E5C8A;
            font-family: Calibri;
        }
        input
        {
            padding: 2px 3px 2px 3px;
            border: solid 1px #006B8F;
            color: black;
            font-family: Calibri;
            background-color: #E0F5FF;
        }
        textarea
        {
            padding: 0px 3px 0px 3px;
            border: solid 1px #006B8F;
            color: black;
            text-align: left;
            font-family: Calibri;
            background-color: #E0F5FF;
        }
        select
        {
            border: solid 1px #006B8F;
            padding: 0px 3px 0px 3px;
            height: 22px;
            vertical-align: middle;
            border: none;
            color: black;
            font-family: Calibri;
            background-color: #E0F5FF;
        }
        td
        {
            vertical-align: middle;
        }
        .unit
        {
            color: Blue;
            font-size: 13px;
            text-transform: uppercase;
        }
        .range
        {
            color: Red;
            font-size: 13px;
            text-transform: uppercase;
        }
        .stickyFooter
        {
            position: fixed;
            bottom: 0px;
            width: 100%;
            overflow: visible;
            z-index: 99;
            padding: 5px;
            background: white;
            border-top: solid black 2px;
            -webkit-box-shadow: 0px -5px 15px 0px #bfbfbf;
            box-shadow: 0px -5px 15px 0px #bfbfbf;
            vertical-align: middle;
            background-color: #FFFFCC;
        }
        .btn
        {
            border: none;
            color: #ffffff;
            background-color: #B870FF;
            padding: 4px;
        }
        .btn:hover
        {
            background-color: #006B8F;
            color: White;
        }
        .bordered tr td
        {
            border:solid 1px #f2f2f2;font-size:12px;
        }

        .btn1
        {
            border: none;
            border-bottom: none;
            background-color: #B2D1FF;
            padding: 5px;
            color:#221e1e;            
        }
        .btn1-sel
        {
            border: none;
            border-bottom: none;
            background-color: #3385FF;
            padding: 5px;
            color:white;            
        }
        .menu-container {
            padding:0;
            border-bottom:solid 3px #3385FF;
            margin-bottom:2px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
</head>
<body>
    <form id="form" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    

    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color: #F4F4F4; color:Black;">
        <tr>
            <td colspan="13" >
                  <div class="text headerband">
        Position Report
    </div>
            </td>
        </tr>
        <tr>
            <td width="60px" style="text-align: right">Vessel :</td>
            <td width="150px" ><asp:Label ID="lblVessel" runat="server"></asp:Label></td>
            <td width="70px" style="text-align: right">Voyage # :</td>
            <td width="80px"><asp:TextBox runat="server" ID="txtVoyageNo" CssClass="user-input-nopadding" Width="70px" MaxLength="50"></asp:TextBox></td>
            <td width="70px" style="text-align: right">Period :</td>
            <td width="100px">
                <asp:TextBox runat="server" ID="txtFDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
            </td>
            <td width="100px">
                <asp:TextBox runat="server" ID="txtTDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
            </td>
            <td width="100px" style="text-align: right">Activity Type :</td>
            <td width="120px" style="text-align: left">
                <asp:DropDownList runat="server" ID="ddlReportType" Width="110px">
                    <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                    <asp:ListItem Text="Arrival" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Departure" Value="D"></asp:ListItem>
                    <asp:ListItem Text="Noon" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Port-Anchorage" Value="PA"></asp:ListItem>
                    <asp:ListItem Text="Port-Berthing" Value="PB"></asp:ListItem>
                    <asp:ListItem Text="Port-Drift" Value="PD"></asp:ListItem>
                    <asp:ListItem Text="Shifting" Value="SH"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="70px" style="text-align: right">Location :</td>
            <td width="50px" style="text-align: left">
                <asp:DropDownList runat="server" ID="ddlLocation" Width="70px">
                    <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                    <asp:ListItem Text="At Sea" Value="A"></asp:ListItem>
                    <asp:ListItem Text="In Port" Value="I"></asp:ListItem>
                </asp:DropDownList>
            </td>
            
            <td style="text-align:right; width:150px">
                <asp:Button runat="server" ID="btnSearch" Text=" Search " OnClick="btnSearch_Click" CssClass="btn" Width="70px" />
                <asp:Button runat="server" ID="btnBack" Text=" Back " CssClass="btn" PostBackUrl="~/PositionReport/PositionReport.aspx"  Width="70px"/>
            </td>
            <td>
                <a target="_blank" href='VoyageDataAnalysis.aspx?CurrentShip=<%=Page.Request.QueryString["CurrentShip"].ToString() %> &VessleName=<%=Page.Request.QueryString["VslName"].ToString() %> '> ( Voyage Data Analysis Report ) </a>
                &nbsp;
              <%--  <a target="_blank" href='VPR_EEOI.aspx?CurrentShip=<%=Page.Request.QueryString["CurrentShip"].ToString() %> &VessleName=<%=Page.Request.QueryString["VslName"].ToString() %> '> ( EEOI Report ) </a>--%>

            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
        <tr style="height: 20px;">
            <td style="padding: 5px; background-color:#4c7a6f;color:White;">
                <strong>Reports
            </strong>
            </td>
            <td style="padding: 5px; background-color:#4c7a6f;color:White;border-left:solid 1px black;">
                <strong>Consumption & ROB
            </strong>
            </td>
            <td style="padding: 5px; background-color:#4c7a6f;color:White;border-left:solid 1px black;">
                <strong>ME Performance</strong>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div style="border-bottom: none; overflow:scroll;  overflow-x:hidden;">
                    <table width="100%" cellpadding="5" border="0" style="border-collapse: collapse" class="bordered">
                        <thead>
                            <tr class= "headerstylegrid" >
                                <td style="width: 30px; text-align: center; color: White;">
                                    View
                                </td>
                                <td style="width: 30px; text-align: center; color: White;">
                                    ID
                                </td>
                                <td style="text-align: center; color: White; width:100px;">
                                    Voyage#
                                </td>
                                <td style="width: 80px; color: White; text-align: center;">
                                    Report Date
                                </td>
                                <td style="width: 100px; color: White;">
                                    Report Type
                                </td>
                                <td style=" color: White;">
                                    Dep. / Arrival Port
                                </td>
                               <%-- <td style="width: 15px; color: White;">T</td>
                                <td style="width: 15px; color: White;">M</td>--%>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div style="height: 462px; border-bottom: none; overflow-x: hidden; overflow-y: scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
                    <table width="100%" cellpadding="5" border="1" style="border-collapse: collapse" class="bordered">
                        <tbody>
                            <asp:Repeater runat="server" ID="rptPR">
                                <ItemTemplate>
                                    <tr onmouseover="">
                                        <td style="width: 30px; text-align: center;">
                                            <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Modules/HRD/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("REPORTSPK").ToString() + "~" + Eval("VESSELID").ToString() + "~" + Eval("ACTIVITY_CODE").ToString() %>' Style='background-color: transparent; height: 12px;' />
                                        </td>
                                        <td style="width: 30px; text-align: center;">
                                            <%#Eval("reportsPK")%>
                                        </td>
                                        <td style=" text-align: center;width:100px;">
                                            <%#Eval("VoyageNo")%>
                                        </td>
                                        <td style="width: 80px; text-align: center;">
                                            <%#Common.ToDateString(Eval("ReportDate"))%>
                                        </td>
                                        <td style="width: 100px; text-align: left;">
                                            <%#Eval("ACTIVITY_NAME")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <div style="overflow:hidden; text-overflow: ellipsis;">
                                            <%#Eval("DEPPORT")%>/<%#Eval("DEPARRIVALPORT")%></div>
                                        </td>
                                      <%--<td style="width: 15px; color: White;">
                                            <img src='<%#( Eval("TechVerified").ToString()=="True"?"../../HRD/Images/green_circle.gif":"../../HRD/Images/red_circle.png") %>'  />
                                        </td>
                                    <td style="width: 15px; color: White;">
                                           <img src='<%#( Eval("MarineVerified").ToString()=="True"?"../../HRD/Images/green_circle.gif":"../../HRD/Images/red_circle.png") %>'  />
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </td>
            <td style="vertical-align: top;border-left:solid 1px black;width:28%;">
                <table width="100%" cellpadding="5" border="0" style="border-collapse: collapse" class="bordered">
                    <colgroup>
                        <col style="text-align: left" />
                        <col style="text-align: right" />
                        <col style="text-align: right" />
                        <col style="text-align: right" />
                    </colgroup>
                    <tr  class= "headerstylegrid">
                        <td style="width: 40%">
                            Item Name
                        </td>
                        <td style="width: 15%">
                            Open. Bal
                        </td>
                        <td style="width: 15%">
                            Received
                        </td>
                        
                        <td style="width: 15%">
                            Consumption
                        </td>
                        <td style="width: 15%">
                            ROB
                        </td>
                    </tr>
                    <tr style="font-weight: bold;text-align:right;">
                        <td colspan="5" style=" border-bottom: solid 1px gray; text-align: left;">
                            Fuel ( MT )
                        </td>
                    </tr>
                    <tr>
                        <td>
                            VLSFO
                        </td>
                        <td align="right">
                            <asp:Label ID="lblIFO_OB" runat="server" ></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblIFORec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblIFOTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBIFOTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LSMGO
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMGO_OP" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMGORec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMGOTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBMGOTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            MDO
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMDO_OP" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMDORec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMDOTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBMDOTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                   <tr style="font-weight: bold;">
                        <td colspan="5" style=" border-bottom: solid 1px gray; text-align: left;">
                            Lube (&nbsp; LTR )
                        </td>
                    </tr>
                    <tr>
                        <td>
                            MECC
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECC_OB" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECCRec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECCTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBMECCTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            MECYL( LOW BN)
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECY_OB" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECYLRec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECYLTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBMECYLTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            MECYL
                            ( High BN )</td>
                        <td align="right">
                            <asp:Label ID="lblMECY_OB_HighBn" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECYLRec_HighBn" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMECYLTotal_HighBn" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBMECYLTotal_HighBn" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            AECC
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAECC_OB" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAECCRec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAECCTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBAECCTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            HYD
                        </td>
                        <td align="right">
                            <asp:Label ID="lblHYD_OB" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblHYDRec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblHYDTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBHYDTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                      <table width="100%" cellpadding="5" border="0" style="border-collapse: collapse" class="bordered">
                    <colgroup>
                        <col style="text-align: left" />
                        <col style="text-align: right" />
                        <col style="text-align: right" />
                        <col style="text-align: right" />
                        <col style="text-align: right" />
                    </colgroup>
                     <tr style="font-weight: bold;">
                        <td colspan="6" style=" border-bottom: solid 1px gray; text-align: left;">
                            Fresh Water ( MT )
                        </td>
                    </tr>
                    <tr class= "headerstylegrid">
                        <td >
                            Item Name
                        </td>
                        <td style="width: 15%">
                            Open. Bal
                        </td>
                        <td style="width: 15%">
                            Generated
                        </td>
                        <td style="width: 15%">
                            Received
                        </td>
                        <td style="width: 15%">
                            Consumption
                        </td>
                         <td style="width: 15%">
                            ROB
                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            Total
                        </td>
                     
                        <td align="right">
                            <asp:Label ID="lblFW_OB" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblFWGenerated" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblFWRec" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblFreshWaterTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblROBFreshWaterTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 33%; vertical-align: top;border-left:solid 1px black">
                <div style="border-bottom: none; " class='ScrollAutoReset' id='Div1'>
                    <table width="100%" cellpadding="0" border="0" style="border-collapse: collapse">
                        <tr>
                            <td style="vertical-align: top">
                                <table width="100%" cellpadding="5" border="0" style="border-collapse: collapse" class="bordered">
                                    <colgroup>
                                        <col style="text-align: left" />
                                        <col style="text-align: right" />
                                    </colgroup>
                                    <tr class= "headerstylegrid" >
                                        <td>
                                            Item Name
                                        </td>
                                        <td>
                                            Value
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Exh Temp Min( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblExchTempMin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Exh Temp Max( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblExchTempMax" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME RPM  ( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblMERPM" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Engine Distance ( Sum ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEGDist" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Total Distance (Sum) :</td>
                                        <td align="right">
                                            <asp:Label ID="lblDistanceMadeGoods" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Slip :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblSlip" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME Output ( Avg ) :
                                        </td>
                                       <td align="right">
                                            <asp:Label ID="lblMEOutPut" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME LOADINDICATOR ( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblMELoadIndicator" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME T/C( No. 1)RPM  ( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTC1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME T/C( No. 2)RPM  ( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTC2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME SCAV Pressure  ( Avg ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblMEScav" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                   
                                   
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table width="100%" cellpadding="5" border="0" style="border-collapse: collapse" class="bordered">
                                    <colgroup>
                                        <col style="text-align: left" />
                                        <col style="text-align: right" />
                                    </colgroup>
                                    <tr class= "headerstylegrid" >
                                        <td>
                                            Item Name
                                        </td>
                                        <td>
                                            Value
                                        </td>
                                    </tr>
                                  
                                    <%--<tr>
                                        <td>
                                            Total AUX Load ( Avg ):
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTotAuxLoad" runat="server"></asp:Label>
                                        </td>
                                    </tr>--%>
                                  
                                    <tr>
                                        <td>
                                            A/E FO INLET TEMP ( Avg ):
                                        </td>
                                       <td align="right">
                                            <asp:Label ID="lblAETEMP" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                      
                                    <tr>
                                        <td>
                                            Shaft Generator ( Total HRS ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblShaftGenHrs" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Tank Cleaning ( Total HRS ) :
                                        </td>
                                       <td align="right">
                                            <asp:Label ID="lblTCHrs" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cargo Heating ( Total HRS ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCHHrs" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Inert Hours ( Total HRS ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblIHrs" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            Bilge Pump( if run ) ( Total HRS ) :
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblBilgePump" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                     <tr>
                                        <td>
                                            SCAV TEMP  ( Avg ) :
                                        </td>
                                       <td align="right">
                                            <asp:Label ID="lblScavTemp" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Lube Oil Pressure  ( Avg ):
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblLubeOilPressure" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sea Water Temp  ( Avg ):
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblSeaWaterTemp" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Eng. Room Temp  ( Avg ):
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEngineRoomTemp" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <%--------------------------------------------%>
                                
                </div>
            </td>
        </tr>
    </table>
        <table width="100%" cellpadding="5" border="0" style="border-collapse: collapse" class="bordered">
                    
                        <col style="text-align: left" />
                        <col style="text-align: left" />
                        <col style="text-align: left" />
                        <col style="text-align: left" />
                        <col style="text-align: left" />
                    <tr style="font-weight: bold; background-color:#99D0FF;color:White">
                
                        <td colspan="5" style="padding: 5px; background-color:#4c7a6f;color:White;border-left:solid 1px black;">
                <strong>AE Performance ( AVG )</strong>
            </td>
                </tr>
            
                    <tr class= "headerstylegrid">
                        <td></td>
                        <td>Load ( KW ) </td>
                        <td>HRS ( if run )</td>
                        <td>Exh Temp Min ( C )</td>
                        <td>Exh Temp Max ( C )</td>
                        </tr>
                        <tr>
                            <td>A/ E 1</td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAux1Load" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAE1" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblExtTempMin1" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblExtTempMax1" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>A/ E 2</td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAux2Load" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAE2" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblExtTempMin2" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblExtTempMax2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>A/ E 3</td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAux3Load" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAE3" runat="server"></asp:Label>
                                        </td>
                           <td style="text-align:right;">
                                            <asp:Label ID="lblExtTempMin3" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;"><asp:Label ID="lblExtTempMax3" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>A/ E 4</td>
                           <td style="text-align:right;">
                                            <asp:Label ID="lblAux4Load" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblAE4" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;">
                                            <asp:Label ID="lblExtTempMin4" runat="server"></asp:Label>
                                        </td>
                            <td style="text-align:right;"><asp:Label ID="lblExtTempMax4" runat="server"></asp:Label></td>
                        </tr>
                    </table>
    <script language="javascript" type="text/javascript">
        function ScrollHeader() {
            $("#Div41").scrollLeft($("#Div4").scrollLeft());
        }
        $(document).ready(
        function () {
            $("#Div4").width($("#dv1").width());
            $("#Div41").width($("#dv1").width());
            $("#Div4").scroll(function () {
                ScrollHeader();
            });
        }
        );
    </script>
    </form>
</body>
</html>
