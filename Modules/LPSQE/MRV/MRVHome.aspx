<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MRVHome.aspx.cs" Inherits="Modules_LPSQE_MRV_MRVHome" %>

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
	 <link rel="stylesheet" href="../../CSS/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../../CSS/StyleSheet.css" />
	 <link rel="stylesheet" type="text/css" href="../../CSS/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
        <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
         <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table style="background-color: #f9f9f9; border-collapse:collapse; width :100%" border="1" bordercolor="#4371a5" >
            <tr>
                <td style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband">
                    <%--<img runat="server" id="imgHelp" moduleid="2" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/images/help.png" alt="Help ?"/>--%> 
                     MRV 
                </td>
            </tr>
            <tr>
                <td style=" background-color: #f9f9f9; vertical-align:top; overflow:visible;" >
                    <div style=" text-align:left;float:left">
                        <table>
                            <tr>
                                <td><asp:Button runat="server" Text="MRV" CommandArgument="0" ID="btnMRVHome" CssClass="btn1"  Font-Bold="True" OnClick="Menu_Click" /></td>
                                <td>
                                    <asp:Button runat="server" Text=" Vessel SetUp" CommandArgument="1" ID="btnVesselSetUp" CssClass="btn1"  Font-Bold="True" OnClick="Menu_Click" /> 
                                </td>
                                <td><asp:Button runat="server" Text="Fuel Types" CommandArgument="2"  ID="btnFuleTypes"  CssClass="btn1"  Font-Bold="True" OnClick="Menu_Click"/></td>
                                <td><asp:Button runat="server" Text="Activity" CommandArgument="3"  ID="btnActivity"  CssClass="btn1"  Font-Bold="True" OnClick="Menu_Click" Visible="false"/></td>
                                 <td><asp:Button runat="server" Text="Report" CommandArgument="4"  ID="btnReport"  CssClass="btn1"  Font-Bold="True" OnClick="Menu_Click" /></td>
                            </tr>
                        </table>
                         
                    </div>
                   
                  
                </td>
            </tr>
        </table>
        </td>
        </tr> 
        </table>  
    </div>
<div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/LPSQE/MRV/Home.aspx" id="frm" frameborder="0" width="100%" height="485px" scrolling="no"></iframe>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainFoot" Runat="Server">
</asp:Content>

