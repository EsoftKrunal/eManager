<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestHourEntry_N.aspx.cs" Inherits="VIMS_RestHourEntry_N" Trace="false" %>
<%--<%@ Register Src="~/UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>--%>
<%--<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="~/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <script src="../JS/jquery-1.10.2.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function selectdate(ctlid) {
            $("#" + ctlid).prop("checked", true);
            $("#" + ctlid).parent().addClass("active");
        }
    </script>
    <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .btn1
        {
            background-color:forestgreen;
            color:white;
            padding:8px;
            border:none;
            width:100px;
            font-size:13px;
        }
          .btn2
        {
            background-color:#ff6868;
            color:white;
            padding:8px;
            border:none;
            width:100px;
            font-size:13px;
        }
        table
        {
            border-collapse:collapse;
        }
        .bordered tr td
        {
            border:solid 1px #efeded;
        }
        .dayname
        {
            width:25px;
            border:solid 1px #808080;
            height:30px;
            vertical-align:middle;
            font-weight:bold;
        }
        .day
        {
       
           
            background-color:#cee3ff;
            cursor:pointer;
        }
        .border
        {
            vertical-align:middle;
            /*width:25px;*/
            height:30px;
            border:solid 1px #808080;
        
        }
        .day:hover {
            background-color:#80b1f5;
            color:black;
        }
        .active {
            background-color:#00132d;
            color:white;
            font-weight:bold;
        }

        .dateline
        {
            background-color:#ffffff;
            background-repeat:no-repeat;
            background-position:center;
        }
        .planned
        {
            background-image:url('../Images/exclamation-mark-yellow.png');
        }
        .saved
        {
            background-image:url('../Images/checked-mark-green.png');
        }
        .unsaved
        {
            background-image:url('../Images/exclamation-mark-Red.png');
        }
        .nc
        {
            background-color:#ff6868;
            color:white;
        }
        .today
        {
            border:solid 3px black;
        }

        /*.today:before
        {
            content:"*";
            color:orange;
            font-weight:bold;
            position:relative;
            left:0px;
            top:0px;
        }*/
        .control
        {
            line-height:25px;
            border:solid 1px #808080;
            height:25px;
            width:60px;
            font-weight:bold;
        }
        .timeslot{
            cursor:pointer;      
            
        }
         .rest{
            background-color:forestgreen;
            color:white;
        }
          .work{
            background-color:brown ;
            color:white;
        }
          .planning{
            background-color:#f3a910;
            color:white;
        }
        .HourCount
        {
                font-size: 15px;
                font-weight: bolder;
                color: white;
                padding: 7px;
                width:40px;
        }
        .RestHourCount
        {
            border:solid 1px #88c76d;background-color:#7bb919;          
        }
        .WorkHourCount
        {
            border:solid 1px #e47d7d;background-color:#dc8282;
        }
    </style>
    </head>

