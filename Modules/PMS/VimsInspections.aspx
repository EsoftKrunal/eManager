<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VimsInspections.aspx.cs" Inherits="VimsInspections" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%--<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <%--<tr><td>
        <hm:HMenu runat="server" ID="menu2" />  
        </td></tr>--%>
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <asp:UpdatePanel runat="server" ID="ss">
        <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid;" width="100%">
            <tr>
                <td style=" background-color:#4371a5; color:White; font-size:12px; padding:3px;">
                    Inspections List
                </td>
            </tr>
            <tr>
                <td style=" text-align:left;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                <td style='width:350px'>
                <div style="height:465px; border-right:solid 1px black; width:100%;oveflow-y:scroll; overflow-x:hidden;" class="scrollbox" onscroll="SetScrollPos(this)" id="dv_Inspections">
                    <asp:Repeater runat="server" ID="rpt_Inspections">
                        <ItemTemplate>
                            <div style="text-align:left; font-size:11px; background-color: <%#(InspNo.ToString().Trim()==Eval("InspectionNo").ToString().Trim())?"#FFFFB8":"#EBEBFF" %>; margin:1px; padding:2px;">
                            <asp:LinkButton runat="server" id="lnkInspection" Text='<%#Eval("InspectionNo")%>' ForeColor="Blue" OnClick="Select_Inspection"></asp:LinkButton>/ 
                             <span style="color:Purple"><%#Common.ToDateString(Eval("PlanDate"))%> </span> / <%#Eval("PortName")%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
                </td>
                <td style="text-align:left">
                <div style="background-color:#e2e2e2;padding:5px; text-align:center; height:20px;">
                         <asp:Label runat="server" ID="lblInsNo1" Font-Bold="true" Font-Size="15px" Text=" "></asp:Label>
                </div>
                <table cellpadding="2" cellspacing="0" border="1" width="100%" style="background-color:#e2e2e2; border-collapse:collapse">
                <tr>
                    <td style="width:50px;text-align:center; font-weight:bold;">Select</td>
                    <td style="width:120px; font-weight:bold;">&nbsp;Deficiency Code</td>
                    <td style="font-weight:bold;">Deficiency</td>
                </tr>
                </table>
                <div style="height:385px; overflow-y:scroll; overflow-x:hidden; border-bottom:solid 1px gray; width:100%;">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <asp:Repeater runat="server" ID="rpt_Observations">
                <ItemTemplate>
                <tr>
                    <td style="width:50px;color:Red; text-align:center;"> 
                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("Qno")%>' OnClick="btnEdit_Click" CausesValidation="false"   />
                    </td>
                    <td style="width:120px;color:Red;">&nbsp;<%#Eval("Qno")%></td>
                    <td style="color:Blue;"><%#Eval("Deficiency")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                 </table>
                </div>
                <div style="margin:3px; width:100%;">
                    <div style="float:left; text-align:left; padding-left:3px;">
                        <asp:Button runat="server" ID="btnImport" Text="Import" CssClass="btn" Width="130px" onclick="btn_OpenImport_Click" />
                    </div>
                    <div style="float:right; text-align:right">
                        <asp:Button runat="server" ID="btnXML" Text="Export to Office" CssClass="btn" Width="130px" onclick="btnXML_Click" style="float:right; margin-right:5px;" />
                        <asp:Button runat="server" ID="btn_AddObservations" Text="Add Observation" CssClass="btn" Width="130px" onclick="btn_AddObservation_Click" style="float:right; margin-right:5px;" />
                        
                    </div>
                </div>
                </td>
                </tr>
                </table>
                </td>
            </tr>
        </table>

        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dbAddObservations" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:415px; padding :3px; text-align :center; border :solid 10px pink; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                    <td style="text-align:center; background-color:#e2e2e2; padding:4px;" colspan="4">
                        <asp:Label runat="server" ID="lblInspectionNo" Font-Size="13px" ForeColor="Blue"></asp:Label>
                        <asp:HiddenField runat="server" ID="hfdId" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:130px">Deficiency Code : </td>
                        <td style="text-align:left"> 
                        <asp:TextBox runat="server" ID="txtQno" Width='100px' style=" background-color:#FFFBC9"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td style="text-align:left;"> &nbsp;
                        <asp:TextBox runat="server" ID="txtQId" ReadOnly="true" Width='80px' style="display:none" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <b> Deficiency</b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtDeficiency" Width='100%' Height="80px" TextMode="MultiLine" style=" background-color:#FFFBC9"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <b>Master Comments</b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtMC" Width='100%' Height="80px" TextMode="MultiLine" style=" background-color:#FFFBC9" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <b> Corrective Actions </b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtC" Width='100%' Height="100px" TextMode="MultiLine" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;" colspan="4">
                            <asp:Label ID="lblMsgAppRej" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn" Width="80px" onclick="btnClose_Click" style="float:right" CausesValidation="false" />
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn" Width="80px" onclick="btnSave_Click" style="float:right; margin-right:5px;" />
                            
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        </div>

        <div style="position:absolute;top:0px;left:0px; height :550px; width:100%;" id="dvImport" runat="server" visible="false" >
        <center>
        <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative;width:400px; height:100px;padding :3px; text-align :center;background : white; z-index:150;top:100px; border:solid 10px pink;">
        <center >
        <b style='font-size:15px'>Inspection File</b>
        <hr />
            <asp:FileUpload runat="server" Width='200px' ID="flp_Upload" /> 
            <br /><br />
            <asp:Button runat="server" ID="btn_ImportInspections" Text="Upload" CssClass="btn" Width="130px" onclick="btn_ImportInspections_Click" />
            <asp:Button runat="server" ID="btnCancelImport" Text="Close" CssClass="btn" Width="130px" onclick="btnCancelImport_Click" />
        </center>
        </div>
        </center>
        </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID='btn_ImportInspections' />
            <asp:PostBackTrigger ControlID='btnXML' />
        </Triggers>
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>  
     </div>
     <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
