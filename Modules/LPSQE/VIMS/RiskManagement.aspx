<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RiskManagement.aspx.cs" Inherits="RiskManagement"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/HSSQE/mainmene.ascx" tagname="mrvmenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>KPI</title>
    <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../HSSQE/style.css"/>
    <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>
  
    <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
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
      function OpenSCMWindow(pk, Mode) {          
          window.open('SCM.aspx?Mode=' + Mode +'&pk=' + pk, '');
      }
      function AddNewSCM(OC) {
          window.open('SCM.aspx?Mode=A&OC=' + OC, '');
      }
</script>
 <style type="text/css">
    body
    {
        margin:0px;
        font-family:Helvetica;
        font-size:13px;
        color:#2E5C8A;
        font-family:Calibri;
    }
  input
  {
      padding:2px 3px 2px 3px;
      border:solid 1px #006B8F;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  textarea
  {
      padding:0px 3px 0px 3px;
      border:solid 1px #006B8F;
      color:black; 
      text-align:left;
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  select
  {
      border:solid 1px #006B8F;
      padding:0px 3px 0px 3px;
      height:22px;
      vertical-align:middle;
      border:none;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  td
  {
      vertical-align:middle;
  }
  .unit
  {
      color:Blue;
      font-size:13px;
      text-transform:uppercase;
  }
  .range
  {
      color:Red;
      font-size:13px;
      text-transform:uppercase;
  }
  .stickyFooter {
     position: fixed;
     bottom: 0px;
     width: 100%;
     overflow: visible;
     z-index: 99;
     padding:5px;
     background: white;
     border-top: solid black 2px;
     -webkit-box-shadow: 0px -5px 15px 0px #bfbfbf;
     box-shadow: 0px -5px 15px 0px #bfbfbf;
     vertical-align:middle;
     background-color:#FFFFCC;
}
.btn
{
     border:none;
    color:#ffffff;
    background-color:#B870FF;
    padding:4px;
    
}
.btn:hover
{
    background-color:#006B8F;
    color:White;
}
.div1
{
 background-color:#006B8F; 
 color:White;
 padding:8px; 
 font-size:14px;
 text-align:center;
 margin-top:5px;
 width:300px;
 text-align:left;
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

<td style="text-align:right">
      <asp:Button runat="server" ID="btnAddNew" Text="Add New RA" onclick="btnAddNew_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      <asp:Button runat="server" ID="btnRefresh" Text="" onclick="btnRefresh_Click" style="display:none;" />
    </td>
</tr>
</table>
<div style="height:28px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #dddddd;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White; height:20px;'>
        <td style="width:30px;"><b>Edit</b></td>
        <td style="width:150px;color:White;text-align:center;"><b>Ref#</b></td>
        <td style="width:80px;color:White;text-align:center;"><b>Event Date</b></td>
        <td style="color:White;text-align:left;"><b>Event Name</b></td>
        <td style="width:250px;color:White;text-align:left;"><b>HOD Name</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>Status</b></td>
        <td style="width:100px;color:White;text-align:center;"><b>Export</b></td>
        <td style="width:20px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:390px; border-bottom:none; border:solid 1px #dddddd; overflow-x:hidden; overflow-y:scroll; border-top:none;" class='ScrollAutoReset' id='dv_Risk_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptRiskList">
<ItemTemplate>
<tr onmouseover="">
      <td style="width:30px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnEdit"  ImageUrl="~/Images/12-em-pencil.png" OnClick="btnEdit_Click" ToolTip="Edit" CommandArgument='<%#Eval("RiskId")%>' />
      </td>
      <td style="width:150px;text-align:center;"><%#Eval("REFNO")%></td>
      <td style="width:80px;text-align:center;"><%#Common.ToDateString(Eval("EVENTDATE"))%></td>
      <td style="text-align:left;"><%#Eval("EVENTNAME")%></td>
      <td style="width:250px;text-align:left;"><%#Eval("HOD_NAME")%></td>
      <td style="width:50px;text-align:center;">
        <img id="Img1" title="Office Comments received" alt="" src="~/Images/green_circle.gif" runat="server" visible='<%#(Common.ToDateString(Eval("OfficeRecdOn"))!="")%>' />
        <img id="Img2" alt="" src="~/Images/red_circle.png" title="Office Comments not received" runat="server" visible='<%#(Common.ToDateString(Eval("OfficeRecdOn"))=="")%>' />
      </td>
      <td style="width:100px;text-align:center;">
      <asp:ImageButton runat="server" ID="btnExport"  ImageUrl="~/Images/export.gif" OnClick="btnExport_Click" ToolTip="Send for Export" CommandArgument='<%#Eval("RiskId")%>' CssClass='<%#Eval("REFNO")%>' />
       <%--<asp:ImageButton runat="server" ID="btnPrint"  ImageUrl="~/Images/printer16x16.png" OnClick="btnPrint_Click" ToolTip="Print" CommandArgument='<%#Eval("ReportsPK")%>' />--%>
      </td>
      <td style="width:20px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>

<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvAddNew" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px; height:408px;text-align :center;background : white; z-index:150;top:30px; border:solid 8px black;">
            <center >
                <div style="padding:6px; background-color:#7094FF; font-size:14px; color:White;"><b>
                Select Risk Event                      
                </b></div>
                <div style='padding:10px 0px 10px  0px; background-color:#99DDF3'>
                <input type="text" style='width:90%; padding:4px;' onkeyup="filter(this);" />
                </div>               
                <div style="height:330px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='Div1'>
                <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                        <col style="width:50px;" />
                        <col />
                    </colgroup>
                    <asp:Repeater ID="rpt_Events" runat="server">
                        <ItemTemplate>
                            <tr class='listitem'>
                                <td style="text-align:center; border:solid 1px #dddddd">
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%#Eval("EventId")%>' OnClick="btnSelect_Click" ImageUrl="~/Images/approved.png" style="background-color:transparent" ToolTip="Select" />
                                </td>
                                <td align="left" class='listkey' style="border:solid 1px #dddddd">&nbsp;<%#Eval("EventName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <div style="text-align:right; position:relative; right:-20px; top:-10px;">
                    <asp:ImageButton runat="server" ID="ImageButton1" Text="Close" style="background-color:transparent" onclick="btnClose_Click" ImageUrl="~/Images/close-button.png" CausesValidation="false" title='Close this Window !' OnClientClick="this.value='Processing..';"/>   
                </div>
            </center>
        </div>
    </center>
    <script type="text/javascript">
        function filter(ctl) {
            var par = $(ctl).val().toLowerCase();
            $(".listitem").each(function (i, o) {
                var txt = $(o).find(".listkey").first().html().toLowerCase();
                if (parseInt(txt.search(par)) >= 0) {
                    $(o).css('display', '');
                }
                else {
                    $(o).css('display', 'none');
                }
            });
        }
     </script>
</div>

<%--<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_Closure" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:220px;padding :5px; text-align :center;background : white; z-index:150;top:100px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>LFI Closure</b></div>
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
    </div>--%>
</form>
</body>
</html>
