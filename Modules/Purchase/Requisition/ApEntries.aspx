<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApEntries.aspx.cs" Inherits="ApEntries" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/Common.js"></script>
     <script src="../JS/Calender.js" type="text/javascript"></script>
     <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <style type="text/css"> 
        td
        {
         word-break:break-all; 
        }
        .green
        {
        	background-color:#54C571;
        }
        .yellow
        {
        	background-color: #FFF8C6; 
        }
    </style>
    <script type="text/javascript" >
    function CheckUncheck(chk)
    {
    
        //var chkbx=trSel.firstChild.childNodes[1].childNodes[1].firstChild.firstChild.firstChild;
        var trSel=chk.parentNode.parentNode.parentNode.parentNode;
        var cls =trSel.className;
        if(chk==null)
        {return;}
        if(cls!="selectedrow")
        {
            trSel.className="selectedrow";
        }
        else
        {
             trSel.className="";
        }
    }
    function ReSelect(ctl)
    {
        var ctls=document.getElementsByName("tdcheck");
        for(i=0;i<=ctls.length-1;i++)  
        {
            var chk=ctls[i].firstChild.firstChild;
            if (chk.checked)
            {
                CheckUncheck(chk); 
            }
        }
    }
    function SelectAll(ctl)
    {
        var ctls=document.getElementsByName("tdcheck");
        for(i=0;i<=ctls.length-1;i++)  
        {
            var chk=ctls[i].firstChild.firstChild;
            if(chk.type=="checkbox" && chk.disabled!=true)
            {
                chk.checked=ctl.checked;
                CheckUncheck(chk); 
            }
        }
    }
    function SaveInvoiceDate(txtDate)
    {
        if(event.keyCode==13)
        {
            var obj=txtDate.nextSibling.nextSibling; //  nextObject
            obj.focus();
            obj.click();
        }
    }
    </script>
    
