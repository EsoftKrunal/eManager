<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Peap_AppraisersEntry.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Peap_AppraisersEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <script src="../../JS/jquery.min.js"></script>
    <script src="../../JS/KPIScript.js"></script>
    <script language="javascript" type="text/javascript">
         function focusthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "70px";
         }
         function blurthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "30px";
         }
         function SetValue(tp, ctl) {
             var PNode = ctl.parentNode;
             var txtScale = getElementById_Like(PNode, "txtRating_KPI", "input");
             
             //alert(txtScale);

             if(txtScale.value.toString() == "")
             {
              txtScale.value = "0";
          }
          
             var Value = parseFloat(txtScale.value);
            
            
             if (parseFloat(tp) == 1) {  // increse

                 if (parseFloat(Value) <= 4)
                     Value++;
                 else
                     Value = 5;
                 txtScale.value = Value;
                 txtScale.onchange();
             }
             else { // decrese

                 if (parseFloat(Value) >= 2)
                     Value--;
                 else
                     Value = 1

                 txtScale.value = Value;
                // txtScale.onchange();
             }


         }
         function ResetIfInvalid(ctl) {
//             if(parseFloat(ctl.value)>5) {
//                 ctl.value = "5";
//             }
//             if(parseFloat(ctl.value)<1) {
//                 ctl.value = "1";
//             }
//             if (isNaN(ctl.value)) {
//                 ctl.value = "1";
//             }

         }
         function getElementById_Like(var_parent, var_Id, var_tag) {
             var ret_ctl = null;
             var ctrs = var_parent.getElementsByTagName(var_tag);
             for (cnt = 0; cnt <= ctrs.length - 1; cnt++) {
                 if (ctrs[cnt].id != null && ctrs[cnt].id != "") {
                     if (ctrs[cnt].id.length >= var_Id.length) {
                         if (var_Id == ctrs[cnt].id.substr(0, var_Id.length)) {
                             ret_ctl = ctrs[cnt];
                             break;
                         }
                         else if (var_Id == ctrs[cnt].id.substr(ctrs[cnt].id.length - var_Id.length, var_Id.length)) {
                             ret_ctl = ctrs[cnt];
                             break;
                         }
                     }
                 }
             }
             return ret_ctl;
         }

         function SetValue_Comp(tp, ctl) {
             var PNode = ctl.parentNode;
             var txtScale = getElementById_Like(PNode, "txtRating_Comp", "input");

             if (txtScale.value.toString() == "") {
                 txtScale.value = "0";
             }

             var Value = parseFloat(txtScale.value);             
             if (parseFloat(tp) == 1) {  // increse

                 if (parseFloat(Value) <= 3)
                     Value++;
                 else
                     Value = 4;
                 txtScale.value = Value;
                 txtScale.onchange();
             }
             else { // decrese

                 if (parseFloat(Value) >= 2)
                     Value--;
                 else
                     Value = 1

                 txtScale.value = Value;
                 txtScale.onchange();
             }


         }
         function ResetIfInvalid_Comp(ctl) {
             if (parseFloat(ctl.value) > 4) {
                 ctl.value = "4";
             }
             if (parseFloat(ctl.value) < 1) {
                 ctl.value = "1";
             }
             if (isNaN(ctl.value)) {
                 ctl.value = "1";
             }

         }
         function showGuidance(CID) {
             window.open('Popup_CompGuidence.aspx?CID=' + CID, 'asdf', '');
         }
         function enterremark(PID, JSID, AID) {
             window.open('PopupRemark.aspx?PID=' + PID + '&JSID=' + JSID + '&AID=' + AID, 'remark', 'title=no,toolbars=no,scrollbars=yes,width=700,height=370,left=150,top=150,addressbar=no,resizable=0,status=0');
         }

         function RefreshRemark() {
             var btn = document.getElementById('btnRefresh');
             btn.click();
         }
         function ShowTrainingNeed(PID,Mode,AID) {
            var qry="Peap_EmpTrainingNeed.aspx?PID=" + PID + "&Mode=" + Mode + "&AID=" + AID + "";
            window.open(qry, "Training");
         }
     </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                              <table cellpadding="2" cellspacing="0" style="width: 100%;background-color: #f9f9f9">
                            <tr>
                                <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" CssClass="text">
                                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                        <col width="310px" />
                                        <col />
                                        <col width="200px" />
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                              <td style="font-size:16px; font-weight:bold; color:White; text-align:center;"> 
                                            Performance Evaluation & Assessment Of Potential (PEAP)</td>
                                            <td style="font-size:10px; color:White;vertical-align:top;">
                                            <asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" style="display:none;" runat="server" CausesValidation="false" />
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding:10px">
                                    <span style="font-size:Large; font-weight:bold; color:#336699;">
                                [ 
                                    <asp:Label ID="txtFirstName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                    <asp:Label ID="txtLastName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label> &nbsp;/ <asp:Label ID="lblPeapLevel" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                &nbsp;]
                                </span>
                                    <span style="font-size:Large; font-weight:bold; color:#6600CC;">
                                ( 
                                    <asp:Label ID="txtOccasion" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                &nbsp;)

                                </span>
                                 <span style="font-size:Large; font-weight:bold; color:#0000FF;">
                                 <asp:Label ID="lblAppraiserName" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                 </span>
                                <span style="float:right"> 
                                    <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Modules/HRD/Images/back_button.gif" CausesValidation="false" AlternateText="Back" OnClick="btnBack_Click" />
                                </span>
                                <br /><br />
                                <div style="padding:5px; background-color:#f2f2f2;">

                                    <span style="float:right"><a target="_blank" href='../Emtm_Performance.aspx?EmpId=<%=PeapEmpID.ToString()%>'>Performance KPI</a></span
                                    <asp:Label ID="lblPeapStatus" runat="server" CssClass="input_box" Font-Bold="True" Font-Size="Large" ForeColor="#993300"></asp:Label>
                                
                                </div>
                                <hr />
                                </td>
                            </tr>
                            <tr>
                            <td>
                                <ajaxToolkit:TabContainer ID="TabContainer1" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged" runat="server" ActiveTabIndex="0" CssCssClass="ajax__myTab" Height="380px" Width="100%">
                                                                       
                                    <%--Performance on the Job--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="Performance on the Job ">
                                    <ContentTemplate>
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                      <ContentTemplate>
                                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                            <colgroup>
                                                <col width="850px" />
                                                <tr><td style="font-weight:bold;">Please select a scale between 1 and 5 from the definition given below.</td></tr>
                                                <tr>
                                                    <td style="vertical-align:top;">
                                                        <table border="0" cellpadding="2" cellspacing="2" style="text-align:left;" width="100%">
                                                            <colgroup>
                                                                <col width="30px" />
                                                                <col />
                                                                <col width="80px" />
                                                                <col width="100px" />
                                                                <col width="100px" />
                                                                <col width="80px" />
                                                                <col width="80px" />
                                                                <col width="80px" />
                                                                <col width="60px" />
                                                                <col width="80px" />
                                                                </colgroup>
                                                                <tr>
                                                                   <td style="text-align:center;"><b>SNo.</b></td>
                                                                    <td><b>Job Responsibility</b></td>
                                                                    <td style="text-align:center;"><b>Weightage</b></td>            
                                                                    <td style="text-align:center;"><b>Target Score </b></td>            
                                                                    <td style="text-align:center;"><b>Achieved Score</b></td>            
                                                                    <td style="text-align:center;"><b>No. of Ques.</b></td>
                                                                    <td style="text-align:center;"><b>Avg Score</b></td>
                                                                    <td style="text-align:center;"><b>Scale</b></td>
                                                                    <td style="text-align:center;"><b>Remarks</b></td>
                                                                    <td><b>&nbsp;</b></td>
                                                                </tr>
                                                            
                                                        </table>
                                                        <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:270px; float:left; padding:2px; border:solid 1px gray;">
                                                        <table border="0" cellpadding="2" cellspacing="2" style="text-align:left;" 
                                                                width="100%">
                                                               <colgroup>
                                                                <col width="30px" />
                                                                <col />
                                                                <col width="80px" />
                                                                <col width="100px" />
                                                                <col width="100px" />
                                                                <col width="80px" />
                                                                <col width="80px" />
                                                                <col width="80px" />
                                                                <col width="60px" />
                                                                <col width="80px" />
                                                                </colgroup>
                                                            <asp:Repeater ID="rptPerformanceOnTheJob" OnItemDataBound="rptPerformanceOnTheJob_ItemDataBound" runat="server">
                                                                <ItemTemplate>
                                                                    <tr style="vertical-align:top;">
                                                                        <td style="text-align:center"><%#Eval("sno")%></td>
                                                                        <td><%#Eval("JobResponsibility")%><asp:HiddenField ID="hfJSId" runat="server" Value='<%#Eval("JSId")%>' /></td>
                                                                        <td style="text-align:center"><asp:Label ID="lblWeightage" Text='<%#Eval("Waitage").ToString() + "%"%>' runat="server"></asp:Label></td>
                                                                        <td style="text-align:center"><%#Eval("TARGET")%></td>
                                                                        <td style="text-align:center"><%#Eval("ACHEIVED")%></td>
                                                                        <td style="text-align:center"><%#Eval("NOQ")%></td>
                                                                        <td style="text-align:center"><%#Eval("Rating")%></td>
                                                                        <td><div style="text-align:center"><asp:TextBox ID="txtRating" runat="server" Font-Size="11px" style="text-align:center; font-size:13px; font-weight:bold; color:Purple; border:none;" Text='<%#Eval("Answer1")%>' Width="40px"></asp:TextBox></div></td>
                                                                        <td style="text-align:center"><asp:ImageButton ID="imgRemark" CommandArgument='<%#Eval("JSId") %>' OnClick="imgRemark_Click"  CausesValidation="false" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" /></td>
                                                                        <td style="text-align:left;"><asp:Button ID="btnFillRating" CommandArgument='<%#Eval("JSID")%>' Text="KPI Rating" OnClick="btnFillKPI_Click" CssClass="btn" CausesValidation="false" Width="65px" runat="server" /></td>
                                                                    </tr>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <colgroup>
                                                            </colgroup>
                                                            <table>
                                                            </table>
                                                        </div>
                                                        <div style="float:left; width:450px;">
                                                            <img src="../../Images/rating-5.png" usemap="#Description" style="border:none;" />
                                                            <map name="Description">
                                                                <area shape="rect" coords="0,0,55,55" title="Did not complete the job within the stipulated time, required constant supervision, high degree of errors, did not meet expectations." >
                                                                <area shape="rect" coords="80,0,140,140" title="Managed to complete most of the tasks but only under supervision, exceeded time limit in many tasks, made many errors, met a few expectations." >
                                                                <area shape="rect" coords="170,0,220,220" title="Did not require too much supervision, completed some of the tasks within the stipulated time, made a few errors, met some of the expectations." >
                                                                <area shape="rect" coords="250,0,310,310" title="Worked under minimal supervision, completed most of the tasks within the stipulated time, did not commit any errors, met most of the expectations.">
                                                                <area shape="rect" coords="340,0,410,410" title="Worked with no supervision, completed all the tasks within or before the stipulated time, zero errors, met all the expectations." >
                                                            </map>
                                                        </div>
                                                        <div style="float:right; text-align:right; ">
                                                        <div style="float :left">
                                                            <asp:Label ID="lblMsg_JR" runat="server" Style="color: Red; float: left; margin-top: 3px; margin-right: 5px;"></asp:Label>
                                                        </div>
                                                        <div style="text-align:right; float:right; width:100%; width:220px;"> 
                                                            <table border="0" cellpadding="2" cellspacing="0" style="text-align:left;border-collapse:collapse;font-size:15px; " width="100%">
                                                                <tr>
                                                                    <td style="text-align:right; width:150px;"><b style="font-size:12px;">Scale :&nbsp;</b></td>
                                                                    <td style="text-align:left;" runat="server" id="JRColor"><b style="font-size:12px;"><asp:Label ID="lblTotScore_JS" runat="server"></asp:Label></b></td>
                                                                </tr>
                                                                <tr runat="server" visible="false">
                                                                    <td style="text-align:right;font-size:17px;"><b style="font-size:12px;">Avg. Perf. Score :&nbsp;</b></td>
                                                                    <td style="text-align:left;" ><b style="font-size:12px;"><asp:Label ID="lblPerformanceScore_JS" runat="server"></asp:Label></b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                         <asp:Button ID="btnSaveJR" runat="server" OnClick="btnSaveJR_Click" CausesValidation="false" Style="padding-right: 5px; margin-top: 3px; margin-left: 25px;" Text="Save" Width="80px" />
                                                         <asp:Button ID="btnNotify" runat="server" OnClick="btnNotify_Click" Visible="false" CausesValidation="false" Style="padding-right: 5px; margin-top: 3px; margin-left: 25px;" Text="Notify" Width="80px" />
                                                     
                                                        </div>
                                                        
                                                        </div>



                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAssignKPI" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:850px; padding :0px; text-align :center; border :solid 10px #666; background : white; z-index:150;top:50px;opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpKPI">
                <ContentTemplate>
                <center>
                <div style="border:1px solid #A9A9C6;">
                <table cellpadding="1" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="background-color:#f1f1f1; padding:6px; "><b><span style="font-size:13px">KPI Scale</span></b></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <table width="100%" cellspacing="0" cellpadding="3" border="0" rules="all" style="border-collapse: collapse;">
                                <colgroup>
                                    <col />
                                    <col width="250px;" />
                                    <col width="80px;" />
                                    <col width="17px;" />
                                </colgroup>
                                <tr align="left" class="blueheader">                                   
                                    <th scope="col" style="font-size: 11px; text-align:left;">KPI Name</th>
                                    <th scope="col" style="font-size: 11px; text-align:left;">Description</th>
                                    <th scope="col" style="font-size: 11px;">Scale</th>
                                    <th scope="col" style="font-size: 11px;"></th>
                                </tr>
                            </table>
                            <div id="Div3" runat="server" style="overflow-x: hidden; overflow-y: scroll; width: 100%;height: 150px; border: solid 1px Gray;" onscroll="SetScrollPos(this)">
                                <table width="100%" cellspacing="0" cellpadding="3" border="1" rules="all" style="border-collapse: collapse;">
                                    <colgroup>
                                        <col />
                                        <col width="250px;" />
                                        <col width="80px;" />
                                        <col width="17px;" />
                                    </colgroup>
                                    <asp:Repeater runat="server" ID="rptKPI">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: left; font-size: 11px;">
                                                    <asp:Label ID="lblKPIName" runat="server" Text='<%#Eval("KPIName")%>'></asp:Label>
                                                    <asp:HiddenField ID="hfKPIID" runat="server" Value='<%#Eval("KPIId")%>' />
                                                </td>
                                                <td style="text-align: left; font-size: 11px;">
                                                    <asp:Label ID="lblKPIValue" runat="server" Text='<%#Eval("KPIValue")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: center; font-size: 11px;">
                                                    <%--<asp:TextBox ID="txtRating" runat="server" MaxLength="3" Text='<%#Eval("Rating")%>' Width="30px"></asp:TextBox>--%>
                                                    <%--<img src="../../Images/perv.jpg" style="height:16px; float:left;cursor:pointer;" onclick="SetValue(-1,this);"/>
                                                    <asp:TextBox ID="txtRating_KPI" runat="server" EnableViewState="true" Font-Size="11px" MaxLength="1" onblur="ResetIfInvalid(this);" style="text-align:center; font-size:13px; font-weight:bold; color:Purple; border:none;" Text='<%#Eval("Rating")%>' Width="20px"></asp:TextBox>
                                                    <img src="../../Images/next.jpg" style="height:16px;float:right; cursor:pointer;" onclick="SetValue(1,this);"/>--%>
                                                    <asp:DropDownList ID="ddlRating" SelectedValue='<%#Eval("Rating")%>' Width="60px" runat="server">
                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="NA" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <%-- <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div>
                                <div style="padding:5px; background:#f4f4f4">Enter Remarks Below:</div>
                                <asp:TextBox ID="txtAnswerJR" runat="server" Height="200px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; padding-right:5px;">
                        <asp:Label ID="lblKPIMsg" runat="server" style="color:Red;"></asp:Label>&nbsp;
                            <asp:Button ID="btnSaveKPI" runat="server" CssClass="btn" OnClick="btnSaveKPI_Click" Text="Save" CausesValidation="false" />
                            <asp:Button ID="btnCancelKPI" runat="server" CssClass="btn"  OnClick="btnCancelKPI_Click"  Text="Close" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                </div>
                </center>
                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnCancelKPI" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>

                                         
                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    
                                    <%--Assessment of Competency--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Assessment of Competency">
                                    <ContentTemplate>
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                      <ContentTemplate>
                                        <div style="float:left; width:100%;">
                                            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                <col width="900px" />
                                                <col />
                                               <tr>
                                                <td style="vertical-align:top; text-align:left;">
                                                        <%------------ Labels ---------------%>
                                                        <b>Please select a scale between 1 and 4 from the definition given below.</b><br />
	                                                    <table  cellpadding="2" cellspacing="2" border="0" style="border-collapse:collapse; font-size:11px;" width="100%">
                				                            
                                                            <tr>
                                                                <td>
                                                                    
                                                                </td>
                                                            </tr>
	                                                    </table>
                                                        <br />
                                                    </td>
                                               </tr>
                                                <tr>
                                                    <td>
                                                     <table cellpadding="1" cellspacing="1" border="0" style="border-collapse:collapse;" rules="" width="100%" >
                                                            <colgroup>
                                                            <col  />
                                                            <col width="80px"/>
                                                            <col width="100px"/>
                                                            </colgroup>
                                                            <tr>
                                                                <td><b>Competency</b></td>
                                                                <td><b>Guidance</b></td>
                                                                <td><b>Scale (1-4)</b></td>
                                                            </tr>
                                                        </table>
                                                     <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:240px; float:left; padding:2px; border:1px solid #d2d2d2;" id="FDSFSD" class="ScrollAutoReset">
                                                       
                                                        <table cellpadding="1" cellspacing="1" border="0" style="border-collapse:collapse;" rules="" width="100%" >
                                                           <colgroup>
                                                            <col  />
                                                            <col width="80px"/>
                                                            <col width="100px"/>
                                                            </colgroup>

                                                            <asp:Repeater ID="rptCompetency" runat="server">
                                                                <ItemTemplate>
                                                                <tr>
                                                                    <td><%#Eval("Competency") %></td>
                                                                    <td style="text-align:center">
                                                                    <asp:ImageButton ID="imgGuidelines" CommandArgument='<%#Eval("CId") %>' OnClick="imgGuidelines_Click" ToolTip="Show Guidance" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/information.gif" runat="server" /> 
                                                                    </td>
                                                                    <td >
                                                                      <div style="text-align:center">
                                                                           <img src="../../Images/perv.jpg" style="height:16px; float:left;cursor:pointer;" onclick="SetValue_Comp(-1,this);"/>
                                                                              <asp:TextBox ID="txtRating_Comp" runat="server" EnableViewState="true" AutoPostBack="true" OnTextChanged="txtRating_Comp_TextChanged" Font-Size="11px" MaxLength="1" onblur="ResetIfInvalid_Comp(this);" style="text-align:center; font-size:13px; font-weight:bold; float:left; color:Purple;border:none;" Text='<%#Eval("Answer")%>' Width="20px"></asp:TextBox>
                                                                           <img src="../../Images/next.jpg" style="height:16px;float:left; cursor:pointer;" onclick="SetValue_Comp(1,this);"/>
                                                                        
                                                                        </div>  
                                                                        
                                                                        <asp:HiddenField ID="hfComp" Value='<%#Eval("CId") %>' runat="server" />
                                                                    </td>
                                                                </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <div style="float: left; width: 550px">
                                                <img src="../../Images/rating-4.png" />
                                            </div>
                                            <div style="float: right; padding-top: 0px;"> 
                                                <div style="float:left" >
                                                <asp:Label ID="lblMsg_Comp" Style="color: Red; float: right; margin-top: 3px; margin-right: 5px;" runat="server"></asp:Label>
                                                </div>
                                                <div style="text-align:right; float:right; width:100%; width:220px;"> 
                                                    <table border="0" cellpadding="2" cellspacing="0" style="text-align:left;border-collapse:collapse;font-size:15px; " width="100%">
                                                                       <tr>
                                                          <td style="text-align: right; width: 150px;">
                                                              <b style="font-size: 12px;">Total Score :&nbsp;</b>
                                                          </td>
                                                          <td style="text-align: left;">
                                                              <b style="font-size: 12px;">
                                                                  <asp:Label ID="lblTotScore_Comp" runat="server"></asp:Label></b>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td style="text-align: right; font-size: 17px;">
                                                              <b style="font-size: 12px;">Avg. Comp. Score :&nbsp;</b>
                                                          </td>
                                                          <td style="text-align: left;" runat="server" id="CompColor">
                                                              <b style="font-size: 12px;">
                                                                  <asp:Label ID="lblPerformanceScore_Comp" runat="server"></asp:Label></b>
                                                          </td>
                                                      </tr>
                                                  </table>
                                                   <asp:Button ID="btnSave_Comp" Text="Save" OnClick="btnSave_Comp_Click" CausesValidation="false" Style="float: right;padding-right: 5px; margin-top: 3px; margin-left: 25px;" runat="server" Width="80px"/>
                                                   <asp:Button ID="Button1" runat="server" OnClick="btnNotify_Click" CausesValidation="false" Visible="false" Style="padding-right: 5px; margin-top: 3px; margin-left: 25px;" Text="Notify" Width="80px" />
                                                  </div>
                                            </div>
                                        </div>
                                      </ContentTemplate>
                                     </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    
                                    <%--Potential Assessment--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Potential Assessment">
                                    <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                      <ContentTemplate>
                                         <div style="overflow-x:hidden;overflow-y:hidden; width:100%; height:350px; float:left; padding:2px;" >
                                        <table cellpadding="2" cellspacing="2" width="100%">
                                            <%--<tr style="background-color:#d2d2d2;">
                                                <td  style="text-align:center; font-weight:bold; font-size:12px;">
                                                   Potential Assessment and Identification of Training & Development Needs
                                                    <br />
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>    
                                                    <b>A. What additional responsibilities is he/she capable of taking beyond the normal job functions? </b>   
                                                    <asp:TextBox ID="txtPotAdditionalResponsibilities" runat="server" Width="100%" TextMode="MultiLine" Height="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <b> B. Is he/she ready for promotion? </b>
                                                    <asp:RadioButtonList ID="rdoReadyForPromotion" RepeatDirection="Horizontal" runat="server" >
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                        <asp:ListItem Text="Later" Value="L"></asp:ListItem>
                                                        <asp:ListItem Text="NA" Value="E"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <samp >
                                                       <b>If found unsuitable for promotion, please specify reasons</b>
                                                        <asp:TextBox ID="txtLaterPromotionReasion"  runat="server" Width="100%" TextMode="MultiLine" Height="120px"></asp:TextBox>
                                                    </samp>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>       
                                    </div>
                                         <div>
                                           <asp:Button ID="Button2" OnClick="btnNotify_Click" runat="server" CausesValidation="false" Visible="false" Style="float: right;padding-right: 5px; margin-top: 3px; margin-left: 25px;" Text="Notify" Width="80px" />
                                           <asp:Button ID="btnSave_Pot" Text="Save" OnClick="btnSave_Pot_Click" Style="float: right;padding-right: 5px; margin-top: 3px; margin-left: 25px;" runat="server" Width="80px"/>
                                           <asp:Label ID="lblMsg_Pot" Style="color: Red; float: right; margin-top: 3px; margin-right: 5px;" runat="server"></asp:Label>
                                    </div>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </ajaxToolkit:TabPanel>

                                     <%--Training & Development--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Training & Development">
                                    <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div style="overflow-x: hidden; overflow-y: hidden; width: 100%; height: 370px; float: left;
                                                padding: 2px;">
                                                <table cellpadding="2" cellspacing="2" width="100%">
                                                    <tr>
                                                    <td>
                                                      <div style="text-align: center; height: 20px; padding-left: 20px; padding-top: 5px;font-size: 12px; font-weight: bold; font-family: Verdana; background-color: #d2d2d2;">
                                                                Training Attanded
                                                      </div>
                                                      <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                                                border-collapse: collapse;">
                                                                <colgroup>
                                                                    <col style="width: 40px;" />                                                                    
                                                                    <col />
                                                                    <col style="width: 120px;" />
                                                                    <col style="width:90px;" />
                                                                    <col style="width: 25px;" />
                                                                    <col style="width: 17px;" />
                                                                    <tr align="left" class="blueheader">
                                                                        <td>
                                                                            Sr#
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            Training Title
                                                                        </td>
                                                                        <td>
                                                                            Last Done Date
                                                                        </td>
                                                                        <td>Due Date</td>
                                                                        <td>
                                                                            ReAssign
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </colgroup>
                                                            </table>
                                                      <div id="Div1" runat="server" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 120px; text-align: center;">
                                                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                                                    border-collapse: collapse;">
                                                                    <colgroup>
                                                                        <col style="width: 40px;" />                                                                    
                                                                        <col />
                                                                        <col style="width: 120px;" />
                                                                        <col style="width:90px;" />
                                                                        <col style="width: 25px;" />
                                                                        <col style="width: 17px;" />
                                                                    </colgroup>
                                                                    <%--<asp:UpdatePanel ID="upPot" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>--%>
                                                                    <asp:Repeater ID="rptTrainingDone" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr class="row">
                                                                                <td align="center">
                                                                                    <%#Eval("SrNo")%>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#Eval("TrainingName")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("LastDoneDate")%>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:TextBox ID="txtDueDt"  MaxLength="12" Width="80px" runat="server" ></asp:TextBox >
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDueDt" TargetControlID="txtDueDt" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                                                                </td>
                                                                                <td>
                                                                                <asp:CheckBox ID="chkReassign" AutoPostBack="true" OnCheckedChanged="chkReassign_CheckedChanged" runat="server" />
                                                                                <asp:HiddenField ID="hfTrainingId" Value='<%#Eval("TrainingId") %>' runat="server" />
                                                                                </td>
                                                                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <%--</ContentTemplate>
                                                           </asp:UpdatePanel>--%>
                                                                </table>
                                                            </div>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td> 
                                                            <div style="text-align: center; height: 20px; padding-left: 20px; padding-top: 5px;
                                                                font-size: 12px; font-weight: bold; font-family: Verdana; background-color: #d2d2d2;">
                                                                Recommended Trainings
                                                            </div>
                                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                                                border-collapse: collapse;">
                                                                <colgroup>
                                                                    <col style="width: 40px;" />
                                                                    <col style="width: 25px;" />
                                                                    <col />
                                                                    <col style="width: 110px;" />
                                                                    <col style="width: 90px;" />
                                                                    <col style="width: 17px;" />
                                                                </colgroup>
                                                                    <tr align="left" class="blueheader">
                                                                        <td>Sr#</td>
                                                                        <td></td>
                                                                        <td style="text-align: left">Training Title</td>
                                                                        <td>Last Done Date</td>
                                                                        <td>Due Date</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                             
                                                            </table>
                                                            <div id="Div2" runat="server" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden;
                                                                width: 100%; height: 120px; text-align: center;">
                                                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                                     <colgroup>
                                                                    <col style="width: 40px;" />
                                                                    <col style="width: 25px;" />
                                                                    <col />
                                                                    <col style="width: 110px;" />
                                                                    <col style="width: 90px;" />
                                                                    <col style="width: 17px;" />
                                                                </colgroup>
                                                                    <%--<asp:UpdatePanel ID="upPot" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>--%>
                                                                    <asp:Repeater ID="rptPlannedTrainings" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr class="row">
                                                                                <td align="center">
                                                                                    <%#Eval("SrNo")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="imgbtnDelete" OnClientClick="javascript:return confirm('Are you sure to delete training?');"
                                                                                        CausesValidation="false" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete Training"
                                                                                        CommandArgument='<%#Eval("TrainingRecommId") %>' OnClick="imgbtnDelete_Click"
                                                                                        runat="server" />
                                                                                </td>
                                                                                <td align="left"><%#Eval("TrainingName")%></td>
                                                                                <td><%#Eval("LastDoneDate")%></td>
                                                                                <td><%#Eval("DueDate")%></td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       <td>
                                                           <asp:Button ID="btnTrainingNeed" OnClick="btnTrainingNeed_Click" ToolTip="Click to add training." Style="font-weight: bold; float:right;" Text="Add New Training" CausesValidation="false" runat="server" />
                                                       </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                     </ContentTemplate>
                                     </ajaxToolkit:TabPanel>
                                                                      
                                </ajaxToolkit:TabContainer>
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
    </form>
</body>
</html>
