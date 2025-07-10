<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestHourEntry_N_Old.aspx.cs" Inherits="VIMS_RestHourEntry_N_Old" Trace="false" %>
<%--<%@ Register Src="~/UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>--%>
<%--<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="~/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipSoft- PMS - Rest Hour Entry</title>
    <%--<script src="../eReports/JS/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../JS/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function selectdate(ctlid) {
            $("#" + ctlid).prop("checked", true);
            $("#" + ctlid).parent().addClass("active");
        }
    </script>
    
    <%--<link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
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
            width:25px;
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
            background-image:url('../Images/checked-mark-yellow.png');
        }
        .saved
        {
            background-image:url('../Images/checked-mark-green.png');
        }
        .nc
        {
            background-color:#ff6868;
            color:white;
        }
        .today:before
        {
            content:"*";
            color:orange;
            font-weight:bold;
            position:relative;
            left:0px;
            top:0px;
        }
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
            text-se
        }
         .rest{
            background-color:forestgreen;
            color:white;
        }
          .work{
            background-color:brown ;
            color:white;
        }
    </style>
    <style type="text/css">
    table{border-collapse:collapse;}
    .borderd tr td{
            border:solid 1px #dddbdb;
            color:#333;
    }
    .header tr td{
        background-color:#4e4e4e;
        color:white;
    }
</style>  
    </head>

