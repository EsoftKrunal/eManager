<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCMLIST.aspx.cs" Inherits="SCMLIST"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>KPI</title>
    <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
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
        font-size:14px;
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
<mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
<%--<asp:UpdatePanel runat="server" id="up1">
<ContentTemplate>--%>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
<%--<td width="100px" style="text-align:right">Category :</td>
<td width="200px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlLFICat" AutoPostBack="true" OnSelectedIndexChanged="Filter_LFI"></asp:DropDownList> 
</td>
<td width="100px" style="text-align:right">From Date :</td>
<td width="120px">
    <asp:TextBox runat="server" id="txtFDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15" AutoPostBack="true" OnTextChanged="Filter_LFI"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td width="100px" style="text-align:right">To Date :</td>
<td width="120px">
    <asp:TextBox runat="server" id="txtTDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15" AutoPostBack="true" OnTextChanged="Filter_LFI"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td style="text-align:right">
      
    </td>--%>
<td style="text-align:right">
      <asp:Button runat="server" ID="btnAddNew" Text="Add New" onclick="btnAddNew_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      <asp:Button runat="server" ID="btnShowImport" Text="Import SCM" onclick="btnShowImport_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
    </td>
</tr>
</table>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White; height:25px;'>
        <td style="width:50px;color:White;text-align:center;"><b>View</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>Edit</b></td>
        <td style="width:100px;color:White;text-align:center;"><b>SCM Date</b></td>
        <td style="width:150px;color:White;"><b>Occasion</b></td>
        <td style="width:300px;text-align:center;color:White;"><b>Voy From / To</b></td>
        <td style="color:White;"><b>Master/ Supdt</b></td>
        <td style="width:30px;color:White;text-align:center;"><b></b></td>
        <td style="width:60px;color:White;text-align:center;"><b>Export</b></td>
        <td style="width:30px;color:White;text-align:center;"><b></b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:390px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_SCM_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptSCMLIST">
<ItemTemplate>
<tr onmouseover="">
      <td style="width:50px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" ToolTip="View" CommandArgument='<%#Eval("ReportsPK").ToString() + "~" + Eval("Ocassion1").ToString()%>' />
      </td>
      <td style="width:50px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/Images/12-em-pencil.png" OnClick="btnEdit_Click" ToolTip="Edit" CommandArgument='<%#Eval("ReportsPK").ToString() + "~" + Eval("Ocassion1").ToString()%>' Visible='<%#Eval("UpdatedBy").ToString() == "" %>' />
        </td>
       <td style="width:100px;text-align:center;">
           <%#Common.ToDateString(Eval("SDate"))%>
      </td>
      <td style="width:150px;text-align:left;"><%#Eval("Ocassion")%></td>
      <td style="width:300px;text-align:left;">
           <%#Eval("ShipPosFrom")%>/ <%#Eval("ShipPosTo")%>
      </td>
      <td style="text-align:left;"><%#Eval("MASTER")%></td>
      <td style="width:30px;text-align:center;">
        <img id="Img1" title="Office Comments received" alt="" src="~/Images/green_circle.gif" runat="server" visible='<%#Eval("UpdatedBy").ToString() != "" %>' />
        <img id="Img2" alt="" src="~/Images/red_circle.png" title="Office Comments not received" runat="server" visible='<%#Eval("UpdatedBy").ToString() == "" %>' />
        </td>
      <td style="width:60px;text-align:center;">
       <asp:ImageButton runat="server" ID="btnExport"  ImageUrl="~/Images/export.gif" OnClick="btnExport_Click" ToolTip="Send for Export" CommandArgument='<%#Eval("ReportsPK")%>' Visible='<%#Eval("UpdatedBy").ToString() == "" %>' />
      </td>
      <td style="width:30px;text-align:center;">
       <asp:ImageButton runat="server" ID="btnPrint"  ImageUrl="~/Images/printer16x16.png" OnClick="btnPrint_Click" ToolTip="Print" CommandArgument='<%#Eval("ReportsPK")%>' />
      </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>

<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvAddNew" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:400px; height:100px;text-align :center;background : white; z-index:150;top:100px; border:solid 8px black;">
            <center >
                <div style="padding:6px; background-color:#7094FF; font-size:14px; color:White;"><b>
                Add New SCM                               
                </b></div>
                                
                <div style="margin:5px">
                Occasion :&nbsp;<asp:DropDownList runat="server" ID="ddlOccasion" >
                                <asp:ListItem Text="SELECT" Value=""></asp:ListItem>
                                <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                                <asp:ListItem Text="NON-Routine" Value="N"></asp:ListItem>
                                <asp:ListItem Text="SUPTD" Value="S"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="req1" runat="server" ForeColor="Red" ErrorMessage="*" ValidationGroup="AN" ControlToValidate="ddlOccasion" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                </div>
                <div style="padding-left:5px;padding-right:5px; text-align:center;">
                    <asp:Button runat="server" ID="btnAddNewSCM" Text="Add" onclick="btnAddNewSCM_Click" ValidationGroup="AN" class="btn" width="100px" OnClientClick="this.value='Processing..';" />   
                </div>
                <div style="text-align:right; position:relative; right:-20px; top:-0px;">
                    <asp:ImageButton runat="server" ID="ImageButton1" Text="Close" onclick="btnClose_Click" ImageUrl="~/Images/close-button.png" CausesValidation="false" title='Close this Window !' OnClientClick="this.value='Processing..';"/>   
                </div>
            </center>
        </div>
    </center>
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

    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_Import" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:120px;padding :5px; text-align :center;background : white; z-index:150;top:100px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>SCM Import</b></div>
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
