<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payroll.aspx.cs" Inherits="Modules_HRD_CrewPayroll_Payroll" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <script type="text/javascript" src="../Scripts/jquery.js"></script>
      <link href="../Styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <script type="text/javascript">
       
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

        function AddDocuments(Mode, PayrollID) {
            window.open("../CrewAccounting/AddDocuments.aspx?Mode=" + Mode + "&PayrollID=" + PayrollID, '', 'width=800,height=600');
        }

        function RefereshPage() {
            document.getElementById('btn_search').click();
        }

        
            function OpenDocument(TableID) {
                window.open("../CrewAccounting/ShowDocuments.aspx?TableID=" + TableID + "");
        }

        function OpenPaySlip() {
            var PayRollID =<%=PayRollID.ToString()%>;
            window.open('../Reporting/PaySlip_New.aspx?PayrollId=' + PayRollID);
        }
        function OpenFinalWagesReport()
        {
            var PayrollId =<%=PayRollID.ToString()%>;
            window.open('../Reporting/FinalWagesAccount_New.aspx?PayrollId=' + PayrollId + '');
            //            window.open('../Reporting/FinalWagesAccount.aspx?Vess='+Vess+'&Month='+Month+'&Year='+Year+'&CrewNo='+CrewNo+'&VesselName='+VesselName+'&Rank='+Rank+'&Name='+Name+'');
        }

    </script>
   

    <style type="text/css">
        .auto-style1 {
            width: 50px;
        }
    </style>
   

