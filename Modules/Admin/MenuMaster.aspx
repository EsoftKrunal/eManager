<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MenuMaster.aspx.cs" Inherits="MenuMaster" %>

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
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Menu Master</td>
            </tr>
             
        </table> 
                            <table id="EntryPanel" runat="server" class="app-form" style="width: 100%">

                                <tr>

                                    <td>


                                        <div class="app-form-scroll">
                                            <table class="table table-bordered table-responsive">

                                                <tr>
                                                    <td>Menu D</td>
                                                     <td>

                                                        <asp:TextBox ID="txtMenuID" runat="server" BackColor="Gainsboro" CssClass="form-control disabled" ReadOnly="true"></asp:TextBox>

                                                    </td>
                                                    <td>Menu Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control required_box" MaxLength="150" placeholder="Enter Menu Name ..."></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMenuName" Display="Dynamic" ForeColor="Red" Font-Bold="true" ErrorMessage="Menu Name Required."></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>Select Module</td>
                                                     <td>
                                                        <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="True" CssClass="form-control dropdown-open" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>Select Page Url</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPages" runat="server" placeholder="Enter Page Url ..." CssClass="form-control dropdown-open">
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlPages" runat="server" ControlToValidate="ddlPages" InitialValue="0" Display="Dynamic" ForeColor="Red" Font-Bold="true" ErrorMessage="Page Url Required."></asp:RequiredFieldValidator>
                                          
                                                    </td>
                                                    <td>Select Parent Menu</td>
                                                    <td>
                                                        <asp:TextBox ID="txtParentMenu" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtParentMenu_TextChanged" placeholder="Enter Parent Menu ..."></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="Autoextender1" runat="server" CompletionInterval="100" CompletionSetCount="10" EnableCaching="false" FirstRowSelected="true" MinimumPrefixLength="1" ServiceMethod="GetMenuList" ServicePath="~/AutoComplete.asmx" TargetControlID="txtParentMenu">
                                                        </asp:AutoCompleteExtender>
                                                    </td>
                                                    <td>Parent MenuID</td>
                                                     <td>
                                                        <asp:TextBox ID="txtParentMenuID" runat="server" AutoPostBack="false" BackColor="Silver" CssClass="form-control disabled" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td>Display/Sort Order</td>
                                                     <td >
                                                        <asp:TextBox ID="txtSortOrder" runat="server" AutoPostBack="false" CssClass="form-control "></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtSortOrder" />
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" CssClass="checkbox-inline" Text="IsActive" TextAlign="Right" />
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="6">
                                                        <div class="app-pull-right">

                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn " OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />

                                            </div>
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                        <div>









                                            
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

                            <table id="GridPanel" runat="server" class="table table-bordered table-responsive" width="100%">

                                <tr>
                                    <td class="col-xs-1">
                                        <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="btn" OnClick="btnAdd_Click" Text="Add New Entry" />
                                    </td>
                                    <td class="col-xs-2"><strong>Enter Search Keyword :</strong>
                                        </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchKeyWord" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtSearchKeyWord_TextChanged" Width="250px"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td colspan="3">

                                        <asp:GridView ID="gvMenuList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive" Width="100%" DataKeyNames="MenuID"
                                            PagerSettings-Visible="true"
                                            OnSorting="OnGrid_Sorting" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="OnPageIndex_Changing" OnRowCreated="OnRow_Created" EmptyDataText="Records Not Found." >
                                            <Columns>

                                                <asp:BoundField DataField="MenuID" HeaderText="Menu ID" SortExpression="MenuID" />
                                                <asp:BoundField DataField="MenuName" HeaderText="Menu Name" SortExpression="MenuName" />
                                                <asp:BoundField DataField="MenuLocation" HeaderText="Menu Url" SortExpression="MenuLocation" />
                                                <asp:BoundField DataField="DisplayOrder" HeaderText="Sort Order" SortExpression="DisplayOrder" />
                                                <asp:TemplateField HeaderText="Status" SortExpression="IsActive">

                                                    <ItemTemplate>
                                                        <%# (Eval("IsActive").ToString()=="True")? "<span style='color:green;' >Active </span>":"<span   style='color:red;' >InActive </span>"  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditRow" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit" runat="server" CommandName="Select" OnClick="btnEditRow_Click" CausesValidation="False" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Modulename" HeaderText="Module Name" Visible="false" />
                                                <asp:BoundField DataField="ModuleID" HeaderText="ModuleID" Visible="false" />
                                            </Columns>

                                            <SelectedRowStyle CssClass="red" />
                                             <HeaderStyle CssClass="thead-dark" />
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
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $('#ctl00_ContentMainMaster_ddlPages').select2();

            //$('#ctl00_ContentMainMaster_gvMenuList').DataTable({

            //     'paging': false,
            //     'lengthChange': false,
            //     'searching': true,
            //     'ordering': false,
            //     'info': false,
            //     'autoWidth': false


            //  });
        }
    </script>
</asp:Content>
