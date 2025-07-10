<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTemplate.aspx.cs" Inherits="HSSQE_RiskManagement_EditTemplate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../IncidentReport/JS/jquery.min.js"></script>
    <title>EMANAGER</title>
    <style type="text/css">
    .btnact
    {
        font-size:11px;
        width:100%;
        border:none;
        cursor:pointer;
    }
    .g
    {background-color:Green;color:White;}
    .b
    {background-color:#A3E0FF;}
    .a
    {background-color:#FFCCCC;}
    .r
    {background-color:red;color:White;}
    
    
    </style>
    <script type="text/javascript">
        function RefreshParent() {
            var btn = window.opener.document.getElementById("btnRefresh");
            btn.click();
        }
    </script>
     <script src="../js/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="../js/AutoComplete/jquery-ui.css" />
     <script src="../js/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <script type="text/javascript">
         function RegisterAutoComplete() 
         {
             $("#txtTaskname").keydown(function () {
                 $("#hfdTaskId").val("");
             });

             $(function () {
                 //------------
                // alert('Hi');
                 $("#txtTaskname").autocomplete(
                 {
                        
                         source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/LPSQE/getAutoCompleteData.ashx", 
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtTaskname").val(), Type: "CONSEQUENCES" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.CNAME, value: item.CNAME, bidid: item.CONSEQUENCESID } }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         $("#hfdTaskId").val(ui.item.bidid);
                         
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
             });
         }
         function getBaseURL() {
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
     </script>


</head>
<body style="font-family:Calibri; font-size:13px;">
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        
    <div style="position:fixed;top:0px; width:100%; height:135px; border-bottom:Solid 5px #47A3FF; background-color:#eeeeee;">
        <div style=" padding:10px; font-size:14px; text-align:center; " class="text headerband">Risk Template Management</div>
        <div style="overflow:none;">
        <div style="float:left; width:80%;">
        <table cellspacing="0" rules="none" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
        <tr>
        <td style="text-align:right;font-weight:bold;width:120px;">Template Code :&nbsp;</td>
        <td style="text-align:left;"><asp:Label runat="server" ID="lblTempCode" ></asp:Label></td>
        </tr>
        <tr>
        <td style="text-align:right; font-weight:bold; width:120px;">Event Name :&nbsp;</td>
        <td style="text-align:left;"><asp:Label runat="server" ID="lblEventName" ></asp:Label>
        </td>
        </tr>
        </table>
        </div>
        <div style="width:20%; text-align:right;margin-left:80%;">
            <div style="margin-right:5px; padding-top:5px;">
               <%-- <asp:Button runat="server" ID="btnAddTask" OnClick="btnAddTask_Click" Text="Add Risk Mitigation Steps" CssClass="btn" Width="130px" />--%>

                 <asp:Button runat="server" ID="btnAddHazard"  Text="Add Hazard" CssClass="btn" Width="130px" OnClick="btnAddHazard_Click" />
            </div>
        </div>
        </div>
        <%--<table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse; height:25px;">
                <tr style="font-weight:bold; background-color:#dddddd;">
                    <td style="text-align:left; vertical-align:middle;width:300px;">Task Name</td>
                    <td style="text-align:left; vertical-align:middle;width:300px;">Hazard Name</td>
                    <td style="text-align:left; vertical-align:middle;">Existing Control Measures</td>
                    <td style="text-align:center;width:80px;">Severity</td>
                    <td style="text-align:center;width:80px;">LikeliHood</td>                                                        
                    <td style="text-align:center;width:80px;">Risk Level</td>
                    <td style="text-align:center;width:60px;">Open</td>
                </tr>
        </table>--%>
        <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse; height:50px;">
                <tr style="font-weight:bold; background-color:#dddddd;">
                    <td style="text-align:center;width:30px;">&nbsp;</td>
                    <td style="text-align:left;width:150px; "></td>
                    <td colspan="12" style="text-align:center;width:360px; background-color:#CA911F">INITIAL RISK</td>
                    <td style="text-align:left;width:150px;">CONTROL MEASURES</td>
                    <td colspan="12" style="text-align:center;width:360px; background-color:#6699ff">RESIDUAL RISK</td>
                    <td style="text-align:left;border-left:solid 3px black;width:150px;">Additional Control Measures</td>
                    <td style="text-align:center;width:100px;">Proceed To Work</td>
                    <td style="text-align:center;width:30px;">&nbsp;</td>
                </tr>
                <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td style="text-align:center;width:30px;">View</td>
                    <td style="text-align:left;width:150px; ">Hazard </td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">People (P) </td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">Environment (E)</td>                                
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">Asset (A) </td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">Reputation (R) </td>
                     <td style="text-align:left;width:150px;"></td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">People (P)</td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">Environment (E)</td>    
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">Asset (A)</td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">Reputation (R)</td>
                    <td style="text-align:left; border-left:solid 3px black;width:150px;"></td>
                    <td style="text-align:center;width:100px;background-color:#99CCFF"></td>
                    <td style="text-align:center;width:30px;">Edit</td>
                </tr>
                 <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td style="text-align:center;width:30px;"></td>
                    <td style="text-align:left;width:150px; ">Consequences</td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityPI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodPI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelPI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td>
                   <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityEI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodEI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelEI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td>
                   <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityAI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodAI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelAI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityRI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodRI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelRI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td> 
                      <td style="text-align:left; width:150px;"></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityPF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodPF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelPF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityEF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodEF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelEF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityAF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodAF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelAF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityRF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodRF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelRF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:left; border-left:solid 3px black;width:150px;"></td>
                    <td style="text-align:center;width:100px;background-color:#99CCFF"></td>
                    <td style="text-align:center;width:30px;"></td>
                </tr>
        </table>
    </div>
    <div style="padding:0px;padding-top:175px;padding-bottom:250px; ">
    <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
            <asp:Repeater ID="rptHazardsNew" runat="server">
            <ItemTemplate>
                <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;">
                <tr>
                <td style="width:30px; text-align:center;">
                    <asp:ImageButton ID="btnAddHazard" runat="server" CommandArgument='<%#Eval("Hazard_TableId")%>' OnClick="btnAddHazard_Click1" ImageUrl="~/Modules/HRD/Images/add_16.gif" ToolTip="Add New Task" />                              
                </td>
                <td><b><%#Eval("HazardName")%></b></td>
                <td align="center" style="width:30px;">
                    <asp:ImageButton ID="btnDeleteHazard" OnClick="btnDeleteHazard_Click" ImageUrl="~/Modules/HRD/Images/delete_12.gif" ToolTip="Delete Hazard"  CommandArgument='<%#Eval("Hazard_TableId")%>' runat="server" />
                </td>
                </tr>
                </table>
                <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
                <asp:Repeater ID="rptHazardsTask" DataSource='<%#BindTasksNew(Common.CastAsInt32(Eval("Hazard_TableId")))%>' runat="server">
                <ItemTemplate>
                 <tr>
                    <td align="center" style="border:solid 1px #c2c2c2;width:30px;">
                       <asp:ImageButton ID="btnViewTask" runat="server" CommandArgument='<%#Eval("TableId")%>' OnClick="btnViewTask_Click" ImageUrl="~/Modules/HRD/Images/search_magnifier_12.png" ToolTip="View Task" />
                    </td>
                    <td align="left" style="text-align:left;width:150px;"><%#Eval("CONSEQUENCESNAME")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityPI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodPI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelPI"))%>'><%#Eval("RisklevelPI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityEI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodEI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelEI"))%>'><%#Eval("RisklevelEI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityAI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodAI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelAI"))%>'><%#Eval("RisklevelAI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityRI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodRI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelRI"))%>'><%#Eval("RisklevelRI")%></td>
                    <td align="left" style="text-align:left;width:150px;" ><%#Eval("ControlMeasures")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityPF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_LikeliHoodPF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelPF"))%>'><%#Eval("Re_RisklevelPF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityEF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_LikeliHoodEF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelEF"))%>'><%#Eval("Re_RisklevelEF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityAF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_LikeliHoodAF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelAF"))%>'><%#Eval("Re_RisklevelAF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityRF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityRF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelRF"))%>'><%#Eval("Re_RisklevelRF")%></td>
                    <td align="left" style="border-left:solid 3px black;width:150px;" ><%#Eval("ADD_CONTROL_MEASURES")%></td>
                    <td style="text-align:center;width:100px;"> <%#((Eval("Proceed").ToString()=="Y")?"Yes":(Eval("Proceed").ToString()=="N")?"No":"")%> </td>
                    <td align="center" style="border:solid 1px #c2c2c2;width:30px;">
                      <asp:ImageButton ID="btnEditTask" OnClick="btnEditTask_Click" ImageUrl="~/Images/editX12.jpg" ToolTip="Edit Hazard"  CommandArgument='<%#Eval("TableId")%>' runat="server" />
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                </table>
            </ItemTemplate>
            </asp:Repeater>
            </table>
    </div>
    <div style="position:fixed;bottom:0px; width:100%; padding:10px; background-color:#DDDDDD;">
        <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
        <tr>
            <td style="text-align:right; width:123px; "><b>Initial Risk :</b>&nbsp;</td>
            <td style="text-align:center; width:20px;"><asp:Image runat="server" ID="imgER" /></td>
            <td style="text-align:left; width:545px; "><asp:Label ID="lblExtAction" runat="server"></asp:Label></td>
            <td style="text-align:right; width:123px;"><b>Residual Risk :</b>&nbsp;</td>
            <td style="text-align:center; width:20px;"><asp:Image runat="server" ID="imgRR" /></td>
            <td style="text-align:left; "><asp:Label ID="lblResAction" runat="server"></asp:Label></td>
        </tr>
        </table>
         <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
        <tr>
            <td style="text-align:center; width:33%; font-weight:bold;">Created By/ On :&nbsp;<asp:Label runat="server" ID="lblCreatedByOn" ></asp:Label></td>
            <td style="text-align:center; width:34%; font-weight:bold;">Modified By/ On :&nbsp;<asp:Label runat="server" ID="lblModifiedByOn" ></asp:Label></td>
            <td style="text-align:center; width:33%; font-weight:bold;">Approved By/ On :&nbsp;<asp:Label runat="server" ID="lblApprovedByOn" ></asp:Label></td>
        </tr>
        </table>
        <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
        <tr>
            <td>&nbsp;<asp:Label runat="server" ID="lblMsg" ForeColor="Red" ></asp:Label></td>
            <td style="text-align:right; width:400px; padding-right:20px;">
                    <asp:Button runat="server" ID="btnSave" Text="Save Template" OnClick="btnSave_Click" CssClass="btn" Width="130px"  />
                    <asp:Button runat="server" ID="btnRequestApproval" Text="Request for Approval" OnClick="btnRequestAppprove_Click" CssClass="btn" Width="150px" />
                    <asp:Button runat="server" ID="btnApproveTemplate" Text="Approve" OnClick="btnApprove_Click" CssClass="btn" Width="130px" />
                    <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn" Width="100px" OnClientClick="window.close();" />
            </td>
        </tr>
        </table>
    </div>
   
    <div ID="dv_NewTask" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
       <asp:HiddenField ID="hfdTaskIdNew" runat="server" />
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :0px; text-align :center;background : white; z-index:0;top:0px; border:solid 5px black;">
             <div style="float:right;">
                        <asp:ImageButton runat="server" ID="ibCloseNewTask" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." CssClass="btn"   OnClick="ibCloseNewTask_Click" />
                    </div>
        <center >
                <div class="text headerband" style='padding:5px 0px 5px  0px'><b>Add / Edit Risk Management</b></div>
                <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;text-align:left; margin-left:10px;">
                <tr>
                <td colspan="4" style="text-align:center">
                 <center><b><asp:Label runat="server" ID="lblHazardName"></asp:Label></b></center>
                </td>
                </tr>
                <tr>
                <td style="width:120px"><b> Select Consequences</b></td>
                <td style="width:20px">:</td>
                <td style="text-align:left">
                    
                   <%-- --%>
                    <asp:TextBox ID="hfdTaskId" runat="server" Width="500px" style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="txtTaskname" runat="server" Width="500px"></asp:TextBox>
                  <%--  <asp:DropDownList runat="server" ID="ddlTask" Width="600px" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTaskname" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                    <td style="text-align:right;padding-right:10px;">
                        <asp:LinkButton ID="lbRAMatrix" runat="server" Text="RA Matrix" OnClick="lbRAMatrix_Click" ></asp:LinkButton>                   

                    </td>
                   <%-- <td style="width:150px">
                        Generic RA Reference No.:
                    </td>
                    <td style="width:200px;text-align:center;vertical-align:central;">
                        <asp:Label ID="lblGRA"  Text="GRA" runat="server" ForeColor="Blue"></asp:Label> &nbsp;
                        <asp:TextBox ID="txtGRANo" runat="server" Width="100px" Text="" TextMode="Number" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtGRANo" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>--%>
                </tr>
                   
                </table>
                <div class="box3" style='padding:5px 0px 5px  0px'><b>INITIAL RISK</b></div>
           
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <colgroup>
                    <col width="10%" />
                    <col width="10%"  />
                    <col width="6%"  />
                    <col width="10%" />
                    <col width="22%" />
                    <col width="10%" />
                    <col width="32%" />
                
                <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td>
                        &nbsp; Harm To
                    </td>
                    <td colspan="2">
                        &nbsp;Severity
                    </td>
                    <td colspan="2">
                        &nbsp;Likelihood
                    </td>
                    <td>
                         &nbsp;Risk level 
                    </td>
                    <td>
                         &nbsp;Risk Text 
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:UpdatePanel runat="server" ID='rerpi'>
                         <ContentTemplate>
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;People (P)  </td>
                                <td style="  text-align:left;width:10%;">
                                    <asp:DropDownList ID="ddlSeveritypi" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidual_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="imgbtnSeveritypi" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlLikelihoodpi" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidual_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblLikelihoodTextpi" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_Rlpi">
                                      <asp:Label ID="lblR13pi" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextpi" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                </tr>
                    <tr>
                    <td colspan="7">
                        <asp:UpdatePanel runat="server" ID='rerEI'>
                         <ContentTemplate>
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Environment (E)  </td>
                                <td style="  text-align:left;width:10%;">
                                    <asp:DropDownList ID="ddlSeverityEI" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualEI_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    
                                    <asp:ImageButton ID="lblSeverityTextEI" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlLikelihoodEI" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualEI_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblLikelihoodTextEI" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_RlEI">
                                      <asp:Label ID="lblR13EI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextEI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                </tr>
                    <tr>
                    <td colspan="7">
                        <asp:UpdatePanel runat="server" ID='rerAI'>
                         <ContentTemplate>
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Asset (A)  </td>
                                <td style="  text-align:left;width:10%;">
                                    <asp:DropDownList ID="ddlSeverityAI" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualAI_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                   
                                     <asp:ImageButton ID="lblSeverityTextAI" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlLikelihoodAI" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualAI_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblLikelihoodTextAI" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_RlAI">
                                      <asp:Label ID="lblR13AI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextAI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                </tr>
                    <tr>
                    <td colspan="7">
                        <asp:UpdatePanel runat="server" ID='rerRI'>
                         <ContentTemplate>
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Reputation (R)  </td>
                                <td style="  text-align:left;width:10%;">
                                    <asp:DropDownList ID="ddlSeverityRI" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualRI_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                   
                                    <asp:ImageButton ID="lblSeverityTextRI" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlLikelihoodRI" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualRI_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblLikelihoodTextRI" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_RlRI">
                                      <asp:Label ID="lblR13RI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextRI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                </tr>
                    </colgroup>

            </table>
                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding:2px 0px 2px 0px;">
                     <tr>
                          <td style="width:120px"><b>Control Measures</b></td>
                <td style="width:20px">:</td>
                <td style="text-align:left">
                    <asp:TextBox ID ="txtStdCM" runat="server" MaxLength="250"  Width="99%" TextMode="MultiLine" Height="40px"  CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0">

                    </asp:TextBox>
                    </td>

                    </tr>
                </table>
           
              <%--  <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td >
                         <asp:UpdatePanel runat="server" ID='rer'>
                         <ContentTemplate>
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style=" background-color:#dddddd; text-align:left;width:100px;">&nbsp;Severity  : </td>
                                <td style="width:160px;">
                                    <asp:DropDownList ID="ddlSeverity" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidual_Click" Width="160px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblSeverityText" runat="server"></asp:Label>
                                </td>
                                 
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;width:100px;">&nbsp;Likelihood :</td>
                                <td style="width:160px;">
                                  <asp:DropDownList ID="ddlLikelihood" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidual_Click" Width="160px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblLikelihoodText" runat="server"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;width:100px;">&nbsp;Risk level :</td>
                                 <td style="width:160px;"  runat="server" id="rd_Rl" >&nbsp;
                                    <asp:Label ID="lblR13" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblRiskText" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                              </tr>
                            </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                    </tr>
                </table>--%>

                <div class="box3" style='padding:5px 0px 5px  0px'><b>CONTROL MEASURES </b></div>

             <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <colgroup>
                    <col width="10%" />
                    <col width="10%"  />
                    <col width="6%"  />
                    <col width="10%" />
                    <col width="22%" />
                    <col width="10%" />
                    <col width="32%" />
                <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td>
                        &nbsp; Harm To
                    </td>
                    <td colspan="2">
                        &nbsp;Severity
                    </td>
                    <td colspan="2">
                        &nbsp;Likelihood
                    </td>
                    <td>
                         &nbsp;Risk level 
                    </td>
                    <td>
                         &nbsp;Risk Text 
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:UpdatePanel runat="server" ID='UpdatePanel2'>
                         <ContentTemplate>
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;People (P)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:DropDownList ID="ddlReSeverityPF" AutoPostBack="true" OnSelectedIndexChanged="btnReFillResidual_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor" Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major" Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe" Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityPF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlReLikelihoodPF" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualPF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextPF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1PF">
                                      <asp:Label ID="lblReR13PF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextPF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="7">  
                        <asp:UpdatePanel runat="server" ID='UpdatePanel1'>
                         <ContentTemplate>
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Environment (E)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:DropDownList ID="ddlReSeverityEF" AutoPostBack="true" OnSelectedIndexChanged="btnReFillResidualEF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor" Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major" Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe" Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityEF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlReLikelihoodEF" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualEF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextEF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1EF">
                                      <asp:Label ID="lblReR13EF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextEF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="7">  
                        <asp:UpdatePanel runat="server" ID='UpdatePanel3'>
                         <ContentTemplate>
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Asset (A)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:DropDownList ID="ddlReSeverityAF" AutoPostBack="true" OnSelectedIndexChanged="btnReFillResidualAF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor" Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major" Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe" Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityAF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlReLikelihoodAF" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualAF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextAF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1AF">
                                      <asp:Label ID="lblReR13AF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextAF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="7">  
                        <asp:UpdatePanel runat="server" ID='UpdatePanel4'>
                         <ContentTemplate>
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Reputation (R)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:DropDownList ID="ddlReSeverityRF" AutoPostBack="true" OnSelectedIndexChanged="btnReFillResidualRF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor" Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major" Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe" Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityRF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:DropDownList ID="ddlReLikelihoodRF" AutoPostBack="true" OnSelectedIndexChanged="btnFillResidualRF_Click" Width="120px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextRF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1RF">
                                      <asp:Label ID="lblReR13RF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextRF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                    </td>
                    </tr>
                    </colgroup>
                 </table>
                <%--<table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:50%;">
                        <asp:TextBox runat="server" ID="txtACM" Width="99%" TextMode="MultiLine" Height="125px"  CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox>
                    </td>
                    <td >
                        <asp:UpdatePanel runat="server" ID='UpdatePanel1'>
                         <ContentTemplate>
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style=" background-color:#dddddd; text-align:left;width:100px;">&nbsp;Severity  : </td>
                                <td style="width:160px;">
                                    <asp:DropDownList ID="ddlReSeverity" AutoPostBack="true" OnSelectedIndexChanged="btnReFillResidual_Click" Width="160px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Minor" Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Major" Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Severe" Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Catastropic" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblReSeverityText" runat="server"></asp:Label>
                                </td>
                                 
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;width:100px;">&nbsp;Likelihood :</td>
                                  <td style="width:160px;">
                                  <asp:DropDownList ID="ddlReLikelihood" AutoPostBack="true" OnSelectedIndexChanged="btnReFillResidual_Click" Width="160px" runat="server">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances" Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely" Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely" Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely" Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblReLikelihoodText" runat="server"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;width:100px;">&nbsp;Risk level :</td>
                                 <td style="width:160px;" runat="server" id="rd_ReR1" >&nbsp;
                                    <asp:Label ID="lblReR13" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblReRiskText" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                              </tr>
                            </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                    </tr>
                </table>--%>

                <div runat="server" visible="false" style="height:393px ;"  >
                   <table cellspacing="0"  border="1" cellpadding="1" style="width:100%;border-collapse:collapse; font-size:12px;">
                     <tr>
                         <td style="width:50%">
                         <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td colspan="7">Severity of Consequences</td>
                           </tr>
                            <tr style="  font-weight:bold;" class= "headerstylegrid">
                                <td rowspan="7" style="writing-mode: tb-rl;">Likelihood of Consequences</td>
                                <td></td>
                                <td></td>
                                <td>Negligible</td>
                                <td>Minor</td>
                                <td>Major</td>
                                <td>Severe</td>
                                <td>Catastropic</td>
                           </tr>
                           <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td>Rating</td>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Very Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">5</td>
                               <td><asp:Button ID="Button1"  CommandArgument="1,5,L" OnClick="btnFillResidual_Click" Text="5" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button2" CommandArgument="2,5,L" OnClick="btnFillResidual_Click" Text="10" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button3" CommandArgument="3,5,L" OnClick="btnFillResidual_Click" Text="15" runat="server" CssClass="btnact a" /></td>
                               <td><asp:Button ID="Button4" CommandArgument="4,5,M" OnClick="btnFillResidual_Click" Text="20" runat="server" CssClass="btnact r" /></td>
                               <td><asp:Button ID="Button5" CommandArgument="5,5,M" OnClick="btnFillResidual_Click" Text="25" runat="server" CssClass="btnact r" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">4</td>
                               <td><asp:Button ID="Button6" CommandArgument="1,4,L" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g"/></td>
                               <td><asp:Button ID="Button7" CommandArgument="2,4,L" OnClick="btnFillResidual_Click" Text="8" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button8" CommandArgument="3,4,M" OnClick="btnFillResidual_Click" Text="12" runat="server" CssClass="btnact a" /></td>
                               <td><asp:Button ID="Button9" CommandArgument="4,4,M" OnClick="btnFillResidual_Click" Text="16" runat="server" CssClass="btnact r" /></td>
                               <td><asp:Button ID="Button10" CommandArgument="5,4,M" OnClick="btnFillResidual_Click" Text="20" runat="server" CssClass="btnact r" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">3</td>
                               <td><asp:Button ID="Button11" CommandArgument="1,3,L" OnClick="btnFillResidual_Click" Text="3" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button12" CommandArgument="2,3,M" OnClick="btnFillResidual_Click" Text="6" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button13" CommandArgument="3,3,M" OnClick="btnFillResidual_Click" Text="9" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button14" CommandArgument="4,3,M" OnClick="btnFillResidual_Click" Text="12" runat="server" CssClass="btnact a"/></td>
                               <td><asp:Button ID="Button15" CommandArgument="5,3,H" OnClick="btnFillResidual_Click" Text="15" runat="server" CssClass="btnact a" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Highly Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">2</td>
                               <td><asp:Button ID="Button16" CommandArgument="1,2,M" OnClick="btnFillResidual_Click" Text="2" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button17" CommandArgument="2,2,M" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button18" CommandArgument="3,2,M" OnClick="btnFillResidual_Click" Text="6" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button19" CommandArgument="4,2,H" OnClick="btnFillResidual_Click" Text="8" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button20" CommandArgument="5,2,H" OnClick="btnFillResidual_Click" Text="10" runat="server" CssClass="btnact b" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Almost Nil Chances</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">1</td>
                               <td><asp:Button ID="Button21" CommandArgument="1,1,M" OnClick="btnFillResidual_Click" Text="1" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button22" CommandArgument="2,1,M" OnClick="btnFillResidual_Click" Text="2" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button23" CommandArgument="3,1,H" OnClick="btnFillResidual_Click" Text="3" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button24" CommandArgument="4,1,H" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button27" CommandArgument="5,1,H" OnClick="btnFillResidual_Click" Text="5" runat="server" CssClass="btnact g" /></td>
                           </tr>

                       </table>
                             <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                             <tr class= "headerstylegrid">
                                  <td style="width:60px">Colour</td>
                                  <td style="width:100px">Risk Score (AxB)</td>
                                  <td style="width:50px">Classification</td>
                                  <td>Action</td>
                             </tr>
                             <tr>
                                  <td class="r">Red</td>
                                  <td>16 - 25</td>
                                  <td class="r">High</td>
                                  <td style="text-align:left">Do not undertake Consequences.If operation is already in progress, abort and inform office.</td>
                             </tr>
                             <tr>
                                  <td class="a">Amber</td>
                                  <td>12 - 15</td>
                                  <td class="a">Warning</td>
                                  <td style="text-align:left">Job to be under taken only with office Approval.</td>
                             </tr>
                             <tr>
                                  <td class="b">Blue</td>
                                  <td>8 - 10</td>
                                  <td class="b">Medium</td>
                                  <td style="text-align:left">Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.</td>
                             </tr>
                             <tr>
                                  <td class="g">Green</td>
                                  <td>1 - 6</td>
                                  <td class="g">Low</td>
                                  <td style="text-align:left">Job can be under taken by ship staff.</td>
                             </tr>
                             </table>
                             
                         </td>
                         <td>
                            <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr class= "headerstylegrid">
                                  <td colspan="2">Likelihood(of Consequence)</td>
                                  <td colspan="2">Severity(of Consequence) </td>
                             </tr>
                             <tr >
                                  <td>Very Likely</td>
                                  <td style="text-align:left">More frequently than 10% of the time/ cases</td>
                                  <td>Catastrophic</td>
                                  <td style="text-align:left">* Serious loss of reputation which is devastating for trust and respect<br />
                                      * Considerable economic loss which can not be restored<br />
                                      * More than 5 m3 to sea                                
                                  </td>
                             </tr>
                             <tr >
                                  <td>Likely</td>
                                  <td style="text-align:left">Occurs between 1% and 10% of the time/ cases</td>
                                  <td>Severe</td>
                                  <td style="text-align:left">* Serious loss of reputation that will influence trust and respect for a long time<br />
                                      * Lagre economic loss more than US$100,000 that can be restored <br />
                                      * 1 to 5 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>UnLikely</td>
                                  <td style="text-align:left">Occurs between 0.1% and 1% of the time/ cases</td>
                                  <td>Major</td>
                                  <td style="text-align:left">* Reduction of reputation that may influence trust and respect<br />
                                      * Economic loss between US$10,000 and US$100,000 which can be restored <br />
                                      * Less than 1 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Highly UnLikely</td>
                                  <td style="text-align:left">Occurs less than 0.1% of the time/ cases</td>
                                  <td>Minor</td>
                                  <td style="text-align:left">* Small reduction of reputation in the short run<br />
                                      * Economic loss upto US$10,000 which can be restored <br />
                                      * Sheen on sea : evidance of loss to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Almost Nill Chances</td>
                                  <td style="text-align:left">Never heard within the industry</td>
                                  <td>Negligible</td>
                                  <td style="text-align:left">* No effect on reputation<br />
                                      * Negligible economic loss which can be restored <br />
                                      * Nill to sea : contained onboard
                                  </td>
                             </tr>
                             </table>
                         </td>
                     </tr>
                   </table>
                    
                </div>
          </center>
            <div>
                <div class="box3" style='padding:5px 0px 5px  0px'><b>ADDITIONAL CONTROL MEASURES </b></div>
                 <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;text-align:left; margin-left:10px;">
                      <tr>
               
                <td colspan="9"> <asp:TextBox runat="server" ID="txtACM" Width="99%" TextMode="MultiLine" Height="50px"  CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="width:180px;"><b> Decision to Proceed with work </b></td>
                <td style="width:10px;">:</td>
                <td><asp:RadioButton ID="rdoProceed_Y" runat="server" Text="Yes" GroupName="GNProceed" /><asp:RadioButton ID="rdoProceed_N" runat="server" Text="No" GroupName="GNProceed" /></td>
                   
                     <td><b>Person Incharge</b></td>
                <td>:</td>
                <td><asp:TextBox runat="server" ID="txtPIC" style="width:300px"></asp:TextBox></td>
               
                </tr>
               
               
                </table>
            </div>
          <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
            <asp:Label runat="server" ID="lblMess" style="float:left;color:Red"></asp:Label>
            <asp:Button runat="server" ID="btnSaveSingle" Text="Save" OnClick="btnSaveSingle_Click" CausesValidation="true" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
            <asp:Button ID="btnDeleteTask" OnClick="btnDeleteTask_Click" Text="Delete Consequences"  OnClientClick="return confirm('Are you sure to delete this?')" Visible="false" runat="server" style=" border:solid 1px grey;width:130px;" CssClass="btn"/>
            <asp:Button runat="server" ID="btnCancelTask" Text="Close" OnClick="btnCancelTask_Click" CausesValidation="false" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
          </div>
          </div>
        </center>
       
    </div>
    <div ID="dvNewHazard" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  padding :0px; text-align :center;background : white; z-index:0;top:30px; border:solid 5px black;">
        <center >
                <div class="text headerband" style='padding:10px 0px 10px  0px'><b>Select Hazard</b></div>
                <div style='padding:10px 0px 10px  0px; background-color:#99DDF3'>
                <input type="text" style='width:90%; padding:4px;' onkeyup="filter(this);" />
                </div>
                <div class="dvScrolldata" style="height: 330px;">
                <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                        <col style="width:50px;" />
                        <col style="width:100px;" />
                        <col />
                    </colgroup>
                    <asp:Repeater ID="rptAddHazard" runat="server">
                        <ItemTemplate>
                            <tr class='listitem'>
                                <td style="text-align:center">
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%#Eval("HazardId")%>' OnClick="btnSelectHazard_Click" ImageUrl="~/Modules/HRD/Images/check.gif" ToolTip='<%#Eval("HazardName")%>'/>
                                </td>
                                <td style="text-align:center"><%#Eval("HazardCode")%></td>
                                <td align="left" class='listkey'><%#Eval("HazardName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
          </center>
          <div style="padding:5px">
          <asp:Button runat="server" ID="Button25" Text="Close" OnClick="btnCancelNew_Click" CausesValidation="false" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
          </div>
          </div>
        </center>
       
        <script type="text/javascript">
            function filter(ctl) {
                var par = $(ctl).val().toLowerCase();
                $(".listitem").each(function (i, o) {
                    var txt = $(o).find(".listkey").first().html().toLowerCase();
                    if (parseInt(txt.search(par)) >= 0) {
                        $(o).css('display', '');
                    }
                    else {
                        $(o).css('display', 'none');
                    }
                });
            }
     </script>

      </div>

        <div ID="divRAMatrix" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
      
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :0px; text-align :center;background : white; z-index:0;top:0px; border:solid 5px black;">
        <center >
             <div style="float:right;">
                        <asp:ImageButton runat="server" ID="ibCloseRAMatrix" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." CssClass="btn" OnClick="ibCloseRAMatrix_Click"  />
                    </div>
                <div class="text headerband" style='padding:5px 0px 5px  0px'><b>RA MATRIX</b>
                   
                    
                </div>
            <table cellspacing="0"  border="1" cellpadding="1" style="width:100%;border-collapse:collapse; font-size:12px;">
                     <tr>
                         <td style="width:50%">
                         <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td colspan="7">Severity of Consequences</td>
                           </tr>
                            <tr style="  font-weight:bold;" class= "headerstylegrid">
                                <td rowspan="7" style="writing-mode: tb-rl;">Likelihood of Consequences</td>
                                <td></td>
                                <td></td>
                                <td>Negligible</td>
                                <td>Minor</td>
                                <td>Major</td>
                                <td>Severe</td>
                                <td>Catastropic</td>
                           </tr>
                           <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td>Rating</td>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Very Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">5</td>
                               <td><asp:Button ID="Button26"  CommandArgument="1,5,L" OnClick="btnFillResidual_Click" Text="5" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button28" CommandArgument="2,5,L" OnClick="btnFillResidual_Click" Text="10" runat="server" CssClass="btnact b" Enabled="false"/></td>
                               <td><asp:Button ID="Button29" CommandArgument="3,5,L" OnClick="btnFillResidual_Click" Text="15" runat="server" CssClass="btnact a" Enabled="false"/></td>
                               <td><asp:Button ID="Button30" CommandArgument="4,5,M" OnClick="btnFillResidual_Click" Text="20" runat="server" CssClass="btnact r" Enabled="false"/></td>
                               <td><asp:Button ID="Button31" CommandArgument="5,5,M" OnClick="btnFillResidual_Click" Text="25" runat="server" CssClass="btnact r" Enabled="false"/></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">4</td>
                               <td><asp:Button ID="Button32" CommandArgument="1,4,L" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button33" CommandArgument="2,4,L" OnClick="btnFillResidual_Click" Text="8" runat="server" CssClass="btnact b" Enabled="false" /></td>
                               <td><asp:Button ID="Button34" CommandArgument="3,4,M" OnClick="btnFillResidual_Click" Text="12" runat="server" CssClass="btnact a" Enabled="false" /></td>
                               <td><asp:Button ID="Button35" CommandArgument="4,4,M" OnClick="btnFillResidual_Click" Text="16" runat="server" CssClass="btnact r" Enabled="false" /></td>
                               <td><asp:Button ID="Button36" CommandArgument="5,4,M" OnClick="btnFillResidual_Click" Text="20" runat="server" CssClass="btnact r" Enabled="false" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">3</td>
                               <td><asp:Button ID="Button37" CommandArgument="1,3,L" OnClick="btnFillResidual_Click" Text="3" runat="server" CssClass="btnact g" Enabled="false" /></td>
                               <td><asp:Button ID="Button38" CommandArgument="2,3,M" OnClick="btnFillResidual_Click" Text="6" runat="server" CssClass="btnact g" Enabled="false" /></td>
                               <td><asp:Button ID="Button39" CommandArgument="3,3,M" OnClick="btnFillResidual_Click" Text="9" runat="server" CssClass="btnact b" Enabled="false" /></td>
                               <td><asp:Button ID="Button40" CommandArgument="4,3,M" OnClick="btnFillResidual_Click" Text="12" runat="server" CssClass="btnact a" Enabled="false"/></td>
                               <td><asp:Button ID="Button41" CommandArgument="5,3,H" OnClick="btnFillResidual_Click" Text="15" runat="server" CssClass="btnact a" Enabled="false"/></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Highly Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">2</td>
                               <td><asp:Button ID="Button42" CommandArgument="1,2,M" OnClick="btnFillResidual_Click" Text="2" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button43" CommandArgument="2,2,M" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button44" CommandArgument="3,2,M" OnClick="btnFillResidual_Click" Text="6" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button45" CommandArgument="4,2,H" OnClick="btnFillResidual_Click" Text="8" runat="server" CssClass="btnact b" Enabled="false"/></td>
                               <td><asp:Button ID="Button46" CommandArgument="5,2,H" OnClick="btnFillResidual_Click" Text="10" runat="server" CssClass="btnact b" Enabled="false"/></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Almost Nil Chances</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">1</td>
                               <td><asp:Button ID="Button47" CommandArgument="1,1,M" OnClick="btnFillResidual_Click" Text="1" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button48" CommandArgument="2,1,M" OnClick="btnFillResidual_Click" Text="2" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button49" CommandArgument="3,1,H" OnClick="btnFillResidual_Click" Text="3" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button50" CommandArgument="4,1,H" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" Enabled="false"/></td>
                               <td><asp:Button ID="Button51" CommandArgument="5,1,H" OnClick="btnFillResidual_Click" Text="5" runat="server" CssClass="btnact g" Enabled="false"/></td>
                           </tr>

                       </table>
                             <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                             <tr class= "headerstylegrid">
                                  <td style="width:60px">Colour</td>
                                  <td style="width:100px">Risk Score (AxB)</td>
                                  <td style="width:50px">Classification</td>
                                  <td>Action</td>
                             </tr>
                             <tr>
                                  <td class="r">Red</td>
                                  <td>16 - 25</td>
                                  <td class="r">High</td>
                                  <td style="text-align:left">Do not undertake Consequences.If operation is already in progress, abort and inform office.</td>
                             </tr>
                             <tr>
                                  <td class="a">Amber</td>
                                  <td>12 - 15</td>
                                  <td class="a">Warning</td>
                                  <td style="text-align:left">Job to be under taken only with office Approval.</td>
                             </tr>
                             <tr>
                                  <td class="b">Blue</td>
                                  <td>8 - 10</td>
                                  <td class="b">Medium</td>
                                  <td style="text-align:left">Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.</td>
                             </tr>
                             <tr>
                                  <td class="g">Green</td>
                                  <td>1 - 6</td>
                                  <td class="g">Low</td>
                                  <td style="text-align:left">Job can be under taken by ship staff.</td>
                             </tr>
                             </table>
                             
                         </td>
                         <td>
                            <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr class= "headerstylegrid">
                                  <td colspan="2">Likelihood(of Consequence)</td>
                                  <td colspan="2">Severity(of Consequence) </td>
                             </tr>
                             <tr >
                                  <td>Very Likely</td>
                                  <td style="text-align:left">More frequently than 10% of the time/ cases</td>
                                  <td>Catastrophic</td>
                                  <td style="text-align:left">* Serious loss of reputation which is devastating for trust and respect<br />
                                      * Considerable economic loss which can not be restored<br />
                                      * More than 5 m3 to sea                                
                                  </td>
                             </tr>
                             <tr >
                                  <td>Likely</td>
                                  <td style="text-align:left">Occurs between 1% and 10% of the time/ cases</td>
                                  <td>Severe</td>
                                  <td style="text-align:left">* Serious loss of reputation that will influence trust and respect for a long time<br />
                                      * Lagre economic loss more than US$100,000 that can be restored <br />
                                      * 1 to 5 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>UnLikely</td>
                                  <td style="text-align:left">Occurs between 0.1% and 1% of the time/ cases</td>
                                  <td>Major</td>
                                  <td style="text-align:left">* Reduction of reputation that may influence trust and respect<br />
                                      * Economic loss between US$10,000 and US$100,000 which can be restored <br />
                                      * Less than 1 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Highly UnLikely</td>
                                  <td style="text-align:left">Occurs less than 0.1% of the time/ cases</td>
                                  <td>Minor</td>
                                  <td style="text-align:left">* Small reduction of reputation in the short run<br />
                                      * Economic loss upto US$10,000 which can be restored <br />
                                      * Sheen on sea : evidance of loss to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Almost Nill Chances</td>
                                  <td style="text-align:left">Never heard within the industry</td>
                                  <td>Negligible</td>
                                  <td style="text-align:left">* No effect on reputation<br />
                                      * Negligible economic loss which can be restored <br />
                                      * Nill to sea : contained onboard
                                  </td>
                             </tr>
                             </table>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2" style="text-align:center;">
                              <asp:Button runat="server" ID="btnCloseRAMatrix" Text="Close" CssClass="btn" Width="100px" OnClick="btnCloseRAMatrix_Click"  />
                         </td>
                     </tr> 
                   </table>

            </center>
            </div>
           </center>
            </div>
    </div>
        
        <script type="text/javascript">
            
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
    }
    $(document).ready(function () {
        RegisterAutoComplete();
    });

</script>
    </form>
</body>

</html>
