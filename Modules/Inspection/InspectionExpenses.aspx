<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspectionExpenses.aspx.cs" Inherits="Transactions_InspectionExpenses" Title="Untitled Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <style type="text/css">
  .btns
  {
       background-color:#A3A3CC;
       padding:3px;
       padding-left:15px;
       padding-right:15px;
       border:solid 1px gray;
  }
  
  .btns_sel
  {
       background-color:#E0E0FF;
       padding:3px;
       padding-left:15px;
       padding-right:15px;
       border:solid 1px gray;
  }  
        .auto-style1 {
            margin-top: 0px;
        }
    </style>
    <style type="text/css">
        .btn
        {
            border: 1px solid #fe0034;
	font-family: arial;
	font-size: 12px;
	color: #fff;
	border-radius: 3px;
	-webkit-border-radius: 3px;
	-ms-border-radius: 3px;
	background: #fe0030;
	background: linear-gradient(#ff7c96, #fe0030);
	background: -webkit-linear-gradient(#ff7c96, #fe0030);
	background: -ms-linear-gradient(#ff7c96, #fe0030);
	padding: 4px 6px;
	cursor: pointer;
        }
        </style>
         </head>
<body  >
<form id="form1" runat="server" >
<div>
<script type="text/javascript" src="../js/CheckFormFlds.js"></script>
<script type="text/javascript">
month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
function checkDate(theField)
{
    dPart = theField.value.split("-");
    if(dPart.length!=3){
        alert("Enter Date in this format: dd-mmm-yyyy!");
        theField.focus();
        return false;
    }
    var check=0;
    for(i=0;i<month.length;i++){
    if(dPart[1].toLowerCase()==month[i].toLowerCase()){
     check=1;
      dPart[1]=i;
      break;
    }
  }
  if(check==0)
  {
  alert("Enter Date in this format: dd-mmm-yyyy!");
  return false;
  }
  nDate = new Date(dPart[2], dPart[1], dPart[0]);
 // nDate = new Date(dPart[0], dPart[1], dPart[2]);
  if(isNaN(nDate) || dPart[2]!=nDate.getFullYear() || dPart[1]!=nDate.getMonth() || dPart[0]!=nDate.getDate()){
    alert("Enter Date in this format: dd-mmm-yyyy!");
    theField.select();
    theField.focus();
    return false;
  } else {
    return true;
  }
}
 
function check()
{
        if(document.getElementById('txt_Date').value=='')
        {
            alert("Please Enter Date!");
            document.getElementById('txt_Date').focus();
            return false;
        }
        if(!checkDate(document.getElementById('txt_Date')))
        return false;
        if(document.getElementById("txt_Amt").value=="")
        {
            alert("Please Enter Amount!");
            document.getElementById("txt_Amt").focus();
            return false;
        }
        if(CheckFields("ctl00_ContentPlaceHolder1_txt_Amt","decimal",15,"Amount")==false) return false
        if(document.getElementById("txt_ExchngRate").value=="")
        {
            alert("Please Enter Exchange Rate!");
            document.getElementById("txt_ExchngRate").focus();
            return false;
        }
	    if(CheckFields("txt_ExchngRate","decimal",15,"Exchange Rate")==false) return false
//	    if(CheckFields("ctl00_ContentPlaceHolder1_txt_SubTotal","decimal",15,"SubTotal")==false) return false
//	    if(CheckFields("ctl00_ContentPlaceHolder1_txt_CashAdvnc","decimal",15,"Cdash in Advance")==false) return false
}		

</script>
<script language="javascript" type="text/javascript">
    function PrintExpRPT()
    {
        var InspDueId=document.getElementById('HiddenField_InspId').value;
        //alert(InspDueId);
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Reports\\InspExpense_Report.aspx?InspId='+ InspDueId,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
</script>
</div>
       <div style="font-family:Arial;font-size:12px;">
        <center>
             <div style="background-color:#206020; width:95% ; height:3px;">
            </div>
            <br />
<div style="height:23px;">
    <div style="float:left">
        <asp:Button runat="server" ID="btn_VBC" Text="Vetting Bonus Calculator" CausesValidation="false" OnClick="btn_VBC_Click" CssClass="btns"/>
        <asp:Button runat="server" ID="btn_OtherExp" Text="Other Expences" CausesValidation="false" OnClick="btn_OtherExp_Click" CssClass="btns_sel"/>
    </div>
    <asp:Label runat="server" ID="lblmessage" style="float:left; margin-left:10px;" Text="fsafsdf" ForeColor="Red" ></asp:Label>
</div>
<div style=" background-color:#A3A3CC; height:5px;"></div>
<div style="border:solid 1px gray;">
<asp:Panel id="pnlVBC" runat="server" Visible="true">
<div style="padding:5px; text-align:left">
<asp:Label runat="server" ID="lblEffDate"></asp:Label> | 
<asp:Button runat="server" ID="Button1" Text="Import Bonus" CssClass="btn" OnClick="btnUpdateScale_Click" />
</div>
<div style="border:solid 1px gray; overflow-x:hidden;overflow-y:scroll; height:44px; background-color:wheat;">
<table cellpadding="0" cellspacing="0" width='100%' style='border-collapse:collapse' border="1">
<tr>
<td colspan="3">&nbsp;<asp:Label runat="server" ID="lblRcount" Font-Bold="true" ForeColor="Red"></asp:Label></td>
<td style="width:60px;text-align:center"></td>
<td style="width:100px;text-align:center"></td>
<td style="width:100px;text-align:center"><asp:TextBox runat="server" ID="txtDedPer" Width="40px" CssClass="input_box" style="text-align:center" Text="0" AutoPostBack="true" OnTextChanged="txtDedPer_OnTextChanged"></asp:TextBox>(%) </td>
<td style="width:100px;text-align:center"><asp:Label runat="server" id="lblSuperPer" ></asp:Label>( % ) </td>
<td style="width:100px;text-align:center"></td>
<td style="text-align:center"></td>
</tr>
<tr class= "headerstylegrid">
<td style="width:40px; text-align:center">Sr#</td>
<td style="width:50px; text-align:center">Crew #</td>
<td style="width:300px;text-align:center">&nbsp;Crew Name</td>
<td style="width:60px;text-align:center">Rank</td>
<td style="width:100px;text-align:center">Bonus Scale($)</td>
<td style="width:100px;text-align:center">Ded. Amt($)</td>
<td style="width:100px;text-align:center">Suptd Ded.($)</td>
<td style="width:100px;text-align:center">Payable O/B($)</td>
<td style="text-align:center">Remarks</td>
</tr>
</table>
</div>
<div style="border:solid 1px gray; overflow-x:hidden;overflow-y:scroll; height:200px;">

<table cellpadding="0" cellspacing="0" width='100%' style='border-collapse:collapse' border="1">
<asp:Repeater runat="server" ID="RPT_BONUS">
<ItemTemplate>
<tr>
<td style="width:40px; text-align:center"><%#Eval("SNO")%>
<asp:HiddenField runat="server" ID="hdfCrewId" Value='<%#Eval("CrewId")%>'/>
<asp:HiddenField runat="server" ID="hfdRankId" Value='<%#Eval("NewRankId")%>' />
</td>
<td style="width:50px; text-align:center"><%#Eval("CREWNUMBER")%></td>
<td style="width:300px; text-align:left">&nbsp;<%#Eval("CREWNAME")%></td>
<td style="width:60px; text-align:center;">&nbsp;<%#Eval("RANKCODE")%></td>
<td style="width:100px; text-align:center;"><asp:TextBox Runat="server" id="txtAmount" Text='<%#Eval("BONUS")%>' CssClass="input_box" style='text-align:right; background-color:#e2e2e2;' width="80px" ReadOnly="true" ></asp:TextBox></td>
<td style="width:100px; text-align:center;"><asp:TextBox Runat="server" id="txtDed" Text='<%#Eval("DED")%>' CssClass="input_box" style='text-align:right' width="80px" Enabled='<%#(EnableDed>0)%>' AutoPostBack="true" OnTextChanged="txtDedPer_OnTextChanged2"></asp:TextBox></td>
<td style="width:100px; text-align:center;"><asp:TextBox Runat="server" id="txtSuptdAmt" Text='<%#Eval("SUPTD")%>' CssClass="input_box" style='text-align:right' width="80px" ReadOnly="true" ></asp:TextBox></td>
<td style="width:100px; text-align:center;"><asp:TextBox Runat="server" id="txtPayable" Text='<%#Eval("PAYABLE")%>' CssClass="input_box" style='text-align:right' width="80px" ReadOnly="true"></asp:TextBox></td>
<td style="text-align:center;"><asp:TextBox Runat="server" id="txtRemarks" Text='<%#Eval("REMARKS")%>' CssClass="input_box" style='text-align:left' width="95%" Enabled='<%#(EnableDed>0)%>'></asp:TextBox></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
<div>
<table cellpadding="2" cellspacing="1" width="100%" style="background-color:Wheat; border:solid 1px white;">
    <tr>
    <td style="text-align:center">
        Suptd. Amount ($) 
    </td>
    <td style="text-align:center">
        Total Payable Amount ($)  
    </td>
    <td style="text-align:center">
        Observations Count 
    </td>
    <td style="text-align:center">
        SHELL Score 
    </td>
    <td style="text-align:center">
        Observation from same SIRE Chapter (> 2)
    </td>
    <td style="text-align:center">
        Repeat observation from last 2 SIRE 
    </td>
   <td style="text-align:center">
   &nbsp;
    </td>
    </tr>
    <tr style=" background-color:#FFF5EB">
        <td style="text-align:center">
            <asp:TextBox ID="txtSuptdAmt" runat="server" CssClass="input_box" Width="70px" style="text-align:right"></asp:TextBox>
        </td>
        <td style="text-align:center">
            <asp:TextBox ID="txtTotalPayable" runat="server" CssClass="input_box" Width="70px" style="text-align:right"></asp:TextBox>
        </td>
        <td style="text-align:center" runat="server" id="tdInspCount">
            <asp:Label ID="lblInspCount" runat="server" ForeColor="White"></asp:Label>
        </td>
        <td style="text-align:center" runat="server" id="tdShellScore">
            <asp:Label ID="lblShellScore" runat="server" ForeColor="White"></asp:Label>
        </td>
        <td style="text-align:center" runat="server" id="td_SameSire">
            <asp:Label ID="lbl_SameSire" runat="server" ForeColor="White"></asp:Label>
        </td>
        <td style="text-align:center" runat="server" id="td_NoRepeat">
            <asp:Label ID="lbl_NoRepeat" runat="server" ForeColor="White"></asp:Label>
        </td>
        <td style="text-align:center; width:70px">
            <asp:Button ID="btnSaveBonus" runat="server" CssClass="btn" OnClick="btnSavebonus_Click" Text="Save" Width="60px" Visible="false" style="padding:3px" />
        </td>
    </tr>
</table>
</div>
<div style="padding:3px; background-color:Wheat; margin-top:3px; text-align:center" >
    <b>Change History</b>
    <asp:Button runat="server" ID="btnReview" CssClass="btn" OnClick="btnReview_Click" Text="Review" Visible="false" style="background-color:#D6EBFF"/>
    <asp:Button runat="server" ID="btnApprove" Text="Approval" CssClass="btn" OnClick="btnApproval_Click" Visible="false"  style="background-color:#99CCFF" />
    <asp:Button runat="server" ID="btnPrint" Text="Print" CssClass="btn" OnClick="btnPrint_Click" Visible="false" style="background-color:#99CCFF" />
    <asp:Button runat="server" ID="btnApprove2" Text="Approval2" CssClass="btn" OnClick="btnApproval2_Click" Visible="false" style="background-color:#6B8FB2;color:White;"/>
    <div runat="server" id="dvHistory" style=" float:right; width:300px; height:20px; text-align:center;">
        <asp:LinkButton runat="server" ID="lblreason" OnClick="lblreason_click" Font-Bold="true" ></asp:LinkButton>
    </div>
</div>
<div style="border:solid 1px gray; overflow-x:hidden;overflow-y:scroll; height:90px;">
<table cellpadding="1" cellspacing="0" width="100%">
<asp:Repeater runat="server" ID="rptData">
<ItemTemplate>
<tr>
<td style="text-align:center; width:30px"><%#Eval("Sno")%>.</td>
<td style="text-align:left">&nbsp;<b style='color:Blue'>
<a href="#" onclick='window.open("../Reports/InspExpenseReport.aspx?InspId=<%#Eval("inspectionid")%>&MaxMode=<%#Eval("Mode")%>");'><%#Eval("Action")%></a>

</b> done by <b  style='color:green'><%#Eval("ModifiedBy")%></b> on <b style='color:Red'><%#Common.ToDateString(Eval("CreatedOn"))%></b> </td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
  <!-- Section -->    
    <div style="position:absolute;top:0px;left:0px; height :520px; width:100%;" id="dvAccountBox" runat="server" visible="false" >
    <center>
        <div style="position:absolute;top:0px;left:0px; height :520px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:500px; height:300px;padding :3px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px gray;">
         <center >
         <div style="height:270px; text-align:left; padding:5px;">
         <span runat="server" id="spn_G" visible="false">
         <b>GREEN – AUTO CLEARANCE. Straight to Marine manager for payment approval.</b>
         <hr />
            <ol>
             <li>Zero observations OR</li>
             <li>1-5 observations AND SHELL score <15 AND</li>
             <li>No repeat observation from last 2 Sires on the same ship AND</li>
             <li><2 Observations from the same Sire Chapter. </li>
             </ol>
        </span>

        <span runat="server" id="spn_O" visible="false">
         <b>Orange Clearance – Review by Marine Super & forwarded to Marine manager for approval.</b>
         <hr />
            <ol>
             <li>1-5 observations AND SHELL score >15 <35 OR</li>
             <li>Any observation repeated from last 2 Sires on the same ship OR</li>
             <li>>2 Observations from the same Sire Chapter. </li>
             </ol>
        </span>

        <span runat="server" id="spn_R" visible="false">
         <b>Red Clearance – Additional clearance by Vetting Manager needed.</b>
         <hr />
            <ol>
             <li>5 observations AND SHELL score >35 </li>
             </ol>
        </span>

         </div>
         <asp:Button runat="server" ID="btnClose" OnClick="btnClose_Click" Text="Close" style="background-color:Red;color:White; padding:3px; width:100px; border:none;" />
         </center>
         </div>
    </center>
    </div> 
</asp:Panel>
<asp:Panel id="pnlOtherExp" runat="server" Visible="false">
    <div style="width:100% ;height:240px; vertical-align:top; margin-top:0px;" >
    <div style="border:solid 1px gray;width: 98%; text-align: left; padding-left: 5px; padding-right: 5px;height:250px; overflow-y:scroll;overflow-x:hidden">
        <asp:GridView ID="gvOtherExp" runat="server" DataKeyNames="TableId" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="100%" OnRowDeleting="gvOtherExp_RowDeleting" OnRowEditing="gvOtherExp_RowEditing" PageSize="9" AllowPaging="True" OnPageIndexChanging="gvOtherExp_PageIndexChanging" OnDataBound="gvOtherExp_DataBound" CssClass="auto-style1">
        <RowStyle CssClass="rowstyle" />
        <Columns>
                <asp:BoundField HeaderText="Sr.#" DataField="Sno">
                    <ItemStyle HorizontalAlign="center" Width="40px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Descr" HeaderText="Description">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CostHead" HeaderText="Cost Head">
                    <ItemStyle HorizontalAlign="Left" ></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Amt" HeaderText="Amt (USD)" DataFormatString="{0:0.00}" HtmlEncode="false">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CurrencyName" HeaderText="Currency">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ExchRates" HeaderText="Ex. Rates">
                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Attach">
                    <ItemTemplate>
                        <a target="_blank" href='<%# GetPath(Eval("FileName").ToString()).ToString() %>' style='display: <%# FileExists(Eval("FileName").ToString())%>; cursor: hand'><img src="../Modules/HRD/Images/paperclip.gif" border="0" /></a>
                    </ItemTemplate>
                    <ItemStyle Width="35px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton  ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit" />                                                                      
                    </ItemTemplate>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />                                                                
                    </ItemTemplate>
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        <pagerstyle horizontalalign="Center" />
        <SelectedRowStyle CssClass="selectedtowstyle" />
        <HeaderStyle CssClass="headerstylefixedheadergrid" />
        </asp:GridView>
    </div>
    <div style="border:solid 1px gray;width: 98%;padding-left: 5px; padding-right: 5px;">
    <strong> Total : </strong><asp:Label runat="server" ID="lblSum"></asp:Label> USD
    &nbsp;&nbsp;&nbsp;&nbsp;
    <strong>Grand Total (including expences) :</strong> <asp:Label runat="server" ID="lblGTotal"></asp:Label> USD
    </div>
    </div>
    <asp:Panel ID="pnlOtherExEdit" runat="server" style="padding-top:10px;">
    <table width="98%" style="border:solid 1px Gray" >
    <tr>
    <td style="text-align :right ">Descr. :</td>
    <td rowspan="4"><asp:TextBox runat="server" ID="txtDesc" CssClass="required_box" TextMode="MultiLine" Height="80px" Width="100%"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesc" ErrorMessage="Required."></asp:RequiredFieldValidator>
    </td>
    <td style="text-align :right ">Cost Head</td>
    <td><asp:TextBox runat="server" ID="txtCostHead" Width="200px" MaxLength="40" CssClass="input_box"></asp:TextBox> </td>
    </tr>
    <tr>
    <td>&nbsp;</td>
    <td style="text-align :right ">Amount(USD) :</td>
    <td><asp:TextBox runat="server" ID="txtAmt" Width="50px" MaxLength="10" CssClass="required_box"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmt" ErrorMessage="Required."></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td>&nbsp;</td>
    <td style="text-align :right ">Currency & Exch Rates :</td>
    <td><asp:DropDownList runat="server" ID="ddlCurrency2" CssClass="required_box"></asp:DropDownList> 
    <asp:TextBox CssClass="required_box" runat="server" ID="txtExRates" Width="50px" MaxLength="10"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExRates" ErrorMessage="Required."></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td>&nbsp;</td>
    <td style="text-align :right ">Upload File :</td>
    <td><asp:FileUpload Width="200px" runat="server" ID="flp1"/> </td>
    </tr>
    </table>
    </asp:Panel>
    <table width="100%">
    <tr>
    <td style="text-align:right">
    <asp:Label ID="lblMess2" runat="server" ForeColor="#C00000" style="float:left"></asp:Label>
    
    <asp:Button id="btnAdd" CausesValidation="false" onclick="btnAdd_Click" runat="server" CssClass="btn" Width="59px" Text="Add"></asp:Button>
    <asp:Button id="btnSave2" onclick="btnSave2_Click" runat="server" CssClass="btn" Width="59px" Text="Save"></asp:Button>
    <asp:Button id="btnCancel" CausesValidation="false" onclick="btnCancel_Click" runat="server" CssClass="btn" Width="59px" Text="Cancel"></asp:Button>
    </td>
    </tr>
    <tr>
    <td>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom" TargetControlID="txtAmt" ValidChars=".0123456789"></ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom" TargetControlID="txtExRates" ValidChars=".0123456789"></ajaxToolkit:FilteredTextBoxExtender>
    
    </td>
    </tr>
    </table>
</asp:Panel>
</div>
            </center></div>
 </form>
</body>
</html>
