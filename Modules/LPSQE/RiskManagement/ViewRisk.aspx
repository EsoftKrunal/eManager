<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRisk.aspx.cs" Inherits="RiskManagement_ViewRisk" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
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
        
        .verticaltext
{
      writing-mode:tb-rl;
    -webkit-transform:rotate(90deg);
    -moz-transform:rotate(90deg);
    -o-transform: rotate(90deg);
    white-space:nowrap;
    display:block;
    bottom:0;
    width:20px;
    height:20px;
}
    </style>--%>
    <title>Planned Maintenance System : Risk Analysis > Add Risk </title>
    <script src="../eReports/js/jquery.min.js" type="text/javascript"></script>
    <script src="../eReports/js/KPIScript.js" type="text/javascript"></script>
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    
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
        .ajax__myTab .ajax__tab_tab { height: 13px; padding: 4px; margin: 0; }
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
</head>
<body style="margin:0px; font-family:WOL_Reg, 'Segoe UI', Tahoma, Helvetica, sans-serif; font-size:13px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
   <%-- <div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; border:solid 1px #0099CC">
    <tr style="background-color:#0099CC; color:White;">
    <td style="padding:8px; text-align:center"><b>Risk Management</b></td>
    </tr>
    </table>

     <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
        <tr>
    <td style="text-align:right">Vessel : </td>
    <td style="width:200px"><asp:Label runat="server" ID="lblVesselName" Width="150px"></asp:Label> </td>
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
    <td style="text-align:left"><b>Risk Analysis : <asp:CheckBox runat="server" ID="chkAlt"  Text="Alternate methods of work considered (if yes provide details)"/></b></td>
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
                        <col style="width:17px" />
                    </colgroup>
                    <tr>
                        <td style="vertical-align:middle;">&nbsp;Routine</td>
                        <td style="text-align:left; vertical-align:middle;">&nbsp;Hazards Identified
                        
                        </td>                        
                        
                    </tr>
            </table>
        </div>                                                                                                                                                                    <div class="ScrollAutoReset" style="height:400px; overflow-x:hidden;overflow-y:scroll; border:solid 1px #c2c2c2" id="dv001_1">
                    <table cellspacing="0" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;" bordercolor="#F0F0F5">
                       <colgroup>
                            <col style="width:60px;" />
                            <col />
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
    <tr id="trClosure" runat="server" visible="false">
      <td style="padding-top:5px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
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
        </td>
    </tr>
    </table>
    </div>
    <div style="text-align:center; padding:5px;">        
        <asp:Button ID="btnClosure" runat="server" OnClick="btnClosure_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" Text="Approve" ValidationGroup="v1" />
        <asp:Button ID="btnClose" runat="server" CausesValidation="false" OnClientClick="javascript:self.close();" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" Text="Close" />
    </div>
    <div style="padding:0px 0px 0px 10px;color:Red; font-weight:bold">
        <asp:Label runat="server" ID="msg1" ></asp:Label>
    </div>
     <div ID="dv_Closure" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
   
   <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  height:250px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
            <div class="box3"><b>Approval</b></div>

               <div class="dvScrolldata" style="height: 200px; text-align:center;">
                <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                <tr>
                <td style="text-align:left;">Approval Comments :</td>
                </tr>
                <tr>
                <td style="text-align:left;">
                    <asp:TextBox runat="server" ID="txtOfficeComments" TextMode="MultiLine" Width="99%" Height="150px"></asp:TextBox>                    
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtOfficeComments" ErrorMessage="*" ValidationGroup="c1" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                </table>
                <asp:Label ID="lblMsg_Closure" runat="server" ForeColor="Red" ></asp:Label>.
                </div>
        </center>
        <div style="padding:3px">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" style=" border:none; color:White; background-color:#2E9AFE; width:100px;" Text="Save" ValidationGroup="c1" />
        <asp:Button runat="server" ID="btnCloseClosure" Text="Close" OnClick="btnCloseClosure_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
        </div>
        </center>
   
   </div>--%>
   <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; border:solid 0px #0099CC">
    <tr style="background-color:#0099CC; color:White;">
    <td style="padding:8px; text-align:center"><b>Risk Management - [ <asp:Label runat="server" ID="lblRefNo"></asp:Label> ]</b></td>
    </tr>
    <tr><td style="text-align:center"><asp:Label runat="server" ID="lblEventName" ForeColor="Brown" Font-Bold="true" Font-Size="Large"></asp:Label></td></tr>
    </table>
    <asp:TabContainer  ID="TabContainer2" runat="server" CssClass="ajax__myTab">
        <asp:TabPanel ID="TabPanel1" runat="server">
        <HeaderTemplate>Risk Description</HeaderTemplate>
        <ContentTemplate>
            <table cellpadding="4" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                 <tr>
                <td style="text-align:right;width:150px;">Vessel : </td>
                <td style=""><asp:Label runat="server" ID="lblVesselName" Width="150px"></asp:Label> </td>
                
                <td style="text-align:right;width:150px;">Date of Event: </td>
                <td><asp:TextBox runat="server" ID="txtEventDate" CssClass="input_box" MaxLength="15" Width="90px" BackColor="#FFFFE0"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEventDate" runat="server" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                </td>
                
                <td style="text-align:right;width:150px;">Created By :</td>
                <td style="text-align:left; "><asp:TextBox runat="server" ID="txtCreatedBy" CssClass="input_box" MaxLength="50" Width="150px" BackColor="#FFFFE0"></asp:TextBox> </td>
                
                <td style="text-align:right;width:150px;">Position : </td>
                <td><asp:TextBox runat="server" ID="txtPosition" CssClass="input_box" MaxLength="50"  Width="150px" BackColor="#FFFFE0"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="text-align:right;">Head of Department : </td>
                <td style="text-align:left;"><asp:TextBox runat="server" ID="txtHOD" CssClass="input_box" MaxLength="50"  Width="150px" BackColor="#FFFFE0"></asp:TextBox> </td>

                <td style="text-align:right;">Master Name : </td>
                <td><asp:TextBox runat="server" ID="txtMaster" CssClass="input_box" MaxLength="50" Width="150px"></asp:TextBox> </td>
                
                <td style="text-align:right;">Safety Officer : </td>
                <td><asp:TextBox runat="server" ID="txtSO" CssClass="input_box" MaxLength="50"  Width="150px" BackColor="#FFFFE0"></asp:TextBox> </td>

                <td style="text-align: right;">&nbsp;</td>
                <td>&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="4" style="text-align:left;"><b>Risk Description :</b></td>
                    <td colspan="4" style="text-align:left;"><b>Alternate methods of work considered&nbsp; : </b><asp:DropDownList ID="ddlAlt" runat="server"><asp:ListItem Text="< Select >" Value="0"></asp:ListItem><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem></asp:DropDownList>&nbsp; <span> ( if yes provide details below ) </span></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:left">
                     <asp:TextBox runat="server" ID="txtRiskDescr" CssClass="input_box" TextMode="MultiLine"  Width="99%" Height="350px" BackColor="#FFFFE0"></asp:TextBox>
                    </td>
                    <td colspan="4" style="text-align:left; vertical-align:top;">
                    <asp:TextBox runat="server" ID="txtDetails" CssClass="input_box" TextMode="MultiLine"  Width="99%" Height="350px" Visible="false" BackColor="#FFFFE0" ></asp:TextBox>
                    </td>
                 </tr>
                </table>
        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server">
        <HeaderTemplate>Risk Assessment</HeaderTemplate>
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
            <tr>
                <td>
                   <div style="height:65px; background-color:#0099CC; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2">  
                        <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="0" style="width:100%;border-collapse:collapse;height:65px; color:White;">
                                <colgroup>
                                    <col style="width:60px;" />
                                    <col />
                                    <col style="width:110px;" />
                                    <col style="width:110px;" />
                                    <col style="width:110px;" />
                                    <col style="width:110px;" />
                                    <col style="width:110px;" />
                                    <col style="width:110px;" />
                                    <%--<col style="width:180px;" />--%>
                                    <col style="width:17px" />
                                </colgroup>
                                 <tr>
                                    <td style="vertical-align:middle;">&nbsp;</td>
                                    <td style="vertical-align:middle;">&nbsp;</td>
                                    <td colspan="3" style=" text-align:center;">&nbsp;Inherent</td>
                                    <td colspan="3" style="text-align:center;">&nbsp;Residual</td>
                                    <%--<td style="text-align:center; vertical-align:middle;">&nbsp;</td>--%>
                                    <td>&nbsp;</td>
                                   </tr> 
                                   <tr>
                                    <td style="vertical-align:middle;">&nbsp;Routine</td>
                                    <td style="text-align:left; vertical-align:middle;">&nbsp;Hazards Identified                                    
                                    </td>
                                    <td style=" text-align:center;">&nbsp;Probability</td>
                                    <td style=" text-align:center;">&nbsp;Impact</td>
                                    <td style=" text-align:center;">&nbsp;Rating</td>
                                    <td style=" text-align:center;">&nbsp;Probability</td>
                                    <td style=" text-align:center;">&nbsp;Impact</td>
                                    <td style=" text-align:center;">&nbsp;Rating</td>                        
                                    <%--<td style="text-align:center; vertical-align:middle;">&nbsp;Action</td>--%>
                                    <td>&nbsp;</td>
                                </tr>
                        </table>
                    </div>
      
                            <div class="ScrollAutoReset" style="height:350px; overflow-x:hidden;overflow-y:scroll; border:solid 1px #c2c2c2" id="dv001_1">
                                <table cellspacing="0" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;" bordercolor="#F0F0F5">
                                   <colgroup>
                                        <col style="width:60px;" />
                                        <col />
                                        <col style="width:110px;" />
                                        <col style="width:110px;" />
                                        <col style="width:110px;" />
                                        <col style="width:110px;" />
                                        <col style="width:110px;" />
                                        <col style="width:110px;" />
                                        <%--<col style="width:180px;" />--%>
                                        <col style="width:17px" />
                                    </colgroup>
                                    <asp:Repeater ID="rptRisk" runat="server">
                                        <ItemTemplate>
                                            <tr >
                                                <td style="text-align:center"><%#((Eval("ROUTINE").ToString()=="Y")?"Yes":"No")%></td>
                                                <td style="text-align:left">
                                                <span style="color:Red;">&nbsp;<%#Eval("HazardName")%></span></td>
                                                <td style="text-align:center;"><%#getFullText(Eval("LIKELIHOOD").ToString(),"L")%></td>
                                                <td style="text-align:center;"><%#getFullText(Eval("CONSEQUENCES").ToString(), "C")%></td>
                                                <td style="text-align:center;background-color:<%#getColor(Eval("RISKRANK").ToString())%>"><%#getFullText(Eval("RISKRANK").ToString(), "R")%></td>
                                                <td style="text-align:center;"><%#getFullText(Eval("Re_LIKELIHOOD").ToString(),"L")%></td>
                                                <td style="text-align:center;"><%#getFullText(Eval("Re_CONSEQUENCES").ToString(), "C")%></td>
                                                <td style="text-align:center;background-color:<%#getColor(Eval("Re_RISKRANK").ToString())%>"><%#getFullText(Eval("Re_RISKRANK").ToString(), "R")%></td>
                                                <%--<td style="text-align:center">
                                                <asp:LinkButton ID="btnSelect" Text="Management/Exec Plan"  runat="server" OfficeId='<%#Eval("OfficeId")%>' RiskId='<%#Eval("RiskId")%>' Visible='<%#(RiskStatus=="O")%>' HazardId='<%#Eval("HazardId")%>' CommandArgument='<%#Eval("SRKey")%>' OnClick="ShowMangementPlan" ></asp:LinkButton>&nbsp;<span id="Span1" style="color:Red; font-weight:bold;" runat="server" visible='<%#(Eval("STD_CONTROL_MESASRUES").ToString().Trim()=="")%>'>*</span>
                                                </td>--%>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                   <div style="text-align:center; width:100%;">
                   <table cellpadding="0" cellspacing="0" border="0" style="border-collapse:collapse;" width="100%">
                   <tr>
                   <td style="width:270px">
                   <div style="border:solid 1px #c2c2c2; box-shadow: 2px 2px 2px 2px #d2d2d2; padding:10px; width:250px; margin:10px;min-height:110px;">
                    <span style=" font-size:15px; text-align:left;"><b>Inherent</b></span>
                    <hr />
                    <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;text-align:right;">
                    <tr>
                        <td style="width:50%">&nbsp;Probability :</td>
                        <td >&nbsp;<asp:Label ID="lbl_In_Probability" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td >&nbsp;Impact :</td>
                        <td >&nbsp;<asp:Label ID="lbl_In_Impact" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td >&nbsp;Rating :</td>
                        <td id="tdInRating" runat="server">&nbsp;<asp:Label ID="lbl_In_Rating" runat="server"></asp:Label></td>
                    </tr>   
                    </table>
                   </div>
                   </td>
                   <td  style="width:270px">
                   <div style="border:solid 1px #c2c2c2; box-shadow: 2px 2px 2px 2px #d2d2d2; padding:10px; width:250px; margin:10px;min-height:110px;">
                   <span style=" font-size:15px; text-align:left;"><b>Residual</b></span>
                   <hr />
                   <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse; text-align:right;">
                      <tr>
                        <td style="width:50%">&nbsp;Probability :</td>
                        <td >&nbsp;<asp:Label ID="lbl_Re_Probability" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                        <td >&nbsp;Impact :</td>
                        <td >&nbsp;<asp:Label ID="lbl_Re_Impact" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                        <td >&nbsp;Rating :</td>
                        <td id="tdReRating" runat="server">&nbsp;<asp:Label ID="lbl_Re_Rating" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                   </div>
                   </td>
                   <td>
                   <div style="border:solid 1px #c2c2c2; box-shadow: 2px 2px 2px 2px #d2d2d2; padding:10px; margin:10px; min-height:110px;">
                    <span style=" font-size:15px; text-align:left;"><b>Approval</b></span>
                    <hr />
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;" id="trClosure" runat="server" visible="false">
                     <tr style="background-color:#0099CC; color:Black;">
                        <td style="padding:0px; text-align:left">
                        <div class="box1" style="padding-left:10px; text-align:left">
                         Approved By / On : ( <asp:Label runat="server" ID="lblCommentsByOn" ></asp:Label> )
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
                   </td>
                   </tr>
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
    </table>
                </div>
        </ContentTemplate>
        </asp:TabPanel>
     </asp:TabContainer>
   
    <div style="text-align:center; padding:10px;">
        <div style="float:left;"><asp:Label runat="server" ID="msg1" ></asp:Label></div>
        <div style="float:right;">
        <asp:Button ID="btnClosure" runat="server" OnClick="btnClosure_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" Text="Approve" ValidationGroup="v1" />
        <asp:Button ID="btnClose" runat="server" CausesValidation="false" OnClientClick="javascript:self.close();" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" Text="Close" />
        <div style="float:left;">
    </div>

   <%-- <div ID="dv_MgmtPlan" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:900px;  height:550px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 10px black;">
        <center >
            <div class="box3"><b><asp:HiddenField ID="hfd_MP" runat="server" />RISK ASSESSMENT</b></div>
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
                    <asp:Button runat="server" ID="Button56" OnClientClick="MoveNext1();return false;" Text="Next >>" style=" background-color:#2E9AFE; color:White; border:solid 1px grey;width:100px;"/>
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
                <asp:Label ID="lblMsg_MP" runat="server" ForeColor="Red" ></asp:Label>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </center>
        <div style="padding:3px; text-align:right;">
        <div style="float:left">
         <span style='color:Red'>&nbsp;* - Fields are manditory.</span>
        </div>
        <div style="float:right">
            <asp:Button runat="server" ID="Button25" Text="Close" OnClick="btnCloseMP_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
        <span style="clear:both"></span>
        </div>
        </div>
        </center>
    </div>--%>
    <div ID="dv_Closure" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
   
   <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  height:250px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
            <div class="box3"><b>Approval</b></div>

               <div class="dvScrolldata" style="height: 200px; text-align:center;">
                <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                <tr>
                <td style="text-align:left;">Approval Comments :</td>
                </tr>
                <tr>
                <td style="text-align:left;">
                    <asp:TextBox runat="server" ID="txtOfficeComments" TextMode="MultiLine" Width="99%" Height="150px"></asp:TextBox>                    
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtOfficeComments" ErrorMessage="*" ValidationGroup="c1" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                </table>
                <asp:Label ID="lblMsg_Closure" runat="server" ForeColor="Red" ></asp:Label>.
                </div>
        </center>
        <div style="padding:3px">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" style=" border:none; color:White; background-color:#2E9AFE; width:100px;" Text="Save" ValidationGroup="c1" />
        <asp:Button runat="server" ID="btnCloseClosure" Text="Close" OnClick="btnCloseClosure_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
        </div>
        </div>
        </center>
   
   </div>
    <script type="text/javascript">
        function MoveFirst() {
            window.setTimeout(function () { document.getElementById("__tab_TabContainer1_RiskAssessment").click(); }, 200);
            return false;
        }
   </script>
    </form>
</body>
</html>
