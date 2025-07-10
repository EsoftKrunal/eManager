<%@ Register Src="SearchSignOff.ascx" TagName="SearchSignOff" TagPrefix="uc2" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchReliver.ascx.cs" Inherits="CrewOperation_SearchReliver" %>
<!--  MODEL POP UP SECTION BEGIN-->
<asp:LinkButton ID="Main" runat="server" Text=" Please Confirm" OnClick="Main_Click" /> 
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../Styles/style.css" />
<script type="text/javascript">   
function Ok(sender, e)
{
$find('ctl00_contentPlaceHolder1_SearchReliver1_md1').hide();
WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$contentPlaceHolder1$SearchReliver1$Main","", true,"&","", false, true));//('SearchReliver1_Main', e); 
}
</script>
<script type="text/javascript">
    function ShowRequirement(crewid, rankid) {
        var str_vsl = '<%=Convert.ToString(Session["S_VesselId"])%>';
        var vesselid = 0;
        if (str_vsl != '') {
            vesselid = parseFloat(str_vsl)
        }

        var str_rank = '<%=Convert.ToString(Session["S_RankId"])%>';
        var newrankid = 0;
        if (str_rank != '') {
            newrankid = parseFloat(str_rank)
        }
        window.open('Crew_Required_Docs.aspx?crewid=' + crewid + '&vesselid=' + vesselid + '&rankid=' + newrankid, '', '');
    }
