<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReliefPlanning_SignOn.aspx.cs" Inherits="ReliefPlanning_SignOn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     
        <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
      <link rel="stylesheet" type="text/css" href="../Styles/style.css" />
        <style type="text/css">
       .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_B
       {
           background:yellow; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_D
       {
           background:red; 
           width:15px;
           height:15px;
           color:white;
           border:solid 1px grey;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
      <div style="padding:6px;  font-size:14px;" class="text headerband"><strong>Search Reliever for 
      [ <asp:Label ID="lblPlanCrewName" runat="server"></asp:Label> ]
      </strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden;">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="1" style="width: 100%" >
            <tr>
                <td style="text-align: center;" valign="top">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <table cellpadding="2" cellspacing="0" width="98%" border="0" style="text-align: center"  >
                <tr>
                    <td style="width: 100px; text-align: right">
                        Emp. # :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="80px"></asp:TextBox></td>
                    <td style="width: 173px; text-align: right">
                        First Name :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_FirstName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 175px; text-align: right">
                        Last Name :</td>
                        
                    <td style="text-align: left; width: 200px;">
                        <asp:TextBox ID="txt_LastName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 133px; text-align: right">
                        Rank :</td>
                    <td style="width: 180px; text-align: left;">
                        <asp:DropDownList ID="ddl_Rank" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                    <td style="width: 250px; text-align: center;">
                        <asp:Button ID="btn_Search" CausesValidation="false" runat="server" OnClientClick="this.value='Loading...';"  style=" background-color:RED; color:White; border:none; padding:3px; " Text="Search" OnClick="btn_Search_Click" Width="70px" />
                        
                        &nbsp;
                        <a href="SearchPrint.aspx" target="_blank" style="  border:none; padding:4px;text-decoration:none;font-size:12px;background-color:RED; color:White;" class="btn" > Print </a>

                        </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 2px; text-align: right">
                        Owner Pool :</td>
                    <td style="width: 173px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_OwnerPool" runat="server" CssClass="input_box" Width="80px"></asp:DropDownList></td>
                    <td style="width: 173px; height: 2px; text-align: right">
                        Vessel Type:</td>
                    <td style="width: 173px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="input_box" Width="180px" >
                    </asp:DropDownList>
                        </td>
                    <td style="width: 175px; height: 2px; text-align: right">
                        Status :</td>
                    <td style="width: 200px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box">
                        </asp:DropDownList>
                       </td>
                    <td style="width: 133px; height: 2px; text-align: right">
                         </td>
                    <td style="width: 180px; height: 2px; text-align: left">
                    <asp:CheckBox ID="chk_Exclude" runat="server" Text="Exclude Planned" Checked="True" />
                        </td>
                    <td style="width: 180px; height: 2px; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 2px; text-align: right" >
                        Rec.Office :</td>
                    <td style="width: 173px; height: 2px; text-align: left" >
                        <asp:DropDownList ID="ddl_RecOff" runat="server" CssClass="input_box" Width="80px"></asp:DropDownList>
                    </td>
                    <td style="text-align: right" >
                        &nbsp;Avail. Date :</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddl_Month" runat="server" CssClass="input_box" Width="70px" >
                        <asp:ListItem Text="Select" Value="" ></asp:ListItem>  
                        <asp:ListItem Text="Jan" Value="1" ></asp:ListItem>  
                        <asp:ListItem Text="Feb" Value="2" ></asp:ListItem>  
                        <asp:ListItem Text="Mar" Value="3" ></asp:ListItem>  
                        <asp:ListItem Text="Apr" Value="4" ></asp:ListItem>  
                        <asp:ListItem Text="May" Value="5" ></asp:ListItem>  
                        <asp:ListItem Text="Jun" Value="6" ></asp:ListItem>  
                        <asp:ListItem Text="Jul" Value="7" ></asp:ListItem>  
                        <asp:ListItem Text="Aug" Value="8" ></asp:ListItem>  
                        <asp:ListItem Text="Sep" Value="9" ></asp:ListItem>  
                        <asp:ListItem Text="Oct" Value="10" ></asp:ListItem>  
                        <asp:ListItem Text="Nov" Value="11" ></asp:ListItem>  
                        <asp:ListItem Text="Dec" Value="12" ></asp:ListItem>  
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_Year" runat="server" CssClass="input_box" Width="70px"></asp:DropDownList>
                    </td>
                    <td style="text-align: right" class="style3">
                    Compatibility :
                        </td>
                    <td style="text-align: left" class="style4">
                        <asp:RadioButton id="r1" runat="server" Text="Owner" GroupName="OV"/>
                            <asp:RadioButton id="r2" runat="server" Checked="true" Text="Vessel" GroupName="OV"/>
                        </td>
                    <td style="text-align: right;width:250px;display:none;" class="style5">
                        +ve Grading By :
                        </td>
                    <td style="text-align: left;display:none;" class="style6" colspan="2">
                        <asp:CheckBox ID="CheckBoxOR" runat="server" Text="OR" />        
                        <asp:CheckBox ID="CheckBoxCH" runat="server" Text="CH" />        
                        <asp:CheckBox ID="CheckBoxTS" runat="server" Text="TS" />        
                        <asp:CheckBox ID="CheckBoxFM" runat="server" Text="FM" />        
                        <asp:CheckBox ID="CheckBoxMS" runat="server" Text="MS" />      
                    </td>
                </tr>
                </table>
                </td>
            </tr>
    <tr>
        <td style="text-align:left;" >
                        <div style="height:60px; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0" style=" height:60px">
                        <tr class= "headerstylegrid"> 
                            <td style="width:40px"></td>
                            <td style="width:50px"></td>
                            <td></td>
                            <td style="width:80px"></td>
                            <td style="width:80px"></td>
                            <td style="width:80px"></td>
                            <td style="width:80px"></td>
                            <td style="width:80px"></td>
                            <%--<td style="width:80px">Planned VSL</td>--%>
                            <%--<td style="width:80px">Exp. Join Date</td>--%>
                            <td style="width:80px"></td>
                            <%--<td style="width:200px; text-align:center;" colspan="5">Latest Grading for <asp:Label runat="server" ID="lblGradingFor"></asp:Label> </td>--%>
                            <td style="width:30px"></td>
                            <td style="width:20px"></td>
                        </tr>
                        <tr class= "headerstylegrid"> 
                            <td style="width:40px">Assign</td>
                            <td style="width:50px">Crew#</td>
                            <td>Crew Name</td>
                            <td style="width:80px">Rank</td>
                            <td style="width:80px">Nationality</td>
                            <td style="width:80px">Last VSL</td>
                            <td style="width:80px">Planned VSL</td>
                            <%--<td style="width:80px">Exp. Join Date</td>--%>
                            <td style="width:80px">Avl From.</td>
                            <td style="width:200px">Avl Remarks</td>
                            <%--<td style="width:40px; text-align:center;">OR</td>
                            <td style="width:40px; text-align:center;">CH</td>
                            <td style="width:40px; text-align:center;">TS</td>
                            <td style="width:40px; text-align:center;">FM</td>
                            <td style="width:40px; text-align:center;">MS</td>--%>
                            <td style="width:30px"></td>
                            <td style="width:20px"></td>
                        </tr>
                        </table>
                        </div>
                        <div style="height:322px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0">
                        <asp:Repeater runat="server" ID="rpt_SignOnCrewList">
                        <ItemTemplate>
                        <tr>
                            <td style="width:40px; text-align:center;">
                                <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/group.gif" Visible='<%#(Eval("PlanVessel").ToString().Trim()=="")%>' CommandArgument='<%#Eval("Crewid")%>' CssClass='<%#Eval("RankId")%>' OnClientClick="this.src='../Images/loading.gif';" ID="btnAssignCrew" OnClick="btnAssignCrew_Click"/></td>
                            <td style="width:50px"><%#Eval("EMPNO")%></td>
                            <td><%#Eval("Name")%></td>
                            <td style="width:80px"><%#Eval("Rank")%></td>
                            <td style="width:80px"><%#Eval("nationality")%></td>
                            <td style="width:80px"><%#Eval("LastVessel")%></td>
                            <td style="width:80px"><%#Eval("PlanVessel")%></td>
                            <%--<td style="width:80px"><%# Common.ToDateString(Eval("ExpDate"))%></td>--%>
                            <td style="width:80px"><%#Common.ToDateString(Eval("AVLDate"))%></td>
                            <td style="width:200px"><div style='height:15px;overflow:hidden;' title='<%#Eval("AvalRemark").ToString() + "\n" + Convert.ToString(Eval("AvlByName")) + " / " + Common.ToDateString(Eval("AvlOn")) %>'><%#Eval("AvalRemark")%></div></td>
                          <%--  <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("OwnerRep")%>'><%#Eval("OwnerRep")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("Charterer")%>'><%#Eval("Charterer")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("TechSupdt")%>'><%#Eval("TechSupdt")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("FleetMgr")%>'><%#Eval("FleetMgr")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("MarineSupdt")%>'><%#Eval("MarineSupdt")%></div></td>--%>
                            <td style="width:30px"><asp:ImageButton ImageUrl="~/Modules/HRD/Images/HourGlass1.gif" ToolTip="View All grading" ID="lnkMoreDetails" OnClick="lnkMoreDetails_Click" CommandArgument='<%#Eval("Crewid")%>' OnClientClick="this.src='../Images/loading.gif';" runat="server"></asp:ImageButton></td>
                            <td style="width:20px"></td>
                        </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                        <tr style="background-color:#E0F5FF">
                           <td style="width:40px; text-align:center;"><asp:ImageButton runat="server" Visible='<%#(Eval("PlanVessel").ToString().Trim()=="")%>' ImageUrl="~/Modules/HRD/Images/group.gif" CommandArgument='<%#Eval("Crewid")%>' CssClass='<%#Eval("RankId")%>' OnClientClick="this.src='../Images/loading.gif';" ID="btnAssignCrew" OnClick="btnAssignCrew_Click"/></td>
                            <td style="width:50px"><%#Eval("EMPNO")%></td>
                            <td><%#Eval("Name")%></td>
                            <td style="width:80px"><%#Eval("Rank")%></td>
                            <td style="width:80px"><%#Eval("nationality")%></td>
                            <td style="width:80px"><%#Eval("LastVessel")%></td>
                            <td style="width:80px"><%#Eval("PlanVessel")%></td>
                            <%--<td style="width:80px"><%# Common.ToDateString(Eval("ExpDate"))%></td>--%>
                            <td style="width:80px"><%#Common.ToDateString(Eval("AVLDate"))%></td>
                            <td style="width:200px"><div style='height:15px;overflow:hidden;' title='<%#Eval("AvalRemark").ToString() + "\n" + Convert.ToString(Eval("AvlByName")) + " / " + Common.ToDateString(Eval("AvlOn")) %>'><%#Eval("AvalRemark")%></div></td>
                           <%-- <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("OwnerRep")%>'><%#Eval("OwnerRep")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("Charterer")%>'><%#Eval("Charterer")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("TechSupdt")%>'><%#Eval("TechSupdt")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("FleetMgr")%>'><%#Eval("FleetMgr")%></div></td>
                            <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("MarineSupdt")%>'><%#Eval("MarineSupdt")%></div></td>--%>
                            <td style="width:30px"><asp:ImageButton ImageUrl="~/Modules/HRD/Images/HourGlass1.gif" ToolTip="View All grading" ID="lnkMoreDetails" OnClick="lnkMoreDetails_Click" CommandArgument='<%#Eval("Crewid")%>' OnClientClick="this.src='../Images/loading.gif';" runat="server"></asp:ImageButton></td>
                            <td style="width:20px"></td>
                        </tr>
                        </AlternatingItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
        </td>
    </tr>
 </table>
            
            <!--  MODEL POP UP SECTION BEGIN-->
<asp:LinkButton ID="Main" runat="server" Text=" Please Confirm" OnClick="Main_Click" /> 
<script type="text/javascript">
    function Ok(sender, e) {
        $find('md1').hide();
        WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("Main", "", true, "&", "", false, true)); //('SearchReliver1_Main', e); 
    }
</script>
<script type="text/javascript">
    function ShowRequirement(crewid, rankid) {
        var str_vsl = '<%=Convert.ToString(Session["S_VesselId"])%>';
        var vesselid = 0;
        if (str_vsl != '') {
            vesselid = parseFloat(str_vsl)
        }

        var str_rank = '<%=Convert.ToString(Session["S_RankId"])%>';
        var newrankid = 0;
        if (str_rank != '') {
            newrankid = parseFloat(str_rank)
        }
        window.open('Crew_Required_Docs.aspx?crewid=' + crewid + '&vesselid=' + vesselid + '&rankid=' + newrankid, '', '');
    }
</script>
<ajaxToolkit:ModalPopupExtender ID="md1" runat="server" TargetControlID="Main" PopupControlID="pnl_yesno" BackgroundCssClass="modalBackground"  OnOkScript="Ok()" OkControlID="yes" CancelControlID="no" />
<asp:Panel ID="pnl_yesno" runat="server" CssClass="modalSignUp" Width="600px" Height="275px"  style="vertical-align:top; display:none;">
<table style="width: 100%" cellspacing="0" cellpadding="0">
    <tr>
        <td >
            <div style="padding:6px; background-color:#008AE6; font-size:14px; color:#fff; text-align:center;"><strong>Planning Remarks</strong></div></td>
    </tr>
    <tr><td style="text-align: center; height:20px;">
            <asp:Label ID="lbl_MessText" ForeColor="Red" runat="server" Font-Size="12px" Text="Label"></asp:Label>
            </td>
    </tr>
    <tr>
        <td style="text-align: center">
            <asp:TextBox ID="txt_PRemarks" runat="server" CssClass="input_box" Width="550px" Height="180px" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
<tr>
    <td style="text-align: center; padding-top : 5px;" >
        <asp:Button ID="yes" runat="server" Text="Yes" style=" background-color:#009900; color:White; border:none; padding:3px; " Width="70px"/>
        <asp:Button ID="no" runat="server" Text="No" style=" background-color:RED; color:White; border:none; padding:3px; " Width="70px"/> 
    </td>
</tr>
</table>
</asp:Panel> 
<!--  MODEL POP UP SECTION END -->

<!--  More Details Region -->

<div>
<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_Details" runat="server" visible="false">
    <center>
        <div style="position: fixed; top: 0px; left: 0px; min-height: 100%; width: 100%;
            background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)">
        </div>
        <div style="position: relative; width: 900px; padding: 5px; text-align: center; background: white; z-index: 150; top: 20px; border: solid 0px black;">
            <center>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <div style="padding: 6px;  font-size: 14px;
                                text-align: center;" class="text headerband">
                                <strong>Grading</strong></div>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <div style="overflow-y: scroll; overflow-x: hidden; height: 20px; border: solid 1px gray;">
                                <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse: collapse;
                                    font-weight: bold; font-size: 11px; height: 20px; background-color: #e5e5e5;
                                    color: #0e64a0'>
                                    <colgroup>
                                        <col />
                                        <col width='60px' />
                                        <col width='90px' />
                                        <col width='90px' />
                                        <col width='80px' />
                                        <col width='80px' />
                                        <col width='80px' />
                                        <col width='80px' />
                                        <col width='100px' />
                                        <col width='80px' />
                                        <col width='20px' />
                                    </colgroup>
                                    <tr>
                                        <td>
                                            Vessel
                                        </td>
                                        <td>
                                            Rank
                                        </td>
                                        <td>
                                            SignOn Dt
                                        </td>
                                        <td>
                                            SignOff Dt
                                        </td>
                                        <td>
                                            Owner Rep.
                                        </td>
                                        <td>
                                            Charterer
                                        </td>
                                        <td>
                                            Tech Supdt
                                        </td>
                                        <td>
                                            Fleet Mgr
                                        </td>
                                        <td>
                                            Marine Suptd
                                        </td>
                                        <td>
                                            Bonus Amt
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="overflow-y: scroll; overflow-x: hidden; height: 300px; border: solid 1px gray;">
                                <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse: collapse;
                                    font-size: 11px; height: 30px;'>
                                    <colgroup>
                                        <col />
                                        <col width='60px' />
                                        <col width='90px' />
                                        <col width='90px' />
                                        <col width='80px' />
                                        <col width='80px' />
                                        <col width='80px' />
                                        <col width='80px' />
                                        <col width='100px' />
                                        <col width='80px' />
                                        <col width='20px' />
                                    </colgroup>
                                    <asp:Repeater runat="server" ID="rprCrewAssessments">
                                        <ItemTemplate>
                                            <tr>                                                
                                                <td style="text-align: left">
                                                    <%#Eval("VESSELNAME")%>
                                                </td>
                                                <td style="text-align: left">
                                                    <%#Eval("RANKCODE")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%# Common.ToDateString(Eval("SignOndt"))%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%# Common.ToDateString(Eval("SignOffDt"))%>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("OwnerRep")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("OwnerRep")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("Charterer")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("Charterer")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("TechSupdt")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("TechSupdt")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("FleetMgr")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("FleetMgr")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("MarineSupdt")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("MarineSupdt")%></a></div>
                                                </td>
                                                <td style="text-align: right">
                                                    <%# String.Format("{0:C}", Eval("BonusAmount"))%>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #E0F5FF">                                                
                                                <td style="text-align: left">
                                                    <%#Eval("VESSELNAME")%>
                                                </td>
                                                <td style="text-align: left">
                                                    <%#Eval("RANKCODE")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%# Common.ToDateString(Eval("SignOndt"))%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%# Common.ToDateString(Eval("SignOffDt"))%>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("OwnerRep")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("OwnerRep")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("Charterer")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("Charterer")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("TechSupdt")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("TechSupdt")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("FleetMgr")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("FleetMgr")%></a></div>
                                                </td>
                                                <td style="text-align: center">
                                                    <div class='Grade_<%#Eval("MarineSupdt")%>'>
                                                        <a href="#" style="text-decoration: none;">
                                                            <%#Eval("MarineSupdt")%></a></div>
                                                </td>
                                                <td style="text-align: right">
                                                    <%# String.Format("{0:C}", Eval("BonusAmount"))%>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div style="text-align:center;">
                         <asp:Button ID="btnClose_MD" runat="server" OnClick="btnClose_MD_Click" Text="Close" style=" background-color:RED; color:White; border:none; padding:3px; " Width="70px"/>
                         </div>
            </center>
        </div>
    </center>
</div>
</div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
    </div>

    

    

    </form>
</body>
</html>
