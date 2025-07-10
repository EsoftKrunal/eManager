<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselMiningScale.ascx.cs" Inherits="VesselRecord_VesselMiningScale" %>
<%@ Register TagPrefix="vbm" TagName="VesselBudgetManning" Src="~/Modules/HRD/VesselRecord/VesselBudgetManning.ascx"  %>
<%@ Register src="VesselSafeManning.ascx" tagname="VesselSafeManning" tagprefix="uc1" %>

<%--<asp:RadioButtonList runat="server" OnSelectedIndexChanged="ManningType_Changed" id="Rad_Manning" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true">
<asp:ListItem Text="Safe Manning" Value="S" Selected="True"></asp:ListItem>
<asp:ListItem Text="Budget Manning" Value="B"></asp:ListItem>
</asp:RadioButtonList> --%>

           
<%--<asp:Panel runat="server" ID="pan_SafeManning" Width="100%" >
<center>
    <uc1:VesselSafeManning ID="VesselSafeManning1" runat="server" />
</center>
</asp:Panel>   
<asp:Panel runat="server" ID="pan_BudgetManning" Width="100%">
<center>
    <vbm:VesselBudgetManning ID="VesselBudgetManning1" runat="server" />
</center>
</asp:Panel>   --%>
<script type="text/javascript">
    function OpenSafeManning()
    {
        var VID= document.getElementById('HiddenPK').value;
        var VName = document.getElementById('HiddenVesselName').value;       
        var e = document.getElementById("VesselMiningScale1_ddlYearManning");
        var year = e.options[e.selectedIndex].value;        
        window.open('../Reporting/VesselSafeMContainer.aspx?year=' + year + '&vid=' + VID + '&vname=' + VName + '');
    }
</script>
 <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<style type="text/css">
    .highlight
    {
        color:white;
        background-color:red;
    }
    .bordered
    {
        border-collapse:collapse;
    }
    .bordered tr td{
        border:solid 1px #e1dada;
    }
</style>
<asp:Panel ID="pnlSafeManning" runat="server" >
<left>
    <div style="font-family:Arial;font-size:12px;">
    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:60px;">
         <table cellpadding="5" cellspacing="0" border="0" width="100%" class="bordered" style="height:60px;font-weight:bold;" >
       <tr class= "headerstylegrid" >
            <td>Safe Manning Grade</td>            
            <td style="text-align:center;">Safe Manning</td>
             <td>Rank Code</td>
            <td colspan="3" style="text-align:center; ">Budget Manning - 
                <div style="float:right;">
                    <asp:DropDownList ID="ddlYearManning" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYearManning_OnSelectedIndexChanged" Height="25px"  style="margin:2px;" Width="100px"></asp:DropDownList>
                </div>
           
           </td>
            <td colspan="3" style="text-align:center;">Actual Manning</td>
        </tr>
         <tr class= "headerstylegrid">
            <td></td>            
            <td style="width:120px;"></td>
            <td style="width:80px;"></td>

            <td style="text-align:center; width:70px; ">Count</td>
            <td style="text-align:center; width:80px; ">Wages</td>
            <td style="text-align:center; width:130px; ">Nationality</td>
            
            <td style="text-align:center; width:60px;">Count</td>
            <td style="text-align:center; width:80px;">Wages</td>
            <td style="text-align:center; width:300px;">Nationality</td>
        </tr>
        </table>
    </div>
    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:280px;">
    <table cellpadding="5" cellspacing="0" border="0" width="100%" class="bordered">
        <asp:Repeater ID="rptSafeManningData" runat="server" >            
            <ItemTemplate>
                <tr>
                    <td style="text-align:left;"> <%#Eval("GradeName") %> </td>                    
                    <td style="text-align:right; width:120px;"> <%#Eval("NewSafeManning") %> </td>
                    <td style="text-align:left;width:80px;"> <%#Eval("RankCode") %> </td>

                    <td style="text-align:right;width:70px;"> <%#Eval("BudgetManning") %> </td>
                    <td style="text-align:right;width:80px;"> <%#FormatCurrency(Eval("BudgetWages")) %> </td>
                    <td style="text-align:left;width:130px;"> <%#Eval("BudgetCounty") %> </td>

                    <td style="text-align:right;width:60px;"> <%#Eval("ActualManning") %> </td>
                    <td style="text-align:right;width:80px;" class='<%#HighLight(Eval("BudgetWages"),Eval("ACTUALWAGES")) %>' > <%# FormatCurrency(Eval("ACTUALWAGES")) %> </td>
                    <td style="text-align:left; width:300px;"> <%#Eval("ActualManningCountries") %> </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    
    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:25px; font-weight:bold;">
    <table cellpadding="5" cellspacing="0" width="100%" border="0" class="bordered" style="height:25px;" >
         <tr class="header-row">
            <td>Total</td>
            <td style="width:120px;text-align:right;background-color:#f9a4a4"><asp:Label ID="lblTotManning" runat="server" Font-Bold="true"></asp:Label></td>
            <td style="width:80px;">&nbsp;</td>

            <td style="text-align:right; width:70px;background-color:#f8ffae;"><asp:Label ID="lblTotBudgetManning" runat="server" Font-Bold="true"></asp:Label></td>
            <td style="text-align:right; width:80px;background-color:#f8ffae;"><asp:Label ID="lblTotBudgetWages" runat="server" Font-Bold="true"></asp:Label></td>
            <td style="text-align:center; width:130px;background-color:#f8ffae;">&nbsp;</td>
            
            <td style="text-align:right; width:60px;background-color:#9fd5ff;"><asp:Label ID="lblTotActualManning" runat="server" Font-Bold="true"></asp:Label></td>
            <td style="text-align:right; width:80px;background-color:#9fd5ff;"><asp:Label ID="lblTotActualWages" runat="server" Font-Bold="true"></asp:Label></td>
            <td style="text-align:center; width:300px;background-color:#9fd5ff;">&nbsp;</td>
        </tr>
        </table>
    </div>
    <input  value="Print" type="button" style="width:100px; margin:2px; margin-right:0px; float:right;"  class="btn" onclick="OpenSafeManning()" /> 
    <input  id="btnAddmanning" runat="server" value="Modify Manning" type="button" style="width:100px; margin:2px; margin-right:0px; float:right;"  class="btn" onclick="OpenReport();"  /> 
        </div>
</left>
</asp:Panel>