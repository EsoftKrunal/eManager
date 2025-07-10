<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Export.aspx.cs" Inherits="EventManagement_Export" MasterPageFile="~/MasterPage.master" Title="EMANAGER" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="Menu_Event.ascx" TagName="leftmenu" TagPrefix="mtm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="font-family:Arial;font-size:12px;">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         <div class="text headerband">
             Risk Management
         </div>
       <mtm:leftmenu runat="server" ID="LefuMenu1" />
        <div class="box_withpad" style="min-height:450px">
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        <td style=" text-align :left; vertical-align : top;"> 
            <div>
            <table style="width :100%" cellpadding="0" cellspacing="0" border="0" height="465px">
                    <tr>
                    <td style="text-align:center; padding:10px; background-color:#dddddd">
                        <table width="100%">
                            <tr>
                                <td>
                                  <b>Vessel :&nbsp;</b>
                                </td>
                                <td><asp:DropDownList ID="ddlVessel" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged" runat="server" Width="250px" ></asp:DropDownList></td>
                            </tr>
                        </table>
                        
                    </td>

                    </tr>
                    <tr>  
                    <td>
                      
                         <div style="border:none;">
                            <div class="dvScrollheader">  
                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <%--<col style="width:50px;" />--%>
                                        <col style="width:150px;" />
                                        <col />                     
                                        <col style="width:70px;" />
                                        <col style="width:150px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:20px;" />
                                    </colgroup>
                                    <tr class= "headerstylegrid">
                                        <%--<td style="text-align:center; vertical-align:middle;">Select</td>--%>
                                        <td style="text-align:left; vertical-align:middle;">Template Code</td>
                                        <td style="text-align:left;">Event Name</td>
                                        <td style="text-align:left; vertical-align:middle;">Status</td>                                                        
                                        <td style="text-align:left; vertical-align:middle;">Approved By</td>                                                        
                                        <td style="text-align:left;">Approved On</td>
                                        <td style="text-align:left; vertical-align:middle;">Sent By</td>                                                        
                                        <td style="text-align:left;">Sent On</td>
                                        <td style="text-align:left; vertical-align:middle;">Ack Recd</td>                                                        
                                        <td style="text-align:left;">Ack Recd On</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="dvScrolldata" style="height: 365px;">
                            <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <%--<col style="width:50px;" />--%>
                                    <col style="width:150px;" />
                                    <col />                     
                                    <col style="width:70px;" />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:20px;" />
                                </colgroup>
                                <asp:Repeater ID="rptTemplate" runat="server">
                                    <ItemTemplate>
                                        <tr >
                                            <%--<td style="text-align:center">
                                                <asp:CheckBox ID="chkSelect" CssClass='<%#Eval("TemplateId")%>' runat="server" />
                                            </td>--%>
                                            <td align="left"><%#Eval("TemplateCode")%></td>
                                            <td align="left"><%#Eval("EventName")%></td>
                                            <td align="left"><%#((Eval("Status").ToString()=="A")?"Approved":"Not Approved")%></td>
                                            <td align="left"><%#Eval("ApprovedBy")%></td>
                                            <td align="center"><%# Common.ToDateString(Eval("ApprovedOn"))%></td>
                                            <td align="left"><%#Eval("SentBy")%></td>
                                            <td align="center"><%# Common.ToDateString(Eval("SentOn"))%></td>
                                            <td align="center"><%#Eval("AckRecd")%></td>
                                            <td align="center"><%# Common.ToDateString(Eval("AckRecdOn"))%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                         </div>
                         <div style="padding:3px; text-align:right; background-color:#FFFFEB">
                          <asp:Label ID="lblMsg" ForeColor="Red" runat="server" style="float:left ; font-size:15px;" ></asp:Label>
                          <asp:Button runat="server" ID="btnDownload" Text="Send to Vessel" OnClick="btnDownload_Click" CausesValidation="false" style="   border:solid 1px grey;width:130px;" CssClass="btn"/>
                         </div>
                    </td>
                    </tr>
                    </table>
            </div>
        </td>
        </tr>
        </table>
        </div>
    </div>
 </asp:Content>
