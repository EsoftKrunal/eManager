<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetTrackingPOList.aspx.cs" Inherits="BudgetTrackingPOList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/jquery-1.4.2.min.js" type="text/javascript"></script>
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
    body
    {
        font-family:Arial;
        font-size:13px !important;
        margin:0px;
    }
    td,th
    {
        font-size:12px !important;
    }
    *
    {
        box-sizing:border-box;
    }
    .normalcell,.red,.green{
        cursor:pointer;
    }
    .normalcell:hover,.red:hover,.green:hover{
        background-color:yellow;
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
    .btn
    {
        background-color:#394b65;
        color:White;
        border:none;
        padding:5px 10px 5px 10px;        
    }    
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
.error_msg
{
background-color:#fbb3b3 !important
}

 
            .menu{
                margin:0px;
                padding:0px;
                display:block;
            }
            .menu li
            {
                display:inline-block;
                float:left;
                margin-left:5px;
            }
            .menu li a{
                display:block;
                width:150px;
                height:35px;
                line-height:35px;
                background-color:#c2c2c2;
                text-decoration:none;
                color:#333;
            }
                .menu li a:hover,.menu li a:active,.menu li a:focus,.menu li a.active{
                background-color:#f63f2d;
                color:white;
            }
                .clear{
                    clear:both;
                }
                .data-label
                {
                    background-color:#c2c2c2;
                    color:#333;
                }
                a img{
                    border:none;
                }
        </style>
</head>
<body >
    <form id="form1" runat="server">     
        <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>
    <div>
        <div style="position:fixed;width:100%;top:0px;left:0px;z-index:20;background-color:white">
        <div class="Text headerband"> Budget Tracking - ( PO Records )</div>        
        <table cellpadding="10" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; background-color:#8fafdb">
            <tr>
                <td style="font-size:16px;text-align:right;" ><b>Company :</b></td>
                <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblCompany" runat="server"></asp:Label></td>
                <td style="font-size:16px;text-align:right;" > <b>Vessel :</b></td>
                <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblVessel" runat="server"></asp:Label></td>
                <td style="font-size:16px;text-align:right;" ><b>Year :</b></td>
                <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblYear" runat="server"></asp:Label></td>
                <td style="font-size:16px;text-align:right;" ><b>Account :</b></td>
                <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblAccountNoNameTaskList" runat="server"></asp:Label></td>
            </tr>
        </table>
        <div style="padding:6px" ><b> Task Name : </b> <asp:Label ID="lblTaskName" runat="server"></asp:Label> </div>
        <table width="100%">
                    <tr>
                        <td>
                        <table width="100%" border="0" cellpadding="6" class="bordered" cellspacing="0" style="border-collapse:collapse;">
                             <tr style="background-color:#c2c2c2;font-weight:bold;">
                                <td>Year To Date</td>
                                <td style="width:10px">:</td>
                                <td style="text-align:right">Amount (US$)</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Actual Amount </td>
                                <td style="width:10px">:</td>
                                <td style="text-align:right"><asp:Label ID="lblYTDActule" runat="server"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Committed Amount </td>
                                <td style="width:10px">:</td>
                                <td style="text-align:right"><asp:Label ID="lblYTDCommitted" runat="server"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr>
                            <td>Consumed Amount </td>
                            <td style="width:10px">:</td>
                            <td style="text-align:right"><asp:Label ID="lblYTDConsumed" runat="server"></asp:Label> </td>
                            <td>
                                <div runat="server" id="dvbudcons" style="height:15px; background-color:#ea9226;left:0px;top:0px; display:inline-table;"></div>
                            </td>
                        </tr>
                             <tr>
                            <td >Budget Amount </td>
                            <td style="width:10px">:</td>
                            <td style="text-align:right"><asp:Label ID="lblYTDBudget" runat="server"></asp:Label></td>
                            <td><div runat="server" id="dvbud" style="height:15px; background-color:#4988cc; left:0px;top:0px;"></div></td>
                        </tr>
                        
                          <tr>
                            <td style="width:160px;">Variance Amount </td>
                            <td style="width:10px">:</td>
                            <td style="width:130px; text-align:right">
                                <asp:Label ID="lblYTDVariance" runat="server"></asp:Label>
                            </td>
                            <td>                                    
                            </td>
                        </tr>
                            <tr>
                            <td style="width:160px;">Variance (%) </td>
                            <td style="width:10px">:</td>
                            <td style="width:130px; text-align:right">
                                <asp:Label ID="lblYTDVariancePer" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>                
                    </table>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border:solid 0px green;float:left;">
                                    <col width="50%" />
                                    <col />
                                    <tr>
                                        <td style="padding-right:5px;padding-bottom:5px;">
                                            <div style="padding:6px; background-color:#ccc7c7">
                                                Annual Budget (US$)
                                                <br />
                                                <div style="padding:5px;">
                                                <asp:Label ID="lblYTDAnnualBudget" runat="server" Font-Size="16px" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="padding-right:5px;padding-bottom:5px;">
                                            <div style="padding:6px; background-color:#ccc7c7; margin-top:5px;">
                                                Annual Budget Utilization
                                                <br />
                                                <div style="padding:5px;">
                                                <asp:Label ID="lblYTDAnnualUtilization" runat="server" Font-Size="16px" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right:5px;padding-bottom:5px;">
                                            <div style="padding:6px; background-color:#ccc7c7; margin-top:5px;">
                                                No of Orders Not Linked With Any Task
                                                <br />
                                                <div style="padding:5px;">
                                                <asp:LinkButton ID="lblOrdersCount" runat="server" Font-Size="16px" Font-Bold="true" OnClick="lblOrdersCount_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="padding-right:5px;padding-bottom:5px;">
                                            <%--<div style="padding:6px; background-color:#ccc7c7; margin-top:5px;">
                                                No of NON PO Orders Not Linked With Any Task
                                                <br />
                                                <div style="padding:5px;">
                                                <asp:LinkButton ID="lblNONPoOrders" runat="server" Font-Size="16px" Font-Bold="true" OnClick="lblOrdersCount1_Click"></asp:LinkButton>
                                                </div>
                                            </div>--%>
                                        </td>
                                    </tr>
                                </table>
                                
                                
                                
                                
                                
                                l</td>
                        </tr>
                    </table>
             <div style="text-align:center;">
                <ul class="menu">
                    <li><a href="#" onclick="SetDiv('dv1',this);" class="active">PO </a></li>
                    <%--<li><a href="#" onclick="SetDiv('dv2',this);" >Non PO</a></li>--%>
                </ul>

                </div>
                <div class="clear"></div>
              <div style="height:6px; background-color:#f63f2d"></div>
            </div>
           <div id="dv1">
                <div style="top:335px;position:absolute; width:100%;left:0px;">          
                    <table cellpadding="4" cellspacing="0" border="0" width="100%" class="rpt-table">
                        <col width="100px" />
                        <col width="150px" />
                        <col />
                        <col width="100px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="60px" />
                        <col width="40px" />
                        
                        <tr style="height:30px;" class= "headerstylegrid">
                            <td style="text-align:left;color:white;"> Change Task</td>
                            <td style="text-align:left;color:white;"> PO#</td>
                            <td style="text-align:left;color:white;"> Supplier Name</td>
                            <td style="text-align:center;color:white;"> PO Date</td>
                            <td style="text-align:center;color:white;"> Amount (US$)</td>
                            <td style="text-align:center;color:white;"> PO Status</td>
                            <td style="text-align:center;color:white;"> Invoice Ref#</td>
                            <td style="text-align:center;color:white;"> Account </td>         
                            <td style="text-align:center;color:white;"> Status</td>                            
                           
                        </tr>
                    </table>
                    <div>
     
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="rpt-table">
                        <col width="100px" />
                        <col width="150px" />
                        <col />
                        <col width="100px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="60px" />
                        <col width="40px" />
                        
                        <asp:Repeater ID="rptTaskDetails" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:center"> 
                                        <asp:ImageButton ID="btnLinkPoToTaskPopup" runat="server" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnLinkPoToTaskPopup" CommandArgument='<%#Eval("BidID") %>' />
                                    </td>
                                    <td style="text-align:center"><a href='../Purchase/Requisition/VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BidID") %>'  target="_blank"><%#Eval("BidPoNum") %></a></td>
                                    <td style="text-align:left"><%#Eval("SupplierName") %>
                                        <div style="font-size:11px;font-style:italic;color:red;"><%#Eval("ApproveComments") %></div>
                                    </td>
                                    <td><%# Common.ToDateString(Eval("PoDate")) %></td>                                    
                                    <td style="text-align:right"><%#ProjectCommon.FormatCurrencyWithoutSign(Eval("PoAmount"))%></td>
                                    <td style="text-align:left"><%#Eval("bidstatusname") %></td>
                                    <td style="text-align:left"><a target="_blank" href="../Purchase/Invoice/ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>"><%#Eval("REfNo") %></a></td>
                                    <td style="text-align:center"><%#Eval("accountnumber") %></td>
                                    <td style="text-align:center;font-weight:bold">
                                        <%#(Eval("Status").ToString())%>
                                      <%--  <%#(Eval("CA").ToString()=="1")?"A":"C" %>--%>
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                </div>
           <div style="position:fixed;bottom:0px;left:0px;width:100%;">
                 <table cellpadding="4" cellspacing="0" border="0" width="100%" class="rpt-table">
                        <col width="100px" />
                        <col width="150px" />
                        <col />
                        <col width="100px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="120px" />
                        <col width="60px" />
                        <col width="40px" />
                        <tr style="background-color:#5f5f5f;color:white; height:30px;">
                            <td style="text-align:left;color:white;"> </td>
                            <td style="text-align:left;color:white;"> </td>
                            <td style="text-align:right;color:white;"> Total :</td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white; text-align:right;"> <asp:Label runat="server" ID="lblctot"></asp:Label> </td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white;"> </td>                            
                           
                        </tr>
                    </table>
            </div>
           </div>       
            <%--<div id="dv2">
                 <div style="top:335px;position:absolute; width:100%;left:0px;">   
                    <table cellpadding="4" cellspacing="0" border="0" width="100%" class="rpt-table">
                        <col width="100px" />
                        <col width="150px" />
                        <col />
                        <col width="120px" />
                        <col width="120px" />
                        <col/>
                        <col width="100px" />
                        <col width="40px" />                        
                        <tr style="background-color:#5f5f5f;color:white; height:30px;">
                            <td style="text-align:left;color:white;"> Change Task</td>
                            <td style="text-align:left;color:white;"> PO#</td>
                            <td style="text-align:left;color:white;"> Vendor/Supplier Name</td>
                            <td style="text-align:center;color:white;"> Transaction Date</td>
                            <td style="text-align:center;color:white;"> Amount (US$)</td>
                            <td style="text-align:center;color:white;"> Description</td>
                            <td style="text-align:center;color:white;"> Invoice #</td>
                            <td style="text-align:center;color:white;"> Account </td>         
                        </tr>
                    </table>
                    <div>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="rpt-table">
                         <col width="100px" />
                        <col width="150px" />
                        <col />
                        <col width="120px" />
                        <col width="120px" />
                        <col/>
                        <col width="100px" />
                        <col width="40px" /> 
                        <asp:Repeater ID="RPTnONpo" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:center"> 
                                        <asp:ImageButton ID="btnLinkNONPoToTaskPopup" runat="server" ImageUrl="~/Images/AddPencil.gif" OnClick="btnLinkNONPoToTaskPopup" CommandArgument='<%#Eval("ENTRYNUM") %>' />
                                    </td>
                                    <td style="text-align:center"><%#Eval("PoNumCALC") %></td>
                                    <td style="text-align:left"><%#Eval("VendorName") %></td>
                                    <td><%# Common.ToDateString(Eval("TransDate")) %></td>                                    
                                    <td style="text-align:right"><%#ProjectCommon.FormatCurrencyWithoutSign(Eval("Amount"))%></td>
                                    <td style="text-align:left"><%#Eval("DESC") %></td>
                                    <td style="text-align:left"><%#Eval("InvoiceNum") %></td>
                                    <td style="text-align:center"><%#Eval("accountnumber") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
             </div>
                </div>
                <div style="position:fixed;bottom:0px;left:0px;width:100%;">
                 <table cellpadding="4" cellspacing="0" border="0" width="100%" class="rpt-table">
                         <col width="100px" />
                        <col width="150px" />
                        <col />
                        <col width="120px" />
                        <col width="120px" />
                        <col/>
                        <col width="100px" />
                        <col width="40px" /> 
                        <tr style="background-color:#5f5f5f;color:white; height:30px;">
                            <td style="text-align:left;color:white;"> </td>
                            <td style="text-align:left;color:white;"> </td>
                            <td style="text-align:right;color:white;"> Total :</td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white; text-align:right;"> <asp:Label runat="server" ID="lblNonPoSum"></asp:Label> </td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white;"> </td>
                            <td style="text-align:center;color:white;"> </td>
                        </tr>
                    </table>
            </div>
        </div>--%>

      <asp:HiddenField runat="server" ID="hfdSelTaskID_Linkpo" Value="0" />
           <%-- Link PO To Task-----------------------------------------%>

        <asp:HiddenField runat="server" ID="HiddenField1" Value="0" />
        <asp:Button ID="btnTempUpdateBIdID" runat="server" OnClick="btnTempUpdateBIdID_OnClick" style="display:none;" />

                <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="divLinkPoToTask" visible="false" >
                <center>
                <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
                <div style="position :relative; width:90%; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:110px;opacity:1;filter:alpha(opacity=100)">
                <center>
                    <div style="font-size:16px;font-weight:bold;padding:8px; text-align:left;line-height:30px;" class="Text headerband">
                        <asp:ImageButton runat="server" ID="btnCloseLinkPoPopup" OnClick="btnCloseLinkPoPopup_OnClick" ImageUrl="~/Modules/HRD/Images/closewindow.png" style="float:right;width:24px;" />
                    <span style="font-size:18px;">Link PO to task</span>
                    </div>                                  
                    <div >
                        <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;">
                            <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                                <col width="40px" />
                                <col width="35px" />
                                <col />
                                <col width="80px" />
                                <col width="80px" />
                                <tr class= "headerstylegrid">
                                    <td></td>
                                    <td></td>
                                    <td style="text-align:left;color:white;vertical-align:central;">Task Description</td>
                                    <td style="text-align:right;color:white;vertical-align:central;">Budget</td>
                                    <td style="text-align:right;color:white;vertical-align:central;">Consumed</td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x:hidden;overflow-y:scroll;height:200px;border:solid 1px #d5d2d2;">
                        <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" class="rpt-table">
                                       <col width="40px" />
                                       <col width="35px" />
                                       <col />
                                       <col width="80px" />
                                        <col width="80px" />
                                     
                                       <asp:Repeater ID="rptTaskListLink" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                   <td style="text-align:center">
                                                       <%--hfdtaskid--%>
                                                       <input  type="radio" name="link" taskid='<%#Eval("TaskID") %>'  <%#((Eval("TaskID").ToString()==hfdSelTaskID_Linkpo.Value)?"checked":"") %> onclick=<%# "LinkPoToTask("+Eval("TaskID")+");" %> />
                                                   </td>
                                                   <td>
                                                       <%--<input type="radio" name="rdo" class="selRadio" TaskID='<%#Eval("TaskID") %>' onclick="click_btn(this);" />--%>
                                                       <span class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'>
                                                            <%# ((Eval("budgeted").ToString()=="True")?"B":"U") %>
                                                        </span>
                                                   </td>
                                                   <td style="text-align:left;"> 
                                                       <%#Eval("TaskDescription") %> 
                                                   </td>
                                                   <td style="text-align:right;">
                                                        <%# Common.CastAsInt32(Eval("Amount")) %> 
                                                       <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID") %>'  />
                                                   </td>
                                                        
                                               </tr>                                     
                                           </ItemTemplate>
                                       </asp:Repeater>
                           
                                    </table>
                        </div>

                      <div style="text-align:center;padding:5px;">
                            <%--<asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveTrackingTask_OnClick" />--%>
                           
                        </div>
                        <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                            &nbsp; <asp:Label ID="Label1" runat="server" CssClass="error"></asp:Label>
                        </div>
                        
                    </div>
                </center>
                </div>
                </center>
            </div>
        <script type="text/javascript">
            function SetDiv(id, self) {
                $(".menu li a").removeClass('active');
                $(self).addClass('active');

                $("#dv1").css('display', 'none');
                $("#dv2").css('display', 'none');

                $("#" + id).css('display', '');
            }

            function LinkPoToTask(TaskID) {
                $("#hfdSelTaskID_Linkpo").val(TaskID);
                $("#btnTempUpdateBIdID").click();
            }

        </script>
    </form>
    <div style="height:50px;">
        &nbsp;
    </div>
</body>
</html>

