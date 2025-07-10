<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Drill_Home"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/HSSQE/mainmene.ascx" tagname="mrvmenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<script src="../eReports/JS/jquery.min.js" type="text/javascript"></script>
<script src="../eReports/JS/KPIScript.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="../eReports/css/jquery.datetimepicker.css"/>
<link rel="stylesheet" type="text/css" href="../eReports/css/stylesheet.css"/>
    <link rel="stylesheet" type="text/css" href="../HSSQE/style.css"/>
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

      function OpenExecute(DTId, RT) {
          window.open('PopupExecuteDrillTraining.aspx?DTId= ' + DTId + '&RT=' + RT, '');
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
.rb
{
border-right:solid 1px grey;
}
.lb
{
border-left:solid 1px grey;
}
.OD
{
    background-color:#FFE0E0;
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
<div style="padding:5px">
    <asp:RadioButton Text="&nbsp;Drill Planner-M" ToolTip="Drill Planner (Monthly)" runat="server" ID="r1" GroupName="tt" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true" />
    <asp:RadioButton Text="&nbsp;Drill Planner-W" ToolTip="Drill Planner (Weekly)" runat="server" ID="r3" GroupName="tt" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true" />
    <asp:RadioButton Text="&nbsp;Drill Update" runat="server" ID="r2" GroupName="tt" Checked="true" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true"/>
</div>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
   <%-- <td style="text-align:right;width:100px;" >Due Between : </td>
    <td style="text-align:left; width:75px;" ><asp:TextBox runat="server" ID="txtFromDt" Width="75px"  MaxLength="15" CssClass="cal"></asp:TextBox></td>
    <td style="width:10px">-</td>
    <td style="text-align:left; width:75px;" class="rb"><asp:TextBox runat="server" ID="txtToDt" Width="75px"  MaxLength="15"  CssClass="cal"></asp:TextBox></td>
    <td style="text-align:center;width:120px;"  class="rb">Due in <asp:TextBox runat="server" ID="txtDays" Width="30px" MaxLength="4" Text="" ></asp:TextBox> Days </td>--%>
    <td style="text-align:center; width:200px;" class="rb"><asp:CheckBox runat="server" ID="rad_T"  Text="&nbsp;Trainings"/>&nbsp;&nbsp;<asp:CheckBox runat="server" ID="rad_D" Text="&nbsp;Drill" /> </td>
    <td>
    <asp:Label runat="server" ID="lblCount"></asp:Label>
    </td>    
    <td style="text-align:right">
      <asp:Button runat="server" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      <asp:Button runat="server" ID="btnClear" Text="Clear" onclick="btnClear_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      <asp:Button runat="server" ID="btnSave" Text="Save Planning" onclick="btnSave_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      <asp:Button runat="server" ID="btnImport" Text="Import" onclick="btnImport_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      <asp:Button runat="server" ID="btnPrint" Text="Print" onclick="btnPrint_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
    </td>
</tr>
</table>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White; height:25px;'>
        <td style="width:50px;color:White;text-align:center;"><b>History</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>Type</b></td>
        <td style="width:150px;color:White;"><b>Group Name</b></td>
        <td style="color:White;"><b>Training / Drill Name</b></td>
         <td style="width:50px;color:White;text-align:center;"><b>Interval</b></td>
        <td style="width:90px;text-align:center;"><b>Guidelines</b></td>
        <td style="width:90px;color:White;text-align:center;"><b>Last Done Dt.</b></td>
        <%--<td style="width:90px;color:White;text-align:center;"><b>NextDue Dt.</b></td>--%>
        <%--<td style="width:90px;color:White;text-align:center;"><b>DueIn (Days)</b></td>--%>
        <td style="width:50px;color:White;text-align:center;"><b>Action</b></td>
        <td style="width:110px;color:White;text-align:center;"><b>Plan Date</b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:362px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_SCM_List001'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptSCMLIST">
<ItemTemplate>
<tr onmouseover="">
      <td style="width:50px;text-align:center;">
      <a href="History.aspx?DTId=<%#Eval("DTId")%>&RT=<%#Eval("RecordType")%>" target="_blank" style="padding:0px">
        <img style="border:none; margin:0px;" src="../Images/export.gif" title="Show History"/>
      </a></td>
      <td style="width:50px;text-align:center;"><%#((Eval("RecordType").ToString()=="D")?"Drill":"Training")%></td>
      <td style="width:150px;text-align:left;"><%#Eval("GroupName")%></td>
      <td style="text-align:left;"><%#Eval("TraininingName")%></td>
      <td style="width:50px;text-align:center;"><%# (Eval("ValidityValue").ToString() + " - " + Eval("ValidityType").ToString())%></td>
      <td style="width:90px;text-align:center;background-color:White;">
          <asp:ImageButton ID="btnShowOGL" runat="server" ImageUrl="~/Modules/PMS/Images/gtk-history.png" CommandArgument='<%#Eval("DTId")%>' RecordType='<%#Eval("RecordType")%>' ToolTip="Office Guide Lines" OnClick="btnShowOGL_Click" style="background-color:transparent"  />
          <asp:ImageButton ID="btnShowAttachment" runat="server" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" CommandArgument='<%#Eval("DTId")%>' RecordType='<%#Eval("RecordType")%>' ToolTip="Attachment" OnClick="btnShowAttachment_Click" Visible='<%#Eval("AttachmentFileName").ToString() != "" %>' style="background-color:transparent"/>
          <img style='height:15px;<%#((Common.CastAsInt32(Eval("COUNT_OP"))>0)?"":"display:none")%>' src="../Images/warning.gif" title="Office Remark Pending" />
      </td>
      <td style="width:90px;text-align:left;"><%#Common.ToDateString(Eval("DoneDate"))%></td>
      <%--<td style="width:90px;text-align:left;" class='<%#Eval("ODSTATUS")%>'><%#Common.ToDateString(Eval("NextDueDate"))%></td>--%>
      <%--<td style="width:90px;text-align:left;"><%#Eval("DueInDays")%></td>--%>
      <td style="width:50px;text-align:center; ">
        <asp:ImageButton ID="btnOpenExecute" CommandArgument='<%#Eval("DTId")%>' RecordType='<%#Eval("RecordType")%>' OnClick="btnOpenExecute_Click" ImageUrl="~/Modules/PMS/Images/gtk-execute.png" runat="server" ToolTip="Execute" Visible='<%#Eval("PlanDate").ToString() != "" %>' style="background-color:transparent"/>
        <%--<asp:ImageButton ID="btnOpenPostpone" CommandArgument='<%#Eval("DTId")%>' RecordType='<%#Eval("RecordType")%>' OnClick="btnOpenPostpone_Click" ImageUrl="~/Modules/PMS/Images/gtk-postpone.png" runat="server" ToolTip="Postpone" Visible='<%#Eval("PlanDate").ToString() != "" %>' style="background-color:transparent"/>--%>
      </td>
      <td style="width:110px;text-align:left;">
      <asp:CheckBox runat="server" ID="chkPlan" DTId='<%#Eval("DTId")%>' RecordType='<%#Eval("RecordType")%>' />
      <asp:TextBox ID="txtPlanDate" runat="server" Text='<%#Common.ToDateString(Eval("PlanDate"))%>' Width="75px" MaxLength="12"  CssClass="cal"></asp:TextBox> </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
<div style="padding:5px; background-color:#FFFFCC; text-align:left;">&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
</div>


<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_Import" runat="server" visible="false">
    <center>
        
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:500px; padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 10px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Import</b></div>
             <div style="width:100%; text-align:left; height:50px;">
             <br />
             <center>
                <asp:FileUpload runat="server" ID="flp_Upload" Width="400px" />
                <asp:RequiredFieldValidator ID="rfv_Import" runat="server" ControlToValidate="flp_Upload" ErrorMessage="Required." ForeColor="Red" Display="Dynamic" ValidationGroup="Im1" SetFocusOnError="true"></asp:RequiredFieldValidator> 
                </center>
             </div>     
             </center>
             <br />
             <div style="padding:5px;">
             <asp:Button runat="server" ID="btnSaveImport" Text="Save" onclick="btnSaveImport_Click" ValidationGroup="Im1" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:100px;"/>
             <asp:Button runat="server" ID="btnCancelImport" Text="Cancel" OnClick="btnCancelImport_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
             </div>
        </div>
    </center>
    </div>

<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_OGL" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:800px; padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b> Office Guide Lines </b></div>
             <div style="width:100%; text-align:left;">
           
             <table cellpadding="3" cellspacing="0" width="100%" border="1" style="border-collapse:collapse">
                <tr>
                    <td><asp:TextBox runat="server" ID="txtShowOGL" TextMode="MultiLine" Height="250px" Width="99%" /></td>
                </tr>
                </table>
             </div>
             </center>
             <div style="padding:3px;">
             <asp:Button runat="server" ID="btnCloseOGL" Text="Close" OnClick="btnCloseOGL_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
             </div>
             
        </div>
    </center>
    </div>

<script type="text/javascript" src="../eReports/JS/jquery.datetimepicker.js"></script>
<script type="text/javascript">
    $('.cal').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
</script>
</form>

</body>
</html>
