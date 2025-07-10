<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingManagement.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_TrainingManagement" %>
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
//        function OpenPlanningDetails(TPId) {
//            window.open('../StaffAdmin/PopUpTrainingDetails.aspx?TrainingPlanningId=' + TPId,'','');
//        }
//        function OpenRecomm(year, month, mode) {
//            window.open('../StaffAdmin/PopUpRequirement.aspx?Year=' + year + '&Month=' + month + '&Mode=' + mode, '', '');
//        }
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
//                window.open('../StaffAdmin/Emtm_PopUpRequirement.aspx?TrainingID=' + TID, '', '');
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

    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <%--<asp:UpdatePanel runat="server" ID="up1">
         <ContentTemplate>--%>
            <asp:Button runat="server" ID="btnHidden" OnClick="btnHidden_Click" style="display:none;" />
            <asp:Button runat="server" ID="btnRefresh" OnClick="btnRefresh_Click" style="display:none;" />
            <asp:TextBox runat="server" ID="txtHidden" style="display:none;"/>
            <table width="100%" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             <div>
                                <uc1:Hr_TrainingMainMenu ID="Emtm_Hr_TrainingMainMenu1" runat="server" />
                             </div> 
                            </td>
                        </tr>
                        </table> 
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <colgroup>
                                <col align="right" />
                                <col align="left" />
                                <col align="right" />
                                <col align="left" />
                                <col align="right" />
                                <col align="left" />
                                    <tr style=" height:25px; padding-left:20px; background-color:#e2e2e2;">
                                        <td>
                                        Training Type :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddltypeOfTraining" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        Training Name :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTrainingName" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Status :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                                            <asp:ListItem Text="< All >" Value=""> </asp:ListItem>
                                            <asp:ListItem Text="Due" Value="D" Selected="True"> </asp:ListItem>
                                            <asp:ListItem Text="Planned" Value="A"> </asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="E"> </asp:ListItem>
                                            <asp:ListItem Text="Cancelled" Value="C"> </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    </tr>
                                <tr style=" height:25px; padding-left:20px; background-color:#e2e2e2;">
                                    <td>
                                        Office :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOffice" runat="server" Width="160px" AutoPostBack="true"  onselectedindexchanged="ddlOffice_SelectedIndexChanged" ></asp:DropDownList>
                                        <%--onchange="SetFocus();"--%>
                                    </td>
                                    <td>
                                        Department :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Position :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPosition" runat="server" Width="200px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style=" height:25px; padding-left:20px; background-color:#e2e2e2;">
                                    
                                    <td>
                                        Emp Name :</td>
                                    <td>
                                        <asp:TextBox ID="txtEmpName" runat="server" Width="195px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Duration :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromDt" runat="server" Width="80px"></asp:TextBox>
                                        &nbsp;&nbsp;:&nbsp;&nbsp;
                                        <asp:TextBox ID="txtToDt" runat="server" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="txtFromDt" PopupPosition="BottomRight" 
                                            TargetControlID="txtFromDt">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="txtToDt" PopupPosition="BottomRight" 
                                            TargetControlID="txtToDt">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                       Over Due (Only):
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkOverDue" runat="server" />
                                    </td>
                                </tr>
                                <tr style=" height:25px; padding-left:20px; background-color:#e2e2e2;">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td >
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server"  CssClass="btn" Text=" Search " OnClick="btnSearch_OnClick"/>
                                        <asp:Button ID="btnClear" runat="server"  CssClass="btn" Text=" Clear " OnClick="btnClear_OnClick"/>
                                        <asp:Button ID="btnPlan" runat="server"  CssClass="btn" Text="   Plan   " OnClick="btnPlan_OnClick"/>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>                        
                        <table cellpadding="2" cellspacing="2" width="100%" border="0">
                                <tr>
                                    <td style="background-color:#FFFFCC; border: 1px solid black; height:5px;text-align:center;">Planned Training</td>
                                    <td style="background-color:#51B751; border: 1px solid black; height:5px;text-align:center;">Completed Training </td>
                                    <td style="background-color:#FF5F5F; border: 1px solid black; height:5px;text-align:center;">Cancelled Training </td>
                                    <td style="background-color:#FFA500; border: 1px solid black; height:5px;text-align:center;">Due Training </td>                                    
                                </tr>
                        </table> 
                        <%----------------------------%>
                        <div  style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center;">
                        <table cellpadding="1" cellspacing="0" border="1" rules="all" width="100%" style="border-collapse:collapse;border-bottom:none;">
                                <col width="25px" />
                                <col />
                                <col width="160px" />
                                <col width="70px" />
                                <col width="100" />
                                <col width="180px" />
                                <col width="180px" />
                                <col width="90px" />
                                <%--<col width="17px" />--%>
                                <tr class="greyheader" style="font-weight:bold;" >
                                    <td></td>
                                    <td >
                                        <asp:LinkButton ID="LinkButton5" runat="server" Text="Training Name" OnClick="Sorting" CommandArgument="TRAININGNAME"></asp:LinkButton>
                                    </td>
                                    <td> 
                                        <asp:LinkButton ID="LinkButton6" runat="server" Text="Emp Name" OnClick="Sorting" CommandArgument="EmpName"></asp:LinkButton>
                                    </td>
                                    <td> 
                                        <asp:LinkButton ID="LinkButton7" runat="server" Text="Position" OnClick="Sorting" CommandArgument="PositionCode"></asp:LinkButton>
                                    </td>
                                    <td> 
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Due Date " OnClick="Sorting" CommandArgument="DUEDATE"></asp:LinkButton>
                                     </td>
                                    <td> 
                                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Plan Dt. " OnClick="Sorting" CommandArgument="PLANSTART"></asp:LinkButton>
                                     </td>
                                    <td> 
                                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Complition Dt. " OnClick="Sorting" CommandArgument="CompStart"></asp:LinkButton>
                                    </td>
                                    <td> 
                                    <asp:LinkButton ID="LinkButton4" runat="server" Text="Status " OnClick="Sorting" CommandArgument="StatusName"></asp:LinkButton>
                                    </td>
                                    <%--<td>&nbsp;</td>--%>
                                </tr>
                         </table>
                        </div>
                        <%--class="scrollbox"--%>
                        <div  style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 338px; text-align:center;" id="dvscroll_TM" onscroll="SetScrollPos(this)">
                        <table cellpadding="1" cellspacing="0" border="1" rules="all" width="100%" style="border-collapse:collapse; font-size:8px;">
                            <colgroup>
                                <col width="25px" />
                                <col />
                                <col width="160px" />
                                <col width="70px" />
                                <col width="100" />
                                <col width="180px" />
                                <col width="180px" />
                                <col width="90px" />
                                <%--<col width="17px" />--%>
                                </colgroup>
                            <asp:Repeater ID="rptCrew" runat="server">
                                <ItemTemplate>
                                    <tr  class='<%# Eval("IsOverDue")%>'>
                                        <td>
                                            <asp:CheckBox ID="chkDue" runat="server" Enabled='<%# (Eval("StatusName").ToString()=="Due") %>' CssClass='<%#Eval("TrainingRecommID")%>'  />
                                            <asp:HiddenField ID="hfTrainingID" runat="server" Value='<%#Eval("TRAININGID")%>' />
                                        </td>
                                        <td style="text-align:left">
                                             <a class='<%#  ((Eval("StatusName").ToString()=="Due")?"":"PP") %>' onclick="OpenTraining('<%#Eval("TrainingPlanningID")%>','<%#Eval("TRAININGID")%>')"  > <%#Eval("TRAININGNAME")%> </a>
                                             <%#Common.CastAsInt32(Eval("ValidityPeriod")) > 0 ? "<span style='color:green;font-weight:bold;'>[R]</span>" : ""%>
                                        </td>
                                        <td align="left"> <%# Eval("EmpName")%>  </td>
                                        <td align="left"> <%# Eval("PositionCode")%>  </td>
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
                                        <%--<td>&nbsp;</td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            
                            </table>
                        </div>                        
                        </td>
                    </tr>
            </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>     --%>
    </div>
    </form>
</body>
</html>
