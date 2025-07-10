<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CrewTravel.aspx.cs" Inherits="CrewOperation_CrewTravel" Title="EMANAGER" %>
<%@ Register Src="../UserControls/GraphicalPlanningTool.ascx" TagName="GraphicalPlanningTool" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <style type="text/css">
         .auto-style1 {
             width: 100%;
             height: 431px;
         }
     </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div  class="text headerband">
         <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
        Ticketing
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<table cellpadding="4" cellspacing="0" width="100%" >
<tr>
<td style=" text-align:right;width:150px;" >Vessel : </td>
<td style=" text-align:left;width:200px;" ><asp:DropDownList ID="ddl_VesselName" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselName_SelectedIndexChanged"></asp:DropDownList></td>
<td style=" text-align:right;width:150px;" >Port Call Status: </td>
<td style=" text-align:left;width:200px;" >
    <asp:DropDownList ID="ddl_PCStatus" Width="120px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_port_SelectedIndexChanged">
<asp:ListItem Text=" < ALL > " Value=""></asp:ListItem>
<asp:ListItem Text="Open" Value="O" Selected="True"></asp:ListItem>
<asp:ListItem Text="Closed" Value="C"></asp:ListItem>
</asp:DropDownList>
   </td>
<td style=" text-align:right;width:150px;" >Crew # :</td>
<td style=" text-align:left;width:150px;">
    <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="65px" OnTextChanged="txt_EmpNo_TextChanged" AutoPostBack="True"></asp:TextBox>
    </td>
<td style=" text-align:right" ><asp:Label ID="lblCountry" runat="server" Text="Country :" Visible="false" />  </td>
<td style=" text-align:left" >
     <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="176px" Visible="false"> </asp:DropDownList>
</td>
<td style=" text-align:right" ><asp:Label ID="lblPort" runat="server" Text="Port :" Visible="false" />  </td>
<td style=" text-align:left" > <asp:DropDownList ID="ddl_port" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_port_SelectedIndexChanged" Visible="false"></asp:DropDownList><asp:ImageButton ID="imgaddport" Visible ="false"  runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="return openpage();" /></td>
</tr>
</table>
<table cellpadding="2" cellspacing="0"  width="100%" >
<tr>
<td style="text-align :left; width :100%;vertical-align :top;" >
    <div style='width:100%;height:30px; text-align:right'>
        <asp:Label runat="server" ID="lblCount" style="float:left" Font-Bold="true" Font-Size="16px" ></asp:Label>
         <asp:LinkButton ID="lbSendTicketRequest" runat="server" Text="Send Tickets Request" style="width:130px;"  Font-Bold="True" ForeColor="#206020" OnClick="lbSendTicketRequest_Click" /> &nbsp;
        <asp:LinkButton ID="btnOrderTick" runat="server" Text="Order Tickets" style="width:130px;" OnClick="Order_Tickets" Font-Bold="True" ForeColor="#206020" /> &nbsp;
        <asp:LinkButton ID="btnChangeTick" runat="server" Text="Tickets Mgmt." style=" width:130px;" OnClick="btnTicketMgmt_Click" Font-Bold="True" ForeColor="#206020" />&nbsp;
        <asp:LinkButton ID="btnInvMgmt" runat="server" Text="Invoice Mgmt." style=" width:130px;" OnClick="btnInvMgmt_Click" Font-Bold="True" ForeColor="#206020" /> &nbsp;
    </div>
</td>
</tr>
<tr>
<td style="text-align :left; width :100%;vertical-align :top;" >
    <div style="text-align :left; overflow-x :hidden; overflow-y :scroll; border:solid 1px Gray;" class="auto-style1">
            <asp:GridView ID="GvRefno" runat="server" Width="100%" CellPadding="2" AutoGenerateColumns="False" GridLines="Horizontal" >
            <Columns>
              <asp:TemplateField HeaderText="Port Call #" SortExpression="PortReferenceNumber" >
              <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                       <asp:LinkButton ID="btnrefno" runat="server" status='<%#Eval("Status")%>' Text='<%#Eval("PortReferenceNumber") %>'  Font-Underline="false" CommandName="Select" ForeColor="Blue"></asp:LinkButton>  
                       <asp:HiddenField ID="HiddenPortCallId" runat="server" Value='<%#Eval("PortCallId")%>' />
                    </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <%#Eval("Status")%>
                      <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Status")%>' />
                  </ItemTemplate>
                  <ItemStyle Width="60px" HorizontalAlign="left" />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Total S/On)" ShowHeader="False"  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate><%#Eval("COUNT_SIGNON")%></ItemTemplate>
                <ItemStyle Width="90px" HorizontalAlign="Center"  ForeColor="#206020"/>
                <HeaderStyle  />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Total S/Off)" ShowHeader="False"  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate><%#Eval("COUNT_SIGNOFF")%></ItemTemplate>
                <ItemStyle Width="90px" HorizontalAlign="Center" ForeColor="Purple"/>
                <HeaderStyle  />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Active Tickets" ShowHeader="False"  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                <div style='<%#( Common.CastAsInt32(Eval("TICKETS_A")) != Common.CastAsInt32(Eval("COUNT_SIGNON")) + Common.CastAsInt32(Eval("COUNT_SIGNOFF")) ) ?"color:red":""%>'>
                <b>
                <%#Eval("TICKETS_A")%>
                </b>
                </div>
                </ItemTemplate>
                <ItemStyle Width="90px" HorizontalAlign="Center" />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Tickets Cancelled" ShowHeader="False"  HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate><%#Eval("TICKETS_C")%></ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
              </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="rowstyle" />
            <SelectedRowStyle Font-Bold="true" BackColor="Yellow"/>
            <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
    </div>
    
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

