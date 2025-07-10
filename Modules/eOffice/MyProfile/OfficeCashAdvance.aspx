<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeCashAdvance.aspx.cs" Inherits="emtm_MyProfile_Emtm_OfficeCashAdvance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript" src="../../JS/jquery.min.js"></script>
    <link href="../style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/Common.js"></script>
    <script type="text/javascript" language="javascript">
        function openwindow(id) {

            window.open("Emtm_PopupAttachment.aspx?expid=" + id, "att", "");
        }
        function openreport(id) {

            // window.open("../../Reporting/OfficeAbsence_Expense.aspx?id=" + id, "report", "");
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 

         <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Cash Advance Summary</div>
                <table cellpadding="5" cellspacing="0" width="100%">
            
            <tr>
            <td style="text-align :center; border:solid 1px #C2E0FF; vertical-align:top;">
                <table cellpadding="2" cellspacing="1" width="100%">
                            <tr>
                                <td>
                                    Currency
                                </td>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    Exch. Rate (SGD)
                                </td>
                                <td>
                                    Amount (SGD)
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlCurr" runat="server" Width="90px" required="yes"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAmount" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" MaxLength="8" Width="70px" runat="server" required="yes" ></asp:TextBox>
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
                        <col style="width: 100px;" />
                        <col style="width: 150px;" />
                        <col />
                        <col style="width: 150px;" />
                        <col style="width: 50px;" />
                        <tr align="left" class="blueheader">
                            <td></td>
                            <td></td>
                            <td>Currency</td>
                            <td>Amount</td>
                            <td>Exch.Rate(SGD)</td>
                            <td>Amount(SGD)</td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                    </colgroup>
                </table>
                </div>
                <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 250px; text-align: center;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 25px;" />
                            <col style="width: 100px;" />
                            <col style="width: 100px;" />
                            <col style="width: 150px;" />
                            <col />
                            <col style="width: 150px;" />
                            <col style="width: 50px;" />
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
                                <td align="center">
                                    <%#Eval("Currency")%>
                                </td>
                                <td align="right">
                                    <%# String.Format("{0:F}", Eval("Amount"))%>
                                </td>
                                
                                <td align="right">
                                    <%# Eval("ExcRate")%>
                                </td>
                                <td align="right">
                                    <%# String.Format("{0:F}", Eval("CashAdvance")) %>
                                </td>
                                <td>
                                    
                                </td>
                                <td>&nbsp;</td>
                                <%-- <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                                
                    </table>
                </div>
            </td>            
            </tr>
            </table>
            <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width: 100%; border-collapse: collapse;">
                        <tr>
                            <td style="text-align:left;"><asp:Label ID="lblMsg" ForeColor="Red" runat="server" Font-Size="13px"></asp:Label></td>
                            <td style="text-align:right; font-weight:bold;">
                                Total Cash Advance Taken (SGD) :&nbsp;
                            </td>
                            <td style="width:150px;text-align:right;font-weight:bold;"><asp:Label ID="lblTotalgivenSGD"  runat="server"></asp:Label></td>
                            <td style="width:200px;text-align:right;"><asp:Button ID="btnClose" Text="Close" CssClass="btn" style="background-color:Red" runat="server" OnClientClick="window.parent.HideFrame();"  Width="80px" /></td> 
                         </tr>
                </table>
        </ContentTemplate>
        </asp:UpdatePanel>
  
    </div>
 </form>   
</body>
</html>
