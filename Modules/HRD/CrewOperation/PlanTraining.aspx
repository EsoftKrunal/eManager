<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanTraining.aspx.cs" Inherits="PlanTraining" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57 || event.keyCode == 46)) {
                 event.returnValue = false;
             }
         }
</script>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="~/Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="~/Styles/StyleSheet.css" />
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
        <tr>
        <td>
            <table cellpadding="0" cellspacing="0" width="100%">
             <tr>
                <td style=" background-color:#C2C2C2;padding-3px;">
                     <table width="100%"  cellpadding="2" cellspacing="0" rules="all" border="1">
                     <tr>
                         <td style="text-align: right" class="style1">Training Location :</td>
                         <td style="text-align: left" class="style1">
                                 <asp:DropDownList ID="ddlCrewStatus" runat="server" Width="120px" CssClass="required_box" >
                                    <asp:ListItem Text="< Select >" Value="" Selected="True"> </asp:ListItem>
                                    <asp:ListItem Text="Shipboard" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Shorebased" Value="0"></asp:ListItem>
                                 </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCrewStatus" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>
                             </td>
                         <td style="text-align: right" class="style1">Vessel :</td>
                         <td style="text-align: left" class="style1">
                                 <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" TabIndex="10" Width="167px" ValidationGroup="s">
                                 </asp:DropDownList>
                                 <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="*" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                 </td>
                            <td style="text-align: right; " class="style1">
                                                                  Recruitment Off :
                            </td>
                             <td style="text-align: left; " class="style1">
                                <asp:DropDownList ID="ddlRecruitmentOff" runat="server" Width="120px" CssClass="input_box" ></asp:DropDownList>
                                 </td>
                         <td style="text-align: left; " class="style1">
                            
                            
                            <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="input_box" TabIndex="7" Visible="false"
                                 Width="53px">
                                 <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:TextBox ID="txtName" runat="server" CssClass="input_box" MaxLength="6" TabIndex="1" Visible="false"
                                 Width="29px"></asp:TextBox>
                            
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align: right">Due In :</td>
                         <td style="text-align: left">
                                 <asp:DropDownList ID="ddlMonth" runat="server" CssClass="input_box" TabIndex="16"
                                     Width="70px">
                                     <asp:ListItem Text="Month" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                     <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                     <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                     <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                     <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                     <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                     <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                     <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                     <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                     <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                     <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                 </asp:DropDownList>
                                 <asp:DropDownList ID="ddlYear" runat="server" CssClass="input_box" TabIndex="16" Width="70px">
                                 </asp:DropDownList>
                                 
                                    </td>
                         <td style="text-align: right">
                                 
                                    <div style="float:right;"> Rank :</div>
                                 </td>
                         <td style="text-align: left">
                          <asp:DropDownList ID="ddlRank" runat="server" CssClass="input_box" TabIndex="7" Width="100px" ></asp:DropDownList>
                            
                            
                                 </td>
                            <td style="text-align: right; height: 19px;">
                                 Source :</td>
                             <td style="text-align: left; height: 19px;">
                                <asp:DropDownList ID="ddlSource" runat="server" CssClass="input_box"  Width="100px">
                                    <asp:ListItem Text="< All >" Value=""> </asp:ListItem>
                                    <asp:ListItem Text="MATRIX" Value="MATRIX"> </asp:ListItem>
                                    <asp:ListItem Text="PEAP" Value="PEAP"> </asp:ListItem>
                                    <asp:ListItem Text="ASSIGNED" Value="ASSIGNED"> </asp:ListItem>
                                </asp:DropDownList>
                                 </td>
                         <td style="text-align: left; ">
                            <asp:Button ID="btn_show" runat="server" CssClass="btn" Text=" Search " OnClick="btn_show_Click" TabIndex="2" Width="96px" CausesValidation="true" ValidationGroup="s" />
                            
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align: right">&nbsp;</td>
                         <td style="text-align: left">
                                    <asp:CheckBox ID="chkOverDue" runat="server" Text="Over Due Only"  
                                        OnCheckedChanged="chkOverDue_OnCheckedChanged" 
                                 AutoPostBack="true" />
                                </td>
                         <td style="text-align: right">Emp No. :</td>
                         <td style="text-align: left">
                             <asp:TextBox ID="txt_MemberId" runat="server" CssClass="input_box" MaxLength="6"
                                 TabIndex="1" Width="60px"></asp:TextBox>
                                 </td>
                            <td style="text-align: right; height: 19px;">
                                 &nbsp;</td>
                             <td style="text-align: left; height: 19px;">
                                <asp:CheckBox ID="chkRec" runat="server" Text="Promotion Recommended" />
                                 
                                 </td>
                         <td style="text-align: left; ">
                                <asp:Button ID="btnClear" runat="server" CssClass="btn" Text=" Clear " OnClick="btnClear_Click" Width="96px" CausesValidation="false" />
                            
                         </td>
                     </tr>
                         <tr>
                            <td style="text-align: right;"  >Training :
                            </td>                     
                         <td style="text-align: left; " colspan="3">
                                <asp:TextBox ID="txtTraining" runat="server" CssClass="input_box" Width="450px" ></asp:TextBox>
                         </td>   
                                 
                                 <td style="text-align: right; height: 19px;">
                                 Planed For : </td>
                             <td style="text-align: left; height: 19px;">
                                <asp:UpdatePanel ID="up1" runat="server" >
                                <ContentTemplate>
                                    Next
                                    <asp:TextBox ID="txtPlanedForFilter" runat="server" CssClass="input_box" Width="30px" OnTextChanged="txtDaysText_Changed" AutoPostBack="true" MaxLength="3" onkeypress="fncInputNumericValuesOnly(this)"></asp:TextBox> (Days)
                                </ContentTemplate>
                                </asp:UpdatePanel>
                             </td>
                         </tr>
                         </table>
                </td>
             </tr>
             <tr>
                <td style="border:solid 1px gray;">
                        <table width="100%" cellpadding="2" cellspacing="1" rules="all" border="1" >
                        <col width="25px" />
                        <col width="25px" />
                        <col width="50px" />
                        <col width="150px" />
                        <col width="50px" />
                        <col />
                        <col width="75px" />
                        <col width="75px" />
                        <col width="75px" />
                        <col width="150px" />
                        <col width="80px" />   
                        <col  />   
                            <tr class="headerstylegrid" style="font-weight:bold;">
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_OnCheckedChanged" ToolTip="All" AutoPostBack="true"  />
                                </td>
                                <td>Crew #</td>
                                <td>Crew Name</td>
                                <td>Rank</td>
                                <td>Training Name</td>
                                <td>Source</td>
                                <td>Due Date</td>
                                <td>Plan. For</td>
                                <td>Institute</td>
                                <td>Planned By</td>                                
                                <td style="width:13px;"></td>
                            </tr>
                        </table>
                        <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 245px">
                        <table width="100%" cellpadding="2" cellspacing="1" rules="all" border="1" >
                        <col width="25px" />
                        <col width="25px" />
                        <col width="50px" />
                        <col width="150px" />
                        <col width="50px" />
                        <col />
                        <col width="75px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="150px" />
                        <col width="80px" /> 
                            <asp:Repeater ID="rptPlanTraining" runat="server" >
                                <ItemTemplate>
                                    <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                                        <td><asp:ImageButton runat="server" ID="btnDel" ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDeleteTraining_Click" Visible='<%#((Eval("SOURCE").ToString())=="ASSIGNED")%>' CommandArgument='<%#Eval("TrainingRequirementID")%>' CausesValidation="false" OnClientClick="javascript:confirm('Are You Sure to Delete?');" /> </td>
                                        <td>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </td>
                                        <td>
                                            <%#Eval("CREWNUMBER")%>
                                            <asp:HiddenField ID="hfdPKId" runat="server" Value='<%#Eval("TrainingRequirementID")%>' />
                                            <asp:HiddenField ID="hfdCrewId" runat="server" Value='<%#Eval("CrewID")%>' />
                                            <asp:HiddenField ID="hfdTID" runat="server" Value='<%#Eval("TrainingID")%>' />
                                        </td>
                                        <td align="left"><%#Eval("CREWNAME")%></td>
                                        <td><%#Eval("RANKCODE")%></td>
                                        <td align="left">■<%#Eval("TRAININGNAME_Similer").ToString().Replace(",", "</br>■")%></td>
                                        <td><asp:Label runat="server" ID="lblSOURCE" Text='<%#Eval("SOURCE")%>'></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblDueDate" Text='<%#Common.ToDateString(Eval("NEXTDUE"))%>'></asp:Label></td>
                                        <td><%#Common.ToDateString(Eval("PlannedFor"))%></td>
                                        <td align="left"><%#Eval("Institute")%></td>
                                        <td align="left"><%#Eval("PlannedBy")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        </div>                        
                </td>
             </tr> 
             <tr>
                <td style="text-align:center">
                <center>
                <asp:Label ID="lblTotRow" runat="server" style="font-weight:bold;float:left; margin:4px;" ></asp:Label>
                <table cellpadding="2" cellspacing="0" width="600px" border="0" style="margin:3px;">
                            <tr style="text-align:right">
                                <td>Planned For :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox ID="txtPlanedFor" runat="server" Width="80px" CssClass="input_box"  ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtPlanedFor"></asp:RequiredFieldValidator>
                                                                        
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                                       PopupButtonID="txtPlanedFor" PopupPosition="TopLeft" 
                                       TargetControlID="txtPlanedFor">
                                   </ajaxToolkit:CalendarExtender>
                                </td>
                                
                                <td>Institute :</td>
                                <td style="text-align:left;">
                                    <asp:DropDownList ID="ddlInstitute" runat="server" CssClass="input_box" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlInstitute"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdatePlan" runat="server" OnClick="btnUpdatePlan_OnClick" Text=" Save " CssClass="btn" Width="100px" />
                                </td>
                            </tr>                        
                        </table>
                 </center>
                </td>
            </tr>
            </table>
        </td>
        </tr>
    </table>
</div>
<ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtTraining" ServicePath="~/WebService.asmx" ServiceMethod="GetTrainingName" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList" CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH" ></ajaxToolkit:AutoCompleteExtender>
</form>
</body>
</html>
