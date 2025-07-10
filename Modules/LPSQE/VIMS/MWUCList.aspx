<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MWUCList.aspx.cs" Inherits="MWUCList"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>MWUC</title>
  <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>
  
  <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
  
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
 <script type="text/javascript">
     function Refresh() {
         var btnRefresh = document.getElementById("btnRefresh");
         btnRefresh.click(); 
     }
 
 </script>
</head>

<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
<td width="100px" style="text-align:right">From Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtFDate" CssClass="user-input-nopadding" Width="90px" MaxLength="15" ></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td width="100px" style="text-align:right">To Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtTDate" CssClass="user-input-nopadding" Width="90px" MaxLength="15" ></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td style="text-align:right">
      <asp:Button runat="server" ID="btnSearch" Text=" Search " onclick="btnSearch_Click" CssClass="btn" />
      <asp:Button runat="server" ID="btnAddNew" Text="Add New MWUC" OnClientClick="window.open('MWUC_EntryMaster.aspx','');" CssClass="btn" />
      <asp:Button runat="server" ID="btnAddEditCategory" Text="Modify Category" OnClientClick="window.open('MWUC_AddEditCategory.aspx','');"  CssClass="btn" Visible="false" />
      <asp:Button runat="server" ID="btnImport" Text="Import" onclick="btnImport_Click" CssClass="btn" />
      <asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" style="display:none;" runat="server" />
</td>
</tr>
</table>

<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; border-bottom:none;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>

<tr style='color:White; height:25px; '>
        <td style="width:70px;text-align:center;color:White;">View</td>
        <td style="width:100px;color:White;text-align:center;">Date</td>        
        <td style="width:100px;color:White;">Crew#</td>
        <td style="color:White;">Master Name</td> 
        <td style="width:120px;color:White;">Created On</td>
        <td style="width:150px;color:White;">Office Recd. On</td> 
        <td style="width:30px;color:White;"></td>
        <td style="width:40px;color:White;">Export</td>       
        <td style="width:30px;">&nbsp;</td>
</tr>
</thead>
</table>
</div>
<div style="height:360px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptPR">
<ItemTemplate>
<tr onmouseover="">
       
      <td style="width:70px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("TableId")%>'/>
      </td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("FORDATE"))%></td>
      <td style="width:100px;text-align:left;"><%#Eval("MASTERCREWNO")%></td>
      <td style="text-align:left;"><%#Eval("MASTERNAME")%></td>
      <td style="width:120px;text-align:left;"><%#Common.ToDateString(Eval("CREATEDON"))%></td>
      <td style="width:150px;text-align:center;"><%#Common.ToDateString(Eval("OFFICERECDON"))%></td>
      <td style="width:30px;text-align:center;"><img alt="" src="~/Images/green_circle.gif" runat="server" visible='<%#Eval("OFFICERECDON").ToString() != "" %>' /> <img id="Img1" alt="" src="~/Images/red_circle.png" runat="server" visible='<%#Eval("OFFICERECDON").ToString() == "" %>' /> </td>
      <td style="width:40px;text-align:center;"><asp:ImageButton ID="btnExport" ToolTip="Send for Export" CssClass='<%#Eval("MASTERCREWNO") %>'  ImageUrl="~/Images/export.gif" CommandArgument='<%#Eval("TableId")%>' runat="server" OnClick="btnExport_Click" Visible='<%#Eval("OFFICERECDON").ToString() == "" %>' /> </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>

<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvImport" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:400px; height:100px;text-align :center;background : white; z-index:150;top:100px; border:solid 8px black;">
            <center >
                <div style="padding:6px; background-color:#7094FF; font-size:14px; color:White;"><b>
                Import MWUC                                
                </b></div>
                                
                <div style="margin:5px">
                Packet to import :&nbsp;<asp:FileUpload runat="server" ID="flp_Upload" ></asp:FileUpload>
                </div>
                <div style="padding-left:5px;padding-right:5px; text-align:center;">
                    <asp:Label runat="server" ForeColor="Red" ID="lblMsgPOP" style="float:left" Font-Bold="true"></asp:Label>
                    <asp:Button runat="server" ID="btnSaveImport" Text="Import" onclick="btnSaveImport_Click" class="btn" width="100px" OnClientClick="this.value='Processing..';" />   
                </div>
                <div style="text-align:right; position:relative; right:-20px; top:-0px;">
                    <asp:ImageButton runat="server" ID="ImageButton1" Text="Close" onclick="btnClose_Click" ImageUrl="~/Images/close-button.png" CausesValidation="false" title='Close this Window !' OnClientClick="this.value='Processing..';"/>   
                </div>
            </center>
        </div>
    </center>
</div>

    
</form>
</body>
</html>
