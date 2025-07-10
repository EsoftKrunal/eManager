<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyTasks.aspx.cs" Inherits="emtm_Emtm_MyTasks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Tasks</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <style>
     .monthtd
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #C2C2C2;
     	cursor:pointer;  
     }
     .monthtdselected
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #E5A0FC;
     	cursor:pointer;
     }
     .box_
     {
     	background-color :White; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_H
     {
     	background-color :Orange; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_W
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px gray;
     }	
     .box_L
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px gray;
     }
     .box_P
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_B
     {
     	background-color :Purple;
     	color :White; 
     	border :solid 1px gray;
     }
     </style> 
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             window.close();
         }         
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <div>
          <table width="100%">
                <tr>
                    <td valign="top" style="border:solid 1px #4371a5; padding:5px;">
                     <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                       My Tasks
                                  </div>                       
                        
                        <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none; height:30px;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:30px;" bordercolor="white">
                            <colgroup>
                                <col style="width:150px;" />                                
                                <col />
                                <col style="width:100px;" />
                                <col style="width:60px;" />
                                <col style="width:25px;" />
                                <tr align="left" style="background-color:#1C5E55; font-weight:bold; color:White;">                                    
                                    <td>&nbsp;Source</td>
                                    <td>&nbsp;Task</td>
                                    <td>&nbsp;Target Date</td>
                                    <td>&nbsp;Action</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                   </table>  
                   </div>
                   <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                        <colgroup>
                                <col style="width:150px;" />                                
                                <col />
                                <col style="width:100px;" />
                                <col style="width:60px;" />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="rptTasks" runat="server"> 
                            <ItemTemplate>
                                <tr style="background-color:#E3EAEB;">                                   
                                    <td align="left">&nbsp;<%#Eval("Source")%></td>
                                    <td align="left">&nbsp;<%#Eval("FollowUpText")%></td>
                                    <td align="center">&nbsp;<%# Common.ToDateString(Eval("TCDate"))%></td>
                                    <td align="center"><asp:ImageButton ID="btnClosure" CommandArgument='<%#Eval("FollowUpId")%>' ImageUrl="~/Modules/HRD/Images/icon_delete_12.png" OnClientClick="return confirm('Are you sure to close?')" OnClick="btnClosure_Click" ToolTip="Closure" runat="server" /> </td>
                                    <td>&nbsp;</td>
                                </tr>                                
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color:White;">                                   
                                    <td align="left">&nbsp;<%#Eval("Source")%></td>
                                    <td align="left">&nbsp;<%#Eval("FollowUpText")%></td>
                                    <td align="center">&nbsp;<%# Common.ToDateString(Eval("TCDate"))%></td>
                                    <td align="center"><asp:ImageButton ID="btnClosure" CommandArgument='<%#Eval("FollowUpId")%>' ImageUrl="~/Modules/HRD/Images/icon_delete_12.png" OnClientClick="return confirm('Are you sure to close?')" OnClick="btnClosure_Click" ToolTip="Closure" runat="server" /> </td>
                                    <td>&nbsp;</td>
                                </tr>  
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div>
                                                                      
                     </td>
                     </tr>
                     </table>
    </div>

    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dv_Closure" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:50%; padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 10px black;">
            <div style='background-color:#FFEBCC; padding:8px; font-weight:bold; color:Maroon;'>Closure</div>
                <div >          
                <table width="100%" border="0" cellpadding="3" cellspacing="0" >
                        <tr>
                <td style="padding-right: 5px; text-align: right;">
                    Closed Date :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_ClosedDate" runat="server" CssClass="input_box" Width="122px" MaxLength="11"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_ClosedDate"></ajaxToolkit:CalendarExtender>
                </td>
                <td style="padding-right: 5px; text-align: right">
                    Cause :</td>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="311px">
                        <asp:ListItem Value="1">People</asp:ListItem>
                        <asp:ListItem Value="2">Process</asp:ListItem>
                        <asp:ListItem Value="3">Equipment</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top">
                    Remarks :</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txt_ClosedRemarks" runat="server" CssClass="input_box" Height="140px"
                        TextMode="MultiLine" Width="99%"></asp:TextBox></td>
            </tr>
           <tr>
                <td style="padding-right: 5px; text-align: right" valign="top" class="style3">
                    Closure Evidence :
                </td>
                <td style="text-align: left" class="style2" colspan="3" >
                    <asp:FileUpload runat="server" ID="flp_Evidence" Width="300px" CssClass="input_box" />                    
                    </td>
            </tr>            
                </table>       
                </div>
                <div><asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label></div>
                <div style="text-align:center; padding:10px;">
                   <asp:Button ID="btnSave" Text="Save" CssClass="btn" ValidationGroup="txt" OnClick="btnSave_Click" runat="server"  Width="120px"/>
                   <asp:Button ID="btnClose" CausesValidation="false" OnClick="btnClose_Click" Text="Close" CssClass="btn" Width="120px" runat="server" style="background-color:#FF3333"/>
                </div>
                    
         </div>
         </center>
    </div>

    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSave" />
    </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
