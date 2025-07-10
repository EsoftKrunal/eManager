<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CrewAssessment.aspx.cs" Inherits="CrewApproval_CrewAssessment" Title="Crew Approval" %>
<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
   
    <style type="text/css">
       .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:18px;
           height:18px;
           border:solid 1px grey;
       }
       .Grade_B
       {
           background:yellow; 
           color:Black ;
           width:18px;
           height:18px;
           border:solid 1px grey;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:18px;
           height:18px;
           border:solid 1px grey;
       }
       .Grade_D
       {
           background:red; 
           width:18px;
           height:18px;
           color:white;
           border:solid 1px grey;
       }
    </style>
    <script type="text/javascript">
        function ShowRemarks(CBId, MUM) {
            document.getElementById('hfCBId').value = CBId;
            document.getElementById('hfMUM').value = MUM;            
            document.getElementById('btnShowRemarks').click();
        }
    </script>
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</head>
<body style="margin: 5 0 5 0; background-color:White;" >
<form id="form1" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
        <ProgressTemplate>
            <div style="background-color: Black; opacity: 0.4; filter: alpha(opacity=40); width: 100%;
                z-index: 50; min-height: 100%; position: absolute; top: 0px; left: 0px;">
            </div>
            <div style="position: absolute; top: 300px; left: 0px; width: 100%; z-index: 100;
                text-align: center; color: Blue;">
                <center>
                    <div style="border: solid 2px blue; height: 50px; width: 120px; background-color: White;">
                        <img src="../Images/loading.gif" alt="loading">
                        Loading ...
                    </div>
                </center>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
<div style="text-align: center">
<asp:UpdatePanel ID="up1" runat="server">
<ContentTemplate>
<table cellpadding="5" cellspacing="0" width="100%" border="0" >
<%--<tr>
    <td colspan="2" class="text headerband">Crew Assessment For Performance Bonus</td>
</tr>--%>
<tr>
<td style=" text-align :left;background-color:#d2d2d2;font-family:Arial;font-size:12px; ">
   <table>
        <tr>
            <td style="width:120px;">Select Vessel : </td>
            <td style="width:220px;"><asp:DropDownList runat="server" ID="ddl_VW_Vessel" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged"></asp:DropDownList></td>
            <td style="width:120px;">Select Rank : </td>
            <td style="width:120px;"><asp:DropDownList runat="server" ID="ddl_Rank" CssClass="required_box"  AutoPostBack="true" Width='100px' OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged"></asp:DropDownList></td>
            <td style="width:120px;">Select Status : </td>
            <td style="width:120px;">
            <asp:DropDownList runat="server" ID="ddl_Status" CssClass="required_box"  AutoPostBack="true" Width='100px' OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged">
                <asp:ListItem Text="<Select>" Value="" ></asp:ListItem>
                <asp:ListItem Text="Open" Value="O" Selected="True" ></asp:ListItem>
                <asp:ListItem Text="Closed" Value="C" ></asp:ListItem>
            </asp:DropDownList>
           </td>
            <td style="text-align:right;"><asp:Label ID="lblRecCount" Font-Bold="true" runat="server" ></asp:Label>
                <asp:Button ID="btnRefresh" Text="" OnClick="btnRefresh_Click" style="display:none;" runat="server" />
            </td>
        </tr>
   </table> 
</td>
<td style="text-align:right;background-color:#d2d2d2">
    <%--<asp:Button ID="btnAddCrew" runat="server" CssClass="btn" Text=" Add Crew " Width="100px" CausesValidation="False" onclick="btnAddCrew_Click" />--%>
</td>
</tr>
<tr>
<td style="text-align: center;" colspan="2">
<div style="overflow-y:scroll;overflow-x:hidden;height:30px; border:solid 1px gray;" >
<table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:13px;font-family:Arial;  height:30px; background-color:#FFE6B2'>
<colgroup>
    <col width='25px' />
    <col width='25px' />
    <col width='25px' />
    <col width='25px' />
    <%--<col width='25px' />--%>
    <col width='90px' />
    <col width='70px' />
    <col width='200px' />
    <col />
    <col width='60px' />
    <%--<col width='80px' />--%>
    <col width='90px' />
    <col width='90px' />
    <col width='100px' />
    <col width='80px' />
    <col width='80px' />
    <col width='80px' />
    <col width='100px' />
    <col width='20px' />
