<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewEditPaySlip.aspx.cs" Inherits="CrewAccounting_ViewEditPaySlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
         input[type=text]
        {
             font-family: Arial;             
             color: #003399;
             text-align:right;
             width:150px;
        } 
        .label
        {
             font-family: Arial;
             font-size: 12px;
             color: #003399;
             text-align :right;
             padding-right:10px;
        }
        .right_align
        {
        	text-align :right;
        }
    </style>
    <script type="text/javascript">
        function RefereshParentPage()
        {
            window.opener.RefereshPage();
        }
        function OpenPaySlip()
        {
            var PayRollID=<%=PayRollID.ToString()%>;
            window.open('../Reporting/PaySlip.aspx?PayrollId=' + PayRollID);
        }
        function OpenFinalWagesReport()
        {
            var PayrollId=<%=PayRollID.ToString()%>;
            window.open('../Reporting/FinalWagesAccount.aspx?PayrollId='+PayrollId+'');
//            window.open('../Reporting/FinalWagesAccount.aspx?Vess='+Vess+'&Month='+Month+'&Year='+Year+'&CrewNo='+CrewNo+'&VesselName='+VesselName+'&Rank='+Rank+'&Name='+Name+'');
        }
        function Check()
        {
            var FD=parseFloat(document.getElementById("txtFD").value);
            var TD=parseFloat(document.getElementById("txtTD").value);  
            
            if(isNaN(FD))
            {
                alert("Please enter valid from day(1-31).") 
                document.getElementById("txtFD").focus();
                return false; 
            }
            if( FD < 0 || FD > 31 )
            {
                alert("Please enter valid from day(1-31).") 
                document.getElementById("txtFD").focus();
                return false; 
            }
            
            if(isNaN(TD))
            {
                alert("Please enter valid to day(1-31).") 
                document.getElementById("txtTD").focus();
                return false;
            }
            if( TD < 0 || TD > 31 )
            {
                alert("Please enter valid to day(1-31).") 
                document.getElementById("txtTD").focus();
                return false; 
            }
        }
    </script>
    <title>View/Edit Pay Slip</title>
