<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingManagementMatrix.aspx.cs" Inherits="Emtm_Hr_TrainingManagementMatrix" %>
<%@ Register Src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMainMenu.ascx" TagName="Hr_TrainingMainMenu" TagPrefix="uc1" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMenu.ascx" tagname="Hr_TrainingMenu" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/Common.js"></script>
    <script type="text/javascript" language="javascript">
        function ViewPlaning(pid) {
            document.getElementById("txtHidden").setAttribute("value", pid);
            document.getElementById("btnHidden").click();
            return false;
        }
        function Refresh() {
            var btn = document.getElementById("btnRefresh");
            btn.click();
        }
        function print() {
            var yearobj = document.getElementById('ddlYear');
            var year = yearobj.options[yearobj.selectedIndex].value;

            window.open('../../Reporting/TrainingReport.aspx?Year=' + year + '&EmpId=0', '', '');
        }

        function OpenTraining(TPID, TID) {
            //if (TPID == '')
//                window.open('../StaffAdmin/PopUpRequirement.aspx?TrainingID=' + TID, '', '');
            //else
            if (TPID != '')
                window.open('../StaffAdmin/PopUpTrainingDetails.aspx?TrainingPlanningId=' + TPID, '', '');
        }
        function OpenRecommTraining() {
            window.open('../StaffAdmin/PopUpRequirement.aspx', '', '');
        }
    </script>
    <style type="text/css">
            .Planned
            {
                background-color:#FFFFCC
            }
            .Completed
            {
                background-color:#51B751
            }
            .Cancelled
            {
                background-color:#FF5F5F
            }
            .Due
            {
                background-color:#FFA500
            }
            .PP
            {
                cursor:pointer;
                color:#0059ff;
                text-decoration:none;
            }
            .PP:hover
            {
                cursor:pointer;
                color:#0059ff;
                text-decoration:underline;
            }
            .OverDue
            {
                background-color:#F5BCA9;
            }
    </style>
