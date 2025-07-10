<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchVacancyDetails.aspx.cs" Inherits="Modules_HRD_Vacancy_SearchVacancyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <%--  <link rel="stylesheet" type="text/css" href="../Styles/style.css" />
   <link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <script type="text/javascript" language="javascript">
         function reloadVacancyDetails() {
             document.getElementById('ctl00_ContentMainMaster_btn_Search').click();
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="text-align: left">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
                        <tr>
                            <td style="text-align: center; background-color: #4c7a6f; color: #fff; font-size: 14px;" class="text headerband"> 
                               Search Vacancy Slot
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
                                                                <td style="text-align: right;">Company Name :
                                                                </td>
                                                                <td >
                                                                <asp:DropDownList ID="ddl_Owner" runat="server"  CssClass="form-control" Width="165px" TabIndex="1">
                                                                       <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList> 
                                                                </td>
                                                                <td  style="text-align: right;">Vessel :
                                                                </td>
                                                                <td colspan="4" >
                                                                     <asp:DropDownList ID="ddl_Vessel" runat="server"  CssClass="form-control" Width="165px" TabIndex="2">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="text-align: right;">Rank :</td>
                                                                <td >
                                                                    <%--<asp:TextBox ID="txt_LastName_Search" runat="server" CssClass="form-control" MaxLength="24"
                                                                        Width="160px" TabIndex="3"></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="form-control" Width="165px" TabIndex="3">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                               <td style="text-align: right;"> Manning Office :
                                                                </td>
                                                                <td >
                                                                <asp:DropDownList ID="ddl_Recr_Office" runat="server" CssClass="form-control" Width="165px" TabIndex="7">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right;"> Vacancy Status : </td>
                                                                <td colspan="4" style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlVStatus" runat="server" Width="165px" TabIndex="19">
                            <asp:ListItem Selected="True" Value="1" Text="Open"></asp:ListItem>
                            <asp:ListItem  Value="0" Text="Closed"></asp:ListItem>
                        </asp:DropDownList>
                                                                    </td>
                                                                <td style="text-align: right;">
                                                                </td>
                                                                <td>
                                                                   
                                                                </td>
                                                            </tr>
                                                        </table>
                                                         <asp:HiddenField ID="hdnCandidateId" runat="server" ></asp:HiddenField> 
                         <asp:HiddenField ID="hdnResendProposal" runat="server" Value="false" ></asp:HiddenField>
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
                                                                    <strong>Total Filterd Records :&nbsp;<asp:Label ID="VacancySlotCount" runat="server"></asp:Label>&nbsp;</strong>
                                                                   
                                                                    <asp:Button ID="btn_Search" runat="server" CausesValidation="true" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="8" /> &nbsp;
                                                                    <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="9" /> &nbsp;
                                                                    <asp:Button ID="btn_Add" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Add_Click" Text="Add Vacancy" Width="120px" TabIndex="10" /> &nbsp;
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                    
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                        <td style="text-align:left;padding:2px 0px 2px 5px;">
                                            <b> Vessel wise Slot Details :  </b>
                                        </td>
                                    </tr>
                                                            <tr>
                                                                <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 5px; padding-top: 0px;width:98%;">
                                                                    <div style="height: 340px; overflow-x: hidden; overflow-y: scroll;">
                                                                        <center>
                                                                            <asp:Label ID="lblGrid" runat="Server"></asp:Label>
                                                                        </center>
                                                                        <asp:GridView  CellPadding="0" CellSpacing="0" ID="Gv_Vacancy" OnRowCommand="Gv_Vacancy_RowCommand" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="Gv_Vacancy_OnPageIndexChanging" OnRowDataBound="Gv_Vacancy_RowDataBound" OnSelectedIndexChanged="Gv_Vacancy_SelectIndexChanged" OnSorting="on_Sorting" Width="98%"  GridLines="horizontal" OnPreRender="Gv_Vacancy_PreRender" >
                                                                            <Columns>
                                                                                <asp:CommandField ItemStyle-Width="20px" ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center"></asp:CommandField>
                                                                              <%--   <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="30px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditVacancy" CausesValidation="false" OnClick="btnEditVacancy_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("VacancyId")%>' />
                                                                            <asp:HiddenField ID="HiddenVacancyId" runat="server" Value='<%#Eval("VacancyId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete ?');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                                <asp:TemplateField  HeaderText="Vacancy #" SortExpression="VacancyId">
                                                                                    <ItemStyle Width="40px" />
                                                                                 
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnk_VacancyId" Visible="false" CommandName="lnk_Vacancy"  runat="server" Text='<%#Eval("VacancyId")%>'></asp:LinkButton>
                                                                                        <asp:Label ID="lblVacancyId" runat="server" Text='<%#Eval("VacancyId")%>'></asp:Label>
                                                                                        
                                                                                        <asp:HiddenField ID="hdnVacancyId" runat="server" Value='<%#Eval("VacancyId")%>' />
                                                                                       
                                                                                        
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                                <asp:TemplateField  HeaderText="Company Name" SortExpression="OwnerName">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="175px" />
                                                                                 
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("OwnerName")%>'></asp:Label>
                                                                                       
                                                                                        
                                                                                        <asp:HiddenField ID="hdnOwnerId" runat="server" Value='<%#Eval("OwnerId")%>' />
                                                                                         
                                                                                        <asp:HiddenField ID="hdnVesselId" runat="server" Value='<%#Eval("VesselId")%>' />
                                                                                       
                                                                                        
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:BoundField DataField="VesselName" HeaderText="Vessel" SortExpression="VesselName">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="125px" />
                                                                                </asp:BoundField>
                                                                               <%-- <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DOJ" HeaderText="Joining Date" SortExpression="DOJ">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DOP" HeaderText="Proposal Date" SortExpression="DOP">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                                </asp:BoundField>
                                                                                 <asp:TemplateField  HeaderText="Assign" >
                                                                                    <ItemStyle Width="50px" HorizontalAlign="Center"  />
                                                                                  <ItemTemplate >
                                                                                        <asp:ImageButton runat="server"  ImageUrl="~/Modules/HRD/Images/group.gif" CommandArgument="AssignVacancy"  OnClientClick="this.src='../Images/loading.gif';" ID="btnAssignCrew" OnClick="btnAssignCrew_Click"/> &nbsp; ( <asp:Label ID="lblVacancyCount" runat="server" Text='<%#Eval("CandidateCount")%>'></asp:Label> )
                                                                                       
                                                                                        
                                                                                    </ItemTemplate>
                                                                                     </asp:TemplateField>--%>
                                                                                  <asp:BoundField DataField="VacancyCount" HeaderText="Total Vacancy" SortExpression="VacancyCount">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                 <asp:BoundField DataField="ProposalSentCount" HeaderText="Proposal Sent" SortExpression="ProposalSentCount">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                  <asp:BoundField DataField="ApprovedCount" HeaderText="Proposal Approved" SortExpression="ApprovedCount">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                 <asp:BoundField DataField="RejectedCount" HeaderText="Proposal Rejected" SortExpression="RejectedCount">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                  <asp:BoundField DataField="InProgressCount" HeaderText="Approval In Progress" SortExpression="InProgressCount">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                                </asp:BoundField>
                                                                                  <asp:TemplateField  ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <div id="divAction" runat="server" visible='<%#Common.CastAsInt32(Eval("OwnerAction"))>0%>' style="margin-left:7px;" > 
                                                      <img src="../Images/Bell.png" />
                                                  </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                           
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
        <!--  Vessel Slot Summary Details -->
        <div style="position:absolute;top:50px;left:50px; height :510px; width:95%;" id="dvVesselSlotDetails" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:50px; min-height :100%; width:95%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:98%; padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 0px black;">
            <center >
                <div class="text headerband">
                    Vacancy Details 
                      <asp:ImageButton ID="btnCloseVacancyDtlsDiv" CausesValidation="false" style="  border:none; padding:3px; float:right;padding-right:5px; "  runat="server" OnClick="btnCloseVacancyDtlsDiv_Click" ImageUrl="~/Modules/HRD/Images/delete_12.gif" />
                </div>
                <table width="100%">
                    <tr>
                        <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Owner :
                        </td>
                        <td style="width:100px;text-align:left;padding:2px 0px 2px 5px;"">
                            <asp:Label ID="lblOwnerName" runat="server" > </asp:Label>
                        </td>
                        <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Vessel Name : 
                        </td>
                         <td style="width:100px;text-align:left;padding:2px 0px 2px 5px;">
                             <asp:Label ID="lblVesselName" runat="server" > </asp:Label>
                        </td>
                        <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Total Vacancy : 
                        </td>
                        <td style="width:100px;text-align:left;padding:2px 0px 2px 5px;">
                            <asp:Label ID="lblTotalVacancy" runat="server" > </asp:Label>
                        </td>
                         <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Proposal Sent : 
                        </td>
                        <td style="width:50px;text-align:left;padding:2px 0px 2px 5px;">
                            <asp:Label ID="lblProposalsent" runat="server" > </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        
                        <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Proposal Approved :
                        </td>
                        <td style="width:100px;text-align:left;padding:2px 0px 2px 5px;"">
                            <asp:Label ID="lblProposalApproved" runat="server" > </asp:Label>
                        </td>
                         <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Proposal Rejected :
                        </td>
                        <td style="width:100px;text-align:left;padding:2px 0px 2px 5px;"">
                            <asp:Label ID="lblProposalRejected" runat="server" > </asp:Label>
                        </td>
                         <td style="width:100px;text-align:right;padding:2px 5px 2px 0px;">
                            Proposal In-Progress :
                        </td>
                        <td style="width:50px;text-align:left;padding:2px 0px 2px 5px;"">
                            <asp:Label ID="lblProposalInProgress" runat="server" > </asp:Label>
                        </td>
                    </tr>
                </table>
               
                                                                    <div style="height: 450px; overflow-x: hidden; overflow-y: scroll;">
                                                                        <center>
                                                                            <asp:Label ID="lblVacancyMsg" runat="Server"></asp:Label>
                                                                        </center>
                                                                        <asp:GridView CellPadding="0" CellSpacing="0" ID="GV_VacancyDtls" OnRowCommand="GV_VacancyDtls_RowCommand" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="GV_VacancyDtls_OnPageIndexChanging" OnRowDataBound="GV_VacancyDtls_RowDataBound" OnSelectedIndexChanged="GV_VacancyDtls_SelectIndexChanged"  OnSorting="GV_VacancyDtls_Sorting" Width="98%"  OnRowDeleting="GV_VacancyDtls_Row_Deleting" GridLines="horizontal" OnPreRender="GV_VacancyDtls_PreRender" >
                                                                            <Columns>
                                                                                <asp:CommandField ItemStyle-Width="20px" ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center"></asp:CommandField>
                                                                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="30px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditVacancy" CausesValidation="false" OnClick="btnEditVacancy_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("VacancyId")%>' />
                                                                            <asp:HiddenField ID="HiddenVacancyId" runat="server" Value='<%#Eval("VacancyId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDeleteVacancy" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete ?');"
                                            Text="Delete" Visible='<%#Common.CastAsInt32(Eval("CandidateCount"))==0%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                                <asp:TemplateField  HeaderText="Vacancy #" SortExpression="VacancyId">
                                                                                    <ItemStyle Width="40px" />
                                                                                 
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnk_VacancyId" Visible="false" CommandName="lnk_Vacancy"  runat="server" Text='<%#Eval("VacancyId")%>'></asp:LinkButton>
                                                                                        <asp:Label ID="lblVacancyId" runat="server" Text='<%#Eval("VacancyId")%>'></asp:Label>
                                                                                        
                                                                                        <asp:HiddenField ID="hdnVacancyId" runat="server" Value='<%#Eval("VacancyId")%>' />
                                                                                        <asp:HiddenField ID="hdnVacancyOwnerId" runat="server" Value='<%#Eval("OwnerId")%>' />
                                                                                         
                                                                                        <asp:HiddenField ID="hdnVacancyVesselId" runat="server" Value='<%#Eval("VesselId")%>' />
                                                                                       
                                                                                        
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:BoundField DataField="Owner" HeaderText="Company Name" SortExpression="Owner">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="175px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="VesselName" HeaderText="Vessel" SortExpression="VesselName">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="125px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DOJ" HeaderText="Joining Date" SortExpression="DOJ">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DOP" HeaderText="Proposal Date" SortExpression="DOP">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                                                </asp:BoundField>
                                                                                 <asp:TemplateField  HeaderText="Assign" >
                                                                                    <ItemStyle Width="50px" HorizontalAlign="Center"  />
                                                                                  <ItemTemplate >
                                                                                        <asp:ImageButton runat="server"  ImageUrl="~/Modules/HRD/Images/add_12.jpg" CommandArgument="AssignVacancy"  OnClientClick="this.src='../Images/loading.gif';" ID="btnAssignCrew" OnClick="btnAssignCrew_Click"/> &nbsp; ( <asp:Label ID="lblVacancyCount" runat="server" Text='<%#Eval("CandidateCount")%>'></asp:Label> )
                                                                                       
                                                                                        
                                                                                    </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                 <asp:TemplateField  ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <div id="divAction" runat="server" visible='<%#Common.CastAsInt32(Eval("OwnerAction"))>0%>' style="margin-left:7px;" > 
                                                      <img src="../Images/Bell.png" />
                                                  </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerTemplate>
                                                                                Page
                                                        <asp:TextBox ID="txtGoToPageVacancyDtls" runat="server" AutoPostBack="true" CssClass="gotopage"
                                                            OnTextChanged="GoToPageVacancyDtls_TextChanged" Width="17px"></asp:TextBox>
                                                                                of
                                                        <asp:Label ID="lblTotalNumberOfPagesVacancyDtls" runat="server"></asp:Label>
                                                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <asp:Button ID="btnVacancyDtlsPrevious" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                            CommandName="Page" CssClass="previous" Text="" ToolTip="Previous Page" />
                                                                                <asp:Button ID="btnVacancyDtlsNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                    CommandName="Page" CssClass="next" Text="" ToolTip="Next Page" />
                                                                            </PagerTemplate>
                                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                            <PagerStyle CssClass="pagerstyle" />
                                                                           <%-- <HeaderStyle BackColor="Black" ForeColor="White" Font-Size="Small" Font-Bold="true" Font-Names="Arial, sans-serif"  />--%>
                                                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                            <RowStyle CssClass="rowstyle" />
                                                                        </asp:GridView>
                                                                    </div>
                                                               
           
            </center>
        </div>
    </center>
