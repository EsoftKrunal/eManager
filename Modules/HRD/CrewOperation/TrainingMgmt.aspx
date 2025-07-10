<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="TrainingMgmt.aspx.cs" Inherits="TrainingMgmt"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManager1" runat="server"></asp:ScriptManagerProxy>
<style type="text/css">
.selbtn
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
	 <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="MonitorTraining.aspx" id="frm" frameborder="0" width="100%" height="485px" scrolling="no"></iframe>
</div>

</asp:Content>
 