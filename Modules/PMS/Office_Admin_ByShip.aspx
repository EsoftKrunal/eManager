<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office_Admin_ByShip.aspx.cs" Inherits="Office_Admin_ByShip" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
        <link href="../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="eReports/JS/jquery.min.js"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    

    <link rel="stylesheet" href="eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>

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
     <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
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
   <div>
       <div style="padding-top:2px">
            <table cellpadding="0" cellspacing="0"  border="0" width="100%">
                <colgroup>
                    <col width="70px" />
                    <%--<col width="70px" />--%>
                    <col />
                    <tr>
                        <td>
                            <asp:Button ID="btnplanning" runat="server" CommandArgument="C" CssClass="selbtn" OnClick="RedirectToPage" Text="Component List" />
                        </td>
                        <td>
                            <asp:Button ID="btnrunninghr" runat="server" CommandArgument="J" CssClass="btn1" OnClick="RedirectToPage" Text="Job List" />
                        </td>
                        <td style="text-align: left;"></td>
                    </tr>
                </colgroup>
    </table>
            <%--<div style="float:left; width:100%;" id="submenu">
            <ul>
            <li runat="server" id="tr_jobplanning"><a ID="btnplanning" class="selbtn" runat="server" onserverclick="RedirectToPage" ModuleId="C" >Component List</a></li>
            <li runat="server" id="tr_shiprunninghr"><a ID="btnrunninghr" runat="server"  onserverclick="RedirectToPage" ModuleId="J">Job List</a></li>
            <li class="clear"></li>
            </ul>
            <div class="clear"></div>
            <div style="background-color:#0099CC; height:5px;"></div>
       </div>--%>
   <table  border="0" cellpadding="3" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: left; width:130px; text-align:right;">
                    Select Vessel :
                </td>
                <td>
                    <asp:DropDownList ID="ddlVessels" runat="server"></asp:DropDownList>
                    
                </td>
            </tr>
            <table  border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                 <asp:Panel ID="plOfficeComponent" runat="server" Visible="true">
                                 <table  border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" border="0" cellspacing="2" width="100%">
                                                <tr>
                                                    <td style="width:130px; text-align:right">Component Code :&nbsp</td>
                                                    <td ><asp:TextBox ID="txtCompCode" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" MaxLength="12"></asp:TextBox></td>
                                                    <td style="width:130px; text-align:right">Component Name :&nbsp</td>
                                                    <td><asp:TextBox ID="txtCopmpname" runat="server" MaxLength="50"></asp:TextBox></td>
                                                    <td>Maker :&nbsp</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMaker" runat="server"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetMakerList" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="5" TargetControlID="txtMaker"  ></asp:AutoCompleteExtender>
                                                    </td>
                                                    <td><asp:Button runat="server" CssClass="btn"  ID="btnSearch" Text="Search" onclick="btnSearch_Click" />
                                                        <asp:Button runat="server" CssClass="btn"  ID="btnDownloadComponentList" Text="Download" onclick="btnDownloadComponentList_Click" Width="80px" />
                                                        <asp:Button runat="server" CssClass="btn"  ID="btnUpdateMaker" Text="Update Maker" onclick="btnUpdateMaker_Click" Width="100px" />
                                                        <asp:HiddenField ID="hfOfficeCompId" runat="server" /><asp:HiddenField ID="hfCompCode" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                       </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="box1">
                                                <asp:Label ID="lblComponentListCounter" runat="server" style="float:right;margin-right:15px;"></asp:Label>
                                                Component List</div>
                                            <div class="dvScrollheader">  
                                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                    <col style="text-align :left" width="30px" />
                                                    <col style="text-align :left" width="55px" />
                                                    <col style="text-align :left" width="110px" />
                                                    <col style="text-align :left" />
                                                    <col style="text-align :left" width="200px"/>
                                                    <col style="text-align :left" width="200px"/>
                                                    <col style="text-align :left" width="120px"/>                                                 
                                                    <col style="text-align :left" width="100px"/>
                                                    <col width="17px" />
                                                  <tr class= "headerstylegrid">
                                                      <td>
                                                          <asp:CheckBox ID="chkSelectAllCompontnt" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAllCompontnt_OnCheckedChanged"  />
                                                      </td>
                                                    <td>Vessel</td>
                                                    <td>Code</td>
                                                    <td>Component Name</td>
                                                    <td style="text-align:left">Maker</td>
                                                    <td style="text-align:left">Maker Type</td>
                                                    <td style="text-align:center">Component Status</td> 
                                                    <td style="text-align:center">Action</td>
                                                    <td>Action Component Status</td>    
                                                  </tr>
                                                 </colgroup>
                                               </table>
                                               </div>
                                        <div id="dvScroll1" class="dvScrolldata"  onscroll="SetScrollPos(this)" style="height :250px;">                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="30px" />
                                                <col style="text-align :left" width="55px" />
                                                <col style="text-align :left" width="110px" />
                                                <col style="text-align :left" />
                                                <col style="text-align :left" width="200px"/>
                                                <col style="text-align :left" width="200px"/>
                                                <col style="text-align :left" width="120px"/>                                                
                                                <col style="text-align :left" width="100px"/>
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptCompoenents" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                       <td>
                                                            <asp:CheckBox ID="chkSelectCompontnt" runat="server"  />
                                                            <asp:HiddenField ID="hfdCompid" runat="server" Value='<%# Eval("ComponentID") %>' />
                                                       </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("VesselCode") %>
                                                           
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentCode") %>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentName")%>
                                                        </td>
                                                        <td style="text-align:left"><%# Eval("Maker")%></td>
                                                        <td style="text-align:left"><%# Eval("MakerType")%></td>
                                                        <td style="text-align:Center"><%# Eval("Status")%></td>                                   
                                                        <td style="text-align:Center">
                                                            <asp:Button ID="btnActiveComp" CssClass="btn" CommandArgument='<%# Eval("ComponentCode") %>' OnClientClick="javascript:return confirm('Are you sure to active component?')" OnClick="btnActiveComp_Click" Text="Active" Visible='<%# Eval("Status").ToString() == "I" %>' runat="server" />
                                                            <asp:Button ID="btnInactiveComp" CssClass="btn" CommandArgument='<%# Eval("ComponentCode") %>' OnClientClick="javascript:return confirm('Are you sure to inactive component?')" OnClick="btnInactiveComp_Click" Text="Inactive" Visible='<%# Eval("Status").ToString() == "A" %>' runat="server" />
                                                            <asp:HiddenField ID="hfdVesselCode" runat="server" Value='<%#Eval("VesselCode") %>' />
                                                         
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </div>
                                        </td>
                                    </tr>
                                 </table>
                               </asp:Panel>
                 <asp:Panel ID="plOfficeJob" runat="server" Visible="false">
                     <table  border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                         <table cellpadding="0" border="0" cellspacing="2" width="100%">
                        <tr>
                            <td style="width:120px; text-align:right">Component Code :&nbsp</td>
                            <td><asp:TextBox ID="txtJobCompCode" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" MaxLength="12"  Width="80px"></asp:TextBox>&nbsp</td>
                            <td style="width:120px; text-align:right">Component Name :&nbsp</td>
                            <td><asp:TextBox ID="txtComponentName" runat="server" MaxLength="12"  Width="120px"></asp:TextBox>&nbsp</td>
                            <td style="width:70px; text-align:right">Job Code :&nbsp
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJobCode" runat="server" Width="70px"></asp:DropDownList>
                            </td>

                            <%--<td style="width:70px; text-align:right">Job Type :&nbsp</td>
                            <td>
                                <asp:DropDownList ID="ddlJobType" runat="server" Width="70px">
                                    <asp:ListItem Value="" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Calender"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Hour"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>--%>
                            <td style="width:110px; text-align:right">Interval Type :&nbsp
                               
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlIntervalType" runat="server"  Width="70px"></asp:DropDownList>
                               
                                </td>
                            

                            <td style="width:90px; text-align:right">Job KeyWord :&nbsp</td>
                            <td><asp:TextBox ID="txtKeyWord" runat="server" MaxLength="50"  Width="150px"></asp:TextBox>Job KeyWord :&nbsp</td>
                            <td>Maker :&nbsp</td>
                            <td>
                                <asp:TextBox ID="txtMakerJL" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetMakerList" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="5" TargetControlID="txtMakerJL"  ></asp:AutoCompleteExtender>
                            </td>                            
                            <td>
                                <asp:Button runat="server" CssClass="btn"  ID="btnJobCompSearch" Text="Search" onclick="btnJobCompSearch_Click"  />
                                <asp:Button runat="server" CssClass="btn"  ID="btnDownloadJobList" Text="Download" onclick="btnDownloadJobList_Click" Width="80px" />
                                        <asp:HiddenField ID="hfCompJobId" runat="server" />
                            </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="box1">
                                Job List
                                <asp:Label ID="lblJonListCount" runat="server" style="float:right;margin-right:15px;"></asp:Label>
                            </div>
                            <div class="dvScrollheader">  
                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="text-align :left" width="55px" />
                                <col style="text-align :left" width="110px" />
                                <col style="text-align :left" width="250px" />
                                <col style="text-align :left" width="200px"/>
                                <col style="text-align :left" width="70px" />
                                <col style="text-align :left" />
                                <col style="text-align :left" width="110px" />
                                <col style="text-align :left" width="50px"/>
                                <col style="text-align :left" width="100px"/>
                                                           
                                    <col width="17px" />
                                </colgroup>
                                <tr class= "headerstylegrid">
                                    <td>Vessel</td>
                                    <td>Comp Code</td>
                                    <td>Comp Name</td>
                                    <td>Maker</td>                                    
                                    <td>Job Code</td>
                                    <td>Job </td>
                                    <td>Job Interval</td>                                    
                                    <td style="text-align:center">Job Status</td>
                                    <td style="text-align:center">Action</td>
                                    <td>ActionActionJob Status</td>    
                                 </tr>
                             </table>
                             </div>
                            <div id="divJob1" class="dvScrolldata"  onscroll="SetScrollPos(this)" style="height :250px;">                        
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="text-align :left" width="55px" />
                                <col style="text-align :left" width="110px" />
                                <col style="text-align :left" width="250px" />
                                <col style="text-align :left" width="200px"/>
                                <col style="text-align :left" width="70px" />
                                <col style="text-align :left" />
                                <col style="text-align :left" width="110px" />
                                
                                <col style="text-align :left" width="50px"/>
                                <col style="text-align :left" width="100px"/>
                                
                                <col width="17px" />
                            </colgroup>
                            <asp:Repeater ID="rptJobs" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align:left">
                                            <%# Eval("VesselCode")%></td>
                                        <td style="text-align:left"><%# Eval("ComponentCode")%></td>
                                        <td style="text-align:left"><%# Eval("ComponentName")%></td>
                                        <td style="text-align:left"><%# Eval("Maker")%></td>
                                        <td style="text-align:left">
                                            <%# Eval("JobCode") %>
                                        </td>
                                        <td style="text-align:left"><%# Eval("JobDesc")%></td>
                                        <td style="text-align:left"><%# Eval("Interval")%> - <%# Eval("IntervalName")%></td>
                                        <td style="text-align:Center"> <%# Eval("IntervalName")%>- <%# Eval("Status")%></td>
                                        <td style="text-align:Center">
                                            <asp:Button ID="btnActiveJob" CssClass="btn" CommandArgument='<%# Eval("CompJobId") %>' OnClientClick="javascript:return confirm('Are you sure to active job?')" OnClick="btnActiveJob_Click" Text="Active" Visible='<%# Eval("Status").ToString() == "I" %>' runat="server" />
                                            <asp:Button ID="btnInactiveJob" CssClass="btn" CommandArgument='<%# Eval("CompJobId") %>' OnClientClick="javascript:return confirm('Are you sure to inactive job?')" OnClick="btnInactiveJob_Click" Text="Inactive" Visible='<%# Eval("Status").ToString() == "A" %>' runat="server" />
                                            <asp:HiddenField ID="hfdVesselCode" runat="server" Value='<%# Eval("VesselCode") %>' />
                                            <%# Eval("Status")%>
                                        </td>
                                      
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>
                        </div>
                        </td>
                    </tr>
                    </table>
                </asp:Panel> 
                </td>
            </tr>
     </table>
     <div style="padding-top:2px;"><uc1:MessageBox ID="MessageBox1" runat="server" /></div>
    </div>


       <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="divUpdateMaker" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:450px; height:150px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <table cellpadding="4" cellspacing="3" style="margin-top:25px;" width="100%">
                            <tr>
                                <td style="text-align:center;">
                                    <b>Maker :</b>
                                    <asp:TextBox ID="txtMakerPopup" runat="server" Width="250px"></asp:TextBox>
                                <asp:Label ID="lblMsgUpdatemaker" runat="server" style="color:red;"></asp:Label>
                                </td>
                            </tr>
                            
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnSave_UpdateMaker" runat="server" Text="Save" OnClick="btnSave_UpdateMaker_OnClick" CssClass="btn" />
                                <asp:Button ID="btnCloseUpdateMakerPopup" runat="server" Text="Close" OnClick="btnCloseUpdateMakerPopup_OnClick" CssClass="btn" />                                
                            </td>
                        </tr>


                    </table>
                    
                    

                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnCloseUpdateMakerPopup" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>


     </ContentTemplate>

       <Triggers>
           <asp:PostBackTrigger ControlID="btnDownloadJobList"/>
           <asp:PostBackTrigger ControlID="btnDownloadComponentList"/>
           
       </Triggers>
                </asp:UpdatePanel> 
    </form>
</body>
</html>
