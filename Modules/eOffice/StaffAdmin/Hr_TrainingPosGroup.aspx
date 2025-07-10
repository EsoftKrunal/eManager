<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingPosGroup.aspx.cs" Inherits="Emtm_Hr_TrainingPosGroup" %>
<%@ Register Src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMainMenu.ascx" TagName="Hr_TrainingMainMenu" TagPrefix="uc1" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/Hr_TrainingHeaderMenu.ascx" tagname="Hr_TrainingHeaderMenu" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <table width="100%">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             <div>
                                <uc1:Hr_TrainingMainMenu ID="Emtm_Hr_TrainingMainMenu1" runat="server" />
                             </div> 
                            </td>
                        </tr>
                         <tr>
                            <td>
                             <div>
                                <uc2:Hr_TrainingHeaderMenu ID="Emtm_Hr_TrainingHeaderMenu1" runat="server" /> 
                             </div> 
                            </td>
                        </tr>
                        </table> 
                            <table width="100%">
                            <tr>
                            <td style="width:350px">
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                        <colgroup>
                                            <col style="width:25px;" />  
                                            <col style="width:25px;" /> 
                                            <col style="width:25px;" />                                    
                                            <col />
                                            <col style="width:25px;" />
                                            <tr align="left" class="blueheader">
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align:left">Group Name&nbsp;</td>
                                                <td>&nbsp;Group Name</td>
                                            </tr>
                                        </colgroup>
                                    </table>
                                </div>
                                <div id="dvTG" onscroll="SetScrollPos(this)" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px; text-align:center;">
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:25px;" /> 
                                            <col style="width:25px;" /> 
                                            <col style="width:25px;" />                                     
                                            <col />
                                            <col style="width:25px;" />
                                        </colgroup>
                                        <asp:Repeater ID="rptTrainingGroup" runat="server">
                                            <ItemTemplate>
                                                <tr class='<%# (Common.CastAsInt32(Eval("GroupId"))==SelectedId)?"selectedrow":" "%>'>
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnTGView" runat="server" CausesValidation="false" 
                                                            CommandArgument='<%# Eval("GroupId") %>' 
                                                            ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnTGView_Click" 
                                                            ToolTip="View" />
                                                    </td>
                                            
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnTGEdit" runat="server" CausesValidation="false" 
                                                            CommandArgument='<%# Eval("GroupId") %>'  
                                                            ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnTGEdit_Click"
                                                            ToolTip="Edit" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnTGDelete" runat="server" CausesValidation="false" 
                                                            CommandArgument='<%# Eval("GroupId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                            OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  RowId='<%# Eval("GroupId") %>' 
                                                            OnClick="btnTGDelete_Click"
                                                            ToolTip="Delete" />
                                                    </td>
                                             
                                                    <td align="left"><%#Eval("GroupName")%></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </table>  
                                </div>
                                <div style="padding:5px">
                                    <asp:Button ID="btnAddNew" CssClass="btn" runat="server" Text=" + Add Group" Width = "100px" CausesValidation="false" onclick="btnAddNew_Click"></asp:Button>
                                </div>
                                </td>
                            <td style="vertical-align:top;">
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                        <colgroup>
                                            <col style="width:25px;" /> 
                                            <col style="width:150px" />
                                            <col />
                                            <col style="width:25px;" />
                                        </colgroup>
                                            <tr align="left" class="blueheader">
                                                <td></td>
                                                <td style="text-align:left">Office &nbsp;</td>
                                                <td style="text-align:left">PositionOffice </td>
                                                <td>&nbsp;Position</td>
                                            </tr>
                                    </table>
                                </div>
                                <div id="dvtgd" onscroll="SetScrollPos(this)" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:25px;" /> 
                                            <col style="width:150px" />
                                            <col />
                                            <col style="width:25px;" />
                                        </colgroup>
                                        <asp:Repeater ID="rptTrainings" runat="server">
                                            <ItemTemplate>
                                                <tr >
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnTrainingDelete" runat="server" CausesValidation="false" 
                                                            CommandArgument='<%# Eval("PositionID") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                            OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  
                                                            OnClick="btnTrainingDelete_Click"
                                                            ToolTip="Delete" />
                                                    </td>
                                                    <td align="left"><%#Eval("OfficeName")%></td>
                                                    <td align="left"><%#Eval("PositionName")%></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </table> 
                                </div>
                                <div style="padding:5px; text-align:right;">
                                    <asp:Button ID="btnAddTrainings" CssClass="btn" runat="server" Text=" + Add Positions" Width = "100px" CausesValidation="false" onclick="btnAddTrainings_Click"></asp:Button>
                                </div>
                            </td>
                        </tr>
                        </table>
                        <div style="padding:5px; background-color:#ecf880; border-top:solid 1px #e8e2e2">
                            &nbsp;<asp:Label ID="lbl_TrainingType_Message" runat="server" ForeColor="#C00000"></asp:Label>
                        </div>
                        </td>
                    </tr>
            </table>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddGroup" visible="false" >
                            <center>
                            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                            <div style="position :relative; width:800px;text-align :center; border :solid 5px #333;padding-bottom:5px; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                                <center>
                           <b>Add Position Group</b>
                          <table border="0" cellpadding="4" cellspacing="0" style="width:100%;">
                              <tr>
                                  <td style="text-align :right">
                                      &nbsp;Group Name :<asp:TextBox ID="txt_GroupName" runat="server" MaxLength="255" TabIndex="1" 
                                          Width="450px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_GroupName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>
                                  
                              </tr>
                          </table>
                          <div>
                            <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" onclick="btnsave_Click" ></asp:Button>
                            <asp:Button ID="btncancel" CssClass="btn"  runat="server" Text="Cancel" CausesValidation="false" onclick="btncancel_Click"></asp:Button>
                          </div>
                        </div>
                       </center>
                       </div>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvPositions" visible="false" >
                            <center>
                            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                            <div style="position :relative; width:800px;text-align :center; border :solid 5px #333;padding-bottom:5px; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                            <center>
                            <b>Select More Positions..</b>
                               <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                        <colgroup>
                                            <col style="width:25px;" /> 
                                            <col style="width:150px" />
                                            <col />
                                            <col style="width:25px;" />
                                        </colgroup>
                                            
                                            <tr align="left" class="blueheader">
                                                <td></td>
                                                <td style="text-align:left">Office </td>
                                                <td style="text-align:left">Position</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        
                                    </table>
                                <asp:Button ID="btnAddPos" CssClass="btn"  runat="server" Text="Save" onclick="btnSavePos_Click" ></asp:Button>
                                <asp:Button ID="btnCancelAdd" CssClass="btn"  runat="server" Text="Cancel" CausesValidation="false" onclick="btnCancelAdd_Click"></asp:Button>
                                </div>
                                <div id="Div1" onscroll="SetScrollPos(this)" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col style="width:25px;" /> 
                                            <col style="width:150px" />
                                            <col />
                                            <col style="width:25px;" />
                                        </colgroup>
                                        <asp:Repeater ID="rptAddPositions" runat="server">
                                            <ItemTemplate>
                                                <tr >
                                                    <td align="center">
                                                        <asp:CheckBox ID="chksel" runat="server" CssClass='<%# Eval("PositionID") %>' />
                                                    </td>
                                                    <td align="left"><%#Eval("OfficeName")%></td>
                                                    <td align="left"><%#Eval("PositionName")%></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            
                                            <tr align="left" class="blueheader">
                                                <td></td>
                                                <td style="text-align:left">Office </td>
                                                <td style="text-align:left">Position</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        
                                        </table> 
                                </div>
                                
                            </center>
                            </div>
                            </center>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>         
    </div>
    </form>
</body>
</html>
