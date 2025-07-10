<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RoleRights.aspx.cs" Inherits="RoleRights" %>

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
    <script type="text/javascript">
        function checkAll(objRef, cellidx) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && i > 0) {

                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        // row.style.backgroundColor = "aqua";
                        //inputList[i].checked = true;
                        alert(GridView.rows[i].cells[cellidx]);
                        GridView.rows[i].cells[cellidx].getElementsByTagName("INPUT")[0].checked = true;

                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        //if (row.rowIndex % 2 == 0) {
                        //    //Alternating Row Color
                        //   // row.style.backgroundColor = "#C2D69B";
                        //}
                        //else {
                        //  //  row.style.backgroundColor = "white";
                        //}
                        // inputList[i].checked = false;
                        GridView.rows[i].cells[cellidx].getElementsByTagName("INPUT")[0].checked = false;
                    }
                }
            }




        }
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdPages.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function ViewHeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkView";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function AddHeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkAdd";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function EditHeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkEdit";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function DeleteHeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkDelete";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function PrintHeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkPrint";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function Verify1HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkVerify1";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function Verify2HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.grdPages.ClientID %>');
            var TargetChildControl = "chkVerify2";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function ChildClick(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }

    </script>
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <section class="content">
                <div class="box-wrapper">
                     <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Assign Role Rights</td>
            </tr>
             
        </table> 
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="EntryPanel" runat="server" class="table table-bordered table-responsive" style="width: 100%">
                                    <tr>
                                        <td>Module</td>
                                        <td>
                                            <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged" Width="250px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlModules"
                                                ErrorMessage="Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>Module Sections</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSections" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSections_SelectedIndexChanged" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Roles</td>
                                        <td>
                                            <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <div class="app-pull-right">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <div class="alert alert-danger alert-dismissable" id="divmsg" style="display: none;">
                                                <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                                                <strong>
                                                    <asp:Label ID="lblMsgType" runat="server"></asp:Label>! </strong>
                                                <asp:Label runat="server" ID="lblmsgerror"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" id="GridPanel" runat="server">
                                            <asp:GridView ID="grdPages" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive" Width="100%"
                                                PagerSettings-Visible="true"
                                                OnSorting="OnGrid_Sorting" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="OnPageIndex_Changing" OnRowCreated="OnRow_Created" EmptyDataText="Records Not Found." ShowHeaderWhenEmpty="True">
                                                <Columns>

                                                    <asp:BoundField DataField="PageID" HeaderText="ID" SortExpression="PageID" HeaderStyle-Width="50px" />
                                                    <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" HeaderStyle-Width="10%" />
                                                    <asp:BoundField DataField="Sectionname" HeaderText="Section Name" SortExpression="Sectionname" HeaderStyle-Width="10%" />
                                                    <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="10%" />
                                                    <asp:BoundField DataField="PageUrl" HeaderText="Page Url" SortExpression="PageUrl" />
                                                    <asp:TemplateField HeaderText="View" ItemStyle-Width="150px">
                                                        <HeaderTemplate>
                                                            Can View<br />
                                                            <asp:CheckBox runat="server" ID="chkViewAll" onclick="javascript:ViewHeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" CssClass="checkbox" ID="chkView" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            Add<br />

                                                            <asp:CheckBox runat="server" ID="chkAddAll" onclick="javascript:AddHeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox CssClass="checkbox" runat="server" ID="chkAdd" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            Edit<br />

                                                            <asp:CheckBox runat="server" ID="chkEditAll" onclick="javascript:EditHeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" CssClass="checkbox" ID="chkEdit" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            Delete<br />

                                                            <asp:CheckBox runat="server" ID="chkDeleteAll" onclick="javascript:DeleteHeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" CssClass="checkbox" ID="chkDelete" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            Print<br />

                                                            <asp:CheckBox runat="server" ID="chkPrintAll" onclick="javascript:PrintHeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" CssClass="checkbox" ID="chkPrint" />
                                                        </ItemTemplate>


                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Verify1" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            Verify1<br />

                                                            <asp:CheckBox runat="server" ID="chkVerify1All" onclick="javascript:Verify1HeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" CssClass="checkbox" ID="chkVerify1" />
                                                        </ItemTemplate>


                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Verify2" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            Verify2<br />

                                                            <asp:CheckBox runat="server" ID="chkVerify2All" onclick="javascript:Verify2HeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" CssClass="checkbox" ID="chkVerify2" />
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
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
