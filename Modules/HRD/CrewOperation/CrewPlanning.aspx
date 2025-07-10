<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="CrewPlanning.aspx.cs" Inherits="CrewOperation_CrewPlanning" %>

    <style type="text/css">
       .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
           text-align:center;
       }
       .Grade_B 
       {
           background:yellow; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
           text-align:center;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
           text-align:center;
       }
       .Grade_D 
       {
           background:red; 
           width:15px;
           height:15px;
           color:white;
           border:solid 1px grey;
           text-align:center;
       }
       .headertable tr td
       {
           
           border:solid 1px #e9e9e9;
           padding:2px;
                  }
       
    </style>
    <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
	
}
.btn1
{
	   background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
 <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<!--  Sign Off Region -->
  <body>
    <form id="frm" runat="server">  
<div>
   <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <div  >
         <table cellpadding="5" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;font-family:Arial;font-size:12px;">
             <tr>
                 <td style="text-align:left"> <asp:CheckBox runat="server" ID="chkTop" Text="Top 4 Ranks" AutoPostBack="false" OnCheckedChanged="btnSearchSignOff_Click" /> </td>
                 <td style="text-align:right">Off / Crew :</td>
                 <td style="text-align:left">
                      <asp:DropDownList runat="server" ID="ddlOR" AutoPostBack="false" OnSelectedIndexChanged="btnSearchSignOff_Click" >
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Officer" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Rating" Value="R"></asp:ListItem>
                    </asp:DropDownList>
                 </td>
                 <%--<td style="text-align:right">Fleet :</td>--%>
                  <td style="vertical-align:top; text-align:left;">
                            <asp:DropDownList ID="ddlFleet" runat="server"   Width="120px" AutoPostBack="true" OnSelectedIndexChanged="FleetOwner_Changed">
                            </asp:DropDownList>
                         </td>
                <%-- <td style="text-align:right">Owner :</td>--%>
                    <td style="vertical-align:top; text-align:left;">
                        <asp:DropDownList ID="ddlOwner" runat="server"   Width="140px" AutoPostBack="true" OnSelectedIndexChanged="FleetOwner_Changed">
                        </asp:DropDownList>
                    </td>
                
                     <td style="vertical-align:top; text-align:left;">
                        <asp:DropDownList ID="ddlOffice" runat="server"   Width="80px" AutoPostBack="true" OnSelectedIndexChanged="FleetOwner_Changed">
                        </asp:DropDownList>
                    </td>
                  
                 <%--<td>Vessel :</td>--%>
                    <td style="vertical-align:top; text-align:left;">
                        <asp:DropDownList ID="ddlVessel" runat="server"   SelectionMode="Multiple" Width="180px"></asp:DropDownList>

                        </td>
                 <td>
                     <asp:DropDownList ID="ddlsRank" runat="server" >                         
                     </asp:DropDownList>
                 </td>
                 <td>Relief Due In <asp:TextBox ID="txtReliefDueInDays" runat="server" Width="35px" MaxLength="3" CssClass="NumberValidation"></asp:TextBox> Days :</td>
                 <td>
                     <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_OnClick" CssClass="btn" />
                     <%--<input type="button" value="Print" onclick="window.open('../Reporting/CrewPlanning.aspx');" />--%>
                     <asp:Button ID="btnOpenPrintPopup" runat="server" Text="Print" CssClass="btn" OnClick="btnOpenPrintPopup_OnClick" />
                     <%--<a href="../Reporting/CrewPlanning.aspx" target="_blank"> Print</a>--%>
                 </td>
             </tr>
             </table>
              

    </div>
      <div style="height:30px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
           <table cellpadding="2" cellspacing="0" width="100%" border="0" class="headertable" style="border-collapse:collapse; font-size:10px; height:30px">
                            <tr class= "headerstylegrid"> 
                                <td style="width:35px; ">VSL</td>
                                <td style="width:45px; " >Crew#</td>
                                <td style="text-align:left; " >Off Signer Name</td>
                                <td style="width:30px; " >Rank</td>
                                <td style="width:35px; " >Nat.</td>
                                <td style="width:80px; " >Sign On Dt.</td>
                                <td style="width:80px;  border-right:solid 1px black;" >Rel Due Dt.</td>

                                <td style="width:50px;">Crew#</td>
                                <td style="text-align:left;width:200px; ">Reliever Name</td>
                                <td style="width:40px; ">Rank</td>
                                <td style="width:35px; ">Nat.</td>
                                <td style="width:50px; ">Last VSL</td>
                                <td style="width:50px; ">Grading</td>
                                <td style="width:50px; ">Checklist</td>
                                <td style="width:60px;border-right:solid 1px black; ">Approval</td>
                                <td style="width:40px; ">View</td>
                            </tr>
               </table>
          </div>
            <div style="height:420px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
            <table cellpadding="2" cellspacing="0" width="100%" border="0" class="headertable" style="border-collapse:collapse; font-size:10px;">
                        <asp:Repeater runat="server" ID="rpt_SignOffList">
                        <ItemTemplate>
                            <tr>
                                <td style="width:35px; ">
                                   <b> <%#Eval("PLANVESSELCODE") %></b>
                                    <img src="../Images/aeroplane.png" style="margin-left:20px;" runat="server"  visible='<%#(Convert.ToBoolean(Eval("IsVacancy"))) %>' />
                                    <asp:HiddenField ID="hfdVesselId" runat="server" Value='<%#Eval("planVESSELID") %>' />
                                    <asp:HiddenField ID="hfdSignOffId" runat="server" Value='<%#Eval("OFF_CREWID") %>' />
                                    <asp:HiddenField ID="hfdSignOnId" runat="server" Value='<%#Eval("ON_CREWID") %>' />
                                </td>
                                <td style="width:45px;"><%#Eval("OFF_CrewNumber") %>
                                    <%--<%#Eval("Crit_Remark")%>--%>

                                </td>
                                <td style="text-align:left;"><%#Eval("OFF_CrewName") %></td>
                                <td style="width:30px;"><%#Eval("OFF_RankCODE") %></td>
                                <td style="width:35px;"><%#Eval("OFF_COUNTRYNAME") %></td>
                                <td style="width:80px;" ><%#Common.ToDateString(Eval("SignOnDate"))%></td>
                                <td style='width:80px; border-right:solid 1px black;background-color:<%#Eval("COLOR")%>'><%#Common.ToDateString(Eval("ReliefDueDate"))%> </td>
                                
                                <td style="width:50px;"><%#Eval("ON_CrewNumber") %></td>
                                <td style="text-align:left;width:200px;"><%#Eval("ON_CrewName")%></td>
                                <td style="width:40px;"><%#Eval("ON_RankCODE")%></td>
                                <td style="width:35px;"><%#Eval("ON_COUNTRYNAME")%></td>
                                <td style="width:50px;"><%#Eval("ON_VesselCODE")%></td>
                                <td style="width:50px;"><span class='Grade_<%#Eval("Grading")%>'></span><%#Eval("Grading")%></td>
                                <td style="width:50px; text-align:center;">
                                    <div runat="server" visible='<%#(Eval("ON_CrewNumber").ToString()!="") %>'>
                                    <img src="../Images/icon_note.png" onclick='<%#"OpenCheckList(" + Eval("PlanningId").ToString() + ");"%>' title="Open Document CheckList"/>
                                        </div>
                                </td>
                                 <td style="width:60px;border-right:solid 1px black;"><%#(Eval("APP_STATUS").ToString()=="A")?"Approved":((Eval("APP_STATUS").ToString()=="R")?"Rejected":((Eval("APP_STATUS").ToString()=="N")?"Pending":""))%></td>
                              
                                <td style="width:40px;">
                                    <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" OnClick="OpenDetails_Click" style="width:15px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="color:#FA5858;" >Rem:</td>
                                <td colspan="6" style="color:#FA5858; text-align:left;border-right:solid 1px black;"><i><%#Eval("Off_Plan_Remarks")%></i></td>
                                <td colspan="6" style="color:#FA5858; text-align:left;"><i><%#Eval("On_Plan_Remarks")%></i></td>
                                <td>&nbsp;</td>
                                <td style="border-right:solid 1px black;">&nbsp;</td>
                                <td style="width:40px;">&nbsp;</td>
                            </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
              
        <script type="text/javascript">
            function selectDeselect(listbox, checkbox) {
                if (checkbox.checked) {
                    var multi = document.getElementById(listbox.id);
                    for (var i = 0; i < multi.options.length; i++) {
                        multi.options[i].selected = true;
                    }
                } else {
                    var multi = document.getElementById(listbox.id);
                    multi.selectedIndex = -1;
                }
            }
            function SetRank() {
                selectDeselect( document.getElementById("ctl00_contentPlaceHolder1_chkrank"),document.getElementById("chkallrank0"));
            }
            function SetVessel() {
                selectDeselect( document.getElementById("ctl00_contentPlaceHolder1_chkvessel"),document.getElementById("chkallvsl"));
            }
            function OpenCheckList(PlanningId) {
                window.open('ViewCrewCheckList.aspx?_P=' + PlanningId);
            }
            
        </script>
       
</div>
<!--  Sign On Region -->

<div>
    <%------------------------------------------------------%>
    <div style="position:absolute;top:40px;left:0px; height :470px; width:100%;" id="divPrintPopup" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:500px; padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
                <%--<asp:ScriptManager ID="ddd" runat="server"></asp:ScriptManager>--%>

            <%--<asp:UpdatePanel ID="UP1" runat="server">
                <ContentTemplate>--%>

                
            <table cellpadding="15" cellspacing="15">
                <tr>
                    <td>
                        <asp:RadioButton ID="radAll" runat="server" Text="All" GroupName="radFilter" AutoPostBack="true"  OnCheckedChanged="FilterRadios_OnCheckedChanged"></asp:RadioButton>
                        <asp:RadioButton ID="radAssignedtoManningUser" runat="server" Text="Manning Agent"  GroupName="radFilter" AutoPostBack="true" OnCheckedChanged="FilterRadios_OnCheckedChanged" ></asp:RadioButton>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPrintByManningUser" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPrintByManningUser_OnSelectedIndexChanged" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
               
            </table>
            <div>
                <%--<input type="button" value="Print" onclick="window.open('../Reporting/CrewPlanning.aspx');" />--%>
                <asp:Button ID="btnGoToPrint" runat="server" Text="Print" OnClick="btnGoToPrint_OnClick" />
                <asp:Button ID="btnClosePrintPopup" CausesValidation="false" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnClosePrintPopup_Click" />  
                <asp:Label ID="lblMsgPrint" runat="server" ForeColor="Red"></asp:Label>
          
            </div>
                    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </center>
    </div>
    <%------------------------------------------------------%>

<div style="position:absolute;top:40px;left:0px; height :470px; width:100%; display:none; " id="dv_SignOn" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
            <center >
            <iframe id="frmSignOn" runat="server" src="" height="440px" width="100%" scrolling="no" frameborder="0">

            </iframe>
            <asp:Button ID="btn_Close_Search" CausesValidation="false" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btn_Close_Search_Click" />
            </center>
        </div>
    </center>
</div>
</div>
    <!--  Add Signer Details -->
    <div style="position:absolute;top:40px;left:0px; height :510px; width:100%;" id="dvAddSignerDetails" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:98%; padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
            <center >
                 <asp:Button ID="btnCloseAddSignerDiv" CausesValidation="false" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnCloseAddSignerDiv_Click" />
            <iframe id="ifmAddSignerDetails" runat="server" src="" height="550px" width="100%" scrolling="no" frameborder="0">

            </iframe>
           
            </center>
        </div>
    </center>
</div>

    <!--  Sign On Region -->
    <div style="position:absolute;top:40px;left:25px; height :470px; width:85%;font-family:Arial;font-size:12px;" id="divSignOnOff" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:40px;left:25px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:950px; left:25px;padding :0px; text-align :center;background : white; z-index:150;top:40px; border:solid 2px #c2c2c2;">
            <center >   
                <div class="OffSignerDetails" runat="server" id="dvOffSignerDetails">
                    <div style="font-size:15px;color:#206020;text-align:left; padding:10px; font-weight:bold;"> 
                        
                        <div style="text-align:left; float:right;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                <table border="0" runat="server" id="box_assign">
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkAssigntoManing" AutoPostBack="true" Font-Bold="true" Text="Assign this slot to Manning Agent" OnCheckedChanged="chkAssigntoManing_OnCheckedChanged" ForeColor="#206020"/></td>
                                        <td>
                                            <asp:LinkButton ID="lbAddVacancy" runat="server" Text="Add Vacancy" OnClick="lbAddVacancy_Click" ForeColor="Green" Visible="false"></asp:LinkButton>
                                            <%--<asp:DropDownList runat="server" ID="ddlManningUser" Visible="false" ValidationGroup="v002"> </asp:DropDownList>--%>
                                        </td>
                                        <td>
                                          <%--  <asp:Button ID="btnAssigntoManning" runat="server" OnClick="btnAssigntoManning_OnClick" Text="OK" Visible="false" CssClass="btn" ValidationGroup="v002" />--%>
                                        </td>
                                    </tr>
                                </table>
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="color:#206020;">
                             Off Signer 

                        <asp:LinkButton ID="LinkButton2" runat="server" Text=" ( Open Planning History ) " OnClick="lnlPlaningHistiry_Click" style="text-decoration:none;"></asp:LinkButton>   
                       
                            </div>
                    </div>
                    <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;background-color:#FFE7DC;font-family:Arial;font-size:12px;">
                        <tr class="headerstylegrid">
                            <td>Vessel</td>
                            <td>Crew#</td>
                            <td>Crew Name</td>
                            <td>Rank</td>
                            <td>Nationality</td>
                            <td>Sign on Dt.</td>
                            <td>Rel Due Dt.</td>                            
                        </tr>
                        <asp:Repeater ID="rptOffSignerDetails" runat="server">
                            <ItemTemplate>
                        <tr style="font-weight:bold;">
                            <td style="width:70px;"><%#Eval("PLANVESSELCODE") %></td>
                            <td style="width:30px;"><%#Eval("OFF_CrewNumber") %></td>
                                <td style="text-align:left;"><%#Eval("OFF_CrewName") %></td>
                                <td style="width:30px;"><%#Eval("OFF_RankCODE") %></td>
                                <td style="width:100px;"><%#Eval("OFF_COUNTRYNAME") %></td>
                                <td style="width:100px;" ><%#Common.ToDateString(Eval("SignOnDate"))%></td>
                                <td style="width:100px;"><%#Common.ToDateString(Eval("ReliefDueDate"))%> </td>
                        </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                    </table>
                    <div style="padding:0px;border:solid 1px #c2c2c2;">
                         <div style="padding:5px; text-align:left;"> 
                             
                        
                            <b>  Remark for Offsigner </b>
                         </div>
                         <asp:TextBox ID="txtRemarkOffsigner" runat="server" Width="99%"  MaxLength="100" style="background-color:#fcfca3"></asp:TextBox>
                        <div style="padding:5px;border:solid 0px red;">
                            <table width="98%">
                                <col width="50%" style="text-align:left;" />
                                <col width="50%" style="text-align:right;" />
                                <col width="70px" style="text-align:right;" />
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRemarksUpdatedByOffSigner" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMessRemarksOffSigner" runat="server" ForeColor="Red" Font-Size="Larger" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUpdateRemarksOffSigner" runat="server" OnClick="btnUpdateRemarksOffSigner_OnClick" CssClass="btn" Text="Update Remarks" />
                                    </td>
                                </tr>
                            </table>
                           
                            
                        </div>
                    </div>
                 </div>

                <div class="OnSignerDetails" >
                    <div style="font-size:15px;color:#58d7ee;text-align:left; padding:10px; font-weight:bold;"> 
                        <div> <asp:Label ID="lblReliever" runat="server" ForeColor="#206020" Text="Reliever / Onsigner's Details"></asp:Label>
                           
                            <span > 
                                <asp:LinkButton ID="lbkAddSigner" runat="server" Text=" + Assign Reliever " OnClick="lbkAddSigner_OnClick" style="color:#F78181;text-decoration:none;"></asp:LinkButton>   </span>


                        </div>

                    </div>  
                                 
                   
                    <div style="border:solid 1px #c2c2c2;" id="divOnSignerData" runat="server">

                         <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; background-color:#E0FFDC;font-family:Arial;font-size:12px;">
                        <tr class="headerstylegrid">
                            <td style="width:50px;">Crew#</td>
                            <td style="text-align:left;width:200px; ">Reliever / Onsigner's Name</td>
                            <td style="width:50px; ">Rank</td>
                            <td style="width:80px; ">Nationality</td>
                            <td style="width:70px; ">Last VSL</td>
                            <td style="width:80px; ">Grading</td>
                            <td style="width:80px; ">PP Expiry</td>
                            <td style="width:80px; ">Approval</td>
                            
                            <td style="width:80px; "></td>

                        </tr>
                        <asp:Repeater ID="rptOnSignerDetails" runat="server">
                            <ItemTemplate>
                       <tr style="font-weight:bold;">
                            <td style="width:50px;">
                                <%#Eval("ON_CrewNumber") %>
                                <asp:HiddenField ID="hfPLANVESSELID" runat="server" Value='<%#Eval("PLANVESSELID")%>' />
                                <asp:HiddenField ID="hfCrewID" runat="server" Value='<%#Eval("OFF_CREWID")%>' />
                                <asp:HiddenField ID="hfRelieverId" runat="server" Value='<%#Eval("ON_CREWID")%>' />
                            </td>
                            <td style="text-align:left;width:200px;"><%#Eval("ON_CrewName")%></td>
                            <td style="width:50px;"><%#Eval("ON_RankCODE")%></td>
                            <td style="width:80px;"><%#Eval("ON_COUNTRYNAME")%></td>
                            <td style="width:70px;"><%#Eval("ON_VesselCODE")%></td>
                              <td style="width:80px;"><span class='Grade_<%#Eval("Grading")%>'></span><%#Eval("Grading")%></td>
                             <td style="width:100px;"><%#Common.ToDateString(Eval("P_EXPIRY"))%></td>
                            <td style="width:80px;"><%#(Eval("APP_STATUS").ToString()=="A")?"Approved":((Eval("APP_STATUS").ToString()=="R")?"Rejected":((Eval("APP_STATUS").ToString()=="N")?"Pending":""))%></td>
                        
                            <td>
                                <asp:Button ID="btnDeleteOnSigner" runat="server" OnClick="btnDeleteOnSigner_OnClick" Text="Delete" ToolTip="Remove Reliever" OnClientClick="return window.confirm('Are you sure to delete this planning?')" Visible='<%# (authPage.IsDelete) %>' CssClass="btn" />
                            </td>
                        </tr>
                                </ItemTemplate>
                        </asp:Repeater>
                       
                    </table>
                        <div style="margin-top:0px;">
                            <div style="padding:5px;text-align:left;"> <b>  Remark for Reliever / Onsigner's </b></div>
                         
                                <asp:TextBox ID="txtCommentOnSigner" runat="server" Width="99%" MaxLength="100" style="background-color:#fcfca3"></asp:TextBox> 
</div>
                        <div style="padding:5px;border:solid 0px red;">
                            <table width="98%">
                                <col width="50%" style="text-align:left;" />
                                <col width="50%" style="text-align:right;" />
                                <col width="70px" style="text-align:right;" />
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRemarksUpdatedByOnSigner" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMessRemarksOnSigner" runat="server" ForeColor="Red" Font-Size="Larger" ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUpdateRemarksOnSigner" runat="server" OnClick="btnUpdateRemarksOnSigner_OnClick" CssClass="btn" Text="Update Remarks" />
                                    </td>
                                </tr>
                            </table>
                            
                            
                        </div>
                    </div>
                    <div style="padding:5px; text-align:center;">
                          <asp:Label ID="lb_msg" runat="server" ForeColor="Red" Font-Size="Larger" ></asp:Label>
                    </div>
                    <div>
                        
                    </div>
                 </div>

                <div style="padding:5px;">
                <asp:Button ID="btnCloseSignOnOff" runat="server" Text="Close" CssClass="btn" OnClick="btnCloseSignOnOff_OnClick" />
                    </div>
            </center>
        </div>
        </center>
        </div>
     <script type="text/javascript">
        $(document).ready(function () {
            $(".NumberValidation").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        });
    </script>
</form>
      </body>



