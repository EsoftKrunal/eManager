<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPlanning.aspx.cs" Inherits="CrewOperation_NewPlanning" MasterPageFile="~/Modules/HRD/CrewPlanning.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <div>
<script type="text/javascript">
    function ShowRequirement(crewid, rankid) {
        var vslClientId = '<%=ddl_VesselName.ClientID %>';
        var ind = document.getElementById(vslClientId).selectedIndex;
        var vesselid = 0;
        if (parseFloat(ind) > 0)
        {
            vesselid = document.getElementById(vslClientId).options[ind].value;
        }
        window.open('Crew_Required_Docs.aspx?crewid=' + crewid + '&vesselid=' + vesselid + '&rankid=' + rankid,'','');
    }
</script>

<style type="text/css">
       .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_B
       {
           background:yellow; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_D
       {
           background:red; 
           width:15px;
           height:15px;
           color:white;
           border:solid 1px grey;
       }
    </style>
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellspacing="0" width="100%" style=" background-color:#f9f9f9" >
        <tr>
        <td style="width: 50%; text-align: center"><asp:Label ID="lb_msg" runat="server" ForeColor="Red" ></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align:center">
            <table border="0" cellpadding="0" cellspacing="0" style="text-align:center" width="100%">
        <tr>
            <td>
                <table style="background-color:#f9f9f9" border="1" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: center; text-align : left;background-color : #e2e2e2;"">Vessel: &nbsp; &nbsp;<asp:DropDownList ID="ddl_VesselName" Width="248px" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselName_SelectedIndexChanged"> </asp:DropDownList></td>
                    </tr>
                       <tr>
                        <td style="">
                        <table border="0" cellspacing="0" cellpadding="0px"  style="text-align: center;" width="100%">
                            <tr><td style="padding-top: 0px">  
                            <div style="width:100%;overflow-x:hidden; overflow-y:scroll; height:170px" >
                            <asp:GridView ID="gvsearch" runat="server" OnRowCommand="gvsearch_RowCommand"  AllowSorting="True" OnSorted="on_Sorted1" OnSorting="on_Sorting1"  AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Height="32px" Style="text-align: center" Width="98%" OnRowDataBound="GV_OnRowDataBound">
                                    <Columns>
                                     <asp:TemplateField Visible="false" >
                                       <ItemTemplate>
                                       <asp:CheckBox ID="chk_select" runat="server" />                                                   
                                       </ItemTemplate>
                                       </asp:TemplateField>
                                      
                                      <asp:CommandField ButtonType="Image" Visible="false"   HeaderText="Select" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
                                          <ItemStyle Width="30px" />
                                      </asp:CommandField>
                                       <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                               <asp:ImageButton ID="img_Delete" runat="Server" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="img_Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" />
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                      
                                      
                                      <asp:BoundField DataField="CrewNumber"  SortExpression="CrewNumber" HeaderText="Emp. #" >
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                      </asp:BoundField>
                             
                                                                      
                                        
                                        
                                         <asp:TemplateField HeaderText="Name" SortExpression="CrewName" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server"  Text='<%# Eval("CrewName") %>'></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                                              
                                       
                                       <asp:TemplateField HeaderText="Rank" SortExpression="RankName" >
                                       <ItemTemplate>
                                       <asp:Label ID="lblrankname" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
                                       <asp:Label ID="lblrankid" runat="server" Width="0px" Visible="false" Text='<%# Eval("CurrentRankId") %>'></asp:Label>
                                       <span style="display:none">
                                       <asp:Label ID="lblvesselid" runat="server" Text='<%# Eval("CurrentVesselId") %>'></asp:Label>
                                       <asp:Label ID="Lb_CrewID" runat="server" Text='<%# Eval("crewid") %>'></asp:Label>
                                       <asp:Label ID="lb_R_ID" runat="server" Text=""></asp:Label>
                                       <asp:Label ID="Lb_ReliverID" runat="server" Text='<%# Eval("crewid") %>'></asp:Label>
                                       <asp:Label ID="lb_ReliverID1" runat="server" Text=""></asp:Label>
                                       <asp:Label ID="Lb_ReliverRankId" runat="server" Text='<%# Eval("CurrentRankId") %>'></asp:Label>
                                       <asp:Label ID="Lb_ReliverRankId1" runat="server" Text=""></asp:Label>   
                                       </span>  
                                       </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Left" />
                                       </asp:TemplateField>
                                        <asp:BoundField DataField="Nationality"  SortExpression="Nationality" HeaderText="Nationality">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                             <asp:BoundField DataField="VesselCode"  SortExpression="VesselCode" HeaderText="Vessel Code" >
                                            <ItemStyle HorizontalAlign="Left" />
                                      </asp:BoundField>
                                        
                                        <asp:BoundField DataField="EOC"  SortExpression="EOC" HeaderText="EOC">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExpectedJoinDate"  SortExpression="ExpectedJoinDate" HeaderText="Exp. Join Dt.">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Checklist">
                                        <ItemTemplate>
                                                       <asp:ImageButton ID="btnCL" CommandName="img_dc" runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Document CheckList" OnClick='btnCL_Click'/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
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
        <tr>
    <td style=" text-align:center;">
    <script type="text/javascript">   
    function Ok(sender, e)
    {
    $find('ctl00_contentPlaceHolder1_md1').hide();
    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$contentPlaceHolder1$Main","", true,"&","", false, true));//('SearchReliver1_Main', e); 
    }
    </script>
    <asp:LinkButton ID="Main" runat="server" Text=" Please Confirm" OnClick="Main_Click" /> 
    <ajaxToolkit:ModalPopupExtender ID="md1" runat="server" TargetControlID="Main" PopupControlID="pnl_yesno" BackgroundCssClass="modalBackground"  OnOkScript="Ok()" OkControlID="yes" CancelControlID="no"/>
    <asp:Panel ID="pnl_yesno" runat="server" CssClass="modalSignUp" Width="400px" Height="275px"  style="vertical-align:top; display:none;">
    <table style="width: 100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="text-align: center; background-color : Gray ; height :20px; color: White" >
                <strong>Planning Remarks</strong></td>
        </tr>
        <tr><td style="text-align: center; height:20px;">
                <asp:Label ID="lbl_prompt" runat="server" Text="Label"></asp:Label>
                </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:TextBox ID="txt_PRemarks" runat="server" CssClass="input_box" Width="350px" Height="200px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
    <tr>
        <td style="text-align: center; padding-top : 5px;" >
            <asp:Button ID="yes" runat="server" Text="Yes" CssClass="btn" Width="52px"/>
            <asp:Button ID="no" runat="server" Text="No" CssClass="btn" Width="54px"/> 
        </td>
    </tr>
    </table>
    </asp:Panel> 
    <table cellpadding="0" cellspacing="0" border="1" style="width: 100%" >
    <tr>
        <td style="text-align: center;background-color : #e2e2e2;"" valign="top">
            <table cellpadding="1" cellspacing="0" width="98%" border="0" style="text-align: center"  >
                <tr>
                    <td style="width: 190px; text-align: right">
                        Emp. # :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="76px"></asp:TextBox></td>
                    <td style="width: 143px; text-align: right">
                        First Name :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_FirstName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 222px; text-align: right">
                        Last Name :</td>
                    <td style="text-align: left; width: 166px;">
                        <asp:TextBox ID="txt_LastName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 134px; text-align: right">
                        Rank :</td>
                    <td style="width: 180px; text-align: left;">
                        <asp:DropDownList ID="ddl_Rank" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 190px; text-align: right">
                        Owner Pool :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:DropDownList ID="ddl_OwnerPool" runat="server" CssClass="input_box">
                    </asp:DropDownList></td>
                    <td style="width: 143px; text-align: right">
                        Vessel Type :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselType_SelectedIndexChanged" Width="176px">
                    </asp:DropDownList></td>
                    <td style="width: 222px; text-align: right">
                        Vessel :</td>
                    <td style="text-align: left; width: 166px;">
                        <asp:DropDownList ID="ddl_Vessel" runat="server" Width="180px" 
                            CssClass="input_box" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged">
                    </asp:DropDownList></td>
                    <td style="width: 134px; text-align: right">
                        Status :</td>
                    <td style="width: 180px; text-align: left;">
                        <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 190px; text-align: right">
                        Rec. Office :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:DropDownList ID="dd_RecOff" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                    <td style="width: 143px; text-align: right">
                        &nbsp;</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:CheckBox ID="chkfamily" runat="server" Text="Family Members" /></td>
                    <td style="width: 222px; text-align: right">
                        &nbsp;</td>
                    <td style="text-align: left; width: 166px;">
                        <asp:CheckBox ID="chk_Exclude" runat="server" Text="Exclude Planned Crew" />
                    </td>
                    <td style="width: 134px; text-align: right">
                        &nbsp;</td>
                    <td style="width: 160px; text-align: center;">
                    <asp:Button ID="btn_Search" CausesValidation="false" Width="60px"   runat="server" Text="Search" CssClass="input_box" OnClick="btn_Search_Click" /></td>
                </tr>
                </table>
            <table style=" display : none" >
                <tr>
                <td> Matrix Exp. :<asp:DropDownList ID="ddl_Matrix" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                <td>  <asp:CheckBox ID="chk_BON" runat="server" Text="Budgeted Nationality" /></td>
                <td> Nationality :<asp:DropDownList ID="dd_Nationality" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                </tr>
            </table> 
        </td>
    </tr>
    <tr>
        <td style=" text-align:left;padding-top:5px" >
         <div id="div-datagrid" style=" width:100%; height :175px; overflow-y:Scroll; overflow-x;hidden: ;text-align:center">
            <asp:GridView ID="GridView1" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" AutoGenerateColumns="False" Width="98%" OnRowCommand="Row_Command" GridLines="Horizontal" OnPreRender="GridView1_PreRender" CellPadding="0" CellSpacing="0">
            <HeaderStyle CssClass="headerstylefixedheadergrid" />
            <PagerStyle CssClass="pagerstyle" />
            <RowStyle CssClass="rowstyle" />
            <SelectedRowStyle CssClass="selectedtowstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Add"  >
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                    <asp:ImageButton runat="server"  CommandName="Assign" ImageUrl="~/Modules/HRD/Images/group.gif" ID="Assign" />
                    </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:BoundField DataField="EMPNO"  SortExpression="EMPNO" HeaderText="Emp. #">
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name" >
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                    <asp:Label id="lbl_Name" runat="Server" Text='<%#Eval("Name")%>'></asp:Label>
                    <asp:HiddenField id="lbl_HiddenCrewId" runat="Server" Value='<%#Eval("Crewid")%>'></asp:HiddenField>
                    <asp:HiddenField id="lbl_HiddenRankId" runat="Server" Value='<%#Eval("RankId")%>'></asp:HiddenField>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rank"  SortExpression="Rank" HeaderText="Rank">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nationality"  SortExpression="nationality" HeaderText="Nationality">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vessel"  SortExpression="LastVessel" HeaderText="Last Vessel">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LastVessel" SortExpression="PlanVessel" HeaderText="Planned Vessel">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExpDate"  SortExpression="ExpDate" HeaderText="Exp. Join Date">
                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                    </asp:BoundField>
                      <asp:BoundField DataField="AVLDate"  SortExpression="AVLDate" HeaderText="Available From">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="OR">
                     <ItemStyle HorizontalAlign="Center" Width="40px" />
                     <ItemTemplate>
                      <div class='Grade_<%#Eval("OwnerRep")%>'><%#Eval("OwnerRep")%></div>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CH">
                     <ItemStyle HorizontalAlign="Center" Width="40px" />
                     <ItemTemplate>
                      <div class='Grade_<%#Eval("Charterer")%>'><%#Eval("Charterer")%></div>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TS">
                     <ItemStyle HorizontalAlign="Center" Width="40px" />
                     <ItemTemplate>
                      <div class='Grade_<%#Eval("TechSupdt")%>'><%#Eval("TechSupdt")%></div>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FM">
                     <ItemStyle HorizontalAlign="Center" Width="40px" />
                     <ItemTemplate>
                      <div class='Grade_<%#Eval("FleetMgr")%>'><%#Eval("FleetMgr")%></div>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MS">
                     <ItemStyle HorizontalAlign="Center" Width="40px" />
                     <ItemTemplate>
                      <div class='Grade_<%#Eval("MarineSupdt")%>'><%#Eval("MarineSupdt")%></div>
                     </ItemTemplate>
                    </asp:TemplateField>
                    
                      <%--<asp:TemplateField HeaderText="CheckList" SortExpression="" >
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <img src="../Images/cv.png" alt='Document Requirement' title='Document Requirement' onclick="<%# "ShowRequirement(" + Eval("Crewid").ToString()+ "," +Eval("RankId").ToString() + ");" %> " style='cursor:pointer' />
                        
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
         </div>
        </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
    </div>
</asp:Content>
    