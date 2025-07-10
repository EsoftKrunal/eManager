<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModuleSections.aspx.cs" Inherits="ModuleSections" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" href="../../css/app_style.css" />
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
                            <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Module Sections</td>
            </tr>
             
        </table> 

                            <table id="EntryPanel" runat="server" class="table table-bordered table-responsive" style="width: 100%">
                                <tr>
                                    <td>
                                        <div class="app-form-scroll">
                                            <table class="table table-bordered table-responsive">
                                                <tr>
                                                    <td>Select Module</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlModules" CssClass="form-control"  runat="server" AutoPostBack="True"></asp:DropDownList>
                                                    </td>
                                                    <td>Section ID</td>
                                                    <td>
                                                        <asp:TextBox ID="txtSectionID" runat="server" CssClass="app-form-control  " ReadOnly="true" BackColor="Silver"></asp:TextBox>
                                                    </td>
                                                    <td><span class="text-red text-bold">*</span>Section Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtSection" runat="server" placeholder="Enter Section Name ..." MaxLength="100" CssClass="app-form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSection" Display="Dynamic"
                                                             ForeColor="Red" Font-Bold="true" ErrorMessage="Section name required.">Section name required.</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" CssClass="checkbox-inline" Text="IsActive" TextAlign="Right" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <div class="app-pull-right">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn" OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="alert alert-danger alert-dismissable" id="divmsg" style="display: none;">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                                <strong>
                                    <asp:Label ID="lblMsgType" runat="server"></asp:Label>! </strong>
                                <asp:Label runat="server" ID="lblmsgerror"></asp:Label>
                            </div>
                            <table id="GridPanel" runat="server" class="table table-bordered table-responsive" width="100%">
                                <tr>
                                    <td class="col-xs-1">
                                        <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="btn" OnClick="btnAdd_Click" Text="Add New Entry" />
                                    </td>
                                    <td class="col-xs-2"><strong>Enter Search Keyword :</strong>
                                        </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchKeyWord" runat="server" AutoPostBack="True" MaxLength="30"
                                            CssClass="app-form-control" OnTextChanged="txtSearchKeyWord_TextChanged" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="gvModuleSections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive" Width="100%"
                                            PagerSettings-Visible="true" ShowHeaderWhenEmpty="true"
                                            OnSorting="OnGrid_Sorting" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="OnPageIndex_Changing" OnRowCreated="OnRow_Created" EmptyDataText="Records Not Found." >
                                            <Columns>
                                                <asp:BoundField DataField="ModuleSectionID" HeaderText="Section ID" SortExpression="ModuleSectionID" />
                                                <asp:BoundField DataField="SectionName" HeaderText="Section Name" SortExpression="SectionName" />
                                                <asp:TemplateField HeaderText="Status" SortExpression="IsActive">
                                                    <ItemTemplate>
                                                        <%# (Eval("IsActive").ToString()=="True")? "<span style='color:green;' >Active </span>":"<span   style='color:red;' >InActive </span>"  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="50px">
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
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
