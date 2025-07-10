<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAppraisalData.aspx.cs" Inherits="CrewAppraisal_ViewAppraisalData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .lbl2 {font-family: Arial, Helvetica, sans-serif; font-size: 11pt; color: #003370; font-weight: bold; font-style:italic}
         body {font-size:11px; font-family:Times New Roman;Color:#324242; margin : 0 0 0 0 ; font-weight:bold;}
        .shadow {background-repeat:repeat-x; border:solid 1px #4371a5; background-image : url(menu_gra.jpg); background-position:bottom; }
        .lbl {font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #003370; font-weight: bold; text-transform:uppercase;}
        .lbl1 {font-family: Arial, Helvetica, sans-serif; font-size: 9pt; color: #003370; font-weight: bold}
        .val  {font-family: Arial, Helvetica, sans-serif; font-size: 8pt; color: #003370; font-weight: normal}
        .val1 {font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #003370; font-weight: normal}
        input {text-transform: uppercase;}
        .heading {font-size: 20px; font-family: Verdana, Arial, Helvetica, sans-serif; color: #003370; font-weight: bold; text-align:center;}
        .comment {font-family:Verdana, Arial, Helvetica, sans-serif; font-size:9px; color:#003370; font-weight:normal; font-weight:bold;}
        .lbl2 {font-family: Arial, Helvetica, sans-serif; font-size: 8pt; color: #FF3333; font-weight: bold;}
        input {border:solid 1px #4371a5;font-family:Arial;font-size:10px;}
        .SmallInput{border:solid 1px #4371a5;font-family:Arial;font-size:10px; width:100px;}
        select {border:solid 1px #4371a5;font-family:Arial;font-size:10px;}
        .Heading{font-size:18px;text-Align:left;color: Black; padding :3px; font-family:Verdana; background-color:#B7CEEC; }
        .header{color:White;text-align:center;text-align:center; background-color :#7AAAD2}
        textarea{border:solid 1px #4371a5;font-family:Arial;font-size:10px;height:50px;}
        td{vertical-align:top;}
        #tblAddRowTable tr td table tr td{vertical-align:top; text-align:center}
    </style>
    <style type="text/css">
        .Desc
        {
        	font-family:Arial
        	text-align:left; 
        	font-size:10px;
        	font-weight:bold; 
        	font-style:italic;  
        	color:Gray;
        }
       input[type=text]
        {
             font-family: Arial;
             font-size: 11px;
             color: #003399;
             background-color:#F7F8E0;  
             border:1px solid #9abcd7;
             text-transform:uppercase;
             width:160px;
        }
                
        select
        {
             font-family: Arial ;
             font-size: 11px;
             color: #003399;
             border:1px solid #9abcd7;
             width: 50px;
             background-color:#F7F8E0;
             text-transform:uppercase;  
        }
        
        textarea
        {
	         font-family:  Arial;
	         font-size: 11px;
	         color: #003399;
	         background-color:#F7F8E0;  
	         border:1px solid #9abcd7;
	         text-transform:uppercase;
        }
        body
        {
        	font-family:  Verdana;
	        font-size: 11px;
	        color: #342826;
        }
        .section    
        {
        	font-size:20px;
        	font-weight:bold; 
        	color:Black;
        }
        .secHeading
        {
        	font-size:18px;
        	font-weight:bold; 
        	color:White;
        	background-color:#C3862E;
        }
        .TblHeading
        {
        	font-size:13px;
        	font-weight:bold; 
        	background:#A9A9A9	;
        	color:White;
        }
        .hint
        {
        	text-align:left; 
        	font-size:10px;
        	font-weight:bold; 
        	font-style:italic;  
        	color:White;
        	text-transform :uppercase; 
        }    
        .SelBtn
        {
        	font-size:13px;
        	font-weight:bold; 
        	color:White;
        	background-color:#C3862E;
        }
        .Btn
        {
        	font-size:12px;
        	font-family:Verdana;
        	
        	color:Black;
        	background-color:#F0F0F0 ;
        	border:solid 1px Gray;
        }
        .lblData
        {
        	font-family:Arial;
        	text-align:left;
        	font-size:11px;
        	color:Gray;
        }
        .error
        {
        	font-family:Arial;
        	text-align:left;
        	font-size:11px;
        	color:#F62817;
        }
        .total
        {
        	background-color:#A8A8A8;
        	color:White;
        	font-weight:bold;
        	font-size:13px;
        	font-family:Verdana;
        }
        .lable
        {	font-size:12px;
        	font-family:Verdana;
        	font-weight:normal;
        }
        .Ques
        {	font-size:13px;
        	font-family:Verdana;
        	font-weight:bold;
        }
        .Red
        {
        	color:Black;
        	background-color:Red;
        }
        .Silver
        {
        	color:Black;
        	background-color:#C0C0C0;
        }
        .Gold
        {
        	color:Black;
        	background-color:#EAC117;
        }
    </style>    
    <script type="text/javascript">
        function ShowReport()
        {
            PeapID=document.getElementById('lblPeapID').value;
            var vsl=document.getElementById('hfdVessel').value;
            var location=document.getElementById('hfdLocation').value;
            
            window.open("../Reporting/AppraisalReport.aspx?PeapID=" + PeapID + "&VesselCode=" + vsl + "&Location=" + location,"Report","resizable=1,toolbar=0,scrollbars=1,top=70,left=220"); 
        }
        function setOptionValue()
        {
            var txt=document.getElementById("txtTraining");
            var txtName=document.getElementById("txtTrainingName");
            
            var ddl=document.getElementById("ddlTrainings");
            txt.value=ddl.options[ddl.selectedIndex].value;
            txtName.value=ddl.options[ddl.selectedIndex].text;
            
            if(ddl.selectedIndex==0)
            {
                alert ("Please Select Training.");
                ddl.focus();
                return false; 
            }
        }
        function OpenTrainingMatrix()
        {
            var CrewID=<%=CrewID %>;
            window.open('../CrewOperation/PopupTMatrix.aspx?c='+CrewID+'','','');
        }
    </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appraisal</title>
    <script type="text/javascript">
         function SetColor(val,type)
        {
            var btn1=document.getElementById('btnMGT1');
            var btn2=document.getElementById('btnMGT2');
            var btn3=document.getElementById('btnMGT3');
            var btn4=document.getElementById('btnMGT4');
            var btn5=document.getElementById('btnMGT5');
            if(val==1)
            {
            //#C3862E
                btn1.className="SelBtn";
                btn2.className="Btn";
                btn3.className="Btn";
                btn4.className="Btn";
                btn5.className="Btn";
            }
             else if(val==2)
            {
                
                btn1.className="Btn";
                btn2.className="SelBtn";
                btn3.className="Btn";
                btn4.className="Btn";
                btn5.className="Btn";
                
            }
            else if(val==3)
            {
                
                btn1.className="Btn";
                btn2.className="Btn";
                btn3.className="SelBtn";
                btn4.className="Btn";
                btn5.className="Btn";
                
            }
            else if(val==4)
            {
                btn1.className="Btn";
                btn2.className="Btn";
                btn3.className="Btn";
                btn4.className="SelBtn";
                btn5.className="Btn";
            }
            else if(val==5)
            {
                btn1.className="Btn";
                btn2.className="Btn";
                btn3.className="Btn";
                btn4.className="Btn";
                btn5.className="SelBtn";
            }
        }
         function GetManSelecteButtonID()
        {
            var Btn1= document.getElementById('btnMGT1');
            var Btn2= document.getElementById('btnMGT2');
            var Btn3= document.getElementById('btnMGT3');
            var Btn4= document.getElementById('btnMGT4');
            var Btn5= document.getElementById('btnMGT5');
            
            var SelBtnID;
            if(Btn1.getAttribute('class')=='SelBtn')
            {
                SelBtnID=Btn1.id;
            }
            else if(Btn2.getAttribute('class')=='SelBtn')
            {
                SelBtnID=Btn2.id;
            }
            else if(Btn3.getAttribute('class')=='SelBtn')
            {
                SelBtnID=Btn3.id;
            }
            else if(Btn4.getAttribute('class')=='SelBtn')
            {
                SelBtnID=Btn4.id;
            }
            else if(Btn5.getAttribute('class')=='SelBtn')
            {
                SelBtnID=Btn5.id;
            }
            return SelBtnID;
        }
        function ShowMGTDiv(val,ClickedBtn)
        {       
            
            if(val==1)
            {
                document.getElementById('trPerformance').style.display='block';
                document.getElementById('trAssesment').style.display='none';
                document.getElementById('trPotensial').style.display='none';
                document.getElementById('trAssessment').style.display='none';
                document.getElementById('trOfficeComment').style.display='none';
                document.getElementById('trTraining').style.display='none';
                
                SetColor(1,'M');
            }
            else if(val==2)
            {
               document.getElementById('trPerformance').style.display='none';
                document.getElementById('trAssesment').style.display='block';
                document.getElementById('trPotensial').style.display='none';
                document.getElementById('trAssessment').style.display='none';
                document.getElementById('trOfficeComment').style.display='none';
                document.getElementById('trTraining').style.display='none';
                SetColor(2,'M');
            }
            else if(val==3)
            {
               document.getElementById('trPerformance').style.display='none';
                document.getElementById('trAssesment').style.display='none';
                document.getElementById('trPotensial').style.display='block';
                document.getElementById('trAssessment').style.display='none';
                document.getElementById('trOfficeComment').style.display='none';
                document.getElementById('trTraining').style.display='none';
                SetColor(3,'M');
            }
            else if(val==4)
            {
                document.getElementById('trPerformance').style.display='none';
                document.getElementById('trAssesment').style.display='none';
                document.getElementById('trPotensial').style.display='none';
                document.getElementById('trAssessment').style.display='block';
                document.getElementById('trOfficeComment').style.display='none';
                document.getElementById('trTraining').style.display='none';
                SetColor(4,'M');
            }
            else if(val==5)
            {
            
                document.getElementById('trOfficeComment').style.display='block';
                document.getElementById('trTraining').style.display='block';
                document.getElementById('trPerformance').style.display='none';
                document.getElementById('trAssesment').style.display='none';
                document.getElementById('trPotensial').style.display='none';
                document.getElementById('trAssessment').style.display='none';
                SetColor(5,'M');
            }
        }
        
        function ShowTrainignSection()
        {
           var x=document.getElementById('ddlTrainingRequired');
           var val=x.options[x.selectedIndex].value;
           
           if(val=='yes')
            document.getElementById('tblTraining').style.display='block';
           else
            document.getElementById('tblTraining').style.display='none';
            
        }
    </script>
    <%--Show labels--%>
    <script type="text/javascript">
        function Performancelabel()
        {
            //management
            var Peaplevel= document.getElementById('hfPeaplevel').value;
            var PerScale3Row= document.getElementById('lblPerScale3Row');
            
            var PerScale1= document.getElementById('lblPerScale1txt');
            var PerScale2= document.getElementById('lblPerScale2txt');
            var PerScale3= document.getElementById('lblPerScale3txt');
            
            
            //Assessment
            
            var trAssScale8= document.getElementById('trAssScale8');
            
            var tdAssScale1= document.getElementById('tdAssScale1');
            var tdAssScale2= document.getElementById('tdAssScale2');
            var tdAssScale3= document.getElementById('tdAssScale3');
            var tdAssScale4= document.getElementById('tdAssScale4');
            var tdAssScale5= document.getElementById('tdAssScale5');
            var tdAssScale6= document.getElementById('tdAssScale6');
            var tdAssScale7= document.getElementById('tdAssScale7');
            var tdAssScale8= document.getElementById('tdAssScale8');
            
            if(Peaplevel=='MANAGEMENT')
            {
                PerScale3Row.style.display='block';
                PerScale1.innerHTML="Compliance with & enforcement of vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. ";
                PerScale2.innerHTML="Compliance with & enforcement of Policies & Procedures from Owners, Charterers & Company’s Safety Management System. ";
                PerScale3.innerHTML="Compliance with & enforcement of all Statutory, Flag State, Port State, National and International Regulations. ";
                
                trAssScale8.style.display='block';
                tdAssScale1="Decision Making";
                tdAssScale2="Process Orientation";
                tdAssScale3="Leadership";
                tdAssScale4="Strategy Execution";
                tdAssScale5="Technical Know-How";
                tdAssScale6="Managing Pressure and Stress";
                tdAssScale7="Initiative";
                tdAssScale8="Team Management";
            }
            else if(Peaplevel=='SUPPORT')
            {
                PerScale3Row.style.display='none';
                PerScale1.innerHTML="Compliance with Company’s Policies and Procedures including but not limited to Safety and Protection of environment. ";
                PerScale2.innerHTML="Understanding of & Compliance with Company’s Safety Management System. ";
                
                trAssScale8.style.display='none';
                tdAssScale1="Team Management";
                tdAssScale2="Decision Making";
                tdAssScale3="Drive and Resilience";
                tdAssScale4="Interpersonal Skills ";
                tdAssScale5="Strategy Execution";
                tdAssScale6="Technical Know-how";
                tdAssScale7="Initiative";
                tdAssScale8="";
            }
            else if(Peaplevel=='OPERATION')
            {
                PerScale3Row.style.display='block';
                PerScale1.innerHTML="Understanding of & Compliance with vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. ";
                PerScale2.innerHTML="Understanding of & Compliance with Policies & Procedures of Owners, Charterers & Company’s Safety Management System. ";
                PerScale3.innerHTML="Understanding of & Compliance with all Statutory, Flag State, Port State, National and International Regulations. ";
                
                trAssScale8.style.display='none';
                tdAssScale1="Team Management";
                tdAssScale2="Decision Making";
                tdAssScale3="Drive and Resilience";
                tdAssScale4="Interpersonal Skills ";
                tdAssScale5="Strategy Execution";
                tdAssScale6="Technical Know-how";
                tdAssScale7="Initiative";
                tdAssScale8="";
            }
            
             
        }
    </script>
</head>
<body onload="Performancelabel()">
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
        <table cellpadding="0" cellspacing="0" width="100%" style="border:solid 1px Gray;" >
            <tr>
		        <td colspan="1" class="Heading" style=" background-color :#7AAAD2;color:White; height:25px;">
		            <input id="txtPeapType" type="text"  style="display:none;" />
		                <table style="font-size:11px; width:270px; float:left;margin-left:10px;text-align:left;">
		                    <tr>
		                        <td> Issued By:	SQM </td>
		                        <td>  Issue Date: OCT 2011</td>
		                    </tr>
		                    <tr>
		                        <td>Approved By : MD</td>
		                        <td></td>
		                    </tr>
		                </table>
		            
		            PERFORMANCE EVALUATION AND ASSESSMENT OF POTENTIAL
		            <asp:HiddenField ID="hfPeaplevel" runat="server"  />
		            <asp:HiddenField ID="hfPeapID" runat="server"  />
		            <asp:HiddenField ID="hfOccasion" runat="server"  />
		        </td>
		    </tr>
		    <tr>
		        <td style="text-align:right;background-color:Gray;padding:3px;">
		            <table style="font-size:12px; width:99%; float:left;margin-left:10px;text-align:left;color:White;" border="0">
		                    <tr>
		                        <td >Form No: G113 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Version : 1.0 </td>
		                        <td style="width:65px;">
		                        
		                        </td>
		                        <td style="width:80px;">
		                            <asp:TextBox ID="lblPeapID" runat="server" style="display:none;"  ></asp:TextBox>
		                        </td>
		                    </tr>
		                </table>
                    
		            
		        </td>
		    </tr>
        
        <%--<tr>
            <td>
                <fieldset id="fMissMatch" runat="server"  style="width:98%; padding:5px;">
                <legend>MissMatch</legend>
                     <table cellpadding="2" cellspacing="0" width="100%" style="text-align:center;">
                        <col width="100px" />
                        <col width="170px" />
                        <col width="170px" />
                        <col />
                        <tr >
                            <td>Crew#</td>
                            <td>First Name</td>
                            <td>Last Name</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCrewNoforSearch" runat="server"  Width="80px" AutoPostBack="true" OnTextChanged="txtCrewNoforSearch_OnTextChanged" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFnameMisMatch" runat="server"  ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLnameMisMatch" runat="server"  ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblmsgMissMatch" runat="server" CssClass="error" style="float:left;" ></asp:Label>
                                <asp:Button ID="btnUpdMissMatch" runat="server" OnClick="btnUpdMissMatch_OnClick" Text="Update" CssClass="btn" style="float:right;" OnClientClick="return confirm('Are u sure to update ?')" />
                            </td>
                        </tr>
                    </table>
            </fieldset>
            </td>
        </tr>--%>
        
        <tr>
            <td>
                  <table width="100%" cellpadding="3" cellspacing="3" border="0" >
                        <col width="150px" />
                        <col width="230px" />
                        <col width="180px" />
                        <col width="230px" />
                        <col width="170px" />
                        <col />
				            <tr>
				                <td>
									Vessel 
								</td>
								<td>
								    <asp:Label ID="lblVessel" runat="server" CssClass="lblData" ></asp:Label>	
                                    <asp:HiddenField ID="hfdVessel" runat="server" ></asp:HiddenField>	
                                    <asp:HiddenField ID="hfdLocation" runat="server"></asp:HiddenField>	
								</td>
				                <td>PEAP LEVEL</td>
				                <td>
				                    <asp:Label ID="lblPeapLevel" runat="server" CssClass="lblData"></asp:Label>
				                </td>
				                <td>
									OCCASION 

								</td>
								<td>
									<asp:Label ID="lblOccation" runat="server" CssClass="lblData"></asp:Label>
								</td>
				                
				            </tr>
					        <tr>
								<td>
									 FIRST NAME 
								</td>
								<td>
									<asp:Label ID="lblFirstName" runat="server" CssClass="lblData"></asp:Label>
								</td>
								<td>LAST NAME</td>
								<td>
								    <asp:Label ID="lblLastName" runat="server" CssClass="lblData"></asp:Label>
								</td>
								<td>CREW #</td>
								<td>
								    <asp:Label ID="lblCrewNo" runat="server" CssClass="lblData"></asp:Label>
								</td>
							</tr>
							<tr>
								<td>
									RANK
								</td>
								<td>
									<asp:Label ID="lblRank" runat="server" CssClass="lblData"></asp:Label>
								</td>
								<td>APPRAISAL FROM </td>
							    <td>
							        <asp:Label ID="lblAppFrom" runat="server" CssClass="lblData"></asp:Label>
							        
							    </td>
							    <td>APPRAISAL TO </td>
							    <td>
							        <asp:Label ID="lblAppTO" runat="server" CssClass="lblData"></asp:Label>
							    </td>							    
							</tr>
							<tr>
								
								<td>
									DATE JOINED VESSEL 

								</td>
								<td>
									<asp:Label ID="lblDtJoinedVessel" runat="server" CssClass="lblData"></asp:Label>
								</td>
								<td>
									Avg. PERFORMANCE SCORE
								</td>
								<td ID="tdPerformanceScore" runat="server" >
									<asp:Label ID="lblPerformanceScore" runat="server" ></asp:Label>
								</td>
								<td >
									Avg. COMPETENCY SCORE
								</td>
								<td ID="tdCompetencyScore" runat="server" >
								    <asp:Label ID="lblCompetencyScore" runat="server" ></asp:Label>
								</td>
							</tr>
						</table>            
            </td>
        </tr>   
        <%--Performance--%> 
        <tr>
            <td>
               <table cellpadding="2" cellspacing="0" width="150px">
                <tr>
		            <td>
		                <input type="button" id="btnMGT1" class="SelBtn" value="Performance on the Job " onclick="ShowMGTDiv(1,this.id);" /> <!--SetColor(1,'M')-->
		            </td>
		            <td>
		                <input type="button" id="btnMGT2" class="Btn" value="Assessment of Competency" onclick="ShowMGTDiv(2,this.id);" /> <!--SetColor(2,'M')-->
		            </td>
		            <td>
		                <input type="button" id="btnMGT3" class="Btn" value="Potential Assessment " onclick="ShowMGTDiv(3,this.id);"/>  <!--SetColor(3,'M')-->
		            </td>
		            <td>
		                <input type="button" id="btnMGT4" class="Btn" value="Remarks" onclick="ShowMGTDiv(4,this.id);"/> <!--SetColor(4,'M')-->
		            </td>
		            <td>
		                <input type="button" id="btnMGT5" class="Btn" value="Office Comment" onclick="ShowMGTDiv(5,this.id);"/> <!--SetColor(4,'M')-->
		            </td>
		            <td>
		                <input type="button" id="btnPring" class="Btn" value="Print" onclick="ShowReport()" />
		            </td>
		        </tr>
               </table>
            </td>
        </tr>
        <tr id="trPerformance">
            <td>
                <table width="100%" cellpadding="4" cellspacing="2" border="2" style="border-collapse:collapse; text-align:left;" >
                            <col width="350px"/>
                            <col width="70px"/>
                            <col />
                                <tr>
                                    <td class="Heading" style=" background-color :#7AAAD2;color:White;text-align:left" colspan="3">
                                        Performance on the Job
                                    </td>
                                </tr>   
                                <tr class="TblHeading">
                                    <td style="text-align:center;">Job Responsibility</td>
                                    <td style="text-align:center;">Scale</td>
                                    <td style="text-align:center;">Actual Example/s to support the assessment</td>
                                </tr>
                                <tr >
                                    <td id="lblPerScale1txt">
                                        Compliance with & enforcement of vessel’s KPIs including 
                                        but not limited to Zero Spill, Zero Incidents and   Zero off-hire.
                                        
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPerScale1" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        <div style="width:100%; min-height:70px;vertical-align:top;text-align:left;">
                                            <asp:Label ID="lblPerAss1" runat="server" CssClass="lable" ></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr >
                                    <td id="lblPerScale2txt">
                                        Compliance with & enforcement of Policies & Procedures 
                                        from Owners, Charterers & Company’s Safety Management 
                                        System. 
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPerScale2" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        <div style="width:100%; min-height:70px;vertical-align:top;text-align:left;">
                                            <asp:Label ID="lblPerAss2" runat="server" CssClass="lable" ></asp:Label>
                                         </div>
                                    </td>
                                </tr>
                                <tr id="lblPerScale3Row">
                                    <td id="lblPerScale3txt">
                                        Compliance with & enforcement of all Statutory, Flag State,
                                        Port State, National and International Regulations.
                                         
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPerScale3" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        <div style="width:100%; min-height:70px;vertical-align:top;text-align:left;">
                                            <asp:Label ID="lblPerAss3" runat="server" CssClass="lable"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="total">
                                    <td style="text-align:right;">Total Score</td>
                                    <td >
                                        <asp:Label ID="lblTotPer" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr class="total">
                                    <td style="text-align:right;">
                                        Avg. Performance Score 
                                    </td>
                                    <td >
                                        <asp:Label ID="lblPerPerformanceScore" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                         <span class="hint"> 
                                            
                                            <%--Performance Score =  Total Score÷ 3--%>
                                         </span>
                                    </td>
                                </tr>
                            </table>
            </td>
        </tr>   
        <%--Assesment--%> 
        <tr id="trAssesment" style="display:none;" > 
            <td>
                <table width="100%" cellspacing="4" cellpadding="2" border="1" style="border-collapse:collapse;text-align:left;">
                <col width="350px" />
                <col />
                <tr>
                    <td class="Heading" style=" background-color :#7AAAD2;color:White;text-align:left" colspan="2">
                        Assessment of Competency
                    </td>
                </tr>  
                <tr>
                    <td colspan="2">
                        <table width="600px" cellspacing="4" cellpadding="4" border="1" style="border-collapse:collapse;text-align:left;">
                              <tr class="TblHeading">
                                <td>Competency</td>
                                <td>Scale</td>
                            </tr>
                            <tr id="trAssScale8">
                                <td id="tdAssScale8">Team Management</td>
                                <td>
                                    <asp:Label ID="lblAssScale8" runat="server" CssClass="lable" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdAssScale1">Decision Making</td>
                                <td>
                                    <asp:Label ID="lblAssScale1" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdAssScale2">Process Orientation</td>
                                <td>
                                    <asp:Label ID="lblAssScale2" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdAssScale3">Leadership</td>
                                <td>
                                    <asp:Label ID="lblAssScale3" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr >
                                <td id="tdAssScale4">Strategy Execution</td>
                                <td>
                                    <asp:Label ID="lblAssScale4" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdAssScale5">Technical Know-How</td>
                                <td>
                                    <asp:Label ID="lblAssScale5" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdAssScale6">Managing Pressure and Stress</td>
                                <td>
                                    <asp:Label ID="lblAssScale6" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdAssScale7">Initiative</td>
                                <td>
                                    <asp:Label ID="lblAssScale7" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr class="total">
                                <td style="text-align:right;">Total Score</td>
                                <td>
                                    <asp:Label ID="lblTotAss" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr class="total">
                                <td style="text-align:right;">
                                Avg. Competency Score
                                </td>
                                <td style="text-align:left;">
                                    <span >
                                        <asp:Label ID="lblAssCompetencyScore" runat="server" ></asp:Label>
                                    </span>
                                    <span class="hint" style="margin-left:100px;" > 
                                        <%--Competency Score= Total Score ÷ 8--%>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td>
        </tr>  
         <%--Potensial--%> 
        <tr id="trPotensial" style="display:none;">
            <td>
                <table width="100%" cellpadding="4" cellspacing="3">
                <tr>
                    <td class="Heading" style=" background-color :#7AAAD2;color:White;text-align:left" >
                        Potential Assessment and Identification of Training & Development Needs
                    </td>
                </tr>
                <tr>
                    <td class="Ques">
                        A. What additional responsibilities is he capable of taking beyond the normal job functions?<br />
                        <div id="divPotSecA1" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px; min-height:60px;  padding:3px;" class="lable"></div>
                    </td>
                </tr>
                <tr>
                    <td class="Ques">
                        B. Is he ready for promotion? <br />
                        <asp:Label ID="lblPotReadyForPromotion" runat="server" style="margin-left:15px;" CssClass="lable"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Ques">
                        Please specify reasons<br />
                        <div id="divPotSecB1" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px; min-height:60px; padding:3px;" class="lable"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="Ques">
                        C. Identify training needs: <br /><br />

              
                        i.	For undertaking present job functions more effectively.<br />
                        <div id="divPotSecC1" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px;min-height:60px; padding:3px;" class="lable"></div>


                        ii.	For undertaking higher responsibilities.<br />
                        <div id="divPotSecC2" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px; min-height:60px;" class="lable"></div>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <%--Appraisee Remarks:--%>
        <tr id="trAssessment" style="display:none;">
            <td>
                <table width="100%" border="1">
                <tr>
                    <td>
                        <table width="100%" cellpadding="3" cellspacing="3" border="0" style="float:left;border:solid 0px gray;">
                            <tr>
                                <td class="Heading" style=" background-color :#7AAAD2;color:White;text-align:left" colspan="4">
                                    Remarks:
                                </td>
                            </tr>
                            <tr>
                                <td class="TblHeading" colspan="4">
                                    Appraisee Remarks:
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div id="divAppAppraiseeRem" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px; padding:3px;min-height:60px;" class="lable"></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;" >Name: </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiseeName" runat="server" CssClass="lable"></asp:Label>
                                </td >
                                <td style="text-align:right;"> Crew#:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiseeCrewNo" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">Rank:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiseeRank" runat="server" CssClass="lable"></asp:Label>
                                </td>
                                <td style="text-align:right;">Date: </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiseeDate" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr class="TblHeading">
                                <td colspan="4">Appraiser Remarks:</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div id="divAppAppraiserRem" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px; min-height:60px;" class="lable"></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">Name    :</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiserName" runat="server" CssClass="lable"></asp:Label>
                                </td>
                                <%--<td style="text-align:right;">Crew#:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiserCrewNo" runat="server" CssClass="lable"></asp:Label>
                                </td>--%>
                                <td style="text-align:right;">Rank:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiserRank" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">Date:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppAppraiserDate" runat="server" CssClass="lable"></asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr class="TblHeading">
                                <td colspan="4">Master’s Remarks:<br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div id="divAppMasterRem" runat="server" style="background-color:#F5F5F5; margin-left:15px; width:1230px;min-height:60px; " class="lable"></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">Name:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppMasterName" runat="server" CssClass="lable"></asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">Crew #:</td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppMasterCrewNo" runat="server" CssClass="lable"></asp:Label>
                                </td>
                                <td style="text-align:right;">Date: </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblAppMasterDate" runat="server" CssClass="lable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align:right;">
                                    
                                </td>
                            </tr>
                        </table>            
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr id="trOfficeComment" style="display:none;" >   
            <td>
                <table width="100%" cellpadding="2" cellspacing="2" border="1">
                <col width="510px" />
                    <tr>
                        <td class="Heading" style=" background-color :#7AAAD2;color:White;text-align:left" colspan="4">
                            Office Comments 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="2" cellspacing="2" width="500px" border="0">
                            <col width="70px" />
                            <col width="150px" />
                            <col width="70px" />
                            <col  />
                           <%-- <tr>
                                <td class="Heading" style=" background-color :#7AAAD2;color:White;text-align:left" colspan="4">
                                    Office Comment 
                                    <span style="float:right;font-size:12px">
                                        Training Required
                                        <asp:DropDownList ID="ddlTrainingRequired" runat="server" Width="50px" onchange="ShowTrainignSection()" > 
                                            <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="yes" Value="yes"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="4">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:TextBox ID="txtOfficeComment" runat="server" style="width:500px;height:400px;margin-left:15px;" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trreviwedBySec" runat="server" visible="false">
                                <td>
                                       Reviewed by
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReviewedby" runat="server" ReadOnly="true" ></asp:TextBox>
                                </td>
                                <td>Reviewed Date</td>
                                <td>
                                    <asp:TextBox ID="txtReviewedDate" runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="imgReviewedDate" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                       PopupButtonID="imgReviewedDate" PopupPosition="TopLeft" TargetControlID="txtReviewedDate"></ajaxToolkit:CalendarExtender>
                                </td>
                                
                            </tr>
                         </table>
                        </td>
                        <td>
                          <table  width="100%" style="background-color:#7AAAD2;color:White;text-align:left;" >
                          <tr>
                            <td class="heading">
                                <span style="float:left;font-size:12px;color:White;">
                                Training Required
                                    <asp:DropDownList ID="ddlTrainingRequired" runat="server" Width="50px" onchange="ShowTrainignSection()" style="margin-left:5px;" > 
                                                <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                                                <asp:ListItem Text="yes" Value="yes"></asp:ListItem>
                                            </asp:DropDownList>
                                </span>
                                <input type="button" value="View Training Matrix" onclick="OpenTrainingMatrix();" style="float:right;" />
                            </td>
                          </tr>
                          </table>
                          
                          
                            <table id="tblTraining" runat="server" cellpadding="2" cellspacing="2" width="100%" border="0" style="display:none;">
                            <%--<col width="130px" />
                            <col width="160px" />
                            <col width="100px" />
                            <col />--%>
                           <tr>
                               <td style="width:220px">Sire Chapter :</td> 
                               <td style="width:280px" colspan="2">Training Name :</td>
                               <td >Due&nbsp; Date :</td>
                               
                            </tr>
                           <tr>
                                <td colspan="4">
                                <asp:UpdatePanel ID="UP1" runat="server" >
                                <ContentTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                     <tr>
                                       <td style="text-align: left">
                                            <asp:DropDownList ID="ddlSireChap1" runat="server" Width="250px" CssClass="required_box" AutoPostBack="true" OnSelectedIndexChanged="ddlSireChap1_SelectedIndexChanged" ></asp:DropDownList>
                                        </td>
                                       <td style="text-align: left" colspan="2">
                                           <asp:Literal runat="server" ID="litSelectTr"></asp:Literal>
                                            <asp:TextBox runat="server" ID="txtTraining" style="display:none"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtTrainingName" style="display:none"></asp:TextBox>
                                       </td>
                                       <td>
                                           <asp:TextBox ID="txt_DueDate" runat="server" CssClass="input_box" MaxLength="20"
                                               TabIndex="3" Width="70px"></asp:TextBox>
                                           <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                       </td>
                               
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:right;">
                                            <asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text="Add" Width="50px" TabIndex="9" OnClick="btn_Save_PlanTraining_Click" OnClientClick="return setOptionValue();"  style="margin-right:10px;" Visible="false"/>
                                        </td>
                                    </tr>
                                </table>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton5" PopupPosition="Left" TargetControlID="txt_DueDate" ></ajaxToolkit:CalendarExtender>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                           </tr>
                           <tr>
                                <td colspan="4">
                                <div style="height:330px; width:700px; border:solid 1px Gray;overflow-x:hidden;overflow-y:scroll;">
                                <asp:UpdatePanel ID="updata" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <table cellpadding="2" cellspacing="2" width="100%" border="1" style="border-collapse:collapse">
                                    <col width="130px" />
                                    <col width="400px" />
                                    <col width="120px" />
                                    <asp:Repeater ID="rptTraining" runat="server" OnItemDataBound="rptTraining_OnItemDataBound" >
                                        <ItemTemplate>
                                            <tr class="lable">    
                                                <td style="font-size:10px;color:Blue;">
                                                    <%#Eval("TrainingTypetxt")%>
                                                    <asp:HiddenField ID="hfTrainingID" runat="server" Value='<%#Eval("TrainingID")%>' />
                                                    <asp:HiddenField ID="hfTodate" runat="server" Value='<%#Eval("Todatetxt")%>' />
                                                </td>
                                                <td style="margin:2px;"><%#Eval("TrainingIDtxt")%></td>
                                                <td><%#Eval("Todatetxt")%></td>
                                                <td><asp:ImageButton ID="imgDel" runat="server" OnClick="imgDel_OnClick" ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClientClick="return confirm('Are you sure to delete?')"  /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                 </div>
                                </td>
                                
                           </tr>
                           <tr>
                            <td colspan="4">
                                <%--<input ID="btnupdateCrew" runat="server" type="button" value="Update Crew"  onclick="UpdateCrewDetails()" />--%>
                                <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" />
                                <asp:Label ID="lblmsg" runat="server"  CssClass="error"></asp:Label>--%>
                           </td>
                           </tr>
                        </table>
                            
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                        <span class="hint" style="color:Gray; float:left;">This is an electronic form and does not require signature.</span>
                        <asp:UpdatePanel ID="UPSave" runat="server" >
                            <ContentTemplate>
                                <asp:Label ID="lblmsg" runat="server"  CssClass="error"></asp:Label>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" style="margin-right:15px;" Visible="false" />
                                </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trTraining" style="display:none;" > 
           
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