<%--</head>
<body>
    <form id="form1" runat="server" defaultbutton="imgSearch" >--%>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div>
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
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <div class="text headerband" style="font-family:Arial, Helvetica, sans-serif;font-size:16px;" >
             AP Entries
            <%-- <asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Modules/HRD/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " CausesValidation="false"/>  --%>
        </div>
        <table width="100%" style="font-family:Arial, Helvetica, sans-serif;font-size:10px;">
            <tr>
                <td>
                    <table width="100%" > 
                        <tr>
                            <td>
                                <%--Search filter table--%>
                                <table width="100%" style="font-weight:bold;" border="0" > 
                                    <tr>
                                        <td style="width:70px;text-align:right;">
                                            Vessel :
                                            </td>
                                        <td>
                                            <td style="width:250px;">
                                            <asp:DropDownList ID="ddlVessel" width="220px" runat="server" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*" ControlToValidate="ddlVessel"></asp:RequiredFieldValidator> 
                                           
                                        </td>
                                            <td style="width:50px;text-align:right;">
                                            PO # :

                                            </td>
                                             <td style="width:50px;">
                                          <asp:TextBox runat="server" ID="txtPRNo" MaxLength="4" CssClass="input_box" Width="40px" style=" text-align :center "  ></asp:TextBox> 
                                            
                                        </td>
                                             <td style="width:60px;text-align:left;">
                                            Status :
                                             
                                        </td>
                                        <td style="width:160px;text-align:left;">
                                            <%--<asp:CheckBox  ID="chkPostedTransaction" runat="server" Text="Posted Transactions" />--%>
                                            <asp:DropDownList ID="ddlPostedUnPosted" runat="server" Width="100px">
                                                <asp:ListItem Text="--ALL--" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="PO" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="NON-PO" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                          
                                        </td>
                                            <td style="width:60px;text-align:left;">
                                            Year : 
                                        </td>
                                        <td style="width:120px;text-align:left;">
                                            <asp:DropDownList ID="ddlYear" runat="server"  AutoPostBack="false"  Width="70px"></asp:DropDownList>
                                           
                                        </td>
                                        <td style="width:60px;text-align:left;">
                                            Month : 
                                        </td>
                                        <td style="width:120px;text-align:left;">
                                             <asp:DropDownList ID="ddlMonth" runat="server" Width="65px">
                                                 <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                            <td>
                                            <asp:Button ID="imgSearch" runat="server" Text="Search" CssClass="btn" style="float:left;" OnClick="imgSearch_OnClick" />
                                            <asp:Button ID="ImageButton1" runat="server" Text="Export" CssClass="btn" style="float:left; margin-left :5px;" OnClick="imgExport_OnClick" OnClientClick="return confirm('Are you sure to Export?.');" Visible="false"/>
                                            <asp:Button ID="ImagePrint" runat="server" Text="Print" CssClass="btn" style="float:left; margin-left :5px;" OnClick="imgPrint_Click"/>
                                            <asp:Label ID="lblNoOfRows"  runat="server" style="font-weight:bold ;float:right;" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            
                <td style=" border : solid 2px Grey; font-size :10px" >
                    
                       <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse; padding-top :3px;">
                           <colgroup> 
                          <%--  <col width="3%;" />--%>
                            <col width="4%;" />
                            <col width="8%;" />
                            <col width="9%;" />
                            <col width="7%;" />
                            <col width="9%;" />
                            <col width="9%" />
                            <col width="10%;" />
                            <col />
                            <col width="15%;" />
                           <%-- <col width="4%;" />--%>
                            <col width="12%;" />
                            <col width="2%;" />
                        </colgroup> 
                        <tr class= "headerstylegrid" style="height:25px;">
                           <%-- <td>
                                 <div>
                                    <input type="checkbox" runat="server" id="c1" onclick="SelectAll(this);" /><br /><label for="c1">All</label>
                                </div>
                            </td>--%>
                            <td>
                                Vessel
                            </td>
                             <td>
                                PO No
                            </td>
                             <td>
                                Inv No
                            </td>
                             <td>
                                Inv Date
                            </td>
                             <td>
                                Inv Amt 
                            </td>
                            <td>
                                Exch Rate (US$)
                            </td>
                            <td>
                                Total Amount (US$)
                            </td>
                             <td>
                                Vendor 
                            </td>
                             <td>
                                Account Code
                            </td>
                             <td>
                                Booking Date
                            </td>
                           <%-- <td>
                                Currency
                            </td>--%>
                            <td>
                            </td>
                        </tr>
                        </table>
                        
                     <div id="divScroll" runat="server"  style="height:375px; font-size :11px ;overflow-y:scroll;  OVERFLOW-X: hidden;background-color :#d7d4d4;" onscroll="SetScrollPos(this)">
                         
                             
                            <asp:Repeater ID="rptApEntriesDetails" runat="server" OnItemDataBound="rptApEntriesDetails_OnItemDataBound">
                            <ItemTemplate>
                            <div style="width :100%;border-bottom : solid 1px White;" >
                            <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;"  >
                            <colgroup>
                          <%--  <col width="3%;" />--%>
                            <col width="4%;" />
                            <col width="8%;" />
                            <col width="9%;" />
                            <col width="7%;" />
                            <col width="9%;" />
                            <col width="9%" />
                            <col width="10%;" />
                            <col />
                            <col width="15%;" />
                           <%-- <col width="4%;" />--%>
                            <col width="12%;" />
                            <col width="1.5%;" />
                            </colgroup> 
                            <tr style="font-size:10px; color:Blue; height :20px; " >
                                    <%-- <td style="text-align:left; " id="tdcheck" >
                                    <asp:CheckBox runat="server" ID="chkSelect" MyDetails='<%# Eval("jeid").ToString() + "`" + Eval("vencurr").ToString()%>' onclick="CheckUncheck(this);" class="" /> --%>
                                  <%--  <asp:ImageButton runat="server" OnClientClick="return false;" Visible="false" id="imgError" ImageUrl="~/Modules/HRD/Images/error.gif" ></asp:ImageButton>
                                  
                                </td>--%> 
                                    <td style="text-align:left;"  >
                                        <%#Eval("ShipID")%>
                                          <asp:HiddenField runat="server" ID="hfdCheckTrav" Value='<%# Eval("TravVenID").ToString() + "`" + Eval("travid").ToString() + "`" + Eval("BidCurr").ToString() + "`" + Eval("TransUSD").ToString()+ "`" + Eval("ExRate").ToString()+ "`" + Eval("ExDate").ToString()+ "`" + Eval("vencurr").ToString()%>'/> 
                                     <span style="color:Red;" title="Batch Id" ><%# Eval("Batch")%></span>
                                         <asp:Label ID="lblJeID" runat="server" Text='<%#Eval("jeid")%>' Visible="false" ></asp:Label>
                                         <asp:HiddenField ID="hdnperClosed" runat="server" Value='<%#Eval("perClosed")%>' ></asp:HiddenField>
                                    </td>
                                  
                                     <td>
                                        <%#Eval("BidPoNum")%>
                                        
                                    </td>
                                    <td>
                                    <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                    </td>
                                <td>
                                    <%#Eval("BidInvoiceDate") %>
                                </td>
                                    <td style="text-align:left;">
                                        
                                         <%#Eval("ForChangeTotal")%>  (<%#Eval("BidCurr")%>)
                                      
                                </td>
                                  <%--  <td style="text-align:center;"  >
                                         <span style ="float :left "><%#Eval("vencurr")%>  </span> 
                                    </td>--%>
                                    <td style="text-align:center;">
                                        <%#Eval("ExRate")%> 
                                       
                                     
                                    </td>
                                   <td style="text-align:right;padding-right:3px;" >
                                            <%#Eval("TransUSD")%>
                                   
                                 
                                   </td>
                                    <td style="text-align:left;margin-left:5px;">
                                      <span style="float :left;text-align:left;padding-left:3px;">
                                               <%#Eval("TravID")%> : <%#Eval("SupplierName")%>
                                         </span>
                                           <asp:ImageButton ID="imgPOSVendor" runat="server" CommandArgument='<%#Eval("jeid")%>' ImageUrl="~/Modules/HRD/Images/editX12.jpg" ToolTip="Update Vendor." OnClick="imgUpdateVendor_OnClick" style="float:right;"  />
                                       
                                    </td>
                                    <td style="text-align:left;margin-left:5px;">
                                    <span style="float :left"><%#Eval("AccountNumber")%> - <%#Eval("AccountName")%></span>
                                    <asp:ImageButton ID="imgAccCode" runat="server" CommandArgument='<%#Eval("jeid")%>' ImageUrl="~/Modules/HRD/Images/editX12.jpg" ToolTip="Update Account Code."  OnClick="imgUpdateAccCode_OnClick" style="float:right;" />
                                    <asp:HiddenField ID="hfdShipId" runat="server" Value='<%#Eval("ShipId") %>' />
                                            
                                </td>
                                  <td style="display: flex"  >
                                      <asp:TextBox ID="txtBookingDate" runat="server" MaxLength="11" Text='<%#Eval("DateCreated") %>' onfocus="showCalendar('',this,this,'','holder1',-205,-88,1)" Width="100px"  style="margin-right:8px; " onkeypress="SaveInvoiceDate(this)"></asp:TextBox>
                                       <%-- <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtBookingDate">
                    </asp:CalendarExtender>--%>
                                        <asp:ImageButton ID="imgUpdateBookingDate" runat="server" ImageUrl="~/Modules/HRD/Images/approved.png" OnClick="imgUpdateBookingDate_OnClick" style="" ValidationGroup="v1" />
                                        <asp:HiddenField ID="hfBidID" runat="server" Value='<%#Eval("BidID") %>' />
                                      &nbsp;</td>
                                    <td>&nbsp;
                                    </td>
                            </tr>
                           
                            </table>
                            </div>
                            </ItemTemplate>
                            </asp:Repeater>
                             
                     </div>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" ></asp:Label> 
    <!-- Section to Update Vendor Box -->    
    <div style="position:absolute;top:0px;left:0px; height :425px; width:100%; text-align :right " runat="server" id="ModalPopupExtender1" visible="false" >
    <%--<center>--%>
        <div style="position:absolute;top:0px;left:0px; height :425px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <center>
        <div style="position :relative;width:506px; height:314px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
        <div style=" float :right " >
        <asp:ImageButton runat="server" ID="btnClose" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose_Click"/> 
