<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NavigationAudit.aspx.cs" Inherits="NavigationAudit" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>SAFETY & QUALITY</title>
<link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function CheckAll(self) {
            for (i = 0; i <= document.getElementsByTagName("input").length - 1; i++) {
                if (document.getElementsByTagName("input").item(i).getAttribute("type") == "checkbox" && document.getElementsByTagName("input").item(i).getAttribute("id") != self.id) {
                    document.getElementsByTagName("input").item(i).checked = self.checked;
                }
            }
        }
        function UnCheckAll(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
        {
            for (i = 0; i <= document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls").cells.length - 1; i++) {
                if (document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls_" + i).checked == false) {
                    document.getElementById("ctl00_ContentPlaceHolder1_chklst_AllVsl").checked = false;
                }
            }
        }
        function OpenSelectVesselWindow() {
            var LoginId = document.getElementById('ctl00_ContentPlaceHolder1_HiddenField_LoginId').value;
            window.open('..\\FormReporting\\SelectVesselPopUp.aspx?LgnId=' + LoginId, null, 'title=no,toolbars=no,scrollbars=yes,width=680,height=160,left=250,top=180,addressbar=no');
        }
        function OpenKPIWindow() {
            window.open('..\\FormReporting\\EnterKPI.aspx', null, 'title=no,toolbars=no,scrollbars=yes,width=680,height=260,left=250,top=180,addressbar=no');
        }
        var last = "Show";
</script>

<script type="text/javascript">
    var lastno = '';
    function OpenNearMissDetailWindow(vslid) {
        var VesselId = vslid;
        var FromDate = "";
        var ToDate = "";
        if (last == "Show") {
            FromDate = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField_FrmDate").value;
            ToDate = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField_ToDate").value;
        }
        else {
            FromDate = '01-Jan-1900';
            ToDate = '01-Jan-2079';
        }
        window.open('..\\FormReporting\\ViewNearMissDetail.aspx?NearMissVesselId=' + VesselId + '&NMFromDate=' + FromDate + '&NMToDate=' + ToDate, null, 'title=no,toolbars=no,scrollbars=yes,width=1000,height=360,left=150,top=180,addressbar=no');
    }
    function AskDirection(vnano) {
        lastno = vnano;
        OpenPageReport(0);
        //document.getElementById("dvAccountBox").style.display = "";
    }
    function HideAsk() {
        lastno = "";
        document.getElementById("dvAccountBox").style.display = "none";
    }
    
    function OpenPageReport(tp) {
        if (parseFloat(tp) == 0) {
            window.open('EDIT_NavigationAudit.aspx?VNANo=' + lastno, null, '');
        }
//        else {
//            window.open('..\\Reports\\RPT_NavigationAudit.aspx?VNANo=' + lastno, null, 'title=no,toolbars=no,scrollbars=yes,width=1000,height=700,left=150,top=180,addressbar=no');
//        }
        //document.getElementById("dvAccountBox").style.display = "";
    }
</script>
</head>
<body>
<form id="form" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" ></asp:ScriptManager>

    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" >
    <ContentTemplate>

        <table id="filter" width="100%" style ="text-align :center" cellpadding="5" cellspacing="3" style="border:solid 1px #4371a5;" >
        <tr style="background-color :#4371a5;font-weight:bold;">
            <td style="width:10%;color:White; text-align:center">Fleet</td>
            <td style="width:27%;color:White; text-align:center">Vessel</td>
            <td style="width:20%;color:White; text-align:center">Inactive Vessels</td>
            <td style="width:26%;color:White; text-align:center">View Report for</td>
            <td style="width:16%;color:White; text-align:center">
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"  ID="ddlFleet" CssClass="input_box" Width="100%"></asp:DropDownList>  
                <asp:FileUpload id="FileUpload1" runat="server" CssClass="input_box" Width="352px" Visible="false"></asp:FileUpload>
            
            </td>
            <td>
                <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="100%"></asp:DropDownList> 
            </td>
            <td style="text-align:left">
                <asp:CheckBox runat="server" ID="chk_Inactive"  Text="Include Inactive Vessels" AutoPostBack="true" OnCheckedChanged="chk_Inactive_OnCheckedChanged" /> 
            </td>
            <td style="text-align:center">
            Year &nbsp;:
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="input_box"></asp:DropDownList>
            </td>
            <td>
            <asp:Button runat="server" CssClass="input_box" ID="Button1" Text="Show"  Width="60px" onclick="btn_Show_Click"/>
            <asp:Button runat="server" CssClass="input_box" ID="btnClear" Text="Clear" Width="60px" OnClick="btnClear_OnClick"/>   
            </td>
        </tr>
    </table>

        <TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
    <TBODY><TR><TD vAlign=top align=center>
    <TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0><TBODY>
    <TR>

    <TD colspan="3">
           <TABLE style="WIDTH: 100%;" cellSpacing="0"cellPadding="0" ><TBODY>
            <TR>

            <TD  colspan="3" style="padding:2px;">
        <table cellspacing="0" rules="all" border="1" id="ctl00_ContentPlaceHolder1_grd_Data" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="17px" />
            
            <tr class="headerstyle" id="tr1" >
                <th>Vessel Name</th>
                <th style="text-align:center;">Q1</th>
                <th style="text-align:center;">Q2</th>
                <th style="text-align:center;">Q3</th>
                <th style="text-align:center;">Q4</th>
                <th></th>
            </tr>
            </table>
            <div style="height :360px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table1" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                <col />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <asp:Repeater ID="Grd_NearMiss" runat="server" >
                <ItemTemplate>
                    <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                        <td>
                            <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VesselName") %>'></asp:Label>
                            <asp:HiddenField ID="hfd_VesselId" runat="server" Value='<%#Eval("vesselcode") %>' />            
                        </td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskDirection('<%#Eval("Q1")%>')" style='color:<%#Eval("V1")%>'><%#Eval("Q1")%></span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskDirection('<%#Eval("Q2")%>')" style='color:<%#Eval("V2")%>'><%#Eval("Q2")%></span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskDirection('<%#Eval("Q3")%>')" style='color:<%#Eval("V3")%>'><%#Eval("Q3")%></span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskDirection('<%#Eval("Q4")%>')" style='color:<%#Eval("V4 ")%>'><%#Eval("Q4")%></span></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>  
            </div>
        
    </TD>
    </TR>   </TBODY></TABLE>
     </TD>
     </TR>
     </TBODY></TABLE>
    <asp:HiddenField id="HiddenField_LoginId" runat="server"></asp:HiddenField>
    <asp:HiddenField id="HiddenField_FrmDate" runat="server"></asp:HiddenField>
    <asp:HiddenField id="HiddenField_ToDate" runat="server"></asp:HiddenField>
     </TD></TR></TBODY></TABLE>
     
    <!-- Section to Update Account Code -->    
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;display:none" id="dvAccountBox">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :450px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:200px; height:70px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
         <center >
         <img src="../Images/DeleteSquare.png" onclick="HideAsk();"  style="float:right"/>
         <br />
         <button class="btn" onclick="OpenPageReport(0);">Verify Report</button>
         <button class="btn" onclick="OpenPageReport(1);">Print Report</button> 
         </center>
         </div>
    </center>
    </div> 
    
    </ContentTemplate>
    </asp:UpdatePanel>
</form>
</body>
</html>
