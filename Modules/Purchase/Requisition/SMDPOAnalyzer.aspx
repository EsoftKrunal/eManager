<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMDPOAnalyzer.aspx.cs" Inherits="SMDPOAnalyzer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript" src="../JS/jquery_v1.10.2.min.js"></script>
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/jquery.datetimepicker.js" type="text/javascript"></script>
    <link href="../CSS/jquery.datetimepicker.css" rel="stylesheet" type="text/css"  />
    <script type="text/javascript" >
        //function ViewPopUp(BidID) {

        //    window.open('ApproveRFQPopUp.aspx?BidID=' + BidID + '', '', 'height=180,width=411,resizable=no,toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes,left=100, top=100');
        //}
        function ViewRFQ(BidId) {
            $.ajax({
                url: "SMDPOAnalyzer.aspx/GetBidItemCount",
                data: "{'BidId': '" + BidId + "'}",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    // alert('Hi');
                    if (data.d <= 0) {
                        alert("Please select atleast one Item to Process PO.")
                    }
                    else { window.open('RFQDetailsForApproval.aspx?BidId=' + BidId); }
                },
                error: function (data, status, jqXHR) {
                    alert("Error while Updating data of :" + BidItemId);
                    // alert(jqXHR);
                }
            });
           
        }
        function ApproveThis(BidID) {
            document.getElementById("txtParam").setAttribute("value", BidID);
            document.getElementById("btnApprove").click(); 
        }
        function CancelApproval(BidID) { 
            if (window.confirm('Are you sure to cancel this Approval?')) {
                document.getElementById("txtParam").setAttribute("value", BidID);
                document.getElementById("btnCancel").click();
            }
        }
    </script>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
   <script type="text/javascript"> 
       //$(function () {
       //    fn_chkBox('.form-check-input'); //see if checked on load
       //    function fn_chkBox(this) {
               
               
       //        if ($(this).is(':checked')) {
       //            alert('Hi');
       //            $(this).closest("tr").find("input.form-check-input").prop("disabled", true);
       //        } else {
       //            $(this).closest("tr").find("input.form-check-input").prop("disabled", false);
       //        }
       //    }
       //});
       $(document).ready(function () {
           var ItemCount = $("#hdnItemCount").val(); 
           var cellcount = 0;
           //alert(ItemCount);
           for (var i = 1; i <= ItemCount; i++)
           {
                cellcount = $('.row' + i).length;
              // $('.row'+i).prop('disabled', !$('.row'+i).is(':checked'));
               var celldisSelectedcount = 0;
               var selectedrowcount = $(".row" + i + ":checked").length;
               var approvedCount = $(".row" + i).filter(function () {
                   return $(this).attr('approvalstatus') == 'Approved'
               }).length;
               //console.log("Approved Count :" + approvedCount);
               for (var j = 1; j <= cellcount; j++) {
                   const isChecked = $("#chkItem" + j + ".row" + i).is(":checked"); 
                   var approvalstatusrow = $("#chkItem" + j + ".row" + i).attr("approvalstatus");
                  // console.log($("#chkItem" + j + ".row" + i));
                   if (isChecked) {
                       if (approvalstatusrow == 'Approved') {
                           $(".row" + i).prop('disabled', true);
                       }
                       else {
                           $("#chkItem" + j + ".row" + i).prop('disabled', false);
                       }                       
                   } else {
                       if (approvalstatusrow == 'Approved') {
                           $("#chkItem" + j + ".row" + i).prop('disabled', true);
                       }
                       else {
                           celldisSelectedcount = celldisSelectedcount + 1;
                           $("#chkItem" + j + ".row" + i).prop('disabled', false);
                       }
                      
                   }

                  /* var approvalstatusrow = $("#chkItem" + j + ".row" + i).attr("approvalstatus");*/
                  // alert(approvalstatusrow.length);
                   //if (approvalstatusrow.length > 0)
                   //{ $("#chkItem" + j + ".row" + i).prop('disabled', true); }
               }
               if (selectedrowcount > 0 && approvedCount > 0) {         
                   $('.row' + i).prop('disabled', true);
               }
               if (celldisSelectedcount == cellcount) {  
                   $('.row' + i).prop('disabled', false);
               }
           }
           var selectedcount = 0; 
           var headercellcount = $('.headerrow').length; 

           for (i = 1; i <= headercellcount; i++) {
               var numberOfChecked = $("#chkItem" + i + ":checked").length;
               if (ItemCount == numberOfChecked) {
                   $("#chkItem" + i + ".headerrow").prop('checked', true);
                   $("#chkItem" + i + ".headerrow").prop('disabled', false);
               }

               var approvalstatusheaderrow = $("#chkItem" + i + ".headerrow").attr("approvalstatus");
              //alert(approvalstatusheaderrow.length);
               if (approvalstatusheaderrow.length > 0) {
                   $(".headerrow").prop('disabled', true);
               }
               //else {
               //    $("#chkItem" + i + ".headerrow").prop('checked', false);
               //    $("#chkItem" + i + ".headerrow").prop('disabled', true);
               //}
           }

           var totalcount = ItemCount * cellcount;
           // console.log("Count", totalcount);
           var totalnumberOfunchecked = 0;
           for (i = 1; i <= ItemCount; i++) {
               for (j = 1; j <= cellcount; j++) {
                   var numberOfunchecked = $("#chkItem" + j + ".row" + i + ":not(\":checked\")").length;
                   totalnumberOfunchecked = totalnumberOfunchecked + numberOfunchecked;
               }
           }
           // console.log("unchecked", totalnumberOfunchecked);
           if (totalcount == totalnumberOfunchecked) {
               for (i = 1; i <= ItemCount; i++) {
                   for (j = 1; j <= cellcount; j++) {

                       $("#chkItem" + j + ".row" + i).prop('checked', false);
                       $("#chkItem" + j + ".row" + i).prop('disabled', false);
                   }

               }
           }
           //for (i = 1; i <= ItemCount; i++) {
               
           //    var count = $('.row' + i).length;               
           //    for (var j = 1; j <= count; j++) {
           //        var id = $("#chkItem" + j + ".headerrow").attr("id");
           //        var cellid = $("#chkItem" + j + ".row" + i).attr("id");
                                   
           //        if (id == cellid)
           //        {
           //            if ($(cellid).is(':checked'))
           //            {
           //                selectedcount = selectedcount + 1;
           //                alert(selectedcount);
           //            }
           //        }
                   
           //        if (ItemCount == selectedcount) {
           //            $("#chkItem" + j + ".headerrow").prop('checked', true);
           //            $("#chkItem" + j + ".headerrow").prop('disabled', false);
           //        }
                   
           //    }
           //}
           // call on check box change event
           $('input:checkbox').on('change', function () {
               //alert('hi');
               if (!$(this).hasClass("headerrow")) {
                   for (var i = 1; i <= ItemCount; i++) {
                       if ($('.row' + i).is(':checked')) {
                           var disSelectedcount = 0;
                           var count = $('.row' + i).length;
                           for (var j = 1; j <= count; j++) {
                               const isChecked = $("#chkItem" + j + ".row" + i).is(":checked");
                               var approvalstatusrow = $("#chkItem" + j + ".row" + i).attr("approvalstatus");
                              // alert(isChecked);
                               if (isChecked) {
                                   if (approvalstatusrow == 'Approved') {

                                        $("#chkItem" + j + ".row" + i).prop('disabled', true);
                                   }
                                   else {
                                       $("#chkItem" + j + ".row" + i).prop('disabled', false);
                                   }
                               } else {
                                   
                                   if (approvalstatusrow == 'Approved')
                                   {
                                       $("#chkItem" + j + ".row" + i).prop('disabled', true);
                                   }
                                   else {
                                       disSelectedcount = disSelectedcount + 1;
                                       $("#chkItem" + j + ".row" + i).prop('disabled', true);
                                   }
                               }

                             
                              //  alert(approvalstatusrow.length);
                               
                           }
                           if (disSelectedcount == count) {
                              
                               $('.row' + i).prop('disabled', false);
                           }
                       }
                       else {
                           var count1 = $('.row' + i).length;
                           for (var j = 1; j <= count1; j++) {
                               var approvalstatusrow = $("#chkItem" + j + ".row" + i).attr("approvalstatus");
                               if (approvalstatusrow == 'Approved') { $("#chkItem" + j + ".row" + i).prop('disabled', true); } else {
                                   $("#chkItem" + j + ".row" + i).prop('disabled', false);
                               }
                           }
                           
                       }
                   }
               }
               
           });

           $('.headerrow').on('change', function () {
               var id = $(this).attr("id");
               var count = 0;

               for (i = 1; i <= ItemCount; i++) {
                   count = $('.row' + i).length;
                   for (var j = 1; j <= count; j++) {
                       var cellid = $("#chkItem" + j + ".row" + i).attr("id");
                       if (id == cellid) {
                           if ($(this).is(":checked")) {
                               $("#chkItem" + j + ".row" + i).prop('disabled', false);
                               $("#chkItem" + j + ".row" + i).prop('checked', true);
                           }
                           else {
                               var headercellchecked = $("#chkItem" + j + ".headerrow").is(":checked");
                               if (headercellchecked) {
                                   $("#chkItem" + j + ".headerrow").prop('checked', false);
                               }
                               $("#chkItem" + j + ".row" + i).prop('checked', false);
                               $("#chkItem" + j + ".row" + i).prop('disabled', true);               
                           }                                               
                       }
                       else {
                           var headercellchecked = $("#chkItem" + j + ".headerrow").is(":checked");
                           if (headercellchecked) {
                               $("#chkItem" + j + ".headerrow").prop('checked', false);
                           }
                           $("#chkItem" + j + ".row" + i).prop('checked', false);
                           $("#chkItem" + j + ".row" + i).prop('disabled', true);
                       }

                       var approvalstatusheaderrow = $("#chkItem" + j + ".headerrow").attr("approvalstatus");
                       //alert(approvalstatusheaderrow.length);
                       if (approvalstatusheaderrow.length > 0) { $("#chkItem" + j + ".headerrow").prop('disabled', true); }
                   }
               }
               var totalcount = ItemCount * count;
              // console.log("Count", totalcount);
               var totalnumberOfunchecked = 0;
               for (i = 1; i <= ItemCount; i++) {
                   for (j = 1; j <= headercellcount; j++) {
                       var numberOfunchecked = $("#chkItem" + j + ".row" + i + ":not(\":checked\")").length;
                       totalnumberOfunchecked = totalnumberOfunchecked + numberOfunchecked;
                   }
               }
              // console.log("unchecked", totalnumberOfunchecked);
               if (totalcount == totalnumberOfunchecked) {
                   for (i = 1; i <= ItemCount; i++) {
                   for (j = 1; j <= headercellcount; j++) {
                       
                       $("#chkItem" + j + ".row" + i).prop('checked', false);
                       $("#chkItem" + j + ".row" + i).prop('disabled', false);
                       }
                             
                   }
               }
               
               
           });

           //$(".form-check-input").change(function () {
           //    if ($(this).is(':checked')) {
           //        $(this).closest("tr").find("input.form-check-input").prop("disabled", true);
           //    } else { //unchecked
           //        $(this).closest("tr").find("input.form-check-input").prop("disabled", false);
           //    }
           //});
       });
   </script>