</div>
               
         <!--  Add Signer Details -->
    <div style="position:absolute;top:50px;left:50px; height:510px; width:95%;" id="dvAddSignerDetails" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:50px; min-height :100%; width:95%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:98%; padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 0px black;">
            <center >
                    <asp:Button ID="btnCloseAddSignerDiv" CausesValidation="false" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnCloseAddSignerDiv_Click" />

            <iframe id="ifmAddSignerDetails" runat="server" src="" height="510px" width="100%" scrolling="no" frameborder="0">

            </iframe>
           
            </center>
        </div>
    </center>
    </div>
       <!--  Candidate Details -->
    <div style="position:absolute;top:50px;left:50px; height :510px; width:95%;" id="divCandidate" runat="server" visible="false">
    <center>
    <div style="position:fixed;top:0px;left:50px; min-height :100%; width:95%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:98%; padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 0px black;">
    <center >
    <div class="text headerband">
        Vacancy Details 
            <asp:ImageButton ID="imgbtnCloseCandidateDtls" CausesValidation="false" style="  border:none; padding:3px; float:right;padding-right:5px; "  runat="server" OnClick="imgbtnCloseCandidateDtls_Click" ImageUrl="~/Modules/HRD/Images/delete_12.gif" />
    </div>
        <table width="100%">
                                                                         <tr><td>
                                                                             <div style="overflow-x:hidden; overflow-y :scroll;height:30px; border:solid 1px gray;" >
                                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 30px; color: black;" class="bordered">
                                <colgroup >
                                <col width="5%;" /> 
                                <col width="5%;" /> 
                                <col width="20%;"/> 
                                <col width="7%;" />  
                                <col width="8%;" /> 
                                <col width="12%;"  />
                                <col width="10%;"  />
                                <col width="8%;" />
                                </colgroup>
                                <tr class="headerstylegrid"  style="font-family: Arial, sans-serif;	font-weight:bold;">
                                <td scope="col" style=" text-align:left">Delete</td>
                                <td scope="col" style=" text-align:left" >Applicant ID</td>
                                <td scope="col" style=" text-align:left" >Name</td>
                                <td scope="col" style=" text-align:left" >Rank</td>
                                <td scope="col" style=" text-align:left" >Nationality</td>
                                <td scope="col" style=" text-align:center" colspan="2">Proposal Action</td> 
                                <td scope="col" style=" text-align:left"></td>  
                                </tr>
                                </table>
                                </div>
                                                                       <div style="overflow-x:hidden; overflow-y :scroll; width:100%;height:383px;" id="dvscroll_cdinfo" onscroll="SetScrollPos(this)">
                                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                                <colgroup >
                                <col width="5%;" /> 
                                <col width="5%;" /> 
                                <col width="20%;"/> 
                                <col width="7%;" />  
                                <col width="8%;" /> 
                                <col width="12%;" />
                                <col width="10%;" />
                                <col width="8%;" />
                                </colgroup>
                                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                                <ItemTemplate>
                                <tr id='tr<%#Eval("CandidateId")%>' onclick='Selectrow(this,<%#Eval("CandidateId")%>);' lastclass='alternaterow'>
                                    <td style =" text-align :center;  ">
                                    <asp:ImageButton runat="server" ID="imgDelete" ToolTip="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%#Eval("CandidateId")%>' OnClick="Delete_Candidate"  OnClientClick="return confirm('Are you sure to remove the assigned candidate for this vacancy ?');"  />
                                        <asp:HiddenField ID="hdnVacancyId" runat="server" Value='<%#Eval("VacancyId")%>' />
                                        <asp:HiddenField ID="hdnOwnerId" runat="server" Value='<%#Eval("OwnerId")%>' />
                                        <asp:HiddenField ID="hdnVesselId" runat="server" Value='<%#Eval("VesselId")%>' />
                                    </td>
                                    <td style=" "><asp:LinkButton ID="lbCandidate" runat="server" Text='<%#Eval("CandidateId")%>' OnClick="Open_Candidate" CommandArgument='<%#Eval("CandidateId")%>'></asp:LinkButton></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("Name")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("Rank")%></td>
                                    <td style=" text-align :left;">&nbsp;<%#Eval("Country")%></td>
                                    <%--<td style=" text-align :left;">&nbsp;<%#Eval("AvailableFrom")%></td>--%>
                                    <td style =" text-align :center;font-size:12px;  ">
                                    <asp:LinkButton runat="server" ID="lbProposalToOwner" Text="Proposal to Client"  CommandArgument='<%#Eval("CandidateId")%>' OnClick="ProposalToOwner" Visible="false" Font-Size="14px" ForeColor="DarkGreen" /> 
                                        <asp:LinkButton runat="server" ID="lbResendProposal" Text="Resend"  CommandArgument='<%#Eval("CandidateId")%>' OnClick="lbResendProposal_click" Visible="false" Font-Size="14px" ForeColor="DarkGreen"  /> 
                                        <%--<asp:Label ID="lblslase" runat="server" Text="/" Visible="false"></asp:Label>--%> 
                                       
                                        <asp:HiddenField ID="hdnCrewNumber" runat="server" Value='<%#Eval("CrewNumber")%>' />
                                        <asp:HiddenField ID="hdnClientStatus" runat="server" Value='<%#Eval("ClientStatus")%>' />
                                        <asp:HiddenField ID="hdfStatusName" runat="server" Value='<%#Eval("StatusName")%>' />
                                    </td>
                                    <td style =" text-align :center;font-size:12px;  ">
                                         <asp:LinkButton runat="server" ID="lbWithDraw" Text="Withdraw"  CommandArgument='<%#Eval("CandidateId")%>' OnClientClick="return confirm('Are you sure to withdraw proposal for this applicant?');" OnClick="lbWithDraw_click" Visible="false" Font-Size="14px" ForeColor="DarkGreen" /> 
                                    </td>
                                     <td style =" text-align :center;  ">
                                          <asp:LinkButton runat="server" ID="lbAppRejRemarks" Text="Approval Status"  CommandArgument='<%#Eval("CandidateId")%>' OnClick="lbAppRejRemarks_click" Visible="false" Font-Size="14px" ForeColor="DarkGreen"  /> 
                                         <asp:Label ID="lblWithDraw" Text="Proposal WithDrawn" runat="server" ForeColor="Red" Visible="false" />
                                     </td>
                                </tr> 
                                </ItemTemplate> 
                                </asp:Repeater> 
                                </table> 
                                <asp:Label ID="lbl_License_Message" runat="server"></asp:Label>
                                </div>
                                                                             </td></tr>
                                                                     
                                                                       
                                                                         </table>
    </center>
    </div>
    </center>
    </div>
 <!--  Send Proposal to client  -->
        <div style="position: absolute; top: 50px; left: 50px; height: 450px; width: 85%;" id="dvProposalToOwner" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 25px; left: 50px; height: 450px; width: 85%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 85%; height: 400px; padding: 3px; text-align: center; background: white; z-index: 150; top: 50px; border: solid 1px Black">
                      <div style=" text-align: center;vertical-align:central " class="text headerband">
                   Send Proposal to Client
                           <div style=" float :right " >
                          <asp:ImageButton ID="ibClose" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="ibClose_Click" CausesValidation="false" />
                               </div>
             </div>
                    <div style=" text-align: center;width:100%;padding-left:100px;padding-right:100px;padding:5px;">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> 
                        <asp:HiddenField ID="hdnRandomPwd" runat="server" ></asp:HiddenField> 
                       <%--  <asp:HiddenField ID="HiddenField1" runat="server" ></asp:HiddenField> 
                         <asp:HiddenField ID="HiddenField2" runat="server" Value="false" ></asp:HiddenField>--%>
                        <asp:HiddenField ID="hdnCompany" runat="server" Value="false" ></asp:HiddenField>
                    </div>
            <div style=" text-align: center;width:100%;">
                <table style="width:100%;" >
                    
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Owner : 
                        </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;">
                            <asp:DropDownList ID="ddl_Ownerlist" runat="server" Width="450px" TabIndex="1"  CssClass="input_box" Style="background-color: lightyellow" OnSelectedIndexChanged="ddl_Ownerlist_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList> &nbsp;  <asp:CompareValidator ID="cv_Ownerlist" runat="server" ControlToValidate="ddl_Ownerlist" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                       
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Vessel : 
                        </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;">
                            <asp:DropDownList ID="ddlVesselOwner" runat="server" Width="450px" TabIndex="1"  CssClass="input_box" Style="background-color: lightyellow"  AutoPostBack="true" OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged"   ></asp:DropDownList> &nbsp;  <asp:CompareValidator ID="cv_Vessel" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;"> Requirement : </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;"> <b><asp:Label ID="lblVacancyId" runat="server" Visible="false"></asp:Label> <asp:Label ID="lblVacanyRequirement" runat="server" ></asp:Label> </b> </td>
                    </tr>
                    
                    <tr id="trFromEmail" runat="server" visible="false">
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            From Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtFromAddress" runat="server" TabIndex="3" TextMode="SingleLine" MaxLength="200"   Width="700px" ReadOnly="true" ></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvFromAddress" runat="server" ControlToValidate="txtFromAddress" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Approval 1 : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtApproval1Email" runat="server" TabIndex="4" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvToEmail" runat="server" ControlToValidate="txtApproval1Email" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator> <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtApproval1Email"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <%--<asp:HiddenField ID="hdnApproval1Status" runat="server" Value="" />--%> 
                        </td>
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Approval 2 : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtApproval2Email" runat="server" TabIndex="5" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtApproval2Email"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                             <%--<asp:HiddenField ID="hdnApproval2Status" runat="server" Value="" />--%> 
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Approval 3 : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtApproval3Email" runat="server" TabIndex="6" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox> <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtApproval3Email"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <%--<asp:HiddenField ID="hdnApproval3Status" runat="server" Value="" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Approval 4 : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtApproval4Email" runat="server" TabIndex="7" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox> <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtApproval4Email"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <%--<asp:HiddenField ID="hdnApproval4Status" runat="server" Value="" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Manning Officer Remarks : 
                        </td>
                        <td style="padding-left:5px;text-align:left;padding:5px;" >
                            <asp:TextBox ID="txtManningOfficerRemarks" runat="server" TabIndex="8" Width="700px" TextMode="MultiLine"  CssClass="input_box" Style="background-color: lightyellow" Height="55px" MaxLength="500"></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvManningOfficerRemarks" runat="server" ControlToValidate="txtManningOfficerRemarks" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr id="trBccEmail" runat="server" visible="false">
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            BCC Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtBCCEmail" runat="server" TabIndex="4" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr id="trSubject" runat="server" visible="false">
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Subject : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtSubject" runat="server" TabIndex="5" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>
                        </td>
                        
                    </tr>
                     <tr id="trBody" runat="server" visible="false">
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Email Body : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <%--<asp:TextBox ID="txtEmailBody" runat="server" TabIndex="6" TextMode="MultiLine" MaxLength="4000"   CssClass="input_box" Width="700px" Height="200px" ></asp:TextBox> &nbsp;  <asp:RequiredFieldValidator ID="rfvEmailBody" runat="server" ControlToValidate="txtEmailBody" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>--%>
                            <div contenteditable="true" runat="server" id="dvContent" style="overflow-x:hidden; overflow-y :scroll; width:100%;height:200px;"  onscroll="SetScrollPos(this)">
                            <asp:Literal runat="server" ID="litMessage"></asp:Literal> 
                            </div>
                        </td>
                      
                    </tr>
                </table>
            </div>
             <div style=" text-align: center;width:70%;padding-left:100px;padding-right:100px;padding:5px;">
                 <asp:Button ID="btnSendProposal" runat="server" CssClass="btn" Width="150px" CausesValidation="false" TabIndex="9" Text="Send Mail to Client" OnClick="btnSendProposal_Click" />
                 <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text=" Close " TabIndex="8" CausesValidation="false" OnClick="btnCancel_Click"  />
             </div>
                    
                </div>
            </center>
        </div>
        <!--  Approval Remarks  -->
        <div style="position: absolute; top: 50px; left: 50px; height: 300px; width: 85%;" id="divApprovalRemark" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 25px; left: 50px; height: 300px; width: 85%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 85%; height: 200px; padding: 3px; text-align: center; background: white; z-index: 150; top: 50px; border: solid 1px Black">
                      <div style=" text-align: center;vertical-align:central " class="text headerband">
                  Approval's Remarks
                           <div style=" float :right " >
                          <asp:ImageButton ID="imgbtnAppRejClose" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="imgbtnAppRejClose_Click" CausesValidation="false" />
                               </div>
             </div>
            <div style=" text-align: center;width:100%;">
                <table  border="0" cellpadding="3" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%"> 
       <tr>
                        <td>
                             <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; height: 30px; border-collapse: collapse;" class="bordered">
                                        <colgroup>
                                            <col width="200px" />
                                            <col />
                                            <col width="200px"/>
                                            <tr style="font-weight: bold; text-align: left; color:#333;background-color:#E0F5FF;"> 
                                                <td><b> Approval Owner </b></td>
                                                <td><b>Approval Remarks </b></td>
                                                <td><b>Approval Status </b></td>
                                            </tr>
                                        </colgroup>
                                    </table>
                                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                                        <colgroup>
                                            <col width="200px" />
                                            <col />
                                            <col width="200px"/>
                                            
                                        </colgroup>
                                        <asp:Repeater ID="rptApprovalHistory" runat="server">
                                            <ItemTemplate>
                                                <tr style="text-align: left;">
                                            
                                                    <td style="text-align: left;">
                                                 <%#Eval("OwnerEmail")%>
                                                    </td>
                                                    <td style="text-align: left;"><%#Eval("OwnerRemarks")%></td>
                                                    <td style="text-align: left;"><%#Eval("ApprovalStatus")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                        </td>
                    </tr>
        </table>
            </div>
                </div>
            </center>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainFoot" Runat="Server">
</asp:Content>

