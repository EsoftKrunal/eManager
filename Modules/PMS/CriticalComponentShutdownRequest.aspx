<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CriticalComponentShutdownRequest.aspx.cs"
    Inherits="CriticalComponentShutdownRequest" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    <%--<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>--%>
    <script src="JS/jquery_v1.10.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .myrow
        {
         
        }
        .myrow:hover
        {
            background-color: #ffff99;
        }
        .active
        {
            background-color:#c2c2c2;
        }
        .dvVessel
        {
            padding:5px; 
            margin-bottom:1px; 
            background-color:#cce6ff; 
            color:#444;
        }
        .active
        {
            background-color:#ffad33;
            color:white;
        }
        .verified
        {
            background-color:#e6ffee;
        }
        .notverified
        {
            background-color:#ffebe6;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function opendetails(tr) {
            var RP = tr.getAttribute('act');
            var HID = tr.getAttribute('hid');
            var VC = tr.getAttribute('vsl');
            if (RP == 'R') {
                RP = 'R';
                window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');

            }
            if (RP == 'P') {
                RP = 'P';
                window.open('PopupHistoryJobDetails_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
            }

        }

        function ReloadPage() {
            document.getElementById('btnViewReport').click();
        }
        function ShowComments(Comments) {
            alert(Comments);
        }
    </script>
    <style type="text/css">
        .withborder td
        {
            border: solid 1px #e9e9e9;
        }
    </style>
              <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body style="font-family: Calibri; font-size: 14px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" ></asp:ScriptManager>
    <div style="font-family:Arial;font-size:12px;">
        <div class="text headerband">
            <b>Component Shutdown Request</b>
        </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">        
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style=" padding-top:2px;">
                            <td style="padding-right: 5px; padding-left: 2px;">
                            
                            <div style="width:100%; height:100%; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                        <tr>
                        <td>
                        <table cellpadding="2" cellspacing="0" width="100%" border="1" rules="cols" >
                        <tr style=" font-weight: bold ;"  >
                            <td style="text-align:left;width:165px; ">&nbsp;Fleet :</td>          
                            <td style="text-align:left;width:165px; ">&nbsp;Year :</td>                            
                            <td style="text-align:left;width:200px; ">&nbsp;Component Code :</td>                                                         
                            <td style="text-align:left; ">&nbsp;</td>
                            <td style="text-align:left; ">&nbsp;</td>
                            <td style="text-align:left; ">&nbsp;</td>
                            <td style="text-align:left; ">&nbsp;</td>
                            <td style="text-align:left; ">&nbsp;</td>
                        </tr>
                        <tr style=" background-color :#F2F2F2" >
                        <td id="tdFleet" style="text-align:left" runat="server" >
                        <table  cellpadding="0"  cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px">   <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Fleet_OnSelectedIndexChanged" Width="125px">
                                </asp:DropDownList></td>
                            </tr>
                        </table>
                        </td>
                        <td style="text-align:left"  >
                        <table  cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px"> <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged" Width="125px">                                    
                                </asp:DropDownList> </td>
                               
                           </tr>
                           
                        </table> 
                        </td>                       
                        <td style="text-align:left" >
                          <table cellpadding="0" cellspacing="0" width="100%" >
                           <tr>
                              <td style="text-align:left">
                              <asp:TextBox ID="txtCompCode" runat="server" AutoPostBack="true" MaxLength="15"  Width="200px"
                                            OnTextChanged="btnTemp_OnClick"></asp:TextBox>
                              </td>                        
                           </tr>
                          </table>
                        </td>                                           
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="2" width="100%">
                           <tr>
                              <td style="text-align:right">   </td>
                                            
                           </tr>
                           
                           
                          </table>
                        </td>
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="0" width="100%" >
                           <tr>
                              <td style="text-align:left">
                           
                              </td>                        
                           </tr>
                          </table>
                        </td>
                        <td style="text-align:left" >
                            
                        </td>
                        <td style="text-align:left" >
                           
                        </td>
                        <td style="text-align:center">
                        
                        </td>
                        </tr>
                            <tr>
                                <td colspan="2" style="padding: 7px; background: #e6f2ff; width:330px;">
                                    <table cellpadding="3" cellspacing="0" width="100%" >
                       <%-- <tr>
                            <td style="text-align: left;">
                                Vessel
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="text-align: left;">
                                <div style="height: 530px; overflow-y: scroll; overflow-x: hidden; border: solid 1px #e3e3e3">
                                        <div style="padding:8px; ">
                                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <col />
                                        <col width="50px" align="center" />
                                        <col width="90px" align="center" />
                                        <tr class= "headerstylegrid">
                                            <td>Vessel Name</td>
                                            <td>Total</td>
                                            <td>For Approval</td>
                                        </tr>
                                        </table>
                                        </div>
                                        <asp:Repeater ID="rptRepeterFleet" runat="server">
                                            <ItemTemplate>
                                            <div class="dvVessel" >

                                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                <col />
                                                <col width="50px" align="center"/>
                                                <col width="90px" align="center" />
                                                <tr style="">
                                                    <td>
                                                        <%#Eval("VesselCode")%>
                                                    </td>
                                                    <td style="cursor:pointer">
                                                        <span class="SpanTotal" vessel='<%#Eval("VesselCode")%>'>
                                                            <%#Eval("Total")%>
                                                        </span>
                                                    </td>
                                                    <td style="cursor:pointer">
                                                        <span class="SpanUnverified"
                                                            vessel='<%#Eval("VesselCode")%>'>
                                                            <%#Eval("Unverified")%>
                                                        </span>
                                                    </td>
                                                </tr>
                                                </table>

                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    
                                    <asp:CheckBoxList ID="chkVessels" runat="server" Visible="false">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                      
                      <%--  <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnViewReport" CssClass="btnorange" Text="Show Report" runat="server"
                                    Width="100px" OnClick="btnViewReport_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblCount" Style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                                </td>
                               <td style="vertical-align: top" colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>                   
                    <asp:HiddenField ID="hfVessel" runat="server" />
                    <asp:HiddenField ID="hfType" runat="server" />
                    <asp:Button ID="btnTemp" runat="server" OnClick="btnTemp_OnClick" Style="display: none;" /> 
                    <div style="height:50px; overflow-y: scroll; overflow-x: hidden; background-color: #0066CC;color:#fff;">
                        <table cellspacing="0" class="withborder" border="1" cellpadding="2" style="width: 100%;border-collapse: collapse; font-size: 13px; height:50px; font-weight:bold;">
                            <colgroup>
                                <col style="width: 2%;" />
                                <col style="width: 10%;" />
                                <col style="width:21%;"/>
                                <col style="width:8%;" />
                                <col style="width:12%;" />
                                <col style="width:10%;" />
                                <col style="width:14%;" />
                                <col style="width:14%;" />
                                <col style="width:9%;" />
                                <tr align="left" class= "headerstylegrid">
                                   <td style="text-align: center"><img src="Images/magnifier.png" style="height: 12px;margin-top:5px;" /></td>               
                                   <td>Comp.Code</td>
                                   <td>Component Name</td>
                                   <td>Request Date.</td>
                                   <td>Master/CE Name</td>
                                   <td>Planned Shutdown (Total Hours)</td>
                                   <td>Maintenance Commenced (Ship’s LT)</td>
                                   <td>Maintenance Completed (Ship’s LT)</td>
                                   <td>Approved</td>                                 
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div style="height: 600px; overflow-y: scroll; overflow-x: hidden;">
                        <table cellspacing="0" class="withborder" border="1" cellpadding="2" style="width: 100%;border-collapse: collapse; font-size: 13px; color:#777;">
                            <colgroup>
                                <col style="width: 2%;" />
                                <col style="width: 10%;" />
                                <col style="width:21%;"/>
                                <col style="width:8%;" />
                                <col style="width:12%;" />
                                <col style="width:10%;" />
                                <col style="width:14%;" />
                                <col style="width:14%;" />
                                <col style="width:9%;" />
                            </colgroup>
                            <asp:Repeater ID="rptComponentUnits" runat="server">
                                <ItemTemplate>
                                    <tr>
                                       <td style="text-align: center; padding-top: 5px;">
                                            <a href="Office_CriticalEqpShutdownReq.aspx?CompCode=<%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SD=<%#Eval("ShutdownId")%>" target="_blank">
                                                <img src="Images/magnifier.png" style="height: 12px;cursor: pointer; border:none;" />
                                            </a>
                                        </td>
                                        <td align="left">
                                            <%#Eval("ComponentCode")%>
                                        </td>
                                        <td align="left">
                                            <%#Eval("ComponentName")%>
                                        </td>
                                        <td align="left">
                                            <b><%#Eval("requestDate_s")%></b>
                                        </td>
                                        <td align="left">
                                            <%#Eval("MasterCEName")%>
                                         </td>
                                         <td align="center">
                                            <%#Eval("Pl_ShutDownTotalHrs")%>
                                         </td>
                                         
                                        <td align="center"><%# ToDateTimeString(Eval("Ma_CommencedDateTime"))%></td>
                                        <td align="center"><%# ToDateTimeString(Eval("Ma_CompletedDateTime"))%></td> 

                                        <td align="left">
                                            <span style="color: maroon">
                                                <%#Eval("ApprovalStatus_s1")%>
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                            </tr>
                        </table>
                        </td>
                        </tr>
                          </table>
                                
                                </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td>
            </tr>
            </table>
        
        <div style="text-align: center; padding: 6px; background-color: #ffffcc">
            &nbsp;
            <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".SpanTotal").click(function () {
                var Vessel = $(this).attr("Vessel");

                $("#hfVessel").val(Vessel);
                $("#hfType").val("1");
                $("#btnTemp").click();
                $(".dvVessel").removeClass("active");
                $(this).parents(".dvVessel").addClass("active");
            });


            $(".SpanUnverified").click(function () {
                var Vessel = $(this).attr("Vessel");
                $("#hfVessel").val(Vessel);
                $("#hfType").val("2");
                $("#btnTemp").click();

                $(".dvVessel").removeClass("active");
                $(this).parents(".dvVessel").addClass("active");
            });

            $(".dvVessel").click(function () {
                //                $(".dvVessel").removeClass("active");
                //                $(this).addClass("active");
            });


        });
    </script>
    </form>
</body>
</html>