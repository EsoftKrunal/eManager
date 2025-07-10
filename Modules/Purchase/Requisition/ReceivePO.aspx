<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceivePO.aspx.cs" Inherits="ReceivePO" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

    <title>EMANAGER</title>
    
      <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" src="../JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('hfPRID').value = prid;
                }
            }
        }
        
        function AddPR()
        {
            alert('Pending to Work'); 
        }
        
        function ViewPR()
        {
        __doPostBack('btnViewPR','');
        }
        
        function EditPR()
        {
            alert('Pending to Work'); 
        }
        
        function AskPRtoCancel(Status)
        {
           return confirm('Are you sure you want to cancel it?');
       }
       function CheckReceiveQty() 
       {
           if (document.getElementById('lblTotQtyRcv').innerHTML==0)
           {    
               return confirm('Total quantity received is 0. Confirm anyway ?');
           }
           else 
           {
               return confirm('Confirm receipt of Goods on ' + document.getElementById('txtitmRcvDate').value);
               
           }
       }
    </script>
    <script language="javascript" type="text/javascript">
        //function for file name
        function FileName(Objsku) 
        {
            if (IsFileChar(Objsku.value) == false) {
                alert('Invalid char for file name ');
                var objId = Objsku.id;
                var vals = document.getElementById(objId).value;
                vals = vals.substring(0, vals.length - 1);
                document.getElementById(objId).value = vals;
                document.getElementById(objId).focus();
                return false;
            }

        }

        function IsFileChar(sText) 
        {
            var ValidChars = "1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM_-.";
            var IsNumber = true;
            var Char;
            for (i = 0; i < sText.length && IsNumber == true; i++) 
            {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) 
                {
                    IsNumber = false;
                }
            }
            return IsNumber;
        }
        //end file name function
        function numVal(Objsku) 
        {
            if (IsNumeric(Objsku.value) == false) 
            {
                var objId = Objsku.id;
                var vals = document.getElementById(objId).value;
                vals = vals.substring(0, vals.length - 1);
                document.getElementById(objId).value = vals;
                document.getElementById(objId).focus();
                return false;
            }
        }

        function IsNumeric(sText) 
        {
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
        function do_totals1() 
        {
            document.all.pleasewaitScreen.style.pixelTop = (document.body.scrollTop + 50);
            document.all.pleasewaitScreen.style.visibility = "visible";
            window.setTimeout('do_totals2()', 1);
        }
        function do_totals2() 
        {
            lengthy_calculation();
            document.all.pleasewaitScreen.style.visibility = "hidden";
        }
        function lengthy_calculation() 
        {
            var x, y
            for (x = 0; x < 1000000; x++) 
            {
                y += (x * y) / (y - x);
            }
        }
    </script>
     <script type="text/javascript">
         function OpenDocument(TableID, PoId, VesselCode) {
             // alert(VesselCode);
             window.open("ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode + "&PRType=''");
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>  
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; text-align: left;">
        <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:left; width: 100%" border="0" cellpadding="0" cellspacing="0">
        <col width="40%"/>
        <col width="60%"/>
        <tr class="text headerband" >
            <td colspan="2" style="padding:4px;"   >Receipt of Items
            </td>
        </tr>
        <tr>
            <td colspan="2" style=""  >
                
                
                           <table cellpadding="2" cellspacing="0" width="100%" style ="border-collapse : collapse; font-weight:bold; background-color:#C2C2C2; font-size :13px;">
                           <tr>
                           <td style="text-align :right">Quotation No. :</td>
                           <td style="text-align :left">
                               <asp:Label ID="lblRFQNO" runat="server"></asp:Label></td>
                           
                           <td style="text-align :right">VSL Req No. :</td>
                           <td style="text-align :left"> 
                               <asp:Label ID="lblReqNo" runat="server"></asp:Label></td>
                           
                           <td style="text-align :right">Request Type :</td>
                           <td style="text-align :left"> <asp:Label ID="lblReqType" 
                                   runat="server"></asp:Label></td>
                           
                               <td style="text-align :right">Created By :</td>
                           <td style="text-align :left">
                               <asp:Label ID="lblCreatedBy" runat="server"></asp:Label></td>

                           <td style="text-align :right">REQN Date :</td>
                           <td style="text-align :left">
                               <asp:Label ID="lblDateCreated" 
                                   runat="server"></asp:Label></td>
                           
                           </tr>
                           </table> 
                
                
                </td>
        </tr>
       
        <tr>
            <td colspan="2" style="background-color : #FAEBD7" >
                <table border="1" cellpadding="1" cellspacing="0" style="border :solid 1px #4371a5; border-collapse : collapse" width="100%">
                        <colgroup>
                            <col width="135px;" />
                            <col />
                        </colgroup>
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
                                                
                    </table>
            </td>
        </tr>
         <tr>
            <td >
                &nbsp;
            </td>
            <td style="text-align:right; padding:1px;">
                 <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents" />
                              (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>
                              ) &nbsp;&nbsp;
                  <asp:Button ID="imgUpdateAll" runat="server" Text="Receive All" CssClass="btn" Height="25px" Width="120px" OnClick="imgUpdateAll_OnClick" />
                    <asp:Button ID="imgClearAll" runat="server" Text="Clear" CssClass="btn" Height="25px" Width="80px"  OnClick="imgClearAll_OnClick" />
            </td>
        </tr>
        </table>
        </td> 
        </tr> 
        </table> 
        <asp:UpdatePanel ID="UP1" runat="server" >
            <ContentTemplate>
                <div style="border:2px solid #4371a5;" >
            <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
        <tr>
           <td>
          
           </td>
        </tr>
        <tr>
            <td>
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:50px;" />
                        <col  />
                        <col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:80px;" />
                        <col style="width:70px;" />
                        <col style="width:70px;" />
                        <col style="width:80px;" />
                        <col style="width:17px;"/> 
                        <tr align="left" class= "headerstylegrid">
                            <td>S No</td>
                            <td>
                                Description</td>
                            <td>
                               Part#</td>
                            <td style="text-align:left;">
                                Drawing#</td>
                            <td>
                                Code#</td>
                            <td>
                                Unit Type</td>
                            <td>
                                Qty Order</td>
                            <td>
                                Qty Rcv</td>
                            <td>
                                Qty Open</td>
                                <td></td>
                        </tr>
                    </colgroup>
                </table>
               <div id="dvscroll_RECPO" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 290px ; text-align:center;" onscroll="SetScrollPos(this)">
               <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:50px;" />
                        <col  style="text-align:left;"/>
                        <col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:80px; text-align:center;" />
                        <col style="width:70px;" />
                        <col style="width:70px;" />
                        <col style="width:80px;"/>
                        <col style="width:17px;"/> 
                    </colgroup>
                    <asp:Repeater ID="RptReceiveOrderLst" runat="server">
                        <ItemTemplate>
                             <tr id='tr<%#Eval("recid")%>' class='<%#(Convert.ToInt32(Eval("recid"))!=SelectedRecId)?"":"selectedrow"%>'   >
                                <td><%#Eval("RowNum")%> </td>
                                <td style ="text-align :left "><%# Eval("BidDescription")%></td>
                                <td>
                                    <%# Eval("PartNo")%>
                                </td>
                                <td >
                                    <%# Eval("EquipItemDrawing")%>
                                </td>
                                <td>
                                    <%# Eval("EquipItemCode")%>
                                </td>
                                <td style="text-align :left;"><%#Eval("uom")%></td> 
                                
                                <td>
                                    <%--<asp:TextBox ID="txtQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="txtQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>--%>
                                    <asp:Label ID="lblQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="lblQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:Label>
                                    <asp:HiddenField ID="hfQtyPo" runat="server" Value='<%#Eval("QtyPO")%>' />
                                    <asp:HiddenField ID="hfBidItmID" runat="server" Value='<%#Eval("BidItemID")%>' />
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRcvQty" runat="server" Text ='<%#Eval("QtyRecd")%>' MaxLength="10" Width="60px" OnTextChanged="txtRcvQty_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>
                                    <asp:HiddenField ID="hfRcvQty" runat="server" Value='<%#Eval("QtyRecd")%>' />
                                </td>
                                <td>
                                    <asp:Label id="lblRemainQty" runat="server" Text='<%#Eval("RemQty")%>' ></asp:Label> 
                                </td>
                                <td style='width:17px'></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id='tr<%#Eval("recid")%>' class='<%#(Convert.ToInt32(Eval("recid"))!=SelectedRecId)?"alternaterow":"selectedrow"%>'  lastclass='alternaterow' >
                                 <td><%#Eval("RowNum")%> </td>
                                <td style ="text-align :left "><%# Eval("BidDescription")%></td>
                                <td>
                                    <%# Eval("PartNo")%>
                                </td>
                                <td >
                                    <%# Eval("EquipItemDrawing")%>
                                </td>
                                <td>
                                    <%# Eval("EquipItemCode")%>
                                </td>
                                <td style="text-align :left;"><%#Eval("uom")%></td> 
                                
                                <td>
                                    <%--<asp:TextBox ID="txtQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="txtQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>--%>
                                    <asp:Label ID="lblQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="lblQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:Label>
                                    <asp:HiddenField ID="hfQtyPo" runat="server" Value='<%#Eval("QtyPO")%>' />
                                    <asp:HiddenField ID="hfBidItmID" runat="server" Value='<%#Eval("BidItemID")%>' />
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRcvQty" runat="server" Text ='<%#Eval("QtyRecd")%>' MaxLength="10" Width="60px" OnTextChanged="txtRcvQty_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>
                                    <asp:HiddenField ID="hfRcvQty" runat="server" Value='<%#Eval("QtyRecd")%>' />
                                </td>
                                <td>
                                    <asp:Label id="lblRemainQty" runat="server" Text='<%#Eval("RemQty")%>' ></asp:Label> 
                                </td>
                                <td style='width:17px'></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
               </div>
            </td>
        </tr>
        </table>
        </div>
            <div style="border:2px solid #4371a5;" >
                <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align:center;font-weight:bold;">
                               Order Status Comments 
                            </td>
                             <td style="text-align:center;font-weight:bold;">
                               Ship Comments 
                            </td>
                        </tr>
                        <tr>
                            <td style =" text-align :center">
                                <asp:TextBox ID="txtOrderStatusComm" runat="server" TextMode="MultiLine" Height="60px" Width="98%" ></asp:TextBox>
                            </td>
                             <td style =" text-align :center">
                                <asp:TextBox ID="txtShipComm" runat="server" TextMode="MultiLine" ReadOnly="true" Height="60px" Width="98%" ></asp:TextBox>
                            </td>
                        </tr>
                       </table>
            <table cellspacing="0" cellpadding="0" width="100%" >
                <colgroup>
                    <col width="50%"/>
                    <col width="50%"/>
                    <tr>
                        <td style="text-align:right; vertical-align:top; ">
                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <%-- <tr class="alternaterow" >
                                    <td><b>Total :</b> </td>
                                    <td style="text-align:center; width :80px;">
                                        <asp:Label ID="lblTotQtyOrder" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align:center; width :70px;">
                                        <asp:Label ID="lblTotQtyRcv" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align:center; width :70px;">
                                        <asp:Label ID="lblTotQtyPending" runat="server"></asp:Label>
                                    </td>
                                    <td style="width :17px">&nbsp;</td>
                                </tr>--%>
                                <tr class="row">
                                    <td><b>Date Items Received:</b> </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtitmRcvDate" runat="server" MaxLength="15" Width="85px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <tr class="row">
                                    <td style="text-align:right;padding-right:5px;"><b>Delivery Notes:</b> </td>
                                    <td style="text-align:left;padding-left:5px;">
                                         <asp:FileUpload ID="FU" runat="server" CssClass="input_box" /> &nbsp;
                                    <span>
                                         <asp:ImageButton id="ImgAttDeliveryNotes" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" onclick="ImgAttDeliveryNotes_Click" ToolTip="Click to view attached delivery notes documents" style="width: 12px" />  (<asp:Label ID="lblDeliveryNotesCount" runat="server" Text="0"></asp:Label>) 
                                     </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;padding:2px;">
                            <asp:Label ID="lblmsg" runat="server" CssClass="error"></asp:Label>
                            <asp:Button ID="imgUpdateQty" runat="server" CssClass="btn" Height="25px" OnClick="imgUpdateQty_OnClick" Text="Save" Width="80px" />
                            <asp:Button ID="imgConfirm" runat="server" CssClass="btn" Height="25px" OnClick="imgConfirm_OnClick" OnClientClick="return CheckReceiveQty()" Text="Confirm Receipt" Width="130px" />
                            <asp:Button ID="imgClose" runat="server" CssClass="btn" Height="25px"  Text="Close" Width="80px"  OnClientClick="window.close();return false;" />
                            <%--OnClientClick="window.close();return false;"--%>
                        </td>
                        <td style="text-align:left;padding:2px 0px 2px 5px;" >
                            <asp:Button ID="btnDeliveryNotes" runat="server" CssClass="btn" Text="Upload Delivery Notes" OnClick="btnDeliveryNotes_Click" Width="150px" Height="25px" CausesValidation="false"/> &nbsp;  <asp:Label ID="lblDeliveryNotesMsg" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                </colgroup>
            </table> 
        </div>     
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID ="txtitmRcvDate"></asp:CalendarExtender>
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
             <Triggers>
        <asp:PostBackTrigger ControlID = "btnDeliveryNotes" />
        <asp:PostBackTrigger ControlID="ImgAttDeliveryNotes" />
    </Triggers>
        </asp:UpdatePanel>
    </div>
</form> 
    
</body>

<script type="text/javascript">
    window.opener.blur();
    window.focus();
</script>
</html>   


