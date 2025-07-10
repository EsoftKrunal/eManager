<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RFQDetailsForApproval.aspx.cs" Inherits="EditSpareRFQ" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Modules/Purchase/UserControls/ChangeAccountCode.ascx" TagName="Chagne" TagPrefix="Account" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />  
    <link href="../CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" >
        function ShowAccount() {
            document.getElementById('trAccode').style.display = '';
        }
        function fncInputNumericValuesOnly(evnt) {

            if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                event.returnValue = false;
            }
        }
        function CloseChangeAccountCode() {
            document.getElementById('trAccode').style.display = 'none';
        }
    </script>
    <script type="text/javascript" >
        function CloseWindow() 
        {
            window.opener.ReloadPage();
            window.close();
            return false ;
        }
    </script>
    <script language="javascript" type="text/javascript">
        //function for file name
        function FileName(Objsku) {


            if (IsFileChar(Objsku.value) == false) {
                alert('Invalid char for file name ');
                var objId = Objsku.id;
                var vals = document.getElementById(objId).value;
                //alert(vals.length);
                vals = vals.substring(0, vals.length - 1);
                document.getElementById(objId).value = vals;
                document.getElementById(objId).focus();
                return false;
            }

        }
        function IsFileChar(sText) {
            //alert('file char');
            var ValidChars = "1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM_-.";
            var IsNumber = true;
            var Char;


            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);

                if (ValidChars.indexOf(Char) == -1) {

                    IsNumber = false;
                }
            }
            return IsNumber;

        }
        //end file name function
        function numVal(Objsku) {
            if (IsNumeric(Objsku.value) == false) {
                var objId = Objsku.id;
                var vals = document.getElementById(objId).value;
                //alert(vals.length);
                vals = vals.substring(0, vals.length - 1);
                document.getElementById(objId).value = vals;
                document.getElementById(objId).focus();
                return false;
            }



        }
        function IsNumeric(sText) {
            var ValidChars = "0123456789.";
            var IsNumber = true;
            var Char;


            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    IsNumber = false;
                }
            }
            return IsNumber;

        }
        function do_totals1() {

            document.all.pleasewaitScreen.style.pixelTop = (document.body.scrollTop + 50);

            document.all.pleasewaitScreen.style.visibility = "visible";

            window.setTimeout('do_totals2()', 1);

        }
        function do_totals2() {

            lengthy_calculation();

            document.all.pleasewaitScreen.style.visibility = "hidden";

        }
        function lengthy_calculation() {
            var x, y
            for (x = 0; x < 1000000; x++) {
                y += (x * y) / (y - x);
            }
        }

        var currentPosition = 0;
        var newPosition = 0;
        var direction = "Released";
        var currentHeight = 0;
        var offX = 15;          // X offset from mouse position
        var offY = 15;          // Y offset from mouse position
        var divHeight;
        var ie5 = document.all && document.getElementById
        var ns6 = document.getElementById && !document.all
        function updatebox1(mouse, SKU) {

            //			var DivName='pop1'+SKU;
            var DivName = SKU;
            var divObj = document.getElementById(DivName)
            direction = "Pressed";
            //alert(DivName)
            currentPosition = mouse.clientY;
            document.getElementById(DivName).style.zIndex = 1;
            document.getElementById(DivName).style.display = "block";
            document.getElementById(DivName).style.backgroundColor = "#ffffff";
            document.getElementById(DivName).style.position = "absolute";


        }

        function SetDivPosition1(mouse, SKU) {


            var DivName = SKU;

            var divObj = document.getElementById(DivName);
            document.getElementById(DivName).style.display = "block";
            document.getElementById(DivName).style.backgroundColor = "#ffffff";
            document.getElementById(DivName).style.position = "absolute";

            document.getElementById(DivName).style.zIndex = 1;
            if ((parseInt(mouseX(mouse)) + offX) + 420 > window.screen.width)
                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX) - 420) + 'px';
            else
                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX)) + 'px';
            document.getElementById(DivName).style.top = ((parseInt(mouseY(mouse)) + offY) - 200) + 'px';



        }


        function HideBox1(evt, SKU) {

            var DivName = SKU;

            document.getElementById(DivName).style.display = "none";
        }
        //           -----------------------------------------------------------------
        function updatebox11(mouse, SKU) {

            //var DivName='pop11'+SKU;
            var DivName = SKU;

            var divObj = document.getElementById(DivName)
            direction = "Pressed";
            //alert(DivName)
            currentPosition = mouse.clientY;
            document.getElementById(DivName).style.zIndex = 1;
            document.getElementById(DivName).style.display = "block";
            document.getElementById(DivName).style.backgroundColor = "#ffffff";
            document.getElementById(DivName).style.position = "absolute";


        }

        function SetDivPosition11(mouse, SKU) {


            //			var DivName='pop11'+SKU;
            var DivName = SKU;
            var divObj = document.getElementById(DivName);
            document.getElementById(DivName).style.display = "block";
            document.getElementById(DivName).style.backgroundColor = "#ffffff";
            document.getElementById(DivName).style.position = "absolute";

            document.getElementById(DivName).style.zIndex = 1;
            if ((parseInt(mouseX(mouse)) + offX) + 420 > window.screen.width)
                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX) - 420) + 'px';
            else
                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX)) + 'px';
            document.getElementById(DivName).style.top = ((parseInt(mouseY(mouse)) + offY) - 200) + 'px';



        }


        function HideBox11(evt, SKU) {

            //			var DivName='pop11'+SKU;
            var DivName = SKU;
            document.getElementById(DivName).style.display = "none";
        }


        function mouseX(evt) { if (!evt) evt = window.event; if (evt.pageX) return evt.pageX; else if (evt.clientX) return evt.clientX + (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft); else return 0; }
        function mouseY(evt) { if (!evt) evt = window.event; if (evt.pageY) return evt.pageY; else if (evt.clientY) return evt.clientY + (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop); else return 0; }
    </script>

    <style type="text/css">
        .towTab {
            font-size: 13px;
    font-weight: bolder;
    display: block;
    background-color: #5f6a75;
    color: white;
    padding: 7px;
        }
        .bottom-strip
        {
            position:fixed;
            width:100%;padding:5px;
            background-color:#ffd89d;
            left:0;bottom:0;
        }
    </style>
    <style type="text/css">
        
        .rpt-table1 td {
            border:none;
        }
    .imgMajor{
    background-image:url('Images/arrow-right.png');
    background-repeat:no-repeat;
    background-position:center;
    width: 16px;
    height: 16px;
      }
      .arrow-down{
          background-image:url('Images/arrow-down.png');
          background-repeat:no-repeat;
          background-position:center;
          width: 16px;
          height: 16px;
      }
    
    *
    {
        box-sizing:border-box;
    }
    .tblMonth td{
        text-align:center;
        font-weight:bold;
    }
    .Budgeted {
         background-color: #148a1a;
    border: 0px solid #a0f161;
    display: inline-block;
    padding:4px 7px;
    text-align:center;
    color:white;
    }
    .UnBudgeted {
    background-color: #f63f2d;
    border: 0px solid #a0f161;
    display: inline-block;    
    padding:4px 7px;
    text-align:center;
    color:white;
    }
        .green {
            background-color:#148a1a;
        }
        .red {
            background-color:#f63f2d;
        }
    /*.btn
    {
        background-color:#394b65;
        color:White;
        border:none;
        padding:5px 10px 5px 10px;        
    } */   
    .btn:hover
    {
        background-color:#0052CC;
    }
    .right_align
    {
        text-align:right;
    }
    .columnHead
    {
        background-color:#c2c2c2;
        height:50px;
        color:Black;
    }
    .newinput
    {
        border:solid 1px #c2c2c2;
        font-size:12px;
        padding:2px;
        text-align:right;
    }
    .nav
    {
        margin:0px;
        padding:0px;
        width:100%;
    }
    .inactive_tab
    {
        padding:4px;
        background-color:White;
        border:solid 1px #4D70DB;
        border-bottom:solid 1px white;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        color:Black;
    }
     .inactive_tab a
    {
        color:Grey;
    }
    .active_tab
    {
        padding:4px;
        background-color:#4D70DB;
        border:solid 1px #4D70DB;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        border-bottom:solid 1px #4D70DB;
    }
     .active_tab a
    { color:White; }
    .bl
    {   border-left:solid 1px #dddddd;height:25px;}
    .br
    {   border-right:solid 1px #dddddd;height:25px;}

    
    input[type='text'],select
    {
        line-height:20px;
        height:20px;
        padding-left:5px;
        border:solid 1px #c2c2c2;
        vertical-align:middle;
    }
    
    td{
        vertical-align:middle;
    }
    .table-centered tr td
    {
        text-align:center !important;
    }
    .red{
        background-color:#f38585;
    }

    
    </style>
    <style type="text/css">
        .rpt-table-task td {
            font-size:9px !important;
        }
        .saveDiv {
            position:fixed;display:block;width:550px;background-color:#f3ee57;right:0px;top:0px;padding:25px;
        }
            .saveDiv span {
                font-size:18px;color:#5f5f5f;padding:20px;display:block
            }
            .saveDiv input {
                font-size:16px;color:white;padding:5px;width:100px;color:#5f5f5f;font-weight:bold;
            }
        .auto-style1 {
            float: right;
        }
    </style>

    <script type="text/javascript">       
        var divStauts = 'C';
        //console.log("Status", divStauts);
        //alert(divStauts);
        $(document).ready(function () {
            //var divStauts = $('#hdnStatus').val();
            //alert(divStauts);
            $(".qtyPo").change(function () {       
                if (divStauts == 'C') {
                    $(".saveDiv").slideDown(500);
                    divStauts = 'O';
                   // alert(divStauts);
                }
                {
                    var AllEqual = true;
                    $(".qtyPo").each(function (i, o) {
                        var cv = $(o).val();
                        var ov = $(o).next().html();
                        if (cv != ov) {
                            AllEqual = false;
                        }
                        //console.log("cv", cv);
                        //console.log("ov", ov);
                    });
                    if (AllEqual) {
                        $(".saveDiv").slideUp(500);
                        divStauts = 'O';
                       // alert(divStauts);
                    }
                }
            })
        })     
    </script>
    <script type="text/javascript">
        function OpenDocument(TableID, PVNo) {
            window.open("ShowDocuments.aspx?DocId=" + TableID + "&PVNo=" + PVNo + "&PRType=''");
        }
    </script>
</head>
<body style="font-size:13px;margin-bottom:30px;"  >
    <%-- onbeforeunload="RefreshBack();" --%>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager> <asp:HiddenField ID="hdnStatus" runat="server" Value=""  />
       <%-- <asp:Button ID="btnRefreshRFQ" runat="server" style="display:none;" OnClick="btnRefreshRFQ_Click" />--%>
    <div>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up22" ID="UpdateProgress1">
        <ProgressTemplate>
            <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                <center>
                <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                <img src="Images/loading.gif" alt="loading"> Loading ...
                </div>
                </center>
            </div>
        </ProgressTemplate> 
     </asp:UpdateProgress> 
             
    <asp:UpdatePanel ID="RptUpdatepanel" runat="server">
    <ContentTemplate>
        <div>
            <table cellpadding="0" cellspacing="0" width="100%" border="0"  style ="border-collapse : collapse" >
                <colgroup>
                    <col width="200px" />
                    <col/>
                   
                    <tr>
                        <td class="text headerband">
                           <b> View/Approve Quote</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <table cellpadding="4" cellspacing="0" width="100%" style ="border-collapse : collapse;" class="bordered">
                           <tr>
                           <td style="text-align :right; width:180px;">Quotation No. :</td>
                           <td style="text-align :left"><asp:Label ID="lblRFQNO" runat="server" Font-Bold="true"></asp:Label></td>
                           <td style="text-align :right">VSL Req No. :</td>
                           <td style="text-align :left"> <asp:Label ID="lblReqNo" runat="server" Font-Bold="true"></asp:Label></td>
                           <td style="text-align :right">Request Type :</td>
                           <td style="text-align :left"><asp:Label ID="lblReqType" runat="server" Font-Bold="true"></asp:Label></td>
                          
                           <td style="text-align :right">REQN Date :</td>
                           <td style="text-align :left"><asp:Label ID="lblDateCreated" runat="server" Font-Bold="true"></asp:Label></td>
                                <td style="text-align :right">Created By :</td>
                           <td style="text-align :left"><asp:Label ID="lblCreatedBy" runat="server" Font-Bold="true"></asp:Label></td>
                           </tr>
                           <tr>
                           <td style="text-align :right">Vendor Name :</td>
                           <td style="text-align :left" colspan="5"><asp:Label ID="lblVenderName" runat="server" Font-Bold="true"></asp:Label>
                               <span style="text-decoration:underline; font-weight:bold;color:red;">Show Contact Details</span>

                           </td>
                           <td style="text-align :right">Delivery Port & Date :</td>
                           <td style="text-align :left" colspan="3"> <asp:Label ID="lblPortNDate" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblPoDesc" runat="server" Text=""></asp:Label></td>
                           </tr>
                               <tr>
                           <td style="text-align :right">Vendor Comments :</td>
                           <td style="text-align :left" colspan="9"><asp:Label ID="lblVenComments" runat="server" ForeColor="Red" Font-Size="12px" Font-Italic="true"></asp:Label></td>
                           </tr>
                                <%--<tr >
                           <td style="text-align :right">Comments for SMD :</td>
                           <td style="text-align :left" colspan="9"><asp:Label ID="lblSmdComments" runat="server" ForeColor="Red" Font-Size="12px" Font-Italic="true"></asp:Label></td>
                           </tr>
                                <tr>
                           <td style="text-align :right">Comments for Purchaser :</td>
                           <td style="text-align :left" colspan="9"><asp:Label ID="txtVenderComm" runat="server" ForeColor="Red" Font-Size="12px" Font-Italic="true"></asp:Label></td>
                           </tr>--%>
                               
                           </table> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" rules="all" width="100%">
                                <colgroup>
                                    <col width="25%" />
                                    <col />
                                    <tr>
                                        <td style="vertical-align:top;" valign="top">
                                            <div class="box" style="display:none;">
                                                <div class="boxheader">
                                                    Vendor Contact Details &amp; Comments</div>
                                                <div style="background-color:#e3ffde">
                                                    <table border="0" cellpadding="3" cellspacing="0" class="bordered" style="border-collapse : collapse" width="100%">
                                                        <colgroup>
                                                            <tr>
                                                                <td><b>Contact Details </b></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="lblVenContact" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </colgroup>
                                                    </table>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="up22" runat="server">
                                                <ContentTemplate>
                                                    <div id="dvBudget" runat="server" style="border:solid 0px red;">
                                                        <div class="box" style="vertical-align:top;">
                                                            <div class="boxheader">
                                                                Budget Summary ( US$)</div>
                                                            <div style="background-color:#feffb9">
                                                                <div>
                                                                    <table cellpadding="3" cellspacing="0" class="bordered" style="border-collapse : collapse" width="100%">
                                                                        <colgroup>
                                                                            <col width="120px" />
                                                                            <col />
                                                                            <tr style=" font-weight :bold ">
                                                                                <td>Account </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAccountName" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Ann. Budget </b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAnnualBudget" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>YTD Consumed </b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblConsumedDate" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Remaining </b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblBudgetRem" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Utilization (%)</b> </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblUtilization" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="vertical-align:middle;"><%--<div onclick="ShowAccount();" style="cursor:pointer; color:red; text-decoration:underline; font-weight:bold; text-align:center;">.</div>--%>
                                                                                    <asp:LinkButton ID="lnkChangeAccountCode" runat="server" ForeColor="Red" OnClick="lnkChangeAccountCode_OnClick" Text="( Change Account Code )" Visible="false"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </colgroup>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="box">
                                                <div class="boxheader">
                                                    Spare Information</div>
                                                <div style="background-color:#ffd89d">
                                                    <table border="0" cellpadding="3" cellspacing="0" class="bordered" style="border-collapse : collapse" width="100%">
                                                        <colgroup>
                                                            <col width="40%" />
                                                            <col width="60%" />
                                                            <tr>
                                                                <td style="font-weight:bold">Equip Name </td>
                                                                <td>
                                                                    <asp:Label ID="lblEquipName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight:bold">Model/Type </td>
                                                                <td>
                                                                    <asp:Label ID="lblModelType" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight:bold">Serial# </td>
                                                                <td>
                                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight:bold">Year Built </td>
                                                                <td>
                                                                    <asp:Label ID="lblBuiltYear" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="font-weight:bold">Maker&#39;s name and Address </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblNameAddress" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </colgroup>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="box" style="display:none;">
                                                <div class="boxheader">
                                                    Comments for analysis</div>
                                                <div>
                                                    <asp:TextBox ID="txtQuarComm" runat="server" Height="60px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="box">
                                                <div class="boxheader">
                                                    Currency &amp; Exchange
                                                </div>
                                                <div>
                                                    <table border="0" cellpadding="3" cellspacing="0" class="bordered" style="border-collapse : collapse" width="100%">
                                                        <colgroup>
                                                            <col width="140px" />
                                                            <col />
                                                            <tr style="background-color : #E0FFFF;">
                                                                <td><b>Local Currency </b></td>
                                                                <td>
                                                                    <asp:Label ID="lblLocalCurr" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color : #FAEBD7;">
                                                                <td><b>Exch. Rate </b></td>
                                                                <td>
                                                                    <asp:Label ID="lblCurrRate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color : #D8BFD8;">
                                                                <td><b>Exch. Date </b></td>
                                                                <td>
                                                                    <asp:Label ID="lblBidExchDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </colgroup>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="box" style="padding:6px; background-color:#ffffff;color:#333;">
                                                <asp:CheckBox ID="ChkBreakdown" runat="server" AutoPostBack="true" OnCheckedChanged="ChkBreakdown_OnCheckedChanged" Text=" Breakdown/Unbudgted " Visible="false" />
                                                &nbsp;<asp:Label ID="lblBreakdown" runat="server" Font-Bold="true" Font-Size="Larger" Text=""></asp:Label>
                                            </div>
                                        </td>
                                        <td valign="top">
                                            <div class="text headerband" style="height :20px;">
                                                Item List for Quotation
                                            </div>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                    <col style="width:4%;" />
                                                    <%--<col style="width:50px;" />--%>
                                                    <col />
                                                    <col style="width:8%;" />
                                                    <col style="width:6%;" />
                                                    <col style="width:9%" />
                                                    <col style="width:6%" />
                                                    <col style="width:5%" />
                                                    <col style="width:10%" />
                                                    <col style="width:6%" />
                                                    <col style="width:8%" />
                                                    <col style="width:6%" />
                                                    <col style="width:9%" />
                                                    <col style="width:6%" />
                                                    <col style="width:2%" />
                                                </colgroup>
                                                <tr align="left" class= "headerstylegrid">
                                                    <td>S.No.</td>
                                                    <%--<td>REF#</td>--%>
                                                    <td>Description</td>
                                                    <td>Part#</td>
                                                    <td>Bid Qty</td>
                                                    <td>PO Qty</td>
                                                    <td>UOM</td>
                                                    <td>ROB</td>
                                                    <td>Unit Price(LC)</td>
                                                    <td>GST/Tax (%)</td>
                                                    <td>Total (LC)</td>
                                                    <td>GST/Tax (LC)</td>
                                                    <%--Ext Price--%>
                                                    <td>Total ($)</td>
                                                    <td>GST/Tax ($)</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <div id="tbl_Spares" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 380px ; text-align:center;">
                                                <table border="0" cellpadding="4" cellspacing="0" class="bordered" style="width:100%;border-collapse:collapse;">
                                                    <colgroup>
                                                       <col style="width:4%;" />
                                                    <%--<col style="width:50px;" />--%>
                                                    <col />
                                                    <col style="width:8%;" />
                                                    <col style="width:6%;" />
                                                    <col style="width:9%" />
                                                    <col style="width:6%" />
                                                    <col style="width:5%" />
                                                    <col style="width:10%" />
                                                    <col style="width:6%" />
                                                    <col style="width:8%" />
                                                    <col style="width:6%" />
                                                    <col style="width:9%" />
                                                    <col style="width:6%" />
                                                   
                                                   <%-- <col style="width:2%" />--%>
                                                    </colgroup>
                                                    <asp:Repeater ID="rptItems" runat="server">
                                                        <ItemTemplate>
                                                            <tr id='tr<%#Eval("recid")%>'>
                                                                <td>
                                                                    <asp:Label ID="lblRowNumber" runat="server"></asp:Label>
                                                                    <%--<%# Eval("Sno")%>--%></td>
                                                                <%--<td><%# Eval("REFNumber")%></td>--%>
                                                                <td style="text-align :left"><span title='Part#:<%# Eval("PartNo")%>, Drawing#:<%# Eval("EquipItemDrawing")%>, Code#:<%# Eval("EquipItemCode")%>'><%# Eval("BidDescription")%></span></td>
                                                                <td style="text-align :left"><%# Eval("PartNo")%></td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="lblBidQty" runat="server" Text='<%# Eval("bidqty")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hfItemID" runat="server" Value='<%#Eval("BidItemID") %>' />
                                                                    <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%#Eval("UnitPrice") %>' />
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:TextBox ID="txtOrderQTY" runat="server" CssClass="qtyPo" MaxLength="10" onkeypress="numVal(this)" onkeyup="numVal(this)" style="text-align:right;" Text='<%# Eval("qtyPo")%>' Width="65px" ></asp:TextBox> 
                                                                    <%--OnTextChanged="txtOrderQTY_OnTextChanged"--%>
                                                                    
                                                                    <asp:Label ID="lblOrderQTY" runat="server" style="display:none;" Text='<%# Eval("qtyPo")%>' Width="70px"></asp:Label>
                                                                </td>
                                                                <td><%# Eval("UOM")%></td>
                                                                <td style="text-align:right;"><%# Eval("Qtyob")%></td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice") %> '></asp:Label>
                                                                </td>
                                                                 <td style="text-align:right;">
                                                                    <asp:Label ID="lblGSTPer" runat="server" Text='<%# Eval("GSTTaxPercentage") %> '></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="lblLC" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("LCPoTotal"))%>'></asp:Label>
                                                                </td>
                                                                 <td style="text-align:right;">
                                                                    <asp:Label ID="lblGSTLC" runat="server" Text='<%# Eval("GSTTaxAmtLC") %> '></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">$&nbsp;<asp:Label ID="lblUsd" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("UsdPoTotal"))%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="lblGSTUsd" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtUSD"))%>'></asp:Label>
                                                                </td>
                                                               <%--<td></td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                            <div class="saveDiv" style="padding:5px;text-align:center;display:none;">
                                                <span>Please click save button to save the changes </span>
                                                <div style="text-align:center;">
                                                    <asp:Button ID="btnSavePoQuantity" runat="server" OnClick="btnSavePoQuantity_OnClick" Text="Save" />
                                                </div>
                                            </div>
                                            <div>
                                                <table border="0" cellpadding="3" cellspacing="0" style="border-collapse : collapse" width="100%" class="bordered">
                                                    <colgroup>
                                                     <col style="width:4%;" />
                                                    <%--<col style="width:50px;" />--%>
                                                    <col />
                                                    <col style="width:8%;" />
                                                    <col style="width:6%;" />
                                                    <col style="width:9%" />
                                                    <col style="width:6%" />
                                                    <col style="width:5%" />
                                                    <col style="width:10%" />
                                                    <col style="width:6%" />
                                                    <col style="width:8%" />
                                                    <col style="width:6%" />
                                                    <col style="width:9%" />
                                                    <col style="width:6%" />
                                                        </colgroup>
                                            <tr>
                                                <td colspan="7"></td>
                                                <td colspan="2"><b>Est&#39;d Shpg(LC) </b></t>
                                                <td >
                                                    <asp:TextBox ID="txtLC" runat="server" AutoPostBack="true" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)" OnTextChanged="txtLC_OnTextChanged" Width="70px"></asp:TextBox>
                                                </td>
                                                 <td></td>
                                                <td>
                                                     <asp:Label ID="lblUSD" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                                <tr>
                                                <td colspan="7"></td>
                                                <td colspan="2"><b>GST/Tax Total </b></td>
                                                <td >
                                                    <asp:Label ID="lblGSTLC" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <asp:Label ID="lblGSTUsd" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                                   
                                            </tr>
                                            <tr>
                                                <td colspan="7"></td>
                                                <td colspan="2"><b>Discount in %  </b></td>
                                                <td >
                                                    <asp:TextBox ID="txtDiscountPer" runat="server" AutoPostBack="true" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)" OnTextChanged="txtDiscountPer_OnTextChanged" Width="70px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>
                                                      <asp:Label ID="lblTotalDiscountUSD" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                                
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="7">
                                                    <asp:LinkButton ID="imgResetValues" runat="server" OnClick="imgResetValues_Click" Text="Update Amount" CssClass="auto-style1" Height="16px" Visible="false"></asp:LinkButton>
                                                </td>
                                               
                                                    <td colspan="2"><b>Total LC ( <asp:Label ID="lblLC" runat="server"></asp:Label>) </b></td>
                                               
                                                <td >
                                                    <asp:Label ID="lblTotalLCD" runat="server"></asp:Label>
                                                </td>
                                                <td ><b>Total(US$) </b></td>
                                                <td >
                                                    <asp:Label ID="lblTotalUSdD" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                                
                                            </tr>
                                        
                                        
                                   </table>
                                            </div>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>

                            

                        </td>
                    </tr>
                </colgroup>
            </table> 
            <table cellpadding="0" cellspacing="0" width="100%" border="0"  style ="border-collapse : collapse" >
                <colgroup>
                    <col width="160px;" />
                    <col />
                    <tr>
                        <td colspan="2" style="border-right:solid 1px #C2C2C2;"><span class="towTab">Sent for approval By Purchaser </span></td>
                    </tr>
                    <tr>
                        <td style="padding:3px;"><b>Sent By/On :</b></td>
                        <td>
                            <asp:Label ID="lblApprovalrequestByOn" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:3px;"><b>Purchaser Comments :</b></td>
                        <td>
                            <asp:Label ID="lblApprovalrequestComments" runat="server"></asp:Label>
                        </td>
                    </tr>
                </colgroup>
            </table>
            <table cellpadding="0" cellspacing="0" width="100%" border="0"  style ="border-collapse : collapse" >

                <tr>
                    <td style="border-right:solid 1px #C2C2C2;"> <span class="towTab"> Approval Summary </span></td>
                    <td style="width:350px;border-right:solid 1px #C2C2C2;"> <span class="towTab"> Amount Summary </span></td>
                </tr>
                <tr>
                    <td>

                        <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;" class="bordered">
                            <colgroup>
                                <col width="90px" />
                                <col width="160px" />
                                <col width="100px" />
                                <col />
                                <col width="130px;"/>
                                <col width="40px" />
                                <col width="100px" />
                                <tr align="left" class= "headerstylegrid">
                                    <td>Status</td>
                                    <td>Approved By</td>
                                    <td>Approved On</td>
                                    <td>Comments</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                              </colgroup>
                           
                    <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" class="bordered">
                         <colgroup>
                             <col width="90px" />
                             <col width="160px" />
                             <col width="100px" />
                             <col />
                             <col width="130px;"/>
                             <col width="40px" />
                             <col width="100px" />
                             <tr id="tr1">
                                 <td>Approval 1 </td>
                                 <td>
                                     <asp:Label ID="lblApprovalName_1" runat="server" ForeColor="Red" Text="Not Required"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledOn_1" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledComments_1" runat="server"></asp:Label>
                                 </td>
                                 <td>

                                 </td>
                                 <td style="text-align:center;">
                                     <asp:Button ID="btnSendBackToPurchaser_1" runat="server" OnClick="btnSendBackToPurchaserPopup_OnClick" style="background-image: url(../../HRD/Images/reset.png); background-repeat: no-repeat; width: 23px" ToolTip="Send Back to Purchaser" />
                                 </td>
                                 <td><%--<asp:ImageButton ID="imgDelete" runat="server" OnClick="btnSendBackToPurchaserPopup_OnClick" ImageUrl="~/Images/reset.png"  title="Send back to purchaser"/>--%>
                                     <asp:Button ID="btnApprovePO_1" runat="server" Enabled="false" OnClick="lnlApprovePo_1_Request_Popup_OnClick" Text="Approve PO"  />
                                 </td>
                             </tr>
                             <tr id="tr2">
                                 <td>Approval 2 </td>
                                 <td>
                                     <asp:Label ID="lblApprovalName_2" runat="server" ForeColor="Red" Text="Not Required"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledOn_2" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledComments_2" runat="server"></asp:Label>
                                 </td>
                                 <td></td>
                                   <td></td>
                                 <td>
                                     <asp:Button ID="btnApprovePO_2" runat="server" Enabled="false" OnClick="lnlApprovePoRequest_Popup_OnClick" Text="Approve PO"  />
                                 </td>
                             </tr>
                             <tr id="tr3">
                                 <td>Approval 3 </td>
                                 <td>
                                     <asp:Label ID="lblApprovalName_3" runat="server" ForeColor="Red" Text="Not Required"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledOn_3" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledComments_3" runat="server"></asp:Label>
                                 </td>
                                 <td></td>
                                  <td></td>
                                 <td>
                                     <asp:Button ID="btnApprovePO_3" runat="server" Enabled="false" OnClick="lnlApprovePoRequest_Popup_OnClick" Text="Approve PO" />
                                 </td>
                             </tr>
                             <tr id="tr4">
                                 <td>Approval 4 </td>
                                 <td>
                                     <asp:Label ID="lblApprovalName_4" runat="server" ForeColor="Red" Text="Not Required"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledOn_4" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledComments_4" runat="server"></asp:Label>
                                 </td>
                                 <td></td>
                                  <td></td>
                                 <td>
                                     <asp:Button ID="btnApprovePO_4" runat="server" Enabled="false" OnClick="lnlApprovePoRequest_Popup_OnClick" Text="Approve PO"  />
                                 </td>
                             </tr>
                             <tr id="tr5">
                                 <td>Issue PO </td>
                                 <td>
                                     <asp:Label ID="lblApprovalName_5" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledOn_5" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblApprovaledComments_5" runat="server"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="ddlPOAccountCompany" runat="server" Width="120px" Visible="false"></asp:DropDownList>
                                 </td>
                                 <td style="text-align:center;">
                                     <asp:Button ID="btnSendMail" runat="server" OnClick="btnSendMail_OnClick" style="background-image: url(../../HRD/Images/email.png); background-repeat: no-repeat; width: 23px" ToolTip="Send Mail" Visible="false" />
                                 </td>
                                 <td><%--<asp:Button ID="btnApprovePO_5" runat="server" Visible="false"  Text="Approve PO" OnClick="lnlApprovePoRequest_Popup_OnClick" ></asp:Button>                                                --%>
                                     <asp:Button ID="imgPlaceOrder" runat="server" onclick="imgPlaceOrder_OnClick" Text="Place Order"  />
                                 </td>
                             </tr>
                         </colgroup>
                </table>  
                                
                            </table>
                        <%-----------------------------------------------------------%>
                        <div id="dvApprovalList" runat="server" style="border:solid 1px #C2C2C2;margin:0px;padding:0px;display:none;">
                <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col width="130px" />
                        <col width="200px" />
                        <col width="100px" />
                        <col />
                        <tr align="left" class= "headerstylegrid">
                            <td>Status</td>
                            <td>Assigned To</td>
                            <td>Approved On</td>
                            <td>Comments</td>
                        </tr>
                    </colgroup>
                </table>
                <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;" class="bordered">
                    
                    <colgroup>
                        <col width="130px" />
                        <col width="200px" />
                        <col width="100px" />
                        <col />
                    </colgroup>
            </table>
                            <asp:Repeater ID="rptApprovalList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("ApprovalPhase")%></td>
                                        <td><%#Eval("Name")%></td>
                                        <td><%# Common.ToDateString(Eval("ApprovedOn"))%></td>
                                        <td><%#Eval("Comments")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <table>
                            </table>
        </div>
                    </td>
                    <td style="vertical-align:top;">
                        
                            
 <div style='text-align:center;padding:10px;'>

                            

                        </div>
                    </td>
                </tr>
            </table>
            
            <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                    <colgroup>
                        <col/>
                        <col width="400px;" />
                        <tr>
                        <td>
                        
                        
                         
                        </td>
                        <td style=" text-align :right ; vertical-align :top" >
                        <asp:Label ID="lblmsg" runat="server" CssClass="error"></asp:Label>
                        <asp:ImageButton  id="imgSaveSpareRFQ" runat="server" ImageUrl="~/Modules/HRD/Images/save.jpg" Visible="false"    />
                        <%--<asp:ImageButton ID="imgApp1" runat="server"  ImageUrl="~/Images/approval1.jpg" onclick="btnApp_OnClick" />
                        <asp:ImageButton ID="imgApp2" runat="server"  ImageUrl="~/Images/approval2.jpg" onclick="btnApp_OnClick" />
                        <asp:ImageButton ID="imgPlaceOrder" runat="server"  ImageUrl="~/Images/placeorder.jpg" onclick="btnApp_OnClick" />--%>
                        
                        </td>
                        </tr>
                        
                    </colgroup>
                </table> 
            
            <div class="bottom-strip">
                <div style="width:450px;float:left;color:red;">
                    <asp:Label ID="lblMsgBS" runat="server" Text="" Font-Bold="True" ></asp:Label>
                </div>
                <table cellpadding="0" cellspacing="3" border="0" style="float:right;margin-right:10px;">
                    <tr>
                        <td>
                             <span>
        <asp:ImageButton id="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents"/> 
    (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>) 
    </span> &nbsp;&nbsp;
                            <asp:Button runat="server" Text="Send Back to Approval-1 Stage" id="btnMakeQuote" Visible="false" OnClick="btnMakeQuote_Click" onClientClick="return window.confirm('Are you sure to send back to Approval-1 stage ?');" CssClass="btn" />
                            <asp:Button ID="btnQuoteAnalyzer" runat="server"  onclick="btnQuoteAnalyzer_OnClick" Text="Quote Analyzer" CssClass="btn"   />

                            <asp:Button ID="imgApp1" runat="server"  onclick="btnApp_OnClick" Text="Approval 1" style="display:none;" />
                            
                        </td>
                        <td>
                            
                            <asp:Button ID="imgApp2" runat="server"  onclick="btnApp_OnClick" Text="Approval 2" style="display:none;" />
                        </td>
                        
                        <td>
                            
                        </td>
                        
                        <td>
                            <asp:Button  id="imgCancel" runat="server" Text="Close" OnClientClick="window.close();" CssClass="btn"/>
                        </td>
                        <td style="display:none;">
                            <asp:Button ID="btnSendBackToPurchaserPopup" runat="server"  onclick="btnSendBackToPurchaserPopup_OnClick" Text="Send back to purchaser"  CssClass="btn" />
                            <asp:Button ID="lnlApprovePoRequest_Popup" runat="server" Visible="false" Text="Approve PO" OnClick="lnlApprovePoRequest_Popup_OnClick"  CssClass="btn" ></asp:Button>                            
                        </td>
                    </tr>
                </table>
                
                
                
                

                

                



                <table ID="tblApp1" runat="server" border="0" cellpadding="3" cellspacing="0"  width="100%" style="font-weight:bold;display:none">
                                                    <colgroup>
                                                        <tr>
                                                            <td style="text-align :right; padding-right:3px;">
                                                                <asp:Label ID="lblApp1Name" runat="server"> </asp:Label>&nbsp;&nbsp;&nbsp;/
                                                            </td>
                                                            <td style="text-align :left; padding-left:5px;">
                                                                <asp:Label ID="lblApp1On" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </colgroup>
                                                </table>
                <table ID="tblApp2" runat="server" border="0" cellpadding="3" cellspacing="0"  width="100%" style="font-weight:bold;display:none;">
                                                    <colgroup>
                                                        <tr>
                                                            <td style="text-align :right ; padding-right:3px;">
                                                                <asp:Label ID="lblApp2Name" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;/
                                                            </td>
                                                            <td style="text-align :left; padding-left:5px;">
                                                                <asp:Label ID="lblApp2On" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </colgroup>
                                                </table>
            </div>

            <%--//-----------------------------------------------------------------------------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvApprovalWindow" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:650px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
        <center>
            
        <div style=" float :right " ><asp:ImageButton runat="server" ID="btnApprovePoRequest_ClosePopup" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." onclick="btnApprovePoRequest_ClosePopup_OnClick"/> </div>
        <table border="0" cellpadding="6" cellspacing="0" style=" text-align: left; border-collapse:collapse; width:100%;">
                       
                <tr style="text-align:center;" class="text headerband">
                        <td >
                            <asp:Label ID="lblApprovalPhase" runat="server" style="font-size:16px;font-weight:bold;" ></asp:Label>
                        </td>
                </tr>
                    <tr id="trApprovalReasionLable" runat="server" visible="false">
                            <td>
                                <b>Reason for selecting Higher price quotation :</b>
                            </td>
                        </tr>
                    <tr id="trApprovalReasionControl" runat="server" visible="false">
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlApprovalReasion" runat="server">
                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Consolidated with another supplier"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Logistics more cost/time efficient"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Product quality not meeting requirements/OEM"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Incomplete quote"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b> Remarks :</b>
                        </td>
                    </tr>
                       <tr >
                              <td style="text-align: left;">
                                  <asp:TextBox ID="txtVerifyComments" runat="server"  Width="99%" TextMode="MultiLine" Height="120px"></asp:TextBox>
                              </td>
                       </tr>
                        <tr >
                              <td align="right" style="text-align: right; padding-right:15px; width:100px;" >
                                  <asp:Label ID="lblMsgPoApproval" runat="server" CssClass="error"></asp:Label>
                                <asp:Button ID="btn_VerifySave" runat="server" Text="Save" Width="80px" OnClick="btn_VerifySave_Click" ValidationGroup="verify" style="  border:none; padding:4px;" CssClass="btn" />
                                
                              
                            </td>
                        </tr>
                      </table>              
        

        </center>    
        </div>
        </center>
            </div>

            <%------------------------------%>
             <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;display:none;" runat="server"  id="trAccode"  >
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:450px; height:120px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">

                    <center>
                         <div id="tr2_old" style="margin-top:0px;" >
                             <div style="background-color:#E6F3FC;font-size:16px;font-weight:bold;padding:5px;margin-top:0px;">
                                 Change Account Code
                                 <img ID="ImageButton12" src="../../HRD/Images/Close.gif"  title="Close this Window." onclick="CloseChangeAccountCode()" style="float:right;" />
                             </div>
                                <table width="100%" style="margin-top:10px;" >
                                    <tr>
                            <td>
                                <b>New Acct. Code :</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAccCode" runat="server"  ></asp:DropDownList>            
                            </td>
                                    </tr>
                                    <tr>
                            <td colspan="2" style="text-align:center;">
                                <asp:Button ID="btnSaveAccCode" runat="server" style="background-color:Orange;color:White;border-color:White;border-collapse:collapse; font-weight:bold; vertical-align:middle; font-size:12px;" Text="Change" Width="80px" Height="21px"  OnClick="btnSaveAccCode_OnClick" OnClientClick="return confirm('Are you sure to change the account code ?');"/>
                            </td>
                            </tr>
                            </table>
                        </div>
                    </center>
                        </div>
                </center>
                </div>
            <%------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="DvSendBackToPurchaser" visible="false"  >
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:450px; height:250px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                    <center>
                        <div class="text headerband">
                            Send Back To Purchaser
                            <asp:ImageButton ID="btnCloseSendBackToPurchaser" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="btnCloseSendBackToPurchaser_OnClick" />
                        </div>     
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td> Comments for purchaser   </td>
                            </tr>
                            <tr>
                                <td> 
                                    <asp:TextBox ID="txtPurchaserComments" runat="server"  TextMode="MultiLine" Width="96%" Height="120px"> </asp:TextBox>

                                </td>
                            </tr>                            
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="btnSendBackToPurchaser" runat="server" OnClick="btnSendBackToPurchaser_OnClick" Text=" Save " CssClass="btn" />
                                    <%--OnClientClick="return confirm('Are your sure to reset the record?')"--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Label ID="lblMSgSendBackToPurchaser" runat="server" CssClass="error"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </center>
                    </div>
                </center>
                </div>

            <%--PO Approval Level 1-----------------------------------------------------------------------------------------------------%>
            <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvPoApprovalLeve_1" visible="false" >
                <center>
                <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
                <div style="position :relative; width:95%; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <center>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        Approval ( Level-1 ) [ Quotation No : <asp:Label ID="lblrfqno1" runat="server"></asp:Label>]
                        <asp:HiddenField ID="hfSelectedTaskID_app" runat="server" />
                    </div>                
                              
                    <div style="padding:3px;">
                        <span style="font-weight:bold;font-size:11px;color:red;">Link the purchase order with the allocation list given below & enter your remarks for approval. If allocation not exists ( Add New )</span>
                     </div>
                    <%------------------------------------------------------------------------------%>
                    <div style="font-size:15px;font-weight:bold;padding:4px;" >
                        <table cellpadding="2" cellspacing="2" border="0" width="100%" style="font-weight:normal;font-size:15px">
                            <colgroup>
                                <col width="100px" />
                                <col width="250px" />
                                <col width="70px" />
                                <col width="250px" />
                                <col width="50px" />
                                <col width="150px" />
                                <col width="80px" />
                                <col />
                                <tr >
                                    <td style="font-size:13px;text-align:right;"><b>Company :</b></td>
                                    <td style="font-size:13px;text-align:left;">
                                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-size:13px;text-align:right;"><b>Vessel :</b></td>
                                    <td style="font-size:13px;text-align:left;">
                                        <asp:Label ID="lblVessel" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-size:13px;text-align:right;"><b>Year :</b></td>
                                    <td style="font-size:13px;text-align:left;">
                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                    </td>
                                    <td style="font-size:13px;text-align:right;"><b>Account :</b></td>
                                    <td style="font-size:13px;text-align:left;">
                                        <asp:Label ID="lblAccountNoNameTaskList" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                        <table cellspacing="0" cellpadding="5" style="border-collapse:collapse; width:100%; height:25px; font-weight:bold;" class="rpt-table">
                            <colgroup>
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <col width="120px" />
                                <tr style="background-color:#5f5f5f;color:white;">
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>YTD-Actual</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>YTD-Committed</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>YTD-Consumed</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>YTD-Budget</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>YTD-Variance(US$)</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>YTD-Variance(%)</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>Annual Budget</span></td>
                                    <td style="text-align:right;color:white;vertical-align:central;"><span>Utilization(%)</span></td>
                                </tr>
                                <tr style="color:#5f5f5f;">
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDActule" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDCommitted" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDConsumed" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDBudget" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDVariance" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDVariancePer" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDAnnualBudget" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblYTDAnnualUtilization" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                    
                </div>

                    
                        
                <div style="padding-top:2px;text-align:right;margin-right:20px;">
                    <asp:Button ID="btnOpenAddTaskPopup" runat="server" OnClick="btnOpenAddTaskPopup_OnClick" Width="80px" Text="Add Task" style="  border:none; padding:4px;"  CssClass="btn"/>                        
                </div>
             <%--   <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;">
                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" >
                        <colgroup>
                            <col width="30px" />
                            <col width="35px" />
                            <col />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <col width="70px" />
                            <tr class= "headerstylegrid">
                                <td></td>
                                <td></td>
                                <td style="text-align:left;color:white;vertical-align:central;">Allocation Description</td>
                                <td style="text-align:right;color:white;vertical-align:central;">Budget</td>
                                <td style="text-align:right;color:white;vertical-align:central;">Consumed</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Jan</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Feb</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Mar</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Apr</td>
                                <td style="text-align:center;color:white;vertical-align:central;">May</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Jun</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Jul</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Aug</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Sep</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Oct</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Nov</td>
                                <td style="text-align:center;color:white;vertical-align:central;">Dec</td>
                            </tr>
                        </colgroup>
                    </table>
                </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:170px;border:solid 1px #d5d2d2;">
                              <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" >
                                  <colgroup>
                                      <col width="30px" />
                                      <col width="35px" />
                                      <col />
                                      <col width="80px" />
                                      <col width="80px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                      <col width="70px" />
                                  
                                  </colgroup>
                               
                              <asp:Repeater ID="rptTrackingTaskList" runat="server">
                                  <ItemTemplate>
                                      <tr>
                                          <td>
                                              <input type="radio"  name="sel" taskid='<%#Eval("TaskID")%>' onclick='SetSelectedTaskID_app(<%#Eval("TaskID")%>)' 
                                                  <%#((Eval("TaskID").ToString()==hfSelectedTaskID.Value)?"checked":"") %> />
                                          </td>
                                          <td>
                 
                    <span class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'><%# ((Eval("budgeted").ToString()=="True")?"B":"U") %></span></td>
                                          <td style="text-align:left;"><%#Eval("TaskDescription") %></td>
                                          <td style="text-align:right;"><%#Common.CastAsInt32(Eval("Amount")) %>
                                              <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID") %>' />
                                          </td>
                                          <td style="text-align:right;"><%# Common.CastAsInt32( Eval("TotConsume")) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),1, Eval("Jan"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),1)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),2, Eval("Feb"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),2)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),3, Eval("Mar"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),3)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),4, Eval("Apr"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),4)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),5, Eval("May"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),5)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),6, Eval("Jun"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),6)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),7, Eval("Jul"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),7)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),8, Eval("Aug"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),8)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),9, Eval("Sep"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),9)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),10,Eval("Oct"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),10)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),11,Eval("Nov"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),11)) %></td>
                                          <td class='<%#(GetCSSVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),12,Eval("Dec"))) %>' style="text-align:right;"><%#(GetVariancePerMonthTaskKList(Eval("TaskID"),Eval("ConsumptionMonthCount"),12)) %></td>
                                      </tr>
                                  </ItemTemplate>
                              </asp:Repeater>
                               </table>

                                       
                           </div>--%><%--<input type="radio" name="rdo" class="selRadio" TaskID='<%#Eval("TaskID") %>' onclick="click_btn(this);" />--%>
                    <%------------------------------------------------------------------------------%>
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 100px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td>
                                        <b>Suptd. PO Remarks :</b>
                                    </td>
                                </tr>
                                   <tr >
                                          <td style="text-align: left;">
                                              <asp:TextBox ID="txtRemarksApproval_1" runat="server"  Width="99%" TextMode="MultiLine" Height="120px"></asp:TextBox>
                                          </td>
                                   </tr>
                                    <%-- change1562017 ----------------------------------------------------------------------------------%>
                                    <tr id="trApp1ReasonForHigherPriceLable" runat="server" visible="false">
                                    <td>
                                        <b>Reason for selecting Higher price quotation :</b>
                                    </td>
                                    </tr>
                                    <tr id="trApp1ReasonForHigherPriceControl" runat="server" visible="false">
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlApp1ReasonForHigherPrice" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Consolidated with another supplier"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Logistics more cost/time efficient"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Product quality not meeting requirements/OEM"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Incomplete quote"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%-------------------------------------------------------------------------------------%>           
                                    <tr >
                                          <td align="right" style="text-align: right; padding-right:15px; width:100px;" >
                                              
                                            
                                        </td>
                                    </tr>
                                  </table>    
                    <%------------------------------------------------------------------------------%>
                    

                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btn_VerifyPoApproval_1" runat="server" Text="Approve" Width="80px" OnClick="btn_VerifyPoApproval_1_Click" ValidationGroup="verify" style="  border:none; padding:4px;" CssClass="btn" />
                        <asp:Button ID="btnClosePoApprovalLevel_1" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." onclick="btnClosePoApprovalLevel_1_ClosePopup_OnClick"/>
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgPoApproval_1" runat="server" CssClass="error"></asp:Label>
                    </div>
                    </center>    
                    </div>
                    </center>
                        </div>

            <%-- Add TrackingTask-----------------------------------------%>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddTrackingTask" visible="false" >
        <center>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:750px; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:110px;opacity:1;filter:alpha(opacity=100)">
        <center>
            <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                Add New Budget Allocation
            </div>                
            <div >
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; text-align:left; border-collapse:collapse;">
                            <colgroup>
                                <col width="120px" />
                                <col />
                                <tr>
                                    <td>
                                        <br />
                                        Allocation Type : </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlTaskType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskType_OnSelectedIndexChanged" Width="150px">
                                            <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Budgeted" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Unbudgeted" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Allocation Description : </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtTtDescription" runat="server" Rows="2" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Budget Amount : </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtTtAmount" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                              <%--  <tr>
                                    <td>
                                        <br />
                                        Expenses scheduled for months : </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="4" cellspacing="0" class="rpt-table table-centered" style="text-align:center;width:100%;">
                                            <colgroup>
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <col width="50px" />
                                                <tr class="group-3">
                                                    <td>Jan</td>
                                                    <td>Feb</td>
                                                    <td>Mar</td>
                                                    <td>Apr</td>
                                                    <td>May</td>
                                                    <td>Jun</td>
                                                    <td>Jul</td>
                                                    <td>Aug</td>
                                                    <td>Sep</td>
                                                    <td>Oct</td>
                                                    <td>Nov</td>
                                                    <td>Dec</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtJan" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtFeb" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtMar" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtApr" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtMay" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtJun" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtJul" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtAug" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtSep" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtOct" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtNov" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTtDec" runat="server" />
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                    </td>
                                </tr>--%>
                            </colgroup>
                            
                        </table>
                <div style="text-align:center;padding:5px;">
                    <asp:Button ID="btnSaveTrackingTask" runat="server" Text="Save" Width="80px" OnClick="btnSaveTrackingTask_OnClick" style="  border:none; padding:4px;" CssClass="btn" />
                    <asp:Button ID="btnClsose1" runat="server" Text="Close"  Width="80px"  OnClick="btnCloseAddTrackingTaskPopup_OnClick" style="  border:none; padding:4px;" CssClass="btn" />
                </div>
                <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                    &nbsp; <asp:Label ID="lblMsgTrackingTask" runat="server" CssClass="error"></asp:Label>
                </div>
                        
            </div>
        </center>
        </div>
        </center>
    </div>


            <%------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvSendBackToApproval1" visible="false"  >
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:450px; height:250px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                    <center>
                        <div style="font-size:16px;font-weight:bold;padding:5px;margin-top:0px;" class="text headerband">
                            Send Back To Approval-1 Stage
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="btnCloseSendBackToPurchaser_OnClick" CssClass="btn" />
                        </div>     
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td> Comments </td>
                            </tr>
                            <tr>
                                <td> 
                                    <asp:TextBox ID="txtSendBackToApprovalMessage" runat="server"  TextMode="MultiLine" Width="96%" Height="120px"> </asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="btnSendBackToApproval1_Save" runat="server" OnClick="btnSendBackToApproval1_Save_OnClick" Text=" Save " CssClass="btn" />
                                    <asp:Button ID="btnSendBackToApproval1_Close" runat="server" OnClick="btnSendBackToApproval1_Close_OnClick" Text=" Close " CssClass="btn" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Label ID="lblSendBackToApproval1" runat="server" CssClass="error"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </center>
                    </div>
                </center>
                </div>

            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divAttachment" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
        <br />
        <div class="text headerband"> <b>Attached Documents</b> 
             <asp:ImageButton ID="imgClosePopup" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" OnClick="imgClosePopup_Click" />
        </div>
        <br /><br />
        <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                                 
                               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                                   <colgroup>
                                       <col width="50px" />
                                       <col />
                                       <col width="90px" />
                                       <tr class="headerstylegrid" style="font-weight:bold;">
                                           <td ></td>
                                           <td >File Name</td>
                                           <td >Attachment</td>
                                       </tr>
                                       <asp:Repeater ID="rptDocuments" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                  <td style="text-align:center;">
                                                       <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("DocId")%>' visible='<%#Common.CastAsInt32(Eval("StatusId")) == 0 %>' />
                                                  </td>
                                                   <td style="text-align:left;padding-left:5px;"><%#Eval("FileName")%>
                                                       <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("DocId")%> ' />
                                                   </td>
                                                   <td style="text-align:center;"> 
                                                    <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                                                        <a onclick='OpenDocument(<%#Eval("DocId")%>,<%#Eval("RequisitionId")%>,"<%#Eval("VesselCode")%>")' style="cursor:pointer;">
                                                       <img src="../../HRD/Images/paperclip12.gif" />
                                                       </a>
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </colgroup>
                        </table>
                                     </div>
        <asp:Button ID="btnPopupAttachment" runat="server" CssClass="btn" onclick="btnPopupAttachment_Click" Text="Cancel" CausesValidation="false" Width="100px" />
         </center>
        </div> 
    </center>
    </div>
       </div>

        <%-- Change Account Code----------------------------------------------------------------------------------%>
        <Account:Chagne id="account" runat="server" Visible="false"></Account:Chagne>
        <asp:HiddenField ID="hfSelectedTaskID" runat="server" />
        <asp:Button ID="Temp"  runat="server" OnClick="Temp_OnClick" style="display:none;" />
        <asp:Button ID="btnRefereshPage"  runat="server" OnClick="btnRefereshPage_OnClick" style="display:none;" />

    </ContentTemplate> 
    </asp:UpdatePanel> 
    </div>
        <script type="text/javascript">
            //function RefreshBack()
            //{    
            //  //  alert('Hi');
            //  // document.getElementById("btnRefreshRFQ").focus();
            //   document.getElementById("btnRefreshRFQ").click();
            //}
            //function SetSelectedTaskID_app(ctrl) {
            //    $("#hfSelectedTaskID_app").val($(ctrl).attr("taskid"));

            //}
            function SetSelectedTaskID_app(TaskId) {
                //alert('Hi');
                //alert(TaskId);
                document.getElementById('hfSelectedTaskID_app').value = TaskId;
                
               // $("#hfSelectedTaskID_app").val(TaskId);
               // alert(getElementById("#hfSelectedTaskID_app").value);
                //$("#Temp").click();
            }
        </script>
        <script type="text/javascript" >
            
            function SetSelectedTaskID(ctrl) {
                
                $("#hfSelectedTaskID").val($(ctrl).attr("taskid"));
                $("#Temp").click();
            }
            function SetSelectedTaskID(TaskId) {
                //alert(ctrl);
                //alert($(ctrl).attr("taskid"));
                $("#hfSelectedTaskID").val(TaskId);
                $("#Temp").click();
            }
            function RefereshPage() {
                $("#btnRefereshPage").click();
            }

            
        </script>
    </form>
</body>
</html>
