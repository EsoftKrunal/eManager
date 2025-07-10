<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.ascx.cs" Inherits="UserControls_LeftMenu" %>
<div style="width :290px; height:88px; text-align :center; float: left ; vertical-align :top">
<div><asp:ImageButton runat="server" ID="btn_CrewList" 
        ImageUrl="~/images/curr_crew_list.png" onclick="btn_CrewList_Click"  /> </div>
<div><asp:ImageButton runat="server" ID="btn_vet_stat" 
        ImageUrl="~/images/vett_stat.png" onclick="btn_vet_stat_Click"  /> </div>
<div><asp:ImageButton runat="server" ID="btn_ins_rept" ImageUrl="~/images/tech_ins_rep.png"  /> </div>
</div> 
