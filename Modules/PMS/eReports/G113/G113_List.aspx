<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G113_List.aspx.cs" Inherits="eReports_G113_List" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../../eReports/JS/KPIScript.js" type="text/javascript"></script>

    <link href="../../CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="../../eReports/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../eReports/CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>

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
                                <td colspan="4" style='background-color:#CCE6FF; text-align:center; padding:4px; font-size:13px;'><asp:Label ID="lblReportName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                            <td style="vertical-align:middle; width:300px;;">
                            Period :  <asp:TextBox runat="server" ID="txtFd" MaxLength="11" Width="75px" CssClass="date_only"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtTD" MaxLength="11" Width="75px" CssClass="date_only"></asp:TextBox>
                            </td>
                            
                            <%--<td  style="vertical-align:middle;width:80px; text-align:right;">Category :</td>--%>
                            <td  style="vertical-align:middle;width:300px;">
                                <asp:CheckBoxList ID="cblCategory" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                            </td>
                            <td  style="vertical-align:middle;width:150px;">
                            Status : <asp:DropDownList ID="ddlStatus" runat="server">
                                       <asp:ListItem Text="All" Value="0" Selected="True" ></asp:ListItem>
                                       <asp:ListItem Text="Open" Value="N" ></asp:ListItem>
                                       <asp:ListItem Text="Closed" Value="Y" ></asp:ListItem>
                                     </asp:DropDownList>
                            </td>
                            <td  style="vertical-align:middle">
                                <asp:Button runat="server" ID="btnShow" CssClass="btn" Text="Show" onclick="btnShow_Click" />
                                <%--<a href="\eReports\G113\eReport_G113.aspx?Type=E" class="btn" target="_blank">Add New Report</a>--%>
                                <asp:Button ID="btnAddNewReport" runat="server" Text="Add" CssClass="btn" OnClick="btnAddNewReport_OnClick"  />
                            </td>
                            </tr>
                            </table>
                         </td>
                    </tr>
                    <tr>
                         <td>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:30px;" />
                                    <col style="width:65px;" />    
                                    <col />
                                    <col style="width:100px;" />                                                                        
                                    <col style="width:100px;" />       
                                    <col style="width:180px;" />                                                                        
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Sr#</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Edit</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Crew#</td>
                                    <td style="color:White;vertical-align:middle; text-align:left;">&nbsp;Name</td>
                                    <td style="color:White;vertical-align:middle;">Rank</td>
                                    <td style="color:White;vertical-align:middle;">Occasion</td>
                                    <td style="color:White;vertical-align:middle;">Appraisal Duration</td>
                                    <td style="color:White;vertical-align:middle;"></td>
                                </tr>
                                </table>
                           </div>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 385px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:30px;" />
                                    <col style="width:65px;" />  
                                    <col />
                                    <col style="width:100px;" />                                    
                                    <col style="width:100px;" />      
                                    <col style="width:180px;" />      
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptReports" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                                <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                                <td style="text-align:right"> 
                                                    <a style='display:<%#Eval("img_view")%>' target="_blank" href='ER_G113_Report.aspx?PeapID=<%#Eval("AssMgntID")%>'> <img src="../../Images/edit.png" /> </a>                                                     
                                                </td>
                                                <td align="left">&nbsp;<%#Eval("CrewNo")%></td>
                                                <td align="left">&nbsp;<%#Eval("Name")%></td>                                                
                                                <td align="center"><%#Eval("Rank")%></td>
                                                <td align="center"><%#Eval("Occasion")%></td>
                                                <td align="center"> 
                                                    <%# Common.ToDateString(Eval("AppraisalFromDate"))%> - <%#Common.ToDateString(Eval("AppraisalToDate"))%>

                                                </td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style="background-color:#FFF5E6">
                                                <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                                <td style="text-align:right"> <a style='display:<%#Eval("img_view")%>'  target="_blank" href='ER_G113_Report.aspx?PeapID=<%#Eval("AssMgntID")%>'> <img src="../../Images/edit.png" /> </a> </td>
                                                <td align="left">&nbsp;<%#Eval("CrewNo")%></td>
                                                <td align="left">&nbsp;<%#Eval("Name")%></td>                                                
                                                <td align="center"><%#Eval("Rank")%></td>
                                                <td align="center"><%#Eval("Occasion")%></td>
                                                <td>
                                                    <%# Common.ToDateString(Eval("AppraisalFromDate"))%> - <%#Common.ToDateString(Eval("AppraisalToDate"))%>
                                                </td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                           </div>
                         </td>
                    </tr>
                </table>
    </div>

        <%-------------------------------------------------------------------------------%>
        <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <%--------------------------------------------------------------%>

        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_CrewList" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:900px;  height:380px;padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#FFE6B2; font-size:14px; "><strong>Select Onboard Crew</strong></div>
            <div style="overflow-y:hidden;overflow-X:hidden;border:solid 1px #f2f2f2;" >
               <table border="0" cellpadding="5" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                   <col width="30px" />
                   <col width="80px" />
                   <col width="380px" />
                   <col width="200px" />
                   <col width="110px" />
                   <col  />
                <tr style="background-color:#f2e6e6;color:!important white;">                  
                    <td> <b></b></td>
                    <td> <b>Crew#</b></td>
                    <td> <b>Crew Name</b></td>
                    <td> <b>Rank</b></td>
                    <td style="text-align:left;"> <b>Sign On Dt.</b></td>
                    <td style="text-align:left;"> <b> Relief Due Dt. </b></td>
                </tr>
                </table>
             </div>
                <div style="height:300px;overflow-y:scroll;overflow-X:hidden;border:solid 1px #f2f2f2;" >
                    <asp:HiddenField ID="hfSelectedCrewNumber" runat="server" Value="" />
                    <table border="0"  cellpadding="2" cellspacing="0" rules="rows" style="text-align: center; border-collapse:collapse; width:100%;">
                        <col width="30px" />
                        <col width="80px" />
                       <col width="380px" />
                       <col width="200px" />
                       <col width="110px" />
                       <col  />
                        <asp:Repeater ID="rptCrewList" runat="server">
                    <ItemTemplate>
                    <tr>
                        <td>
                            <input type="radio" name="selectcrew" id="rdocrew" onclick='SetHidden("<%#Eval("CrewNumber") %>")'/>
                        </td>
                       <td> 
                           <%--<a href='G113/ER_G113_Report.aspx?CrewNumber=<%#Eval("CrewNumber") %>' target="_blank" style="text-decoration:none;"> <%#Eval("CrewNumber") %> </a>  --%>
                           <%#Eval("CrewNumber") %>
                       </td>
                       <td><%#Eval("CrewName") %></td>
                       <td><%#Eval("RankName") %></td>
                        <td style="text-align:left;"><%# Common.ToDateString( Eval("SignOnDate")) %></td>
                        <td style="text-align:left;"><%# Common.ToDateString(Eval("ReliefDuedate")) %></td>
                   </tr>    
                        </ItemTemplate>
                </asp:Repeater>
                    </table>
                </div>
                <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style=" text-align:center;padding-top:5px;">
                              <asp:Button ID="btnGoToAppraisal" runat="server" Text="Go for appraisal" Width="120px" OnClick="btnGoToAppraisal_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>
                              <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Close_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>                              
                          </td>
                        </tr>
                      </table>
             </center>
        </div>
    </center>
    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        <%------------------------------------------------------------------------------------------%>
        
    </form>
</body>
<script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>

    <script type="text/javascript">
        function SetHidden(CrewNumber) {
            $("#hfSelectedCrewNumber").val(CrewNumber);
        }
        $(document).ready(function () {
            $("#rdocrew").click(function () {
                //alert('a');
                //$("#hfSelectedCrewNumber").val($(this).attr("CrewNumber"))
            });
        });
    </script>

<script type="text/javascript">
    $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
</script>
</html>
