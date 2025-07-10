<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_Experiance.aspx.cs" Inherits="emtm_Emtm_Experiance" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master"%>

<%@ Register src="Profile_ExperienceHeaderMenu.ascx" tagname="Profile_ExperienceHeaderMenu" tagprefix="uc2" %>

<%@ Register src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


   
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/ecmascript">
        function PopUPWindow(obj,Mode,Office) 
        {
            winref = window.open('../MyProfile/Profile_ShoreExperience.aspx?ShoreId=' + obj + ' &Mode=' + Mode + ' &Office=' + Office, '', 'title=no,toolbars=no,scrollbars=yes,width=800,height=220,left=250,top=150,addressbar=no,resizable=1,status=0');
             return false;   
        }
    </script>
    

    <div style="font-family:Arial;font-size:12px;">
       
        <asp:UpdatePanel  runat="server" >
        <ContentTemplate>
         <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                           <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                            Personal Details : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                           </div>
                            <div class="dottedscrollbox">
                                 <uc3:Profile_PersonalHeaderMenu ID="Emtm_Profile_PersonalHeaderMenu1" 
                                     runat="server" />
                            </div>
                            <div class="dottedscrollbox">
                                <uc2:Profile_ExperienceHeaderMenu ID="Emtm_Profile_ExperienceHeaderMenu1" runat="server" />
                            </div> 
                           <div id="divmtmexp" runat="server" style="padding:5px 5px 5px 5px;" >
                           <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid"><legend>MTM Exp.</legend>
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col />
                                    <col style="width:200px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class= "headerstylegrid">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            Office Name</td>
                                        <td>
                                            Position</td>
                                        <td>
                                            From Date</td>
                                        <td>
                                            To Date</td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </colgroup>
                            </table> 
                            </div>          
                            
                            <div id="dvMTMExp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 125px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:25px;" />
                                   <col style="width:25px;" />
                                   <col style="width:25px;" />
                                   <col />
                                   <col style="width:200px;" />
                                   <col style="width:100px;" />
                                   <col style="width:100px;" />
                                   <col style="width:25px;" />
                               </colgroup>
                                <asp:Repeater ID="rptmtmexp" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("MtmOfficeExpId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btnMtmView" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("MtmOfficeExpId") %>' 
                                                    ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                    OnClientClick="return PopUPWindow(this.RowId,'View','Mtm');" 
                                                    RowId='<%# Eval("MtmOfficeExpId") %>' ToolTip="View" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnMtmedit" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("MtmOfficeExpId") %>' Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                    OnClientClick="return PopUPWindow(this.RowId,'Edit','Mtm');"  RowId='<%# Eval("MtmOfficeExpId") %>' 
                                                    ToolTip="Edit" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnMtmDelete" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("MtmOfficeExpId") %>' Visible="<%#auth.IsDelete%>" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                    OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  RowId='<%# Eval("MtmOfficeExpId") %>' 
                                                    OnClick="btnMtmDelete_Click"
                                                    ToolTip="Delete" />
                                            </td>
                                            <td align="left">
                                                <%#Eval("OfficeName")%></td>
                                            <td align="left">
                                                <%#Eval("Designation")%></td>
                                            <td align="center">
                                                <%#Eval("FromDate")%></td>
                                            <td align="center">
                                                <%#Eval("ToDate")%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div> 
                            </fieldset>    
                            </div> 
                           <div id="divotherexp" runat="server" style="padding:5px 5px 5px 5px;">
                          <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid"><legend>Other Company Exp.</legend>
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col />
                                    <col style="width:150px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:120px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class= "headerstylegrid">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            Company<td>
                                                Position</td>
                                            <td>
                                                From Date</td>
                                            <td>
                                                To Date</td>
                                            <td>
                                                Location</td>
                                            <td>&nbsp;
                                            </td>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                            <div  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 150px ; text-align:center;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                       <col style="width:25px;" />
                                       <col style="width:25px;" />
                                       <col style="width:25px;" />
                                       <col />
                                       <col style="width:150px;" />
                                       <col style="width:80px;" />
                                       <col style="width:80px;" />
                                       <col style="width:120px;" />
                                       <col style="width:25px;" />
                                   </colgroup>
                                <asp:Repeater ID="rptotherexp" runat="server">
                                    <ItemTemplate>
                                        <tr class="alternaterow">
                                            <td align="center">
                                                <asp:ImageButton ID="btnotherView" runat="server" 
                                                    CommandArgument='<%# Eval("ShoreId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                    OnClientClick="return PopUPWindow(this.RowId,'View','Other');" 
                                                    RowId='<%# Eval("ShoreId") %>' ToolTip="View" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnotheredit" runat="server" 
                                                    CommandArgument='<%# Eval("ShoreId") %>' Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                    OnClientClick="return PopUPWindow(this.RowId,'Edit','Other');" 
                                                    RowId='<%# Eval("ShoreId") %>' ToolTip="Edit" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnotherDelete" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("ShoreId") %>' Visible="<%#auth.IsDelete%>" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                    OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  
                                                    OnClick="btnotherDelete_Click"
                                                    ToolTip="Delete" />
                                            </td>
                                            <td align="left">
                                                <%#Eval("Company")%></td>
                                            <td align="left">
                                                <%#Eval("Position")%></td>
                                            <td align="left">
                                                <%#Eval("FromDate")%></td>
                                            <td align="center">
                                                <%#Eval("ToDate")%></td>
                                            <td align="left">
                                                <%#Eval("Location")%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>                                   
                                </asp:Repeater>
                             </table>
                             </div> 
                             </fieldset>
                           </div> 
                        <table width="100%" cellpadding="2" cellspacing ="0" border="0">
                        <tr>
                            <td align="right" style="display:none;">
                                <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" /> 
                                 </td>
                            <td align="right">
                                <asp:Button ID="btnaddnew" CssClass="btn" runat="server" Text="Add New" Width="80px" OnClientClick="PopUPWindow('0','Add','Mtm');" ></asp:Button>
                            </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                </table>
          </ContentTemplate>
          </asp:UpdatePanel>
    </div>
   </asp:Content>