</head>
<body>
    <form id="form1" runat="server">
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div class="stickyHeader" >
            <div style="padding:5px;font-family:Arial;font-size:12px;">
                <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: right; padding-right: 5px;">
                                                Vessel :
                                            </td>
                                            <td style="text-align: left;width:150px">
                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" TabIndex="3" Width="198px"></asp:DropDownList>
                                                <asp:HiddenField ID="hfd_PayrollId" runat="server" />
                                            </td>
                                            <td style="text-align: left;width:50px">     
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;width:75px">Month:
                                            </td>
                                            <td style="text-align: left;width:80px">
                                                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="75px" >
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
                                            <td style="text-align: left;width:30px">
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                                    ErrorMessage="*" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td style="text-align: right; padding-right: 5px;width:75px">
                                                Year:
                                            </td>
                                            <td style="text-align: left;width:100px">
                                                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="3"
                                                    Width="94px">
                                                    <asp:ListItem Value="0">&lt; Select &gt; </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;width:50px">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Year"
                                                    ErrorMessage="*" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Show Data" Width="120px" CausesValidation="false" OnClick="btn_search_Click"/> &nbsp;
                                                
                                                <asp:Label ID="lbl_gv_Main" runat="server"></asp:Label>
                                               

                                                <%--<a href="WageScaleMasterNew.aspx" target="_blank">Open Wage Scale</a>--%>
                                            </td>
                                            <td>
                                               
                                            </td>
                                        </tr>
            <tr>
                <td colspan="11"  align="center">
                 
                    <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                
                </td>
            </tr>
                                    </table>                             
            </div>
        </div>
        <br /> 
        <div style="height:30px"><br /></div>
            <div style="overflow-y: scroll; overflow-x: scroll;width:100%;max-height:715px;">
                <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                    <tr>
                                <td style="text-align: left;width:100% " id="td_list">
                                 <%--   <asp:UpdatePanel runat="server" id="up1">
            <ContentTemplate>--%>
                                        
                           <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top" width="430px">
                                      
                                                    <table cellpadding="2" cellspacing="0" width="100%" border="0" >
                                                        <tr>
                                                            <td colspan="6" style=" text-align :center; height :29px;">
                                                                <strong>Crew List</strong><b>
                                                                <asp:Label ID="lblTotCrew" runat="server" ></asp:Label>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                              <%-- <div style="overflow-y:scroll;max-height:750px;overflow-x:scroll;width:100%;">--%>
                                                   <table cellpadding="2" cellspacing="0" style="border-collapse:collapse;width:100%;" border="1">
                                                    <tr class= "headerstylegrid" style=" height:45px;font-size:14px;position:sticky;top:0px;">
                                                        <td style="width :13px;"><img src="../Images/HourGlass.gif" /></td>
                                                        <td class="auto-style1">Crew #</td>
                                                        <td>Name</td>
                                                        <td style="width :50px;text-align :center">Rank</td>
                                                        <%--<td style="width :30px;text-align:center;">FD</td>
                                                        <td style="width :30px;text-align:center;">TD</td>
                                                        <td style="width :30px;text-align:center;">OT (Hrs)</td>--%>
                                                    </tr>

                                                    <asp:Repeater runat="server" ID="rptPersonal">
                                                    <ItemTemplate>
                                                    <tr style='font-size:large ;background-color:<%#(Convert.ToString(Session["vPayrollID"])==Convert.ToString(Eval("payrollID")))?"Orange":(Eval("IsFinalWages").ToString()=="1")?"#FAAFBE":"#FFF"%>' >
                                                    <%--<tr style='font-size:10px;background-color :<%# (Eval("Verified").ToString()=="True")?"#C3FDB8":"#FAAFBE"%>'>--%>
                                                        <td>
                                                            <asp:ImageButton runat="server" ID="btnHourG" CausesValidation="false" OnClick="btnHourG_Click" CommandArgument='<%#Eval("Sno")%>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" />
                                                        </td>
                                                        <td style="width :50px;">
                                                            <a onclick="OpenModifyContract(<%#Eval("ContractID")%>)" style="cursor:pointer;">
                                                                <asp:Label ID="lblCrewNo" runat="server" Text='<%#Eval("CrewNumber")%>' ForeColor="Black"></asp:Label>
                                                                </a>
                                                            <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractID")%>' />
                                                            <asp:HiddenField ID="hfdFD" runat="server" Value='<%#Eval("StartDay")%>' />
                                                            <asp:HiddenField ID="hfdTD" runat="server" Value='<%#Eval("EndDay")%>' />
                                                            <asp:HiddenField ID="hfdOT" runat="server" Value='<%#Eval("ExtraOTdays")%>' />
                                                        </td>
                                                        <td>           
                                                            <div style="width :100%; height :25px;overflow :hidden;color:black;padding-left:5px;">
                                                            <%#Eval("CrewName")%>
                                                            </div>  
                                                        </td>
                                                        <td style="width :50px; text-align :center;height:25px;color:black;"><%#Eval("RankCode")%></td> 
                                                       <%-- <td style="width :30px; text-align :center"><%#Eval("StartDay")%>
                                                            <span style="color:Red"><%# ((Eval("StartDay").ToString()!=Eval("StartDay_O").ToString())?"*":"")%></span>
                                                        </td>
                                                        <td style="width :30px; text-align :center"><%#Eval("EndDay")%>
                                                            <span style="color:Red"><%# ((Eval("EndDay").ToString()!=Eval("EndDay_O").ToString())?"*":"")%></span>
                                                        </td>
                                                        <td style="width :30px; text-align :center"><%#Eval("ExtraOTdays")%></td>--%>
                                                    </tr>
                                                    </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                              <%-- </div>--%>
                                                
                                            </td>
                                            <td valign="top" style="text-align:center;">                                           
                                                <div id="div1" runat="server" visible="false">
          <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>
        
        <table cellpadding="2" cellspacing="2" border="0" width="100%" bordercolor="Gray" style="border-collapse:collapse; margin:auto;">
            <tr>
                <td align="center" style=" width: 100%;font-size:medium;font-family:Arial;" class="text headerband" ><b>View / Edit Portage Bill  - </b> <asp:Label ID="lblWagesForDuration" runat="server" style="padding-left:5px" Font-Names="Arial" Font-Size="Medium" Font-Bold="true"  ></asp:Label></td>
            </tr>
            <tr>
                <td >
                    <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                            <tr>
                                <td class="label" style="text-align:right;width:3.5%;padding-right:5px;">
                                    FD :</td>
                                <td style="width:5%;padding:3px 0px 3px 5px;text-align:left;">
                                    <asp:TextBox ID="txtFD" runat="server" CssClass="textlabel" Width="30px" AutoPostBack="true"  OnTextChanged="ReCalCulateWages" MaxLength="2" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat ="server" ErrorMessage="*" ControlToValidate="txtFD"></asp:RequiredFieldValidator>
                                </td>
                                <td class="label" style="text-align:right;width:3.5%;padding-right:5px;">TD :</td>
                                <td style="width:5%;padding:3px 0px 3px 5px;text-align:left;">
                                    <asp:TextBox ID="txtTD" runat="server" CssClass="textlabel" Width="30px" AutoPostBack="true"  OnTextChanged="ReCalCulateWages" MaxLength="2" Enabled="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ErrorMessage="*" ControlToValidate="txtTD"></asp:RequiredFieldValidator>
                                </td>
                                 <td class="label" style="text-align:right;width:12%;padding-right:5px;vertical-align:central;">Extra OT  <span> <asp:TextBox ID="txtExtraOT" runat="server" CssClass="textlabel"  Width="50px" AutoPostBack="true" OnTextChanged="ReCalCulateWages"  MaxLength="5" Enabled="false" ></asp:TextBox>&nbsp;  </span>  Hrs  <asp:RequiredFieldValidator runat ="server" ErrorMessage="*" ControlToValidate="txtExtraOT"></asp:RequiredFieldValidator>
                                      <asp:CompareValidator ID="txtExtraOT_Integer" runat="server" ValidationGroup="Insert"
                    ControlToValidate="txtExtraOT" Display="Dynamic" ErrorMessage="*"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="true" Type="Double" />
                                 </td>
                                <td style="padding:3px 0px 3px 5px;text-align:left;width:12%;">
                                 <span> OT Rate</span>    <asp:TextBox ID="txtExtrOTRate" runat="server" Width="50px" AutoPostBack="true"  ReadOnly="true" CssClass="textlabel"></asp:TextBox> &nbsp; <span> /- Hrs</span>
                                </td>
                                <td style="padding:3px 0px 3px 5px;text-align:left;width:13%;">
                                    <asp:Label ID="lblTotalTraveldays" runat="server" Text="Total Travel days : " ></asp:Label>  &nbsp; <asp:TextBox ID="txtTotalTraveldays" runat="server" Width="30px" AutoPostBack="true"  OnTextChanged="ReCalCulateWages" MaxLength="2" CssClass="textlabel" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat ="server" ErrorMessage="*" ControlToValidate="txtTotalTraveldays"></asp:RequiredFieldValidator>
                                </td>
                                <td style="padding:3px 0px 3px 5px;text-align:left;width:18%;">
                                    <div id="divTavelPay" runat="server" visible="false">
                                        <span> Travel Pay</span>    <asp:TextBox ID="txtTravelPay" runat="server" Width="80px" AutoPostBack="true"   OnTextChanged="ReCalCulateWages" MaxLength="5" CssClass="textlabel" Enabled="false"></asp:TextBox> &nbsp; <span> /- Day</span>
                                    </div>
                                 
                                </td>
                                 <td style="padding:3px 0px 3px 5px;text-align:left;vertical-align:central;">
                                    <asp:Button ID="btnModify" runat="server" CssClass="btn" OnClick="btnModify_Click" Text="Modify" Width="80px" Visible="false" CausesValidation="false" /> &nbsp; <asp:Button ID="btn_Save" runat="server" CssClass="btn"   Text="Save" Width="80px" OnClick="Save_Click" OnClientClick="return Check();" /> 
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn"   Width ="70px" OnClick="btnCancel_Click"/>
                                </td>
                            </tr>
                        
                    </table>
                </td>                
            </tr>
           
            
        </table>
            <table width="100%">
                <tr>
                    <td width="70%">
                         <div>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">  <asp:Label ID="lblContract" runat="server" Text="Earnings/Deductions (As per Contract)" Font-Names="Arial" Font-Size="Medium" Font-Bold="true" ForeColor="#206020"></asp:Label> &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:50%;padding-left:20px;vertical-align:top;padding:10px 2px 10px 10px;">
                            <div style="min-height:200px;height:450px; width :100%;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:12px;padding-top: 5px;" >
                <table width="99%"  style="border: 1px solid #4C7A6F; border-radius: 10px;" >
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblEarningMessage" runat="server" Text="Earnings" Font-Names="Arial" Font-Size="Medium" Font-Bold="true" ForeColor="#206020" Font-Underline="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID="lblEarningWages_Message" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        <asp:Repeater runat="server" ID="rptEaringWages" Visible="false" >
                    <HeaderTemplate>
                    <tr class= "headerstylegrid">
                     <th style="width:100px;"></th> 
                     <th style="width:200px;">Component Name</th>  
                     <th style="width:100px;">Amount (<asp:Label ID="lblEarnCurrency" runat="server"></asp:Label>)</th>
                    </tr>
                    </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td style="width:100px;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;vertical-align: central;display: <%#(Eval("IsFirstRow").ToString()=="true") ? "" : "none" %>;" rowspan="<%# Eval("CountOfCompnentType") %>"> 
                        <asp:Label runat="server" ID="lblComponentType" Text='<%#Eval("ComponentType")%>' CssClass="textlabel" Width='' style='text-align:center'  Font-Size="14px"></asp:Label>
                    </td>
                    <td style="width:200px;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> <asp:Label runat="server" ID="lblComponentName" Text='<%#Eval("ComponentName")%>' CssClass="textlabel" Width='' style='text-align:center'  Font-Size="14px"></asp:Label>
                    </td>
                    <td style="width:150px;text-align:center;border: 1px solid #4C7A6F;"><asp:TextBox runat="server" ID="txtComponentAmount" Text='<%#FormatCurr(Eval("WageScaleComponentvalue"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="14px" OnTextChanged="TxtComponentAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:HiddenField ID="hdnComponentId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' />
                        <asp:HiddenField ID="hdnComponentType" runat="server" Value='<%#Eval("ComponentType")%>' />
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                       
                    </td>
                    </tr>
                 </table>
                    </div>
               
                        </td>
                        <td style="width:50%;padding-left:20px;vertical-align:top;padding:10px 10px 10px 2px;">

                            <div style="min-height:200px;height:450px; width :100%;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:14px;padding-top: 5px;" >
                <table width="99%"  style="border: 1px solid #4C7A6F; border-radius: 10px;" >
                     <tr >
                        <td align="center" >
                            <asp:Label ID="lblDeduction" runat="server" Text="Deductions" Font-Names="Arial" Font-Size="Medium" Font-Bold="true" ForeColor="#206020" Font-Underline="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID="lblDeductionWage_Message" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label> 
                        <asp:Repeater runat="server" ID="rptDeductionWages" Visible="false">
                    <HeaderTemplate>
                    <tr class= "headerstylegrid">
                        <th style="width:100px;"></th>
                        <th style="width:200px;">Component Name</th>  
                        <th style="width:150px;">Amount (<asp:Label ID="lblDeductCurrency" runat="server"></asp:Label>) </th>
                    </tr>
                    </HeaderTemplate>
                <ItemTemplate>
                <tr>
                     <td style="width:100px;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;vertical-align: central;display: <%#(Eval("IsFirstRow").ToString()=="true") ? "" : "none" %>;" rowspan="<%# Eval("CountOfCompnentType") %>"> 
                        <asp:Label runat="server" ID="lblComponentType" Text='<%#Eval("ComponentType")%>' CssClass="textlabel" Width='' style='text-align:center'  Font-Size="14px"></asp:Label>
                    </td>
                    <td style="width:200px;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> <asp:Label runat="server" ID="lbldeductComponentName" Text='<%#Eval("ComponentName")%>' CssClass="textlabel" Width='' style='text-align:center'  Font-Size="14px"></asp:Label>
                    </td>
                    <td style="width:150px;text-align:center;border: 1px solid #4C7A6F;"><asp:TextBox runat="server" ID="txtdeductComponentAmount" Text='<%#FormatCurr(Eval("WageScaleComponentvalue"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="14px" OnTextChanged="TxtdeductComponentAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:HiddenField ID="hdndeductComponentId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' />
                        <asp:HiddenField ID="hdndeductComponentType" runat="server" Value='<%#Eval("ComponentType")%>' />
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                    </td>
                    </tr>
               </table>
                     </div>
                        </td>
                        
                    </tr>
                    <tr>
                <td  align="center" valign="top" >
                <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%" >
                      <tr id="trtravelPay" runat="server" visible="false">
                <td class="label" style="width :50%;text-align:left;padding-left:10px;">
                    Travel Pay Amount :&nbsp;
                </td>
                <td style="padding-right:10px;">
                    <asp:Label ID="lbltravelPayAmount" runat="server" Text="0.00"   style='text-align:right;float:right;' ReadOnly="true" Width="100px"></asp:Label>
                </td>
                
            </tr>
                     <tr>
                <td class="label" style="width :50%;text-align:left;padding-left:10px;">
                    Extra OT Amount :
                </td>
                <td style="padding-right:10px;">
                    <asp:Label ID="txtExtraOTAmount" runat="server" Text="0.00"   style='text-align:right;float:right;' ReadOnly="true" Width="100px"></asp:Label>
                </td>
               
            </tr>
                    <tr style="height:50px;">
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                             <asp:Button ID="btnPrint" runat="server" Text="Print Payslip" CssClass="btn" OnClientClick="javascript:OpenPaySlip();" Width ="100px"/>
                    
                     &nbsp;
                             <asp:Button ID="btnFinalWages" runat="server" Text="Final Wages" CssClass="btn" OnClientClick="javascript:OpenFinalWagesReport();" Width ="100px" />
                  <%--  <input type="button" value="Final Wages" class="btn" onclick='OpenFinalWagesReport()' Width ="100px"/>--%>
                        </td>
                    </tr>
                    </table>
                </td>
                        <td align="center" valign="top"> 
                            <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%" >
                     <tr>
                <td colspan="3" style="text-align:center; font-size:13px;font-weight:bold;">
                    WAGES SUMMARY 
                </td>                
                
            </tr>
                  
            <tr>
                <td class="label" style="width :50%;text-align:left;padding-left:10px;">
                    Earning this Month :
                </td>
                <td style="padding-right:10px;">
                    <asp:Label ID="lblWagesForValue" runat="server" style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="label" style="width :50%;text-align:left;padding-left:10px;">
                   BOW Previous Month <asp:Label ID="lblBowFromDuration" runat="server" ></asp:Label> :
                 </td>
                <td style="padding-right:10px;">
                    <asp:TextBox ID="txtBowFromValue" runat="server" CssClass="input_box" Visible="false" style="text-align:right; float:right;"></asp:TextBox>
                    <asp:Label ID="lblBowFromValue" runat="server" style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="imgUpdateBowFrom" runat="server" OnClick="imgUpdateBowFrom_OnClick"  ImageUrl="~/Modules/HRD/Images/icon_pencil.gif" CausesValidation="false"/>
                </td>
            </tr>
            <tr>
                <td class="label" style="width :50%;text-align:left;padding-left:10px;">Deductions this Month :</td>
                <td style="padding-right:10px;">
                    <asp:Label ID="TotDedThisMonth" runat="server" 
                        style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
                     <tr >
                         <td class="label" style="width :50%;text-align:left;padding-left:10px;">
                            <b> Total Wages Payable : </b> </td>
                         <td style="padding-right:10px;">
                             <asp:Label ID="TotWagesPayAbleOnBoard" runat="server" 
                                 style="text-align:right; float:right;font-weight:bold;"></asp:Label>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
            <tr>
                <td class="label" style="width :50%;text-align:left;padding-left:10px;">Closing Balance :</td>
                 <td style="text-align :right;padding-right:10px;">
                    <asp:Label ID="lblBalanceOfWages" runat="server" style="text-align:right; float:right;font-weight:bold;" ></asp:Label>
                </td>
                <td></td>
                
            </tr>
           
           
                </table>
                        </td>
            </tr>
                </table> 
            </div>
          
                    </td>
                    <td width="30%" style="padding:5px 5px 5px 5px; vertical-align:top;">
                        <table width="100%">
                            <tr style="height:40px;">
                            
                            <td colspan="2" style="text-align:left;padding-left:5px;">  <asp:Label ID="lblName" runat="server" style="padding:5px 5px 5px 0px;" Font-Names="Arial" Font-Size="Large" Font-Bold="true" ForeColor="#206020" ></asp:Label> &nbsp; (<asp:Label ID="lblCrewNo" runat="server" Font-Names="Arial" Font-Size="Large" Font-Bold="true" ></asp:Label>) 
                            </td>
                            </tr>
                            <tr style="height:25px;">
                                <td colspan="2">
                                    <asp:Label ID="lblrank" runat="server" style="float:left; padding:2px 5px 5px 5px;" Font-Names="Arial" Font-Size="Medium" Font-Bold="true" ForeColor="#206020"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px;">
                                <td colspan="2" style="text-align:left;padding-left:5px;">  <asp:Label ID="lblVessel" runat="server" Font-Names="Arial" Font-Size="Small"   Text="Vessel Name" ></asp:Label></td>
                                
                            </tr>
                             <tr style="height:30px;">
                                <td colspan="2" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblVesselName" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"></asp:Label></td>
                                
                            </tr>
                            <tr style="height:20px;">
                                <td width="50%" style="text-align:left;padding-left:5px;">  <asp:Label ID="lblContractStart" runat="server" Font-Names="Arial" Font-Size="Small"   Text="Start of Contract" ></asp:Label></td>
                                <td width="50%" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblContractEnd" runat="server" Font-Names="Arial" Font-Size="Small"   Text="End of Contract" ></asp:Label></td>
                            </tr>
                             <tr style="height:30px;">
                                <td width="50%" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblContractStartDt" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"   ></asp:Label></td>
                                <td width="50%" style="text-align:left;padding-left:5px;"><asp:Label ID="lblContractEndDt" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"     ></asp:Label></td>
                            </tr>
                             <tr style="height:20px;">
                                <td width="50%" style="text-align:left;padding-left:5px;">  <asp:Label ID="lblSignOn" runat="server" Font-Names="Arial" Font-Size="Small"   Text="Embarkation Date" ></asp:Label></td>
                                <td width="50%" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblSignOff" runat="server" Font-Names="Arial" Font-Size="Small"   Text="Disembarkation Date" ></asp:Label></td>
                            </tr>
                             <tr style="height:30px;">
                                <td width="50%" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblSignOnDt" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"   ></asp:Label></td>
                                <td width="50%" style="text-align:left;padding-left:5px;"><asp:Label ID="lblSignOffDt" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"     ></asp:Label></td>
                            </tr>
                             <tr style="height:20px;">
                                <td width="50%" style="text-align:left;padding-left:5px;">  <asp:Label ID="lblSignOnPt" runat="server" Font-Names="Arial" Font-Size="Small"   Text="Embarkation Port" ></asp:Label></td>
                                <td width="50%" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblSignOffPt" runat="server" Font-Names="Arial" Font-Size="Small"   Text="Disembarkation Port" ></asp:Label></td>
                            </tr>
                             <tr style="height:30px;">
                                <td width="50%" style="text-align:left;padding-left:5px;"> <asp:Label ID="lblSignOnPort" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"   ></asp:Label></td>
                                <td width="50%" style="text-align:left;padding-left:5px;"><asp:Label ID="lblSignOffPort" runat="server" Font-Names="Arial" Font-Size="Medium"  Font-Bold="true"     ></asp:Label></td>
                            </tr>
                        </table>
                        <br />
                        <div style="height:205px;vertical-align:top;">
