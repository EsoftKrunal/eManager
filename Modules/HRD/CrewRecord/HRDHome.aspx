<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRDHome.aspx.cs" Inherits="HRDHome" MasterPageFile="~/MasterPage.master" Title ="HRD : Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/> 
   <%--  <link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
 <%--  <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/KPIScript.js" ></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js" ></script>--%>
    <style type="text/css">
.fh1
{ 
    font-weight:bold;
    font-size:14px;
}
.fh2
{ 
    font-size:14px;
}
.fh3
{ 
    font-size:14px;
    cursor:pointer;
    text-decoration:underline;
}
.fh3:hover
{ 
    font-size:14px;
    color:Red;
    cursor:pointer;
    text-decoration:underline;
}
.btn_Close
{
    background-color:Red;
    border:solid 1px grey;
    color:White;
    width:100px;
}
.cls_I
{
    background-color:#80E680;
}
.cls_O
{
    background-color:#FFAD99;
}

</style>
    <script type="text/javascript">

        function Show1(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop1").slideDown();
            $("#dv_Pop1").css('top', bodystp + 50 + 'px');
            $("#btn_Show1").focus();
            $("#btn_Show1").click();
        }

        function Show2(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop2").slideDown();
            $("#dv_Pop2").css('top', bodystp + 50 + 'px');
            $("#btn_Show2").focus();
            $("#btn_Show2").click();
        }
        function Show3(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop3").slideDown();
            $("#dv_Pop3").css('top', bodystp + 50 + 'px');
            $("#btn_Show3").focus();
            $("#btn_Show3").click();
        }
        function Show4(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop4").slideDown();
            $("#dv_Pop4").css('top', bodystp + 50 + 'px');
            $("#btn_Show4").focus();
            $("#btn_Show4").click();
        }
        function Show5(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop5").slideDown();
            $("#dv_Pop5").css('top', bodystp + 50 + 'px');
            $("#btn_Show5").focus();
            $("#btn_Show5").click();
        }
        function Show6(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop6").slideDown();
            $("#dv_Pop6").css('top', bodystp + 50 + 'px');
            $("#btn_Show6").focus();
            $("#btn_Show6").click();
        }
        function Show7(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop7").slideDown();
            $("#dv_Pop7").css('top', bodystp + 50 + 'px');
            $("#btn_Show7").focus();
            $("#btn_Show7").click();
        }
        function Show51(ctl) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Pop51").slideDown();
            $("#dv_Pop51").css('top', bodystp + 50 + 'px');
            $("#btn_Show51").focus();
            $("#btn_Show51").click();
        }
        function HideAll() 
        {
            $(".dv_ModalBox").hide();
            $("#dv_Pop1").hide();
            $("#dv_Pop2").hide();
            $("#dv_Pop3").hide();
            $("#dv_Pop4").hide();
            $("#dv_Pop5").hide();
            $("#dv_Pop6").hide();
            $("#dv_Pop7").hide();
            $("#dv_Pop51").hide();
        }