</div> 
<div style="float:left;padding: 0px 5px 0px 5px;" class="bluetext" >Supplier Name :</div>
<div style="float:left" ><asp:TextBox runat="server" ID="txtVendor"  Text="A" ></asp:TextBox> </div>
<div style="float:left;padding-left :5px;" >
<asp:ImageButton runat="server" id="btnFind" OnClick="btnFind_Click" ImageUrl="~/Modules/HRD/Images/magnifier.png" AlternateText="Find"/></div>
<table>
<tr>
<td>
<div style="width :500px; border :solid 1px #4371a5;" >
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
<colgroup>
        <col style="width:75px;" />
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
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 500px;HEIGHT: 260px ; text-align:center; float :left;border:solid 1px #4371a5">
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" id="tbl_Vendors">
    <colgroup>
        <col style="width:75px;" />
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
</td>

</tr>
</table>
</div>
            </center>
    <%--</center>--%> 
    </div>   
    <!-- Section to Update Account Code -->    
    <div style="position:absolute;top:0px;left:0px; height :425px; width:100%;" runat="server" id="dvAccountBox" visible="false" >
    <center>
        <div style="position:absolute;top:0px;left:0px; height :425px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:450px; height:260px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
         <div style="float:right"><asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose2_Click"/> </div> 
         <center >
         <br />
         <div style="padding-left:13px;" runat="server"  >
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
        <%-- <fieldset style="width:400px;" >
         <legend><b>Update Currency</b></legend>
         <br /> 
         <b> Select New Currency </b> 
         <br /><br />
         <asp:DropDownList runat="server" ID="ddlCurrency" ValidationGroup="acc1" width="90px"></asp:DropDownList> 
         <br />
         <asp:Label runat="server" ID="validCurrencyMess" ForeColor="Red"></asp:Label>  
         <br />
         <asp:Button Text="Save" runat="server" ValidationGroup="acc1" ID="btnNewCurrency" OnClick="btnNewCurrency_click" OnClientClick="return confirm('Aure you sure to Update Currency?');" CssClass="btn"/> 
         </fieldset>--%>
         </center>
         </div> 
    </center>
    </div> 

        <div style="position:absolute;top:0px;left:0px; height :425px; width:100%;" runat="server" id="dvCurrencyBox" visible="false" >
    <center>
        <div style="position:absolute;top:0px;left:0px; height :425px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:450px; height:260px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
         <div style="float:right"><asp:ImageButton runat="server" ID="ImageButton3" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose2_Click"/> </div> 
         <center >
        
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
         <asp:Button Text="Save" runat="server" ValidationGroup="acc1" ID="btnNewCurrency" OnClick="btnNewCurrency_click" OnClientClick="return confirm('Aure you sure to Update Currency?');" CssClass="btn"/> 
         </fieldset>
         </center>
         </div> 
    </center>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
    </div>
    </asp:Content>
   <%-- </form>
</body>
</html>--%>
