<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeExpense.aspx.cs" Inherits="emtm_MyProfile_Emtm_OfficeExpense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../JS/jquery.min.js"></script>
    <link href="../style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function openwindow(id) 
            {
                window.open("Emtm_PopupAttachment.aspx?expid=" + id, "att", "");
            }
        function openreport(id) 
            {
                  window.open('../../Reporting/OfficeAbsenceExpense.aspx?id=' + id, 'asdf', '');
            }
   </script>
    <style type="text/css">
    .btn11sel
    {
        font-size:14px;
        background-color:#99CCFF;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #99CCFF;
        padding:5px;
    }
    .btn11
    {
        font-size:14px;
        background-color:#e2e2e2;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #c2c2c2;
        padding:5px;
    }
    </style>
</head>
<body style="font-family:Verdana;font-size:11px;">
    <form id="form1" runat="server">
    <div>
    <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Cash Advance & Expense Summary</div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:250px;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <Triggers>
        <asp:PostBackTrigger ControlID="btnSave_Exp" />
        </Triggers>
        <ContentTemplate>
         <!-- DIV TO UPDATE AND ADD EXPENSES -->
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvAddExp" runat="server" visible="false">
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                <div style="position :relative; width:1000px; height:150px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                    <table cellpadding="2" cellspacing="3" width="100%" style=" position:relative">
                                <tr>
                                <td colspan="8" style="padding:5px; text-align:center; background-color:Orange; font-size:15px; font-weight:bold;">Add Expense
                                 ( <asp:Label runat="server" ID="lblheading" Font-Size="13px"></asp:Label> )
                                </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; ">Date :</td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtExpDt" runat="server" MaxLength="11" Width="90px" required='yes'></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtExpDt0_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="imgExpDt" PopupPosition="TopLeft" 
                                            TargetControlID="txtExpDt">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:ImageButton ID="imgExpDt" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" 
                                            OnClientClick="return false;" />
                                    </td>
                                   
                                    <td style="text-align: right;">
                                        &nbsp;Descr. :
                                    </td>
                                    <td style="text-align: left;" colspan="3">
                                        <asp:TextBox ID="txtExp_Details" runat="server" MaxLength="100" Width="95%" required='yes'></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        Receipt No. :</td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtReceiptNo" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                    </td>
                                </tr>
                                  
                                   <tr>
                                    <td style="text-align: right; ">
                                        Charge To :</td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlInchargeTo" runat="server" Width="150px" required='yes'>
                                        </asp:DropDownList>
                                    </td>
                                       <td style="text-align: right; ">
                                           Amount (Foreign CUR) :</td>
                                       <td style="text-align: left;">
                                           <asp:TextBox ID="txtExpAmt" runat="server" AutoPostBack="true" MaxLength="8" 
                                               ontextchanged="txtExpAmt_TextChanged" Width="70px" required='yes'></asp:TextBox>
                                           <asp:DropDownList ID="ddlCurr_Exp" runat="server" required='yes' 
                                               Width="90px">
                                           </asp:DropDownList>
                                       </td>
                                       <td style="text-align: right;">
                                           Exch. Rate (SGD) :
                                       </td>
                                       <td style="text-align: left;">
                                           <asp:TextBox ID="txtExchRate_Exp" runat="server" AutoPostBack="true" required='yes' 
                                               MaxLength="8" ontextchanged="txtExchRate_Exp_TextChanged" Width="70px"></asp:TextBox>
                                       </td>
                                       <td style="text-align: right;">
                                           Amount in (SGD) :
                                       </td>
                                       <td style="text-align: left;">
                                           <asp:Label ID="lblExpense" runat="server" Width="90px"></asp:Label>
                                       </td>
                                   </tr>
                                  
                                   <tr>
                                    <td style="text-align: right;">
                                        Account Code :</td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtAccCode" runat="server" MaxLength="4" Width="70px"></asp:TextBox>
                                    </td>
                                       <td style="text-align: right; ">
                                           Attachment :
                                       </td>
                                       <td style="text-align: left;">
                                           <asp:FileUpload ID="FileUpload1" runat="server" Width="119px" />
                                       </td>
                                       <td colspan="4" style="text-align: right; padding-right: 5px;">
                                           <asp:Label ID="lblMsg_Exp" runat="server" ForeColor="Red"></asp:Label>
                                           <asp:Button ID="btnSave_Exp" runat="server" CssClass="btn" OnClick="btnSave_Exp_Click" Text="Save" />
                                           <asp:Button ID="btnClear_Exp" runat="server" CssClass="btn" OnClick="btnClear_Exp_Click" Text="Close" />
                                       </td>
                                   </tr>
                            </table>
                </div>
                </center>
        </div>
         <table id="Table1" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation" Font-Size="Large" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose" Font-Size="Large" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay" Font-Size="Large" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                <tr>
                <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
                    <asp:Label runat="server" ID="Label1"></asp:Label>
                </td>
                </tr>
                </table>
    <table cellpadding="2" cellspacing="1" width="100%" border="1">
        <tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Travel Start Date :
            </td>
            <td>
                <asp:Label runat="server" ID="lblStartDate"></asp:Label>
          
            </td>
                <td style="text-align: right; ">
                Travel End Date : </td>
            <td style="text-align: left;">
                <asp:Label runat="server" ID="lblEndDate"></asp:Label>
            </td>
            <td style="width:150px">
                 <asp:Button ID="btnPrint" Text="Print" CssClass="btn" OnClick="btnPrint_Click" runat="server" />
                 <asp:Button ID="btnClose" Text="Close" CssClass="btn" style="background-color:Red" runat="server" OnClientClick="window.parent.HideFrame();"  Width="80px" />
                </td>
        </tr>
        </table>
        <asp:Label ID="lblUMsg" style="float:right;" ForeColor="Red" runat="server"></asp:Label>
        <div style="text-align:left;">
            <asp:Button runat="server" Text="Cash Advance" ID="btnCash" onclick="btnCash_Click"  CssClass="btn11sel" />
            <asp:Button runat="server" Text="Expense" ID="btnExp" onclick="btnExp_Click"  CssClass="btn11" />
        </div>
        <div style="text-align:left; background-color: #99CCFF; padding-top:10px">
            <div style="text-align:left; background-color: white; ">
            <asp:Panel runat="server" ID="pnlCash" Visible="true">
                <table cellpadding="5" cellspacing="0" width="100%">
                <tr>
                <td style="text-align :center; background-color:#C2E0FF; width:50%;">
                    <b><span style="font-size:14px;">Cash Advance Taken</span></b>
                </td>
                <td style="text-align :center; background-color:#FFF0F0; width:50%;">
                    <b><span style="font-size:14px; text-align:center">Cash Returned</span></b>
                </td>
                </tr>
            <tr>
            <td style="text-align :center; border:solid 1px #C2E0FF; vertical-align:top;">
                <table cellpadding="2" cellspacing="1" width="100%" border="0">
                            <tr>
                            <td>
                                    Advance Taken
                                </td>
                                <td>
                                    Currency
                                </td>
                                <td>
                                    Exch. Rate (SGD)
                                </td>
                                <td>
                                    Advance (SGD)
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAmount" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" MaxLength="8" Width="70px" runat="server" required="yes" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurr" runat="server" Width="90px" required="yes"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExchRate" MaxLength="8" runat="server" Width="70px" AutoPostBack="true" ontextchanged="txtExchRate_TextChanged" required="yes"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblCashAdv" Width="70px" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnSave1" Text="Save" CssClass="btn" runat="server" OnClick="btnSaveCV_Click" />
                                    <asp:Button ID="btnClear1" Text="Clear" CssClass="btn" runat="server" OnClick="ClearCV_Click" />
                                </td>
                            </tr>
                                   
                        </table>
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse; height:26px;">
                    <colgroup>
                        <col style="width: 25px;" />
                        <col style="width: 25px;" />
                        <col style="width: 100px;" />
                        <col style="width: 50px;" />
                        <col style="width: 150px;" />
                        <col />
                        <col style="width: 25px;" />
                        <tr align="left" class="blueheader">
                            <td></td>
                            <td></td>
                            <td>Advance</td>
                            <td>Curr</td>
                            <td>Exch.Rate(SGD)</td>
                            <td>Advance(SGD)</td>
                            <td>&nbsp;</td>
                        </tr>
                    </colgroup>
                </table>
                </div>
                <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 180px; text-align: center;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 25px;" />
                            <col style="width: 100px;" />
                            <col style="width: 50px;" />
                            <col style="width: 150px;" />
                            <col />
                            <col style="width: 25px;" />
                        </colgroup>                                
                    <asp:Repeater ID="rptCashAdv" runat="server">
                        <ItemTemplate>
                            <tr class='<%# (Common.CastAsInt32(Eval("CashId"))==CashId)?"selectedrow":"row"%>'>
                                <td align="center">
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("CashId") %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure to delete?');" ToolTip="Delete" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnEditCashDetails" runat="server" CommandArgument='<%# Eval("CashId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditCashDetails_Click" ToolTip="Edit Cash Advance Details" />
                                </td>
                                <td align="right">
                                    <%# String.Format("{0:F}", Eval("Amount"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Currency")%>
                                </td>
                                <td align="right">
                                    <%# Eval("ExcRate")%>
                                </td>
                                <td align="right">
                                    <%# String.Format("{0:F}", Eval("CashAdvance")) %>
                                </td>
                                <td>&nbsp;</td>
                                <%-- <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                                
                    </table>
                </div>
                
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Font-Size="13px"></asp:Label>
            </td>
            <td style="text-align :center; border:solid 1px #CC99FF; vertical-align:top;">
                <table cellpadding="2" cellspacing="1" width="100%">
                            <tr>
                                
                                <td>
                                    Amt Returned
                                </td>
                                <td>
                                    Curr
                                </td>
                                <td>
                                    Exch. Rate (SGD)
                                </td>
                                <td>
                                    Amt Returned (SGD)
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAmount_1" OnTextChanged="txtAmount_1_TextChanged" AutoPostBack="true" MaxLength="8" Width="70px" runat="server" required="yes" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurr_1" runat="server" Width="90px" required="yes"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExchRate_1" MaxLength="8" runat="server" Width="70px" AutoPostBack="true" ontextchanged="txtExchRate_1_TextChanged" required="yes"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblCashAdv_1" Width="70px" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnSave1_1" Text="Save" CssClass="btn" runat="server" OnClick="btnSaveCV_1_Click" />
                                    <asp:Button ID="btnClear_1" Text="Clear" CssClass="btn" runat="server" OnClick="ClearCV_1_Click" />
                                </td>
                            </tr>
                                   
                        </table>
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse; height:26px;">
                  <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 25px;" />
                            <col style="width: 100px;" />
                            <col style="width: 50px;" />
                            <col style="width: 150px;" />
                            <col />
                            <col style="width: 25px;" />
                        </colgroup>  
                        <tr align="left" class="blueheader">
                            <td></td>
                            <td></td>
                            <td>Advance</td>
                            <td>Currency</td>
                            <td>Exch.Rate(SGD)</td>
                            <td>Advance(SGD)</td>
                            <td>&nbsp;</td>
                        </tr>
                    </colgroup>
                </table>
                </div>
                <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 180px; text-align: center;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                       <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 25px;" />
                            <col style="width: 100px;" />
                            <col style="width: 50px;" />
                            <col style="width: 150px;" />
                            <col />
                            <col style="width: 25px;" />
                        </colgroup>                             
                    <asp:Repeater ID="rptCashAdv_1" runat="server">
                        <ItemTemplate>
                            <tr class='<%# (Common.CastAsInt32(Eval("CashId"))==CashId_1)?"selectedrow":"row"%>'>
                                <td align="center">
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("CashId") %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDelete_1_Click" OnClientClick="return confirm('Are you sure to delete?');" ToolTip="Delete" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnEditCashDetails" runat="server" CommandArgument='<%# Eval("CashId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditCashDetails_1_Click" ToolTip="Edit Return Details" />
                                </td>
                                <td align="right">
                                    <%# String.Format("{0:F}", Eval("Amount"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Currency")%>
                                </td>
                                <td align="right">
                                    <%# Eval("ExcRate")%>
                                </td>
                                <td align="right">
                                    <%# String.Format("{0:F}", Eval("CashAdvance")) %>
                                </td>
                                <td>&nbsp;</td>
                                <%-- <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                   
                </div>
                <asp:Label ID="lblMsg_1" ForeColor="Red" runat="server" Font-Size="13px"></asp:Label>
            </td>
            </tr>
            <tr style=" font-weight:bold">
            <td style="text-align :center; background-color:#C2E0FF; width:50%; vertical-align:top;">
                <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                        <tr>
                            <td style="text-align:right;">
                                Total Cash Advance Taken (SGD) :&nbsp;
                            </td>
                            <td style="width:150px;text-align:right;"><asp:Label ID="lblTotalgivenSGD"  runat="server"></asp:Label></td>
                            <td style="width:50px;">&nbsp;</td> 
                            <td style="width:17px;">&nbsp;</td>                  
                        </tr>
                       
                </table>
            </td>
            <td style="text-align :center; background-color:#FFF0F0; width:50%; vertical-align:top;">
                <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                     <tr>
                            <td style="text-align:right;">
                                Cash Returned (SGD) :&nbsp;
                            </td>
                            <td style="width:150px;text-align:right;"><asp:Label ID="lblTotalRetdSGD" runat="server"></asp:Label></td>
                            <td style="width:50px;">&nbsp;</td> 
                            <td style="width:17px;">&nbsp;</td>                  
                        </tr>               
                  </table>
            </td>
            </tr>
            <tr style="font-weight:bold; ">
                <td colspan="2" style="text-align:center">
                <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                 <tr>
                    <td style="text-align:center;font-size:17px;">Balance Cash Advance (SGD) :&nbsp; <asp:Label ID="lblActGivenSGD" runat="server" Font-Size="17px"></asp:Label>
                    </td>
                 </tr>
                </table>
                </td>
            </tr>
            </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlExp" Visible="false">
            <div style=" background-color:#C2E0FF; padding:5px; font-size:13px; font-weight:bold; text-align:center ">Cash Expense&nbsp;
                <asp:Button runat="server" ID="btnAdd" CommandArgument="C" Text="Add New" OnClick="ExpAdd_Click"/>
            </div>
            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse; height:26px;">
                                <colgroup>
                                    <col style="width: 25px;" />
                                    <col style="width: 25px;" />
                                    <col style="width: 25px;" />
                                    <col style="width: 25px;" />
                                    <col style="width: 80px;" />
                                    <col />
                                    <col style="width: 80px;" />
                                    <col style="width: 100px;" />
                                    <col style="width: 170px;" />
                                    <col style="width: 70px;" />
                                    <col style="width: 120px;" />
                                    <col style="width: 120px;" />
                                    <col style="width: 25px;" />
                                    <tr align="left" class="blueheader">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                           Sr#
                                        </td>
                                        <td>
                                            Date
                                        </td>
                                         <td>
                                            Details
                                        </td>
                                        <td>
                                            Receipt#
                                        </td>
                                        <td>
                                            Charge To
                                        </td>
                                        <td>
                                            Amount (Foreign CURR)
                                        </td>
                                        <td>
                                            Currency
                                        </td>
                                        <td>
                                            Exch. Rate(SGD)
                                        </td>
                                        <td>
                                            Amount in (SGD)
                                        </td>
                                       
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                        <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 230px; text-align: center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                                    <colgroup>
                                        <col style="width: 25px;" />
                                        <col style="width: 25px;" />
                                        <col style="width: 25px;" />
                                        <col style="width: 25px;" />
                                        <col style="width: 80px;" />
                                        <col />
                                        <col style="width: 80px;" />
                                        <col style="width: 100px;" />
                                        <col style="width: 170px;" />
                                        <col style="width: 70px;" />
                                        <col style="width: 120px;" />
                                        <col style="width: 120px;" />
                                        <col style="width: 25px;" />
                                    </colgroup>
                                <asp:Repeater ID="rptExpense" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("ExpenseId"))==ExpenseId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btnDelete_Exp" runat="server" 
                                                    CommandArgument='<%# Eval("ExpenseId") %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" 
                                                    OnClick="btnDelete_Exp_Click" 
                                                    OnClientClick="return confirm('Are you sure to delete?');" ToolTip="Delete" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnEditExpenseDetails" runat="server" CommandArgument='<%# Eval("ExpenseId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditExpenseDetails_Click" ToolTip="Edit Details" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgAttachment" runat="server" 
                                                    CommandArgument='<%# Eval("ExpenseId") %>' ImageUrl="~/Modules/HRD/Images/paperclip.gif" 
                                                    OnClick="imgAttachment_Click" ToolTip="Attachment" 
                                                    Visible='<%# Eval("FileName").ToString() != "" %>' />
                                            </td>
                                            <td align="center">
                                                <%#Eval("SrNo")%>
                                            </td>
                                            <td align="center">
                                                <%#Eval("ExpDt")%>
                                            </td>
                                             <td align="Left">
                                                <%#Eval("Details")%>
                                            </td>
                                            <td align="Left">
                                                <%#Eval("ReceiptNo")%>
                                            </td>
                                            <td align="Left">
                                                <%#Eval("chargeTo")%>
                                            </td>
                                            <td align="right">
                                                <%#String.Format("{0:F}",Eval("Amount"))%>
                                            </td>
                                            <td align="center">
                                                <%#Eval("Currency")%>
                                            </td>
                                            <td align="right">
                                                <%#String.Format("{0:F}",Eval("ExcRate"))%>
                                            </td>
                                            <td align="right">
                                                <%#String.Format("{0:F}", Eval("Expense"))%>
                                            </td>
                                           
                                             <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                </table>
                            </div>
            </asp:Panel>
            </div>
        </div>
        
        <asp:Panel runat="server" ID="pnlExp1" Visible="false">
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse; background-color:#e2e2e2; font-weight: bold; font-size:13px; text-align:right;">
        <tr>
            <td style=" min-width:600px">Total Cash Expenses (SGD) :</td>
            <td style=" width:120px; text-align:right;"><asp:Label ID="lblTotExp" runat="server"></asp:Label></td>
            <td style="width:17px">&nbsp;</td>
        </tr>
        <tr>
            <td>Total Cash Adv. (SGD) :</td>    
            <td style=" width:120px; text-align:right;"><asp:Label ID="lblTotalCashAdvance" runat="server"></asp:Label></td>
            <td >&nbsp;</td>
        </tr>
        <tr>
            <td>Balance Amount (SGD) :</td>
            <td style="width:120px; text-align:right;">
            <asp:Label ID="lblTotBal" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        </table>
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse; background-color:#e2e2e2; font-weight: bold; font-size:13px; text-align:right;">
        <tr>
            <td>
                <div style="float:left;font-size:15px">
                    Remark :&nbsp;<asp:Label ID="lblRemarks" runat="server" Font-Size="15px"></asp:Label>
                </div>
                
            </td>
        </tr>
        </table>
        <div style=" background-color:#99CCFF; padding:5px; font-size:13px; font-weight:bold; text-align:center ">Non-Cash Expense
            &nbsp;
                ( Paid by COY/COY Credit Card )
                <asp:Button runat="server" ID="Button1" CommandArgument="R" Text="Add New" OnClick="ExpAdd_Click"/> 
            </div>       
        <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 200px; text-align: center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                                    <colgroup>
                                        <col style="width: 25px;" />
                                        <col style="width: 25px;" />
                                        <col style="width: 25px;" />
                                        <col style="width: 25px;" />
                                        <col style="width: 80px;" />
                                        <col />
                                        <col style="width: 80px;" />
                                        <col style="width: 100px;" />
                                        <col style="width: 170px;" />
                                        <col style="width: 70px;" />
                                        <col style="width: 120px;" />
                                        <col style="width: 120px;" />
                                        <col style="width: 17px;" />
                                    </colgroup>
                                <asp:Repeater ID="rptExpense_1" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("ExpenseId"))==ExpenseId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btnDelete_Exp" runat="server" CommandArgument='<%# Eval("ExpenseId") %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDelete_Exp_Click" OnClientClick="return confirm('Are you sure to delete?');" ToolTip="Delete" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnEditExpenseDetails" runat="server" CommandArgument='<%# Eval("ExpenseId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditExpenseDetails_Click" ToolTip="Edit Details" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgAttachment" runat="server" CommandArgument='<%# Eval("ExpenseId") %>' ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="imgAttachment_Click" ToolTip="Attachment" Visible='<%# Eval("FileName").ToString() != "" %>' />
                                            </td>
                                            <td align="center">
                                                <%#Eval("SrNo")%>
                                            </td>
                                            <td align="center">
                                                <%#Eval("ExpDt")%>
                                            </td>
                                             <td align="Left">
                                                <%#Eval("Details")%>
                                            </td>
                                            <td align="Left">
                                                <%#Eval("ReceiptNo")%>
                                            </td>
                                            <td align="Left">
                                                <%#Eval("chargeTo")%>
                                            </td>
                                            <td align="right">
                                                <%#String.Format("{0:F}",Eval("Amount"))%>
                                            </td>
                                            <td align="center">
                                                <%#Eval("Currency")%>
                                            </td>
                                            <td align="right">
                                                <%#String.Format("{0:F}",Eval("ExcRate"))%>
                                            </td>
                                            <td align="right">
                                                <%#String.Format("{0:F}", Eval("Expense"))%>
                                            </td>
                                           
                                             <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                </table>
                            </div>
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse; background-color:#e2e2e2; font-weight: bold; font-size:13px; text-align:right;">
        <tr>
            <td style=" min-width:600px">Total NON-Cash Expenses (SGD) :</td>
            <td style=" width:120px; text-align:right;"><asp:Label ID="lblSumNonCash" runat="server"></asp:Label></td>
            <td style="width:17px">&nbsp;</td>
        </tr>
        <tr>
            <td style=" min-width:600px">Gross Expenses (SGD) :</td>
            <td style=" width:120px; text-align:right;"><asp:Label ID="lblGrossExp" runat="server"></asp:Label></td>
            <td style="width:17px">&nbsp;</td>
        </tr>                         
        </asp:Panel>
       
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
