<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WageScaleMasterPopUp.aspx.cs" Inherits="CrewOperation_WageScaleMasterPopUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
    <script type="text/javascript" src="../Scripts/thickbox.js"></script>
    <link rel="stylesheet" href="../Styles/thickbox.css" type="text/css" media="screen" />
    <style type="text/css">
        .style2
        {
            width: 51px;
        }
        .style3
        {
            width: 63px;
        }
        .style4
        {
            width: 245px;
        }
        .style5
        {
            width: 226px;
        }
        .style6
        {
            width: 110px;
        }
    </style>
</head>
<body style="background-color: #f9f9f9">
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
    var Nat=document.getElementById('<%=dp_nationality.ClientID%>').value;
    ob.href="WageScalePopupCopy.aspx?Mode=History&WCId=" + WSId + "&Sen=" + Sen + "&Nat=" + Nat;
}
function ShowCopy(ob)
{
    var WSId=document.getElementById('<%=dp_WSname.ClientID%>').value;
    var Sen=document.getElementById('<%=txt_Seniority.ClientID%>').value;
    var Nat=document.getElementById('<%=dp_nationality.ClientID%>').value;
    ob.href="WageScalePopupCopy.aspx?Mode=Copy&WCId=" + WSId + "&Sen=" + Sen + "&Nat=" + Nat;
}         
</script>
 <div>
    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width:100%">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >Wage Master</td>
            </tr>
            <tr>
            <td style="width: 100%">
                <table cellspacing="0" width="100%"  style="background-color: #f9f9f9;" border="0">
                <tr>
                    <td style="text-align: center;"><asp:Label ID="lb_msg" runat="server" ForeColor="Red" Width="100%"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: center; padding-right: 10px; padding-left: 10px; padding-bottom: 10px;">
                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;padding-bottom:5px"><legend><strong>Wage Master</strong></legend>
                        <table width="100%" cellpadding="3" cellspacing="0" >
                            <tr>
                                <td style=" text-align: right;" class="style6">Wage Scale : </td>
                                <td style="text-align: left; " class="style4">
                                    <asp:DropDownList ID="dp_WSname" runat="server" ValidationGroup="vg" CssClass="required_box" Width="241px"></asp:DropDownList></td>
                                <td>
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="dp_WSname" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td style="text-align: right;">Nationality:</td>
                                <td style="text-align: left; " class="style5">
                                    <asp:DropDownList ID="dp_nationality" runat="server" ValidationGroup="vg" CssClass="required_box" Width="220px"></asp:DropDownList>
                                </td>
                                <td><asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dp_nationality" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td style=" text-align: right">Seniority(Year):</td>
                                <td style=" text-align: left" class="style2">
                                    <asp:TextBox ID="txt_Seniority" runat="server" ValidationGroup="vg" CssClass="required_box" MaxLength="2" Width="45px" ></asp:TextBox></td>
                                <td class="style3">
                                    <asp:RequiredFieldValidator ID="rq1" runat="server" ErrorMessage="Required." ControlToValidate="txt_Seniority" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                <td>
                                <asp:Button ID="btn_Show" runat="server" CssClass="btn" OnClick="btn_Show_Click" Text="Show" Width="60px" />
                                <a title="Wage Scales History" class="thickbox" onclick="return ShowHistory(this);" href="WageScalePopupCopy.aspx" >
                                    <input type="button" class="btn" value="History" style="width :60px" />
                                </a> 
                                <a title="Copy Wage Scales from" class="thickbox" onclick="return ShowCopy(this);" href="WageScalePopupCopy.aspx" >
                                    <input type="button" class="btn" value="Copy" style="width :60px" />
                                </a> 
                                <div style="display :none " >
                                
                                <asp:Button ID="btn_Show_History" runat="server" OnClick="btn_ShowHistory_Click" CausesValidation="false"/>
                                <asp:Button ID="btn_Show_Copy" runat="server" OnClick="btn_ShowCopy_Click" CausesValidation="false"/>
                                <asp:TextBox runat="server" ID="txtHistory" ></asp:TextBox>  
                                <asp:TextBox runat="server" ID="txtCopy" ></asp:TextBox>  
                                </div>
                                </td>
                                <td style=" text-align: left">&nbsp;</td>
                            </tr>
                            </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align:center ;padding-right: 10px; padding-left: 10px; text-align:center">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding:5px;">
                        <legend><strong> Components Value </strong></legend>
                       <div style="height:350px; overflow:scroll; width:100%;">
                            <asp:GridView ID="Gd_Wage" OnRowEditing="Row_Editing" AutoGenerateColumns="false" runat="server" Width="98%" OnRowDataBound="Gd_Wage_RowDataBound">
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <PagerStyle CssClass="pagerstyle" />
                            <RowStyle CssClass="rowstyle"  />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                             <Columns>
                            <asp:BoundField HeaderText="RANK" HeaderStyle-Font-Bold="true" DataField="RankCode" ><ItemStyle Width="60px" HorizontalAlign="Left"/></asp:BoundField>
                            <asp:TemplateField >
                                 <HeaderTemplate>
                                 <table cellpadding="0" cellspacing="0"><tr><td><asp:CheckBox ID="chk1" runat="server"  /></td></tr>
                                 <tr><td><asp:Label ID="lbl1" CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                                 </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt1" runat="server" Text='<%# Eval("C1") %>' OnBlur ="return CheckUnit(this)"  MaxLength="10" Width="60px" CssClass="input_box" style="text-align:right"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                            <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0"><tr><td> <asp:CheckBox ID="chk2" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl2" CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Eval("C2") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                               <table cellpadding="0" cellspacing="0"><tr><td> <asp:CheckBox ID="chk3" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl3"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt3" runat="server" Text='<%# Eval("C3") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0"><tr><td>   <asp:CheckBox ID="chk4" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl4"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt4" runat="server" Text='<%# Eval("C4") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                       <table cellpadding="0" cellspacing="0"><tr><td>   <asp:CheckBox ID="chk5" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl5"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt5" runat="server" Text='<%# Eval("C5") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField >
                               <HeaderTemplate>
                               <table cellpadding="0" cellspacing="0"><tr><td>     <asp:CheckBox ID="chk6" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl6"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt6" runat="server" Text='<%# Eval("C6") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0"><tr><td><asp:CheckBox ID="chk7" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl7" CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt7" runat="server" Text='<%# Eval("C7") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            
                            </asp:TemplateField>
                            <asp:TemplateField >
                               <HeaderTemplate>
                               <table cellpadding="0" cellspacing="0"><tr><td>      <asp:CheckBox ID="chk8" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl8"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt8" runat="server" Text='<%# Eval("C8") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                  <table cellpadding="0" cellspacing="0"><tr><td><asp:CheckBox ID="chk9" runat="server" /></td></tr>
                                 <tr><td>  <asp:Label ID="lbl9"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt9" runat="server" Text='<%# Eval("C9") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                  <table cellpadding="0" cellspacing="0"><tr><td>  <asp:CheckBox ID="chk10" runat="server" /></td></tr>
                                 <tr><td>    <asp:Label ID="lbl10"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt10" runat="server" Text='<%# Eval("C10") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                               <HeaderTemplate>
                                   <table cellpadding="0" cellspacing="0"><tr><td>    <asp:CheckBox ID="chk11" runat="server" /></td></tr>
                                 <tr><td>     <asp:Label ID="lbl11"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt11" runat="server" Text='<%# Eval("C11") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                  <table cellpadding="0" cellspacing="0"><tr><td><asp:CheckBox ID="chk12" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl12"  CssClass="SmallFont" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt12" runat="server" Text='<%# Eval("C12") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                           <asp:BoundField HeaderText="TOTAL" HeaderStyle-Font-Bold="true" DataField="Total" DataFormatString="{0:0.00}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"  /> 
                          </Columns>
                        </asp:GridView>
                        </div>
                    </fieldset>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="Txt_WEF">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
                <tr>
                    <td style="text-align: right;padding-right: 10px;padding-top: 10px; padding-bottom: 10px;" align="center">
                        <table cellspacing="0" cellpadding="0" border ="1" width="100%" >
                        <tr>
                        <td style=" text-align:right"  >
                            <asp:HiddenField ID="hwef" runat="server" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txt_Seniority"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:HiddenField ID="hfwsid" runat="server" />
                            W.e. From : &nbsp;
                        </td>
                        <td style=" text-align:right;width :120px;" >
                            <asp:TextBox ID="Txt_WEF" runat="server" CssClass="required_box" Width="90px"></asp:TextBox>
                            <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        </td>
                        <td style=" text-align:left; width :100px;">&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vg" runat="server" ErrorMessage="Required." ControlToValidate="Txt_WEF" Display="Static"></asp:RequiredFieldValidator>
                        </td>
                        <td style=" text-align:center; width :100px;">
                            <asp:Button ID="btn_Save" runat="server" CssClass="btn" ValidationGroup="vg" OnClick="Save_Click" Text="Save" Width="80px" />
                        </td>
                        </tr>
                        </table>
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
