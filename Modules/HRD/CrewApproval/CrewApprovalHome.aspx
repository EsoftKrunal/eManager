<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="CrewApprovalHome.aspx.cs" Inherits="CrewApproval_CrewApprovalHome" Title="Crew Approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <%-- <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
/*.color_tab
{
  border:none;  
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
  background: #facc8c;
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
}*/
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
        <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td  class="text headerband" >
            <img runat="server" id="img1" moduleid="9" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
            Approvals</td>
            </tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
        <div style="">
            <asp:Button runat="server" ID="btnDashBoard" Text="Owner/Manager Approval" CssClass="btn1" onclick="btnDashBoard_Click"  />&nbsp;
            <asp:Button runat="server" ID="btnApp" Text="Manning Office Approval" CssClass="btn1" onclick="btnApp_Click"  Visible="False" />&nbsp;
            <asp:Button runat="server" ID="Button1" Text="Vessel Assignment Approval" CssClass="btn1" onclick="Button1_Click" Visible="False"  />
            <asp:Button runat="server" ID="Button2" Text="Crew Documents" CssClass="btn1" onclick="Button2_Click" Visible="False" />
            <asp:Button runat="server" ID="Button3" Text="Crew Assessment" CssClass="btn1" onclick="btbCrewAssessment_Click" Visible="False" />

            <asp:Button runat="server" ID="btnChecklist" Text="Approval Management" CssClass="btn1" onclick="btnChecklist_Click" Visible="False" />
        </div>
        <div style="width:100%; height:3px; background-color:#facc8c"></div>
            <div>

            <iframe runat="server" id="frm" width="100%" height="556px" scrolling="no" frameborder="1"></iframe>    
            </div>
        </td>
        </tr>
      </table>
      </div>
    </asp:Content>
