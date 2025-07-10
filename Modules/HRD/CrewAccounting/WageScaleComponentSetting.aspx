<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WageScaleComponentSetting.aspx.cs" Inherits="Modules_HRD_CrewAccounting_WageScaleComponentSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Wage Master</title>
    <%--<link href="../styles/mystyle.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
      <link href="../Styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
   <%-- <style type="text/css">
         tblborder table, th, td {
            border: 1px solid #4371a5;
         }
      </style>--%>
        <style type="text/css">
            .auto-style1 {
                height: 380px;
                width: 340px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <script language="javascript" type="text/javascript">

            function CheckUnit(txtname) {
                var txt = txtname;

                if (isNaN(document.getElementById(txtname.id).value)) {
                    alert("Please enter numbers only");
                    document.getElementById(txtname.id).value = "0.00";
                    document.getElementById(txtname.id).focus();
                    return false;
                }

                else if (parseFloat(document.getElementById(txtname.id).value) < 0) {
                    alert("Value should be greater than or equal to zero");
                    document.getElementById(txtname.id).value = "0.00";
                    document.getElementById(txtname.id).focus();
                    return false;
                }
                else {
                    document.getElementById(txtname.id).value = roundNumber(document.getElementById(txtname.id).value, 2);
                }
            }
            function roundNumber(num, dec) {
                var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
                return result;


            } 
            function ShowHistory(ob) {
                var WSId = document.getElementById('<%=dp_WSname.ClientID%>').value;
                var Sen = document.getElementById('<%=txt_Seniority.ClientID%>').value;
    ob.href="WageScalePopupCopy.aspx?Mode=History&WCId=" + WSId + "&Sen=" + Sen + "&Nat=0";
}
function ShowCopy(ob)
{
    var WSId=document.getElementById('<%=dp_WSname.ClientID%>').value;
    var Sen=document.getElementById('<%=txt_Seniority.ClientID%>').value;
                ob.href = "WageScalePopupCopy.aspx?Mode=Copy&WCId=" + WSId + "&Sen=" + Sen + "&Nat=0";
            }
        </script>
   <div class="stickyHeader" style="height:70px">
<%-- <div class="text headerband">Crew Accounting - Wage Master</div>--%>
 <div style="padding:5px;font-family:Arial;font-size:12px;">
        <table width="100%" cellpadding="3" cellspacing="0" >
        <tr>
            <td style=" text-align: right; width:80px;">Wage Scale :</td>
            <td style="text-align: left; width:265px;"><asp:DropDownList ID="dp_WSname" runat="server" ValidationGroup="vg" CssClass="ctltext" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="dp_WSname_SelectedIndexChanged"></asp:DropDownList> &nbsp;
                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="dp_WSname" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
            </td>
           
            <td style=" text-align: right; width:100px;">
                Rank Group : &nbsp;
            </td>
            <td style="text-align: left; width:100px;">
                 <asp:DropDownList ID="ddl_RankGroup" runat="server" CssClass="ctltext" Width="95px" AutoPostBack="True" OnSelectedIndexChanged="ddl_RankGroup_SelectedIndexChanged">
                                       <asp:ListItem Value="A">All</asp:ListItem>
                                       <asp:ListItem Value="O">Officer</asp:ListItem>
                                       <asp:ListItem Value="R">Rating</asp:ListItem>
                                   </asp:DropDownList>
            </td>
            <td style="text-align: right; width:100px;">Seniority(Year) :&nbsp;</td>
            <td style="text-align: left; width:60px; ">
                <asp:TextBox ID="txt_Seniority" runat="server" ValidationGroup="vg" CssClass="ctltext" MaxLength="2" Width="45px" AutoPostBack="True" OnTextChanged="txt_Seniority_TextChanged" ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txt_Seniority"></ajaxToolkit:FilteredTextBoxExtender>
                 <asp:RequiredFieldValidator ID="rq1" runat="server" ErrorMessage="Required." ControlToValidate="txt_Seniority" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right;width:105px;">Effective From :&nbsp;</td>
            <td style="text-align: left;width:90px;"> <asp:Label runat="server" ID="lblEffectiveFrom"></asp:Label> </td>
            <td style="text-align: right;width:70px;"> Currency : &nbsp;
            </td>
            <td style="text-align: left;width:70px;">
                <asp:Label runat="server" ID="lblWageScaleCurrency"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btn_Show" runat="server" CssClass="btn" OnClick="btn_Show_Click" Text="Show" Width="80px" />
                <asp:Button ID="btnCBARevisition" runat="server" CssClass="btn"  Text="Update CBA" Width="100px" Enabled="false" OnClick="btnCBARevisition_Click"  />
            </td>
        </tr>
            <tr >
                <td colspan="11" align="center"> 
                    <br />
                    <asp:Label ID="lblMessage" runat="server"  ForeColor="Red" Font-Size="18px"  Font-Names="Arial"></asp:Label>
                </td>
            </tr>
        </table>
     </div>
</div>
<div style="height:110px">&nbsp;</div>
<div style='padding-bottom:50px;padding-left:50px; font-family:Arial;font-size:12px;'>
    <table width="95%" height="500px;" >
        <tr>
            <td style="width:260px;vertical-align:top;padding-right:10px;">
                <div style="overflow-x:hidden; overflow-y :auto;height:32px; border:solid 1px gray;" >
                <table  cellspacing="0" border="0" style="border-collapse: collapse;height: 30px; color: black; width: 280px;" class="bordered">
                <colgroup >
                <col width="150px;" /> 
                <col width="80px;" />           
                </colgroup>
                <tr class="headerstylegrid"  style="font-family: Arial, sans-serif;	font-weight:bold;">
                <td scope="col" style=" text-align:left ">Rank Name</td>
                <td scope="col" style=" text-align:left ">Status</td>             
                </tr>
                </table>
                </div>
            </td>
            <td colspan="2" class="text headerband" style="padding-right:20px;">
                Rank Name : <b> <asp:Label ID="lblRankheader" runat="server" ></asp:Label> </b>
                <div style="float:right;padding-right:5px;">
                    <asp:Button ID="btnModify" runat="server" CssClass="btn" OnClick="btnModify_Click" Text="Modify" Width="100px" Visible="false" /> &nbsp;
                </div>
            </td>
            <td style="width:200px;vertical-align:top;padding-right:10px;" class="text headerband">
                Effective Dates
            </td>
        </tr>
        <tr >
            <td style="width:260px;vertical-align:top;padding-right:10px;"> 
                <div style="min-height:200px;height:380px; width :280px;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:12px;" >
                    <table width="280px;" cellpadding="0" cellspacing="0" style="border-collapse:collapse;padding-right:20px;"  >
                       <%-- <tr>
                        <td  colspan="2">
                            
                        </td>
                        </tr>--%>
                        <colgroup >
                                <col width="150px;" /> 
                                <col width="80px;" /> 
                            </colgroup>
                        <tr><td>
                        <asp:Repeater runat="server" ID="rptRank" OnItemCommand="rptRank_ItemCommand" OnItemDataBound="rptRank_ItemDataBound">
                      <%--  <HeaderTemplate>
                        <tr class= "headerstylegrid">
                        <th style="width:180px;text-align:left;">Rank Name</th>  
                        </tr>
                        </HeaderTemplate>--%>
                        <ItemTemplate>
                        <tr id="row" runat="server" >
                        <td style="width:150px;text-align:left;border: 1px solid #4371a5;">
                            <asp:LinkButton ID="lnkRank" runat="server" CommandArgument='<%# Eval("RankId") %>' Text='<%#Eval("RankName")%>' ForeColor="DarkGreen" font-size="14px" Font-Names="Arial"  >
                            </asp:LinkButton>
                            <asp:Label ID="lblRankCode" runat="server" Visible="false" Text='<%#Eval("RankCode")%>'></asp:Label>
                        </td>
                            <td style="width:80px;text-align:center;border: 1px solid #4371a5;">
                                <asp:Image ID="Imgbtn" runat="server" ImageUrl="~/images/success.png" Visible="false" Height="30px" />
                                 <asp:Label ID="lblWageTotal" runat="server" Visible="false" Text='<%#Eval("Total")%>'></asp:Label>
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </td>
                        </tr>
                   </table>
                    </div>
                <br />
            </td>
            <td style="width:350px;padding-left:20px;vertical-align:top;">
                <div style="min-height:200px;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:12px;padding-top: 5px;" class="auto-style1" >
                <table width="340px"  style="border: 1px solid #4C7A6F; border-radius: 10px;" >
                    <tr>
                        <td class="text headerband" colspan="2">
                            <asp:Label ID="lblEarningMessage" runat="server" Text="Earnings Monthly"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                         <asp:Label ID="lblEarningWages_Message" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Repeater runat="server" ID="rptEaringWages" Visible="false" >
                    <HeaderTemplate>
                    <tr class= "headerstylegrid">
                     <th style="width:200px;">Component Name</th>  
                     <th style="width:100px;">Amount (<asp:Label ID="lblEarnCurrency" runat="server" Width="30px"></asp:Label>) </th>
                    </tr>
                    </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td style="width:200px;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> <asp:Label runat="server" ID="lblComponentName" Text='<%#Eval("ComponentName")%>' CssClass="textlabel" Width='' style='text-align:center'  Font-Size="12px"></asp:Label>
                    </td>
                    <td style="width:150px;text-align:center;border: 1px solid #4C7A6F;"><asp:TextBox runat="server" ID="txtComponentAmount" Text='<%#FormatCurr(Eval("WageScaleComponentvalue"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="12px" OnTextChanged="TxtComponentAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                <div style="font-size:14px;font-family:Arial;" id="divEarnings" runat="server" visible="false">
                    Total (Earnings - Deductions) : <asp:Textbox ID="txtTotalEarnings" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> <asp:Label ID="lblTotalEarnCurrency" runat="server"></asp:Label>
                </div>
               
            </td>
            <td style="width:350px;padding-left:20px;vertical-align:top;">
                 <div style="min-height:200px;height:380px; width :390px;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:12px;padding-top: 5px;" >
                <table width="340px"  style="border: 1px solid #4C7A6F; border-radius: 10px;" >
                     <tr >
                        <td class="text headerband" colspan="2">
                            <asp:Label ID="lblDeduction" runat="server" Text="Deductions Monthly"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID="lblDeductionWage_Message" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label> <br />
                        <asp:Repeater runat="server" ID="rptDeductionWages" Visible="false">
                    <HeaderTemplate>
                    <tr class= "headerstylegrid">
                        <th style="width:200px;">Component Name</th>  
                        <th style="width:150px;">Amount (<asp:Label ID="lblDeductCurrency" runat="server" Width="30px"></asp:Label>) </th>
                    </tr>
                    </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td style="width:200px;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> <asp:Label runat="server" ID="lbldeductComponentName" Text='<%#Eval("ComponentName")%>' CssClass="textlabel" Width='' style='text-align:center'  Font-Size="12px"></asp:Label>
                    </td>
                    <td style="width:150px;text-align:center;border: 1px solid #4C7A6F;"><asp:TextBox runat="server" ID="txtdeductComponentAmount" Text='<%#FormatCurr(Eval("WageScaleComponentvalue"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="12px" OnTextChanged="TxtdeductComponentAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                <div style="font-size:14px;font-family:Arial;" id="div1" runat="server" visible="false">
                    Extra OT Rate (Hourly) : <asp:Textbox ID="txtExtraOtRate" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ></asp:Textbox> <asp:Label ID="lblExtraOtRateCurrency" runat="server"></asp:Label>
                </div>
            </td>
             <td style="width:200px;vertical-align:top;padding-right:10px;">
            <asp:Repeater runat="server" id="rpt_History">
            <ItemTemplate >
            <div style=" padding:4px; text-align:left;">
                <asp:LinkButton ID="lbEffectiveDts" runat="server" Text='<%# Eval("wefdate")%>' CommandArgument='<%# Eval("WAGESCALERANKID")%>' CausesValidation="false" OnClick="lbEffectiveDts_Click"></asp:LinkButton>
            <%--<input type="radio" name='his' value='<%# Eval("WAGESCALERANKID")%>' /> &nbsp;--%>  

            </div>
            </ItemTemplate>
            </asp:Repeater>   
<%--<br />
<asp:Button ID="btnOpenHistory" runat="server" CssClass="btn" OnClick="btn_Open_Click" Text="Open History" Width="150px" Visible="false" />--%>
             </td>
        </tr>
        
    </table>
    
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="Txt_WEF"></ajaxToolkit:CalendarExtender>
     
    <asp:HiddenField ID="hdnRankCode" runat="server" />
</div>

        <div ID="dv_CBARevisition" runat="server" class="modal" visible="false">
<center>
<div class="modalbackground"></div>
<div class="modalContentFrame" style="width:300px;">
<div style='padding:5px;font-family:Arial;font-size:12px;'>
<h3>Effective From: </h3>
 <table cellspacing="0" cellpadding="0" border ="0" width="100%"  >
<tr>

<td style=" text-align:center;" >
<asp:TextBox ID="txtCBAEffDt" runat="server" CssClass="ctltext" Width="100px"></asp:TextBox>
<asp:ImageButton ID="imgCBAEffDt" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vg1" runat="server" ErrorMessage="*" ControlToValidate="txtCBAEffDt" Display="Static"></asp:RequiredFieldValidator>
     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtCBAEffDt"></ajaxToolkit:CalendarExtender>
</td>

</tr>
</table>
<br />

<asp:Button ID="btnCBA_save" runat="server" CssClass="btn" ValidationGroup="vg1"  Text="Save" Width="100px" OnClick="btnCBA_save_Click" /> &nbsp;&nbsp;
<asp:Button ID="btnCBA_Close" runat="server" CssClass="btn" OnClick="btnCBA_Close_Click" Text="Close" Width="100px" />
</div>        
    
</div>
</center>
</div>
<%--<div ID="dv_History" runat="server" class="modal" visible="false">
<center>
<div class="modalbackground"></div>
<div class="modalContentFrame" style="width:300px;">
<div style='padding:5px;font-family:Arial;font-size:12px;'>
<h3>Effective Dates </h3>
<asp:Repeater runat="server" id="rpt_History">
<ItemTemplate >
    <div style=" padding:4px; text-align:left;"><input type="radio" name='his' value='<%# Eval("WAGESCALERANKID")%>' /> &nbsp; <%# Eval("wefdate")%> </div>
</ItemTemplate>
</asp:Repeater>   
<br />
<asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="btn_Open_Click" Text="Open History" Width="150px" />
<asp:Button ID="btn_CloseHistory" runat="server" CssClass="btnred" OnClick="btn_CloseHistory_Click" Text="Close" Width="100px" />
</div>        
    
</div>
</center>
</div>--%>
<div class="stickyFooter" style='min-height:30px;font-family:Arial;font-size:12px;'>
<table cellspacing="0" cellpadding="0" border ="0" width="100%" >
<tr>
<td style="text-align:center">
    
     <%-- <asp:Button ID="btn_Show_History" runat="server" OnClick="btn_ShowHistory_Click" CssClass="btn" CausesValidation="false" Text="Show History" Width="120px" Visible="false" /> --%>         
</td>
<td style="width:450px">
<table cellspacing="0" cellpadding="0" border ="0" width="100%" runat="server" id="tbl_Save" visible="false" >
<tr>
<td style=" text-align:right; width: 150px;" ><asp:HiddenField ID="hwef" runat="server" /><asp:HiddenField ID="hfwsid" runat="server" />Effective From: &nbsp;</td>
<td style=" text-align:left;width :140px;" >
<asp:TextBox ID="Txt_WEF" runat="server" CssClass="ctltext" Width="100px" ></asp:TextBox>
<asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  />
</td>
<td style=" text-align:left; width :30px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vg" runat="server" ErrorMessage="*" ControlToValidate="Txt_WEF" Display="Static"></asp:RequiredFieldValidator></td>
<td style=" text-align:center; width :125px; padding-right:5px;"><asp:Button ID="btn_Save" runat="server" CssClass="btn" ValidationGroup="vg"  Text="Save" Width="120px" OnClick="Save_Click" />   </td>
</tr>
</table>
</td>
</tr>
</table>
</div>
    </form>
</body>
</html>
