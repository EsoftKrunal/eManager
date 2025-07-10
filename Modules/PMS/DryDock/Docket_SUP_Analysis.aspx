<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Docket_SUP_Analysis.aspx.cs" Inherits="Docket_SUP_Analysis1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <%--<script src="../JS/KPIScript.js" type="text/javascript"></script>--%>

    <script src="../JQ_Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <link href="../../../css/app_style.css" rel="stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">

        $('.number').keypress(function (event) {
            alert('a');
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
              ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }

            var text = $(this).val();

            if ((text.indexOf('.') != -1) &&
              (text.substring(text.indexOf('.')).length > 2) &&
              (event.which != 0 && event.which != 8) &&
              ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });

        function SaveDiscount(ctrl)
        {
            var rfq_id = $(ctrl).attr('rfqid');
            rfq_id = parseInt(rfq_id);
            
            $("#hfDicountedValue").val($(ctrl).val());
            $("#hfRfqIDToUpdate").val(rfq_id);
            
            $("#btnTempClick").click();
            //$.ajax({
            //    url: 'Docket_SUP_Analysis.aspx/Update_discount',
            //    data: "{ 'rfqid': '" + rfq_id + "','val': '" + $(ctrl).val() + "'}",
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        alert(data.d);
            //        var myobj = JSON.parse(data.d);

            //        objpricelc.html(parseFloat( myobj.priceLC).toFixed(2));
            //        priceusd.html(parseFloat(myobj.priceUSD).toFixed(2));

                    
            //    },
            //    error: function (data, status, jqXHR) { alert(jqXHR); }
            //});

        }

    </script>
    <script type="text/javascript" language="javascript">
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>
    <style type="text/css">
    body
    {
        font-family:Calibri;
        font-size:12px;
        margin:0px;
    }
    .btn
    {
        background-color:#0066FF;
        color:White;
        border:none;
        padding:5px;
    }
    .btn:hover
    {
        background-color:#0052CC;
        color:White;
        border:none;
        padding:5px;
    }
    .right_grey_border
    {
        border-right:solid 1px #e2e2e2;
    }
    .right_align
    {
        text-align:right;
    }
    .hover_highlight_cat
     {
         vertical-align:middle;
         color:Red;
         height:20px;
         cursor:pointer;
         border-bottom:solid 1px #e2e2e2;
     }
    .hover_highlight_cat_active
    {
        background-color:yellow;
        color:Black;
    }
    
    
    .hover_highlight_job
     {
         vertical-align:middle;
         color:blue;
         height:20px;
         cursor:pointer;
         border-bottom:solid 1px #e2e2e2;
     }
     
    .hover_highlight_job_active
    {
        background-color:yellow;
        color:Black;
    }
 
    .hover_highlight_subjob
     {
         vertical-align:middle;
         color:Black;
         height:20px;
         cursor:pointer;
         border-bottom:solid 1px #e2e2e2;
     }
     
    .hover_highlight_subjob_active
    {
        background-color:yellow;
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
        margin:0px;
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
    {
        color:White;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style=" text-align:center;  font-size:15px; " class="text headerband"><b>SUPTD. QUOTE ANALYSIS</b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="0">
        <tr><td>
            
            <asp:Button ID="btnTempClick" runat="server" OnClick="btnTempClick_OnClick" style="display:none;" />
            <asp:HiddenField ID="hfDicountedValue" runat="server" />
            <asp:HiddenField ID="hfRfqIDToUpdate" runat="server" />
        <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; width:">Docket # :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label>
                <asp:ImageButton id="btnDocketView" runat="server" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" OnClick="btnDocketView_Click" />
            </td>
            <td style="text-align:right">Vessel :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right">Type :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td style="text-align:right">Plan Duration :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label></td>
            </tr>
            </table>
        </div>
        </td></tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
         <div>
            
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr>
         <td>
            <div style="float:left; padding:2px;">
                <asp:Button runat="server" ID="btnLevel1" Text="Show Level-1" OnClick="btnLevel_Click" CommandArgument="1" CssClass='btn'/>
                <asp:Button runat="server" ID="btnLevel2" Text="Show Level-2" OnClick="btnLevel_Click" CommandArgument="2" CssClass='btn'/>
                <asp:Button runat="server" ID="btnLevel3" Text="Show Level-3" OnClick="btnLevel_Click" CommandArgument="3" CssClass='btn'/>
            </div>
         </td>
         </tr>
         </table>
         <table cellpadding="0" width="100%" cellspacing="0" style="background-color:#99CCFF;">
                    <tr>
                         <td style="padding:4px; text-align:right; font-weight:bold; font-size:12px; width:130px;">Select Job Category :</td>
                         <td style="padding:4px; text-align:left; width:250px;"><asp:DropDownList ID="ddlJobCategory" CssClass="newinput" AutoPostBack="true" OnSelectedIndexChanged="ddlJobCategory_Click" runat="server" Width="240px"></asp:DropDownList></td>

                         <td style="padding:4px; text-align:right; font-weight:bold; font-size:12px; width:70px;">Select Job :</td>
                         <td style="padding:4px; text-align:left;"><asp:DropDownList ID="ddlJob" AutoPostBack="true" CssClass="newinput" OnSelectedIndexChanged="ddlJob_Click" runat="server" Width="400px"></asp:DropDownList></td>

                         <td style="padding:4px; text-align:right; font-weight:bold; font-size:12px;">Cost Type :</td>
                         <td style="padding:4px; text-align:left; font-weight:bold; font-size:12px;"> 
                         <asp:DropDownList runat="server" ID="ddlYC" AutoPostBack="true" OnSelectedIndexChanged="ddlJobCategory_Click" >
                         <asp:ListItem Text="All Cost" Value=""></asp:ListItem>
                         <asp:ListItem Text="Shipyard Supply Costs" Value="Y"></asp:ListItem>
                         <asp:ListItem Text="Owner’s Supply Shipyard Costs" Value="N"></asp:ListItem>
                         </asp:DropDownList>
                         </td>
                         <td style='width:200px'>
                         <asp:CheckBox runat="server" Text="Outside Repair Only." ID="chkOut" AutoPostBack="true" OnCheckedChanged="chkOut_OnCheckedChanged" />
                         </td>
                         </tr>
                </table>

         <table style="width:100%; border-collapse:collapse" rules="none" cellpadding="0" cellspacing="0" border="0" id="container">
         <tr style=" background-color:#99CCFF; font-size:15px;">
            <td style="padding:4px; width:400px;"><b>Job Details</b></td>
            <td style="padding:4px;"><b>Quotations :</b></td>
         </tr>
         <tr>
            <td>
                <div style='width:100%'>
                    <div class='columnHead'>&nbsp;</div>
                    <div class="ScrollAutoReset" style="overflow-x:hidden;overflow-y:hidden;height:460px;" id="d936" onscroll="ScrollTo1();"><asp:Literal runat="server" ID="litLeftHead"></asp:Literal></div>
                    <div class='columnHead' style="height:23px;text-align:right; font-weight:bold; "><div style="padding-top:3px;">Total :&nbsp;</div></div>
                    <div class='columnHead' style="height:23px;text-align:right; font-weight:bold; "><div style="padding-top:3px;">Final Yard Discount(%) :&nbsp;</div></div>
                    <div class='columnHead' style="height:23px;text-align:right; font-weight:bold; "><div style="padding-top:3px;">Net Total :&nbsp;</div></div>
                </div>
            </td>
            <td>
                <div class='columnHead' style="overflow-x:hidden;overflow-y:scroll; width:800px; position:relative;" id="dcolh">
                <div style="height:20px;padding-top:5px;">
                    <asp:Literal runat="server" ID="litColumnHeader1"></asp:Literal>
                </div>
                <div style="height:25px;">
                    <asp:Literal runat="server" ID="litColumnHeader"></asp:Literal>
                </div>
                </div>
                <div class="ScrollAutoReset" style="overflow-x:scroll;overflow-y:scroll;width:800px; height:460px;" id="d569" onscroll="ScrollTo2();">
                <asp:Literal runat="server" ID="litData"></asp:Literal>
                </div>
                <div class='columnHead' style="overflow-x:hidden;overflow-y:scroll; width:800px; position:relative; height:69px;" id="dcolf">
                <div style="height:61px;">
                    <asp:Literal runat="server" ID="litColumnFooter"></asp:Literal>
                </div>
                </div>
            </td>
         </tr>
         <tr style=" background-color:#1947A3; font-size:15px;">
            <td  style="padding:4px;" colspan="2"><b>&nbsp;</b></td>
         </tr>
         </table>
         
         </div>
         <div style="text-align:right; padding:3px;">
            <div style="text-align:left;float:left; padding-top:5px;">
                 <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </div>
         </div>
        </td> 
    </tr>
    </table>
    </div>
    <div>
    <span style="float:left;color:Red; font-size:14px;"><b>Please click Notify to Technical Manager once docket specs is ready.</b></span>
    <center>
    <asp:Button runat="server" ID="btnPrint" Text="Print Comparison" OnClick="btnAskPrint_Click" CommandArgument="3" style=" padding:3px; border:none; color:White; background-color:Red; " Width="130px"/>
    <asp:Button runat="server" ID="btnNotifyToGM" Text="Notify to Technical Manager" OnClick="btnNotifyGM_Click"  Width="150px" OnClientClick="return ConfirmNotify(this);" CommandArgument="4" style=" padding:3px; border:none; color:White; background-color:Red; "/>
    <asp:Button runat="server" ID="btnGMApproval" Text="Approval" OnClick="btnGMApproval_Click"  Width="130px" OnClientClick="return ConfirmApprove(this);" CommandArgument="4" style=" padding:3px; border:none; color:White; background-color:Red; "/>
    </center>
    </div>
     <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; display:none;" id="pnl_Adjustments">
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
            <div style="position: relative; width: 1000px; height: 550px; padding: 3px; text-align: left;background: white; z-index: 150; top: 80px; border: solid 10px black;">
            <div style='font-size:23px; font-weight:bold; padding:5px; background-color:#e2e2e2;'>Modify RFQ Details</div>
                <div id="DYNTEXT" style="padding:5px">
                
                </div>
                <div style="text-align:right; padding:0px; bottom:5px; position:absolute; width:100%;margin-right:10px;">
                    <asp:Button runat="server" Text="Close" style=" padding:3px; border:none; color:White; background-color:#FF4719; width:80px;" OnClick="btnClose_Click" />
                    <asp:HiddenField ID="hfSJCode" runat="server" />
                </div>
            </div>
            </center>
        </div>
    <script type="text/javascript">
        function ScrollTo1() {
            $("#d569").scrollTop($("#d936").scrollTop());
        }
        function ScrollTo2() {
            $("#d936").scrollTop($("#d569").scrollTop());
            $("#dcolh").scrollLeft($("#d569").scrollLeft());
            $("#dcolf").scrollLeft($("#d569").scrollLeft());


        }

        $(document).ready(function () {

            $(".hover_highlight_cat").mouseenter(function () {
                var name = $(this).attr('name');
                $("[name=" + name + "]").addClass('hover_highlight_cat_active');
            });

            $(".hover_highlight_cat").mouseleave(function () {
                var name = $(this).attr('name');
                $("[name=" + name + "]").removeClass('hover_highlight_cat_active');
            });




            $(".hover_highlight_job").mouseenter(function () {
                var name = $(this).attr('name');
                $("[name=" + name + "]").addClass('hover_highlight_job_active');
            });

            $(".hover_highlight_job").mouseleave(function () {
                var name = $(this).attr('name');
                $("[name=" + name + "]").removeClass('hover_highlight_job_active');
            });




            $(".hover_highlight_subjob").mouseenter(function () {
                var name = $(this).attr('name');
                $("[name=" + name + "]").addClass('hover_highlight_subjob_active');
            });

            $(".hover_highlight_subjob").mouseleave(function () {
                var name = $(this).attr('name');
                $("[name=" + name + "]").removeClass('hover_highlight_subjob_active');
            });

            $("#dcolh").width($("#container").width()-400);
            $("#d569").width($("#container").width() - 400);
            $("#dcolf").width($("#container").width() - 400);
        });

    </script>
    <script type="text/javascript">

        function Update_Values(txt) 
         {
             var RFQId = $(txt).attr("RFQId");
             var SubJobCode = $("#lblJobCode").html();

             var ParentTable = $(txt).parent().parent().parent();

             var QuoteQty = ParentTable.find("[id$='QTY']").val();
             var Rate = ParentTable.find("[id$='UP']").val();
             var Disc = ParentTable.find("[id$='DISC']").val();
             var Rem = ParentTable.find("[id$='txRem']").val();

             var ctlNET = $(txt).parent().parent().find("[id$='NET']");

             $.post('UpdatePO.ashx', {
                 Type: "SUPQuoteQty",
                 RFQId: RFQId,
                 SubJobCode: SubJobCode,
                 QuoteQty: QuoteQty,
                 Rate: Rate,
                 Disc: Disc,
                 Remark: Rem
             },
            function (data, status) {
                var arr = data.split(',');
                $(ctlNET).html(arr[0]);
            }
            );
         }

         function ShowAdjustmentPanel(img) {
             var dcid = $(img).attr('docketid');
             var subjobcode = $(img).attr('subjobcode');
             var url = '<%=ResolveClientUrl("~/RFQRepeater.aspx")%>';
             $("#hfSJCode").val(subjobcode);             
             $("#DYNTEXT").load(url + '?DocketId=' + dcid + '&JC=' + subjobcode + "&rnd=" + Math.random());
             $("#pnl_Adjustments").fadeIn();
             
         }
         function CloseAdjustmentPanel() {
            $("#pnl_Adjustments").fadeOut();
         }
         function OpenRFqPrint(rfqid, mode, l, jcid, jid) {
             window.open('PrintRFQComparison.aspx?Sup=1&RFQId=' + rfqid + '&CostType=' + mode + "&ReportLevel=" + l + "&CATID=" + jcid + "&DOCKETJOBID=" + jid, '');
         }
         function OpenRFQ(rfqid) {
            // window.open('PublishRFQSUP.aspx?RFQId=' + rfqid, '');
         }
         function OpenRFQExcel(rfqid) {
             window.open('SupRFQExcel.aspx?RFQId=' + rfqid, '');
         }
         
    </script>
    <script type="text/javascript">
        function ConfirmNotify(ctl) {
            if (window.confirm('Are you sure to notify to GM/DGM ?')) {
                $(ctl).val('Processing..');
                return true;
            }
            else
                return false;
        }
        //----------------------------
        function ConfirmApprove(ctl) {
            if (window.confirm('Are you sure to approve ?')) {
                $(ctl).val('Processing..');
                return true;
            }
            else
                return false;
        }
    </script>

    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_RFQ" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
                            <div style="position: relative; width: 700px; padding: 3px; text-align: center;background: white; z-index: 150; top: 100px; border: solid 10px black;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel221411">
                                <ContentTemplate>
                                <center><div style="font-size:18px; padding:5px;">Select Any 4 Or less RFQ For Comparison Print<br />
                                <span style="font-size:12px;color:Blue;"><i> ( if select more than 4 only first 4 will be printed ) </i> </span>
                                </div></center>
                                <table cellpadding="3" rules="all" border="1" cellspacing="0" width="100%" bordercolor="#c2c2c2" style="border-collapse:collapse" >
                                <tr class= "headerstylegrid">
                                <td style='width:150px'>RFQ#</td>
                                <td>Yard Name</td>
                                <td style="width:30px">Select</td>
                                </tr>
                                <asp:Repeater runat="server" id="rptRFQ">
                                <Itemtemplate>
                                <tr>
                                <td style="text-align:left; font-size:13px;"><%#Eval("RFQNo")%></td>
                                <td style="text-align:left; font-size:13px;"><%#Eval("YardName")%></td>
                                <td><asp:CheckBox runat="server" ID='chkSelect' arg='<%#Eval("RFQid")%>'/></td> 
                                </tr>
                                </Itemtemplate>
                                </asp:Repeater>
                                </table>
                                <div>
                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radMode">
                                    <%--<asp:ListItem Text="All Cost" Value="" Selected="True"></asp:ListItem>--%>
                                    <asp:ListItem Text="Owner’s Supply Shipyard Costs" Value="N" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Shipyard Supply Costs" Value="Y"></asp:ListItem>
                                </asp:RadioButtonList>
                                </div>
                                <br />
                                <div style="text-align:center;">
                                    <asp:Button runat="server" ID="Button1" Text="Print" OnClick="btnPrint_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                    <asp:Button runat="server" ID="btnClosePrint" Text="Close" OnClick="btnClosePrint_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                </div>

                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnPrint" />
                                  <asp:PostBackTrigger ControlID="btnClosePrint" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </div>
    </center>
    </div>
    </form>
</body>
    <script type="text/javascript">
        $('.number').keypress(function (event) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
              ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }

            var text = $(this).val();

            if ((text.indexOf('.') != -1) &&
              (text.substring(text.indexOf('.')).length > 2) &&
              (event.which != 0 && event.which != 8) &&
              ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });
    </script>
</html>
