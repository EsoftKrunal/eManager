<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_ShipExperience.aspx.cs" Inherits="Emtm_Profile_TravelDocs" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master" %>


<%@ Register src="Profile_ExperienceHeaderMenu.ascx" tagname="Profile_ExperienceHeaderMenu" tagprefix="uc4" %>
<%@ Register src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


   <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
   
    <div style="font-family:Arial;font-size:12px;">
       
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
        <Triggers>
        <asp:PostBackTrigger ControlID ="btnsave" />
        </Triggers> 
        <ContentTemplate>
             <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="text headerband" style=" text-align :left; font-size :14px; padding :3px; font-weight: bold;">
                            Documents : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                           </div>
                        <div class="dottedscrollbox">
                                 
                            <uc5:Profile_PersonalHeaderMenu ID="Emtm_Profile_PersonalHeaderMenu1" 
                                runat="server" />
                                 
                        </div>   
                        <div class="dottedscrollbox">
                            <uc4:Profile_ExperienceHeaderMenu ID="Emtm_Profile_ExperienceHeaderMenu1" 
                                runat="server" />
                        </div>
                        <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                            
                        
                        <table cellpadding="2" cellspacing="2" border="0"  width="100%"> <%--bordercolor="red"--%>
                            <colgroup>
                                <col width="650px"/>
                                <tr>
                                    <td></td>
                                    <td style="font-weight:bold;text-align:center;">Experience Summary</td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                                <colgroup>
                                                    <col style="width:25px;" />
                                                    <col style="width:25px;" />
                                                    <col style="width:25px;" />
                                                    <%--<col />
                                    <col style="width:120px;" />
                                    <col style="width:100px;" />--%>
                                                    <col  width="150px" />
                                                    <col  width="150px" />
                                                    <%--<col style="width:80px;" />
                                    <col style="width:80px;" />--%>
                                                    <col style="width:100px;" />
                                                    <%--<col style="width:30px;" />--%><%--<col />--%>
                                                    <col style="width:25px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <%--<td>
                                                Company Name</td>
                                            <td>
                                                Rank</td>    
                                            <td>
                                                Vessel</td>    --%>
                                                        <td>Vessel Type</td>
                                                        <td>Experience Type</td>
                                                        <%--<td>
                                                Sign On Dt.</td>    
                                            <td>
                                                Sign Off Dt.</td>        --%>
                                                        <td>Duration(Year)</td>
                                                        <%--<td>
                                                GRT</td>--%><%--<td>
                                                Sign Off Reason</td>--%>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <%--</colgroup>--%>
                                                </colgroup>
                                            </table>
                                        </div>
                                        <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 330px; text-align:center;">
                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                    <col style="width:25px;" />
                                                    <col style="width:25px;" />
                                                    <col style="width:25px;" />
                                                    <%--<col />
                                            <col style="width:120px;" />
                                            <col style="width:100px;" />--%>
                                                    <col  width="150px" />
                                                    <col  width="150px" />
                                                    <%--<col style="width:80px;" />
                                            <col style="width:80px;" />--%>
                                                    <col style="width:100px;" />
                                                    <%--<col style="width:30px;" />--%><%--<col  />--%>
                                                    <col style="width:25px;" />
                                                </colgroup>
                                                <asp:Repeater ID="RptShipExpDoc" runat="server">
                                                    <ItemTemplate>
                                                        <tr class='<%# (Common.CastAsInt32(Eval("ShipExpId"))==SelectedId)?"selectedrow":"row"%>'>
                                                            <td align="center">
                                                                <asp:ImageButton ID="btnShipExpView" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ShipExpId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnShipExpView_Click" ToolTip="View" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="btnShipExpedit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ShipExpId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnShipExpedit_Click" ToolTip="Edit" Visible="<%#auth.IsUpdate%>" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="btnShipExpDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ShipExpId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnShipExpDelete_Click" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" Visible="<%#auth.IsDelete%>" />
                                                            </td>
                                                            <%--<td align="center">
                                                        <%#Eval("CompanyName")%></td>
                                                    <td align="center">
                                                        <%#Eval("Rank")%></td>    
                                                    <td align="center">
                                                        <%#Eval("Vessel")%></td>    --%>
                                                            <td align="left"><%#Eval("VesselType")%></td>
                                                            <td><%#Eval("ExpTypeText")%><%--<td align="center">
                                                        <%#Eval("SignOnDt")%></td>    
                                                    <td align="center">
                                                        <%#Eval("SignOffDt")%></td>        --%>
                                                                <td align="center"><%#Eval("Experiance")%></td>
                                                                <%--<td align="center">
                                                        <%#Eval("GRT")%></td>--%><%--<td align="center">
                                                        <%#Eval("SignOffReason")%></td>--%>
                                                                <td>&nbsp;</td>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                                <colgroup>
                                                    <col style="width:200px;" />
                                                    <col />
                                                    <col style="width:25px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td>Vessel Type</td>
                                                        <td>Total Experience</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                        </div>
                                        <div id="DivTotExp" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 330px; text-align:center;">
                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                    <col style="width:200px;" />
                                                    <col  />
                                                    <col style="width:25px;" />
                                                </colgroup>
                                            </table>
                                            <asp:Repeater ID="rptTotExp" runat="server">
                                                <ItemTemplate>
                                                    <tr class="row">
                                                        <td align="left"><%#Eval("VesselType")%></td>
                                                        <td><%#Eval("TotExp")%></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <table>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>    
                          
                        
                        <br /> 
                           <table id="tblview" runat="server" width="100%" cellpadding="1" cellspacing ="0" border="0">
                               <colgroup>
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <tr>
                                       <td style="text-align :right">
                                           Vessel Type :</td>
                                       <td style="text-align :left">
                                           <asp:DropDownList ID="ddlVesselType" runat="server" Height="20px" Width="170px">
                                           </asp:DropDownList>
                                       </td>
                                       <td style="text-align :right">
                                            Experiance Type :
                                       </td>
                                       <td style="text-align :left">
                                            <asp:DropDownList ID="ddlExpType" runat="server" Height="20px" Width="128px">
                                                <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="ShipBoard" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Shore Based" Value="2"></asp:ListItem>
                                           </asp:DropDownList>
                                       </td>
                                       <td style="text-align :right">
                                            Duration(Year)
                                        </td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtDuration" runat="server" Width="50px" ></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">&nbsp;</td>
                                       <td style="text-align :left"></td>
                                       <td style="text-align :right"></td>
                                       <td style="text-align :left"></td>
                                       <td style="text-align :left"></td>
                                       <td style="text-align :left"></td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">Remarks :</td>
                                       
                                       <td style="text-align :left" colspan="5">
                                            <asp:TextBox ID="txtResion" runat="server" TextMode="MultiLine" Width="99%" Height="70px"></asp:TextBox>
                                       </td>
                                       
                                   </tr>                                   
                                   <tr>
                                       <td valign="top">&nbsp;</td>
                                       <td valign="top">&nbsp;</td>
                                       <td>&nbsp;</td>
                                       <td>&nbsp;</td>
                                       <td>&nbsp;</td>
                                       <td>
                                           
                                       </td>
                                   </tr>
                                   </col>
                                   </col>
                               </colgroup>
                            </table>
                            <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblMsg" runat="server" style="color:Red" ></asp:Label>
                                    <asp:Button ID="btnaddnew" CssClass="btn" runat="server" Text="Add New" 
                                        Width="80px" onclick="btnaddnew_Click" CausesValidation="false"></asp:Button>
                                    <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" 
                                        onclick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btncancel" CssClass="btn"  runat="server" Text="Cancel" 
                                        onclick="btncancel_Click" CausesValidation="false"></asp:Button>
                                </td>
                            </tr>
                            </table>
                        </div> 
                    </td>
                </tr>
         </table> 
         </ContentTemplate>
      </asp:UpdatePanel> 
    </div>
    </asp:Content>