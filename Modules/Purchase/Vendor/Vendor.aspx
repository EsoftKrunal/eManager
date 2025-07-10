<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vendor.aspx.cs" Inherits="Vendor" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <script type="text/javascript" src="../JS/Common.js"></script>    
    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         .controlarea{display:inline; text-align:left;  empty-cells:show; }

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
        /*.btn
        {
            background-color:#0099ff;
            border:solid 1px #0099ff;
            padding:3px 20px 3px 20px;
            color:White;
        }*/
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
<body style="margin:0px;">
    <form id="form1" runat="server" defaultbutton="imgSearch" >
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <asp:HiddenField ID="hfSID" runat="server" />
        <asp:Button ID="btnPageReload" runat="server" OnClick="OnClick_btnPageReload" style="display:none;" />
        <div style="padding:5px; font-size:13px;">
        <table cellpadding="2" cellspacing="0"  border="0" width="100%">
        <col width="100"/>
        <col width="250"/>
        <col width="80"/>
        <col width="200"/>
        <col width="80"/>
        <col width="150"/>
        <col width="110"/>
        <col />
        <col />
            <tr  style="padding:5px;">
                <td style="text-align:right"> Vendor Name :</td>
                <td>
                    <asp:TextBox ID="txtVendor" runat="server" Width="180px" Text="A" ></asp:TextBox>
                    <asp:AutoCompleteExtender ID="extVendor" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetPortTitles" ServicePath="~/WebService1.asmx" TargetControlID="txtVendor"></asp:AutoCompleteExtender>
                </td>
                <td style="text-align:right"> Port :</td>
                <td>
                    <asp:TextBox ID="txtPort" runat="server" Width="180px" ></asp:TextBox> 
                    <asp:AutoCompleteExtender ID="extPort" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetPort" ServicePath="~/WebService1.asmx" TargetControlID="txtPort"></asp:AutoCompleteExtender>
                </td>
                <td style="text-align:right"> Active :</td>
                <td>
                    <asp:UpdatePanel ID="up01" runat="server" >
                        <ContentTemplate>

                        
                        <asp:DropDownList ID="ddlActive" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlActive_OnSelectedIndexChanged">  
                            <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0" ></asp:ListItem>
                        </asp:DropDownList>

                        <asp:CheckBox ID="chkBlackList" runat="server" Visible="false" Text="Blacklist" /> 

                            </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="text-align:right"> <%--Approval Type :--%></td>
                <td>
                    <%--<asp:TextBox ID="txtApprovalType" runat="server"  Width="180"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetApprovalType" ServicePath="~/WebService1.asmx" TargetControlID="txtApprovalType"></asp:AutoCompleteExtender>--%>
                </td>
            </tr>
            <tr>
                <td style="text-align:right"> Vendor Type :</td>
                <td style="text-align:left">
                    <asp:Label ID="lblBusinessType" runat="server"></asp:Label>    
                    <asp:LinkButton ID="lnkOpenBusinessType" runat="server" OnClick="lnkOpenBusinessType_OnClick" Text="Select"  style="margin-left:20px;" ></asp:LinkButton>                
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnCleaerBusinessType_OnClick" Text="Clear"  style="margin-left:10px;" ></asp:LinkButton>                
                </td>
                <td style="text-align:right"> Country :</td>
                <td style="text-align:left"><asp:DropDownList ID="ddlCountry" runat="server" Width="150px"></asp:DropDownList></td>
                <td style="text-align:right"> City/State :</td>
                <td style="text-align:left"><asp:TextBox ID="txtcity" runat="server" Width="180px" ></asp:TextBox> </td>
                <td>&nbsp;</td>
                <td style="text-align:left;">
                   <asp:Button runat="server" ID="imgSearch" Text="Search" OnClick="OnClick_imgSearch" CssClass="btn" />
                    <asp:Button runat="server" ID="imgCancel" Text="Clear" OnClick="OnClick_imgCancel" CssClass="btn" />
                    <asp:Button runat="server" ID="btPrint" Text="Print" OnClientClick="PrintVendor();return false;" CssClass="btn" />
                </td>
            </tr>
        </table>
        </div>
        <div style="background-color:#aed6ed; vertical-align:middle; padding:5px;text-align:right">
            <asp:Label ID="lblTotRec" runat="server" style="font-weight:bold;float:right; padding-right:10px;" ></asp:Label>&nbsp;
        </div>
        <div style="width:100% ; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">
            <table cellpadding="3" cellspacing="0"  border="0" rules="none" width="100%" class="alternate_tableheader">
                    <colgroup>
                    <col width="50px" />  
                    <col   />
                    <col width="100px" />  
                    <col width="150px" />  
                    <col width="120px" />
                    <col width="150px" /> 
                    <col width="80px" /> 
                    <col width="17px" />  
                    </colgroup>
                     <tr class= "headerstylegrid">
                        <td style="text-align:center">SrNo</td>
                        <td style="text-align:center">Vendor</td>
                        <td style="text-align:center">Vendor Code</td>
                        <td style="text-align:center">Country</td>
                        
                        <td style="text-align:center">Active</td>
                        <td style="text-align:center">Approval Type</td>
                        <td style="text-align:center">Activiity</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>        
            </div>
        <div style="width:100% ; height:380px; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">
            
               <table cellpadding="3" cellspacing="0"  border="0" rules="none" width="100%" class="alternate_table" >
                    <colgroup>
                    <col width="50px" />  
                    <col />
                    <col width="100px" />  
                    <col width="150px" />  
                    <col width="120px" />
                    <col width="150px" />  
                    <col width="80px" />  
                    <col width="17px" />  
                    </colgroup>
                <asp:Repeater ID="rptVendor" runat="server"  OnItemCommand="rptVendor_ItemCommand"  >
                    <ItemTemplate>
                            <tr id='tr<%#Eval("SupplierID")%>' class='<%#(Convert.ToInt32(Eval("SupplierID"))!=SelectedPoId)?"":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("SupplierID")%>);'   ondblclick="OpenSupplierDetailsView(<%#Eval("SupplierID")%>)" >
                                <td align="center"> <%#Eval("srno")%>.</td>
                                <td><%#Eval("SupplierName")%>
                                    <asp:HiddenField ID="hfSupplierID" runat="server" Value='<%#Eval("SupplierID") %>' />
                                    <span style="color:red;" runat="server" visible='<%#Eval("Expiring").ToString()=="Y"%>'> <i>
                                        ( Valid till <%#Common.ToDateString(Eval("ValidityDate"))%> ) 
                                    </i></span>
                                    <span style="color:red;font-weight:bold;" runat="server"> <i>
                                        <%#Eval("Blacklist")%>
                                    </i></span>

                                    
                                </td>
                                <td align="center"> <%#Eval("TravID")%> </td>
                                <td><%#Eval("SupplierPort")%>, <%#Eval("cOUNTRYNAME")%> </td>
                                <td style="text-align:center"> <%#(Eval("Active").ToString()=="True")?"Yes":"No"%> </td>
                                <td><%#Eval("ApprovalTypeName")%> </td>
                                <td style="text-align:center">
                                    <%--<asp:ImageButton ID="ImgCopy" Visible="false" runat="server" tooltip="Copy to Vendor Request." CommandArgument='<%#Eval("SupplierID") %>' CommandName="CopyToVendrorRequest" ImageUrl="Images/gear.png"  ></asp:ImageButton>--%>

                                    <a id="A1" target="_blank" href='<%#"ModifyVendorProfile_Proposal.aspx?KeyId=" + Eval("VRID").ToString()%>' runat="server" visible='<%#(Common.CastAsInt32(Eval("VRID"))>0)%>'><img src="../../HRD/Images/magnifier.png" style="height:12px; border:none;"/></a>

                                    <asp:ImageButton ID="ImgEmail" runat="server" tooltip="Email to vendor" Width="12px" CommandArgument='<%#Eval("SupplierID")%>' CommandName="emailToVendor" ImageUrl="../../HRD/Images/email.png" ></asp:ImageButton>

                                    <asp:ImageButton ID="ImgAllowToResubmit" runat="server" tooltip="Allow to resubmit." Visible='<%#Eval("AllowToEditByMail").ToString()=="N"%>' ImageUrl="../../HRD/Images/reset.png" CommandArgument='<%#Eval("VRID")%>' CommandName="AllowResubmit"></asp:ImageButton>

                                    <a id="A12" target="_blank" href='<%#"ModifyVendorProfile.aspx?KeyId=" + Eval("VRID").ToString()%>' runat="server" visible='<%# ((Common.CastAsInt32(Session["loginid"])==1) ||  (Session["UserName"].ToString().ToUpper() =="SSEN") || (Session["UserName"].ToString().ToUpper() =="SPAL") || (Session["UserName"].ToString().ToUpper() ==  "ARAVINDH") || (Session["UserName"].ToString().ToUpper() ==  "EMMA")) && (Eval("VRID").ToString().Trim()!="")%>'><img src="../../HRD/Images/AddPencil.gif" style="height:12px; border:none;"/></a>
<!--
                                    <a id="A2" target="_blank" href='<%#"ModifyVendorProfile.aspx?KeyId=" + Eval("VRID").ToString()%>' runat="server" visible='<%# ( ( Common.CastAsInt32(Session["loginid"])==1 ) )  && (Eval("VRID").ToString().Trim()!="")%>'><img src="Images/AddPencil.gif" style="height:12px; border:none;"/></a>
-->
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            
        </div>

<div class="modalframe" runat="server" id="dvCopyToVendorRequest" visible="false">
<div class="modalborder">
<div class="modalcontainer">
<table cellpadding="3" cellspacing="0"  border="0" rules="none" style="width:100%; text-align:center" class="alternate_table" >
<tr style="font-size:17px;">
<td>Select Manager</td>
<td>Select Management</td>
</tr>
<tr style="font-size:17px;">
<td><asp:DropDownList runat="server" ID="ddlMgr" Width="90%"></asp:DropDownList>
<asp:RequiredFieldValidator runat="server" ID="Fd" ErrorMessage="*" ValidationGroup="c1c" ControlToValidate="ddlMgr"></asp:RequiredFieldValidator>
</td>
<td><asp:DropDownList runat="server" ID="ddlMgmt" Width="90%"></asp:DropDownList>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ValidationGroup="c1c" ControlToValidate="ddlMgmt"></asp:RequiredFieldValidator>
</td>
</tr>
</table>
<div style="padding:5px">
  <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn" ValidationGroup="c1c" />&nbsp;
  <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" CssClass="btn" CausesValidation="false" />
</div>
</div>
</div>
</div>

<div class="modalframe" runat="server" id="modalframeVendroProfile" visible="false">
<div class="modalborder">
<div class="modalcontainer">
 <%--After second approval div--%>
     <div class="formcontainer" runat="server" visible="false" id="div_AfterSecondApproval">
       <h2> <asp:Label ID="lbl_AfterSecondApproval" runat="server"></asp:Label></h2>
        <div class="row">           
         <span class="label">Action</span>
            <span class="controlarea">
           <asp:RadioButtonList ID="rd_ForDlist" runat="server" RepeatDirection="Horizontal">
           <asp:ListItem Text="Nominated" Value="1" Selected="True"></asp:ListItem>
           <asp:ListItem Text="OTA" Value="2"></asp:ListItem>
           <asp:ListItem Text="Other" Value="3"></asp:ListItem>
           <asp:ListItem Text="Delist" Value="4"></asp:ListItem>
           </asp:RadioButtonList>
            </span>
        </div>
        
        <div class="row">
            <span class="label">Name</span>
            <span class="controlarea">           
            <asp:TextBox runat="server" ID="txt_Name_ForDlist" CssClass="control" Width="500px" MaxLength="250" ReadOnly="true"></asp:TextBox>           
            </span>
        </div>    
   
        <div class="row">
            <asp:Label runat="server"  ID="lblMessage"></asp:Label>
        </div>
         <div class="actionbox">  
           <asp:Button runat="server" ID="btn_SaveRequestStatus_Dlist" OnClick="btn_SaveRequestStatus_Dlist_Click" CssClass="btn" Text="Save"  />
           <asp:Button runat="server" ID="btn_closeAfterSecodnApproval" OnClick="btn_closemodel_Click" CssClass="btn close" Text="Close"/>
         </div>
        </div> 
</div>
</div>
</div>


        <%-- Business type popup-------------------------------------------------------------------------%>
<div style="position:absolute;top:0px;left:0px; height :550px; width:100%;z-index:100;" runat="server"  id="divAddBusinessType" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
     <div style="position :relative; width:700px; height:425px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
         <asp:HiddenField ID="hfSelBusinessType" runat="server" />
        
         
         <table cellpadding="3" cellspacing="3" border="0" width="100%">
             <tr class="text headerband">
                 <td>
                     <b>Select Business Type</b>
                     <asp:ImageButton ID="btnCloseAddBusinessTypePopup" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="btnCloseAddBusinessTypePopup_OnClick" style="float:right;"  />
                 </td>
             </tr>
            </table>
        <table cellpadding="3" cellspacing="3" border="0">
            <tr>
                <td style="text-align:left;">
                    <b>Business Type</b>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="controlarea">&nbsp;
                        <asp:CheckBoxList id="chkVendorbusinesseslist" runat="server" RepeatColumns="3"  Width="100%"  ></asp:CheckBoxList>               
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <asp:Button ID="btnAddBusinessType" runat="server" Text="Select" CssClass="btn" OnClick="btnAddBusinessType_OnClick" />
                    <asp:Button ID="btnCloseBusinessType" runat="server" Text=" Close " CssClass="btn" OnClick="btnCloseAddBusinessTypePopup_OnClick" />
                </td>
            </tr>
        </table>
     </div>
        
    </center>
</div>
</form>   
<script type="text/javascript" >
    var lastSel = document.getElementById("tr<%=hfSID.Value%>");
    function Selectrow(trSel, prid) {
        if (lastSel == null) {
            trSel.setAttribute(CSSName, "selectedrow");
            lastSel = trSel;
            document.getElementById('hfSID').value = prid;
        }
        else {
            if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel = trSel;
                document.getElementById('hfSID').value = prid;
            }
            else // clicking on ohter row
            {
                lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel = trSel;
                document.getElementById('hfSID').value = prid;
            }
        }
    }

    function OpenAddVendor() {
        window.open('AddNewVendor.aspx', 'Open', 'menubar=1,resizable=0,width=1060,height=350');
    }
    function OpenEditVendor() {
        var SID = document.getElementById('hfSID').value;
        if (SID > 0)
            window.open('AddNewVendor.aspx?SupplierID=' + SID + '', 'Open1', 'menubar=0,resizable=0,width=1060,height=350');
        else
            alert('Please select a Supplies.');
    }
    function PageReLoad() {
        document.getElementById('btnPageReload').click();
    }
    function OpenSupplierDetailsView(SID) {
        if (SID > 0)
            window.open('AddNewVendor.aspx?View=1&SupplierID=' + SID + '', 'Open1', 'menubar=0,resizable=0,width=1060,height=350');
    }
    function PrintVendor() {

        var Vendor = document.getElementById('txtVendor').value;
        var Port = document.getElementById('txtPort').value;
        //var Co = document.getElementById('ddlCompany').value;
        var Active = document.getElementById('ddlActive').value;
        var AppType = document.getElementById('txtApprovalType').value;

        window.open('Print.aspx?Vendor=1&Vendor=' + Vendor + '&Port=' + Port + '&Active=' + Active + '&AppType=' + AppType + '', '', '');

    }
    </script>  
</body>
</html>   

