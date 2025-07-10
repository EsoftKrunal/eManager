<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewPersonalDetails.aspx.cs" Inherits="CrewPersonalDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ShipSoft::Candidate Details</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
     <div style="text-align: center">
    <table width="999" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td style="background-image:url(../images/index_03.jpg); background-repeat:repeat-x"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="17%" align="right"><img src="../Images/Logo/CompanyLogo.jpg"   alt="" width="145" height="121" />
       
        </td>
        <td width="83%" align="right">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="26" align="right" style="background-image:url(../images/index_06.jpg); background-repeat:repeat-x; padding-right:10px">Welcome Guest</td>
  </tr>
  <tr>
    <td   valign="top" ><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td style="padding-left:23px">&nbsp;</td>
      </tr>
      <tr><td align="center" style="width:100%; height: 26px;">
      <strong><asp:Label ID="lbl_otherdoc_message" runat="server" ForeColor="Red"></asp:Label><br />
      </strong></td></tr>
     
      <tr>
        <td  valign="top" align="center" style="width:100%" >
        <table cellpadding="0" cellspacing="0" border="0"  style="width: 825px ;border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
        <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >
                   Candidate Details&nbsp;
                </td>
            </tr>
        <tr><td style="width:90%;">
                <%--<fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">--%>
                 
                   <br />
                  
            <table style="width: 100%" cellpadding="0" cellspacing="0"> 
           
         
                           <tr><td colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 0px; height: 153px;">   <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                            <legend><strong></strong></legend><table cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-left: 5px">
                            <tr>&nbsp;
                            </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                        First Name:</td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                    <asp:TextBox ID="txt_fname" runat="server" CssClass="required_box" MaxLength="25" Width="130px" TabIndex="1"></asp:TextBox></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                        Middle Name:</td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                    <asp:TextBox ID="txt_mname" runat="server" CssClass="input_box" MaxLength="25" Width="130px" TabIndex="2"></asp:TextBox></td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                                        Last Name:</td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                    <asp:TextBox ID="txt_lname" runat="server" CssClass="required_box" MaxLength="25" Width="130px" TabIndex="3"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                    </td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_fname"
                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                                    </td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                                    </td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_lname"
                        ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                        Nationality:</td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                                        <asp:DropDownList ID="dd_nationality" runat="server" Width="140px" CssClass="required_box" TabIndex="4">
                    </asp:DropDownList></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                        DOB:</td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                        <asp:TextBox ID="txtdob" runat="server" CssClass="required_box" MaxLength="20" Width="114px" TabIndex="5"></asp:TextBox>
                                        <asp:ImageButton ID="img_dob" runat="server" CausesValidation="false"
                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                        POB:</td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_POB" runat="server" CssClass="required_box" MaxLength="30" Width="130px" TabIndex="6"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                    </td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dd_nationality"
                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtdob"
                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                    <td style="width: 28%; height: 15px; text-align: left" colspan="2">
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtdob"
                                            ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtdob"
                                            ErrorMessage="Less Than Current Date" Operator="LessThan" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_POB"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                        Gender:</td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        <asp:DropDownList ID="ddl_gender" runat="server" Width="140px" CssClass="required_box" TabIndex="7">
                            <asp:ListItem Value="-1">&lt;Select&gt;</asp:ListItem>
                            <asp:ListItem Value="1">Male</asp:ListItem>
                            <asp:ListItem Value="2">Female</asp:ListItem>
                        </asp:DropDownList></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                        Rank Applied:</td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                        <asp:DropDownList ID="ddl_Rank" runat="server" Width="185px" CssClass="required_box" TabIndex="8">
                           
                        </asp:DropDownList></td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                        Available From:</td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_AvailableFrom" runat="server" CssClass="input_box" MaxLength="25" Width="106px" TabIndex="9"></asp:TextBox>
                        <asp:ImageButton ID="img_availablefrom" runat="server" CausesValidation="false"
                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                    </td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddl_gender"
                            ErrorMessage="Required" Operator="NotEqual" Type="Integer" ValueToCompare="-1"></asp:CompareValidator></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddl_Rank"
                            ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                    <td class="text-1" style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                                        Country Code Area Code&nbsp;
                        Number</td>
                                    <td align="right" colspan="2" style="padding-left: 5px; width: 22%; height: 15px;
                                        text-align: right">
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txt_AvailableFrom"
                            ErrorMessage="Greater Than Today Date" Operator="GreaterThan" Type="Date" Width="153px"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_AvailableFrom"
                            ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"
                            Width="78px"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                        Qualification:</td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        <asp:DropDownList ID="ddl_Qualification" runat="server" Width="140px" CssClass="input_box" TabIndex="10">
                        </asp:DropDownList></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                        Contact No:</td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_ccode" runat="server" CssClass="input_box" MaxLength="5" Width="42px" TabIndex="11"></asp:TextBox>-<asp:TextBox
                            ID="txt_acode" runat="server" CssClass="input_box" MaxLength="5" Width="42px" TabIndex="12"></asp:TextBox>-<asp:TextBox
                                ID="txt_telno" runat="server" CssClass="input_box" MaxLength="15" Width="80px" TabIndex="13"></asp:TextBox></td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                        Email Id:</td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_emailid" runat="server" CssClass="input_box" MaxLength="100" Width="130px" TabIndex="14"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                    </td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                                    </td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                                    </td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                                    </td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                                    </td>
                                </tr>
                <tr>
                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                        Passport #:</td>
                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_PassportNo" runat="server" CssClass="required_box" TabIndex="15"></asp:TextBox></td>
                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                        Issue Date.:</td>
                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_IssueDt" runat="server" CssClass="input_box" Width="110px" TabIndex="16"></asp:TextBox>
                        <asp:ImageButton ID="img_IssueDt" runat="server" CausesValidation="false"
                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                        Expiry Date.:</td>
                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                        <asp:TextBox ID="txt_ExpiryDt" runat="server" CssClass="input_box" Width="110px" TabIndex="17"></asp:TextBox>
                        <asp:ImageButton ID="img_ExpiryDt" runat="server" CausesValidation="false"
                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                    </td>
                                    <td rowspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_PassportNo"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                    <td align="right" style="padding-left: 5px; width: 12%; height: 15px">
                                    </td>
                                    <td style="padding-left: 5px; width: 26%; height: 15px; text-align: left">
                                        <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txt_IssueDt"
                                            ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                                        <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txt_ExpiryDt"
                                            ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                    </td>
                                    <td colspan="3" rowspan="1" style="padding-left: 5px; width: 22%; height: 15px; text-align: right">
                                        <asp:CompareValidator ID="PassportDateCheck" runat="server" ControlToCompare="txt_IssueDt"
                                            ControlToValidate="txt_ExpiryDt" Display="Static" ErrorMessage="Issue Date Must be Less Than Expiry Date."
                                            Operator="GreaterThanEqual" Type="Date" Width="251px"></asp:CompareValidator></td>
                                    <td colspan="2" style="padding-left: 5px; height: 15px; text-align: left">
                                    </td>
                                </tr>
           <%--     <tr>
                    <td align="right" style="width: 17%; height: 15px; text-align: right">
                    </td>
                    <td rowspan="1" style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                        </td>
                    <td align="right" style="width: 17%; height: 15px">
                    </td>
                    <td style="padding-left: 5px; width: 18%; height: 15px; text-align: left" colspan="2">
                        &nbsp;</td>
                    <td colspan="1" style="padding-left: 5px; width: 18%; height: 15px; text-align: left">
                        </td>
                </tr>--%>
             </table></fieldset></td></tr>
            <tr><td colspan="6" align="right" style="padding-right: 10px; padding-left: 10px; padding-bottom: 0px;padding-top: 0px;">
                &nbsp;&nbsp;&nbsp;
            </td></tr>
                <tr><td colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 0px;padding-top: 0px;" align="left" valign="middle"><asp:Button ID="Button2" runat="server" Text="Add Experience" CssClass="btn" OnClick="Button2_Click" CausesValidation="False" Width="107px" TabIndex="18" /><br />
                    &nbsp;</td></tr>                  
                <tr id="trgvex" runat="server"><td colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 0px;padding-top: 0px;"> <%--<asp:Panel ID="pnl_Experience" runat="server" Visible="true">--%>
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                            <legend><strong>Experience</strong></legend>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-bottom: 5px; padding-top: 5px; text-align: center" colspan="">
                                    <asp:Label ID="lblgvexp" runat="server"></asp:Label>
                                        <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Auto" Width="800px">
                                            <asp:GridView ID="gvexperience" runat="server" AutoGenerateColumns="False" DataKeyNames="CandidateExperienceId"
                                                Height="9px" meta:resourcekey="GridView3_1Resource1"  GridLines="Horizontal"
                                                PageSize="3" Style="text-align: center" Width="1200px" OnRowDeleting="gvexperience_RowDeleting" OnRowEditing="gvexperience_RowEditing" OnSelectedIndexChanged="gvexperience_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" HeaderText="View" meta:resourcekey="CommandFieldResource1"
                                                        SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" />
                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                        ShowEditButton="True">
                                                        <ItemStyle Width="30px" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                        <ItemStyle Width="30px" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                Text="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name" meta:resourcekey="TemplateFieldResource2">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompanyName" runat="server" meta:resourcekey="lblCompanyNameResource2"
                                                                Text='<%# Eval("CompanyName") %>'></asp:Label>
                                                                                                                 </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RankName" HeaderText="Rank" meta:resourcekey="BoundFieldResource7">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VesselName" HeaderText="Vessel Name" meta:resourcekey="BoundFieldResource11">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VesselType" HeaderText="Vessel Type" meta:resourcekey="BoundFieldResource12">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SignOn" HeaderText="Sign On Dt." meta:resourcekey="BoundFieldResource8">
                                                        <ItemStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SignOff" HeaderText="Sign Off Dt." meta:resourcekey="BoundFieldResource9">
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                          <asp:BoundField DataField="Registry" HeaderText="Registry" meta:resourcekey="BoundFieldResource9">
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                   
                                                          <asp:BoundField DataField="DWT" HeaderText="DWT" meta:resourcekey="BoundFieldResource9">
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GWT" HeaderText="GRT">
                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BHP" HeaderText="BHP(Kw)">
                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SOFReason" HeaderText="Sign Off Reason">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                            </asp:GridView>
                                           
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                   <%-- </asp:Panel>--%></td></tr>
                <tr id="trexperience" runat="server">
                    <td align="right" colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 0px;padding-top: 10px;">
                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                            <legend><strong> Experience</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="0" style="height: 169px; text-align: right; width: 100%;">
                                <tr>
                                    <td align="right" style="width: 183px; height: 7px">
                                    </td>
                                    <td style="width: 186px; height: 7px; text-align: left">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 251px; height: 7px; text-align: left">
                                    </td>
                                    <td style="width: 166px; height: 7px">
                                    </td>
                                    <td align="right" style="width: 168px; height: 7px">
                                    </td>
                                    <td style="width: 202px; height: 7px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 2px">
                                        <asp:Label ID="Label24" runat="server" meta:resourcekey="Label24Resource1" Text="Company Name:"></asp:Label></td>
                                    <td style="width: 186px; height: 2px; text-align: left; padding-left: 5px;">
                                        <asp:TextBox ID="txt_compname" runat="server" CssClass="required_box" MaxLength="49"
                                            meta:resourcekey="txtcompnameResource1" TabIndex="19" Width="130px"></asp:TextBox></td>
                                    <td style="width: 251px; height: 2px; text-align: right">
                                        <asp:Label ID="Label29" runat="server" meta:resourcekey="Label29Resource1" Text="Vessel Name:"></asp:Label></td>
                                    <td style="width: 166px; height: 2px; text-align: left; padding-left: 5px;">
                                        <asp:TextBox ID="txt_vesselname" runat="server" CssClass="required_box" MaxLength="49"
                                            meta:resourcekey="txtvesselnameResource1" TabIndex="20" Width="130px"></asp:TextBox></td>
                                    <td align="right" style="width: 168px; height: 2px">
                                        <asp:Label ID="Label28" runat="server" meta:resourcekey="Label28Resource1" Text="Vessel Type:"
                                            Width="82px"></asp:Label></td>
                                    <td style="width: 202px; height: 2px; text-align: left; padding-left: 5px;">
                                        <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="required_box" meta:resourcekey="ddrankappResource1"
                                            TabIndex="21" Width="135px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px;">
                                    </td>
                                    <td style="width: 186px; text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_compname"
                                            ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator9Resource1"
                                            ValidationGroup="aa"></asp:RequiredFieldValidator></td>
                                    <td style="width: 251px; text-align: left">
                                    </td>
                                    <td style="width: 166px; text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_vesselname"
                                            ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator9Resource1" ValidationGroup="aa"></asp:RequiredFieldValidator></td>
                                    <td align="right" style="width: 168px;">
                                    </td>
                                    <td style="width: 202px; text-align: left">
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddl_VesselType"
                                            ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0" ValidationGroup="aa"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 28px;">
                                        <asp:Label ID="Label26" runat="server" meta:resourcekey="Label26Resource1" Text="Sign On Dt:"
                                            Width="108px"></asp:Label></td>
                                    <td style="width: 186px; text-align: left; height: 28px; padding-left: 5px;">
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="required_box" MaxLength="10"
                                            meta:resourcekey="txtfromdateResource1" TabIndex="22" Width="114px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                            TabIndex="77" CausesValidation="False" /></td>
                                    <td style="text-align: right; height: 28px; width: 251px;">
                                        <asp:Label ID="Label27" runat="server" meta:resourcekey="Label27Resource1" Text="Sign Off Dt:"
                                            Width="100%"></asp:Label></td>
                                    <td style="width: 166px; text-align: left; height: 28px; padding-left: 5px;">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="required_box" MaxLength="10"
                                            meta:resourcekey="txttodateResource1" TabIndex="23" Width="114px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                            TabIndex="79" CausesValidation="False" /></td>
                                    <td align="right" style="text-align: right; height: 28px; width: 168px;">
                                        <asp:Label ID="Label25" runat="server" meta:resourcekey="Label25Resource1" Text="Rank:"></asp:Label></td>
                                    <td style="width: 202px; text-align: left; height: 28px; padding-left: 5px;">
                                        <asp:DropDownList ID="ddl_Rank1" runat="server" CssClass="input_box" meta:resourcekey="ddrankappResource1"
                                            TabIndex="24" Width="135px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 5px">
                                    </td>
                                    <td style="width: 186px; height: 5px; text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtfromdate"
                                            ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator7Resource1" ValidationGroup="aa"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtfromdate"
                                            ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                    <td style="width: 251px; height: 5px; text-align: left">
                                    </td>
                                    <td style="width: 166px; height: 5px; text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txttodate"
                                            ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator8Resource1" ValidationGroup="aa"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txttodate"
                                            ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                    <td align="right" style="width: 168px; height: 5px">
                                    </td>
                                    <td style="width: 202px; height: 5px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 19px">
                                        <asp:Label ID="Label30" runat="server" meta:resourcekey="Label30Resource1" Text="Registry:"
                                            Width="64px"></asp:Label></td>
                                    <td style="width: 186px; height: 19px; text-align: left; padding-left: 5px;">
                                        <asp:TextBox ID="txtregistry" runat="server" CssClass="input_box" MaxLength="49"
                                            meta:resourcekey="txtregistryResource1" TabIndex="25" Width="130px"></asp:TextBox></td>
                                    <td style="width: 251px; height: 19px; text-align: right">
                                        <asp:Label ID="Label31" runat="server" meta:resourcekey="Label31Resource1" Text="DWT:"></asp:Label></td>
                                    <td style="width: 166px; height: 19px; text-align: left; padding-left: 5px;">
                                        <asp:TextBox ID="txtdwt" runat="server" CssClass="input_box" MaxLength="49" meta:resourcekey="txtdwtResource1"
                                            TabIndex="26" Width="130px"></asp:TextBox></td>
                                    <td align="right" style="height: 19px; text-align: right; width: 168px;">
                                        <asp:Label ID="Label32" runat="server" meta:resourcekey="Label32Resource1" Text="GRT:"
                                            Width="41px"></asp:Label></td>
                                    <td style="width: 202px; height: 19px; text-align: left; padding-left: 5px;">
                                        <asp:TextBox ID="txtgwt" runat="server" CssClass="input_box" MaxLength="49" meta:resourcekey="txtgwtResource1"
                                            TabIndex="27" Width="130px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 13px">
                                    </td>
                                    <td style="width: 186px; height: 13px; text-align: left">
                                    </td>
                                    <td style="width: 251px; height: 13px; text-align: left">
                                    </td>
                                    <td style="width: 166px; height: 13px">
                                    </td>
                                    <td align="right" style="width: 168px; height: 13px">
                                    </td>
                                    <td style="width: 202px; height: 13px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px">
                                        <asp:Label ID="Label33" runat="server" meta:resourcekey="Label33Resource1" Text="BHP:"></asp:Label></td>
                                    <td style="width: 186px; text-align: left; padding-left: 5px;">
                                        <asp:TextBox ID="txtbhp" runat="server" CssClass="input_box" MaxLength="49" meta:resourcekey="txtbhpResource1"
                                            TabIndex="28" Width="130px"></asp:TextBox></td>
                                    <td style="width: 251px; text-align: right">
                                        Sign Off Reason:</td>
                                    <td align="right" colspan="2" style="text-align: left; padding-left: 5px;">
                                        <asp:DropDownList ID="ddl_Sign_Off_Reason" runat="server" CssClass="required_box"
                                            meta:resourcekey="ddrankappResource1" TabIndex="29" Width="203px">
                                        </asp:DropDownList></td>
                                    <td style="width: 202px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 16px;">
                                    </td>
                                    <td style="width: 186px; text-align: left; height: 16px;">
                                        &nbsp;</td>
                                    <td style="width: 251px; text-align: right; height: 16px;">
                                    </td>
                                    <td style="width: 166px; text-align: left; height: 16px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddl_Sign_Off_Reason"
                                            ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator7Resource1" ValidationGroup="aa"></asp:RequiredFieldValidator></td>
                                    <td align="right" style="width: 168px; height: 16px;">
                                    </td>
                                    <td style="width: 202px; text-align: left; height: 16px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 183px; height: 6px">
                                    </td>
                                    <td colspan="3" style="height: 6px">
                                        <asp:CompareValidator ID="cvStartDateLessThanEndDate" runat="server" ControlToCompare="txtfromdate" ValidationGroup="aa"
                                            ControlToValidate="txttodate" Display="Static" ErrorMessage="Signed On. Date Must be Less Than Signed Off. Date."
                                            Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                        &nbsp;
                                    </td>
                                    <td align="right" colspan="2" style="height: 6px; text-align: center">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                        &nbsp;<br />
                        <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn" OnClick="Button1_Click" ValidationGroup="aa" Width="59px" TabIndex="30" />
                        <asp:Button ID="btn_Reset1" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" OnClick="btn_Reset1_Click" ValidationGroup="aa" Width="59px" TabIndex="31" />
                    </td>
                </tr>
               
                <tr><td><table cellpadding="0" cellspacing="0" >
                <tr><td align="left" colspan="6" style="padding-right: 10px; padding-left: 5px; padding-top: 13px; padding-bottom: 0px;"><asp:Button ID="btn_AddCargo" runat="server" Text="Add Endorsments" CssClass="btn" OnClick="btn_AddCargo_Click" CausesValidation="False" Width="107px" /><br />
                    &nbsp;</td></tr>
                                                        <tr>
                                                            <td align="center" colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;" valign="top">
                                                            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Dangerous Cargo Endorsement</strong></legend>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                        <tr><td style=" padding-top:5px;" align="center">
                                                                         <div style=" width:800px; height:110px; overflow:auto" > 
                                                                            <asp:GridView ID="GvDCE"  runat="server" AutoGenerateColumns="False" PageSize="3" Style="text-align: center" Width="1200px"
                                                                                GridLines="Horizontal" OnPreRender="GvDCE_PreRender" DataKeyNames="CandidateDCEId" OnRowDeleting="GvDCE_RowDeleting" OnRowEditing="GvDCE_RowEditing" OnSelectedIndexChanged="GvDCE_SelectedIndexChanged" Height="9px" >
                                                                               <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                                <Columns>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                                        ShowSelectButton="True">
                                                                                        <ItemStyle Width="50px" />
                                                                                    </asp:CommandField>
                                                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                                        ShowEditButton="True">
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:CommandField>
                                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                        <ItemStyle Width="30px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                                Text="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Cargo Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblcargoname" runat="server" Text='<%#Eval("DCE")%>'></asp:Label>
                                                                                                                                                                                       
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Number" HeaderText="Number">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="nationality" HeaderText="Nationality">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="GradeLevel" HeaderText="Grade Level">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Issue">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <asp:Label ID="lbl_GridView_cargo" runat="server" Width="127px"></asp:Label>
                                                                        </div>
                                                                        </td></tr>
                                                                        </table>
                                                                    </fieldset>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr id="trdce" runat="server"><td colspan="6"><table>
                                                        <tr>
                                                            <td align="right" colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top:10px">
                                                                
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Dangerous Cargo Endorsement</strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 102px">
                                                                            <tr>
                                                                                    <td colspan="6">
                                                                                    &nbsp; &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 208px; height: 1px;">
                                                                                    Dangerous Cargo Endorsements:</td>
                                                                                <td align="left" style="width: 128px; height: 1px; padding-left: 5px;">
                                                                <asp:DropDownList ID="ddDCEcargoname" runat="server" CssClass="required_box" TabIndex="32" Width="135px"> <asp:ListItem Value=" ">&lt; Select &gt;</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                                                <td align="right" style="width: 100px; height: 1px;">
                                                                                    Number:</td>
                                                                                <td align="left" style="padding-left: 5px; width: 139px; height: 1px">
                                                                                    <asp:TextBox ID="txt_cargo_number" runat="server" CssClass="input_box" MaxLength="19" TabIndex="33"
                                                                                        Width="130px"></asp:TextBox></td>
                                                                                <td align="right" style="width: 83px; height: 1px">
                                                                                    Nationality:</td>
                                                                                <td align="left" style="width: 134px; height: 1px; padding-left: 5px;">
                                                                                    <asp:DropDownList ID="ddDCEnationality" runat="server" CssClass="required_box" TabIndex="34"
                                                                                        Width="123px">
                                                                                        <asp:ListItem Value=" ">&lt; Select &gt;</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td align="center" style="height: 1px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 208px; height: 13px;">
                                                                                </td>
                                                                                <td align="left" style="width: 128px; height: 13px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddDCEcargoname"
                                                                                        ErrorMessage="Required." ValidationGroup="dce"></asp:RequiredFieldValidator></td>
                                                                                <td align="right" style="width: 100px; height: 13px;">
                                                                                </td>
                                                                                <td align="right" style="width: 139px; height: 13px;">
                                                                                </td>
                                                                                <td align="right" style="width: 83px; height: 13px;">
                                                                                </td>
                                                                                <td align="left" style="width: 134px; height: 13px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddDCEnationality"
                                                                                        ErrorMessage="Required." ValidationGroup="dce"></asp:RequiredFieldValidator></td>
                                                                                <td align="center" style="height: 13px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="height: 16px; width: 208px;">
                                                                                    Grade / Level I / II:</td>
                                                                                <td align="left" style="width: 128px; height: 16px; padding-left: 5px;">
                                                                                    <asp:TextBox ID="txt_cargo_gradelevel" runat="server" CssClass="input_box" MaxLength="9"
                                                                                        TabIndex="35" Width="130px"></asp:TextBox></td>
                                                                                <td align="right" style="width: 100px; height: 16px;">
                                                                                    Place Of Issue:</td>
                                                                                <td align="left" style="padding-left: 5px; width: 139px; height: 16px">
                                                                                    <asp:TextBox ID="txt_cargo_PlaceOfIssue" runat="server" CssClass="input_box" MaxLength="49" TabIndex="36"
                                                                                        Width="130px"></asp:TextBox></td>
                                                                                <td align="right" style="width: 83px; height: 16px">
                                                                                    </td>
                                                                                <td align="left" style="width: 134px; height: 16px; padding-left: 5px;">
                                                                                    &nbsp;</td>
                                                                                <td align="right" style="height: 16px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 208px; height: 17px">
                                                                                </td>
                                                                                <td align="left" style="width: 128px">
                                                                                </td>
                                                                                <td align="right" style="width: 100px">
                                                                                    &nbsp;</td>
                                                                                <td align="right" style="width: 139px">
                                                                                </td>
                                                                                <td align="right" style="width: 83px">
                                                                                </td>
                                                                                <td align="left" style="width: 134px">
                                                                                </td>
                                                                                <td align="right" style="height: 17px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 208px; height: 8px;">
                                                                                    DateOf Issue:</td>
                                                                                <td align="left" style="width: 128px; padding-left: 5px; height: 8px;">
                                                                                    <asp:TextBox ID="txt_Cargo_DOI" runat="server" CssClass="input_box" MaxLength="10"
                                                                                        TabIndex="37" Width="104px"></asp:TextBox>
                                                                                    <asp:ImageButton ID="Imagedoi" runat="server"
                                                                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False" /></td>
                                                                                <td align="right" style="width: 100px; height: 8px;">
                                                                                    Expiry Date:</td>
                                                                                <td align="left" style="width: 139px; padding-left: 5px; height: 8px;">
                                                                                    <asp:TextBox ID="txt_cargo_expiry" runat="server" CssClass="input_box" MaxLength="10"
                                                                                        TabIndex="38" Width="106px"></asp:TextBox>
                                                                                    <asp:ImageButton ID="Imageexpiry"
                                                                                            runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False" /></td>
                                                                                <td align="right" style="width: 83px; height: 8px;">
                                                                                </td>
                                                                                <td align="left" style="width: 134px; height: 8px;">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 208px; height: 17px">
                                                                                </td>
                                                                                <td align="left" style="padding-left: 5px; width: 128px; height: 17px">
                                                                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txt_Cargo_DOI"
                                                                                        ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                                                                <td align="right" style="width: 100px; height: 17px">
                                                                                </td>
                                                                                <td align="left" style="padding-left: 5px; width: 139px; height: 17px">
                                                                                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txt_cargo_expiry"
                                                                                        ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0"></asp:CompareValidator></td>
                                                                                <td align="right" style="width: 83px; height: 17px">
                                                                                </td>
                                                                                <td align="left" style="width: 134px; height: 17px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 208px; height: 17px">
                                                                                </td>
                                                                                <td align="left" style="padding-left: 5px; width: 128px; height: 17px">
                                                                                </td>
                                                                                <td align="right" colspan="3" style="width: 192px; height: 17px">
                                                                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToCompare="txt_Cargo_DOI"
                                                                                        ControlToValidate="txt_cargo_expiry" Display="Static" ErrorMessage="Issue Date Must be Less Than Expiry Date"
                                                                                        Operator="GreaterThanEqual" Type="Date" Width="321px"></asp:CompareValidator></td>
                                                                                <td align="right" style="width: 121px; height: 17px">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
                                                                <br />
                                                                <asp:Button ID="btn_cargo_save" runat="server" CssClass="btn"
                                                                    Text="Save"  Width="59px" TabIndex="39" OnClick="btn_cargo_save_Click" ValidationGroup="dce" />
                                                                <asp:Button ID="btn_cargo_cancel" runat="server" CausesValidation="false" CssClass="btn" Text="Cancel" Width="59px" TabIndex="40" OnClick="btn_cargo_cancel_Click" /></td>
                                                        </tr>
                                                        </table></td></tr>
                    <tr runat="server" id="Tr1">
                        <td align="right" colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top:5px">
                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-right: 5px; padding-bottom: 5px; padding-top: 5px;">
                        <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Send" CssClass="btn" Width="59px" TabIndex="41" />
                        <asp:Button ID="btn_Reset" runat="server" CssClass="btn" Text="Reset" OnClick="btn_Reset_Click" CausesValidation="False" Width="59px" TabIndex="42" /></fieldset>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr2">
                        <td align="right" colspan="6" style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px;
                            padding-top: 5px">
                            <table cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-left: 5px">
                                <tr>
                                    <td align="right" style="width: 12%; height: 15px; text-align: right">
                                        Attach CV:</td>
                                    <td colspan="3" rowspan="1" style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input_box" Width="438px" TabIndex="43" /></td>
                                    <td align="right" style="padding-left: 5px; width: 13%; height: 15px; text-align: right">
                                    </td>
                                    <td style="padding-left: 5px; width: 22%; height: 15px; text-align: left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                                                  
                                                    </table></td></tr>
                </table>
                   
                <%--</fieldset>--%></td></tr>
                
                
            </table>
       </td>
          
      </tr>
      <tr>
                    <td align="right" colspan="3" style="width: 14%; height: 21px; text-align: right" valign="middle">
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="img_dob" PopupPosition="TopLeft" TargetControlID="txtdob">
                        </ajaxToolkit:CalendarExtender>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="Imageexpiry" PopupPosition="TopRight" TargetControlID="txt_cargo_expiry">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtfromdate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AutoComplete="true"
                                            ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfromdate">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AutoComplete="true"
                                            ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txttodate">
                                        </ajaxToolkit:MaskedEditExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender
           ID="FilteredTextBoxExtender1"
           runat="server"
           TargetControlID="txt_ccode"
           FilterType="Numbers"/>
                        <ajaxToolkit:FilteredTextBoxExtender
           ID="FilteredTextBoxExtender2"
           runat="server"
           TargetControlID="txt_telno"
           FilterType="Numbers"/>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txttodate">
                                        </ajaxToolkit:CalendarExtender>
                                                                                <asp:HiddenField ID="Hidden_cargo" runat="server" Value="" />
                                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" AutoComplete="true"
                                            ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Cargo_DOI">
                                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="Imagedoi" PopupPosition="TopRight" TargetControlID="txt_Cargo_DOI">
                                                                                    </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:FilteredTextBoxExtender
           ID="FilteredTextBoxExtender3"
           runat="server"
           TargetControlID="txt_acode"
           FilterType="Numbers"/>
                                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AutoComplete="true"
                                            ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_cargo_expiry">
                                                                                    </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="true"
                            ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdob">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:HiddenField ID="hcandidateid" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="img_availablefrom" PopupPosition="TopLeft" TargetControlID="txt_AvailableFrom">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"
                            ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_AvailableFrom">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="img_IssueDt" PopupPosition="TopRight" TargetControlID="txt_IssueDt">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" AutoComplete="false"
                            ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txt_IssueDt">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="img_ExpiryDt" PopupPosition="TopRight" TargetControlID="txt_ExpiryDt">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" AutoComplete="false"
                            ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txt_ExpiryDt">
                        </ajaxToolkit:MaskedEditExtender>
                       </td>
                </tr>
      <tr>
        <td align="center" bgcolor="#4673a7" class="text_bottom" style="height: 16px">Copyright &copy;2008 MTM Ship Management Ltd. All rights reserved.</td>
      </tr>
      </table></td>
  </tr>
 
</table></div>
    </form>
</body>
</html>
