<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payroll_New.aspx.cs" Inherits="Modules_HRD_CrewAccounting_Payroll_New" %>

<!DOCTYPE html>

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
    <%--<script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //-------------------------
        var Height1 = window.innerHeight;
        var Height2 = window.document.body.scrollHeight;
        var CH = (parseInt(Height1) > parseInt(Height2)) ? parseInt(Height1) : parseInt(Height2);
        var CH1 = (parseInt(CH) / 2) + 100;
        //-------------------------
        var Width1 = window.innerWidth;
        var Width2 = window.document.body.scrollWidth;
        var CW = (parseInt(Width1) > parseInt(Width2)) ? parseInt(Width1) : parseInt(Width2);
        //-------------------------
        prm.add_initializeRequest
            (
                function () {
                    dvGray.style.height = CH + 'px';
                    dvCont.style.marginTop = '-' + CH1 + 'px';
                    dvGray.style.width = CW + 'px';
                }
            );
    </script>--%>
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
    <script  type="text/javascript">
        function OpenShipDetailReport() {
            var Vess = document.getElementById('ddl_Vessel').value;
            var Month = document.getElementById('ddl_Month').value;
            var Year = document.getElementById('ddl_Year').value;
            window.open('ShipImportedDate.aspx?Vess=' + Vess + '&Month=' + Month + '&Year=' + Year + '');
        }
        function OpenPaySlipReport(PayrollId) {
            window.open('../CrewAccounting/ViewEditPaySlip.aspx?PayrollId=' + PayrollId);
        }
        function OpenFinalWagesReport(CrewNo, Rank, Name) {
            var Vess = document.getElementById('ddl_Vessel').value;
            var V = document.getElementById('ddl_Vessel');
            var VesselName = V.options[V.selectedIndex].text;

            var Month = document.getElementById('ddl_Month').value;
            var Year = document.getElementById('ddl_Year').value;
            window.open('../Reporting/FinalWagesAccount.aspx?Vess=' + Vess + '&Month=' + Month + '&Year=' + Year + '&CrewNo=' + CrewNo + '&VesselName=' + VesselName + '&Rank=' + Rank + '&Name=' + Name + '');
        }
    </script>
</head>
<body style="background-color: #f9f9f9; margin: 0 0 0 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align: left">           
            
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
                </asp:UpdateProgress>--%>
            

            <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0" >
                <tr>
                    <td style="text-align :left; vertical-align: top;">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                          
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
                                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" Width="120px" OnClick="btn_search_Click"/>
                                                <asp:Label ID="lbl_gv_Main" runat="server"></asp:Label>
                                               

                                                <%--<a href="WageScaleMasterNew.aspx" target="_blank">Open Wage Scale</a>--%>
                                            </td>
                                            <td>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;width:100% " id="td_list">
                                    <asp:UpdatePanel runat="server" id="up1">
            <ContentTemplate>
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
                                                            <asp:HiddenField ID="hfdOT" runat="server" Value='<%#Eval("ExtraOTdays")%>' />
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
                                                        <td style="width :30px; text-align :center"><%#Eval("ExtraOTdays")%></td>
                                                    </tr>
                                                    </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                            <td valign="top" style="text-align:center;">
                                              <%-- <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>--%>
        
    <div>
        <table cellpadding="2" cellspacing="2" border="0" width="100%" bordercolor="Gray" style="border-collapse:collapse; margin:auto;">
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
                            </colgroup>
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
                                    <asp:TextBox ID="txtFD" runat="server" CssClass="required_box" Width="20px" AutoPostBack="true" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat ="server" ErrorMessage="*" ControlToValidate="txtFD"></asp:RequiredFieldValidator>
                                </td>
                                <td class="label" style="text-align:right">TD :</td>
                                <td style="text-align:left">
                                    <asp:TextBox ID="txtTD" runat="server" CssClass="required_box" Width="20px" AutoPostBack="true" ></asp:TextBox>
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
                                <td class="label" style="text-align:right">OT Hrs :</td>
                                <td style="text-align:left">
                                    <asp:TextBox ID="txtOT" runat="server" CssClass="required_box" style="float:left;" Width="20px" AutoPostBack="true" ></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator runat ="server" ErrorMessage="*" ControlToValidate="txtOT"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        
                    </table>
                </td>                
            </tr>
            <tr>
            <td> 
            
            </td>
            <td style="vertical-align :top;"> 
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" >
                
                </td>
            </tr>
                <tr>
                <td colspan="2" >
                 
                </td>
            </tr>
            
        </table>
    </div>
    
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                    </table>
                                     </ContentTemplate>
                </asp:UpdatePanel>
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
