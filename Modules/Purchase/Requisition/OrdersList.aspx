<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrdersList.aspx.cs" Inherits="OrderList" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
    <meta http-equiv="x-ua-compatible" content="IE=9" />
     <%--<link href="../CSS/style.css" rel="stylesheet" type="text/css" /--%>>
    <style type="text/css">
        
    .autocomplete_completionListElement 
{  
	margin:0px 0px 0px 0px; 
	padding:0px 0px 0px 0px; 
	background-color : #f4f6f9;
	color : windowtext;
	border : #5f8ab7;
	border-width : 1px;
	border-style : solid;
	cursor : default;
	overflow : auto;
	text-align : left; 
    list-style-type : none;
    font-family : Verdana;
    font-size :12px;
}
    /* AutoComplete highlighted item */

.autocomplete_highlightedListItem
{
	margin:0px 0px 0px 0px; 
	padding:0px 0px 0px 0px; 
	background-color: #5f8ab7;
	color :White;
	padding: 1px;
	font-family : Verdana;
	font-size :12px;
	list-style : none;
}

/* AutoComplete item */

.autocomplete_listItem 
{
	margin:0px 0px 0px 0px; 
	padding:0px 0px 0px 0px; 
	color :#5f8ab7;
	padding : 1px;
	font-family : Verdana;
	font-size :12px;
	list-style : none;
	cursor:pointer;
}