</colgroup>
<tr class= "headerstylegrid">
    <td><img src="../Images/mail.gif" alt="" /></td>
    <td><img src="../Images/exclamation.gif" alt="" /></td>
    <td><img src="../Images/print_16.png" alt="" /></td>
    <td><img src="../Images/green_circle.gif" alt="" /></td>
    <%--<td><img src="../Images/forward.png" alt="" /></td>--%>
    <td>Notify Dt</td>
    <td>Crew #</td>
    <td>Vessel </td>
    <td>Crew Name</td>
    <td>Rank</td>
    <%--<td>Contract #</td>--%>
    <td>SignOn Dt.</td>
    <td>Rel Due Dt.</td>
    <td>Owner Rep.</td>
    <td>Charterer</td>
    <td>Tech Supdt</td>
    <td>Fleet Mgr</td>
    <td>Marine Suptd</td>
    <td></td>
</tr>
</table>
</div>
<div style="overflow-y:scroll;overflow-x:hidden;height:395px; border:solid 1px gray;" >
<table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px; font-family:Arial;  height:30px;'>
<colgroup>
    <col width='25px' />
    <col width='25px' />
    <col width='25px' />
    <col width='25px' />
   <%-- <col width='25px' />--%>
    <col width='90px' />
    <col width='70px' />
    <col width='200px' />
    <col />
    <col width='60px' />
    <%--<col width='80px' />--%>
    <col width='90px' />
    <col width='90px' />
    <col width='100px' />
    <col width='80px' />
    <col width='80px' />
    <col width='80px' />
    <col width='100px' />
    <col width='20px' />
