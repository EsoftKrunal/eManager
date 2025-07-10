<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_OFC_Home.aspx.cs" Inherits="DD_OFC_Home" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <%--<link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />--%>
    <title>eMANAGER</title>
    <style type="text/css">
.status_C
{
    background-image:url('../Images/green_circle.gif');
    width:12px; 
    height:12px;
}
.status_E
{
    background-image:url('../Images/orange_circle.png');
    width:12px; 
    height:12px;
}
.status_P
{
    background-image:url('../Images/yellow_circle.png');
    width:12px; 
    height:12px;
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
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
      <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
       <div class="text headerband">
          Dry Docking
       </div>
        <div class="box_withpad" style="min-height:450px">
       <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        <td >
            <div style="text-align: left; border: none; padding: 0px; margin: 0px;">

                                            <asp:Button runat="server" ID="btnDDTracker" Text="DD Tracker" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="0" />
                                            <asp:Button runat="server" ID="btnDocket" Text="DD Mgmt" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="1" />
                                            <asp:Button runat="server" ID="btnJobMaster" Text="DD Master" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="2" />
                                            <asp:Button runat="server" ID="btnDDPlanSettings" Text="Planner Settings" CssClass="btn1" OnClick="RegisterSelect" CommandArgument="3" />
                                            
                                               
                                            </div>
                                      
        </td>
            </tr>
           <tr>
               <td>
                   <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="DD_OFC_Tracker.aspx" id="frm" frameborder="0" width="100%" height="485px" scrolling="no"></iframe>
</div>
               </td>
           </tr>
           
            </table >
           <table id="tbl1" runat="server" visible="false"> 
               <tr> 
        <td style=" text-align :left; vertical-align : top;" > 

            <table style="width :100%" cellpadding="0" cellspacing="0" border="0" height="465px" >
            <tr>  
            <td>
             <div class="box1">
             <b>Select Year :</b>&nbsp;<asp:DropDownList ID="ddlYear" Width="70px" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList>
             </div>
             <div class="dvScrollheader">  
                           <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:20px;" />
                                </colgroup>
                                <tr>
                                    <td style="text-align:left;">Vessel Name</td>
                                    <td style="text-align:center;">JAN</td>
                                    <td style="text-align:center;">FEB</td>
                                    <td style="text-align:center;">MAR</td>
                                    <td style="text-align:center;">APR</td>
                                    <td style="text-align:center;">MAY</td>
                                    <td style="text-align:center;">JUN</td>
                                    <td style="text-align:center;">JUL</td>
                                    <td style="text-align:center;">AUG</td>
                                    <td style="text-align:center;">SEP</td>
                                    <td style="text-align:center;">OCT</td>
                                    <td style="text-align:center;">NOV</td>
                                    <td style="text-align:center;">DEC</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            </div>
             <div style="HEIGHT: 405px;"  class="dvScrolldata">
                           <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;" />
                                    <col style="width:20px;" />
                                </colgroup>
                                <asp:Repeater ID="rptDocket" runat="server">
                                    <ItemTemplate>
                                            <tr>                                                
                                                <td style="text-align:left">
                                                <%--<a target="_blank" href='DD_OFC_VesselDockets.aspx?VesselId=<%#Eval("VesselId")%>&VesselName=<%#Eval("VesselName")%>'>--%>
                                                <%#Eval("VesselName")%>
                                                <%--</a>--%>
                                                </td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Jan").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Feb").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Mar").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Apr").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("May").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Jun").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Jul").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Aug").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Sep").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Oct").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Nov").ToString()%>' ></div></td>
                                                <td align="center"> <div class='<%#"status_" + Eval("Dec").ToString()%>' ></div></td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>
            </div>
            </td>
            </tr>
            </table>
        </td> 
        </tr>
        </table>
        </div>
    </div>
    </asp:Content>
