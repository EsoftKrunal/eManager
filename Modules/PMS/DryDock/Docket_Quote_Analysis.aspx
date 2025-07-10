<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Docket_Quote_Analysis.aspx.cs" Inherits="Docket_Quote_Analysis" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>eMANAGER</title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <style type="text/css">
    .hover_highlight_cat
     {
         vertical-align:middle;
         color:Red;
         height:17px;
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
         height:17px;
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
         height:17px;
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
    input:checked
    {
        background-color:green; 
        border:solid 1px red;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:#1947A3;color:White; text-align:center; padding:3px; font-size:15px; "><b>VENDOR QUOTE ANALYSIS</b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr><td>
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
           <%-- <div class="nav" >
            <ul style="padding:0px; margin:0px">
                <li class="active_tab"><a href="#">Docket Quote Analysis</a></li>
                <li class="inactive_tab"><a runat="server" id="a1" href="Docket_Sup_Analysis.aspx">Suptd Quote Analysis</a></li>
            </ul>
            </div>--%>
            <div style="border:solid 4px #4D70DB;vertical-align:top;">
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr>
         <td>
            <div style="float:left">
            <asp:Button runat="server" ID="btnLevel1" Text="Ist Level" OnClick="btnLevel_Click" CommandArgument="1" Visible="false"/>
            <asp:Button runat="server" ID="btnLevel2" Text="IInd Level" OnClick="btnLevel_Click" CommandArgument="2" Visible="false"/>
            <asp:Button runat="server" ID="btnLevel3" Text="IIIrd Level" OnClick="btnLevel_Click" CommandArgument="3"  Visible="false"/>
            </div>
         </td>
         </tr>
         </table>
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr style="font-size:15px;">
            <td colspan="2" >
                 <table style="width:100%;" cellpadding="0" cellspacing="0">
                    <tr>
                         <td style="padding:4px; text-align:right; font-weight:bold; font-size:12px; width:130px;">Job Category :</td>
                         <td style="padding:4px; text-align:left; width:250px;"><asp:DropDownList ID="ddlJobCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlJobCategory_Click" runat="server" Width="400px"></asp:DropDownList></td>
                         <td style="padding:4px; text-align:right; font-weight:bold; font-size:12px; width:70px;">Job :</td>
                         <td style="padding:4px; text-align:left;"><asp:DropDownList ID="ddlJob" AutoPostBack="true" OnSelectedIndexChanged="ddlJob_Click" runat="server" Width="400px"></asp:DropDownList></td>
                    </tr>
                </table>
            </td>
         </tr>
         <tr style=" background-color:#99CCFF; font-size:15px;">
            <td  style="padding:4px;">
            Select Job Level : <asp:DropDownList runat="server" ID="ddlLevel" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_OnSelectedIndexChanged">
                <asp:ListItem Text="Ist Level" Value="1"></asp:ListItem>
                <asp:ListItem Text="IInd Level" Value="2"></asp:ListItem>
                <asp:ListItem Text="IIIrd Level" Value="3"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td style="padding:4px;width:1000px"><b>Quotations :</b></td>
         </tr>
         <tr>
            <td>
                <div class='columnHead'>&nbsp;</div>
                <div style="overflow-x:scroll;overflow-y:hidden;height:360px;" id="d936" onscroll="ScrollTo1();">
                    <asp:Literal runat="server" ID="litLeftHead"></asp:Literal>
                </div>
                <div class='columnHead' style="height:25px; text-align:right; font-weight:bold; "><div style="padding-top:5px;">Total :&nbsp;</div></div>
            </td>
            <td>
                <div class='columnHead' style="overflow-x:hidden;overflow-y:scroll; width:1000px; position:relative;" id="dcolh">
                <div style="height:25px; width:1200px; padding-top:5px;">
                    <asp:Literal runat="server" ID="litColumnHeader1"></asp:Literal>
                </div>
                <div style="height:25px; width:1200px;">
                    <asp:Literal runat="server" ID="litColumnHeader"></asp:Literal>
                </div>
                </div>
                <div style="overflow-x:scroll;overflow-y:scroll; width:1000px; height:360px;" id="d569" onscroll="ScrollTo2();">
                <asp:Literal runat="server" ID="litData"></asp:Literal>
                </div>
                <div class='columnHead' style="overflow-x:hidden;overflow-y:scroll; width:1000px; position:relative; height:25px;" id="dcolf">
                <div style="height:25px; width:1200px;">
                    <asp:Literal runat="server" ID="litColumnFooter"></asp:Literal>
                </div>
                </div>
            </td>
         </tr>
         </table>
            </div>
        </div>
         <div style="text-align:right; padding:3px;">
            <div style="text-align:left;float:left; padding-top:5px;">
                 <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div style="text-align:left;float:right; padding:3px;">
                
            </div>
         </div>
        </td> 
    </tr>
    </table>
    </div>
   
    <div style="padding:3px; text-align:center">
        <asp:Button runat="server" ID="btnAskPrint" Text="Print Comparison" OnClick="btnAskPrint_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;"  />
        <%--<asp:Button runat="server" ID="btnCreatePO" OnClientClick="return confirm('Are you sure to confirm ?');" Text="Yard Confirmation" OnClick="btnCreatePO_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;"  />--%>
    </div>  
     <%-- Select  RFQ for print --%>
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
                                <tr style="font-size:14px; background-color:#e2e2e2">
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
                               <%-- <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radMode">
                                    <asp:ListItem Text="All Cost" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Owner Cost" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Yard Cost" Value="Y"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                                </div>
                                <br />
                                <div style="text-align:center;">
                                    <asp:Button runat="server" ID="btnPrint" Text="Print" OnClick="btnPrint_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
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
            
            $(".left").click(function () {
                $("#hfd1").val($(this).attr('arg'));
            });

            $(".right").click(function () {
                $("#hfd2").val($(this).attr('arg'));
            });

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


        function OpenRFqPrint(rfqid, mode, l, jcid, jid) {
            window.open('PrintRFQComparison.aspx?RFQId=' + rfqid + '&CostType=' + mode + "&ReportLevel=" + l + "&CATID=" + jcid + "&DOCKETJOBID=" + jid, '');
        }
    </script>
    </form>
</body>
</html>