<script type="text/javascript">
    function Checked(ele) {
        //$(":checkbox").on("change", function () {
        //    var chx = $(this).is(':checked');
        //    $(this).closest('tr').find('input:checkbox').prop("disabled", chx);
        //});
        var BidItemId = $(ele).attr("data-id");
        var ProductAccepted = "0";
        var isChecked = $(ele).is(":checked");
        if (isChecked) {
            ProductAccepted = "1";
        }
        else {
            ProductAccepted = "0";
        }

        BidItemId = parseInt(BidItemId);
      //  alert(ProductAccepted);
        $.ajax({
            url: "SMDPOAnalyzer.aspx/UpdateBidItemStatus",
            data: "{'ProductAccepted': '" + ProductAccepted + "','BidItemId': '" + BidItemId + "'}", 
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                getSelectedItemAmount();
               // alert('Hi');
                if (data.d > 0) {
                    //alert("Bid Item updated Successfully");
                }
            },
            error: function (data, status, jqXHR) {
                alert("Error while Updating Bid Item of :" + BidItemId);
               // alert(jqXHR);
            }
        });
    }

    function CheckedAll(ele) {
        //$(":checkbox").on("change", function () {
        //    var chx = $(this).is(':checked');
        //    $(this).closest('tr').find('input:checkbox').prop("disabled", chx);
        //});
        var BidId = $(ele).attr("bidid");
        var PoId = $(ele).attr("poid");
        var ProductAccepted = "0";
        var isChecked = $(ele).is(":checked");
        if (isChecked) {
            ProductAccepted = "1";
        }
        else {
            ProductAccepted = "0";
        }

        BidId = parseInt(BidId);
        PoId = parseInt(PoId);
        //  alert(ProductAccepted);
        $.ajax({
            url: "SMDPOAnalyzer.aspx/UpdateAllBidItemStatusforBid",
            data: "{'ProductAccepted': '" + ProductAccepted + "','BidId': '" + BidId + "','PoId': '" + PoId + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                getSelectedItemAmount();
                // alert('Hi');
                if (data.d > 0) {
                    //alert("Bid Item updated Successfully");
                }
            },
            error: function (data, status, jqXHR) {
                alert("Error while Updating all product Item of :" + BidId);
                // alert(jqXHR);
            }
        });
    }
