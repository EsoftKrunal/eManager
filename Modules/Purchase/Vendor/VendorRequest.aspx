<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRequest.aspx.cs" Inherits="Vendor" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

<title>Purchase Requisition</title>
<meta http-equiv="x-ua-compatible" content="IE=9" />
<script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>   
<script type="text/javascript" src="JS/Common.js"></script>   
<script language="javascript" type="text/javascript">    
    function OpenNew() {
        var key = '<%= System.Configuration.ConfigurationManager.AppSettings["NewVendor"].ToString() %>';
        window.open(key, '');
    }
    function showprogress(ctl) {
        var leafname = $(ctl).attr('src').split('\\').pop().split('/').pop();
        var newname = $(ctl).attr('src').replace(leafname, 'progress.gif');
        $(ctl).attr('src',newname);
    }
</script>
<style type="text/css">
    .label
    {display:inline-block; text-align:left; float:left; width:150px; position:relative;}
    .label:after
    {content:":"; text-align:right; width:100px; padding-right:5px; display:inline; float:right; position:absolute; right:5px;top:0px;}
    .controlarea
    {display:inline-block; text-align:left; }
    .row
    {
        padding:3px;
        text-align:left;
    }
    .control
    {
        border:solid 1px #ddd;
        padding:5px;
        line-height:14px;
    }
.alternate_table 
{
border:solid 1px #f1f1f1;
border-collapse:collapse;
}
.alternate_table tr:nth-child(even)
{
background-color:#f1f1f1;
}
.alternate_table tr:nth-child(odd)
{
background-color:white;
}

.alternate_table tr td
{
border:solid 1px #eee;
}
.alternate_tableheader 
{
-collapse:collapse;
}
.alternate_tableheader tr td
{
border:solid 1px #86A2DB;
border-bottom:none;
color:#333;
background-color:#527ACC;
color:White;
vertical-align:middle;
height:27px;
}
.btn
{
    background-color: #527acc;
    border: medium none;
    color: #fff;
    font-size: 12px;
    padding: 7px;
    min-width: 80px;
    text-align:center;
    vertical-align:middle;
}
input
{
    border:solid 1px #ddd;
    padding:5px;
    line-height:12px;
    font-size:12px;
}
select
{
    border:solid 1px #ddd;
    padding:3px;
    line-height:12px;
    font-size:12px;
}
 .actionbox
    {
        text-align:center;
        padding:5px;
    }
.bgmodal
{
    background-color:Black;
    opacity: 0.6;
    filter: alpha(opacity=60);
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:5;
}
.modalframe
{
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:6;
    text-align:center;
    margin:0px auto;
    padding-top:5%;    
}
.modalborder
{
    background:rgba(0,0,0,0.3);
    width:800px;
    margin:0px auto;
    padding:12px;
    padding-top:1px;
    vertical-align:top;
}
.modalcontainer
{
    width:100%;
    height:100%;
    background-color:White;
    vertical-align:top;
}
.msgbox
{
    padding:5px 5px 7px 5px;
    border:solid 1px #eee;
    text-align:center;
}
.success
{
    background-color:#00A300;
    color:White;
}
.error
{
    background-color:#FF7E5E;
    color:White;
}
 .*
    {
        box-sizing:border-box;
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
    }
</style>
</head>
<body style="font-family:Calibri; font-size:13px; margin:0px;">
<form id="form1" runat="server" >   
<asp:ToolkitScriptManager ID="r1" runat="server"></asp:ToolkitScriptManager>
<div class="bgmodal" runat="server" id="modalBox" visible="false"></div>
<div>
<table cellpadding="5" cellspacing="0"  border="0" rules="none" width="100%">
<tr>
<td>
<table cellpadding="0" cellspacing="0"  border="0" rules="none" width="100%">
<tr>
<td>Company Name</td>
<td>: <asp:TextBox runat="server" ID="txt_CompanyName"></asp:TextBox> </td>
<td>Activity</td>
<td>:
<asp:DropDownList ID="ddlActivity" runat="server">
<asp:ListItem Text="Select Activity" Value="0"></asp:ListItem>
<asp:ListItem Text="Evaluation" Value="1"></asp:ListItem>
<asp:ListItem Text="Approval" Value="2"></asp:ListItem>
</asp:DropDownList> </td>
<td>Status</td>
<td>:
<asp:DropDownList ID="ddlStatus" runat="server">
<asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
<asp:ListItem Text="New Vendor" Value="2"></asp:ListItem>
<asp:ListItem Text="Awaiting First Approval" Value="3"></asp:ListItem>
<asp:ListItem Text="Awaiting Second Approval" Value="4"></asp:ListItem>
<asp:ListItem Text="Rejected" Value="6"></asp:ListItem>
</asp:DropDownList>
</td>
<td>Email Address</td>
<td>: <asp:TextBox runat="server" ID="txt_EmailAddress"></asp:TextBox> </td>
</tr>
</table>
</td>
<td style="text-align:right; width:200px;">
    <asp:Button runat="server" ID="btnSearchProfile" Text="Search" OnClick="btnSearchProfile_Click" CssClass="btn" />
     <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" OnClientClick="OpenNew();" CssClass="btn" />
