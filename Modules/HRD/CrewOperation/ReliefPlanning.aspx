<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="ReliefPlanning.aspx.cs" Inherits="CrewOperation_ReliefPlanning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
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
    </style>
<!--  Sign Off Region -->
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div>
        <table cellspacing="0" border="0" width="100%" style="background-color:#f9f9f9" >
            <table width="100%" >
            <tr>
                <td colspan="2" style="padding:6px; background-color:#008AE6; font-size:14px; color:#fff; font-weight:bold;">Relief Planning - SignOff Crew</td>
            </tr>
            <tr>
            <td style="vertical-align:top; width:250px; background-color:#E0F5FF;;border:solid 1px #c2c2c2;">
             <asp:UpdatePanel ID="up1" runat="server">
             <ContentTemplate>
             <table style="width:100%;" cellpadding="2" cellspacing="0" border="0" >
                   <tr>
                       <td style="text-align:left">
                       <table style="width:100%;" cellpadding="2" cellspacing="0" border="0" >
                       <tr>
                       <td style="width:50%"><b>From Date:</b></td>
                       <td><b>To Date:</b></td>
                       </tr>
                       <tr>
                       <td style="text-align:left">
                           <asp:TextBox ID="txt_from" runat="server" CssClass="input_box" MaxLength="15" Width="90px"></asp:TextBox>
                       </td>
                       <td style="text-align:left"><asp:TextBox ID="txt_to" runat="server" CssClass="required_box" MaxLength="15" Width="90px"></asp:TextBox>
                         
                       </td>
                       </tr>
                       <tr>
                       <td><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_from" ErrorMessage="Invalid Date." ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator></td>
                       <td><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_to" ErrorMessage="Invalid Date." ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator></td>
                       </tr>
                       </table>
                       </td>
                   </tr>
                   <tr>
                       <td style="text-align:left">
                           <input type="checkbox" id="chkallrank0" onclick="SetRank();" /><b>[ All Rank ]</b></td>
                   </tr>
                   <tr>
                       <td style="text-align:left">
                           <asp:ListBox ID="chkrank" runat="server" CssClass="input_box" Height="110px" SelectionMode="Multiple" Width="234px"></asp:ListBox>
                       </td>
                   </tr>
                   <tr>
                       <td style="text-align:left">
                           <input type="checkbox" id="chkallvsl" onclick="SetVessel();"/><b>[ All Vessel ]</b></td>
                   </tr>
                   <tr>
                       <td style="text-align:left"><asp:ListBox ID="chkvessel" runat="server" CssClass="input_box" Width="234px" SelectionMode="Multiple" Height="150px"></asp:ListBox></td>
                   </tr>
                   <tr>
                       <td style="text-align:center">
                       <asp:Button ID="btnSearchSignOff" runat="server" Text="Search" style=" background-color:#008AE6; color:White; border:none; padding:3px; " OnClick="btnSearchSignOff_Click" />
                       </td>
                       </tr>
             </table>
             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_from"></ajaxToolkit:CalendarExtender>
             <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_to"></ajaxToolkit:CalendarExtender>
             </ContentTemplate>
             <Triggers>
             <asp:PostBackTrigger ControlID="btnSearchSignOff" />
             </Triggers>
             </asp:UpdatePanel>
                </td>
            <td valign="top" style=" text-align : left;">
                        <div style="height:25px; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0" style=" height:25px">
                        <tr style="padding:6px; background-color:#008AE6; font-size:11px; color:#fff; font-weight:bold;"> 
                        <td style="width:50px">Crew#</td>
                        <td>Crew Name</td>
                        <td style="width:50px">Rank</td>
                        <td style="width:80px">Nationality</td>
                        <td style="width:50px">Vessel</td>
                        <td style="width:80px">EOC</td>
                        <td style="width:80px">Sign On Dt.</td>
                        <td style="width:80px">Rel Due Dt.</td>
                        <td style="width:60px">Reliever</td>
                        <td style="width:40px">Checklist</td>
                        <td style="width:20px"></td>
                        </tr>
                        </table>
                        </div>
                        <div style="height:385px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0">
                        <asp:Repeater runat="server" ID="rpt_SignOffList">
                        <ItemTemplate>
                        <tr>
                        <td style="width:50px"><%#Eval("CrewNumber") %>
                        </td>
                        <td><%#Eval("CrewName")%></td>
                        <td style="width:50px"><%#Eval("RankName")%></td>
                        <td style="width:80px"><%#Eval("Nationality")%></td>
                        <td style="width:50px"><%#Eval("VesselName")%></td>
                        <td style="width:80px"><%#Eval("EOC")%></td>
                        <td style="width:80px"><%#Eval("SignOnDate")%></td>
                        <td style='width:80px; background-color:<%#Eval("signoffdate_color")%>'><%#Eval("SignOffDate")%></td>
                        <td style="width:80px">
                            <div>
                                <div style="float:left; width:20px;text-align:left; ">
                                    <asp:ImageButton ID="imgReliver_Plan" OnClick="imgReliver_Plan_Click" CommandArgument='<%#Eval("CrewID")%>' VesselId='<%#Eval("currentvesselid")%>' Visible='<%#Common.CastAsInt32(Eval("ReliverID"))<=0%>' runat="server" ImageUrl="~/Modules/HRD/Images/add12.gif" OnClientClick="this.src='../Images/loading.gif';" ToolTip="Plan Reliever"/>
                                </div>
                                <asp:Panel ID="Panel1" style="float:right; width:60px;" runat="server" Visible='<%#Common.CastAsInt32(Eval("ReliverID"))>0%>'>
                                    <img src="../Images/user_icon.png" style=" text-align:left; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("Details") %>')" onmouseout="if(t1)t1.Hide(event)">  
                                    <asp:ImageButton ID="imgReliver_Remove" runat="server" CommandArgument='<%#Eval("CrewID")%>' RelieverId='<%#Eval("ReliverID")%>' OnClick="imgReliver_Remove_Click" ImageUrl="~/Modules/HRD/Images/icon_delete_12.png" ToolTip="Remove Reliever" OnClientClick="return window.confirm('Are you sure to delete this planning?')"/>
                                </asp:Panel>
                            </div> 
                        </td>
                        <td style="width:40px"><img src="../Images/icon_note.png" onclick='<%#"OpenCheckList(" + Eval("PlanningId").ToString() + ");"%>' title="Open Document CheckList"/></td>
                        <td style="width:20px"></td>
                        </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                        <tr style="background-color:#E0F5FF">
                        <td style="width:50px"><%#Eval("CrewNumber") %></td>
                        <td><%#Eval("CrewName")%></td>
                        <td style="width:50px"><%#Eval("RankName")%></td>
                        <td style="width:80px"><%#Eval("Nationality")%></td>
                        <td style="width:50px"><%#Eval("VesselName")%></td>
                        <td style="width:80px"><%#Eval("EOC")%></td>
                        <td style="width:80px"><%#Eval("SignOnDate")%></td>
                        <td style='width:80px; background-color:<%#Eval("signoffdate_color")%>'><%#Eval("SignOffDate")%></td>
                        <td style="width:80px">
                            <div>
                                <div style="float:left; width:20px;text-align:left; ">
                                    <asp:ImageButton ID="imgReliver_Plan" OnClick="imgReliver_Plan_Click" CommandArgument='<%#Eval("CrewID")%>' VesselId='<%#Eval("currentvesselid")%>' Visible='<%#Common.CastAsInt32(Eval("ReliverID"))<=0%>' runat="server" ImageUrl="~/Modules/HRD/Images/add12.gif" OnClientClick="this.src='../Images/loading.gif';" ToolTip="Plan Reliever"/>
                                </div>
                                <asp:Panel ID="Panel1" style="float:right; width:60px;" runat="server" Visible='<%#Common.CastAsInt32(Eval("ReliverID"))>0%>'>
                                    <img src="../Images/user_icon.png" style=" text-align:left; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("Details") %>')" onmouseout="if(t1)t1.Hide(event)">  
                                    <asp:ImageButton ID="imgReliver_Remove" runat="server" CommandArgument='<%#Eval("CrewID")%>' RelieverId='<%#Eval("ReliverID")%>' OnClick="imgReliver_Remove_Click" ImageUrl="~/Modules/HRD/Images/icon_delete_12.png" ToolTip="Remove Reliever" OnClientClick="return window.confirm('Are you sure to delete this planning?')"/>
                                </asp:Panel>
                            </div> 
                        </td>
                        <td style="width:40px"><img src="../Images/icon_note.png" onclick='<%#"OpenCheckList(" + Eval("PlanningId").ToString() + ");"%>' title="Open Document CheckList"/></td>
                        <td style="width:20px"></td>
                        </tr>
                        </AlternatingItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
                        </td>
            </tr>
            </table>
            </tr>            
        </table>
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
                selectDeselect(document.getElementById("ctl00_contentPlaceHolder1_chkrank"), document.getElementById("chkallrank0"));
            }
            function SetVessel() {
                selectDeselect(document.getElementById("ctl00_contentPlaceHolder1_chkvessel"), document.getElementById("chkallvsl"));
            }
            function OpenCheckList(PlanningId) {
                window.open('ViewCrewCheckList.aspx?_P=' + PlanningId);
            }

        </script>
</div>
<!--  Sign On Region -->

<div>
<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_SignOn" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:1100px; padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
            <center >
            <iframe id="frmSignOn" runat="server" src="" height="440px" width="100%" scrolling="no" frameborder="0">

            </iframe>
            <asp:Button ID="btn_Close_Search" CausesValidation="false" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btn_Close_Search_Click" />
            </center>
        </div>
    </center>
</div>
</div>
</asp:Content>

