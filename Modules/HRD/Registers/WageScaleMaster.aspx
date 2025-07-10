<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="WageScaleMaster.aspx.cs" Inherits="CrewOperation_WageScaleMaster" %>
<asp:Content ID="Content1"  ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="background-color: #f9f9f9">
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      
                    </asp:ScriptManager>
    --%>
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
    </script>
 <div>
            <table cellspacing="0" style="background-color: #f9f9f9;" width="100%" border="0">
            <tr>
                <td class="textregisters" colspan="2" style="height: 17px;">
                    Wage Master</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 16px; text-align: center;">
                    &nbsp;<asp:Label ID="lb_msg" runat="server" ForeColor="Red" Width="362px"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="width: 50%; height: 16px; text-align: center; padding-right: 10px; padding-left: 10px; padding-bottom: 10px;">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;padding-bottom:5px">
                        <legend><strong>Wage Master</strong></legend>
                        <table width="100%" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style=" height: 19px; text-align: left">
                                    &nbsp;</td>
                                <td style=" text-align: left" colspan="1">
                                </td>
                                <td style=" height: 19px; text-align: left">
                                </td>
                                <td style=" height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                                <td style=" height: 19px; text-align: left; width: 38px;">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style=" text-align: right; height: 19px;">
                                    Wage Scale:&nbsp;
                                </td>
                                <td style="text-align: left; height: 19px;">
                                    <asp:DropDownList ID="dp_WSname" runat="server" CssClass="required_box" OnSelectedIndexChanged="dp_WSname_SelectedIndexChanged" Width="166px">
                                    </asp:DropDownList></td>
                                <td style="text-align: right; height: 19px;">
                                    Nationality:&nbsp;
                                </td>
                                <td style="text-align: left; height: 19px;"><asp:DropDownList ID="dp_nationality" runat="server" CssClass="required_box" OnSelectedIndexChanged="dp_nationality_SelectedIndexChanged">
                                </asp:DropDownList></td>
                                <td style="height: 19px; text-align: right">
                                    Seniority(Year):&nbsp;
                                </td>
                                <td style="height: 19px; text-align: left">
                                    <asp:TextBox ID="txt_Seniority" runat="server" CssClass="required_box" MaxLength="2"
                                        Width="25px" OnTextChanged="txt_Seniority_TextChanged"></asp:TextBox></td>
                                <td style="height: 19px; text-align: left; width: 38px;">
                                    WEF:</td>
                                <td style="height: 19px; text-align: left">
                                    <asp:TextBox ID="Txt_WEF" runat="server" CssClass="required_box" Width="90px"></asp:TextBox>&nbsp;
                                    <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                <td style="height: 19px; text-align: left">
                                    <asp:Button ID="btn_Show" runat="server" CssClass="input_box" OnClick="btn_Show_Click" Text="Show" Width="59px" /></td>
                            </tr>
                            
                            <tr>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left" valign="top">
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="dp_WSname"
                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left" valign="top">
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dp_nationality"
                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td style="height: 19px; text-align: right" colspan="2">
                                        <asp:RequiredFieldValidator ID="rq1" runat="server" ErrorMessage="Required" ControlToValidate="txt_Seniority" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Required."
                                        MaximumValue="10" MinimumValue="1" Type="Integer" ControlToValidate="txt_Seniority" Display="Dynamic"></asp:RangeValidator>
                                </td>
                                <td style=" height: 19px; text-align: left; width: 38px;">
                                </td>
                                <td style="height: 19px; text-align: left">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txt_WEF" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="Txt_WEF" ErrorMessage="Required." ></asp:RequiredFieldValidator>
                                   </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right">
                                    History:</td>
                                <td style="height: 19px; text-align: left" valign="top">
                                    <asp:DropDownList ID="ddl_History" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_History_SelectedIndexChanged" Width="120px">
                                        <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left" valign="top">
                                </td>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left" valign="top">
                                </td>
                                <td style="height: 19px; text-align: left; width: 38px;">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfwsid" runat="server" />
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="txt_Seniority">
                                                            </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:HiddenField ID="hwef" runat="server" />
                        &nbsp;
                    </fieldset>
                </td>
            </tr>
            
                <tr>
                <td colspan="2" style="width: 50%; height: 16px; text-align: center; padding-right: 10px; padding-left: 10px; padding-bottom: 10px;">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;padding-bottom:5px">
                        <legend><strong>Copy From</strong></legend>
                        <table width="100%" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style=" height: 19px; text-align: left">
                                    &nbsp;</td>
                                <td style=" height: 19px; text-align: right">
                                </td>
                                <td style=" text-align: left">
                                </td>
                                <td style=" height: 19px; text-align: right">
                                </td>
                                <td style=" height: 19px; text-align: left">
                                </td>
                                <td style=" height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                                <td style=" height: 19px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style=" text-align: right; height: 19px;">
                                    Wage Scale:&nbsp;
                                </td>
                                <td style="text-align: left; height: 19px;">
                                    <asp:DropDownList ID="ddcopywage" runat="server" CssClass="required_box">
                                    </asp:DropDownList></td>
                                <td style="text-align: right; height: 19px;">
                                    Nationality:&nbsp;
                                </td>
                                <td style="text-align: left; height: 19px;"><asp:DropDownList ID="ddcopynationality" runat="server" CssClass="required_box">
                                </asp:DropDownList></td>
                                <td style="height: 19px; text-align: right">
                                    Seniority(Year):&nbsp;
                                </td>
                                <td style="height: 19px; text-align: left">
                                    <asp:TextBox ID="txtcopyseniority" runat="server" CssClass="required_box" MaxLength="2"
                                        Width="25px"></asp:TextBox></td>
                                <td style="height: 19px; text-align: left">
                                    <asp:Button ID="btncopyshow" runat="server" CssClass="input_box" OnClick="btncopyshow_Click" Text="Copy" Width="59px" ValidationGroup="aa" /></td>
                            </tr>
                            
                            <tr>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left" valign="top">
                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="ddcopywage"
                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer" ValidationGroup="aa"></asp:RangeValidator></td>
                                <td style="height: 19px; text-align: left">
                                </td>
                                <td style="height: 19px; text-align: right">
                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="ddcopynationality"
                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer" ValidationGroup="aa"></asp:RangeValidator></td>
                                <td style="height: 19px; text-align: left" valign="top">
                                    </td>
                                <td style="height: 19px; text-align: right">
                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Required."
                                        MaximumValue="10" MinimumValue="1" Type="Integer" ControlToValidate="txtcopyseniority" Display="Dynamic" ValidationGroup="aa"></asp:RangeValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtcopyseniority" Display="Dynamic"  ValidationGroup="aa"></asp:RequiredFieldValidator>
                                        </td>
                                <td style=" height: 19px; text-align: left" valign="top">
                                    &nbsp;</td>
                                <td style=" height: 19px; text-align: left">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <%--<tr>
                <td colspan="2" style="padding-right: 10px; padding-left: 10px; text-align: left; height: 109px;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="height: 5px">
                                Vessel :</td>
                        </tr>
                        <tr>
                            <td>
                                <div class="input_box" style="overflow-y: scroll; overflow-x: hidden;
                                    height: 78px; width: 100% ;">
                                    <asp:CheckBoxList ID="chklst_Vessel" runat="server" RepeatColumns="10" RepeatDirection="Horizontal" Width="95%">
                                    </asp:CheckBoxList></div></td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2" style="text-align:center ; padding-right: 10px; padding-left: 10px;">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding:5px;">
                        <legend><strong> Components Value </strong></legend>
                        <div style=" height:200px;overflow-x:hidden; overflow-y:scroll;width:100%;">
                            <asp:GridView ID="Gd_Wage" OnRowEditing="Row_Editing" AutoGenerateColumns="false" OnRowDeleting="Row_Deleting" runat="server" Width="98%" OnRowDataBound="Gd_Wage_RowDataBound">
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <PagerStyle CssClass="pagerstyle" />
                            <RowStyle CssClass="rowstyle"  />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                             <Columns>
                            <asp:BoundField HeaderText="Rank" DataField="RankCode" >
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" HorizontalAlign="Left"/>
                            </asp:BoundField>
                            <asp:TemplateField>
                                 <HeaderTemplate>
                                 <table><tr><td><asp:CheckBox ID="chk1" runat="server"  /></td></tr>
                                 <tr><td><asp:Label ID="lbl1" runat="server"></asp:Label></td></tr>
                                 </table>
                                    
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt1" runat="server" Text='<%# Eval("C1") %>' OnBlur ="return CheckUnit(this)"  MaxLength="10" Width="60px" CssClass="input_box" style="text-align:right"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                               
                            <asp:TemplateField>
                            <HeaderTemplate>
                            <table><tr><td> <asp:CheckBox ID="chk2" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl2" runat="server"></asp:Label></td></tr>
                                 </table>
                            
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Eval("C2") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                               <HeaderTemplate>
                               <table><tr><td> <asp:CheckBox ID="chk3" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl3" runat="server"></asp:Label></td></tr>
                                 </table>
                            
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt3" runat="server" Text='<%# Eval("C3") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                               <HeaderTemplate>
                                <table><tr><td>   <asp:CheckBox ID="chk4" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl4" runat="server"></asp:Label></td></tr>
                                 </table>
                            
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt4" runat="server" Text='<%# Eval("C4") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                               <HeaderTemplate>
                                       <table><tr><td>   <asp:CheckBox ID="chk5" runat="server" /></td></tr>
                                 <tr><td><asp:Label ID="lbl5" runat="server"></asp:Label></td></tr>
                                 </table>
                            
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt5" runat="server" Text='<%# Eval("C5") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>                            
                            
                            <asp:TemplateField >
                               <HeaderTemplate>
                               <table><tr><td>     <asp:CheckBox ID="chk6" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl6" runat="server"></asp:Label></td></tr>
                                 </table>
                           
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt6" runat="server" Text='<%# Eval("C6") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                               <HeaderTemplate>
                                <table><tr><td>     <asp:CheckBox ID="chk7" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl7" runat="server"></asp:Label></td></tr>
                                 </table>
                           
                            
                             
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt7" runat="server" Text='<%# Eval("C7") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                           
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField >
                               <HeaderTemplate>
                               <table><tr><td>      <asp:CheckBox ID="chk8" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl8" runat="server"></asp:Label></td></tr>
                                 </table>
                            
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt8" runat="server" Text='<%# Eval("C8") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                           
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                               <HeaderTemplate>
                                  <table><tr><td><asp:CheckBox ID="chk9" runat="server" /></td></tr>
                                 <tr><td>  <asp:Label ID="lbl9" runat="server"></asp:Label></td></tr>
                                 </table>
                           
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt9" runat="server" Text='<%# Eval("C9") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                           
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                               <HeaderTemplate>
                                  <table><tr><td>  <asp:CheckBox ID="chk10" runat="server" /></td></tr>
                                 <tr><td>    <asp:Label ID="lbl10" runat="server"></asp:Label></td></tr>
                                 </table>
                          
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt10" runat="server" Text='<%# Eval("C10") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                           
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField >
                               <HeaderTemplate>
                                   <table><tr><td>    <asp:CheckBox ID="chk11" runat="server" /></td></tr>
                                 <tr><td>     <asp:Label ID="lbl11" runat="server"></asp:Label></td></tr>
                                 </table>
                           
                           
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt11" runat="server" Text='<%# Eval("C11") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                           
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                               <HeaderTemplate>
                                  <table><tr><td><asp:CheckBox ID="chk12" runat="server" /></td></tr>
                                 <tr><td> <asp:Label ID="lbl12" runat="server"></asp:Label></td></tr>
                                 </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:TextBox ID="txt12" runat="server" Text='<%# Eval("C12") %>' MaxLength="10" Width="60px" CssClass="input_box" OnBlur ="return CheckUnit(this)" style="text-align:right"></asp:TextBox>
                            </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                           <asp:BoundField HeaderText="Total" ItemStyle-Width="150px" DataField="Total" DataFormatString="{0:0.00}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"  /> 
                            
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
                    <td colspan="2" style="width: 100%; padding-right: 10px; padding-left: 10px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%; text-align: right; ; padding-right: 10px;padding-top: 10px; padding-bottom: 10px;" align="center">
                        <asp:Button ID="btn_Save" runat="server" CssClass="input_box" OnClick="Save_Click"
                            Text="Save" Width="59px" /></td>
                </tr>
        </table>
    
    </div>
     </asp:Content>
 <%--   </form>
</body>
</html>
--%>