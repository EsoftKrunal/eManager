<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Circular.aspx.cs" Inherits="Circular"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/HSSQE/mainmene.ascx" tagname="mrvmenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>KPI</title>
  <link rel="stylesheet" type="text/css" href="../css/Style.css" />
    <link rel="stylesheet" type="text/css" href="../HSSQE/style.css"/>
  <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  <script type="text/javascript">
      function Disable(ctl) {
          $(ctl).attr("value", "Please wait...");
          $(ctl).removeClass("btn"); $(ctl).addClass("btn_disabled");
      }

      function SelectDeSelect() {
          $.each($(".a1"), function (i, e) {
              if ($("#c1").attr("checked") == "checked") {
                  $(e).children().first().attr("checked", true);
              }
              else {
                  $(e).children().first().attr("checked", false);
              }
          });
      }
      function SetValue(ctl) {
          var val = $(ctl).attr('value');
          var stat = $(ctl).attr('class');
          $("#hfd_Key").attr("value", val);
          $("#hfd_Key1").attr("value", stat);
      }

      $(document).ready(function () {
          var val = $("#hfd_Key").attr("value");
          var cls = $("#hfd_Key1").attr("value");
          $("[name=rad1]").filter("[value=" + val + "]").filter("[class=" + cls + "]").attr('checked', true);
      });

      function Validate_Action() {
          var val = $("#hfd_Key").attr("value");
          if (parseInt(val) > 0) {
              return true;
          }
          else {
              alert("Please select a LFI");
              return false;
          }
      }
      function Validate_Delete() {
          var val = $("#hfd_Key").attr("value");
          if (parseInt(val) > 0) {
              return window.confirm('Are you sure to delete?');
          }
          else {
              alert("Please select a LFI");
              return false;
          }
      }
      function OpenProgressWindow(lfiid) {
          window.open('LFI_FC_Progress.aspx?Mode=L&Id=' + lfiid, '');
      }
</script>
  <style type="text/css">
