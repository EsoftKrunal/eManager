<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_SCM.aspx.cs" Inherits="HSSQE_Add_SCM" Title="Add SCM" %>

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
        
        input:focus
        {
        	background-color:#fafad2;
        	
        }
        textarea
        {
        	border:solid 1px gray;
        	height:30px;
        }
        textarea:focus
        {
            border:solid 1px gray;
        	background-color:#fafad2;
        	height:180px;
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
        input[type=checkbox]
        {
            background-color:lightgreen;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="UP1"  runat="server" >
        <ContentTemplate>--%>
            
            <div>
            <center>
              <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center; background-color: #f9f9f9">
            <tr>
                <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" class="text">
                    Safety Committee Meeting
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="1" cellspacing="1" border="0" width="100%">
                        <colgroup>
                            <col width="100px"  />
                            <col width="150px" />
                            <col width="120px" />
                            <col width="100px" />
                            <col width="120px" />
                            <col width="300px" />
                            <col width="150px" />
                            <col />
                            <tr>
                                <td style="text-align:right;">
                                    Vessel :</td>
                                <td style="text-align:left;">
                                    <asp:DropDownList runat="server" ID="ddlVessel" CssClass="required_box" Width='150px'></asp:DropDownList>
                                </td>
                                <td style="text-align:right;">
                                    Date :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox ID="txtRDate" runat="server" CssClass="required_box" 
                                        MaxLength="15" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" ID="sdf" TargetControlID="txtRDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="text-align:right;">
                                    Ship&#39;s Position :</td>
                                <td style="text-align:left;">
                                    <asp:DropDownList ID="ddlShipPosition" runat="server" CssClass="required_box" 
                                        Width="100px">
                                        <asp:ListItem Text="AT SEA" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="IN PORT" Value="P"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:right;">
                                    Time Commenced :
                                    
                                </td>
                                <td style="text-align:left;">
                                    <asp:DropDownList runat="server" ID="ddlCommTime1" CssClass="required_box" Width='50px'></asp:DropDownList> : <asp:DropDownList runat="server" ID="ddlCommTime2" CssClass="required_box" Width='50px'></asp:DropDownList>
                                    <span style="font-size:8px; font-style:italic;color:Black;">(Local Time)</span>
                                </td>
                                <td rowspan="2">
                                    <table cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td>
                                             <%--   <asp:Button ID="btnPrint" runat="server" CssClass="btn" 
                                                    OnClick="btnPrint_OnClick" 
                                                    style="font-weight:bold; font-size:13px; padding:4px;width:70px;" 
                                                    Text="Print" />--%>

                                                    <asp:Button ID="Button1" runat="server" OnClick="btnOpenImport_OnClick" Text="Import CrewList" style="float:right; padding:5px; margin-top:4px; width:120px;"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    Occasion :</td>
                                <td style="text-align:left;">
                                    
                                    <asp:Label ID="lblOccasion" runat="server" class="vcolor"></asp:Label>
                                    
                                </td>
                                <td style="text-align:right;">
                                    &nbsp;</td>
                                <td style="text-align:left;">
                                    &nbsp;</td>
                                <td style="text-align:right;">
                                Voy From/To :
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtVoyFrom" CssClass="input_box" Width="120px" MaxLength="50"></asp:TextBox> -
                                    <asp:TextBox runat="server" ID="txtVoyTo" CssClass="input_box" Width="120px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="text-align:right;">
                                    Time Completed :</td>
                                <td style="text-align:left;">
                                    <asp:DropDownList runat="server" ID="ddlCompTime1" CssClass="required_box" Width='50px'></asp:DropDownList> : <asp:DropDownList runat="server" ID="ddlCompTime2" CssClass="required_box" Width='50px'></asp:DropDownList>
                                    <span style="font-size:8px; font-style:italic; color:Black;">(Local Time)</span>
                                </td>
                            </tr>
                        </colgroup>
                    </table>
                    <hr />
                </td>
            </tr>
            <tr>
            <td>
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" 
                    CssClass="ajax__myTab" Height="500px" Width="100%">
                    <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="Crew List">
                    <ContentTemplate>
                        <div style="float:left; width:610px;">
                            <table cellpadding="4" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="width:800px;border-right:solid 1px gray;border-left:solid 1px gray;">
                                            <table cellspacing="0" rules="cols" border="0" id="ctl00_ContentPlaceHolder1_grd_Data" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                                                <colgroup>
                                                    <col width="150px" />
                                                    <col width="250px" />
                                                    <col />
                                                    <col width="17px" />
                                                    <tr class="headerstyle">
                                                        <td colspan="4" style="text-align:center;">
                                                            <b>Safety Committee Attendance List</b>
                                                        </td>
                                                    </tr>
                                                    <tr ID="tr1" class="headerstyle">
                                                        <th>
                                                            Rank Name</th>
                                                        <th>
                                                            Crew Name</th>
                                                        <th>
                                                            Remark</th>
                                                        <th>
                                                        </th>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            </div>
                                        <div style="height :450px; overflow-x:hidden;overflow-y:scroll;width:800px;border:solid 1px gray;border-top:none;">
                                                <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table1" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> 
                                                    <colgroup>
                                                        <col width="150px" />
                                                        <col width="250px" />
                                                        <col />
                                                        <col width="17px" />
                                                    </colgroup>
                                                <asp:Repeater ID="rptAttendeeRank" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("RankName")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Name")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Remarks")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </table>
                                                </div>
                                    </td>
                                    <td>
                                        <div style="width:550px;border-right:solid 1px gray;border-left:solid 1px gray;">
                                            <table cellspacing="0" rules="cols" border="0" id="Table11" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                                                <colgroup>
                                                    <col width="200px" />
                                                    <col  />
                                                    <col width="17px" />
                                                    <tr class="headerstyle">
                                                        <td colspan="3" style="text-align:center;">
                                                            <b>Absentee List </b>
                                                        </td>
                                                    </tr>
                                                    <tr ID="tr5" class="headerstyle">
                                                        <th>
                                                            Rank Name</th>
                                                        <th>
                                                            Crew Name</th>
                                                        <th>
                                                        </th>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                        </div>            
                                        <div style="height :450px; overflow-x:hidden;overflow-y:scroll;width:550px;border:solid 1px gray;border-top:none;">
                                                <table cellspacing="5" cellpadding="1" rules="all" border="1" id="Table12" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> 
                                                    <colgroup>
                                                        <col width="200px" />
                                                        <col />
                                                        <col width="17px" />
                                                    </colgroup>
                                                <asp:Repeater ID="rptAbsenteeRank" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("RankName")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Name")%>
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
                    </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanel10" runat="server" HeaderText="SCM Agenda">
                    <ContentTemplate>
                    <div style="float:left; width:100%; overflow-X:hidden; overflow-Y:scroll; height:500px;" >
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <b>Compliance with Regulations and Standards :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_CompliancewithRegulations" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Deviations from safety procedures and safety routines observed during the visit :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_DeviationsfromSafety" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Details of Training Conducted during the visit. :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_DetailsofTraining" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Health, Safety and Welfare Measures :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_HealthSafetyMeasures" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" runat="server" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Suggestions for Improvement :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_Suggestions" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Best Practice Identified :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_BPI" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Any other issues :  </b>
                                        <%--<asp:Label ID="" runat="server"></asp:Label>--%>
                                        <asp:TextBox ID="txtSUPTD_AnyOtherTopic" runat="server" onfocus="javascript:this.style.height='200px';" onblur="javascript:this.style.height='30px';" TextMode="MultiLine" style="width:100%;" ></asp:TextBox>
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
            <div style=" text-align:left">
                <asp:Label runat="server" ID="lblSaveMSG" ForeColor="Red" style="font-size:15px; padding-right:5px;" ></asp:Label> 
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="Save" style="float:right; padding:5px; margin-top:4px; width:100px;"/>
            </div>
            </center>                 
            </div>   
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:101;" runat="server" id="dvImport" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:101; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:800px; height:600px; padding :0px; text-align :center; border :solid 10px #4371a5; background : white; z-index:152;top:30px;opacity:1;filter:alpha(opacity=100)">
                <div style=" padding:8px; font-size:16px; background-color:#e2e2e2"><b>Safety Committee Attendee List</b></div>
                <div style="height :33px; overflow-x:hidden;overflow-y:scroll;width:800px;border:solid 1px gray;border-top:none;">
                <table cellspacing="0" cellpadding="3"  rules="all" border="1" id="Table2" style="width:100%;border-collapse:collapse;border:solid 1px #4371a5;">
                    <colgroup>
                        <col width="20px" />
                        <col width="180px" />
                        <col width="300px" />
                        <col />
                        <col width="17px" />
                    </colgroup>    
                    <tr >
                      
                    </tr>
                    <tr class="headerstyle">
                        <th></th>
                        <th>
                            Rank Name</th>
                        <th>
                            Crew Name</th>
                        <th>
                            Remark</th>
                        <th></th>
                    </tr>
                </table>
                </div>
                <div style="height :490px; overflow-x:hidden;overflow-y:scroll;width:800px;border:solid 1px gray;border-top:none;">
                <table cellspacing="0" cellpadding="3" rules="all" border="1" id="Table3" style="width:100%;border-collapse:collapse;border:solid 1px Gray;"> 
                    <colgroup>
                        <col width="20px" />
                        <col width="180px" />
                        <col width="300px" />
                        <col />
                        <col width="17px" />
                    </colgroup>
                <asp:Repeater ID="rptImport" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkSelect" Checked="true"/>
                                <asp:HiddenField runat="server" ID="hfd_RankName" Value='<%#Eval("RankName")%>'/>
                                <asp:HiddenField runat="server" ID="hfd_Name" Value='<%#Eval("Name")%>'/>
                                <asp:HiddenField runat="server" ID="hfd_Remarks" Value='<%#Eval("Remarks")%>'/>
                            </td>
                            <td>
                                <%#Eval("RankName")%>
                            </td>
                            <td>
                                <%#Eval("Name")%>
                                <asp:TextBox runat="server" ID="txt_SName" CssClass="required_box" Width="95%" MaxLength="50" Visible='<%# Common.CastAsInt32(Eval("RANKID"))==-1 %>'></asp:TextBox>
                            </td>
                            <td>
                                <%#Eval("Remarks")%>
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="input_box" Width="95%" MaxLength="50" Visible='<%# Common.CastAsInt32(Eval("RANKID"))>2 %>'></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
                </div>
               <div>
               <asp:Label runat="server" ID="lblMessage" ForeColor="Red" style="float:right; font-size:15px; padding-right:5px;" ></asp:Label> 
               <asp:Button ID="Button2" runat="server" OnClick="btnImport_OnClick" Text="Import" style="float:right; padding:5px; margin-top:4px; width:100px;"/>
               <asp:Button ID="Button3" runat="server" OnClick="btnCancel_OnClick" Text="Cancel" style="float:right; padding:5px; margin-top:4px; width:100px; margin-right:5px;"/>
               </div>
               
            </div>
            </center>
            
            </div>
 <%--       </ContentTemplate>
     </asp:UpdatePanel>--%>
    </form>
</body>
</html>
