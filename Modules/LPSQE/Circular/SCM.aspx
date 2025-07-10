<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCM.aspx.cs" Inherits="Circular_SCM" Title="SCM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>KPI</title>
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
</head>
<body>
<form id="form" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" ></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" >
    <ContentTemplate>

        <table id="filter" width="100%" style ="text-align :center" cellpadding="5" cellspacing="3" style="border:solid 1px #4371a5;" >
        <tr style="background-color :#4371a5;font-weight:bold;">
            <td style="width:10%;color:White; text-align:center; ">Fleet</td>
            <td style="width:27%;color:White; text-align:center">Vessel</td>
            <td style="width:20%;color:White; text-align:center">Inactive Vessels</td>
            <td style="width:26%;color:White; text-align:center">View Report for</td>
            <td style="width:16%;color:White; text-align:center">
                <a href="SUPTD.htm" style="color:White; text-align:center" title="Right Click then click Save Target As , To download the file." > Download Form For(SUPTD)</a>
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
                <%--<asp:LinkButton ID="lnkGenerateSUPTDForm" runat="server" Text="Generate" OnClick="lnkdownloadSUPTDForm_OnClick"></asp:LinkButton>--%>
            <%--<a id="aSUPTDForm" ></a>--%>
            
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
            <div style="height :25px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                <table cellspacing="0" rules="all" border="1" id="ctl00_ContentPlaceHolder1_grd_Data" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                    <colgroup>
                        <col />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="17px" />
                        <tr ID="tr1" style="font-size:9px; background-color:#006BB2;">
                            <th style="text-align:left; color:White">
                                &nbsp;Vessel Name</th>
                            <th style="text-align:center; color:White">
                                Jan</th>
                            <th style="text-align:center; color:White">
                                Feb</th>
                            <th style="text-align:center; color:White">
                                Mar</th>
                            <th style="text-align:center; color:White">
                                Apr</th>
                            <th style="text-align:center; color:White">
                                May</th>
                            <th style="text-align:center; color:White">
                                Jun</th>
                            <th style="text-align:center; color:White">
                                Jul</th>
                            <th style="text-align:center; color:White">
                                Aug</th>
                            <th style="text-align:center; color:White">
                                Sep</th>
                            <th style="text-align:center; color:White">
                                Oct</th>
                            <th style="text-align:center; color:White">
                                Nov</th>
                            <th style="text-align:center; color:White">
                                Dec</th>
                            <th>
                            </th>
                        </tr>
                    </colgroup>
            </table>
            </div>
            <div style="height :360px; overflow-x:hidden;overflow-y:scroll;width:100%;">
                <table cellspacing="0" cellpadding="1" rules="all" border="1" id="Table1" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                    <colgroup>
                        <col />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="17px" />
                    </colgroup>
            </table>  
                <asp:Repeater ID="Grd_NearMiss" runat="server">
                    <ItemTemplate>
                        <tr onmouseout="this.style.backgroundColor=this.style.historycolor;" 
                            onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" 
                            style="font-size:9px">
                            <td>
                                &nbsp;<asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VesselName") %>'></asp:Label>
                                <asp:HiddenField ID="hfd_VesselId" runat="server" 
                                    Value='<%#Eval("vesselcode") %>' />
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("JanCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("JanID")%>&#039;)'><%#Eval("Jan")%>
                                </span><span class='<%#Eval("JanCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("JanIDSUP")%>&#039;)'>
                                <%#Eval("JanSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("FebCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("FebID")%>&#039;)'><%#Eval("Feb")%>
                                </span><span class='<%#Eval("JanCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("FebIDSUP")%>&#039;)'>
                                <%#Eval("FebSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("MarCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("MarID")%>&#039;)'><%#Eval("Mar")%>
                                </span><span class='<%#Eval("MarCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("MarIDSUP")%>&#039;)'>
                                <%#Eval("MarSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("AprCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("AprID")%>&#039;)'><%#Eval("Apr")%>
                                </span><span class='<%#Eval("AprCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("AprIDSUP")%>&#039;)'>
                                <%#Eval("AprSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("MayCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("MayID")%>&#039;)'><%#Eval("May")%>
                                </span><span class='<%#Eval("MayCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("MayIDSUP")%>&#039;)'>
                                <%#Eval("MaySUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("JunCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("JunID")%>&#039;)'><%#Eval("Jun")%>
                                </span><span class='<%#Eval("JunCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("JunIDSUP")%>&#039;)'>
                                <%#Eval("JunSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("JulCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("JulID")%>&#039;)'><%#Eval("Jul")%>
                                </span><span class='<%#Eval("JulCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("JulIDSUP")%>&#039;)'>
                                <%#Eval("JulSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("AugCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("AugID")%>&#039;)'><%#Eval("Aug")%>
                                </span><span class='<%#Eval("AugCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("AugIDSUP")%>&#039;)'>
                                <%#Eval("AugSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("SepCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("SepID")%>&#039;)'><%#Eval("Sep")%>
                                </span><span class='<%#Eval("SepCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("SepIDSUP")%>&#039;)'>
                                <%#Eval("SepSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("OctCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("OctID")%>&#039;)'><%#Eval("Oct")%>
                                </span><span class='<%#Eval("OctCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("OctIDSUP")%>&#039;)'>
                                <%#Eval("OctSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("NevCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("NevID")%>&#039;)'><%#Eval("Nev")%>
                                </span><span class='<%#Eval("NevCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("NevIDSUP")%>&#039;)'>
                                <%#Eval("NevSUP")%></span>
                            </td>
                            <td style="text-align:center; cursor:pointer;">
                                <span class='<%#Eval("DecCSS")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("DecID")%>&#039;)'><%#Eval("Dec")%>
                                </span><span class='<%#Eval("DecCSSSUP")%>' 
                                    onclick='OpenPageReport(&#039;<%#Eval("DecIDSUP")%>&#039;)'>
                                <%#Eval("DecSUP")%></span>
                            </td>
                            <td>
                                &nbsp;</td>
                            <%--<td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("JanID")%>')"> <%#Eval("Jan")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("FebID")%>')"> <%#Eval("Feb")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("MarID")%>)"> <%#Eval("Mar")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("AprID")%>')"> <%#Eval("Apr")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("MayID")%>')"> <%#Eval("May")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("JunID")%>')"> <%#Eval("Jun")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("JulID")%>')"> <%#Eval("Jul")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("AugID")%>')"> <%#Eval("Aug")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("SepID")%>')"> <%#Eval("Sep")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("OctID")%>')"> <%#Eval("Oct")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("NevID")%>')"> <%#Eval("Nev")%> </span></td>
                        <td style="text-align:center; cursor:pointer;"><span onclick="AskSCM('<%#Eval("DecID")%>')"> <%#Eval("Dec")%> </span></td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <table>
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
