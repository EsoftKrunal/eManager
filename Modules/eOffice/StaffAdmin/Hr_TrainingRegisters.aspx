<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingRegisters.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_TrainingRegisters" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%@ Register Src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMainMenu.ascx" TagName="Hr_TrainingMainMenu" TagPrefix="uc1" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/Hr_TrainingHeaderMenu.ascx" tagname="Hr_TrainingHeaderMenu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>EMANAGER</title>

    <div style="font-family:Arial;font-size:12px;">
    
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
                        <br />
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                        <tr>
                        <td>
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:25px;" />                                     
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />                                    
                                    <col />
                                    <col style="width:200px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class= "headerstylegrid">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            Training Group</td>
                                        <td>
                                            Status</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                        <div id="dvTG" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 280px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:25px;" />
                                   <col style="width:25px;" />
                                   <col style="width:25px;" />
                                   <col />
                                   <col style="width:200px;" />
                                   <col style="width:25px;" />
                               </colgroup>
                                <asp:Repeater ID="rptTrainingGroup" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("TrainingGroupId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btnTGView" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TrainingGroupId") %>' 
                                                    ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnTGView_Click" 
                                                    ToolTip="View" />
                                            </td>
                                            
                                            <td align="center">
                                                <asp:ImageButton ID="btnTGEdit" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TrainingGroupId") %>'  
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnTGEdit_Click"
                                                    ToolTip="Edit" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnTGDelete" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TrainingGroupId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                    OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  RowId='<%# Eval("TrainingGroupId") %>' 
                                                    OnClick="btnTGDelete_Click"
                                                    ToolTip="Delete" />
                                            </td>
                                             
                                            <td align="left">
                                                <%#Eval("TrainingGroupName")%>
                                                <asp:HiddenField ID="HiddenTrainingGroup" Value='<%#Eval("TrainingGroupName")%>' runat="server" /> </td>
                                            <td align="left">
                                                <%#Eval("StatusId")%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                        <div id = "divAddEdit" runat="server" visible = "false">  
                        <fieldset style=" height :40px">
                         <legend><strong>Training Group</strong></legend> 
                          <table border="0" cellpadding="0" cellspacing="6" style="width:100%;">
                              <tr>
                                  <td style="text-align :right">
                                      &nbsp;Training Group:</td>
                                  <td>
                                      <asp:TextBox ID="txt_TrainingGroup" runat="server" MaxLength="50" TabIndex="1" Width="300px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_TrainingGroup" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>
                                  <td style="text-align :right">
                                      &nbsp;Status :</td>
                                  <td>
                                      <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="2" Width="205px">
                                       <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                                       <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                                  
                              </tr>
                         
                        </table>
                         
                        </fieldset>
                        </div>
                        <div >
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_TrainingType_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnAddNew" CssClass="btn" runat="server" Text="Add New" Width = "100px" 
                                            CausesValidation="false" onclick="btnAddNew_Click"></asp:Button>
                                        <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" Visible="false" OnClick="btnsave_Click">
                                        </asp:Button>
                                        <asp:Button ID="btncancel" CssClass="btn" runat="server" Text="Cancel" Visible="false" CausesValidation="false"
                                            OnClick="btncancel_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </td>
                        </tr>
                        </table>
                       
                    
                    </td>
                    </tr>
            </table>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers>
        </asp:UpdatePanel>         
    </div>
    
   </asp:Content>
