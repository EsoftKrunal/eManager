<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserRoles.aspx.cs" Inherits="Modules_Admin_UserRoles" %>

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

    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <section class="content">
                <div class="box-wrapper">
                    <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Assign User Roles</td>
            </tr>
             
        </table> 
                    <div class="box box-warning">
                        
                        <div class="box-body">
                            
                            <table id="EntryPanel" runat="server" class="app-form" style="width: 100%">

                                <tr>

                                    <td>


                                        <div class="app-form-scroll">
                                             
                                            <table class="table table-bordered table-responsive">

                                                <tr>
                                                    <td class="col-xs-1"><strong>Select User</strong></td>
                                                    <td>

                                                        <asp:DropDownList ID="ListUsers" CssClass="form-control dropdown-open" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListUsers_SelectedIndexChanged"></asp:DropDownList>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="2"><strong>Assign Roles</strong></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:CheckBoxList ID="ListAssigned" CssClass="checkbox" runat="server"></asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="app-pull-right">
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                   <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" />
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
