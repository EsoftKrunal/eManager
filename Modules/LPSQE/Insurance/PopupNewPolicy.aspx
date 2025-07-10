<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupNewPolicy.aspx.cs" Inherits="InsuranceRecordManagement_PopupNewPolicy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    
    
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
    function fncReadOnly(evnt)
    {
      event.returnValue = false;
    }
    function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
    function refreshparent()
    {
        window.opener.reloadme();
    }
    </script>
    <style type="text/css">
        .style1
        {
            width: 40px;
        }
        .style2
        {
            width: 106px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
     <ajaxToolkit:ToolkitScriptManager  ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center; background-color: #f9f9f9">
            <tr>
                <td colspan="2" style="text-align: center; height: 23px"
                    class="text headerband">
                    <asp:Label runat="server" ID="lblPageName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="width:50%">
                                <table cellpadding="2" cellspacing="0" style="width: 100%;">
                                <col width="145px" />
                                    <tr>
                                        <td style="text-align: right;">
                                            INSC Group :
                                        </td>
                                        <td style="text-align: left; height: 5px">
                                            <asp:DropDownList ID="ddl_Groups" CssClass="input_box" Width="160px" required='yes'
                                                runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddl_Groups_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="0"
                                                ControlToValidate="ddl_Groups" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                          </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                               <legend style="font-weight:bold">Vessel Details</legend>
                               <table cellpadding="2" cellspacing="0" style="width: 100%;">
                                <col width="139px" />
                                  <tr>
                                      <td style="text-align: right;">
                                          Vessel :
                                      </td>
                                      <td style="text-align: left; height: 5px">
                                          <asp:DropDownList ID="ddl_Vessels" CssClass="input_box" Width="160px" runat="server" required='yes'
                                              AutoPostBack="True" OnSelectedIndexChanged="ddl_Vessels_SelectedIndexChanged">
                                          </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="0"
                                              ControlToValidate="ddl_Vessels" ErrorMessage="*"></asp:RequiredFieldValidator>
                                      </td>
                                  </tr>
                                  <%--<tr>
                                      <td style="text-align: right;">
                                          Material :
                                      </td>
                                      <td style="text-align: left; height: 5px">
                                          <asp:TextBox ID="txt_Material" CssClass="input_box" Width="155px" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue="0"
                                              ControlToValidate="txt_Material" ErrorMessage="*"></asp:RequiredFieldValidator>
                                      </td>
                                  </tr>--%>
                                  <tr>
                                      <td style="text-align: right;">
                                          Gross Tonnage :
                                      </td>
                                      <td style="text-align: left; height: 5px">
                                          <asp:Label ID="lbl_GrossTonnage"  runat="server"></asp:Label>
                                          
                                      </td>
                                  </tr>
                                  <tr>
                                      <td style="text-align: right;">
                                          Vessel Type :
                                      </td>
                                      <td style="text-align: left; height: 5px">
                                          <asp:Label ID="lbl_VesselType" runat="server"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td style="text-align: right;">
                                          Flag :
                                      </td>
                                      <td style="text-align: left; height: 5px">                                          
                                          <asp:Label ID="lbl_Flag" Width="155px" runat="server"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td style="text-align: right;">
                                          Year Built :</td>
                                      <td style="text-align: left; height: 5px">                                          
                                          <asp:Label ID="lbl_YearBuild" runat="server"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td style="text-align: right;">
                                          &nbsp;</td>
                                      <td style="text-align: left; height: 5px">
                                          &nbsp;</td>
                                  </tr>
                               </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                       <td>
                           <fieldset>
                               <legend style="font-weight:bold">Insurance Details</legend>
                               <table cellpadding="1" cellspacing="0" style="width: 100%;">
                                <col width="139px" />
                               <tr>
                                   <td style="text-align: right;">
                                       Arranged By :
                                   </td>
                                   <td style="text-align: left;">
                                       <asp:DropDownList ID="ddlArrangedBy" CssClass="input_box" Width="205px" 
                                           runat="server" 
                                           onselectedindexchanged="ddlArrangedBy_SelectedIndexChanged" 
                                           AutoPostBack="False">
                                           <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                           <asp:ListItem Text="Company" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="Owner" Value="2"></asp:ListItem>
                                       </asp:DropDownList>
                                       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue="0"
                                           ControlToValidate="ddl_UW" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                   </td>
                                   <td style="text-align: right;">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                    <td style="text-align: right;">
                                        Payment by Company :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlPaymentByMTM" runat="server" CssClass="input_box" Width="205px" >
                                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                               </tr>
                               <tr>
                                    <td style="text-align: right;">Mortgagee INT. :</td>
                                    <td style="text-align: left;">
                                        <asp:CheckBox ID="chkMortagee" runat="server" />
                                    </td>
                                    <td></td>
                                    <td></td>
                               </tr>
                               <tr>
                                   <td style="text-align: right;">
                                       Under Writer :
                                   </td>
                                   <td style="text-align: left;">
                                       <asp:DropDownList ID="ddl_UW" CssClass="input_box" required='yes' Width="205px" runat="server">
                                       </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="0"
                                           ControlToValidate="ddl_UW" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align: right;">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td style="text-align: right;">
                                       Broker :
                                   </td>
                                   <td colspan="3" style="text-align: left;">
                                       <asp:DropDownList ID="ddlBroker" CssClass="input_box" Width="70px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBroker_SelectedIndexChanged">
                                           <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                           <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                       </asp:DropDownList>&nbsp<asp:TextBox ID="txtBrokername" runat="server" 
                                           MaxLength="50" CssClass="input_box" Width="125px" Visible="false"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td style="text-align: right;">
                                           Place Issued :
                                       </td>
                                   <td style="text-align: left;">
                                           <asp:TextBox ID="txt_IssuedPlace" runat="server" MaxLength="25" CssClass="input_box"
                                               Width="200px"></asp:TextBox>
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                               ControlToValidate="txt_IssuedPlace" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                   </td>
                                   <td style="text-align: right;">
                                           &nbsp;</td>
                                   <td>
                                           &nbsp;</td>
                               </tr>
                                   <tr>
                                       <td style="text-align: right;">
                                           Policy # :
                                       </td>
                                       <td style="text-align: left;">
                                           <asp:TextBox ID="txt_PolicyNo" runat="server" MaxLength="20" CssClass="input_box" required='yes'
                                               Width="200px"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                               ControlToValidate="txt_PolicyNo" ErrorMessage="*"></asp:RequiredFieldValidator>    
                                       </td>
                                       <td style="text-align: right;">
                                           &nbsp;</td>
                                       <td style="text-align: left;">
                                           &nbsp;</td>
                                   </tr>
                                   <tr>
                                       <td style="text-align: right;">
                                           Date Issued :
                                       </td>
                                       <td style="text-align: left;">
                                           <asp:TextBox ID="txt_IssuedDt" runat="server" MaxLength="12" CssClass="input_box"
                                               Width="90px"></asp:TextBox>
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                               ControlToValidate="txt_IssuedDt" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                       </td>
                                       <td style="text-align: right;">
                                           &nbsp;</td>
                                       <td style="text-align: left;">
                                           &nbsp;</td>
                                   </tr>
                                   </table>
                                   
                                   <asp:UpdatePanel ID="up1" runat="server">
                                       <ContentTemplate>
                                            <table cellpadding="1" cellspacing="0" style="width: 100%;">
                                                <colgroup>
                                                    <col width="139px" />
                                                    <tr>
                                                        <td style="text-align:right;">Currency :</td>
                                                        <td style="text-align: left;">
                                                            <asp:DropDownList ID="ddlCurrInsuredValue" runat="server" CssClass="input_box" OnSelectedIndexChanged="ddlCurrInsuredValue_OnSelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCurrInsuredValue" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">Insured Value (LC) : </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txt_InsuredAmount" runat="server" CssClass="input_box" MaxLength="25" onkeypress="fncInputNumericValuesOnly(event)" OnTextChanged="txt_InsuredAmount_TextChanged" style="text-align:right;" Width="200px"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                 runat="server" ControlToValidate="txt_InsuredAmount" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">Insured Value (Us$) :</td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_InsuredAmountUSDoler" runat="server" CssClass="input_box" MaxLength="25" ReadOnly="false" style="text-align:right;" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                           </table>
                                       </ContentTemplate>
                                   </asp:UpdatePanel>
                                   
                                   <table cellpadding="1" cellspacing="0" style="width: 100%;">
                                   <col width="139px" />
                                   <tr>
                                       <td style="text-align: right;">
                                           Period of Insurance :
                                       </td>
                                       <td style="text-align: left" colspan="3">
                                           <asp:TextBox ID="txt_InceptionDt" runat="server" CssClass="input_box" Width="90px"></asp:TextBox>
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_InceptionDt"
                                               ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                           <asp:DropDownList ID="ddlFromHour" runat="server" CssClass="input_box" Width="40px">
                                           </asp:DropDownList>
                                           :
                                           <asp:DropDownList ID="ddlFromMin" runat="server" CssClass="input_box" Width="40px">
                                           </asp:DropDownList>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align: right;">
                                           &nbsp;</td>
                                       <td style="text-align: left" colspan="3">
                                           <asp:TextBox ID="txt_ExpiryDt" runat="server" CssClass="input_box" Width="90px" 
                                               AutoPostBack="True" OnTextChanged="txt_ExpiryDt_TextChanged"></asp:TextBox>
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ExpiryDt"
                                               ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                           <asp:DropDownList ID="ddlToHour" runat="server" CssClass="input_box" Width="40px">
                                           </asp:DropDownList>
                                           :
                                           <asp:DropDownList ID="ddlToMin" runat="server" CssClass="input_box" Width="40px">
                                           </asp:DropDownList>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align: right;" colspan="4">
                                           <asp:Label ID="lbl_InsurancePeriod" runat="server"></asp:Label>
                                       </td>
                                   </tr>
                                   <%--<tr>
                                       <td style="text-align: right;">
                                          Deductible (Us$) :
                                       </td>
                                       <td style="text-align: left" colspan="3">
                                          <asp:TextBox ID="txt_Deductible" runat="server" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)" CssClass="input_box" Width="158px" ></asp:TextBox>
                                       </td>
                                   </tr>--%>
                               </table>
                           </fieldset>
                       </td>
                    </tr>
                
                  
                    </table>
                </td>
                <td style="vertical-align:top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <fieldset>
                                <legend style="font-weight:bold">Premium Details</legend>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="2" cellspacing="0" style="width: 100%;">
                                            <colgroup>
                                                <col />
                                                <col />
                                                <col width="150px" />
                                                <col />
                                                <col />
                                                <col />
                                                <tr>
                                                    <td></td>
                                                    <td><%--<asp:DropDownList id="ddlCurrPremium" runat="server" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrPremium_OnSelectedIndexChanged" ></asp:DropDownList>--%></td>
                                                    <td style="text-align: right;">Rate : </td>
                                                    <td class="style2" style="text-align: left">
                                                        <asp:TextBox ID="txt_Rate" runat="server" AutoPostBack="True" CssClass="input_box" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)" ontextchanged="txt_Rate_TextChanged" style="text-align:right;" Width="90px"></asp:TextBox>
                                                        % <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_Rate" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                                                    <td></td>
                                                    <td><%--<asp:TextBox ID="txtRateUSDoller" runat="server" CssClass="input_box" ReadOnly="true" Width="90px" ></asp:TextBox>--%></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align: right;">Tot. Premium (LC) : </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txt_Premium" runat="server" AutoPostBack="True" CssClass="input_box" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)" ontextchanged="txt_Premium_TextChanged" ReadOnly="true" style="text-align:right;" Width="90px"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_Premium" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                                                    <td style="text-align: right;">Tot. Premium (Us$) :</td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtPremiumUSDoller" runat="server" CssClass="input_box" style="text-align:right;" Width="90px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align: right;">No. of Installments : </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtNoInstallment" runat="server" CssClass="input_box" MaxLength="2" onkeypress="fncInputNumericValuesOnly(event)" Width="90px"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoInstallment" ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">&nbsp; </td>
                                                    <td style="text-align: left;">&nbsp; </td>
                                                    <td style="text-align: left">&nbsp; </td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: right"></td>
                                                    <td style="text-align: left">
                                                        <asp:Button ID="btnShowSchedule" runat="server" CausesValidation="False" CssClass="btn" OnClick="btnShowSchedule_Click" Text="Show Schedule" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                                        border-collapse: collapse;">
                                                            <colgroup>
                                                                <col />
                                                                <col style="width: 160px;" />
                                                                <col style="width: 140px;" />
                                                                <col style="width: 100px;" />
                                                                <col style="width: 17px;" />
                                                                <tr align="center" class= "headerstylegrid">
                                                                    <td align="center">Installment Details </td>
                                                                    <td align="center">Premium Amount (LC) </td>
                                                                    <td align="center">Premium Amount (Us$) </td>
                                                                    <td align="center">Payble On </td>
                                                                    <td></td>
                                                                </tr>
                                                            </colgroup>
                                                        </table>
                                                        <div id="divSchedule" style="overflow-y: scroll; overflow-x: hidden; width: 100%;
                                                        height: 160px; text-align: center;">
                                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                                            border-collapse: collapse;">
                                                                <colgroup>
                                                                    <col />
                                                                    <col style="width: 160px;" />
                                                                    <col style="width: 140px;" />
                                                                    <col style="width: 100px;" />
                                                                    <col style="width: 17px;" />
                                                                </colgroup>
                                                                <asp:Repeater ID="rptInstallment" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class="row">
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtInstDetails" runat="server" CssClass="input_box" MaxLength="50" Text='<%#Eval("InstallmentDetails") %>' Width="350px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtAmountLC" runat="server" CssClass="input_box" MaxLength="50" OnTextChanged="txttxtAmountLC_OnTextChanged" style="text-align:right;" Text='<%#Eval("AmountLC") %>' Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="input_box" MaxLength="50" OnTextChanged="txtAmount_TextChanged" style="text-align:right;" Text='<%#Eval("Amount") %>' Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtInstDate" runat="server" CssClass="input_box" MaxLength="12" Text='<%#Eval("InstallmentDt") %>' Width="75px"></asp:TextBox>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtInstDate">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                            </td>
                                                                            <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <fieldset>
                                <legend style="font-weight:bold">Other Details</legend>
                                <table cellpadding="3" cellspacing="0" style="width: 100%;">
                                    
                                  <tr>
                                      <td style="text-align: Center;">
                                          Assured(S)/Co Assured
                                      </td>
                                       <td style="text-align: Center;">
                                           </td>
                                  </tr>
                                  <tr>
                                      <td style="text-align: left;">
                                          <asp:TextBox ID="txt_AssuredS" TextMode="MultiLine" runat="server" 
                                              CssClass="input_box" Width="300px" Height="130px"></asp:TextBox>
                                      </td>
                                      <td style="text-align: left; vertical-align:top;">
                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                              <ContentTemplate>
                                                  <table cellpadding="2" cellspacing="0" style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: right;"></td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ddlCurrDeductible" runat="server" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrDeductible_OnSelectedIndexChanged"  ></asp:DropDownList>--%>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                      <tr id="tr_Deductible" runat="server">
                                                          <td style="text-align: right;">
                                                              Deductible (LC):
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txt_Deductible" runat="server" style="text-align:right;" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)"
                                                                  CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                  ontextchanged="txt_Deductible_TextChanged"></asp:TextBox>
                                                          </td>
                                                          <td style="text-align: right;">Deductible (Us$):</td>                                                            
                                                        <td>
                                                            <asp:TextBox ID="txt_DeductibleDoller" runat="server" style="text-align:right;" CssClass="input_box" ReadOnly="true" ></asp:TextBox>
                                                        </td>
                                                      </tr>                                                      
                                                      <tr id="tr_HM" runat="server" visible="false">
                                                          <td colspan="4" style="text-align: left;">
                                                              <table cellpadding="4" cellspacing="0" style="width: 100%;">
                                                                  
                                                                  <tr>                                                                      
                                                                        <td style="text-align: right;">Hull(LC)</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtHalLC" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px"  
                                                                              ontextchanged="txtHalLC_TextChanged"></asp:TextBox>
                                                                        </td>
                                                                      <td style="text-align: right;">
                                                                          Hull (Us$):
                                                                      </td>
                                                                      <td>
                                                                          <asp:TextBox ID="txt_Hull" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                              ontextchanged="txt_Hull_TextChanged"></asp:TextBox>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td style="text-align: right;">
                                                                        Machinery(LC):
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMachineryLC" runat="server"  style="text-align:right;" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px"  
                                                                              ontextchanged="txtMachineryLC_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                      <td style="text-align: right;">
                                                                          Machinery (Us$):
                                                                      </td>
                                                                      <td>
                                                                          <asp:TextBox ID="txt_Machinery" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                              ontextchanged="txt_Machinery_TextChanged"></asp:TextBox>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td style="text-align: right;">HM (LC):</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHMLC" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px"  
                                                                              ontextchanged="txtHMLC_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                      <td style="text-align: right;">
                                                                          HM (Us$):
                                                                      </td>
                                                                      <td>
                                                                          <asp:TextBox ID="txt_HM" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                              ontextchanged="txt_HM_TextChanged"></asp:TextBox>
                                                                      </td>
                                                                  </tr>
                                                              </table>
                                                          </td>
                                                      </tr>
                                                      <tr id="tr_PNI" runat="server" visible="false">
                                                          <td colspan="4" style="text-align: left;">
                                                              <table cellpadding="4" cellspacing="0" style="width: 100%;">
                                                                  <tr>
                                                                    
                                                                        <td style="text-align: right;">Cargo (LC):</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCargoLC" runat="server" style="text-align:right;" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" 
                                                                              ontextchanged="txtCargoLC_TextChanged"></asp:TextBox>
                                                                        </td>
                                                                      <td style="text-align: right;">
                                                                          Cargo (Us$):
                                                                      </td>
                                                                      <td>
                                                                          <asp:TextBox ID="txt_Cargo" runat="server" style="text-align:right;" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                              ontextchanged="txt_Cargo_TextChanged"></asp:TextBox>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td style="text-align: right;">Crew (LC):</td>
                                                                     <td>
                                                                        <asp:TextBox ID="txtCrewLC" runat="server" style="text-align:right;" MaxLength="10" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" 
                                                                              ontextchanged="txtCrewLC_TextChanged"></asp:TextBox>
                                                                     </td>
                                                                      <td style="text-align: right;">
                                                                          Crew (Us$):
                                                                      </td>
                                                                      <td>
                                                                          <asp:TextBox ID="txt_Crew" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                              ontextchanged="txt_Crew_TextChanged"></asp:TextBox>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td style="text-align: right;">Others (LC):</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOthersLC" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" 
                                                                              ontextchanged="txtOthersLC_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                      <td style="text-align: right;">
                                                                          Others (Us$):
                                                                      </td>
                                                                      <td>
                                                                          <asp:TextBox ID="txt_Others" runat="server" MaxLength="10" style="text-align:right;" onkeypress="fncInputNumericValuesOnly(event)"
                                                                              CssClass="input_box" Width="100px" AutoPostBack="True" 
                                                                              ontextchanged="txt_Others_TextChanged"></asp:TextBox>
                                                                      </td>
                                                                  </tr>
                                                              </table>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td style="text-align: right;">
                                                              RDC :
                                                          </td>
                                                          <td>
                                                              <asp:DropDownList ID="ddlRDC" CssClass="input_box" Width="163px" runat="server">
                                                                  <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                                  <asp:ListItem Text="EXCL" Value="1"></asp:ListItem>
                                                                  <asp:ListItem Text="1/4" Value="2"></asp:ListItem>
                                                                  <asp:ListItem Text="4/4" Value="3"></asp:ListItem>
                                                              </asp:DropDownList>
                                                          </td>
                                                          <td></td>
                                                          <td></td>
                                                      </tr>
                                                  </table>
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                      
                                          <%--<asp:TextBox ID="txt_MatterInsured" TextMode="MultiLine" runat="server" 
                                              CssClass="input_box" Width="300px" Height="130px"></asp:TextBox>--%>
                                      </td>
                                  </tr>
                                  </table>
                            </fieldset>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="padding-right: 10px; text-align:center">
                            <div style="float: left; padding-left: 10px;">
                                <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                            </div>
                            <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" Width="107px" OnClick="btnSave_Click" Font-Bold="True" Height="27px" />
                            <input type="button" onclick="window.close();" class="btn" value="Close" 
                                style="width: 107px; height: 27px; font-weight: bold;" />
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            
            <%--<tr>
                <td style="padding-right: 10px; text-align: right">
                   Trading Warranty :</td>
                <td colspan="5" style="text-align: left; height:5px">
                <asp:TextBox ID="txt_TradWarranty" TextMode="MultiLine" runat="server" 
                        CssClass="input_box" Width="376px" Height="108px" ></asp:TextBox>
                </td>
            </tr>--%>
            
            
            <%--<tr>
                <td style="padding-right: 10px; text-align: right">
                  Conditions :</td>
                <td colspan="5" style="text-align: left; height:5px">
                <asp:TextBox ID="txt_Conditions" TextMode="MultiLine" runat="server" 
                        CssClass="input_box" Width="376px" Height="108px" ></asp:TextBox>
                </td>
            </tr>--%>
            
            <%--<tr>
                <td style="padding-right: 10px; text-align: right">
                   SubGroup :</td>
                <td colspan="5" style="text-align: left; height:5px">
                <asp:DropDownList ID="ddl_SubGroups" CssClass="input_box" Width="195px" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="0" ControlToValidate="ddl_SubGroups" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <%--<tr>
                <td colspan="2"  style="padding-right: 10px; text-align:right">
                    <div style="float:left; padding-left:10px;">
                      <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" Width="70px" 
                        onclick="btnSave_Click" />
                    <input type="button" onclick="window.close();" class="btn" value="Close" style="width: 70px" />
                </td>
            </tr>--%>
            <tr>
            <td colspan="2"><hr style="color:Black" /></td>
            </tr>
            <tr>
                        <td colspan="2">
                            <fieldset>
                                <legend style="font-weight: bold">Documents</legend>
                                 <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                     <tr>
                                         <td style="text-align:right">Document No :</td>
                                         <td style="text-align:left">
                                             <asp:TextBox ID="txt_DocNo" CssClass="input_box" MaxLength="50" Width="250px" runat="server"></asp:TextBox>
                                         </td>
                                         <td style="text-align:right">
                                                Document Name :
                                            </td>
                                         <td style="text-align:left">
                                             <asp:TextBox ID="txt_DocName" CssClass="input_box" MaxLength="50" Width="250px" runat="server"></asp:TextBox>
                                         </td>
                                         <td style="text-align:left">
                                             <asp:FileUpload ID="flAttachDocs" CssClass="input_box" Width="200px" runat="server" />
                                         </td>
                                         <td style="text-align:right">
                                            <asp:Button ID="btnSaveDoc" Text="Save Document" CssClass="btn" runat="server" 
                                                 CausesValidation="false" onclick="btnSaveDoc_Click" />
                                         </td>
                                     </tr>
                                 </table>
                                 <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width: 450px;" />
                                        <col />
                                        <col style="width: 100px;" />
                                        <col style="width: 50px;"/>
                                        <col style="width: 17px;" />
                                        <tr align="center" class= "headerstylegrid">                                            
                                            <td align="left">
                                                Document No.
                                            </td>
                                            <td align="left">
                                                Document Name
                                            </td>
                                            <td align="left">
                                                Attachment
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </colgroup>
                                </table>
                                
                                <div id="dvDocs" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 40px; text-align: center;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                        border-collapse: collapse;">
                                        <colgroup>
                                            <col style="width: 450px;" />
                                            <col />
                                            <col style="width: 100px;" />
                                            <col style="width: 50px;"/>
                                            <col style="width: 17px;" />
                                        </colgroup>
                                        <asp:Repeater ID="rptDocs" runat="server">
                                            <ItemTemplate>
                                                <tr class="row">
                                                    <td align="center">
                                                        <asp:Label ID="lbl_DocNo" Text='<%#Eval("DocNumber") %>' runat="server"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lbl_DocName" Text='<%#Eval("DocName") %>' runat="server"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                      <a runat="server" ID="ancdoc"  href='<%#"~/EMANAGERBLOB/LPSQE/Insurance\\" + Eval("FileName").ToString() %>' target="_blank"  title="Show Doc" >
                                                         <%--<a runat="server" ID="ancdoc"  href='<%#"~/InsuranceRecordManagement/PopupShowAttachment.aspx?DId=" + Eval("DocId").ToString() %>' target="_blank"  title="Show Doc" >--%>
                                                       <img src="../../HRD/Images/paperclipx12.png" style="border:none"  /></a>
                                                    </td>
                                                    <td align="right"> 
                                                       <asp:ImageButton ID="btnDeleteDoc" CommandArgument='<%#Eval("DocId") %>' Visible='<%# (Request.QueryString["Mode"].ToString() != "V")  %>' OnClick="btnDeleteDoc_Click" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete Doc" OnClientClick="javascript:return confirm('Are you sure to delete?');" runat="server" />
                                                    </td>
                                                    <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
        </table>
        
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_InceptionDt">
    </ajaxToolkit:CalendarExtender>
     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_ExpiryDt">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_IssuedDt">
    </ajaxToolkit:CalendarExtender>
    
    </div>
    </form>
</body>
</html>