<table cellpadding="2" cellspacing="0" border="1" style=" border-collapse:collapse; text-align :right; " width="100%">
                                <tr style="text-align :center;height :25px;  " >
                                    <td style="width :100px;"><b>DOCUMENTS </b></td>
                                    <td style="text-align:right;padding-right:5px;">
                                        <asp:ImageButton ID="btnAddDoc" runat="server" ImageUrl="../Images/add_16.gif" style='cursor:pointer;' OnClick="btnAddDoc_Click" OnClientClick="aspnetForm.target ='_blank';"/>
                                        
                                        </td>
                                </tr>
                                
                                 <tr>
        <td colspan="2"> 
            <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                                 
                               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                                   <colgroup>
                                      
                                       <col />
                                       <col width="100px" />
                                       <tr class="headerstylegrid" style="font-weight:bold;">
                                          
                                           <td>File Name</td>
                                           <td>Attachment</td>
                                       </tr>
                                       <asp:Repeater ID="rptDocuments" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                  
                                                   <td style="text-align:left;padding-left:5px;"><%#Eval("DocumentName")%>
                                                       <asp:HiddenField ID="hfTableID" runat="server" Value='<%#Eval("TableID")%> ' />
                                                   </td>
                                                   <td><a onclick='OpenDocument(<%#Eval("TableID")%>)' style="cursor:pointer;">
                                                       <img src="../Images/paperclip12.gif" />
                                                       </a></td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </colgroup>
                        </table>
                                     </div> 
        </td>
    </tr>
                                 
                            </table>
                        </div>
                        <div >
                <table cellpadding="3" cellspacing="0" border="1" bordercolor="Gray" style="border-collapse:collapse;" width="100%">
                     <tr>
                <td colspan="2" style="text-align:center; font-size:13px;font-weight:bold;">
                    VERIFICATION
                </td>                
                
                </tr>
            
            <tr>
                <td colspan="2" class="label" style="text-align:left">
                    <b>Remarks :</b> 
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="input_box" TextMode="MultiLine" Height="50px" Width="95%" style="text-align :left" ></asp:TextBox>
                </td>
            </tr>
            <tr id="trVerified" runat="server" visible="false">
                <td class="label">Verified By/On :</td>
                 <td style="text-align :left; padding-left:10px;">
                    <%--<asp:TextBox ID="txtVerifedBy" runat="server" CssClass="input_box" Width="97%" style="text-align :left" ></asp:TextBox>--%>
                    <asp:Label ID="lblVerifiedByOn" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>    
                <td colspan="2" style="padding :5px;">
                        <asp:Button ID="btnVerify" runat="server" Text="Verify" CssClass="btn" OnClick="btnVerify_OnClick" Width ="100px" CausesValidation="false"/>
                </td>
            </tr>
                </table>
            </div>
                    </td>
                </tr>
                

            </table>
            <%--<div class="stickyFooter" style='min-height:30px;font-family:Arial;font-size:12px;text-align:center;'>
            <table cellspacing="0" cellpadding="0" border ="0" width="100%" runat="server" id="tbl_Save" visible="false" >
            <tr>
                <td style=" text-align:center; width :200px; padding-right:5px;">
                   
                </td>
            </tr>
            </table>
            </div>--%>
            
             <asp:HiddenField ID="hdnCrewId" runat="server" />
            <asp:HiddenField ID="hdnOtherAmount" runat="server" />
             </ContentTemplate>
    </asp:UpdatePanel>
    </div>  
                                            </td>
                                        </tr>
                               
                                    </table>
               <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                             <%--</div>--%>
                </td>
                </tr>
                </table>
            </div>
            
        
    </form>
</body>
</html>
