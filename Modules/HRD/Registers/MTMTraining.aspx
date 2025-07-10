<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MTMTraining.aspx.cs" Inherits="MTMTraining" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;">
        <tr>
       <td>
          </td>
    </tr>  
        <tr>
            <td>
                    <div style="overflow-x:hidden;overflow-y:hidden; width: 100%;">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <col width="40px" />
                            <col width="40px" />
                            <col width="50px" />
                            <col />
                            <col width="80px" />
                            <col width="17px" />
                            <tr class="headerstylegrid" style="font-weight:bold;">    
                                <td>View</td>
                                <td>Edit</td>
                                <td>Delete</td>
                                <td>Training</td>
                                <td>Status</td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 150px">
                        <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows">
                            <col width="40px" />
                            <col width="40px" />
                            <col width="50px" />
                            <col />
                            <col width="80px" />
                            <asp:Repeater ID="rptTraining" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td> 
                                            <asp:ImageButton ID="imgView" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="imgView_OnClick" /> 
                                            <asp:HiddenField ID="hfTid" runat="server" Value='<%#Eval("TRAININGID")%>' />
                                            <asp:HiddenField ID="hfStatusID" runat="server" Value='<%#Eval("STATUSID")%>' />
                                            
                                            <asp:HiddenField ID="hfUpdatedBy" runat="server" Value='<%#Eval("CreatedBy")%>' />
                                            <asp:HiddenField ID="hfUpdatedOn" runat="server" Value='<%#Eval("CreatedOn")%>' />
                                            <asp:HiddenField ID="hfModifyBy" runat="server" Value='<%#Eval("ModifiedBy")%>' />
                                            <asp:HiddenField ID="hfModifyOn" runat="server" Value='<%#Eval("ModifiedOn")%>' />
                                        </td>
                                        <td> <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEdit_OnClick" /> </td>
                                        <td> <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelete_OnClick" OnClientClick="return confirm('Are you sure to delete?')" /> </td>
                                        <td style="text-align:left;"> 
                                            <asp:Label ID="lblTName" runat="server" Text='<%#Eval("TrainingName")%> ' ></asp:Label>
                                            
                                        </td>
                                        <td style="text-align:left;"> <%#Eval("Status")%> </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Label ID="lblTraining" runat="server"></asp:Label>
                <asp:Label ID="lbl_Training_Message" runat="server" ForeColor="#C00000">Record Successfully Saved.</asp:Label>
                    
                </td>
        </tr>
            <tr>
                <td style="padding-top:5px">
                    
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <asp:Button ID="btn_Training_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_Training_add_Click" Text="Add" Width="59px" TabIndex="3" style="margin-right:17px;" />
                    
                    <asp:Button ID="btn_Print_Training" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Training_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Trainingpanel');" Visible="False" />                 
               </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
    
    <div id="divAddTraining" style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100; font-family:Arial;font-size:12px;" runat="server" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:170px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                    
                <asp:Panel ID="Trainingpanel" runat="server" Visible="false" Width="100%">
                     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                <legend><strong>Training Details</strong></legend>
                                      <table cellpadding="0" cellspacing="0" width="100%">
                                          <tr>
                                             <td colspan="4">
                                                 
                                                 &nbsp;
                                             </td>
                                          </tr>
                                          <tr>
                                             <td align="right" style="padding-right:15px; height: 19px;">
                                                 Training:</td>
                                                 
                                             <td align="left">
                                                 <asp:TextBox ID="txtTrainingname" runat="server" CssClass="required_box" MaxLength="255" TabIndex="1"
                                                     Width="240px"></asp:TextBox></td>
                                             <td align="right" style="padding-right: 15px; height: 19px">
                                                 Status:</td>
                                             <td align="left">
                                             <asp:DropDownList ID="ddstatus_Training" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
                                                 </asp:DropDownList>
                                             </td>
                                          </tr>
                                          <tr>
                                              <td align="right" style="padding-right: 15px; height: 13px">
                                              </td>
                                              <td align="left" style="height: 13px">
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTrainingname"
                                                      ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                              <td align="right" style="height: 13px">
                                              </td>
                                              <td align="left" style="height: 13px">
                                                  
                                          </tr>
                                          <tr>
                                             <td align="right" style="padding-right:15px; height: 19px;">
                                                 Created By:</td>
                                                
                                             <td align="left" style="height: 20px">
                                                 <asp:TextBox ID="txtcreatedby_Training" runat="server" CssClass="input_box" MaxLength="24"
                                                     Width="240px" ReadOnly="True"></asp:TextBox></td>
                                             <td align="right" style="padding-right:15px; height: 19px;">
                                                 Created On:</td>
                                             <td align="left" style="height: 20px">
                                                 <asp:TextBox ID="txtcreatedon_Training" runat="server" CssClass="input_box" MaxLength="24"
                                                     Width="240px" ReadOnly="True"></asp:TextBox></td>
                                          </tr>
                                          <tr>
                                              <td colspan="4">
                                               &nbsp; &nbsp;
                                              </td>
                                          </tr>
                                          <tr>
                                             <td align="right" style="padding-right:15px; height: 19px;">
                                                 Modified By:</td>
                                                
                                             <td align="left" style="height: 20px">
                                                 <asp:TextBox ID="txtmodifiedby_Training" runat="server" CssClass="input_box" MaxLength="24"
                                                     Width="240px" ReadOnly="True"></asp:TextBox></td>
                                             <td align="right" style="padding-right:15px; height: 19px;">
                                                 Modified On:</td>
                                             <td align="left" style="height: 20px">
                                                 <asp:TextBox ID="txtmodifiedon_Training" runat="server" CssClass="input_box" MaxLength="24"
                                                     Width="240px" ReadOnly="True"></asp:TextBox></td>
                                          </tr>
                                          <tr>
                                              <td colspan="4">
                                               &nbsp; &nbsp;
                                              </td>
                                          </tr>
                                          <tr>
                                              <td colspan="4" style="padding:4px;">
                                               <asp:Button ID="btn_Training_save" runat="server" CssClass="btn" OnClick="btn_Training_save_Click"
                                                    Text="Save" Width="59px" Visible="False" TabIndex="4" />
                                                <asp:Button ID="btn_Training_Cancel" runat="server"
                                                        CausesValidation="false" CssClass="btn" OnClick="btn_Training_Cancel_Click" Text="Cancel" style="margin-right:2px;"
                                                        Width="59px" Visible="False" TabIndex="5" />
                                              </td>
                                          </tr>
                                      </table>
                     </fieldset>
                </asp:Panel>                                                                         
            </div>
            
        </center>
    </div>
            
    </form>
</body>
</html>
