<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPeap.aspx.cs" Inherits="CrewAppraisal_ViewPeap" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <script type="text/javascript">
        function ShowReport()
        {
            PeapID=document.getElementById('lblPeapID').value;
            window.open("../Reporting/AppraisalReport.aspx?PeapID="+PeapID,"Report","resizable=1,toolbar=0,scrollbars=1,top=70,left=220"); 
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
         function SetSelectedBtnColor(val,type)
        {
            var btn1=document.getElementById('btnMGT1');
            var btn2=document.getElementById('btnMGT2');
            var btn3=document.getElementById('btnMGT3');
            var btn4=document.getElementById('btnMGT4');
            var btn5=document.getElementById('btnMGT5');
            var btn6=document.getElementById('btnMGT6');

            btn1.className="Btn";
            btn2.className="Btn";
            btn3.className="Btn";
            btn4.className="Btn";
            btn5.className="Btn";
            btn6.className="Btn";

                  if(val==1)btn1.className="SelBtn";
             else if(val==2)btn2.className="SelBtn";            
             else if(val==3)btn3.className="SelBtn";
             else if(val==4)btn4.className="SelBtn";
             else if(val==5)btn5.className="SelBtn";
             else if(val==6)btn6.className="SelBtn";
            
        }
        function ShowMGTDiv(val,ClickedBtn)
        {       
            document.getElementById('trPerformance').style.display='none';
            document.getElementById('trAssesment').style.display='none';
            document.getElementById('trPotensial').style.display='none';
            document.getElementById('trRemarks').style.display='none';
            document.getElementById('trOfficeComment').style.display='none';
            document.getElementById('trTraining').style.display='none';

            if(val==1)
            {
                document.getElementById('trPerformance').style.display='block';
                SetSelectedBtnColor(1,'M');
            }
            else if(val==2)
            {
                document.getElementById('trAssesment').style.display='block';               
                SetSelectedBtnColor(2,'M');
            }
            else if(val==3)
            {
                document.getElementById('trPotensial').style.display='block';               
                SetSelectedBtnColor(3,'M');
            }
            else if(val==4)
            {
                document.getElementById('trRemarks').style.display='block';               
                SetSelectedBtnColor(4,'M');
            }            
            else if(val==5)
            {
                document.getElementById('trOfficeComment').style.display='block';
                SetSelectedBtnColor(5,'M');
            }
            else if(val==6)
            {
                document.getElementById('trTraining').style.display='block';                
                SetSelectedBtnColor(6,'M');
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
    
    <script src="../JS/jquery.min.js"></script>
    <script src="../JS/jquery.datetimepicker.js"></script>
    <link href="../Styles/jquery.datetimepicker.css" rel="stylesheet" type="text/css"  />
    <script>
        $(document).ready(function(){            
            $(".CalPerScale").change(function(){
                var FinAmount=0;
                $(".CalPerScale").each(function(){
                    var d=$(this).val();
                    d=parseFloat(d);
                    if(isNaN(d))d=0;
                    FinAmount=FinAmount+d;
                });
                $(".TotPerScale").html(FinAmount);
                $(".AvgPerScale").html((FinAmount/3).toPrecision(2));
                $("#hfPerformanceScore").val((FinAmount/3).toPrecision(2));
            });
            

            $(".CalAssScale").change(function(){
                var FinAmount=0;
                $(".CalAssScale").each(function(){
                    var d=$(this).val();
                    d=parseFloat(d);
                    if(isNaN(d))d=0;
                    FinAmount=FinAmount+d;
                });
                $(".TotAssScale").html(FinAmount);
                $(".AvgAssScale").html((FinAmount/8).toPrecision(2));
                $("#hfCompetencyScore").val((FinAmount/8).toPrecision(2));
            });

            $(".menu li").click(function(){
                $(".menu li").removeClass("active");
                $(this).addClass("active");
            });
        });
    </script>
    <style type="text/css">
        body{
            font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            font-size:13px;
            margin:0px;
        }

        .bordered tr td
        {
            border:solid 1px #e2e2e2;
        }

        .menu
        {
            padding:0px;
            margin:0px;
            display:block;
            overflow:auto;
        }
        .nav
        {
            vertical-align:bottom;
            margin-top:20px;
        }
        .menu li
        {
            padding:5px 10px 5px 10px;
            margin-left:-3px;
            float:left;
            background-color:#8c8c8c;
            color:white;
            text-align:center;
            list-style-type:none;
            margin-left:1px;
        }
        .menu li:hover
        {
            background-color:#929292;cursor:pointer;
        }
        .menu li.active
        {
            color:white;
            background-color:#000000;
        }
        .headercell
        {
            font-weight:bold;
            background-color:#ffe2bb;
            padding:5px;
        }
        .CalPerScale,.CalAssScale
        {
            width:80px;
            text-align:center;
        }
        .btn
        {
            border:solid 0px white;
            background-color:#0094ff;
            color:white;
            padding:5px;
            min-width:100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
        <div style=" background-color :#3c7cb1;color:White;text-align:center; padding:8px; font-size:18px;">
            <span style="float:left; font-size:13px; color:white;">Form No: G113  &nbsp; / &nbsp; Version : 1.0 </span>
		            <input id="txtPeapType" type="text"  style="display:none;" />	                
		            
		            PERFORMANCE EVALUATION AND ASSESSMENT OF POTENTIAL
                   
                    <asp:TextBox ID="lblPeapID" runat="server" style="display:none;"  ></asp:TextBox>
		            <asp:HiddenField ID="hfPeaplevel" runat="server"  />
		            <asp:HiddenField ID="hfPeapID" runat="server"  />
		            <asp:HiddenField ID="hfOccasion" runat="server"  />
            

		     </div>
        <table width="100%" cellpadding="4" cellspacing="0" border="0" class="bordered" style="border-collapse:collapse" >
                <col width="200px" style="font-weight:bold" />
                <col />
                <col width="200px" style="font-weight:bold" />
                <col />
                <col width="200px" style="font-weight:bold" />
                <col />
				    <tr>
				        <td>Vessel :</td>
					    <td><asp:Label ID="lblVessel" runat="server" CssClass="lblData" ></asp:Label></td>
				        <td>Peap Level :</td>
				        <td><asp:Label ID="lblPeapLevel" runat="server" CssClass="lblData"></asp:Label></td>
				        <td>Occasion :</td>
					    <td><asp:Label ID="lblOccation" runat="server" CssClass="lblData"></asp:Label></td>
				    </tr>
				    <tr>
					    <td>Crew Name :</td>
					    <td>
						    <asp:Label ID="lblFirstName" runat="server" CssClass="lblData"></asp:Label>
						    <asp:Label ID="lblLastName" runat="server" CssClass="lblData"></asp:Label>
					    </td>
					    <td>Crew #</td>
					    <td><asp:Label ID="lblCrewNo" runat="server" CssClass="lblData"></asp:Label></td>
					    <td>Date Joined Company :</td>
					    <td><asp:Label ID="lblDateJoinedCompany" runat="server"  CssClass="lblData" style="width:80px;"></asp:Label></td>
				    </tr>
				    <tr>
					    <td>Rank :</td>
					    <td>
						    <asp:Label ID="lblRank" runat="server" CssClass="lblData"></asp:Label>
                            <asp:HiddenField ID="hfRankID" runat="server" />
					    </td>
					    <td>Appraisal From :</td>
					    <td><asp:Label ID="lblAppFrom" runat="server" CssClass="lblData" style="width:80px;"></asp:Label></td>
					    <td>Appraisal To : </td>
					    <td>
						    <asp:Label ID="lblAppTO" runat="server" CssClass="lblData" style="width:80px;"></asp:Label>
					    </td>							    
				    </tr>
				    <tr>
								
					    <td>
						    Date Joined Vessel :
					    </td>
					    <td>
						    <asp:Label ID="lblDtJoinedVessel" runat="server"   CssClass="lblData" style="width:80px;"></asp:Label>
					    </td>
					    <td>
						    Avg. Perf. Score :
					    </td>
					    <td ID="tdPerformanceScore" runat="server" >
						    <asp:Label ID="lblPerformanceScore" runat="server" CssClass="lblData AvgPerScale"></asp:Label>
                            <asp:HiddenField ID="hfPerformanceScore" runat="server" Value="0" />
					    </td>
					    <td >
						    Avg. Comp. Score :
					    </td>
					    <td ID="tdCompetencyScore" runat="server" >
						    <asp:Label ID="lblCompetencyScore" runat="server" CssClass="lblData AvgAssScale"></asp:Label>
                            <asp:HiddenField ID="hfCompetencyScore" runat="server" Value="0" />
					    </td>
				    </tr>
                </table>   
        <div style="text-align:left;margin:5px; margin-bottom:0px;">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    
                     
                    <asp:UpdateProgress ID="AP1" runat="server">
                        <ProgressTemplate>
                            <%--929292--%>
                            <div style="top :0px;left:0px; width:100%;height:100%; position :absolute;background-color:rgba(51, 51, 51, 0.8);z-index:1000;">
                            <center >
                            <div style ="border :solid 2px #e2e2e2; width : 150px; background-color :rgba(254, 255, 254,1); height :50px;margin-top:150px;z-index:20000;">
                                <img src="../Images/loading.gif" alt="loading ..."/>
                                <span style ="font-size :11px;"><br />Loading ... </span>
                            </div>
                            </center>
                            </div> 
                        </ProgressTemplate>
                    </asp:UpdateProgress>

            </ContentTemplate>
            </asp:UpdatePanel>
            <div class="nav">
            <ul class="menu" style="margin-top:5px; ">
                <li class="active" onclick="ShowMGTDiv(1,this.id);"><a> Performance on the Job</a></li>
                <li onclick="ShowMGTDiv(2,this.id);"><a>Assessment of Competency</a></li>
                <li onclick="ShowMGTDiv(3,this.id);"><a>Potential Assessment</a></li>
                <li onclick="ShowMGTDiv(4,this.id);"><a>Appraiser Remarks</a></li>     
                <li onclick="ShowMGTDiv(5,this.id);"><a>Office Comments</a></li>                    
                <li onclick="ShowMGTDiv(6,this.id);"><a>Training</a></li>                    
            </ul>
            </div>
        </div>         
        <div style="border-top:solid 3px black"></div>
        <table cellpadding="2" cellspacing="0" width="150px" style="display:none;">
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
         
        <div id="trPerformance">
             <table width="100%" >
                <tr>
                    <td>
        <table width="100%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="bordered" >
                            <col width="40%"/>
                            <col width="40px"/>
                            <col />
                                <tr>
                                    <td style="text-align:center;" class="headercell">Job Responsibility</td>
                                    <td style="text-align:center;" class="headercell">Scale</td>
                                    <td style="text-align:center;" class="headercell">Actual Example/s to support the assessment</td>
                                </tr>
                                <tr >
                                    <td >
                                        <asp:Label ID="lblPerScale1" runat="server" Font-Bold="true" Text="Compliance with & enforcement of vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and   Zero off-hire."></asp:Label>                                                                                
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPerScale1" runat="server" CssClass="CalPerScale" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPerAss1" runat="server" CssClass="lable" ></asp:Label>
                                    </td>
                                </tr>
                                <tr >
                                    <td >
                                        <asp:Label ID="lblPerScale2" runat="server" Font-Bold="true" Text="Compliance with & enforcement of Policies & Procedures from Owners, Charterers & Company’s Safety Management System. "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPerScale2" runat="server" CssClass="CalPerScale"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPerAss2" runat="server" CssClass="lable"   ></asp:Label>
                                      
                                    </td>
                                </tr>
                                <tr id="trPerScale3" runat="server" >
                                    <td >
                                        <asp:Label ID="lblPerScale3" runat="server" Font-Bold="true" Text="Compliance with & enforcement of all Statutory, Flag State,Port State, National and International Regulations."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPerScale3" runat="server" CssClass="CalPerScale"></asp:Label>
                                    </td>
                                    <td>
                                       
                                            <asp:Label ID="txtPerAss3" runat="server" CssClass="lable"  ></asp:Label>
                                     
                                    </td>
                                </tr>
                                <tr class="total">
                                    <td style="text-align:right;">Total Score</td>
                                    <td >
                                        <asp:Label ID="lblTotPer" runat="server" CssClass="TotPerScale" ></asp:Label>
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr class="total">
                                    <td style="text-align:right;">
                                        Avg. Performance Score 
                                    </td>
                                    <td >
                                        <asp:Label ID="lblPerPerformanceScore" runat="server"  CssClass="AvgPerScale"></asp:Label>
                                    </td>
                                    <td>
                                         <span class="hint"> 
                                            
                                            <%--Performance Score =  Total Score÷ 3--%>
                                         </span>
                                    </td>
                                </tr>
                            </table>
                         <td>
                        <table width="96%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="bordered">
                            <tbody><tr>
                                <td>
                                    <b>Please select a scale between 1 and 5 from the definition given below.</b> 
                                </td>
                            </tr>
                        </tbody></table>
                        <table width="96%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="bordered">
                                
                                                                <tbody><tr style="text-align:center;" class="headercell">
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
                                                                        Worked with no supervision, completed all the tasks within or <br>
                                                                         before the stipulated time, zero errors, met all the expectations.
                                                                    </td>
                                                                </tr>
                                                            </tbody></table>
                    </td></tr>
                       </table>
        </div>   
        <div id="trAssesment" style="display:none;" > 
            <table width="100%" >
                <tr>
                    <td>
                 <table width="100%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="bordered" >
                            <col  />
                            <col width="80px" />
                              <tr>
                                <td  class="headercell">Competency</td>
                                <td  class="headercell">Scale</td>
                            </tr>
                            <tr id="trAssScale8" runat="server">
                                <td > 
                                    <asp:Label ID="lblAssScale8" runat="server" Font-Bold="true" Text="Team Management" ></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="txtAssScale8" runat="server" CssClass="CalAssScale"  ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td> <asp:Label ID="lblAssScale1" runat="server" Font-Bold="true" Text="Decision Making" ></asp:Label> </td>
                                <td>
                                    <asp:Label ID="txtAssScale1" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td > <asp:Label ID="lblAssScale2" runat="server" Font-Bold="true" Text="Process Orientation" ></asp:Label> </td>
                                <td>
                                    <asp:Label ID="txtAssScale2" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td ><asp:Label ID="lblAssScale3" runat="server" Font-Bold="true" Text="Leadership" ></asp:Label> </td>
                                <td>
                                    <asp:Label ID="txtAssScale3" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr >
                                <td ><asp:Label ID="lblAssScale4" runat="server" Font-Bold="true" Text="Strategy Execution" ></asp:Label></td>
                                <td>
                                    <asp:Label ID="txtAssScale4" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td ><asp:Label ID="lblAssScale5" runat="server" Font-Bold="true" Text="Technical Know-How" ></asp:Label></td>
                                <td>
                                    <asp:Label ID="txtAssScale5" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id=""><asp:Label ID="lblAssScale6" runat="server" Font-Bold="true" Text="Managing Pressure and Stress" ></asp:Label></td>
                                <td>
                                    <asp:Label ID="txtAssScale6" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id=""><asp:Label ID="lblAssScale7" runat="server" Font-Bold="true" Text="Initiative" ></asp:Label></td>
                                <td>
                                    <asp:Label ID="txtAssScale7" runat="server" CssClass="CalAssScale"></asp:Label>
                                </td>
                            </tr>
                            <tr class="total">
                                <td style="text-align:right;">Total Score</td>
                                <td>
                                    <asp:Label ID="lblTotAss" runat="server" CssClass="TotAssScale" ></asp:Label>
                                </td>
                            </tr>
                            <tr class="total">
                                <td style="text-align:right;">
                                Avg. Competency Score
                                </td>
                                <td style="text-align:left;">
                                    <span >
                                        <asp:Label ID="lblAssCompetencyScore" runat="server" CssClass="AvgAssScale" ></asp:Label>
                                    </span>
                                    <span class="hint" style="margin-left:100px;" > 
                                        <%--Competency Score= Total Score ÷ 8--%>
                                    </span>
                                </td>
                            </tr>
                        </table>
                         </td>
           <td valign="top">
                        <table width="96%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="bordered">
                            <tbody><tr>
                                <td>
                                    Please select a scale between 1 and 4 from the definition given below.
                                </td>
                            </tr>
                        </tbody></table>
                        <table width="96%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="bordered">
                                                                <tbody><tr class="headercell">
                                                                    <td class="headercell">Scale</td>
                                                                    <td class="headercell">Definition</td>
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
                                                            </tbody></table>
                                
                    </td></tr>
                       </table>
        </div>  
        <div id="trPotensial" style="display:none;">
                <table width="100%" cellpadding="4" cellspacing="2" border="0" style="border-collapse:collapse; text-align:left;" class="" >
                <tr>
                    <td >
                       <b> A. What additional responsibilities is he capable of taking beyond the normal job functions?</b><br />
                        <div id="divPotSecA1" runat="server" style="margin-left:15px; min-height:60px;  padding:3px;" class="lable">
                            <asp:Label ID="txtPotSecA1" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>B. Is he ready for promotion? </b>
                        <%--<asp:TextBox ID="txtPotReadyForPromotion" runat="server" style="margin-left:15px;" CssClass="lable"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlPotReadyForPromotion" runat="server" style="margin-left:10px;width:120px;">
                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <span style="margin-left:15px;"><b>Please specify reasons</b></span> <br />
                        <div id="divPotSecB1" runat="server" style="margin-left:15px;min-height:60px; padding:3px;" class="lable">
                            <asp:Label ID="txtPotSecB1" runat="server"  TextMode="MultiLine" Width="95%" Height="50px"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td >
                       <b> C. Identify training needs:</b> <br /><br />

              
                        <span style="margin-left:15px;"><b>i.	For undertaking present job functions more effectively.</b> </span> <br />
                        <div id="divPotSecC1" runat="server" style="margin-left:15px;min-height:60px; padding:3px;" class="lable">
                            <asp:Label ID="txtPotSecC1" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:Label>
                        </div>


                        <span style="margin-left:15px;"><b>ii.	For undertaking higher responsibilities.</b></span><br />
                        <div id="divPotSecC2" runat="server" style="margin-left:15px; min-height:60px;" class="lable">
                            <asp:Label ID="txtPotSecC2" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        
        <div id="trRemarks" style="display:none;" >  
            <div class="headercell">Appraisee Remarks :</div> 
            <div style="padding:5px;"><asp:Label runat="server" id="txtManAppAppraiseeRem" style="width:99%; height:70px;" rows="4" TextMode="MultiLine"></asp:Label></div>
                <div style="padding:5px;">
                        <table border="0" rules="none" cellpadding="5" cellspacing="0" >
                               <colgroup>
                                   <col width="100px" />
                                   <col width="200px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                               </colgroup>
                                <tr>
                                    <td style="text-align:right"><b>Name:</b> </td>
                                    <td>
                                        <asp:Label runat="server" id="txtManAppAppraiseeName" type="text" maxlength="50" Enabled="false"></asp:Label>
                                    </td>
                                    <td style="text-align:right"> <b>Crew#.:</b></td>
                                    <td>
                                        <asp:Label runat="server" id="txtManAppAppraiseeCrewNo" type="text" maxlength="6"  Enabled="false"></asp:Label>
                                    </td>
                                    <td style="text-align:right"><b>Rank:</b></td>
                                    <td>
                                        <asp:Label runat="server" id="txtManAppAppraiseeRank" type="text" maxlength="6"  Enabled="false"></asp:Label>
                                    </td>
                                    <td style="text-align:right"><b>Date:</b> </td>
                                    <td>
                                        <asp:Label runat="server" id="txtManAppAppraiseeDate"  type="text" maxlength="11" ></asp:Label>
                                                            
                                    </td>
                                </tr>
                        </table>
                </div>
                <div>
                    <div class="headercell">Appraiser Remarks :</div>
                     <div style="padding:5px;"><asp:Label runat="server" id="txtManAppAppraiserRem" style="width:99%; height:70px;" rows="4"  TextMode="MultiLine"></asp:Label>        </div>
                    <div style="padding:5px;">
                        <table border="0" rules="none" cellpadding="5" cellspacing="0" >
                             <colgroup>
                                   <col width="100px" />
                                   <col width="200px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                               </colgroup>
                                                    <tr>
                                                        <td style="text-align:right" ><b>Name:</b></td>
                                                        <td>
                                                            <asp:Label runat="server" id="txtManAppAppraiserName" type="text" maxlength="50"></asp:Label>
                                                        </td>
                                                        <td style="text-align:right"><b>Crew#.:</b></td>
                                                        <td>
                                                            <asp:Label runat="server" id="txtManAppAppraiserCrewNo" type="text" maxlength="6" onblur="CheckCrewFormat(this)"></asp:Label>
                                                        </td>
                                                        <td style="text-align:right"><b>Rank:</b></td>
                                                        <td>
                                                            <asp:Label runat="server" id="txtManAppAppraiserRank" type="text" maxlength="6" ></asp:Label>
                                                        </td>
                                                        <td style="text-align:right"><b>Date:</b></td>
                                                        <td>
                                                            <asp:Label runat="server" id="txtManAppAppraiserDate"  type="text" ></asp:Label>
                                                        </td>
                                                    </tr>
                        </table>
                        </div>
                </div>
                <div>
                    <div class="headercell">Master’s Remarks:</div>
                     <div style="padding:5px;"><asp:Label runat="server" id="txtManAppMasterRem" style="width:700px; height:70px;" TextMode="MultiLine"></asp:Label></div>
                    <div style="padding:5px;">
                        <table  border="0" rules="none" cellpadding="5" cellspacing="0" >
                             <colgroup>
                                   <col width="100px" />
                                   <col width="200px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   <col width="100px" />
                                   
                               </colgroup>                                                  
                             
                            <tr>
                                <td style="text-align:right"><b>Name:</b></td>
                                <td>
                                    <asp:Label runat="server" id="txtManAppMasterName" type="text" maxlength="50"></asp:Label>
                                </td>
                               
                                <td style="text-align:right"><b>Crew#:</b></td>
                                <td>
                                    <asp:Label runat="server" id="txtManAppMasterCrewNo" type="text" maxlength="6" onblur="CheckCrewFormat(this)"></asp:Label>
                                </td>
                                <td style="text-align:right"><b>Date:</b> </td>
                                <td>
                                    <asp:Label runat="server" id="txtManAppMasterDate"  type="text" ></asp:Label>
                                </td>
                            </tr>
                          </table>
                        </div>  
                </div>
        </div>
        <div id="trOfficeComment" style="display:none;" >
                <table  width="100%" style="text-align:left;" >
                            <col width="140px" />
                            <col width="150px" />
                            <col width="140px" />
                            <col  />
                            <tr>
                                <td colspan="4">
                                    <asp:TextBox ID="txtOfficeComment" runat="server" style="width:99%;height:350px; background-color:#fffaa5" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <b> Office Comments by :</b>
                                </td>
                                <td>
                                    <asp:Label ID="txtReviewedby" runat="server" ReadOnly="true" ></asp:Label>
                                </td>
                                <td><b>Office Comments On :</b></td>
                                <td>
                                    <asp:Label ID="txtReviewedDate" runat="server" Width="90px" ReadOnly="true"  CssClass="date_only" style="width:80px;"></asp:Label>
                                </td>
                            </tr>
                </table>
           
        </div>
        <div id="trTraining" style="display:none; " > 
             <asp:UpdatePanel ID="UP1" runat="server" >
                            <ContentTemplate>
                    <table  width="100%" style="background-color:#7AAAD2;color:White;text-align:left;" >
                          <tr>
                            <td style="width:130px;">
                                Training Required :
                            </td>
                            <td >
                                   
                        <asp:DropDownList ID="ddlTrainingRequired" runat="server" Width="50px" style="margin-left:5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlTraining_OnSelectedIndexChanged"> 
                                    <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            </asp:DropDownList>
                              
                                
                            </td>
                              <td style="text-align:right;">
                                  <input type="button" value="View Training Matrix" onclick="OpenTrainingMatrix();" />

                              </td>
                          </tr>
                          </table>
                    <div id="tblTraining" runat="server" visible="false" >
                        
                    <table cellpadding="5" width="100%" cellspacing="0">
                        <tr>
                            <td>
                           
                            <table width="100%" cellpadding="5" cellspacing="0" class="bordered" style="border-collapse:collapse;">
                            <tr>
                                <td style="width:300px">Sire Chapter :</td> 
                                <td  >Training Name :</td>
                                <td style="width:160px" >Due Date :</td>
                                <td style="width:100px">&nbsp;</td>
                            </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlSireChap1" runat="server" Width="250px" CssClass="required_box" AutoPostBack="true" ValidationGroup="v789"  OnSelectedIndexChanged="ddlSireChap1_SelectedIndexChanged" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSireChap1" ErrorMessage="*" ForeColor="Red" ValidationGroup="v789" ></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlTrainingName" runat="server" Width="250px" CssClass="required_box" ValidationGroup="v789"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTrainingName" ErrorMessage="*" ForeColor="Red" ValidationGroup="v789" ></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_DueDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3" Width="80px" ValidationGroup="v789" ></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_DueDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="v789" ></asp:RequiredFieldValidator>
                                    </td>
                                   <td style="text-align:right;">
                                            <asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text="Add" Width="50px" TabIndex="9" ValidationGroup="v789" OnClick="btn_Save_PlanTraining_Click" style="margin-right:10px;"/>
                                        </td>
                                    </tr>                                
                            </table>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton5" PopupPosition="Left" TargetControlID="txt_DueDate" ></ajaxToolkit:CalendarExtender>
                          
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <div style="height:330px; width:100%; border:solid 1px Gray;overflow-x:hidden;overflow-y:scroll;">
                            <table cellpadding="2" cellspacing="2" width="100%" style="border-collapse:collapse" class="bordered">
                                <tr style="background-color:#444;color:white; height:30px">
                                    <td style="width:50%">Training Type</td>
                                    <td>Training Name</td>
                                    <td style="width:100px">Due Date</td>
                                    <td style="width:80px">Delete</td>
                                </tr>
                            <col width="130px" />
                            <col width="400px" />
                            <col width="120px" />
                            <asp:Repeater ID="rptTraining" runat="server" OnItemDataBound="rptTraining_OnItemDataBound" >
                                <ItemTemplate>
                                    <tr class="lable">    
                                        <td >
                                            <b><%#Eval("TrainingTypetxt")%></b>
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
                            </div>
                        </td>
                                
                        </tr>
                           
                    </table>
                         
               </div>
                                 </ContentTemplate>
                            </asp:UpdatePanel>
        </div>

        <div style="text-align:center; padding:5px; background:#f8fe75;color:red;">
            <asp:UpdatePanel ID="UPMsg" runat="server" UpdateMode="Always">
            <ContentTemplate>
                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
            
            
        </div>
         <div style="padding:5px; text-align:center;">
             <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_OnClick" style="margin-right:3px;" Visible="false" />
             <asp:Button ID="btnClosure" runat="server" Text="Closure" CssClass="btn" OnClick="btnClosure_OnClick" style="margin-right:3px;" Visible="false" />
         </div>
    </div>
    </form>

    <script type="text/javascript">
    $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
</script>
</body>
</html>
