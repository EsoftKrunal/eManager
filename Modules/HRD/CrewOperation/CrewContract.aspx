<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewContract.aspx.cs" Inherits="CrewAccounting_CrewContract" Title="Crew Contract Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title></title>
 <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
                function Show_Amount()
                {
                    var Amt = 0.0;
                    var deduct = 0.0;
                var temp=0;
                var ob=null;
                    var CtlList = new Array("02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20");
                for(i=0;i<=18;i++)
                {
                    ob=document.getElementById("Gd_AssignWages_ctl" + CtlList[i] + "_txtAmount");  
                    if(ob!=null)
                    {
                        if(ob.getAttribute('Wid')!="12")
                        {
                            temp=parseFloat(ob.value); 
                            if(! isNaN(temp))
                            { 
                                Amt=Amt+parseFloat(temp);  
                            }
                        }
                    }
                    }

                    for (i = 0; i <= 18; i++) {
                        ob = document.getElementById("Gd_AssingWagesDeduction_ctl" + CtlList[i] + "_txtAmount");
                        if (ob != null) {
                            if (ob.getAttribute('Wid') != "12") {
                                temp = parseFloat(ob.value);
                                if (!isNaN(temp)) {
                                    deduct = deduct + parseFloat(temp);
                                }
                            }
                        }
                    }
                
                var Gross='lb_Gross';
                var Ded='lb_deduction';
                var Net ='lb_NewEarning';
                var Txt='txt_Other_Amount';
                var totalEarning = 'lblTotalEarning';
                var totalDeduction = 'lblTotalDeduction';
                var Gross_Val=Amt;//"0" + document.getElementById(Gross).innerHTML;
                var Ded_Val = deduct;
                    var Amount = "0" + document.getElementById(Txt).value;
                    var netAmount = "0";
                  //  alert(Ded_Val);
                if(isNaN(Gross_Val))
                {Gross_Val="0";}
                if(isNaN(Ded_Val))
                {Ded_Val="0";}
                if(isNaN(Amount)) 
                {Amount="0";}
                    var earning = parseFloat(parseFloat(Gross_Val) + parseFloat(Amount)).toFixed(2);
                   
                    totalDeduction = parseFloat(Ded_Val).toFixed(2);
                    netAmount = parseFloat(earning).toFixed(2) - parseFloat(Ded_Val).toFixed(2);
                    netAmount = Math.round(netAmount * 100) / 100;
                   
                    if (isNaN(netAmount)) { netAmount ="0";}
                    document.getElementById(Net).innerHTML = parseFloat(netAmount).toFixed(2);
                    document.getElementById(totalEarning).innerHTML = parseFloat(earning).toFixed(2);
                    document.getElementById(totalDeduction).innerHTML = parseFloat(Ded_Val).toFixed(2);
                }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
            <td style=" text-align:center" class="text headerband" >
            <strong>Crew Contract</strong> <span lang="en-us">- </span>
                         <asp:Label ID="lbl_RefNo" runat="server"></asp:Label>
            </td>
         </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" border="1" >
<tr>
    <td colspan="3">
    <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
    </td>
