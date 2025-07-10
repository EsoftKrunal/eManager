<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyRCA.aspx.cs" Inherits="ModifyRCA" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
    <title>Edit Template</title>
    <style type="text/css">
    .btn
    {
        font-size:11px;
        width:100%;
        border:solid 1px black;
        cursor:pointer;
        color:White;
        background-color:#47A3FF;
    }
    .g
    {background-color:Green;color:White;}
    .b
    {background-color:#A3E0FF;}
    .a
    {background-color:#FFCCCC;}
    .r
    {background-color:red;color:White;}
    *
    {
        box-sizing:border-box;
    }
    
    </style>
    <script type="text/javascript">
        function RefreshParent() {
            var btn = window.opener.document.getElementById("btnRefresh");
            btn.click();
        }
    </script>
</head>
<body style="font-family:Calibri; font-size:13px;">
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="position:fixed;width:100%; height:215px; border-bottom:Solid 5px #47A3FF; background-color:#eeeeee; left: 0px;">
        <div style="background-color:#47A3FF; padding:10px; font-size:14px; text-align:center; color:White">Risk Assesment</div>
        <div>
        <div>
        <table cellspacing="0" rules="none" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;">
        <tr>
        <td style="text-align:left;font-weight:bold;width:120px;">Vessel &nbsp;</td>
        <td style="text-align:center;font-weight:bold;width:10px;">:</td>
        <td style="text-align:left;width:100px;"><asp:Label runat="server" ID="lblVesselCode" ></asp:Label></td>
        <td style="text-align:right;width:100px;font-weight:bold;width:100px;">RA # :&nbsp;</td>
        <td style="text-align:left;width:100px;"><asp:Label runat="server" ID="lblRefNo" ></asp:Label></td>
        <td style="text-align:right;font-weight:bold;width:100px;">Template # :&nbsp;</td>
        <td style="text-align:left;"><asp:Label runat="server" ID="lblRevNo" ></asp:Label></td>        
        <td rowspan="2" style="text-align:right">
         <asp:Button runat="server" ID="btnAddTask" OnClick="btnAddTask_Click" Text="Add Task" CssClass="btn" Width="130px" />
        </td>
        </tr>
        <tr>
        <td style="text-align:left; font-weight:bold; width:120px;">Event Name &nbsp;</td>
        <td style="text-align:center;font-weight:bold;width:10px;">:</td>
        <td colspan="3" style="text-align:left;"><asp:Label runat="server" ID="lblEventName" Font-Bold="true" ></asp:Label></td>
        <td style="text-align:right;font-weight:bold;width:100px;">Event Date :&nbsp;</td>
        <td style="text-align:left;"><asp:TextBox runat="server" ID="txtEventDate" Width="80px" ></asp:TextBox>
         <asp:CalendarExtender Format="dd-MMM-yyyy" ID="CE1" runat="server" PopupButtonID="txtEventDate" TargetControlID="txtEventDate" ></asp:CalendarExtender>
        </td>
        </tr>
        </table>
        <div style="padding:3px">
           <b>Alternate Methods of work considered? :</b>
        </div>

        <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;">
        <tr>
        <td style="text-align:left; width:75px;">
                <asp:DropDownList ID="ddlAMW" AutoPostBack="true" OnSelectedIndexChanged="ddlAMW_SelectedIndexChanged" runat="server" Width="70px" >
                <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y" ></asp:ListItem>
                <asp:ListItem Text="No" Value="N" ></asp:ListItem>
                <asp:ListItem Text="NA" Value="A" ></asp:ListItem>
                </asp:DropDownList>
            </td>
        <td style="text-align:left;">
            <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox>
         </td>
        </tr>
        </table>

        </div>
        </div>
        <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse; height:50px;">
                <tr style="font-weight:bold; background-color:#dddddd;">
                    <td style="text-align:left; width:40px;">&nbsp;</td>
                    <td style="text-align:left; ">Task Name</td>
                    <td style="text-align:left; width:200px;">&nbsp;</td>
                    <td colspan="3" style="text-align:center;width:240px; background-color:#FFDB94">Existing Risk</td>
                    <td style="text-align:left;border-left:solid 3px black;width:200px;">&nbsp;</td>
                    <td colspan="3" style="text-align:center;width:240px; background-color:#99CCFF">Residual Risk</td>
                    <td style="text-align:center;width:40px;">&nbsp;</td>
                </tr>
                <tr style="font-weight:bold; background-color:#dddddd;">
                    <td style="text-align:center;width:40px;">View</td>
                    <td style="text-align:left;">Hazard Name</td>
                    <td style="text-align:left; width:200px;">Existing Control Measures</td>
                    <td style="text-align:center;width:80px;background-color:#FFDB94">Severity</td>
                    <td style="text-align:center;width:80px;background-color:#FFDB94">LikeliHood</td>                                                        
                    <td style="text-align:center;width:80px;background-color:#FFDB94">Risk Level</td>
                    <td style="text-align:left; border-left:solid 3px black; width:200px;">Additional Control Measures</td>
                    <td style="text-align:center;width:80px;background-color:#99CCFF">Severity</td>
                    <td style="text-align:center;width:80px;background-color:#99CCFF">LikeliHood</td>                                                        
                    <td style="text-align:center;width:80px;background-color:#99CCFF">Risk Level</td>
                    <td style="text-align:center;width:40px;">Edit</td>
                </tr>
        </table>
    </div>
    <div style="padding-top:220px;padding-bottom:180px;">
          <asp:Repeater ID="rptTasks" runat="server">
          <ItemTemplate>
            <table cellspacing="0" rules="all" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;">
            <tr>
            <td style="width:40px; text-align:center;">
                <asp:ImageButton ID="btnAddHazard" runat="server" CommandArgument='<%#Eval("Task_TableId")%>' OnClick="btnAddHazard_Click" ImageUrl="~/Images/add.png" ToolTip="Add New Hazard" />                              
            </td>
            <td><b><%#Eval("TaskName")%></b></td>
            <td align="center" style="width:40px;">
                <asp:ImageButton ID="btnEditTask" OnClick="btnEditTask_Click" ImageUrl="~/Images/editX12.jpg" ToolTip="Edit Task"  CommandArgument='<%#Eval("Task_TableId")%>' Visible='<%#CanEdit%>' runat="server" />
            </td>
            </tr>
            </table>
            <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
            <asp:Repeater ID="rptHazards" runat="server" DataSource='<%#BindHazards(Eval("Task_TableId"))%>'>
            <ItemTemplate>
                <tr>
                    <td align="center" style="border:solid 1px #c2c2c2;width:40px;">
                        <asp:ImageButton ID="btnViewHazard" runat="server" CommandArgument='<%#Eval("TableId")%>' OnClick="btnViewHazard_Click" ImageUrl="~/Images/icon-view-blue_12.png" ToolTip="View Hazard" />                              
                    </td>
                    <td align="left" style="text-align:left;"><%#Eval("HazardName")%></td>
                    <td align="left" style="width:200px;" ><%#Eval("ControlMeasures")%></td>
                    <td align="center" style="text-align:center;width:80px;"><%#Eval("Severity")%></td>
                    <td align="center" style="text-align:center;width:80px;"><%#Eval("LikeliHood")%></td>
                    <td align="center" style="text-align:center;width:80px;" class='<%#GetCSSColor(Eval("Risklevel"))%>'><%#Eval("Risklevel")%></td>
                    <td align="left" style="border-left:solid 3px black;width:200px;" ><%#Eval("ADD_CONTROL_MEASURES")%></td>
                    <td align="center" style="text-align:center;width:80px;"><%#Eval("Re_Severity")%></td>
                    <td align="center" style="text-align:center;width:80px;"><%#Eval("Re_LikeliHood")%></td>
                    <td align="center" style="text-align:center;width:80px;" class='<%#GetCSSColor(Eval("Re_Risklevel"))%>'><%#Eval("Re_Risklevel")%></td>
                    <td align="center" style="border:solid 1px #c2c2c2;width:40px;">
                        <asp:ImageButton ID="btnEditHazard" OnClick="btnEditHazard_Click" ImageUrl="~/Images/editX12.jpg" ToolTip="Edit Hazard"  CommandArgument='<%#Eval("TableId")%>' Visible='<%#CanEdit%>' runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
          </ItemTemplate>
          </asp:Repeater>
          
    </div>
    <div style="position:fixed;bottom:0px; width:100%; background-color:#DDDDDD; ">
   
    <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
    <tr>
    <td style="width:50%;">
      <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
        <tr>
            <td style="text-align:left; width:123px; "><b>Existing Risk :</b>&nbsp;</td>
            <td style="text-align:left; width:20px;"><asp:Image runat="server" ID="imgER" /></td>
            <td style="text-align:left; width:545px; "><asp:Label ID="lblExtAction" runat="server"></asp:Label></td>
            </tr>
    </table>
    </td>
    <td>
     <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
        <tr>
               <td style="text-align:left; width:123px;"><b>Residual Risk :</b>&nbsp;</td>
            <td style="text-align:left; width:20px;"><asp:Image runat="server" ID="imgRR" /></td>
            <td style="text-align:left; "><asp:Label ID="lblResAction" runat="server"></asp:Label></td>
 
        </tr>
        </table>
    </td>
    </tr>
    <tr>
    <td style="width:50%;">
        <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
            <tr>
                <td style="background-color:#DDDDDD; text-align:left;"><b>HOD Name/ Position :</b>&nbsp;<asp:TextBox ID="txtHodName" runat="server" Width="50%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                     <asp:TextBox ID="txtHodComments" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox> 
                </td>
            </tr>
            <tr>
                <td style="background-color:#DDDDDD; text-align:left;"><b>Date :&nbsp;<asp:Label runat="server" ID="lblHODDate"></asp:Label> </b></td>
            </tr>
        </table>
    </td>
    <td >
        <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
            <tr>
                <td style="background-color:#DDDDDD; text-align:left;"><b>Approver Name/ Position :</b>&nbsp;[ <asp:Label ID="lblApproverName" runat="server"></asp:Label> ]</td>
            </tr>
            <tr>
                <td>
                     <div style="width:100%; height:50px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #444444 ">
                         <asp:Label ID="txtApproverComments" runat="server"></asp:Label> 
                     </div>
                </td>
            </tr>
            <tr>
                <td style="background-color:#DDDDDD;text-align:left;"><b>Date :&nbsp;<asp:Label runat="server" ID="lblReviewDate"></asp:Label> </b></td>
            </tr>
        </table>
    </td>
    </tr>
    </table>    
     <div style="text-align:right; background-color:#FFFFEB; padding:5px;">
                <asp:Label runat="server" ID="lblMsg" ForeColor="Red" style="float:left; font-size:15px; margin-top:3px;"></asp:Label>
                <div style="">
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn" Width="130px" />
                    <asp:Button runat="server" ID="btnApprove" Text="Review" OnClick="btnApprove_Click" Visible="false" CssClass="btn" Width="130px" />
                    <asp:Button runat="server" ID="btnPrint" Text="Print" OnClick="btnPrint_Click"  CssClass="btn" Width="130px" />
                    <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn" Width="100px" OnClientClick="window.close();" />
                </div>
        </div> 
    </div>
    <div ID="dv_NewEditTask" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:60%; padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px black;">
        <center>
            <div class="box3" style='padding:10px 0px 10px  0px'><b>Add / Edit Task</b></div>
            <asp:TextBox runat="server" ID="txtTaskName" TextMode="MultiLine" Width="99%" Rows="10"></asp:TextBox>
            <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
                 <asp:Label runat="server" ID="lblAddTaskMsg" style="float:left;color:Red"></asp:Label>
                 <asp:Button runat="server" ID="btnSaveTask" Text="Save" OnClick="btnSaveTask_Click" style=" background-color:green; color:White; border:solid 1px grey;width:100px;"/>
                 <asp:Button runat="server" ID="btnDeleteTask" Text="Delete" OnClick="btnDeleteTask_Click"  OnClientClick="return confirm('Are you sure to delete this?')" style=" background-color:#c2c2c2; color:White; border:solid 1px grey;width:100px;"/>
                 <asp:Button runat="server" ID="btnCloseTask" Text="Close" OnClick="btnCloseTask_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
            </div>
        </center>
        </div>
        </center>
    </div>
    <div ID="dv_NewHazard" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px black;">
        <center>
                <div class="box3" style='padding:10px 0px 10px  0px'><b>Add / Edit Hazard</b></div>
                <div style="padding:3px"><b><asp:Label runat="server" ID="txtTask"></asp:Label></b></div>
                <table cellspacing="0" rules="all" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;text-align:left;">
                <tr>
                <td style="width:100px; text-align:left">&nbsp;Hazard Details :</td>
                <td style="text-align:left">
                    <asp:TextBox runat="server" ID="txtHazard" Width="99%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                </td>
                <td style="width:20px">
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtHazard" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                </tr>
                </table>
                <div style="padding:3px; text-align:right">
                <asp:LinkButton ID="btnOpenGL" runat="server" Text="Open Risk Matrix" CausesValidation="false" OnClick="btnOpenGL_Click" style="text-decoration:none; color:Red; font-weight:bold;" ></asp:LinkButton>
                </div>
                <div class="box3" style='padding:5px 0px 5px  0px'><b>Existing Control Measures</b></div>
                <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse; text-align:center;">
                <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:100%">
                        <asp:TextBox runat="server" ID="txtStdCM" Width="99%" TextMode="MultiLine" Height="70px" CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox>
                    </td>
                    </tr>
                    </table>
                    
                </td>
                <td style="width:50%">
                <asp:UpdatePanel runat="server" id="re1">
                <ContentTemplate>
                <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                    <td style=" background-color:#DDDDDD; text-align:left; width:80px;">&nbsp;Severity  : </td>
                                    <td style="width:165px">
                                        <asp:DropDownList ID="ddlR11" AutoPostBack="true" OnSelectedIndexChanged="FillResidual_Click" runat="server" Width="160px"  >
                                           <asp:ListItem Text="" Value="0"></asp:ListItem>
                                           <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                           <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                           <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                           <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                           <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align:left"><asp:Label runat="server" ID="lblDetails1"></asp:Label></td> 
                                </tr>
                                <tr>
                                   <td style=" background-color:#DDDDDD; text-align:left;">&nbsp;Likelihood :</td>
                                   <td><asp:DropDownList ID="ddlR12" AutoPostBack="true" OnSelectedIndexChanged="FillResidual_Click" runat="server" Width="160px" >
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td style="text-align:left"><asp:Label runat="server" ID="lblDetails2"></asp:Label></td> 
                                </tr>
                                <tr>
                                    <td style=" background-color:#DDDDDD; text-align:left;">&nbsp;Risk level :</td>
                                    <td id="tdRisk" runat="server"><asp:Label ID="lblR13" runat="server" Font-Bold="true"  MaxLength="1" Width="30px"></asp:Label></td>
                                    <td style="text-align:left"><asp:Label runat="server" ID="lblDetails3"  Font-Bold="true"></asp:Label></td> 
                                </tr>
                              <tr>
                                
                              </tr>
                            </table>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                </tr>
                </table>
                <div class="box3" style='padding:5px 0px 5px  0px'><b>Additional Control Measures</b></div>
                <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse; text-align:center;">
                <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:100%">
                        <asp:TextBox runat="server" ID="txtAddCM" Width="99%" TextMode="MultiLine" Height="70px" CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox>
                    </td>
                    </tr>
                </table>
                </td>
                <td style="width:50%">
                <asp:UpdatePanel runat="server" id="UpdatePanel1">
                <ContentTemplate>
                    <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style=" background-color:#DDDDDD; text-align:left; width:80px">&nbsp;Severity  : </td>
                                <td style="width:165px">
                                    <asp:DropDownList ID="ddlReR11" AutoPostBack="true" OnSelectedIndexChanged="FillReResidual_Click" runat="server" Width="160px">
                                         <asp:ListItem Text="" Value="0"></asp:ListItem>
                                         <asp:ListItem Text=" 1 - Negligible " Value="1"></asp:ListItem>
                                         <asp:ListItem Text=" 2 - Minor " Value="2"></asp:ListItem>
                                         <asp:ListItem Text=" 3 - Major " Value="3"></asp:ListItem>
                                         <asp:ListItem Text=" 4 - Severe " Value="4"></asp:ListItem>
                                         <asp:ListItem Text=" 5 - Catastropic " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left"><asp:Label runat="server" ID="lblDetails11"></asp:Label></td> 
                                </tr>
                                <tr>
                                <td style=" background-color:#DDDDDD; text-align:left;">&nbsp;Likelihood :</td>
                                  <td >
                                    <asp:DropDownList ID="ddlReR12" AutoPostBack="true" OnSelectedIndexChanged="FillReResidual_Click" runat="server" Width="160px">
                                       <asp:ListItem Text="" Value="0"></asp:ListItem>
                                       <asp:ListItem Text=" 1 - Almost Nil Chances " Value="1"></asp:ListItem>
                                       <asp:ListItem Text=" 2 - Highly Unlikely " Value="2"></asp:ListItem>
                                       <asp:ListItem Text=" 3 - Unlikely " Value="3"></asp:ListItem>
                                       <asp:ListItem Text=" 4 - Likely " Value="4"></asp:ListItem>
                                       <asp:ListItem Text=" 5 - Very Likely " Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:left"><asp:Label runat="server" ID="lblDetails12"></asp:Label></td> 
                                </tr>
                                <tr>
                                <td style=" background-color:#DDDDDD; text-align:left;">&nbsp;Risk level :</td>
                                <td id="tdReRisk" runat="server">&nbsp;
                                    <asp:Label ID="lblReR13" runat="server" Font-Bold="true"  MaxLength="1" Width="30px"></asp:Label>
                                </td>
                                <td style="text-align:left"><asp:Label runat="server" ID="lblDetails13" Font-Bold="true"></asp:Label></td> 
                              </tr>
                              <tr>
                                
                              
                                
                              </tr>
                            </table>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                </tr>
                </table>
                <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;text-align:left; margin-left:10px;">
                <tr>
                <td style="width:180px;">Decision to Proceed with work </td>
                <td style="width:10px;">:</td>
                <td><asp:RadioButton ID="rdoProceed_Y" runat="server" Text="Yes" GroupName="GNProceed" /><asp:RadioButton ID="rdoProceed_N" runat="server" Text="No" GroupName="GNProceed" /></td>
                </tr>
                <tr>
                <td>Agreed Time</td>
                <td>:</td>
                <td style="" ><asp:TextBox runat="server" ID="txtAgreedtime" style="width:85%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Person Incharge</td>
                <td>:</td>
                <td><asp:TextBox runat="server" ID="txtPIC" style="width:85%"></asp:TextBox></td>
                </tr>
                </table>
          </center>
          <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
            <asp:Label runat="server" ID="lblMess" style="float:left;color:Red"></asp:Label>
            <asp:Button runat="server" ID="btnSaveHazard" Text="Save" OnClick="btnSaveHazard_Click" CausesValidation="true" style=" background-color:green; color:White; border:solid 1px grey;width:100px;"/>
            <asp:Button ID="btnDelteHazard" OnClick="btnDelteHazard_Click" Text="Delete Hazard"  OnClientClick="return confirm('Are you sure to delete this?')" Visible="false" runat="server" style=" background-color:grey; color:White; border:solid 1px grey;width:130px;"/>
            <asp:Button runat="server" ID="btnCancelHazard" Text="Close" OnClick="btnCancelHazard_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
          </div>
          </div>
        </center>
        <script type="text/javascript">
            function filterHaz(ctl) {
                var par = $(ctl).val().toLowerCase();
                $(".listitemHaz").each(function (i, o) {
                    var txt = $(o).find(".listkeyHaz").first().html().toLowerCase();
                    if (parseInt(txt.search(par)) >= 0) {
                        $(o).css('display', '');
                    }
                    else {
                        $(o).css('display', 'none');
                    }
                });
            }
     </script>
    </div>
    <div ID="dv_GuideLines" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; height:450px;padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px black;">
        <center>
              <div class="box3" style='padding:10px 0px 10px  0px'><b>Risk Matrix</b></div>
              <div >
                <table cellspacing="0"  border="1" cellpadding="1" style="width:100%;border-collapse:collapse; font-size:12px;">
                     <tr>
                         <td style="width:50%">
                             <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td colspan="7">Severity of Consequences</td>
                           </tr>
                            <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td rowspan="7" style="writing-mode: tb-rl;">Likelihood of Consequences</td>
                                <td></td>
                                <td></td>
                                <td>Negligible</td>
                                <td>Minor</td>
                                <td>Major</td>
                                <td>Severe</td>
                                <td>Catastropic</td>
                           </tr>
                           <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td>Rating</td>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Very Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">5</td>
                               <td class="g">5</td>
                               <td class="b">10</td>
                               <td class="a">15</td>
                               <td class="r">20</td>
                               <td class="r">25</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">4</td>
                               <td class="g">4</td>
                               <td class="b">8</td>
                               <td class="a">12</td>
                               <td class="r">16</td>
                               <td class="r">20</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">3</td>
                               <td class="g">3</td>
                               <td class="g">6</td>
                               <td class="b">9</td>
                               <td class="a">12</td>
                               <td class="a">15</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Highly Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">2</td>
                               <td class="g">2</td>
                               <td class="g">4</td>
                               <td class="g">6</td>
                               <td class="b">8</td>
                               <td class="b">10</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Almost Nil Chances</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">1</td>
                               <td class="g">1</td>
                               <td class="g">2</td>
                               <td class="g">3</td>
                               <td class="g">4</td>
                               <td class="g">5</td>
                           </tr>

                       </table> <br />
                             <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                             <tr style="background-color:#c2c2c2;">
                                  <td style="width:60px">Colour</td>
                                  <td style="width:100px">Risk Score (AxB)</td>
                                  <td style="width:50px">Classification</td>
                                  <td>Action</td>
                             </tr>
                             <tr>
                                  <td class="r">Red</td>
                                  <td>16 - 25</td>
                                  <td class="r">High</td>
                                  <td style="text-align:left">Do not undertake task.If operation is already in progress, abort and inform office.</td>
                             </tr>
                             <tr>
                                  <td class="a">Amber</td>
                                  <td>12 - 15</td>
                                  <td class="a">Warning</td>
                                  <td style="text-align:left">Job to be under taken only with office Approval.</td>
                             </tr>
                             <tr>
                                  <td class="b">Blue</td>
                                  <td>8 - 10</td>
                                  <td class="b">Medium</td>
                                  <td style="text-align:left">Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.</td>
                             </tr>
                             <tr>
                                  <td class="g">Green</td>
                                  <td>1 - 6</td>
                                  <td class="g">Low</td>
                                  <td style="text-align:left">Job can be under taken by ship staff.</td>
                             </tr>
                             </table>
                         </td>
                         <td>
                            <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr style="background-color:#c2c2c2;">
                                  <td colspan="2">Likelihood(of Consequence)</td>
                                  <td colspan="2">Severity(of Consequence) </td>
                             </tr>
                             <tr >
                                  <td>Very Likely</td>
                                  <td style="text-align:left">More frequently than 10% of the time/ cases</td>
                                  <td>Catastrophic</td>
                                  <td style="text-align:left">* Serious loss of reputation which is devastating for trust and respect<br />
                                      * Considerable economic loss which can not be restored<br />
                                      * More than 5 m3 to sea                                
                                  </td>
                             </tr>
                             <tr >
                                  <td>Likely</td>
                                  <td style="text-align:left">Occurs between 1% and 10% of the time/ cases</td>
                                  <td>Severe</td>
                                  <td style="text-align:left">* Serious loss of reputation that will influence trust and respect for a long time<br />
                                      * Lagre economic loss more than US$100,000 that can be restored <br />
                                      * 1 to 5 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>UnLikely</td>
                                  <td style="text-align:left">Occurs between 0.1% and 1% of the time/ cases</td>
                                  <td>Major</td>
                                  <td style="text-align:left">* Reduction of reputation that may influence trust and respect<br />
                                      * Economic loss between US$10,000 and US$100,000 which can be restored <br />
                                      * Less than 1 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Highly UnLikely</td>
                                  <td style="text-align:left">Occurs less than 0.1% of the time/ cases</td>
                                  <td>Minor</td>
                                  <td style="text-align:left">* Small reduction of reputation in the short run<br />
                                      * Economic loss upto US$10,000 which can be restored <br />
                                      * Sheen on sea : evidance of loss to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Almost Nill Chances</td>
                                  <td style="text-align:left">Never heard within the industry</td>
                                  <td>Negligible</td>
                                  <td style="text-align:left">* No effect on reputation<br />
                                      * Negligible economic loss which can be restored <br />
                                      * Nill to sea : contained onboard
                                  </td>
                             </tr>
                             </table>
                         </td>
                     </tr>
                   </table>
                </div>
        </center>
          <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
            <asp:Button runat="server" ID="Button28" Text="Close" OnClick="btnCloseGL_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
          </div>
          </div>
        </center>
    </div>
    <div ID="dv_Approve" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:60%;padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px black;">
        <center>
              <div class="box3" style='padding:10px 0px 10px  0px'><b>Review</b></div>
              <div id="dvApproveDetails" runat="server" >
                <table cellspacing="0" rules="none" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
                 <tr>
                    <td style="text-align:right; width:155px;"><b>Reviewer Comments :</b>&nbsp;</td>
                    <td style="text-align:left;"><asp:TextBox ID="txtReviewerComments" runat="server" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right; width:155px;"><b>Reviewer Name/ Position : </b>&nbsp;</td>
                    <td style="text-align:left;"><asp:TextBox ID="txtReviewerName" runat="server" Width="99%"></asp:TextBox></td>
                </tr>
                </table>
              </div>
        </center>
          <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
            <asp:Label runat="server" ID="lblMsg_Approve" style="float:left;color:Red"></asp:Label>
            <asp:Button runat="server" ID="btnSaveApprove" Text="Save" OnClick="btnSaveApprove_Click" CausesValidation="true" style=" background-color:green; color:White; border:solid 1px grey;width:100px;"/>
            <asp:Button runat="server" ID="Button1" Text="Close" OnClick="btnCloseApprove_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
          </div>
          </div>
        </center>
    </div>
    </div>
</form>
</body>
</html>
