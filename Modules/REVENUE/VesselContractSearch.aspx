<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeFile="VesselContractSearch.aspx.cs" Inherits="VesselContractSearch" MasterPageFile="~/MasterPage.master" Title="EMANAGER" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">--%>
    <title>EMANAGER</title>
     <%--  <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }
        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }
        function SetLastFocus(ctlid) {
            pos = getCookie(ctlid);
            if (isNaN(pos)) { pos = 0; }
            if (pos > 0) {
                document.getElementById(ctlid).scrollTop = pos;
            }
        }
        function SetScrollPos(ctl) {
            setCookie(ctl.id, ctl.scrollTop, 1);
        }


month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
function checkDate(theField){
  dPart = theField.value.split("-");
  if(dPart.length!=3){
    alert("Enter Date in this format: dd-mmm-yyyy");
    theField.focus();
    return false;
  }
var check=0;
for(i=0;i<month.length;i++){
if(dPart[1].toLowerCase()==month[i].toLowerCase()){
    
     check=1;
      dPart[1]=i;
      break;
    }
  }
  if(check==0)
  {
  alert("Enter Date in this format: dd-mmm-yyyy");
  return false;
  }
  nDate = new Date(dPart[2], dPart[1], dPart[0]);
 // nDate = new Date(dPart[0], dPart[1], dPart[2]);
 
  if(isNaN(nDate) || dPart[2]!=nDate.getFullYear() || dPart[1]!=nDate.getMonth() || dPart[0]!=nDate.getDate()){
    alert("Enter Date in this format: dd-mmm-yyyy");
    theField.select();
    theField.focus();
    return false;
  } else {
    return true;
  }
}
 function trimAll(sString) 
{
while (sString.substring(0,1) == ' ')
{
sString = sString.substring(1, sString.length);
}
while (sString.substring(sString.length-1, sString.length) ==' ')
{
sString = sString.substring(0,sString.length-1);
}
return sString;
}
function checkform()
{
    if (!checkDate(document.getElementById('ctl00_ContentMainMaster_txt_fromdt')))
    return false;
    if (!checkDate(document.getElementById('ctl00_ContentMainMaster_txt_todt')))
    return false;
   
}
 function trimAll(sString) 
{
while (sString.substring(0,1) == ' ')
{
sString = sString.substring(1, sString.length);
}
while (sString.substring(sString.length-1, sString.length) ==' ')
{
sString = sString.substring(0,sString.length-1);
}
return sString;
}
    </script>   
    <script language="javascript" type="text/javascript"> 
        function openAddEditVoyage(ContractId) {   
            window.open('AddEditVoyageDetails.aspx?ContractId=' + ContractId + '', '', '');
        }
    </script>  
    <style type="text/css" >
        .grdPadding {
            padding-right : 5px;
        }
    </style>
   <%--<script language="javascript" type="text/javascript">
    function PrintSearchRPT()
    {
        if (document.getElementById("ctl00_ContentMainMaster_ddl_duestatus").value!='All')
       {
            var stid = document.getElementById("ctl00_ContentMainMaster_ddl_duestatus").value;
       }
       else
       {
           var stid='';
       }
        var frmdt = document.getElementById("ctl00_ContentMainMaster_txt_fromdt").value;
        var todt = document.getElementById("ctl00_ContentMainMaster_txt_todt").value;
        var inspid = document.getElementById("ctl00_ContentMainMaster_HiddenField_InspId").value;
        if (document.getElementById("ctl00_ContentMainMaster_ddl_owner").value!='All')
       {
            var oid = document.getElementById("ctl00_ContentMainMaster_ddl_owner").value;
       }
       else
       {
           var oid='';
       }
        if (document.getElementById("ctl00_ContentMainMaster_ddVessel").value!='All')
       {
            var vid = document.getElementById("ctl00_ContentMainMaster_ddVessel").value;
       }
       else
       {
           var vid='';
       }
        var duedt = document.getElementById("ctl00_ContentMainMaster_txtduedate").value;   
        var logid = document.getElementById("ctl00_ContentMainMaster_HiddenField_LoginId").value;   
        var port = document.getElementById("ctl00_ContentMainMaster_HiddenField_PortId").value;
        var inspname = document.getElementById("ctl00_ContentMainMaster_HiddenField_InspName").value; 
        var chap = document.getElementById("ctl00_ContentMainMaster_HiddenField_Chapter").value; 
        var inspnum = document.getElementById("ctl00_ContentMainMaster_HiddenField_InspNo").value;  
        var crewnum = document.getElementById("ctl00_ContentMainMaster_HiddenField_CrewNum").value;  
        window.open('..\\Inspection\\Reports\\InspSearch_Report.aspx?STATUS=' + stid + '&FRMDT=' + frmdt + '&TDT=' + todt + '&INPID=' + inspid + '&OWNID=' + oid + '&VSLID=' + vid + '&DUEDT=' + duedt + '&LOGID=' + logid + '&PORT=' + port + '&IPNAME=' + inspname + '&CHAP=' + chap + '&IPNO=' + inspnum + '&CRNO=' + crewnum + '',null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
    }
</script>--%>
    </asp:Content>
<%--</head><body><form id="form1" runat="server" defaultbutton="btndearch">--%>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:Panel ID="pnl_Search" runat="server">     
       <div class="text headerband">
           Revenue
       </div>
        <br />
    <asp:TextBox Style="display: none" ID="hid" runat="server" />
    <asp:TextBox Style="display: none" ID="hid1" runat="server" Value="Init" />
        <table border="0" style="background-color: #f9f9f9; border: #8fafdb 1px solid; border-top: #8fafdb 0px solid;" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >
                    <table border="1" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                        <td >
                            <table border="0" cellspacing="1" cellpadding="0" width="100%">
                               <tr>
                                    <td style="text-align: right; padding-right: 5px;width:10%;height:30px;">
                                       Vessel :
                                    </td>
                                    <td style="text-align: left; padding-left: 5px;width:20%;">
                                       <asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" Width="150px"></asp:DropDownList>
                                     <asp:CheckBox runat="server" ID="chk_Inact" Text="Inactive Vessel" 
                                        OnCheckedChanged="chk_Inact_OnCheckedChanged" AutoPostBack="True" Visible="false"/>
                                    </td>
                                     <td style="text-align: right; padding-right: 5px;width:10%;"> Contract Type :</td>
                                <td style="text-align: left; padding-left: 5px;width:20%;"><asp:DropDownList ID="ddlCharter" runat="server" CssClass="input_box" Width="150px" AutoPostBack="True" ></asp:DropDownList></td>
                                <td style="text-align: right; padding-right: 5px;width:10%;">Contract # :</td>
                                <td style="text-align: left; padding-left: 5px;width:20%;"><asp:TextBox ID="txtContractId" runat="server" Width="100px"></asp:TextBox></td>
                                   <td style="text-align: left; padding-left: 5px;width:10%;">

                                   </td>
                               </tr>
                               <tr>
                                    <td style="padding-right: 5px; text-align: right;height:30px;" valign="top">
                                        Voyage #:
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;" valign="top">
                                       <asp:TextBox ID="txtVoyage" runat="server" Width="140px"></asp:TextBox>
                                    </td>
                                   <td style=" text-align :right;padding-right:5px;width:15%;">
                                       Status :
                                </td><td style="text-align: left;padding-left: 5px;">
                                 <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                              <asp:ListItem Text="<ALL>" Value="0"></asp:ListItem> 
                                              <asp:ListItem Text="Open" Selected="True" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="Closed"  Value="2"></asp:ListItem>
                                          </asp:DropDownList>
                                    </td>
                                <td style=" text-align :left;padding-left:5px;" colspan="3"> 
                                    <table>
                                        <tr>
                                            <td>  <asp:Label ID="lblFromDt" runat="server" Text="From Dt. :" ></asp:Label> &nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txt_fromdt" runat="server" CssClass="input_box" Width="75px"></asp:TextBox> &nbsp;
                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_fromdt"></ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
   <asp:Label ID="lblToDate" runat="server" Text="To Dt. :" ></asp:Label> &nbsp; 
                                            </td>
                                            <td>
                                                 <asp:TextBox ID="txt_todt" runat="server" CssClass="input_box" Width="75px" AutoPostBack="True" OnTextChanged="txt_todt_TextChanged" ></asp:TextBox>&nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txt_todt"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                   
                                </td>
                              
                                </tr>
                                <tr>
                                   
                                    <td colspan="7"  style="text-align:right;height:30px;padding:2px 10px 2px 0px;">
                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Search" Width="100px" OnClick="btnsearch_Click" /> &nbsp;
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px" OnClick="btnClear_Click"  /> &nbsp;
                                            <asp:Button ID="btnNewContract" CssClass="btn" runat="server" Text="New Contract" Width="100px"
                                                OnClick="btnNewContract_Click" /> &nbsp;
                                                                    <%--<table border="0" cellspacing="0" cellpadding="0" width="100%">
                            
                            <tr>
                                <td>
                                        
                                                        
                                 
                                </td>
                            </tr>
                            </table> 
                            --%>
                                    </td>
                                </tr>
                             </table> 
                        </td>
                       
                       
                        </tr>
                        <!---------- grid area -->
                        <tr>
                            <td colspan="3" style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <div style="width: 100%; height: 350px; text-align: center;">
                                    <asp:UpdatePanel ID="uplnk" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton id="lnk" onclick="lnk_Click" runat="server" Text=""></asp:LinkButton>
                                              <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                                            <br />
                                            <asp:GridView style="TEXT-ALIGN: center" id="GrdContractRevenue" runat="server" Width="100%" OnRowCreated="GrdContractRevenue_RowCreated" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" AllowPaging="True" PageSize="15" OnPageIndexChanging="GrdContractRevenue_PageIndexChanging" OnSorted="GrdContractRevenue_Sorted" OnSorting="GrdContractRevenue_Sorting" OnPreRender="GrdContractRevenue_PreRender" OnRowDataBound="GrdContractRevenue_RowDataBound" HeaderStyle-Wrap="false" OnSelectedIndexChanged="GrdContractRevenue_SelectedIndexChanged"  OnRowEditing="Row_Editing" >
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <RowStyle CssClass="rowstyle"></RowStyle>
                                            <Columns>
                                                 <asp:CommandField ItemStyle-Width="90px" ButtonType="Image" ShowEditButton="True" EditImageUrl="~/images/editX12.jpg" HeaderText="Edit" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField  HeaderText="Activity">
                                    <ItemStyle Width="50px" HorizontalAlign="Center"  />
                                    <ItemTemplate >
                                    <asp:ImageButton runat="server" ID="lnkbtnAddEditVoyage" OnClick="lnkbtnAddEditVoyage_Click" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"  ToolTip="Activity"/> 
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                                  <asp:BoundField DataField="VesselName" HeaderText="Vessel" SortExpression="VesselName">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Contract #">
                                                    <ItemTemplate >
                                                      
                                                        <asp:Label ID="lblContractId" runat="server" Text='<%# Eval("ContractNo")%>' ></asp:Label>
                                                        <asp:Label ID="lblid" runat="server" Style="display: none" Text='<%# Eval("ContractId")%>'></asp:Label>
                                                        <asp:HiddenField ID="hfdid" runat="server" Value='<%# Eval("Status")%>'></asp:HiddenField>                                              
                                                       <%-- <asp:HiddenField id="hfdStatusColor" runat="server" Value='<%# Eval("StatusColor")%>'></asp:HiddenField>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:TemplateField>

                                               <%-- <asp:BoundField DataField="InspectionNo" HeaderText="Insp #" SortExpression="AA,BB">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>--%>
                                              
                                                <asp:BoundField DataField="ContractType" HeaderText="Contract Type" SortExpression="ContractType">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HireAmount" HeaderText="Contract Amount ($)" SortExpression="HireAmount">
                                                    <ItemStyle HorizontalAlign="Right" Wrap="false" CssClass="grdPadding"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExpectedExpenses" HeaderText="Expected Expenses" SortExpression="ExpectedExpenses">
                                                    <ItemStyle HorizontalAlign="Right" Wrap="false" CssClass="grdPadding"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalExpRevenue" HeaderText="Expected Revenue" SortExpression="TotalExpRevenue">
                                                    <ItemStyle HorizontalAlign="Right" Wrap="false" CssClass="grdPadding"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ActualExpenses" HeaderText="Actual Expenses ($)" SortExpression="ActualExpenses">
                                                    <ItemStyle HorizontalAlign="Right" Wrap="false" CssClass="grdPadding"></ItemStyle>
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="OffhireAmount" HeaderText="Off-hire ($)" SortExpression="ActualExpenses">
                                                    <ItemStyle HorizontalAlign="Right" Wrap="false" CssClass="grdPadding"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalActualRevenue" HeaderText="Actual Revenue ($)" SortExpression="TotalActualRevenue">
                                                    <ItemStyle HorizontalAlign="Right" Wrap="false" CssClass="grdPadding"></ItemStyle>
                                                </asp:BoundField>
                                                
                                                
                                               
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center"></PagerStyle>
                                            <SelectedRowStyle CssClass="selectedrowstyle"></SelectedRowStyle>
                                            <HeaderStyle CssClass="headerstylefixedheadergrid"  Wrap="false"></HeaderStyle>
                                            </asp:GridView> 
                                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <table cellpadding="0" cellspacing="0" style="width: 100%; padding-top: 5px; padding-bottom: 5px"
                                    border="0px">
                                    <%--<tr>
                                        <td style="text-align: left;">
                                            &nbsp;</td>
                                    </tr>--%>
                                    <tr>
                                        <td style="text-align: center; padding-right: 5px; padding-left: 5px;">
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td style="padding-right: 8px; text-align: left">
                                                        <asp:Label ID="Label2" runat="server" Visible="False">Total Records Found: </asp:Label>&nbsp;
                                                        <asp:Label ID="lblrecord" runat="server"></asp:Label>&nbsp;
                                                      </td>
                                                    <td style="text-align: left">
                                                    </td>
                                                    <td style="text-align: right">
                                           </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
        </asp:Content>
 <%--   </form>
</body>
</html>--%>