<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMSCrewTraining.aspx.cs" Inherits="eReports_CMSCrewTraining" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="./eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="./eReports/JS/KPIScript.js" type="text/javascript"></script>

    <link href="./CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="./eReports/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="./eReports/CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="./css/jquery.datetimepicker.css"/>

</head>
<body style=" margin:0px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                    <tr>
                         <td style="text-align:center; vertical-align:middle;">
                            <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                            <tr>
                                <td colspan="4" style='background-color:#CCE6FF; text-align:center; padding:4px; font-size:13px;'>OnBoard Crew List - Training Requirement</td>
                            </tr>
                            </table>
                         </td>
                    </tr>
                    <tr>
                         <td>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" bordercolor="#c2c2c2" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col style="width:50px;" />                                    
                                    <col style="width:150px;"/>
                                    <col />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" /> 
                                    <col style="width:100px;" /> 
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Sr#</td>                                    
                                    <td style="color:White;vertical-align:middle;">&nbsp;Crew#</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Name</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Rank</td>
                                    <td style="color:White;vertical-align:middle;text-align:center; ">&nbsp;Planned</td>
                                    <td style="color:White;vertical-align:middle;text-align:center;">&nbsp;Completed</td>   
                                    <td style="color:White;vertical-align:middle;text-align:center;">&nbsp;Remaining</td>   
                                    <td style="color:White;vertical-align:middle;">&nbsp;Status</td>
                                </tr>
                                </table>
                           </div>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" bordercolor="#c2c2c2" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                     <col style="width:50px;" />                                    
                                    <col style="width:150px;"/>
                                    <col />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" /> 
                                    <col style="width:100px;" /> 
                                    <col style="width:100px;" /> 
                                </colgroup>
                                <asp:Repeater ID="rptTraining" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                            <td style="text-align:center"><%#Eval("Sr")%>.</td>                                            
                                            <td align="left">&nbsp;<%#Eval("CrewNumber")%></td>
                                            <td align="left">&nbsp;<%#Eval("CrewName")%></td>
                                            <td align="left">&nbsp;<%#Eval("RankCode")%></td>
                                            <td align="center" style="text-align:center"><%#Eval("Planned")%> </td>
                                            <td align="center" style="text-align:center"><a target="_blank" href="CMSDoneTraining.aspx?CrewID=<%#Eval("CrewID")%>"><%#Eval("Completed")%> </a></td>
                                            <td align="center" style="text-align:center"><a target="_blank" href="CMSDueTraining.aspx?CrewID=<%#Eval("CrewID")%>"><%#Eval("Due")%> </a></td>
                                            <td style="text-align:center">
                                                <asp:Image runat="server" Visible='<%#(Common.CastAsInt32(Eval("pendingexport"))>0)%>' ImageUrl="~/Modules/PMS/Images/warning.gif" />
                                            </td>
                                           </tr>                                        
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>
                           </div>
                         </td>
                    </tr>
                </table>
        <div style="text-align:center;">
            <asp:Button ID="btnExport" runat="server" Text="Export to Office" style="padding:5px 15px; background-color:#0094ff;color:white;border:none;" OnClick="btnExport_OnClick"/> &nbsp;
            <asp:Label ID="lblMsg" runat="server" style="color:red;font-weight:bold;float:right;"></asp:Label>
            
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
<script type="text/javascript">
    $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
</script>
</html>