</tr>
    <tr>
        <td style="vertical-align:top; width:50%">
             <div style="color:Black;width:100%; background-color:#c2c2c2; font-size:13px; padding-bottom:4px;"><b>Crew Details</b></div>
             <table cellpadding="3" cellspacing="0" width="96%">
                 <tr>
                     <td align="right" style="padding-right: 10px;  text-align: right; width: 166px;">Crew Name :</td>
                     <td align="left" style="width: 299px;"><asp:Label ID="lb_name" runat="server" Width="100%" Font-Bold="true"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Planned Vessel :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                     <asp:Label ID="lb_PlanVessel" runat="server" Width="100%"></asp:Label></td>
                 </tr>
                 <tr>
                     <td align="right" style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Planned Rank :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                     <asp:Label ID="Lb_PlanRank" runat="server" Width="100%"></asp:Label></td>
                 </tr>
                 <tr>
                     <td align="right" style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Nationality :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                    <asp:DropDownList ID="dp_nationality" runat="server" CssClass="required_box" 
                             Width="200px">
                    </asp:DropDownList>
                         <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dp_nationality" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                 </tr>
                 <tr>
                     <td align="right" style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Wage Scale :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                    <asp:DropDownList ID="dp_wagescale" runat="server" CssClass="required_box" 
                             ValidationGroup="abc" Width="200px">
                    </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dp_wagescale"
                             Display="Dynamic" ErrorMessage="Required." ValidationGroup="abc"></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Contract Period :</td>
                     <td align="left" style="width: 299px">
                    <asp:TextBox ID="Txt_ContractPeriod" runat="server" 
                             CssClass="required_box"                       Width="88px" MaxLength="2"></asp:TextBox>
                         (Month)
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_ContractPeriod"
                             Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Seniority :</td>
                     <td align="left" style="width: 299px">
                    <asp:TextBox ID="Txt_Seniority" runat="server" CssClass="required_box" MaxLength="2" 
                             Width="88px" ValidationGroup="abc"></asp:TextBox>
                         (Year)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Seniority"
                        Display="Dynamic" ErrorMessage="Required." ValidationGroup="abc"></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Issue Date :</td>
                     <td align="left" style="width: 299px">
                         <asp:TextBox ID="txt_issuedt" runat="server" CssClass="required_box" MaxLength="15"
                        Width="88px"></asp:TextBox>
                         <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_issuedt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_issuedt" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Start Date :</td>
                     <td align="left" style="width: 299px">
                         <asp:TextBox ID="txt_startdt" runat="server" CssClass="required_box" MaxLength="15"
                        Width="88px"></asp:TextBox>
                         <asp:ImageButton ID="img2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_startdt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_startdt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                        </td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 8px; text-align: right;" 
                         valign="top">
                         Rank :</td>
                     <td align="left" style="width: 299px; height: 8px">
                         <asp:DropDownList ID="dp_Rank" runat="server" CssClass="required_box" 
                             ValidationGroup="abc" Width="140px">
                         </asp:DropDownList>
                         <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dp_Rank"
                             ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"
                             ValidationGroup="abc"></asp:RangeValidator>
                     </td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 8px; text-align: right;" 
                         valign="top">
                         &nbsp;</td>
                     <td align="left" style="width: 299px; height: 8px">
              <asp:Button ID="btn_ShowWages" runat="server" Text=" Show Wages >> " Width="130px" OnClick="btn_ShowWages_Click" ValidationGroup="abc" CssClass="btn"  />
             <asp:Button ID="btn_cancel" runat="server" CssClass="btn" Text="Cancel" Width="102px" Visible="False" />
                     </td>
                 </tr>
                                                   
                 </table>
            
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                 FilterType="Custom" ValidChars="0123456789." FilterMode="ValidChars" TargetControlID="txt_Other_Amount">
             </ajaxToolkit:FilteredTextBoxExtender>
          </td>
       
        <td style="vertical-align:top">
             <div style="color:Black;width:100%; background-color:#c2c2c2; font-size:13px; padding-bottom:4px;"><b>Wages Details</b>
                    &nbsp;</div>
             <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 300px; text-align: left;">
                 <table width="100%">
                     <tr>
                         <td width="50%" style="text-align:center;">
                            <b> Earnings </b>
                         </td>
                         <td width="50%" style="text-align:center;">
                            <b> Deductions </b>
                         </td>
                     </tr>
                     <tr style="vertical-align:top;">
                         <td>
                             <asp:GridView ID="Gd_AssignWages"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                Width="100%">
                 <Columns>
                   <asp:TemplateField HeaderText="Component Type" Visible="false" ItemStyle-Width="200px">
                   <ItemTemplate>
                     <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' /> 
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left"  />               
                   <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right">
                   <ItemTemplate >   
                    <asp:TextBox runat="server" onchange="Show_Amount(); " Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" style="text-align :right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>' ></asp:TextBox>
                    <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' /> 
                   </ItemTemplate>
                   </asp:TemplateField> 
                    <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20px">
                   <ItemTemplate >   
                    &nbsp;
                   </ItemTemplate>
                   </asp:TemplateField> 
                   <%--<asp:BoundField DataField="ComponentType" HeaderText="Component Type" ItemStyle-HorizontalAlign="Center"/>            --%>
                </Columns>
                <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
                         </td>
                         <td>
                             <asp:GridView ID="Gd_AssingWagesDeduction"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                Width="100%">
                 <Columns>
                   <asp:TemplateField HeaderText="Component Type" Visible="false" ItemStyle-Width="200px">
                   <ItemTemplate>
                     <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' /> 
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left"  />               
                   <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right">
                   <ItemTemplate >   
                    <asp:TextBox runat="server" onchange="Show_Amount(); " Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" style="text-align :right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>' ></asp:TextBox>
                    <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' /> 
                   </ItemTemplate>
                   </asp:TemplateField> 
                    <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20px">
                   <ItemTemplate >   
                    &nbsp;
                   </ItemTemplate>
                   </asp:TemplateField> 
                   <%--<asp:BoundField DataField="ComponentType" HeaderText="Component Type" ItemStyle-HorizontalAlign="Center"/>            --%>
                </Columns>
                <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
                         </td>
                     </tr>
                 </table>
               
        </div>
            <br /><br />
             <table width="100%" cellpadding="2" cellspacing="0" style="text-align : center; border-collapse:collapse" bordercolor="Gray" border="1">
                    <tr>
                        <td style="text-align: left;width:230px; ">EXTRA ALLOW (M) :</td>
                        <td style="text-align: right; padding-right :17px; " colspan="3"><b><asp:TextBox ID="txt_Other_Amount" style="text-align :right " onkeyup="Show_Amount();" CssClass="input_box" width="80px" runat="server"></asp:TextBox></b></td>
                         
                    </tr>
                     <tr>
                         <td style="text-align: left;width:230px; "> Total Earning : </td>
                         <td style="text-align: left; padding-left :5px; "> <asp:Label ID="lblTotalEarning" runat="server" ForeColor="Green" Width="80px" Font-Bold="true"></asp:Label> </td>
                          <td style="text-align: left;width:150px; "> Total Deduction : </td>
                         <td style="text-align: left; padding-left :5px; "> <asp:Label ID="lblTotalDeduction" runat="server" ForeColor="Green" Width="80px" Font-Bold="true"></asp:Label> </td>
                     </tr>
                    <tr>
                        <td style="text-align: left;"><b>Net Earning (Total Earning - Total Deduction)<span lang="en-us"> </span></b></td>
                        <td style="text-align: left; padding-left :5px;" colspan="3"><asp:Label Font-Bold="true" ID="lb_NewEarning" ForeColor="Green" runat="server" Width="80px"></asp:Label></td>
                    </tr>
                 <tr>
                        <td style="text-align: left;width:230px; ">EXTRA OT Rate (Hourly) :</td>
                        <td style="text-align: right; padding-right :17px; "><b><asp:TextBox ID="txtExtraOtRate" style="text-align :right "  CssClass="input_box" width="80px" runat="server"></asp:TextBox></b></td>
                         <td style="text-align: left;width:150px; "> Travel Pay (Per Day) :</td>
                                    <td style="text-align: left; "> <asp:DropDownList ID="ddlTravelPay" runat="server" Width="150px">
                                        <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem  Text="Full Salary Basis" Value="1"></asp:ListItem>
                                        <asp:ListItem  Text="Basic Wages Basis" Value="2"></asp:ListItem>
                                        <%--<asp:ListItem  Text="Custom (Wages per day)" Value="3"></asp:ListItem>--%>
                                                                     </asp:DropDownList></td>
                    </tr>
                    </table><b>Remarks</b>
                    <asp:TextBox ID="txt_Remark" runat="server" CssClass="input_box" TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>
          </td>
        </tr>
         <tr>
             <td align="right" 
                 style =" background-color :#e2e2e2; text-align :right ; padding:2px;" 
                 colspan="3"  >
                 <div style =" display :none;">
                         <asp:Label ID="lb_Gross" ForeColor="Green" runat="server" Width="122px"></asp:Label>
                         <asp:Label ID="lb_deduction" ForeColor="Red" runat="server" Width="122px"></asp:Label>
                         </div>
             <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save Contract" Width="130px" OnClick="btn_Save_Click" />&nbsp;
             <asp:Button ID="btn_cancelContract" runat="server" CssClass="btn" Text="Cancel Contract" Width="130px" OnClick="btn_cancelContract_Click" OnClientClick="javascript:return confirm('Are You Sure to Cancel this Contract?')" />&nbsp;
             <asp:Button ID="btn_ContLetter" runat="server" CssClass="btn" Text="Print Contract" Width="110px" OnClick="btn_ContLetter_Click" />&nbsp;
             </td>
         </tr>
        <%-- <tr>
         <td colspan="3" style=" text-align:center" 
                 class="text headerband" >
         <strong>Pervious Contracts</strong>
         </td>
         </tr>
         <tr>
            <td colspan="3" style=" padding:2px;" >
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                 <td>
                     <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 150px; text-align: left;">
                         <asp:GridView ID="gv_Contract" OnSelectedIndexChanged="GridView_SelectIndexChanged"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%">
                             <Columns>
                               <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
                                   <ItemStyle Width="30px" />
                               </asp:CommandField>
                               <asp:TemplateField HeaderText="Ref.#" >
                               <ItemTemplate>
                               <asp:Label ID="lbl_Contractid" runat="server" Text='<%# Eval("RefNo") %>'></asp:Label>
                               <asp:HiddenField ID="hfd_ContractId" runat="server" Value='<%# Eval("ContractId") %>'></asp:HiddenField>
                               </ItemTemplate>
                                   <ItemStyle Width="70px" />
                               </asp:TemplateField>
                                 <asp:BoundField DataField="Name" HeaderText="Name" />
                                 <asp:BoundField DataField="RankCode" HeaderText="Planned Rank" />
                                  <asp:BoundField DataField="NewRankCode" HeaderText="Rank" />
                                  <asp:BoundField DataField="VesselName" HeaderText="Vessel" />
                                 <asp:BoundField DataField="IssueDate" HeaderText="IssueDate" />
                                 <asp:BoundField DataField="StartDate" HeaderText="StartDate" />
                                 <asp:BoundField DataField="CountryName" HeaderText="Nationality" />
                             </Columns>
                             <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                             <SelectedRowStyle CssClass="selectedtowstyle" />
                             <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                         </asp:GridView>
                     </div>
                 </td>
             </tr>
         </table>
            </td>
         </tr>--%>
         </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_issuedt">
                </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                    PopupButtonID="img2" PopupPosition="TopRight" TargetControlID="txt_startdt">
                </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="f2" runat="server" FilterType="Numbers" TargetControlID="Txt_Seniority"></ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="Fl1" runat="server" FilterType="Numbers" TargetControlID="Txt_ContractPeriod"></ajaxToolkit:FilteredTextBoxExtender>
    <script language="javascript"  type="text/javascript">
        Show_Amount();
    </script>
    </form>
</body>
</html>
