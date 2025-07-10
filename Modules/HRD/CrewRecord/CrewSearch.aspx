<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewSearch.aspx.cs" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="CrewSearch" %>

<%--<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <%--  <link rel="stylesheet" type="text/css" href="../Styles/style.css" />
   <link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="margin: 0 0 0 0;">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <%--<form id="form1" runat="server" defaultbutton="btn_Search" >--%>
    <div style="text-align: left">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
                        <tr>
                            <td style="text-align: center; background-color: #4c7a6f; color: #fff; font-size: 14px;" class="text headerband">
                                <div style="float: left" id="div_Post">
                                    <asp:Button ID="Button3" runat='server' Text="Crew DB" OnClick="CrewDb_Click" BackColor="White" Font-Size="11px" /></div>
                                <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
                                Crew Data
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="background-color: #f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-right: 10px; text-align: center; color: Red">
                                            <asp:Label ID="lblmessage" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                                <tr>
                                                    <td >
                                                        <table border="0" cellspacing="0" cellpadding="0px" width="100%" >
                                                            <tr>
                                                                <td style="text-align: right;">Crew Number :
                                                                </td>
                                                                <td >
                                                                    <asp:TextBox ID="txt_MemberId_Search" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Width="160px" TabIndex="1"></asp:TextBox></td>
                                                                <td  style="text-align: right;">Crew Name :
                                                                </td>
                                                                <td colspan="4" >
                                                                    <asp:TextBox ID="txt_FirstName_Search" runat="server" CssClass="form-control" MaxLength="24"
                                                                        Width="160px" TabIndex="2"></asp:TextBox></td>
                                                                <td style="text-align: right;">Rank :</td>
                                                                <td >
                                                                    <%--<asp:TextBox ID="txt_LastName_Search" runat="server" CssClass="form-control" MaxLength="24"
                                                                        Width="160px" TabIndex="3"></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="form-control" Width="165px" TabIndex="4">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;"> Nationality : </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="input_box" Width="165px" TabIndex="7">
                                                                        <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                <td style="text-align: right;">Vessel Type : </td>
                                                                <td colspan="4"> <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="form-control" Width="165px" TabIndex="17">
                                                                    </asp:DropDownList> 
                                                                </td>
                                                                
                                                                <td style="text-align: right;">  <asp:Label ID="Label3" runat="server" Text="Vessel :"></asp:Label> </td>
                                                                <td>
                                                                     <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="form-control" Width="165px" TabIndex="10" AutoPostBack="true" OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged">
                                                                        <asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    Owner :
                                                                    
                                                                </td>
                                                                <td>
                                                                     <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="form-control" Width="165px" TabIndex="15">
                                                                        <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                   
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    Manning Office :
                                                                   
                                                                </td>
                                                                <td colspan="4">
                                                                     <asp:DropDownList ID="ddl_Recr_Office" runat="server" CssClass="form-control" Width="165px" TabIndex="16">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                  Status :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_CrewStatus_Search" runat="server" CssClass="form-control"
                                                                        Width="165px" TabIndex="11">
                                                                        <asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;"> </td>
                                                                <td>
                                                                   
                                                                     
                                                                    
                                                                   <%-- <table width="300px"  >
                                                                        <tr > 
                                                                            <td width="120px">
                                                                                
                                                                            </td>
                                                                            <td width="120px">
                                                                                
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                    </table>
                                                                    --%>
                                                                    
                                                                    
                                                                   </td>
                                                                <td style="text-align: right;">Age Between :</td>
                                                                <td colspan="4">
                                                                    <div style="float:left;padding-right: 10px;">
                                                                        <asp:TextBox ID="txt_Age_From" runat="server" CssClass="form-control" MaxLength="2"
                                                                        Width="16px" TabIndex="12"></asp:TextBox>
                                                                        </div>
                                                                    <div style="float:left;">
                                                                    <asp:TextBox ID="txt_Age_To" runat="server" CssClass="form-control"
                                                                        MaxLength="2" Width="16px" TabIndex="13"></asp:TextBox>
                                                                        </div>
                                                                </td>
                                                                
                                                                <td style="text-align: right;">Experience (Month) : </td>
                                                                <td>
                                                                    <div style="float:left;padding-right: 10px;">
                                                                         <asp:TextBox ID="txt_Exp_From" runat="server" CssClass="form-control"
                                                                        MaxLength="3" Width="50px" TabIndex="5"></asp:TextBox>
                                                                        </div>
                                                                    <div style="float:left;">
                                                                    <asp:TextBox ID="txt_Exp_To" runat="server" CssClass="form-control" MaxLength="3" Width="50px"
                                                                        TabIndex="6"></asp:TextBox>
                                                                        </div>
                                                                   
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="Label1" runat="server" Text="Onboard Period :"></asp:Label>
                                                                </td>
                                                                <td  style="text-align: left;padding-left:2px;">
                                                                     <div style="float:left;">
                                                                        <asp:TextBox ID="txt_SignOn_Date" runat="server" CssClass="form-control" MaxLength="15" Width="80px" TabIndex="8"></asp:TextBox> &nbsp;
                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" style="margin-top:8px;" />&nbsp;
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_SignOn_Date" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <div style="float:left;">
                                                                         <asp:TextBox ID="txt_SignOff_Date" runat="server" CssClass="form-control" MaxLength="15" Width="80px" TabIndex="9"></asp:TextBox>&nbsp;
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" style="margin-top:8px;"/>&nbsp;
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_SignOff_Date" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date."></asp:RegularExpressionValidator>
                                                                    </div>
                                                                </td>
                                                                <td style="text-align: left;" colspan="5">US Visa : 
                                                                    <asp:CheckBox ID="chk_UsVisa" runat="server" TabIndex="18" />
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    Schengen visa : &nbsp;&nbsp;
                                                                    <asp:CheckBox ID="chk_SchengenVisa" runat="server" TabIndex="18" />&nbsp;&nbsp;Family Members:
                                                               
                                                                   &nbsp;&nbsp; <asp:CheckBox ID="chk_Family" runat="server" TabIndex="18" /></td>
                                                                <td style="text-align: right;">
                                                                    Passport # :
                                                                    <%--<span style="font-size: 8pt; font-family: Verdana">Relief Due/Sign Off:</span>--%>

                                                                </td>
                                                                <td>
                                                                     <asp:TextBox ID="txt_PassportNo" runat="server" CssClass="input_box" MaxLength="20" Width="160px" TabIndex="14"></asp:TextBox>
                                                                    <asp:TextBox ID="txt_ReliefDue" runat="server" CssClass="form-control" MaxLength="10"
                                                                        TabIndex="9" Width="82px" Visible="false"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" Visible="false" />&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #e2e2e2">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" colspan="6" style="padding-right: 15px; text-align: right;">
                                                                    <strong>Total Filterd Records :&nbsp;<asp:Label ID="CrewCount" runat="server"></asp:Label>&nbsp;</strong>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txt_SignOff_Date" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" TargetControlID="txt_ReliefDue" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txt_SignOn_Date"></ajaxToolkit:CalendarExtender>
                                                                    <asp:Button ID="btn_Search" runat="server" CausesValidation="true" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="19" />
                                                                    <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="20" />
                                                                    <asp:Button ID="btn_Add" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Add_Click" Text="Add New" Width="75px" TabIndex="21" />
                                                                    <asp:Button ID="btnWeb" runat="server" Visible="false" CausesValidation="false" CssClass="btn" OnClick="btnWeb_Click" Text="Web Application" Width="100px" TabIndex="22" />
                                                                    <asp:Button ID="btn_Print" runat="server" CausesValidation="true" CssClass="btn" OnClientClick="window.open('SearchPrint.aspx');return false;" Text="Print" Width="75px" TabIndex="21" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td colspan="6" style="padding-right: 0px; padding-left: 0px; padding-bottom: 5px; padding-top: 0px">
                                                                    <div style="width: 100%; height: 340px; overflow-x: hidden; overflow-y: scroll;">
                                                                        <center>
                                                                            <asp:Label ID="lblGrid" runat="Server"></asp:Label>
                                                                        </center>
                                                                        <asp:GridView OnDataBound="DataBound" CellPadding="0" CellSpacing="0" ID="GridView1" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="gvCustomers_OnPageIndexChanging" OnRowDataBound="GvCustomers_RowDataBound" OnSelectedIndexChanged="GridView_SelectIndexChanged" OnSorted="on_Sorted" OnSorting="on_Sorting" Width="98%" OnRowEditing="Row_Editing" OnRowDeleting="Row_Deleting" GridLines="horizontal" OnPreRender="Grid_PreRender">
                                                                            <Columns>
                                                                                <asp:CommandField ItemStyle-Width="30px" ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center"></asp:CommandField>
                                                                                <asp:CommandField ItemStyle-Width="30px" ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:CommandField ItemStyle-Width="30px" ButtonType="Image" ShowDeleteButton="True" DeleteImageUrl="~/Modules/HRD/Images/delete.jpg" HeaderText="Delete" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:TemplateField  HeaderText="Crew #" SortExpression="CrewNumber">
                                                                                    <ItemStyle Width="50px" />
                                                                                 
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnk_Empno" Visible="false" CommandName="lnk_Emp"  runat="server" Text='<%#Eval("CrewNumber")%>'></asp:LinkButton>
                                                                                        <asp:Label ID="lblMemberId" runat="server" Text='<%#Eval("CrewNumber")%>'></asp:Label>
                                                                                        <asp:Label ID="Lb_crewID" Visible="false" runat="server" Text='<%#Eval("CrewId")%>'></asp:Label>
                                                                                        <asp:HiddenField ID="HiddenId" runat="server" Value='<%#Eval("CrewId")%>' />
                                                                                        <asp:HiddenField ID="HiddenCrewNumber" runat="server" Value='<%#Eval("CrewNumber")%>' />
                                                                                        
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="FullName" HeaderText="Crew Name" SortExpression="FullName">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="175px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="RankLevel">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="CountryName" HeaderText="Nationality" SortExpression="CountryName">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="LastVessel" HeaderText="Vessel" SortExpression="LastVessel">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="135px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="SignedOnDt" HeaderText="Sign On Dt." SortExpression="SignedOnDt">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="SignedOffDt" HeaderText="Sign Off Dt." SortExpression="SignedOffDt">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ReliefDueDate" HeaderText="Rel.Due Dt." SortExpression="ReliefDueDate">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <PagerTemplate>
                                                                                Page
                                                        <asp:TextBox ID="txtGoToPage" runat="server" AutoPostBack="true" CssClass="gotopage"
                                                            OnTextChanged="GoToPage_TextChanged" Width="17px"></asp:TextBox>
                                                                                of
                                                        <asp:Label ID="lblTotalNumberOfPages" runat="server"></asp:Label>
                                                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                            CommandName="Page" CssClass="previous" Text="" ToolTip="Previous Page" />
                                                                                <asp:Button ID="Button2" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                    CommandName="Page" CssClass="next" Text="" ToolTip="Next Page" />
                                                                            </PagerTemplate>
                                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                            <PagerStyle CssClass="pagerstyle" />
                                                                           <%-- <HeaderStyle BackColor="Black" ForeColor="White" Font-Size="Small" Font-Bold="true" Font-Names="Arial, sans-serif"  />--%>
                                                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                            <RowStyle CssClass="rowstyle" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="Numbers" TargetControlID="txt_Exp_From">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers" TargetControlID="txt_Exp_To">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="Numbers" TargetControlID="txt_Age_From">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterType="Numbers" TargetControlID="txt_Age_To">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="dvConfirmAddNew" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 500px; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 400px; height: 120px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 1px Black">
                    <table cellpadding="5" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td style="text-align: center; font-weight: bold;">Please Enter Approval No to Continue..</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:TextBox ID="txtComments" runat="server" Style="width: 150px" MaxLength="10" ValidationGroup="v" CssClass="required_box"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="r1" ErrorMessage="*" ControlToValidate="txtComments"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnNewGo" runat="server" CssClass="btn" Text=" Continue.. " OnClick="btnNewGo_OnClick" CausesValidation="true" ValidationGroup="v" />
                                <asp:Button ID="btnNewCancel" runat="server" CssClass="btn" Text=" Cancel " OnClick="btnNewCancel_OnClick" CausesValidation="false" ValidationGroup="v" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNewGoMsg" runat="server" Style="font-weight: bold; color: Red;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </div>

    </div>
    <%--</form>--%>
    
    <%--</body>
</html>--%>
</asp:Content>


