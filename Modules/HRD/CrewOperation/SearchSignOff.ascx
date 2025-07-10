<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchSignOff.ascx.cs" Inherits="CrewOperation_SearchSignOff" %>
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div align="center">
<asp:Label ID="lb_msg" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
                    <table cellpadding="0" cellspacing="0" style="width:100%;" border="1">
                        <tr>
                        <td width="240px" style=" background-color : #e2e2e2; padding : 0px 0px 0px 0px;" >
                                <table style="width:240px;" cellpadding="0" cellspacing="0" border="1" >
                                <tr>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="Chk_all" Text="Select All" AutoPostBack=true runat="server" OnCheckedChanged="Chk_all_CheckedChanged" />[ Rank ]
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                    <table width="100%" cellspacing="0" cellpadding ="0" >
                                            <tr>
                                                <td rowspan="4"><asp:ListBox ID="chkrank" runat="server" CssClass="input_box" Width="90px" SelectionMode="Multiple" Height="75px"></asp:ListBox></td>
                                                <td >From:</td>
                                                <td><asp:TextBox ID="txt_from" runat="server" CssClass="input_box" MaxLength="15" Width="90px"></asp:TextBox><asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td >&nbsp;</td>
                                                <td>
                                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">To:</td>
                                                <td><asp:TextBox ID="txt_to" runat="server" CssClass="required_box" MaxLength="15" Width="90px"></asp:TextBox><asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td class="style2">&nbsp;</td>
                                                <td>
                                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_to" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                         
                                            </tr>
                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;"><asp:CheckBox ID="Chk_vessel" AutoPostBack="true" Text="Select All" runat="server" OnCheckedChanged="Chk_vessel_CheckedChanged" />[ Vessel ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn" Width="60px" OnClick="Button2_Click" /></td>
                                </tr>
                                 <tr>
                                    <td style="text-align: left;">
                                    </td>
                                </tr>
                                </table>
                                <asp:ListBox ID="chkvessel" runat="server" CssClass="input_box" Width="234px" SelectionMode="Multiple" Height="80px"></asp:ListBox>
                                    
                        </td>
                        <td valign="top" style=" text-align : left;">
                        <div style=" height :203px; overflow-x:hidden; overflow-y:scroll">
                        <asp:GridView ID="gvsearch" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" OnRowCommand="gvsearch_RowCommand" OnRowCreated="gvsearch_RowCreated" AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Height="32px" Style="text-align: left" Width="98%" OnPreRender="gvsearch_prerender" OnRowDataBound="GV_OnRowDataBound" >
                            <Columns>
                            <asp:TemplateField Visible="False" ><ItemTemplate><asp:CheckBox ID="chk_select" runat="server" /></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp. #" SortExpression="CrewNumber" >
                            <ItemStyle HorizontalAlign="Left"  Width="60px"/>
                                <ItemTemplate>
                                   <asp:LinkButton ID="btnrefno" runat="server" Text='<%#Eval("CrewNumber") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                                   <asp:HiddenField ID="hiddenreliefduedate" runat="server" Value='<%#Eval("SignOffDate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="CrewName" >
                                <ItemTemplate><asp:Label ID="lblCompanyName" runat="server"  Text='<%# Eval("CrewName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" /></asp:TemplateField>
                           <asp:TemplateField HeaderText="Rank" SortExpression="RankName" >
                           <ItemTemplate>
                           <asp:Label ID="lblrankname" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
                           <div style="display:none">
                           <asp:Label ID="lblrankid" runat="server" Width="0px" Text='<%# Eval("CurrentRankId") %>'></asp:Label>
                           <asp:Label ID="lblvesselid" runat="server" Text='<%# Eval("CurrentVesselId") %>'></asp:Label>
                           <asp:Label ID="Lb_CrewID" runat="server" Text='<%# Eval("crewid") %>'></asp:Label>
                           <asp:Label ID="lb_R_ID" runat="server" Text='<%# Eval("crewid") %>'></asp:Label>
                           <asp:Label ID="Lb_ReliverID" runat="server" Text='<%# Eval("ReliverID") %>'></asp:Label>
                           <asp:Label ID="lb_ReliverID1" runat="server" Text='<%# Eval("ReliverID1") %>'></asp:Label>
                           <asp:Label ID="Lb_ReliverRankId" runat="server" Text='<%# Eval("ReliverRankId") %>'></asp:Label>
                           <asp:Label ID="Lb_ReliverRankId1" runat="server" Text='<%# Eval("ReliverRankId1") %>'></asp:Label>   
                           </div>
                           </ItemTemplate>
                              <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                            <asp:BoundField DataField="Nationality" SortExpression="Nationality"  HeaderText="Nationality">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            
                                 <asp:BoundField DataField="VesselName"  SortExpression="VesselName" HeaderText="Vessel" >
                                <ItemStyle HorizontalAlign="Left" />
                          </asp:BoundField>
                            
                            <asp:BoundField DataField="EOC" HeaderText="EOC" SortExpression="EOC" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                              <asp:BoundField HeaderText="Sign On" SortExpression="SignOnDate" DataField="SignOnDate">
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                             </asp:BoundField>
                              <asp:BoundField HeaderText="Rel. Due Date" SortExpression="SignOffDate"  DataField="SignOffDate">
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                             </asp:BoundField>
                            <asp:TemplateField HeaderText="Ist Reliever">
                            <ItemTemplate>
                            <div style='display:<%# (Eval("Reliver").ToString()=="")?"none":"" %>'>
                                <div style="float:left; width:50px;height:15px; ">
                                    <img src="../Images/user_icon.png" style=" text-align:left; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("Details") %>')" onmouseout="if(t1)t1.Hide(event)">  
                                    <asp:ImageButton ID="btnCL" CommandName="img_dc" runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Document CheckList" OnClick='btnCL_Click'/>
                                </div>
                                <div style="float:right; width:20px;height:15px; ">
                                <asp:ImageButton ID="img_rel" CommandName="img_reliver" runat="server" ImageUrl="~/Modules/HRD/Images/delete1.gif" />
                                </div> 

                            </div> 
                            </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="IInd Reliever">
                            <ItemTemplate>
                            <div style='display:<%# (Eval("Reliver1").ToString()=="")?"none":"" %>'>
                            <div style="float:left; width:20px;height:15px; ">
                            <img src="../Images/user_icon.png" style=" text-align:left; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("Details1") %>')" onmouseout="if(t1)t1.Hide(event)">  
                            </div>
                            <div style="float:right; width:20px;height:15px; ">
                                <asp:ImageButton ID="img_rel1"  CommandName="img_reliver1" runat="server" ImageUrl="~/Modules/HRD/Images/delete1.gif"/>
                                </div> 
                            </div> 
                            </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                          <RowStyle CssClass="rowstyle" />
                                          
                    </asp:GridView>
                        </div>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                            PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_from">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                            PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txt_to">
                        </ajaxToolkit:CalendarExtender>
                        </td>
                        </tr>
                     </table>
</div> 
  