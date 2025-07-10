<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WageScaleMasterNew.aspx.cs" Inherits="CrewOperation_WageScaleMasterNew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Crew Accounting - Wage Master</title>
    <%--<link href="../styles/mystyle.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
      <link href="../styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</head>
<body>
<form id="form1" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<script language="javascript" type="text/javascript">
    
    function CheckUnit(txtname) 
       { 
       var txt=txtname;
       
       if(isNaN(document.getElementById(txtname.id).value))
                  {   
                    alert("Please enter numbers only");
                    document.getElementById(txtname.id).value="0.00"  ;
                    document.getElementById(txtname.id).focus();
                    return false;  
                  }
                  
         else if(parseFloat(document.getElementById(txtname.id).value)<0)
         {
                alert("Value should be greater than or equal to zero");
                document.getElementById(txtname.id).value="0.00"  ;
                document.getElementById(txtname.id).focus();
                return false;
         }
         else
         {        
              document.getElementById(txtname.id).value=roundNumber(document.getElementById(txtname.id).value,2);    
         }
         }         
      function roundNumber(num, dec) {
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
	
	
}   
function ShowHistory(ob)
{
    var WSId=document.getElementById('<%=dp_WSname.ClientID%>').value;
    var Sen=document.getElementById('<%=txt_Seniority.ClientID%>').value;
    ob.href="WageScalePopupCopy.aspx?Mode=History&WCId=" + WSId + "&Sen=" + Sen + "&Nat=0";
}
function ShowCopy(ob)
{
    var WSId=document.getElementById('<%=dp_WSname.ClientID%>').value;
    var Sen=document.getElementById('<%=txt_Seniority.ClientID%>').value;
    ob.href="WageScalePopupCopy.aspx?Mode=Copy&WCId=" + WSId + "&Sen=" + Sen + "&Nat=0";
}         
</script>
<div class="stickyHeader" style="height:105px">
 <div class="text headerband">Crew Accounting - Wage Master</div>
 <div style="padding:5px;font-family:Arial;font-size:12px;">
        <table width="100%" cellpadding="3" cellspacing="0" >
        <tr>
            <td style=" text-align: right; width:120px;">Wage Scale :&nbsp;</td>
            <td style="text-align: left; width:250px;"><asp:DropDownList ID="dp_WSname" runat="server" ValidationGroup="vg" CssClass="ctltext" Width="241px"></asp:DropDownList></td>
            <td style="width:120px;">
                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="dp_WSname" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            <td style="text-align: right; width:120px;">Seniority(Year) :&nbsp;</td>
            <td style="text-align: left;; width:60px; ">
                <asp:TextBox ID="txt_Seniority" runat="server" ValidationGroup="vg" CssClass="ctltext" MaxLength="2" Width="45px" ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txt_Seniority"></ajaxToolkit:FilteredTextBoxExtender>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rq1" runat="server" ErrorMessage="Required." ControlToValidate="txt_Seniority" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right;width:120px;">Effective From :&nbsp;</td>
            <td style="text-align: left;width:100px;"> <asp:Label runat="server" ID="lblEffectiveFrom"></asp:Label> </td>
            <td>
                <asp:Button ID="btn_Show" runat="server" CssClass="btn" OnClick="btn_Show_Click" Text="Show" Width="100px" />
                <asp:Button ID="btnModify" runat="server" CssClass="btn" OnClick="btnModify_Click" Text="Modify" Width="100px" Visible="false" />
                <asp:Button ID="btn_Show_History" runat="server" OnClick="btn_ShowHistory_Click" CssClass="btn" CausesValidation="false" Text="Show History" Width="120px" Visible="false" />
            </td>
        </tr>
        </table>
     </div>
 <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse;font-family:Arial;font-size:12px;" >
    <tr class= "headerstylegrid">
    <th>Rank Code</th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox1" Text="&nbsp;" /><asp:Label runat="server" ID="Label1" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox2" Text="&nbsp;" /><asp:Label runat="server" ID="Label2" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox3" Text="&nbsp;" /><asp:Label runat="server" ID="Label3" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox4" Text="&nbsp;" /><asp:Label runat="server" ID="Label4" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox5" Text="&nbsp;" /><asp:Label runat="server" ID="Label5" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox6" Text="&nbsp;" /><asp:Label runat="server" ID="Label6" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox7" Text="&nbsp;" /><asp:Label runat="server" ID="Label7" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox8" Text="&nbsp;" /><asp:Label runat="server" ID="Label8" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox9" Text="&nbsp;" /><asp:Label runat="server" ID="Label9" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox10" Text="&nbsp;" /><asp:Label runat="server" ID="Label10" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox11" Text="&nbsp;" /><asp:Label runat="server" ID="Label11" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:CheckBox runat="server" ID="CheckBox12" Text="&nbsp;" /><asp:Label runat="server" ID="Label12" Font-Size="11px"></asp:Label></th>    
    </tr>
    </table>
