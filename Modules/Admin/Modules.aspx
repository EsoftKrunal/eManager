<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Modules.aspx.cs" Inherits="Modules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../../css/app_style.css" />
    <script type="text/javascript" >
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>

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
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>

            <section class="content">
                <div class="box-wrapper">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Module Master</td>
            </tr>
             
        </table> 
                            <table id="EntryPanel" runat="server" class="app-form" style="width: 100%">
                                <tr>
                                    <td style="padding: 10px;">
                                        <div class="app-form-scroll">
                                            <table class="table table-bordered table-responsive" width="100%">
                                                <tr>
                                                    <td>ModuleID</td>
                                                    <td>
                                                        <asp:TextBox ID="txtModuleID" runat="server" CssClass="app-form-control disabled" BackColor="Silver" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="text-red text-bold">*</span>Module Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtModuleName" runat="server" CssClass="form-control required_box" MaxLength="50" placeholder="Enter Module Name ..."></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtModuleName" Display="Dynamic" ErrorMessage="Module name required." Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td><span class="text-red text-bold">*</span>Short Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control required_box" MaxLength="10" placeholder="Enter Short Name..."></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShortName" Display="Dynamic" ErrorMessage="Short name required" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                  
                                                </tr>
                                                <tr>
                                                    <td>
                                                      <span class="text-red text-bold">*</span> Anlystic Icon URL
                                                    </td>
                                                    <td> <asp:TextBox ID="txtAnlysticIconURL" runat="server" CssClass="form-control required_box" MaxLength="200" placeholder="Enter Anlystic Icon URL ..."></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnlysticIconURL" Display="Dynamic" ErrorMessage="Anlystic Icon URL required." Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td><%--<span class="text-red text-bold">*</span>--%>
                                                        Page URL</td>
                                                    <td><asp:TextBox ID="txtPageURL" runat="server" CssClass="form-control required_box" MaxLength="200" placeholder="Enter Page URL ..."></asp:TextBox>
                                                        <br />
                                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPageURL" Display="Dynamic" ErrorMessage="Page URL required." Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                                    </td>
                                                    <td><span class="text-red text-bold">*</span>Menu Icon URL
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMenuIconURL" runat="server" CssClass="form-control required_box" MaxLength="200" placeholder="Enter Menu Icon URL ..."></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMenuIconURL" Display="Dynamic" ErrorMessage="Menu Icon URL required." Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td><span class="text-red text-bold">*</span>Module Display Order</td>
                                                    <td><asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control required_box" MaxLength="3" placeholder="Enter Module Display Order ..." onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDisplayOrder" Display="Dynamic" ErrorMessage="Module Display order required." Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator></td>
                                                    <td colspan="2"><asp:CheckBox ID="chkIsActive" runat="server" Checked="True" CssClass="checkbox-inline" Text="IsActive" TextAlign="Right" /></td>
                                                    <td></td>
                                                    <td></td>
                                                   
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <div class="app-pull-right">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn " OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                                </div>
                             <div class="table-responsive">
                            <table id="GridPanel" runat="server" class="table table-bordered table-responsive" width="100%">
                                <tr>
                                    <td class="col-xs-1">
                                        <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="btn" OnClick="btnAdd_Click" Text="Add New Entry" />
                                    </td>
                                    <td  class="col-xs-2"><strong>Enter Search Keyword :</strong>
                                        </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchKeyWord" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtSearchKeyWord_TextChanged" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="gvModuleList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive" Width="100%"
                                            PagerSettings-Visible="true"
                                            OnSorting="OnGrid_Sorting" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="OnPageIndex_Changing" OnRowCreated="OnRow_Created" EmptyDataText="Record(s) Not Found." ShowHeaderWhenEmpty="True">
                                            <Columns>

                                                <asp:BoundField DataField="ModuleID" HeaderText="Module ID" SortExpression="ModuleID" />
                                                <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" />
                                                <asp:BoundField DataField="ShortName" HeaderText="Short Name" SortExpression="ShortName" />
                                                <asp:BoundField DataField="ImageURL" HeaderText="Anlystic Icon URL" SortExpression="ImageURL" />
                                                <asp:BoundField DataField="PURL" HeaderText="Page URL" SortExpression="PURL" />
                                                <asp:BoundField DataField="MenuIcon" HeaderText="Menu Icon URL" SortExpression="MenuIcon" />
                                                <asp:BoundField DataField="DisOrder" HeaderText="Display Order" SortExpression="DisOrder" />
                                                <asp:TemplateField HeaderText="Status" SortExpression="IsActive">
                                                    <ItemTemplate>
                                                        <%# (Eval("IsActive").ToString()=="True")? "<span style='color:green;' >Active </span>":"<span   style='color:red;' >InActive </span>"  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit"  ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditRow" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit" runat="server" CommandName="Select" OnClick="btnEditRow_Click" CausesValidation="False" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                           
                                            <SelectedRowStyle CssClass="red" />
                                        </asp:GridView>
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


                        </div>
                    </div>
                </div>

            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