</td>
</tr>
</table>
</div>
<div style="width:100% ; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">
<table cellpadding="3" cellspacing="0"  border="0" rules="none" width="100%" class="alternate_tableheader">
<colgroup>
        <col width="100px" />
        <col width="50px" style="text-align:center"/>
        <col/>  
        <col width="150px"/>
        <col width="150px"/>  
        <col width="250px"/>  
        <%--<col width="150px"/>  
        <col width="150px"/>                      
        <col width="150px"/>    --%>
        <col width="80px"/>    
        <col width="150px"/>        
        <col width="100px"/>                           
        <col width="17px" />             
        </colgroup>                     
        <tr>
        <td style="text-align:center">Edit</td>
        <td style="text-align:center">SRNo</td>
        <td style="text-align:left">Company Name</td>
        <td style="text-align:left">Contact Person</td>
        <td style="text-align:left">Contact No</td>
        <td style="text-align:left">Email Address</td>
        <%--<td style="text-align:left">Proposed By</td>          
        <td style="text-align:left">Country</td>                 
        <td style="text-align:left">City/State</td> --%>
        <td style="text-align:left">Valid Till</td>
        <td style="text-align:left">Status</td>
        <td style="text-align:left">Activity</td>    
        <td>&nbsp;</td>      
    </tr>
    </table>        
</div>     
<div style="width:100% ; height:390px; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ; border-bottom:solid 1px #eee">
    <table cellpadding="3" cellspacing="0"  border="0" rules="none" width="100%" class="alternate_table" >
        <colgroup>
        <col width="100px" style="text-align:center"/>
        <col width="50px" style="text-align:center"/>
        <col/>  
        <col width="150px" style="text-align:left"/>
        <col width="150px" style="text-align:left"/>
        <col width="250px" style="text-align:left"/>    
        <%--<col width="150px" style="text-align:left"/>  
        <col width="150px" style="text-align:left"/>                      
        <col width="150px" style="text-align:left"/>  --%>
        <col width="80px"/>    
        <col width="150px"/>                        
        <col width="100px"/>                        
        <col width="17px" style="text-align:left" /> 
            
        </colgroup>
    <asp:Repeater ID="rptVendorRequest" runat="server"  OnItemCommand="rptVendorRequest_OnItemCommand"  OnItemDataBound="rptVendorRequest_ItemDataBound" >                     
    <ItemTemplate>           
        <tr>                
            <td style="text-align:center">
            <asp:ImageButton ID="ImgEdit" runat="server" tooltip="Edit record." Visible='<%#Eval("AllowToEditByMail").ToString()=="Y"%>' ImageUrl="Images/addpencil.gif" CommandArgument='<%#Eval("GUID")+","+ Eval("VRID")%>' CommandName="EditVednor"></asp:ImageButton>
            <asp:ImageButton ID="ImgEmail" runat="server" tooltip="Email to vendor" Visible='<%#Eval("AllowToEditByMail").ToString()=="Y"%>' Width="12px" CommandArgument='<%#Eval("GUID")+","+ Eval("EmailAddress")%>' CommandName="emailToVendor" ImageUrl="Images/email.png" OnClientClick="showprogress(this);"></asp:ImageButton>
            <asp:ImageButton ID="ImgAllowToResubmit" runat="server" tooltip="Allow to resubmit." Visible='<%#Eval("AllowToEditByMail").ToString()=="N"%>' ImageUrl="Images/reset.png" CommandArgument='<%#Eval("GUID")+","+ Eval("VRID")%>' CommandName="AllowResubmit"></asp:ImageButton>
            
            <%--<asp:ImageButton ID="ImgRequestStatus" runat="server" tooltip="Status" Visible='<%#(Convert.ToInt32(Eval("RequestApprovalStatus"))>=2 && Convert.ToInt32(Eval("RequestApprovalStatus"))<= 5)?true:false%>' ImageUrl="Images/gear.png" CommandArgument='<%#Eval("VRID")%>' CommandName="RequestStatus"  ></asp:ImageButton>--%>
            </td>
            <td style="text-align:center"><%#Eval("SNo")%>.</td>
            <td><%#Eval("CompanyName")%></td>                               
            <td> <%#Eval("ContactPersonName")%> </td>
            <td> <%#Eval("CP_MobileNo")%> </td>
            <td> <%#Eval("EmailAddress")%> </td>
            <%--<td> <%#Eval("ProposedByName") %>- <%#Eval("ProposedByPosition")%> </td>                 
            <td> <%#Eval("CountryName")%></td> 
            <td> <%#Eval("City_State")%></td>  --%>
            <td> <%#Common.ToDateString(Eval("ValidityDate"))%></td>  
            <td> <%#Eval("StatusText")%></td>  
            <td><asp:LinkButton ID="lnk_ActivityStatus" runat="server" Text='<%#Eval("ActivityStatus")%>' tooltip="Activity Status" CommandArgument='<%#Eval("VRID")%>' CommandName="ActivityStatus"></asp:LinkButton></td>                      
            <td>&nbsp;</td>
        </tr>            
    </ItemTemplate>