<body style="font-size:13px; color:#333">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="text-align: center; padding:8px; background-color:#3f90ff;color:white; font-size:17px;">Rest Hour Entry</div>
    <div style="text-align: center" id="content">
        <table style="margin:0 auto; width:100%;" cellpadding="5" border="1" class="bordered">
            <tr>
                <td style="text-align:right"><b> Crew Number : </b></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lblcrewnumber"></asp:Label> </td>
                <td style="text-align:right"><b> Crew Name : </b></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lblname"></asp:Label></td>
                <td style="text-align:right"><b> Sign On Date : </b></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lblfdt"></asp:Label></td>
                <td style="text-align:right"><%--<b> Relif Due Date : </b>--%></td>
                <td style="text-align:left"> <asp:Label runat="server" ID="lbltdt"></asp:Label></td>
            </tr>
        </table>
        
       
            <div style="text-align: center;background-color:#d4d1d1; padding:5px;">
            <b> Month & Year :  </b>
            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>

                <asp:Button ID="btnPrintWorkRestRecord" runat="server" Text="Print Work And Rest Record" OnClick="btnPrintWorkRestRecord_OnClick" CssClass="btn1" Width="200px" />
                <asp:Button ID="btnPrint" runat="server" Text="Print NC" OnClick="btnPrint_OnClick" CssClass="btn1" Width="100px" />
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
                     <td style="font-size:18px;text-align:left; width:230px;">International Dateline : </td>
                     <td style="text-align:left; font-weight:bold;"> <asp:Label runat="server" ID="lblidl" Font-Size="18px"  Font-Bold="true"></asp:Label></td>
                 </tr>
            </table>
            <div>
                <br />
            </div>
            <div unselectable="on" onselectstart="return false;" onmousedown="return false;">
                <asp:Literal runat="server" ID="litLog"></asp:Literal>   
            </div>
                <br />
                <asp:RadioButtonList runat="server" ID="radStatus" RepeatDirection="Horizontal" Font-Size="18px" Enabled="false" >
                         <asp:ListItem Text="Planning" Value="P"></asp:ListItem>
                         <asp:ListItem Text="Final Log" Value="S"></asp:ListItem>
                </asp:RadioButtonList> 
                <br />
            
            <div style="display:none;">
                <asp:Button runat="server" ID="btnsave" Text=" Save Log " CssClass="btn1" OnClientClick="return ReadytoSave();" OnClick="btnsave_Click" />
                 <asp:Button runat="server" ID="btnClear" Text=" Clear Log " CssClass="btn2" OnClientClick="return confirm('Are you sure to delete log for selected day?');" OnClick="btnClear_Click" />
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
                        <div style="padding:10px;">
                             <div style="border:solid 1px black; width:95%;overflow-x:hidden;overflow-y:scroll;">
                                <table width="100%" cellpadding="2" cellspacing="2" rules="all">
                                     <col  />
                                     <tr>
                                         <td><b>NC Type</b></td>
                                     </tr>
                                </table>
                             </div>
                            <div style="border:solid 1px black; width:95%;height:130px;overflow-x:hidden;overflow-y:scroll;">
                                 <table  width="100%"  cellpadding="2" cellspacing="2" rules="all">
                                     <col/>
                                              
                                <asp:Repeater ID="rptNC" runat="server">
                                    <ItemTemplate>                        
                                        <tr>
                                            <td><%#Eval("NCTypeName") %>  </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                     </table>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">
                             <div style="border:solid 1px black; width:95%;overflow-x:hidden;overflow-y:scroll;height:150px;">
                                 <table width="100%" cellpadding="2" cellspacing="2" rules="none">
                                     <tr>
                                         <td><b>Remarks :</b></td>
                                     </tr>
                                     <tr>
                                         <td>
                                             <asp:Label ID="txtRemarks" runat="server" TextMode="MultiLine" Width="99%" Height="75px"></asp:Label>
                                         </td>
                                         
                                     </tr>
                                     <tr style="display:none;">
                                         <td align="right">
                                             <asp:Button ID="btnUpdateRemarks" runat="server" Text="Update" OnClick="btnUpdateRemarks_OnClick" CssClass="btn1" Visible="false" />
                                         </td>
                                     </tr>
                                </table>
                             </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        
    <%-----------------------------------------------------------------%>
        
    <%-----------------------------------------------------------------%>

    <div style="position:fixed; bottom:0; width:100%; font-size:13px; padding:10px; background-color:#f3f6b2; font-weight:bold; ">
            
            <asp:Label runat="server" ForeColor="Red" Font-Bold="true" ID="lblMsg" Text=""></asp:Label>
            
            <table cellpadding="5" style="float:right;margin-right:10px;">
                <tr>
                    <td class="border planned dateline" style="height:15px;width:15px"></td>
                    <td style="vertical-align:middle;" >Work Hours Planned Only </td>
                    <td class="border saved dateline" style="height:15px;width:15px"> </td>
                    <td style="vertical-align:middle;">Work Hours Saved </td>
                    <td class="border nc dateline" style="height:15px;width:15px"> </td>
                    <td style="vertical-align:middle;">Non-Conformity Exists</td>
                    <td class="border today dateline" style="height:15px;width:15px"></td>
                    <td style="vertical-align:middle;">Today</td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        var lastday = null;
        var drag = -1;
        $(document).ready(function () {

            $(".day>input").click(function () {
                $("#hfdid").val($(this).attr('id'));
                $("#btnpost").click();
            });

            $(".timeslot").mousedown(function () {
                drag = $(this).attr('index');
                if ($(this).hasClass("rest")) {
                    $(this).html("W");
                    $(this).addClass("work");
                    $(this).removeClass("rest");
                }
                else {
                    $(this).html("R");
                    $(this).addClass("rest");
                    $(this).removeClass("work");
                }
            });

            $(".timeslot").mouseenter(function () {
                if (drag >= 0) {
                    if ($(this).hasClass("rest"))
                    {
                        $(this).html("W");                       
                        $(this).addClass("work");
                        $(this).removeClass("rest");
                    }
                    else
                    {
                        $(this).html("R");
                        $(this).addClass("rest");
                        $(this).removeClass("work");
                    }
                }
            });

            $(".timeslot,#content").mouseup(function () {
                drag = -1;
            });
        });

        function ReadytoSave()
        {
            var log = "";
            $(".timeslot").each(function (i, o) {
                log += $(this).html();
            });

            if ($('input[name=radStatus]:checked').val() == undefined)
            {
                alert('Please select Planning or Final Log.')
                return false;
            }


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
