<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewWebPortal.aspx.cs" Inherits="CrewWebPortal" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Management</title>
    <link type="text/css" href="StyleSheet.css" rel="Stylesheet" />   
    <style type="text/css" >
/* AutoComplete item */
.AutoCompleteExtender_CompletionList 
{
background-color : lightgray;
color : windowtext;
padding : 1px;
font-size: small;
background-color:Gray;
list-style-type: none ; 
border :solid 1px gray;
height :100px;
overflow-y:scroll;  
overflow-x:hidden; 
/*creates border with
autocomplete_completionListElement
background-color*/
}

/*AutoComplete flyout */
.AutoCompleteExtender_CompletionListItem 
{ 
text-align : left;
background-color:White;
list-style-type: none ; 
color:black;
}

/* AutoComplete highlighted item */
.AutoCompleteExtender_HighlightedItem
{
background-color: Silver;
text-align :left; 
color: blue;
list-style-type: none ; 
}
</style> 
<script type="text/javascript" >
function move()
{
var cd=event.keyCode;
if (cd==13)
{
//document.getElementById('btnShow').focus();
//document.getElementById('btnShow').click();
}
}

</script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnShow">
    <div style="text-align:center">
   <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellspacing="0" border="0" cellpadding="0" style="width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <strong class="pagename" >Crew Web Portal</strong>
            <mtm:Message runat="server" ID="Msgbox" /> 
            <div style=" white-space :100%; height:50px;">
            <center >
            <fieldset style="width :95%; height :50px;"  >
            <legend>Filter</legend>
                <table width="100%" border="0">
                    <col width="450px" />
                    <col  />
                    <col width="200px" />
                    <tr>
                        <td>
                            User Name : <asp:TextBox runat="server" ID="myTextBox" Width="300" autocomplete="off" onkeydown="move()" />
                             <ajaxToolkit:AutoCompleteExtender runat="server" 
                                  ID="autoComplete1" 
                                  TargetControlID="myTextBox"
                                  ServiceMethod="GetCompletionList"
                                  CompletionInterval="1000"
                                  EnableCaching="true"
                                  MinimumPrefixLength="0"
                                  CompletionSetCount="12"
                                  CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                  CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                  CompletionListCssClass="AutoCompleteExtender_CompletionList" />
                        </td>
                        <td>

                            Status : 
                    <asp:DropDownList ID="ddlsStatus" runat="server">
                        <asp:ListItem Value=""     Text="All"></asp:ListItem>
                        <asp:ListItem Value="1"     Text="Active" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2"     Text="Inactive"></asp:ListItem>
                     </asp:DropDownList>
                        </td>
                        <td>
                            <span style="" >
                                <asp:Button runat="server" ID="btnShow" UseSubmitBehavior="false" Text="Show" OnClick="Show_Click" ></asp:Button> </span>
                                <asp:Button runat="server" id="btnBack" UseSubmitBehavior="false" Text="<< Go Back" OnClientClick="return parent.DoPost(5);"></asp:Button> 
                        </td>
                        
                    </tr>
                </table>
            
           
                                  
            
                
            </fieldset>
            </center>
         
             
            </div>
            <div style=" padding-right :17px;" >
            <table cellpadding="0" cellspacing ="0" width="100%" border="1">
            <tr class="header" >
            <td style=" width :50px">Edit</td>            
            <td style=" width :200px">Name</td>
            <td style=" width :320px">Email</td>
            <td style=" width :63px" >Code</td>
            <td style=" width :63px" >Status</td>
            </tr>
            </table>
            </div>
            <asp:Panel runat="server" ID="pnlList" Width="100%" style="overflow-x:hidden; overflow-y:scroll" >
            <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
            <asp:Repeater runat="server" ID="rpt_Data">
            <ItemTemplate >
            <tr class="<%# (int.Parse(Eval("LoginId").ToString())==int.Parse(ViewState["SelId"].ToString()))?"selrowcontent":"rowcontent" %>">
            <td style=" width :50px"><asp:ImageButton runat="server" ImageUrl="images/edit.jpg" ID="imgEdit" OnClick="EditClick" CommandArgument='<%# Eval("LoginId") %>'/></td>
            <td style=" width :200px; text-align : left">&nbsp;<%# Eval("Name") %></td>
            <td style=" width :320px; text-align : left">&nbsp;<%# Eval("Email") %></td>
            <td style=" width :63px">&nbsp;<%# Eval("EmpCode") %></td>
            <td style=" width :63px">&nbsp;<%# Eval("Status") %></td>
            </tr>
            </ItemTemplate>
            </asp:Repeater> 
            </table>
            </asp:Panel>
            <asp:Panel ID="pnl_UserLogin" runat="server" Width="100%" Visible="False">
            <table border="0" cellpadding="0" cellspacing="4" style="text-align: center" width="100%">
            <tr>
            <td colspan="4" style =" background-color :gray">&nbsp;</td>
            </tr>
              <tr>
              <td colspan="4">
                  <asp:HiddenField ID="HiddenUserLogin" runat="server" />
              </td>
                                                    </tr>
              <tr>
                  <td align="right" 
                      style="padding-right: 15px; text-align: right; padding-left: 65px;">
                      Role Name:</td>
                  <td style="text-align: left">
                      <asp:DropDownList ID="ddlRoleName" runat="server" CssClass="required_box" 
                          Width="185px">
                      </asp:DropDownList>
                      <span class="required">*</span></td>
                  <td align="right" style="text-align: right; padding-right:15px">
                      User Name:</td>
                  <td style="text-align: left">
                      <asp:TextBox ID="txtUserId" runat="server" CssClass="required_box" 
                          MaxLength="25" Width="180px"></asp:TextBox>
                      <span class="required">*</span></td>
              </tr>
              <tr>
            <td align="right" style="text-align: right; padding-right:15px; ">
                    Password:</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="required_box" 
                        MaxLength="15" TextMode="Password" Width="180px"></asp:TextBox>
                    <span style="color: #0000cc"><span class="required">*</span></span></td>
                <td align="right" style="text-align: right; padding-right:15px">
                    Confirm Password:</td>
                <td style="text-align: left; color: #0e64a0;">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="required_box" 
                        Width="180px" MaxLength="15" TextMode="Password" ></asp:TextBox>
                    <span class="required">*</span></td>
            </tr>
              <tr>
                  <td align="right" style="padding-right: 15px; text-align: right">
                      Name:</td>
                  <td style="text-align: left">
                      <asp:TextBox ID="txtFirstName" runat="server" CssClass="required_box" 
                          Width="140px" MaxLength="24"></asp:TextBox>
                      <span class="helptext">(First)</span> <span class="required">*</span>
                      <asp:TextBox ID="txtLastName" runat="server" CssClass="required_box" 
                          MaxLength="24" Width="180px"></asp:TextBox>
                      <span class="helptext">(Last)</span> </td>
                  <td align="right" style="text-align: right; padding-right:15px;">
                      <%--DOB:--%></td>
                  <td style="text-align: left; display:none; ">
                      <asp:TextBox ID="txtDOB" runat="server" CssClass="input_box" Width="120px"></asp:TextBox>&nbsp;<asp:ImageButton ID="img_dob_user" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar_icon.gif" />
                      </td>
              </tr>
              <tr>
                  <td align="right" style="padding-right: 15px; text-align: right">
                      Email:</td>
                  <td style="text-align: left">
                      <asp:TextBox ID="txtEmail" runat="server" CssClass="required_box" Width="180px" 
                          MaxLength="99"></asp:TextBox><span class="required">*</span></td>
                  <td align="right" style="text-align: right; padding-right:15px">
                      Status:</td>
                  <td style="text-align: left">
                      <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" 
                          Width="185px">
                      </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                  
                  <td align="right" style="text-align: right; padding-right:15px">
                      Recruiting Office:</td>
                  <td style="text-align: left">
                      <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                          overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                          height: 100px">
                          <asp:CheckBoxList ID="chklstRecruitingOffice" runat="server" 
                              CssClass="input_box" Width="175px">
                          </asp:CheckBoxList>
                      </div>
                  </td>
                  <td align="right" style="padding-right: 15px; text-align: right">
                      </td>
                  <td style="text-align: left">
                      <%--<div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                          overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                          height: 100px">
                          <asp:CheckBoxList ID="ckldepartment" runat="server" CssClass="input_box" 
                              Width="175px">
                          </asp:CheckBoxList>
                      </div>--%>
                  </td>
              </tr>
              <tr>
                  <td align="right" style="padding-right: 15px; text-align: right">
                      Employee Code :</td>
                  <td style="text-align: left">
                      <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="readonly" 
                           Width="180px"></asp:TextBox>
                  </td>
                  <td align="right" style="text-align: right; padding-right: 15px;">
                      </td>
                  <td style="text-align: left">
                            <%--<asp:CheckBox ID="chkNWC" runat="server"></asp:CheckBox>--%>
                      </td>
              </tr>
                <tr>
                    <td align="right" style="padding-right: 15px; text-align: right">
                        Created By:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="readonly" 
                            ReadOnly="True" Width="180px"></asp:TextBox>
                    </td>
                    <td align="right" style="text-align: right; padding-right: 15px;">
                        Created On:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCreatedOn" runat="server" CssClass="readonly" 
                            ReadOnly="True" Width="180px"></asp:TextBox>
                    </td>
                </tr>
              <tr>
                                                        <td align="right" style="text-align: right; padding-right:15px">
                                                            Modified By:</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtModifiedBy" runat="server" CssClass="readonly" 
                                                                ReadOnly="True" Width="180px"></asp:TextBox></td>
                                                        <td align="right" style="text-align: right; padding-right:15px">
                                                            Modified On:</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtModifiedOn" runat="server" CssClass="readonly" 
                                                                ReadOnly="True" Width="180px"></asp:TextBox></td>
                                                    </tr>
            </table>
            </asp:Panel>
            <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="MM/dd/yyyy" PopupButtonID="img_dob_user" TargetControlID="txtDOB" ></ajaxToolkit:CalendarExtender>  
                        <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" />
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
<%--OnRowDeleting="GridView_UserLogin_Row_Deleting"--%>