<body style="font-size:13px; color:#333;font-family:Arial;font-size:12px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div id="dvContainer" runat="server" visible="false">
    <div style="text-align: center; padding:8px;  font-size:17px;" class="text headerband">Rest Hour Entry</div>
    <div style="text-align: center" id="content">
        <table style="margin:0 auto; width:100%;" cellpadding="5" border="1" class="bordered">
            <col width="130px" />
            <col width="100px" />

            <col width="115px" />
            <col width="200px" />

            <col width="80px" />
            <col width="130px" />

            <col width="130px" />
            <col width="140px" />

            <col width="120px" />
            <col width="110px" />

            <col width="130px" />
            <col  />

            <tr >
                <td style="text-align:right"><b> Crew Number : </b></td>
                <td style="text-align:left"> 
                    <asp:Label runat="server" ID="lblcrewnumber"></asp:Label> 
                    <asp:HiddenField ID="hfUserType" runat="server" Value="user" />
                </td>
                <td style="text-align:right"><b> Crew Name : </b></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lblname"></asp:Label></td>

                <td style="text-align:right"><b> Rank :</b></td>
                <td style="text-align:left"><asp:Label runat="server" ID="lblRank"  ></asp:Label></td>

                <td style="text-align:right"><b> Sign On Date : </b></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lblfdt"></asp:Label></td>

                <td style="text-align:right"><b> Sign Off Date : </b></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lbltdt"></asp:Label></td>
            </tr>
        </table>
        
       
            <div style="text-align: center;background-color:#d4d1d1; padding:5px;">
            <b> Month & Year :  </b>
            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>

                <asp:Button ID="btnPrintWorkRestRecord" runat="server" Text="Print Work And Rest Record" OnClick="btnPrintWorkRestRecord_OnClick" CssClass="btn" Width="200px" />
                <asp:Button ID="btnPrint" runat="server" Text="Print NC" OnClick="btnPrint_OnClick" CssClass="btn" Width="100px" />

            </div>
         <div style=" padding:10px;">
             <div style="border-bottom:solid 2px black; width:95% ">
                <asp:Literal runat="server" ID="litcalander"></asp:Literal>   
             </div>
        </div>
        <div style="padding:0px 50px 0px 50px; ">
            <asp:HiddenField runat="server" ID="hfdid" Value="" />
            <asp:HiddenField runat="server" ID="hfdSlots" Value="" />
            <asp:Button runat="server" ID="btnpost"  Text="post" OnClick="btnpost_Click" style="display:none" />
             <table cellpadding="5" border="0">
                 <tr>
                     <td style="font-size:18px;text-align:left; width:70px; ">Date : </td>
                     <td style="font-size:18px; text-align:left;font-weight:bold;width:180px;"><asp:Label runat="server" ID="lblForDate" Text=""></asp:Label></td>
                     <td style="font-size:18px;text-align:right; width:150px; ">Location : </td>
                     <td style="font-size:18px;text-align:left; width:150px; "><asp:Label runat="server" ID="lbllocation" Font-Size="18px" Font-Bold="true"></asp:Label></td>

                     <td style="font-size:18px;text-align:left; width:230px;"><%--International Dateline :--%> </td>
                     <td style="text-align:left; font-weight:bold;"> <asp:Label runat="server" ID="lblidl" Font-Size="18px"  Font-Bold="true" style="display:none;"></asp:Label></td>
                 </tr>
            </table>
            <div>
                <br />
            </div>

             
            <div unselectable="on" onselectstart="return false;" onmousedown="return false;">
                
                <asp:Literal runat="server" ID="litLog"></asp:Literal>   
            </div>                
            <table cellpadding="0" cellspacing="0" border="0" id="tblWorRestCount" runat="server" visible="false" style="margin-top:5px;">
                        <tr>
                            <td style="padding:8px; font-weight:bold;font-size:15px;text-align:right;">Work :</td>
                            <td >
                                <div class="HourCount WorkHourCount" >
                                    <asp:Label ID="lblWorkHourCount" runat="server" Text="0.0" ></asp:Label>
                                 </div>
                            </td>
                            <td style="padding:8px; font-weight:bold;font-size:15px; text-align:right;">Rest :</td>
                            <td >
                                <div class="HourCount RestHourCount" >
                                     <asp:Label ID="lblRestHourCount" runat="server" Text="24.0" ></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>   
                <%--<asp:RadioButtonList runat="server" ID="radStatus" RepeatDirection="Horizontal" Font-Size="18px" >
                         <asp:ListItem Text="Planning" Value="P"></asp:ListItem>
                         <asp:ListItem Text="Final Log" Value="S"></asp:ListItem>
                </asp:RadioButtonList> --%>
                <br />
            
            <div style="display:none;">
                <%--OnClientClick="return ReadytoSave();" OnClick="btnsave_Click"--%>
                <asp:Button runat="server" ID="btnsave" Text=" Save " CssClass="btn1"  Visible="false" />
                 <asp:Button runat="server" style="display:none;" Visible="false" ID="btnClear" Text=" Clear Log " CssClass="btn2" OnClientClick="return confirm('Are you sure to delete log for selected day?');" OnClick="btnClear_Click"  />
                 <input type="button" class="btn2" value="Close" onclick="window.close();"  style="display:none;"/>
            </div>
        </div>
    </div>

    <%-----------------------------------------------------------------%>
        <div style="padding:10px;">
            <table width="100%" cellpadding="0" cellspacing="0" rules="all">
                <col />
                <col width="50%" />
                <tr>
                    <td>
                        <div style="border:solid 1px #666767">
                            <table width="100%" cellpadding="8" cellspacing="2" style="background:#666767;color:white;">
                                    <col  />
                                    <tr>
                                        <td><b>NC Type</b></td>
                                    </tr>
                            </table>
                            <div style="width:99%;height:130px;overflow-x:hidden;overflow-y:scroll;">
                                 <ul>
                                              
                                <asp:Repeater ID="rptNC" runat="server">
                                    <ItemTemplate>                        
                                        <li><b> <%#Eval("NCTypeName") %></b></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                   </ul>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div style="border:solid 1px #666767; height:162px;">
                            <table width="100%" cellpadding="8" cellspacing="2" style="background:#666767;color:white;">
                                    <col  />
                                    <tr>
                                        <td><b>NC Remarks</b></td>
                                    </tr>
                            </table>
                                 <table width="100%" cellpadding="2" cellspacing="2" rules="none">
                                     <tr>
                                         <td>
                                             <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="99%" Height="120px"></asp:TextBox>
                                         </td>
                                         
                                     </tr>
                                </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        
    <%-----------------------------------------------------------------%>
        
    <%-----------------------------------------------------------------%>

    <div style="position:fixed; bottom:0px;left:0px; width:100%; font-size:13px; padding:10px; background-color:#f3f6b2; font-weight:bold; ">
            
            <asp:Label runat="server" ForeColor="Red" Font-Bold="true" ID="lblMsg" Text=""></asp:Label>
            
            <table cellpadding="5" style="float:right;margin-right:10px;">
                <tr>
                    <td class="border unsaved dateline" style="height:15px;width:15px"></td>
                    <td style="vertical-align:middle;" >Work Hours Not Updated. </td>
                    <td class="border planned dateline" style="height:15px;width:15px"></td>
                    <td style="vertical-align:middle;" >Work Hours Planned Only. </td>
                    <td class="border saved dateline" style="height:15px;width:15px"> </td>
                    <td style="vertical-align:middle;">Work Hours Updated. </td>
                    <td class="border nc dateline" style="height:15px;width:15px"> </td>
                    <td style="vertical-align:middle;">Non-Conformity </td>
                    <td class="border today dateline" style="height:15px;width:15px"></td>
                    <td style="vertical-align:middle;">Today</td>
                </tr>
            </table>
        </div>
