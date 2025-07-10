<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MWUC_EntryMaster.aspx.cs" Inherits="MWUC_EntryMaste"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title runat="server">MWUC</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

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
      border:solid 1px #c2c2c2;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  textarea
  {
      padding:0px 3px 0px 3px;
      border:solid 1px #c2c2c2;
      color:black; 
      text-align:left;
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  select
  {
      border:solid 1px #c2c2c2;
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
 text-align:left;
 width:48%;
}
.largetext
{
    word-break: break-all;
    height:100px;
}
.largetext:focus
{
    height:100px;
    background-color:Red;
}
</style>

  <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>
  
  <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>

  <script type="text/javascript">
      function RefreshParent() {          
          window.opener.Refresh();
      }
 
 </script>

</head>
<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<div style="background-color:#006B8F; color:White;padding:8px; font-size:14px;text-align:center;">
    MWUC ENTRY
</div>
<div style="padding-left:50px;padding-right:50px; margin-top:2px;">
<div style="background-color:#ffffff; border:solid 1px #006B8F;">
   <table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td>Date :</td>
            <td><asp:TextBox runat="server" ID="txtRDate" style="width:100px; " MaxLength="10" ></asp:TextBox>
                <asp:CalendarExtender runat="server" id="Cal1" TargetControlID="txtRDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" id="rec1" ControlToValidate="txtRDate" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>Crew# :</td>
            <td><asp:TextBox runat="server" ID="txtCrewNo" style="width:100px; " MaxLength="10" ></asp:TextBox>                
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="txtCrewNo" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>Master Name :</td>
            <td><asp:TextBox runat="server" ID="txtMasterName" MaxLength="50" ></asp:TextBox>                
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" ControlToValidate="txtMasterName" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table cellpadding="4" cellspacing="0" border="0" width="100%" style="border-top:solid 1px #cccccc" >
    <tr>
    <td style='color:Red'>
    Have the deck round taken by management team ? : 
    <asp:DropDownList runat="server" ID="ddlVisit" Width="100px">
        <asp:ListItem Text="" Value=""></asp:ListItem>
        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
        <asp:ListItem Text="No" Value="No"></asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" ControlToValidate="ddlVisit" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
    </tr>
    </table>
    </div>
</div>
<div style="padding-left:50px;padding-right:50px;">
<asp:Repeater runat="server" ID="rptMWUC">
<ItemTemplate>
<div class="div1">
    <asp:Label ID="lblCatName" Text='<%#Eval("CATNAME") %>' CatId='<%#Eval("CATID") %>' runat="server"></asp:Label>
</div>
<div style="background-color:#ffffff; border:solid 1px #006B8F;">
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
    <tr>
        <td style="width:50%;">Ship Comments :
            <asp:TextBox runat="server" Text='<%#Eval("SHIPCOMMENT") %>' TextMode="MultiLine" CssClass='largetext' ID="txtShipComments" Width="98%" ></asp:TextBox>
        </td>
        <td > Office Comments : <asp:Label ID="lblOfficeCommmentByOn" Text='<%#Eval("OFFICECOMMENTBYON") %>' style="color:Blue; font-weight:bold; " runat="server"></asp:Label>
            <%--<asp:TextBox runat="server" Text='<%#Eval("OFFICECOMMENT") %>' ID="txtOfficeComments" TextMode="MultiLine" Width="98%" Height="70px" ></asp:TextBox>--%>
            <div id="dv_OfficeComments" runat="server" class='largetext' style="width:98%; overflow-x:hidden; overflow-y:scroll; border:1px solid #c2c2c2; padding:0px 3px 0px 3px; vertical-align:middle; font-family:Calibri; background-color:#E0F5FF;">
             <%#Eval("OFFICECOMMENT") %>
            </div>
                           
        </td>       
    </tr>
</table>
</div>
</ItemTemplate>
</asp:Repeater>

<%--<div class="div1">
    Charter Party Details 
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td >Charterer Name :</td>
                            <td colspan="3" >
                                <asp:TextBox runat="server" ID="txtChartererName" style="width:98%;"  ></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="width:220px">Charter Party Speed :</td>
                            <td style="width:400px">
                                <asp:TextBox runat="server" ID="txtCharterPartySpeed" style="width:40px;text-align:right;" MaxLength="4" onkeypress="FloatValueOnly(this)" ></asp:TextBox>                                
                                <span class="unit">KTS</span>
                            </td>
                            <td style="width:250px">Voy. Order Speed :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtVoyOrderSpeed" style="width:40px;text-align:right;" MaxLength="4" onkeypress="FloatValueOnly(this)" ></asp:TextBox>
                                <span class="unit">KTS</span>
                            </td>
                        </tr>

                        <tr>
                            <td>Voy. Instructions :</td>
                            <td colspan="3" >
                                <asp:TextBox runat="server" ID="txtVoyInstructions" TextMode="MultiLine" Height="70px" Width="98%"  style="text-align:left;" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
</table>--%>

<br /><br /><br />
</div>

<div class="stickyFooter">
  <div style="text-align:right; width:98%" >
   <asp:Label runat="server" ID="lblMessage" ForeColor="Red" style="float:left" Font-Bold="true" Font-Size="Large"></asp:Label>
     <asp:Button ID="btnSave" CssClass="btn" OnClick="btnSave_Click" Text="Save" Width="120px" runat="server" />
     <asp:Button ID="btnClose" CausesValidation="false" OnClientClick="self.close();" Text="Close" CssClass="btn" Width="120px" runat="server" />
  </div>
</div>


<%--<script type="text/javascript">

    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
        
        $("#txtRobIFO45Recv").keyup(function () {
            $("#txtRobIFO45").val(ConvertToDec($("#hfdRobIFO45").val()) + ConvertToDec($(this).val()));
            $("#hfdRobIFO45_S").val($("#txtRobIFO45").val());
        });

        $("#txtRobIFO1Recv").keyup(function () {
            $("#txtRobIFO1").val(ConvertToDec($("#hfdRobIFO1").val()) + ConvertToDec($(this).val()));
            $("#hfdRobIFO1_S").val($("#txtRobIFO1").val());
        });

        $("#txtRobMGO5Recv").keyup(function () {
            $("#txtRobMGO5").val(ConvertToDec($("#hfdRobMGO5").val()) + ConvertToDec($(this).val()));
            $("#hfdRobMGO5_S").val($("#txtRobMGO5").val());
        });

        $("#txtRobMGO1Recv").keyup(function () {
            $("#txtRobMGO1").val(ConvertToDec($("#hfdRobMGO1").val()) + ConvertToDec($(this).val()));
            $("#hfdRobMGO1_S").val($("#txtRobMGO1").val());
        });

        $("#txtRobMDORecv").keyup(function () {
            $("#txtRobMDO").val(ConvertToDec($("#hfdRobMDO").val()) + ConvertToDec($(this).val()));
            $("#hfdRobMDO_S").val($("#txtRobMDO").val());
        });

        $("#txtRobMECCRecv").keyup(function () {
            $("#txtRobMECC").val(ConvertToDec($("#hfdRobMECC").val()) + ConvertToDec($(this).val()));
            $("#hfdRobMECC_S").val($("#txtRobMECC").val());
        });

        $("#txtRobMECYLRecv").keyup(function () {
            $("#txtRobMECYL").val(ConvertToDec($("#hfdRobMECYL").val()) + ConvertToDec($(this).val()));
            $("#hfdRobMECYL_S").val($("#txtRobMECYL").val());
        });

        $("#txtRobAECCRecv").keyup(function () {
            $("#txtRobAECC").val(ConvertToDec($("#hfdRobAECC").val()) + ConvertToDec($(this).val()));
            $("#hfdRobAECC_S").val($("#txtRobAECC").val());
        });

        $("#txtRobHYDRecv").keyup(function () {
            $("#txtRobHYD").val(ConvertToDec($("#hfdRobHYD").val()) + ConvertToDec($(this).val()));
            $("#hfdRobHYD_S").val($("#txtRobHYD").val());
        });

        $("#txtRobMDOLubeRecv").keyup(function () {
            $("#txtRobMDOLube").val(ConvertToDec($("#hfdRobMDOLube").val()) + ConvertToDec($(this).val()));
            $("#hfdRobMDOLube_S").val($("#txtRobMDOLube").val());
        });

        $("#txtRobFesshWaterRecv").keyup(function () {
            $("#txtRobFesshWater").val(ConvertToDec($("#hfdRobFesshWater").val()) + ConvertToDec($(this).val()));
            $("#hfdRobFesshWater_S").val($("#txtRobFesshWater").val());
        });
    }
    $(document).ready(function () 
    {
        Page_CallAfterRefresh();
    }
    );


    
</script>--%>
</form>
</body>
</html>
