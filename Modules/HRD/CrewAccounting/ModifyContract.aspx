<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyContract.aspx.cs" Inherits="ModifyContract" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify Contract</title>
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function Show_Amount() 
        {
            try {
                var Amt = 0.0;
                var deduct = 0.0;
                var temp = 0;
                var ob = null;
                var CtlList = new Array("02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20");
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
                var Net = 'lb_NewEarning1';
                var Txt = 'txt_Other_Amount';                

                var Gross_Val = Amt; 
                var Ded_Val = deduct; 
                var Amount = "0"; 

                if (isNaN(Gross_Val))
                { Gross_Val = "0"; }
                if (isNaN(Ded_Val))
                { Ded_Val = "0"; }
                if (isNaN(Amount))
                { Amount = "0"; }

                Amount = parseFloat(Gross_Val) - parseFloat(Ded_Val) + parseFloat(Amount);
                if (document.getElementById(Txt).value!="") {                
                    Amount = Amount + parseFloat(document.getElementById(Txt).value);
                }
                
                Amount = Math.round(Amount * 100) / 100;
                if (isNaN(Amount)) { Amount = "0"; }
                document.getElementById(Net).innerHTML = Amount;
            }
            catch (err) {}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-bottom: 0px; padding-top: 0px;" align="right">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                        <br />
                        <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse">
                            <tr>
                                <td>
                                    <div style="color: Black; width: 100%; background-color: #c2c2c2; font-size: 13px;
                                        padding-bottom: 4px;">
                                        <b>Crew Details</b></div>
                                </td>
                                <td>
                                    <div style="color: Black; width: 100%; background-color: #c2c2c2; font-size: 13px;
                                        padding-bottom: 4px;">
                                        <b>Wages Details</b>&nbsp;</div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 400px;">
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
                                 <td >&nbsp;<asp:Label ID="Txt_ContractPeriod" runat="server" Width="100%"></asp:Label></td>
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
                             <tr id="Tr1" runat="server" visible="false">
                                <td align="right">
                                     Ref. No. : </td>
                                 <td >
                                     <asp:Label ID="lbl_RefNo" runat="server"></asp:Label></td>
                                 
                             </tr>
                             <tr id="Tr2" runat="server" visible="false">
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
                                         Display="Dynamic" ErrorMessage="Required" Width="57px"></asp:RequiredFieldValidator></td>--%></td>
                             </tr>
                            <tr id="Tr3" runat="server" visible="false">
                                 <td align="right" >Sup. Cert. Allow. : </td>
                                 <td ><asp:CheckBox ID="chk_SupCert" runat="server" Enabled="false" /></td>                     
                             </tr>
                             
                         </table>           
                                    <%--<table cellpadding="3" cellspacing="0" width="100%" border="0" style="text-align: left;">
                                        <tr>
                                            <td align="right">
                                                Name:
                                            </td>
                                            <td>
                                                <asp:Label ID="lb_name" runat="server" Width="137px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Planned Rank:
                                            </td>
                                            <td>
                                                <asp:Label ID="Lb_PlanRank" runat="server" Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Planned Vessel:
                                            </td>
                                            <td>
                                                <asp:Label ID="lb_PlanVessel" runat="server" Width="126px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: #0e64a0">
                                            <td align="right">
                                                Issue Date:
                                            </td>
                                            <td>
                                                <asp:Label ID="txt_IssueDate" runat="server" Width="78px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Start Date:
                                            </td>
                                            <td>
                                                <asp:Label ID="txt_StartDate" runat="server" Width="78px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Contract Period(nos.):
                                            </td>
                                            <td>
                                                &nbsp;<asp:Label ID="Txt_ContractPeriod" runat="server" Width="1px"></asp:Label></td>
                                        </tr>
                                      
                                        <tr style="color: #0e64a0">
                                            <td align="right">
                                                Seniority(Year):
                                            </td>
                                            <td>
                                                <asp:Label ID="Txt_Seniority" runat="server" Width="78px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Wage Scale:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dp_wagescale" runat="server" ValidationGroup="abc" Enabled="false"
                                                    Width="170px" CssClass="input_box">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="dp_wagescale"
                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Nationality:
                                            </td>
                                            <td>
                                                <asp:Label ID="txt_contact_nationality" runat="server" Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: #0e64a0">
                                            <td align="right">
                                                &nbsp; Rank:
                                            </td>
                                            <td>
                                                <asp:Label ID="txt_contract_rank" runat="server" Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Ref. No.:
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_RefNo" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Other Amount:
                                            </td>
                                            <td>
                                                <asp:Label ID="txt_OtherAmount" runat="server" Width="82px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: #0e64a0">
                                            <td>
                                                <asp:Label ID="txt_wagescale" runat="server" Width="120px" Height="35px" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                            
                                        </tr>
                                        <tr style="color: #0e64a0">
                                            <td align="right">
                                                Sup. Cert. Allow.:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_SupCert" runat="server" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>--%>
                                </td>
                                <td valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td width="50%">
                                                <asp:GridView ID="Gd_AssignWages" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Style="text-align: center" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Component Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="Amount (US$)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" onchange="Show_Amount();" Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" Style="text-align: right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>'></asp:TextBox>
                                                    <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' />
                                                    <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                    </asp:GridView>
                                            </td>
                                            <td width="50%">
                                                <asp:GridView ID="Gd_AssignWagesDeductions" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Style="text-align: center" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Component Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="Amount (US$)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" onchange="Show_Amount();" Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" Style="text-align: right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>'></asp:TextBox>
                                                    <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' />
                                                    <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                    </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <table width="100%" cellpadding="2" cellspacing="0" style="text-align: center; border-collapse: collapse;
                                        margin-top: 10px;" bordercolor="Gray" border="1">
                                        <tr>
                                            <td style="text-align: left;width:200px;">
                                                EXTRA ALLOW (M) :
                                            </td>
                                            <td style="text-align: right;width:150px;">
                                                <b>
                                                    <asp:TextBox ID="txt_Other_Amount" Style="text-align: right" onkeyup="Show_Amount();" CssClass="input_box" Width="80px" runat="server"></asp:TextBox></b>
                                            </td>
                                            <td style="text-align: left;width:150px; "> Travel Pay (Per Day) :</td>
                                    <td style="text-align: left;"> <asp:DropDownList ID="ddlTravelPay" runat="server" Width="150px">
                                        <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem  Text="Full Salary Basis" Value="1"></asp:ListItem>
                                        <asp:ListItem  Text="Basic Wages Basis" Value="2"></asp:ListItem>
                                        <asp:ListItem  Text="Custom (Wages per day)" Value="3"></asp:ListItem>
                                                                     </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                EXTRA OT RATE (Hourly) :
                                            </td>
                                            <td style="text-align: right;">
                                               <b><asp:TextBox ID="txtExtraOTRate" style="text-align :right "  CssClass="input_box" width="80px" runat="server"></asp:TextBox></b>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <b>Net Earnings<span lang="en-us"> </span>(+ EXTRA ALLOW)</b>
                                            </td>
                                            <td style="text-align: right;" colspan="3">
                                                <asp:Label Font-Bold="true" ID="lb_NewEarning1" ForeColor="Green" runat="server"
                                                    Width="80px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <b>Remarks</b>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="input_box" TextMode="MultiLine"
                                        Width="98%" Height="50px"></asp:TextBox>
                                    <br />
                                    
                                    <b> <asp:Label ID="lblLockbyOn" runat="server" ></asp:Label></b>
                                    <asp:Button ID="btn_Contract_Print" runat="server" CausesValidation="False" CssClass="btn" Text="Print" Width="59px" OnClick="btn_Contract_Print_Click" Style="float: right; margin: 1px;" />
                                    
                                    <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn" OnClick="btnLock_OnClick" Style="float: right; margin: 1px;" OnClientClick="return confirm('Are you sure to lock the contract?')" />
                                    
                                    <asp:Button ID="btnUpdateWages" runat="server" Text="Update Wages" CssClass="btn" OnClick="btnUpdateWages_OnClick" Style="float: right; margin: 1px;" /> <asp:Label ID="lblMsgWages" runat="server" ForeColor="Red" Style="float: right; margin: 1px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <asp:HiddenField ID="HiddenField_NationalityId" runat="server" />
                    <asp:HiddenField ID="HiddenField_RankId" runat="server" />
                    <asp:HiddenField ID="hfd_vesselid" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        Show_Amount();
    </script>
    </form>
</body>
</html>
