<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRisk.aspx.cs" Inherits="RiskManagement_AddRisk" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script src="../../JS/jquery.min.js" type="text/javascript"></script>
    <script src="./../JS/KPIScript.js" type="text/javascript"></script>

    <link href="../IncidentReport/CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    <title>EMANAGER </title>
    <style type="text/css">
    .btnact
    {
        font-size:11px;
        width:100%;
        border:none;
        cursor:pointer;
    }
    .g
    {background-color:Green !important ;color:White !important ;}
    .b
    {background-color:#A3E0FF !important ;}
    .a
    {background-color:#FFCCCC !important ;}
    .r
    {background-color:red !important ;color:White !important ;}
    
    
    </style>
    <%--<link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jqScript.js" type="text/javascript"></script>
    

    <style type="text/css">
        
        input.cmn-toggle-yes-no + label {
          padding: 2px;
          width: 120px;
          height: 60px;
        }
        input.cmn-toggle-yes-no + label:before,
        input.cmn-toggle-yes-no + label:after {
          display: block;
          position: absolute;
          top: 0;
          left: 0;
          bottom: 0;
          right: 0;
          color: #fff;
          font-family: "Roboto Slab", serif;
          font-size: 20px;
          text-align: center;
          line-height: 60px;
        }
        input.cmn-toggle-yes-no + label:before {
          background-color: #dddddd;
          content: attr(data-off);
          transition: transform 0.5s;
          backface-visibility: hidden;
        }
        input.cmn-toggle-yes-no + label:after {
          background-color: #8ce196;
          content: attr(data-on);
          transition: transform 0.5s;
          transform: rotateY(180deg);
          backface-visibility: hidden;
        }
        input.cmn-toggle-yes-no:checked + label:before {
          transform: rotateY(180deg);
        }
        input.cmn-toggle-yes-no:checked + label:after {
          transform: rotateY(0);
        }
    </style>--%>
    
    
    
    <style type="text/css">
    .sel1
    {
        border:solid 1px #c2c2c2;
    }
    .withborder
    {
        border:solid 1px #c2c2c2;
    }
    </style>
    <style type="text/css">
        .ajax__myTab .ajax__tab_header { font-family: Arial, Helvetica, sans-serif; font-size: 12px;font-weight: bold;color:#000;border-left:solid 1px #666666; border-bottom:thin 1px #666666 }
        .ajax__myTab .ajax__tab_outer { padding-right: 4px; height: 25px; background-color: #fff; margin-right: 1px; border-right: solid 1px #666666; border-top: solid 1px #666666 }
        .ajax__myTab .ajax__tab_inner { padding-left: 4px; background-color: #fff; }
        .ajax__myTab .ajax__tab_tab { height: 20px; padding: 4px; margin: 0; }
        .ajax__myTab .ajax__tab_hover .ajax__tab_outer { background-color:  #c9c9c9}
        .ajax__myTab .ajax__tab_hover .ajax__tab_inner { background-color:  #c9c9c9}
        .ajax__myTab .ajax__tab_hover .ajax__tab_tab { background-color: #c9c9c9; cursor:pointer }
        .ajax__myTab .ajax__tab_active .ajax__tab_outer { background-color:#9ebae8; border-left: solid 1px #999999; }
        .ajax__myTab .ajax__tab_active .ajax__tab_inner { background-color:#9ebae8; }
        .ajax__myTab .ajax__tab_active .ajax__tab_tab {background-color:#9ebae8;cursor:inherit }
        .ajax__myTab .ajax__tab_body {border: 1px solid #666666; padding: 6px; background-color: #ffffff; }
        .ajax__myTab .ajax__tab_disabled {color:Gray }
    </style>
    <script type="text/javascript">

        function MoveNext1() {
            document.getElementById("__tab_TabContainer1_Management").click();
            return false;
        }

        function MoveNext2() {
            document.getElementById("__tab_TabContainer1_Execution").click();
            return false;
        }

    </script>
     <script src="../../JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="../../JS/AutoComplete/jquery-ui.css" />
     <script src="../../JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function RegisterAutoComplete() {
            //alert('Hi');
            $("#TabContainer2_TabPanel2_txtTaskname").keydown(function () {
                $("#TabContainer2_TabPanel2_hfdTaskId").val("");
            });

            $(function () {
                //------------
                 //alert('Hi 2');
                $("#TabContainer2_TabPanel2_txtTaskname").autocomplete(
                    {
                       
                        source: function (request, response) {
                            $.ajax({
                                url: getBaseURL() + "/LPSQE/getAutoCompleteData.ashx",
                                dataType: "json",
                                headers: { "cache-control": "no-cache" },
                                type: "POST",
                                data: { Key: $("#TabContainer2_TabPanel2_txtTaskname").val(), Type: "CONSEQUENCES" },
                                cache: false,
                                success: function (data) {
                                    response($.map(data.geonames, function (item) { return { label: item.CNAME, value: item.CNAME, bidid: item.CONSEQUENCESID } }
                                    ));
                                }
                            });
                        },
                        minLength: 2,
                        select: function (event, ui) {
                            $("#TabContainer2_TabPanel2_hfdTaskId").val(ui.item.bidid);

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
     <script type="text/javascript">
         function refreshParent() {
            /* alert('Hi');*/
             window.opener.location.reload();
             window.close();
         }
     </script>
    
</head>
<body style="margin:0px;     font-family: Verdana, Arial, Helvetica, sans-serif; font-size:12px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%--<div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; border:solid 1px #0099CC">
    <tr style="background-color:#0099CC; color:White;">
    <td style="padding:8px; text-align:center"><b>Risk Management</b></td>
    </tr>
    </table>
     <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
        <tr>
    <td style="text-align:right">Vessel : </td>
    <td style="width:200px"><asp:Label runat="server" ID="lblVesselName"></asp:Label> </td>
    <td style="text-align:right; ">Created By :</td>
    <td style="text-align:left;width:200px "><asp:TextBox runat="server" ID="txtCreatedBy" CssClass="input_box" MaxLength="50" Width="98%"></asp:TextBox> </td>
                
    <td style="text-align: right;">Ref # : </td>
    <td><asp:Label runat="server" ID="lblRefNo"></asp:Label> </td>

    <td style="text-align:right;">Head of Department : </td>
    <td style="text-align:left;width:200px"><asp:TextBox runat="server" ID="txtHOD" CssClass="input_box" MaxLength="50"  Width="98%"></asp:TextBox> </td>

    </tr>
    <tr>
    <td style="text-align:right">Master Name : </td>
    <td><asp:TextBox runat="server" ID="txtMaster" CssClass="input_box" MaxLength="50" Width="98%"></asp:TextBox> </td>

    <td style="text-align:right">Position : </td>
    <td><asp:TextBox runat="server" ID="txtPosition" CssClass="input_box" MaxLength="50"  Width="98%"></asp:TextBox></td>

    <td style="text-align:right">Date of Event: </td>
    <td><asp:TextBox runat="server" ID="txtEventDate" CssClass="input_box" MaxLength="15" Width="90px"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEventDate" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
    </td>
    <td style="text-align:right">Safety Officer : </td>
    <td><asp:TextBox runat="server" ID="txtSO" CssClass="input_box" MaxLength="50"  Width="98%"></asp:TextBox> </td>
    </tr>
    </table>
    <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
    <tr><td colspan="2" style="text-align:center"><asp:Label runat="server" ID="lblEventName" ForeColor="Brown" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
    <tr>
    <td style="text-align:left"><b>Risk Description :</b></td>
    <td style="text-align:left"><b>Risk Analysis : <asp:CheckBox runat="server" ID="chkAlt" AutoPostBack="true" OnCheckedChanged="chkAlt_OnCheckedChanged" Text="Alternate methods of work considered (if yes provide details)"/></b></td>
    </tr>
    <tr>
    <td style="width:50%">
    <asp:TextBox runat="server" ID="txtRiskDescr" CssClass="input_box" TextMode="MultiLine"  Width="98%" Height="50px"></asp:TextBox>
    </td>
    <td>
    <asp:TextBox runat="server" ID="txtDetails" CssClass="input_box" TextMode="MultiLine"  Width="98%" Height="50px"></asp:TextBox>
    </td>
    </tr>
    </table>
    
    <div style=" font-weight:bold; padding-top:5px">&nbsp;Risk Assessment :</div>    
     <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
<tr>
    <td>
       <div style="height:35px; background-color:#0099CC; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2">  
            <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="0" style="width:100%;border-collapse:collapse;height:35px; color:White;">
                    <colgroup>
                        <col style="width:60px;" />
                        <col />
                        <col style="width:180px;" />
                        <col style="width:17px" />
                    </colgroup>
                    <tr>
                        <td style="vertical-align:middle;">&nbsp;Routine</td>
                        <td style="text-align:left; vertical-align:middle;">&nbsp;Hazards Identified
                        <span style="color:Yellow; vertical-align:middle;">&nbsp;&nbsp; if not listed here  (  <asp:LinkButton runat="server" ID="lnkAddHazard" OnClick="lnkAddHazard_Click" Text="Add New" ForeColor="Yellow"></asp:LinkButton> )</span>
                        </td>                        
                        <td style="text-align:center; vertical-align:middle;">&nbsp;Action</td>
                        <td>&nbsp;</td>
                    </tr>
            </table>
        </div>
      
                <div class="ScrollAutoReset" style="height:400px; overflow-x:hidden;overflow-y:scroll; border:solid 1px #c2c2c2" id="dv001_1">
                    <table cellspacing="0" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;" bordercolor="#F0F0F5">
                       <colgroup>
                            <col style="width:60px;" />
                            <col />                            
                            <col style="width:180px;" />
                            <col style="width:17px" />
                        </colgroup>
                        <asp:Repeater ID="rptRisk" runat="server">
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align:center"><%#((Eval("ROUTINE").ToString()=="Y")?"Yes":"No")%></td>
                                    <td style="text-align:left">
                                    <span style="color:Red;">&nbsp;<%#Eval("HazardName")%></span>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                    <td style="width:50%">
                                        <div id="Div1" runat="server" visible='<%#(Eval("LIKELIHOOD").ToString().Trim()!="")%>'>
                                            <div style="margin:5px;padding:5px;background-color:<%#getColor(Eval("RISKRANK").ToString())%>">
                                                <div><b>Inherent</b></div>
                                                <span style=" width:33%;  display:block; float:left;">Probability : <%#getFullText(Eval("LIKELIHOOD").ToString(),"L")%></span>
                                                <span style="width:33%; display:block; float:left;">Impact : <%#getFullText(Eval("CONSEQUENCES").ToString(), "C")%></span>
                                                <span style="padding-left:0px; display:block; float:left; ">Rating : <%#getFullText(Eval("RISKRANK").ToString(), "R")%></span>
                                                <div style="clear:both;"></div>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width:50%">
                                    <div id="Div2" runat="server" visible='<%#(Eval("Re_LIKELIHOOD").ToString().Trim()!="")%>' >
                                            <div style="margin:5px; padding:5px;background-color:<%#getColor(Eval("Re_RISKRANK").ToString())%>">
                                                <div><b>Residual</b></div>
                                                <span style=" width:33%; border:0px solid #ff0000; display:block; float:left;">Probability : <%#getFullText(Eval("Re_LIKELIHOOD").ToString(),"L")%></span>
                                                <span style=" width:33%; border:0px solid #ff0000; display:block; float:left;">Impact : <%#getFullText(Eval("Re_CONSEQUENCES").ToString(), "C")%></span>
                                                <span style="padding-left:0px; border:0px solid #ff0000; display:block; float:left; ">Rating : <%#getFullText(Eval("Re_RISKRANK").ToString(), "R")%></span>
                                                <div style="clear:both;"></div>
                                            </div> 
                                    </div>
                                    </td>
                                    </tr>
                                    </table>
                                    </td>
                                    <td style="text-align:center">
                                    <asp:LinkButton ID="btnSelect" Text="Management/Exec Plan"  runat="server" VesselCode='<%#Eval("VesselCode")%>' RiskId='<%#Eval("RiskId")%>' HazardId='<%#Eval("HazardId")%>' CommandArgument='<%#Eval("SRKey")%>' OnClick="ShowMangementPlan" ></asp:LinkButton>&nbsp;<span id="Span1" style="color:Red; font-weight:bold;" runat="server" visible='<%#(Eval("STD_CONTROL_MESASRUES").ToString().Trim()=="")%>'>*</span>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
    </td>
</tr>
 <tr style="background-color:#0099CC; color:Black; display:none;">
    <td style="padding:0px; text-align:left">
    <div class="box1" style="padding-left:10px; text-align:left">
    Sign for Assessment Plan
    </div>
    </td>
    </tr>
    
         <tr style="background-color:#0099CC; color:Black;">
            <td style="padding:0px; text-align:left">
            <div class="box1" style="padding-left:10px; text-align:left">
             Comments : ( <asp:Label runat="server" ID="lblCommentsByOn" ></asp:Label> )
            </div>
            </td>
            </tr>
         <tr style="color:Black;">
            <td style="padding:0px; text-align:left; padding:5px">
            <i style="color:Blue">
                <asp:Label ID="lblOfficeComments" runat="server"></asp:Label>
            </i>
            </td>
            </tr>
    </table>
   
    </div>
    <div style="text-align:center; padding:5px;">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" Text="Save" ValidationGroup="v1" />
        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" Text="Export" Visible="false" />
        <asp:Button ID="btnClose" runat="server" CausesValidation="false" OnClientClick="javascript:self.close();" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" Text="Close" />
    </div>
    <div>
        <uc1:MessageBox runat="server" ID="msg1" />
    </div>

   <div ID="dv_NewHazard" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
            <div style="position:relative;width:900px;  height:100px;padding :0px; text-align :center;background : white; z-index:150;top:200px; border:solid 5px black;">
            <center >
            <div class="box3"><b>Add New Hazard</b></div>
             <table cellspacing="0" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
             <tr>
             <td style="text-align:right; width:100px;">Hazard Name :</td>
             <td style="text-align:left">
                <asp:TextBox runat="server" id="txtHazardName" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="fsa" ControlToValidate="txtHazardName" ErrorMessage="*"></asp:RequiredFieldValidator>
             </td>
             </tr>
             </table>
             <asp:Label runat="server" ID="lblM1" ForeColor="Red"></asp:Label>
             <div style="padding:3px">
                <asp:Button runat="server" ID="Button27" Text="Save" OnClick="btnAddHazard_Click" ValidationGroup="a1" style=" background-color:#2E9AFE; color:White; border:solid 1px grey;width:100px;"/>
                <asp:Button runat="server" ID="Button28" Text="Close" OnClick="btnCloseAddHazard_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
                </div>
            </center>
            </div>
    </center>
    </div>

   <div ID="dv_MgmtPlan" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:900px;  height:550px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 10px black;">
        <center >
            <div class="box3"><b><asp:HiddenField ID="hfd_MP" runat="server" />RISK ASSESSMENT</b></div>
            <asp:UpdatePanel runat="server" ID="fsads">
            <ContentTemplate>
            <div style="height: 470px; text-align:left; margin:10px; border:none; overflow:hidden; overflow-x:hiden; overflow-y:hidden;">
                <asp:TabContainer  ID="TabContainer1" runat="server">
                  <asp:TabPanel ID="RiskAssessment" runat="server">
                   <HeaderTemplate>Inherent Risk</HeaderTemplate>
                   <ContentTemplate>
                   <table cellspacing="3" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                    <tr >
                        <td><asp:HiddenField ID="hfHazardId_RA" runat="server" />
                        <asp:HiddenField ID="hfHazards" runat="server" /> 
                        </td>
                        <td colspan="5" style="text-align:center;"><b>Inherent Probability :</b></td>
                    </tr>
                    <tr >
                        <td style="text-align:right"><b>Inherent Impact : </b></td>
                        <td colspan="5" style="text-align:left;">
                            <table cellspacing="0"  border="1" cellpadding="3" style="width:95%;border-collapse:collapse;">
                            <tr>
                                <td></td>
                                <td>1: Unlikely</td>
                                <td>2: Possible</td>
                                <td>3: Quite Possible</td>
                                <td>4: Likely</td>
                                <td>5: Very Likely</td>
                           </tr>
                           <tr>
                               <td>1: Negligible</td>
                               <td>
                               <asp:Button ID="Button"  CommandArgument="1,1,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button1" CommandArgument="2,1,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button2" CommandArgument="3,1,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button3" CommandArgument="4,1,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button4" CommandArgument="5,1,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>2: Slight</td>
                               <td><asp:Button ID="Button5" CommandArgument="1,2,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button6" CommandArgument="2,2,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button7" CommandArgument="3,2,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button8" CommandArgument="4,2,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button9" CommandArgument="5,2,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>3: Moderate</td>
                               <td><asp:Button ID="Button10" CommandArgument="1,3,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button11" CommandArgument="2,3,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button12" CommandArgument="3,3,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button13" CommandArgument="4,3,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button14" CommandArgument="5,3,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>4: High</td>
                               <td><asp:Button ID="Button15" CommandArgument="1,4,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button16" CommandArgument="2,4,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button17" CommandArgument="3,4,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button18" CommandArgument="4,4,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button19" CommandArgument="5,4,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>5: Very High</td>
                               <td><asp:Button ID="Button20" CommandArgument="1,5,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button21" CommandArgument="2,5,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button22" CommandArgument="3,5,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button23" CommandArgument="4,5,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button24" CommandArgument="5,5,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>

                       </table>
                        </td>
                    </tr>    
                    <tr>
                    <td style="text-align:right; padding-top:10px;">
                        <b>Selected Value : </b>
                    </td>
                    <td colspan="5" style=" padding-top:10px;">
                    <b>
                    <asp:Label runat="server" ID="lblSelIR"></asp:Label> 
                    </b>
                    </td>
                    
                    </tr>
                    </table>
                    </ContentTemplate>
                  </asp:TabPanel>

                  <asp:TabPanel ID="Management" runat="server">
                  <HeaderTemplate>Management / Execution </HeaderTemplate>
                  <ContentTemplate>
                  <div style="height:410px ; border-top:solid 1px #c2c2c2 ; overflow:hidden; overflow-y:scroll;">
                  <table cellspacing="0" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
                  <tr>
                    <td style="text-align:left;">Routine :<asp:RadioButton runat="server" ID="rad_R_yes" GroupName="r1" Text="Yes" />&nbsp;<asp:RadioButton runat="server" ID="rad_R_no" GroupName="r1" Text="No" Checked="true" /></td>
                  </tr>
                  <tr>
                    <td style="text-align:left;">Standard Control Measures ( Sample Guidelines ) :
                        <div runat="server" id="dv_SCM" style="width:95%;color:Blue;"></div>
                    </td>
                </tr>
                  <tr>
                    <td style="text-align:left;">Standard Control Measures ( Ship Remarks) :</td>
                  </tr>
                  <tr>
                <td style="text-align:left;"><asp:TextBox runat="server" ID="txtSCM" Width="95%" TextMode="MultiLine" Height="80px" ValidationGroup="a1"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtSCM" ErrorMessage="*" ValidationGroup="a1" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                    <td style="text-align:left;">Additional Control Measures ( Sample Guidelines ) :
                        <div runat="server" id="dvACM" style="width:95%; color:Blue;"></div>
                    </td>
                </tr>
                  <tr>
                        <td style="text-align:left;">Additional Control Measures ( Ship Remarks) :</td>
                  </tr>
                  <tr>
                <td style="text-align:left;"><asp:TextBox runat="server" ID="txtACM" Width="95%" TextMode="MultiLine" Height="80px"  ValidationGroup="a1"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtACM" ErrorMessage="*" ForeColor="Red" ValidationGroup="a1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                <td style="text-align:left; padding:0px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td style="width:33%; display:inline-block">Agreed Time :</td>
                    <td style=" display:inline-block">PIC Name :</td>
                </tr>
                </table>
                
                </td>
                </tr>
                  <tr>
                <td style="text-align:left; padding:0px;">
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:33%">
                        <asp:TextBox runat="server" ID="txtAgreedTime" MaxLength="50" ValidationGroup="a1" Width="93%"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtAgreedTime"  ForeColor="Red" ErrorMessage="*" ValidationGroup="a1"></asp:RequiredFieldValidator>
                    </td>
                    <td >
                        <asp:TextBox runat="server" ID="txtPN" Width="93%" ValidationGroup="a2"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtPN" ErrorMessage="*" ForeColor="Red" ValidationGroup="a2"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                    </table>
                </td>
                </tr>
                  </table>
                  </div>
                  </ContentTemplate>
                  </asp:TabPanel>
                  
                  <asp:TabPanel ID="Execution" runat="server">
                    <HeaderTemplate>Residual Risk</HeaderTemplate>
                    <ContentTemplate>
                    <table cellspacing="3" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                    <tr>
                        <td><asp:HiddenField ID="hfResidual" runat="server" /> </td>
                        <td colspan="5" style="text-align:center;"><b>Residual Probability :</b></td>
                    </tr>
                    <tr>
                    <td style="text-align:right;"><b>Residual Impact :</b></td>
                    <td colspan="5" style="text-align:left;">
                       <table cellspacing="0"  border="1" cellpadding="3" style="width:95%;border-collapse:collapse;">
                            <tr>
                                <td></td>
                                <td>1: Unlikely</td>
                                <td>2: Possible</td>
                                <td>3: Quite Possible</td>
                                <td>4: Likely</td>
                                <td>5: Very Likely</td>
                           </tr>
                           <tr>
                               <td>1: Negligible</td>
                               <td><asp:Button ID="Button29"  CommandArgument="1,1,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%; border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button30" CommandArgument="2,1,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button31" CommandArgument="3,1,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button32" CommandArgument="4,1,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button33" CommandArgument="5,1,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>2: Slight</td>
                               <td><asp:Button ID="Button34" CommandArgument="1,2,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button35" CommandArgument="2,2,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button36" CommandArgument="3,2,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button37" CommandArgument="4,2,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button38" CommandArgument="5,2,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>3: Moderate</td>
                               <td><asp:Button ID="Button39" CommandArgument="1,3,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button40" CommandArgument="2,3,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button41" CommandArgument="3,3,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button42" CommandArgument="4,3,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button43" CommandArgument="5,3,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>4: High</td>
                               <td><asp:Button ID="Button44" CommandArgument="1,4,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button45" CommandArgument="2,4,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button46" CommandArgument="3,4,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button47" CommandArgument="4,4,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button48" CommandArgument="5,4,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>5: Very High</td>
                               <td><asp:Button ID="Button49" CommandArgument="1,5,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button50" CommandArgument="2,5,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button51" CommandArgument="3,5,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button52" CommandArgument="4,5,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button53" CommandArgument="5,5,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>

                       </table>
                    </td>
                    
                </tr>    
                <tr>
                    <td style="text-align:right; padding-top:10px;">
                        <b>Selected Value : </b>
                    </td>
                    <td colspan="5" style=" padding-top:10px;">
                    <b>
                    <asp:Label runat="server" ID="lblSelRR"></asp:Label> 
                    </b>
                    </td>
                    
                    </tr>
                </table>
                    </ContentTemplate>
                  </asp:TabPanel>
                </asp:TabContainer>
                <uc1:MessageBox runat="server" ID="lblMsg" />
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </center>
        <div style="padding:3px">
        <asp:Button runat="server" ID="Button26" Text="Save" OnClick="btnSaveMP_Click" ValidationGroup="a1" style=" background-color:#2E9AFE; color:White; border:solid 1px grey;width:100px;"/>
        <asp:Button runat="server" ID="Button25" Text="Close" OnClick="btnCloseMP_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
        </div>
        </center>
   
   </div>--%>
    
    <div style="font-family:Arial;font-size:12px;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; border:solid 0px #0099CC">
    <tr class="text headerband">
    <td style="padding:8px; text-align:center"><b>Risk Management - [ <asp:Label runat="server" ID="lblRefNo"></asp:Label> ]</b></td>
    </tr>
    <tr><td style="text-align:center"><b> Activity Name : </b><asp:Label runat="server" ID="lblEventName" ForeColor="Brown" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
    </table>
    <asp:TabContainer  ID="TabContainer2" runat="server" CssClass="ajax__myTab" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" TabIndex="1">
        <HeaderTemplate >
Risk Description
</HeaderTemplate>
        
<ContentTemplate>
<table cellpadding="4" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;"><tr><td style="text-align:right; width: 200px;">PTW Required : </td><td style="text-align:left;width:150px;"><asp:DropDownList ID="ddlPTWRequired" runat="server" AutoPostBack="True" Width="120px"><asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem></asp:DropDownList></td><td style="text-align:right;width:150px;">Type of PTW :</td><td colspan="3" style="text-align:left;width:550px;"><asp:DropDownList ID="ddlTypeoFPTW" runat="server" AutoPostBack="True" Width="225px"></asp:DropDownList>

                   
                    </td>
                   
                </tr>
                <tr><td colspan="6" style="text-align:left"><b>Alternate methods of work considered&#160; : </b><asp:DropDownList ID="ddlAlt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkAlt_OnCheckedChanged"><asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem></asp:DropDownList>&#160; <span>( if yes provide details below ) </span></td></tr>
            
                <tr><td style="text-align:left;" colspan="6">Geographical Location (Port or Lat/Long) :<b>Risk Description :</b></td><td style="text-align:right;width:200px;">Ship&#39;s Name : </td><td style="text-align:left;width:150px;" ><asp:Label ID="lblVesselName" runat="server"></asp:Label></td><td style="text-align:right;width:150px;">Date of Activity : </td><td style="text-align:left;width:150px;"><asp:TextBox runat="server" ID="txtEventDate" MaxLength="15" Width="120px" CssClass="input_box" BackColor="LightYellow"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEventDate"></asp:CalendarExtender>

                    </td>
                    <td style="text-align:right;width:200px;"></td><td style="text-align:left;width:150px;"><asp:TextBox ID="txtGrographicLocation" runat="server" CssClass="input_box" MaxLength="50" Width="200px"></asp:TextBox></td></tr><tr><td style="text-align:right;width:200px;">Personnel required for the Task : </td><td style="text-align:left;width:150px;"><asp:DropDownList ID="ddlPerRefTask" runat="server" AutoPostBack="True" Width="120px"><asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem><asp:ListItem Text="Ship Crew" Value="1"></asp:ListItem><asp:ListItem Text="Shore Assistance" Value="2"></asp:ListItem></asp:DropDownList></td><td style="text-align:right;width:150px;">No. of Personnel for Task : </td><td style="text-align:left;width:150px;"><asp:TextBox ID="txtNoofPerTask" runat="server" CssClass="input_box" MaxLength="5" TextMode="Number" Width="120px"></asp:TextBox></td><td></td>
                    <td></td>

                </tr><tr><td colspan="6" style="text-align:left; vertical-align: top;"><asp:TextBox ID="txtDetails" runat="server" BackColor="LightYellow" CssClass="input_box" Height="130px" TextMode="MultiLine" Visible="False" Width="99%"></asp:TextBox></td></tr><tr><td colspan="6" style="text-align:left;"><asp:TextBox ID="txtRiskDescr" runat="server" BackColor="LightYellow" CssClass="input_box" Height="130px" TextMode="MultiLine" Width="99%"></asp:TextBox></td></tr></table><table cellpadding="4" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;"><tr><td style="text-align:right;width:125px;"></td><td style="text-align:left;width:150px; "><asp:TextBox ID="txtCreatedBy" runat="server" BackColor="LightYellow" CssClass="input_box" MaxLength="50" Width="140px"></asp:TextBox></td><td style="text-align:right;width:125px;">Created By :Rank : </td><td style="text-align:left;width:150px; "><asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True" Width="120px"></asp:DropDownList>

                </td>
                <td style="text-align:right;width:150px;">Head of Department : </td><td style="text-align:left;width:200px;"><asp:TextBox runat="server" ID="txtHOD" CssClass="input_box" MaxLength="50"  Width="150px" BackColor="LightYellow"></asp:TextBox></td></tr><tr><td style="text-align:right;width:125px;">Safety Officer : </td><td><asp:TextBox runat="server" ID="txtSO" CssClass="input_box" MaxLength="50"  Width="150px" BackColor="LightYellow"></asp:TextBox></td><td style="text-align:right;width:125px;">Master Name : </td><td><asp:TextBox runat="server" ID="txtMaster" CssClass="input_box" MaxLength="50" Width="150px"></asp:TextBox></td><td style="text-align:right;width:150px;">Verified By/On :</td><td style="text-align:left;width:200px;"><asp:Label ID="lblVerifiedOn" runat="server"></asp:Label></td></tr></table><div style="text-align:center; padding:10px;"><div style="float:left;"><uc1:MessageBox runat="server" ID="msg1" />
</div>
              <div style="display:flex;justify-content:flex-end;"><asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Modules/HRD/Images/next.png" OnClick="btnNext_Click" ToolTip="Go to Next Tab" style="margin-right:5px;" />
 &nbsp; <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClientClick="javascript:self.close();" style=" padding:3px; border:none;  width:80px;Margin-right:5px;" Text="Close" CssClass="btn" />
 
                   <asp:Button ID="btnPrint" runat="server" Text="Print"  style="margin-right:5px;padding:3px; border:none;  width:80px;" CssClass="btn"  CausesValidation="False" Visible="False" OnClick="btnPrint_Click" />
 &nbsp; </div></div>
</ContentTemplate>
        
</asp:TabPanel>

        <asp:TabPanel ID="TabPanel2" runat="server" TabIndex="2">
        <HeaderTemplate>
Risk Assessment
</HeaderTemplate>
        
<ContentTemplate>
              <div style="position:fixed;top:85px; width:100%; height:135px; border-bottom:Solid 5px #47A3FF; background-color:#eeeeee;">
        <div style="overflow:none;">
        <div style="float:left; width:80%;">
        <table cellspacing="0" rules="none" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
        <tr>
        <td style="text-align:right;font-weight:bold;width:120px;">Template Code :&nbsp;</td>
        <td style="text-align:left;"><asp:Label runat="server" ID="lblTempCode" ></asp:Label></td>
        </tr>
        </table>
        </div>
       <%-- <div style="width:20%; text-align:right;margin-left:80%;">
            <div style="margin-right:5px; padding-top:5px;">

                 <asp:Button runat="server" ID="btnAddHazard"  Text="Add Hazard" CssClass="btn" Width="130px" OnClick="btnAddHazard_Click" />
            </div>
        </div>--%>
        </div>
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
                   <%-- <td style="text-align:center;width:30px;">Edit</td>--%>
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
                   <%-- <td style="text-align:center;width:30px;"></td>--%>
                </tr>
        </table>
    </div>
    <div style="padding:0px;padding-top:135px;padding-bottom:250px;height:260px;overflow-y:auto; ">
    <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
            <asp:Repeater ID="rptHazardsNew" runat="server">
            <ItemTemplate>
                <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;">
                <tr>
                <td style="width:30px; text-align:center;">
                   <%-- <asp:ImageButton ID="btnAddHazard" runat="server" CommandArgument='<%#Eval("Hazard_TableId")%>' OnClick="btnAddHazard_Click1" ImageUrl="~/Modules/HRD/Images/add_16.gif" ToolTip="Add New Task" />  --%>                            
                </td>
                <td><b><%#Eval("HazardName")%></b></td>
                <td align="center" style="width:30px;">
                  <%--  <asp:ImageButton ID="btnDeleteHazard" OnClick="btnDeleteHazard_Click" ImageUrl="~/Modules/HRD/Images/delete_12.gif" ToolTip="Delete Hazard"  CommandArgument='<%#Eval("Hazard_TableId")%>' runat="server" />--%>
                </td>
                </tr>
                </table>
                <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
                <asp:Repeater ID="rptHazardsTask" DataSource='<%#BindTasksNew(Common.CastAsInt32(Eval("Hazard_TableId")))%>' runat="server">
                <ItemTemplate>
                 <tr>
                    <td align="center" style="border:solid 1px #c2c2c2;width:30px;">
                       <asp:ImageButton ID="btnViewTask" runat="server" CommandArgument='<%#Eval("TableId")%>' OnClick="btnViewTask_Click" ImageUrl="~/Modules/HRD/Images/magnifier.png" ToolTip="View Task" />
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
                   <%-- <td align="center" style="border:solid 1px #c2c2c2;width:30px;">
                      <asp:ImageButton ID="btnEditTask" OnClick="btnEditTask_Click" ImageUrl="~/Modules/HRD/Images/editX12.jpg" ToolTip="Edit Hazard"  CommandArgument='<%#Eval("TableId")%>' runat="server" Visible='<%#Eval("RAStatus").ToString()== "O" %>'  />
                    </td>--%>
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
       <%--  <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
        <tr>
            <td style="text-align:center; width:33%; font-weight:bold;">Created By/ On :&nbsp;<asp:Label runat="server" ID="lblCreatedByOn" ></asp:Label></td>
            <td style="text-align:center; width:34%; font-weight:bold;">Modified By/ On :&nbsp;<asp:Label runat="server" ID="lblModifiedByOn" ></asp:Label></td>
            <td style="text-align:center; width:33%; font-weight:bold;">Approved By/ On :&nbsp;<asp:Label runat="server" ID="lblApprovedByOn" ></asp:Label></td>
        </tr>
        </table>--%>
        <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
        <tr>
            <td>&nbsp;<asp:Label runat="server" ID="Label2" ForeColor="Red" ></asp:Label></td>
            <td style="text-align:right;  display:flex;justify-content:flex-end; padding-right:20px;margin-right:10px;" >
                 <asp:Label ID="lblApprovalRequiredMsg" runat="server" ForeColor="Red" style=" padding:3px; border:none;  Margin-right:10px;"></asp:Label> 
                 <asp:ImageButton ID="btnPrevious" runat="server" ImageUrl="~/Modules/HRD/Images/Prev.png" OnClick="btnPrevious_Click" ToolTip="Go to Previous Tab" style="margin-right:5px;" /> 
                  <asp:ImageButton ID="btnNextTabPanel2" runat="server" ImageUrl="~/Modules/HRD/Images/next.png" OnClick="btnNextTabPanel2_Click" ToolTip="Go to Next Tab" style="margin-right:5px;" /> &nbsp;
              <%--   <Asp:Button btn="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" style=" padding:3px; border:none;  width:80px;" CssClass="btn" />--%>
                 <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" style=" padding:3px; border:none;  width:80px;" Text="Save" ValidationGroup="v1" CssClass="btn"/>
                <Asp:Button ID="btnVerify" runat="server" style=" padding:3px; border:none;  width:80px;" Text="Verify" ValidationGroup="v1" CssClass="btn" Visible="false" OnClick="btnVerify_Click" />
       <%-- <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" style=" padding:3px; border:none;  width:80px;margin-right:5px;" Text="Export" Visible="false" CssClass="btn" />--%>
                    <asp:Button runat="server" ID="btnCloseRefresh" Text="Close" CssClass="btn" Width="100px" OnClick="btnCloseRefresh_Click"  />
            </td>
        </tr> 
        </table>
    </div>
            <div ID="dv_NewTask" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="False">
       <center>
       <asp:HiddenField ID="hfdTaskIdNew" runat="server" />
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :0px; text-align :center;background : white; z-index:0;top:0px; border:solid 5px black;">
             <div style="float:right;">
                        <asp:ImageButton runat="server" ID="ibCloseNewTask" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." CssClass="btn"  OnClick="ibCloseNewTask_Click" CausesValidation="false" Width="25px" />
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
                    
                    <asp:TextBox ID="hfdTaskId" runat="server" Width="500px" style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="txtTaskname" runat="server" Width="500px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTaskname" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                    <td style="text-align:right;padding-right:10px;">
                         <asp:LinkButton ID="lbRAMatrix" runat="server" Text="RA Matrix" OnClick="lbRAMatrix_Click" CausesValidation="false" ></asp:LinkButton>   
                    </td>
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
                    <asp:TextBox ID ="txtStdCM" runat="server" MaxLength="250"  Width="99%" TextMode="MultiLine" Height="40px"  CssClass="withborder" ValidationGroup="a1" BackColor="LightYellow"></asp:TextBox>
                    </td>

                    </tr>
                </table>
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
                <div runat="server" visible="False" style="height:393px ;"  >
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
                               <td><asp:Button ID="Button57"  CommandArgument="1,5,L" OnClick="btnFillResidual_Click" Text="5" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button58" CommandArgument="2,5,L" OnClick="btnFillResidual_Click" Text="10" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button59" CommandArgument="3,5,L" OnClick="btnFillResidual_Click" Text="15" runat="server" CssClass="btnact a" /></td>
                               <td><asp:Button ID="Button60" CommandArgument="4,5,M" OnClick="btnFillResidual_Click" Text="20" runat="server" CssClass="btnact r" /></td>
                               <td><asp:Button ID="Button61" CommandArgument="5,5,M" OnClick="btnFillResidual_Click" Text="25" runat="server" CssClass="btnact r" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">4</td>
                               <td><asp:Button ID="Button62" CommandArgument="1,4,L" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g"/></td>
                               <td><asp:Button ID="Button63" CommandArgument="2,4,L" OnClick="btnFillResidual_Click" Text="8" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button64" CommandArgument="3,4,M" OnClick="btnFillResidual_Click" Text="12" runat="server" CssClass="btnact a" /></td>
                               <td><asp:Button ID="Button65" CommandArgument="4,4,M" OnClick="btnFillResidual_Click" Text="16" runat="server" CssClass="btnact r" /></td>
                               <td><asp:Button ID="Button66" CommandArgument="5,4,M" OnClick="btnFillResidual_Click" Text="20" runat="server" CssClass="btnact r" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">3</td>
                               <td><asp:Button ID="Button67" CommandArgument="1,3,L" OnClick="btnFillResidual_Click" Text="3" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button68" CommandArgument="2,3,M" OnClick="btnFillResidual_Click" Text="6" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button69" CommandArgument="3,3,M" OnClick="btnFillResidual_Click" Text="9" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button70" CommandArgument="4,3,M" OnClick="btnFillResidual_Click" Text="12" runat="server" CssClass="btnact a"/></td>
                               <td><asp:Button ID="Button71" CommandArgument="5,3,H" OnClick="btnFillResidual_Click" Text="15" runat="server" CssClass="btnact a" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Highly Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">2</td>
                               <td><asp:Button ID="Button72" CommandArgument="1,2,M" OnClick="btnFillResidual_Click" Text="2" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button73" CommandArgument="2,2,M" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button74" CommandArgument="3,2,M" OnClick="btnFillResidual_Click" Text="6" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button75" CommandArgument="4,2,H" OnClick="btnFillResidual_Click" Text="8" runat="server" CssClass="btnact b" /></td>
                               <td><asp:Button ID="Button76" CommandArgument="5,2,H" OnClick="btnFillResidual_Click" Text="10" runat="server" CssClass="btnact b" /></td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Almost Nil Chances</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">1</td>
                               <td><asp:Button ID="Button77" CommandArgument="1,1,M" OnClick="btnFillResidual_Click" Text="1" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button78" CommandArgument="2,1,M" OnClick="btnFillResidual_Click" Text="2" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button79" CommandArgument="3,1,H" OnClick="btnFillResidual_Click" Text="3" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button80" CommandArgument="4,1,H" OnClick="btnFillResidual_Click" Text="4" runat="server" CssClass="btnact g" /></td>
                               <td><asp:Button ID="Button81" CommandArgument="5,1,H" OnClick="btnFillResidual_Click" Text="5" runat="server" CssClass="btnact g" /></td>
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
                                  <td style="text-align:left">* Serious loss of reputation which is devastating for trust and respect<br />* Considerable economic loss which can not be restored<br />* More than 5 m3 to sea                                
                                  </td>
                             </tr>
                             <tr >
                                  <td>Likely</td>
                                  <td style="text-align:left">Occurs between 1% and 10% of the time/ cases</td>
                                  <td>Severe</td>
                                  <td style="text-align:left">* Serious loss of reputation that will influence trust and respect for a long time<br />* Lagre economic loss more than US$100,000 that can be restored <br />
                                      * 1 to 5 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>UnLikely</td>
                                  <td style="text-align:left">Occurs between 0.1% and 1% of the time/ cases</td>
                                  <td>Major</td>
                                  <td style="text-align:left">* Reduction of reputation that may influence trust and respect<br />* Economic loss between US$10,000 and US$100,000 which can be restored <br />
                                      * Less than 1 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Highly UnLikely</td>
                                  <td style="text-align:left">Occurs less than 0.1% of the time/ cases</td>
                                  <td>Minor</td>
                                  <td style="text-align:left">* Small reduction of reputation in the short run<br />* Economic loss upto US$10,000 which can be restored <br />
                                      * Sheen on sea : evidance of loss to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Almost Nill Chances</td>
                                  <td style="text-align:left">Never heard within the industry</td>
                                  <td>Negligible</td>
                                  <td style="text-align:left">* No effect on reputation<br />* Negligible economic loss which can be restored <br />
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
               
                <td colspan="6">  <asp:TextBox runat="server" ID="txtACM" Width="99%" TextMode="MultiLine" Height="50px"  CssClass="withborder" ValidationGroup="a1" BackColor="LightYellow"></asp:TextBox></td>
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
            <asp:Button runat="server" ID="btnSaveSingle" Text="Save" OnClick="btnSaveSingle_Click" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
            <asp:Button runat="server" ID="btnCancelTask" Text="Close" OnClick="btnCancelTask_Click" CausesValidation="False" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
          </div>
          </div>
        </center>
       
    </div>
            <div ID="dvNewHazard" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="False">
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
                        <col />
                    </colgroup>
                    <asp:Repeater ID="rptAddHazard" runat="server">
                        <ItemTemplate>
                            <tr class='listitem'>
                                <td style="text-align:center">
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%#Eval("HazardId")%>' OnClick="btnSelectHazard_Click" ImageUrl="~/Modules/HRD/Images/checked-mark-green.png" ToolTip='<%#Eval("HazardName")%>'/>
                                </td>
                               <%-- <td style="text-align:center"><%#Eval("HazardCode")%></td>--%>
                                <td align="left" class='listkey'><%#Eval("HazardName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
          </center>
          <div style="padding:5px">
          <asp:Button runat="server" ID="Button25" Text="Close" OnClick="btnCancelNew_Click" CausesValidation="False" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
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
            <div id="dvTaskVerification" runat="server" style="position: absolute; top: 100px; left: 100px; width: 800px; height: 300px;" visible="False">
            <center>
            <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
            <div style="position:relative;width:800px;  padding :0px; text-align :center;background : white; z-index:0;top:30px; border:solid 5px black;">
            <center >
                <div class="text headerband"> Risk Assessment Verification</div>
                <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                    <tr>
                        <td style="text-align:right;width:100px;"> 
                            Verified By : 
                        </td>
                        <td style="text-align:left;width:150px;">
                            <asp:TextBox ID="txtVerifiedBy" runat="server" CssClass="input_box" MaxLength="50" Width="140px" BackColor="LightYellow"></asp:TextBox>
                        </td>
                        <td style="text-align:right;width:100px;">
                             Rank : 
                        </td>
                        <td style="text-align:left;width:100px;">
                            <asp:DropDownList runat="server" Width="120px" ID="ddlVerifiedRank" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td style="text-align:right;width:100px;">
                            Verified On :
                        </td>
                        <td style="text-align:left;width:150px;">
                              <asp:TextBox runat="server" ID="txtVerifiedOn" CssClass="input_box" MaxLength="15" Width="140px" BackColor="LightYellow"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtVerifiedOn" runat="server" Format="dd-MMM-yyyy" Enabled="True" ></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;padding-left:10px;" colspan="4">
                            <asp:Label ID="lblVerifyMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="text-align:right;padding-right:10px;" colspan="2">
                           <asp:Button ID="btnSaveVerified" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveVerified_Click"  /> &nbsp;
                            <asp:Button ID="btnCloseVerified" runat="server" Text="Close" CssClass="btn" OnClick="btnCloseVerified_Click" />
                        </td>
                    </tr>
                </table>

            </center>
            </div>
            </center>
            </div>
            <div ID="divRAMatrix" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
      
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :0px; text-align :center;background : white; z-index:0;top:0px; border:solid 5px black;">
        <center >
             <div style="float:right;">
                        <asp:ImageButton runat="server" ID="ibCloseRAMatrix" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." CssClass="btn" OnClick="ibCloseRAMatrix_Click" CausesValidation="false" Width="25px"  />
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
                              <asp:Button runat="server" ID="btnCloseRAMatrix" Text="Close" CssClass="btn" Width="100px" OnClick="btnCloseRAMatrix_Click" CausesValidation="false"  />
                         </td>
                     </tr> 
                   </table>

            </center>
            </div>
           </center>
            </div>
        
</ContentTemplate>
        
</asp:TabPanel>
         <asp:TabPanel ID="TabPanel3" runat="server" TabIndex="3">
 <HeaderTemplate>
Office Details
</HeaderTemplate>
      
<ContentTemplate>
          <table width="100%" id="trOfficeComments" runat="server">
              <tr >
                    <td style="text-align:right;width:200px;" >
                        Office Comments : 
                    </td>
                    <td style="text-align:left;"> 
                        <asp:TextBox runat="server" ID="txtOfficeCommnets"  TextMode="MultiLine" Height="100px" width="99%" BackColor="LightYellow" ValidationGroup="Voff2"></asp:TextBox>
                    </td>
                    
                </tr>
              <tr>
                 <td style="text-align:right;width:200px;" runat="server">
                    Approval Authority / Updated on :</td>
                    <td style="text-align:left;" >
                        <asp:Label ID="lblApprovalAuthority" runat="server"></asp:Label>
                    </td>
              </tr>
          </table>
           <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
        <tr>
            <td>&nbsp;<asp:Label runat="server" ID="lblOffCommentMsg" ForeColor="Red" ></asp:Label></td>
            <td style="text-align:right;  display:flex;justify-content:flex-end; padding-right:20px;margin-right:10px;" >
                 <asp:ImageButton ID="btnPreviousTabPanel3" runat="server" ImageUrl="~/Modules/HRD/Images/Prev.png" OnClick="btnPreviousTabPanel3_Click" ToolTip="Go to Previous Tab" style="margin-right:5px;" /> 
                 <asp:Button ID="btnSaveOfficeTab" runat="server" OnClick="btnSaveOfficeTab_Click" style=" padding:3px; border:none;  width:80px;margin-right:5px;" Text="Save" ValidationGroup="Voff2" CssClass="btn"/>
                <asp:Button ID="btnFinalSubmission" runat="server" Visible="false" Text="Finalize" style="margin-right:5px; width:120px;"  CssClass="btn" OnClick="btnFinalSubmission_Click" OnClientClick="return confirm('Click OK to finalize the Risk Assessment Report.');"/>
                 <asp:Button ID="btnExportOfficeTab" runat="server" OnClick="btnExport_Click" style=" padding:3px; border:none;  width:80px;margin-right:5px;" Text="Export"  CssClass="btn" />
                    <asp:Button runat="server" ID="btnCloseOfficeTab" Text="Close" CssClass="btn" Width="100px" OnClick="btnCloseRefresh_Click"  />
            </td>
        </tr> 
            <tr>
                <td colspan="2" >
                    <span style="color:red;"><b> Note : No modification will be possible after report is finalized. </b></span>
                </td>
            </tr>
        </table>
          
</ContentTemplate>
              
</asp:TabPanel>
     </asp:TabContainer>
   
  

   <%-- <div ID="dv_MgmtPlan" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:900px;  height:550px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 10px black;">
        <center >
            <div class="text headerband"><b><asp:HiddenField ID="hfd_MP" runat="server" />RISK ASSESSMENT</b></div>
            <asp:UpdatePanel runat="server" ID="fsads">
            <ContentTemplate>
            <div style="height: 470px; text-align:left; margin:10px; border:none; overflow:hidden; overflow-x:hiden; overflow-y:hidden;">
                <asp:TabContainer  ID="TabContainer1" runat="server" CssClass="ajax__myTab">
                  <asp:TabPanel ID="RiskAssessment" runat="server">
                   <HeaderTemplate>Inherent Risk</HeaderTemplate>
                   <ContentTemplate>
                   <table cellspacing="3" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                    <tr >
                        <td><asp:HiddenField ID="hfHazardId_RA" runat="server" />
                        <asp:HiddenField ID="hfHazards" runat="server" /> 
                        </td>
                        <td style="text-align:center;"><b>Inherent Probability :</b></td>
                    </tr>
                    <tr >
                        <td style="text-align:right"><b>Inherent Impact : </b></td>
                        <td style="text-align:left;">
                            <table cellspacing="0" border="1" cellpadding="3" style="width:95%;border-collapse:collapse;">
                            <tr>
                                <td></td>
                                <td>1: Unlikely</td>
                                <td>2: Possible</td>
                                <td>3: Quite Possible</td>
                                <td>4: Likely</td>
                                <td>5: Very Likely</td>
                           </tr>
                           <tr>
                               <td>1: Negligible</td>
                               <td>
                               <asp:Button ID="Button"  CommandArgument="1,1,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button1" CommandArgument="2,1,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button2" CommandArgument="3,1,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button3" CommandArgument="4,1,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button4" CommandArgument="5,1,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>2: Slight</td>
                               <td><asp:Button ID="Button5" CommandArgument="1,2,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button6" CommandArgument="2,2,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button7" CommandArgument="3,2,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button8" CommandArgument="4,2,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button9" CommandArgument="5,2,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>3: Moderate</td>
                               <td><asp:Button ID="Button10" CommandArgument="1,3,L" OnClick="btnFillRA_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button11" CommandArgument="2,3,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button12" CommandArgument="3,3,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button13" CommandArgument="4,3,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button14" CommandArgument="5,3,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>4: High</td>
                               <td><asp:Button ID="Button15" CommandArgument="1,4,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button16" CommandArgument="2,4,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button17" CommandArgument="3,4,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button18" CommandArgument="4,4,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button19" CommandArgument="5,4,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>5: Very High</td>
                               <td><asp:Button ID="Button20" CommandArgument="1,5,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button21" CommandArgument="2,5,M" OnClick="btnFillRA_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button22" CommandArgument="3,5,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button23" CommandArgument="4,5,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button24" CommandArgument="5,5,H" OnClick="btnFillRA_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>

                       </table>
                        </td>
                    </tr>    
                    <tr>
                    <td colspan="2" >
                          <div style="border:solid 1px #c2c2c2; box-shadow: 2px 2px 2px 2px #d2d2d2; padding:10px; width:95%; margin:10px;min-height:60px; text-align:center;">
                           <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse; text-align:center;">
                              <tr style=" background-color:#A3E0FF">
                                <td >&nbsp;Probability</td>
                                <td >&nbsp;Impact </td>
                                <td >&nbsp;Rating </td>
                              </tr>
                              <tr>
                                <td >&nbsp;<asp:Label ID="lblIR1" runat="server" Font-Bold="true"></asp:Label></td> 
                                <td >&nbsp;<asp:Label ID="lblIR2" runat="server" Font-Bold="true"></asp:Label></td>
                                <td >&nbsp;<asp:Label ID="lblIR3" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                              </tr>
                            </table>
                           </div>
                    </td>
                    </tr>
                    </table>
                   <br />
                    <center>
                    <asp:Button runat="server" ID="Button56" OnClientClick="MoveNext1();return false;" Text="Next >>" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
                    </center>
                   </ContentTemplate>
                  </asp:TabPanel>

                  <asp:TabPanel ID="Management" runat="server">
                  <HeaderTemplate>Management / Execution </HeaderTemplate>
                  <ContentTemplate>
                  <div style="height:410px ; border-top:solid 1px #c2c2c2 ; overflow:hidden; overflow-y:scroll;">
                  <table cellspacing="0" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
                  <tr>
                    <td style="text-align:left;">Routine :<asp:RadioButton runat="server" ID="rad_R_yes" GroupName="r1" Text="Yes" />&nbsp;<asp:RadioButton runat="server" ID="rad_R_no" GroupName="r1" Text="No" Checked="true" /></td>
                  </tr>
                  <tr>
                    <td style="text-align:left;">Standard Control Measures ( Sample Guidelines ) :
                        <div runat="server" id="dv_SCM" style="width:95%;color:Blue;"></div>
                    </td>
                </tr>
                  <tr>
                    <td style="text-align:left;">Standard Control Measures ( Ship Remarks) :</td>
                  </tr>
                  <tr>
                <td style="text-align:left;"><asp:TextBox runat="server" ID="txtSCM" Width="95%" TextMode="MultiLine" Height="80px" CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox></td>
                </tr>
                  <tr>
                    <td style="text-align:left;">Additional Control Measures ( Sample Guidelines ) :
                        <div runat="server" id="dvACM" style="width:95%; color:Blue;"></div>
                    </td>
                </tr>
                  <tr>
                        <td style="text-align:left;">Additional Control Measures ( Ship Remarks) :</td>
                  </tr>
                  <tr>
                <td style="text-align:left;"><asp:TextBox runat="server" ID="txtACM" Width="95%" TextMode="MultiLine" Height="80px" CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox></td>
                </tr>
                  <tr>
                <td style="text-align:left; padding:0px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td style="width:33%; display:inline-block">Agreed Time :</td>
                    <td style=" display:inline-block">PIC Name :</td>
                </tr>
                </table>
                
                </td>
                </tr>
                  <tr>
                <td style="text-align:left; padding:0px;">
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:33%">
                        <asp:TextBox runat="server" ID="txtAgreedTime" MaxLength="50" ValidationGroup="a1" Width="93%" CssClass="withborder" BackColor="#FFFFE0"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox runat="server" ID="txtPN" Width="93%" ValidationGroup="a2" BackColor="#FFFFE0" CssClass="withborder"></asp:TextBox>
                    </td>
                    </tr>
                    </table>
                </td>
                </tr>
                  </table>
                  </div>
                  <center>
                  <asp:Button runat="server" ID="btnNext" OnClientClick="MoveNext2();return false;" Text="Next >>" style=" background-color:#2E9AFE; color:White; border:solid 1px grey;width:100px;"/>
                  </center>
                  </ContentTemplate>
                  </asp:TabPanel>
                  
                  <asp:TabPanel ID="Execution" runat="server">
                    <HeaderTemplate>Residual Risk</HeaderTemplate>
                    <ContentTemplate>
                    <table cellspacing="3" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                    <tr>
                        <td><asp:HiddenField ID="hfResidual" runat="server" /> </td>
                        <td colspan="5" style="text-align:center;"><b>Residual Probability :</b></td>
                    </tr>
                    <tr>
                    <td style="text-align:right;"><b>Residual Impact :</b></td>
                    <td colspan="5" style="text-align:left;">
                       <table cellspacing="0"  border="1" cellpadding="3" style="width:95%;border-collapse:collapse;">
                            <tr>
                                <td></td>
                                <td>1: Unlikely</td>
                                <td>2: Possible</td>
                                <td>3: Quite Possible</td>
                                <td>4: Likely</td>
                                <td>5: Very Likely</td>
                           </tr>
                           <tr>
                               <td>1: Negligible</td>
                               <td><asp:Button ID="Button29"  CommandArgument="1,1,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%; border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button30" CommandArgument="2,1,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button31" CommandArgument="3,1,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button32" CommandArgument="4,1,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button33" CommandArgument="5,1,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>2: Slight</td>
                               <td><asp:Button ID="Button34" CommandArgument="1,2,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button35" CommandArgument="2,2,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button36" CommandArgument="3,2,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button37" CommandArgument="4,2,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button38" CommandArgument="5,2,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>3: Moderate</td>
                               <td><asp:Button ID="Button39" CommandArgument="1,3,L" OnClick="btnFillResidual_Click" Text="Low Risk" runat="server" style="background-color:#80E6B2; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button40" CommandArgument="2,3,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button41" CommandArgument="3,3,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button42" CommandArgument="4,3,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;"/></td>
                               <td><asp:Button ID="Button43" CommandArgument="5,3,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>4: High</td>
                               <td><asp:Button ID="Button44" CommandArgument="1,4,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button45" CommandArgument="2,4,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button46" CommandArgument="3,4,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button47" CommandArgument="4,4,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button48" CommandArgument="5,4,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>
                           <tr>
                               <td>5: Very High</td>
                               <td><asp:Button ID="Button49" CommandArgument="1,5,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button50" CommandArgument="2,5,M" OnClick="btnFillResidual_Click" Text="Medium Risk" runat="server" style="background-color:#FFFFAD; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button51" CommandArgument="3,5,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button52" CommandArgument="4,5,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                               <td><asp:Button ID="Button53" CommandArgument="5,5,H" OnClick="btnFillResidual_Click" Text="High Risk" runat="server" style="background-color:#FF7373; width:100%;border:solid 1px black;" /></td>
                           </tr>

                       </table>
                    </td>
                    
                </tr>    
                <tr>
                <td colspan="6" >
                          <div style="border:solid 1px #c2c2c2; box-shadow: 2px 2px 2px 2px #d2d2d2; padding:10px; width:95%; margin:10px;min-height:60px; text-align:center;">
                           <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse; text-align:center;">
                              <tr style=" background-color:#A3E0FF">
                                <td >&nbsp;Probability </td>
                                <td >&nbsp;Impact </td>
                                <td >&nbsp;Rating </td>
                              </tr>
                              <tr>
                                <td >&nbsp;<asp:Label ID="lblRR1" runat="server" Font-Bold="true"></asp:Label></td> 
                                <td >&nbsp;<asp:Label ID="lblRR2" runat="server" Font-Bold="true"></asp:Label></td>
                                <td >&nbsp;<asp:Label ID="lblRR3" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                              </tr>
                            </table>
                           </div>
                    </td>
                </tr>
                </table>
                    <br />
                    <center>
                        <asp:Button runat="server" ID="Button26" Text="Save Assessment" OnClick="btnSaveMP_Click" ValidationGroup="a1" style=" background-color:#2E9AFE; color:White; border:solid 1px grey;width:130px;"/>
                    </center>
                    </ContentTemplate>
                  </asp:TabPanel>
                </asp:TabContainer>
                <uc1:MessageBox runat="server" ID="lblMsg" />
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </center>
        <div style="padding:3px; text-align:right;">
        <div style="float:left">
         <span style='color:Red'>&nbsp;* - Fields are manditory..</span>
        </div>
        <div style="float:right">
            <asp:Button runat="server" ID="Button25" Text="Close" OnClick="btnCloseMP_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
        <span style="clear:both"></span>
        </div>
        </div>
        </center>
    </div>--%>
   <%--<div ID="dv_NewHazard" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
            <div style="position:relative;width:900px;  height:100px;padding :0px; text-align :center;background : white; z-index:150;top:200px; border:solid 5px black;">
            <center >
            <div class="text headerband"><b>Add New Hazard</b></div>
             <table cellspacing="0" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
             <tr>
             <td style="text-align:right; width:100px;">Hazard Name :</td>
             <td style="text-align:left">
                <asp:TextBox runat="server" id="txtHazardName" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="fsa" runat="server" ControlToValidate="txtHazardName" ErrorMessage="*"></asp:RequiredFieldValidator>
             </td>
             </tr>
             </table>
             <asp:Label runat="server" ID="lblM1" ForeColor="Red"></asp:Label>
             <div style="padding:3px">
               <asp:Button runat="server" ID="Button27" Text="Save" OnClick="btnAddHazard_Click" ValidationGroup="a1" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
                <asp:Button runat="server" ID="Button28" Text="Close" OnClick="btnCloseAddHazard_Click" CausesValidation="false" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
                </div>
            </center>
            </div>
    </center>
    </div>--%>
    </div>
    
   <script type="text/javascript">
       function MoveFirst() {
           window.setTimeout(function () { document.getElementById("__tab_TabContainer1_RiskAssessment").click(); }, 200);
           return false;
       }
   </script>

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