</head>
<body >

    <form id="form1" runat="server" >
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <%--<asp:UpdatePanel runat="server" ID="up1">
         <ContentTemplate>--%>
         <asp:TextBox runat="server" ID="txtHidden" style="display:none;"/>
        
            
                <div style="font-size:17px; font-weight:bold; padding:5px; text-align:center; height:40px; " class="greyheader"> 
                        Training Matrix for 
                        <asp:Label runat="server" id="lblPos" Font-Size="17px" Font-Bold="true"></asp:Label>
                <br /> Year : <asp:Label runat="server" ID="lblYear" Font-Size="17px" Font-Bold="true"></asp:Label>
                </div>
                <div style="float:right;font-size:17px; font-weight:bold; padding:5px; ">Category : <asp:Label runat="server" id="lblTG" Font-Size="17px" Font-Bold="true"></asp:Label> </div>
                
              <%--  <table cellpadding="2" cellspacing="2" width="100%" border="0">
                        <tr>
                            <td style="background-color:#FFFFCC; border: 1px solid black; height:5px;text-align:center;">Planned Training</td>
                            <td style="background-color:#51B751; border: 1px solid black; height:5px;text-align:center;">Completed Training </td>
                            <td style="background-color:#FF5F5F; border: 1px solid black; height:5px;text-align:center;">Cancelled Training </td>
                            <td style="background-color:#FFA500; border: 1px solid black; height:5px;text-align:center;">Due Training </td>                                    
                        </tr>
                </table> --%>
                <%----------------------------%>
                <table cellpadding="1" cellspacing="0" border="0" rules="all" width="100%" style="border-collapse:collapse; font-size:8px;border:solid 1px #c2c2c2;">
                    <asp:Repeater ID="rptEmployee" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>  
                                    <div style=" background-color:#EDEDFF; font-family:Arial; padding:3px;">
                                        <span style="font-size:16px; font-weight:bold; "> <%#Eval("EmpName") %> </span> [ <i style="color:Blue;"><%#Eval("PositionName")%> </i> ]<br /> 
                                    </div>
                                    <div style="font-family:Arial; padding:10px;">
                                    <table cellpadding="3" cellspacing="0" border="1" rules="all" width="100%" style="border-collapse:collapse; font-size:8px;border:solid 1px #c2c2c2;">
                                            <col />
                                            <col width="160px" />
                                            <col width="200px" />
                                            <col width="180px" />
                                            <col width="90px" />
                                            <col width="90px" />
                                            <col width="20px" />
                                            <col width="90px" />
                                                 <tr class="" style="font-weight:bold; padding-top:3px;padding-bottom:3px; text-align:center;" >
                                                    <td style="text-align: left">
                                                        <asp:LinkButton ID="LinkButton5" runat="server" Text="Training Name" CommandArgument="TRAININGNAME" style="cursor:default; text-decoration:none;"></asp:LinkButton>
                                                        <%--OnClick="Sorting"  Apply it to  all below buttons for sorting --%>
                                                    </td>
                                                    <td> 
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Due Date " CommandArgument="DUEDATE" style="cursor:default; text-decoration:none;"></asp:LinkButton>
                                                        </td>
                                                    <td> 
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Plan Dt. "  CommandArgument="PLANSTART" style="cursor:default; text-decoration:none;"></asp:LinkButton>
                                                        </td>
                                                    <td> 
                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Done Dt. "  CommandArgument="CompStart" style="cursor:default; text-decoration:none;"></asp:LinkButton>
                                                    </td>
                                                    <td> 
                                                    <asp:LinkButton ID="LinkButton4" runat="server" Text="Status "  CommandArgument="StatusName" style="cursor:default; text-decoration:none;"></asp:LinkButton>
                                                    </td>
                                                    <td>Exp.Dt.</td>
                                                    <td>&nbsp;</td>
                                                    <td>Source</td>
                                                </tr>
                                                 <asp:Repeater ID="rptCrew" runat="server" DataSource='<%# BindTraining(Eval("EmpID").ToString()) %>'>
                                                    <ItemTemplate>
                                                        <tr  class='<%# Eval("IsOverDue")%>'>
                                                            <td style="text-align:left">
                                                                    <a class='<%#  ((Eval("StatusName").ToString()=="Due")?"":"PP") %>' onclick="OpenTraining('<%#Eval("TrainingPlanningID")%>','<%#Eval("TRAININGID")%>')"  > <%#Eval("TRAININGNAME")%> </a>
                                                                    <%#Common.CastAsInt32(Eval("ValidityPeriod")) > 0 ? "<span style='color:green;font-weight:bold;'>[R]</span>" : ""%>
                                                            </td>                                                            
                                                            <td align="center"> <%# Common.ToDateString( Eval("DUEDATE"))%>  </td>
                                                            <td align="center">
                                                                <%# Common.ToDateString(Eval("PLANSTART"))%>  <%#  ((Eval("PLANEND").ToString() == "") ? "" : "<b> To </b>" + Common.ToDateString(Eval("PLANEND")))%>                                            
                                                            </td>
                                                            <td align="center">
                                                                <%# Common.ToDateString(Eval("CompStart"))%>  <%#  ((Eval("CompEnd").ToString() == "") ? "" : "<b> To </b>" + Common.ToDateString(Eval("CompEnd")))%>                                            
                                                            </td>
                                                            <td class='<%#Eval("StatusName")%>' align="center">
                                                                <%#Eval("StatusName")%>
                                                            </td>
                                                            <td align="center"><%#Common.ToDateString(Eval("EXPIRYDATE"))%></td>

                                                            <td align="center">
                                                            <a href='<%# "http://192.168.1.18/home/EMANAGERBLOB/HRD/EmpTrainingRecord/" + Eval("FILENAME").ToString() %>' target="_blank" >
                                                                <img runat="server" src="../../Images/paperclip.gif" visible='<%# ( Eval("FILENAME").ToString().Trim()!="")%>' style="border:none"/>
                                                            </a>
                                                            </td>
                                                            <td align="center"><%#Eval("SOURCE")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </table>
                                    </div>
                                
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>     --%>
    
    </form>
</body>
</html>