</style>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('ctl00_ContentMainMaster_hfBidID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPoid').value = "";
                    //                    }
                    //                    else
                    //                    {
                       // trSel.setAttribute(CSSName, "selectedrow");
                        //lastSel=trSel;
                        //document.getElementById('hfPoid').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfBidID').value = prid;
                }
            }
        }

        function ViewPO(BidId) 
        {
              window.open('VeiwRFQDetailsForApproval.aspx?BidId=' + BidId);
        }
      
        function EditPR()
        {
            alert('Pending to Work'); 
        }
        
        function AskPRtoCancel(Status)
        {
           return confirm('Are you sure you want to cancel it?');
        }
       
    </script>
    <script type="text/javascript" >
        function OpenWindow(BidID) 
        {
            window.open('UpdateOrderStautsComment.aspx?BidID=' + BidID, '', 'height=250,width=450,resizable=no, toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes,left=450, top=280');
        }
        function ReloadPage() 
        {
            document.getElementById("ctl00_ContentMainMaster_BtnReload").click();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 3%;
            height: 21px;
        }
        .auto-style2 {
            width: 9%;
            height: 21px;
        }
        .auto-style3 {
            width: 6%;
            height: 21px;
        }
        .auto-style4 {
            width: 8%;
            height: 21px;
        }
        .auto-style5 {
            width: 7%;
            height: 21px;
        }
        .auto-style6 {
            width: 14%;
            height: 21px;
        }
        .auto-style7 {
            height: 21px;
        }
    </style>
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
    <div>
        <asp:Button ID="BtnReload" runat="server" OnClick="BtnReload_OnClick" style="display:none;" />
        <asp:Button ID="btnViewPO" runat="server" OnClick="btnViewPO_OnClick" style="display:none;" />
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; text-align: left;">
        <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td class="Text headerband" style=" padding:4px;" >Purchase orders
        <asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Modules/HRD/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent;" />  
        </td>
        </tr>
        </table>
        </td> 
        </tr> 
        </table> 
        <div style="border:2px solid #4371a5;" >
        <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
        <tr>
           <td>
           <table cellspacing="1" cellpadding="4" width="100%" border="0">
             <colgroup>
                       <col style="text-align:center;" width="330px" />
                       <col style="text-align:center;" width="170px" />
                       <col style="text-align:center;" width="100px" />
                       <col style="text-align:center;" width="170px" />
                       <col style="text-align:center;" width="100px" />
                       <col style="text-align:center;" width="100px" />
                       <col style="text-align:center;" width="100px" />
                       <col style="text-align:center;" />
             </colgroup>
                       <tr align="center" class="header">
                           <td style="text-align:left;padding-left:5px;">
                               Vessel</td>
                           <td>
                               PO Type</td>
                           <td>
                               PO Number</td>
                           <td>
                               Status</td>
                           <td>
                               From Date</td>
                           <td>
                               To Date</td>
                            <td>
                               Acct Code</td>
                           <td> 
                               &nbsp;</td>
                       </tr>
                       <tr align="center" >
                           <td>
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                               <ContentTemplate>
                                 <%--   <uc1:VSlDropDown ID="ddlVessel" runat="server" IncludeInActive="false" />--%>
                                     <asp:DropDownList ID="ddlVessel" runat="server" Width="160px"></asp:DropDownList>
                                    <asp:CheckBox ID="ChkAllVess" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkAllVess_CheckedChanged" Text="Include Inactive" />
                                    <asp:CheckBox ID="chkNWC" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkNWC_CheckedChanged" Text="NWC" />
                               </ContentTemplate>
                               </asp:UpdatePanel>
                               <%--<asp:DropDownList ID="ddlVessel" runat="server" Width="50%"></asp:DropDownList>--%>
                               <asp:Button ID="btnPost" runat="server" OnClick="btnPost_OnClick"  style="display:none;"/>
                               <asp:HiddenField ID="hfBidID" runat="server" Value="0" />
                           </td>
                           <td>
                               <asp:DropDownList ID="ddlPRType" runat="server" Width="150px"></asp:DropDownList>
                           </td>
                           <td>
                               <asp:TextBox ID="txtPRNumber" runat="server" Width="95%"></asp:TextBox>
                           </td>
                           <td>
                               <asp:DropDownList ID="ddlStatus" runat="server" Width="150px"></asp:DropDownList>
                           </td>
                           <td>
                               <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="95%" style="text-align:center"></asp:TextBox>
                           </td>
                           <td>
                               <asp:TextBox ID="txtToDate" runat="server" Text="" Width="95%" style="text-align:center"></asp:TextBox>
                           </td>
                            <td>
                               <asp:TextBox ID="txtAcctCode" runat="server" Text="" Width="95%" style="text-align:center"></asp:TextBox>
                           </td>
                           <td>
                               <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn" />
                               <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn" />
                               <asp:Button ID="btnReport" runat="server" Text="POS Report" CssClass="btn" OnClientClick="window.open('POReport.aspx'); return false;" />
                           </td>
                       </tr>
            </table>   
           </td>
        </tr>
        <tr>
            <td>
                <div style="position:relative;">
                    <asp:UpdatePanel ID="up1" runat="server"  >
                       <ContentTemplate>
                        <asp:Button runat="server" id="btnCancelPO" Text="Cancel PO" CssClass="btn" onclick="btnCancelPO_Click" OnClientClick="return confirm('Are you sure to cancel this PO?');" ToolTip="Cancel PO : Cancel Purchase Order" />
                        <asp:Button runat="server" id="btnRecGoods" Text="Receive Goods" CssClass="btn" onclick="btnViewGoodsRcv_OnClick" ToolTip="Recieve Goods" />
                        <asp:Button runat="server" id="btnEnterInv" Text="Enter Invoice" CssClass="btn" onclick="btnInvoice_OnClick" ToolTip="Invoice Entry" Visible="false" />
                        <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label>
                       </ContentTemplate>
                        <Triggers >
                            <asp:PostBackTrigger ControlID="btnCancelPO" />
                        </Triggers>
                     </asp:UpdatePanel>
                    <asp:Label ID="lblRowCount" runat="server" Font-Bold="true" style="position:absolute; right:0px;top:5px;" ></asp:Label>
                </div>
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:3%;" />
                        <col style="width:3%;" />
                        <col style="width:9%;" />
                        <col style="width:6%;" />
                        <col style="width:8%;" />
                        <col style="width:7%;" />
                        <col style="width:9%;" />
                        <col style="width:15%;" />
                        <col style="width:15%;" />
                        <col style="width:6%;" />
                        <col style="width:5%;" />
                        <col style="width:8%;"/> 
                        <col style="width:6%;"/> 
                        <col style="width:2%;"/>
                         
                        <tr align="left" class= "headerstylegrid">
                            <td>VSL </td>                           
                            <td>Type</td>
                            <td>PO #</td>
                            <td>Off Req#</td>
                            <td>VSL Req#</td>
                            <td>PO Date</td>
                            <td>Status</td>
                            <td>Requsition Title</td>
                            <td>Vendor</td>
                            <td>Acc.Code</td>
                            <td>Inv.RefNo</td>
                            <td>PO Amount (USD)</td>
                            <td>Comments</td>
                            <td> </td>
                        </tr>
                   </colgroup>
                   </table>
                
               <div id="dvscroll_Order" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 328px ; text-align:center;" onscroll="SetScrollPos(this)">
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                       <col style="width:3%;" />
                        <col style="width:3%;" />
                        <col style="width:9%;" />
                        <col style="width:6%;" />
                        <col style="width:8%;" />
                        <col style="width:7%;" />
                        <col style="width:9%;" />
                        <col style="width:15%;" />
                        <col style="width:15%;" />
                        <col style="width:6%;" />
                        <col style="width:5%;" />
                        <col style="width:8%;"/> 
                        <col style="width:6%;"/>  
                        <col style="width:1%;"/>
                    </colgroup>  
                    <asp:Repeater ID="RptPRMaster" runat="server">
                        <ItemTemplate>
                                <tr id='tr<%#Eval("Bidid")%>' class='<%#(Convert.ToInt32(Eval("Bidid"))!=SelectedPoId)?"":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("Bidid")%>);'  ondblclick='ViewPO(<%#Eval("Bidid")%>)' title="Double click to view the purchase order.">
                                <td style=""><asp:Label ID="lblShip" runat="server" Text='<%# Eval("ShipID") %>'></asp:Label></td>
                                <td style=""><%# Eval("PRTypeCode")%></td>
                                <td style=""><%# Eval("bidponum")%></td>
                                <td style=""><asp:Label ID="lblPRNumber" runat="server" Text='<%# Eval("prnum") %>'></asp:Label> </td>
                                <td style=""><%# Eval("reqno") %></td>
                                <td style=""><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("BidPODate") %>'></asp:Label></td>
                                <td style="text-align :left;color:<%#getStatusColor(Eval("BidStatusName").ToString())%>"><%#Eval("BidStatusName")%></td>
                                <td style="text-align:left;"><%# Eval("RequisitionTitle")%></td>
                                <td style="text-align:left;"><%# Eval("SupplierName")%></td>
                                <td style="text-align:left;"><%# Eval("accountnumber")%></td>
                                <td style="text-align:left;"><a target="_blank" href='../Invoice/ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>'><%#Eval("RefNo")%></a></td>
                               <td style="text-align:left;width:8%;"><%# Eval("totalpoamount")%></td>
                               <%-- <td style="text-align:left;width:8%;"></td>--%>
                                     <td>
                                     <span style='<%#(Eval("OrderStatusComment").ToString().Trim()=="")?"display:none":""%>'> 
                                          <asp:ImageButton ID="IbCommentForVessel" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip='<%# TrimRemarks(Eval("OrderStatusComment")) %>' />
                                     </span>
                                 </td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id='tr<%#Eval("Bidid")%>' class='<%#(Convert.ToInt32(Eval("Bidid"))!=SelectedPoId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("Bidid")%>);' lastclass='alternaterow' ondblclick='ViewPO(<%#Eval("Bidid")%>)'  title="Double click to view the purchase order.">
                                <td style=""><asp:Label ID="lblShip" runat="server" Text='<%# Eval("ShipID") %>'></asp:Label></td>
                                <td style=""><%# Eval("PRTypeCode")%></td>
                                <td style=""><%# Eval("bidponum")%></td>
                                <td style=""><asp:Label ID="lblPRNumber" runat="server" Text='<%# Eval("prnum") %>'></asp:Label></td>
                                <td style=""><%# Eval("reqno") %></td>
                                <td style=""><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("BidStatusDate") %>'></asp:Label></td>
                                <td style="text-align :left;color:<%#getStatusColor(Eval("BidStatusName").ToString())%>"><%#Eval("BidStatusName")%></td>
                                  <td style="text-align:left;"><%# Eval("RequisitionTitle")%></td>
                                <td style="text-align:left;"><%# Eval("SupplierName")%></td>
                                <td style="text-align:left;"><%# Eval("accountnumber")%></td>
                                <td style="text-align:left;"><a target="_blank" href="../Invoice/ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>"><%#Eval("RefNo")%></a></td>
                                 <td style="text-align:left;"><%# Eval("totalpoamount")%></td>
                                 <td>
                                     <span style='<%#(Eval("OrderStatusComment").ToString().Trim()=="")?"display:none":""%>'> 
                                          <asp:ImageButton ID="IbCommentForVessel" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip='<%# TrimRemarks(Eval("OrderStatusComment")) %>' />
                                     </span>
                                 </td>
                              
                                 <%--<td style="text-align:left;width:8%;"></td>--%>
                                <td ></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                       
                      
                </table>
               </div>
                   
            </td>
        </tr>
        </table>
        <table>
        <tr>
                <td>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtFromDate">
                    </asp:CalendarExtender>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                        Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txtToDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
        </div>
    </div>
    <script type="text/javascript" >
        var Id = 'tr' + document.getElementById('ctl00_ContentMainMaster_hfBidID').value;
    lastSel=document.getElementById(Id);
    </script> 
</asp:Content>

