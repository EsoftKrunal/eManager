<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_TrainingManagement.aspx.cs" Inherits="emtm_MyProfile_Emtm_Profile_TrainingManagement" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master" %>
<%@ Register Src="~/Modules/eOffice/MyProfile/Profile_TrainingMainMenu.ascx" TagName="Profile_TrainingMainMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
        function ViewPlaning(pid) {
            document.getElementById("txtHidden").setAttribute("value", pid);
            document.getElementById("btnHidden").click();
            return false;
        }
        function OpenPlanningDetails(TPId) {
            window.open('../MyProfile/PopUp_Profile_TrainingDetails.aspx?TrainingPlanningId=' + TPId, '', '');
        }
        
        function Refresh() {
            var btn = document.getElementById("btnRefresh");
            btn.click();
        }
        function print() {
            var yearobj = document.getElementById('ddlYear');
            var year = yearobj.options[yearobj.selectedIndex].value; 

            window.open('../../Reporting/TrainingReport.aspx?Year=' + year + '&EmpId=' + <%= Common.CastAsInt32(Session["ProfileId"])%> , '', '');
        }
    </script>
    

    <div style="font-family:Arial;font-size:12px;">
   
         <asp:UpdatePanel runat="server" ID="up1">
         <ContentTemplate>
            <asp:Button runat="server" ID="btnHidden" OnClick="btnHidden_Click" style="display:none;" />
            <asp:Button runat="server" ID="btnRefresh" OnClick="btnRefresh_Click" style="display:none;" />
            <asp:TextBox runat="server" ID="txtHidden" style="display:none;"/>
            <table width="100%" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             <div>
                                <uc1:Profile_TrainingMainMenu ID="Emtm_Profile_TrainingMainMenu1" runat="server" />
                             </div> 
                            </td>
                        </tr>
                        </table> 
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                        <td style=" height:25px; padding-left:20px; ">
                            <div style=";padding-left:10px;">
                                 Select Year : <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" onselectedindexchanged="ddlYear_SelectedIndexChanged" Font-Size="13px" Height="20px"></asp:DropDownList>
                            </div>
                           
                            <div style="padding-right:10px;">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn" Width="90px" Visible="false" onclick="btnBack_Click" />
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn" Width="90px" OnClientClick="javascript:print();" />
                                </div>
                            
                        </td>
                        </tr>
                        </table>
                        
                        <asp:Panel ID="pnlYear" Visible="true" runat="server">
                        <div id="divHeader" runat="server" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; ">
                        <table cellpadding="2" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center;">
                        <tr class="_tr_tableheader">
                            <td style="width:100px">Month</td>                            
                            <td style="padding-left:5px;">
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                     <tr>
                                         <td style="background-color:#FFFFCC; border: 1px solid black; width:160px; height:5px;"></td>
                                         <td align="left" style="height:5px;">Planned Training</td>
                                         <td style="background-color:#51B751; border: 1px solid black; width:160px; height:5px;"></td>
                                         <td align="left" style="height:5px;">Training Completed</td>
                                         <td style="background-color:#FF5F5F; border: 1px solid black; width:160px; height:5px;"></td>
                                         <td align="left" style="height:5px;">Training Cancelled</td>
                                     </tr>
                                </table> 
                            </td>
                            <td style="width:25px">&nbsp</td>
                        </tr>
                        </table>
                        </div>
                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center;">
                        <tr class="_tr_tabledata">
                        <td class="_tr_month" style="width:100px">
                         <asp:LinkButton ID="lbJan" CommandArgument="1" runat="server" onclick="lbJan_Click"></asp:LinkButton>                            
                        </td>                        
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litJan" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbFeb" CommandArgument="2" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq2" CommandArgument="2" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litFeb" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbMar" CommandArgument="3" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq3" CommandArgument="3" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litMar" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbApr" CommandArgument="4" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq4" CommandArgument="4" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litApr" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbMay" CommandArgument="5" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                <asp:LinkButton ID="lbReq5" CommandArgument="5" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litMay" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbJun" CommandArgument="6" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq6" CommandArgument="6" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litJun" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbJul" CommandArgument="7" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq7" CommandArgument="7" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litJul" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbAug" CommandArgument="8" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq8" CommandArgument="8" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litAug" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbSep" CommandArgument="9" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                <asp:LinkButton ID="lbReq9" CommandArgument="9" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litSep" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbOct" CommandArgument="10" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq10" CommandArgument="10" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litOct" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbNov" CommandArgument="11" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                 <asp:LinkButton ID="lbReq11" CommandArgument="11" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litNov" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        <tr class="_tr_tabledata">
                        <td class="_tr_month">
                         <asp:LinkButton ID="lbDec" CommandArgument="12" runat="server" onclick="lbJan_Click" ></asp:LinkButton>                            
                        </td>
                            <%--<td class="_tr_month">
                                <asp:LinkButton ID="lbReq12" CommandArgument="12" runat="server" onclick="lbReq_Click"></asp:LinkButton></td>--%>
                        <td style="text-align:left; vertical-align:top;">
                        <asp:Literal ID="litDec" runat="server"></asp:Literal>
                        </td>
                        <td style="width:25px">&nbsp</td>
                        </tr>
                        
                        </table>
                        </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlMonth" Visible="false" runat="server">
                            <div style="text-align:center; background-color:#FFAD33; padding:6px; ">
                            <asp:Label ID="lblMonthYearName" runat="server" BorderColor="Black" Font-Bold="True" ForeColor="#FFFFFF" Font-Size="17px"></asp:Label>
                            </div>
                            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 500px;">
                            <asp:Calendar ID="CalTrainingDetails" ShowGridLines="false" runat="server" Height="500px" ShowTitle="false" DayStyle-VerticalAlign="Top" DayHeaderStyle-HorizontalAlign="Center" ShowNextPrevMonth="False" Width="100%" ondayrender="CalTrainingDetails_DayRender" BackColor="White" CellPadding="2" DayNameFormat="Full" Font-Names="Verdana" Font-Size="15px" ForeColor="Black">
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                                <TodayDayStyle BorderWidth="2" BorderStyle="Solid" BorderColor="Red" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <DayStyle VerticalAlign="Top" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <DayHeaderStyle ForeColor="#006666" BackColor="#FFB84D" Font-Bold="True" Font-Size="14px" HorizontalAlign="Center" Height="22px"/>
                            </asp:Calendar>
                            </div>
                        </asp:Panel>
                        </td>
                    </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>      
    </div>
  </asp:Content>
