<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CrewDocuments.aspx.cs" Inherits="CrewApproval_CrewDocuments" Title="Crew Approval" %>
<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript">

        function RefreshMe1(v, self) {
            document.getElementById('txtMode').value = 1;
            document.getElementById('btn_Hid_Update').click();
        }
        function RefreshMe2(v, self) {
            document.getElementById('txtMode').value = 2;
            document.getElementById('btn_Hid_Update').click();
        }
        function RefreshMe3(v, self) {
            document.getElementById('txtMode').value = 3;
            document.getElementById('btn_Hid_Update').click();
        }
        

        function AddFSWise() {

            var str = document.getElementById("ddl_VW_FS").selectedIndex;
            if (parseFloat(str) <= 0) {
                alert('Plese select Flag State');
                return;
            }
            var v = document.getElementById("ddl_VW_FS").options[str].value
            window.open('AddFSDocuments.aspx?FID=' + v);
        }
        function AddVesselWise() {

            var str = document.getElementById("ddl_VW_Vessel").selectedIndex;
            if (parseFloat(str) <= 0) {
                alert('Plese select Vessel');
                return; 
            }
            var v = document.getElementById("ddl_VW_Vessel").options[str].value
            window.open('AddVesselDocuments.aspx?VID=' + v);
        }
        function AddRankWise() {
            var str = document.getElementById("ddl_RWRankFilter").selectedIndex;
            if (parseFloat(str) <= 0) {
                alert('Plese select Rank');
                return;
            }
            var v = document.getElementById("ddl_RWRankFilter").options[str].value
            window.open('AddRankDocuments.aspx?RID=' + v);
        }
 </script>
</head>
<body style="margin: 5 0 5 0; background-color:White;" >
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div style="text-align: center">
    <table cellpadding="5" cellspacing="0" width="100%" border="0" >
    <%--<tr>
            <td  class="text headerband" >
            <img runat="server" id="img1" moduleid="9" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
            Crew Documents</td>
        </tr>--%>
        </table>
<asp:Button runat="server" ID="btn_Hid_Update" style='display:none' onclick='Refresh' CausesValidation="false"></asp:Button>
<asp:TextBox runat="server" ID="txtMode" style='display:none'></asp:TextBox>
<asp:RadioButtonList runat="server" ID="rbl_Type" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="rbl_Type_OnSelectedIndexChanged" Font-Name="Verdana" Font-Size="14px" CssClass="" > 
<asp:ListItem Text="Rank Wise" Value="0" Selected="True" ></asp:ListItem> 
<asp:ListItem Text="Flag State Wise" Value="1"></asp:ListItem> 
<asp:ListItem Text="Vessel Wise" Value="2"></asp:ListItem> 
</asp:RadioButtonList>
<asp:Panel runat="server" ID="pnlRankWise" Visible="true">
<asp:UpdatePanel runat="server" ID="UpdatePanel2">
<ContentTemplate>
<table cellpadding="5" cellspacing="0" width="100%" border="0" >
<tr>
<td style=" text-align :center; background-color:#d2d2d2"> 
    Select Rank : <asp:DropDownList runat="server" ID="ddl_RWRankFilter" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlRank_OnSelectedIndexChanged"></asp:DropDownList>
    <asp:Button ID="btn_RWAdd" runat="server" CssClass="btn" Text=" Add / Edit " Width="100px" OnClientClick="AddRankWise();" CausesValidation="False" />
</td>
</tr>
<tr>
<td style="text-align: center;">
<table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:14px;  height:30px; background-color:#FFE6B2'>
<colgroup>
    <col width='50px' />
    <col width='200px' />
    <col />
</colgroup>
<tr class= "headerstylegrid">
    <td>Delete</td>
    <td>Document Type</td>
    <td>Document Name</td>
</tr>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:400px; width:100%;  border:solid 1px gray;" >
<table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
<colgroup>
    <col width='50px' />
    <col width='200px' />
    <col />
