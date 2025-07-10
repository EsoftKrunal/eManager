<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalList.aspx.cs" Inherits="ApprovalList"%>
<%@ Register src="~/Modules/Purchase/UserControls/OrderMenu.ascx" tagname="OrderMenu" tagprefix="uc1" %>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval List</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel = null;
        function Selectrow(trSel, prid) {
            if (lastSel == null) {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel = trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else {
                if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
                {
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel = trSel;
                    document.getElementById('hfPRID').value = prid;
                    
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel = trSel;
                    document.getElementById('hfPRID').value = prid;
                }
            }
        }

    function ViewRFQ(BidId) 
    {
        window.open('RFQDetailsForApproval.aspx?BidId=' + BidId);
    }
    function ReloadPage() 
    {
        document.getElementById("BtnReload").click();
    }
    </script> 
     <script type="text/javascript" >

         function fncInputNumericValuesOnly(evnt) {
             
             if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                 event.returnValue = false;

             }

         }

    </script>
    <link href="CSS/style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>   
    <uc1:OrderMenu ID="OrderMenu1" runat="server" />
        <table width="100%" >
            <tr>
                <td>
                    <div style="border:2px solid #4371a5;" id="tbl_RFQ">
                    <div style="float:right; padding-top :3px;" class="header"><asp:ImageButton runat="server" ID="btnBack" PostBackUrl="~/Search.aspx" ImageUrl="~/Images/home.png" style="float :right; padding-right :5px" /></div>
                    <div class="header" style="width :100%;height:20px; padding-top :5px;"><center>Quote for approval</center>
                    <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
                    </div>
                    <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;text-align:center;">
                    <col  width="300px"/>
                    <col  width="250px"/>
                    <col  width="300px"/>
                    <col  />
                    <col  />
                    <tr >
                        <td><b>Vessel</b></td>
                        <td><b>PR Number</b></td>
                        <td><b>Approved User</b></td>
                        <td><b>Status</b></td>
                        <td></td> 
                    </tr>
                    <tr>
                    <td>
                        <uc1:VSlDropDown ID="ddlVessel" runat="server" IncludeInActive="false" />
                        <asp:CheckBox ID="ChkAllVess" runat="server" AutoPostBack="true" Font-Size="Smaller" oncheckedchanged="ChkAllVess_CheckedChanged" Text="Include Inactive" style="display:none;" />
                        <asp:Button ID="BtnReload" runat="server" OnClick="BtnReload_OnClick" style="display:none;" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRNumber" runat="server" onkeypress='fncInputNumericValuesOnly(event)' MaxLength="5" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSupt" runat="server" Width="200px"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                        <asp:ListItem Text="< All >" Value="0" ></asp:ListItem> 
                        <asp:ListItem Text="Ready for Approval 1" Value="1"></asp:ListItem> 
                        <asp:ListItem Text="Ready for Approval 2" Value="2"></asp:ListItem> 
                        <asp:ListItem Text="Ready for Approval 3" Value="3"></asp:ListItem> 
                        <asp:ListItem Text="Ready for Approval 4" Value="4"></asp:ListItem> 
                        <asp:ListItem Text="Place Order" Value="5"></asp:ListItem> 

                        </asp:DropDownList>
                    </td>
                    <td>
                     <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search.jpg" OnClick="btnSearch_Click" />
                     <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/Images/clear.jpg" OnClick="btnClear_Click" />
                    </td>
                    </tr>
                    </table>
                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT:30px ; text-align:center;">
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="width:40px;" />
                                <col style="width:140px;" />
                                <col />
                                <col style="width:140px;" />
                                <col style="width:100px;" />
                                <col style="width:200px;" />
                                <col style="width:140px;" />
                                <col style="width:80px;" />
                                
                             </colgroup>
                            <tr align="left" class="header">
                                <td style=" height :20px;" >S.No.</td>
                                <td>RFQ #</td>
                                <td>Vendor</td>
                                <td>Location</td>
                                <td>Bid Amt(US$)</td>
                                <td>Status</td>
                                <td>Approved User</td>                                
                                <td>Action</td>
                                
                            </tr>
                        </table>
                        </div>
                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 357px ; text-align:center;">
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:40px;" />
                                    <col style="width:140px;" />
                                    <col />
                                    <col style="width:140px;" />
                                    <col style="width:100px;" />
                                    <col style="width:200px;" />
                                <col style="width:140px;" />
                                    
                                    <col style="width:80px;" />
                                    
                                </colgroup>
                                <asp:Repeater ID="rptRFQList" runat="server">
                                     <ItemTemplate>
                                         <tr id='tr<%#Eval("bidid")%>'  onclick='Selectrow(this,<%#Eval("BidID")%>);' lastclass='row' title="Double click to view the RFQ." ondblclick='ViewRFQ(<%#Eval("bidid")%>)' >
                                            <td>
                                                <%# Eval("Sno")%>
                                                <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("bidid")%>' />
                                            </td> 
                                            <td><%# Eval("RFQNO")%></td> 
                                            <td style=" text-align:left"><asp:Label ID="lblVendor" runat="server" Text='<%# Eval("SUPPLIERNAME") %>' Width="300px" ></asp:Label></td> 
                                            <td style="text-align :left"><%# Eval("SUPPLIERPORT")%></td> 
                                            <td style="text-align :right"><%#ProjectCommon.FormatCurrency(Eval("AMT"))%></td>
                                            <td style="text-align:left;"><%# Eval("BidStatus")%></td>
                                            <td style="text-align:left;"><%# ProjectCommon.getUserNameByID(Eval("UserID").ToString())%></td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <col width="26px" />
                                                    <col width="26px" />
                                                    <col />
                                                    <tr>
                                                        <td>
                                                            <img src="Images/poanalysis.png" alt="" style="border:solid 0px red" title="Quote Analyzer" onclick="window.open('SMDPOAnalyzer.aspx?Prid=<%#Eval("PoID")%>')" />
                                                        </td>
                                                        <td>
                                                            <img src="Images/approved.png" alt="" style="border:solid 0px red " title="Approve/Place Order" onclick='ViewRFQ(<%#Eval("bidid")%>)' />
                                                        </td>
                                                        <td>
                                                            
                                                            <asp:ImageButton ID="imgDelete" runat="server" OnClick="imgDelete_OnClick" ImageUrl="~/Images/reset.png"  title="Send back to purchaser" Visible='<%# (Eval("ApprovalPhase").ToString()=="1") %>' />
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                            
                                        </tr>
                                     </ItemTemplate>
                                     <AlternatingItemTemplate>
                                        <tr id='tr<%#Eval("bidid")%>' class='alternaterow' onclick='Selectrow(this,<%#Eval("BidID")%>);' lastclass='alternaterow' title="Double click to view the RFQ." ondblclick='ViewRFQ(<%#Eval("bidid")%>)'>
                                            <td>
                                                <%# Eval("Sno")%>
                                                <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("bidid")%>' />
                                            </td> 
                                            <td><%# Eval("RFQNO")%></td> 
                                            <td style=" text-align:left"><asp:Label ID="lblVendor" runat="server" Text='<%# Eval("SUPPLIERNAME") %>' Width="300px" ></asp:Label></td> 
                                            <td style="text-align :left"><%# Eval("SUPPLIERPORT")%></td> 
                                            <td style="text-align :right"><%#ProjectCommon.FormatCurrency(Eval("AMT"))%></td>
                                            <td style="text-align:left;"><%# Eval("BidStatus")%></td>
                                            <td style="text-align:left;"><%# ProjectCommon.getUserNameByID(Eval("UserID").ToString())%></td>
                                            <%--<td style="text-align :center"> 
                                                <img src="Images/poanalysis.png" alt="" style="border:solid 0px red" title="Quote Analyzer" onclick="window.open('SMDPOAnalyzer.aspx?Prid=<%#Eval("PoID")%>')" />
                                                <img src="Images/approved.png" alt="" style="border:solid 0px red" title="Approve/Place Order" onclick='ViewRFQ(<%#Eval("bidid")%>)'/>
                                                
                                            </td>--%>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <col width="26px" />
                                                    <col width="26px" />
                                                    <col />
                                                    <tr>
                                                        <td>
                                                            <img src="Images/poanalysis.png" alt="" style="border:solid 0px red" title="Quote Analyzer" onclick="window.open('SMDPOAnalyzer.aspx?Prid=<%#Eval("PoID")%>')" />
                                                        </td>
                                                        <td>
                                                            <img src="Images/approved.png" alt="" style="border:solid 0px red " title="Approve/Place Order" onclick='ViewRFQ(<%#Eval("bidid")%>)' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgDelete" runat="server" OnClick="imgDelete_OnClick" ImageUrl="~/Images/reset.png"   title="Send back to purchaser"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--<asp:ImageButton ID="imgDelete" runat="server" OnClick="imgDelete_OnClick" ImageUrl="~/Images/reset.png"  OnClientClick="return confirm('Are you sure to reset the record ?')" title="Reset"/>--%>
                                            </td>
                                            
                                        </tr>
                                    </AlternatingItemTemplate>
                                 </asp:Repeater>
                            </table>
                        </div>
                        
                        
                        
                        <table cellpadding="1" cellspacing="2" style="text-align:center; border-collapse:collapse;margin:auto;" >
                                    <tr>
                                    <td style="width:150px;float:right;">
                                        <asp:LinkButton ID="lnkPrev20Pages" runat="server" Text='<< Previous 20' OnClick="lnkPrev20Pages_OnClick" Font-Size="11px" style="text-decoration:none;" Visible="false" ></asp:LinkButton>
                                    </td>
                                    <asp:Repeater ID="rptPageNumber" runat="server" >
                                        <ItemTemplate>
                                            <td style="width:10px;"  id='tr<%#Eval("PageNumber")%>' class='<%#(Convert.ToInt32(Eval("PageNumber"))!=PageNo)?"Page":"SelectedPage"%>' onclick='Selectrow(this,<%#Eval("PageNumber")%>);' lastclass='Page' a='<%=PageNo %>' b='<%#Eval("PageNumber") %>'>
                                                <asp:LinkButton ID="lnPageNumber" runat="server" Text='<%#Eval("PageNumber") %>' OnClick="lnPageNumber_OnClick" Font-Size="10px" style="text-decoration:none;" ></asp:LinkButton>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td style="width:150px;">
                                        <asp:LinkButton ID="lnkNext20Pages" runat="server" Text='Next 20 >>' OnClick="lnkNext20Pages_OnClick" Font-Size="11px" style="text-decoration:none;" ></asp:LinkButton>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    </tr>
                                </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                                
                </td>
            </tr>
        </table>
    </div>

        <%------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="DvSendBackToPurchaser" visible="false"  >
                <center>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:450px; height:250px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                    <center>
                        <div style="background-color:#E6F3FC;font-size:16px;font-weight:bold;padding:5px;margin-top:0px;">
                            Send Back To Purchaser
                            <asp:ImageButton ID="btnCloseSendBackToPurchaser" runat="server" ImageUrl="~/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="btnCloseSendBackToPurchaser_OnClick" />
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
    </form>
</body>
</html>
