<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KSTMenu.ascx.cs" Inherits="KSTMenu" %>
<style type="text/css">
.color_tab
{
  border:none;  
  /*background: #3498db;*/
  background: #3498db;
  background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
  background-image: -moz-linear-gradient(top, #3498db, #2980b9);
  background-image: -ms-linear-gradient(top, #3498db, #2980b9);
  background-image: -o-linear-gradient(top, #3498db, #2980b9);
  background-image: linear-gradient(to bottom, #3498db, #2980b9);
  -webkit-border-radius: 0;
  -moz-border-radius: 0;
  border-radius: 0px;
  font-family: Arial;
  color: #ffffff;
  font-size: 12px;
  padding: 5px 9px 5px 9px;
  text-decoration: none;
}

.color_tab:hover {
 background: #3cb0fd;
  background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
  background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
  background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
  background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
  background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
  text-decoration: none;
}

.color_tab_sel
{
  border:none;  
  background: orange;
  background-image: -webkit-linear-gradient(top, #f7af51, #facc8c);
  background-image: -moz-linear-gradient(top, #f7af51, #facc8c);
  background-image: -ms-linear-gradient(top, #f7af51, #facc8c);
  background-image: -o-linear-gradient(top, #f7af51, #facc8c);
  background-image: linear-gradient(to bottom, #f7af51, #facc8c);
  font-family: Arial;
  color: black;
  font-size: 12px;
  padding: 5px 9px 5px 9px;
  text-decoration: none;
}

</style>
<div style="text-align : left;width:100%; padding:0px; padding-top:3px;">
    <asp:button ID="btnHome" runat="Server" Text="Home" CssClass="color_tab" CommandArgument="KSTHome.aspx" OnClick="btn_Tab_Click" CausesValidation="false"/>
    <asp:button ID="btnClassification" runat="Server" Text="Classification Master" CssClass="color_tab" CommandArgument="Classification.aspx" OnClick="btn_Tab_Click" CausesValidation="false"/>
    <asp:button ID="btnbehaviours" runat="Server" Text="Behaviours Master" CssClass="color_tab" CommandArgument="Behaviours.aspx" OnClick="btn_Tab_Click" CausesValidation="false"/>
    <asp:button ID="btnmtmvalue" runat="Server" Text="MTM Value Master" CssClass="color_tab" CommandArgument="MTMValueMaster.aspx" OnClick="btn_Tab_Click" CausesValidation="false"/>
</div> 
<div style="width:100%; height:5px; background-color:orange"></div>