</colgroup>
<asp:Repeater runat="server" ID="rptData">
<ItemTemplate>
<tr>
    <td><asp:ImageButton ID="btnDel" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick='DeleteSingle_ByRank' CommandArgument='<%#Eval("RankDocumentId")%>' OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"/></td>
    <td><%#Eval("DocumentTypeName")%></td>
    <td style="text-align:left">&nbsp;<%#Eval("VesselDocumentName")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
</div>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<asp:Panel runat="server" ID="pnlFlagStateWise" Visible="false">
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
<ContentTemplate>
<table cellpadding="5" cellspacing="0" width="100%" border="0" >
<tr>
<td style=" text-align :center; background-color:#d2d2d2"> 
    Select Flag State : <asp:DropDownList runat="server" ID="ddl_VW_FS" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlFS_OnSelectedIndexChanged"></asp:DropDownList>
    <%--&nbsp;&nbsp;&nbsp;Select Rank : <asp:DropDownList runat="server" ID="ddl_FS_Rank" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlFSRank_OnSelectedIndexChanged"></asp:DropDownList>--%>
    <asp:Button ID="btnAddEditFS" runat="server" CssClass="btn" Text=" Add / Edit " Width="100px" OnClientClick="AddFSWise();" CausesValidation="False" />
</td>
</tr>
<tr>
<td style="text-align: center;">
<table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:14px;  height:30px; background-color:#FFE6B2'>
<colgroup>
    <col width='50px' />
    <col width='200px' />
    <col width='200px' />
    <col />
</colgroup>
<tr class= "headerstylegrid">
    <td>Delete</td>
    <td>Rank</td>
    <td>Document Type</td>
    <td>Document Name</td>
</tr>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:405px; width:100%;  border:solid 1px gray;" >
<table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
<colgroup>
    <col width='50px' />
    <col width='200px' />
    <col width='200px' />
    <col />
</colgroup>
<asp:Repeater runat="server" ID="rptDataFS">
<ItemTemplate>
<tr>
    <td><asp:ImageButton ID="btnDel" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick='DeleteSingle_ByFS' CommandArgument='<%#Eval("FSDocumentId")%>' OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"/></td>
    <td><%#Eval("rankCode")%></td>
    <td><%#Eval("DocumentTypeName")%></td>
    <td style="text-align:left">&nbsp;<%#Eval("DocumentName")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>

</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
<asp:Panel runat="server" ID="pnlVesselWise" Visible="false">
<asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>

<table cellpadding="5" cellspacing="0" width="100%" border="0" >
<tr>
<td style=" text-align :center; background-color:#d2d2d2"> 
    Select Vessel : <asp:DropDownList runat="server" ID="ddl_VW_Vessel" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged"></asp:DropDownList>
    <%--&nbsp;&nbsp;&nbsp;Select Rank : <asp:DropDownList runat="server" ID="ddl_VW_Rank" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlVWRank_OnSelectedIndexChanged"></asp:DropDownList>--%>
    <asp:Button ID="btnAddEdit2" runat="server" CssClass="btn" Text=" Add / Edit " Width="100px" OnClientClick="AddVesselWise();" CausesValidation="False" />
</td>
</tr>
<tr>
<td style="text-align: center;">
<table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:14px;  height:30px; background-color:#FFE6B2'>
<colgroup>
    <col width='50px' />
    <col width='200px' />
    <col width='200px' />
    <col />
</colgroup>
<tr class= "headerstylegrid">
    <td>Delete</td>
    <td>Rank</td>
    <td>Document Type</td>
    <td>Document Name</td>
</tr>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:405px; width:100%;  border:solid 1px gray;" >
<table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
<colgroup>
    <col width='50px' />
    <col width='200px' />
    <col width='200px' />
    <col />
</colgroup>
<asp:Repeater runat="server" ID="rprData1">
<ItemTemplate>
<tr>
    <td><asp:ImageButton ID="btnDel" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick='DeleteSingle_ByVesel' CommandArgument='<%#Eval("VesselDocumentId")%>' OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"/></td>
    <td><%#Eval("rankCode")%></td>
    <td><%#Eval("VesselDocumentTypeName")%></td>
    <td style="text-align:left">&nbsp;<%#Eval("VesselDocumentName")%></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>

</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
</div>


</form>
</body>
</html>
