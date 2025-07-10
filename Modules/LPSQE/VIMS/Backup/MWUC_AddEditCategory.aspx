<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MWUC_AddEditCategory.aspx.cs" Inherits="VIMS_MWUC_AddEditCategory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#B870FF; color:#ffffff; font-weight:bold; border:solid 1px #00ABE1; height:30px;">
    <tr> 
       <td style="text-align:Center">Add/ Edit MWUC Category</td>
    </tr>
    </table>
    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
    <tr>    
    <td style="text-align:right">
          <asp:Button runat="server" ID="btnAddNewCat" Text="Add New Category" onclick="btnAddNewCat_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:150px;" />
    </td>
    </tr>
    </table>

    <div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; border-bottom:none;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>

<tr style='color:White; height:25px; '>
        <td style="width:70px;text-align:center;color:White;">Edit</td>        
        <td style="color:White;">Category Name</td>
        <td style="width:30px;">&nbsp;</td>
</tr>
</thead>
</table>
</div>
    <div style="height:500px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptCat">
<ItemTemplate>
<tr onmouseover="">
       
      <td style="width:70px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/Images/editX16.png" OnClick="btnEdit_Click" CommandArgument='<%#Eval("CATID") %>'/>
      </td>      
      <td style="text-align:left;"><%#Eval("CATNAME")%></td>      
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>

    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; " id="dv_AddNew" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:600px; height:150px;  padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 8px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Add/ Edit Category</b></div>
                
                <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                <td style="text-align:left; ">
                  <b> Category Name : &nbsp;</b>
                </td>
                </tr>
                <tr>
                <td style="text-align:left;"> 
                 <asp:TextBox runat="server" ID="txtCatName" MaxLength="250" ValidationGroup="vg"  Width="95%" ></asp:TextBox>                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="vg" ErrorMessage="*" ControlToValidate="txtCatName" Display="Dynamic" ></asp:RequiredFieldValidator>
                </td>
                </tr>
                </table>
             </center>
             <br />
             <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
             <br />
             <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" ValidationGroup="vg" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:100px;"/>
             <asp:Button runat="server" ID="btnClose" Text="Cancel" OnClick="btnClose_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
             
        </div>
    </center>
    </div>

    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
