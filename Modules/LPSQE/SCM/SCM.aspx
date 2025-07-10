<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCM.aspx.cs" Inherits="Modules_LPSQE_SCM" Title="SCM"  MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="AuditMenu.ascx" tagname="AuditMenu" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>EMANAGER</title>
     <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
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
    function AskSCM(SCMID) {
        lastno = SCMID;
        document.getElementById("dvAccountBox").style.display = "";
    }
    function HideAsk() {
        lastno = "";
        document.getElementById("dvAccountBox").style.display = "none";
    }
    
    function OpenPageReport(SCMID) {
        //if (parseFloat(tp) == 0) {
            window.open('EDIT_SCM.aspx?SCMID=' + SCMID, null, '');
        //}
//        else {
//            window.open('..\\Reports\\RPT_NavigationAudit.aspx?VNANo=' + lastno, null, 'title=no,toolbars=no,scrollbars=yes,width=1000,height=700,left=150,top=180,addressbar=no');
//        }
//        document.getElementById("dvAccountBox").style.display = "";
    }
</script>
    <style type="text/css">
            .red
            {
            	color:Red;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <div class="text headerband">
        SCM Dashboard
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
    <ProgressTemplate>
    <center>
    <div style='width:100%; height:150px; position:absolute;'>
        <%--<div style="width:150px; background-color:White; height:50px"></div>--%>
            <img src="../../HRD/Images/loading.gif" alt="Loading.." style="top:0px; color:Red; font-weight:bold; " />
    </div>
    </center>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<uc1:AuditMenu ID="AuditMenu1" runat="server" />--%>
    <table width='100%' cellpadding="0" cellspacing="0">
    <tr>
    <td style="vertical-align:top">
    <center>
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" >
    <ContentTemplate>
    <div >
    <asp:RadioButtonList runat="server" id="rad_SCM" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="SCM_OnSelectedIndexChanged">
    <asp:ListItem Text="SCM - SHIP" Value="V" Selected="True"></asp:ListItem>
    <asp:ListItem Text="SCM - SUPERINTENDENT" Value="S"></asp:ListItem>
    </asp:RadioButtonList>
    </div>
        <table id="filter" width="100%" style ="text-align :center;border:solid 1px #4371a5;" cellpadding="5" cellspacing="3" >
        <tr class= "headerstylegrid">
            <td style="width:10%;color:White; text-align:center; ">Fleet</td>
            <td style="width:27%;color:White; text-align:center">Vessel</td>
            <td style="width:20%;color:White; text-align:center">Inactive Vessels</td>
            <td style="width:22%;color:White; text-align:center">View Report for Year</td>
            <td style="width:20%;color:White; text-align:center">
                <%--<a href="SUPTD.htm" style="color:White; text-align:center" title="Right Click then click Save Target As , To download the file." > Download Form For(SUPTD)</a>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"  ID="ddlFleet"  Width="85%"></asp:DropDownList>  
                <asp:FileUpload id="FileUpload1" runat="server" CssClass="input_box" Width="352px" Visible="false"></asp:FileUpload>
            
            </td>
            <td>
                <asp:DropDownList runat="server"  ID="ddlVessel"  Width="85%"></asp:DropDownList> 
            </td>
            <td style="text-align:left">
                <asp:CheckBox runat="server" ID="chk_Inactive"  Text="Include Inactive Vessels" AutoPostBack="true" OnCheckedChanged="chk_Inactive_OnCheckedChanged"  /> 
            </td>
            <td style="text-align:center">
            
            <asp:DropDownList runat="server" ID="ddlYear" Width="85%"></asp:DropDownList>
            </td>
            <td style="text-align:center">
                <%--<asp:LinkButton ID="lnkGenerateSUPTDForm" runat="server" Text="Generate" OnClick="lnkdownloadSUPTDForm_OnClick"></asp:LinkButton>--%>
            <%--<a id="aSUPTDForm" ></a>--%>
            
            <asp:Button runat="server" CssClass="btn" ID="Button1" Text="Show"  Width="60px" onclick="btn_Show_Click"/>
            <asp:Button runat="server" CssClass="btn" ID="btnClear" Text="Clear" Width="60px" OnClick="btnClear_OnClick"/>   
            <asp:Button runat="server" CssClass="btn" ID="btnAdd" Text="Add New" Width="80px" OnClick="btnAdd_OnClick" Visible="false" />   
            <asp:Button runat="server" CssClass="btn" ID="btn_Print" Text="Print Best Practice"  Width="130px" onclick="btn_Print_Click" Visible="false"/>
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
            <div style="height :25px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                <table cellspacing="0" rules="all" cellpadding="1" border="1" id="ctl00_ContentPlaceHolder1_grd_Data" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                    <colgroup>
                        <col />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="17px" />
                        <tr ID="tr1" style="height:25px; " class= "headerstylegrid">
                            <th style="text-align:left; class='c2'">
                                &nbsp;Vessel Name</th>
                            <th style="text-align:center;class='c2' ">
                                Jan</th>
                            <th style="text-align:center;class='c2' ">
                                Feb</th>
                            <th style="text-align:center;class='c2' ">
                                Mar</th>
                            <th style="text-align:center;class='c2' ">
                                Apr</th>
                            <th style="text-align:center;class='c2' ">
                                May</th>
                            <th style="text-align:center;class='c2' ">
                                Jun</th>
                            <th style="text-align:center;class='c2' ">
                                Jul</th>
                            <th style="text-align:center;class='c2' ">
                                Aug</th>
                            <th style="text-align:center;class='c2' ">
                                Sep</th>
                            <th style="text-align:center;class='c2' ">
                                Oct</th>
                            <th style="text-align:center;class='c2' ">
                                Nov</th>
                            <th style="text-align:center;class='c2' ">
                                Dec</th>
                            <th>
                            </th>
                        </tr>
                    </colgroup>
            </table>
            </div>
            <div style="height :240px; overflow-x:hidden;overflow-y:scroll;width:100%; border:solid 1px gray;">
                <table cellspacing="0" cellpadding="1" rules="all" border="1" id="Table1" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                    <colgroup>
                        <col />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="70px" />
                        <col width="17px" />
                    </colgroup>
                <asp:Repeater ID="Grd_NearMiss" runat="server">
                    <ItemTemplate>
                        <tr onmouseout="this.style.backgroundColor=this.style.historycolor;" 
                            onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" 
                            style="font-size:10px;font-family:Arial, Helvetica, sans-serif;">
                            <td>
                                &nbsp;<asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VesselName") %>'></asp:Label>
                                <asp:HiddenField ID="hfd_VesselId" runat="server" 
                                    Value='<%#Eval("vesselcode") %>' />
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Jan"))%><%#WriteContent_S(Eval("JanSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Feb"))%><%#WriteContent_S(Eval("FebSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Mar"))%><%#WriteContent_S(Eval("MarSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Apr"))%><%#WriteContent_S(Eval("AprSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("May"))%><%#WriteContent_S(Eval("MaySUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Jun"))%><%#WriteContent_S(Eval("JunSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Jul"))%><%#WriteContent_S(Eval("JulSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Aug"))%><%#WriteContent_S(Eval("AugSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Sep"))%><%#WriteContent_S(Eval("SepSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Oct"))%><%#WriteContent_S(Eval("OctSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Nov"))%><%#WriteContent_S(Eval("NovSUP"))%>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <%#WriteContent(Eval("Dec"))%><%#WriteContent_S(Eval("DecSUP"))%>
                            </td>
                            <td>
                                &nbsp;</td>
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
    </center>   
    </td>
    </tr>
    </table>
</asp:Content>
