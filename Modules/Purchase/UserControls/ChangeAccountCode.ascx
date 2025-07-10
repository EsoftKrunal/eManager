<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangeAccountCode.ascx.cs" Inherits="UserControls_ChangeAccountCode" %>
<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;">
    <style type="text/css">
        .rpt-table tr td{
            border:solid 1px #d5d2d2;
            padding:5px;
        }
        .Budgeted
        {
            background-color:green;
            color:white;
        }
        .UnBudgeted
        {
            background-color:red;
            color:white;
        }
    </style>
    <center>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :black;z-index:5; opacity:0.7;filter:alpha(opacity=40)"></div>
        <div style="position :relative;display:inline-block; padding :0px; text-align :center; border :solid 3px #276dc3; background-color : white; z-index:10;top:20px;opacity:1;filter:alpha(opacity=100)">
            <div style="font-weight:bold; padding:8px;font-size:18px; text-align:left;" class="text headerband">        
                <span style="float:right;margin-top:5px;">
                    <asp:ImageButton img="btnClose" runat="server" OnClick="btnCloseAccountCode_OnClick" ImageUrl="~/Modules/HRD/Images/closewindow.png" style="width:32px;" />
                </span>        
                <asp:Label ID="lblHeading" runat="server" Text="Select New Account Code & Allocation"></asp:Label>
                 
                <div style="padding:3px;font-style:italic;color:white;font-size:12px;font-weight:normal;">
                    &nbsp;<asp:Label ID="lblHeading1" runat="server" Text="( If no Allocation exists in new account please create new one )"></asp:Label>
                </div>
            </div>
            <div style="background-color:#d5d2d2;padding:6px;text-align:center;">  
                <asp:Button ID="btnOpenAddTaskPopup" runat="server" Text="+ Add New Budget Allocation"  Width="140px"  OnClick="btnOpenAddTaskPopup_OnClick" style=" background-color:#006EB8; color:White; border:none; padding:4px;float:right;" Visible="false" />              
                <table border="0" cellpadding="2" style="margin:0px auto;"  >
                <tr>
                    <td style="text-align:center;">Select New Account Code :</td>  
                    <td style="text-align:center;"><asp:DropDownList runat="server" ID="ddlaccts" AutoPostBack="true" OnSelectedIndexChanged="ddlaccts_OnSelectedIndexChanged"></asp:DropDownList></td>
                    
                </tr>
            </table>
                
            </div>
            <div id="divTaskList" runat="server" style="width:1000px;" visible="false">
                <div style="overflow-x:hidden;overflow-y:scroll;height:30px;">
                <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                    <colgroup>
                    <col width="50px" />
                    <col width="50px" />
                    <col />
                    <col width="150px" />
                    <col width="150px" />
                    <col width="250px" />  
                        </colgroup>
                    <tr >
                        <td>Select</td>
                        <td>B/U</td>
                        <td style="text-align:left;color:white;vertical-align:central;">Allocation Description</td>
                        <td style="text-align:right;color:white;vertical-align:central;">Allocation Budget (US$)</td>
                        <td style="text-align:right;color:white;vertical-align:central;">Consumed (US$)</td>
                        <td style="text-align:left;color:white;vertical-align:central;">Created By/On</td>           
                    </tr>
                </table>
            </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:450px;">
                        <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" class="rpt-table">
                            <colgroup>
                        <col width="50px" />
                        <col width="50px" />
                        <col />
                        <col width="150px" />
                        <col width="150px" />
                        <col width="250px" />
                            </colgroup>                           
                        <asp:Repeater ID="rptTrackingTaskList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>                                               
                                        <input type="radio" name="sel" taskid='<%#Eval("TaskID") %>' onclick="SetSelectedTaskID(this)" <%# ((Eval("TaskID").ToString()==TaksIDToUpdate.ToString() )?"checked":"") %> />
                                    </td>
                                    <td class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'>
                                        <span ><%# ((Eval("budgeted").ToString()=="True")?"B":"U") %></span>
                                    </td>
                                    <td style="text-align:left;"> 
                                        <%#Eval("TaskDescription") %> 
                                    </td>
                                    <td style="text-align:right;">
                                        <%# ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(Eval("Amount"))) %> 
                                        <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID") %>'  />
                                    </td>
                                    <td style="text-align:right;">
                                            <%#  ProjectCommon.FormatCurrencyWithoutSign(Eval("TotConsume")) %>                                                 
                                    </td>
                                    <td style="text-align:left;"> <%#Eval("ModifiedBy")%> / <%# Common.ToDateString(Eval("ModifiedOn")) %> </td>                                                    
                                </tr>                                     
                            </ItemTemplate>
                        </asp:Repeater>
                           
                    </table>
                </div>
                </div>
        <div style="text-align:left; padding:5px;">
            <asp:Label runat="server" ID="lblMsg" style="color:red;font-weight:bold;"></asp:Label>
        </div>
            <div style="text-align:center; padding:5px;">
                
                <asp:Button ID="btnSaveAccountCode" runat="server" Text="Save Changes"  Width="140px"  OnClick="btnSaveAccountCode_OnClick" style="  border:none; padding:4px;" CssClass="btn" />
            </div>
        </div>
    </center>
