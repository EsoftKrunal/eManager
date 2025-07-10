<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Registers.ascx.cs" Inherits="UserControls_Registers" %>
<style>
.current
{
	border-left :solid 1px white;
	border-right:solid 1px white;
	border-top :solid 1px white;
	border-bottom:none;
	background-color :#62A9FF;
	text-align:center; 
}
.normal
{
	border-left :solid 1px white;
	border-right:solid 1px white;
	border-top :solid 1px white;
	border-bottom:none;
	background-color :#4371a5;
	text-align:center;
}
</style>
<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<div>
    <table style="width :100%" cellpadding="0" cellspacing="0">
    <tr>
    <td style="vertical-align: top; text-align: left;">
    <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width: 100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td class="text headerband" style=" padding:4px;" >Purchase Masters
    <%--<asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Images/home.png" style="float :right; padding-right :5px" /> --%> 
    </td>
    </tr>
    </table>
    </td> 
    </tr> 
    </table> 
    <table style="width :100%" cellpadding="0" cellspacing="0">
    <tr>
    <td style="vertical-align: top; text-align: left;">
    <table style="text-align:Left; width: 100%;font-family:Arial;" cellpadding="4" cellspacing="0">
    <tr id="t1" style="padding-left:10px;">
        <td>
            <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/CompanyMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Company"></asp:LinkButton> 
        &nbsp;
      <%--  <asp:LinkButton runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/VesselMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Vessel Master"></asp:LinkButton> --%>
       &nbsp;
        <asp:LinkButton ID="AccountMajor"  runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/AccountMajorMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Major Accounts"></asp:LinkButton> 
       &nbsp;
        <asp:LinkButton ID="AccMinor" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/AccountMinorMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Minor Accounts"></asp:LinkButton> 
       &nbsp;
        <asp:LinkButton ID="AccMid" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/AccountMidMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Mid Accounts"></asp:LinkButton> 
       &nbsp;
        <asp:LinkButton ID="Department" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/DepartmentMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Departments"></asp:LinkButton> 
        &nbsp;
        <asp:LinkButton ID="Accounts" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/AccountMaster.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Account Codes"></asp:LinkButton> 
       &nbsp;
            <asp:LinkButton ID="lnkMapping" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/AccountMapping.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Account Mapping"></asp:LinkButton> 
            &nbsp;
             <asp:LinkButton ID="lnkStoreMaster" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/StoreManagement.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Store Items"></asp:LinkButton> 
            &nbsp;
             <asp:LinkButton ID="lnkUnCategorisedStoreItem" runat="server" PostBackUrl="~/Modules/Purchase/Purchase_Masters/StoreManagementUncategorised.aspx" Font-Size="14px" Font-Bold="true" ForeColor="#206020" Font-Underline="false" Text="Uncategorised Store Items"></asp:LinkButton>
        </td>
        
    </tr>
    <tr>
    <td  style ="background-color:#62A9FF; height :3px;"></td>
    </tr>
    </table>
    </td> 
    </tr> 
    </table> 
</div>

<script>
        function tabing()
        {
             var strHref = window.location.href;
             var strQueryString = strHref.split(".aspx");
             var aQueryString = strQueryString[0].split("/");
             var curpage =unescape(aQueryString[aQueryString.length-1]).toLowerCase();
             //var menunum=document.getElementById('header').getElementsByTagName('li').length;
             //var menu=document.getElementById('header').getElementsByTagName('li')
             
             //alert(curpage);
             
             
             if(curpage=='companymaster')
                tdComapny.className='current';             
             if(curpage=='vesselmaster')
                tdVessel.className='current';
             if(curpage==unescape('accountmajormaster'))
                tdAccountMajor.className='current';
             if(curpage==unescape('accountminormaster'))
                tdAccMinor.className='current';
             if(curpage==unescape('accountmidmaster'))
                tdAccMid.className='current';
             if(curpage==unescape('departmentmaster'))
                tdDepartment.className='current';
             if(curpage==unescape('accountmaster'))
                tdAccounts.className='current';
             if(curpage==unescape('accountmapping'))
                tdAccMapping.className='current';
                
        }
        tabing();
   </script>