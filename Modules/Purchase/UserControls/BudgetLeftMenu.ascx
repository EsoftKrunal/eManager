<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BudgetLeftMenu.ascx.cs" Inherits="UserControls_BudgetLeftMenu" %>
<div style="text-align : center;background-color : #4371a5;width:177px;min-height:465px; vertical-align : top; padding-bottom :15px;">
<asp:Image ID="Image10" runat="server" ImageUrl="~/Images/logo.jpg" Visible ="false"  />
<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}

</script>


<br />
<div style="min-height:200px; width :177px" >
<table style="width:177px; text-align : center " cellpadding="4" cellspacing="4" border="0">
    <tr id="trBudgetHome" runat="server">
        <td> 
            <a href='<%=Page.ResolveUrl("~/BudgetHome.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/budgethome.jpg")%>'  border="0"/> </a>
        </td>
    </tr>
    <tr id="trCurrBudget" runat="server">
        <td> 
            <a href='<%=Page.ResolveUrl("~/currentYearBudget.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/curryearbudget.jpg")%>'  border="0"/> </a>
        </td>
    </tr>
    <tr id="trAnalysis" runat="server">
        <td>
            <a href='<%=Page.ResolveUrl("~/ReportingAndAnalysis.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/analysis_comments.jpg")%>'  border="0"/></a>
        </td>
    </tr>
    <tr id="trBudgetForecast" runat="server">
        <td>
            <a href='<%=Page.ResolveUrl("~/BudgetForecastingNextYearNew.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/budgetforecast.jpg")%>'  border="0"/></a>
        </td>
    </tr>
    <tr id="trPublish" runat="server">
        <td>
            <a href='<%=Page.ResolveUrl("~/PublishReport.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/publishreport.jpg")%>'  border="0"/></a>
        </td>
    </tr>
                        
        <tr id="trBudgetTracking" runat="server">
        <td>
            <a href='<%=Page.ResolveUrl("~/BudgetTracking1.aspx")%>' target="_blank"><img alt="" src='<%=Page.ResolveUrl("~/Images/budgettracking.jpg")%>'  border="0"/></a>
        </td>
    </tr>
    </table>
</div>
</div>