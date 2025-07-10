<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewTrainingReport.aspx.cs" Inherits="Reporting_CRMReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:140px 0px 0px 140px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style>
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show">
<div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td valign="top" >
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
             <tr>
                <td align="center" class="text headerband" style="width: 100%; ">Training Requirements & Tracking</td>
             </tr>
             <tr>
                 <td style="text-align: center;"><asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
             </tr>
             <tr>
                 <td style="text-align: center; padding : 3px;">
                 <table width="100%" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                 <td style="text-align: right">Emp No. :</td>
                 <td style="text-align: left">
                     <asp:TextBox ID="txt_MemberId_Search" runat="server" CssClass="input_box" MaxLength="6"
                         TabIndex="1" Width="60px"></asp:TextBox></td>
                 <td style="text-align: right">Crew Name :</td>
                 <td style="text-align: left">
                     <asp:TextBox ID="txtName" runat="server" CssClass="input_box" MaxLength="6" TabIndex="1"
                         Width="160px"></asp:TextBox></td>
                 <td style="text-align: right">Nationality :</td>
                 <td style="text-align: left; width: 187px;">
                     <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="input_box" TabIndex="7"
                         Width="167px">
                         <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                     </asp:DropDownList></td>
                 <td style="text-align: left; ">
                     &nbsp;</td>
                 </tr>
                     <tr>
                         <td style="text-align: right; height: 19px;">
                             Due Date :</td>
                         <td style="text-align: left; height: 19px;">
                             <asp:DropDownList ID="ddlMonth" runat="server" CssClass="input_box" TabIndex="16"
                                 Width="56px">
                                 <asp:ListItem Text="Month" Value="0"></asp:ListItem>
                                 <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                 <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                 <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                 <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                 <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                 <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                 <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                 <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                 <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                 <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:DropDownList ID="ddlYear" runat="server" CssClass="input_box" TabIndex="16" Width="50px">
                             </asp:DropDownList>
                             </td>
                         <td style="text-align: right; height: 19px;">
                             Current
                             Vessel :</td>
                         <td style="text-align: left; height: 19px;">
                             <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" TabIndex="10"
                                 Width="167px">
                                 <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                             </asp:DropDownList></td>
                         <td style="text-align: right; height: 19px;">
                             Training Status :</td>
                         <td style="text-align: left; width: 300px; height: 19px;">
                             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" TabIndex="16"
                                 Width="58px">
                                 <asp:ListItem Text="All" Value ="A"></asp:ListItem>
                                 <asp:ListItem Text="Open" Value ="O"></asp:ListItem>
                                 <asp:ListItem Text="Closed" Value ="C"></asp:ListItem>
                             </asp:DropDownList>
                             &nbsp;
                             <asp:CheckBox ID="chkRec" runat="server" Text="Promotion Recommended" />
                        </td>
                         <td style="text-align: left; width: 100px; height: 19px;">
                        <asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_show_Click" TabIndex="2" Width="96px" /></td>
                     </tr>
                 </table>
                 </td>
             </tr>
             <tr>
                 <td>
                 </td>
             </tr>
           <tr>
           <td style="text-align: left">
           <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:410px; overflow:auto"></iframe>
           </td>
          </tr>
         </table>
        </td>
       </tr>
      </table>
</td> </tr> </table> 
</div>
</form>
</body>
</html>