</script>
    <script language="javascript" type="text/javascript"> 
        function getSelectedItemAmount() {
            var bidCount = $("#hdnBidCount").val();
            var ItemCount = $("#hdnItemCount").val();
            for (var i = 1; i <= bidCount; i++) {
                var selectedItemTotal = 0;
                for (var j = 1; j <= ItemCount; j++) {
                    const isChecked = $(".lblrow" + i + j).is(":checked");
                    if (isChecked) {
                        var selectedItemAmt = $("#lblselectedrow" + i + j).attr("ItemAmount");
                        selectedItemTotal += parseFloat(selectedItemAmt);
                    }
                }
                $("#lblSelectedItemTotal" + i).text(selectedItemTotal.toFixed(2));
            }
        }
        $(document).ready(function () {
            getSelectedItemAmount();
        });
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
        <asp:Button runat="server" id="btnApprove" OnClick="Approve_Click" style="display:none"/>
        <asp:Button runat="server" id="btnCancel" OnClick="Cancel_Click" style="display:none"/>
        <asp:TextBox runat="server" id="txtParam" style="display:none"></asp:TextBox>
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style="border-collapse:collapse; text-align:left;font-family:Arial;font-size:12px; ">
            <tr class="text headerband">
                <td style="padding:7px;">
                     Quote Analyzer 
                     <div style="float:right;padding-right:5px;">
                        
        <asp:ImageButton id="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents"/> 
    (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>) 
     &nbsp;&nbsp;
                     </div>
                </td>
            </tr>
            <tr>
                <td>    
                    <div id="div" runat="server"  >
                    </div>
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:center; ">
                    <div id="divCloseReq" runat="server" visible="false">
                        <asp:Label ID="lblCloseReqMsg" runat="server" ForeColor="Red" Text="If no more PO to be issued for this requisition then click here to close remaining quotations."></asp:Label> <br />
                        <asp:Button ID="btnCloseReq" runat="server" Text="Close Remaining Quotations" CssClass="btn" OnClick="btnCloseReq_Click" OnClientClick="return confirm('Are you sure to close remaining quotations ?')"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; padding-right:0px;">
                    <img src="../../HRD/Images/close.jpg" alt="Close" style="cursor:pointer; border:none;" title="Close"  onclick="window.close(); return false;"/>
                </td>
            </tr>
        </table>
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
    
    
    </form>
</body>
</html>