</div>


<%-- TrackingTask-----------------------------------------%>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddTrackingTask" visible="false" >
        <center>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:750px; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
        <center>
            
             <div style="font-size:16px;font-weight:bold;padding:8px; text-align:left;line-height:30px;background-color:#276dc3;color:white;" class="PageHeader">
                        <asp:ImageButton runat="server" ID="btnClsose1" OnClick="btnCloseAddTrackingTaskPopup_OnClick" ImageUrl="~/Images/closewindow.png" style="float:right;width:24px;" />
                    <span style="font-size:18px;">  Add New Task</span>
                    </div>             
            <div >
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; text-align:left; border-collapse:collapse;">
                            <col width="120px" />
                            <col />
                            <tr>
                                <td>
                                    <br />
                                    Task Type :
                                </td>
                            </tr>                                          
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlTaskType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskType_OnSelectedIndexChanged">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Budgeted"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Unbudgeted"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr> 
                            <tr>
                                <td >
                                    Task Description :                                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTtDescription" runat="server" TextMode="MultiLine" Width="99%" Rows="2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Budget Amount :                                       
                                </td>                                   
                            </tr>  
                            <tr>
                                <td >
                                    <asp:TextBox ID="txtTtAmount" runat="server"></asp:TextBox>                                   
                                </td>
                            </tr>                             
                           <%-- <tr>
                                <td>
                                    Expenses scheduled for months :                                      
                                </td>                                   
                            </tr>   
                            <tr>
                                <td>
                                    <table cellpadding="4" cellspacing="0" border="0" class="rpt-table table-centered" style="text-align:center;width:100%;">
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <tr class="group-3" >
                                            <td>Jan</td>
                                            <td>Feb</td>
                                            <td>Mar</td>
                                            <td>Apr</td>
                                            <td>May</td>
                                            <td>Jun</td>
                                            <td>Jul</td>
                                            <td>Aug</td>
                                            <td>Sep</td>
                                            <td>Oct</td>
                                            <td>Nov</td>
                                            <td>Dec</td>
                                        </tr>
                                        <tr>
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtJan" runat="server" />
                                </td>
                               
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtFeb" runat="server" />
                                </td>
                               
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtMar" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtApr" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtMay" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtJun" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtJul" runat="server" />
                                </td>
                               
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtAug" runat="server" />
                                </td>
                                    
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtSep" runat="server" />
                                </td>
                               
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtOct" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtNov" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtDec" runat="server" />
                                </td>
                            </tr>
                                    </table>
                                </td>
                            </tr>--%>
                                                                    
                        </table>
                        
                <div style="text-align:center;padding:5px;">
                    <asp:Button ID="btnSaveTrackingTask" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveTrackingTask_OnClick" />
                   
                </div>
                <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                    &nbsp; <asp:Label ID="lblMsgTrackingTask" runat="server" CssClass="error"></asp:Label>
                </div>
                </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnClsose1" />
                    </Triggers>
                </asp:UpdatePanel>           
            </div>
        </center>
        </div>
        </center>
    </div>

       