</colgroup>
<asp:Repeater runat="server" ID="rprData1">
<ItemTemplate>
<tr>
    <td style="text-align:center;"><asp:ImageButton ID="btnMail" ImageUrl="~/Modules/HRD/Images/mail.gif" runat="server" RankId='<%#Eval("RANKID")%>' ToolTip='Send Mail' OnClick="btnMail_Click" CssClass='<%#Eval("VESSELID")%>' CommandArgument='<%#Eval("CREWBONUSID")%>' Visible='<%#Eval("BonusApproved").ToString() == ""%>' /> </td>
    <td style="text-align:center;"><asp:ImageButton ID="btnClosure" CREWNO='<%#Eval("CREWNUMBER")%>' CREWNAME='<%#Eval("CREWNAME")%>' VESSEL='<%#Eval("VESSELNAME")%>' ImageUrl="~/Modules/HRD/Images/exclamation.gif" runat="server" ToolTip="Closure"  OnClick="btnClosure_Click" CommandArgument='<%#Eval("CREWBONUSID")%>' Visible='<%#Eval("BonusApproved").ToString() == ""%>' /> </td>
    <td style="text-align:center;"><a href='CrewAssessmentPrint.aspx?CCBId=<%#Eval("CREWBONUSID")%>' style='<%#Convert.IsDBNull(Eval("BonusApproved"))?"display:none;":""%>' target="_blank"  ><img src="../Images/print_16.png" title="Print" style="border:none;" alt="" /> </a></td>
    <td style="text-align:center;"><img src="../Images/green_circle.gif" title="Email sent" alt="" runat="server" Visible='<%#( (Eval("IsMailSent").ToString() == "Y") && ((Common.CastAsInt32(Eval("RANKID"))==1) || (Common.CastAsInt32(Eval("RANKID"))==12))  )%>' /> <img alt="" src="~/Modules/HRD/Images/red_circle.png" title="Email not sent" runat="server" visible='<%#Eval("IsMailSent").ToString() != "Y"%>'/> </td> 
    <%--<td style="text-align:center;">
        
         <asp:ImageButton ID="imgAddPeap" runat="server" ImageUrl='<%#"~/Modules/HRD/Images/" + ((Common.CastAsInt32(Eval("PEAPID"))>0)?"green_circle.gif":"red_circle.png")%>' OnClick="imgAddPeap_OnClick" ToolTip="Forward for Appraisal !" CommandArgument='<%#Eval("CREWBONUSID")%>'  Visible='<%#((Common.CastAsInt32(Eval("RANKID"))==1) || (Common.CastAsInt32(Eval("RANKID"))==12))%>'  />
    </td>--%>
    <td style="text-align:center"><%#Common.ToDateString(Eval("NotifyDt"))%></td>
    <td style="text-align:center"><%#Eval("CREWNUMBER")%></td>
    <td style="text-align:left"><%#Eval("VESSELNAME")%></td>
    <td style="text-align:left"><%#Eval("CREWNAME")%></td>
    <td style="text-align:left"><%#Eval("RANKCODE")%></td>
    <%--<td style="text-align:center"><%#Eval("ContractRefNumber")%></td>--%>
    <td style="text-align:center"><%#Common.ToDateString(Eval("SignOnDate"))%></td>
    <td style="text-align:center"><%#Common.ToDateString(Eval("SignOffDate"))%></td>
    <td style="text-align:center"><div class='Grade_<%#Eval("OwnerRep")%>'><a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 1);" href="#" title="View Remarks" style="text-decoration:none;"><%#Eval("OwnerRep")%></a></div><asp:ImageButton ID="imgOwnerRepEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgOwnerRepEdit_Click" runat="server" Visible='<%# (Eval("OwnerRep").ToString() == "" && Eval("OwnerId").ToString() == "7"  && (Session["loginid"].ToString() == "21" || Session["loginid"].ToString() == "22" ) && Eval("IsMailSent").ToString() == "Y") %>' /></td>
    <td style="text-align:center"><div class='Grade_<%#Eval("Charterer")%>'><a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 2);" href="#" title="View Remarks" style="text-decoration:none;"><%#Eval("Charterer")%></a></div></td>
    <td style="text-align:center"><div class='Grade_<%#Eval("TechSupdt")%>'><a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 3);" href="#" title="View Remarks" style="text-decoration:none;"><%#Eval("TechSupdt")%></a></div><asp:ImageButton ID="imgTechSupdtEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgTechSupdtEdit_Click" runat="server" Visible='<%# (Eval("TechSupdt").ToString() == ""  && Session["loginid"].ToString() == Eval("TechSupdtId").ToString() && Eval("IsMailSent").ToString() == "Y") %>' /> </td>
    <td style="text-align:center"><div class='Grade_<%#Eval("FleetMgr")%>'><a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 4);" href="#" title="View Remarks" style="text-decoration:none;"><%#Eval("FleetMgr")%></a></div><asp:ImageButton ID="imgFleetMgrEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgFleetMgrEdit_Click" runat="server" Visible='<%# (Eval("FleetMgr").ToString() == ""  && Session["loginid"].ToString() == Eval("FleetManagerId").ToString() && Eval("IsMailSent").ToString() == "Y") %>' /> </td>
    <td style="text-align:center"><div class='Grade_<%#Eval("MarineSupdt")%>'><a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 5);" href="#" title="View Remarks" style="text-decoration:none;"><%#Eval("MarineSupdt")%></a></div><asp:ImageButton ID="imgMarineSupdtEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgMarineSupdtEdit_Click" runat="server" Visible='<%# (Eval("MarineSupdt").ToString() == ""  && Session["loginid"].ToString() == Eval("MarineSupdtId").ToString() && Eval("IsMailSent").ToString() == "Y") %>' /></td>
    <td></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
</td>
</tr>
</table>
<asp:Button ID="btnShowRemarks" OnClick="btnShowRemarks_Click" runat="server" style="display:none;" />
<asp:HiddenField ID="hfCBId" runat="server" />
<asp:HiddenField ID="hfMUM" runat="server" />

</div>
<%-- DIV FOR Remarks View --%>

<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_ViewRemarks" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:255px;padding :5px; text-align :center;background : white; z-index:150;top:180px; border:solid 0px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><strong>View Remarks</strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:230px;">
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 180px; text-align: center; border-collapse:collapse; width:100%;">
                     <tr>                         
                          <td style="text-align: left;">
                             <b>Email :</b>&nbsp;<asp:Label ID="lblEmail" runat="server" ></asp:Label>   
                          </td>
                      </tr>
                     <tr>                         
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Height="130px" Width="98%" runat="server" ></asp:TextBox>   
                          </td>
                      </tr>
                   <tr>                         
                          <td style="text-align: left;">
                             <b>Comments On :</b>&nbsp;<asp:Label ID="lblCommentDate" runat="server" ></asp:Label>   
                          </td>
                      </tr>
                      </table>
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style=" text-align:center;">                              
                              <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Close_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>
                          </td>
                        </tr>
                      </table>
             </div>
             </center>
        </div>
    </center>
    </div>

