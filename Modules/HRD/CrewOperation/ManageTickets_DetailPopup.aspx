<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ManageTickets_DetailPopup.aspx.cs" Inherits="CrewOperation_ManageTickets_DetailPopup" Title="CMS : Crew Operation > Crew Travel ( Ticket Mangement ) " %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>CMS : Crew Operation > Crew Travel ( Ticket Mangement ) </title>
<link href="../../../css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link type="text/css" rel="Stylesheet" href="../Styles/CalenderStyle.css" />
<script type="text/javascript" language="javascript" src="../Scripts/Calender.js"></script>
<script type="text/javascript" language="javascript">
    function getRefundAnount() {
        var Amount = parseFloat(document.getElementById("txt_C_OldAmt").value);
        var CancellationCharges = parseFloat(document.getElementById("txt_C_Cancel_LC").value);

        var Refund = Amount - CancellationCharges;
        document.getElementById("txt_C_Refund_LC").value = Refund.toFixed(2);
    }
</script>
</head>
<body style=" background-color:White">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <form id="form1" runat="server">
    <center>
        <div style="font-family:Arial;">
            <div style="padding:5px; text-align:center;">
            <asp:Label runat="server" ID="lblPortCallNo" Font-Size="Large"></asp:Label>
            </div>
            <div class="text headerband">
                <span style=" font-size:14px; color:Blue;">Crew Details</span>
            </div>
            <table cellpadding="5" cellspacing="0" width="100%" style="text-align:left ; font-size:12px; border-collapse:collapse" border="1" bordercolor="wheat">
            <tr>
            <td style='width:150px'>Crew Name :</td>
            <td style='text-align:left; width:400px'><asp:Label runat="server" ID="lblCrewName"></asp:Label></td>
            <td style='width:100px'>Rank :</td>
            <td style="text-align:left;"><asp:Label runat="server" ID="lblRankName"></asp:Label></td>
            </tr>
            </table>
            <div style="text-align:left; background-color:#E0F0FF; padding:4px;">
                <span style=" font-size:14px; color:Blue;">Travel Details</span>
            </div>
            <table cellpadding="5" cellspacing="0" width="100%" style="text-align:left ; font-size:12px; border-collapse:collapse" border="1" bordercolor="wheat">
            <tr>
            <td style='width:150px'>Travel Agent Name :</td>
            <td style='text-align:left; width:400px'><asp:Label runat="server" ID="lblTAName"></asp:Label></td>
            <td style='width:100px'>Vessel :</td>
            <td ><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            </tr>
            <tr>
            <td>Source :</td>
            <td><asp:Label runat="server" ID="lblSource"></asp:Label></td>
            <td>Destination :</td>
            <td><asp:Label runat="server" ID="lblDest"></asp:Label></td>
            </tr>
            <tr>
            <td>Dept. Date :</td>
            <td>
                <asp:Label runat="server" ID="lblDeptDate"></asp:Label>
                </td>
            <td>PNR # :</td>
            <td><asp:Label runat="server" ID="lblPNR"></asp:Label></td>
            </tr>
            <tr>
            <td>Class :</td>
            <td><asp:Label runat="server" ID="lblClass"></asp:Label></td>
            <td>Currency :</td>
            <td><asp:Label runat="server" ID="lblCurrency"></asp:Label></td>
            </tr>
            </table>
            <div style="text-align:left; background-color:#E0F0FF; padding:4px;">
                <span style=" font-size:14px; color:Blue;">Amount Details</span>
            </div>
            <table cellpadding="5" cellspacing="0" width="100%" style="font-size:12px; border-collapse:collapse" border="1" bordercolor="wheat">
            <tr style=' background-color:wheat'>
            <td style='width:20%'>&nbsp;</td>
            <td style='width:16%'>Booking</td>
            <td style='width:16%'>Change</td>
            <td style='width:16%'>Cancellation</td>
            <td style='width:16%'>Refund</td>
            <td style='width:16%'>Total</td>
            </tr>
            <tr>
            <td style="text-align:left">Date</td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_B_Date"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_H_Date"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_C_Date"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_R_Date"></asp:Label></td>
            <td style="text-align:right"></td>
            </tr>
           
            <tr runat="server" visible="false" >
            <td style="text-align:left">Ex. Rate(%)</td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_B_Rate"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_H_Rate"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_C_Rate"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_R_Rate"></asp:Label></td>
            <td style="text-align:right"></td>
            </tr>
             <tr>
            <td style="text-align:left">Amount (LC)</td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_B_LC"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_H_LC"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_C_LC"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_R_LC"></asp:Label></td>
            <td style="text-align:right"><asp:Label runat="server" ID="lbl_F_LC"></asp:Label></td>
            </tr>
            <tr>
            <td style="text-align:left">Amount (USD)</td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_B_Amount"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_H_Amount"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_C_Amount"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_R_Amount"></asp:Label></td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lbl_F_Amount"></asp:Label></td>
            </tr>
            </table>
            <div style="position:absolute;top:0px;left:0px; height :500px; width:100%;" runat="server" id="dv_ChangeCancel" visible="false">
            <center>
                <div style="position:absolute;top:0px;left:0px; height :520px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                 <div style="position :relative;width:600px; height:250px;padding :3px; text-align :center;background : white; z-index:150;top:150px; border:solid 10px #E0F0FF;">
                 <center >
                    <div>
                    <div style=" padding:3px; background-color:wheat;">
                    <asp:Label Text="" id="lblBoxName" runat="server" Font-Size="15px"></asp:Label>
                    </div>
                    <table cellpadding="5" cellspacing="0" width="100%" style="font-size:12px; border-collapse:collapse" border="1" bordercolor="#E0F0FF" runat="server" id="tbl_Change" visible="false">
                    <tr>
                        <td style="width:200px ; text-align:left">Current Dep. Date : </td>
                        <td style='text-align:left'>
                            <asp:TextBox runat="server" ID="txt_H_OldDate" Width='80px' ReadOnly="true" style='background-color:#e2e2e2' ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px; text-align:left" >Total Amount (<asp:Label runat="server" ID="lblLC1"></asp:Label>) :</td>
                        <td style='text-align:left'><asp:TextBox runat="server" ID="txt_H_OldAmt" ReadOnly="true" Width='80px' style='background-color:#d2d2d2; text-align:right;'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:200px ; text-align:left">New Dep. Date : </td>
                        <td style='text-align:left'>
                            <asp:TextBox runat="server" ID="txt_H_NewDate" Width='80px' onfocus="showCalendar('',this,this,'','holder1',5,22,1)" ValidationGroup="v1" style=" background-color:yellow" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txt_H_NewDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="v1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px; text-align:left" >Charges for Change (<asp:Label runat="server" ID="lblLC2"></asp:Label>) :</td>
                        <td style='text-align:left'>
                            <asp:TextBox runat="server" ID="txt_H_ChangeAmt" Width='80px' ValidationGroup="v1" style=" background-color:yellow;text-align:right;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_H_ChangeAmt" ErrorMessage="*" ForeColor="Red" ValidationGroup="v1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>

                    <table cellpadding="5" cellspacing="0" width="100%" style="font-size:12px; border-collapse:collapse" border="1" bordercolor="#E0F0FF" runat="server" id="tblCancel" visible="false">
                    <tr>
                        <td style="width:200px; text-align:left" >Ticket Amount (<asp:Label runat="server" ID="lblLC3"></asp:Label>) :</td>
                        <td style='text-align:left'><asp:TextBox runat="server" ID="txt_C_OldAmt" ReadOnly="true" Width='80px' style='background-color:#d2d2d2; text-align:right;'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:200px; text-align:left" >Charges for Cancellation (<asp:Label runat="server" ID="lblLC4"></asp:Label>) :</td>
                        <td style='text-align:left'>
                            <asp:TextBox runat="server" ID="txt_C_Cancel_LC" Width='80px' onchange="getRefundAnount();" ValidationGroup="v1" style=" background-color:yellow;text-align:right;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_H_ChangeAmt" ErrorMessage="*" ForeColor="Red" ValidationGroup="v1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td style="width:200px; text-align:left" >Refund Amount (<asp:Label runat="server" ID="lblLC5"></asp:Label>) :</td>
                        <td style='text-align:left'>
                            <asp:TextBox runat="server" ID="txt_C_Refund_LC" Width='80px' ValidationGroup="v1" style=" background-color:yellow;text-align:right;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txt_H_ChangeAmt" ErrorMessage="*" ForeColor="Red" ValidationGroup="v1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblM" Text="" ForeColor="Red"></asp:Label><br />
                        <asp:Button runat="server" id="btn_Change_Submit" CssClass="btn"  Text="Submit" Visible="false" OnClick="btn_Change_Submit_Click" CausesValidation="true"  ValidationGroup="v1" OnClientClick="return window.confirm('Are you sure to change this ticket?');" />
                        <asp:Button runat="server" id="btn_Cancel_Submit" CssClass="btn"  Text="Submit" Visible="false" OnClick="btn_Cancel_Submit_Click" CausesValidation="true"  ValidationGroup="v1" OnClientClick="return window.confirm('Are you sure to cancel this ticket?');" />
                    </div>
                    <div style="float:right; margin-top:5px;">
                    <br />
                    <asp:Button runat="server" id="btnClose" CssClass="btn"  Text="Close" OnClick="btn_Close_Click" />    
                    </div>
                    </div>
                 </center>
                 </div>
            </center>
            </div> 
            <div>
            <div style="float:center; margin-top:5px;">
            <asp:Button runat="server" id="btnChangeTic" CssClass="btn" style="padding:5px; background-color:Purple; color:White;" Text="Change Ticket" OnClick="btn_ChangeTic_Click" />
            <asp:Button runat="server" id="btnCancelTic" CssClass="btn" style="padding:5px; background-color:red; color:White;" Text="Cancel Ticket" OnClick="btn_CancelTic_Click" />
            </div>
            </div>
                    </div>
    </center>
    </form>
</body>
</html>


