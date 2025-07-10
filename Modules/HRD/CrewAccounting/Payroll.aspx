<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payroll.aspx.cs"  Inherits="CrewAccounting_Payroll"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title></title>
 <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script type ="text/javascript" language="javascript" >
    function SetScroll() {
        dv_1.scrollTop = dv_2.scrollTop;
    }
    function SetDv(ctl, cnt) {
        document.getElementById("dvLine").style.backgroundColor = ctl.style.backgroundColor;
        for (i = 1; i <= 5; i++) {
            if (i == parseInt(cnt))
                document.getElementById("dv_" + i).style.display = "";
            else
                document.getElementById("dv_" + i).style.display = "none";
        }
    }
    function RefereshPage() {
        document.getElementById('btn_search').click();
    }
    function OpenModifyContract(CID) {
        window.open("ModifyContract.aspx?Cid=" + CID + "");
    }

    function OpenDocument(TableID) {
        window.open("ShowDocuments.aspx?TableID=" + TableID + "");
    }
    function AddDocuments(Mode, PayrollID) {
        window.open("AddDocuments.aspx?Mode=" + Mode + "&PayrollID=" + PayrollID, '', 'width=800,height=600');
    }
</script>
<style type ="text/css">
    td
    {
    	word-wrap: break-word;
    }
    .opq
    {
    	z-index:1;
    	background-color :Gray; 
    	opacity:0.4;
    	filter:alpha(opacity=4);
    }
</style>
<style type="text/css">
        .input_box {}
        .btn
{
	/*color:White ;
	background-image:url(images/bar_bg.png);
	height :25px;*/
	border:1px solid #fe0034; font-family:arial; font-size:12px; color:#fff; border-radius:3px; -webkit-border-radius:3px; -ms-border-radius:3px; background:#fe0030; background:linear-gradient(#ff7c96, #fe0030); background:-webkit-linear-gradient(#ff7c96, #fe0030); background:-ms-linear-gradient(#ff7c96, #fe0030); padding:4px 6px; cursor:pointer;
}
        </style>
     <style type="text/css">
         .selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;

	
}
.btn1
{
	
    background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
}
         </style>
