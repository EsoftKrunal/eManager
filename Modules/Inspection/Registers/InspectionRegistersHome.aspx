<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InspectionRegistersHome.aspx.cs" Inherits="Modules_Inspection_Registers_InspectionRegistersHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <%--<link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <%--<link href="Styles/MTMSheet.css" rel="stylesheet" type="text/css" />--%>
   <%-- <link href="Styles/tabs.css" rel="stylesheet" type="text/css" />--%>
      <script language="javascript" type="text/javascript">
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
// prtContent.innerHTML=strOldOne;
}
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
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="text-align: center">
        
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9 " width="100%">
            <tr>
                <td align="center" style=" height:23px;text-align :center;" class="text headerband" >
                   Inspection - Registers
                </td>
            </tr>
            <tr>
                <td style="height:485px; background-color:#f9f9f9" valign="top">
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:10px; padding-left:3px;padding-top:3px;padding-bottom:3px;text-align:left;">
                                <div id="header"><%--tabs--%>
                                     <asp:Button ID ="btnInsGroup" runat="server" CommandArgument="0" Text="Inspection Group" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                    <asp:Button ID ="btnIns" runat="server" CommandArgument="1" Text="Inspection" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                     <asp:Button ID ="btnMainChapter" runat="server" CommandArgument="2" Text="Main Chapter" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                     <asp:Button ID ="btnSubChapter" runat="server" CommandArgument="3" Text="Sub Chapter" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                    <asp:Button ID ="btnInsSettings" runat="server" CommandArgument="4" Text="Inspection Settings" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                    <asp:Button ID ="btnInsVersion" runat="server" CommandArgument="5" Text="Inspection Version" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                    <asp:Button ID ="btnInsReportCat" runat="server" CommandArgument="6" Text="Insp Report Cat." OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                    <asp:Button ID ="btnIntInsChecklist" runat="server" CommandArgument="7" Text="Internal Inspection Checklist" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                     <asp:Button ID ="btnVIMSTools" runat="server" CommandArgument="8" Text="VIMS Tools" OnClick="btnTabs_Click" CssClass="btn1" /> &nbsp;
                                  <%--<ul>
                                    <li> <a href="InspectionGroup.aspx"><span>Inspection Group</span></a></li>
                                    <li><a href="Inspection.aspx"><span>Inspection</span></a></li>
                                    <li><a href="ChaptersEntry.aspx"><span>Main Chapter</span></a></li>
                                    <li><a href="SubChapter.aspx"><span>Sub Chapter</span></a></li>
                                    <li><a href="InspectionChecklist.aspx"><span>CheckList</span></a></li>
                                    <li><a href="DocumentType.aspx"><span>Document Type</span></a></li>
	                                <li><a href="InspectionSettings.aspx"><span>Inspection Settings</span></a></li>
	                                <li><a href="InspectionChangeStatus.aspx"><span>VIMS Tools</span></a></li>
	                                <li><a href="InspVersions.aspx"><span>Inspection Version</span></a></li>
	                                <li><a href="IRM_GroupMaster.aspx"><span>Insurance Group</span></a></li>
	                                <li><a href="IRM_SubGroupMaster.aspx"><span>Insurance SubGroup</span></a></li>
                                    <li><a href="IRM_UnderWriterMaster.aspx"><span>Insurance UW</span></a></li>
                                    <li><a href="KPIParameters.aspx"><span>KPI </span></a></li>
                                    <li><a href="Psccode.aspx"><span>PSC Code </span></a></li>
                                    <li><a href="InspReportCat.aspx"><span>Insp Report Cat.</span></a></li>
                                    <li><a href="BonusScale.aspx"><span>Bonus Scale</span></a></li>
                                    <li><a href="VesselReportGroup.aspx"><span>Vessel Report Group</span></a></li>
                                    <li><a href="VesselReports.aspx"><span>Vessel Reports</span></a></li>
                                    <li><a href="InternalInspectionsQuestions.aspx"><span>Internal Inspection Checklist </span></a></li>
                                  </ul>--%>
                                  
                                  
                                </div>
                                <script>
                                    function tabing() {
                                        var strHref = window.location.href;
                                        var strQueryString = strHref.split(".aspx");
                                        var aQueryString = strQueryString[0].split("/");
                                        var curpage = unescape(aQueryString[aQueryString.length - 1]).toLowerCase();
                                        //alert(curpage); 
                                        //alert(document.getElementById('navigation1').getElementsByTagName('li').length);
                                        var menunum = document.getElementById('header').getElementsByTagName('li').length;
                                        var menu = document.getElementById('header').getElementsByTagName('li')
                                        if (curpage == 'inspection') {
                                            menu[1].className = 'current';
                                        } else {
                                            for (x = 0; x < menunum; x++) {
                                                //alert(unescape(menu[x].childNodes[0]).toLowerCase());

                                                if (unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1) {
                                                    //alert(menu[x].childNodes[0])
                                                    menu[x].className = 'current';
                                                }

                                            }
                                        }
                                    }
                                    tabing();
                                </script>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 10px; height:10px;">
   <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/Inspection/Registers/InspectionGroup.aspx" id="frm" frameborder="0" width="100%" height="525px" scrolling="no"></iframe>
</div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         </td> 
    </tr></table> 
        
     </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainFoot" Runat="Server">
</asp:Content>