</div>

         <%----------------------------------------------------------------------------------------------------------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvDobValidation" visible="true" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:400px; height:130px; padding :10px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
            <center>
                    <table cellpadding="5" cellspacing="5" border="0" width="100%" style="text-align:center;">
                        <tr class="text headerband">
                            <td  >
                                <span style="font-size:18px;font-weight:bold;" > Please enter your date of birth of crew </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlYearValidate" runat="server" Width="70px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlMonthValidate" runat="server" Width="70px">
                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlDay" runat="server" Width="70px">
                                    <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtDob" runat="server" Width="100px" style="padding:5px;font-size:14px;" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtDob" Format="dd-MMM-yyyy"></asp:CalendarExtender>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="btn" OnClick="btnGo_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsgDobValidation" runat="server" CssClass="error"></asp:Label>
                            </td>
                        </tr>
                    </table>                    
            </center>
            </div>
             </center>
         </div>
        <%----------------------------------------------------------------------------------------------------------------------------------%>
    </form>
    <script type="text/javascript">
        var Planning = false;
        var lastday = null;
        var drag = -1;
        var WH = parseFloat($("#lblWorkHourCount").html());
        
        var s_d = new Date($("#lblForDate").html().replace('-', ' ')).getDate();
        var c_d = new Date().getDate();
        

        $(document).ready(function () {
            
            //if (Date.parse($("#lblForDate").html()) > Date.now())
            if (s_d > c_d)
                Planning = true;


            $(".day>input").click(function () {
                $("#hfdid").val($(this).attr('id'));
                $("#btnpost").click();
            });

            //$(".timeslot").mousedown(function () {
            //    drag = $(this).attr('index');
            //    if ($(this).hasClass("rest")) {
            //        $(this).html("W");
            //        if (Planning)
            //            $(this).addClass("planning");
            //        else
            //            $(this).addClass("work");

            //        $(this).removeClass("rest");
            //        WH = WH + 0.5;
            //    }
            //    else {
            //        $(this).html("R");
            //        $(this).addClass("rest");
            //        $(this).removeClass("work").removeClass("planning");
            //        WH = WH - 0.5;
            //    }
            //    $("#lblWorkHourCount").html(parseFloat(WH).toFixed(1));
            //    $("#lblRestHourCount").html(parseFloat(24 - WH).toFixed(1));
            //});

            //$(".timeslot").mouseenter(function () {
            //    if (drag >= 0) {
            //        if ($(this).hasClass("rest"))
            //        {
            //            $(this).html("W");                       
            //            if (Planning)
            //                $(this).addClass("planning");
            //            else
            //                $(this).addClass("work");
            //            $(this).removeClass("rest");
            //            WH = WH + 0.5;
            //        }
            //        else
            //        {
            //            $(this).html("R");
            //            $(this).addClass("rest");
            //            $(this).removeClass("work").removeClass("planning");
            //            WH = WH - 0.5;
            //        }
            //        $("#lblWorkHourCount").html(parseFloat(WH).toFixed(1));
            //        $("#lblRestHourCount").html(parseFloat(24 - WH).toFixed(1));
            //    }
            //});

            //$(".timeslot,#content").mouseup(function () {
            //    drag = -1;
            //});
        });

        function ReadytoSave()
        {
            var log = "";
            $(".timeslot").each(function (i, o) {
                log += $(this).html();
            });

            //if ($('input[name=radStatus]:checked').val() == undefined)
            //{
            //    alert('Please select Planning or Final Log.')
            //    return false;
            //}
            //if ($('input[name=radStatus]:checked').val() == "P" && $("#hfUserType").val()=="user") {
            //    alert('Please select Final Log.')
            //    return false;
            //}


            //var indx = log.indexOf("W");
            //if (indx < 0) {
            //    alert('Please enter work log.')
            //    return false;
            //}
            $("#hfdSlots").val(log);
            return true;
        }
       
    </script>
</body>
</html>
