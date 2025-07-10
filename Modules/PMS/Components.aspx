<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Components.aspx.cs" Inherits="Components" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
      <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
         <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function openaddwindow(CompCode) {
            window.open('AddJobsToComponents.aspx?CompCode=' + CompCode + '&Type=ADD', '', '');
        }
        //status=1,scrollbars=0,toolbar=0,resizable=1, menubar=0
        function openeditwindow(CompCode, JobId, CompJobId) {
            window.open('AddJobsToComponents.aspx?CompCode=' + CompCode + '&Type=EDIT&JobId=' + JobId + '&CJId=' + CompJobId, '', '');
        }
        //'status=1,scrollbars=0,toolbar=0,menubar=0'
        function opensearchwindow() {
            if (typeof (winref) == 'undefined' || winref.closed) {
                winref = window.open('SearchComponents.aspx', '', '');
            }
            else {
                winref.focus();
            }
        }
        function reloadme() {
            document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();
            //__doPostBack('btnRefresh', '');
        }
        function reloadComponents(CompCode) {
            document.getElementById('ctl00_ContentMainMaster_hfSearchCode').value = CompCode;
            document.getElementById('ctl00_ContentMainMaster_btnSearchedCode').click();
            //__doPostBack('btnSearchedCode', '');
        }
        function setFocus(ctltitle) {
            ctltitle = ctltitle.toLowerCase().replace(/^\s+|\s+$/g, "");
            ctls = document.getElementsByTagName("a");
            i = 0;
            for (i = 0; i <= ctls.length - 1; i++) {
                var v = ctls[i].title.toLowerCase().replace(/^\s+|\s+$/g, "");
                if (v == ctltitle) {
                    ctls[i].focus();
                    dvscroll_Componenttree.scrollLeft = 0;
                    dvscroll_Componenttree.scrollTop = dvscroll_Componenttree.scrollTop + 50;
                }
            }
        }
        function opencompreport(Mode, Vessel) {
            window.open('Reports/OfficeComponentReport.aspx?Mode=' + Mode + '&Vessel=' + Vessel, '', '');
        }
        function opencompjobreport(Mode, Vessel) {
            window.open('Reports/OfficeComponentJobsReport.aspx?Mode=' + Mode + '&Vessel=' + Vessel, '', '');
        }
        function OpenOfficeJobes(CompJobID) {
            window.open('Reports/JobDesc.aspx?CompJobId=' + CompJobID, '', '');
        }
        function showAttachment(file) {
            window.open('UploadFiles/AttachmentForm/' + file);
        }
        function showRisk(file) {
            window.open('UploadFiles/RiskAssessment/' + file);
        }
    </script>

    <script language="javascript" type="text/javascript">


        function Validation() {

            //var txtComponentCode = document.getElementById('txtComponentCode').value; // textbox 1 ID is txt1
            //var txtUnitCode = document.getElementById('txtUnitCode').value; // textbox 2 ID is txt2
            //var txtComponentName = document.getElementById('txtComponentName').value; // textbox 2 ID is txt2
            if (document.getElementById("<%=txtComponentCode.ClientID %>").value == "") {
                alert("Please fill Component code");
                txtComponentName.focus();
                return false;
            }
            if (document.getElementById("<%=txtUnitCode.ClientID %>").value == "") {
                alert("Please fill Unit code");
                txtUnitCode.focus();
                return false;
            }
            if (document.getElementById("<%=txtComponentName.ClientID %>").value == "") {
                alert("Please fill Component Name");
                txtComponentName.focus();
                return false;
            }
            return true;

        }

    </script>
    <script type="text/javascript">
    function EnableDisableTextBox() {
        var txtComponentCode = document.getElementById("ctl00_ContentMainMaster_txtComponentCode");
        var txtUnitCode = document.getElementById("ctl00_ContentMainMaster_txtUnitCode");
        var txtComponentName = document.getElementById("ctl00_ContentMainMaster_txtComponentName");
      
        txtComponentCode.removeAttribute("disabled");
        txtUnitCode.removeAttribute("disabled");
        txtComponentName.removeAttribute("disabled");
        
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <%--<div id="header">
                                  <ul>
                                    <li><a href="Components.aspx"><span>Component Management</span></a></li>
                                  </ul>
                                </div>--%>
        <div class="text headerband">
            Component Management
        </div>
        <table style="width :100%;" cellpadding="0" cellspacing="0">
        
        <tr>
       <%-- <td style="width:150px; text-align :left; vertical-align : top;">
            <uc2:Left runat="server" ID="menu2" />  
        </td>--%>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <%--<tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >
                    <img runat="server" id="imgHelp" moduleid="10" style ="cursor:pointer;float :right; padding-right :5px;" src="images/help.png" alt="Help ?"/> 
                    Planned Maintenance System ( PMS ) - Office Master</td>
            </tr>--%>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:5px; padding-left:2px;">
                                
                                <script>
                                function tabing(){
                                     var strHref = window.location.href;
	                                 var strQueryString = strHref.split(".aspx");
	                                 var aQueryString = strQueryString[0].split("/");
	                                 var curpage =unescape(aQueryString[aQueryString.length-1]).toLowerCase();
	                                 //alert(document.getElementById('navigation1').getElementsByTagName('li').length);
	                                 var menunum=document.getElementById('header').getElementsByTagName('li').length;
	                                 var menu=document.getElementById('header').getElementsByTagName('li')
//	                                 if(curpage=='inspection'){
//	                                     menu[1].className='current';
//	                                 }else{
                                     for(x=0; x<menunum; x++)
	                                 {
	                                    //alert(unescape(menu[x].childNodes[0]).toLowerCase());
	 	                                if(unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1 ){
	 	                                //alert(menu[x].childNodes[0])
		                                menu[x].className='current';
		                                //}
	                                    }
	                                 }
                                }
                                tabing();
                                </script>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 2px; padding-top:2px">
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
                            <asp:UpdatePanel runat="server" id="up1" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <div style="width:100%; height:417px; border:0px solid #000;overflow:hidden" >
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                            <td style="width:350px; text-align:left; padding-top:5px;">
                                 <div style="padding-bottom:5px" >
                             
                             <%-- <asp:ImageButton ID="btnSearchComponents" ImageUrl ="~/Modules/PMS/Images/find.jpg" 
                                     runat="server" onclick="btnSearchComponents_Click" ToolTip="Search Components" 
                                     Visible="false" style="float:right; margin-right :5px;" 
                                     CausesValidation="False"/>--%>
                                   <span style="float :right">
                              <asp:Button ID="btnSearchComponents" ToolTip="Find Component" runat="server" forecolor="Black" Text="&nbsp;&nbsp;&nbsp;&nbsp;Search" CssClass="btn" Width="110px" OnClick="btnSearchComponents_Click" style="background-image:url(/Modules/PMS/Images/find.jpg); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px"/>       
                                     </span>
                              
                            </div> 
                            <div id="dvscroll_Componenttree" style="width :350px; overflow-y:scroll; overflow-x: hidden;  height :350px; " onscroll="SetScrollPos(this)" class="scrollbox" >
                               <asp:TreeView ID="tvComponents" ShowLines="true" runat="server" OnTreeNodePopulate="tvComponents_TreeNodePopulate" onselectednodechanged="tvComponents_SelectedNodeChanged">
                                <LevelStyles>
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="Red" />
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="Purple" />
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="DarkGreen" />
                                  <asp:TreeNodeStyle Font-Underline="False"  />
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="Black" />
                                  
                                </LevelStyles>
                                <HoverNodeStyle CssClass="treehovernode" />
                                <SelectedNodeStyle CssClass="treeselectednode" />                                
                                </asp:TreeView>
                                <asp:HiddenField ID="hfSearchCode" runat="server"  />
                                <asp:Button ID="btnSearchedCode" style="display:none" runat="server" onclick="btnSearchedCode_Click" />                                
                            </div>
                            <div style="width :350px; height :30px; padding :3px; vertical-align:top;text-align :center">    
                                    <asp:Button ID="btnPrintCompList" ToolTip="Print Components" runat="server" forecolor="Black" Text="&nbsp;&nbsp;&nbsp;&nbsp;Components" CssClass="btn" Width="110px" OnClick="btnPrintCompList_Click" style="background-image:url(/Modules/PMS/Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" /> &nbsp;     
                                    <asp:Button ID="btnPrintJobs" ToolTip="Print Jobs" runat="server" forecolor="Black" Text="&nbsp;&nbsp;&nbsp;&nbsp;Jobs" CssClass="btn" Width="60px" OnClick="btnPrintJobs_Click" style="background-image:url(/Modules/PMS/Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px"/>&nbsp; 
                                    <asp:Button ID="btnExport" ToolTip="Export Components And Jobs To Excel" runat="server" OnClientClick="javascript:return confirm('Are you sure to export?');" Text="Export" Width="50px"  CssClass="btn" onclick="btnExport_Click" style="border :solid 1px gray;"/>
                            </div>
                            </td>
                            <td style="text-align :left; ">                           
                            <asp:Panel ID="plComponent" runat="server" Width="100%" style="padding-top:5px; " >
                            <div style="height: 31px; padding-top:1px; text-align :left; " class="dottedscrollbox" >
                            <asp:Button ID="btnCompComponentMaster" runat="server" CssClass="btn1" 
                                    onclick="btnComponentMaster_Click" style="float :left" 
                                    Text="Add/Modify Component" Width="150px" CausesValidation="false" />
                            <asp:Button ID="btnCompJobMapping" Text="Add/Modify Jobs" CssClass="btn1" 
                                    Width="140px" style="float :left" runat="server" 
                                    onclick="btnJobMapping_Click" Visible="false"  CausesValidation="false"/>
                           </div>
                           <div style="height: 31px; padding-top:1px; text-align :left; " class="dottedscrollbox" >
                           <div style=" padding-top :5px;  " >
                            <asp:ImageButton ID="btnAddComponents" ImageUrl="~/Modules/PMS/Images/add1.png" runat="server" onclick="btnAddComponents_Click" ToolTip="Add New Child Component" style="float :left;padding-left :5px;" CausesValidation="False" OnClientClick="EnableDisableTextBox()" /> 
                            <asp:ImageButton ID="btnEditComponents" ImageUrl="~/Modules/PMS/Images/edit1.png" runat="server" onclick="btnEditComponents_Click" ToolTip="Edit Selected Component" style="float :left; padding-left :5px;" CausesValidation="False"/>
                            <%--<asp:ImageButton ID="btnMoveComponent" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/move.png" runat="server" ToolTip="Move Selected Component" style="float :left; padding-left :5px; display:none;"/>--%>
                            <asp:ImageButton ID="btnDeleteComponents" ImageUrl="~/Modules/PMS/Images/delete1.png" runat="server" OnClientClick="javascript:return confirm('Are you sure to delete this component?')" ToolTip="Delete Selected Component" style="float :left; padding-left :5px;" onclick="btnDeleteComponents_Click" CausesValidation="False" />
                            
                            <asp:Label ID="lblCompCode" Font-Bold="true" style="float :left;padding-left :5px; padding-top:3px;" runat="server"></asp:Label>
                              <asp:LinkButton ID="btnMoveComponent" CausesValidation="false" runat="server"  Text="Move Selected Component" style="float :right; padding-right :5px; display:none;color:#206020;font-size:12px;"/>
                            </div>
                           </div>
                           <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader">
                           Add/Modify Component
                           </div> 
                           <div style="width :100%; overflow-y:hidden; overflow-x:hidden; height :280px;" class="scrollbox" >
                            <table cellspacing="4" style=" border-collapse: collapse;width:100%;" border="1" bordercolor="#c2c2c2" rules="rows" cellpadding="3" >
                            <tr>
                            <td>Linked To:&nbsp;</td>
                            <td>
                                <asp:Label ID="lblLinkedto" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td>Component Code :</td>
                            <td>
                                <asp:TextBox ID="txtComponentCode"  style="text-align:right" runat="server" MaxLength="12"  Width="75px"></asp:TextBox>
                                <asp:TextBox ID="txtUnitCode"  style="text-align:left" runat="server" MaxLength="2" Visible="False" Width="30px"></asp:TextBox>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtComponentCode" FilterType="Numbers,Custom" ValidChars="."  runat="server"></asp:FilteredTextBoxExtender>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtComponentCode" Mask="999.99.99.99" MaskType="Number" InputDirection="LeftToRight"  DisplayMoney="Left" runat="server" ></asp:MaskedEditExtender>--%>
                                </td>                                
                            </tr>
                            <tr>
                            <td>Component Name :</td>
                            <td>
                                <asp:TextBox ID="txtComponentName" runat="server"  MaxLength="100" Width="350px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr><td>Description :<asp:HiddenField ID="hfCompCode" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComponentDesc" runat="server" Height="66px" 
                                        MaxLength="500" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                </td>
                                </tr>
                            <tr>                             
                            <td>
                                <asp:HiddenField ID="hfCompId" runat="server" />
                                </td>
                                <td style="text-align:left">
                                    <table cellpadding="0" cellspacing="0" width="355px">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkClass" runat="server" />Class EQIP
                                            </td>
                                            <td style="vertical-align:middle">
                                               <%-- Class EQIP Code :--%></td>
                                            <td style=" text-align:right">
                                                <%--<asp:TextBox ID="txtClassCode" runat="server" Enabled="false" MaxLength="30"></asp:TextBox>--%>
                                            </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <asp:CheckBox ID="chkSms" runat="server" AutoPostBack="True" oncheckedchanged="chkSms_CheckedChanged" />SIRE EQIP
                                                </td>
                                                <td style="vertical-align:middle">
                                                    SIRE Code :</td>
                                                <td style=" text-align:right">
                                                    <asp:TextBox ID="txtSmsCode" runat="server" Enabled="false" MaxLength="30"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel runat="server" ID="ypp1">
                                                    <ContentTemplate>
                                                    <asp:CheckBox ID="chkCritical" runat="server" AutoPostBack="true" OnCheckedChanged="chkCritical_CheckChanged" />Critical EQIP <asp:CheckBox ID="chkCE" Text="Critical for Environment" runat="server" />
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <%--<asp:CheckBox ID="chkCSM" runat="server" />CSM Item--%>
                                                </td>
                                            </tr>                                     
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkInactive" runat="server" />Inactive
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRHComponent" runat="server" AutoPostBack="True" OnCheckedChanged="chkRHComponent_CheckedChanged" /> RH Component
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td id="tdIntParent" colspan="2" runat="server">
                                                    <asp:CheckBox ID="chkInhParent" runat="server" />Apply Job To Child
                                                </td>                                            
                                            </tr>    --%>                                                                                
                                    </table>
                                </td>
                            </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                    <table>
                                    <tr>
                                     <td><asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="btnSave_Click" Text="Save" Visible="false" CausesValidation="false" OnClientClick="return Validation();"/></td>
                                    <td style="text-align:left"><asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" Visible="false" onclick="btnCancel_Click" CausesValidation="false"/>                                        
                                    </td>  
                                    </tr>
                                    </table>
                                    </td>                                    
                                </tr>
                            </table>                        
                            </div>
                            </asp:Panel>
                            <asp:Panel ID="plJobsMapping" runat="server" Width="100%" style="padding-top:2px; ">
                            <table width="100%" border="0">
                            <tr>
                            <td>
                            <div style="height: 31px; padding :0px; text-align :left;" class="dottedscrollbox">
                            <asp:Button ID="btnJobComponentmaster" runat="server" CssClass="btn1" onclick="btnComponentMaster_Click" style="float :left" Text="Add/Modify Component" Width="150px" />
                            <asp:Button ID="btnJobJobMapping" Text="Add/Modify Jobs" CssClass="btn1" Width="150px" style="float :left" runat="server" onclick="btnJobMapping_Click" />
                            <asp:Button ID="btnRefresh" style="display:none;" runat="server" onclick="btnRefresh_Click" /></div>
                            <div style="height: 31px; padding :0px; text-align :left;" class="dottedscrollbox">
                            <div style=" padding-top :5px;  " >
                            <asp:ImageButton ID="imgbtnAddJobs" ImageUrl="~/Modules/PMS/Images/add1.png" style="float :left;padding-left :5px;" runat="server" onclick="btnAddJobs_Click" ToolTip="Add/Edit Jobs"/>
                            <asp:ImageButton ID="imgbtnEditJobs" ImageUrl="~/Modules/PMS/Images/edit1.png" style="float :left;padding-left :5px;" runat="server" onclick="btnEditJobs_Click"  ToolTip="Edit Selected Jobs" />
                            <asp:ImageButton ID="btnDelete" ImageUrl="~/Modules/PMS/Images/delete1.png" style="float :left;padding-left :5px;" ToolTip="Delete Selected Jobs" OnClientClick="return confirm('Are you sure to delete?');" runat="server" onclick="btnDelete_Click" Visible="False" />
                           <%-- <asp:ImageButton id="btnCopyJobs"  CausesValidation="false" ImageUrl="~/Modules/PMS/Images/copy_job.png" runat="server" ToolTip="Copy Jobs" style="float :left;padding-left :5px; display:none;"/>--%>
                            <asp:ImageButton ID="btnPrint" ImageUrl="~/Modules/PMS/Images/print.png" style="float :left;padding-left :5px;" runat="server" ToolTip="Print Jobs" OnClick="btnPrint_OnClick" />
                            <asp:Label ID="lblComponentCode" Font-Bold="true" style="float :left;padding-left :5px; padding-top:3px;" runat="server"></asp:Label>
                                 <asp:LinkButton id="btnCopyJobs"  CausesValidation="false"  runat="server" Text="Copy Jobs" style="float :right;padding-right :5px; display:none;color:#206020;font-size:12px;"/>
                            </div>
                            </div> 
                            <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader">
                           Add/Modify Jobs
                           </div> 
                            </td>
                            </tr>
                            <tr>
                            <td>
                            <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                            <col style="text-align :center" width="3%" /> 
                            <col style="text-align :left" width="10%"  />
                            <col style="text-align :left" width="51%" />
                            <col style="text-align :left" width="10%"  />
                            <col style="text-align :left" width="10%" />
                            <col style="text-align :left" width="10%" />
                            <col style="text-align :center" width="3%" />
                            <%--<col style="width:4%;" />--%>
                            <col width="3%" />
                            </colgroup>
                            <tr class="headerstylegrid">
                                <td width="3%">&nbsp;</td>
                                <td width="10%">
                                    &nbsp;Job Code</td>
                                <td width="51%">
                                    &nbsp;Job Description</td>
                                <td width="10%">
                                    &nbsp;Dept.
                                </td>
                                <td width="10%">
                                    &nbsp;Assign To</td>    
                                <td width="10%">&nbsp;Int. Type</td>
                                <td width="3%">
                                    <img src="Images/paperclip.gif" style="border:none;" />
                                </td>
                              <%--<td style="text-align:center;" width="4%"><img title="Attachments" src="Images/wc-upload.png" style="cursor:pointer;" /></td> --%>   
                                <td width="3%">&nbsp;</td>
                            </tr>
                                        
                             </table>
                            <div id="dvscroll_JobMaster" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :263px;" class="scrollbox" onscroll="SetScrollPos(this)" >                                               
                            <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="text-align :center" width="3%" /> 
                            <col style="text-align :left" width="10%"  />
                            <col style="text-align :left" width="51%" />
                            <col style="text-align :left" width="10%"  />
                            <col style="text-align :left" width="10%" />
                            <col style="text-align :left" width="10%" />
                            <col style="text-align :center" width="3%" />
                         <%--   <col style="width:4%;" />--%>
                            <col  style="text-align :center" width="1.5%" />
                                </colgroup>
                                <asp:Repeater ID="rptComponentJobs" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Eval("CompJobId").ToString()==SelectedCompJobId.ToString())?"selectedrow":"" %>' ondblclick="OpenOfficeJobes('<%# Eval("CompJobId") %>')">
                                            <td width="3%"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("CompJobId") %>' ImageUrl="~/Modules/PMS/Images/magnifier.png" OnClick="btnView_Click" /></td>
                                            <td width="10%"><%#Eval("JobCode")%><asp:HiddenField ID="hfJobId" Value='<%# Eval("JobId")%>' runat="server" /></td>
                                            <td width="51%"><%#Eval("DescrSh")%>
                                            <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                            </td>
                                            <td width="10%"><%#Eval("DeptName")%></td>
                                            <td width="10%"><%#Eval("RankCode")%></td>
                                            <td style="text-align :center" width="10%"><%#Eval("IntervalName")%></td>
                                            <td width="3%">
                                                <a  href="DocManagement.aspx?CJID=<%#Eval("CompJobId") %>" target="_blank" style='Display:<%#((Eval("AttachmentCount").ToString()=="0")?"none":"block")%>' > <img src="Images/paperclip.gif" style="border:none;" /></a>
                                                <%--<img src="Images/paperclip.gif" title="Company Form" style='cursor:pointer;<%#(Eval("AttachmentForm").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("AttachmentForm")%>' onclick="showAttachment(this.getAttribute('file'));"/>--%>
                                             </td>
                                           <%-- <td>
                                                <a href="UpdateJobInterval.aspx?ComponentId=<%#Eval("ComponentId")%>&CJID=<%#Eval("CompJobID")%>&VesselCode=LIG" target="_blank"> <img src="Images/copy-blue.png" style="border:none;" title="Copy Jobs" /> </a>
                                            </td>--%>
                                             <%--<td>
                                                <img src="Images/paperclip.gif" title="Risk Assessment" style='cursor:pointer;<%#(Eval("RiskAssessment").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("RiskAssessment")%>' onclick="showRisk(this.getAttribute('file'));"/>
                                             </td>
                                             <td style="text-align :center" width="4%">
                                                <a href="ShipJobExecAttachments.aspx?ComponentId=<%#Eval("ComponentId")%>&CJID=<%#Eval("CompJobID")%>" target="_blank"> <img src="Images/wc-upload.png" style="border:none;" title="Attachments Need to Execute." /> </a>
                                            </td>--%>
                                            <td></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                            </div>
                            </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:MessageBox ID="MessageBox2" runat="server" />                                    
                                </td>
                            </tr>
                            </table>                        
                            </asp:Panel>
                            <div style="padding-top : 5px;" >
                            <uc1:MessageBox ID="MessageBox1" runat="server" />
                            </div> 
                           </td>
                           </tr></table>
                           </div>
                           <asp:Panel runat="server" ID="pnlUpdate" Width="325px">
                                <table width="100%" style=" background-color :White; " cellpadding="5">
                                <tr><td class="text headerband"><br/>Enter New Component Code</td></tr>
                                <tr><td><asp:TextBox ID="txtNewCode" runat="server" MaxLength="12"></asp:TextBox></td></tr>
                                <tr>
                                <td>
                                
                                <asp:UpdatePanel runat="server" id="UpdatePanel1" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:Button ID="btnCopyComponent" ToolTip="Copy Component" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Copy" CssClass="btn" Width="80px" style="background-image:url(Images/save.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" OnClick="btnCopyComponent_Click" forecolor="Black" />   
                                <asp:Button ID="btnUpdateComponentCode" ToolTip="Move ComponentCode" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Move" CssClass="btn" Width="80px" OnClick="btnUpdateCompCode_Click" style="background-image:url(Images/save.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" forecolor="Black" />     
                                <asp:Button ID="btnCancelUpdate" ToolTip="Cancel Move" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Cancel" CssClass="btn" Width="80px" style="background-image:url(Images/cancel.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" forecolor="Black" />     
                                <asp:LinkButton ID="lbtnOk" Text="OK" style="display :none " runat="server"></asp:LinkButton>
                                <br />
                                <br />
                                <asp:Label ID="lblPopupErrorMsg" ForeColor="Red" Font-Size="12px" runat="server"></asp:Label>
                                
                                </ContentTemplate>
                                <Triggers>
                                <asp:PostBackTrigger ControlID="lbtnOk" />
                                <asp:PostBackTrigger ControlID="btnCancelUpdate" /> 
                                <asp:PostBackTrigger ControlID="btnUpdateComponentCode" /> 
                                <asp:PostBackTrigger ControlID="btnCopyComponent" />
                                </Triggers> 
                                
                                </asp:UpdatePanel>
                                </td>
                                </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlCopyJob" Width="325px">
                                <table width="100%" style=" background-color :White; " cellpadding="5">
                                <tr><td class="text headerband">Enter Component Code</td></tr>
                                <tr style="text-align:center"><td>
                                <asp:DropDownList ID="ddlComponents" runat="server" Visible="false"></asp:DropDownList>
                                <asp:TextBox runat="server" style="background-color:Yellow;" Width="120px" MaxLength="12" id="txtCopyCompToCode" CausesValidation="true" ValidationGroup="vg"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtCopyCompToCode" ErrorMessage="*" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                </td>
                                </tr>
                                <tr>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="radjobtype" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="All Jobs" Value="A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Selected Jobs" Value="S"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                </tr>
                                <tr>
                                <td>
                                
                                <asp:UpdatePanel runat="server" id="UpdatePanel2" UpdateMode="Conditional">
                                <ContentTemplate>
                                <asp:Button ID="btnCopyJob" OnClick="btnCopyJob_Click" ToolTip="Copy Job" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Copy Job" CssClass="btn" Width="80px" style="background-image:url(Images/save.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" ValidationGroup="vg" forecolor="Black"/>
                                <asp:Button ID="btnCancelCopyJob" ToolTip="Cancel" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Cancel" CssClass="btn" Width="80px" style="background-image:url(Images/cancel.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" CausesValidation="false" forecolor="Black" />     
                                <asp:LinkButton ID="lbtnCopyJobOk" Text="OK" style="display :none " runat="server"></asp:LinkButton>
                                <br />
                                <br />
                                <asp:Label ID="lblCopyJobErrMsg" ForeColor="Red" Font-Size="12px" runat="server"></asp:Label>
                                
                                </ContentTemplate>
                                <Triggers>
                                <asp:PostBackTrigger ControlID="lbtnCopyJobOk" />
                                <asp:PostBackTrigger ControlID="btnCancelCopyJob" />
                                <asp:PostBackTrigger ControlID="btnCopyJob" />
                                </Triggers>
                                </asp:UpdatePanel>
                                </td>
                                </tr>
                                </table>
                            </asp:Panel>
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnMoveComponent" OkControlID="lbtnOk" BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlUpdate"  CancelControlID="btnCancelUpdate" ></asp:ModalPopupExtender>
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnCopyJobs" OkControlID="lbtnCopyJobOk" BackgroundCssClass="modalBackground" DropShadow="true" PopupControlID="pnlCopyJob"  CancelControlID="btnCancelCopyJob" ></asp:ModalPopupExtender>
                           </ContentTemplate>
                           </asp:UpdatePanel>  
                           </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td>
        </tr>
        </table>  

     </div>
</asp:Content>
