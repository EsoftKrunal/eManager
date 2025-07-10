<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewChange.aspx.cs" Inherits="CrewOperation_CrewChange1" Title="Crew Change" %>
<%--<%@ Register Src="../UserControls/GraphicalPlanningTool.ascx" TagName="GraphicalPlanningTool" TagPrefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
        <style type="text/css">
            .auto-style1 {
                position: absolute;
                top: 0px;
                left: 0px;
                height: 510px;
                width: 100%;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
 <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="text-align :left; vertical-align: top;">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
           <%-- <tr>
                <td style="text-align:center;" class="text headerband">
                    Deployment
                </td>
            </tr>--%>
            <tr>
                <td>       
                    <table cellpadding="4" cellspacing="0" width="100%" style="background-color:white" >
<tr>
<td style=" text-align:right;width:150px;" >Vessel : </td>
<td style=" text-align:left;width:200px;" ><asp:DropDownList ID="ddl_VesselName" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselName_SelectedIndexChanged"></asp:DropDownList></td>
<td style=" text-align:right;width:100px;" > Status :  </td>
<td style=" text-align:left;width:200px;" >
     <asp:DropDownList ID="ddlStatus" runat="server" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged">
            <asp:ListItem Value="" Text="All"></asp:ListItem>
            <asp:ListItem Value="O" Text="Open"></asp:ListItem>
            <asp:ListItem Value="C" Text="Closed"></asp:ListItem>
     </asp:DropDownList>
 </td>
<td style=" text-align:right;width:100px;" >Crew # :  </td>
<td style=" text-align:left;width:200px;" >
    <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="65px" OnTextChanged="txt_EmpNo_TextChanged" AutoPostBack="True"></asp:TextBox>
    </td>
<td style=" text-align:right;width:100px;" > <asp:Label ID="lblCountry" runat="server" Text="Country :" Visible="false"></asp:Label>  </td>
    <td style=" text-align:left" >
       <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="176px" Visible="false"> </asp:DropDownList>   
    </td>
<td style=" text-align:right" ><asp:Label ID="lblPort" runat="server" Text="Port :" Visible="false"></asp:Label> </td>
<td style=" text-align:left" > <asp:DropDownList ID="ddl_port" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_port_SelectedIndexChanged" Visible="false"></asp:DropDownList><asp:ImageButton ID="imgaddport" Visible ="false"  runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="return openpage();" /></td>
</tr>
</table>
<table cellpadding="2" cellspacing="0"  width="100%" >
<tr>
<td style="text-align :left; width :265px;vertical-align :top;" >
    <div style="text-align :left; width :100%; height :255px; overflow-x :hidden ; overflow-y :scroll; border:solid 1px Gray;">
            <asp:GridView ID="GvRefno" runat="server" Width="250px" CellPadding="2" AutoGenerateColumns="False" GridLines="Horizontal" OnRowCommand="GvRefno_RowCommand" Style="text-align: center" OnRowDeleting="GvRefno_Row_Deleting">
            <Columns>
            <asp:TemplateField HeaderText="Port Call #" SortExpression="PortReferenceNumber" >
              <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                       <asp:LinkButton ID="btnrefno" runat="server" status='<%#Eval("Status")%>' Text='<%#Eval("PortReferenceNumber") %>'  Font-Underline="false" CommandName="Select" ForeColor="DarkGreen" Font-Size="12px"></asp:LinkButton>  
                       <asp:HiddenField ID="HiddenPortCallId" runat="server" Value='<%#Eval("PortCallId")%>' />
                    </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
              <%#Eval("Status")%>
              </ItemTemplate>
              </asp:TemplateField>
               <asp:TemplateField HeaderText="" ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonCancel" runat="server" Visible='<%# (Eval("Status").ToString()=="Open") %>' CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="rowstyle" />
            <SelectedRowStyle Font-Bold="true"/>
            <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
    </div>
    <b>Promotion/Vessel Rename/Contract Rivision </b>
    <div style="text-align :left; width :100%; height :150px; overflow-x :hidden ; overflow-y :scroll; border:solid 1px Gray;">
            <asp:GridView ID="GvRefno2" runat="server" Width="335px" CellPadding="2" AutoGenerateColumns="False" GridLines="Horizontal" OnRowCommand="GvRefno_RowCommand2" Style="text-align: center" >
            <Columns>
            <asp:TemplateField HeaderText="Port Call #" SortExpression="PortReferenceNumber" >
              <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                       <asp:LinkButton ID="btnrefno" runat="server" status='Open' Text='<%#Eval("PortReferenceNumber") %>'  Font-Underline="false" CommandName="Select" ForeColor="DarkGreen" Font-Size="12px"></asp:LinkButton>  
                       <asp:HiddenField ID="HiddenPromotionSignOnId" runat="server" Value='<%#Eval("PromotionSignOnId")%>' />
                         <asp:HiddenField ID="hdnPCMode" runat="server" Value='<%#Eval("PCMode")%>' />
                    </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
             <asp:Label ID="lblTag" runat="server" Text="Open" Font-Size="12px"></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>
               <asp:TemplateField HeaderText="" ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonCancel" runat="server" Visible='False' CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="rowstyle" />
            <SelectedRowStyle Font-Bold="true"/>
            <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
    </div>
    <%--<b>Contract Rivision </b>
    <div style="text-align :left; width :100%; height :75px; overflow-x :hidden ; overflow-y :scroll; border:solid 1px Gray;">
            <asp:GridView ID="Gv_ContractRivision" runat="server" Width="335px" CellPadding="2" AutoGenerateColumns="False" GridLines="Horizontal"  Style="text-align: center" OnRowCommand="Gv_ContractRivision_RowCommand" >
            <Columns>
            <asp:TemplateField HeaderText="Port Call #" SortExpression="PortReferenceNumber" >
              <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                       <asp:LinkButton ID="btnContractrefno" runat="server" status='Open' Text='<%#Eval("PortReferenceNumber") %>'  Font-Underline="false" CommandName="Select" ForeColor="DarkGreen" Font-Size="12px"></asp:LinkButton>  
                       <asp:HiddenField ID="hdnCrewContractRevisionId" runat="server" Value='<%#Eval("ContractRevisionId")%>' />
                    </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
              <asp:Label ID="lblTag" runat="server" Text="Open" Font-Size="12px"></asp:Label>
                  
              </ItemTemplate>
                  
              </asp:TemplateField>
               <asp:TemplateField HeaderText="" ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonCancel" runat="server" Visible='False' CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="rowstyle" />
            <SelectedRowStyle Font-Bold="true"/>
            <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
    </div>--%>
</td>
<td style="text-align :left; vertical-align :top;" >
    <center>
    <div style="width :100%; text-align :right; padding :2px;">
    <div style="float:left">
    <asp:CheckBox runat="server" ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged" Text="Select All" AutoPostBack="true"/>&nbsp;&nbsp;
        
    </div>
    <b> 
    Sort By : 
    <asp:LinkButton runat="server" id="lnkCrewNo" Text="Crew#" CommandArgument="CrewNumber" OnClick="SortGrid" ForeColor="#206020" Font-Size="12px"></asp:LinkButton> / 
    <asp:LinkButton runat="server" id="lnkName" Text="Name" CommandArgument="CrewName" OnClick="SortGrid" ForeColor="#206020"  Font-Size="12px"></asp:LinkButton> / 
    <asp:LinkButton runat="server" id="lnkRank" Text="Rank" CommandArgument="RankName" OnClick="SortGrid" ForeColor="#206020"  Font-Size="12px"></asp:LinkButton> / 
    <asp:LinkButton runat="server" id="lnkSignOn" Text="SignOn" CommandArgument="SignOnDate" OnClick="SortGrid" ForeColor="#206020"  Font-Size="12px"></asp:LinkButton> / 
    <asp:LinkButton runat="server" id="lnkReliefDue" Text="ReliefDue" CommandArgument="ReliefDueDate" OnClick="SortGrid" ForeColor="#206020"  Font-Size="12px"></asp:LinkButton> /
    <asp:LinkButton runat="server" Visible="false" id="lnkExpectedJoinDate" Text="Exp.JoinDt." CommandArgument="ExpectedJoinDate" OnClick="SortGrid" ForeColor="#206020"  Font-Size="12px"></asp:LinkButton>
    </b>&nbsp;
    </div>
    </center>
    <div style="text-align :left; width :100%; height :410px; overflow-x :hidden ; overflow-y :scroll; border:solid 1px Gray;">
    <asp:GridView ID="gvsearch" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Width="98%" OnPreRender="gvsearch_OnPreRender" >
    <Columns>
    <asp:TemplateField ItemStyle-Width="20px">
    <ItemTemplate>
            <asp:CheckBox ID="chkselect" value='<%#Eval("CrewId")%>' runat="server" Visible='<%# ViewState["PC_Status"].ToString()=="Open" %>' />
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Crew #" SortExpression="CrewNumber" ItemStyle-Width="50px" ControlStyle-Font-Size="12px" ControlStyle-Font-Names="Arial">
    <ItemTemplate>
       <asp:HiddenField ID="hfdEmpNo" runat="server" Value='<%#Eval("CrewNumber")%>' />
       <asp:LinkButton ID="btncrewnosignoff" runat="server" Text='<%#Eval("CrewNumber") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
       <asp:HiddenField ID="HiddencrewIdsignoff" runat="server" Value='<%#Eval("CrewId")%>' />
       <asp:HiddenField ID="HfdCrewFlag" runat="server" Value='<%#Eval("CrewFlag")%>' />
       <asp:HiddenField ID="hfdPlanningId" runat="server" Value='<%#Eval("PlanningId")%>' />
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Crew Name" SortExpression="CrewName" ControlStyle-Font-Size="12px" ControlStyle-Font-Names="Arial">
        <ItemStyle HorizontalAlign="Left" />
        <ItemTemplate>
            <asp:Label ID="lblCompanyNamesignoff" runat="server" Text='<%# Eval("CrewName") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Actions" ItemStyle-Width="225px" ControlStyle-Font-Size="12px" ControlStyle-Font-Names="Arial">
        <ItemTemplate>
            <asp:ImageButton ID="imgCheckList" runat="server" ImageUrl="~/Modules/HRD/Images/check.png" ToolTip="Print Checklist"/> 
            <asp:ImageButton ID="imgContract" runat="server" ImageUrl="~/Modules/HRD/Images/icon_pencil.gif" ToolTip="Show Contract"/> 
            <asp:LinkButton ID="lnkAction" Font-Underline="false" runat="server" Text="" Font-Bold="True" Font-Italic="True"></asp:LinkButton> 
            
            <asp:ImageButton ID="prnCheckList" runat="server" ImageUrl="~/Modules/HRD/Images/pdf_icon.png" ToolTip="Print Chek List"/> 
            <asp:ImageButton ID="prnContract" runat="server" ImageUrl="~/Modules/HRD/Images/pdf_icon.png" ToolTip="Print Contract"/> &nbsp;&nbsp;
            <asp:LinkButton ID="lbPrejoingDoc" Font-Underline="false" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="DarkGreen" Text="Prejoing Docs"> </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Rank" SortExpression="RankName" ControlStyle-Font-Size="12px" ControlStyle-Font-Names="Arial">
        <ItemStyle HorizontalAlign="Left" />
        <ItemTemplate>
            <asp:Label ID="lblranknamesignoff" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="SignOn / Relief Due" SortExpression="RankName" ItemStyle-Width="180px" ControlStyle-Font-Size="12px" ControlStyle-Font-Names="Arial">
        <ItemStyle HorizontalAlign="Left" />
        <ItemTemplate>
          <div style='display:<%#(Eval("CrewFlag").ToString()=="O")?"block":"none" %>'>
            <span style="color :Green;"><%# Eval("SignOnDate") %></span> / <span style="color :Red"><%# Eval("ReliefDueDate")%></span> 
          </div>
         <%-- <div style='display:<%#(Eval("CrewFlag").ToString()=="I")?"block":"none" %>'>
           <span style="color :Blue"><%# Eval("ExpectedJoinDate")%></span>
          </div>--%>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField ItemStyle-Width="20px" ControlStyle-Font-Size="12px" ControlStyle-Font-Names="Arial" >
    <ItemTemplate>
            <asp:ImageButton ID="btnDel" runat="server" CausesValidation="False" crewtype='<%#Eval("CrewFlag")%>' OnClick="Delete_Crew" CommandArgument='<%#Eval("CrewId")%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="rowstyle" />
    <SelectedRowStyle Font-Bold="true" />
    <HeaderStyle CssClass="headerstylefixedheadergrid" />
</asp:GridView>
    </div>
    <div style=" padding-top :2px; width :100%; text-align :left" >
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align:left;">
                        <input type="button" onclick="window.open('CreatePortCall.aspx','');" value="Create New Port Call" class="btn"  /> 
                        <asp:Button runat="server" id="btnAddCrew" OnClick="btnAddCrew_Click" Text="Add Crew in Port Call"  CssClass="btn" Visible="false" ></asp:Button>
                        
                </td>
                <td></td>
                <td style="text-align:right;">
                          <div style=" display :none">
                            <asp:Button runat="server" ID="btnRefresh" Text ="Refresh" onclick="Refresh_Click"  /></div>
                            <img src="../images/mail.gif" runat="server" Visible="false" /> 
                     <asp:Button Id="btnMailTravel" runat="server" Text="&nbsp;Travel Agent"  Width="100px"   onclick="btnMailTravel_Click" CssClass="btn" Visible="false" /> &nbsp;&nbsp;
                     <img src="../images/mail.gif" runat="server" />         
                    <asp:Button ID="btnMailPort" runat="server" Text="&nbsp;Port Agent"  Width="90px"  onclick="btnMailPort_Click" CssClass="btn" />

                </td>
            </tr>
        </table>
  
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


    
<!--  Add New Crew in Port Call -->
    <div id="dvAddCrew" runat="server" visible="false" class="auto-style1">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
            <center >
                 <div style="text-align:right; padding:5px;">
            <asp:Button ID="btnClose1" CausesValidation="false" style="background-color:RED; color:White; border:none; padding:3px; " Width="30px" runat="server" Text="X" OnClick="btnClose1_Click" />
                    </div>
            <iframe id="ftmCrewlist" runat="server" src="" height="490px" width="100%" scrolling="no" frameborder="0">

            </iframe>
               
            </center>
        </div>
    </center>
</div>
</ContentTemplate>
</asp:UpdatePanel>
 </form>
</body>
</html>

