<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeFile="InspectionSearch.aspx.cs" Inherits="InspectionSearch" MasterPageFile="~/MasterPage.master" Title="EMANAGER" %>
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
    
    <script language="JavaScript" type="text/javascript">
        //for all checkboxes
        function UnCheckAllRadioButtons()
        {
            //re = new RegExp(':' + aspRdBtnID + '$')  //generated control name starts with a colon
            for(i = 0; i < document.forms[0].elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'radio')
                {
    //                alert(elm.name + '  re: ' + re);
    //                if (re.test(elm.name)) {
                        elm.checked = false
    //                }
                }
            }
        }
        ////////////////// end
    </script>
    
<script language="javascript" type="text/javascript">
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
</script>
    </asp:Content>
<%--</head><body><form id="form1" runat="server" defaultbutton="btndearch">--%>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:Panel ID="pnl_Search" runat="server">
       
       <div class="text headerband">
           Inspection Record
       </div>
        <br />
    <asp:TextBox Style="display: none" ID="hid" runat="server" />
    <asp:TextBox Style="display: none" ID="hid1" runat="server" Value="Init" />
        <table border="0" style="background-color: #f9f9f9; border: #8fafdb 1px solid; border-top: #8fafdb 0px solid;" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >
                    <table border="1" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                        <td style="width:330px;">
                            <table border="0" cellspacing="1" cellpadding="0" width="330px">
                               <tr>
                                    <td style="text-align: left; padding-left: 5px;">
                                        <asp:CheckBoxList ID="chkallgp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkallgp1"><asp:ListItem>All Groups</asp:ListItem></asp:CheckBoxList>
                                    </td>
                                    <td style="text-align: left; padding-left: 5px;">
                                        <asp:CheckBoxList ID="chkallinsp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chakallinsp"><asp:ListItem>All Inspections</asp:ListItem></asp:CheckBoxList>
                                    </td>
                               </tr>
                               <tr>
                                    <td style="padding-left: 5px; text-align: left;" valign="top">
                                        <div style="overflow-x: hidden; overflow-y: scroll; border: solid 1px #8fafdb; height: 110px;text-align: left;" onscroll="SetScrollPos(this)" id="d1">
                                            <asp:CheckBoxList ID="chkgroup" runat="server" Width="130" AutoPostBack="True" OnSelectedIndexChanged="chkgroup_SelectedIndexChanged"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;" valign="top">
                                        <div style="overflow-x: hidden; overflow-y: scroll; border: solid 1px #8fafdb; height: 110px;width: 200px;" onscroll="SetScrollPos(this)" id="d2">
                                            <asp:CheckBoxList ID="chk_inspection" runat="server" Width="200" AutoPostBack="True" OnSelectedIndexChanged="chk_inspection_SelectedIndexChanged"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                             </table> 
                        </td>
                        <td style="vertical-align :top; padding-left :5px; padding-top :5px;">
                            <table border="0" cellspacing="0" cellpadding="1" >
                            <tr>
                                <td style=" text-align :left">Owner :</td>
                                <td><asp:DropDownList ID="ddl_owner" runat="server" CssClass="input_box" Width="170px" AutoPostBack="True" OnSelectedIndexChanged="ddl_owner_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style=" text-align :left">Vessel :</td>
                                <td><asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" Width="170px"></asp:DropDownList>
                                    <asp:CheckBox runat="server" ID="chk_Inact" Text="Inactive Vessel" 
                                        OnCheckedChanged="chk_Inact_OnCheckedChanged" AutoPostBack="True"/></td>
                            </tr>
                            <tr>
                            <td colspan="2" style=" text-align :center; font-family :10px; font-family :Arial; color :Gray; font-style :italic" >
                            Search Inspection For</td>
                            </tr>
                            <tr>
                            <td colspan="2">
                            <div style=" float :left;" >
                            <asp:RadioButtonList ID="rdbsearch" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdbsearch_SelectedIndexChanged">
                                    <asp:ListItem Value="Crew">Crew#</asp:ListItem>
                                    <asp:ListItem>InspectorName</asp:ListItem>
                                    <asp:ListItem>Chapter</asp:ListItem>
                                    <asp:ListItem Value="Inspection">Inspection#</asp:ListItem>
                                </asp:RadioButtonList>
                            </div> 
                            </td> 
                            </tr>
                            <tr>
                            <td colspan="2">
                            <div style="float :left;padding-top :1px" >
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input_box" Width="320px" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                            </div> 
                            </td>
                            </tr> 
                            <tr>
                            <td style=" text-align :right">Port :</td>
                            <td>
                             <asp:TextBox ID="txtport" runat="server" CssClass="input_box" Width="165px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2" style=" text-align :left" >
                                       
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtduedate"></cc1:FilteredTextBoxExtender>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetPortTitles" ServicePath="WebService.asmx" TargetControlID="txtport"></cc1:AutoCompleteExtender>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value=""></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_StatusColor" runat="server"></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_InspId" runat="server"></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_LoginId" runat="server"></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_InspName" runat="server"></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_Chapter" runat="server"></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_InspNo" runat="server"></asp:HiddenField>
                                        <asp:HiddenField id="HiddenField_CrewNum" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="HiddenField_PortId" runat="server" />
                            </td>
                            </tr>
                            </table> 
                        </td>
                        <td style="vertical-align :top; padding-left :5px; padding-top :5px;">
                            <left>
                            <table border="0" cellspacing="0" cellpadding="0"  height="120" width="500px" >
                            
                            <tr>
                                <td style=" text-align :right;width:15%;padding-right:5px; " >Insp. Status :&nbsp;</td>
                                    <td style=" text-align :left;width:25%;">
                                        
                                 <asp:DropDownList ID="ddl_duestatus" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_duestatus_SelectedIndexChanged" Width="150px">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                    <%--<asp:ListItem Value="Due">Due</asp:ListItem>--%>
                                    <asp:ListItem Value="Executed">In-Progress</asp:ListItem>
                                    <%--<asp:ListItem Value="Fail">Fail</asp:ListItem>--%>
                                    <%--<asp:ListItem>FollowUp</asp:ListItem>--%>
                                    <asp:ListItem>Observation</asp:ListItem>
                                    <%--<asp:ListItem Value="Over Due">Over Due</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="Pass">Pass</asp:ListItem>--%>
                                    <asp:ListItem>Planned</asp:ListItem>
                                    <asp:ListItem>Response</asp:ListItem>
                                </asp:DropDownList>
                                 
                                    </td>
                               
                                    
                                <td colspan="2" style=" text-align :left; ">
                                <div style=" float :left;width:40%;">
                                <asp:RadioButtonList ID="radStatusPF" runat="server" RepeatDirection="Horizontal" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="rdbsearch_SelectedIndexChanged" Visible="false">
                                    <asp:ListItem Value="P">Pass</asp:ListItem>
                                    <asp:ListItem Value="F">Fail</asp:ListItem>
                                </asp:RadioButtonList>
                                </div> 
                                </td> 
                            </tr> 
                                <tr>
                                <td style=" text-align :left;width:40%;" colspan="4">
                                <div style=" float :left;width:165px;" >
                                <asp:CheckBox runat="server" ID="chkOverdue" Text="Over Due"/>
                                <asp:CheckBox runat="server" ID="chkDue" Text="Due" OnCheckedChanged="DueCheck_Changed" AutoPostBack="true"/>
                                </div> 
                                <div style=" float :left; padding-top :3px;" >
                                <asp:TextBox ID="txtduedate" runat="server" CssClass="input_box" Width="40px" MaxLength="3"></asp:TextBox>&nbsp;&nbsp;Days
                                </div>
                            </td>
                                
                            </tr>
                                <tr>
                                <td style=" text-align :right;padding-right:5px;width:15%;"><asp:Label ID="lblFromDt" runat="server" Text="From Dt. :" Visible="false"></asp:Label> &nbsp;

                                </td>
                                    <td style=" text-align :left;width:25%;"><asp:TextBox ID="txt_fromdt" runat="server" CssClass="input_box" Width="75px"></asp:TextBox> &nbsp;
                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_fromdt"></ajaxToolkit:CalendarExtender>
                                       
                                    </td>
                                <td style=" text-align :right;width:15%;padding-right:5px;"><asp:Label ID="lblToDate" runat="server" Text="To Dt. :" ></asp:Label> &nbsp; 
                                </td>
                                <td style=" text-align :left;width:25%;"><asp:TextBox ID="txt_todt" runat="server" CssClass="input_box" Width="75px" AutoPostBack="True" OnTextChanged="txt_todt_TextChanged" ></asp:TextBox>&nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txt_todt"></ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            
                                <tr>
                                    <td width="80%" colspan="4">
                                        <div style=" float :left;">
                                <asp:RadioButtonList ID="radPV" runat="server" RepeatDirection="Horizontal" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="rdbsearch_SelectedIndexChanged">
                                    <asp:ListItem Value="P">Port</asp:ListItem>
                                    <asp:ListItem Value="F">Voyage</asp:ListItem>
                                </asp:RadioButtonList>
                                            </div>
                                    </td>
                                </tr>
                            <tr>
                            <td width="80%" colspan="4">
                            <asp:CompareValidator ID="ddvessel11" runat="server" ControlToCompare="txt_fromdt" ControlToValidate="txt_todt" ErrorMessage="To Date Must Be GreaterThan or Equal To From Date" Operator="GreaterThanEqual" Type="Date" Visible="False"></asp:CompareValidator>
                            &nbsp;
                            </td>
                            </tr>
                            <tr>
                                <td style=" text-align:center" width="80%" colspan="4" >
                                        <asp:Button ID="btndearch" runat="server" CssClass="btn" Text="Search" Width="100px" OnClick="Button3_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px" OnClick="btnClear_Click" OnClientClick="javascript:UnCheckAllRadioButtons();" />
                                     <asp:Button ID="btnmyinspection" runat="server" CssClass="btn" Text="My Inspections"
                                                OnClick="Button1_Click" Width="100px" />
                                            <asp:Button ID="btnNewInsp" CssClass="btn" runat="server" Text="New Inspection" Width="100px"
                                                OnClick="btnNewInsp_Click" />
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print" Width="59px" OnClientClick="return PrintSearchRPT();" />
                                 
                                </td>
                            </tr>
                            </table> 
                            </left> 
                        </td>
                        </tr>
                        <!---------- grid area -->
                        <tr>
                            <td colspan="3" style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <div style="width: 100%; height: 275px; text-align: center;">
                                    <asp:UpdatePanel ID="uplnk" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton id="lnk" onclick="lnk_Click" runat="server" Text=""></asp:LinkButton>
                                            <br />
                                            <asp:GridView style="TEXT-ALIGN: center" id="GrdInspection" runat="server" Width="100%" OnRowCreated="GrdInspection_RowCreated" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" AllowPaging="True" PageSize="9" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorted="GrdInspection_Sorted" OnSorting="GrdInspection_Sorting" OnPreRender="GrdInspection_PreRender" OnRowDataBound="GrdInspection_RowDataBound" HeaderStyle-Wrap="false" OnSelectedIndexChanged="GrdInspection_SelectedIndexChanged">
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <RowStyle CssClass="rowstyle"></RowStyle>
                                            <Columns>
                                                 <asp:CommandField ItemStyle-Width="30px" ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center"></asp:CommandField>
                                                <asp:TemplateField HeaderText="Insp #">
                                                    <ItemTemplate >
                                                        <%--<asp:RadioButton ID="rd_select" OnClick="javascript:CheckOnOff(this.id,'GrdInspection');"
                                                            GroupName="ddd" runat ="server" />--%>
                                                        <asp:Label ID="lblInsId" runat="server" Text='<%# Eval("InspectionNo")%>' ></asp:Label>
                                                        <asp:Label ID="lblid" runat="server" Style="display: none" Text='<%# Eval("InsId")%>'></asp:Label>
                                                        <asp:HiddenField ID="hfdid" runat="server" Value='<%# Eval("Status")%>'></asp:HiddenField>                                              
                                                        <asp:HiddenField id="hfdStatusColor" runat="server" Value='<%# Eval("StatusColor")%>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:TemplateField>
                                               <%-- <asp:BoundField DataField="InspectionNo" HeaderText="Insp #" SortExpression="AA,BB">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="VesselName" HeaderText="Vsl Name" SortExpression="VesselName">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InspName" HeaderText="Insp Name" SortExpression="InspName">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DoneDt" HeaderText="Done Dt." SortExpression="ActualDate">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ActualLocation" HeaderText="Port Done" SortExpression="ActualLocation">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NextDue" HeaderText="Next Due" SortExpression="InspectionValidity">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DueInDays" HeaderText="Due (Days)" SortExpression="DueInDays">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FromPort" HeaderText="From Port" SortExpression="PortName">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ToPort" HeaderText="To Port" SortExpression="PortName">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="PlanDate" HeaderText="Planned Date" SortExpression="Plan_Date">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="Supt" HeaderText="Planned Supt" SortExpression="Supt">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Insp Status" SortExpression="Status">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center"></PagerStyle>
                                            <SelectedRowStyle CssClass="selectedrowstyle"></SelectedRowStyle>
                                            <HeaderStyle CssClass="headerstylefixedheadergrid"  Wrap="false"></HeaderStyle>
                                            </asp:GridView> 
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
                                                        <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
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