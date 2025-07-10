<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="VesselDetails.aspx.cs" Inherits="VesselDetails" Culture="auto" UICulture="auto" %>
<%--<%@ Register Src="VesselBudget.ascx" TagName="VesselBudget" TagPrefix="uc2" %>
<%@ Register Src="CrewMatrix.ascx" TagName="CrewMatrix" TagPrefix="uc5" %>--%>
<%@ Register Src="VesselDocuments.ascx" TagName="VesselDocuments" TagPrefix="uc1" %>
<%@ Register Src="VesselMiningScale.ascx" TagName="VesselMiningScale" TagPrefix="uc2" %>
<%@ Register Src="CrewMatrixUpload.ascx" TagName="CrewSireUpload" TagPrefix="ucupld" %>
<%@ Register Src="VesselDetailsGeneral.ascx" TagName="VesselDetailsGeneral" TagPrefix="uc3" %>
<%--<%@ Register Src="VesselDetailsOther.ascx" TagName="VesselDetailsOther" TagPrefix="uc4" %>--%>
<%@ Register Src="~/Modules/HRD/VesselRecord/VesselAllocation.ascx" TagName="VesselAllocation" TagPrefix="ucVA" %>
<%@ Register TagName="menu" Src="~/Modules/HRD/UserControls/VesselMenu.ascx" TagPrefix="mtm"  %>
<%--<%@ Register Src="VesselParticulars.ascx" TagName="VesselParticulars" TagPrefix="ucVP" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function Show_Image_Large(obj) {
            window.open(obj.src, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
    </script>
    <script type="text/javascript">
        // Free for any type of use so long as original notice remains unchanged.
        // Report errors to feedback@ashishware.com
        //Copyrights 2006, Ashish Patil , ashishware.com
        //////////////////////////////////////////////////////////////////////////

        function ToolTip(id, isAnimated, aniSpeed) {
            var isInit = -1;
            var div, divWidth, divHeight;
            var xincr = 10, yincr = 10;
            var animateToolTip = false;
            var html;

            function Init(id) {
                div = document.getElementById(id);
                if (div == null) return;

                if ((div.style.width == "" || div.style.height == "")) {
                    alert("Both width and height must be set");
                    return;
                }

                divWidth = parseInt(div.style.width);
                divHeight = parseInt(div.style.height);
                if (div.style.overflow != "hidden") div.style.overflow = "hidden";
                if (div.style.display != "none") div.style.display = "none";
                if (div.style.position != "absolute") div.style.position = "absolute";

                if (isAnimated && aniSpeed > 0) {
                    xincr = parseInt(divWidth / aniSpeed);
                    yincr = parseInt(divHeight / aniSpeed);
                    animateToolTip = true;
                }

                isInit++;

            }


            this.Show = function (e, strHTML) {

                if (isInit < 0) return;

                var newPosx, newPosy, height, width;
                if (typeof (document.documentElement.clientWidth) == 'number') {
                    width = document.body.clientWidth;
                    height = document.body.clientHeight;
                }
                else {
                    width = parseInt(window.innerWidth);
                    height = parseInt(window.innerHeight);

                }
                var curPosx = (e.x) ? parseInt(e.x) : parseInt(e.clientX);
                var curPosy = (e.y) ? parseInt(e.y) : parseInt(e.clientY);

                if (strHTML != null) {
                    html = strHTML;
                    div.innerHTML = html;
                }

                if ((curPosx + divWidth + 10) < width)
                    newPosx = curPosx + 10;
                else
                    newPosx = curPosx - divWidth;

                if ((curPosy + divHeight) < height)
                    newPosy = curPosy;
                else
                    newPosy = curPosy - divHeight - 10;

                if (window.pageYOffset) {
                    newPosy = newPosy + window.pageYOffset;
                    newPosx = newPosx + window.pageXOffset;
                }
                else {
                    newPosy = newPosy + document.body.scrollTop;
                    newPosx = newPosx + document.body.scrollLeft;
                }

                div.style.display = 'block';
                //debugger;
                //alert(document.body.scrollTop);
                div.style.top = newPosy + "px";
                div.style.left = newPosx + "px";

                div.focus();
                if (animateToolTip) {
                    div.style.height = "0px";
                    div.style.width = "0px";
                    ToolTip.animate(div.id, divHeight, divWidth);
                }


            }



            this.Hide = function (e) {
                div.style.display = 'none';
                if (!animateToolTip) return;
                div.style.height = "0px";
                div.style.width = "0px";
            }

            this.SetHTML = function (strHTML) {
                html = strHTML;
                div.innerHTML = html;
            }

            ToolTip.animate = function (a, iHeight, iWidth) {
                a = document.getElementById(a);

                var i = parseInt(a.style.width) + xincr;
                var j = parseInt(a.style.height) + yincr;

                if (i <= iWidth) { a.style.width = i + "px"; }
                else { a.style.width = iWidth + "px"; }

                if (j <= iHeight) { a.style.height = j + "px"; }
                else { a.style.height = iHeight + "px"; }

                if (!((i > iWidth) && (j > iHeight)))
                    setTimeout("ToolTip.animate('" + a.id + "'," + iHeight + "," + iWidth + ")", 1);
            }

            Init(id);
        }
        //--------------------------
        var t1 = null;
        var l1 = "Tooltip for line one";
        var l2 = "Tooltip for line two";
        function init() {
            t1 = new ToolTip("a", false);
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
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
<%--<body>--%>
<script  type="text/javascript">
    function OpenReport()
    {
        window.open('AddVesselDetails.aspx');
    }    
</script>


<div id="a" style="background-color:ivory; border:solid 1px black;width:184px;height:84px;text-align: left; padding-left:3px; display:none "></div>
<%--<form id="form1" runat="server">--%>
    <div style="text-align: center">
     <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table style="width :98%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" class="text headerband">
                    <%--<img runat="server" id="imgHelp" moduleid="4" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> --%>
                    Vessel Master [ <%=Session["VesselName"].ToString()%> ]
                </td>
            </tr>
        <tr>
        <td style="width:15%; text-align :left; vertical-align : top;">
            <mtm:menu runat="server" ID="menu2" />  
        </td>
        <td style=" text-align :left; vertical-align : top;width:85%;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;width:100%;">
            
            <tr>
                <td style="width: 100%">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td style="padding-right:10px; text-align:center; color:Red;  height: 13px;">
                            <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td></tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="height: 38px; width:700px;">
                                  <%--  <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal"
                                        StaticEnableDefaultPopOutImage="False" Width="250px" meta:resourcekey="Menu1Resource1">
                                        <Items>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/particulars1_a.gif"  Text=" " Value="0" ></asp:MenuItem>
                                 
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/crewDocs_d.gif" Text=" " Value="1" ></asp:MenuItem>
                                  
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/manningScale_d.gif" Text=" " Value="2" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/matrix_d.gif" Text=" " Value="3" ></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/vsl_Allocation_d.gif"  Text=" " Value="4" ></asp:MenuItem>
                                
                                    </Items>
                                    </asp:Menu>--%>

                                              <%-- <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/particulars2_d.gif" Text=" " Value="1" ></asp:MenuItem>
                                               <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/budgeting_d.gif"  Text="" Value="2" ></asp:MenuItem>  --%>
                                              <%--<asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/crewMatrix_d.gif" Text=" " Value="2" ></asp:MenuItem>--%>
                                              <%--  <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/particulars2_d.gif" Text=" " Value="5" ></asp:MenuItem>--%>
                                            <asp:Button runat="server"  CommandArgument="0" Text="VSL Overview" OnClick="Menu1_MenuItemClick" ID="b1" CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /> &nbsp;
                                             <asp:Button runat="server"  CommandArgument="1" Text="Crew Documents" OnClick="Menu1_MenuItemClick" ID="b2" CssClass="btn1"  Font-Bold="True" Width="120px" CausesValidation="False" /> &nbsp;
                                             <asp:Button runat="server"  CommandArgument="2" Text="Manning Status" OnClick="Menu1_MenuItemClick" ID="b3" CssClass="btn1"  Font-Bold="True" Width="110px" CausesValidation="False" /> &nbsp;
                                             <asp:Button runat="server"  CommandArgument="3" Text="Crew Matrix" OnClick="Menu1_MenuItemClick" ID="b4" CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /> &nbsp;
                                             <asp:Button runat="server"  CommandArgument="4" Text="VSL Allocation" OnClick="Menu1_MenuItemClick" ID="b5" CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /> &nbsp;
                                            <asp:HiddenField ID="HiddenPK" runat="server" />
                                            <asp:HiddenField ID="HiddenVesselName" runat="server" />
                                        </td>
                                        <td style=" height: 38px; text-align: center">
                                            <asp:Button ID="imgbtn_Documents" Visible="false"  runat="server" CausesValidation="False"  OnClick="imgbtn_Documents_Click" Text="Crew Documents" CssClass="btn"/></td>
                                        <td style="height: 38px; text-align: center">
                                            <asp:Button ID="imgbtn_Search" runat="server" Visible="false"  CausesValidation="False"  OnClick="imgbtn_Search_Click" ToolTip="Search" Text="Search" CssClass="btn" />
                                                <asp:Button runat="server" ID="btnback" PostBackUrl="~/Modules/HRD/crewrecord/crewsearch.aspx" Text="Back" Width="80px"  CssClass="btn" CausesValidation="false"   /> 
                                                <%--<asp:Button runat="server" ID="btnAdd" Text="Add" Width="45px"  CssClass="btn" CausesValidation="false" OnClientClick=""  /> --%>
                                                
                                                </td>
                                        <td style="height: 38px">
                                          </td>
                                    </tr>
                                </table>
                                     <div id="divPrint">
                                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="Tab1" runat="server">
                                            <uc3:VesselDetailsGeneral ID="VesselDetailsGeneral1" runat="server" />
                                        </asp:View>
                                       <%-- <asp:View ID="Tab2" runat="server">
                                            <uc4:VesselDetailsOther ID="VesselDetailsOther1" runat="server" />
                                        </asp:View>
                                        <asp:View ID="Tab3" runat="server">
                                            <uc2:VesselBudget ID="VesselBudget1" runat="server" />
                                        </asp:View>--%>
                                        <asp:View ID="Tab4" runat="server">
                                            <uc1:VesselDocuments ID="VesselDocuments1" runat="server" />   
                                        </asp:View>
                                         <%--<asp:View ID="Tab5" runat="server">
                                             <uc5:CrewMatrix ID="CrewMatrixl1" runat="server" />
                                        </asp:View>--%>
                                         <asp:View ID="Tab6" runat="server">
                                             <uc2:VesselMiningScale id="VesselMiningScale1" runat="server"></uc2:VesselMiningScale>
                                        </asp:View>
                                         <asp:View ID="View7" runat="server">
                                             <ucupld:CrewSireUpload id="VesselMiningScale2" runat="server"></ucupld:CrewSireUpload>
                                        </asp:View>
                                        <asp:View ID="View8" runat="server">
                                             <ucVA:VesselAllocation id="VesselAllocation" runat="server"></ucVA:VesselAllocation>
                                        </asp:View>
                                      <%-- <asp:View ID="View9" runat="server">
                                            <div style="width:100%;height:385px; overflow-y: hidden; overflow-x: hidden;">
                                             <ucVP:VesselParticulars ID="VesselParticulars" runat="server" />
                                            </div>
                                        </asp:View>--%>
        </asp:MultiView> </div></td></tr></table>
                </td>
            </tr>
        </table>
        </td>
        </tr>
        </table>
        </div>
   <%-- </form>
</body>--%>
    </asp:Content>

                                        
