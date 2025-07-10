<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrainingRegister.aspx.cs" Inherits="TrainingRegister"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
	 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
	<div style="font-family:Arial;font-size:12px;">
		 <%--<asp:ScriptManagerProxy ID="ScriptManager1" runat="server"></asp:ScriptManagerProxy>--%>
	<table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width :100%">
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>&nbsp;Training</td>
            </tr>
		</table>
	<div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<asp:Button runat="server" ID="btnTrainingType" Text="Training Group" CssClass="selbtn" OnClick="RegisterSelect" CommandArgument="1"/>
<asp:Button runat="server" ID="btnTrainingInstitute" Text="Training Institute" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="4"/>
<asp:Button runat="server" ID="btnTraining" Text="Training Master" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="2"/>
<asp:Button runat="server" ID="btnMTraining" Text="MTM Training" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="3" Visible="false"/>
<asp:Button runat="server" ID="btnTM" Text="Matrix Master" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="5"/>
<div style="width :100%;border:solid 1px #4371a5;border-top:solid 1px #4371a5;padding:0px;margin:0px; height:4px; background-color : #4371a5">
</div>
<iframe runat="server" src="../Registers/TrainingType.aspx" id="frm" frameborder="0" width="100%" height="452px" scrolling="no"></iframe>
</div>
	</div>
   
	</asp:Content>
 