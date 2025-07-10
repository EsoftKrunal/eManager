<%@ Page Language="C#" MasterPageFile="~/Modules/LPSQE/Vetting/VettingMasterPage.master" AutoEventWireup="true" CodeFile="FollowUPNoClosed.aspx.cs" Inherits="VIMS_FollowUPNoClosed" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ShipSoft-VIMS</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />

    <link href="VettingStyle.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="VettingScript.js" type="text/javascript"></script>
 
    </head>
<body>
    <form id="form1" runat="server">
--%>

    <style type="text/css">
    .bar
    {
        background-color:LightGreen; 
        height:14px;
    }
    
    .lnkvsl
    {
        cursor:pointer;
    }
     .lnkvsl:hover
    {
        cursor:pointer;
        text-decoration:underline;
    }
    </style>
    <script type="text/javascript">
    </script>

    <div style="text-align: center">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
    <tr>
    <td style="text-align:left; background-color:#C2EBFF; padding:5px ; color:black; ">
        <%--<b>
           <asp:Label  runat="server" ID="lblheader"></asp:Label>
        </b>--%>

        <asp:ImageButton runat="server" style="float:right" ID="btnHome" ImageUrl="~/Images/home.png" PostBackUrl="~/Vetting/VIQHome.aspx" CausesValidation="false"/>

        Select Fleet : <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"  ID="ddlFleet" CssClass="input_box" Width="200px" ></asp:DropDownList>


    </td>
    </tr>
     <tr>
       <td style="vertical-align:top">
        <table cellpadding="0" cellspacing="0" width="100%" border="1">
        <tr>
        <td style="text-align: center; vertical-align:top; width:300px ">
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 2px">
        <tr>
        <td >
            <div style="height:30px; overflow-y:scroll; overflow-x:hidden; width:100%;">
            <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
            <thead>
            <tr>
            <td style="height:29px;"><b>Vessel Name</b></td>
            <td style="width:80px;"><b>Records</b></td>
            <td style="width:30px">&nbsp;</td>
            </tr>
            </table>
            </div>
            <div style=" height:430px; overflow-x:hidden;overflow-y:scroll; width:100%;" class='ScrollAutoReset' id='dv_Vessels0'>
               <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
               <tbody>
               <asp:Repeater runat="server" ID="rpt_Vessels">
               <ItemTemplate>
               <tr style='<%#(Eval("VesselId").ToString().Trim()==SelVessel.Trim())?"background-color:#FF9966;":""%>' > 
                    <td style="text-align:left;">&nbsp;
                    <asp:LinkButton runat="server" ID="lnkVessel" CommandArgument='<%#Eval("VesselId")%>' Text='<%#Eval("VesselName")%>' OnClick="lnk_SelectVessel"></asp:LinkButton>
                    <td style="width:80px;"><%#Eval("NOR")%></td>
                    <td style="width:30px">&nbsp;</td>
               </tr>
               </ItemTemplate>
               </asp:Repeater>
               </tbody>
               </table>
           </div>
        </td>
        </tr>
        </table>
        </td>
        <td style="vertical-align:top">
                <div style="height:30px; overflow-y:scroll; overflow-x:hidden; width:100%;">
                                   <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
                                   <thead>
                                   <tr>
                                   <td style="width:50px; font-weight:bold;height:29px;">SR#</td>
                                   <td style="text-align:left; font-weight:bold;">&nbsp;Deficiency</td>
                                   <td style="width:150px; font-weight:bold;">Source</td>
                                   <td style="width:150px; font-weight:bold;">Target Date</td>
                                   <td style="width:150px; font-weight:bold;">Responsibility</td>
                                   <td style="width:30px">&nbsp;</td>
                                   </tr>
                                   </table>
                                   </div>
                <div style="width:100%; background-color:White;">
                <div style=" height:430px; overflow-x:hidden;overflow-y:scroll; width:100%;" class='ScrollAutoReset' id='dv_Defs1'>
                                   <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
                                   <tbody>
                                   <asp:Repeater runat="server" ID="rpt_Defs" >
                                   <ItemTemplate>
                                   <tr>
                                        <td style="text-align:left; width:50px">&nbsp;<%#Eval("SNO")%></td>
                                        <td style="text-align:left;">&nbsp;<%#Eval("DEFICIENCY")%></td>
                                        <td style="text-align:center;width:150px"><%#Eval("SOURCE")%></td>
                                        <td style="text-align:center;width:150px"><%#Eval("TARGETCLOSEDATE")%></td>
                                        <td style="text-align:center;width:150px">&nbsp;<%#Eval("RESPONSIBILITY")%></td>
                                        <td style="width:30px">&nbsp;</td>
                                   </tr>
                                   </ItemTemplate>
                                   </asp:Repeater>
                                   </tbody>
                                   </table>
                                   </div>
                                   </div>
        </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
   </div>

</asp:Content>
 
   <%-- </form>
</body>
</html>
--%>

