<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorMgmtHome.aspx.cs" Inherits="VendorMgmtHome" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <script type="text/javascript">
        function OpenNew() {
            var key = '<%= System.Configuration.ConfigurationManager.AppSettings["NewVendor"].ToString() %>';
            window.open(key, '');
        }
        function OpenFind() {
            window.open('FindVendor.aspx', '');
        }
        
    </script>   
    <style type="text/css">
        body
        {
           font-family: 'Roboto', sans-serif;
           font-size:11px;
           margin:0px;
        }
        .box
        {
            width:170px; height:40px; color:White; padding:5px; text-align:left;  margin: 0 auto; border:solid 1px #333;
        }
        .color1{background-color:#ff1a75;}
        .color2{background-color:#0099ff;}
        .color3{background-color:#00cc00;}
        .color4{background-color:#e6ac00;}
        .color5{background-color:#00cc99;}
        .color6{background-color:#004de6;}
        .color7{background-color:#9933ff;}
        .color8{background-color:#ff751a;}
        .heading
        {
            background-color:#7D7674; color:White;
            padding:7px;
            font-size:14px;
        }
        input
        {
            padding:3px;
        }
        select
        {
            padding:3px;
            height:25px;
        }
        .btn
        {
            background-color:#0099ff;
            border:solid 1px #0099ff;
            padding:3px 20px 3px 20px;
            color:White;
        }
        .alternate_tableheader
        {
            background-color:#928F8F;
            color:White;
            height:30px;
        }
        .alternate_table tr td
        {
            border:solid 1px #ededed;
        }
        .alternate_table tr:hover
        {
            border:solid 1px #ededed;
            background-color:#ffff99;
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
    width:80%;
    margin:0px auto;
    padding:0px;
    border:solid 10px grey;
}
.modalcontainer
{
    height:100%;
    background-color:White;
    padding:10px;
}
    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <table cellpadding="15" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="text-align:center;">
                <div class="box color1">Proposal
                    <div style="font-size:larger; text-align:center; font-size:24px;"><a href="VendorMgmtDetails.aspx?Mode=1" target="_blank"><asp:Label runat="server" ID="lblProposing" Text=""></asp:Label></a></div>
                </div>
            </td>
            <td style="text-align:center">
            <div class="box color3">First Approval
               <div style="font-size:larger; text-align:center; font-size:24px;"><a href="VendorMgmtDetails.aspx?Mode=2" target="_blank"><asp:Label runat="server" ID="lblApp1" Text=""></asp:Label></a></div>
            </div>
            </td>
            <td style="text-align:center">
            <div class="box color4">IInd Approval
                <div style="font-size:larger; text-align:center; font-size:24px;"><a href="VendorMgmtDetails.aspx?Mode=3" target="_blank"><asp:Label runat="server" ID="lblApp2" Text=""></asp:Label></a></div>
            </div>
            </td>
              <td style="text-align:center">
            <div class="box color5">Accounts
                <div style="font-size:larger; text-align:center; font-size:24px;"><a href="VendorMgmtDetails.aspx?Mode=4" target="_blank"><asp:Label runat="server" ID="lblAccounts" Text=""></asp:Label></a></div>
            </div>
            </td>
        </tr>
        </table>
        <div class="heading">Vendors in Approval / Evaluation Process</div>
        <table cellpadding="5" cellspacing="0"  border="0" rules="none" width="100%" style="background-color:#EAEAEA;">
        <col width="100"/>
        <col />
        <tr style="padding:5px;">
                <td style="text-align:right">Select Activity :</td>
                <td>
                <asp:DropDownList runat="server" ID="ddlType" >
                <asp:ListItem Text="All" ></asp:ListItem>
                <%--<asp:ListItem Text="New Request" ></asp:ListItem>--%>
                <asp:ListItem Text="Proposal" ></asp:ListItem>
                <asp:ListItem Text="First Approval" ></asp:ListItem>
                <asp:ListItem Text="IInd Approval" ></asp:ListItem>
                <asp:ListItem Text="Accounts" ></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td style="text-align:right; width:80px;">Vendor :</td>
                <td>
                <asp:TextBox runat="server" ID="txtVendor" ></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;">Email :</td>
                <td>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                </td>
                <td valign="top" style="text-align:right">
                    <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="ddlType_OnSelectedIndexChanged" CssClass="btn" />
                    <asp:Button runat="server" ID="btnAdd" Text="Add" OnClientClick="OpenNew();" CssClass="btn" />
                    <asp:Button runat="server" ID="btPrint" Text="Print" OnClientClick="PrintVendor();return false;" CssClass="btn" />
		    <asp:Button runat="server" ID="btnFindVendor" Text="Find Vendor" OnClientClick="OpenFind();" CssClass="btn" />
                </td>          
            </tr>
        </table>
        <div style="width:100% ;  height:30px; overflow-y: scroll; height:30px;">
            <table cellpadding="3" cellspacing="0"  border="1" rules="all" width="100%" class="alternate_tableheader">
                   <colgroup>
                    <col width="50px" />  
                    <col   />
                    <col width="100px" />  
                    <col width="120px" />
                    <col width="100px" /> 
                    <col width="120px" />
                    <col width="17px" />  
                    </colgroup>
                     <tr>
                        <td style="text-align:center">SrNo</td>
                        <td style="text-align:left">Vendor Name</td>
                        <td style="text-align:left">Country</td>
                        <td style="text-align:left">Activity</td>
                        <td style="text-align:left">Submitted On</td>
                        <td style="text-align:center">Open</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>        
        </div>
        <div style="width:100% ; height:280px; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">
               <table cellpadding="3" cellspacing="0"  border="1" rules="all" width="100%" class="alternate_table" >
                  <colgroup>
                    <col width="50px" />  
                    <col   />
                    <col width="100px" />  
                    <col width="120px" />
                    <col width="100px" /> 
                    <col width="120px" />
                    <col width="17px" />  
                    </colgroup>
                <asp:Repeater ID="rptVendor" runat="server">
                    <ItemTemplate>
                            <tr >
                                <td align="center"> <%#Eval("srno")%>.</td>
                                <td>
                                <%#Eval("SupplierName")%> 
                                <asp:HiddenField ID="hfSupplierID" runat="server" Value='<%#Eval("SupplierID") %>' />
                                </td>
                                <td><%#Eval("CountryName")%> </td>
                                <td style="text-align:left"><%#Eval("Activity")%></td>
                                <td align="center"> <%#Common.ToDateString(Eval("SubmittedOn"))%> </td>
                                <td style="text-align:center">
                                    <a target="_blank" href='<%#"ModifyVendorProfile_Proposal.aspx?VRID=" + Eval("VRID").ToString()%>' runat="server" visible='<%#(Common.CastAsInt32(Eval("VRID"))>0)%>'><img src="Images/magnifier.png" style="height:12px; border:none;"/></a>
                                   <a target="_blank" href='<%#"ModifyVendorProfile.aspx?VRID=" + Eval("VRID").ToString()%>'><img src="Images/addpencil.gif" style="height:12px; border:none;"/></a>			 	     			
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="width:100% ;  height:30px; overflow-y: scroll; height:30px;">
        <table cellpadding="3" cellspacing="0"  border="1" rules="all" width="100%" class="alternate_tableheader">
        <tr>
        <td>
        
        <asp:Label ID="lblMessage" runat="server" style="float:right; padding-right:10px; font-size:13px;" ></asp:Label>
            <asp:Label ID="lblTotRec" runat="server" style="float:right; padding-right:10px; font-size:13px;" ></asp:Label>
        </td>
        </tr>
        </table>
        </div>

 
</form>   

</body>
</html>   

