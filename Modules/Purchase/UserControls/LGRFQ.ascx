<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LGRFQ.ascx.cs" Inherits="UserControls_LGRFQ" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Modules/Purchase/UserControls/VendorSearchByDescription.ascx" TagName="SearchByDesc" TagPrefix="VS" %>
<%@ Register Src="~/Modules/Purchase/UserControls/VendorSearchByName.ascx" TagName="SearchByName" TagPrefix="VSN" %>
<%@ Register Src="~/Modules/Purchase/UserControls/VendorSearchByISSAIMPA.ascx" TagName="SearchByISSA" TagPrefix="VSISSA" %>
<%@ Register Src="~/Modules/Purchase/UserControls/VendorSearchByPort.ascx" TagName="SearchByPort" TagPrefix="VSP" %>

<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
 function ReloadPage() 
 {
       document.getElementById("<%=BtnReload.ClientID%>").click();
 }
function AskRFQtoCancel(Status)
{
    if (parseInt(Status)==1)
    {
        return confirm('Quote already sent.  Are you sure you want to cancel it?');
    } 
    else if (parseInt(Status)==2)
    {
        return confirm('Quotes are recieved.  Are you sure you want to cancel it?');
    } 
    else
    {
        return confirm('Are you sure you want to cancel it?');
    } 
}
function ViewRFQ(BidId)
{        
    //window.open('EditLGRFQ.aspx?BidId=' + BidId);
    window.open('Quotemanager.aspx?key=' + BidId);
}
function CheckUncheck(trSel)
{
    //    var chkbx=trSel.lastChild.lastChild.lastChild;
    var ctrl = trSel.getElementsByTagName('input');
    var chkbx = ctrl[1]
    if(chkbx==null)
    {chkbx=trSel.lastChild.lastChild;}
     
    if(trSel.getAttribute(CSSName)!="selectedrow")
    {
        trSel.setAttribute(CSSName, "selectedrow");
        chkbx.checked=true;
    }
    else
    {
        trSel.setAttribute(CSSName, trSel.getAttribute("startclass"));
        chkbx.checked=false;
    }
    
}
function ChkSelect(ctl,root)
{
    var ctls=document.getElementById(root).getElementsByTagName("input");
    for(i=0;i<=ctls.length-1;i++)
    {
        if(ctls[i].getAttribute("type")=="checkbox" && ctls[i].getAttribute("id")!="SpareRFQ_chkAll")
        {
            ctls[i].checked=ctl.checked;
            
            var trSel;
            //if(root=="tbl_Vendors")
                trSel= ctls[i].parentNode.parentNode.parentNode;
            //else
            //    trSel= ctls[i].parentNode.parentNode;
                
            if(ctl.checked)
            {
                trSel.setAttribute(CSSName, "selectedrow");
            }
            else
            {
                trSel.setAttribute(CSSName, trSel.getAttribute("startclass"));
            }
        } 
        
    }  
}
</script>  
<script type="text/javascript">
    function OpenDocument(TableID, PoId, VesselCode) {
        // alert(VesselCode);
        window.open("ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode + "&PRType=''");
    }
</script>
<center >
<asp:Button ID="BtnReload" runat="server" OnClick="BtnReload_OnClick" style="display:none;" />
<asp:Button ID="btnFree" runat="server" style="display :none"/> 
<asp:Button ID="btnFullPost" runat="server" style="display :none"/>
<div style="border:2px solid #4371a5;">
<div class="text headerband" style="height:25px; padding-top :5px;padding-bottom :5px;">

<asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Modules/HRD/Images/home.png" style="float :right; padding-right :5px" />
<asp:Label runat="server" ID="lblHeader" style="float :left; padding-left :5px;"></asp:Label>
<center>Item list for Quotation</center>
</div>
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
<colgroup>
<col style="width:60px;" />
<col />
<%--<col style="width:150px;" />
<col style="width:150px;" />
<col style="width:150px;" />--%>
<col style="width:80px;" />
<col style="width:100px;" />
<col style="width:50px;" />
<col style="width:17px;" />
</colgroup>
<tr align="left" class= "headerstylegrid">
    <td>S.No.</td>
    <td>Description</td>
    <%--<td>Part #</td>
    <td>Drawing #</td>
    <td>Code #</td>--%>
    <td>Qty</td>
    <td>UOM</td>
    <td><asp:CheckBox runat="server" ID="chkAll" onclick="ChkSelect(this,'tbl_Spares')"/>All</td>
    <td>&nbsp;</td>
</tr>
</table>
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 208px ; text-align:center;" id="tbl_Spares">
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
    <colgroup>
        <col style="width:60px;" />
        <col />
        <%--<col style="width:150px;" />
        <col style="width:150px;" />
        <col style="width:150px;" />--%>
        <col style="width:80px;" />
        <col style="width:100px;" />
        <col style="width:50px;" />
    </colgroup>
    <asp:Repeater ID="rptItems" runat="server">
        <ItemTemplate>
                <tr id='tr<%#Eval("recid")%>'  onclick='CheckUncheck(this);'>
                <td><%# Eval("Sno")%></td> 
                <td style="text-align :left"><%# Eval("Description")%></td>
                <%--<td><%# Eval("PartNo")%></td> 
                <td><%# Eval("EquipItemDrawing")%></td> 
                <td><%# Eval("EquipItemCode")%></td>--%>
                <td><asp:TextBox ID="txtQty" MaxLength="4" Width="40px" runat="server" Text='<%# Eval("Qty") %>'></asp:TextBox></td> 
                <td><%# Eval("UOM")%></td>
                <td><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("recid")%>'/></td>
                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id='tr<%#Eval("recid")%>' class='alternaterow' startclass='alternaterow' onclick='CheckUncheck(this);'>
                <td><%# Eval("Sno")%></td> 
                <td style="text-align :left"><%# Eval("Description")%></td>
                <%--<td><%# Eval("PartNo")%></td> 
                <td><%# Eval("EquipItemDrawing")%></td> 
                <td><%# Eval("EquipItemCode")%></td>--%>
                <td><asp:TextBox ID="txtQty" MaxLength="4" Width="40px" runat="server" Text='<%# Eval("Qty") %>'></asp:TextBox></td> 
                <td><%# Eval("UOM")%></td>
                <td><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("recid")%>'/></td>
                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
</table>
</div>
</div>
<%--<div style="width :98%; padding:3px; height :22px; ">
<table cellpadding="0" cellspacing="0" width="100%" >
<tr>
<td style="width:33%; text-align :left">
<asp:Label runat="server" ForeColor="Red" ID="lblMessage"></asp:Label>   
</td>
<td style="width:34%">
<asp:ImageButton ID="btnSmdPoAnalyzer" ImageUrl="~/Images/poanalysis.jpg" runat="server"  ToolTip="SDM PO Analyzer" /> 
<asp:ImageButton ID="btnSelectVendor" ImageUrl="~/Images/selectvendors.jpg" runat="server" onclick="btnSelectVendor_Click" /> 
<asp:ImageButton ID="btnFindVendorPopup" ImageUrl="~/Images/FindVendor.jpg" runat="server" onclick="btnFindVendorPopup_Click" /> 

</td>
<td style="width:33%">&nbsp;</td>
</tr>
</table> 
</div>--%>
    <div style="padding:3px; text-align:right; ">
         <span>
        <asp:ImageButton id="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents"/> 
    (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>) 
    </span> &nbsp;&nbsp;
    <asp:Label runat="server" ForeColor="Red" ID="lblMessage" style="float:left" Font-Size="Small" Font-Bold="true"></asp:Label>   
    <asp:Button runat="server" ID="btnSelectVendor" Visible="false" Text="+ Create New Quotation" OnClick="btnSelectVendor_Click" style="border:none;padding:4px;margin:0px;" CssClass="btn"/>
    <asp:Button runat="server" ID="btnBidFinished" Visible="false" OnClick="btnBidFinished_Click" Text=" Biding Closed / Send for Approval " style="border:none;padding:4px;margin:0px;" CssClass="btn"/>
    <asp:Button runat="server" ID="btnSmdPoAnalyzer" Text="Quote Analysis" style="border:none;padding:4px;margin:0px;" CssClass="btn"/>
    <asp:Button runat="server" ID="btnFindVendorPopup" Text="Find Vendors" OnClick="btnFindVendorPopup_Click"  style="border:none;padding:4px;margin:0px;" CssClass="btn"/>    
</div>

<div style="border:2px solid #4371a5; " id="tbl_RFQ">
<div class="text headerband" style="width :100%;height:20px; padding-top :5px;"><center>Quotation List</center></div>
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center;">
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
<colgroup>
       <col style="width:4%;" />
        <col style="width:4%;" />
        <col style="width:24%;"/>
        <col style="width:9%;" />
        <col style="width:9%;" />
        <col style="width:8%;" />
        <col style="width:10%;" />
        <col style="width:8%;" />
        <col style="width:8%;" />
        <col style="width:8%;" />
        <col style="width:2%;" />
        <col style="width:2%;" />
        <%--<col style="width:2%;" />--%>
        <col style="width:2%;" />
        <col style="width:2%;" />
</colgroup>
<tr align="left" class= "headerstylegrid">
    <td style=" height :25px;" >S.No.</td>
    <td>Quote #</td>
    <td style="text-align:left">Vendor</td>
    <td>Location</td>
    <td>Quote Status</td>
    <td>Bid Amount</td>
    <td style="text-align:left">Created By</td>
    <td>Created On</td>
    <td>Quote Recd. On</td>
    <td>Submitted On Approval</td>
    <td>&nbsp</td>
    <td>&nbsp</td>
    <%--<td>&nbsp</td>--%>
    <td>&nbsp</td>
    <td>&nbsp</td>
</tr>
</table>
</div>
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 208px ; text-align:center;">
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
   <colgroup>
       <col style="width:4%;" />
        <col style="width:4%;" />
        <col style="width:24%;"/>
        <col style="width:9%;" />
        <col style="width:9%;" />
        <col style="width:8%;" />
        <col style="width:10%;" />
        <col style="width:8%;" />
        <col style="width:8%;" />
        <col style="width:8%;" />
        <col style="width:2%;" />
        <col style="width:2%;" />
        <%--<col style="width:2%;" />--%>
        <col style="width:2%;" />
        <col style="width:2%;" />
    </colgroup>
    <asp:Repeater ID="rptRFQList" runat="server">
        <ItemTemplate>
            <tr id='tr<%#Eval("bidid")%>'  onclick='CheckUncheck(this);' title="Double click to view the Quotation." ondblclick='ViewRFQ(<%#Eval("bidid")%>)'>
                <td style=" text-align:left"><%# Eval("Sno")%></td> 
                <td style=" text-align:left"><%# Eval("BIDGROUPNAME")%></td> 
                <td style=" text-align:left"><asp:Label ID="lblVendor" runat="server" Text='<%# Eval("SUPPLIERNAME") %>'  ></asp:Label>
 <div style="padding:3px;color:Red">&nbsp;</div>
</td> 
                <td style="text-align :left"><%# Eval("SUPPLIERPORT")%></td> 
                <td style=" text-align:left"><%# Eval("BIDSTATUSNAME")%>
<div style="padding:3px;color:Red;" runat="server" visible='<%#Common.CastAsInt32(Eval("ZeroUPCount"))>0%>'>InComplete</div>
</td> 
                <td style="text-align :right">
                    <asp:Label ID="lblBidAmount"  runat="server" Text='<%#ProjectCommon.FormatCurrency(Eval("AMT"))%>'></asp:Label>
                   <%-- <span runat="server" Visible='<%#(ShowAmount=="N")%>'>****.**</span>--%>
                </td> 
                <td style="text-align :left"><%# Eval("BidCreatedBy")%></td> 
                <td style="text-align :left"><%# ECommon.ToDateTimeString(Eval("BidCreatedOn"))%></td>                 
                <td style="text-align :left"><%# ECommon.ToDateTimeString(Eval("BidUpdatedOn"))%></td> 
                <td style="text-align :left"><%# ECommon.ToDateTimeString(Eval("BidFwdOn"))%></td> 
                <td style=" text-align:left">
                    <div runat="server" visible='<%#!PRBidStopped%>'>
                    <asp:ImageButton runat="server" ID="btnEmail" CssClass='<%#Eval("bidid")%>' Visible='<%# (Convert.ToInt32(Eval("BidStatusId").ToString())< 2 && Convert.ToInt32(Eval("BidStatusId").ToString())>= 0) && authRFQList.IsUpdate %>'  OnClick="btnEmailRFQ_Click" ImageUrl="~/Modules/HRD/Images/email.png" ToolTip="Click here to send Quotation mail to Vendor." />
                    </div></td>
                <td style=" text-align:left">
                    <div runat="server" visible='<%#PRBidStopped%>'>
                        <asp:ImageButton runat="server" ID="btnApprove" CssClass='<%#Eval("bidid")%>' Visible='<%# (Eval("BidStatusId").ToString()=="2" && Eval("APPREQUESTS").ToString()=="0")  %>' OnClick="btnApproveRFQ_Click" ImageUrl="~/Modules/HRD/Images/approved.png" ToolTip="Click here to send bid for Approval." />
                    </div></td>
               <%-- <td>
                    <asp:ImageButton runat="server" ID="btnApprovalPO" CssClass='<%#Eval("bidid")%>' Visible='<%# (Eval("BidStatusId").ToString()=="2" && Eval("APPREQUESTS").ToString()=="1") %>' OnClick="btnApprovalPO_Click" ImageUrl="~/Images/approval1.gif" ToolTip="Click here to send bid for Direct Approval." />
                    
                </td>--%>
                <td style=" text-align:left">
                    <a target="_blank" title="Open to view the change history." href='ChangeHistory.aspx?BidId=<%#Eval("bidid")%>'><img runat="server" src="~/Modules/HRD/Images/poanalysis.png" /></a>
                    <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("bidid")%>' />
                </td>
                <td style=" text-align:left">
                    <div runat="server" visible='<%#!PRBidStopped%>'>
                        <asp:ImageButton runat="server" ID="btnRemove" CssClass='<%#Eval("bidid")%>' Visible='<%# (Eval("CanDelete").ToString()=="Y") %>' OnClick="btnCancelRFQ_Click" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClientClick='<%#"return AskRFQtoCancel(" + Eval("BidStatusId").ToString() + ")" %>' ToolTip="Click here to cancel this Quotation." />
                    </div>                    
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id='tr<%#Eval("bidid")%>' startclass='alternaterow' class='alternaterow' onclick='CheckUncheck(this);' title="Double click to view the Quotation." ondblclick='ViewRFQ(<%#Eval("bidid")%>)'>
                <td style="text-align :left"><%# Eval("Sno")%></td> 
                <td style="text-align :left"><%# Eval("BIDGROUPNAME")%></td> 
                <td style=" text-align:left"><asp:Label ID="lblVendor" runat="server" Text='<%# Eval("SUPPLIERNAME") %>'  ></asp:Label>
 <div style="padding:3px;color:Red">&nbsp;</div>
</td> 
                <td style="text-align :left"><%# Eval("SUPPLIERPORT")%></td> 
                <td style="text-align :left"><%# Eval("BIDSTATUSNAME")%>
<div style="padding:3px;color:Red;" runat="server" visible='<%#Common.CastAsInt32(Eval("ZeroUPCount"))>0%>'>InComplete</div>
</td> 
                <td style="text-align :right">
                    <asp:Label ID="lblBidAmount"  runat="server" Text='<%#ProjectCommon.FormatCurrency(Eval("AMT"))%>'></asp:Label>
                   <%-- <span runat="server" Visible='<%#(ShowAmount=="N")%>'>****.**</span>--%>
                </td> 
                <td style="text-align :left"><%# Eval("BidCreatedBy")%></td> 
                <td style="text-align :left"><%# ECommon.ToDateTimeString(Eval("BidCreatedOn"))%></td>                 
                <td style="text-align :left"><%# ECommon.ToDateTimeString(Eval("BidUpdatedOn"))%></td> 
                <td style="text-align :left"><%# ECommon.ToDateTimeString(Eval("bidfwdon"))%></td> 
                <td style="text-align :left">
                    <div runat="server" visible='<%#!PRBidStopped%>'>
                    <asp:ImageButton runat="server" ID="btnEmail" CssClass='<%#Eval("bidid")%>' Visible='<%# (Convert.ToInt32(Eval("BidStatusId").ToString())< 2 && Convert.ToInt32(Eval("BidStatusId").ToString())>= 0) && authRFQList.IsUpdate %>'  OnClick="btnEmailRFQ_Click" ImageUrl="~/Modules/HRD/Images/email.png" ToolTip="Click here to send Quotation mail to Vendor." />
                    </div></td>
                <td style="text-align :left">
                    <div runat="server" visible='<%#PRBidStopped%>'>
                        <asp:ImageButton runat="server" ID="btnApprove" CssClass='<%#Eval("bidid")%>' Visible='<%# (Eval("BidStatusId").ToString()=="2" && Eval("APPREQUESTS").ToString()=="0")  %>' OnClick="btnApproveRFQ_Click" ImageUrl="~/Modules/HRD/Images/approved.png" ToolTip="Click here to send bid for Approval." />
                    </div></td>
              <%--  <td>
                    <asp:ImageButton runat="server" ID="btnApprovalPO" CssClass='<%#Eval("bidid")%>' Visible='<%# (Eval("BidStatusId").ToString()=="2" && Eval("APPREQUESTS").ToString()=="1") %>' OnClick="btnApprovalPO_Click" ImageUrl="~/Images/approval1.gif" ToolTip="Click here to send bid for Direct Approval." />
                    
                </td>--%>
                <td style="text-align :left">
                   <a target="_blank" title="Open to view the change history." href='ChangeHistory.aspx?BidId=<%#Eval("bidid")%>'><img runat="server" src="~/Modules/HRD/Images/poanalysis.png" /></a>
                    <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("bidid")%>' />
                </td>
                <td style="text-align :left">
                    <div runat="server" visible='<%#!PRBidStopped%>'>
                        <asp:ImageButton runat="server" ID="btnRemove" CssClass='<%#Eval("bidid")%>' Visible='<%# (Eval("CanDelete").ToString()=="Y") %>' OnClick="btnCancelRFQ_Click" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClientClick='<%#"return AskRFQtoCancel(" + Eval("BidStatusId").ToString() + ")" %>' ToolTip="Click here to cancel this Quotation." />
                    </div>                    
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
</table>
</div>
</div>
<!-- div for show vendor  -->
<div style="position:absolute;top:0px;left:0px; height :550px; width:100%;font-family:Arial;font-size:12px;" runat="server" id="ModalPopupExtender12" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:950px;height:550px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;">
        <div style=" float :right">
<asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn"
        ToolTip="Close this Window." onclick="btnClose_Click"/> 
</div> 

        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate> 
<div style="float:left;padding: 0px 5px 0px 5px;" class="bluetext" >Supplier Name :</div>
<div style="float:left" ><asp:TextBox runat="server" ID="txtVendor" OnTextChanged="txtVendor_TextChanged" AutoPostBack="true" Text="A" ></asp:TextBox></div>
<div style="float:left;padding-left :5px;" ><asp:ImageButton runat="server" id="btnFind" OnClick="btnFind_Click" ImageUrl="~/Modules/HRD/Images/Search.jpg" AlternateText="Find"/> 
<div style="float:left;padding-left :170px; font-size :larger; text-decoration :underline" class="bluetext"  >Vendors List</div>
<%--<div class="header" style="width :100%;height:20px; padding-top :5px; margin-top:4px;WIDTH: 50%;"><center>Vendors List</center></div>--%>
<table>
<tr>
<td>
<div style="width :444px; border :solid 1px #4371a5;" >
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
<colgroup>
        <col style="width:40px;" />
        <col />
        <col style="width:50px;" />
        <col style="width:17px;" />
</colgroup>
<tr align="left" class= "headerstylegrid">
    <td>S.No.</td>
    <td>Supplier Name</td>
    <td><asp:CheckBox runat="server" ID="CheckBox1" onclick="ChkSelect(this,'tbl_Vendors')"/>All</td>
    <td>&nbsp;</td>
</tr>
</table>
</div>
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 444px;HEIGHT: 370px ; text-align:center; float :left;border:solid 1px #4371a5">
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" id="tbl_Vendors">
    <colgroup>
        <col style="width:40px;" />
        <col />
        <col style="width:50px;" />
    </colgroup>
    <asp:Repeater ID="rptVendors" runat="server">
        <ItemTemplate>
                <tr id='tr<%#Eval("SupplierId")%>'  onclick='CheckUncheck(this);'>
                <td><%# Eval("Sno")%></td> 
                <td style="text-align :left">
                    <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>' Width="100%" ></asp:Label><br />
                    <asp:TextBox runat="server" ID="txtEmail" BorderColor="Orange" Width="230px" Text ='<%# Eval("SupplierEmail")%>' Enabled="false" ></asp:TextBox>
                    <em style=" color : red" ><%# Eval("TravId")%>, </em> 
                    <em style=" color : blue" ><%# Eval("SupplierPort")%></em> 
                     
                   
                     
                </td>
               <td><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("SupplierId")%>'/>
                     
                   
                     
                    </td>
                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id='tr<%#Eval("SupplierId")%>' class='alternaterow' startclass='alternaterow' onclick='CheckUncheck(this);'>
                <td><%# Eval("Sno")%></td> 
                <td style="text-align :left"><asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label><br />
                <asp:TextBox runat="server" ID="txtEmail" BorderColor="Orange" Width="230px" Text ='<%# Eval("SupplierEmail")%>' Enabled="false"></asp:TextBox>
                <em style=" color : red" ><%# Eval("TravId")%>, </em> 
                <em style=" color : blue" ><%# Eval("SupplierPort")%></em> 
                    
                   
                    
                </td>
                <td><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("SupplierId")%>'/>
                   
                    
                </td>
                 <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
</table>
</div>
</td>

<td>
<div style=" width :40px; float :right; text-align :center; padding:50px 5px 0px 0px">
<center >
<br /><br />
    <asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/Modules/HRD/Images/right_24.png" 
        onclick="btnAdd_Click"  /> 
</center>
</div>
</td>
<td>
<div style="width :444px; border :solid 1px #4371a5;" >
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
<colgroup>
        <col style="width:40px;" />
        <col />
        <col style="width:17px;" />
</colgroup>
<tr align="left" class= "headerstylegrid" style=" height:20px;" >
    <td>S.No.</td>
    <td>Supplier Name</td>
    <td>&nbsp;</td>
</tr>
</table>
</div> 
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 444px; HEIGHT: 330px ; text-align:center; float :right ;border:solid 1px #4371a5">
<asp:Label runat="server" CssClass="bluetext" ID="lblSelVendorMess" Text="No vendors selected."></asp:Label>   
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" id="tbl_SelVendors">
    <colgroup>
        <col style="width:40px;" />
        <col />
        <col style="width:20px;" />
    </colgroup>
    <asp:Repeater ID="rptSelVendors" runat="server">
        <ItemTemplate>
                <tr id='tr<%#Eval("SupplierId")%>' class='row' >
                <td><%# Eval("Sno")%></td> 
                <td style="text-align :left">
                    <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>' Width="300px" ></asp:Label><br />
                    <em style=" color : orange" ><%# Eval("SupplierEmail")%></em>
                    <em style=" color : red" ><%# Eval("TravId")%>, </em> 
                    <em style=" color : blue" ><%# Eval("SupplierPort")%></em> 
                   
                   
                    
                </td>
               <td><asp:ImageButton runat="server" ID="btnRemove" CssClass='<%#Eval("SupplierId")%>' OnClick="btnRemove_Click" ImageUrl="~/Modules/HRD/Images/Delete.jpg"/>
                    
                   
                   
                    </td>
               <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id='tr<%#Eval("SupplierId")%>' class='alternaterow' >
                <td><%# Eval("Sno")%></td> 
                <td style="text-align :left"><asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label><br />
                <em style=" color : orange" ><%# Eval("SupplierEmail")%></em> 
                <em style=" color : red" ><%# Eval("TravId")%>, </em> 
                <em style=" color : blue" ><%# Eval("SupplierPort")%></em> 
                   
                </td>
                <td><asp:ImageButton runat="server" ID="btnRemove" CssClass='<%#Eval("SupplierId")%>' OnClick="btnRemove_Click" ImageUrl="~/Modules/HRD/Images/Delete.jpg" /> 
                    
                  
                </td>
                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
</table>
</div>
<div style=" padding-top :10px;clear:both ; text-align :right  " >
     <table width="100%" style="font-family:Arial;font-size:12px;">
        <tr style="padding:5px;">
  <td width="35%" style="text-align:right;padding-right:5px;">
      Delivery Port : 
        </td>
              <td style="text-align:left;padding-left:5px;">
                  <asp:TextBox ID="txtDeliveryPort" runat="server" MaxLength="15" Width="140px" style="text-align:left;"></asp:TextBox>
        </td>
             </tr>
        <tr style="padding:5px;" >
            <td width="35%" style="text-align:right;padding-right:5px;">Expected Delivery Date : </td>
            <td style="text-align:left;padding-left:5px;"><asp:TextBox ID="txtDeliveryDate"  CssClass="date_only" runat="server" MaxLength="15" Width="100px"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDeliveryDate"></asp:CalendarExtender>
            </td>
        </tr>
    </table>
    <br />
<asp:Label runat="server" style=" float :left " ID="lblPOPMsg" ForeColor="Red" ></asp:Label> 
<asp:Button runat="server" ID="btnCreateRFQ" Text="Create Quotation" CssClass="btn" OnClick ="btnCreateRFQ_Click" /> 
</div>
</td>
</tr>
</table>
</div>
            
            
            
</ContentTemplate> 
        </asp:UpdatePanel>
        </div> 
    </center>
</div>
<!-- div for show approval -->
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="ModalPopupExtender22" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:450px;  padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;">
        <center>
<asp:HiddenField runat="server" ID="hfdBidId" /> 
<div style=" float :right " ><asp:Button runat="server" ID="btnClose2" Text="Close" CssClass="btn"  ToolTip="Close this Window." onclick="btnClose2_Click"/> </div>
<h4 style=" clear :both" >Please select Superintendent for Approval</h4>
<h4><asp:Label runat="server" ID="lblBidRefNo"></asp:Label></h4>
<h4><asp:Label runat="server" ForeColor="Red" ID="lblError"></asp:Label></h4>
<table cellspacing="0" cellpadding="4" style="width:95%;border-collapse:collapse;display:none;">
<tr>
<td style=" text-align:right ; font-weight : bold " >
 Select Supt. : 
</td>
<td style=" text-align:left ">
 <asp:DropDownList ID="ddlSupt" runat="server" Width="200px"></asp:DropDownList>
 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator" ControlToValidate="ddlSupt" ValidationGroup="vv" ErrorMessage="Required." ></asp:RequiredFieldValidator> 
</td>
</tr>
</table> 


<br />
<%-----------------------------------------------------------------------------------------------------------------%>
<div id="dvApprovalLevel" runat="server" visible="true" style="border:solid 1px #C2C2C2;margin:5px;padding:5px;font-family:Arial;font-size:12px;">
                <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                         
                      <tr  runat="server" visible="false" >
                          <td align="right" style="text-align: right; padding-right:15px; ">Approval 1 :</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlVerifyForwardTo"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList> <span style='color:#ff0000'> ( < USD 500 )</span>
                          </td>
                      </tr>
                      <tr  runat="server" visible="false"  style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; ">Approval 2 :</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlApproval2"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList><span style='color:#ff0000'> ( < USD 25000 )</span>
                          </td>
                      </tr>
                      <tr  runat="server" visible="false" >
                          <td align="right" style="text-align: right; padding-right:15px; ">Approval 3 :</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlApproval3"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList><span style='color:#ff0000'> ( < USD 100000 )</span>
                          </td>
                      </tr>
                       <tr  runat="server" visible="false"  style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; ">Approval 4 :</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlApproval4"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList><span style='color:#ff0000'> ( >= USD 100000 )</span>
                          </td>
                      </tr>  
                    <tr>
                        <td>
                            <b>Purchaser Comments</b>
                        </td>
                    </tr>                  
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtpComments" Width="100%" TextMode="MultiLine" Rows="7"></asp:TextBox>
                        </td>
                    </tr>                      
                      </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style="padding:4px;">
                              &nbsp; 
                              <asp:Label ID="lblMsgPOApprovalRequest" runat="server" CssClass="error"></asp:Label>
                          </td>
                        </tr>
                      </table>
            </div>
<%-----------------------------------------------------------------------------------------------------------------%>
<asp:Button ID="btn_AppSave" runat="server" Text="Save" Width="80px" OnClick="btn_AppSave_Click" ValidationGroup="app" style="  border:none; padding:4px;" CssClass="btn" />                            
<%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Images/sendmail.jpg" onclick="btnSendMail_Click" ValidationGroup="vv" style="display:none;"/>--%>
</center> 
        </div> 
    </center>
</div>

<%--Div for Select Vendor List--%>
  <div style="position:absolute;top:0px;left:0px; height :550px; width:100%;z-index:100;" runat="server" runat="server" id="divVendorSearch" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
     <div style="position :relative; width:1200px; height:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <col  width="260px" />            
            <col  width="50px" />     
            <col  width="260px" /> 
            <col  width="200px" />
            <col  width="200px" />
            <col   />
            <tr align="left" class= "headerstylegrid">
                <td>Vendor Name</td>   
                <td>&nbsp;</td>             
                <td>Item Name</td>
                <td>Country</td>
                <td>City/State</td>
                <td>Port</td>
            </tr>      
            <tr>
                <td><VSN:SearchByName ID="VSByName" runat="server" width="180px" /></td>
                <td>&nbsp;</td>     
                <td><VS:SearchByDesc ID="vs1" runat="server"/></td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server" Width="150px"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtCityState" runat="server"  Width="150px"></asp:TextBox>
                </td>
                <td>
                    <VSP:SearchByPort ID="VSByPort" runat="server" width="180px" />
                </td>
            </tr>                 
        </table>
         <table cellpadding="2" cellspacing="2" border="0" width="100%">
             <col width="100px" />
             <col />
             <col width="100px" />
             <tr>
                 <td>
                    Vendor Type :
                </td>
                <td style="text-align:left;">
                    <asp:Label ID="lblBusinessType" runat="server"></asp:Label>    
                    <asp:LinkButton ID="lnkOpenBusinessType" runat="server" OnClick="lnkOpenBusinessType_OnClick" Text="Select"  style="margin-left:20px;" ></asp:LinkButton>                
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnCleaerBusinessType_OnClick" Text="Clear"  style="margin-left:10px;" ></asp:LinkButton>                
                </td>
                <td>
                    <asp:Button ID="btnSearchVendor" runat="server" Text=" Search" CssClass="btn" OnClick="btnSearchVendor_OnClick" />
                </td>
             </tr>
         </table>
        <%---------------------------------------------------------------------------------------------------------------------------------------------------------------%>
        <%--<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px ; text-align:center; float :right ;">--%>
        <div style="width:100% ;OVERFLOW-Y: hidden; OVERFLOW-X:hidden;border:solid 1px #4371a5;">
            <table cellpadding="1" cellspacing="0"  border="1" rules="all" width="100%" style="height:20px; text-align:left;border-collapse:collapse;" >
                    <col   />
                    <col width="110px" />  
                    <col width="120px" />  
                    <col width="190px" />
                    <col width="270px" />                      
                    <col width="60px" />  
                    <col width="80px" />  
                    <col width="17px" />  
                     <tr class= "headerstylegrid" >
                        <td>Vendor</td>
                        <td>Trav Ven Code</td>
                        <td>Port</td>
                        <td>Telephone</td>
                        <td>Email Address</td>
                        <td>Active</td>
                        <td>Select</td>
                        <td></td>
                    </tr>
                </table>        
         </div> 
            <div style="width:100% ; height:380px; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;border:solid 1px #4371a5;">
                <asp:Label ID="lblMsgFindVendor" runat="server" Font-Bold="false" ForeColor="Red" Text=""></asp:Label>
                <table cellpadding="1" cellspacing="0"  border="1" rules="all" width="100%" style="text-align:left; border-collapse:collapse;" >
                    <col />  
                    <col width="110px" />  
                    <col width="120px" />  
                    <col width="190px" />
                    <col width="270px" />                      
                    <col width="60px" />                      
                    <col width="80px" />             
                    <col width="17px" /> 
                </table>
                <asp:Repeater ID="rptSearched_VendorList" runat="server" >
                    <ItemTemplate>
                            <tr>
                                <td>
                                     <%#Eval("SupplierName")%> 
                                     <asp:HiddenField ID="hfSupplierID" runat="server" Value='<%#Eval("SupplierID")%>' />
                                </td>
                                <td align="center"> <%#Eval("TRAVID")%> </td>
                                <td> <%#Eval("SupplierPort")%> </td>
                                <td> <%#Eval("SupplierTel")%> </td>
                                <td> <%#Eval("SupplierEmail").ToString().Replace(";","</br>")%> </td>
                                <td align="center"> <%#(Eval("Active").ToString()=="True")?"Yes":"No"%> </td>
                                <td>
                                    <asp:Button ID="btnSelectVendor" runat="server" Text="Select" CssClass="btn" OnClick="btnSelectVendor_OnClick" CommandArgument='<%#Eval("SupplierName")%> ' />
                                </td>
                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
                </div>
        <%---------------------------------------------------------------------------------------------------------------------------------------------------------------%>
        <asp:Button ID="btnCloseSearchVendor" runat="server" Text="Close" OnClick="btnCloseSearchVendor_OnClick" CssClass="btn" style="margin:4px;" />
     </div>
     </center>
  </div>
</center> 

<%-- Business type popup-------------------------------------------------------------------------%>
<div style="position:absolute;top:0px;left:0px; height :550px; width:100%;z-index:100;" runat="server"  id="divAddBusinessType" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
     <div style="position :relative; width:700px; height:350px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
         <asp:HiddenField ID="hfSelBusinessType" runat="server" />
        
         
         <table cellpadding="3" cellspacing="3" border="0" width="100%">
             <tr class="header">
                 <td>
                     <b>Select Vendor Type</b>
                     <asp:ImageButton ID="btnCloseAddBusinessTypePopup" runat="server" Text="Close" CssClass="btn"
                         OnClick="btnCloseAddBusinessTypePopup_OnClick" style="float:right;"  />
                 </td>
             </tr>
            </table>
        <table cellpadding="3" cellspacing="3" border="0">
            <tr>
                <td style="text-align:left;">
                    <b>Vendor Type</b>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="controlarea">&nbsp;
                        <asp:CheckBoxList id="chkVendorbusinesseslist" runat="server" RepeatColumns="3"   ></asp:CheckBoxList>               
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <asp:Button ID="btnAddBusinessType" runat="server" Text="Select" OnClick="btnAddBusinessType_OnClick" />
                    <asp:Button ID="btnCloseBusinessType" runat="server" Text=" Close " OnClick="btnCloseAddBusinessTypePopup_OnClick" />
                </td>
            </tr>
        </table>
     </div>
        
    </center>
</div>

<%-- Purchaser Comments -------------------------------------------------------------------------%>
<div style="position:absolute;top:0px;left:0px; height :550px; width:100%;z-index:100;" runat="server"  id="dbPRComm" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
     <div style="position :relative; width:700px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
         <asp:HiddenField ID="HiddenField1" runat="server" />
        
         
         <table cellpadding="3" cellspacing="3" border="0" width="100%">
            <tr>
                 <td style="padding:5px; text-align:left;">
                     <asp:Label runat="server" ID="lblCommReason" Font-Bold="true" Font-Size="Larger" ForeColor="Blue"></asp:Label>
                 </td>
             </tr>
             <tr runat="server" id="trReason" visible="false">
                 <td style="padding:5px; text-align:left;">
                     Reason For Single Quote :
                     <asp:DropDownList runat="server" ID="ddlReason">
                         <asp:ListItem Text=" < Select Reason > " Value="0"></asp:ListItem>
                         <asp:ListItem Text="No Other Vendor Available" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Emergency Requirement" Value="2"></asp:ListItem>
                     </asp:DropDownList>
                 </td>
             </tr>
             <tr>
                 <td>
                     <b>Enter Comments Below </b>                     
                 </td>
             </tr>
             <tr>
                 <td>
                    <asp:TextBox runat="server" ID="txtprcomm" Width="100%" Rows="5" TextMode="MultiLine"></asp:TextBox>
                 </td>
             </tr>
            </table>
        
            <div style="text-align:center;">
                <asp:Label runat="server" ID="lblmsg01" ForeColor="Red" Font-Size="Small" style="float:left"></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="Forward" OnClick="btnBidFinishedConfirm_Click" CssClass="btn" />
                <asp:Button ID="Button2" runat="server" Text="Close" OnClick="btnBidFinishedCancel_Click" CssClass="btn"/>
            </div>
            
     </div>
        
    </center>
</div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divAttachment" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
        <br />
        <div class="text headerband"> <b>Attached Documents</b> 
             <asp:ImageButton ID="imgClosePopup" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" OnClick="imgClosePopup_Click" />
        </div>
        <br /><br />
        <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                                 
                               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                                   <colgroup>
                                       <col width="50px" />
                                       <col />
                                       <col width="90px" />
                                       <tr class="headerstylegrid" style="font-weight:bold;">
                                           <td ></td>
                                           <td >File Name</td>
                                           <td >Attachment</td>
                                       </tr>
                                       <asp:Repeater ID="rptDocuments" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                  <td style="text-align:center;">
                                                       <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("DocId")%>' visible='<%#Common.CastAsInt32(Eval("StatusId")) == 0 %>' />
                                                  </td>
                                                   <td style="text-align:left;padding-left:5px;"><%#Eval("FileName")%>
                                                       <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("DocId")%> ' />
                                                   </td>
                                                   <td style="text-align:center;"> 
                                                    <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                                                        <a onclick='OpenDocument(<%#Eval("DocId")%>,<%#Eval("RequisitionId")%>,"<%#Eval("VesselCode")%>")' style="cursor:pointer;">
                                                       <img src="../../HRD/Images/paperclip12.gif" />
                                                       </a>
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </colgroup>
                        </table>
                                     </div>
        <asp:Button ID="btnPopupAttachment" runat="server" CssClass="btn" onclick="btnPopupAttachment_Click" Text="Cancel" CausesValidation="false" Width="100px" />
         </center>
        </div> 
    </center>
    </div>