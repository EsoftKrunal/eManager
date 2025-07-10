<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HazardMaster.aspx.cs" Inherits="EventManagement_HazardMaster" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="Menu_Event.ascx" TagName="leftmenu" TagPrefix="mtm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


   <%-- 
   --%> 
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
     <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
    function ShowProgress(ctl) {
        ctl.src = '../../Images/loading.gif';
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="text headerband">
             Risk Management
         </div>
        <mtm:leftmenu runat="server" ID="LefuMenu1" />
        <asp:UpdatePanel runat="server" id="up11">
        <ContentTemplate>
        <div class="box_withpad" style="min-height:450px">
        <div style="background-color:#E2F1FF; text-align:center;font-weight:bold; font-size:14px; padding:5px; " >
               <asp:RadioButton ID="rdo_Risk" Text="Risk Event Master" AutoPostBack="true" GroupName="GRP" Checked="true" OnCheckedChanged="rdoType_CheckedChanged" runat="server" />
               <asp:RadioButton ID="rdo_Hazard" Text="Hazard Master" AutoPostBack="true" GroupName="GRP" OnCheckedChanged="rdoType_CheckedChanged" runat="server" />
               <asp:RadioButton ID="rdo_Task" Text="Consequences Master" AutoPostBack="true" GroupName="GRP" OnCheckedChanged="rdoType_CheckedChanged" runat="server" />
        </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
           <td>
           </td>
        </tr>
        <tr>     
        <td> 
            <div id="dv_Risk" runat="server">
                <div class="dvScrollheader">  
                                <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:15px;" />
                                            <col style="width:50px;" />
                                            <col style="width:150px;" />                                            
                                            <col />
                                            <col style="width:150px;" />                                            
                                            <col style="width:20px;" />
                                        </colgroup>
                                        <tr class= "headerstylegrid">
                                            <td style="text-align:center;"></td>
                                            <td style="text-align:center; vertical-align:middle;">Edit</td>
                                            <td style="text-align:left; vertical-align:middle;">Risk Code</td>
                                            <td style="text-align:left; vertical-align:middle;">
                                            <span style="float:left">Activity</span>
                                            <span style="float:left; margin-left:10px;"><asp:ImageButton ID="btnAddRisk" runat="server" OnClick="btnAddEvent_Click" ImageUrl="~/Modules/HRD/Images/add_16.gif" ToolTip="Add Activity" OnClientClick="ShowProgress(this);"/></span>
                                            <span style="float:left; margin-left:10px;"><asp:ImageButton ID="btnSearch" runat="server" OnClick="btnSearchEvent_Click" ImageUrl="~/Modules/HRD/Images/HourGlass.png" ToolTip="Search Activity"  OnClientClick="ShowProgress(this);"/></span>
                                            <span style="float:left; margin-left:10px;"><asp:ImageButton ID="btnClear" runat="server" OnClick="btnClearEvent_Click" ImageUrl="~/Modules/HRD/Images/edit_clear.png" ToolTip="Clear Search" OnClientClick="ShowProgress(this);" /></span>
                                            </td>  
                                            <td style="text-align:center; vertical-align:middle;">Office App. Required</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                <div class="dvScrolldata" style="height: 400px;">
                                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:15px;" />
                                            <col style="width:50px;" />
                                            <col style="width:150px;" />                                            
                                            <col />
                                            <col style="width:150px;" />                                            
                                            <col style="width:20px;" />
                                        </colgroup>
                                        <asp:Repeater ID="rptEvents" runat="server">
                                            <ItemTemplate>
                                                <tr > <%--style='<%# (Common.CastAsInt32(Eval("EventId"))== EventId)? "background-color:#1589FF;color:white;" : "" %> ' --%>
                                                    <td style="text-align:left">
                                                    </td>
                                                    <td style="text-align:center">
                                                        <asp:ImageButton ID="btnEditEvent" runat="server" CommandArgument='<%#Eval("EventId")%>' ImageUrl="~/Modules/HRD/Images/editX12.jpg" OnClick="btnEditEvent_Click" ToolTip="Edit" OnClientClick="ShowProgress(this);"/>
                                                    </td>
                                                    <td align="left"><%#Eval("EventCode")%></td>
                                                    <td align="left">
                                                       <%#Eval("EventName")%>
                                                    </td>
						    <td>
							<%#((Eval("OfficeApprovalRequired").ToString()=="Y")?"Yes":"No")%>
						    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
            </div>
            <div id="dv_Hazard" runat="server" visible="false">           
                <div class="dvScrollheader">  
                                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:15px;" />
                                            <col style="width:50px;" />
                                            <col style="width:150px;" /> 
                                            <col />
                                            <col style="width:20px;" />
                                        </colgroup>
                                        <tr class= "headerstylegrid">
                                            <td style="text-align:center;"></td>
                                            <td style="text-align:center; vertical-align:middle;">Edit</td>
                                            <td style="text-align:left; vertical-align:middle;">Hazard Code</td>                                            
                                            <td style="text-align:left;vertical-align:middle;">
                                            <span style="float:left">Hazard Name</span>
                                            <span style="float:left; margin-left:10px;">
                                                <asp:ImageButton ID="btnAddHazard" runat="server" OnClick="btnAddHazard_Click" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="ShowProgress(this);"/>
                                            </span>
                                            
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                <div class="dvScrolldata" style="height: 430px;">
                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:15px;" />
                            <col style="width:50px;" />
                            <col style="width:150px;" />                                             
                            <col />
                            <col style="width:20px;" />
                        </colgroup>
                        <asp:Repeater ID="rptHazard" runat="server">
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align:left">
                                    </td>
                                    <td style="text-align:center">
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("HazardId")%>' ImageUrl="~/Modules/HRD/Images/editX12.jpg" OnClick="btnEdit_Click" ToolTip="Edit" />
                                    </td>
                                    <td align="left">
                                        <%#Eval("HazardCode")%>
                                    </td>                                                    
                                    <td align="left">
                                        <%#Eval("HazardName")%>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div id="dv_Task" runat="server" visible="false">           
                <div class="dvScrollheader">  
                                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:15px;" />
                                            <col style="width:50px;" />
                                            <col style="width:150px;" /> 
                                            <col />
                                            <col style="width:20px;" />
                                        </colgroup>
                                        <tr class= "headerstylegrid">
                                            <td style="text-align:center;"></td>
                                            <td style="text-align:center; vertical-align:middle;">Edit</td> 
                                            <td style="text-align:left; vertical-align:middle;">Consequences Code</td>                                           
                                            <td style="text-align:left;vertical-align:middle;">
                                            <span style="float:left">Consequences Name</span>
                                            <span style="float:left; margin-left:10px;">
                                                <asp:ImageButton ID="btnAddTask" runat="server" OnClick="btnAddTask_Click" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="ShowProgress(this);"/>
                                            </span>
                                            
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                <div class="dvScrolldata" style="height: 430px;">
                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:15px;" />
                            <col style="width:50px;" />
                            <col style="width:150px;" />                                             
                            <col />
                            <col style="width:20px;" />
                        </colgroup>
                        <asp:Repeater ID="rpt_Task" runat="server">
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align:left">
                                    </td>
                                    <td style="text-align:center">
                                        <asp:ImageButton ID="btnEditTask" runat="server" CommandArgument='<%#Eval("ConsequencesId")%>' ImageUrl="~/Modules/HRD/Images/editX12.jpg" OnClick="btnEditTask_Click" ToolTip="Edit" />
                                    </td>
                                    <td align="left">
                                        <%#Eval("ConsequencesCode")%>
                                    </td>                                                    
                                    <td align="left">
                                        <%#Eval("ConsequencesName")%>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
             
        </td>
        </tr>
        </table>

        <%-- ADD/ Edit Hazard --%>
        <div ID="dv_AddEditHazard" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
                <center>
                    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                    <div style="position: relative; width: 500px; height:150px; padding: 3px; text-align: center; background: white; z-index: 150; top: 70px; border: solid 10px gray;">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;">
                                                Add/ Edit Hazard
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right; font-weight:bold; width:95px;">
                                                Hazard Code :&nbsp;
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtHazardCode" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:left; font-weight:bold;">
                                                Hazard Name :&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:left;">
                                                <asp:TextBox ID="txtHazardName" runat="server" Width="95%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txtHazardName" ErrorMessage="*" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="text-align:center; padding:3px;">
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                      
                                    <div style="text-align:center">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" 
                                            style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" 
                                            Text="Save" ValidationGroup="v1" />
                                        <asp:Button ID="btnClose" runat="server" CausesValidation="false" 
                                            OnClick="btnClose_Click" 
                                            style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" 
                                            Text="Close" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnClose" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </center>
            </div>
        <%-- ADD/ Edit Events --%>
        <div ID="dv_AddEditEvent" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
                <center>
                    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)">
                    </div>
                    <div style="position: relative; width: 500px; height: 170px; padding: 3px; text-align: center; background: white; z-index: 150; top: 150px; border: solid 10px gray;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table border="0" cellpadding="4" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2" 
                                                style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;">
                                                Add/ Edit Activity
                                            </td>
                                        </tr>
                                        <tr>
                                              <td style="text-align:right; width:150px;">
                                                  Risk Code :&nbsp;
                                              </td>
                                              <td style="text-align:left;">
                                                  <asp:Label ID="txtEventCode" runat="server" Width="95%"></asp:Label>
                                              </td>
                                          </tr>
                                        <tr>
                                            <td style="text-align:right; width:130px;">
                                                Activity :&nbsp;
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtEventName" runat="server" Width="95%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                    ControlToValidate="txtEventName" ErrorMessage="*" ValidationGroup="r1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
					<tr>
                                            <td style="text-align:right; width:130px;">
                                                Office Approval Required :&nbsp;
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:CheckBox ID="chkOfficeAppRequired" runat="server"></asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="text-align:center; padding:3px;">
                                        <asp:Label ID="lblEventMSG" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                      
                                    <div style="text-align:center">
                                        <asp:Button ID="btnSaveEvent" runat="server" OnClick="btnSaveEvent_Click" 
                                            style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" 
                                            Text="Save" ValidationGroup="r1" />
                                        <asp:Button ID="btnCloseEvent" runat="server" CausesValidation="false" 
                                            OnClick="btnCloseEvent_Click" 
                                            style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" 
                                            Text="Close" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnCloseEvent" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </center>
            </div>
        <%-- Search Events --%>
        <div ID="dv_SearchEvent" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
                <center>
                    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)">
                    </div>
                    <div style="position: relative; width: 500px; height: 120px; padding: 3px; text-align: center; background: white; z-index: 150; top: 150px; border: solid 10px gray;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table border="0" cellpadding="4" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2" 
                                                style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;">
                                                Search Activity
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right; width:130px;">
                                                Enter Search Text :&nbsp;
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtSearchText" runat="server" Width="95%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSearchText" ErrorMessage="*" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                      
                                    <div style="text-align:center">
                                        <asp:Button ID="btnSearchNow" runat="server" OnClick="btnSearchNow_Click" 
                                            style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" 
                                            Text="Search" ValidationGroup="v1" />
                                        <asp:Button ID="btnCancelSearch" runat="server" CausesValidation="false" 
                                            OnClick="btnCancelSearch_Click" 
                                            style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" 
                                            Text="Close" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnCloseEvent" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </center>
            </div>
        <%-- ADD/ Edit Task --%>
        <div ID="dv_AddEditTask" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
                <center>
                    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                    <div style="position: relative; width: 500px; height:150px; padding: 3px; text-align: center; background: white; z-index: 150; top: 70px; border: solid 10px gray;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2" style="text-align: center; font-size:14px;" class="text headerband">
                                                Add/ Edit Consequences
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right; font-weight:bold; width:80px;">
                                                Consequences Code :&nbsp;
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtTaskCode" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:left; font-weight:bold;">
                                                Consequences Name :&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align:left;">
                                                <asp:TextBox ID="txtTaskName" runat="server" Width="95%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtTaskName" ErrorMessage="*" ValidationGroup="t1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="text-align:center; padding:3px;">
                                        <asp:Label ID="lblMsg_Task" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                      
                                    <div style="text-align:center">
                                    <asp:Button ID="btnSaveTask" runat="server" OnClick="btnSaveTask_Click" 
                                            style=" padding:3px; border:none;  width:80px;" CssClass="btn" 
                                            Text="Save" ValidationGroup="t1" />
                                        <asp:Button ID="btnCloseTask" runat="server" CausesValidation="false" 
                                            OnClick="btnCloseTask_Click" 
                                            style=" padding:3px; border:none;  width:80px;" CssClass="btn" 
                                            Text="Close" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnCloseTask" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </center>
            </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
