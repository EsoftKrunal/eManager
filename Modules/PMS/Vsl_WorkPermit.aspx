<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vsl_WorkPermit.aspx.cs" Inherits="Vsl_WorkPermit" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
         <tr><td>
        <hm:HMenu runat="server" ID="menu2" />  
        </td></tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:5px; padding-left:2px;">
                                <div id="header">
                                  <ul>
                                    <li><a href="Search.aspx"><span>Search Jobs</span></a></li>
                                    <%if (Session["UserType"].ToString().Trim() == "S")
                                      {%>
                                    <li><a href="MaintenancePlanning.aspx"><span>Report Job</span></a></li>
                                    <% }%>
                                    <li><a href="Vsl_JobDescription.aspx"><span>Job Description</span></a></li>
                                    <li><a href="Vsl_SpareRequired.aspx"><span>Spares</span></a></li>
                                    <li><a href="Vsl_WorkPermit.aspx"><span>Work Permit</span></a></li>
                                    <li><a href="JobUpdateHistory.aspx"><span>History</span></a></li>
                                  </ul>
                                </div>
                                <script>
                                function tabing(){
                                     var strHref = window.location.href;
	                                 var strQueryString = strHref.split(".aspx");
	                                 var aQueryString = strQueryString[0].split("/");
	                                 var curpage =unescape(aQueryString[aQueryString.length-1]).toLowerCase();
	                                
	                                 var menunum=document.getElementById('header').getElementsByTagName('li').length;
	                                 var menu=document.getElementById('header').getElementsByTagName('li')
                                     for(x=0; x<menunum; x++)
	                                 {
	 	                                if(unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1 ){
		                                menu[x].className='current';
	                                    }
	                                 }
                                }
                                tabing();
                                </script>
                            </td>
                        </tr>
                        <tr style=" padding-top:2px;">
                            <td colspan="2" style="padding-right: 5px; padding-left: 2px;">
                            
                            <div style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" ><%--height:460px;--%>
                            
                    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                         <tr>
                            <td style="text-align:left; padding-left: 5px; padding-right: 5px; height:3px">
                              
                            </td>
                        </tr>
                        <%--<tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <tr class= "headerstylegrid">
                                 <td>Spares</td>
                            </tr>
                            <tr>
                                    <td>
                                         <table border="0" cellpadding="0" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <tr>
                                                    <td style="text-align:right; font-weight:bold">Vessel :&nbsp;</td>
                                                    <td style="text-align:left;"><asp:Label ID="lblSpVessel" runat="server"></asp:Label> </td>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align:right; font-weight:bold">Component :&nbsp;</td>
                                                    <td style="text-align:left;"><asp:Label ID="lblSpComponent" runat="server"></asp:Label> </td>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align:right; font-weight:bold">Job :&nbsp;</td>
                                                    <td style="text-align:left;"><asp:Label ID="lblSpJob" runat="server"></asp:Label> </td>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align:right; font-weight:bold">Interval :&nbsp;</td>
                                                    <td style="text-align:left;"><asp:Label ID="lblSPInterval" runat="server"></asp:Label> </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                         </table>
                                    </td>
                                    </tr>
                                    <tr>
                                       <td>
                                       
                               
           
           
           </td>
                                    </tr>
                                    </table>
                            </td>                            
                        </tr>--%>
                        
                        </table> 
                                </div>
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
    <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