</head>
<body style="margin :10px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" ></asp:ScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>
        
    <div>
        <table cellpadding="2" cellspacing="2" border="0" width="800px" bordercolor="Gray" style="border-collapse:collapse; margin:auto;">
            <tr>
                <td colspan="2" align="center" style=" width: 100%;" class="text headerband" >View / Edit Portage Bill</td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                        <colgroup>
                            <col width="120px" />
                            <col />
                            <col width="70px" />
                            <col width="100px" />
                            
                            <tr>
                                <td class="label" style="text-align:right">
                                    Vessel Name :</td>
                                <td colspan="3" style="text-align :left; padding-left:10px"><asp:Label ID="lblVesselName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="text-align:right">
                                    Crew Name :</td>
                                <td colspan="3">
                                    <asp:Label ID="lblName" runat="server" style="float:left; padding-left:10px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="text-align:right">
                                    Crew# :</td>
                                <td>
                                    <asp:Label ID="lblCrewNo" runat="server" style="float:left; padding-left:10px"></asp:Label>
                                </td>
                                <td class="label" style="text-align:right">
                                    Rank :</td>
                                <td>
                                    <asp:Label ID="lblrank" runat="server" style="float:left; padding-left:10px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="text-align:right">
                                    FD :</td>
                                <td style="text-align:left">
                                    <asp:TextBox ID="txtFD" runat="server" CssClass="required_box" Width="20px" AutoPostBack="true" OnTextChanged="ReCalCulateWages"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat ="server" ErrorMessage="*" ControlToValidate="txtFD"></asp:RequiredFieldValidator>
                                </td>
                                <td class="label" style="text-align:right">TD :</td>
                                <td style="text-align:left">
                                    <asp:TextBox ID="txtTD" runat="server" CssClass="required_box" Width="20px" AutoPostBack="true" OnTextChanged="ReCalCulateWages"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ErrorMessage="*" ControlToValidate="txtTD"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    Month & Year :
                                    </td>
                                <td style="text-align :left">
                                    <asp:Label ID="lblWagesForDuration" runat="server" style="float:left; padding-left:10px"></asp:Label>
                                </td>
                                <td class="label">OT Hrs :</td>
                                <td style="text-align:left">
                                    <asp:TextBox ID="txtOT" runat="server" CssClass="required_box" style="float:left;" Width="20px" AutoPostBack="true" OnTextChanged="ReCalCulateWages"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator runat ="server" ErrorMessage="*" ControlToValidate="txtOT"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </colgroup>
                    </table>
                </td>                
            </tr>
            <tr>
            <td> 
            <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                <colgroup>
                    <col />
                    <col width="120px" />
                    <col width="120px" />
                    <tr>
                        <td colspan="3" style="text-align:center; font-size:13px;font-weight:bold;">
                            EARNED WAGES
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblLastMonthWages" runat="server" style="font-weight:bold;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblThisMonthWages" runat="server" style="font-weight:bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Basic Pay :</td>
                        <td class="label">
                            <asp:Label ID="lblBasicPay" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtBasicPay" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Fixed OT :</td>
                        <td class="label">
                            <asp:Label ID="lblFixedOT" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtFixedOT" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Bonus :</td>
                        <td class="label">
                            <asp:Label ID="lblBonus" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtBonus" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Leave Pay :</td>
                        <td class="label">
                            <asp:Label ID="lblLeavePay" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtLeavePay" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Seniority Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblSeniorityAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtSeniorityAllowance" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Trade Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblTradeAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtTradeAllowance" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Superior Certificate Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblSuperiorCertificateAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtSuperiorCertificateAllowance" runat="server" 
                                AutoPostBack="true" CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Comm. Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblCommAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtCommAllowance" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Sub Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblSubAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtSubAllowance" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            U. Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblUAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtUAllowance" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            GMDSS :</td>
                        <td class="label">
                            <asp:Label ID="lblGmdss" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtGmdss" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            AdditionalOT :</td>
                        <td class="label">
                            <asp:Label ID="lblAdditioinalOT" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtAdditioinalOT" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Other Allowance :</td>
                        <td class="label">
                            <asp:Label ID="lblMTMAllowance" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtMTMAllowance" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Other Payments :</td>
                        <td class="label">
                            <asp:Label ID="lblOtherPayments" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtOtherPayments" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Additional Payment :</td>
                        <td class="label">
                            <asp:Label ID="lblAdditionalPayment" runat="server"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:TextBox ID="txtAdditionalPayment" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnTextChanged="CalCulateWages"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Total Earnings :</td>
                        <td class="label">
                            <asp:Label ID="lblTotalEarningsLastMonth" runat="server" 
                                style="text-align:right; float:right; font-weight:bold;"></asp:Label>
                        </td>
                        <td class="right_align">
                            <asp:Label ID="lblTotalEarnings" runat="server" 
                                style="text-align:right; float:right; font-weight:bold;"></asp:Label>
                        </td>
                    </tr>
                </colgroup>
            
            </table>
            </td>
            <td style="vertical-align :top;"> 
                    <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center; font-size:13px;font-weight:bold;">
                            DEDUCTIONS
                        </td>                
                        
                    </tr>
                    <tr>
                        <td class="label">Allotment :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtAllotment" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Cash Advance :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtCashAdvance" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages"  ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Bond Store :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtBondStore" runat="server" CssClass="input_box"  AutoPostBack="true"  OnTextChanged="CalCulateWages" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Radio Account :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtRadioAccount" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Other Deductions :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtOtherDeductions" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Union Fee :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtUnionFee" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages" ></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="label">FBOW Paid On Board :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtFBOWPaidOnBoard" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages"  ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Paid In Office :</td>
                        <td class="right_align">
                            <asp:TextBox ID="txtPaidOffWages" runat="server" CssClass="input_box" AutoPostBack="true"  OnTextChanged="CalCulateWages"  ></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="label">Total Deductions :</td>
                        <td class="right_align">
                            <asp:Label ID="lblTotalDeductions" runat="server" style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" >
                             <asp:Button ID="btnReImport" runat="server" Text="Re-Import Deduction" 
                                 CssClass="btn" OnClick="btnReImport_OnClick" Width ="156px"/>   
                        </td>
                    </tr>
                     </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" >
                <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                     <tr>
                <td colspan="3" style="text-align:center; font-size:13px;font-weight:bold;">
                    WAGES SUMMARY 
                </td>                
                
            </tr>
            <tr>
                <td class="label" style="width :50%">
                    Current Month Wages :
                </td>
                <td>
                    <asp:Label ID="lblWagesForValue" runat="server" style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="label">
                    Bow From <asp:Label ID="lblBowFromDuration" runat="server" ></asp:Label> :
                 </td>
                <td>
                    <asp:TextBox ID="txtBowFromValue" runat="server" CssClass="input_box" Visible="false" style="text-align:right; float:right;"></asp:TextBox>
                    <asp:Label ID="lblBowFromValue" runat="server" style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="imgUpdateBowFrom" runat="server" OnClick="imgUpdateBowFrom_OnClick"  ImageUrl="~/Modules/HRD/Images/icon_pencil.gif" />
                </td>
            </tr>
            <tr>
                <td class="label">Deductions this Month :</td>
                <td>
                    <asp:Label ID="TotDedThisMonth" runat="server" 
                        style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
                     <tr>
                         <td class="label">
                             Total Wages Payable On-board :</td>
                         <td>
                             <asp:Label ID="TotWagesPayAbleOnBoard" runat="server" 
                                 style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
            <tr>
                <td class="label">Balance of Wages :</td>
                 <td style="text-align :right">
                    <asp:Label ID="lblBalanceOfWages" runat="server" style="text-align:right; float:right;font-weight:bold;" ></asp:Label>
                </td>
                <td></td>
                
            </tr>
            <tr>    
                <td colspan="3" style="padding :5px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_OnClick" Width ="100px" OnClientClick="return Check();"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn" OnClientClick="window.close();"  Width ="100px"/>
                    <asp:Button ID="btnPrint" runat="server" Text="Print Payslip" CssClass="btn" OnClientClick="javascript:OpenPaySlip();" Width ="100px"/>
                    
                    <%--<asp:Button ID="btnFinalWages" runat="server" Text="Final Wages" CssClass="btn" OnClientClick='OpenFinalWagesReport()' Width ="100px"/>--%>
                    <input type="button" value="Final Wages" class="btn" onclick='OpenFinalWagesReport()' Width ="100px"/>
                    
                    <%--javascript:OpenPaySlip();--%>
                    <%--<a onclick='OpenFinalWagesReport("<%#Eval("CrewNumber")%>","<%#Eval("RankCode")%>","<%#Eval("CrewName")%>")' style="cursor:pointer;" >--%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsg" runat="server" style="color:red;" ></asp:Label>
                </td>
            </tr>
                </table>
                </td>
            </tr>
                <tr>
                <td colspan="2" >
                 <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                     <tr>
                <td colspan="3" style="text-align:center; font-size:13px;font-weight:bold;">
                    VERIFICATION
                </td>                
                
                </tr>
            
            <tr>
                <td colspan="3" class="label" style="text-align:left">
                    <b>Remarks :</b> 
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="input_box" TextMode="MultiLine" Height="70px" Width="99%" style="text-align :left" ></asp:TextBox>
                </td>
            </tr>
            <tr id="trVerified" runat="server" visible="false">
                <td class="label">Verified By/On :</td>
                 <td style="text-align :left; padding-left:10px;" colspan="2">
                    <%--<asp:TextBox ID="txtVerifedBy" runat="server" CssClass="input_box" Width="97%" style="text-align :left" ></asp:TextBox>--%>
                    <asp:Label ID="lblVerifiedByOn" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>    
                <td colspan="3" style="padding :5px;">
                        <asp:Button ID="btnVerify" runat="server" Text="Verify" CssClass="btn" OnClick="btnVerify_OnClick" Width ="100px"/>
                </td>
            </tr>
                </table>
                </td>
            </tr>
            
        </table>
    </div>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
