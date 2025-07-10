<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI.aspx.cs" Inherits="KPI" Title="KPI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>KPI</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <style>
            .red
            {
                color:Red;
            }
            .nocss
            {
            }
            
            .linkBtn
            {
                
            }
            .UnlinkBtn
            {
                text-decoration:none;
                cursor:default;
            }
    </style>
    <script type="text/javascript">
        function ShowMontDataVerification(KPIID,M) {
            if (KPIID == 12) {
                alert(KPIID);    
            }
            
        }
    </script>
    <script type="text/javascript">
        function ShowHideAccident(val) {
            if (val == 1) {
                document.getElementById('divAccidentTabular').style.display = 'block'
                document.getElementById('divAccidentGraph').style.display = 'none'
                document.getElementById('divAccidentDataForImport').style.display = 'none'
            }
            else if (val == 2) {
                document.getElementById('divAccidentGraph').style.display = 'block'
                document.getElementById('divAccidentTabular').style.display = 'none'
                document.getElementById('divAccidentDataForImport').style.display = 'none'
            }
            else {
                document.getElementById('divAccidentDataForImport').style.display = 'block'
                document.getElementById('divAccidentTabular').style.display = 'none'
                document.getElementById('divAccidentGraph').style.display = 'none'
            }
        }
        function ShowHideLtif(val) {
            if (val == 1) {
                document.getElementById('divLtifRepeater').style.display = 'block'
                document.getElementById('tblLtiFormula').style.display = 'block'
                document.getElementById('divLtifGraph').style.display = 'none'
            }
            else {
                document.getElementById('divLtifRepeater').style.display = 'none'
                document.getElementById('tblLtiFormula').style.display = 'none'
                document.getElementById('divLtifGraph').style.display = 'block'
                
            }
        }
        function ShowHideTrcf(val) {
            if (val == 1) {
                document.getElementById('divTabularDataTRCF').style.display = 'block'
                document.getElementById('tblLTIFFormula').style.display = 'block'
                document.getElementById('divGraphTRCF').style.display = 'none'
            }
            else {
                document.getElementById('divTabularDataTRCF').style.display = 'none'
                document.getElementById('tblLTIFFormula').style.display = 'none'
                document.getElementById('divGraphTRCF').style.display = 'block'

            }
        }
        function ShowHideNeaaMiss(val) {
            if (val == 1) {
                document.getElementById('divNearMissTabularValue').style.display = 'block'
                document.getElementById('divNearMissChart').style.display = 'none'
                document.getElementById('divNearMissData').style.display = 'none'
            }
            else {
                document.getElementById('divNearMissTabularValue').style.display = 'none'
                document.getElementById('divNearMissChart').style.display = 'block'
                document.getElementById('divNearMissData').style.display = 'none'
            }
            if (val == 3) {
                document.getElementById('divNearMissData').style.display = 'block'
                document.getElementById('divNearMissTabularValue').style.display = 'none'
                document.getElementById('divNearMissChart').style.display = 'none'
            }
        }

        
    </script>