</asp:Repeater>
</table>
</div>
<div class="msgbox" runat="server" id="lblOuterMessage"></div>

<div class="modalframe" runat="server" id="modalframeVendroProfile" visible="false">
<div class="modalborder">
<div class="modalcontainer">     
     <%--Approval div--%>
     <div class="formcontainer" runat="server" visible="false" id="div_Approval">
        <h2> <asp:Label ID="lblApprovalStatge" runat="server"></asp:Label></h2>
        <div class="row">           
         <span class="controlarea">
             <asp:HiddenField ID="hdn_VIDVendor" runat="server"></asp:HiddenField>
             <asp:Button ID="btn_Proposal" runat="server"  CssClass="btn"  Text="Proposal"  OnClick="btn_Proposal_Click" /> 
             <asp:Button ID="btn_Approval" runat="server"  CssClass="btn"  Text="Approval"  OnClick="btn_Approval_Click" />   
            </span>
        </div>
        <%-- <div class="row">           
         <span class="label">Action</span>
            <span class="controlarea">
           <asp:RadioButtonList ID="rd_ApprovalAcrion" runat="server"  AutoPostBack="true"
                RepeatDirection="Horizontal" 
                onselectedindexchanged="rd_ApprovalAcrion_SelectedIndexChanged" >
           <asp:ListItem Text="Approved" Value="A" Selected="True"></asp:ListItem>
           <asp:ListItem Text="Reject" Value="R"></asp:ListItem>
           </asp:RadioButtonList>
            </span>
        </div>
        
       <div class="row">
            <span class="label">Name</span>
            <span class="controlarea">
            <asp:HiddenField ID="hdn_VRID" runat="server" />
            <asp:HiddenField ID="hdn_RequestStatus" runat="server" />
            <asp:TextBox runat="server" ID="txt_ApprovedBy" CssClass="control" Width="500px" MaxLength="250" ReadOnly="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txt_ApprovedBy"></asp:RequiredFieldValidator>                       
            </span>
        </div>
        <div class="row">
            <span class="label">Position</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_ApprovedPosition"  ReadOnly="true" CssClass="control" Width="500px" MaxLength="250"></asp:TextBox>       
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_ApprovedPosition"></asp:RequiredFieldValidator>    
            </span>
        </div>      
        <div class="row" runat="server" id="div_approvaltype" > 
          <span class="label">Vendor Type</span>                   
            <span class="controlarea">
            <asp:DropDownList ID="ddlApprovalType" runat="server" Width="150px"> 
                <asp:ListItem Text="Approval Type" Value="0"></asp:ListItem>
                <asp:ListItem Text="Nominated" Value="1"></asp:ListItem>
                <asp:ListItem Text="OTA" Value="2"></asp:ListItem>
                <asp:ListItem Text="Other" Value="3"></asp:ListItem>               
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlApprovalType"></asp:RequiredFieldValidator>       
               
                Valid Till : <asp:TextBox ID="txt_ValidityDate" runat="server" MaxLength="11" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_ValidityDate"></asp:RequiredFieldValidator>
                <asp:CalendarExtender runat="server" ID="c1" Format="dd-MMM-yyyy" TargetControlID="txt_ValidityDate"></asp:CalendarExtender>
            </span>

         </div>
        <div class="row">
            <span class="label">Remarks</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_ApprovedRemakrs" CssClass="control" Width="500px"  TextMode="MultiLine" Rows="3" MaxLength="250"></asp:TextBox>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txt_ApprovedRemakrs"></asp:RequiredFieldValidator>         
            </span>
        </div>
        <div class="actionbox">            
              <asp:Button runat="server" ID="btn_SaveRequestStatus" OnClick="btn_SaveRequestStatus_Click" CssClass="btn" Text="Save"  />       
              <asp:Button runat="server" ID="btn_closemodel" OnClick="btn_closemodel_Click" CssClass="btn close" Text="Close"/>
         </div>--%>
        </div>
     <%--Rejected div
     <div class="formcontainer" runat="server" visible="false" id="div_Rejected">
        <div class="row">           
            <span class="label"></span>
            <span class="controlarea">
                <asp:Label ID="lbl_rejected" runat="server"></asp:Label>
            </span>
        </div>
            <div class="actionbox">  
                <asp:Button runat="server" ID="btn_closeRejected" OnClick="btn_closemodel_Click" CssClass="btn close" Text="Close"/>
            </div>
     </div>--%>
     <div class="msgbox" runat="server" id="lblMessage"></div>
</div>
</div>
</div>


</form>     
</body>
</html>   