</script>
<ajaxToolkit:ModalPopupExtender ID="md1" runat="server" TargetControlID="Main" PopupControlID="pnl_yesno" BackgroundCssClass="modalBackground"  OnOkScript="Ok()" OkControlID="yes" CancelControlID="no" />
<asp:Panel ID="pnl_yesno" runat="server" CssClass="modalSignUp" Width="400px" Height="275px"  style="vertical-align:top; display:none;">
<table style="width: 100%;font-family:Arial;font-size:12px;" cellspacing="0" cellpadding="0" >
    <tr>
        <td style="text-align: center; background-color : Gray ; height :20px; color: White" >
            <strong>Planning Remarks</strong></td>
    </tr>
    <tr><td style="text-align: center; height:20px;">
            <asp:Label ID="lbl_MessText" runat="server" Text="Label"></asp:Label>
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
<!--  MODEL POP UP SECTION END -->
<table cellpadding="0" cellspacing="0" border="1" style="width: 100%" >
    <tr>
        <td style="text-align: center;background-color : #e2e2e2;" valign="top">
            <table cellpadding="0" cellspacing="0" width="98%" border="0" style="text-align: center"  >
                <tr>
                    <td style="width: 100px; text-align: right">
                        Emp. # :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="76px"></asp:TextBox></td>
                    <td style="width: 173px; text-align: right">
                        First Name :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_FirstName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 175px; text-align: right">
                        Last Name :</td>
                        
                    <td style="text-align: left; width: 200px;">
                        <asp:TextBox ID="txt_LastName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 133px; text-align: right">
                        Rank :</td>
                    <td style="width: 180px; text-align: left;">
                        <asp:DropDownList ID="ddl_Rank" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 2px; text-align: right">
                        Owner Pool :</td>
                    <td style="width: 173px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_OwnerPool" runat="server" CssClass="input_box">
                    </asp:DropDownList></td>
                    <td style="width: 173px; height: 2px; text-align: right">
                        Vessel Type:</td>
                    <td style="width: 173px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselType_SelectedIndexChanged" Width="200px">
                    </asp:DropDownList>
                        </td>
                    <td style="width: 175px; height: 2px; text-align: right">
                        Vessel :</td>
                    <td style="width: 200px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="185px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged">
                    </asp:DropDownList>
                       </td>
                    <td style="width: 133px; height: 2px; text-align: right">
                        Status :</td>
                    <td style="width: 180px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style1">
                        Rec.Office :</td>
                    <td style="text-align: left" class="style2">
                        <asp:DropDownList ID="ddl_RecOff" runat="server" CssClass="input_box"></asp:DropDownList>
                    </td>
                    <td style="text-align: right" class="style2">
                        &nbsp;Avail. Date :</td>
                    <td style="text-align: left" class="style2">
                        <asp:DropDownList ID="ddl_Month" runat="server" CssClass="input_box" Width="50px" >
                        <asp:ListItem Text="Select" Value="0" ></asp:ListItem>  
                        <asp:ListItem Text="Jan" Value="1" ></asp:ListItem>  
                        <asp:ListItem Text="Feb" Value="2" ></asp:ListItem>  
                        <asp:ListItem Text="Mar" Value="3" ></asp:ListItem>  
                        <asp:ListItem Text="Apr" Value="4" ></asp:ListItem>  
                        <asp:ListItem Text="May" Value="5" ></asp:ListItem>  
                        <asp:ListItem Text="Jun" Value="6" ></asp:ListItem>  
                        <asp:ListItem Text="Jul" Value="7" ></asp:ListItem>  
                        <asp:ListItem Text="Aug" Value="8" ></asp:ListItem>  
                        <asp:ListItem Text="Sep" Value="9" ></asp:ListItem>  
                        <asp:ListItem Text="Oct" Value="10" ></asp:ListItem>  
                        <asp:ListItem Text="Nov" Value="11" ></asp:ListItem>  
                        <asp:ListItem Text="Dec" Value="12" ></asp:ListItem>  
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_Year" runat="server" CssClass="input_box" Width="70px"></asp:DropDownList>
                    </td>
                    <td style="text-align: right" class="style3">
                        </td>
                    <td style="text-align: left" class="style4">
                        <asp:CheckBox ID="chk_Exclude" runat="server" Text="Exclude Planned Crew" /></td>
                    <td style="text-align: right" class="style5">
                        </td>
                    <td style="text-align: center" class="style6">
                    <asp:Button ID="btn_Search" CausesValidation="false" Width="60px"  runat="server" Text="Search" style=" background-color:RED; color:White; border:none; padding:3px; " OnClick="btn_Search_Click" />
                        </td>
                </tr>
                </table>
            <table style="display : none">
            <Td><asp:CheckBox ID="chk_BON" runat="server" Text="Budgeted Nationality" /></Td>
            <Td> Matrix Exp. : <asp:DropDownList ID="ddl_Matrix" runat="server" CssClass="input_box">
                        </asp:DropDownList></Td>
            <td>Nationality : <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="input_box" Width="185px">
                        </asp:DropDownList></td>
            </table>
        </td>
    </tr>
    <tr>
        <td style="text-align:left;" >
        <div id="div-datagrid" style=" width:100%; height :175px; overflow-y:Scroll; overflow-x;hidden: ; text-align:center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="98%" AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting"  OnRowCommand="Row_Command" GridLines="Horizontal" OnPreRender="GridView1_PreRender" CellPadding ="0" CellSpacing="0"  >
            <HeaderStyle CssClass="headerstylefixedheadergrid" />
            <PagerStyle CssClass="pagerstyle" />
            <RowStyle CssClass="rowstyle" />
            <SelectedRowStyle CssClass="selectedtowstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Assign"  >
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Assign" ImageUrl="~/Modules/HRD/Images/group.gif" ID="Assign" CausesValidation="false" />
                    </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:BoundField DataField="EMPNO" SortExpression="EMPNO" HeaderText="Emp #">
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                    <asp:Label id="lbl_Name" runat="Server" Text='<%#Eval("Name")%>'></asp:Label>
                    <asp:HiddenField id="lbl_HiddenCrewId" runat="Server" Value='<%#Eval("Crewid")%>'></asp:HiddenField>
                    <asp:HiddenField id="lbl_HiddenRankId" runat="Server" Value='<%#Eval("RankId")%>'></asp:HiddenField>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rank" SortExpression="Rank" HeaderText="Rank">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nationality" SortExpression="nationality" HeaderText="Nationality">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LastVessel" SortExpression="LastVessel" HeaderText="Last Vessel">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PlanVessel" SortExpression="PlanVessel" HeaderText="Planned Vessel">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExpDate" SortExpression="ExpDate" HeaderText="Exp. Join Date">
                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                    </asp:BoundField>
                      <asp:BoundField DataField="AVLDate" SortExpression="AVLDate" HeaderText="Available From">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
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
