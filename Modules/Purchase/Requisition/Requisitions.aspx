<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Requisitions.aspx.cs" Inherits="Requisitions" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

    <%--<meta http-equiv="x-ua-compatible" content="IE=9" />--%>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />    
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowDetails(PrId, VSL, Type) {
            if (typeof (winref) == 'undefined' || winref.closed) {
                if (Type == "Store") {
                    winref = window.open('viewStoreRequest.aspx?PrId=' + PrId + '&VSL=' + VSL, '', '');
                }
                else if (Type == "StoreNew") {
                    winref = window.open('viewStoreRequestNew.aspx?PrId=' + PrId + '&VSL=' + VSL, '', '');
                }
                else {
                    winref = window.open('viewSpareRequest.aspx?PrId=' + PrId + '&VSL=' + VSL, '', '');
                }
            }
            else {
                winref.focus();
            }
        }

    </script>
    <style type="text/css">
    .cls_T
    {
        background-color:#ADEBAD;
    }
    .cls_I
    {
        background-color:#FFCCCC;
    }
    .cls_N
    {
        background-color:LightYellow;
    }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
     
     <div style="text-align: center;font-family:Arial;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; position :relative;">
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress2">
        <ProgressTemplate>
        <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
        <center>
        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
        <img src="../../HRD/Images/loading.gif" alt="loading"> Loading ...
        </div>
        </center>
        </div>
        </ProgressTemplate> 
        </asp:UpdateProgress> 
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate> 
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
        <%--<tr>
          <td style=" background-color:#FFA366">
           <HM:HeaderMenu runat="server" ID="HeaderMenu" />
          </td>
        </tr>
        <tr>
          <td style="text-align:left">
           <hm1:HeaderMenu1 runat="server" ID="HeaderMenu1" />
          </td>
        </tr>--%>
        <tr>
            <td style="  text-align:center; height:30px; font-size:14px; vertical-align:middle; font-weight:bold;" class="text headerband">Requisitions</td>
        </tr>
        <tr>                    
            <td style="border: solid 0px #FFA366;">
                <div style=" padding:7px; text-align:right;background-color:#E0F0FF;">
                <div style="float:left; width:100%;">
                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="text-align: right; font-weight: bold; width:50px;">
                                Fleet :&nbsp;
                            </td>
                            <td style="text-align:left;width:80px;">
                                <asp:DropDownList ID="ddlFleet" runat="server" Width="70px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td style="text-align: right; font-weight: bold; width:50px;">
                                Vessel :&nbsp;
                            </td>
                            <td style="text-align:left;width:100px;">
                                <asp:DropDownList ID="ddlVessel" Width="150px" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; font-weight: bold;width:140px;">
                                Period (From):&nbsp;
                            </td>
                            <td style="text-align:left;width:90px;">
                                <asp:TextBox ID="txtFrom" runat="server" MaxLength="11" Width="80px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; font-weight: bold;width:120px;">
                                Period (To):&nbsp;
                            </td>
                            <td style="text-align:left;width:90px;">
                                <asp:TextBox ID="txtTo" runat="server" MaxLength="11" Width="80px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; font-weight: bold;width:60px;">
                                Type :&nbsp;
                            </td>
                            <td style="text-align:left;width:70px;">
                                <asp:DropDownList ID="ddlType" runat="server" Width="60Px">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Store" Value="Store"></asp:ListItem>
                                    <asp:ListItem Text="Spare" Value="Spare"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; font-weight: bold;width:70px;">
                                Status :&nbsp;
                            </td>
                            <td style="text-align:left;width:160px;">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="150Px">
                                    <asp:ListItem Text="All" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="New Requisition" Value="N" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Sent for RFQ" Value="T"></asp:ListItem>
                                    <asp:ListItem Text="In-Active" Value="I"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" Text="Show" runat="server" Width="70px" 
                                    onclick="btnShow_Click" CssClass="btn" />
                                <asp:Label ID="lblMsg" CssClass="error_msg" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd-MMM-yyyy" TargetControlID="txtFrom" PopupButtonID="txtFrom" PopupPosition="BottomRight" runat="server"></asp:CalendarExtender>
                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd-MMM-yyyy" TargetControlID="txtTo" PopupButtonID="txtTo" PopupPosition="BottomRight" runat="server"></asp:CalendarExtender>
                  </div>
                  <div style="float:right;">
                   </div>
                   <div style="clear:both;"></div>                  
                </div>
                <div style="width: 100%; overflow-y: scroll; overflow-x: hidden; height: 22px;">
                    <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width: 100%;
                        border-collapse: collapse; height: 22px;">
                        <colgroup>
                            <col style="text-align: left" width="150px" />
                            <col style="text-align: left"  width="100px"/>
                            <col style="text-align: left" width="60px" />                            
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="110px" />                            
                            <col style="text-align: left"  />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="170px" />
                            <col width="20px" />
                        </colgroup>
                        <tr class= "headerstylegrid">
                            <td align="left">
                                &nbsp;Vessel
                            </td>
                            <td align="left">
                                &nbsp;Requisition#
                            </td>
                            <td>
                                &nbsp;Type
                            </td>
                            
                            <td align="left">
                                &nbsp;Port Of Supply
                            </td>
                            <td align="left">
                                &nbsp;ETA to Port
                            </td>
                            <td align="left">
                                &nbsp;Remarks
                            </td>
                            <td align="left">
                                &nbsp;Status
                            </td>
                            <td align="left">
                                &nbsp; By/ On
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvscroll_Categories" style="width: 100%; overflow-y: scroll; overflow-x: hidden;
                    height: 385px;" class="scrollbox" onscroll="SetScrollPos(this)">
                    <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width: 100%;
                        border-collapse: collapse;">
                        <colgroup>
                            <col style="text-align: left" width="150px" />
                            <col style="text-align: left"  width="100px"/>
                            <col style="text-align: left" width="60px" />                            
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="110px" />                            
                            <col style="text-align: left"  />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="170px" />
                            <col width="20px" />
                        </colgroup>
                        <asp:Repeater ID="rptManageMenu" runat="server">
                            <ItemTemplate>
                                <tr onclick="ShowDetails('<%#Eval("Id")%>', '<%#Eval("VesselCode") %>', '<%#Eval("Type")%>');" onmouseover="this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor='white';" style="cursor:pointer;">
                                    <td align="left">
                                         &nbsp;<%#Eval("Vessel")%></td>
                                    <td align="left" class='<%#Eval("Row_Status")%>'>
                                        &nbsp;<%#Eval("ReqnNo")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("Type")%></td>                                    
                                    <td align="left">
                                        &nbsp;<%#Eval("Port")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("ETA")%></td>
                                    <td align="left">
                                        <div style="width:98%; height:15px;">&nbsp;<%#Eval("Remarks")%></div>
                                    </td>
                                    <td align="left">
                                        &nbsp;<%#Eval("Status1")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("TransferedBy")%>/ <%# Common.ToDateString(Eval("TransferedOn"))%></td>
                                    <td>
                                        &nbsp;
                                    </td>                                    
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
        </td>
        </tr>
        </table>

     </div>
    
</asp:Content>  

