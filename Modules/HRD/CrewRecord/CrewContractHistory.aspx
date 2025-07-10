<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewContractHistory.aspx.cs" Inherits="Modules_HRD_CrewRecord_CrewContractHistory" %>


    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <title>EMANAGER</title>
            <style type="text/css">
.hd
{
	background-color : #c2c2c2;
}
a img
{
border:none;	
}
</style>
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            // prtContent.innerHTML=strOldOne;
        }
        function Show_Image_Large(obj) {
            window.open(obj.src, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
       
       
    </script>
    <script language="javascript" type="text/javascript">
        function ConfirmDelete(ctl) {
            if (confirm('Are you sure to delete ?')) {
                ctl.src = '../Images/inprocss.gif';
                return true;
            }
            else {
                return false;
            }
        }
        function Show_Amount() {
            try {
                var Amt = 0.0;
                var deduct = 0.0;
                var temp = 0;
                var ob = null;
                var CtlList = new Array("02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13","14","15","16","17","18","19","20");
                for (i = 0; i <= 18; i++) {
                    ob = document.getElementById("Gd_AssignWages_ctl" + CtlList[i] + "_txtAmount");
                    if (ob != null) {
                        if (ob.getAttribute('Wid') != "12") {
                            temp = parseFloat(ob.value);
                            if (!isNaN(temp)) {
                                Amt = Amt + parseFloat(temp);
                            }
                        }
                    }
                }
                
                for (i = 0; i <= 18; i++) {
                    ob = document.getElementById("Gd_AssignWagesDeductions_ctl" + CtlList[i] + "_txtAmount");
                    if (ob != null) {
                        if (ob.getAttribute('Wid') != "12") {
                            temp = parseFloat(ob.value);
                            if (!isNaN(temp)) {
                                deduct = deduct + parseFloat(temp);
                            }
                        }
                    }
                }
               
                var Gross = 'lb_Gross';
                var Ded ='lb_deduction';
                var Net = 'lb_NewEarning1';
                var Txt = 'txt_Other_Amount';

                var Gross_Val = Amt;//"0" + document.getElementById(Gross).innerHTML;
                var Ded_Val = deduct;// + document.getElementById(Ded).innerHTML;
                var Amount = "0";// + document.getElementById(Txt).value;
                
                if (isNaN(Gross_Val)) { Gross_Val = "0"; }
                if (isNaN(Ded_Val)) { Ded_Val = "0"; }
                if (isNaN(Amount)) { Amount = "0"; }

                Amount = parseFloat(Gross_Val) - parseFloat(Ded_Val) + parseFloat(Amount);
                if (document.getElementById(Txt).value != "") {
                    Amount = Amount + parseFloat(document.getElementById(Txt).value);
                }
                Amount = Math.round(Amount * 100) / 100;
                if (isNaN(Amount)) { Amount = "0"; }
                document.getElementById(Net).innerHTML = Amount;
            }
            catch (err) {
            }
        }
    </script>
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
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
         <div style="text-align: left;font-family:Arial, Helvetica, sans-serif;font-size:12px;">
    
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%" id="tblActivity" runat="server">
            <tr>
                <td colspan="3" style="text-align :center"  >
                    <asp:Label ID="lblCrewMemberMsg" runat="server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1" Width="100%"> </asp:Label>
                </td>
            </tr>
             <tr>
                <td style="padding-right: 5px; text-align :right;width:100px;height:25px; ">
                        Crew Number :
                </td>
                <td style="text-align :left;width:400px; height:25px;padding-right: 10px;">
                    <asp:TextBox ID="txtMemberNo" runat="server" MaxLength="6" Width="150px" OnTextChanged="txtMemberNo_TextChanged"></asp:TextBox> 
                        <asp:RequiredFieldValidator id="rfvMemberNo" runat="server" ErrorMessage="Required." ControlToValidate="txtMemberNo" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" Width="100px" OnClick="btnSearch_Click" />
                </td>
                <td style="text-align :left;width:300px; height:25px;">

                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="tbltr" runat="server" visible="false" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                           <td style=" text-align :left; vertical-align : top;">
                            <asp:Panel ID="pnl_history_1" runat="server" Height="100%" Width="100%">
                <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                                                                        <legend style="text-align: left"><strong>Contract Details</strong></legend>
                                                                        <asp:Label ID="lbl_contract_message" runat="server"></asp:Label>
                                                                         <div style=" width:100%; height:165px; overflow-y: scroll; overflow-x: hidden;" > 
<asp:GridView style="TEXT-ALIGN: center" id="gv_Contract" runat="server" Width="98%" GridLines="Horizontal" AutoGenerateColumns="False"  OnSelectedIndexChanged="gv_Contract_SelectedIndexChanged" OnPreRender="gv_Contract_PreRender" OnRowCancelingEdit="gv_Contract_RowCancelingEdit" OnRowCommand="gv_Contract_RowCommand">
                                         <Columns>
                                           <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True"><ItemStyle Width="30px" /></asp:CommandField>
                                          <%-- <asp:CommandField ButtonType="Image" HeaderText="Edit" EditImageUrl="~/Modules/HRD/Images/edit.jpg" ShowEditButton="True"><ItemStyle Width="30px" /></asp:CommandField>
                                              <asp:TemplateField HeaderText="Edit" >
                                                                <ItemStyle Width="25px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Imgbtngv_ContractEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                           <asp:TemplateField HeaderText="Ref.#" ><ItemTemplate>
                                                <asp:Label ID="lbl_Contractid" runat="server" Text='<%# Eval("RefNo") %>'>
                                                </asp:Label><asp:HiddenField ID="hfd_ContractId" runat="server" Value='<%# Eval("ContractId") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hfd_OtherAmount" runat="server" Value='<%# Eval("OtherAmount") %>' />
                                                <asp:HiddenField ID="hfContractStauts" runat="server" Value='<%# Eval("Status") %>' />
                                             </ItemTemplate>
                                             <ItemStyle Width="70px" />
                                          </asp:TemplateField>
                                             <asp:BoundField DataField="Name" HeaderText="Name" ></asp:BoundField>
                                             <asp:BoundField DataField="RankCode" HeaderText="Planned Rank" ></asp:BoundField>
                                              <asp:BoundField DataField="NewRankCode" HeaderText="Rank" ></asp:BoundField>
                                              <asp:BoundField DataField="VesselName" HeaderText="Vessel" ></asp:BoundField>
                                              <asp:TemplateField HeaderText="Issue Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Start Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                             <asp:BoundField DataField="CountryName" HeaderText="Nationality" ></asp:BoundField>
                                             <asp:BoundField DataField="Status" HeaderText="Status" ></asp:BoundField>
                                         </Columns>
                                         <RowStyle CssClass="rowstyle" HorizontalAlign="Left"  />
                                         <SelectedRowStyle CssClass="selectedtowstyle"  />
                                         <HeaderStyle CssClass="headerstylefixedheadergrid"   />
                                     </asp:GridView><asp:HiddenField ID="hfcontractid" runat="server" />
                                                                        </div>
                                                                    </fieldset>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                          <tr id="trcontractdetails" runat="server"><td style="padding-bottom: 0px; padding-top: 0px;" align="right">
                                                                          
                                                                                  <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center">
             <br />
             <table cellpadding="0" cellspacing="0" width="100%" border="1" >
                <tr>
                    <td>
                        <div style="color:Black;width:100%; background-color:#c2c2c2; font-size:13px; padding-bottom:4px;"><b>Crew Details</b></div>
                    </td>
                    <td>
                        <div style="color:Black;width:100%; background-color:#c2c2c2; font-size:13px; padding-bottom:4px;"><b>Wages Details</b>&nbsp;</div>
                    </td>
                </tr>
                <tr>
                    <td style="width:400px; font-size:12px;vertical-align : top;">
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="text-align:left;" >
                             <tr>
                                 <td align="right" >Crew Name : </td>
                                 <td >
                                    <asp:Label ID="lb_name" runat="server" Width="100%"></asp:Label>
                                 </td>
                                 
                             </tr>
                             <tr>
                                <td align="right">Planned Vessel : </td>
                                 <td >
                                    <asp:Label ID="lb_PlanVessel" runat="server" Width="100%"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <td align="right">Planned Rank : </td>
                                 <td >
                                    <asp:Label ID="Lb_PlanRank" runat="server" Width="100%"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <td align="right">Nationality : </td>
                                <td >
                                     <asp:Label ID="txt_contact_nationality" runat="server" Width="100%" ></asp:Label>
                                </td>
                             </tr>
                              <tr>
                                <td align="right">
                                    Wage Scale : 
                                    </td>
                                 <td >
                                     <asp:DropDownList ID="dp_wagescale" runat="server"  ValidationGroup="abc" Enabled="false" Width="200px" CssClass="input_box"></asp:DropDownList>
                                     <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="dp_wagescale" ErrorMessage="*" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                 </td>                     
                             </tr>
                                   <tr>
                                <td align="right">Contract Period(nos.) : </td>
                                 <td ><asp:Label ID="Txt_ContractPeriod" runat="server" Width="100%"></asp:Label></td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     Seniority(Year) : </td>
                                 <td >
                                     <asp:Label ID="Txt_Seniority" runat="server" Width="100%"></asp:Label></td>
                             </tr>
                             <tr>
                                 <td align="right"> Issue Date : </td>
                                 <td >
                                     <asp:Label ID="txt_IssueDate" runat="server"  Width="100%"></asp:Label>
                                 </td>
                                 
                             </tr>
                             <tr>
                                <td align="right"> Start Date : </td>
                                 <td >
                                     <asp:Label ID="txt_StartDate" runat="server"  Width="100%"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right" >
                                     &nbsp; Rank : </td>
                                 <td >
                                     <asp:Label ID="txt_contract_rank" runat="server" Width="100%"></asp:Label></td>
                                 
                             </tr>
                             <tr runat="server" visible="false">
                                <td align="right">
                                     Ref. No. : </td>
                                 <td >
                                     <asp:Label ID="lbl_RefNo" runat="server"></asp:Label></td>
                                 
                             </tr>
                             <tr runat="server" visible="false">
                                <td align="right" >
                                     Other Amount : </td>
                                 <td >
                                     <asp:Label ID="txt_OtherAmount" runat="server"  Width="100%"></asp:Label></td>
                             </tr>
                             
                             
                             <tr style="color: #0e64a0">
                                 <td >
                                     <asp:Label ID="txt_wagescale" runat="server" Width="100%" Height="35px" Visible="False"></asp:Label></td>
                                 <td >
                                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_OtherAmount"
                                         Display="Dynamic" ErrorMessage="Required" Width="57px"></asp:RequiredFieldValidator>--%>

                                 </td>
                             </tr>
                            <tr id="Tr1" runat="server" visible="false">
                                 <td align="right" >Sup. Cert. Allow. : </td>
                                 <td ><asp:CheckBox ID="chk_SupCert" runat="server" Enabled="false" /></td>                     
                             </tr>
                             
                         </table>           
                    </td>
                    <td valign="top">
                        <table width="100%">
                            <tr>
                                <td style="text-align:center;">
                                    <b> Earnings </b>
                                </td>
                                <td style="text-align:center;">
                                    <b> Deductions </b>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <%--<div style="overflow-x: hidden;overflow-y: scroll;width:100%;">--%>
                                    <asp:GridView ID="Gd_AssignWages"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                            Width="100%">
                             <Columns>
                               <asp:TemplateField HeaderText="Component Type" Visible="false">
                               <ItemTemplate>
                                 <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' /> 
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"  />               
                               <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right">
                               <ItemTemplate >   
                                <asp:TextBox runat="server" onchange="Show_Amount(); " Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" style="text-align :right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>' ></asp:TextBox>
                                <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' /> 
                                <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractId")%>' /> 
                                
                               </ItemTemplate>
                               </asp:TemplateField> 
                            </Columns>
                            <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                        </asp:GridView>
                                        <%--</div>--%>
                                </td>
                                <td width="50%">
                                   <%-- <div style="overflow-x: hidden;overflow-y: scroll;width:100%;">--%>
                                    <asp:GridView ID="Gd_AssignWagesDeductions"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                            Width="100%">
                             <Columns>
                               <asp:TemplateField HeaderText="Component Type" Visible="false">
                               <ItemTemplate>
                                 <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' /> 
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"  />               
                               <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right">
                               <ItemTemplate >   
                                <asp:TextBox runat="server" onchange="Show_Amount(); " Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" style="text-align :right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>' ></asp:TextBox>
                                <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' /> 
                                <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractId")%>' /> 
                                
                               </ItemTemplate>
                               </asp:TemplateField> 
                            </Columns>
                            <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                        </asp:GridView>
                                        <%--</div>--%>
                                </td>
                            </tr>
                        </table>
                           
                    
                         <table width="100%" cellpadding="2" cellspacing="0" style="text-align : center; border-collapse:collapse;margin-top:10px;" bordercolor="Gray" border="1">
                                <tr>
                                    <td style="text-align: left;width:270px; ">EXTRA ALLOW (M) :</td>
                                    <td style="text-align: right;" colspan="3"><b><asp:TextBox ID="txt_Other_Amount" style="text-align :right " onkeyup="Show_Amount();" CssClass="input_box" width="80px" runat="server"></asp:TextBox></b></td>
                                   
                                </tr>
                                
                                <tr>
                                    <td style="text-align: left;"><b>Net Earnings (Total Earning - Total Deduction) :</b></td>
                                    <td style="text-align: left;padding-left:5px;" colspan="3"><asp:Label Font-Bold="true" ID="lb_NewEarning1" ForeColor="Green" runat="server" Width="80px"></asp:Label></td>
                                </tr>
                             <tr>
                                    <td style="text-align: left;width:270px; ">EXTRA OT RATE (Hourly) :</td>
                                    <td style="text-align: right;width:150px; "><b><asp:TextBox ID="txtExtraOTRate" style="text-align :right "  CssClass="input_box" width="80px" runat="server"></asp:TextBox></b></td>
                                     <td style="text-align: left;width:150px; "> Travel Pay (Per Day) :</td>
                                    <td style="text-align: left; "> <asp:DropDownList ID="ddlTravelPay" runat="server" Width="150px">
                                        <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem  Text="Full Salary Basis" Value="1"></asp:ListItem>
                                        <asp:ListItem  Text="Basic Wages Basis" Value="2"></asp:ListItem>
                                       <%-- <asp:ListItem  Text="Custom (Wages per day)" Value="3"></asp:ListItem>--%>
                                                                     </asp:DropDownList></td>
                                </tr>
                        </table><b>Remarks</b>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="input_box" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                        <br />
                            
                            <b>  <asp:Label ID="lblLockbyOn" runat="server" ></asp:Label> </b>
                           
                            <asp:Button ID="btn_contract_cancel" runat="server" CausesValidation="False" CssClass="btn" Text="Cancel" Width="59px" TabIndex="10" OnClick="btn_contract_cancel_Click" style="float:right;margin:1px;"  />
                            <asp:Button ID="btnUpdateWages" runat="server" Text="Modify Current Wages" CssClass="btn" OnClick="btnUpdateWages_OnClick" style="float:right;margin:1px;" CausesValidation="false"  />
                         <asp:Button ID="btnConRevision" runat="server" Text="Create New Contract" CssClass="btn"  style="float:right;margin:1px;" CausesValidation="false" OnClick="btnConRevision_Click"  />
                            <asp:Label ID="lblMsgWages" runat="server" ForeColor="Red" style="float:right;margin:1px;" ></asp:Label>
                        </td>
                    </tr>
                 <tr>
                     <td colspan="2">
                         <table style="width:100%;">
                             <tr>
                                 <td style="width:150px; text-align:right; padding-right:5px;">
                                     <asp:Label ID="lblContractTemplate" runat="server" Text="Contract Template :"></asp:Label>
                                 </td>
                                 <td style="width:275px; text-align:Left; padding-Left:10px;">
                                     <asp:DropDownList ID="ddl_ContractTemplate" runat="server" width="200px" CssClass="required_box"  > 
                                         <asp:ListItem Value="0">< Select ></asp:ListItem>
                                     </asp:DropDownList>
                                     &nbsp;
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddl_ContractTemplate"
                                                                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                 </td>
                                 <td style="text-align:Left; padding-Left:10px;">
                                      <asp:Button ID="btn_License_Print" runat="server" CssClass="btn"  Text="Print" Width="70px" OnClick="btn_License_Print_Click" CausesValidation="false" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                    
                 </tr>
                 </table>
             
             
             
             
             
             
             
    </fieldset>
                                                                              <br />
                                                                              <asp:HiddenField ID="HiddenField_NationalityId" runat="server" />
                                                                              <asp:HiddenField ID="HiddenField_RankId" runat="server" />
                                                                                  <br />
                                                                                  <br />
                                                                                  <table style="width:100%;">
                                                                                      <tr>
                                                                                          <td style="text-align: left; width :50%">
                                                                                              
                                                                                          </td>
                                                                                          <td style="width :50%" >
                                                                                          </td>
                                                                                      </tr>
                                                                                  </table>
                                                                                  <br />
    </td></tr></table>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="text-align: right;">
                                                                                <asp:HiddenField ID="hfd_vesselid" runat="server" />
                                                                                 <asp:HiddenField ID="HiddenPK" runat="server" />
                                                                                <asp:Button style="display:none" ID="btn_CrewExt" runat="server" CausesValidation="False" CssClass="btn" Text="Crew Extension" Width="100px" TabIndex="10" OnClick="btn_CrewExt_Click" />
                                                                                <asp:Button Visible="false" ID="btn_close" runat="server" CausesValidation="False" CssClass="btn" Text="Close Contract" Width="100px" TabIndex="10" OnClick="btn_close_Click"  OnClientClick="return confirm('Are you Sure to Close this Contract?')" />
                                                                                <asp:Button ID="btn_ContractSave" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="10" OnClick="btn_contract_Save_Click"  />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                </asp:Panel>
                               <div style="position:absolute;top:50px;left:0px; height :300px; width:100%;" id="dvContractRevision" runat="server" visible="false">
                                    <center>
                                        <div style="position:absolute;top:0px;left:0px; height :290px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                         <div style="position :relative;width:800px; height:280px;padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 0px #4371a5;">
                                         <center >
                                         <div  class="text headerband">
                                          <b>  Create New Contract </b>
                                         </div>
                                         <div style=' height:270px; margin-left:10px;'>
                                         <table cellspacing="0" width="100%" border="1" >
                                        <tr>
                                            <td style=" text-align :center ">
                                                <asp:Label ID="lblContractRevisionmessage" runat="server" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td runat="server" id="trContractRevision">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 15px; text-align :right ">
                                                    Emp.#:</td>
                                                <td style="text-align :left">
                                                    <asp:TextBox ID="txtPEmpNo" MaxLength="6" runat="server" CssClass="input_box" 
                                                        Enabled="false" TabIndex="1"></asp:TextBox></td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Name:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPName" runat="server" Width="100%"></asp:Label></td>
                                                
                                            </tr>
                                            
                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    Current Rank:</td>
                                                <td  style="text-align: left;">
                                                    <asp:Label ID="lblPPresentRank" runat="server"></asp:Label>
                                                    </td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Status:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPStatus" runat="server" ></asp:Label></td>
                                               
                                            </tr>
                                           
                                            <tr>
                                                 <td style="padding-right: 15px;text-align :right ">
                                                    Vessel:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPVessel" runat="server" ></asp:Label></td>
                                                
                                                <td style="padding-right: 15px;text-align :right ">
                                                 Contract Revision Start Dt:  </td>
                                                <td style="text-align :left">
                                                    <asp:TextBox ID="txt_ContRevisionDt" runat="server" CssClass="required_box" 
                                                        MaxLength="15" Width="80px" TabIndex="3"></asp:TextBox>
                                                    <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                   </td>
                                                
                                            </tr>
                                            <tr>
                                                <td ><asp:Label ID="lblPAvailableDate" runat="server" Width="122px" Visible="false"></asp:Label></td>
                                                <td > <asp:Label ID="lblPSignedOff" runat="server" Visible="false" ></asp:Label></td>
                                                <td ></td>
                                                <td style="text-align: left">   <asp:RequiredFieldValidator runat="server" ID="Req1" Display="Dynamic" 
                                                        ControlToValidate="txt_ContRevisionDt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                                        ControlToValidate="txt_ContRevisionDt" 
                                                        ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" 
                                                        ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> 
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" 
                                            TargetControlID="txt_ContRevisionDt"></ajaxToolkit:CalendarExtender>
                                                </td>
                                               
                                                </tr>
                                                <tr>
                                                <td colspan="4" style="border: solid 1px gray; text-align :right ; height :30px;" >
                                                <asp:Button ID="btn_SaveContractRevision" runat="server" CssClass="btn"  Text="Save" Width="80px" CausesValidation="false" OnClick="btn_SaveContractRevision_Click"/>
                                        &nbsp;   <asp:Button runat="server" ID="btnCloseContractRevision" Text="Close" CssClass="btn" OnClick="btnCloseContractRevision_Click"  Width='100px' CausesValidation="false"/>
                                        
                                                </td>
                                                </tr>
                                           
                                        </table>
                                        </td>
                                     </tr>
                               
                            </table>
                                         </div>
                                      
                                         </center>
                                         </div>
                                    </center>
                                    </div>
                                </td>
                    </tr>
                    </table>
                </td>
            </tr>
            
         </table>
        </div>
    </form>
</body>
</html>