</div>
<div style='padding-bottom:50px;font-family:Arial;font-size:12px;'>
<div style="height:110px">&nbsp;</div>
    <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse" class="grid">
    <tr>
    <asp:Repeater runat="server" ID="rptWages">
    <ItemTemplate>
    <tr>
        <td><asp:Label runat="server" ID="lblRankCode" Text='<%#Eval("RankCode")%>'></asp:Label> </td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC1" Text='<%#FormatCurr(Eval("C1"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC2" Text='<%#FormatCurr(Eval("C2"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC3" Text='<%#FormatCurr(Eval("C3"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC4" Text='<%#FormatCurr(Eval("C4"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC5" Text='<%#FormatCurr(Eval("C5"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC6" Text='<%#FormatCurr(Eval("C6"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC7" Text='<%#FormatCurr(Eval("C7"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC8" Text='<%#FormatCurr(Eval("C8"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC9" Text='<%#FormatCurr(Eval("C9"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC10" Text='<%#FormatCurr(Eval("C10"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC11" Text='<%#FormatCurr(Eval("C11"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
        <td style='width:100px'><asp:TextBox runat="server" ID="txtC12" Text='<%#FormatCurr(Eval("C12"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="11px"></asp:TextBox></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
        </tr>
    </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="Txt_WEF"></ajaxToolkit:CalendarExtender>
</div>
<div ID="dv_History" runat="server" class="modal" visible="false">
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
</div>
<div class="stickyFooter" style='min-height:30px;font-family:Arial;font-size:12px;'>
<table cellspacing="0" cellpadding="0" border ="0" width="100%" >
<tr>
<td style="text-align:left">
    &nbsp;&nbsp;<asp:Label runat="server" ID="lb_msg" Font-Size="18px" ForeColor="Red"  ></asp:Label>
</td>
<td style="width:350px">
<table cellspacing="0" cellpadding="0" border ="0" width="100%" runat="server" id="tbl_Save" visible="false" >
<tr>
<td style=" text-align:right; width: 120px;" ><asp:HiddenField ID="hwef" runat="server" /><asp:HiddenField ID="hfwsid" runat="server" />W.e. From :&nbsp;</td>
<td style=" text-align:left;width :140px;" >
<asp:TextBox ID="Txt_WEF" runat="server" CssClass="ctltext" Width="100px"></asp:TextBox>
<asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
</td>
<td style=" text-align:left; width :30px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vg" runat="server" ErrorMessage="*" ControlToValidate="Txt_WEF" Display="Static"></asp:RequiredFieldValidator></td>
<td style=" text-align:center; width :125px; padding-right:5px;"><asp:Button ID="btn_Save" runat="server" CssClass="btn" ValidationGroup="vg" OnClick="Save_Click" Text="Save" Width="120px" /></td>
</tr>
</table>
</td>
</tr>
</table>
</div>
</form>
</body>
</html>
