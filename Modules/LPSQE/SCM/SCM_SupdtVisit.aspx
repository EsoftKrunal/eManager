<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCM_SupdtVisit.aspx.cs" Inherits="VIMS_SCM_SupdtVisit" %>
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
<script type="text/javascript">
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
</script>
 </head>
<body >
<form name="form1" runat="server"  > 
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
	<div style="border :solid 2px #4371a5; width:99%;"  >
		<table width="100%" cellpadding="0" cellspacing="0" border="0" >
		    <tr>
		        <td colspan="1" class="text headerband" style=" height:35px; text-align:center;">
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
				            <col width="60px;" />
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
				                    <%--<span style="font-size:15px; color:Red;padding:4px;">*</span>--%>
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
								    <%--<span style="font-size:15px; color:Red;padding:4px;">*</span>--%>
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
								    <%--<span style="font-size:15px; color:Red;padding:4px;">*</span>--%>
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
                                        <td style="width:100px">
							                <asp:Button ID="btnHome" Text="Crew List" CssClass="color_tab_sel" CommandArgument="1"  OnClick="btnMenu_Click" runat="server" />    
							            </td>
							           <%-- <td>
							                <asp:Button ID="btnMGT1" CssClass="color_tab" runat="server" Text=" Start " CommandArgument="1"  OnClick="btnMenu_Click" /> <!--SetColor(1,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT2" Text=" Safety "  CssClass="color_tab" runat="server" CommandArgument="2"  OnClick="btnMenu_Click" /> <!--SetColor(2,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT3" Text=" Health " CssClass="color_tab" runat="server" CommandArgument="3"  OnClick="btnMenu_Click" />  <!--SetColor(3,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT4" Text="Security " CssClass="color_tab" runat="server" CommandArgument="4"  OnClick="btnMenu_Click" /> <!--SetColor(4,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT5" Text="Quality" CssClass="color_tab" runat="server" CommandArgument="5"  OnClick="btnMenu_Click" /> <!--SetColor(4,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT6" Text="Environment" CssClass="color_tab" runat="server" CommandArgument="6"  OnClick="btnMenu_Click"/> <!--SetColor(4,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT7" Text="AOB" CssClass="color_tab" runat="server" CommandArgument="7"  OnClick="btnMenu_Click"/> <!--SetColor(4,'M')-->
							            </td>
							            <td>
							                <asp:Button ID="btnMGT8" Text="SUPTD" CssClass="color_tab" runat="server" Visible="false"  CommandArgument="8"  OnClick="btnMenu_Click" /> <!--SetColor(4,'M')-->
							            </td>
                                        <td>
							                <asp:Button ID="btnMGT10" Text="Best Practice" CssClass="color_tab" runat="server" CommandArgument="10"  OnClick="btnMenu_Click"/> <!--SetColor(4,'M')-->
							            </td>--%>
                                        <td style="width:100px">
							                <asp:Button ID="btnSCMAgenda" Text="SCM Agenda" CssClass="color_tab" CommandArgument="2"  OnClick="btnMenu_Click" runat="server" Visible="false" /> 
							            </td>
                                        <td style="text-align:right">
                                             <asp:LinkButton ID="ImgAttachment" runat="server" Text="View SCM Report" Visible="false" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" style="padding-right:5px;font-family:Arial, Helvetica, sans-serif;font-size:medium;" />

                                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn" style="font-weight:bold; font-size:13px; padding:4px;width:70px;" OnClick="btnPrint_OnClick" />
                                        </td>
							        </tr>
							    </table>
        </div>
        <div style="height:10px; background-color:#facc8c;"></div>
        <div id="DMember" style="vertical-align:top;" >
					<table width="100%" cellpadding="0" cellspacing="0" border="0">
						<tr>
						    <td style="vertical-align:top;">
						        <!--Home-->
						        <div id="DHome" runat="server" >
						            <table cellpadding="0" cellspacing="0" border="1" width="100%">
						                <col width="60%" />
						                <col />
						                <tr>
						                    <td style="vertical-align:top">
                                               <div class="hdf1">Safety Committee Attendance List</div>
                                               <div style="overflow-x:hidden;overflow-y:hidden; height:400px;"> 
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
                                               <div style="overflow-x:hidden;overflow-y:hidden; height:400px;"> 
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
						             <div style='padding:4px; background:#eeeeee; color:Red;'>&nbsp;Please remember to update your Rest Hours with time spent in this Safety Committe Meeting.</div>
                                     <div style="text-align:center; padding:5px;">
                                       <asp:Button ID="btnImportCrew" Text="Import Crew List" CssClass="btn" ValidationGroup="VG"  OnClick="btnImportCrew_Click" runat="server" />
                                     <asp:Button ID="btnNextHome" runat="server" CssClass="color_tab" Text="Next >>" ValidationGroup="VG"  CommandArgument="1" OnClick="btnNext_Click" Visible="false" />
                                          <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="VG" OnClick="btnSave_Click" style="margin-right:10px; width:100px;" />
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export" OnClick="btnExport_Click" style="width:100px;" Visible="false" />
                                     <br />
                                     <asp:Label ID="lblMsgHome" ForeColor="Red" runat="server" ></asp:Label>
                                     </div>               
						        </div>
                                <%--</div>--%>

                                <!--SCM Agenda-->
						        <div id="DSUPTD" runat="server" visible="false">
						            <div style="overflow-x:hidden;overflow-y:scroll; height:400px">
                                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Compliance with Regulations and Standards :  </b>
                                        <asp:TextBox ID="txtSUPTD_CompliancewithRegulations" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Deviations from safety procedures and safety routines observed during the visit :  </b>
                                        <asp:TextBox ID="txtSUPTD_DeviationsfromSafety" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Details of Training Conducted during the visit. :  </b>
                                        <asp:TextBox ID="txtSUPTD_DetailsofTraining" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Health, Safety and Welfare Measures :  </b>
                                        <asp:TextBox ID="txtSUPTD_HealthSafetyMeasures" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" runat="server" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Suggestions for Improvement :  </b>
                                        <asp:TextBox ID="txtSUPTD_Suggestions" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Best Practice Identified :  </b>
                                        <asp:TextBox ID="txtSUPTD_BPI" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Any other issues :  </b>
                                        <asp:TextBox ID="txtSUPTD_AnyOtherTopic" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                        </table> 
						            </div>
                                    <div style="text-align:center; padding:5px;">
                                    <asp:Button ID="btnPre" runat="server" CssClass="color_tab" Text="<< Pre"  CommandArgument="1" OnClick="btnPrevious_Click" />
                                   <%-- <asp:Button ID="btnSave" runat="server" CssClass="color_tab" Text="Save" ValidationGroup="VG" OnClick="btnSave_Click" style="margin-right:10px; width:100px;" />
                                    <asp:Button ID="btnExport" runat="server" CssClass="color_tab" Text="Export" OnClick="btnExport_Click" style="width:100px;" Visible="false" />--%>
                                    <br />
                                    <asp:Label ID="lblMsgSCM" ForeColor="Red" runat="server" style="float:left; margin:5px;"></asp:Label>
                                    </div>

						        </div>
						   </td>
						</tr>
					</table>
				</div>        
	</div>

    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_ImportCrew" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:850px;  height:395px;padding :5px; text-align :center;background : white; z-index:150;top:100px; border:solid 10px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Import Crew from Rest Hour</b></div>
             <div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 0px #00ABE1;">
                <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#dddddd; border-collapse:collapse;" >
                <thead>
                <tr style='color:White; height:25px; color:Black;'>
                        <td style="width:30px;"><b>&nbsp;</b></td> 
                        <td style="width:20px;"><b>&nbsp;</b></td>
                        <td style="width:225px;"><b>Rank</b></td>
                        <td style="width:350px;"><b>Name</b></td>
                        <td style=""><b>Designation</b></td>
                        <td style="width:20px;"><b>&nbsp;</b></td>
                </tr>
                </thead>
                </table>
                </div>
                <div style="height:250px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_SCM_List'>
                    <table width="100%" border="1" cellpadding="3" cellspacing="0" style="border-collapse:collapse;" >
                     <tbody>
                    <asp:Repeater runat="server" ID="rptCrewList">
                    <ItemTemplate>
                    <tr onmouseover="">
                          <td style="width:30px;"><asp:CheckBox runat="server" ID="chkSel" /></td>
                          <td style="width:20px;"><asp:Image ID="imgRHReq" ImageUrl="~/Images/red_circle.png" ToolTip="Rest Hour Conflict" Visible='<%#(Eval("RHReq").ToString()!="0")%>' runat="server" /></td>
                          <td style="width:225px; text-align:left;"><asp:Label ID="lblRank" Text ='<%#Eval("RANK")%>'  runat="server" /></td>
                           <td style="width:350px; text-align:left;"><asp:Label ID="lblHName" Text ='<%#Eval("Name")%>' Visible='<%#(Eval("RANKLEVEL").ToString()!="-1")%>'  runat="server" />
                                <asp:TextBox runat="server" ID="txt_SName" Width="95%" MaxLength="50" Visible='<%#(Eval("RANKLEVEL").ToString()=="-1")%>'></asp:TextBox>
                           </td>
                          <td>
                              <asp:Label ID="lblDesignation" Text ='<%#Eval("SEL")%>' Visible='<%#(Eval("SHOW").ToString()=="N")%>'  runat="server" /> 
                              <asp:DropDownList ID="ddlHDesignation" runat="server"  Visible='<%#(Eval("SHOW").ToString()=="Y")%>'>
                                <asp:ListItem Text="SELECT" Value=""></asp:ListItem>
                                <asp:ListItem Text="Safety Officer" Value="Safety Officer"></asp:ListItem>
                                <asp:ListItem Text="Committee Member" Value="Committee Member"></asp:ListItem>
                                <asp:ListItem Text="Elected Rep.(Officer)" Value="Elected Rep.(Officer)"></asp:ListItem>
                                <asp:ListItem Text="Elected Rep.(Rating)" Value="Elected Rep.(Rating)"></asp:ListItem>
                                <asp:ListItem Text="Attendee" Value="Attendee"></asp:ListItem>
                                </asp:DropDownList>
                          
                          </td>
                          <td style="width:20px;"><b>&nbsp;</b></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
                    </div>                            
             </center>
             <br />
             <asp:Button runat="server" ID="btnImport" Text="Import" onclick="btnImport_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:100px;"/>
             <asp:Button runat="server" ID="btnCloseImport" Text="Cancel" OnClick="btnCloseImport_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
             <br />
             <asp:Label ForeColor="Red" runat="server" ID="lblMsg_Imp" ></asp:Label> 
        </div>
    </center>
    </div>
	</form>
</body>
</html>