<%-- DIV FOR CLOSURE --%>
<div style="position:absolute;top:0px;left:0px; height :570px; width:100%; " id="dv_Closure" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:1100px;  height:390px;padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 0px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><strong>Closure</strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:350px;">
             <asp:UpdatePanel runat="server" ID="fd">
             <ContentTemplate>
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%; font-family:Arial;">
                     <tr>
                          <td style="vertical-align:top;">
                               <table cellpadding="3" cellspacing="1" width="100%">

                                     <tr >
                                        <td style="text-align: left; width:15%;">Crew # :</td>
                                        <td style="text-align: left; width:25%; "> <asp:Label ID="lblCrewno" runat="server"></asp:Label>
                                            &nbsp;</td>
                                         <td style="text-align: left;width:15%;">Crew Name :</td>
                                         <td style="text-align: left; ">
                                            <asp:Label ID="lblCrewName" runat="server"></asp:Label>
                                            &nbsp;</td>
                                     </tr>
                                    
                                    
                                     <tr >
                                        <td style="text-align: left; width:15%;">Vessel :</td>
                                        <td style="text-align: left; " colspan="3">
                                            <asp:Label ID="lblVessel" runat="server"></asp:Label>
                                            &nbsp;</td>
                                         
                                     </tr>
                                    
                                     <tr >
                                        <td style="text-align: left; width:15%;">Sign On Dt. :</td>
                                        <td style="text-align: left; width:25%; ">
                                            <asp:Label runat="server" ID="lblSignOnDt"></asp:Label>
                                            &nbsp;</td>
                                         <td style="text-align: left;width:15%;">Expected disembarkation Dt. :</td>
                                         <td style="text-align: left;">
                                            <asp:TextBox ID="txtSignOffDate" runat="server" MaxLength="15" Width="80px" AutoPostBack="true" OnTextChanged="txtSignOffDate_OnTextChanged" ValidationGroup="att"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="c1" TargetControlID="txtSignOffDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSignOffDate" Display="Dynamic" ErrorMessage="*" ValidationGroup="att"></asp:RequiredFieldValidator>
                                         </td>
                                     </tr>
                                   <tr>
                                       <td style="text-align: left; width:15%;">
                                           Remarks : 
                                       </td>
                                       <td style="text-align: left;" colspan="3">
                                            <asp:TextBox ID="txtOfficeComments" TextMode="MultiLine" Height="200px" 
                                              Width="98%" runat="server" ></asp:TextBox>
                                       </td>
                                   </tr>
                                      
                                 <%--  <tr style=" font-weight:bold">
                                       <td style="text-align: left;">
                                           Bonus Approval Status:
                                       </td>
                                    </tr>
                                    <tr>
                                       <td style="text-align: left;">
                                           <asp:Label ID="lblBonusStatus" runat="server"></asp:Label>
                                           <asp:HiddenField ID="hfBonusStatus" runat="server" />
                                       </td>
                                   </tr>
                                  <tr style=" font-weight:bold">
                                      <td style="text-align: left;">
                                           Expected Bonus Amt (US$) :
                                       </td>
                                    </tr>
                                    <tr>
                                       <td style="text-align: left;">
                                           <asp:TextBox ID="txtBonusAmt" runat="server" MaxLength="12" Width="80px" ReadOnly="true"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBonusAmt"
                                               Display="Dynamic" ErrorMessage="*" ValidationGroup="att"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                               ControlToValidate="txtBonusAmt" Display="Dynamic" ErrorMessage="Till 2 decimal places only."
                                               ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" ValidationGroup="att"></asp:RegularExpressionValidator>
                                       </td>
                                   </tr>--%>
                               </table>
                          </td>
                          <td style="vertical-align:top;" runat="server" visible="false">
                                 <table cellpadding="3" cellspacing="1" width="100%">

                                     <tr class= "headerstylegrid">
                                        <td style="text-align: center;">Bonus Calculation</td>
                                     </tr>
                                     </table>
                          <table cellpadding="2" cellspacing="0" width="100%">
                                     <tr> 
                                      <td style="font-weight:bold; text-align: center; "></td>
                                 </tr>
                                 </table>
                              <div style="overflow-y:scroll;overflow-x:hidden;height:30px; border:solid 1px gray;" >
                                <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:13px;  height:30px; background-color:#FFE6B2'>
                                <colgroup>
                                    <col width='100px' />
                                    <col width='100px' />
                                    <col width='80px' />
                                    <col  />
                                    <col width='90px' />
                                    <col width='20px' />
                                </colgroup>
                                <tr class= "headerstylegrid">
                                    <td>Start Period</td>
                                    <td>End Period</td>
                                    <td>Vessel Age</td>
                                    <td>Bonus Amt.(Monthly)</td>
                                    <td>Payable Amt</td>
                                    <td></td>
                                </tr>
                                </table>
                                </div>
                              <div style="overflow-y:scroll;overflow-x:hidden;height:240px; border:solid 1px gray;" >
                                <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
                                <colgroup>
                                    <col width='100px' />
                                    <col width='100px' />
                                    <col width='80px' />
                                    <col  />
                                    <col width='90px' />
                                    <col width='20px' />
                                </colgroup>
                                <asp:Repeater runat="server" ID="rptCalcBonus">
                                <ItemTemplate>
                                <tr>
                                    <td style="text-align:center"><asp:Label ID="lblSD" Text='<%#Common.ToDateString(Eval("StartPeriod"))%>' runat="server"></asp:Label></td>
                                    <td style="text-align:center"><asp:Label ID="lblED" Text='<%#Common.ToDateString(Eval("EndPeriod"))%>' runat="server"></asp:Label></td>
                                    <td style="text-align:center"><asp:Label ID="lblVA" Text='<%#Eval("VesselAge")%>' runat="server"></asp:Label></td>
                                    <td style="text-align:right"><asp:Label ID="lblBA" Text='<%#Eval("BonusAmtMonthly")%>' runat="server"></asp:Label></td>
                                    <td style="text-align:right"><asp:Label ID="lblCB" Text='<%#Eval("CalcBonus")%>' runat="server"></asp:Label></td>
                                    <td></td>
                                </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                </table>
                                </div>
                          </td>
                          
                     </tr>
                      </table>
                      </ContentTemplate>
             </asp:UpdatePanel>
             
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style=" text-align:center;"> 
                              <asp:Button ID="btnApprove" runat="server" Text="Save" Width="80px" OnClick="btnApprove_Click" ValidationGroup="att" style="  border:none; padding:4px;" CssClass="btn" />                             
                              <asp:Button ID="btn_Closure_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Closure_Close_Click" CausesValidation="false" style=" border:none; padding:4px;" CssClass="btn"/>
                              <asp:HiddenField ID="hfCrewBonusId" runat="server" />
                          </td>
                        </tr>
                      </table>
             </div>
             </center>
        </div>
    </center>
    </div>

    <%--Add Peap--%>

