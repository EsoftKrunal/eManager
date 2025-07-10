<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalList1.aspx.cs" Inherits="ApprovalList1" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript" src="JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel = null;
        function ViewRFQ(BidId) {
            window.open('RFQDetailsForApproval.aspx?BidId=' + BidId);
        }
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                        trSel.setAttribute(CSSName, "selectedrow");
                        lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                }
            }
        }
        
        function AddPR()
        {
            alert('Pending to Work'); 
        }
        
        function ViewPR()
        {
            __doPostBack('ctl00_ContentMainMaster_btnViewPR','');
        //            var prid="0"+document.getElementById('hfPRID').value;
        //            if(parseInt(prid) > 0)
        //            {
        //                document.location='AddRequisition.aspx?PRID=' + prid;
        //            }  
        //            else
        //            {
        //            alert('please select a purchase request.'); 
        //            }
        }
        
        function AskPRtoCancel(Status)
        {
           return confirm('Are you sure you want to cancel it?');
        }
       
    </script>
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

        .auto-style1 {
            position: absolute;
            top: 200px;
            left: 0px;
            width: 100%;
            z-index: 100;
        }

        .auto-style2 {
            height: 16px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div >
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="text-align :center; color :Blue; " class="auto-style1">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="../../HRD/Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress> 
    <asp:UpdatePanel ID="up1" runat="server" >
    <ContentTemplate>
        
        
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; text-align: left;">
        <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td class="text headerband" style=" padding:4px;" >Purchase Requests (Waiting for approval and order)
        <%--<asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Modules/HRD/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " />  --%>
        </td>
        </tr>
        </table>
        </td> 
        </tr> 
        </table> 
        <div style="border:2px solid #4371a5;" class="table-responsive" >
           <table cellspacing="1" cellpadding="4" width="100%" border="0">
             <colgroup>
                       <col style="text-align:center;" width="16%" />
                       <col style="text-align:center;" width="16%" />
                       <col style="text-align:center;" width="16%" />
                      <col style="text-align:center;" width="16%" />
                       <col style="text-align:center;" width="16%" />
                       <col style="text-align:center;" width="20%"/>
             </colgroup>
                       <tr align="center" class= "headerstylegrid">
                           <td>
                               Fleet</td>
                           <td>
                               Vessel</td>
                           
                           <td>
                               Requisition No.</td>

 <td>
                               Status</td>                           
                           <td> 
                               &nbsp;</td>
                               <td> 
                               &nbsp;</td>
                       </tr>
                       <tr align="center" >
                           <td style=" text-align :left" >
                               <asp:DropDownList runat="server" ID="ddlFleet"></asp:DropDownList>
                           </td>
                           <td style=" text-align :left" >
                               <%--<asp:DropDownList ID="ddlVessel" runat="server" Width="50%"></asp:DropDownList>--%>
                               <asp:UpdatePanel runat="server" UpdateMode="Always">
                               <ContentTemplate>
                                  <%--  <uc1:VSlDropDown ID="ddlVessel" runat="server" width="90%"/>--%>
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="150px"></asp:DropDownList>
                                    <asp:CheckBox ID="ChkAllVess" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkAllVess_CheckedChanged" Visible="false" Text="Include Inactive" />
                                    <asp:CheckBox ID="chkNWC" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkNWC_CheckedChanged" Text="NWC" />
                                    </ContentTemplate>
                               </asp:UpdatePanel>
                               <asp:Button ID="btnPost" runat="server" OnClick="btnPost_OnClick"  style="display:none;"/>
                               <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
                           </td>
                           
                           <td>
                               <asp:TextBox ID="txtPRNumber" runat="server" Width="95%"></asp:TextBox>
                           </td>
<td>
<asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
<asp:ListItem Text="- All -" Value="0"></asp:ListItem>
<asp:ListItem Text="Approval Stage-1" Value="1"></asp:ListItem>
<asp:ListItem Text="Approval Stage-2" Value="2"></asp:ListItem>
<asp:ListItem Text="Approval Stage-3" Value="3"></asp:ListItem>
<asp:ListItem Text="Approval Stage-4" Value="4"></asp:ListItem>
<asp:ListItem Text="Ready to Place Order" Value="5"></asp:ListItem>
</asp:DropDownList>
</td>
			   <td style=" text-align :left" ><asp:CheckBox ID="chkMyVessel" runat="server" Text="My Vessels Only" AutoPostBack="true" OnCheckedChanged="chkMyVessel_OnCheckedChanged" /> </td>
                           <td style=" text-align :left" >
                               <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                               <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"  CssClass="btn"/>
                               &nbsp;&nbsp;&nbsp;&nbsp;
                               
                               
                           </td>                           
                       </tr>
            </table>   
               
        </div>
     
        <div class="table-responsive" id="dvscroll_RFQ" style="OVERFLOW-Y: scroll;  HEIGHT: 450px ; text-align:center;"  >
            <table border="1">
                <thead>
                    <colgroup>
                            <col style="width:10%;" />
                           <%-- <col style="width:100px;" />--%>
                            <col style="width:7%;" />
                            <col style="width:6%;" />
                            <col style="width:23%;" />
                            <col style="width:4%;" />
                            <col style="width:4%;" />
                          <%--  <col style="width:80px;" />
                            <col style="width:140px;" />--%>
                            <col style="width:7%;" />
                            <col style="width:10%;" />
                            <col style="width:24%;"/>
                            <col style="width:5%;" />
                            <tr>
                                <td colspan="4" style=" padding-left :5px;">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label>
                                </td>
                                <td style="text-align:center;" colspan="2">Quotation</td>
                                <td class="header" colspan="1">Submitted For Approval</td>
                                <td></td>
                                <td colspan="2" style=" text-align :right ">
                                    <asp:Label ID="lblRowCount" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" class= "headerstylegrid">
                                <td style="width:10%;">Vessel</td>
                                <%--<td>VSL Req#</td>--%>
                                <td style="width:7%;"> Requisition No.</td>
                                <td style="width:6%;">ETA</td>
                                <td style="text-align:left;width:23%;">Requisition Title</td>
                                <%--<td>Rec. Date</td>--%>
                                <td style="width:4%;">Sent</td>
                                <td style="width:4%;">Rcv</td>
                               <%-- <td>Submitted</td>
                                <td>Ready for Order</td>--%>
                                <td style="width:7%;">Submitted On</td>
                                <td style="text-align:left;width:10%;">Status</td>
                                <td style="text-align:left;width:24%;">Purchaser Comment</td>
                                <td style="width:5%;">View Quotes</td>
                            </tr>
                        </colgroup>
                </thead>
                <tbody>
                     <colgroup>
                            <col style="width:10%;" />
                           <%-- <col style="width:100px;" />--%>
                            <col style="width:7%;" />
                            <col style="width:6%;" />
                            <col style="width:23%;" />
                           <%-- <col style="width:100px;" />--%>
                            <col style="width:4%;" />
                            <col style="width:4%;" />
                           <%-- <col style="width:80px;" />
                            <col style="width:140px;" />--%>
                            <col style="width:7%;" />
                            <col style="width:10%;" />
                            <col style="width:24%;"/>
                            <col style="width:4%;" />
                          
                    </colgroup>
                    <asp:Repeater ID="RptPRMaster" runat="server">
                        <ItemTemplate>
                                <tr id='tr<%#Eval("Poid")%>' >
                                <td >
                                    <asp:Label ID="lblShip" runat="server" Text='<%# Eval("ShipName") %>'></asp:Label>
                                    
                                </td>
                               <%-- <td><%# Eval("reqNo")%></td>--%>
                                <td>
                                    <asp:Label ID="lblPRNumber" runat="server" Text='<%# Eval("prnum") %>'></asp:Label>
                                </td> 
                                     <td ><%# Common.ToDateString(Eval("ETA"))%></td>
                                    <td style="text-align:left;padding-left:2px;"><%#Eval("RequisitionTitle")%></td>
                              <%--  <td >
                                    <asp:Label ID="lblCreated" runat="server" Text='<%# Common.ToDateString(Eval("DateCreated")) %>'></asp:Label>
                                </td>--%>
                               
                                <td ><%#Eval("BidsSent")%></td>
                                <td ><%#Eval("BidsRecd")%></td>
                            <%--    <td ><%#Eval("SubmittedCount")%></td>                                
                                <td ><%#Eval("ReadyForOrder")%></td>  --%>    
                                
                                    <td ><%# Common.ToDateString(Eval("SubmittedOn"))%></td>
				                <td style="text-align:left;" ><%#Eval("StatusText")%></td>
                                <td style="text-align:left;" ><%#Eval("RemarksSMD")%></td>      
                                <td>
                                    
                                    <img src="../../HRD/Images/poanalysis.png" alt="" style="border:solid 0px red" title="Quote Analyzer" onclick="window.open('SMDPOAnalyzer.aspx?Prid=<%#Eval("PoID")%>')" />
                                   <%-- <asp:ImageButton ID="btnAppovePO" runat="server" CommandArgument='<%#Eval("POID")%>' ImageUrl="~/Modules/HRD/Images/approved.png" OnClick="btnOpenApprovePoPopup_OnClick"  />--%>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
       <%-- <div >
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                   
                </table>
               </div>
    --%>
    <%----------------------------------------------------------------------------------------%>
 <%--  <div style="position:absolute;top:50px;left:50px; height :470px; width:100%;z-index:100;" runat="server" id="dvApprovePO" visible="false" >
    <center>
    <div style="position:absolute;top:50px;left:50px; height :470px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:80%; height:330px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px;  ;opacity:1;filter:alpha(opacity=100)">
        <center> 
            <div style="font-size:16px;font-weight:bold;padding:5px;margin-top:0px;" class="text headerband">
                Quotation List
                <asp:Button runat="server" ID="btnRefreshRFQ" OnClick="btnRefreshRFQ_Click" Text="s" style="display:none" />
                <asp:ImageButton ID="btnCloseApprovedPO" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="btnCloseApprovedPO_OnClick" />
            </div>   
           
           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT:24px ; text-align:center;">
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="width:100px;" />
                                <col />
                                <col style="width:140px;" />
                                <col style="width:100px;" />
                                <col style="width:260px;" />
                                <col style="width:140px;" />
                                <col style="width:50px;" />
                                
                             </colgroup>
                            <tr align="left" class= "headerstylegrid">
                                <td>Quote #</td>
                                <td>Vendor</td>
                                <td>Location</td>
                                <td>Bid Amt(US$)</td>
                                <td>Status</td>
                                <td>Approved User</td>                                
                                <td>Action</td>
                                
                            </tr>
                        </table>
                        </div>
           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px ; text-align:center;">
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    
                                    <col style="width:100px;" />
                                    <col />
                                    <col style="width:140px;" />
                                    <col style="width:100px;" />
                                    <col style="width:260px;" />
                                     <col style="width:140px;" />
                                    
                                    <col style="width:50px;" />
                                    
                                </colgroup>
                                <asp:Repeater ID="rptRFQList" runat="server">
                                     <ItemTemplate>
                                         
                                         
                                         <tr id='tr<%#Eval("bidid")%>'  >
                                            <td><%# Eval("RFQNO")%></td> 
                                            <td style=" text-align:left">
                                                <asp:Label ID="lblVendor" runat="server" Text='<%# Eval("SUPPLIERNAME") %>'  ></asp:Label>
                                                <div style="padding:3px;color:Red"><%# Eval("ApprovalTypeName")%></div>
                                            </td> 
                                            <td style="text-align :left"><%# Eval("SUPPLIERPORT")%></td> 
                                            <td style="text-align :right"><%#ProjectCommon.FormatCurrency(Eval("AMT"))%></td>
                                            <td style="text-align:left;"><%# Eval("BidStatus")%>
<span style="padding:3px;color:Red;" runat="server" visible='<%#Common.CastAsInt32(Eval("ZeroUPCount"))>0%>'>InComplete</span>
</td>
                                            <td style="text-align:left;"><%# ProjectCommon.getUserNameByID(Eval("UserID").ToString())%></td>
                                            <td>
                                                <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("bidid")%>' />
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <col width="26px" />
                                                    <col width="26px" />
                                                    <col />
                                                    <tr>
                                                      
                                                        <td>
                                                            <img src="../../HRD/Images/approved.png" alt="" style="border:solid 0px red "  title="Approve/Place Order" onclick='ViewRFQ(<%#Eval("bidid")%>)' />                                                                         
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                            
                                        </tr>
                                     </ItemTemplate>                                     
                                 </asp:Repeater>
                            </table>
                        </div>
            <div style="background-color:#ffd800; padding:5px; text-align:center;">
                <asp:Button runat="server" ID="btnqa" Text="Quote Analyser" OnClick="btnQA_Click"  CssClass="btn"/>
                <asp:Button runat="server" ID="btnBacktoRFQ" Visible="false" Text="Send Back TO Quotation Stage"  CssClass="btn" OnClick="btnBacktoRFQ_Click" OnClientClick="return confirm('Are you sure to send bids to Quotation Stage which are Approval-1 stage ?');" />
            </div>
        </center>
    </div> 
    </center>
    </div>--%>

   <%----------------------------------------------------------------------------------------%>
     <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="DvSendBackToPurchaser" visible="false"  >
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:450px; height:250px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                    <center>
                        <div style="font-size:16px;font-weight:bold;padding:5px;margin-top:0px;" class="text headerband">
                            Send Back To Purchaser
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="btnCloseSendBackToPurchaser_OnClick" />
                        </div>     
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td> Comments for purchaser  </td>
                            </tr>
                            <tr>
                                <td> 
                                    <asp:TextBox ID="txtPurchaserComments" runat="server"  TextMode="MultiLine" Width="96%" Height="120px"> </asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="btnSendBackToPurchaser" runat="server" OnClick="btnSendBackToPurchaser_OnClick" Text=" Save "  />
                                    <%--OnClientClick="return confirm('Are your sure to reset the record?')"--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Label ID="lblMSgSendBackToPurchaser" runat="server" CssClass="error"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </center>
                    </div>
                </center>
                </div>
    
         <%------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvSendBackToRFQ" visible="false"  >
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:450px; height:250px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                    <center>
                        <div style="font-size:16px;font-weight:bold;padding:5px;margin-top:0px;" class="text headerband">
                            Send Back to Quotation Stage
                        </div>     
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td> Comments </td>
                            </tr>
                            <tr>
                                <td> 
                                    <asp:TextBox ID="txtSendBackToRFQMessage" runat="server"  TextMode="MultiLine" Width="96%" Height="120px"> </asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="btnSendBackToRFQ_Save" runat="server" OnClick="btnSendBackToRFQ_Save_OnClick" Text=" Save " CssClass="btn" />
                                    <asp:Button ID="btnSendBackToRFQ_Close" runat="server" OnClick="btnSendBackToRFQ_Close_OnClick" Text=" Close "  CssClass="btn" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Label ID="lblSendBackToRFQ" runat="server" CssClass="error"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </center>
                    </div>
                </center>
                </div>


    </ContentTemplate>    
    </asp:UpdatePanel>
        </div>
    <script type="text/javascript" >
        var Id = 'tr' + document.getElementById('ctl00_ContentMainMaster_hfPRID').value;
    lastSel=document.getElementById(Id);
    </script> 
</asp:Content> 

