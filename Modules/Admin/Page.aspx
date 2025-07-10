<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Page.aspx.cs" Inherits="AdminPanel_Page" %>

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
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                // DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                // DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                //DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
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
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Page Master</td>
            </tr>
             
        </table> 
                                <table id="EntryPanel" runat="server" class="app-form" style="width: 100%">

                                    <tr>

                                        <td>


                                            <div class="app-form-scroll">
                                                <table class="table table-bordered table-responsive">

                                                    <tr>
                                                        <td>Page ID</td>
                                                        <td>

                                                            <asp:TextBox ID="txtPageID" runat="server" CssClass="form-control disabled  required_box" ReadOnly="true" BackColor="Silver"></asp:TextBox>

                                                        </td>
                                                        <td><span class="text-red text-bold">*</span>Page Name</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPageName" runat="server" CssClass="form-control disabled  required_box" placeholder="Enter Page Name ..." MaxLength="150"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPageName" Display="Dynamic" ForeColor="Red" Font-Bold="true" ErrorMessage="name is Required."></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td><span class="text-red text-bold">*</span>Page Description</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPageDesc" runat="server" CssClass="form-control disabled required_box" placeholder="Enter Page Description ..." MaxLength="250"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPageDesc" Display="Dynamic" ForeColor="Red" Font-Bold="true" ErrorMessage="description is Required."></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td><span class="text-red text-bold">*</span>Page Url</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPageLocation" runat="server" CssClass="form-control required_box" MaxLength="150" placeholder="Enter Page Url ..."></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPageLocation" Display="Dynamic" ForeColor="Red" Font-Bold="true" ErrorMessage="location is Required."></asp:RequiredFieldValidator>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                CompletionInterval="100" CompletionSetCount="10" EnableCaching="false" MinimumPrefixLength="1"
                                                                ServiceMethod="GetGLAccountList" TargetControlID="txtPageLocation" FirstRowSelected="true">
                                                            </asp:AutoCompleteExtender>
                                                        </td>
                                                        <td>Module</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlModules" Width="300px" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqval1" ErrorMessage="module is Required.." Display="Dynamic" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlModules" InitialValue="-1"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>Section</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSection" Width="300px" runat="server" CssClass="required_box">
                                                            </asp:DropDownList>
                                                           <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ErrorMessage="Section is Required.." Display="Dynamic" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlSection" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <div class="app-pull-right">
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn " CausesValidation="true" OnClick="btnSubmit_Click" />
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="table-responsive">
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
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSearchKeyWord" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtSearchKeyWord_TextChanged" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="col-xs-1">Filter Module</td>
                                        <td>
                                            <asp:DropDownList ID="ddlModules_Filter" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlModules_Filter_SelectedIndexChanged" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="col-xs-2">Filter By Module Sections</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlSections_Filter" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSections_Filter_SelectedIndexChanged" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">

                                            <%--   <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>

                                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">--%>

                                            <asp:GridView ID="grdPages" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive" Width="100%"
                                                PagerSettings-Visible="true" OnSorting="OnGrid_Sorting" AllowSorting="True" AllowPaging="true"
                                                OnPageIndexChanging="OnPageIndex_Changing" OnRowCreated="OnRow_Created" EmptyDataText="Records Not Found." ShowHeaderWhenEmpty="True">
                                                <Columns>

                                                    <asp:BoundField DataField="PageID" HeaderText="Page ID" SortExpression="PageID" />
                                                    <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" />
                                                    <asp:BoundField DataField="PageUrl" HeaderText="Page Url" SortExpression="PageUrl" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                                    <asp:BoundField DataField="SectionName" HeaderText="Section Name" />


                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEditRow" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit" runat="server" CommandName="Select" OnClick="btnEditRow_Click" CausesValidation="False" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>


                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />

                                                <SelectedRowStyle CssClass="red" />
                                            </asp:GridView>

                                            <%-- </div>

                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </div>--%>


                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