</script>
     <style type="text/css">
        .trBox {
            font-weight: bold;
            font-size: 14px;
            border: 1px solid silver;
            border-radius: 8px;padding: 20px 15px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="background-color:Black; opacity:0.4;filter:alpha(opacity=40); width:100%; z-index:50; min-height:100%; position:absolute; top:0px; left:0px; display:none;" class='dv_ModalBox' onclick="HideAll();"></div>
        <div style="text-align: center">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table style="width :100%" cellpadding="0" cellspacing="0" class="table no-border table-responsive">
                <tr>
                    <td style=" text-align :left; vertical-align : top;">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #b6e3d4 1px solid;border-top: #b6e3d4 1px solid; border-left: #b6e3d4 1px solid; border-bottom: #b6e3d4 1px solid; text-align:center" width="100%">
                            <tr>
                                <td class="text headerband">HRD Home</td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="20" cellspacing="15" width="100%" border="0" class="table ">
                                                    <tr>
                                                        <td style="text-align:left; background-color:#FFAD99;" class="col-xs-11 trBox">
                                                            Crew SignOff Due in next 30 Days :</td>
                                                        <td style="text-align:center; background-color:#FFAD99;" class="trBox">
                                                           <asp:Label runat="server" ID="lblc5" onclick="Show5();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:left; background-color:#80E680;" class="col-xs-11 trBox">
                                                            Crew Change Planned for next 30 Days :        
                                                        </td>
                                                        <td style="text-align:center; background-color:#80E680;" class="trBox">
                                                           <asp:Label runat="server" ID="lblc1" onclick="Show1();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label> 
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:left; background-color:#FFE6E6;" class="col-xs-11 trBox">
                                                            Crew Awaiting Approval :
                                                        </td>
                                                        <td style="text-align:center; background-color:#FFE6E6;" class="trBox">
                                                            <asp:Label runat="server" ID="lblc2" onclick="Show2();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label> 
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <%--<tr>
                                                    <td style="text-align:left; background-color:#E0FFE0;" class="fh2">
                                                        Overdue Crew Training :</td>
                                                    <td style="text-align:center; background-color:#E0FFE0;" class="fh2">
                                                        <asp:Label runat="server" ID="lblc3" onclick="Show3();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label> </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="text-align:left; background-color:#FFEB99;" class="col-xs-11 trBox">
                                                            Vessel exceeding budgeted crew complement :
                                                        </td>
                                                        <td style="text-align:center; background-color:#FFEB99;" class="trBox">
                                                           <asp:Label runat="server" ID="lblc6" onclick="Show6();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td style="text-align:left; background-color:#C2E0FF;" class="col-xs-11 trBox">
                                                            Open PEAP :
                                                        </td>
                                                        <td style="text-align:center; background-color:#C2E0FF;" class="trBox">
                                                           <asp:Label runat="server" ID="lblc7" onclick="Show7();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr runat="server" visible="false">
                                                        <td style="text-align:left; background-color:#CCB2FF;" class="col-xs-11 trBox">
                                                            Crew Availibility Expiring In Next 7 Days :
                                                        </td>
                                                        <td style="text-align:center; background-color:#CCB2FF;" class="trBox">
                                                            <asp:Label runat="server" ID="lblc51" onclick="Show51();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td style="text-align:left; background-color:#4994B1;" class="col-xs-11 trBox">
                                                            Crew Availibility Expire In Next 
                                                            <asp:TextBox ID="txtAvailibilityInDays" runat="server" Width="30px" Text="30" MaxLength="2" style="text-align:center" CssClass="NumberValidation" AutoPostBack="true" OnTextChanged="OnTextChanged_txtAvailibilityInDays" ></asp:TextBox>
                                                            Days :</td>
                                                        <td style="text-align:center; background-color:#4994B1;" class="trBox">
                                                            <%--<a href="CrewAvailablity.aspx" target="_blank">--%>
                                                                <asp:Label runat="server" ID="lblc8" onclick="Show8();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label> 
                                                            <%--</a>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:200px; vertical-align:top;">
                                            <br />
                                                <table cellpadding="5" cellspacing="5" width="100%"  border="0" class="table table-bordered table-responsive">
                                                    <tr runat="server" id="tr_VIQ">
                                                        <td style="text-align:left; width:10px;" ><img src="../Images/right-arrow_12.png" /></td>
                                                        <td style="text-align:left" class="fh3">
                                                            <asp:LinkButton ID="lbChart" runat="server" Text="Chart" ForeColor="#206020" OnClick="lbChart_Click"></asp:LinkButton>
                                                            <%--<a href="Chart.aspx" target="_blank">Chart</a>--%>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="tr_VPR">
                                                        <td style="text-align:left; " ><img src="../Images/right-arrow_12.png" /></td>
                                                        <td style="text-align:left;color:#206020;" >
                                                             <asp:LinkButton ID="lbMissingDoc" runat="server" Text="Missing Documents" ForeColor="#206020" OnClick="lbMissingDoc_Click" ></asp:LinkButton>
                                                            <%--<a href="CrewMissingDocuments.aspx" target="_blank">Missing Documents</a>--%>
                                                            <i style="font-size:11px">(On Board Crew)</i>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table> 
                    </td>
                </tr>
            </table>
        </div>

    <!-- Crew Change PopUP -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop1">
    <center>
    <div style="border:solid 10px orange; width:1000px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show1" Text="Show1" OnClick="btn_Show1_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Crew Change Planned for next 30 Days
            <asp:Label runat="server" ID="lblRcount1"></asp:Label>
                    
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:50px; text-align:center;">Vessel</td>
            <td style="width:50px; text-align:center;">Crew#</td>
            <td>&nbsp;Crew Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:80px; text-align:center;">ETA Date</td>
            <td style="width:150px; text-align:center;">Port Name</td>
            
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt1">
            <ItemTemplate>
            <tr class='<%#"cls_" + Eval("CREWFLAG").ToString()%>'>
            <td style="width:50px; text-align:center;"><%#Eval("VESSELCODE")%></td>
            <td style="width:50px; text-align:center;"><%#Eval("CREWNUMBER")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("RankCode")%></td>
            <td style="width:80px; text-align:center;"><%#Common.ToDateString(Eval("ETA"))%></td>
            <td style="width:150px; text-align:left;">&nbsp;<%#Eval("PORTNAME")%></td>
            
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <div style="float:right; w">
                <input type="button" id="btn_Close1" style="float:right" class="btn_Close" value="Close" onclick="HideAll();"/>
                <div style="background-color:#80E680; float:right; padding:5px; margin-right:10px;">Sign On</div>
                <div style="background-color:#FFAD99; float:right; padding:5px; margin-right:10px;">Sign Off</div>
            </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>
    <!-- Crew Awaiting Approval PopUP -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop2">
    <center>
    <div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show2" Text="Show2" OnClick="btn_Show2_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Crew Awaiting Approval
            <asp:Label runat="server" ID="lblRcount2"></asp:Label>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:50px; text-align:center;">Crew#</td>
            <td>&nbsp;Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:80px; text-align:center;">Plan. Vessel</td>
            <td style="width:150px; text-align:center;">Planned By</td>
            <td style="width:80px; text-align:center;">Planned On</td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt2">
            <ItemTemplate>
            <tr>
            <td style="width:50px; text-align:center;"><%#Eval("EmpNo")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("RankCode")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("VesselCode")%></td>
            <td style="width:150px; text-align:left;">&nbsp;<%#Eval("PlannedBy")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("PlannedOn")%></td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="upg_dv_Done" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button2" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>
    <!-- Overdue Crew Trainining PopUP -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop3">
    <center>
    <div style="border:solid 10px orange; width:1100px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show3" Text="Show3" OnClick="btn_Show3_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Overdue Crew Trainining
            <asp:Label runat="server" ID="lblRcount3"></asp:Label>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:50px; text-align:center;">Crew#</td>
            <td>&nbsp;Crew Name</td>
            <td style="width:80px; text-align:center;">Status</td>
            <td style="width:400px; text-align:center;">Training Name</td>
            <td style="width:100px; text-align:center;">Next Due</td>
            <td style="width:100px; text-align:center;">Rank</td>
            <td style="width:100px; text-align:center;">Source</td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt3">
            <ItemTemplate>
            <tr>
            <td style="width:50px; text-align:center;"><%#Eval("CrewNumber")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("CrewStatusName")%></td>
            <td style="width:400px; text-align:left;">&nbsp;<%#Eval("TRAININGNAME_SIMILER")%></td>
            <td style="width:100px; text-align:center;"><%#Common.ToDateString(Eval("NEXTDUE"))%></td>
            <td style="width:100px; text-align:center;">&nbsp;<%#Eval("RANKCODE")%></td>
            <td style="width:100px; text-align:center;"><%#Eval("SOURCE")%></td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button3" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>
    <!-- On Board Crew Document Missing -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop4">
    <center>
    <div style="border:solid 10px orange; width:1000px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show4" Text="Show4" OnClick="btn_Show4_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">On Board Crew Document Missing
            <asp:Label runat="server" ID="lblRcount4"></asp:Label>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:50px; text-align:center;">Crew#</td>
            <td>&nbsp;Name</td>
            <td style="width:350px; text-align:center;">Document Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:80px; text-align:center;">Vessel</td>
            <td style="width:150px; text-align:center;">Flag State</td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt4">
            <ItemTemplate>
            <tr>
            <td style="width:50px; text-align:center;"><%#Eval("CREWNUMBER")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:350px; text-align:left;">&nbsp;<%#Eval("LicenseName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("RankCode")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("VesselCode")%></td>
            <td style="width:150px; text-align:left;">&nbsp;<%#Eval("CountryName")%></td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button4" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>
    <!-- Crew Sign Off Due in next 30 days -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop5">
    <center>
    <div style="border:solid 10px orange; width:1200px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show5" Text="Show5" OnClick="btn_Show5_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Crew Sign Off Due in next 30 days
            <asp:Label runat="server" ID="lblRcount5"></asp:Label>
            </div>
            <div>
               Recruiting Office : <asp:DropDownList runat="server" ID="ddlRO" AutoPostBack="true" OnSelectedIndexChanged="btn_Show5_Click"></asp:DropDownList>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:50px; text-align:center;">Vessel</td>
            <td style="width:50px; text-align:center;">Crew#</td>
            <td>&nbsp;Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:100px; text-align:center;">Relie Due Dt.</td>
            <td style="width:200px; text-align:center;">Reliever</td>
           <td style="width:200px; text-align:center;">PortCall#</td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:400px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt5">
            <ItemTemplate>
            <tr>
            <td style="width:50px; text-align:center;"><%#Eval("VesselCode")%></td>
            <td style="width:50px; text-align:center;"><%#Eval("CREWNUMBER")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("RankCode")%></td>
            <td style="width:100px; text-align:center;"><%#Common.ToDateString(Eval("RELIEFDUEDATE"))%></td>
            <td style="width:200px; text-align:left;"><%#Eval("RELIEVER")%></td>
            <td style="width:200px; text-align:left;"><%#Eval("PORTCALL")%></td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button5" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>

    <!-- Vessel exceeding budgeted crew complement  -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop6">
    <center>
    <div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show6" Text="Show6" OnClick="btn_Show6_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Vessel exceeding budgeted crew complement
            <asp:Label runat="server" ID="lblRcount6"></asp:Label>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td>&nbsp;Vessel Name</td>
            <td style="width:100px; text-align:center;">Budget Crew</td>
            <td style="width:100px; text-align:center;">Actual Crew</td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt6">
            <ItemTemplate>
            <tr>
            <td style="text-align:left">&nbsp;<%#Eval("VesselName")%></td>
            <td style="width:100px; text-align:center;"><%#Eval("BUDGETCOUNT")%></td>
            <td style="width:100px; text-align:center;"><%#Eval("ACTUALCOUNT")%></td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button6" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>
    <!-- Open PEAP -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop7">
    <center>
    <div style="border:solid 10px orange; width:1000px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
        <ContentTemplate>
        <div>
            <asp:Button runat="server" ID="btn_Show7" Text="Show7" OnClick="btn_Show7_Click" style="display:none"></asp:Button>
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Open PEAP
            <asp:Label runat="server" ID="lblRcount7"></asp:Label>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:70px; text-align:center;">Ocassion</td>
            <td style="width:50px; text-align:center;">Crew#</td>
            <td>&nbsp;Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:90px; text-align:center;">PEAP Type</td>
            <td style="width:80px; text-align:center;">From Date</td>
            <td style="width:80px; text-align:center;">To Date</td>
            
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt7">
            <ItemTemplate>
            <tr>
            <td style="width:70px; text-align:left;"><%#Eval("OCCASION")%></td>
            <td style="width:50px; text-align:center;"><%#Eval("CREWNO")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("AssNameMod")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("ShipsoftRank")%></td>
            <td style="width:90px; text-align:left;">&nbsp;<%#Eval("PeapType")%></td>
            <td style="width:80px; text-align:left;">&nbsp;<%#Common.ToDateString(Eval("AppraisalFromDate"))%></td>
            <td style="width:80px; text-align:left;">&nbsp;<%#Common.ToDateString(Eval("AppraisalToDate"))%></td>
            
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanel7">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button7" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>
    <!-- Crew Availibility in Next 7 Days -->
    <div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Pop51">
    <center>
    <div style="border:solid 10px orange; width:1000px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel51">
        <ContentTemplate>
        <div>
            
            <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Crew/Applicant Availibility in next 7 days
            <asp:Label runat="server" ID="lblRcount51"></asp:Label>
            </div>
            <div style="background-color:#f2f2f2; padding:2px;">
            <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
            <tr>
                <td style="width:110px">Crew # /App Id : </td>
                <td><asp:TextBox runat="server" id="txtCrewNo" style="width:50px" class="input_box" MaxLength="6"></asp:TextBox></td>
                <td style="width:80px">Crew Name :</td>
                <td><asp:TextBox runat="server" id="txtCrewName" style="" class="input_box"></asp:TextBox></td>
                <td style="width:50px">Rank :</td>
                <td><asp:DropDownList runat="server" id="ddlRank" style="" class="input_box"></asp:DropDownList></td>
                <td style="width:80px">Nationality :</td>
                <td><asp:DropDownList runat="server" id="ddlNat" style="" class="input_box"></asp:DropDownList></td>
                <td>Can Join in next :</td>
                <td><asp:TextBox runat="server" id="txtNextDays" class="input_box" MaxLength="3" Text="7" style="width:30px;text-align:center"></asp:TextBox></td>
                <td>(days)</td>
                <td>
                    <asp:Button runat="server" ID="btn_Show51" Text="Show" OnClick="btn_Show51_Click"></asp:Button>
                </td>
            </tr>
            </table>
            </div>
            <div><asp:RadioButtonList runat="server" ID="rad_CA" AutoPostBack="true" OnSelectedIndexChanged="rad_CA_OnSelectedIndexChanged" RepeatDirection="Horizontal"> 
                <asp:ListItem Text="Crew Members" Value="C" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Applicants" Value="A"></asp:ListItem>
            </asp:RadioButtonList> </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2;">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr>
            <td style="width:100px; text-align:center;">Crew # /App Id</td>
            <td>&nbsp;Crew Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:100px; text-align:center;">Exp. Join Dt.</td>
            <td style="width:200px; text-align:center;">Last Vessel</td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style="height:300px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="rows" >
            <asp:Repeater runat="server" ID="rpt51">
            <ItemTemplate>
            <tr>
            <td style="width:100px; text-align:center;"><%#Eval("CREWNUMBER")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("RankCode")%></td>
            <td style="width:100px; text-align:center;"><%#Common.ToDateString(Eval("EXPECTEDJOINDATE"))%></td>
            <td style="width:200px; text-align:left;"><%#Eval("vesselname")%></td>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>

            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress51" runat="server" AssociatedUpdatePanelID="UpdatePanel51">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <input type="button" id="Button8" class="btn_Close" value="Close" onclick="HideAll();"/>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#lblc8").click(function () {
                var x = $("#txtAvailibilityInDays").val();
                window.open("CrewAvailablity.aspx?AD=" + x);
            });

            $(".NumberValidation").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>
