<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReqFromVessels.aspx.cs" Inherits="ReqFromVessels" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">


    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
   <%--  <link href="../CSS/style.css" rel="style" type="text/css" />--%>
    <script type="text/javascript" src="../../../JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel=null;
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
                    //                        document.getElementById('ctl00_ContentMainMaster_hfPRID').value = "";
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
            //__doPostBack('ctl00_ContentMainMaster_btnViewPR','');
           // alert('Hi');
            var btn = document.getElementById("ctl00_ContentMainMaster_btnViewPR");
            btn.click();
        //            var prid="0"+document.getElementById('ctl00_ContentMainMaster_hfPRID').value;
        //            if(parseInt(prid) > 0)
        //            {
        //                document.location='AddRequisition.aspx?PRID=' + prid;
        //            }  
        //            else
        //            {
        //            alert('please select a purchase request.'); 
        //            }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <%--<form id="form1" runat="server" defaultbutton="btnSearch">--%>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
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
        
        
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; text-align: left;">
        <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
        <td class="text headerband" style=" padding:4px;" >Requisition & Quotation
       <%-- <asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " />--%>  
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
                       <col style="text-align:center;" width="16%" />
                       <col style="text-align:center;" width="10%" />
                       <col style="text-align:center;" width="10%" />
                       <col style="text-align:center;" width="10%" />
                       <col style="text-align:center;" width="10%" />
                       <col style="text-align:center;" width="10%" />
                       <col style="text-align:center;" width="7%" />
                       <col style="text-align:center;" width="9%" />
                       <col style="text-align:center;" width="10%" />
                       <col style="text-align:center;" width="8%" />
             
                       <tr align="center" class= "headerstylegrid">
                           <td>
                               Vessel</td>
                           <td>
                               Requisition Type</td>
                           <td>
                               Requisition Number</td>
                           <td>
                               Req. Supply Status </td>
                            <td>
                              Requisition Stage   
                            </td>
                           <td>
                               From Date</td>
                           <td>
                               To Date</td>
                            <td>
                               Criticality</td>
                           <td> 
                               &nbsp;</td>
                               <td> 
                               &nbsp;</td>
                       </tr>
                       <tr align="center" >
                           <td style=" text-align :left" >
                               <%--<asp:DropDownList ID="ddlVessel" runat="server" Width="50%"></asp:DropDownList>--%>
                               <asp:UpdatePanel runat="server" UpdateMode="Always">
                               <ContentTemplate>
                                   <asp:DropDownList ID="ddlVessel" runat="server" Width="150px"></asp:DropDownList>
                                    <%--<uc1:VSlDropDown ID="ddlVessel" runat="server"/>--%>
                                    <asp:CheckBox ID="ChkAllVess" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkAllVess_CheckedChanged" Text="Include Inactive" />
                                    <asp:CheckBox ID="chkNWC" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkNWC_CheckedChanged" Text="NWC" />
                                    </ContentTemplate>
                               </asp:UpdatePanel>
                               <asp:Button ID="btnPost" runat="server" OnClick="btnPost_OnClick"  style="display:none;"/>
                               <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
                           </td>
                           <td>
                               <asp:DropDownList ID="ddlPRType" runat="server" Width="95%"></asp:DropDownList>
                           </td>
                           <td>
                               <asp:TextBox ID="txtPRNumber" runat="server" Width="95%"></asp:TextBox>
                           </td>
                           <td>
                                 <asp:DropDownList ID="ddlReqSupplyStatus" runat="server" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlReqSupplyStatus_SelectedIndexChanged">
                                   <asp:ListItem Text="All" Selected="True" Value="0"  > </asp:ListItem>
                                    <asp:ListItem Text="Supply In-Progress" Value="1" ></asp:ListItem>
                                    <asp:ListItem Text="Supply Completed" Value="2" ></asp:ListItem>
                                    <asp:ListItem Text="Supply Cancelled" Value="3" ></asp:ListItem>
                               </asp:DropDownList>
                              
                           </td>
                             <td>
                              <asp:DropDownList ID="ddlStatus" runat="server" Width="95%"></asp:DropDownList>
                           </td>
                           <td>
                               <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="95%" style="text-align:center"></asp:TextBox>
                           </td>
                           <td>
                               <asp:TextBox ID="txtToDate" runat="server" Text="" Width="95%" style="text-align:center"></asp:TextBox>
                           </td>
                           <td>
                                <asp:DropDownList ID="ddlCriticality" runat="server" Width="95%">
                                   <asp:ListItem Text="Select" Selected="True" Value="0"  > </asp:ListItem>
                                    <asp:ListItem Text="Urgent" Value="U" ></asp:ListItem>
                                    <asp:ListItem Text="Fast Track" Value="FT" ></asp:ListItem>
                                    <asp:ListItem Text="Safety Urgent" Value="SU" ></asp:ListItem>
                               </asp:DropDownList>
                           </td>
                           <td>
                               <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn" /> &nbsp;
                               <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn" /> &nbsp;
                           </td>
                           <td>
                                <asp:Button ID="btnDownloadExcel" runat="server" Text="Download" OnClick="btnDownloadExcel_Click" style=" background-color:Orange; font-size:12px;  border:solid 2px orange; "/>
                           </td>
                       </tr>
                 </colgroup>
            </table>   
           </td>
        </tr>
        <tr>
            <td>
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                        <tr >
                        <td colspan="7" style=" padding-left :5px;" >
                        <asp:Button ID="ancAddReq" runat="server" CssClass="btn" Text="Add Requisition" ToolTip="Add Requisition : Create New Requisition" OnClick="ancAddReq_Click" /> &nbsp;
                        <%--<a href="AddRequisition.aspx" runat="server" id="ancAddReq"><img src="../../HRD/Images/NewReq.jpg" alt="View" style="cursor:pointer; border:none;" title="Add Requisition : Create New Requisition" /></a> --%>
                        <asp:ImageButton runat="server" style="display :none" id="btnViewPR" ImageUrl="~/Modules/HRD/Images/magnifier.png" ToolTip="View : Click here to view the selected purchase request." onclick="btnViewPR_Click" />
                        <img src="~/Modules/HRD/Images/edit.jpg" alt="Edit" style="cursor:pointer;display :none" onclick="EditPR();" title="Edit : Click here to edit the selected purchase request."/> 
                            <asp:Button runat="server" ID="btnCancelRequisition" OnClick="btnCancelPR_Click"  Text="Cancel Requisition" ToolTip="Click here to cancel this Purchase Request." CssClass="btn" OnClientClick="Are you sure you want to cancel it?"/>
                        <asp:button runat="server" id="btnCreateRFQ" Text="Make Inquiry" ToolTip="Click here to create Inquiry for selected purchase request." onclick="btnCreateRFQ_Click" CssClass="btn" />
                        

                           
                        <%--<asp:ImageButton runat="server" id="btnApprovePO" ImageUrl="~/Images/approvepo.jpg" ToolTip="Approve PO : Click here to approve pednding PO." PostBackUrl="~/ApprovalList.aspx"  />--%>

                            
                         <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label>
                        </td>
                                            
                        <td style=" text-align :right " colspan="4" >
                            <asp:Label ID="lblRowCount" runat="server" Font-Bold="true" ></asp:Label></td>
                             <td colspan="3" style="text-align:center;font-weight: bolder;"  >Inquiry</td>   
                        </tr>
                        <tr align="left" class= "headerstylegrid">
                            <td style="width:3%;" >Vessel</td>
                            <td style="width:3%;" >Type</td>
                            <td style="width:6%;" >VSL Req#</td>
                            <td style="width:6%;" >Off Req#</td>
                            <td style="width:15%;">Requisition Title</td>
                            <td style="width:7%;" >Receive Date</td>
                            <td style="width:9%;">Budget Category</td>
                            <td style="width:9%;">Account Code</td>    
                            <td style="width:10%; text-align:left;">Ship Comments</td> 
                            <td style="width:9%; text-align:left;">Req. Supply Status</td> 
                            <td style="width:8%;" >Requisition Stage</td>
                            <td style="width:5%;" >Criticality</td>
                            <td style="width:4%;" >Sent</td>
                            <td style="width:4%;" >Rcv</td>
                            <%--<td style="text-align:left;" >Office Comments</td>--%>
                            <td style="width:2%;"></td>
                        </tr>
                </table>
               <div id="dvscroll_RFQ" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 328px ; text-align:center;" onscroll="SetScrollPos(this)">
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:3%;" />
                        <col style="width:3%;" />
                        <col style="width:6%;" />
                        <col style="width:6%;" />
                        
                        <col style="width:15%;" />
                       
                        <col style="width:7%;" />
                         <col style="width:9%;" />
                        <col style="width:9%;" />
                        <col style="width:10%;" />
                        <col style="width:9%;" />
                        <col style="width:8%;" />
                         <col style="width:5%;" />
                        <col style="width:4%;" />
                        <col style="width:4%;" />
                       <%-- <col />--%>
                        <col style="width:1%;" />
                    </colgroup>
                    <asp:Repeater ID="RptPRMaster" runat="server">
                        <ItemTemplate>
                                <tr id='tr<%#Eval("Poid")%>' class='<%#(Convert.ToInt32(Eval("Poid"))!=SelectedPoId)?"":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("Poid")%>);'  ondblclick="ViewPR()" title="Double click to view the purchase request.">
                                <td>
                                    <asp:Label ID="lblShip" runat="server" Text='<%# Eval("ShipID") %>'></asp:Label>
                                </td>
                                <td ><%# Eval("PRTypeCode")%>
                                <td ><%# Eval("reqNo")%>
                                </td>
                                <td >
                                    <asp:Label ID="lblPRNumber" runat="server" Text='<%# Eval("prnum") %>'></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <%# Eval("RequisitionTitle")%>
                                </td>
                              
                                <td  >
                                    <%--<asp:Label ID="Label2" runat="server" Text='<%# Eval("postatus") %>'></asp:Label>--%>
                                    <asp:Label ID="lblCreated" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                </td>
                                  <td style="text-align:left;">
                                    <asp:Label ID="lblPRType" runat="server" Text='<%# Eval("deptName") %>'></asp:Label>
                                </td>
                                <td style="text-align:left;"  title='<%# Eval("AccountName")%>'> 
                                    <%# Eval("AccountName")%>
                                </td>
                                 <td style=" text-align:left" >
                                           <asp:Label ID="lblCommentForVessel" runat="server" 
                                        Text='<%# Eval("PoCommentShort") %>' ToolTip='<%# Eval("PoComments2") %>'></asp:Label>                      
                                </td>
                                  
                               <%-- <td style="text-align:left;">
                                   
                                    <div style="height:14px;position:relative; overflow:hidden">
                                     <span style="float:left;"><%#Eval("officecomments")%>  </span>
                                     <asp:ImageButton style="position:absolute;top:0px;right:0px;background-color : Transparent" runat="server" ID="imgbtnOfficeRem" CssClass='<%#Eval("Poid")%>' OnClick="imgbtnOfficeRem_Click" ImageUrl="~/Modules/HRD/Images/icon_comment.gif" ToolTip="Click here to enter office comments." />
                                    
                                 </div>
                                </td>
                                <td><asp:ImageButton runat="server" ID="btnRemove" CssClass='<%#Eval("Poid")%>'  OnClick="btnCancelPR_Click" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClientClick='<%#"return AskPRtoCancel(" + Eval("StatusId").ToString() + ")" %>' ToolTip="Click here to cancel this Purchase Request." style="background-color : Transparent" /></td>--%>
                                    <td style="text-align:left;" ><%#Eval("SupplyStatus")%></td> 
                                    <td  style="text-align :left;color:<%#getStatusColor(Eval("StatusName").ToString())%>"><%#Eval("StatusName")%></td>
                                <td><%#Eval("Criticality")%></td>
                                <td>
                                    <%#Eval("TotbidsSent")%>
                                </td>
                                <td>
                                    <%#Eval("totbidsRecd")%>
                                </td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
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
    
   <%-- <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; border:solid 2px red; background-color :Gray; opacity:0.4;filter:alpha(opacity=40)" runat="server" id="dvConfirmCancel" visible="false" >
    <center>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;">
   --%>
   <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvConfirmCancel" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; height:190px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
   
        <br /><br />
        <div class="text headerband"> <b>Please Enter Reason for Cancellation</b> </div>
        <br /><br />
        <asp:TextBox TextMode="MultiLine" runat="server" CssClass="input_box" ID="txtReason" Height="86px" Width="431px"></asp:TextBox> 
        <br />
            <asp:RequiredFieldValidator ID="CompareValidator2" runat="server" ControlToValidate="txtReason" Display="Dynamic" ErrorMessage="Required." ValidationGroup="mnth"></asp:RequiredFieldValidator>
            <br />
            <asp:Label runat="server" ForeColor ="Red" ID="lblPopError" ></asp:Label>  
            <br />
            <asp:Button ID="btnSet" runat="server" CssClass="btn" onclick="btnCancelPRSubmit_Click" Text="Submit" ValidationGroup="mnth" Width="100px" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" onclick="btnCancelPRCancel_Click" Text="Cancel" CausesValidation="false" Width="100px" />
            
        </div> 
    </center>
    </div>

    

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownloadExcel" />
    </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" >
        var Id = 'tr' + document.getElementById('ctl00_ContentMainMaster_hfPRID').value;
    lastSel=document.getElementById(Id);
    </script> 
<%--</form> 
    
</body>
</html>  --%> 
    </asp:Content>

