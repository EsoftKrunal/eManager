<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUpdateVacancy.aspx.cs" Inherits="Modules_HRD_Vacancy_AddUpdateVacancy" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <script type="text/javascript" >
       
        function refresh() {            
            window.opener.reloadVacancyDetails();
            self.close();
         } 

         function refreshClose() {
             self.close();
         } 
     </script>
</head>
<body>
    <form id="form1" runat="server">
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div style="font-family:Arial;font-size:12px;width:90%;" >
            <center>
                <div class="text headerband">
                Add Vacancy
            </div>
            <br />
            <center>
                <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid">
                                        <legend><strong>Vacancy Details</strong></legend>
            <table width="95%" >
                <tr>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Owner :</td>
                    <td style="text-align:left;padding-left:5px;width:30%;padding:3px;">
                         <asp:DropDownList ID="ddl_Owner" runat="server"  CssClass="form-control" Width="165px" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_Owner_SelectedIndexChanged">
                                                                       <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList> &nbsp;
                        <asp:RequiredFieldValidator id="rfvddl_Owner" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Owner" meta:resourcekey="rfvddl_OwnerResource1" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Vessel :</td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;">
                        <asp:DropDownList ID="ddl_Vessel" runat="server"  CssClass="form-control" Width="165px" TabIndex="2" OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged" AutoPostBack="True">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator id="rfvddl_Vessel" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Vessel" meta:resourcekey="ddl_VesselResource1" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                   
                </tr>
                <tr>
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Vessel Type : </td>
                    <td style="text-align:left;padding-left:5px;width:30%;padding:3px;">
                       <%-- <asp:TextBox ID="txtVesselType" runat="server" Width="200px" ReadOnly="true" TabIndex="3"></asp:TextBox>--%>
                          <asp:DropDownList ID="ddl_VesselType" runat="server"  CssClass="form-control" Width="165px" TabIndex="3"  AutoPostBack="True">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList>
                    </td>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Rank :</td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;"> <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="form-control" Width="165px" TabIndex="4">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList> &nbsp;
                        <asp:RequiredFieldValidator id="rfvRank" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Rank_Search" meta:resourcekey="ddl_Rank_SearchResource1" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Joining Date : </td>
                    <td style="text-align:left;padding-left:5px;width:30%;padding:3px;"><asp:TextBox ID="txtDoj" runat="server" CssClass="required_box" Width="140px" TabIndex="5"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                        ID="imgDoj" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgDoj" TargetControlID="txtDoj" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                        &nbsp;
                         <asp:RequiredFieldValidator runat="server" ID="rfvDOJ" ControlToValidate="txtDoj" ErrorMessage="Required."></asp:RequiredFieldValidator>  
                    </td>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Proposal Date : </td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;">
                        <asp:TextBox ID="txtDOP" runat="server" CssClass="required_box" Width="140px" TabIndex="6" ></asp:TextBox>&nbsp;
                        <asp:ImageButton
                                                                                        ID="imgDOP" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgDOP" TargetControlID="txtDOP" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ID="rfvDOP" ControlToValidate="txtDOP" ErrorMessage="Required."></asp:RequiredFieldValidator>  
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Monthly Salary (USD) : </td>
                    <td style="text-align:left;padding-left:5px;width:30%;padding:3px;">
                         <asp:TextBox ID="txtSalary" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtSalaryResource1" TabIndex="7"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtSalary" FilterType="Numbers"  ></ajaxToolkit:FilteredTextBoxExtender>
                        &nbsp;
                        <asp:RequiredFieldValidator runat="server" ID="rfvSalary" ControlToValidate="txtSalary" ErrorMessage="Required."></asp:RequiredFieldValidator>  
                    </td>
                                                                                  
                   
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Contract Period :</td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;"> <asp:TextBox ID="txtContractPeriod" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtContractPeriodResource1" TabIndex="8"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContractPeriod" FilterType="Numbers"  ></ajaxToolkit:FilteredTextBoxExtender>
                        &nbsp;
                        <asp:RequiredFieldValidator runat="server" ID="rfvContractPeriod" ControlToValidate="txtContractPeriod" ErrorMessage="Required."></asp:RequiredFieldValidator>  
                    </td>
                    
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;"> Age Limit :</td>
                    <td style="text-align:left;padding-left:5px;width:30%;padding:3px;">
                         <asp:TextBox ID="txtAgelimit" runat="server" CssClass="input_box" Width="145px" MaxLength="9" meta:resourcekey="txtAgelimitResource1" TabIndex="9"></asp:TextBox>  &nbsp;
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAgelimit" FilterType="Numbers"  ></ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator runat="server" ID="rfvAgeLimit" ControlToValidate="txtAgelimit" ErrorMessage="Required."></asp:RequiredFieldValidator>&nbsp;
                        <asp:RangeValidator ID="rvtxtAgelimit" runat="server" ControlToValidate="txtAgelimit"
                                                                                        ErrorMessage="Age Limit > 18 yrs." MaximumValue="50" MinimumValue="18" Type="Integer"></asp:RangeValidator>
                    </td>
                     <td style="text-align:left;padding:3px;width:40%;padding-left:100px;" colspan="2" >
                         US Visa : &nbsp;&nbsp; <asp:CheckBox ID="chk_UsVisa" runat="server" TabIndex="12" />
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    Schengen visa : &nbsp;&nbsp;
                                                                    <asp:CheckBox ID="chk_SchengenVisa" runat="server" TabIndex="13" />
                    </td>
                    
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:5px;padding:3px;width:15%;" > Rank Exp. (Vessel Type) :</td>
                    <td style="text-align:left;padding-left:5px;padding:3px;width:30%;" >
                        <asp:TextBox ID="txtRankExpV" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtRankExpVResource1" TabIndex="10"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtRankExpV" FilterType="Numbers"  ></ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RangeValidator ID="rvtxtRankExpV" runat="server" ControlToValidate="txtRankExpV"
                                                                                        ErrorMessage="Exp. between 0 to 40 yrs." MaximumValue="40" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                    </td>
                     <td style="text-align:right;padding-right:5px;padding:3px;width:15%;" >Rank Exp. (Total) : </td>
                    <td style="text-align:left;padding-left:5px;padding:3px;width:25%;" ><asp:TextBox ID="txtRankExpT" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtRankExpTResource1" TabIndex="11"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtRankExpT" FilterType="Numbers"  ></ajaxToolkit:FilteredTextBoxExtender>
                         <asp:RangeValidator ID="rvtxtRankExpT" runat="server" ControlToValidate="txtRankExpT"
                                                                                        ErrorMessage="Total Exp. between 0 to 40 yrs." MaximumValue="40" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                    </td>
                   
                    <%--<td style="text-align:left;padding-left:5px;width:20%;padding:5px;"></td>--%>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;height:70px;">Nationality Office : </td>
                    <td style="text-align:left;padding-left:5px;width:30%;padding:3px;">
                         <div style="width:200px;height:59px;overflow-y:auto;">  <asp:CheckBoxList ID="ddcrew_nationality" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal" TabIndex="9" Width="175px" Font-Size="11px">
                                            </asp:CheckBoxList> </div>
                    </td>
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Nationality Rating : </td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;">
                         <div style="width:200px;height:59px;overflow-y:auto;">
                                                         <asp:CheckBoxList ID="ddcrew_nationality1" runat="server" CellPadding="0" CellSpacing="0" Font-Size="11px" RepeatColumns="1" RepeatDirection="Horizontal" TabIndex="12" Width="175px">
                                                         </asp:CheckBoxList>
                                                     </div>
                    </td>
                   
                </tr>
                <tr>
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Recruiter Office : </td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;"><asp:DropDownList ID="ddl_Recr_Office" runat="server" CssClass="form-control" Width="165px" TabIndex="16">
                                                                    </asp:DropDownList> &nbsp;
                        <asp:RequiredFieldValidator id="rfvddl_Recr_Office" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Recr_Office" meta:resourcekey="ddl_Recr_OfficeResource1" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Recruiter Name : </td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;"> 
                        <asp:DropDownList ID="ddlRecruiterName" runat="server" CssClass="form-control" Width="165px" TabIndex="17">
                                                                        
                                                                    </asp:DropDownList> &nbsp;
                        <asp:RequiredFieldValidator id="rfvddlRecruiterName" runat="server" ErrorMessage="Required." ControlToValidate="ddlRecruiterName" meta:resourcekey="ddlRecruiterNameResource1" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
               <tr>
                    
                     <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Vacancy Issue Date : </td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;"> <asp:TextBox ID="txtIssueDate" runat="server" CssClass="required_box" Width="140px" ReadOnly="true" TabIndex="18"></asp:TextBox></td>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Vacancy Status : </td>
                    <td style="text-align:left;padding-left:5px;width:25%;padding:3px;">
                        <asp:DropDownList ID="ddlVStatus" runat="server" Width="165px" TabIndex="19">
                            <asp:ListItem Selected="True" Value="1" Text="Open"></asp:ListItem>
                            <asp:ListItem  Value="0" Text="Closed"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;padding-right:5px;width:15%;padding:3px;">Vacancy Notes : </td>
                    <td style="text-align:left;padding-left:5px;width:65%;padding:3px;" colspan="3">
                        <asp:TextBox ID="txtVacancyNotes" runat="server" Width="939px" TextMode="MultiLine" Height="50px" MaxLength="500" TabIndex="20"></asp:TextBox>
                    </td>
                     
                   
                </tr>
               <tr>
                    <td style="text-align:center;padding:3px;" colspan="2">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                   <td colspan="2" style="text-align:left;padding:3px;padding-left:10px;">
                        <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save"  CssClass="btn" TabIndex="21" />&nbsp;
                       <asp:Button ID="btnClear" runat="server"  Text="Clear"  CssClass="btn" TabIndex="22" CausesValidation="false"  OnClick="btnClear_Click" />&nbsp;
                        <asp:Button ID="btnClose" runat="server"  Text="Close"  CssClass="btn" TabIndex="23" CausesValidation="false" OnClick="btnClose_Click" />&nbsp;
                   </td>
                    
                </tr>
            </table>
                    </fieldset>
                <asp:HiddenField ID="hdnVacancyId" runat="server" />
                <asp:HiddenField ID="hdnNationalityGrp" runat="server" />
                    <asp:HiddenField ID="hdnNationalityGrpRat" runat="server" />
                </center>
            </center>
            
        </div>
    </form>
</body>
</html>
