<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office_Admin_Delete.aspx.cs" Inherits="Office_Admin_Delete" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function openprintwindow(VC, year) {
            window.open('Reports/MaintenanceKPI.aspx?VC=' + VC + '&Year=' + year, 'print', '', '');
        }
    </script>
        <style type="text/css">
    td
    {
        vertical-align:middle;
    }
    </style>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div class="text headerband"> Maintenance</div>
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">            
            <tr>
                <td>
                    <div style="width:100%; height:452px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                   <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress>
             <asp:UpdatePanel runat="server" id="up1" UpdateMode="Always">
                <ContentTemplate>
                    <table style="border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                           <td>
                               <div class="box_withpad" style="min-height:450px">
                               <div class="box1">
                                        <asp:RadioButton ID="rdoVesselFinalize" Text="Vessel Finalization" GroupName="aa" AutoPostBack="true" runat="server" oncheckedchanged="rdoShipComp_CheckedChanged" />
                                        <asp:RadioButton ID="rdoOfficeComp" Text="Component Master" GroupName="aa" AutoPostBack="true"  runat="server" oncheckedchanged="rdoOfficeComp_CheckedChanged" Visible="false" />
                                        <asp:RadioButton ID="rdoOfficeJob" Text="Job Master" GroupName="aa" AutoPostBack="true"  runat="server" oncheckedchanged="rdoOfficeJob_CheckedChanged"  Visible="false" />                                        
                                        <asp:RadioButton ID="rdoMKPI" Text="Maintainance KPI" GroupName="aa" AutoPostBack="true" runat="server" oncheckedchanged="rdomkpi_CheckedChanged"   Visible="false" />
                                        <asp:RadioButton ID="rdoByShip" Text="Job/Component Assignment" GroupName="aa" AutoPostBack="true" runat="server" oncheckedchanged="rdoByShip_CheckedChanged" />
                                        <asp:RadioButton ID="rdoJobCorrection" Text="Job Rejection" GroupName="aa" AutoPostBack="true" runat="server" oncheckedchanged="rdoJobCorrection_CheckedChanged" />
                               </div>
                               <asp:Panel ID="plOfficeComponent" runat="server" Visible="false">
                                    <div style="padding:2px;">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                Component Code :&nbsp
                                               
                                            </td>
                                            <td>
                                                 <asp:TextBox ID="txtCompCode" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" MaxLength="12"></asp:TextBox>
                                            </td>
                                            <td>
                                                Component Name :&nbsp
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCopmpname" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" CssClass="btn"  ID="btnSearch" Text="Search" onclick="btnSearch_Click" />
                                                <asp:HiddenField ID="hfOfficeCompId" runat="server" /><asp:HiddenField ID="hfCompCode" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width:50%">
                                            <div class="box1">Component List</div>
                                            <div class="dvScrollheader">  
                                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" width="200px" />
                                                <col width="17px" />
                                                </colgroup>
                                                <tr class= "headerstylegrid">
                                                <td style="text-align :left">Component Code</td>
                                                <td style="text-align :left">Component Name</td>
                                                <td width="17px" >&nbsp;</td>    
                                                </tr>
                                            </table>
                                            </div>
                                            <div id="dvScroll" class="dvScrolldata" onscroll="SetScrollPos(this);" style="height :300px;">                        
                                            <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" width="200px" />
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptCompoenents" runat="server">
                                                <ItemTemplate>
                                                    <tr class='<%# (Eval("ComponentId").ToString()==SelectedCompId.ToString())?"activetr1":"tr1" %>'>
                                                        <td style="text-align:left"><asp:LinkButton ID="btnComponent" runat="server" CommandArgument='<%# Eval("ComponentId") %>' OnClick="btnComponent_Click" Text='<%# Eval("ComponentCode") %>'></asp:LinkButton></td>
                                                        <td style="text-align:left"><%# Eval("ComponentName")%></td>
                                                        <td width="17px" >&nbsp;</td>    
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </div>
                                        </td>
                                        <td>
                                            <div class="box1">Dependencies</div>
                                            <div class="dvScrollheader">
                                                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                  <col style="text-align :left" width="130px" />
                                                  <col style="text-align :center" width="100px"/>
                                                  <col style="text-align :left" width="70px"/>
                                                  <col width="17px" />
                                                  <tr class= "headerstylegrid">
                                                    <td style="text-align :left">Vessel Name</td>
                                                    <td style="text-align :left">Component Status</td>
                                                    <td></td>
                                                    <td></td>    
                                                  </tr>
                                                 </colgroup>
                                               </table>
                                            </div>
                                            <div id="divComVessel" class="dvScrolldata" onscroll="SetScrollPos(this)" style="height :300px;">                        
                                            <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="130px" />
                                                <col style="text-align :left" width="100px"/>
                                                <col style="text-align :left" width="70px"/>
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptOfficeDependencies" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align:left">
                                                            <%# Eval("Vessel") %> 
                                                        </td>
                                                        <td style="text-align:Center">
                                                            <%#((Eval("Status").ToString().Trim()=="A")?"Active":"InActive")%></td>
                                                        <td>
                                                            <asp:Button ID="btnActiveComp" CssClass="btn" CommandArgument='<%# Eval("VesselCode") %>' OnClientClick="javascript:return confirm('Are you sure to active component?')" OnClick="btnActiveComp_Click" Text="Active" Visible='<%# Eval("Status").ToString() == "I" %>' runat="server" />
                                                            <asp:Button ID="btnInactiveComp" CssClass="btn" CommandArgument='<%# Eval("VesselCode") %>' OnClientClick="javascript:return confirm('Are you sure to inactive component?')" OnClick="btnInactiveComp_Click" Text="Inactive" Visible='<%# Eval("Status").ToString() == "A" %>' runat="server" />
                                                        </td>
                                                        <td></td>    
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </div>
                                        </td>
                                    </tr>
                                    <tr>
                                      
                                       <td colspan="2">
                                            <asp:Button ID="btnoffCompDelete" CssClass="btn" 
                                                style="float:right; padding-right:2px;" 
                                                OnClientClick="javascript:return confirm('Are you sure to delete?')" 
                                                Text="Delete" Visible="true" runat="server" onclick="btnoffCompDelete_Click" />
                                       </td>
                                    </tr>
                                 </table>

                               </asp:Panel>
                               <asp:Panel ID="plOfficeJob" runat="server" Visible="false">
                                   <div style="padding:2px;">
                                   <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        Component Code :&nbsp
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtJobCompCode" onkeypress="fncInputIntegerValuesOnly(event)"
                                                            runat="server" MaxLength="12"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Keyword :&nbsp
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtKeyWord" runat="server" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" CssClass="btn"  ID="btnJobCompSearch" Text="Search" onclick="btnJobCompSearch_Click" />
                                                        <asp:HiddenField ID="hfCompJobId" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                   </div>
                                  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width:50%">
                                            <div class="box1">Job List</div>
                                            <div class="dvScrollheader">  
                                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                  <col style="text-align :left" width="150px" />
                                                  <col style="text-align :left" width="80px" />
                                                  <col style="text-align :left"/>
                                                  <col width="17px" />
                                                </colgroup>
                                                  <tr class= "headerstylegrid">
                                                    <td style="text-align :left" >Component Code</td>
                                                    <td style="text-align :left" >Job Code</td>
                                                    <td></td>
                                                    <td></td>    
                                                  </t>
                                                
                                               </table>
                                            </div>
                                            <div id="divJob"  onscroll="SetScrollPos(this)" style="height :300px;" class="dvScrolldata" >                        
                                            <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="150px" />
                                                <col style="text-align :left" width="80px" />
                                                <col style="text-align :left"/>
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptJobs" runat="server">
                                                <ItemTemplate>
                                                    <tr class='<%# (Eval("CompJobId").ToString()==SelectedJobId.ToString())?"activetr1":"tr1" %>'>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentCode")%></td>
                                                        <td style="text-align:left">
                                                            <asp:LinkButton ID="btnJob" runat="server" CommandArgument='<%# Eval("CompJobId") %>' OnClick="btnJob_Click" Text='<%# Eval("JobCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("DescrSh")%></td>
                                                        <td></td>  
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="box1">Dependencies</div>
                                            <div class="dvScrollheader">  
                                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                  <col style="text-align :left" width="130px" />
                                                  <col style="text-align :left" width="100px"/>
                                                  <col style="text-align :left" width="70px"/>
                                                  <col width="17px" />
                                                  <tr class= "headerstylegrid">
                                                    <td style="text-align :left">Vessel</td>
                                                    <td style="text-align :center">Job Status</td>
                                                    <td></td>
                                                    <td></td>    
                                                  </tr>
                                                 </colgroup>
                                               </table>
                                            </div>
                                            <div id="divJobVessel"  onscroll="SetScrollPos(this)" style="height :300px;" class="dvScrolldata">                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="130px" />
                                                <col style="text-align :left" width="100px"/>
                                                <col style="text-align :left" width="70px"/>
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptjobDependencies" runat="server">
                                                <ItemTemplate>
                                                    <tr class="tr1">
                                                        <td style="text-align:left">
                                                            <%# Eval("Vessel") %> 
                                                        </td>
                                                        <td style="text-align:Center">
                                                            <%#((Eval("Status").ToString().Trim()=="A")?"Active":"InActive")%></td>
                                                            </td>
                                                        <td>
                                                            <asp:Button ID="btnActiveJob" CssClass="btn" CommandArgument='<%# Eval("VesselCode") %>' OnClientClick="javascript:return confirm('Are you sure to active job?')" OnClick="btnActiveJob_Click" Text="Active" Visible='<%# Eval("Status").ToString() == "I" %>' runat="server" />
                                                            <asp:Button ID="btnInactiveJob" CssClass="btn" CommandArgument='<%# Eval("VesselCode") %>' OnClientClick="javascript:return confirm('Are you sure to inactive job?')" OnClick="btnInactiveJob_Click" Text="Inactive" Visible='<%# Eval("Status").ToString() == "A" %>' runat="server" />
                                                        </td>
                                                        <td></td>    
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </div>
                                        </td>
                                    </tr>
                                    <tr>
                                      
                                       <td colspan="2">
                                            <asp:Button ID="btnDeleteJobs" CssClass="btn" 
                                                style="float:right; padding-right:2px;" 
                                                OnClientClick="javascript:return confirm('Are you sure to delete?')" 
                                                Text="Delete" Visible="true" runat="server" onclick="btnDeleteJobs_Click" />
                                       </td>
                                    </tr>
                                 </table>
                               </asp:Panel>
                               <asp:Panel ID="plShipComponent" runat="server" Visible="false">
                                  <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                    <tr>
                                       <td style="text-align :right">Select Vessel :</td>
                                       <td style="text-align:left;">
                                       <asp:DropDownList runat="server" id="ddlVessel" OnTextChanged="ddlVessel_SelectIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                       </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <br />
                                    <td style=" text-align :left; font-size :13px; font-weight:bold">
                                    Exported : <asp:Label runat="server" ID="lblExpoted"></asp:Label> 
                                    <br /><br />
                                    Exported By : <asp:Label runat="server" ID="lblExportedBy" ></asp:Label> 
                                    <br /><br />
                                    Exported On : <asp:Label runat="server" ID="lblExportedOn"></asp:Label> 
                                    <br />
                                    </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <td style=" text-align :left">
                                        <asp:Button runat="server" ID="btnStart" OnClick="btnStart_OnClick" Text="Start Ship-DataBase" Width="200px" OnClientClick="confirm('Are you sure ?');"/> 
                                    </td>
                                    </tr>
                                 </table>
                               </asp:Panel>
                               <asp:Panel ID="plShipJob" runat="server" Visible="false">
                                  <table style="background-color:#f9f9f9" border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                       <td></td>
                                       <td></td>
                                    </tr>
                                 </table>
                               </asp:Panel>
                               <asp:Panel ID="plMKPI" runat="server" Visible="false">

                               <iframe id="ifKPI" src="MaintenanceKPI.aspx" width="100%" height="500px" scrolling="no" runat="server" ></iframe>
                               </asp:Panel>
                               <asp:Panel ID="plByShip" runat="server" Visible="false">
                               <iframe id="IfByShip" src="Office_Admin_ByShip.aspx" width="100%" height="500px" scrolling="no" runat="server" ></iframe>
                               </asp:Panel>

                                   <asp:Panel ID="pnlJobCorrection" runat="server" Visible="false">
                                       <iframe id="Iframe1" src="Office_Admin_JobCorrection.aspx" width="100%" height="500px" scrolling="no" runat="server" ></iframe>
                                    </asp:Panel>

                               <div style="padding-top:2px;">
                                    <uc1:MessageBox ID="MessageBox1" runat="server" />
                               </div>
                                </div>
                           </td>
                        </tr>
                    </table>
               </ContentTemplate>
                </asp:UpdatePanel> 
               </div>   
                    </td>
                </tr>
            </table>
            </td>
            </tr>
        </table>
    </div>
</asp:Content>