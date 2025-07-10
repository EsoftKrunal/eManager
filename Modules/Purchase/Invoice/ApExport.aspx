<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApExport.aspx.cs" Inherits="Invoice_ApExport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <script type="text/javascript" src="../JS/Common.js"></script>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
    <style type="text/css"> 
        *
        {
            font-family: 'Open Sans', sans-serif;
            
        }
        body
        {
            padding:0px;
            margin:0px;
            font-size:13px;
        }
        td
        {
         word-break:break-all; 
         height:25px;
        }
        .green
        {
        	background-color:#54C571;
        }
        .yellow
        {
        	background-color: #FFF8C6; 
        }
        .header
        {
            background-color:#008AE6; color:White;
            text-align:center;
        }
    </style>
    <script type="text/javascript" >
        function CheckUncheck(chk) {

            //var chkbx=trSel.firstChild.childNodes[1].childNodes[1].firstChild.firstChild.firstChild;
            var trSel = chk.parentNode.parentNode.parentNode.parentNode;
            var cls = trSel.className;
            if (chk == null)
            { return; }
            if (cls != "selectedrow") {
                trSel.className = "selectedrow";
            }
            else {
                trSel.className = "";
            }
        }
        function ReSelect(ctl) {
            var ctls = document.getElementsByName("tdcheck");
            for (i = 0; i <= ctls.length - 1; i++) {
                var chk = ctls[i].firstChild.firstChild;
                if (chk.checked) {
                    CheckUncheck(chk);
                }
            }
        }
        function SelectAll(ctl) {
            var ctls = document.getElementsByName("tdcheck");
            for (i = 0; i <= ctls.length - 1; i++) {
                var chk = ctls[i].firstChild.firstChild;
                if (chk.type == "checkbox" && chk.disabled != true) {
                    chk.checked = ctl.checked;
                    CheckUncheck(chk);
                }
            }
        }
        function SaveInvoiceDate(txtDate) {
            if (event.keyCode == 13) {
                var obj = txtDate.nextSibling.nextSibling; //  nextObject
                obj.focus();
                obj.click();
            }
        }
    </script>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"  >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
    <div>
    <%--<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="../Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress> --%>
    <div style="position:fixed;top:0px;  width:100%;">
        <div class="text headerband" style="height:30px; padding-top:10px; vertical-align:middle; " >AP EXPORT</div>
        <table width="100%" border="1" cellpadding="3" cellspacing="0" style="border-collapse:collapse" bordercolor="#e6e6e6" > 
                                    <tr>
                                        <td style="text-align:right;">Invoice No :</td>
                                        <td style=""><asp:Label runat="server" ID="lbl_InvNo"></asp:Label></td>
                                        <td style="text-align:right;">Vessel :</td>
                                        <td style=""><asp:Label runat="server" ID="lbl_Vessel"></asp:Label></td>
                                        <td style="text-align:right;">Ref No :</td>
                                        <td style=""><asp:Label runat="server" ID="lblRefNo"></asp:Label></td>
                                       <td style="text-align:right;">Invoice Amount :</td>
                                        <td style=""><asp:Label runat="server" ID="lbl_InvAmount"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">Invoice Date :</td>
                                        <td style=""><asp:Label runat="server" ID="lbl_InvDate"></asp:Label></td>
                                        <td style="text-align:right;">Approved Amount :</td>
                                        <td style=""><asp:Label runat="server" ID="lbl_ApprovedAmount"></asp:Label></td>
                                        <td style="text-align:right;">Currency :</td>
                                        <td style=""><asp:Label runat="server" ID="lblCurrency"></asp:Label></td>
                                        <td style="text-align:right;">Status :</td>
                                        <td style=""><asp:Label runat="server" ID="lblStatus"></asp:Label></td>
                                    </tr>
                                     <tr>
                                        <td style="text-align:right;">Supplier :</td>
                                        <td style="" colspan="3"><asp:Label runat="server" ID="lblVendorCode"></asp:Label> - <asp:Label runat="server" ID="lblSupplier"></asp:Label></td>
                                        <td style="text-align:right;">Vessel :</td>
                                        <td style=""><asp:Label runat="server" ID="Label2"></asp:Label></td>
                                        <td style="text-align:right;">Due Date :</td>
                                        <td style=""><asp:Label runat="server" ID="lbl_DueDate"></asp:Label></td>
                                       
                                    </tr>
                                </table>
        <div id="div1" style="background-color:#008AE6; font-size:11px;" class="header" >
          <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width:100%;border-collapse:collapse;">
                        <colgroup> 
                            <col width="60px;" />
                            <col width="80px;" />
                            <col width="150px;" />
                            <col width="115px;" />
                            <col />
                            <col width="85px;" />
                            <col width="100px;" />
                            <col width="120px;" />
                            <col width="280px;" />
                            <col width="17px;" />
                        </colgroup> 
                        <tr class= "headerstylegrid">
                            <td>Vessel</td>
                            <td>Date Created</td>
                            <td>PO No</td>
                            <td>Inv Date</td>
                            <td>Traverse Vendor</td>
                            <td>Trav Curr</td>
                            <td>Exch Rate</td>
                            <td>Trans. Amt</td>
                            <td>Trav GL Code</td>
                            <td></td>
                        </tr>
                        <tr class= "headerstylegrid">
                            <td>
                                <div>
                             <%--       <input type="checkbox" runat="server" id="c1" onclick="SelectAll(this);" /><label for="c1">All</label>--%>
                                </div>
                            </td>
                           <td>
                                <asp:Label ID="lblHeaderText" runat="server" Text="JeID"></asp:Label>
                            </td>
                            <td>
                                Invoice No
                            </td>
                             <td>
                               Receive Date
                            </td>
                             
                            <td>
                                POS Vendor
                            </td>
                            <td>
                                Local Amt
                            </td>
                            <td>
                                Date
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                POS Acct. Code
                            </td>
                            <td>
                            </td>
                        </tr>
                     </table>   
        </div>
      </div>
    <div style="height:200px">&nbsp;</div>
    <%--<asp:UpdatePanel ID="up1" runat="server" >
    <ContentTemplate>--%>
           <asp:Repeater ID="rptApEntriesDetails" runat="server" OnItemDataBound="rptApEntriesDetails_OnItemDataBound">
                <ItemTemplate>
                <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width:100%;border-collapse:collapse;  font-size:11px;"  bordercolor="#e2e2e2">
                <colgroup> 
                <col width="60px;" />
                <col width="80px;" />
                <col width="150px;" />
                <col width="115px;" />
                <col />
                <col width="85px;" />
                <col width="100px;" />
                <col width="120px;" />
                <col width="280px;" />
                <col width="17px;" />
            </colgroup> 
                <tr>
                        <td style="text-align:center;"><%#Eval("ShipID")%></td>
                        <td style="text-align:center;"><%#Eval("DateCreated")%></td>
                        <td><span><%#Eval("BidPoNum")%></span></td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtinvDate" runat="server" MaxLength="10" Text='<%#Eval("BidInvoiceDate") %>' Width="80px"  style="float :left ; text-align:center;" onkeypress="SaveInvoiceDate(this)"></asp:TextBox>
                            <asp:ImageButton ID="imgUpdateInvDate" runat="server" ImageUrl="~/Modules/HRD/Images/approved.png" OnClick="imgUpdateInvDate_OnClick" style="float:right;" ValidationGroup="v1" />
                            <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("BidID") %>' />
                                        
                            </td>
                        <td>
                              <b>  <%#Eval("TravID")%> : </b><%#BindTravVenID(Eval("TravID").ToString())%> 
                        </td>
                        <td style="text-align:center;"  >
                                <span style ="float :left "><%#Eval("vencurr")%>  </span> 
                        </td>
                        <td style="text-align:center;">
                            <%#Eval("ExRate")%>
                        </td>
                        <td style="text-align:center;" class='<%# (Eval("vencurr").ToString()=="US$")?"yellow":""%>' >
                            <div style="width:90%; text-align :right "> 
                                <span style="float :left" >US$</span>
                                <%#Eval("TransUSD")%>
                            </div>
                        </td>
                        <td>
                            <%#Eval("GLCode")%>
                        </td>
                        <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:center;" id="tdcheck" >
                        <asp:CheckBox runat="server" ID="chkSelect" MyDetails='<%# Eval("jeid").ToString() + "`" + Eval("vencurr").ToString()%>' onclick="CheckUncheck(this);" class="" /> 
                        <asp:ImageButton runat="server" OnClientClick="return false;" Visible="false" id="imgError" ImageUrl="~/Modules/HRD/Images/error.gif" ></asp:ImageButton> 
                        <asp:HiddenField runat="server" ID="hfdCheckTrav" Value='<%# Eval("TravVenID").ToString() + "`" + Eval("travid").ToString() + "`" + Eval("BidCurr").ToString() + "`" + Eval("TransUSD").ToString()+ "`" + Eval("ExRate").ToString()+ "`" + Eval("ExDate").ToString()+ "`" + Eval("vencurr").ToString()%>'/> 
                        <span style="color:Red;" title="Batch Id" ><%# Eval("Batch")%></span>
                    </td>
                    <td style="text-align:center;"  >
                        <asp:Label ID="lblJeID" runat="server" Text='<%#Eval("jeid")%>' Visible="false" ></asp:Label>
                        <span style="color:Red;" title="Trans Id" ><%# Eval("TransId")%></span>
                    </td>
                    <td><%#Eval("InvoiceNo")%></td>
                    <td style="text-align:left;"><%#Eval("BidReceivedDate")%></td>
                    <td>
                        <span style="float :left"><%#Eval("SupplierName")%></span>
                        <asp:ImageButton ID="imgPOSVendor" runat="server" CommandArgument='<%#Eval("jeid")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" ToolTip="Update Vendor." OnClick="imgUpdateVendor_OnClick" style="float:right;"  />
                    </td>
                    <td style="text-align:right;"   >
                    <span style ="float :left ; font-weight:bold;"><%#Eval("BidCurr")%></span><%#Eval("ForChangeTotal")%>  
                    </td>
                    <td style="text-align:center;">
                        <%#Eval("ExDate")%>
                    </td>
                    <td style="text-align:center;" class='<%# (Eval("vencurr").ToString()=="S$")?"green":""%>' >
                        <div style="width:90%; text-align :right "> 
                            <span style="float :left">S$</span>
                            <%#Eval("TransSGD")%>
                        </div>
                    </td>
                    <td>
                        <span style="float :left"><%#Eval("AccountNumber")%> - <%#Eval("AccountName")%></span>
                        <asp:ImageButton ID="imgAccCode" runat="server" CommandArgument='<%#Eval("jeid")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" ToolTip="Update Account Code."  OnClick="imgUpdateAccCode_OnClick" style="float:right;" />
                        <asp:HiddenField ID="hfdShipId" runat="server" Value='<%#Eval("ShipId") %>' />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                </table>
                </ItemTemplate>
                </asp:Repeater>
                <div style="position:fixed; bottom:0px; width:100%; background-color:#ffffb3; padding:10px; text-align:right; border-top:solid 1px #c2c2c2;">
                    <asp:Button runat="server" ID="btnPost" Text="Post To Traverse" OnClientClick="setTimeout('DisableButton()', 5);" style="float:left; margin-top:3px;" OnClick="imgExport_OnClick"/>
                    <asp:Label runat="server" ID="lblmsg"></asp:Label>
                </div>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" ></asp:Label> 
    <!-- Section to Update Vendor Box -->    
   <%-- <div style="position:fixed;top:0px;left:0px; height :600px; width:100%; text-align :right " runat="server" id="ModalPopupExtender1" visible="false" >
    <center>
        <div style="position:fixed;top:0px;left:0px; height :600px; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative;width:800px; padding :20px; text-align :center;background : white; z-index:150;top:50px; border:solid 5px #777">--%>
        <div style="position:absolute;top:0px;left:0px; height :550px; width:100%;font-family:Arial;font-size:12px;" runat="server" id="ModalPopupExtender1" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:800px; padding :20px; height:550px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;">
        <div style="float:right " >
        <asp:ImageButton runat="server" ID="btnClose" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose_Click"/> 
        </div>
        <table width="100%">
        <tr>
        <td><b>Supplier Name</b></td>
        </tr>
        <tr>
        <td><asp:TextBox runat="server" ID="txtVendor" OnTextChanged="btnFind_Click" Text="A" Width="99%" AutoPostBack="true" ></asp:TextBox> </td>
        </tr>
        </table>
        <div style="border :solid 1px #4371a5;" >
        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
        <colgroup>
                <col style="width:40px;" />
                <col />
                <col style="width:17px;" />
        </colgroup>
        <tr align="left" class= "headerstylegrid">
            <td>Select</td>
            <td>Select New Vendor</td>
            <td>&nbsp;</td>
        </tr>
        </table>
        </div>
        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 400px ; text-align:center; float :left;border:solid 1px #4371a5">
        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" id="tbl_Vendors">
            <colgroup>
                <col style="width:40px;" />
                <col />
            </colgroup>
                                                                                                                                                                                                                    <asp:Repeater ID="rptVendors" runat="server">
        <ItemTemplate>
                <tr id='tr<%#Eval("SupplierId")%>' class='row'>
                <td><asp:ImageButton CommandArgument='<%#Eval("SupplierId")%>' runat="server" ImageUrl="~/Modules/HRD/Images/approval1.gif" ID="btnNewVendor" OnClick="btnNewVendor_Click" />  
                </td> 
                <td style="text-align :left">
                <em style=" color : red" ><%# Eval("TravId")%>,</em><asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>' ></asp:Label>, <em style=" color : blue" ><%# Eval("SupplierPort")%></em>
                    <br />
                    <asp:Label runat="server" ForeColor="Green" Font-Italic="true" ID="txtEmail" BorderColor="Orange" Text ='<%# Eval("SupplierEmail")%>' ></asp:Label>
                </td>
                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id='tr<%#Eval("SupplierId")%>' class='alternaterow'>
                <td><asp:ImageButton CommandArgument='<%#Eval("SupplierId")%>' runat="server" ImageUrl="~/Modules/HRD/Images/approval1.gif" ID="btnNewVendor" OnClick="btnNewVendor_Click" />  
                </td> 
                <td style="text-align :left">
                <em style=" color : red" ><%# Eval("TravId")%>,</em><asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>, <em style=" color : blue" ><%# Eval("SupplierPort")%></em>
                <br />
                <asp:Label runat="server" ForeColor="Green" Font-Italic="true" ID="txtEmail" BorderColor="Orange" Text ='<%# Eval("SupplierEmail")%>' ></asp:Label>
                </td>
                 <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
        </table>
        </div>
        </div>
    </center> 
    </div>   
    <!-- Section to Update Account Code -->    
    <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;" runat="server" id="dvAccountBox" visible="false" >
    <center>
        
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>

         <div style="position :relative;width:450px; padding :20px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px #777">
         <div style="float:right"><asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose2_Click"/> </div> 
         <center >
         <br />
         <div style="padding-left:13px;" runat="server" visible="false" >
         <fieldset style="width :400px;" >
         <legend><b>Update Account Code</b></legend> 
         <br />
         <b> Select New Account </b> 
         <br /><br />
         <asp:DropDownList runat="server" ID="ddlNewAcc" ValidationGroup="acc" ></asp:DropDownList> 
         <br />
         <asp:Label runat="server" ID="validAccountMess" ForeColor="Red"></asp:Label>  
         <br />
         <asp:ImageButton ImageUrl="~/Modules/HRD/Images/save.jpg" runat="server" ValidationGroup="acc" ID="btnNewAccount" OnClick="btnNewAccount_click" OnClientClick="return confirm('Aure you sure to Update Account?');"/> 
         </fieldset>
         </div>
         <br />
         <fieldset style="width:400px;" >
         <legend><b>Update Currency</b></legend>
         <br /> 
         <b> Select New Currency </b> 
         <br /><br />
         <asp:DropDownList runat="server" ID="ddlCurrency" ValidationGroup="acc1" width="90px"></asp:DropDownList> 
         <br />
         <asp:Label runat="server" ID="validCurrencyMess" ForeColor="Red"></asp:Label>  
         <br />
         <asp:ImageButton ImageUrl="~/Modules/HRD/Images/save.jpg" runat="server" ValidationGroup="acc1" ID="btnNewCurrency" OnClick="btnNewCurrency_click" OnClientClick="return confirm('Aure you sure to Update Currency?');"/> 
         </fieldset>
         </center>
         </div> 
    </center>
    </div> 
    
<%--    </ContentTemplate>
    </asp:UpdatePanel> --%>


    </div>
<script type="text/javascript">
function DisableButton() {
document.getElementById('btnPost').disabled = true;
}
</script>
    </form>
</body>
</html>
