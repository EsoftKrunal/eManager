<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselPlanning_CrewSearch.aspx.cs" Inherits="CrewOperation_VesselPlanning_CrewSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
       <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
        <style type="text/css">
       .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_B
       {
           background:yellow; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_D
       {
           background:red; 
           width:15px;
           height:15px;
           color:white;
           border:solid 1px grey;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
      <div class="text headerband" style="font-size:14px; "><strong> 
      [ <asp:Label ID="lblPlanVesselName" runat="server"></asp:Label> ]
      </strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden;">
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>--%>
             <table cellpadding="1" cellspacing="0" width="98%" border="0" style="text-align: center;font-family:Arial;font-size:12px;"   >
                <tr>
                    <td style="width: 100px; text-align: right">
                        Emp. # :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="76px"></asp:TextBox></td>
                    <td style="width: 173px; text-align: right">
                        First Name :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_FirstName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 175px; text-align: right">
                        Last Name :</td>
                    <td style="text-align: left; width: 200px;">
                        <asp:TextBox ID="txt_LastName" runat="server" CssClass="input_box" MaxLength="29"></asp:TextBox></td>
                    <td style="width: 133px; text-align: right">
                        Rank :</td>
                    <td style="width: 180px; text-align: left;">
                        <asp:DropDownList ID="ddl_Rank" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                    <td style="width: 180px; text-align: center;">
                        <asp:Button ID="btn_Search" CausesValidation="false" OnClientClick="this.value='Loading...';"   runat="server" Text="Search" Width="80px" class="btn"  OnClick="btn_Search_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 190px; text-align: right">
                        Owner Pool :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:DropDownList ID="ddl_OwnerPool" runat="server" CssClass="input_box">
                    </asp:DropDownList></td>
                    <td style="width: 173px; text-align: right">
                        Vessel Type :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselType_SelectedIndexChanged" Width="176px">
                    </asp:DropDownList></td>
                    <td style="width: 175px; text-align: right">
                        Status :</td>
                    <td style="width: 200px; text-align: left;">
                        <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                    <td style="width: 133px; height: 2px; text-align: right">
                         </td>
                    <td style="width: 180px; height: 2px; text-align: left">
                        <asp:CheckBox ID="chk_Exclude" runat="server" Text="Exclude Planned" />
                         </td>

                    <td style="width: 180px; height: 2px; text-align: left">
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td style="text-align: right" class="style1">
                        Rec. Office :</td>
                    <td style="text-align: left;" class="style2">
                        <asp:DropDownList ID="dd_RecOff" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                    <td style="text-align: left;" class="style2">
                        </td>
                    <td style="text-align: left" class="style3" >
                    <asp:CheckBox ID="chkfamily" runat="server" Text="Family Members" />
                        </td>
                    <td style="text-align: right" class="style4">
                    Compatibility :
                        
                        </td>
                    <td style="text-align: left" class="style5">
                    <asp:RadioButton id="r1" runat="server" Text="Owner" GroupName="OV"/>
                            <asp:RadioButton id="r2" runat="server" Checked="true" Text="Vessel" GroupName="OV"/>
                    </td>
                    <td style="text-align: right; width:250px;display:none;">+ve Grading By:</td>
                    <td style="text-align: left;display:none;" class="style6" colspan="2">
                        <asp:CheckBox ID="CheckBoxOR" runat="server" Text="OR" />        
                        <asp:CheckBox ID="CheckBoxCH" runat="server" Text="CH" />        
                        <asp:CheckBox ID="CheckBoxTS" runat="server" Text="TS" />        
                        <asp:CheckBox ID="CheckBoxFM" runat="server" Text="FM" />        
                        <asp:CheckBox ID="CheckBoxMS" runat="server" Text="MS" />        
                    </td>
                    
                </tr>
                </table>
            <table style=" display : none" >
                <tr>
                <td> Matrix Exp. :<asp:DropDownList ID="ddl_Matrix" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                <td>  <asp:CheckBox ID="chk_BON" runat="server" Text="Budgeted Nationality" /></td>
                <td> Nationality :<asp:DropDownList ID="dd_Nationality" runat="server" CssClass="input_box">
                        </asp:DropDownList></td>
                </tr>
            </table>

            <div style="height:50px; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2;">
                                <table cellpadding="2" cellspacing="0" width="100%" border="0" style=" height:50px;font-family:Arial;font-size:12px;" >
                                <tr class= "headerstylegrid"> 
                                    <td style="width:25px; text-align:center;"></td>
                                    <td style="width:50px; text-align:left;"></td>
                                    <td style="width:50px;text-align:left;"></td>
                                    <td style="width:80px; text-align:left;"></td>
                                    <td style="width:100px; text-align:left;"></td>
                                    <td style="width:90px; text-align:left;"></td>
                                    <td style="width:100px; text-align:left;"></td>
                                    <td style="width:100px; text-align:center;">.</td>
                                    <td style="width:100px; text-align:center;"></td>
                                   <%-- <td style="width:200px; text-align:center;" colspan="5">Latest Grading for <asp:Label runat="server" ID="lblGradingFor"></asp:Label> </td>--%>
                                    <td style="width:20px"></td>

                                   
                                </tr>
                                <tr class= "headerstylegrid"> 
                                    <td style="width:25px; text-align:center;">Add</td>
                                    <td style="width:50px; text-align:left;">Emp. #</td>
                                    <td style="width:50px;text-align:left;">Name</td>
                                    <td style="width:80px; text-align:left;">Rank</td>
                                    <td style="width:100px; text-align:left;">Nationality</td>
                                    <td style="width:90px; text-align:left;">Last Vessel</td>
                                    <td style="width:100px; text-align:left;">Planned Vessel</td>
                                    <td style="width:100px; text-align:center;">Exp. Join Dt.</td>
                                    <td style="width:100px; text-align:center;">Available From</td>
                                 <%--   <td style="width:40px; text-align:center;">OR</td>
                                    <td style="width:40px; text-align:center;">CH</td>
                                    <td style="width:40px; text-align:center;">TS</td>
                                    <td style="width:40px; text-align:center;">FM</td>
                                    <td style="width:40px; text-align:center;">MS</td>--%>
                                    <td style="width:20px"></td>
                                </tr>
                                </table>
                            </div>
                            <div id="div-datagrid" style="height:300px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0">
                        <asp:Repeater runat="server" ID="rpt_CrewList">
                        <ItemTemplate>
                        <tr style="font-family:Arial;font-size:12px;">
                        <td style="width:25px; text-align:center;">
                           <asp:ImageButton runat="server" OnClick="btnAssign_Click" OnClientClick="this.src='../Images/loading.gif';" CommandArgument='<%#Eval("Crewid")%>' RankId='<%#Eval("RankId")%>'   ImageUrl="~/Modules/HRD/Images/group.gif" ID="btnAssign" />
                        </td>
                        <td style="width:50px; text-align:left;"><%#Eval("EMPNO")%> </td>
                        <td style="width:50px;text-align:left;word-wrap: break-word;"><%#Eval("Name")%></td>
                        <td style="width:80px; text-align:left;"><%# Eval("Rank")%></td>
                        <td style="width:100px; text-align:left;"><%#Eval("nationality")%></td>
                        <td style="width:90px; text-align:left;"><%#Eval("Vessel")%></td>
                        <td style="width:100px; text-align:left;"><%#Eval("LastVessel")%></td>
                        <td style="width:100px; text-align:center;"><%#Eval("ExpDate")%></td>
                        <td style='width:100px; text-align:center;'><%#Eval("AVLDate")%></td>
                       <%-- <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("OwnerRep")%>'><%#Eval("OwnerRep")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("Charterer")%>'><%#Eval("Charterer")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("TechSupdt")%>'><%#Eval("TechSupdt")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("FleetMgr")%>'><%#Eval("FleetMgr")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("MarineSupdt")%>'><%#Eval("MarineSupdt")%></div></td> --%>                       
                        <td style="width:20px"></td>
                        </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                        <tr style="background-color:#E0F5FF;font-family:Arial;font-size:12px;">
                            <td style="width:25px; text-align:center;">
                           <asp:ImageButton runat="server" OnClick="btnAssign_Click" OnClientClick="this.src='../Images/loading.gif';" CommandArgument='<%#Eval("Crewid")%>' RankId='<%#Eval("RankId")%>'   ImageUrl="~/Modules/HRD/Images/group.gif" ID="btnAssign" />
                        </td>
                        <td style="width:50px; text-align:left;"><%#Eval("EMPNO")%> </td>
                        <td style="width:50px;text-align:left;word-wrap: break-word;"><%#Eval("Name")%></td>
                        <td style="width:80px; text-align:left;"><%# Eval("Rank")%></td>
                        <td style="width:100px; text-align:left;"><%#Eval("nationality")%></td>
                        <td style="width:90px; text-align:left;"><%#Eval("Vessel")%></td>
                        <td style="width:100px; text-align:left;"><%#Eval("LastVessel")%></td>
                        <td style="width:100px; text-align:center;"><%#Eval("ExpDate")%></td>
                        <td style='width:100px; text-align:center;'><%#Eval("AVLDate")%></td>
                      <%--  <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("OwnerRep")%>'><%#Eval("OwnerRep")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("Charterer")%>'><%#Eval("Charterer")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("TechSupdt")%>'><%#Eval("TechSupdt")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("FleetMgr")%>'><%#Eval("FleetMgr")%></div></td>
                        <td style="width:40px; text-align:center;"><div class='Grade_<%#Eval("MarineSupdt")%>'><%#Eval("MarineSupdt")%></div></td>   --%>                     
                        <td style="width:20px"></td>

                        </tr>
                        </AlternatingItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
         
             <script type="text/javascript">
                 function Ok(sender, e) {
                     $find('md1').hide();
                     WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("Main", "", true, "&", "", false, true)); //('SearchReliver1_Main', e); 
                 }
             </script>
             <asp:LinkButton ID="Main" runat="server" Text=" Please Confirm" OnClick="Main_Click" />
             <ajaxToolkit:ModalPopupExtender ID="md1" runat="server" TargetControlID="Main" PopupControlID="pnl_yesno"
                 BackgroundCssClass="modalBackground" OnOkScript="Ok()" OkControlID="yes" CancelControlID="no" />
             <asp:Panel ID="pnl_yesno" runat="server" CssClass="modalSignUp" Width="600px" Height="275px"
                 Style="vertical-align: top; display: none;">
                 <table style="width: 100%" cellspacing="0" cellpadding="0">
                     <tr>
                         <td <div style="padding:6px;  font-size:14px; text-align:center;" class="text headerband"><strong>Planning Remarks</strong>
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align: center; height: 20px;">
                             <asp:Label ID="lbl_prompt" ForeColor="Red" runat="server" Font-Size="12px" Text="Label"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align: center">
                             <asp:TextBox ID="txt_PRemarks" runat="server" CssClass="input_box" Width="550px"
                                 Height="180px" TextMode="MultiLine"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align: center; padding-top: 5px;">
                             <asp:Button ID="yes" runat="server" Text="Yes" style=" background-color:#009900; color:White; border:none; padding:3px; " Width="70px" />
                             <asp:Button ID="no" runat="server" Text="No" style=" background-color:RED; color:White; border:none; padding:3px; " Width="70px" />
                         </td>
                     </tr>
                 </table>
             </asp:Panel>
         
           <%--  </ContentTemplate>
             </asp:UpdatePanel>--%>
             </div>
    
    </div>
    </form>
</body>
</html>