<%--    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddPeapPopup" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position :relative; width:800px;text-align :center; border :solid 5px #333;padding-bottom:5px; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
        <center>
            <div style="background-color:#aaa;padding:5px;color:white; font-size:14px;"> Add New Peap </div>
            <table cellpadding="4" cellspacing="2" border="0" width="95%">
                <tr class="heaing1">
                    <td>Occasion :</td>                    
                    <td>
                         <asp:DropDownList ID="ddlOccasion_AP" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlOccasion_AP_OnSelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="101">ROUTINE</asp:ListItem>
                            <asp:ListItem Value="102">ON DEMAND</asp:ListItem>
                            <asp:ListItem Value="103">INTERIM</asp:ListItem>
                        </asp:DropDownList>
                    </td>         
                    <td>
                        
                    </td>   
                </tr>
            </table>    
            
            
            </center>      
        <div style="padding:5px; text-align:left; min-height:18px;">
            <asp:Button ID="btnClosePopup" runat="server" CssClass="btn" onclick="btnClosePopup_Click" Text="Close" CausesValidation="false" Width="100px" style="float:right"/>
            <asp:Label ID="lblMsg_AP" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
        </div>  
    </div>
        </center>
    </div>--%>

</ContentTemplate>
</asp:UpdatePanel>

</form>
</body>
</html>
