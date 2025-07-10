<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link rel="stylesheet" href="../../css/app_style.css" />--%>
    <script type="text/javascript">

        function ShowMessage(msgtype, msg) {
            document.getElementById('divmsg').style.display = 'block';
            $('#<%=lblmsgerror.ClientID %>').html(msg)
            $('#<%=lblMsgType.ClientID %>').html(msgtype)
            $('#divmsg').removeClass();
            if (msgtype == "Error") {
                $('#divmsg').addClass('alert alert-danger alert-dismissable');
            }
            if (msgtype == "Warning") {
                $('#divmsg').addClass('alert alert-warning alert-dismissable');
            }
            if (msgtype == "Info") {
                $('#divmsg').addClass('alert alert-info alert-dismissable');
            }
            if (msgtype == "Success") {
                $('#divmsg').addClass('alert alert-success alert-dismissable');
            }
        }
    </script>
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../css/StyleSheet.css" />
    <style type="text/css">
        .table-responsive {
	/*display: block;*/
	width: 100%;
	overflow-x: auto;
	-webkit-overflow-scrolling: touch;
	-ms-overflow-style: -ms-autohiding-scrollbar;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>





    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>

            <section class="content">
                <%--<div class="box-wrapper">--%>
                     <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" > &nbsp;User Management</td>
            </tr>
             
        </table> 
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive table-condensed no-border">
                                <table id="EntryPanel" runat="server" class="app-form no-border" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <div class="app-form-scroll">
                                                <table class="table table-bordered table-responsive" style="width: 100%;">
                                                    
                                                    <tr>

                                                        <td>Role Name:</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRoleName" runat="server" CssClass="required_box"
                                        Width="95%">
                                    </asp:DropDownList>
                                                            <span class="required">*</span>
                                                        </td>
                                                        <td>User Name: </td>
                                                        <td>
                                                              <asp:TextBox ID="txtUserId" runat="server" CssClass="required_box" MaxLength="25" Width="95%"></asp:TextBox>
                                                            <span class="required">*</span>
                                                        </td>
                                                       

                                                        <td>Password:</td>

                                                        <td>
                                                           <asp:TextBox ID="txtPassword" runat="server" CssClass="required_box"
                                        Width="95%" MaxLength="15" TextMode="Password"></asp:TextBox>
                                                            <span style="color: #0000cc"><span class="required">*</span></span>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>Confirm Password: </td>
                                                        <td>
                                                             <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="required_box" MaxLength="15" TextMode="Password" Width="95%"></asp:TextBox> <span style="color: #0000cc"><span class="required">*</span></span>
                                                        </td>
                                                        <td>Name ( First ): </td>
                                                        <td>
                                                             <asp:TextBox ID="txtFirstName" runat="server" CssClass="required_box" Width="95%"
                                        MaxLength="24"></asp:TextBox>
                                                             <span style="color: #0000cc"><span class="required">*</span></span>
                                                        </td>
                                                        <td>Name ( Last ): </td>
                                                        <td>
                                                             <asp:TextBox ID="txtLastName" runat="server" CssClass="required_box" MaxLength="24" Width="95%"></asp:TextBox> <span style="color: #0000cc"><span class="required">*</span></span>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                         
                                                        <td>DOB: </td>
                                                        <td>
                                                             <asp:TextBox ID="txtDOB" runat="server" CssClass="input_box" Width="120px"></asp:TextBox>
                                    <asp:ImageButton ID="img_dob_user" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar_icon.gif" />
                                                        </td>
                                                        <td>Email: </td>
                                                        <td>
                                                              <asp:TextBox ID="txtEmail" runat="server" CssClass="required_box" MaxLength="99" Width="95%"></asp:TextBox> <span style="color: #0000cc"><span class="required">*</span></span>
                                                        </td>
                                                        <td>Status:</td>

                                                        <td>
                                                             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" Width="185px">
                                    </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Crew Web Portal Access :</td>
                                                        <td>
                                                          <asp:CheckBox runat="server" ID="chkCrewPortalAccess" />
                                                        </td>
                                                        <td>
                                                            Recruiting Office:
                                                        </td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chklstRecruitingOffice" runat="server" CssClass="input_box" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                                        </td>
                                                        <td>
                                                          
                                                            Super User :
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:CheckBox runat="server" ID="chkSuperUser" />
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td >
                                                           Created By / On:
                                                        </td>
                                                        <td>
                                                             <asp:Label ID="txtCreatedBy" runat="server"></asp:Label>/
                                    <asp:Label ID="txtCreatedOn" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            Modified By / On:
                                                        </td>
                                                        <td>
                                                              <asp:Label ID="txtModifiedBy" runat="server"></asp:Label>
                                    /<asp:Label ID="txtModifiedOn" runat="server" Width="180px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            Position:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPositions" runat="server" CssClass="input_box" Width="185px"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Employee Code :
                                                        </td>
                                                        <td>
                                                             <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="readonly" Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td >
                                                               Office :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="input_box" Width="185px">
                                                               
                                                            </asp:DropDownList>
                                                            <span style="color: #0000cc"><span class="required">*</span></span>
                                                        </td>
                                                        <td>
                                                            Web API User :
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" ID="chkWebAPIUser" />
                                                        </td>
                                                        
                                                    </tr>

                                                </table>
                                            </div>
                                            <div>

                                                 <table style="width: 100%">
                            <tr>
                                <td>
                                    <uc1:MessageBox ID="Msgbox" runat="server" />
                                    <%--<mtm:message runat="server" ID="Msgbox" />--%>
                                </td>
                                <td style="text-align: right">
                                    <asp:HiddenField ID="HiddenUserLogin" runat="server" />
                                   
                                    <asp:Button ID="btn_Save" runat="server" CssClass="btn"
                                        Text="Save" Width="59px" OnClick="btn_Save_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn"
                                        Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" />
                                     <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MMM-yyyy" PopupButtonID="img_dob_user" TargetControlID="txtDOB"></ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                        </table>

                                               <%-- <div class="app-pull-right">

                                                   <asp:HiddenField ID="HiddenUserLogin" runat="server" />
                                    <asp:Button ID="btn_Add" runat="server" CssClass="btn"
                                        Text="Add" Width="59px" OnClick="btn_Add_Click" CausesValidation="False" />
                                    <asp:Button ID="btn_Save" runat="server" CssClass="btn"
                                        Text="Save" Width="59px" OnClick="btn_Save_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn"
                                        Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" />

                                                </div>--%>
                                            </div>
                                        </td>
                                    </tr>

                                </table>
                            </div>

                            <div class="table-responsive table-condensed no-border" width="100%">

                                <table id="GridPanel" runat="server" class="table table-bordered table-responsive" width="100%">
                <tr>
                    <td style="vertical-align: top; width: 100%;">
                        <table border="0" width="100%" cellpadding="3" cellspacing="0">
                            <colgroup>
                                <col width="450px" />
                                <col />
                                <col width="200px" />
                                <tr>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="myTextBox" runat="server" autocomplete="off" onkeydown="move()" placeholder="Enter Username." Style="padding: 4px;" Width="350px" />
                                        <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server" CompletionInterval="1000" CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionSetCount="12" EnableCaching="true" MinimumPrefixLength="0" ServiceMethod="GetCompletionList" TargetControlID="myTextBox" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsStatus" runat="server" Style="height: 25px; line-height: 25px; width: 100px;">
                                            <asp:ListItem Text="Emp. Status" Value=""></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Inactive" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td><span style="">
                                        <asp:Button ID="btnShow" runat="server" OnClick="Show_Click" Text="Show"  CssClass="btn" UseSubmitBehavior="false" /> 

                                         <asp:Button ID="btn_Add" runat="server" CssClass="btn"
                                        Text="Add New Entry" Width="120px" OnClick="btn_Add_Click" CausesValidation="False" />
                                        </span><%-- <asp:Button runat="server" id="btnBack" UseSubmitBehavior="false" Text="<< Go Back" OnClientClick="return parent.DoPost(5);"></asp:Button> --%></td>
                                </tr>
                            </colgroup>
                        </table>
                        <br />
                        <div style="height: 400px; overflow-y: scroll; overflow-x: hidden;width:100%;">
                            <table style="border-collapse: collapse;" cellpadding="2" cellspacing="0" width="100%" border="1">
                                <tr class="headerstylegrid">
                                    <td style="width: 50px">Action</td>

                                    <td style="width: 150px">User Name</td>
                                    <td style="width: 200px">Name</td>
                                    <%--<td style=" width :320px">Email</td>--%>
                                    <td style="width: 50px">Code</td>
                                    <td style="width: 63px">Status</td>
                                    <td style="width: 63px">Super User</td>
                                    <td style="width: 63px">Web API User</td>
                                </tr>
                                <asp:Repeater runat="server" ID="rpt_Data">
                                    <ItemTemplate>
                                        <tr class="<%# (int.Parse(Eval("LoginId").ToString())==int.Parse(ViewState["SelId"].ToString()))?"selrowcontent":"rowcontent" %>">
                                            <td style="width: 50px">
                                                <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/images/edit.jpg" ID="imgEdit" OnClick="EditClick" CommandArgument='<%# Eval("LoginId") %>' />
                                                <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/images/delete.jpg" ID="imgDelete" OnClick="DeleteClick" CommandArgument='<%# Eval("LoginId") %>' OnClientClick="return confirm('Are you sure to make inactive this User.')" />
                                            </td>
                                            <td style="width: 150px; text-align: left">&nbsp;<%# Eval("UserId") %><span style="color: red; font-size: 10px;">(<%# Eval("RoleName") %>)</span>
                                            </td>
                                            <td style="width: 200px; text-align: left">&nbsp;<%# Eval("Name") %></td>
                                            <%--<td style=" width :320px; text-align : left">&nbsp;<%# Eval("Email") %></td>--%>
                                            <td style="width: 50px">&nbsp;<%# Eval("EmpCode") %></td>
                                            <td style="width: 63px">&nbsp;<%# Eval("Status") %></td>
                                            <td style="width: 63px">&nbsp;<%# Eval("IsSuperUser") %></td>
                                            <td style="width: 63px">&nbsp;<%# Eval("IsWebAPIUser") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                    
                </tr>
            </table>
                            </div>
                            <div class="alert alert-danger alert-dismissable" id="divmsg" style="display: none;">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                                <strong>
                                    <asp:Label ID="lblMsgType" runat="server"></asp:Label>! </strong>
                                <asp:Label runat="server" ID="lblmsgerror"></asp:Label>
                            </div>
                            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                            <asp:ModalPopupExtender ID="MP1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                                CancelControlID="btnCloseResetPwd" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </div>
                    </div>
                <%--</div>--%>

            </section>
        </ContentTemplate>
       <Triggers>
           <asp:PostBackTrigger ControlID="btn_Add" />
           <asp:PostBackTrigger ControlID ="btn_Cancel" />
           <asp:PostBackTrigger ControlID ="btn_Save" />
           <asp:PostBackTrigger ControlID ="rpt_Data" />
           <asp:PostBackTrigger ControlID ="btnShow" />
       </Triggers>
    </asp:UpdatePanel>
    <%--  <script type="text/javascript">
         var c = document.getElementById("<%=txtSearchKeyWord.ClientID %>");
         
         c.select =
         function (event, ui)
         {
             alert(this.value);
             this.value = ""; return false;
         }
    </script>--%>
</asp:Content>