</head>
<body style="background-color: #f9f9f9; margin: 0 0 0 0;">
    <form id="form1" runat="server">

        <div style="text-align: left">
            <script  type="text/javascript">
                function OpenShipDetailReport()
                {
                    var Vess=document.getElementById('ddl_Vessel').value;
                    var Month=document.getElementById('ddl_Month').value;
                    var Year=document.getElementById('ddl_Year').value;
                    window.open('ShipImportedDate.aspx?Vess='+Vess+'&Month='+Month+'&Year='+Year+'');
                }
                function OpenPaySlipReport(PayrollId)
                {
                    window.open('../CrewAccounting/ViewEditPaySlip.aspx?PayrollId='+PayrollId);
                }
                function OpenFinalWagesReport(CrewNo,Rank,Name)
                {
                    var Vess=document.getElementById('ddl_Vessel').value;
                    var V=document.getElementById('ddl_Vessel');
                    var VesselName=V.options[V.selectedIndex].text;
        
                    var Month=document.getElementById('ddl_Month').value;
                    var Year=document.getElementById('ddl_Year').value;
                    window.open('../Reporting/FinalWagesAccount.aspx?Vess='+Vess+'&Month='+Month+'&Year='+Year+'&CrewNo='+CrewNo+'&VesselName='+VesselName+'&Rank='+Rank+'&Name='+Name+'');
                }
            </script>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <script language="javascript" type="text/javascript">
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                //-------------------------
                var Height1=window.innerHeight;
                var Height2=window.document.body.scrollHeight;
                var CH=(parseInt(Height1)>parseInt(Height2))?parseInt(Height1):parseInt(Height2);   
                var CH1=(parseInt(CH)/2) +100;
                //-------------------------
                var Width1=window.innerWidth;
                var Width2=window.document.body.scrollWidth;
                var CW=(parseInt(Width1)>parseInt(Width2))?parseInt(Width1):parseInt(Width2);   
                //-------------------------
                prm.add_initializeRequest
                    (
                        function () {
                            dvGray.style.height = CH + 'px';
                            dvCont.style.marginTop = '-' + CH1 + 'px';
                            dvGray.style.width = CW + 'px';
                        }
                    );
            </script>

            <%--<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1" >
                <ProgressTemplate>
                <div style="position : absolute; top:0px;left:0px; width:100%; z-index:0; text-align :center; color :Blue;">
                <center>
                    <div id="dvGray" style="background-color :Gray;opacity:0.4;z-index:1; filter:alpha(opacity=40); width :100%">
        
                    </div>
                    <div style="border:dotted 1px blue; width :200px;z-index:200;background-color :White;" id="dvCont">
                    <img src="../Images/loading1.gif" alt="loading"> 
                    </div>
                </center>
                </div>
   
                </ProgressTemplate> 
                </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" id="up1">
            <ContentTemplate>--%>

            <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0" >
                <tr>
                    <td style="text-align :left; vertical-align: top;">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                           <%-- <tr>
                                <td class="text headerband">
                                    <div style="float :left" id="div_Post"><asp:Button ID="Button3" runat='server' Text="Crew DB" BackColor="White" Font-Size="11px" Visible="false" /></div>
                                    <img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
                                    Payroll
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="padding: 5px;">
                                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: right; padding-right: 5px;">
                                                Vessel :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" TabIndex="3" Width="198px"></asp:DropDownList>
                                                <asp:HiddenField ID="hfd_PayrollId" runat="server" />
                                            </td>
                                            <td style="text-align: left;">     
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;">Month:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="94px">
                                                    <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                                    <asp:ListItem Value="7">Jul</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                                    ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;">
                                                Year:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="94px">
                                                    <asp:ListItem Value="0">&lt; Select &gt; </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Year"
                                                    ErrorMessage="*" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" Width="120px" OnClick="btn_search_Click" />
                                                <asp:Label ID="lbl_gv_Main" runat="server"></asp:Label>
                                                <asp:Button ID="btnPrint" runat="server" CssClass="btn" OnClick="btnPrint_Click" TabIndex="7" Text="Print Portage Bill" Width="120px" />
                                                <asp:Button ID="btnPrintShipDetails" runat="server" CssClass="btn" OnClick="btnPrintShipDetails_Click" TabIndex="7" Text="Deduction Received From Ship" Width="200px" OnClientClick="OpenShipDetailReport()" />
                                                <asp:Button ID="BtnPortageBillClosure" runat="server" CssClass="btn" CausesValidation="false" TabIndex="8" Text="Month Closure" OnClick="BtnPortageBillClosure_OnClick" Width="100px" OnClientClick="return confirm('Are you sure to colse this Payroll?')" />
                                                <asp:Button ID="btnGeneratePaySlip" runat="server" CssClass="btn" CausesValidation="false" TabIndex="8" Text="Generate PaySlip" OnClick="btnGeneratePaySlip_OnClick" Width="130px"/>

                                                <%--<a href="WageScaleMasterNew.aspx" target="_blank">Open Wage Scale</a>--%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnBack" runat="server" CssClass="btn" CausesValidation="false" PostBackUrl="~/Modules/HRD/CrewRecord/CrewSearch.aspx" TabIndex="7" Text="Back" Width="65px" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;width:100% " id="td_list">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top" width="430px">
                                                <div style="overflow-y: hidden; overflow-x: hidden;width:100%;" >
                                                    <table cellpadding="2" cellspacing="0" width="100%" border="0" style="">
                                                        <tr>
                                                            <td colspan="6" style=" text-align :center; height :29px;">
                                                                <strong>Crew List</strong><b>
                                                                <asp:Label ID="lblTotCrew" runat="server" ></asp:Label>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <table cellpadding="2" cellspacing="0" style="border-collapse:collapse;width:100%;" border="1">
                                                    <tr class= "headerstylegrid" style=" height:45px;font-size:10px;">
                                                        <td style="width :13px;"><img src="../Images/HourGlass.gif" /></td>
                                                        <td style="width :50px;">Crew #</td>
                                                        <td>Name</td>
                                                        <td style="width :50px;text-align :center">Rank</td>
                                                        <td style="width :30px;text-align:center;">FD</td>
                                                        <td style="width :30px;text-align:center;">TD</td>
                                                        <td style="width :30px;text-align:center;">OT (Hrs)</td>
                                                    </tr>

                                                    <asp:Repeater runat="server" ID="rptPersonal">
                                                    <ItemTemplate>
                                                    <tr style='font-size:10px;background-color:<%# (Convert.ToString(Session["vPayrollID"])==Convert.ToString(Eval("payrollID")))?"Orange":(Eval("Verified").ToString()=="True")?"#C3FDB8":"#FAAFBE"%>' >
                                                    <%--<tr style='font-size:10px;background-color :<%# (Eval("Verified").ToString()=="True")?"#C3FDB8":"#FAAFBE"%>'>--%>
                                                        <td>
                                                            <asp:ImageButton runat="server" ID="btnHourG" OnClick="btnHourG_Click" CommandArgument='<%#Eval("Sno")%>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" />
                                                        </td>
                                                        <td style="width :50px;">
                                                            <a onclick="OpenModifyContract(<%#Eval("ContractID")%>)" style="cursor:pointer;">
                                                                <asp:Label ID="lblCrewNo" runat="server" Text='<%#Eval("CrewNumber")%>'></asp:Label>
                                                                </a>
                                                            <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractID")%>' />
                                                            <asp:HiddenField ID="hfdFD" runat="server" Value='<%#Eval("StartDay")%>' />
                                                            <asp:HiddenField ID="hfdTD" runat="server" Value='<%#Eval("EndDay")%>' />
                                                            <asp:HiddenField ID="hfdOT" runat="server" Value='<%#Eval("OtHrs")%>' />
                                                        </td>
                                                        <td>           
                                                            <div style="width :100%; height :15px;overflow :hidden;">
                                                            <%#Eval("CrewName")%>
                                                            </div>  
                                                        </td>
                                                        <td style="width :50px; text-align :center;height:15px;"><%#Eval("RankCode")%></td> 
                                                        <td style="width :30px; text-align :center"><%#Eval("StartDay")%>
                                                            <span style="color:Red"><%# ((Eval("StartDay").ToString()!=Eval("StartDay_O").ToString())?"*":"")%></span>
                                                        </td>
                                                        <td style="width :30px; text-align :center"><%#Eval("EndDay")%>
                                                            <span style="color:Red"><%# ((Eval("EndDay").ToString()!=Eval("EndDay_O").ToString())?"*":"")%></span>
                                                        </td>
                                                        <td style="width :30px; text-align :center"><%#Eval("OtHrs")%></td>
                                                    </tr>
                                                    </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                            <td valign="top" style="text-align:center;">
                                                <table cellpadding="2" cellspacing="0" border="0" width="600px" >
                                                   <%-- <tr style="text-align :center; cursor :pointer; ">
                                                        <td style="background-color : #c18efa;height :20px;" onclick="SetDv(this,1);"><b>WAGES DURING THE MONTH</b></td>
                                                        <td style="background-color : #eebd67;height :20px;" onclick="SetDv(this,2);"><b>DEDUCTIONS</b></td>
                                                        <td style="background-color : #42c774;height :20px;" onclick="SetDv(this,3);"><b>BALANCE</b></td>
                                                        <td style="background-color : #6c92d3;height :20px;" onclick="SetDv(this,4);"><b>WAGES AS PER CONTRACT</b></td>
                                                        <td style="background-color : #ffe4e1;height :20px;" onclick="SetDv(this,5);"><b>DOCUMENTS</b></td>
                                                    </tr>--%>
                                                    <tr style="text-align :center; cursor :pointer; ">
                                                        <td>
                                                            <asp:Button runat="server" ID="btnWageContract" Text="WAGES AS PER CONTRACT " CssClass="selbtn" OnClick="btnWageContract_Click" Height="30px"/>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnWageMonth" Text="WAGES DURING THE MONTH " CssClass="btn1" OnClick="btnWageMonth_Click" Height="30px"/>
                                                        </td>
                                                        <td>
                                                             <asp:Button runat="server" ID="btnDeductions" Text="DEDUCTIONS" CssClass="btn1" OnClick="btnDeductions_Click" Height="30px"   />
                                                        </td>
                                                        <td>
                                                             <asp:Button runat="server" ID="btnBALANCE" Text="BALANCE" CssClass="btn1" OnClick="btnBALANCE_Click" Height="30px"   />
                                                        </td>
                                                         <td>
                                                             <asp:Button runat="server" ID="btnDocuments" Text="DOCUMENTS" CssClass="btn1" OnClick="btnDocuments_Click"  Height="30px"  />
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>
                                                        <td colspan="5" style="height:5px;background-color: #c18efa;" id="dvLine" colspan="34"></td>
                                                    </tr>--%>
                                                </table>
               
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%" style=" border-collapse:collapse">
                                                    <tr>
                                                        <td style="padding:0px;" id="dv_4" runat="server"  >
                                                            <table cellpadding="2" cellspacing="0" border="1" style=" border-collapse:collapse; text-align:right; background-color:#FAAFBE">
                                                                <tr style="text-align :center;  height :45px;" class= "headerstylegrid">
                                                                    <td style="width :77px;"><%=FieldNames[0]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[1]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[2]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[3]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[4]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[5]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[6]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[7]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[8]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[9]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[10]%></td>
                                                                    <td style="width :77px;"><%=FieldNames[11]%></td>
                                                                    <td style="width :77px;">EXTRA ALLOW</td>
                                                                    <td style="width :77px;">TOTAL</td>
                                                                </tr>
                                                                <asp:Repeater runat="server" ID="rptWages">
                                                                <ItemTemplate>
                                                                <tr>
                                                                    <td style="width :77px; height:15px;">&nbsp;<%#Eval("Cont_Comp_1")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_2")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_3")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_4")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_5")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_6")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_7")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_8")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_9")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_10")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_11")%></td>
                                                                    <td style="width :77px;"><%#Eval("Cont_Comp_12")%></td>
                                                                    <td style="width :77px;"><%#Eval("MTMAllowance")%></td>
                                                                    <td style="width :77px;"><%#Eval("TotalEarnings")%></td>
                                                                </tr>
                                                                </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                        <td style="padding:0px;" id="dv_1" runat="server" visible="false">
                                                            <table cellpadding="2" cellspacing="0" border="1" style=" border-collapse:collapse; text-align :right;background-color : #FAAFBE ">
                                                                <tr style="text-align :center;  height :45px; font-size:10px;" class= "headerstylegrid">
                                                                    <td style="width :70px;"><%=FieldNames[0]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[1]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[2]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[3]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[4]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[5]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[6]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[7]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[8]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[9]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[10]%></td>
                                                                    <td style="width :70px;"><%=FieldNames[11]%></td>
                                                                    <td style="width :70px;">EXTRA ALLOW</td>
                                                                    <td style="width :70px;">TOTAL EARNINGS</td>
                                                                </tr>
                                                                <asp:Repeater runat="server" ID="rptWagesCalc">
                                                                <ItemTemplate>
                                                                <tr style="font-size:10px;">
                                                                    <td style="width :70px; height:15px;">&nbsp;<%#Eval("Cont_Comp_1")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_2")%> </td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_3")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_4")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_5")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_6")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_7")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_8")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_9")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_10")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_11")%></td>
                                                                    <td style="width :70px;"><%#Eval("Cont_Comp_12")%></td>
                                                                    <td style="width :70px;"><%#Eval("MTMAllowance")%></td>
                                                                    <td style="width :70px;"><%#Eval("TotalEarnings")%></td>
                                                                </tr>
                                                                </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                        <td style="padding:0px;" id="dv_2" runat="server" visible="false">
                                                            <table cellpadding="2" cellspacing="0" border="1" style=" border-collapse:collapse; text-align :right;background-color : #FAAFBE ">
                                                                <tr style="text-align :center; height :45px;" class= "headerstylegrid">
                                                                    <td style="width :90px;">ALLOTMENT</td>
                                                                    <td style="width :90px;">CASH ADVANCE</td>
                                                                    <td style="width :90px;">BONDED STORE</td>
                                                                    <td style="width :90px;">RADIO TELE-CALL</td>
                                                                    <td style="width :90px;">OTHER DEDUCTIONS</td>
                                                                    <td style="width :90px;">FBOW PAID ONBOARD</td>
                                                                    <td style="width :90px;">UNION FEE</td>
                                                                    <td style="width :90px;">PAID IN OFFICE</td>
                                                                    <td style="width :90px;">TOTAL DEDUCTIONS</td>
                                                                </tr>
                                                                <asp:Repeater runat="server" ID="rptDED">
                                                                <ItemTemplate>
                                                                <tr>
                                                                    <td style="width :90px; height:15px;"><%#Eval("Allotment")%></td>
                                                                    <td style="width :90px;"><%#Eval("CashAdvance")%></td>
                                                                    <td style="width :90px;"><%#Eval("BondedStore")%></td>
                                                                    <td style="width :90px;"><%#Eval("RadioTelecall")%></td>
                                                                    <td style="width :90px;"><%#Eval("OtherDeductions")%></td>
                                                                    <td style="width :90px;"><%#Eval("FbowPaidOnBoard")%></td>    
                                                                    <td style="width :90px;"><%#Eval("UnionFee")%></td>                        
                                                                    <td style="width :90px;"><%#Eval("Paid")%></td>           
                                                                    <td style="width :90px;"><%#Eval("TotalDeductions")%></td>
                                                                </tr>
                                                                </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                        <td style="padding:0px;" id="dv_3" runat="server" visible="false">
                                                            <table cellpadding="2" cellspacing="0" border="1" style=" border-collapse:collapse; text-align :right;background-color : #FAAFBE ">
                                                                <tr style="text-align :center; height:45px; " class= "headerstylegrid">
                                                                    <td style="width :100px;">THIS MONTH'S BALANCE</td>
                                                                    <td style="width :100px;">PREVIOUS BALANCE</td>
                                                                    <td style="width :100px;">BALANCE OF WAGES</td>
                                                                </tr>
                                                                <asp:Repeater runat="server" ID="rptBal">
                                                                <ItemTemplate>
                                                                <tr>
                                                                    <td style="width :100px; height:15px;">
                                                                        <asp:Label ID="lblCol1" runat="server" Text='<%#Eval("Col1")%>' ></asp:Label>                            
                                                                    </td>
                                                                    <td style="width :100px;">
                                                                        <asp:Label ID="lblCol3" runat="server" Text='<%#Eval("Col2")%>' ></asp:Label>                            
                                                                    </td>
                                                                    <td style="width :100px;">
                                                                        <asp:Label ID="lblCol4" runat="server" Text='<%#Eval("Col3")%>' ></asp:Label>                            
                                                                    </td>
                                                                </tr>
                                                                </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                        
                                                        <td id="dv_5"  style="padding:0px;"  runat="server" visible="false">
                                                            <table cellpadding="2" cellspacing="0" border="1" style=" border-collapse:collapse; text-align :right;background-color : #FAAFBE " width="600px">
                                                                <tr style="text-align :center;  height :45px;" class= "headerstylegrid">
                                                                    <td style="width :77px;">Uploaded Files</td>
                                                                </tr>
                                                                <asp:Repeater ID="rptPayollDoc" runat="server" OnItemDataBound="rptPayollDoc_OnItemDataBound" >
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="width :77px; height:15px;">
                                                                                <asp:HiddenField ID="hfPayrollID" runat="server" Value='<%#Eval("PayrollID")%>' />
                                                                                <asp:Panel ID="Panel" runat="server" ></asp:Panel>
                                                                                <div id="div" runat="server"></div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>      
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
            </table>
        </div>
</form>
</body>
</html>
