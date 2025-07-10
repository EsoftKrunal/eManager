<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VeiwRFQDetailsForApproval.aspx.cs" Inherits="VeiwRFQDetailsForApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Modules/Purchase/UserControls/ChangeAccountCode.ascx" TagName="Chagne" TagPrefix="Account" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript" src="../JS/jquery_v1.10.2.min.js"></script>

     <script type="text/javascript" >
        function ShowAccount() 
        {
            document.getElementById('tr2').style.display = '';
            
        }
        function fncInputNumericValuesOnly(evnt) {
            
            if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                event.returnValue = false;

            }

        }

    </script>
     <script type="text/javascript">
         function OpenDocument(TableID, PoId, VesselCode) {
             // alert(VesselCode);
             window.open("ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode + "&PRType=''");
         }

         function OpenWindow(BidID) {
            // alert('Hi');
             window.open('UpdateOrderStautsComment.aspx?BidID=' + BidID, '', 'height=250,width=450,resizable=no, toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes,left=450, top=280');
         }
     </script>
    <script type="text/javascript" >
        function CloseWindow() 
        {
            window.close();
            return false; 
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
            font-size: 11px;
            font-weight: bolder;
            display: block;
            background-color: #5f6a75;
            color: white;
            padding: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
    <div style="font-family:Arial;font-size:12px;">
    
    <asp:UpdatePanel ID="RptUpdatepanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >
                <table cellpadding="1" cellspacing="0" width="100%" border="1" style ="border :solid 1px #4371a5;" >
                <colgroup>
                    <col/>
                    <tr>
                        <td class="text headerband" style="height :20px; padding-top :3px;">
                           <b> Purchase Order</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="height :20px;">
                           <table cellpadding="2" cellspacing="0" width="100%" style ="border-collapse : collapse; font-weight:bold; background-color:#C2C2C2; font-size :13px;">
                           <tr>
                           <td style="text-align :right">RFQ No. :</td>
                           <td style="text-align :left"><asp:Label ID="lblRFQNO" runat="server"></asp:Label></td>
                           
                           <td style="text-align :right">VSL Req No. :</td>
                           <td style="text-align :left"> <asp:Label ID="lblReqNo" runat="server"></asp:Label></td>
                           
                           <td style="text-align :right">Request Type :</td>
                           <td style="text-align :left"><asp:Label ID="lblReqType" runat="server"></asp:Label></td>
                           <td style="text-align :right">Created By :</td>
                           <td style="text-align :left"><asp:Label ID="lblCreatedBy" runat="server"></asp:Label></td>
                           <td style="text-align :right">REQN Date :</td>
                           <td style="text-align :left"><asp:Label ID="lblDateCreated" runat="server"></asp:Label></td>
                           
                           </tr>
                           </table> 
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left :10px; background-color:#C2C2C2;">
                        <b>Comments for SMD : </b> &nbsp;<asp:Label ID="lblSmdComments" runat="server"></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <table cellpadding="1" cellspacing="0" width="100%" >
                                <colgroup>
                                    <col width="50%" />
                                    <col width="50%" />
                                    <tr>
                                        <td style=" background-color : #FAEBD7">
                                            <table border="1" bordercolor="Grey" cellpadding="1" cellspacing="0" 
                                                style="border :solid 1px #4371a5; border-collapse : collapse" width="100%">
                                                <colgroup>
                                                    <col width="135px;" />
                                                    <col />
                                                    <tr>
                                                        <td class="header" colspan="2" style="padding:4px;">
                                                            Vendor Information
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Vendor Name :</b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblVenderName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Contact Details :</b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblVenContact" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Delivery Port/Date :<b> </b></b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPortNDate" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPoDesc" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <b>Comments from Vendor :</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblVenComments" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                        </td>
                                        <%--<td style=" background-color :#E0FFFF">
                                            <table border="1" bordercolor="Grey" cellpadding="1" cellspacing="0" 
                                                style="border :solid 1px black;border-collapse : collapse" width="100%">
                                                <colgroup>
                                                    <col width="180px" />
                                                    <col />
                                                    <tr class="header">
                                                        <td colspan="2" style="padding:4px;">
                                                            <b>Budget Summary (US$)</b>
                                                        </td>
                                                    </tr>
                                                    <tr style=" font-weight :bold ">
                                                        <td>
                                                            Account :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAccountName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Annual Budget :</b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAnnualBudget" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Year to date Consumed :</b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblConsumedDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Budget Remaining :</b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBudgetRem" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Utilization (%):</b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUtilization" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                        </td>--%>
                                        <td style=" background-color :#D8BFD8">
                                            <table border="1" bordercolor="Grey" cellpadding="1" cellspacing="0" 
                                                style="border :solid 1px #4371a5; border-collapse : collapse" width="100%">
                                                <colgroup>
                                                    <col width="40%" />
                                                    <col width="60%" />
                                                    <tr>
                                                        <td class="header" colspan="2" style="padding:4px;">
                                                            Spare Information
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight:bold">
                                                            Equip Name :
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblEquipName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight:bold">
                                                            Model/Type :
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblModelType" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight:bold">
                                                            Serial# :
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight:bold">
                                                            Year Built :
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblBuiltYear" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight:bold">
                                                            Maker&#39;s name and Address :
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;<asp:Label ID="lblNameAddress" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table> 
                        </td>
                    </tr>
                </colgroup>
            </table> 
        </td> 
         </tr>
         <tr style="height:30px;">
            <td >
                <asp:Button ID="imgCancelPO" runat="server" style="float:left;" OnClick="imgCancelPO_OnClick" OnClientClick="return confirm('Are you sure to cancel this PO?');" Text="Cancel PO" ToolTip="Cancel PO : Cancel Purchase Order" CssClass="btn" Height="25px" Width="100px"/>
                <asp:Button runat="server" id="btnRecGoods"  onclick="btnViewGoodsRcv_OnClick" ToolTip="Recieve Goods" style="float:left;" CssClass="btn" Height="25px" Width="120px" Text="Recieve Goods"/>
                <asp:Button runat="server" id="btnEnterInv"  onclick="btnInvoice_OnClick" ToolTip="PO-Invoice Verification" style="float:left;" CssClass="btn" Text="PO-Invoice Verification" Width="150px" Height="25px" />
                <asp:Button runat="server" id="btnSendMail"  onclick="btnSendMail_OnClick" ToolTip="Send Mail" style="float:left;" CssClass="btn" Text="Send e-Mail" Height="25px" Width="120px" />
                <asp:Button runat="server" id="btnOrderComments" ToolTip="Order Comments" style="float:left;" CssClass="btn" Text="Order Comments" Height="25px" Width="120px"  />
                 
                <asp:Label ID="lblmsginvoice" runat="server" CssClass="error" style="float:left;"></asp:Label>
                
                <div style="float:right; padding-right:10px;">
                      <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents" />
                              (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>
                              ) &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkChangeAccountCode" runat="server" Visible="false" ForeColor="Red" Text="( Change Account Code )" OnClick="lnkChangeAccountCode_OnClick"></asp:LinkButton>
                    <b> Account Code :</b>&nbsp;<asp:Label ID="lblAccountCode" runat="server" Text="Label"></asp:Label>
                </div>
            </td>
         </tr>
           <%--  <tr>
                <td>
                    <table id="tblChangeAccCod" runat="server" width="100%" >
                                                                <tr >
                                                                    <td colspan="3" style="height :15px; background-color:orange; text-align:center;">
                                                                        <div onclick="ShowAccount();" style="cursor:pointer; color:Green; font-weight:bold;"> Change Account Code</div>
                                                                    </td>
                                                                </tr>
                                                                <tr id="tr2" style="display:none;">
                                                                    <td  style="width :100px;text-align :right">
                                                                        <b>Acct. Code :</b>
                                                                    </td>
                                                                    <td  style="width :300px;text-align :left">
                                                                        <asp:DropDownList ID="ddlAccCode" runat="server" ></asp:DropDownList>            
                                                                    </td>
                                                                    <td style="text-align :left">
                                                                        <asp:Button ID="btnSaveAccCode" runat="server" style="background-color:Orange;color:White;border-color:White;border-collapse:collapse; font-weight:bold; vertical-align:middle; font-size:12px;" Text="Change" Width="80px" Height="21px"  OnClick="btnSaveAccCode_OnClick" OnClientClick="return confirm('Are you sure to change the account code ?');"/>
                                                                    </td>
                                                                </tr>
                                                            </table>
                       
                                                                   
                                                            </td>
            </tr>--%>
        <tr>
        <td>
            <table cellpadding="0" cellspacing="0"   width="100%" rules="rows" style ="border :solid 1px #4371a5; margin-top:5px;" >
            <tr>
                <td class="header" style="height :20px;" > Item List for RFQ </td>
            </tr>
            <tr>
            <td>
            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                <col style="width:4%;" />
                <col style="width:5%;" />
                <col style="width:26%;"/>
                <col style="width:8%;" />
                <col style="width:8%" />
                <col style="width:5%" />
                <col style="width:8%" />
                <col style="width:6%" />
                <col style="width:8%" />
                <col style="width:6%" />
                <col style="width:8%" />
                <col style="width:6%" />
                <col style="width:2%" />
            </colgroup>
            <tr align="left" class= "headerstylegrid">
                <td>S.No.</td>
                <td>REF#</td>
                <td>Description</td>
                <td>Bid Qty</td>
                <td>PO Qty</td>
                <td>UOM</td>
                <td>Unit Price</td>
                <td>GST/Tax (%)</td>
                <td>(LC)</td> <%--Ext Price--%>
                <td>GST/Tax (LC)</td>
                <td>(USD)</td>
                <td>GST/Tax (USD)</td>
                <td>&nbsp;</td>
            </tr>
            </table>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 230px ; text-align:center;" id="tbl_Spares">
             <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
               <colgroup>
               <col style="width:4%;" />
                <col style="width:5%;" />
                <col style="width:26%;"/>
                <col style="width:8%;" />
                <col style="width:8%" />
                <col style="width:5%" />
                <col style="width:8%" />
                <col style="width:6%" />
                <col style="width:8%" />
                <col style="width:6%" />
                <col style="width:8%" />
                <col style="width:6%" />
                <col style="width:2%" />
        </colgroup>  
        <asp:Repeater ID="rptItems" runat="server">
               <ItemTemplate>
                <tr id='tr<%#Eval("recid")%>'>
                    <td style="width:4%;">
                        <asp:Label ID="lblRowNumber" runat="server" ></asp:Label>
                        <%--<%# Eval("Sno")%>--%>
                    </td> 
                    <td style="width:5%;"><%# Eval("REFNumber")%></td>
                    <td style="text-align :left;width:26%;"><%# Eval("BidDescription")%><br />
                       <b>Part#:</b><%# Eval("PartNo")%>, <b>Drawing#:</b><%# Eval("EquipItemDrawing")%>, <b>Code#:</b><%# Eval("EquipItemCode")%></td>
                    <td style="width:8%;">
                        <asp:Label ID="lblBidQty" runat="server" Text='<%# Eval("bidqty")%>'></asp:Label>
                        <asp:HiddenField ID="hfItemID" runat="server" Value='<%#Eval("BidItemID") %>' />
                        <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%#Eval("UnitPrice") %>' />
                    </td> 
                    <td style="width:8%;">
                        <asp:TextBox ID="txtOrderQTY" runat="server" Text='<%# Eval("qtyPo")%>' MaxLength="10"  Width="70px" AutoPostBack="true"  OnTextChanged="txtOrderQTY_OnTextChanged" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox>
                    </td>
                    <td style="width:5%;"> <%# Eval("UOM")%> </td>
                    <td style="width:8%;">
                       <asp:Label ID="lblUnitPrice" runat="server" Text ='<%# Eval("UnitPrice") %> '></asp:Label>
                    </td> 
                    <td style="width:6%;">
                       <asp:Label ID="lblGSTPer" runat="server" Text ='<%# Eval("GSTTaxPercentage") %> '></asp:Label>
                    </td> 
                    <td style ="text-align:right;width:8%;">
                        <asp:Label ID="lblLC" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("LCPoTotal"))%>'></asp:Label>
                    </td>
                     <td style="width:6%;">
                       <asp:Label ID="lblGSTLC" runat="server" Text ='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtLC")) %> '></asp:Label>
                    </td>
                    <td style ="text-align:right;width:8%;">
                    
                        $&nbsp;<asp:Label ID="lblUsd" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("UsdPoTotal"))%>'></asp:Label>
                    </td>
                    <td style="width:6%;">
                       <asp:Label ID="lblGSTUsd" runat="server" Text ='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtUSD")) %> '></asp:Label>
                    </td>
                    <td style ="text-align:right;width:1%;"></td>
                </tr>
            </ItemTemplate>
               <AlternatingItemTemplate>
                <tr id='tr<%#Eval("recid")%>' class='alternaterow'>
                    <td style="width:4%;">
                        <asp:Label ID="lblRowNumber" runat="server" ></asp:Label>
                        <%--<%# Eval("Sno")%>--%>
                    </td> 
                    <td style="width:5%;"><%# Eval("REFNumber")%></td>
                    <td style="text-align :left;width:26%;"><%# Eval("BidDescription")%><br />
                        <b>Part#:</b><%# Eval("PartNo")%>, <b>Drawing#:</b><%# Eval("EquipItemDrawing")%>, <b>Code#:</b><%# Eval("EquipItemCode")%></td>
                    <td style="width:8%;">
                        <asp:Label ID="lblBidQty" runat="server" Text='<%# Eval("bidqty")%>'></asp:Label>
                        <asp:HiddenField ID="hfItemID" runat="server" Value='<%#Eval("BidItemID") %>' />
                        <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%#Eval("UnitPrice") %>' />
                    </td> 
                    <td style="width:8%;">
                        <asp:TextBox ID="txtOrderQTY" runat="server" Text='<%# Eval("qtyPo")%>' Width="70px" MaxLength="10" AutoPostBack="true"  OnTextChanged="txtOrderQTY_OnTextChanged" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>
                    </td>
                    <td style="width:5%;"><%# Eval("UOM")%></td>
                    <td style ="width:8%;">
                        <asp:Label ID="lblUnitPrice" runat="server" Text ='<%# Eval("UnitPrice") %> '></asp:Label>
                    </td> 
                     <td style="width:6%;">
                       <asp:Label ID="lblGSTPer" runat="server" Text ='<%# Eval("GSTTaxPercentage") %> '></asp:Label>
                    </td> 
                    <td style ="text-align:right;width:8%;">
                        <asp:Label ID="lblLC" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("LCPoTotal"))%>' ></asp:Label>
                    </td>
                    <td style="width:6%;">
                       <asp:Label ID="lblGSTLC" runat="server" Text ='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtLC")) %> '></asp:Label>
                    </td>
                    <td style ="text-align:right;width:8%;">
                        $&nbsp;<asp:Label ID="lblUsd" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("UsdPoTotal"))%>'></asp:Label>
                    </td>
                     <td style="width:6%;">
                       <asp:Label ID="lblGSTUsd" runat="server" Text ='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtUSD")) %> '></asp:Label>
                    </td>
                    <td style ="text-align:right;width:1%;"></td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
        </table>
    

          </div>
            
            </td>
            </tr>
            </table> 
            
        </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                    <colgroup>
                       <col/>
                        <col width="400px;" />
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <colgroup>
                                        <col width="25%" />
                                        <col width="25%" />
                                        <col width="25%" />
                                        <col width="25%" />
                                        <tr style="display:none;">
                                            <td colspan="4" style="width:100%;" >
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <colgroup>
                                                <col width="25%" />
                                                <col width="25%" />
                                                <col width="25%" />
                                                <col width="25%" />
                                                <tr>
                                                    <td style="width:50%;" >
                                                        <table width="100%" >
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <b >Comment For Analysis</b></td>       
                                                                <td style="text-align: center">
                                                                    <b>Comment For Purchasing Staff</b></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:TextBox ID="txtQuarComm" runat="server" Height="60px" TextMode="MultiLine" Width="95%"></asp:TextBox>
                                                                    <td>
                                                                        <asp:TextBox ID="txtVenderComm" runat="server" Height="60px" TextMode="MultiLine" Width="95%"></asp:TextBox>
                                                                </td>
                                                                </tr>
                                                           </table>
                                                    </td>
                                        </tr>
                                   </table>
                                                
                                            </td>
                                        </tr>                                       
                                        <tr style="display:none;">
                                            <td colspan="4">
                                                <table ID="tblApp1" runat="server" border="0" cellpadding="3" cellspacing="0" 
                                                    style="border :solid 1px #4371a5" width="100%">
                                                    <colgroup>
                                                        <tr>
                                                            <td style="width:150px">
                                                                <b>First Approval By :</b>
                                                            </td >
                                                            <td style="text-align :left">
                                                                <asp:Label ID="lblApp1Name" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:150px">
                                                                <b>First Approval On :</b>
                                                            </td>
                                                            <td style="text-align :left">
                                                                <asp:Label ID="lblApp1On" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </colgroup>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table ID="tblApp2" runat="server" border="0" cellpadding="3" cellspacing="0" 
                                                    style="border :solid 1px #4371a5" width="100%">
                                                    <colgroup>
                                                        <tr>
                                                            <td style="width:150px">
                                                                <b>Second Approval By :</b>
                                                            </td>
                                                            <td style="text-align :left">
                                                                <asp:Label ID="lblApp2Name" runat="server" ></asp:Label>
                                                            </td>
                                                            <td style="width:150px">
                                                                <b>Second Approval On :</b>
                                                            </td>
                                                            <td style="text-align :left">
                                                                <asp:Label ID="lblApp2On" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </colgroup>
                                                </table>
                                            </td>
                                        </tr>
                                        <%--Approval Requested By--%>
                                        <tr >
                                            <td colspan="4">
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
                                            <tr align="left" class= "headerstylegrid">
                                            <td>Status</td>
                                            <td>Approved By</td>
                                            <td>Approved On</td>
                                            <td>Comments</td>
                                            <td></td>
                                            </tr>
                                            </colgroup>
                                            </table>
                                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" class="bordered">
                                        <colgroup>
                                        <col width="90px" />
                                            <col width="160px" />
                                            <col width="100px" />
                                            <col />
                                            <col width="130px;"/>
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
                                        <td></td>
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
                                            <asp:Label ID="lblPOAccountCompany" runat="server"></asp:Label>
                                        </td>
                                        </tr>
                                        </colgroup>
                                        </table>
                                        </td>
                                        </tr>
                                        </table>
                                            </td>
                                        </tr>
                                    </colgroup>
                                </table>
                            </td>
                            <td>
                                <table border="1" cellpadding="3" cellspacing="0" style="border :solid 1px #4371a5;border-collapse : collapse" width="100%">
                                    <colgroup>
                                        <col width="180px" />
                                        <col  />
                                        <col  />
                                        <tr>
                                            <td colspan="3" style="font-size:13px;">
                                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="background-color : #E0FFFF;text-align :right;font-size:10px;">
                                                            <b>LC :</b></td>
                                                        <td style="background-color : #E0FFFF;text-align :left;font-size:10px;">
                                                            <asp:Label ID="lblLocalCurr" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="background-color : #FAEBD7;text-align :right;font-size:10px;">
                                                            <b>Exch. Rate :</b></td>
                                                        <td style="background-color : #FAEBD7;text-align :left;font-size:10px;">
                                                            <asp:Label ID="lblCurrRate" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="background-color : #D8BFD8;text-align :right;font-size:10px;">
                                                            <b>Exch. Date :</b></td>
                                                        <td style="background-color : #D8BFD8;text-align :left;font-size:10px;">
                                                            <asp:Label ID="lblBidExchDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>LC</td>
                                            <td>US$</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Est&#39;d Shpg(LC) :</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLC" runat="server" AutoPostBack="true" MaxLength="10" 
                                                    onkeypress='fncInputNumericValuesOnly(event)'
                                                    OnTextChanged="txtLC_OnTextChanged" Width="70px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUSD" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>GST/Tax Total :</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalGSTLC" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalGSTUSD" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Discount <asp:Label ID="lblDisPer" runat="server" /> (%) :</b>
                                            </td>
                                            <td>
                                                 <asp:Label ID="lblTotalDiscountLC" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalDiscountUsd" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Total :</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalLCD" runat="server" style="float:left;" ></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalUSdD" runat="server" style="float:left;" ></asp:Label>
                                                
                                            </td>
                                        </tr>
                                    </colgroup>
                                    
                                </table>
                                <div class="box" style="padding:6px; background-color:#ffffff;color:#333;" >
                                    <asp:CheckBox ID="ChkBreakdown" runat="server" Text=" Breakdown/Unbudgted " AutoPostBack="true" OnCheckedChanged="ChkBreakdown_OnCheckedChanged" Visible="false"  />
                                    &nbsp;<asp:Label runat="server" ID="lblBreakdown" Font-Bold="true" Font-Size="Larger" Text=""></asp:Label>
                                </div>
				 <div class="box" style="padding:6px; background-color:#ffffff;color:#ff0000;" >
					<asp:Label runat="server" ID="lblbackorder" Font-Bold="true" Font-Size="Larger" Text=""></asp:Label>
				 </div>
                            </td>
                        </tr>
                    </colgroup>
                </table> 
            </td>
        </tr>
        <tr>
            <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0"  >
                    <tr>
                    <td>
                        <asp:Label ID="lblmsg" runat="server" CssClass="error"></asp:Label>
                    </td>
                    <td style="text-align:right; padding:1px;">
                        <asp:Button id="imgSaveSpareRFQ" runat="server" Text="Save" Visible="false" CssClass="btn" Height="25px"    />
                        <asp:Button ID="imgPrint" runat="server" Text="Print" CssClass="btn" Height="25px"   />
                        <asp:Button id="imgCancel" runat="server" Text="Close" OnClientClick="return CloseWindow();" CssClass="btn"  Height="25px" />
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table> 
    
     <div style="padding:7px; color:#00478F; font-size:13px; background-color:#99D6FF">Supplier Ratings & Comments</div>
        <asp:HiddenField runat="server" ID="hfdRating" />
        <div>
       <table cellpadding="5" cellspacing="0" border="1" width="500px" style="border-collapse:collapse; text-align:center; color:#00478F " bordercolor="#0099CC">
            <tr id="ratingctl">
            <td style="width:20%; background-color:#FF5C33;cursor:pointer;" onclick="SetRating(-2,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Poor</td>
            <td style="width:20%; background-color:#FFB84D;cursor:pointer;" onclick="SetRating(-1,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Below Avg.</td>
            <td style="width:20%; background-color:#E6E6E6;cursor:pointer;" onclick="SetRating(0,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Average</td>
            <td style="width:20%; background-color:#99E699;cursor:pointer;" onclick="SetRating(1,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Good</td>
            <td style="width:20%; background-color:#19A347;cursor:pointer;" onclick="SetRating(2,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Outstanding</td>
            </tr>
            </table>
        </div>                    
        <div>
            <asp:TextBox ID="txtSupplierComments" runat="server" TextMode="MultiLine" Width="100%" Height="55px" ReadOnly="true" ></asp:TextBox>
        </div>
     <div style="padding:7px; color:#00478F; font-size:13px; background-color:#99D6FF">Ship Comments </div>
        <div>
            <asp:TextBox ID="txtShipComments" runat="server" TextMode="MultiLine" Width="100%" Height="55px" ReadOnly="true" ></asp:TextBox>
        </div>
    <%-- Change Account Code----------------------------------------------------------------------------------%>
        <Account:Chagne id="account" runat="server" Visible="false"></Account:Chagne>
        <asp:HiddenField ID="hfSelectedTaskID" runat="server" />
        <asp:Button ID="Temp"  runat="server" OnClick="Temp_OnClick" style="display:none;" />
        <asp:Button ID="btnRefereshPage"  runat="server" OnClick="btnRefereshPage_OnClick" style="display:none;" />
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
    </ContentTemplate> 
    </asp:UpdatePanel> 
    </div>
    
        <script type="text/javascript">
            function SetSelectedTaskID(ctrl) {
                $("#hfSelectedTaskID").val($(ctrl).attr("taskid"));
                $("#Temp").click();
            }
            function RefereshPage() {
                $("#btnRefereshPage").click();
            }
       </script>
         <script type="text/javascript">
             function SetRating(rating, control) {
                 $(".check").css('visibility', 'hidden');
                 $(control).find("img").css('visibility', '');
                 $("#hfdRating").val(rating);
             }
             if ($("#hfdRating").val() != "") {
                 var i = parseInt($("#hfdRating").val()) + 3;
                 //$("#ratingctl td:nth-child(" + i + ")").css("color", "red");
                 //alert($("#ratingctl td:nth-child(" + i + ")"));
                 $("#ratingctl td:nth-child(" + i + ")").click();
             }
        </script>
    </form>
</body>
</html>
