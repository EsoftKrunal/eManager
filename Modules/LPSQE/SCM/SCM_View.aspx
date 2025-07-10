<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCM_View.aspx.cs" Inherits="VIMS_SCM_View" Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>SCM</title>    
<script type="text/javascript" src="../JQ_Scripts/jquery.min.js"></script>
  <script src="../js/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../js/KPIScript.js"></script>
  <link rel="Stylesheet" href="../js/AutoComplete/jquery-ui.css" />
  <script src="../js/AutoComplete/jquery-ui.js" type="text/javascript"></script>
	  <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
    body
    {
        margin:0px;
        font-family:Helvetica;
        font-size:14px;
        color:#2E5C8A;
        font-family:Calibri;
    }
  input
  {
      padding:2px 3px 2px 3px;
      border:solid 1px #006B8F;
      color:black; 
      font-family:Calibri;
      /*background-color:#E0F5FF;*/
  }
  input[type='radio']
  {
      padding:2px 3px 2px 3px;
      border:none;
      color:black; 
      font-family:Calibri;
      background-color:white;
  }
  textarea
  {
      padding:0px 3px 0px 3px;
      border:solid 1px #006B8F;
      color:black; 
      text-align:left;
      font-family:Calibri;
      /*background-color:#E0F5FF;*/
  }
  select
  {
      border:solid 1px #006B8F;
      padding:0px 3px 0px 3px;
      height:22px;
      vertical-align:middle;
      border:none;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  td
  {
      vertical-align:middle;
  }
  .unit
  {
      color:Blue;
      font-size:13px;
      text-transform:uppercase;
  }
  .range
  {
      color:Red;
      font-size:13px;
      text-transform:uppercase;
  }
  .stickyFooter {
     position: fixed;
     bottom: 0px;
     width: 100%;
     overflow: visible;
     z-index: 99;
     padding:5px;
     background: white;
     border-top: solid black 2px;
     -webkit-box-shadow: 0px -5px 15px 0px #bfbfbf;
     box-shadow: 0px -5px 15px 0px #bfbfbf;
     vertical-align:middle;
     background-color:#FFFFCC;
}
/*.btn
{
     border:none;
    color:#ffffff;
    background-color:#B870FF;
    padding:4px;
    
}
.btn:hover
{
    background-color:#006B8F;
    color:White;
}*/
.div1
{
 background-color:#006B8F; 
 color:White;
 padding:8px; 
 font-size:14px;
 text-align:center;
 margin-top:5px;
 width:300px;
 text-align:left;
}
.color_tab{
  /*background: #3498db;
  background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
  background-image: -moz-linear-gradient(top, #3498db, #2980b9);
  background-image: -ms-linear-gradient(top, #3498db, #2980b9);
  background-image: -o-linear-gradient(top, #3498db, #2980b9);
  background-image: linear-gradient(to bottom, #3498db, #2980b9);
  -webkit-border-radius: 0;
  -moz-border-radius: 0;
  border-radius: 0px;
  
  color: #ffffff;
  
  padding: 5px 9px 5px 9px;
  text-decoration: none;*/
  background-color :#c2c2c2;
  background-image: -webkit-linear-gradient(top, #c2c2c2, #2980b9);
  background-image: -moz-linear-gradient(top, #c2c2c2, #2980b9);
  background-image: -ms-linear-gradient(top, #c2c2c2, #2980b9);
  background-image: -o-linear-gradient(top, #c2c2c2, #2980b9);
  background-image: linear-gradient(to bottom, #c2c2c2, #2980b9);
	border:solid 1px gray;
	border :none;
	padding: 5px 9px 5px 9px;
    font-family: Arial;
    font-size: 12px;
}

.color_tab:hover {
 background: #3cb0fd;
  background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
  background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
  background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
  background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
  background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
  text-decoration: none;
}

.color_tab_sel{
  /*background: #facc8c;
  background-image: -webkit-linear-gradient(top, #f7af51, #facc8c);
  background-image: -moz-linear-gradient(top, #f7af51, #facc8c);
  background-image: -ms-linear-gradient(top, #f7af51, #facc8c);
  background-image: -o-linear-gradient(top, #f7af51, #facc8c);
  background-image: linear-gradient(to bottom, #f7af51, #facc8c);
  font-family: Arial;
  color: black;
  font-size: 12px;
  padding: 5px 9px 5px 9px;
  text-decoration: none;
  border-bottom:solid 1px #facc8c;
  text-align:center;*/
  background-color :#669900;
  background-image: -webkit-linear-gradient(top, #f7af51, #669900);
  background-image: -moz-linear-gradient(top, #f7af51, #669900);
  background-image: -ms-linear-gradient(top, #f7af51, #669900);
  background-image: -o-linear-gradient(top, #f7af51, #669900);
  background-image: linear-gradient(to bottom, #f7af51, #669900);
	color :White;
	border :none;
    padding: 5px 9px 5px 9px;
    font-family: Arial;
    font-size: 12px;
}
.hdf1
{
    text-align:left;
    padding:5px;
    background-color:#D6EBFF;
    font-weight:bold;
}
</style>
<%--<script type="text/javascript">
    function IsCrewAvailableOnBoard(X) {
        if (X == 1) {
            document.getElementById('txtCrewAvailable').style.display = 'none';
            document.getElementById('txtCrewAvailable').value = '';
        }
        else
            document.getElementById('txtCrewAvailable').style.display = 'block';
    }
    function IsCrewFamiliarWithAll(X) {
        if (X == 1) {
            document.getElementById('txtCrewFamiliar').style.display = 'none';
            document.getElementById('txtCrewFamiliar').value = '';
        }
        else
            document.getElementById('txtCrewFamiliar').style.display = 'block';
    }
    function OutstandingItem(X) {
         if (X == 1) {
              document.getElementById('OutstandingItem').style.display = 'block';
          }
          else {
              document.getElementById('OutstandingItem').style.display = 'none';
          }
</script>--%>
 </head>
<body style="padding-bottom:0px;" >
<form name="form1" runat="server"  > 
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

	<div style="border :solid 2px #4371a5; width:99%;"  >
        
        
		<table width="100%" cellpadding="0" cellspacing="0" border="0" >
		    <tr>
		        <td colspan="1" class="text headerband" style="  height:35px; text-align:center;">
		            <b>SAFETY COMMITTEE MEETING REPORT</b>
		        </td>
		    </tr>
		    <tr>
		        <td  >
		            <!--Genral Information-->
			        <div style="border-top:none;border-left:none; width:100%;" >    
				        <table width="100%" cellpadding="2" cellspacing="2" border="0" >
				            <col width="100px;" />
				            <col width="200px;" />
				            <col width="80px;" />
				            <col width="100px;" />
				            <col width="100px;" />
				            <col width="250px;" />
				            <col width="130px;" />
				            <col />
				            <tr>
				                <td>
									Vessel :
								</td>
								<td>
								    <asp:Label ID="txtShip" runat="server"  />
								</td>
				                
				                <td style="text-align:left">Occasion :</td>
							    <td style="text-align:left">
							        <%--<asp:DropDownList ID="ddlOccasion" style="width:120px" runat="server" >
                                        <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
							        </asp:DropDownList>--%>
                                    <asp:Label ID="lblOccasion" runat="server"  />
							    </td>
							    <td>Date :</td>
				                <td>
				                    <asp:TextBox ID="txtDate" maxlength="12" runat="server" style="width:80px;" />
				                    
                                    <asp:RequiredFieldValidator ID="rfDate" runat="server" ForeColor="Red" ValidationGroup="VG" ErrorMessage="*" ControlToValidate="txtDate" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
				                </td>
				                <td>
									Time Commenced :
								</td>
								<td>
									<asp:DropDownList ID="ddlDTCommenced_H" runat="server" AppendDataBoundItems="true"  style=" width:45px">
                                        <asp:ListItem Text="HH" Value=""></asp:ListItem>
									</asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="VG" ErrorMessage="*" ControlToValidate="ddlDTCommenced_H" SetFocusOnError="true" ></asp:RequiredFieldValidator>
									&nbsp;:&nbsp;
									<asp:DropDownList id="ddlDTCommenced_M" runat="server" AppendDataBoundItems="true"  style=" width:45px" >
									    <asp:ListItem Text="MM" Value=""></asp:ListItem>
									</asp:DropDownList>   <span style="font-size:10px;">Local Time</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="VG" ForeColor="Red" ErrorMessage="*" ControlToValidate="ddlDTCommenced_M" SetFocusOnError="true" ></asp:RequiredFieldValidator>
								    
								</td>
				                
				            </tr>
					        <tr>
								
							    <td>Ship's Position :</td>
							    <td>
							        <asp:DropDownList id="ddlShipPosition" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShipPosition_Click" style="width:120px" >
                                        <asp:ListItem Text="At Sea" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="In Port" Value="P"></asp:ListItem>
							        </asp:DropDownList>
							    </td>
                                <td ></td>
                                <td ></td>
								<td>
								    <asp:Label ID="lblPosition" runat="server" >Voy From/To :</asp:Label>
								</td>
								<td>
								    <table cellpadding="1" cellspacing="0">
								        <tr>
								            <td>
								                <asp:TextBox id="txtShipPosFrom" runat="server" MaxLength="50" style="width:100px;" />
								            </td>
								            <td>
								                <asp:TextBox id="txtShipPosTo" runat="server" MaxLength="50" style="width:100px;" />
								            </td>
								        </tr>
								    </table>
								    
								    
								</td>
								<td>
									Time Completed :

								</td>
								<td>
                                    <asp:DropDownList ID="ddlPlaceCommenced_H" runat="server" AppendDataBoundItems="true"  style=" width:45px"  >
                                        <asp:ListItem Text="HH" Value=""></asp:ListItem>
									</asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ValidationGroup="VG" ErrorMessage="*" ControlToValidate="ddlPlaceCommenced_H" SetFocusOnError="true" ></asp:RequiredFieldValidator>
									&nbsp;:&nbsp;
                                    <asp:DropDownList id="ddlPlaceCommenced_M" runat="server" AppendDataBoundItems="true"  style=" width:45px" >
									    <asp:ListItem Text="MM" Value=""></asp:ListItem>
									</asp:DropDownList> <span style="font-size:10px;"> Local Time</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ValidationGroup="VG" ErrorMessage="*" ControlToValidate="ddlPlaceCommenced_M" SetFocusOnError="true" ></asp:RequiredFieldValidator>
								    
								</td>
							</tr>
							
						</table>
					</div>
			    </td>
		    </tr>
		    <tr>
				<td style="vertical-align:top; padding:2px;">
				<div id="DivGenralInfo">
				    
				</div>
				
				<!--Tabs-->
				
<!-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->				
				</td>
			</tr>    
		</table>
        <div style="height:30px; ">
            <table cellpadding="2" cellspacing="0" style="float:left; font-size:9px;" border="0" width="100%">
							        <tr>
                                        <td style="width:125px;">
							                <%--<asp:Button ID="btnMGT9" Text="Home" CssClass="color_tab_sel" runat="server" OnClientClick="javascript:return false;" /> --%>
                                            <div id="dv_Home" class="color_tab_sel" runat="server" style="width:100%; text-align:center;" >Crew List</div>   
							            </td>
										<td style="width:125px;">
							                <asp:Button ID="btnMGT11" Text="Office Comments" CssClass="color_tab" runat="server" CommandArgument="10" OnClick="btnNext_Click" style="border:none;" /> <!--SetColor(4,'M')-->

											
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT1" CssClass="color_tab" runat="server" Text=" Start " OnClientClick="javascript:return false;"  /> <!--SetColor(1,'M')-->--%>
                                            <div id="dv_Start" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false" >Start</div>
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT2" Text=" Safety "  CssClass="color_tab" runat="server" OnClientClick="javascript:return false;" /> <!--SetColor(2,'M')-->--%>
                                            <div id="dv_Safety" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false" >Safety</div>
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT3" Text=" Health " CssClass="color_tab" runat="server" OnClientClick="javascript:return false;" />  <!--SetColor(3,'M')-->--%>
                                            <div id="dv_Health" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false">Health</div>
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT4" Text="Security " CssClass="color_tab" runat="server" OnClientClick="javascript:return false;" /> <!--SetColor(4,'M')-->--%>
                                            <div id="dv_Security" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false" >Security</div>
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT5" Text="Quality" CssClass="color_tab" runat="server"  OnClientClick="javascript:return false;" /> <!--SetColor(4,'M')-->--%>
                                            <div id="dv_Quality" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false" >Quality</div>
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT6" Text="Environment" CssClass="color_tab" runat="server" OnClientClick="javascript:return false;" /> <!--SetColor(4,'M')-->--%>
                                            <div id="dv_Env" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false">Environment</div>
							            </td>
							            <td>
							                <%--<asp:Button ID="btnMGT7" Text="AOB" CssClass="color_tab" runat="server" OnClientClick="javascript:return false;" /> <!--SetColor(4,'M')-->--%>
                                            <div id="dv_AOB" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false">AOB</div>
							            </td>							            
                                        <td>
							                <%--<asp:Button ID="btnMGT10" Text="Best Practice" CssClass="color_tab" runat="server" OnClientClick="javascript:return false;" /> <!--SetColor(4,'M')-->--%>
                                            <div id="dv_BP" class="color_tab" runat="server" style="width:100%; text-align:center;" visible="false">Best Practice</div>
							            </td>
                                        
										<td style="text-align:right;">
											 <asp:LinkButton ID="ImgAttachment" runat="server" Text="View SCM Report" Visible="false" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" style="float:right;padding-right:5px;font-family:Arial, Helvetica, sans-serif;font-size:medium;" />
										</td>

							        </tr>
							    </table>
        </div>
        <%--<div style="height:10px; background-color:#facc8c;"></div>--%>
        <div id="DMember" style="vertical-align:top;" >
					<table width="100%" cellpadding="0" cellspacing="0" border="0">
						<tr>
						    <td style="vertical-align:top;">
                             <%--<div class='ScrollAutoReset' id='dv_Main' style="overflow-x:hidden; overflow-y:scroll; width:100%; height:330px;" >--%>
						        <!--Start-->
						        <div id="DPerformance" runat="server" visible="false" >
						                        <div style="overflow-x:hidden; overflow-y:hidden;"  >  
						                            <div class="hdf1" >Last Meeting's Minutes And Status of Corrective Actions</div>
						                            <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; border:solid 1px #cccccc;">
						                            <col width="400px" />
						                            <col />
						                            <tr>
						                                <td>
						                                    Minutes of previous  SAFECOM meeting discussed 
						                                </td>
                                                        <td>:</td>
						                                <td>
						                                    <asp:RadioButton ID="rMinutesofPreviousYes" runat="server" Text="Yes" GroupName="Discussed" style="border:none;"   /> 
                                                            &nbsp;&nbsp;&nbsp;
						                                    <asp:RadioButton ID="rMinutesofPreviousNo" runat="server" Text="No" GroupName="Discussed" style="border:none;"  />
						                                    <span style="font-size:15px; color:Red;padding:4px;">*</span>
						                                </td>
						                            </tr>
						                            <tr>
						                                <td>
						                                    Absentees in previous  SAFECOM meeting briefed 
						                                </td>
                                                        <td>:</td>
						                                <td>
						                                    <asp:RadioButton ID="rAbsenteesYes" runat="server" Text="Yes" GroupName="Absentees" style="border:none;"  /> 
                                                            &nbsp;&nbsp;&nbsp;
						                                    <asp:RadioButton ID="rAbsenteesNo" runat="server" Text="No" GroupName="Absentees" style="border:none;" />
						                                    <span style="font-size:15px; color:Red;padding:4px;">*</span>
						                                </td>
						                            </tr>
						                            <tr>
						                                <td>
						                                    Office comments to previous SAFECOM meeting discussed 
						                                </td>
                                                        <td>:</td>
						                                <td>
						                                    <asp:RadioButton ID="rOfficeCommentsYes" runat="server" Text="Yes" GroupName="comments" style="border:none;"    /> 
                                                            &nbsp;&nbsp;&nbsp;
						                                    <asp:RadioButton ID="rOfficeCommentsNo" runat="server" Text="No" GroupName="comments" style="border:none;"  />
						                                    <span style="font-size:15px; color:Red;padding:4px;">*</span>
						                                </td>
						                            </tr>
						                            
						                            <tr >
						                                <td>
						                                    Any Outstanding item from previous SAFECOM meeting &nbsp;&nbsp;
						                                </td>
                                                        <td>:</td>
						                                <td>
						                                    <asp:RadioButton ID="rOutstandingItemYes" runat="server" Text="Yes" GroupName="Ost" OnCheckedChanged="OutstandingItem_OnCheckedChanged" AutoPostBack="true" style="border:none;"   /> 
                                                            &nbsp;&nbsp;&nbsp;
						                                    <asp:RadioButton ID="rOutstandingItemNo" runat="server" Text="No" GroupName="Ost" OnCheckedChanged="OutstandingItem_OnCheckedChanged" AutoPostBack="true" style="border:none;"   />
						                                    <span style="font-size:15px; color:Red;padding:4px;">*</span>
						                                </td>
						                            </tr>
						                         </table>
                                                 <div id="OutstandingItem" runat="server" visible="false">
                                                   <div class="hdf1" >List the outstanding items</div>
			                                       <asp:TextBox ID="txtOutstandingItems" runat="server" TextMode="MultiLine"  style="width:99%; height:280px" ></asp:TextBox>
			                                          
                                                 </div>
						                         </div>
						                         
                                                 <div style="text-align:center; padding:5px;">                                                  
                                                  <asp:Button ID="btnPre_Start" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="9" OnClick="btnPrevious_Click" />
                                                  <asp:Button ID="btnNextStart" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="1" OnClick="btnNext_Click" />
                                                  <br />
                                                  <asp:Label ID="lblMsg_1" ForeColor="Red" runat="server" ></asp:Label>
                                                 </div>               
						                
						        </div>
						        <!--Safety-->
						        <div id="DAssessmentofCompetencies" runat="server" visible="false" style="vertical-align:top;" >
						            <table cellpadding="2" cellspacing="0" width="100%">
						            <col width="420px" />
						            <col />
					                <tr>
					                    <td>
					                        <table cellpadding="4" cellspacing="0" border="0" width="100%">
					                            <tr>
					                                <td style="width:50%">
					                                    Are all Circulars/Safety Alert available onboard :
					                                     <asp:RadioButton ID="rAvailableYes" runat="server" GroupName="a" Text="Yes" OnCheckedChanged="CrewAvailableOnBoard_OnCheckedChanged" AutoPostBack="true" style="border:none;"   /> 
					                                    <asp:RadioButton ID="rAvailableNo" runat="server" GroupName="a" Text="No" OnCheckedChanged="CrewAvailableOnBoard_OnCheckedChanged" AutoPostBack="true" style="border:none;"  />  
					                                    <span style="font-size:15px; color:Red;padding:4px;">*</span>
					                                </td>
					                                <td>
					                                    Are all crew on board familiar with Circulars/safety alerts :
					                                    <asp:RadioButton  runat="server" GroupName="b" Text="Yes" ID="rFamiliarYes" OnCheckedChanged="CrewFamiliarWithAll_OnCheckedChanged" AutoPostBack="true" style="border:none;"  />  
					                                    <asp:RadioButton  runat="server" GroupName="b" Text="No" ID="rFamiliarNo"  OnCheckedChanged="CrewFamiliarWithAll_OnCheckedChanged" AutoPostBack="true"  style="border:none;"  />  
					                                    <span style="font-size:15px; color:Red;padding:4px;">*</span>
					                                </td>
					                            </tr>
					                            <tr>
					                                <td>
					                                    <asp:TextBox ID="txtCrewAvailable" runat="server" TextMode="MultiLine" style="width:99%; height:100px;"></asp:TextBox>
					                                </td>
					                                <td>
					                                    <asp:TextBox ID="txtCrewFamiliar" runat="server" TextMode="MultiLine" style="width:99%; height:100px;"></asp:TextBox>
					                                </td>
					                            </tr>
					                            <tr>
					                                <td colspan="2">
					                                    <div> Accidents/Near Misses from other ships and preventive measures have been discussed during SCM : </div><br />
					                                    <asp:TextBox ID="txtAnyAccident" runat="server" TextMode="MultiLine" style="width:99%; height:110px;"></asp:TextBox>
					                                </td>
					                            </tr>
					                            <tr>
					                                <td colspan="2">
					                                    <div>Review of Mooring Practices (Ref :procedure in GOM-0040) : </div><br />
					                                    <asp:TextBox ID="txtAnyNearMiss" runat="server" TextMode="MultiLine" style="width:99%; height:110px;"></asp:TextBox>
					                                </td>
					                            </tr>
					                        </table>
					                    </td>
					                </tr>
						            </table>
                                    
					                 <div style="text-align:center; padding:5px;">
                                         <asp:Button ID="Button1" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="1" OnClick="btnPrevious_Click" />
                                         <asp:Button ID="btnNextSafety" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="2" OnClick="btnNext_Click" />
                                         <br />
                                         <asp:Label ID="lblMsg_2" ForeColor="Red" runat="server" ></asp:Label>
                                     </div>
		                            
						            </div>
						        <!--Health-->
						        <div id="DPotAssesment" runat="server" visible="false">
						            <table border="0" width="99%" cellpadding="4" cellspacing="0" >
						                <tr>    
						                    <td>
						                        <div style="text-align:left; padding:5px;">Review Health,Hygiene & Sanitation Standards on board : </div>
						                         <asp:TextBox ID="txtReviewHelth" runat="server" TextMode="MultiLine" style="width:99%; height:420px;"></asp:TextBox>
						                    </td>
						                </tr>						                
						            </table>
                                    
                                    <div style="text-align:center; padding:5px;">
                                        <asp:Button ID="Button2" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="2" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnNextHealth" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="3" OnClick="btnNext_Click" />
                                    </div>
						        </div>
						        <!--Security-->
						        <div id="DAppraiseeRemarks" runat="server" visible="false">
						            <table border="0" width="100%" cellpadding="4" cellspacing="0" >
						                <tr>    
						                    <td>
						                         <div style="text-align:left; padding:5px;">Review any immediate security related concerns :</div>
						                         <asp:TextBox ID="txtReviewAnyImmediateSecurity" runat="server" TextMode="MultiLine" style="width:99%; height:420px;"></asp:TextBox>
						                    </td>
						                </tr>
						            </table>
                                    
                                    <div style="text-align:center; padding:5px;">
                                        <asp:Button ID="Button3" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="3" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnNextSeq" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="4" OnClick="btnNext_Click" />
                                    </div>               
		                             
						        </div>
						        
						        <!--Quality-->
						        <div id="DCompliance" runat="server" visible="false">
						            <table border="0" width="99%" cellpadding="4" cellspacing="0" >
						                <tr>
						                    <td>
						                        <div style="padding:5px">Review of regulatory compliance standards on board (including actions to be taken to comply with Future Regulations) : </div>
						                        <asp:TextBox ID="txtReviewOfRegulatoryCompliance" runat="server" TextMode="MultiLine" CssClass="AutoHeight" style="width:99%; height:50px; "  ></asp:TextBox>
						                    </td>
						                </tr>
						                <tr>
						                    <td>
						                        <div style="padding:5px">Review of quality systems implementation during the Voyage (Ref: Procedures in ITM-0020) :</div>
						                        <asp:TextBox ID="txtReviewOfQuality" runat="server" TextMode="MultiLine" style="width:99%;height:50px;" class="AutoHeight" ></asp:TextBox>
						                    </td>
						                </tr>
						                
						                <tr>
						                    <td>
						                        <div style="padding:5px">Review following NCR which are open.Report any that may need office support and /or nearing date to be completed :</div>
						                        <div style="overflow-x:hidden;overflow-y:hidden;  border:solid 1px #cccccc;"> 
						                            <table border="1" width="100%" cellpadding="5" cellspacing="0" style="border-collapse:collapse;" >
						                            <col width="150px" />
						                            <col width="100px" />
						                            <col />
                                                    <col width="17px" />
						                            <tr style="background-color:#facc8c;">
                                                        <td>NCR No.</td>
                                                        <td>Closure Date</td>
                                                        <td>Remarks</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    </table>
                                                </div>
                                        
                                                <div style="overflow-x:hidden;overflow-y:scroll;height:100px;  border:solid 1px #cccccc;"> 
                                                    
                                                    <table border="0" width="100%" cellpadding="5" cellspacing="0" style="border:solid 1px #facc8c;">
                                                     <col width="150px" />
						                            <col width="100px" />
						                            <col />
                                                    <col width="17px" />

                                                    </table>

                                                    <asp:Repeater runat="server" ID="rptNCR">
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td><asp:Label ID="lblNo" Text='<%#Eval("NUMBER")%>' runat="server"></asp:Label> </td>
                                                            <td><asp:Label ID="lblCDate" Text='<%#Common.ToDateString(Eval("CDATE"))%>' runat="server"></asp:Label></td>
                                                            <td><asp:TextBox ID="lblRemarks" Text='<%#Eval("REMARKS")%>' runat="server" Width="99%" MaxLength="1000" ></asp:TextBox></td>
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
						                        <div style="padding:5px">Review quality KPIs :</div>
						                        <asp:TextBox ID="txtReviewAllCompanyKPI" runat="server" TextMode="MultiLine" style="width:99%;height:40px;"  class="AutoHeight"></asp:TextBox>
						                    </td>
						                </tr>
						                <tr>
						                    <td>
						                        <div style="padding:5px">Review Crew welfare :</div>
						                        <asp:TextBox ID="txtReviewCrewWelfare" runat="server" TextMode="MultiLine" style="width:99%;height:40px;" class="AutoHeight"></asp:TextBox>
						                    </td>
						                </tr>
						            </table>
                                    
                                    <div style="text-align:center; padding:5px;">
                                        <asp:Button ID="Button4" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="4" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnNextQty" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="5" OnClick="btnNext_Click" />
                                    </div>               
						        </div>
						        
						        <!--NCR-->
						        <div id="DNCR" runat="server" visible="false">
						            <table border="0" width="99%" cellpadding="4" cellspacing="0" >
						                <tr>    
						                    <td>
						                         <div style="padding:5px">Review Environmental KPIs :</div>
						                         <asp:TextBox ID="txtReviewEnvironmentalKPIs" runat="server" TextMode="MultiLine" style="width:99%; height:400px;"></asp:TextBox>
						                    </td>
						                </tr>
						            </table>
                                    
						            <div style="text-align:center; padding:5px;">
                                     <asp:Button ID="Button5" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="5" OnClick="btnPrevious_Click" />
                                     <asp:Button ID="btnNextEnv" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="6" OnClick="btnNext_Click" />
                                    </div>               
						   
						        </div>
						        
						        <!--AOB-->
						        <div id="DMooringNOther" runat="server" visible="false">
						            <table border="0" width="99%" cellpadding="4" cellspacing="0" >
						                <tr>
						                    <td >
                                                 <div style="padding:5px">Any other issues :</div>
                                                 <asp:TextBox ID="txtAnyOtherIssues" runat="server" TextMode="MultiLine" style="width:99%; height:400px;" ></asp:TextBox>
						                    </td>
						                </tr>
						            </table>
						             
                                    <div style="text-align:center; padding:5px;">
                                     <asp:Button ID="Button6" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="6" OnClick="btnPrevious_Click" /> 
                                     <asp:Button ID="btnNextAOB" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="7" OnClick="btnNext_Click" />
                                    </div>   
						        </div>

                                <!--Best Practice-->
						        <div id="DBestPractice" runat="server" visible="false">
						            <table border="0" width="99%" cellpadding="4" cellspacing="0" >
						                <tr>
						                    <td >
                                                 <div style="padding:5px">Best Practices & Recommendation :</div>
                                                 <asp:TextBox ID="txtBestPractice" runat="server" TextMode="MultiLine" style="width:99%; height:400px;" ></asp:TextBox>
						                    </td>
						                </tr>
						            </table>
                                    <div style="text-align:center; padding:5px;">                                     
                                     <asp:Button ID="Button7" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="7" OnClick="btnPrevious_Click" />
                                     <asp:Button ID="Button8" runat="server" CssClass="color_tab" Text="Next >>"  CommandArgument="10" OnClick="btnNext_Click" style="display:none;" />
                                     
                                    </div>
						        </div>

                                 <!--Office Comments-->
						        <div id="DBOfficeComments"  runat="server" visible="false">
						            
                                        <div class="hdf1">Office Comments : </div>
                                        <asp:TextBox ID="txtOfficeComments" ForeColor="Blue" TextMode="MultiLine" Width="99%" Height="380px" runat="server" ></asp:TextBox>
                                        
						            <div class="hdf1">Updated By/On : <asp:Label ID="lblUpdatedByOn" ForeColor="Blue" runat="server"></asp:Label></div>
                                    <div style="text-align:center; padding:5px;">
                                     <asp:Button ID="Button9" runat="server" CssClass="btn" Text="<< Pre"  CommandArgument="10" OnClick="btnPrevious_Click" />                                    
                                     <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="VG" OnClick="btnSave_Click" style="width:100px;" />
                                     <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export" OnClick="btnExport_Click" style="width:100px;" Visible="false" OnClientClick="this.value='Loading..';" />
                                     <br />
                                     <asp:Label ID="lblMsg_10" ForeColor="Red" runat="server" ></asp:Label>
                                    </div>
                                </div>

						        
						        <!--DSUPTD-->
						        <div id="DSUPTD" runat="server" visible="false">
						            <table border="0" width="99%" cellpadding="2" cellspacing="0" >
						                <tr>
						                    <td style="font-size:15px;" align="center">
                                                Safety meeting held by Superintendent : Attending Superintendent would hold a 
                                                safety meeting on board and discuss following topics (elaborate on each item):
                                                <br />
						                    </td>
						                </tr>
						            </table>
						            <div style="overflow-x:hidden;overflow-y:scroll; height:450px">
                                        <table border="0" width="99%" cellpadding="4" cellspacing="0" >
						                <tr>
						                    <td >
                                                1) Compliance with Regulations and Standards 
                                                <textarea id="lblSUPTDComm1" style="width:99%; height:80px;" ></textarea>
						                    </td>
						                </tr>
						                <tr>
						                    <td >
                                                2) Deviations from safety procedures and safety routines observed during the 
                                                visit 
                                                <textarea id="lblSUPTDComm2" style="width:99%; height:80px;"></textarea>
						                    </td>
						                </tr>
						                <tr>
						                    <td >
                                                3) Details of Training Conducted during the visit. 
                                                <textarea id="lblSUPTDComm3" style="width:99%; height:80px;" ></textarea>
						                    </td>
						                </tr>
						                <tr>
						                    <td >
                                                4) Health, Safety and Welfare Measures. 
                                                <textarea id="lblSUPTDComm4" style="width:99%; height:80px;" ></textarea>
						                    </td>
						                </tr>
						                
						                <tr>
						                    <td >
                                                5) Suggestions for Improvement. 
                                                <textarea  id="lblSUPTDComm5" style="width:99%; height:80px;"></textarea>
						                    </td>
						                </tr>
						                <tr>
						                    <td >
                                                6) Any other topic as deemed necessary. 
                                                <textarea  id="lblSUPTDComm6" style="width:99%; height:80px;" ></textarea>
						                    </td>
						                </tr>
						            </table>
						            </div>

						        </div>
						        
						        <!--Home-->
						        <div id="DHome" runat="server" >
                                <div style='padding:4px; background:#eeeeee; color:Red;'>&nbsp;Please remember to update your Rest Hours with time spent in this Safety Committe Meeting.</div>
						            <table cellpadding="0" cellspacing="0" border="1" width="100%">
						                <col width="60%" />
						                <col />
						                <tr>
						                    <td style="vertical-align:top">
                                               <div class="hdf1">Safety Committee Attendance List</div>
                                               <div style="overflow-x:hidden;overflow-y:scroll; height:400px;"> 
						                            <table border="1" width="100%" cellpadding="2" cellspacing="0" style="border-collapse:collapse; color:Black;" >
						                            <col width="25%" />
						                            <col width="35%" />
						                            <col />
						                            <tr class= "headerstylegrid">
                                                        <td style="text-align:center; height:25px;">Rank</td>
                                                        <td style="text-align:center;"> Name</td>
                                                        <td style="text-align:center;">Designation</td>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="rptPresent">
                                                        <ItemTemplate>
                                                        <tr >
                                                             <td>
                                                                <asp:Label ID="lblRank" Text='<%#Eval("RANK")%>' runat="server" ></asp:Label>
                                                             </td>
                                                            <td>
                                                                <asp:Label ID="lblName" Text='<%#Eval("NAME")%>' runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDesignation" Text='<%#Eval("DESGINATION")%>' runat="server"></asp:Label>
                                                            </td>      
                                                        </tr>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </td>
						                    <td>
						                       <div class="hdf1">Absentees List</div>
                                               <div style="overflow-x:hidden;overflow-y:scroll; height:400px;"> 
						                            <table border="1" width="100%" cellpadding="2" cellspacing="0" style="border-collapse:collapse; color:Black;" >			                            
						                            <col width="185px" />
						                            <col />
                                                    <col width="17px" />
						                           <tr class= "headerstylegrid">
                                                        <td style="height:25px;">Rank</td>
                                                        <td>Name</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="rptAbsent">
                                                        <ItemTemplate>
                                                        <tr >
                                                             <td>
                                                                <asp:Label ID="lblRank" Text='<%#Eval("RANK")%>' runat="server" ></asp:Label>
                                                             </td>
                                                            <td>
                                                                <asp:Label ID="lblName" Text='<%#Eval("NAME")%>' runat="server" />
                                                            </td>
                                                            <td>&nbsp;</td>      
                                                        </tr>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
						                    </td>
						                </tr>
						            </table>
                                     <div style="text-align:center; padding:5px;">
                                     <asp:Button ID="btnNextHome" runat="server" CssClass="btn" Text="Next >>" ValidationGroup="VG"  CommandArgument="9" OnClick="btnNext_Click" />               
                                     <br />
                                     <asp:Label ID="lblMsg" ForeColor="Red" runat="server" ></asp:Label>
                                     </div>
						        </div>

                                
						    </td>
						</tr>
					</table>
				</div>        
	</div>
	</form>
</body>
</html>



