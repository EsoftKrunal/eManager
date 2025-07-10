<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostTracking.aspx.cs" Inherits="Docket_CostTracking" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
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
    .right_align
    {
        text-align:right;
    }
    
    .hover_highlight_cat
     {
         vertical-align:middle;
         color:Red;
         height:25px;
         cursor:pointer;
         vertical-align:middle;
         border-bottom:solid 1px #dddddd;
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
         height:25px;
         cursor:pointer;
         border-bottom:solid 1px #dddddd;
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
         height:25px;
         cursor:pointer;
         border-bottom:solid 1px #dddddd;
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
    </style>
</head>
<body style="margin:0px; padding-bottom:160px;">
    <div id="dvMain"  style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40); display:none; "><div style="font-size:25px; font-weight:bold; text-align:center; vertical-align:middle; color:White; top:400px; padding:200px 0px;">Loading.......</div></div> 
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
      <div style="border:none; background-color : #ADD6FF; font-size:13px; position:fixed; top:0px; width:100%; height:105px;">
            <div style="background-color:#1947A3;color:White; text-align:center; padding:5px; font-size:15px; "><b>COST TRACKING</b></div>
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; width:"><b>Docket #</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label>
                <asp:ImageButton id="btnDocketView" runat="server" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" OnClick="btnDocketView_Click" />
            </td>
            <td style="text-align:right"><b>Vessel</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right"><b>Type</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td style="text-align:right"><b>DD Duration</b> :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label></td>
            </tr>
            </table>
            <div style="padding:4px"> 
             <asp:DropDownList runat="server" ID="ddlJObCat" AutoPostBack="true" OnSelectedIndexChanged="LoadCat_SelectIndexChanged"></asp:DropDownList>
                &nbsp;&nbsp;
                <asp:DropDownList runat="server" ID="ddlCostCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlCostCategory_SelectIndexChanged">
                    <asp:ListItem Text="< -- Select Cost Category -- >" Value=""></asp:ListItem>
                    <asp:ListItem Text="Shipyard Supply Costs" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="Owner’s Supply Shipyard Costs" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="height:25px;">
                <table cellspacing='0' rules='all' border='1' bordercolor='black' cellpadding='0' style='border-collapse:collapse; width:100%; height:25px; background-color:orange;'>
                    <tr>
                    <td style='width:300px; text-align:center;'>Job Details</td>
                    <td style='width:100px; text-align:center;'>Budgeted Costs</td>
                    <td style='width:60px; text-align:center;'>Est. Qty</td>
                    <td style='width:80px; text-align:center;'>Unit Price</td>
                    <td style='width:60px; text-align:center;'>% Disc.</td>
                    <td style='width:100px; text-align:center;'>Est. Costs</td>
                    <td style='width:80px; text-align:center;'>Var(Costs)</td>
                    <td style='width:80px; text-align:center;'>Var(%)</td>
                    <td style='text-align:center;'>Remarks</td>
                    </tr>
                </table>
            </div>
        </div>
      <div style="border:none; background-color : #ADD6FF; padding:0px; position:fixed; bottom:0px;width:100%; height:185px; vertical-align:top;">
      <div style="height:25px;">
                <table cellspacing='0' rules='all' border='1' bordercolor='black' cellpadding='0' style='border-collapse:collapse; width:100%; height:25px; background-color:orange;'>
                    <tr>
                    <td style='width:300px; text-align:right;'>&nbsp;Total Costs : &nbsp;</td>
                    <td style='width:100px; text-align:right;'><asp:Label runat="server" ID="lblPOSUM"></asp:Label>&nbsp;</td>
                    <td style='width:60px; text-align:right;'>&nbsp;</td>
                    <td style='width:80px; text-align:right;'>&nbsp;</td>
                    <td style='width:60px; text-align:right;'>&nbsp;</td>
                    <td style='width:100px; text-align:right;'> <asp:Label runat="server" ID="lblTotal_EstAmount_USD"></asp:Label>&nbsp;</td>
                    <td style='width:80px; text-align:right;'><asp:Label runat="server" Font-Bold="true" ID="lblTotal_Variance" style="text-decoration:none;"></asp:Label>&nbsp;</td>
                    <td style='width:80px; text-align:right;'><asp:Label runat="server" Font-Bold="true" ID="lblTotal_VariancePer" style="text-decoration:none;"></asp:Label>&nbsp;</td>
                    <td style='text-align:center;'>&nbsp;</td>
                    </tr>
                </table>
            </div>
      <table width="100%">
      <tr>
      <td>
      <asp:UpdatePanel runat="server" ID="up1">
      <ContentTemplate>
      <table cellspacing='2' rules='all' border='0' bordercolor='black' cellpadding='3' style='border-collapse:collapse; width:100%; font-size:14px;'>
        <tr>
            <td style='width:200px;text-align:right;'>Budgeted Costs (US$) :&nbsp;</td>
            <td style='width:150px;text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblTotal_PONetAmount_USD"></asp:Label></td>
            <td style='text-align:right;'>Shipyard Supply Budgeted Costs (US$) :&nbsp;</td>
            <td style='width:150px; text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblTotal_PONetAmount_Yard" ></asp:Label></td>
            <td style='text-align:right;'>Owner’s Supply Shipyard Budgeted Costs (US$) :&nbsp;</td>
            <td style='width:150px; text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblTotal_PONetAmount_Owner"></asp:Label></td>
        </tr>
        <tr>
            <td style='text-align:right;'>Estimated Costs (US$) :&nbsp;</td>
            <td style='text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblEstUsd"></asp:Label></td>
            <td style='text-align:right;'>Shipyard Supply Costs (US$) :&nbsp;</td>
            <td style='text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:LinkButton runat="server" Font-Bold="true" OnClick="TotalYardCostPrint" ID="lblTotalYardCost" ></asp:LinkButton></td>
            <td style='text-align:right;'>Owner’s Supply Shipyard Costs (US$) :&nbsp;</td>
            <td style='text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:LinkButton runat="server" OnClick="TotalOwnerCostPrint" Font-Bold="true" ID="lblTotalNonYardCost"></asp:LinkButton></td>
        </tr>
        <tr>
        <td colspan="2">&nbsp;</td>
        <td style='text-align:right;'>Yard Discount (%) :&nbsp;&nbsp;</td>
        <td style='text-align:right; background-color:#B2F0FF;'>
            <asp:TextBox ID="txtYardDiscPer" runat="server" AutoPostBack="true" class="newinput" Font-Bold="true" OnTextChanged="txtYardDiscPer_OnTextChanged" style="text-align:right; width:80%;"></asp:TextBox>
            </td>
        <td colspan="2">&nbsp;</td>
        </tr>
          <tr style="display:none">
              <td colspan="2"></td>
              <td style="text-align:right;">Final Yard Discount (US$) :&nbsp;</td>
              <td style="text-align:right; background-color:#B2F0FF;">&nbsp;<asp:TextBox ID="txtFinalDiscount" runat="server" AutoPostBack="true" class="newinput" Font-Bold="true" OnTextChanged="txtFinalDiscount_OnTextChanged" style="text-align:right; width:80%;"></asp:TextBox>
              </td>
              <td colspan="2"></td>
          </tr>
        <tr>
        <td colspan="2"></td>
        <td style='text-align:right;'>Final Yard Cost (US$) :&nbsp;</td>
        <td style='text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblFinalYardCost"></asp:Label></td>
        <td colspan="2"></td>
        </tr>
        <tr>
        <td style='text-align:right;'>Final DD Costs (US$) :&nbsp;</td>
        <td style='text-align:right; background-color:#B2F0FF;'>&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblTotalAmount"></asp:Label></td>
        <td colspan="4"></td>
        </tr>
        </table> 
      </ContentTemplate>
      </asp:UpdatePanel>
      </td>
      <td style="text-align:center; padding:10px;">
        <asp:Button runat="server" ID="btnAddJob" Text="Add Extra Job" OnClick="btnAddJob_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;" />   
      </td>
      </tr>
      </table>
                
        </div>
      <div style="display:none; text-decoration:none;">
             <asp:TextBox ID="txtForDate" runat="server"></asp:TextBox>
             <asp:Button ID="btnReLoadData" OnClick="btnReLoadData_Click" runat="server" />
             <asp:Label runat="server" ID="Label1"></asp:Label>
      </div>
    <div style="width:100%;padding-top:105px;">
         <table style="width:100%" cellpadding="0" cellspacing="0" border="0">
         <tr>
            <td>
                
                <div style="padding:0px;">
                    <asp:Literal runat="server" ID="litData"></asp:Literal>
                </div>
            </td>
         </tr>
         </table>
         
         <div style="text-align:right; padding:3px;">
            <div style="text-align:left;float:left; padding-top:5px;">
                 <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div style="text-align:left;float:right; padding:3px;">
                
            </div>
         </div>
     </div>
     </div>

    <%-- Add Sub Jobs --%>

    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_A_SubJobs" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 800px; height: 580px; padding: 3px; text-align: center;background: white; z-index: 150; top: 20px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel411">
                                <ContentTemplate>
                                 <asp:Panel runat="server" id="pnlNew">
                                   <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                   <tr>
                                        <td colspan="2" style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;" >
                                            Create New Job
                                        </td>
                                   </tr>
                                   <tr>
                                        <td style="text-align:right; width:120px;">Category :</td>
                                        <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlCat"  Width="99%" AutoPostBack="true" OnSelectedIndexChanged="ddlCat_OnSelectedIndexChanged" ></asp:DropDownList></td>                  
                                   </tr>
                                   <tr>
                                        <td style="text-align:right; width:120px;">Sub Category :</td>
                                        <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlSubCat"  Width="99%" ></asp:DropDownList></td>                  
                                   </tr>
                                   <tr>
                                        <td style="text-align:right; width:120px;">Short Descr :</td>
                                        <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_SubJobName"  Width="99%" TextMode="MultiLine" Height="100px" ></asp:TextBox></td>                  
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                        <td style="text-align:right;">Long Descr :</td>
                                        <td style="text-align:left;">
                                            <asp:TextBox runat="server" ID="txt_A_LongDescr" TextMode="MultiLine" Width="99%" Height="120px" ></asp:TextBox>
                                        </td>
                                   </tr>
                                   <tr >
                                            <td style="text-align:right;">Bid Qty :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_SubJobBidQty"  Width="200px" MaxLength="50"  ></asp:TextBox></td>                  
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Unit :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_SubJobunit"  Width="200px" MaxLength="50"  ></asp:TextBox></td>                  
                                   </tr>
                                   <tr>
                                        <td style="text-align:right;">Cost Category :</td>
                                        <td style="text-align:left;">
                                            <asp:RadioButton ID="rdo_A_YardCost" Text="Yard Cost" runat="server" GroupName="CC" Checked="true" />
                                            <asp:RadioButton ID="rdo_A_NonYardCost" Text="Non Yard Cost" runat="server" GroupName="CC" />
                                        </td>
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                        <td style="text-align:right;">Outside Repair :</td>
                                        <td style="text-align:left;">
                                            <asp:CheckBox ID="chk_A_OutsideRepair" runat="server"  />                                                       
                                        </td>
                                   </tr>
                                   <tr >
                                            <td style="text-align:right;">Attachment :</td>
                                            <td style="text-align:left;">
                                            <asp:FileUpload runat="server" id="ftp_A_Upload" />
                                            </td>                  
                                         </tr>
                                  <tr style="background-color:#DDF2FF;">
                                    <td style="text-align:right;">PO Qty :</td>
                                    <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_POQty"  Width="200px" MaxLength="50"  ></asp:TextBox></td>                  
                                   </tr>
                                   <tr>
                                    <td style="text-align:right;">Unit Price(USD) :</td>
                                    <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_UP"  Width="200px" MaxLength="50"  ></asp:TextBox></td>                  
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                   <td style="text-align:right;">Discount (%) :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_Disc"  Width="200px" MaxLength="3"  ></asp:TextBox></td>                  
                                   </tr>
                                   </table>
                                 </asp:Panel>
                                 <div style="text-align:center; padding:5px;">
                                            <div style="text-align:left;float:left; padding-top:5px;">
                                                <asp:Label ID="lbl_A_MsgSubJob" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:left;float:right">
                                                <asp:Button runat="server" ID="btn_A_SaveJob" Text="Save" OnClick="btn_A_SaveJob_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                                                <asp:Button runat="server" ID="btn_A_CloseJob" Text="Close" OnClick="btn_A_CloseJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            </div>
                                </div>

                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btn_A_CloseJob" />
                                  <asp:PostBackTrigger ControlID="btn_A_SaveJob" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
    </div>

     <%-- Show Job Details --%>
    <div id="dvComments" style="border:solid 10px #c2c2c2; background-color:White;display:none; position:absolute; top:100px; left:200px; height:300px; width:600px;display:none;" >
            <div style="background-color:Green;vertical-align:middle; text-align:center; padding-top:5px; height:20px; position:relative; color:White;box-sizing:border-box;">Job Description</div>
            <asp:UpdatePanel runat="server" id="UpdatePanel1">
             <ContentTemplate>
            <div style="display:none;">
                 <asp:Button ID="lnkJobDescr" Text="adsda" runat="server" OnClick="lnkJobDescr_Click" ></asp:Button>
                 <asp:TextBox ID="txtDocketJobId" runat="server" ></asp:TextBox>
                 <asp:TextBox ID="txtDocketSubJobId" runat="server" ></asp:TextBox>
                 <asp:TextBox ID="txtJobType" runat="server" ></asp:TextBox>
            </div>
            <div style='height:250px; position:relative;' id="dvJobDescr" runat="server">
               <asp:TextBox ID="txtJobDescr" runat="server" TextMode="MultiLine" Width="99%" Height="250px"></asp:TextBox>
            </div>
            <div style='height:250px; position:relative;' id="dvSubJobDescr" runat="server">
               <div style="text-align:center; font-weight:bold;">Short Description : </div>
               <asp:TextBox ID="txtShortDescr" runat="server" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox>
               <div style="text-align:center; font-weight:bold;">Long Description : </div>
               <asp:TextBox ID="txtLongDescr" runat="server" TextMode="MultiLine" Width="99%" Height="120px"></asp:TextBox>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <div style="height:20px; vertical-align:middle;box-sizing:border-box; padding-top:5px; text-align:center; background-color:c2c2c2; position:relative;"><a href="#" style="color:Red;" onclick="$('#dvComments').slideUp();">Close</a> </div>
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


            $("#d936").width($("#td1").width());
        });

       $(document).ready(function () {
            document.onselectstart = new Function("return false");
            var docWidth = document.documentElement.clientWidth || document.body.clientWidth;
            $("#dcolh").width(docWidth - 600 + 'px');
            $("#dcolf").width(docWidth - 600 + 'px');
            $("#d569").width(docWidth - 600 + 'px');
        });

        function ShowJobDesc(ctl) {
            $("#txtDocketJobId").val($(ctl).attr('DocketJobId'));
            $("#txtDocketSubJobId").val($(ctl).attr('DocketSubJobId'));            
            $("#txtJobType").val($(ctl).attr('Job'));            
            $("#lnkJobDescr").click();
            $("#dvComments").slideDown();
        }

        
    </script>

    <script type="text/javascript">

        function Update_Values(txt) {
            var RFQId = $(txt).attr("RFQId");
            var DocketId = $(txt).attr("DocketId");
            var DocketJobId = $(txt).attr("DocketJobId");
            var DocketSubJobId = $(txt).attr("DocketSubJobId");
            var SubJobCode = $(txt).attr("SubJobCode");

            //alert($(txt).parent().find("[id$='txtEstQty_" + SubJobCode + "']"));

            var EstQty = $(txt).parent().parent().find("[id$='txtEstQty_" + SubJobCode + "']").val();
            var EstUnitPrice = $(txt).parent().parent().find("[id$='txtEstUnitPrice_" + SubJobCode + "']").val();
            var EstDisc = $(txt).parent().parent().find("[id$='txtEstDisc_" + SubJobCode + "']").val();
            
            var Remarks = $(txt).parent().parent().find("[id$='txtRemarks_" + SubJobCode + "']").val();
            var txtEstAmount = $(txt).parent().parent().find("[id$='txtEstAmount_" + SubJobCode + "']");

            //alert(EstQty);
            //alert(EstAmt);
            //alert(Remarks);

            var lblVarAmt = $(txt).parent().parent().find("[id$='lblVarAmt_" + SubJobCode + "']");
            var lblVarPer = $(txt).parent().parent().find("[id$='lblVarPer_" + SubJobCode + "']");

            var lblCatNetAmt = $("#lblCatNetAmt_" + SubJobCode.substr(0, 2));
            var lblCatEstAmt = $("#lblCatEstAmt_" + SubJobCode.substr(0, 2));
            var lblCatVarAmt = $("#lblCatVarAmt_" + SubJobCode.substr(0, 2));
            var lblCatVarPer = $("#lblCatVarPer_" + SubJobCode.substr(0, 2));

            var lblJobNetAmt = $("#lblJobNetAmt_" + SubJobCode.substr(0, 6));
            var lblJobEstAmt = $("#lblJobEstAmt_" + SubJobCode.substr(0, 6));
            var lblJobVarAmt = $("#lblJobVarAmt_" + SubJobCode.substr(0, 6));
            var lblJobVarPer = $("#lblJobVarPer_" + SubJobCode.substr(0, 6));

            var lblTotalNet = $("#lblTotal_PONetAmount_USD");
            var lblTotalEst = $("#lblTotal_EstAmount_USD");

            var lblTotal_Variance = $("#lblTotal_Variance");
            var lblTotal_VariancePer = $("#lblTotal_VariancePer");
            
            var lblAmtYC = $("#lblTotalYardCost");
            var lblAmtNY = $("#lblTotalNonYardCost");

            $.post('UpdatePO.ashx', {
                Type: "ESTAMT",
                RFQId: RFQId,
                DocketId: DocketId,
                DocketJobId: DocketJobId,
                DocketSubJobId: DocketSubJobId,
                SubJobCode: SubJobCode,
                EstUnitPrice: EstUnitPrice,
                EstQty: EstQty,
                EstDiscPer: EstDisc,
                Remarks: Remarks
            },
            function (data, status) {
                var arr = data.split('|');

                $(lblCatNetAmt).html(arr[0]);
                $(lblCatEstAmt).html(arr[1]);
                $(lblCatVarAmt).html(arr[2]);
                $(lblCatVarPer).html(arr[3]);

                $(lblJobNetAmt).html(arr[4]);
                $(lblJobEstAmt).html(arr[5]);
                $(lblJobVarAmt).html(arr[6]);
                $(lblJobVarPer).html(arr[7]);

                $(lblVarAmt).html(arr[8]);
                $(lblVarPer).html(arr[9]);

                $(lblTotalNet).html(arr[10]);
                $(lblTotalEst).html(arr[11]);
                //                $(lblAmtYC).html(arr[12]);
                //                $(lblAmtNY).html(arr[13]);
                $(lblAmtYC).val(arr[12]);
                $(lblAmtNY).html(arr[13]);

                $(lblTotal_Variance).html(arr[14]);
                $(lblTotal_VariancePer).html(arr[15]);
                $(txtEstAmount).html(arr[16]);
            }
            );
        }

        function Update_CC(txt) {
                    
            var RFQId = $(txt).attr("RFQId");
            var DocketId = $(txt).attr("DocketId");
            var DocketJobId = $(txt).attr("DocketJobId");
            var DocketSubJobId = $(txt).attr("DocketSubJobId");
            var SubJobCode = $(txt).attr("SubJobCode");
            var CostCat = $(txt).attr("cc");

            if(window.confirm('[ Job code :' + SubJobCode + ' ] Are you sure to change cost category to ' + (CostCat == 'Y' ? 'Yard Cost' : 'Owner Cost' )))
            {

            var imgY = $("#imgY_" + SubJobCode);
            var imgN = $("#imgN_" + SubJobCode);

            var lblAmtYC = $("#lblTotalYardCost");
            var lblAmtNY = $("#lblTotalNonYardCost");

            $.post('UpdatePO.ashx', {
                Type: "UpdateCostCat",
                RFQId: RFQId,
                DocketId: DocketId,
                DocketJobId: DocketJobId,
                DocketSubJobId: DocketSubJobId,
                SubJobCode: SubJobCode,
                CostCat: CostCat
            },
            function (data, status) {
                var arr = data.split('|');
                
                $(lblAmtYC).html(arr[0]);
                $(lblAmtNY).html(arr[1]);

                if (CostCat == "Y") {
                    imgN.show();
                    imgY.hide();
                }
                else {
                    imgN.hide();
                    imgY.show();
                }


            }
            );
          }

        }            

    </script>

    <div id='debug1'></div>
    </form>
</body>
</html>
