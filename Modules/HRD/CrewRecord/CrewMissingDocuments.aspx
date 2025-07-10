<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewMissingDocuments.aspx.cs" Inherits="CrewRecord_CrewMissingDocuments" MasterPageFile="~/MasterPage.master" Title="Crew Missing Documents" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <style type="text/css">
    input
    {
        border:solid 1px grey;
        font-size:12px;
    }
    select
    {
        border:solid 1px grey;
        font-size:12px;
    }
    </style>
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <div style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" >On Board Crew Missing Documents
            <asp:Label runat="server" ID="lblRcount4"></asp:Label>
            </div>
 <div style="height:53px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:25px; background-color:Orange; color:White;">
            <tr class= "headerstylegrid">
            <td style="width:50px; text-align:center; padding:2px 0px 2px 0px;"><asp:TextBox runat="server" ID="txtCrewNo" Width="40px" MaxLength="6" AutoPostBack="true" OnTextChanged="ShowData"></asp:TextBox> </td>
            <td>&nbsp;</td>
            
            <td style="width:80px; text-align:center; padding:2px 0px 2px 0px;"">
            <td style="width:350px; text-align:center; padding:2px 0px 2px 0px;""></td>
            <%--<asp:DropDownList runat="server" ID="ddlRank" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="ShowData"></asp:DropDownList>--%>
            </td>
            <td style="width:80px; text-align:center; padding:2px 0px 2px 0px;""><asp:DropDownList runat="server" ID="ddlVessel" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="ShowData"></asp:DropDownList></td>
            <%--<td style="width:150px; text-align:center; padding:2px 0px 2px 0px;"">--%>
            <%--<asp:DropDownList runat="server" ID="ddlFS" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ShowData"></asp:DropDownList>
            </td>--%>
            <td style="width:20px; text-align:center; padding:2px 0px 2px 0px;"">&nbsp;</td>
            </tr>
               <tr class= "headerstylegrid">
            <td style="width:50px; text-align:center; height:25px;">Crew#</td>
            <td>&nbsp;Crew Name</td>
            <td style="width:80px; text-align:center;">Rank</td>
            <td style="width:350px; text-align:center;">Document Name</td>
            <td style="width:80px; text-align:center;">Vessel</td>
            <%--<td style="width:150px; text-align:center;">Flag State</td>--%>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </table>
            </div>
 <div style="height:500px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;">
            <asp:Repeater runat="server" ID="rpt4">
            <ItemTemplate>
            <tr>
            <td style="width:50px; text-align:center;"><%#Eval("CREWNUMBER")%></td>
            <td style="text-align:left">&nbsp;<%#Eval("CrewName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("RankCode")%></td>
            <td style="width:350px; text-align:left;">&nbsp;<%#Eval("LicenseName")%></td>
            <td style="width:80px; text-align:center;"><%#Eval("VesselCode")%></td>
            <%--<td style="width:150px; text-align:left;">&nbsp;<%#Eval("CountryName")%></td>--%>
            <td style="width:20px; text-align:center;">&nbsp;</td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
</div>
  </asp:Content>
