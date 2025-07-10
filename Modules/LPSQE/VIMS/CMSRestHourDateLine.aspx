<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMSRestHourDateLine.aspx.cs" Inherits="Vims_CMSRestHourDateLine" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipSoft- PMS - Rest Hour Entry</title>
    <script src="../eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function selectdate(ctlid) {
            $("#" + ctlid).prop("checked", true);
            $("#" + ctlid).parent().addClass("active");
        }
    </script>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
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
    </head>

<body style="font-size:13px; color:#333">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="text-align: center; padding:6px; background-color:#3f90ff;color:white; font-size:17px;">Allow Dates For Entry </div>
    <div style="text-align: center" id="content">
       
            <div style="text-align: center;background-color:#d4d1d1; padding:5px;">
            <b> Month & Year :  </b>
            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>
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
                     <td style="font-size:18px; text-align:left;font-weight:bold;width:180px;"><asp:Label runat="server" ID="lblForDate" Text="15-Mar-2016"></asp:Label></td>
                     

                     <td style="font-size:18px;text-align:right; width:150px; ">Ship Location : </td>
                     <td style="font-weight:bold; width:250px;">
                         <asp:RadioButtonList runat="server" ID="radLocation" RepeatDirection="Horizontal" Font-Size="18px">
                         <asp:ListItem Text="In Port" Value="I"></asp:ListItem>
                         <asp:ListItem Text="At Sea" Value="A"></asp:ListItem>
                         
                         </asp:RadioButtonList> 
                     </td>
                     <td style="font-size:18px;text-align:left; width:230px;display:none" id="datelineheader" runat="server">International Dateline :</td>
                     <td style="text-align:right; font-weight:bold;" id="datelinectl" runat="server"> 
                         <asp:RadioButtonList runat="server" ID="radDateLine" RepeatDirection="Horizontal" Font-Size="18px" style="display:none">
                         <asp:ListItem Text="None" Value="0"></asp:ListItem>
                         <asp:ListItem Text="-1" Value="-1"></asp:ListItem>
                         <asp:ListItem Text="+1" Value="+1"></asp:ListItem>
                         </asp:RadioButtonList> 
                     </td>
                 </tr>
            </table>
            <hr/>
             <div>
                <asp:Button runat="server" ID="btnsave" Text=" Save " CssClass="btn1" OnClientClick="ReadytoSave();" OnClick="btnsave_Click" />
                <asp:Button runat="server" ID="btnExport" Text=" Export " CssClass="btn1" OnClick="btnExport_Click" />
            </div>
           

            <div style="text-align:left; padding:5px;">
                <%--Any change in date line please click on the save button to save the record. --%>
                </div>
            <div style="text-align:left; padding:5px;">
                <asp:Label runat="server" ForeColor="Red" Font-Bold="true" ID="lblMsg" Text=""></asp:Label>
                </div>
        </div>

        
            
        </div>
        <div style="text-align:center;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <i>
                            <b>
                                Compliance for all current regulations (STCW 2010, the ILO MLC, US OPA 90 and OCIMF recommendations).
                                </b>
                            </i>
                    </td>
                </tr>
            </table>
            <br />
            <asp:LinkButton runat="server" id="dvmsglogs" Text="View Rest Hour Update Status" ForeColor="Red" Font-Size="18px" OnClick="dvmsglogs_Click">

            </asp:LinkButton>
            
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

           
        });

       
       
    </script>
</body>
</html>
