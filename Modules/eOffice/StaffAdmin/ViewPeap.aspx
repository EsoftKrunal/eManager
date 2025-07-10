<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPeap.aspx.cs" Inherits="Emtm_ViewPeap" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Page</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">

     <script language="javascript" type="text/javascript">
         function focusthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "70px";
         }
        function blurthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "30px";
         }
     </script> 
</head>
<body>
    <form id="form1" runat="server" > 
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div style="">
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
                                            <td style="font-size:12px; color:White; text-align:center;"> 
                                                PERFORMANCE EVALUATION AND ASSESSMENT OF POTENTIAL  [ PEAP ]             
                                            </td>
                                            <td style="font-size:10px; color:White;vertical-align:top;"> 
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="1" cellspacing="1" border="0" width="100%" >
                                        <col width="5px" />
                                        <col width="110px"  />
                                        <col width="20px" />
                                        <col width="250px" />

                                        <col width="110px" />
                                        <col width="20px" />
                                        <col width="250px" />

                                        <col width="110px" />
                                        <col width="20px" />
                                        <col />
                                        <tr >
                                            <td></td>
                                            <td style="text-align:Left;"><b>PEAP Level </b></td>
                                            <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="lblPeapLevel" runat="server"></asp:Label>
                                            </td>

                                            <td style="text-align:left;"> <b>Ocassion </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtOccasion" runat="server" CssClass="input_box"></asp:Label>
                                            </td>

                                            <td style="text-align:left;"> <b>Position </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="lblPosition" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                           <td></td> 
                                            <td style="text-align:left;"><b>Emp # </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtEmpNo" runat="server" CssClass="input_box"></asp:Label>
                                            </td>

                                            <td style="text-align:left;"><b>First&nbsp; Name </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtFirstName" runat="server" CssClass="input_box"></asp:Label>
                                            </td>

                                            <td style="text-align:left;"><b>Last Name </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtLastName" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td></td>
                                            <td style="text-align:left;"><b>Department </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtDept" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                            <td style="text-align:left;"><b>Appraisal From </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtAppraisalFrom" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                            <td style="text-align:left;"><b>Appraisal To </b> </td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtAppraislaTo" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                        </tr>
                                             <tr>
                                             <td></td>
                                            <td style="text-align:left;"><b>Dt. Joined </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtDJC" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                            <td style="text-align:left;"><b>Perf. Score </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtPerformanceScore" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                            <td style="text-align:left;"><b>Comp. Score </b></td>
                                             <td> : </td>
                                            <td style="text-align:left;">
                                                <asp:Label ID="txtCompetencyScore" runat="server" CssClass="input_box"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>
                            <tr id="trAppraiser" runat="server" visible="false" style="text-align:center;">
                              <td><b>Select appraiser :</b>&nbsp;<asp:DropDownList ID="ddlAppraiser" AutoPostBack="true" OnSelectedIndexChanged="ddlAppraiser_SelectedIndexChanged" Width="250px" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                            <td>
                                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssCssClass="ajax__myTab" Height="380px" Width="100%">
                                    
                                    <%--Self Appraisal--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Self Appraisal">
                                    <ContentTemplate>
                                     <asp:UpdatePanel ID="up1" runat="server">
                                      <ContentTemplate>                                      
                                       <table cellpadding="2" cellspacing="2" border="0" width="100%" style="text-align:left;" >
                                        <tr style="background-color:#d2d2d2;">
                                            <td style="text-align:center; font-weight:bold; font-size:12px;">Self Appraisal</td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight:bold;"> Instructions for completing Section 1  </td>
                                        </tr>
                                        <tr>
                                            <td>1. Please reply to all questions in detail. You can upload pdf files as supporting.</td>
                                        </tr>
                                        <tr>
                                            <td>2. Be specific and to the point.</td>
                                        </tr>
                                        <tr>
                                            <td>3. This document will form the basis of your performance discussion with your superior. Please make sure you carry data to support what you have written in this part.</td>
                                        </tr>
                                    </table>
                                               
                                        <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:255px; float:left; padding:2px; border:1px solid #e2e2e2 " >
                                            <br /><br />
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <col width="20px" />
                                                <col />
                                                <asp:Repeater ID="rptSelfAssessment" runat="server">
                                                    <ItemTemplate>
                                                        <tr style="text-align:left;">
                                                            <td>
                                                                <b> <%#Eval("Srno")%> .</b>
                                                            </td>
                                                            <td>
                                                               <b> <%#Eval("Qtext") %></b><br />
                                                                <asp:TextBox ID="txtAnswer" runat="server" onfocus="focusthis(this);" onblur="blurthis(this);" required="yes" TextMode="MultiLine" Width="70%" Height="30px" Text='<%#Eval("Answer") %>'></asp:TextBox>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" Width="29%"/>
                                                                <asp:HiddenField ID="hfQID" Value='<%#Eval("QID")%>' runat="server" />
                                                                <br /><br />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                       
                                        <div  >
                                         <asp:Button ID="btnSaveSA" Text="Save" OnClick="btnSaveSA_Click" style="float:right; padding-right:5px; margin-top:3px; margin-left:25px;" runat="server" />
                                         <asp:CheckBox ID="chkSAComp" Text="Self Appraisal Completed" style="float:right;margin-top:3px; margin-left:25px;" runat="server" />
                                         <asp:Label ID="lblSAmsg" style="color:Red;float:right;margin-top:3px; margin-right:5px;" runat="server"></asp:Label>
                                        </div>

                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    
                                    <%--Performance on the Job--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="Performance on the Job ">
                                    <ContentTemplate>
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                      <ContentTemplate>
                                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                            <col width="850px" />
                                            <tr style="background-color:#d2d2d2;">
                                               <td style="text-align:center; font-weight:bold; font-size:12px;">Performance on the Job</td>
                                            </tr>
                                            <tr>
                                               <td style="font-weight:bold;">Please select a scale between 1 and 5 from the definition given below.</td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;">
                                                    <table width="100%" cellpadding="2" cellspacing="2" border="1" rules="rows" style="border-collapse:collapse; text-align:left; font-size:11px;" >
                                                        <tr class="TblHeading">
                                                            <td>Scale</td>
                                                            <td>Indicates</td>
                                                            <td>Definition</td>
                                                        </tr>
                                                        <tr>
                                                            <td>1</td>
                                                            <td>Poor Output</td>
                                                            <td>
                                                                Did not complete the job within the stipulated time, required constant 
                                                                supervision, high degree of errors, did not meet expectations.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td>
                                                                Below Average Output
                                                            </td>
                                                            <td>
                                                                Managed to complete most of the tasks but only under supervision, 
                                                                exceeded time limit in many tasks, made many errors, met a few expectations.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>3</td>
                                                            <td>Average Output</td>
                                                            <td>
                                                                Did not require too much supervision, completed some of the tasks 
                                                                within the stipulated time, made a few errors, met some of the expectations.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>4</td>
                                                            <td>Good Output</td>
                                                            <td>
                                                                Worked under minimal supervision, completed most of the tasks within the stipulated 
                                                                time, did not commit any errors, met most of the expectations.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>5</td>
                                                            <td>Outstanding Output</td>
                                                            <td>
                                                                Worked with no supervision, completed all the tasks within or <br />
                                                                 before the stipulated time, zero errors, met all the expectations.
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top;" >
                                                <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:160px; float:left; padding:2px;" >
                                                    <table cellpadding="2" cellspacing="2" border="0" width="100%" style="text-align:left; border:solid 1px Gray;" >
                                                        <col width="450px" />
                                                        <col width="90px" />
                                                        <col />
                                                        <tr>
                                                            <td><b>Job Responsibility</b></td>
                                                            <td><b>Scale</b></td>
                                                            <td><b>Actual Example/s to support the assessment</b></td>
                                                        </tr>
                                                    </table>
                                                    
                                                    <table cellpadding="2" cellspacing="2" border="1" width="100%" style="text-align:left; border:solid 1px Gray;border-collapse:collapse;" rules="rows"  >
                                                        <col width="450px" />
                                                        <col width="90px" />
                                                        <col />
                                                        <asp:Repeater ID="rptPerformanceOnTheJob" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="vertical-align:top;">
                                                                    <td>
                                                                        <%#Eval("JobResponsibility")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlScale" selectedvalue='<%#Eval("Answer1")%>'  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlScale_SelectedIndexChanged"  Width="70px">
                                                                            <asp:ListItem Text=" Select " Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                    <asp:HiddenField ID="hfJSId" Value='<%#Eval("JSId")%>' runat="server" /> 
                                                                        <asp:TextBox ID="txtAnswerJR" onfocus="focusthis(this);" onblur="blurthis(this);" runat="server" Text='<%#Eval("Remark")%>' TextMode="MultiLine" Width="99%" Height="30px" ></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td style="text-align:right;"><b>Total Score :&nbsp;</b></td>
                                                            <td style="text-align:left;"><asp:Label ID="lblTotScore_JS" runat="server"></asp:Label></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;"><b>Performance Score :&nbsp;</b></td>
                                                            <td style="text-align:left;"><asp:Label ID="lblPerformanceScore_JS" runat="server"></asp:Label></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                 </div>
                                                    <div>
                                                        <asp:Button ID="btnSaveJR" Text="Save" OnClick="btnSaveJR_Click" Style="float: right;padding-right: 5px; margin-top: 3px; margin-left: 25px;" runat="server" />
                                                        <%--<asp:CheckBox ID="chkAC_JR" Text="Self Appraisal Completed" Style="float: right;margin-top: 3px; margin-left: 25px;" runat="server" />--%>
                                                        <asp:Label ID="lblMsg_JR" Style="color: Red; float: right; margin-top: 3px; margin-right: 5px;" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                         
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
                                               <tr style="background-color:#d2d2d2;">
                                                <td  style="text-align:center; font-weight:bold; font-size:12px;">
                                                    Assessment of Competency 
                                                    <br />
                                                </td>
                                               </tr>
                                               <tr>
                                                <td style="vertical-align:top; text-align:left;">
                                                        <%------------ Labels ---------------%>
                                                        <b>Please select a scale between 1 and 4 from the definition given below.</b><br /><br />
	                                                    <table  cellpadding="2" cellspacing="2" border="0" style="border-collapse:collapse; font-size:11px;" width="100%">
                				                            
                                                            <tr>
                                                                <td>
                                                                    <table width="350px" cellspacing="2"  cellpadding="2" border="1" style="border-collapse:collapse; text-align:left;">
                                                                        <tr class="TblHeading">
                                                                            <td>Scale</td>
                                                                            <td>Definition</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>1</td>
                                                                            <td>Never demonstrates.</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>2</td>
                                                                            <td>Demonstrates some times.</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>3</td>
                                                                            <td>Demonstrates most of the time.</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>4</td>
                                                                            <td>Demonstrates always.</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
	                                                    </table>
                                                        <br />
                                                    </td>
                                               </tr>
                                                <tr>
                                                    <td>
                                                        <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:185px; float:left; padding:2px; border:1px solid #d2d2d2;" >
                                                        <table cellpadding="1" cellspacing="1" border="1" style="border-collapse:collapse;" rules="rows" width="100%" >
                                                            <col width="500px" />
                                                            <col />
                                                             <tr>
                                                                <td><b>Competency*</b></td>
                                                                <td><b>Scale</b></td>
                                                            </tr>
                                                        </table>
                                                        <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                                            <col width="500px" />
                                                            <col />
                                                            <asp:Repeater ID="rptCompetency" runat="server">
                                                                <ItemTemplate>
                                                                <tr>
                                                                    <td> <%#Eval("Competency") %> </td>
                                                                    <td>    
                                                                        <asp:DropDownList  ID="ddlScale_C" selectedvalue='<%#Eval("Answer1")%>' AutoPostBack="true" OnSelectedIndexChanged="ddlScale_C_SelectedIndexChanged" runat="server" Width="70px">
                                                                            <asp:ListItem Text=" Select " Value="0" ></asp:ListItem>
                                                                            <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                                                                            <asp:ListItem Text="2" Value="2" ></asp:ListItem>
                                                                            <asp:ListItem Text="3" Value="3" ></asp:ListItem>
                                                                            <asp:ListItem Text="4" Value="4" ></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hfComp" Value='<%#Eval("CId") %>' runat="server" />
                                                                    </td>
                                                                </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr>
                                                            <td style="text-align:right;"><b>Total Score :&nbsp;</b></td>
                                                            <td style="text-align:left;"><asp:Label ID="lblTotScore_Comp" runat="server"></asp:Label></td
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;"><b>Performance Score :&nbsp;</b></td>
                                                            <td style="text-align:left;"><asp:Label ID="lblPerformanceScore_Comp" runat="server"></asp:Label></td
                                                        </tr>
                                                        </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                           <span style="float:left; padding-left:5px;"><i>* Definition of each Competency is available in the Office Administrative Manual.</i></span>
                                           <asp:Button ID="btnSave_Comp" Text="Save" OnClick="btnSave_Comp_Click" Style="float: right;padding-right: 5px; margin-top: 3px; margin-left: 25px;" runat="server" />
                                           <%--<asp:CheckBox ID="chkAC_JR" Text="Self Appraisal Completed" Style="float: right;margin-top: 3px; margin-left: 25px;" runat="server" />--%>
                                           <asp:Label ID="lblMsg_Comp" Style="color: Red; float: right; margin-top: 3px; margin-right: 5px;" runat="server"></asp:Label>
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
                                         <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:350px; float:left; padding:2px;" >
                                        <table cellpadding="2" cellspacing="2" width="100%">
                                            <tr style="background-color:#d2d2d2;">
                                                <td  style="text-align:center; font-weight:bold; font-size:12px;">
                                                   Potential Assessment and Identification of Training & Development Needs
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>    
                                                    A. What additional responsibilities is he capable of taking beyond the normal job functions?    
                                                    <asp:TextBox ID="txtPotAdditionalResponsibilities" onfocus="focusthis(this);" onblur="blurthis(this);" required='yes' runat="server" Width="90%" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPotAdditionalResponsibilities" ErrorMessage="*" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    B. Is he ready for promotion? (Mark with “X” as appropriate).
                                                    <asp:RadioButtonList ID="rdoReadyForPromotion" runat="server" >
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                        <asp:ListItem Text="Later" Value="L"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdoReadyForPromotion" ErrorMessage="*" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                                    <samp >
                                                        Please specify reasons
                                                        <asp:TextBox ID="txtLaterPromotionReasion" onfocus="focusthis(this);" onblur="blurthis(this);" runat="server" Width="90%" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                    </samp>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    C. Identify training needs: (refer to the guidelines with Master for list of courses available)<br />
                                                    i. For undertaking present job functions more effectively.
                                                    <asp:TextBox ID="txtTrainingNeed" onfocus="focusthis(this);" onblur="blurthis(this);" runat="server" Width="90%" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>       
                                    </div>
                                         <div>
                                           <asp:Button ID="btnSave_Pot" Text="Save" OnClick="btnSave_Pot_Click" Style="float: right;padding-right: 5px; margin-top: 3px; margin-left: 25px;" runat="server" />
                                           <%--<asp:CheckBox ID="chkAC_JR" Text="Self Appraisal Completed" Style="float: right;margin-top: 3px; margin-left: 25px;" runat="server" />--%>
                                           <asp:Label ID="lblMsg_Pot" Style="color: Red; float: right; margin-top: 3px; margin-right: 5px;" runat="server"></asp:Label>
                                    </div>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    
                                    
                                    <%--Appraisee’s Remarks--%>
                                    <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Appraisee’s Remarks">
                                    <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                      <ContentTemplate>
                                        <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:350px; float:left; padding:2px;" >
                                        <table cellpadding="2" cellspacing="2" width="100%" style="text-align:left;vertical-align:top;">
                                            <tr style="background-color:#d2d2d2;">
                                                <td  style="text-align:center; font-weight:bold; font-size:12px;">
                                                   Appraisee’s Remarks
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>    
                                                    <b> Enter Remarks Here</b>
                                                    <asp:TextBox ID="txtAppraiseeRemarks" onfocus="focusthis(this);" onblur="blurthis(this);" runat="server" Width="90%" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="2" cellspacing="2" border="0" width="100%" style="text-align:left; vertical-align:top;">
                                                        <tr>
                                                            <td colspan="4"><b>  Appraisee </b></td>
                                                        </tr>
                                                        <tr>
                                                            <td> Name : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAppraiseeName" runat="server" ></asp:TextBox>
                                                            </td>
                                                            <td> Rank : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRankName" runat="server" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="4"><b> Appraiser </b></td>
                                                        </tr>
                                                        <tr>
                                                            <td> Name : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAppraiserName" runat="server" ></asp:TextBox>
                                                            </td>
                                                            <td> Rank : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAppraiserRank" runat="server" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4"> 
                                                                <br />
                                                                <b>GM’s remarks: </b><br />
                                                                <asp:TextBox ID="txtGMRemarks" onfocus="focusthis(this);" onblur="blurthis(this);" runat="server" Width="90%" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> Name : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGMName" runat="server" ></asp:TextBox>
                                                            </td>
                                                            <td> </td>
                                                            <td></td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="4"> 
                                                                <br />
                                                               <b> MD’s remarks:</b><br />
                                                                <asp:TextBox ID="txtMDRemarks" onfocus="focusthis(this);" onblur="blurthis(this);" runat="server" Width="90%" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> Name : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMDName" runat="server" ></asp:TextBox>
                                                            </td>
                                                            <td> </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
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
                    </td>
               </tr>
         </table>  
    </div>
    </form>
</body>
</html>
