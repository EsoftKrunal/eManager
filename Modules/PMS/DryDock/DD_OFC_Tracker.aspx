<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_OFC_Tracker.aspx.cs" Inherits="DD_OFC_Tracker"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>eMANAGER</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <%--<title>Planned Maintenance System : Dry Docking > DD Tracker </title>--%>
    <style type="text/css">
        .yellowcolor
        {
            background-color:yellow;
            color:Black;
        }
        .redcolor
        {
            background-color:#ff704d;
            color:Black;
        }
        .greencolor
        {
            background-color:#00cc44;
            color:Black;
        }
    </style>
     <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
  </head>
<body>
    <form id="form1" runat="server">
            <%--<div class="text headerband">
            Planned Maintenance System : Dry Docking > DD Tracker
        </div>--%>
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     
        <div style="text-align:right;">
        <table width="100%" cellpadding="3" border="0">
        <tr>
        <td style="width:60px; text-align:left; width:80px;">Fleet :</td>
        <td style="text-align:left;width:80px; " >
            <asp:DropDownList runat="server" id="ddlFleet" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged" Width="80px"></asp:DropDownList>
        </td>
        <td style="width:60px; text-align:left;width:80px; text-align:right;">Due In :</td>
        <td style="text-align:left"> 
        <asp:DropDownList runat="server" id="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged" Width="80px"></asp:DropDownList>
        </td>
        <td>
            <asp:Label runat="server" ID="lblCurryearCount" Font-Bold="true"></asp:Label>
        </td>
        <td>
            <asp:Label runat="server" ID="lblNextyearCount" Font-Bold="true"></asp:Label>
        </td>
        <td style="text-align:right">
            <asp:Button runat="server" ID="btnAddNewVessel" Text=" + Add Vessel" OnClick="btnAdd_Click" style="color:White; background-color:Red;" Width="120px" />
        </td>
        </tr>
        </table>
        
        </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        <td style=" text-align :left; vertical-align : top;"> 
             <div class="dvScrollheader">  
                           <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:200px;" />
                                    <col style="width:100px;" />
                                    <col style="width:180px" />
                                    <col style="width:80px;" />
                                    <col style="width:90px;" />
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                    <col/>
                                </colgroup>
                                <tr class= "headerstylegrid">
                                    <td style="text-align:left;">Vessel Name</td>
                                    <td style="text-align:center;">Last DD Type</td>
                                    <td style="text-align:center;">Last DD Period</td>
                                    <td style="text-align:center;">Next Due In</td>
                                    <td style="text-align:center;">Next DD Type</td>
                                    <td style="text-align:center;">DD Docket</td>
                                    <td style="text-align:center;">Docking Specs</td>
                                    <td style="text-align:center;">Quotation</td>
                                    <td style="text-align:center;">Yard Confirmation</td>
                                    <td style="text-align:center;">Pending Action</td>
                                </tr>
                            </table>
                            </div>
             <div style="HEIGHT: 315px;"  class="dvScrolldata">
                           <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                 <colgroup>
                                    <col style="width:200px;" />
                                    <col style="width:100px;" />
                                    <col style="width:180px" />
                                    <col style="width:80px;" />
                                    <col style="width:90px;" />
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                    <col/>
                                </colgroup>
                                <asp:Repeater ID="rptDocket" runat="server">
                                    <ItemTemplate>
                                           <tr>
                                                <td style="text-align:left;"><%#Eval("VesselName")%></td>
                                                <td style="text-align:left;"><%#Eval("LastDDType")%></td>
                                                <td style="text-align:center;"><%#Common.ToDateString(Eval("LASTDDSTARTDATE"))%> - <%#Common.ToDateString(Eval("LASTDDENDDATE"))%></td>
                                                <td style="text-align:center;"><%#Convert.ToDateTime(Eval("NEXTDUEDATE")).ToString("MMM-yyyy")%></td>
                                                <td style="text-align:center;"><%#Eval("NextDDType")%></td>
                                                <td style="text-align:center;" class='<%#Eval("COLOR1")%>'>Due on <%#Common.ToDateString(Eval("DD_DUE_CREATED"))%></td>
                                                <td style="text-align:center;" class='<%#Eval("COLOR2")%>'>Due on <%#Common.ToDateString(Eval("DD_DUE_SPECS"))%></td>
                                                <td style="text-align:center;" class='<%#Eval("COLOR3")%>'>Due on <%#Common.ToDateString(Eval("DD_DUE_QUOTE"))%></td>
                                                <td style="text-align:center;" class='<%#Eval("COLOR4")%>'>Due on <%#Common.ToDateString(Eval("DD_DUE_YARD"))%></td>
                                                <td style="text-align:left; color:red"><i>
                                                           <span runat="server" visible='<%#Eval("Stage").ToString()=="1"%>'>DD Docket Not Created</span> 
                                                           <span runat="server" visible='<%#Eval("Stage").ToString()=="2"%>'>Docking Specs not ready</span> 
                                                           <span runat="server" visible='<%#Eval("Stage").ToString()=="3"%>'>Quotation analysis not done</span> 
                                                           <span runat="server" visible='<%#Eval("Stage").ToString()=="4"%>'>Yard not confirmed</span>
                                                           </i>
                                                </td>
                                            </tr>
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>
            </div>
        </td> 
        </tr>
        </table>
        <div style="padding:4px; text-align:center; font-size:15px; display:none;">
        <b>Tracker Explanation</b>
        </div>
        <table width="100%" border="1" cellpadding="3" cellspacing="0" style="display:none;">
        <tr>
            <td><b>DD Docket :</b> Initial drydock docket must be published before 1 year DD due date.</td>
            <td><b>Docking Specs :</b> Final docking specs must be approved by Technical Manager 5 month before DD due date.</td>
        </tr>
        <tr>
            <td><b>Quote Analysis :</b> Quote analysis must be approved by Technical Manager 3 months before DD due date.</td>
            <td><b>Yard Confirmation :</b> Yard should be confirmed subject to owner approval one month before DD due date.</td>
        </tr>
        </table>
         <table width="" cellpadding="5" style="display:none;">
        <tr>
            <td style="background-color:Yellow">Task Window Open</td>
            <td style="background-color:#00cc44">Compliance</td>
            <td style="background-color:#ff704d">Non Compliance</td>
        </tr>
        </table>
    </div>
    <%-- Send Mail --%>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_AddVessel" runat="server" visible="false">
         <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
            <div style="position: relative; width: 450px;padding: 0px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px black;">
            <div class="text headerband" >Add Vessel</div>
                 <table cellspacing="0" rules="none" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:right; width:150px; ">Select Vessel :</td>
                        <td style="text-align:left">
                            <asp:DropDownList runat="server" ID="ddlVessel"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlVessel" ErrorMessage="*" ValidationGroup="v001"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td style="text-align:right; width:150px;">Next Due Dt :</td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txtNextDueDt" Width="90px" ValidationGroup="v001"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="Fsdfa" ControlToValidate="txtNextDueDt" ErrorMessage="*" ValidationGroup="v001"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                     <tr>
                        <td style="text-align:right; width:150px;">Last Drydock Type :</td>
                        <td style="text-align:left">
                            <asp:DropDownList runat="server" ID="ddlDDType"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlDDType" ErrorMessage="*" ValidationGroup="v001"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align:right; width:150px; ">Last DD Date :</td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txtStdt" Width="90px"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtStdt" ErrorMessage="*" ValidationGroup="v001"></asp:RequiredFieldValidator>
                            -
                            <asp:TextBox runat="server" ID="txtEndDt" Width="90px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEndDt" ErrorMessage="*" ValidationGroup="v001"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   
                    
                    <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg_Mail" ForeColor="Red" runat="server"></asp:Label>
                        <asp:Button runat="server" ID="btnSaveVessel" Text="Save Vessel" CausesValidation="true" ValidationGroup="v001" OnClick="btnSaveVessel_Click"  style="color:White; background-color:Red;" Width="100px"  />
                        <asp:Button runat="server" ID="btnCloseVesssel" Text="Close" OnClick="btnCloseVesssel_Click" CausesValidation="false" style="color:White; background-color:Red;" Width="100px" />

                        <asp:CalendarExtender runat="server" TargetControlID="txtStdt" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDt" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtNextDueDt" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                    </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
</form>
</body>
</html>
