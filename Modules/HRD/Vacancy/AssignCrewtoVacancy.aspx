<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignCrewtoVacancy.aspx.cs" Inherits="Modules_HRD_Vacancy_AssignCrewtoVacancy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     
        <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
      <link rel="stylesheet" type="text/css" href="../Styles/style.css" />
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
            .auto-style1 {
                width: 40px;
                height: 20px;
            }
            .auto-style2 {
                width: 50px;
                height: 20px;
            }
            .auto-style3 {
                height: 20px;
            }
            .auto-style4 {
                width: 80px;
                height: 20px;
            }
            .auto-style5 {
                width: 200px;
                height: 20px;
            }
            .auto-style6 {
                width: 20px;
                height: 20px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;width:100%;">
      <div style=" font-size:14px;" class="text headerband"><strong>Search Crew for Vacancy
     
      </strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden;">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="1" style="width: 100%" >
            <tr>
                <td style="text-align: center;" valign="top">                                                                                <table cellpadding="2" cellspacing="0" width="98%" border="0" style="text-align: center"  >
                <tr>
                    <td style="width: 100px; text-align: right">
                        Emp. # :</td>
                    <td style="width: 173px; text-align: left;">
                        <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="80px"></asp:TextBox></td>
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
                    <td style="width: 250px; text-align: center;">
                        <asp:Button ID="btn_Search" CausesValidation="false" runat="server" OnClientClick="this.value='Loading...';"  style=" background-color:RED; color:White; border:none; padding:3px; " Text="Search" OnClick="btn_Search_Click" Width="70px" />
                    
                        </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 2px; text-align: right">
                        Vessel Type :</td>
                    <td style="width: 173px; height: 2px; text-align: left">
                        <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="input_box" Width="180px" >
                    </asp:DropDownList></td>
                    <td style="width: 173px; height: 2px; text-align: right">
                        </td>
                    <td style="width: 173px; height: 2px; text-align: left">
                        
                        </td>
                    <td style="width: 175px; height: 2px; text-align: right">
                        </td>
                    <td style="width: 200px; height: 2px; text-align: left">
                       
                       </td>
                    <td style="width: 133px; height: 2px; text-align: right">
                         </td>
                    <td style="width: 180px; height: 2px; text-align: left">
                   
                        </td>
                    <td style="width: 180px; height: 2px; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 2px; text-align: right" >
                        </td>
                    <td style="width: 173px; height: 2px; text-align: left" >
                        
                    </td>
                    <td style="text-align: right" >
                       </td>
                    <td style="text-align: left">
                        
                    </td>
                    <td style="text-align: right" class="style3">
                    
                        </td>
                    <td style="text-align: left" class="style4">
                       
                        </td>
                    <td style="text-align: right;width:250px;display:none;" class="style5">
                        
                        </td>
                    <td style="text-align: left;display:none;" class="style6" colspan="2">
                          
                    </td>
                </tr>
                </table>
                </td>
            </tr>
    <tr>
        <td style="text-align:left;" >
                        <div style="height:60px; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0" style=" height:60px">
                        <tr class= "headerstylegrid"> 
                            <td style="width:60px"></td>
                            <td style="width:100px"></td>
                            <td></td>
                            <td style="width:100px"></td>
                            <td style="width:100px"></td>
                            <td style="width:100px"></td>       
                            <td style="width:20px"></td>
                        </tr>
                        <tr class= "headerstylegrid"> 
                            <td style="width:60px">Assign</td>
                            <td style="width:100px">Crew#</td>
                            <td>Crew Name</td>
                            <td style="width:100px">Rank</td>
                            <td style="width:100px">Nationality</td>
                            <td style="width:100px">Avl From.</td>
                            <td style="width:20px"></td>
                        </tr>
                        </table>
                        </div>
                        <div style="height:322px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0">
                        <asp:Repeater runat="server" ID="rpt_SignOnCrewList">
                        <ItemTemplate>
                        <tr>
                            <td style="width:60px; text-align:center;">
                                <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/group.gif"  CommandArgument='<%#Eval("CandidateId")%>' CssClass='<%#Eval("RankId")%>' OnClientClick="this.src='../Images/loading.gif';" ID="btnAssignCrew" OnClick="btnAssignCrew_Click"/></td>
                            <td style="width:100px;text-align:center;"><%#Eval("CandidateId")%></td>
                            <td style="padding-left:10px;"><%#Eval("Name")%></td>
                            <td style="width:100px"><%#Eval("Rank")%></td>
                            <td style="width:100px"><%#Eval("Country")%></td>
                            <td style="width:100px"><%#Common.ToDateString(Eval("AvailableFrom"))%></td>
                            <td style="width:20px"></td>
                        </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                        <tr style="background-color:#E0F5FF">
                           <td style="width:60px; text-align:center;"><asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/group.gif" CommandArgument='<%#Eval("CandidateId")%>' CssClass='<%#Eval("RankId")%>' OnClientClick="this.src='../Images/loading.gif';" ID="btnAssignCrew" OnClick="btnAssignCrew_Click"/></td>
                            <td style="width:100px;text-align:center;"><%#Eval("CandidateId")%></td>
                            <td style="padding-left:10px;"><%#Eval("Name")%></td>
                            <td style="width:100px"><%#Eval("Rank")%></td>
                            <td style="width:100px"><%#Eval("Country")%></td>
                            <td style="width:100px"><%#Common.ToDateString(Eval("AvailableFrom"))%></td>
                            <td style="width:20px"></td>
                        </tr>
                        </AlternatingItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
        </td>
    </tr>
 </table>



            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
    </div>

    

    

    </form>
</body>
</html>
