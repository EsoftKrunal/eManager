<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMS.aspx.cs" Inherits="CMS" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="eReports/JS/jquery.min.js"></script>
     <style type="text/css" >
    .btnNormal
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;
	    font-weight: bold;
	    background-color:#A3C8EC;
	    height: 25px;	        
	    border:none;	    
	    color:#000000;
    }
    
    .btnSelected
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;	    
	    background-color:#007ACC;
	    color:White;
	    font-weight: bold;
	    height: 25px;	    
	    border:none;	    
    }
    
    .color_tab{
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

    .color_tab_sel{
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
      border-bottom:solid 1px #facc8c;
    }
</style>
<script type="text/javascript">

    function ShowModal() {
        $("#dvModal").css('display','');
    }
    function HideModel() {
        $("#dvModal").css('display', 'none');
    }
    
</script>
</head>
<body style='font-size:12px;' >
    <form id="form1" runat="server" >
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <table style="text-align : center " cellpadding="0" cellspacing="0" border="0">
                <tr >
                    <td>
                        <asp:Button ID="btnCrewList" CssClass="btnNormal"  Text=" Crew List "  runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="1" />
                    </td>
                    <td>
                        <asp:Button ID="btnRestHour" CssClass="btnNormal"  Text=" Rest Hour "  runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="3" />
                    </td>
                    <td>
                        <asp:Button ID="btnRestAdmin" CssClass="btnNormal"  Text=" Rest Hour Admin "  runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="2" />
                    </td>

                    <td>
                        <asp:Button ID="btnPeap" CssClass="btnNormal"  Text=" PEAP "  runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="4" />
                    </td>
                    <td>
                        <asp:Button ID="btnMedicalReport" CssClass="btnNormal"  Text=" Medical Report "  runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="5" />
                    </td>
                    <td>
                        <asp:Button ID="btnTraining" CssClass="btnNormal"  Text=" Training "  runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="6" />
                    </td>

                    <td class="btnNormal">&nbsp;</td>
                </tr>
     </table>
     <iframe runat="server" width="100%" height="455px" id="frm1"></iframe>
     <div>
     </div>
     <mtm:footer ID="footer1" runat ="server" />
     </div>
    </form>
</body>
</html>