.accordionHeader {
    border: 1px solid #2F4F4F;
    color: white;
    background-color: #2E4d7B;
    font-family: Arial, Sans-Serif;
    font-size: 12px;
    font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
.accordionHeader a {
    color: #FFFFFF;
    background: none;
    text-decoration: none;
}
 
.accordionHeader a:hover {
    background: none;
    text-decoration: underline;
}
 
.accordionHeaderSelected {
    border: 1px solid #2F4F4F;
    color: white;
    background-color: #5078B3;
    font-family: Arial, Sans-Serif;
    font-size: 12px;
    font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
.accordionHeaderSelected a {
    color: #FFFFFF;
    background: none;
    text-decoration: none;
}
 
.accordionHeaderSelected a:hover {
    background: none;
    text-decoration: underline;
}
 
.accordionContent {
    background-color: #D3DEEF;
    border: 1px dashed #2F4F4F;
    border-top: none;
    padding: 5px;
    padding-top: 10px;
} 
</style>
</head>

<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
 <div class="menuframe">                
        <uc1:mrvmenu ID="mrvmenu1" runat="server" />                
    </div>
    <div style="border-bottom:solid 5px #4371a5;"></div>
    <%--------------------------------------------------------------------------------------%>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
<td width="100px" style="text-align:right">From Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtFDate" CssClass="user-input-nopadding" Width="90px" MaxLength="15" AutoPostBack="true" OnTextChanged="Filter_Cir"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td width="100px" style="text-align:right">To Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtTDate" CssClass="user-input-nopadding" Width="90px" MaxLength="15" AutoPostBack="true" OnTextChanged="Filter_Cir"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td width="100px" style="text-align:right">Category :</td>
<td width="100px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="true" Width="90px" OnSelectedIndexChanged="Filter_Cir"></asp:DropDownList> 
</td>
<td width="70px" style="text-align:right">Search :</td>
<td width="150px" style="text-align:left">
    <asp:TextBox ID="txtSearchText" runat="server" CssClass="user-input-nopadding" Width="150" AutoPostBack="true" OnTextChanged="Filter_Cir" ></asp:TextBox>
</td>

<td width="100px" style="text-align:right">Status :</td>
<td width="100px" style="text-align:left">
     <asp:DropDownList ID="ddlStatus" runat="server"  Width="80px" CssClass="user-input-nopadding" AutoPostBack="true" OnSelectedIndexChanged="Filter_Cir" >
        <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
        <asp:ListItem Value="1">Active</asp:ListItem>
        <asp:ListItem Value="2">Inactive</asp:ListItem>
        <asp:ListItem Value="3">In SMS</asp:ListItem>
                                
    </asp:DropDownList>
</td>

<td width="100px" style="text-align:right">Type :</td>
<td width="100px" style="text-align:left">
    <asp:DropDownList ID="ddlType_Search" runat="server" CssClass="user-input-nopadding" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="Filter_Cir" >
        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
        <asp:ListItem Text="Internal" Value="I"></asp:ListItem>
        <asp:ListItem Text="External" Value="E"></asp:ListItem>
    </asp:DropDownList>
</td>
<td style="text-align:right">
      <asp:Button runat="server" ID="btnShowImport" Text="Import Circular" onclick="btnShowImport_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
    </td>
</tr>
</table>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White; height:25px;'>
        <td style="width:70px;text-align:center;color:White;"><b>Download</b></td>
        <td style="width:70px;text-align:center;color:White;"><b>Closure</b></td>
        <td style="width:120px;text-align:center;color:White;"><b>Circular#</b></td>
        <td style="width:100px;color:White;text-align:center;"><b>Circular Date</b></td>
        <td style="width:100px;color:White;text-align:center;"><b>Category</b></td>
        <td style="color:White;"><b>Topic</b></td>        
        <td style="width:120px;color:White;text-align:center;"><b>Send for Export</b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:398px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptCIR">
<ItemTemplate>
<tr class='LFI_<%#Eval("Status")%>' onmouseover="">
       <td style="width:70px;text-align:center;">
        <asp:ImageButton runat="server" ID="btndownload" ImageUrl="~/Images/paperclip.gif" OnClick="btnDownloadFile_Click" CommandArgument='<%#Eval("CId")%>'/>
      </td>
      <td style="width:70px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnClosure" Visible='<%#(Eval("Closure").ToString().Trim()=="")%>' ImageUrl="~/Images/approved.png" OnClick="btnShowClosure_Click" CommandArgument='<%#Eval("CId")%>'/>
        <%#(Eval("Closure").ToString().Trim()=="")?"":"Closed"%>
      </td>
      <td style="width:120px;text-align:center;">
          <%#Eval("CircularNumber")%>
      </td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("CircularDate"))%></td>
      <td style="width:100px;text-align:left;"><%#Eval("CirCatName")%></td>
      <td><%#Eval("Topic")%></td>
      <td style="width:120px;text-align:center;">
      <asp:ImageButton runat="server" ID="btnExport" CssClass='<%#Eval("CircularNumber")%>' Visible='<%#(Eval("Closure").ToString().Trim()!="")%>'  ImageUrl="~/Images/icon_zip.gif" OnClick="btnExport_Click" CommandArgument='<%#Eval("CId")%>'/>
      <%#Common.ToDateString(Eval("Closure"))%>
      </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>

    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_Closure" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:220px;padding :5px; text-align :center;background : white; z-index:150;top:100px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Circular Closure</b></div>
             <div style="width:100%; text-align:left; overflow-y:scroll; overflow-x:hidden; height:150px;">
                

                <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                <td>
                <b> Have you discussed this with all crew members onboard ? 
                <asp:RadioButtonList runat="server" ID="rbtn1" RepeatDirection="Horizontal" ValidationGroup="vg1">
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value=""></asp:ListItem>
                </asp:RadioButtonList> </b>
                </td>
                </tr>
                <tr>
                <td>
                <b> Is it displayed on Noticeboard ? 
                <asp:RadioButtonList runat="server" ID="rbtn2"  RepeatDirection="Horizontal" ValidationGroup="vg1">
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value=""></asp:ListItem>
                </asp:RadioButtonList> </b>
                </td>
                </tr>
                </table>
                <br />
                <asp:Label Text="Please discuss it with all crew members and display it on noticeboard to allow the closure." ForeColor="Red" Font-Size="Large" Font-Bold="true" runat="server" ID="lblmess" Visible="false"></asp:Label>
             </div>
             </center>
             <br />
             <asp:Button runat="server" ID="btnClosureSave" Text="Save" onclick="btnClosureSave_Click" ValidationGroup="vg1" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:100px;"/>
             <asp:Button runat="server" ID="btnClosure" Text="Cancel" OnClick="btnClosureCancel_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
    </center>
    </div>

    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_Import" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:120px;padding :5px; text-align :center;background : white; z-index:150;top:100px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Circular Import</b></div>
             <div style="width:100%; text-align:left; overflow-y:scroll; overflow-x:hidden; height:50px;">
             <br />
             
             <center>
                <asp:FileUpload runat="server" ID="flp_Upload" Width="500px" />
                </center>
             </div>     
             </center>
             <br />
             <asp:Button runat="server" ID="btnSaveImport" Text="Save" onclick="btnSaveImport_Click" ValidationGroup="vg1" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:100px;"/>
             <asp:Button runat="server" ID="btnCancelImport" Text="Cancel" OnClick="btnCancelImport_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
    </center>
    </div>
</form>
</body>
</html>
