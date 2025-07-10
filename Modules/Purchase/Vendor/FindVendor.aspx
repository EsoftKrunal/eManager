<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindVendor.aspx.cs" Inherits="FindVendor" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <script type="text/javascript">
        function OpenNew() {
            window.open('http://singapore.mtmshipmanagement.com/Public/VendorManagement/registervendor.aspx', '');
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
            overflow:hidden;
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
        <div style="position:fixed;top:0px; left:0px; width:100%; height:130px; background-color:White;">
        <div class="text headerband" style="text-align:center"><asp:Label ID="lblTotRec" runat="server" style="float:right; padding-right:10px; font-size:13px;" ></asp:Label>Find Vendor</div>
        <div style="padding:5px; font-size:13px;">
                    <table cellpadding="3" cellspacing="0"  border="0" width="100%">
                     <tr>
                        <td style="text-align:right">Vendor Name :</td>
                        <td style="text-align:left"><asp:TextBox ID="txtVendor" runat="server" Width="90%" AutoPostBack="true" OnTextChanged="BindVendor"></asp:TextBox></td>
                        <td style="text-align:right">Email :</td>
                        <td style="text-align:left"><asp:TextBox ID="txtEmail" runat="server" Width="90%" AutoPostBack="true" OnTextChanged="BindVendor"></asp:TextBox></td>
                        <td style="text-align:right">Approval Type :</td>
                        <td style="text-align:left">  <asp:DropDownList ID="ddlApprovalType" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="BindVendor">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Nominated / Contracted" Value="1"></asp:ListItem>
                            <asp:ListItem Text="OTA" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="3"></asp:ListItem>               
                            <asp:ListItem Text="Owner's Recommendation" Value="6"></asp:ListItem>           
                        </asp:DropDownList>
                        <td style="text-align:right">Country :</td>
                        <td style="text-align:left"> <asp:DropDownList ID="ddlCountry" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="BindVendor"></asp:DropDownList></td>
                     </tr>
                     <tr>
                        <td style="text-align:right">Trav Code :</td>
                        <td style="text-align:left"><asp:TextBox runat="server" ID="txtTravCode" Width="90%" AutoPostBack="true" OnTextChanged="BindVendor"></asp:TextBox></td>
                        <td style="text-align:right">Status :</td>
                        <td style="text-align:left">
                        <asp:DropDownList ID="ddlActive" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="BindVendor">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="InActive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td style="text-align:right">Offered Services :</td>
                        <td style="text-align:left">
                        <asp:DropDownList ID="ddlServices" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="BindVendor"></asp:DropDownList>
                        </td>
                        <td style="text-align:right">Validity Expired In :</td>
                        <td style="text-align:left">
                        <asp:DropDownList ID="ddlValidity" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="BindVendor">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Next Week" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Next Month" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Next 6 Month" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Next Year" Value="4"></asp:ListItem>               
                            <asp:ListItem Text="More than 1 Year" Value="5"></asp:ListItem>           
                        </asp:DropDownList>
                        </td>
                    </tr>
                </table> 
                </div>
        <table cellpadding="3" cellspacing="0"  border="1" rules="all" width="100%" class="alternate_tableheader">
                   <colgroup>
                    <col width="50px" />  
                    <col width="100px" />  
                    <col   />
                    <%--<col width="250px" />--%>
                    
                    <col width="100px" />  
                    <col width="100px" /> 
                    <col width="450px" /> 
                    <col width="80px" />
                    <col width="80px" />
                    
                    <%--<col width="50px" /> --%>
                    <col width="17px" />  
                    </colgroup>
                     <tr class= "headerstylegrid">
                        <td style="text-align:center">SrNo</td>
                        <td style="text-align:left">Vendor Code</td>
                        <td style="text-align:left">Vendor Name</td>
                        <td style="text-align:left">Country</td>
                        <%--<td style="text-align:left">Telephone / Fax</td>--%>
                        <td style="text-align:left">Approval Type</td>
                        <td style="text-align:left">Vendor Services</td>
                        <td style="text-align:left">Status</td>
                        <td style="text-align:left">Validity Date</td>
                        <%--<td style="text-align:center">Open</td>--%>
                        <td>&nbsp;</td>
                    </tr>
                </table>
        </div>
        <div style="margin-top:132px">
        
        </div>
        <div style="width:100% ;">
               <table cellpadding="3" cellspacing="0"  border="1" rules="all" width="100%" class="alternate_table" >
                   <colgroup>
                    <col width="50px" />  
                    <%--<col width="250px" />--%>
                    <col width="100px" />  
                    <col   />
                    <col width="100px" />  
                    <col width="100px" /> 
                    <col width="450px" /> 
                    <col width="80px" />
                    <col width="80px" />
                    <%--<col width="50px" /> --%>
                    <col width="17px" />  
                    </colgroup>
                <asp:Repeater ID="rptVendor" runat="server">
                    <ItemTemplate>
                            <tr>
                                <td align="center"><%#Eval("srno")%>.</td>
                                <td><%#Eval("TravID")%></td>
                                <td><%#Eval("SupplierName")%></td>
                                <td><%#Eval("CountryName")%> </td>
                                <%--<td><%#Eval("SupplierEmail")%></td>--%>
                                <%--<td style="text-align:left; height:18px; overflow:hidden;"><%#Eval("SupplierTel")%> / <%#Eval("SupplierFax")%></td>--%>
                                <td><%#Eval("ApprovalTypeName")%> </td>
                                <td><%#Eval("VendorBusinesses")%> </td>
                                <td><%#((Eval("Active").ToString()=="True")?"Active":"InActive")%> </td>
                                <td><%#Common.ToDateString(Eval("ValidityDate"))%> </td>
                                <%--<td style="text-align:center">
                                    <a id="A2" target="_blank" runat="server" visible='<%#(Eval("VRID").ToString()!="")%>' href='<%#"ModifyVendorProfile_Proposal.aspx?VRID=" + Eval("VRID").ToString()%>' runat="server"><img src="Images/magnifier.png" style="height:12px; border:none;"/></a>
                                </td>--%>
                                <td>&nbsp;</td>
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
</form>   
</body>
</html>   

