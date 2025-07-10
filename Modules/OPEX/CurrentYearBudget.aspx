<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrentYearBudget.aspx.cs" Inherits="CurrentYearBudget" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
     <%--<link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />--%>
 <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
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
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div>
     <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            
            <td>
                <table cellpadding="1" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="Text headerband">
                        Current Year Budget
                   </td>
                </tr>
                <tr>
                      <td>
                    <div style=" margin-top:3px; margin-left:2px;">
                        <asp:Button runat="server" ID="btnUpdateBudget" Text="Update Budget" OnClick="btnUpdateBudget_Click" CssClass="selbtn" style="border:none" />&nbsp;
                        <asp:Button runat="server" ID="btnReports" Text="Reports" OnClick="btnReports_Click" style="border:none" CssClass="btn1"/>
                        &nbsp;
                          <asp:Button ID="btnCurrentYearProjection" runat="server" Text="Current Year Projection" OnClick="btnCurrentYearProjection_OnClick" style="margin-left:3px; border:none" CssClass="btn1"/>
                        <%--<asp:Button runat="server" ID="btnPublish" Text="Publish" OnClick="btnPublish_Click" style="border:none;" CssClass="btn1"/>--%>
                        <div style="background-color:white; height:3px;"></div>
                    </div>
                    <iframe runat="server" src="UpdateBudget_CY.aspx" id="frm1" width='100%' height="490px" frameborder="no"></iframe>
                    </td>
                    </tr>
                </table>
                </td>
                </tr>
     </table>
    </div>
</asp:Content>
