<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddVesselMiningScale.ascx.cs" Inherits="VesselRecord_AddVesselMiningScale" %>
<%@ Register TagPrefix="vbm" TagName="VesselBudgetManning" Src="~/Modules/HRD/VesselRecord/VesselBudgetManning.ascx"  %>
<%@ Register src="VesselSafeManning.ascx" tagname="VesselSafeManning" tagprefix="uc1" %>
<asp:RadioButtonList runat="server" OnSelectedIndexChanged="ManningType_Changed" id="Rad_Manning" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true">
<asp:ListItem Text="Safe Manning" Value="S" Selected="True"></asp:ListItem>
<asp:ListItem Text="Budget Manning" Value="B"></asp:ListItem>
</asp:RadioButtonList> 
 <table cellpadding="4" cellspacing="4" width="100%" border="0" style="padding-right :10px; background-color:#e2e2e2" id="tblYear" runat="server" visible="false">
                <col width="50px;" />
                <col />
              <tr >
                 <td style=" text-align :center;" >
                     <b> Year</b>
                     <asp:DropDownList ID="ddlYear" runat="server" Width="70px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged" >                         
                     </asp:DropDownList>
                     <asp:Button ID="btnImport" runat="server" OnClick="btn_Import_Click" Text="Import from Last year to Current Year" Width="240px" CausesValidation="false" OnClientClick="return confirm('Are you sure to import in current year from last year ? This will delete the existing data.');" ></asp:Button>
                 </td>
            </tr>
           </table>
<asp:Panel runat="server" ID="pan_SafeManning" Width="100%" >
<center>
    <uc1:VesselSafeManning ID="VesselSafeManning1" runat="server" />
</center>
</asp:Panel>   
<asp:Panel runat="server" ID="pan_BudgetManning" Width="100%">
<center>
    <vbm:VesselBudgetManning ID="VesselBudgetManning1" runat="server" />
</center>
</asp:Panel>   

