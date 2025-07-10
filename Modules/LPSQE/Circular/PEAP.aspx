<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PEAP.aspx.cs" Inherits="PEAP" Title="PEAP" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PEAP </title>
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
        .input_box
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
                <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" CssClass="text">
                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                        <col width="310px" />
                        <col />
                        <col width="200px" />
                        <tr>
                            <td>
                                <table style="font-size:9px; width:300px; float:left;margin-left:10px;text-align:left; color:White;" cellpadding="0" cellspacing="0">
		                            <tr>
		                                <td style="font-size:10px; color:White;"> Issued By:	SQM </td>
		                                <td style="font-size:10px; color:White;">   Issue Date: OCT 2011</td>
		                            </tr>
		                            <tr>
		                                <td style="font-size:10px; color:White;"> Approved By : MD</td>
		                                <td></td>
		                            </tr>
		                        </table>
                            </td>
                            <td style="font-size:12px; color:White; text-align:center;"> 
                                PERFORMANCE EVALUATION AND ASSESSMENT OF POTENTIAL  [ PEAP ]             
                            </td>
                            <td style="font-size:10px; color:White;vertical-align:top;"> 
                                Form No: G113    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     Version : 1.0 
                            </td>
                        </tr>
                    </table>
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
                            <td style="text-align:right;">VESSEL </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtVessel" runat="server" CssClass="input_box" ></asp:TextBox>
                            </td>
                            <td style="text-align:right;"> PEAP LEVEL </td>
                            <td style="text-align:left;">
                                <asp:DropDownList ID="ddlPeepType" runat="server" CssClass="input_box" OnSelectedIndexChanged="ddlPeepType_OnSelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Management" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Support" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Operation" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:right;"> OCCASION  </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">APPRAISEE FIRST NAME </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtAppraiseeFirstName" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">APPRAISEE LAST NAME </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtAppraiseeLastName" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">CREW #</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtCrewNo" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">RANK  </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtRank" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">APPRAISAL FROM  </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtAppraisalFrom" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">APPRAISAL TO </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtAppraislaTo" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                        </tr>
                             <tr>
                            <td style="text-align:right;">DATE JOINED VESSEL   </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtDateJoinedVessel" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">PERFORMANCE SCORE  </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtPerformanceScore" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">COMPETENCY SCORE </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtCompetencyScore" runat="server" CssClass="input_box"></asp:TextBox>
                            </td>
                        </tr>
                        
                    </table>
                    
                </td>
            </tr>
            <tr>
            <td>
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssCssClass="ajax__myTab" Height="600px" Width="100%">
                    
                    <%--Self Appraisal--%>
                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Self Appraisal">
                    <ContentTemplate>
                    <div style="overflow-x:hidden;overflow-y:scroll; width:100%; height:580px; float:left; padding:2px;" >
                        <table id="tblManagement" runat="server" cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>1. What are your specific accomplishments since the last performance review? :</b>
                                        <asp:TextBox ID="txtSpecificAccomplishments" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>2. What were the goals/standards that you fell short of and why? :</b>
                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>3. What kind of organizational support would you require to improve your performance? :</b>
                                        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>4. How does your present job utilize your capabilities?:</b>
                                        <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>5. What are the areas that you want more experience and/or training in for your current job? :</b>
                                        <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>6. What are the things that you have done to prepare yourself to take on higher responsibility? :</b>
                                        <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>7. What are the areas that you want more experience and/or training in for taking higher responsibility? :</b>
                                        <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>7. Any other comments or information that you would like to provide? :</b>
                                        <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>  
                        <%---------------%>
                        <table id="tblOperation" runat="server" cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>1. What are your specific accomplishments since the last performance review? :</b>
                                        <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>2. What kind of organizational support would you require to improve your performance? :</b>
                                        <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>3. How does your present job utilize your capabilities? :</b>
                                        <asp:TextBox ID="TextBox10" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>4.What are the areas that you want more experience and/or training in for your current job?:</b>
                                        <asp:TextBox ID="TextBox11" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>5. Any other comments or information that you would like to provide? :</b>
                                        <asp:TextBox ID="TextBox12" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>  
                        <%---------------%>
                        <table id="tblSupport" runat="server" cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>1. What are your specific accomplishments since the last performance review? :</b>
                                        <asp:TextBox ID="TextBox13" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>2. What kind of organizational support would you require to improve your performance? :</b>
                                        <asp:TextBox ID="TextBox14" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>3. What are the areas that you want more experience and/or training in for your current job? :</b>
                                        <asp:TextBox ID="TextBox15" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>4.Any other comments or information that you would like to provide?:</b>
                                        <asp:TextBox ID="TextBox16" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:180px; width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>  
                    </div>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Performance on the Job--%>
                    <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="Performance on the Job ">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="1" border="1" width="100%">
                            <col width="850px" />
                            <tr>
                                <td style="vertical-align:top;" >
                                    <table cellpadding="2" cellspacing="2" border="1" width="100%">
                                        <col  width="400px"/>
                                        <col  width="90px"/>
                                        <col  />
                                        <tr>
                                            <td>    
                                                Compliance with & enforcement of vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. 
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="ddlPer1" runat="server">
                                                    <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPer1" runat="server" TextMode="MultiLine" Width="340px" Height="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Compliance with & enforcement of Policies & Procedures from Owners, Charterers & Company’s Safety Management System. 
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPer2" runat="server">
                                                    <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPer2" runat="server" TextMode="MultiLine" Width="340px" Height="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Compliance with & enforcement of all Statutory, Flag State, Port State, National and International Regulations. 
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPer3" runat="server">
                                                    <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                    <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPer3" runat="server" TextMode="MultiLine" Width="340px" Height="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Total Score</td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Performance Score </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align:top;">
                                    <table width="450px" cellpadding="2" cellspacing="2" border="2" style="border-collapse:collapse; text-align:left; font-size:11px;" >
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
                        </table>
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Assessment of Competency--%>
                    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Assessment of Competency">
                    <ContentTemplate>
                        <div style="float:left; width:100%;">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <col width="900px" />
                                <col />
                               <tr>
                                <td>
                                    Assessment of Competency (To be completed by appraiser)
                                </td>
                               </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="1" cellspacing="1" border="1">
                                            <col />
                                            <col width="200px" />
                                             <tr>
                                                <td>Competency*</td>
                                                <td>Scale</td>
                                            </tr>
                                            <tr>
                                                <td>Organization Building</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss1" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Financial Acumen</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss2" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Drive and Resilience</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss3" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Change Management</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss4" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Leadership</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss5" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Strategic Perspective</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss6" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Decision Making</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss7" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Interpersonal Skills</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss8" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Differential Thinking</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAss9" runat="server">
                                                        <asp:ListItem Text="0" Value=" Select "> </asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"> </asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"> </asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Total Score</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Competencies Score= Total Score ÷ 9
                                                    (To be entered on page 1) 
                                             </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align:top;">
                                        <%------------ Labels ---------------%>
                                        Please select a scale between 1 and 4 from the definition given below.<br /><br />
				                        <table  cellpadding="2" cellspacing="2" border="0" style="border-collapse:collapse; font-size:11px;">
				                            
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
                                    </td>
                                </tr>
                            </table>
                        </div>
                    
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--Potential Assessment--%>
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Potential Assessment">
                    <ContentTemplate>
                    <div style="float:left; width:100%;">
                        <table cellpadding="2" cellspacing="2">
                            <tr>
                                <td>    
                                    A. What additional responsibilities is he capable of taking beyond the normal job functions?    
                                    <asp:TextBox ID="txtPotAdditionalResponsibilities" runat="server" Width="90%" TextMode="MultiLine" Height="40px"></asp:TextBox>
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
                                    <samp >
                                        Please specify reasons
                                        <asp:TextBox ID="txtLaterPromotionReasion" runat="server" Width="90%" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                    </samp>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    C. Identify training needs: (refer to the guidelines with Master for list of courses available)<br />
                                    i. For undertaking present job functions more effectively.
                                    <asp:TextBox ID="TextBox17" runat="server" Width="90%" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
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
