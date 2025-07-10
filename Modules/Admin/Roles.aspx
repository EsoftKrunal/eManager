<%@ Page Title=""  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="Roles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
       <link rel="stylesheet" href="../../css/app_style.css" />
      <script type="text/javascript">

       

         function ShowMessage(msgtype,msg)
      {
          document.getElementById('divmsg').style.display = 'block';
          $('#<%=lblmsgerror.ClientID %>').html(msg)
          $('#<%=lblMsgType.ClientID %>').html(msgtype)
          $('#divmsg').removeClass();
          if(msgtype=="Error")
          {
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
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Role Master</td>
            </tr>
             
        </table> 
            <table id="EntryPanel" runat="server" class="app-form" style="width: 100%">
                
                <tr>

                    <td>


                        <div class="app-form-scroll">
                            <table class="table table-bordered table-responsive">

                                <tr>
                                    <td>RoleID</td>
                                    <td>
                                         
                                        <asp:TextBox ID="txtRoleID" runat="server"  CssClass="app-form-control  " ReadOnly="true" BackColor="Silver" ></asp:TextBox>

                                    </td>
                                     <td><span class="text-red text-bold">*</span>Role Name</td>
                                    <td>
                                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control required_box" MaxLength="50" placeholder="Enter Role Name..."></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRoleName" Display="Dynamic" ErrorMessage="Role name required." Font-Bold="true" ForeColor="Red">Role name required.</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                               
                               <%-- <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" CssClass="checkbox-inline" Text="IsActive" TextAlign="Right" />
                                    </td>
                                    <td colspan="3">
                                       
                                    </td>
                                </tr>--%>
                                 

                            </table>
                        </div>
                        <div >


                       






                            <div class="app-pull-right">

                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />

                            </div>
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
                        <asp:TextBox ID="txtSearchKeyWord" runat="server" AutoPostBack="True" CssClass="app-form-control" OnTextChanged="txtSearchKeyWord_TextChanged" Width="250px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvRoleList" runat="server" AutoGenerateColumns="False" CssClass="datalist" Width="100%"  
                            PagerSettings-Visible="true"  OnSorting="OnGrid_Sorting"  AllowSorting="True" AllowPaging="True" OnPageIndexChanging="OnPageIndex_Changing" OnRowCreated="OnRow_Created" >
                            <Columns>

                               <asp:BoundField DataField="RoleID" HeaderText="Role ID" SortExpression="RoleID" />
                      <asp:BoundField DataField="RoleName" HeaderText="Role Name" SortExpression="RoleName" />
                                <%--<asp:TemplateField HeaderText="Status" SortExpression="IsActive">
                                    
                                    <ItemTemplate>
                                        <%# (Eval("IsActive").ToString()=="True")? "<span style='color:green;' >Active </span>":"<span   style='color:red;' >InActive </span>"  %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditRow" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit" runat="server" CommandName="Select" OnClick="btnEditRow_Click" CausesValidation="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>


                            <HeaderStyle CssClass="thead-dark" />
                            
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