</head>
<body>
<form id="form" runat="server">
    <asp:ScriptManager ID="SM1" runat="server" AsyncPostBackTimeout="300" ></asp:ScriptManager>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
                <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                    <img src="../Images/progress_Icon.gif" style="position:relative;" />
                </div>
            </center>
        </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" >
    <ContentTemplate>

        <table id="filter" width="100%" style ="text-align :center" cellpadding="3" cellspacing="1" style="border:solid 1px #4371a5;" >
        <tr style="background-color :#4371a5;font-weight:bold;">
            <td style="width:10%;color:White; text-align:center; ">Fleet</td>
            <td style="width:20%;color:White; text-align:center">Vessel</td>
            <td style="width:20%;color:White; text-align:center">View Report for</td>
            <td style="color:White; text-align:center">
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"  ID="ddlFleet" CssClass="input_box" Width="100%"></asp:DropDownList>  
                <asp:FileUpload id="FileUpload1" runat="server" CssClass="input_box" Width="352px" Visible="false"></asp:FileUpload>
            
            </td>
            <td>
                <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="100%"></asp:DropDownList> 
            </td>
            <td style="text-align:center">
            Year &nbsp;:
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="input_box"></asp:DropDownList>
            </td>
            <td>
                <%--<asp:LinkButton ID="lnkGenerateSUPTDForm" runat="server" Text="Generate" OnClick="lnkdownloadSUPTDForm_OnClick"></asp:LinkButton>--%>
            <%--<a id="aSUPTDForm" ></a>--%>
            
            <asp:Button runat="server" CssClass="input_box" ID="Button1" Text="Show"  Width="60px" onclick="btn_Show_Click" />
            <asp:LinkButton ID="aaa" runat="server" onclick="btn_Show_Click" Text=""></asp:LinkButton>
            <asp:Button runat="server" CssClass="input_box" ID="btnClear" Text="Clear" Width="60px" OnClick="btnClear_OnClick"/>   
            </td>
        </tr>
    </table>

        <TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
    <TBODY><TR><TD vAlign=top align=center>
    <TABLE style="WIDTH: 100%" cellSpacing=0 cellPadding=0><TBODY>
    <TR>

    <TD colspan="3">
           <TABLE style="WIDTH: 100%;" cellSpacing="0"cellPadding="0" ><TBODY>
            <TR>

            <TD  colspan="3" style="padding:2px;">
            <div style="overflow-x:hidden;overflow-y:hidden;width:1250px;" id="divHeader">
            <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table2" style="border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
               <col style="width:250px;" />
                <col style="width:60px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:70px;" />
            
            <tr class="headerstyle" id="tr1" >
                <th>KPI Heads</th>
                <th>KPI/Ship</th>
                <th style="text-align:center;">Jan</th>
                <th style="text-align:center;">Feb</th>
                <th style="text-align:center;">Mar</th>
                <th style="text-align:center;">Apr</th>
                <th style="text-align:center;">May</th>
                <th style="text-align:center;">Jun</th>
                <th style="text-align:center;">Jul</th>
                <th style="text-align:center;">Aug</th>
                <th style="text-align:center;">Sep</th>
                <th style="text-align:center;">Oct</th>
                <th style="text-align:center;">Nov</th>
                <th style="text-align:center;">Dec</th>
                <th style="text-align:center;">Recd.</th>
                <th style="text-align:center;">Target</th>
                <th style="text-align:center;">Achiv.</th>
            </tr>
            <tr class="headerstyle" id="tr2" >
                <th>No of Vessel</th>
                <th></th>
                <th style="text-align:center;">  <asp:Label id="lblM1" runat="server" ></asp:Label>   </th>
                <th style="text-align:center;">  <asp:Label id="lblM2" runat="server" ></asp:Label>    </th>
                <th style="text-align:center;">  <asp:Label id="lblM3" runat="server" ></asp:Label>    </th>
                <th style="text-align:center;">  <asp:Label id="lblM4" runat="server" ></asp:Label>    </th>
                <th style="text-align:center;">  <asp:Label id="lblM5" runat="server" ></asp:Label>    </th>
                <th style="text-align:center;">  <asp:Label id="lblM6" runat="server" ></asp:Label>   </th>
                <th style="text-align:center;">  <asp:Label id="lblM7" runat="server" ></asp:Label>    </th>
                <th style="text-align:center;">  <asp:Label id="lblM8" runat="server" ></asp:Label>    </th>
                <th style="text-align:center;">  <asp:Label id="lblM9" runat="server" ></asp:Label>   </th>
                <th style="text-align:center;">  <asp:Label id="lblM10" runat="server" ></asp:Label>  </th>
                <th style="text-align:center;">  <asp:Label id="lblM11" runat="server" ></asp:Label>  </th>
                <th style="text-align:center;">  <asp:Label id="lblM12" runat="server" ></asp:Label>  </th>
                <th style="text-align:center;"></th>
                <th style="text-align:center;">  <asp:Label id="lblAllVessCount" runat="server" Visible="false" ></asp:Label>  </th>
                <th style="text-align:center;"></th>
                
            </tr>
            </table>
            </div>
            <div style="height :350px; overflow-x:hidden;overflow-y:scroll;width:1250px;" onscroll='Data_OnScroll(this);'>
                <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table1" style="border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                <col style="width:250px;" />
                <col style="width:60px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:70px;" />
                <asp:Repeater ID="GrsKPIs" runat="server" >  <%--OnItemDataBound="GrsKPIs_OnItemDataBound" --%>
                <ItemTemplate>
                    <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                       <td>
                        <asp:LinkButton ID="lnkKPI" runat="server" CssClass='<%# (   (Eval("KPIID").ToString()=="10" || Eval("KPIID").ToString()=="12" )?"UnlinkBtn":"") %>' Text='<%#Eval("KPIName")%>' OnClick="lnkKPI_OnClick"></asp:LinkButton>
                           
                       </td>
                       <td> 
                         <asp:Label ID="lblKPIValues" runat="server" Text='<%#ConvertToInteger(Eval("KPIPerShip").ToString(), "999")%>'></asp:Label> 
                         <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("KPIID")%>' />
                       </td>
                        <td > 
                            <asp:LinkButton ID="lnk1"  runat="server"  CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>'  Text='<%# (Eval("Mon1").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon1").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="1" ></asp:LinkButton>
                        </td>
                        <td> <asp:LinkButton ID="lnk2"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon2").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon2").ToString(), Eval("KPIID").ToString())%>' OnClick="ShowMonthVerification" CommandArgument="2" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk3"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon3").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon3").ToString(), Eval("KPIID").ToString())%>' OnClick="ShowMonthVerification" CommandArgument="3" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk4"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon4").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon4").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="4" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk5"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon5").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon5").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="5" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk6"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon6").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon6").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="6" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk7"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon7").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon7").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="7" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk8"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon8").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon8").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="8" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk9"  runat="server"    CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon9").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon9").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="9" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk10"  runat="server"   CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon10").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon10").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="10" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk11"  runat="server"   CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon11").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon11").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="11" ></asp:LinkButton> </td>
                        <td> <asp:LinkButton ID="lnk12"  runat="server"   CssClass='<%# ((Eval("KPIID").ToString()=="12")?"":"UnlinkBtn") %>' Text='<%# (Eval("Mon12").ToString() == "0.00") ? "" : ConvertToInteger(Eval("Mon12").ToString(), Eval("KPIID").ToString())%> ' OnClick="ShowMonthVerification" CommandArgument="12" ></asp:LinkButton> </td>
                        <td> <%# ConvertToInteger(Eval("Recv").ToString(), Eval("KPIID").ToString())%> </td>
                        
                       <%--<td style="text-align:right;"> <asp:Label ID="lblValuesM1" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Jan")%>'></asp:Label> </td> 
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM2" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Feb")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM3" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Mar")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM4" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Apr")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM5" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "May")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM6" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Jun")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM7" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Jul")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM8" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Aug")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM9" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Sep")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM10" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Oct")%>'></asp:Label></td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM11" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Nov")%>'></asp:Label> </td>
                       <td style="text-align:right;"> <asp:Label ID="lblValuesM12" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Dec")%>'></asp:Label> </td>                       
                       <td style="text-align:right;"> <asp:Label ID="lblRecieved" runat="server" Text='<%# ShowKPIValues(Eval("ID").ToString(), "Recd")%>'></asp:Label> </td>--%> 
                       <td style="text-align:right;"> <asp:Label ID="lblTotal" runat="server" Text='<%#ConvertToInteger(Eval("Target").ToString(), Eval("KPIID").ToString())%>' ></asp:Label> </td>
                       <td style="text-align:right;" > <asp:Label ID="lblPercentValue" runat="server" Text='<%#ConvertToIntegerForAchiev(Eval("Achiv").ToString(), Eval("KPIID").ToString())%>' CssClass='<%# SetAchivCSS(Eval("KPIID").ToString(),Eval("Achiv").ToString()) %>'></asp:Label></td>  <%--CssClass='<%# ((Common.CastAsInt32(Eval("Achiv"))<0)?"red":"nocss") %>'--%>
                                             
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>  
            </div>
        
    </TD>
    </TR>   </TBODY></TABLE>
     </TD>
     </TR>
     </TBODY></TABLE>
    <asp:HiddenField id="HiddenField_LoginId" runat="server"></asp:HiddenField>
     </TD></TR></TBODY></TABLE>
     
        <!-- Near Miss Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvNearMissPopUp" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            <div style="width:100%; padding:10px;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <tr>
                                    <td> 
                                        <input type="button" value="Chart" onclick="ShowHideNeaaMiss(2)" class="btn" style="float:left;margin:2px;" />
                                        <input type="button" value="Data View" onclick="ShowHideNeaaMiss(1)" class="btn" style="float:left;margin:2px;" />
                                        <input type="button" value="Chart Data" onclick="ShowHideNeaaMiss(3)" class="btn" style="float:left;margin:2px;" />
                                    </td>
                                </tr>
                            </table>

                            <div id="divNearMissTabularValue" style="display:none;">
                            
                            <div style="width:98%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2; margin:auto;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="margin:auto;">
                            <col /> <col width="70px" />    <col width="70px" /> <col width="100px" /><col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Injury</td>
                                <td>Pollution</td>
                                <td>Prop. Damage</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:98%;height:300px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2; margin:auto;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="70px" />    <col width="70px" /> <col width="100px" />
                                <asp:Repeater ID="rptKPIperVessel" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%> </td>
                                            <td> <%#Eval("InjuryCategory")%> </td>
                                            <td> <%#Eval("PollutionCategory")%> </td>
                                            <td> <%#Eval("ProDamageCategory")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                            </div>
                            </div>

                            <div id="divNearMissChart">
                                <table cellpadding="2" cellspacing="0" border="0" width="100%" >
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkInjuryCategory" runat="server" Text="Injury Category" Checked="true" AutoPostBack="true" OnCheckedChanged="btnShowGraphPop_OnClick" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkPollutionCategory" runat="server" Text="Pollution Category" Checked="true"  AutoPostBack="true" OnCheckedChanged="btnShowGraphPop_OnClick" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkPreDamageCategory" runat="server" Text="Pre Damage Category" Checked="true"  AutoPostBack="true" OnCheckedChanged="btnShowGraphPop_OnClick" />
                                </td>
                                <td>
                                    <asp:Button ID="btnShowGraph" runat="server" OnClick="btnShowGraphPop_OnClick" Text="Show Report" CssClass="btn" style="display:none;" />
                                </td>
                            </tr>
                        </table>
                                <div style="width:98%; height:300px; overflow-x:scroll;overflow-y:scroll; margin:auto;">
                                    <asp:Image  ID="imgGraph" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                                    <br /><br />
                                </div>
                            </div>

                            <div id="divNearMissData" style="width:98%;display:none;">
                                <asp:GridView ID="grdNearMiss" runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Category"  />
                                        <asp:BoundField DataField="MON1" HeaderText="Jan" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON2" HeaderText="Feb" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON3" HeaderText="Mar" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON4" HeaderText="Apr" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON5" HeaderText="May" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON6" HeaderText="Jun" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON7" HeaderText="Jul" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON8" HeaderText="Aug" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON9" HeaderText="Sep" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON10" HeaderText="Oct" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON11" HeaderText="Nov" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON12" HeaderText="Dec" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                    </td>
                </tr>
            </table>
                <table cellpadding="2" cellspacing="0" border="0" width="95%" style="padding:auto;">
                <tr>
                    <td style="height:50px;">
                        <asp:Button ID="btnExportNearMiss" runat="server" OnClick="btnExportNearMiss_OnClick" Text=" Import To Excel " CssClass="btn" />
                        <asp:Button ID="ddlCloseKPIPerVesselPopUp" runat="server" OnClick="ddlCloseKPIPerVesselPopUp_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
            </div>
         </div>
    </center>
    </div> 
    
        <!-- Major Accident Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvMajorAccidentPopup" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                <tr>
                    <td>
                        <div style="width:90%; margin:auto;">
                        <input type="button" value="Chart" onclick="ShowHideAccident(2)" class="btn" />
                        <input type="button" value="Data View" onclick="ShowHideAccident(1)"  class="btn"/> 
                        <input type="button" value="Chart Data " onclick="ShowHideAccident(3)"  class="btn"/> &nbsp;&nbsp;&nbsp;                        

                        </div>
                    </td>
                </tr>
                <tr>
                    <td >
                        <div id="divAccidentTabular" style="display:none;">
                            <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Major Accident</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:315px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptMajorAccident" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%> </td>
                                            <td> <%#Eval("MajorAccCount")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                        </div>
                        
                        <div id="divAccidentGraph" >
                            <div style="float:right;margin-right:60px;">
                            Scale : &nbsp;&nbsp;
                            <asp:DropDownList ID="ddlScaleAccident" runat="server" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlScaleAccident_OnSelectedIndexChanged" Width="50px" >
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                <asp:ListItem Text="65" Value="65"></asp:ListItem>
                                <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                <asp:ListItem Text="85" Value="85"></asp:ListItem>
                                <%--<asp:ListItem Text="90" Value="90"></asp:ListItem>
                                <asp:ListItem Text="95" Value="95"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                            <div style="width:95%; height:315px; overflow-x:scroll;overflow-y:scroll; margin-top:5px; border:solid 1px #c2c2c2;">  
                                <asp:Image  ID="AccidentGraph" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                            </div>
                        </div>
                        
                        <div id="divAccidentDataForImport" style="display:none;">
                            <asp:GridView ID="grdAccidentDataForImport" runat="server" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Category"  />
                                        <asp:BoundField DataField="MON1" HeaderText="Jan" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON2" HeaderText="Feb" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON3" HeaderText="Mar" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON4" HeaderText="Apr" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON5" HeaderText="May" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON6" HeaderText="Jun" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON7" HeaderText="Jul" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON8" HeaderText="Aug" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON9" HeaderText="Sep" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON10" HeaderText="Oct" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON11" HeaderText="Nov" HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="MON12" HeaderText="Dec" HeaderStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>

            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnAccidentForImport" runat="server" OnClick="btnAccidentForImport_OnClick" Text=" Import To Excel " CssClass="btn" />
                        <asp:Button ID="btnCloseMajorAccPopup" runat="server" OnClick="btnCloseMajorAccPopup_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 

        <!-- Fatality Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvFatality" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Fatality</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:350px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptFatality" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%>    </td>
                                            <td> <%#Eval("FatalityCount")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                    </td>
                    <td>
                        <%--
                        <div style="width:600px; height:350px; overflow-x:scroll;overflow-y:scroll; margin-top:5px;">
                            <asp:Image  ID="Image1" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>--%>
                    </td>
                </tr>
            </table>

            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnCloseFatality" runat="server" OnClick="btnCloseFatality_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 

        <!-- FlawLess Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvFlawLess" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Flawless PSC</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:350px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptFlawLess" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%>    </td>
                                            <td> <%#Eval("FlawLessCount")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                    </td>
                    <td>
                        <%--
                        <div style="width:600px; height:350px; overflow-x:scroll;overflow-y:scroll; margin-top:5px;">
                            <asp:Image  ID="Image1" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>--%>
                    </td>
                </tr>
            </table>

            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnFlawLess" runat="server" OnClick="btnFlawLess_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 

        <!-- LTIF Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divLTIF" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:420px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:10px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="1" cellspacing="0" border="0" width="95%"  style="margin:auto;" >
                <col width="200px" />
                <col />
                <col width="200px" />
                <tr >
                    <td></td>
                    <td style="font-size:15px;font-weight:bold; text-align:center;">M.T.M. SHIP MANAGEMENT PTE. LTD.</td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <input type="button" value="Chart" onclick="ShowHideLtif(2)" class="btn" style="float:left;margin:2px;" />
                        <input type="button" value="Data View" onclick="ShowHideLtif(1)" class="btn" style="float:left;margin:2px;" />
                    </td>
                    <td style="font-size:12px;font-weight:bold;text-align:center">
                        LOST TIME INJURY FREQUENCY <% =ddlYear.SelectedValue %>
                    </td>
                    <td>
                        Scale : &nbsp;&nbsp;
                        <asp:DropDownList ID="ddlLtifScale" runat="server" Width="50px" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlLtifScale_OnSelectedIndexChanged">
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <div id="divLtifRepeater" style="display:none;">
            <table cellpadding="1" cellspacing="0" border="0" width="95%" rules="all" style="border:solid 1px #c2c2c2;margin:auto;" >
                                <col width="80px" /><col width="80px" /><col width="100px" />
                                <col width="80px" /><col width="120px" /><col width="120px" />
                                <col width="120px" /><col width="120px" /><col width="120px" />
                                <col width="80px" /><col width="17px" />
                                <tr class="headerstyle" style="font-weight:bold; text-align:center;">
                                    <td style="text-align:center;">Month</td>
                                    <td style="text-align:center;">Total Crew</td>
                                    <td style="text-align:center;">Total Days</td>
                                    <td style="text-align:center;">Total Hours</td>
                                    <td style="text-align:center;">Exposure Hours</td>
                                    <td style="text-align:center;">Total LTI By Month</td>
                                    <td style="text-align:center;">Accumulated LTI</td>
                                    <td style="text-align:center;">LTIF By Month*</td>
                                    <td style="text-align:center;">LTIF (Year to date)</td>
                                    <td style="text-align:center;">Target</td>
                                </tr>
                                <asp:Repeater ID="rptLTIF" runat="server">
                                    <ItemTemplate>
                                        <tr >
                                            <td style="font-weight:bold;"> <%# GetMonthNameByNumber( Common.CastAsInt32( Eval("Mon")))%>    </td>
                                            <td style="text-align:right;"> <%#Eval("TotalCrew")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("TD")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("TH")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("EH")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("TotLTIByMon")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("AccumulatedLTI")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("LTIFByMonth")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("LTIFYTD")%>    </td>
                                            <td style="text-align:right;"> <%#Eval("Target")%>    </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr  class="headerstyle">
                                    <td style="font-weight:bold;">Total</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align:right;">
                                        <asp:Label ID="lblLTRIFSumEH" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                    <td style="text-align:right;">
                                        <asp:Label ID="lblLTRIFSumLTIByMonth" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align:right;">
                                        <asp:Label ID="lblLTRIFYTD" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                    <td style="text-align:center;"> <b> < 1.5</b> </td>
                                </tr>
                            </table>            
            </div>

            <%--<asp:UpdatePanel ID="UP2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <div id="divLtifGraph" >
                        <div style="width:95%; height:320px; overflow-x:hidden;overflow-y:scroll; margin:auto; border:solid 1px #c2c2c2;">
                            <asp:Image  ID="LtifGraph" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>
                    </div>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>

            
            
                <table cellpadding="2" cellspacing="0" border="0" width="95%"  style="margin:auto;" >
                <tr>
                    <td>
                        <asp:Button ID="btnCloseLTIF" runat="server" OnClick="btnCloseLTIF_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                    <td>
                        <div id="tblLtiFormula">
                            <table cellspacing="0" cellpadding="0" border="0" style="margin-left:100px;display:none;">
                            <tr>
                                <td rowspan="2">LOST TIME INJURY FREQUENCY FORMULA:=</td>
                                <td style="border-bottom:solid 1px #4371a5; padding-left:10px;padding-right:10px;">TOTAL INJURIES * 1000000.00</td>
                            </tr>
                            <tr>
                                <td  style="padding-left:10px; padding-right:10px;">TOTAL EXPOSURE HOURS</td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
            </table>
            
         </div>
        </center>
        </div> 


        <!-- TRCF Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divTRCFPopup" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:430px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:130;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="1" cellspacing="0" border="0" width="95%" style="margin:auto;" >
                <col width="200" /> <col />  <col width="200" />
                <tr >
                    <td></td>
                    <td style="font-size:15px;font-weight:bold; text-align:center;">M.T.M. SHIP MANAGEMENT PTE. LTD.</td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <input type="button" value="Chart" onclick="ShowHideTrcf(2)" class="btn" style="float:left;margin:2px;" />                        
                        <input type="button" value="Data View" onclick="ShowHideTrcf(1)" class="btn" style="float:left;margin:2px;" />
                    </td>
                    <td style="font-size:12px;font-weight:bold;text-align:center">Total Recordable Cases Frequency <% =ddlYear.SelectedValue %></td>
                    <td>
                        Scale : &nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTrcfScale" runat="server" CssClass="input_box" Width="50px" AutoPostBack="true"  OnSelectedIndexChanged="ddlTrcfScale_OnSelectedIndexChanged">
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div id="divTabularDataTRCF" style="display:none;">
            <table cellpadding="1" cellspacing="0" border="0" width="95%" rules="all" style="border:solid 1px #c2c2c2; margin:auto;" >
                <col width="80px" /><col width="80px" /><col width="100px" />
                <col width="80px" /><col width="120px" /><col width="120px" />
                <col width="120px" /><col width="120px" /><col width="120px" />
                <col width="80px" /><col width="17px" />
                <tr class="headerstyle" style="font-weight:bold; text-align:center;">
                    <td style="text-align:center;">Month</td>
                    <td style="text-align:center;">Total Crew</td>
                    <td style="text-align:center;">Total Days</td>
                    <td style="text-align:center;">Total Hours</td>
                    <td style="text-align:center;">Exposure Hours</td>
                    <td style="text-align:center;">Total TRC By Month</td>
                    <td style="text-align:center;">Accumulated TRC</td>
                    <td style="text-align:center;">TRCF By Month*</td>
                    <td style="text-align:center;">TRCF (Year to date)</td>
                    <td style="text-align:center;">Target</td>
                </tr>
                <asp:Repeater ID="rptTRCF" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td style="font-weight:bold;"> <%# GetMonthNameByNumber( Common.CastAsInt32( Eval("Mon")))%>    </td>
                            <td style="text-align:right;"> <%#Eval("TotalCrew")%>    </td>
                            <td style="text-align:right;"> <%#Eval("TD")%>    </td>
                            <td style="text-align:right;"> <%#Eval("TH")%>    </td>
                            <td style="text-align:right;"> <%#Eval("EH")%>    </td>
                            <td style="text-align:right;"> <%#Eval("TotTRCByMon")%>    </td>
                            <td style="text-align:right;"> <%#Eval("AccumulatedTRC")%>    </td>
                            <td style="text-align:right;"> <%#Eval("TRCFByMonth")%>    </td>
                            <td style="text-align:right;"> <%#Eval("TRCFYTD")%>    </td>
                            <td style="text-align:right;"> <%#Eval("Target")%>    </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr  class="headerstyle">
                    <td style="font-weight:bold;">Total</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td style="text-align:right;">
                        <asp:Label ID="lblSumEH" runat="server" style="font-weight:bold;"></asp:Label>
                    </td>
                    <td style="text-align:right;">
                        <asp:Label ID="lblSumOfLTIByMonth" runat="server" style="font-weight:bold;"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td style="text-align:right;">
                        <asp:Label ID="lblFinalLTIDYtd" runat="server" style="font-weight:bold;"></asp:Label>
                    </td>
                    <td style="text-align:center;"> <b> < 1.5</b> </td>
                </tr>
            </table>
            </div>
            
            <div id="divGraphTRCF" style="width:95%; height:335px; overflow-x:hidden;overflow-y:scroll; margin-top:5px; border:solid 1px #c2c2c2;margin:auto;">
                <asp:Image  ID="TRCFGraph" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
            </div>
            
            <table cellpadding="2" cellspacing="0" border="0" width="95%"  style="margin:auto;">
                <tr>
                    <td>
                        <asp:Button ID="btnCloseTRCF" runat="server" OnClick="btnCloseTRCF_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                    <td>
                        <table id="tblLTIFFormula" cellspacing="0" cellpadding="0" border="0" style="margin-left:100px;display:none;">
                            <tr>
                                <td rowspan="2">LOST TIME INJURY FREQUENCY FORMULA:=</td>
                                <td style="border-bottom:solid 1px #4371a5; padding-left:10px;padding-right:10px;">TOTAL INJURIES * 1000000.00</td>
                            </tr>
                            <tr>
                                <td  style="padding-left:10px; padding-right:10px;">TOTAL EXPOSURE HOURS</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 
    
        <!-- Polution Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divPolution" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Polution</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:350px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptPolution" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%> </td>
                                            <td> <%#Eval("PolutionCount")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                    </td>
                    <td>
                        <%--
                        <div style="width:600px; height:350px; overflow-x:scroll;overflow-y:scroll; margin-top:5px;">
                            <asp:Image  ID="Image1" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>--%>
                    </td>
                </tr>
            </table>

            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnClosePolution" runat="server" OnClick="btnClosePolution_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 
    
        <!-- PSC Detention Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divPSCDetention" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>PSC Detention</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:350px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptPSCDetention" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%> </td>
                                            <td> <%#Eval("PscCounter")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                    </td>
                    <td>
                        <%--
                        <div style="width:600px; height:350px; overflow-x:scroll;overflow-y:scroll; margin-top:5px;">
                            <asp:Image  ID="Image1" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>--%>
                    </td>
                </tr>
            </table>

            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnClosePSCDetention" runat="server" OnClick="btnClosePSCDetention_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 

        <!-- Audit Inspection Popup -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divAuditInspection" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
           
            <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
            <col /> <col width="120px" />  <col width="120px" />  <col width="120px" />    <col width="17px" />
            <tr class="headerstyle" style="font-weight:bold;">
                <td></td>
                <td colspan="3" style="text-align:center;"> Inspection </td>
                <td></td>
            </tr>
            <tr class="headerstyle" style="font-weight:bold;">
                <td>Vessel</td>
                <td style="text-align:center;">Due</td>
                <td style="text-align:center;">Done</td>
                <td style="text-align:center;">Deviation</td>
                <td></td>
            </tr>
            </table>
            </div>
            <div style="width:100%;height:330px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <col /> <col width="120px" />  <col width="120px" />  <col width="120px" /> 
                <asp:Repeater ID="rptAuditInspection" runat="server">
                    <ItemTemplate>
                        <tr> 
                            <td > <%#Eval("VESSELNAME")%> </td>
                            <td style="text-align:right; padding-right:15px;"> <%#Eval("TOT_INSP_DUE")%> </td>
                            <td style="text-align:right; padding-right:15px;"> <%#Eval("TOT_INSP_DONE")%> </td>
                            <td style="text-align:right; padding-right:15px;"> <%#Eval("DEVIATIONCOUNT")%> </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div> 
           

            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnCloseAuditInspection" runat="server" OnClick="btnCloseAuditInspection_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 
    
        <!-- CREW SIGN OFF ON MEDICAL GROUND  -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divCrewSignOffMedical" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Crew Sign Off</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:350px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptSignOffMedical" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%> </td>
                                            <td> <%#Eval("CrewSignOffCounter")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                    </td>
                    <td>
                        <%--
                        <div style="width:600px; height:350px; overflow-x:scroll;overflow-y:scroll; margin-top:5px;">
                            <asp:Image  ID="Image1" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>--%>
                    </td>
                </tr>
            </table>
               
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnCloseCrewSignOff" runat="server" OnClick="btnCloseCrewSignOff_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 

        <!-- SENIOR OFFICER’S RETENTION RATE  -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divRetentionRate" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:400px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <col width="420px" />
                <col />
                <tr>
                    <td>
                        <div style="width:100%;overflow-x:hidden  ;overflow-y:hidden; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                            <col /> <col width="120px" />    <col width="17px" />
                            <tr class="headerstyle" style="font-weight:bold;">
                                <td>Vessel</td>
                                <td>Retention Rate</td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
                            <div style="width:100%;height:350px; overflow-x:hidden  ;overflow-y:scroll; border:solid 1px #c2c2c2;">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all">
                                <col /> <col width="120px" /> 
                                <asp:Repeater ID="rptRetentionRate" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td> <%#Eval("VesselName")%> </td>
                                            <td> <%#Eval("RRCounter")%> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div> 
                    </td>
                    <td>
                        <%--
                        <div style="width:600px; height:350px; overflow-x:scroll;overflow-y:scroll; margin-top:5px;">
                            <asp:Image  ID="Image1" runat="server" onclick="window.open(this.src,'','');"  ToolTip="Click to open in new window." style="cursor:pointer;" />
                        </div>--%>
                    </td>
                </tr>
            </table>
               
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnCloseRR" runat="server" OnClick="btnCloseRR_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 


        <!-- Retention Rate Verification Per Month  -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divRRVerification" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:400px; height:230px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            
            <div style="text-align:center;">
                <asp:Label ID="lblRRHeading" runat="server" style="font-weight:bold;"></asp:Label>
            </div>
            <br />
            <table cellpadding="1" cellspacing="0" border="1" width="100%" rules="all">
                <tr>
                    <td>S = NTBR+INACTIVE</td>
                    <td>
                        <asp:Label ID="lblS" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>UT = DECEASED S/OFF</td>
                    <td>
                        <asp:Label ID="lblUT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>BT = NTBR</td>
                    <td>
                        <asp:Label ID="lblBT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>AE = ACUMULATIVE ONBOARD</td>
                    <td>
                        <asp:Label ID="lblAE" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:auto;" >
                            <tr>
                                <td rowspan="2">Retention Rate = </td>
                                <td rowspan="2">100 - &nbsp;&nbsp;</td>
                                <td style="border-bottom:solid 1px #c2c2c2;">{S-(UT+BT)}</td>
                                <td rowspan="2"> &nbsp;&nbsp;* 100</td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">(AE )</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Retention Rate (%)
                    </td>
                    <td>
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
               
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Button ID="btnRRVerification" runat="server" OnClick="btnRRVerification_OnClick" Text=" Close " CssClass="btn" />
                    </td>
                </tr>
            </table>
         
         </div>
        </center>
        </div> 
    </ContentTemplate   >
    <Triggers>        
        <asp:PostBackTrigger ControlID="btnExportNearMiss" />
    </Triggers>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAccidentForImport" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:Literal ID="lit1" runat="server"></asp:Literal>
</form>
</body>
</html>
