<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_SCM.aspx.cs" Inherits="Edit_SCM" Title="Edit SCM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Safety Committee Meeting</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ajaxtabs2.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript">
        function CloseThisWindow()
        {
            this.close();
        }
        function RefereshParentPage()
        {
            window.opener.Reload();
        }
    </script>
    <style type="text/css">
        .selectedrow
        {
            background-color : lightgray;
            color :White; 
            cursor:pointer;
        }
        .row
        {
            background-color : White;
            color :Black;
            cursor:pointer; 
        }
        .style1
        {
            height: 24px;
        }
        input
        {
        	border:solid 1px gray;
        }
        textarea
        {
        	border:solid 1px gray;
        }
        input:focus
        {
        	background-color:#fafad2;
        	
        }
        textarea:focus
        {
        	 background-color:#fafad2;
        	
        }
        td
        {
        	font-size:13px;
        }
        th
        {
        	font-size:12px;
        	background-color:#FFCC99;
        }
        .hd1
        {
        	text-align:left; 
        	padding:8px; 
        	background-color:#FFCC99; 
        	text-align:center; 
        	font-weight:bold;
        	border:solid 1px gray;
        }
        .vcolor
        {
        	color:#333333;
        	font-weight:bold;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1"  runat="server" >
        <ContentTemplate>
            <div>
            <center>
              <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center; background-color: #f9f9f9">
            <tr>
                <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" class="text">
                    Safety Committee Meeting - <asp:Label runat="server" ID="lblNANo"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="1" cellspacing="1" border="0" width="100%">
                        <col width="150px"  />
                        <col width="200px" />
                        <col width="120px" />
                        <col width="100px" />
                        <col width="120px" />
                        <col width="200px" />
                        <col width="200px" />
                        <col />
                        <tr >
                            <td style="text-align:right;">Vessel :</td>
                            <td style="text-align:left;"><asp:Label ID="lblVessel" runat="server" class="vcolor"></asp:Label>
                            </td>
                            <td style="text-align:right;"> Occasion :</td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblOccasion" runat="server"  class="vcolor"></asp:Label>
                            </td>
                            <td style="text-align:right;"> Date :</td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblDate" runat="server"  class="vcolor"></asp:Label>
                            </td>
                            <td style="text-align:right;"> Time Commenced : </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTimeCommenced" runat="server"  class="vcolor"></asp:Label>
                                <span style="font-size:8px; font-style:italic;color:Black;">(Local Time)</span>
                            </td>
                            <td rowspan="2">
                                <table cellpadding="2" cellspacing="2" >
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn" style="font-weight:bold; font-size:13px; padding:4px;width:70px;" OnClick="btnPrint_OnClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;"> Vessel Name :</td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblVesselName" runat="server"  class="vcolor"></asp:Label>
                            </td>
                            <td style="text-align:right;"> Ship's Position :</td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblShipPosition" runat="server" class="vcolor"></asp:Label>
                            </td>
                            <td style="text-align:right;"> 
                                 <asp:Label ID="lblPlaceLabel" runat="server" ></asp:Label> :
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblShipFromTo" runat="server"  class="vcolor"></asp:Label> 
                                
                            </td>
                            <td style="text-align:right;"> Time Completed :</td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTimeCompleted" runat="server"  class="vcolor"></asp:Label>
                                <span style="font-size:8px; font-style:italic; color:Black;">(Local Time)</span>
                            </td>
                        </tr>
                    </table>
                    
                    
                    
                    <hr />
                </td>
            </tr>
            <tr>
            <td>
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__myTab" Height="500px" Width="100%">
                
                    <%--Home--%>
                    <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="Home">
                    <ContentTemplate>
                        <div style="float:left; width:610px;">
                            <table cellpadding="4" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="width:800px;border-right:solid 1px gray;border-left:solid 1px gray;">
                                            <table cellspacing="0" rules="cols" border="0" id="ctl00_ContentPlaceHolder1_grd_Data" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                                            <col width="150px" />
                                            <col width="250px" />
                                            <col />
                                            <col width="17px" />
                                            <tr class="headerstyle"  >
                                                <td colspan="4" style="text-align:center;">
                                                    <b> Safety Committee Attendance List</b>
                                                </td>
                                            </tr>
                                            <tr class="headerstyle" id="tr1" >
                                                <th>Rank Name</th>
                                                <th>Crew Name</th>
                                                <th>Remark</th>
                                                <th></th>
                                            </tr>
                                            </table>
                                            </div>
                                        <div style="height :450px; overflow-x:hidden;overflow-y:scroll;width:800px;border:solid 1px gray;border-top:none;">
                                                <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table1" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                                                <col width="150px" />
                                                <col width="250px" />
                                                <col />
                                                <col width="17px" />
                                                <asp:Repeater ID="rptAttendeeRank" runat="server" >
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("RankName")%></td>
                                                        <td><%#Eval("Name")%></td>
                                                        <td><%#Eval("Remarks")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                            </table>  
                                                </div>
                                    </td>
                                    <td>
                                        <div style="width:550px;border-right:solid 1px gray;border-left:solid 1px gray;">
                                            <table cellspacing="0" rules="cols" border="0" id="Table11" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                                            <col width="200px" />
                                            <col  />
                                            <col width="17px" />
                                            <tr class="headerstyle"  >
                                                <td colspan="4" style="text-align:center;">
                                                    <b> Absentee List </b>
                                                </td>
                                            </tr>
                                            <tr class="headerstyle" id="tr5" >
                                                <th>Rank Name</th>
                                                <th>Crew Name</th>
                                                <th></th>
                                            </tr>
                                            </table>
                                        </div>            
                                        <div style="height :450px; overflow-x:hidden;overflow-y:scroll;width:550px;border:solid 1px gray;border-top:none;">
                                                <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table12" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                                                <col width="200px" />
                                                <col />
                                                <col width="17px" />
                                                <asp:Repeater ID="rptAbsenteeRank" runat="server" >
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("RankName")%></td>
                                                        <td><%#Eval("Name")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                            </table>  
                                                </div>
                                                
                                    </td>
                                </tr>
                            </table>
                            
                            
                            <%--Absentee List--%>
                            
                            
                        </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Start--%>
                    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Start">
                    <ContentTemplate>
                    <div style="position:relative; overflow:hidden">
                    <div class="hd1">Last Meeting's Minutes And Status of Corrective Actions </div>
                        <table cellpadding="4" cellspacing="0" border="0" width="95%">
                            <col style="text-align:left" width="420px" />
                            <col style="text-align:left" />
                            <tr>
                                <td>
                                    Minutes of previous SAFECOM meeting discussed : 
                                </td>
                                <td>
                                    <asp:Label ID="lblMinutesOfPreviousSAFECOM" runat="server" class="vcolor"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Absentees in previous SAFECOM meeting briefed : 
                                </td>
                                <td> 
                                 <asp:Label ID="lblAbsenteesInPreviousSAFECOM" runat="server" class="vcolor"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Office comments to previous SAFECOM meeting discussed : 
                                </td>
                                <td>
                                    <asp:Label ID="lblOfficeCommentsToPreviousSAFECOM" runat="server" class="vcolor"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    Any Outstanding item from previous SAFECOM meeting :
                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblOutStandingItemyesNo" runat="server" class="vcolor"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtOutStandingItems" TextMode="MultiLine" Height="355px" Width="99%" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>    
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Safety--%>
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Safety">
                    <ContentTemplate>
                    <div style="float:left; width:100%;">
                        <div class="hd1" > 
                            <asp:Label ID="lblSCMYear" runat="server"></asp:Label>
                        LIST OF CIRCULARS AND ALERTS 
                        </b></div>
                        
                        <table cellpadding="4" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td style="width:400px;">
                                    <div style="width:100%;border-right:solid 1px gray;border-left:solid 1px gray;">
                                    <table cellspacing="0" rules="cols" border="0" id="Table2" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                                    <col width="150px" align="center" />
                                    <col width="150px" align="center" />
                                    <col width="17px" />
                                    <tr class="headerstyle" id="tr2" >
                                        <th>Number</th>
                                        <th>Type</th>
                                        <th></th>
                                    </tr>
                                    </table>
                                    </div>
                                    <div style="height :440px; overflow-x:hidden;overflow-y:scroll;width:100%;border:solid 1px gray;border-top:none;">
                                    <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table3" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                                       <col width="150px" align="center" />
                                    <col width="150px" align="center" />
                                        <col />
                                        <col width="17px" />
                                        <asp:Repeater ID="rpt_Safety" runat="server" >
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("SafetyNumber")%></td>
                                                <td><%#Eval("Safetytype")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>  
                                    </div>
                                </td>
                                <td valign="top">
                                    <div style="overflow-x:hidden;overflow-y:scroll; height:463px;border:solid 1px #c2c2c2;">
                                    <table cellpadding="2" cellspacing="2" border="0"  width="100%"  >
                                        <col width="500px" />
                                        <col />
                                        <tr>
                                            <td>
                                                If all available on board :
                                                <asp:Label ID="lblCrewAvailableOnBoardYesNo" runat="server" CssClass="vcolor"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblCrewAvailableOnBoard" runat="server" CssClass="vcolor"></asp:Label>
                                            </td>
                                            <td>
                                                Are all crew on board familiar with all :
                                                <asp:Label ID="lblCrewfamilierWithAllYesNo" runat="server" style="display:inline;" CssClass="vcolor"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblCrewfamilierWithAll" runat="server" CssClass="vcolor"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Any Accidents/Near Misses on board (S115/S133) : <br />
                                                <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                                <asp:TextBox ID="lblAccidentNearMiss" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Review of Mooring Practices (Ref :procedure in GOM-0040) :<br />
                                                <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                                <asp:TextBox ID="lblMooring" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Best Practices & Recommendation :<br />
                                                
                                                <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                                <asp:TextBox ID="lblBestPracticeSafety" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                            
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%--Health--%>
                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="HEALTH">
                    <ContentTemplate>
                    <div style="float:left; width:100%;">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Review Health,Hygiene & Sanitation Standards on board :</b>
                                    <%--<div style="overflow-x:hidden; overflow-y:scroll; height:180px; padding:3px; border:solid 1px #c2c2c2; margin-top:10px;">--%>
                                        <asp:TextBox ID="lblReviewHealth" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                    <%--</div>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Best Practices & Recommendation :</b>
                                        <asp:TextBox ID="lblBestPracticeHealth" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                 </td>
                            </tr>
                        </table>
                                                
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Security--%>
                    <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Security">
                    <ContentTemplate>
                        <div style="float:left; width:100%;">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b> Review any immediate security related concerns : </b>
                                        <asp:TextBox ID="lblReviewSecurity" runat="server"  TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Best Practices & Recommendation :</b>
                                        <asp:TextBox ID="lblBestPracticeSecurity" runat="server"  TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                                                
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Quality--%>
                    <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="Quality">
                    <ContentTemplate>
                    <div style="float:left; width:100%; overflow-x:hidden; overflow-y:scroll; height:500px;">
                        <table cellpadding="4" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <b> Review of regulatory compliance standards on board (including actions to be taken to comply with Future Regulations) : </b>
                                    <br />
                                    <%--<asp:Label ID="" runat="server" CssClass="vcolor" >ds fhasfh sdhfgasjfgsag fasdg</asp:Label>--%>
                                    <asp:TextBox ID="lblReviewOfRegulatory" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Review of quality systems implementation during the Voyage (Ref: Procedures in ITM-0020) : </b>
                                    <br />
                                    <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                    <asp:TextBox ID="lblReviewOfQuality" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width:100%;border-right:solid 1px gray;border-left:solid 1px gray;">
                                     <table cellspacing="0" rules="cols" border="0" id="Table6" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                                        <col width="150px" align="center" />
                                        <col width="150px" align="center" />
                                        <col />
                                        <col width="17px" />
                                        <tr class="headerstyle" id="tr4" >
                                            <th>NCR#</th>
                                            <th>Closure Date</th>
                                            <th>Remarks</th>
                                            <th></th>
                                        </tr>
                                        </table>
                                        </div>
                                      <div style="height :255px; overflow-x:hidden;overflow-y:scroll;width:100%;border:solid 1px gray;border-top:none;">
                                        <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table8" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> <%--#4371a5--%>
                                            <col width="150px" align="center" />
                                            <col width="150px" align="center" />
                                                <col />
                                                <col width="17px" />
                                            <asp:Repeater ID="rptNCR" runat="server" >
                                                <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("Number")%></td>
                                                    <td><%#Eval("Cdate")%></td>
                                                    <td><%#Eval("Remarks")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                      </table>  
                                    </div>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <b>Review quality KPIs : </b><br />
                                    <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                    <asp:TextBox ID="lblReviewQualityKPI" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Review Crew welfare :</b><br />
                                    <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                    <asp:TextBox ID="lblReviewCrewWelfare" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>    
                                    <b>Best practices & recommendation :</b><br />
                                    <%--<asp:Label ID="" runat="server" CssClass="vcolor"></asp:Label>--%>
                                    <asp:TextBox ID="lblBestPracticeQuality" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>                                                
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%----------------------------------------------------------------------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="Environment">
                    <ContentTemplate>
                    <div style="float:left; width:100%;">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Review Environmental KPIs : </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="lblReviewEnvironmentalKPI" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Best Practices & Recommendation :</b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="lblBestPracticesEnvironment" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%----------------------------------------------------------------------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="AOB">
                    <ContentTemplate>
                    <div style="float:left; width:100%;">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Any other issues :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="lblAnyOtherIssues" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>    
                    </div>
                        
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%----------------------------------------------------------------------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanel8" runat="server" HeaderText="Office Comments">
                    <ContentTemplate>
                    <div style="float:left; width:100%;">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Office Comments : </b>
                                    <br /><br />
                                    <asp:TextBox ID="txtOfficeComments" runat="server" TextMode="MultiLine" Height="300px" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblUpdatedbyOn" runat="server" cellpadding="10" cellspacing="0" border="0" style="float:left;"> 
                                        <tr>
                                            <td>
                                                Update By/On :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUpdatedByOn" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    
                                    
                                    <table id="tblupdatePannel" runat="server" cellpadding="10" cellspacing="0" border="0" style="float:right;"> 
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCommentsMSG" runat="server" style="color:Red; font-weight:bold;" ></asp:Label>
                                            </td>
                                            <td >
                                                <asp:Button ID="btnSaveOffComments" runat="server" OnClick="btnSaveOffComments_OnClick" Text="Save Comments"/>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                        </table>    
                    </div>
                        
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%----------------------------------------------------------------------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanel10" runat="server" HeaderText="SUPTD Visit ">
                    <ContentTemplate>
                    <div style="float:left; width:100%; overflow-X:hidden; overflow-Y:scroll; height:500px;" >
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Compliance with Regulations and Standards :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_CompliancewithRegulations" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:150px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Deviations from safety procedures and safety routines observed during the visit :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_DeviationsfromSafety" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:150px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Details of Training Conducted during the visit. :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_DetailsofTraining" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:150px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Health, Safety and Welfare Measures :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_HealthSafetyMeasures" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:150px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Suggestions for Improvement :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_Suggestions" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:150px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Any other issues :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_AnyOtherTopic" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>    
                    </div>
                        
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                   
                </ajaxToolkit:TabContainer>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </table>
                
            </center>                 
            </div>        
        </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
