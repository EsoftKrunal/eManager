<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingMaster.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_TrainingMaster" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%@ Register Src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMainMenu.ascx" TagName="Hr_TrainingMainMenu" TagPrefix="uc1" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/Hr_TrainingHeaderMenu.ascx" tagname="Hr_TrainingHeaderMenu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">


style="font-family:Arial;font-size:12px;"
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common.js" type="text/javascript"></script>

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
                        <div style="padding:5px; background:#eee8e8;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width:80px; text-align:right;">Status :</td>
                                    <td style="width:130px"><asp:DropDownList runat="server" ID="ddlRStatus">
                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                    <td style="width:80px;  text-align:right;">Matrix :</td>
                                    <td style="width:130px">
                                        <asp:DropDownList runat="server" ID="ddlMatrix">
                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="btn" Font-Bold="true" />

                                    </td>                                   
                                </tr>
                                </table>
                        </div>
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
                                    <col style="width:250px;" />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:70px;" />
                                    <col style="width:105px;" />
                                    <tr align="left" class= "headerstylegrid">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Training Name</td>
                                        <td>Short Name</td>
                                        <td>Training Group</td>
                                        <td>Validity(Month)</td>
                                        <td>Status</td>
                                        <td>Show In Matrix</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                        <div id="dvTG" onscroll="SetScrollPos(this)" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 250px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:25px;" />                                     
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />                                    
                                    <col />
                                    <col style="width:250px;" />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:70px;" />
                                    <col style="width:105px;" />
                               </colgroup>
                                <asp:Repeater ID="rptTrainingGroup" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("TrainingId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btnTGView" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TrainingId") %>' 
                                                    ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnTGView_Click" 
                                                    ToolTip="View" />
                                            </td>
                                            
                                            <td align="center">
                                                <asp:ImageButton ID="btnTGEdit" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TrainingId") %>'  
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnTGEdit_Click"
                                                    ToolTip="Edit" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnTGDelete" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TrainingId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                    OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  RowId='<%# Eval("TrainingGroupId") %>' 
                                                    OnClick="btnTGDelete_Click"
                                                    ToolTip="Delete" />
                                            </td>
                                             
                                            <td align="left">
                                                <%#Eval("TrainingName")%>
                                                <%#Common.CastAsInt32(Eval("ValidityPeriod")) > 0 ? "<span style='color:green;font-weight:bold;'>[R]</span>" : ""%>
                                                
                                                <asp:HiddenField ID="hfTrainingName" Value='<%#Eval("TrainingName")%>' runat="server" /> </td>
                                            <td align="left">
                                                <%#Eval("ShortName")%>
                                                <asp:HiddenField ID="hfShortName" Value='<%#Eval("ShortName")%>' runat="server" /> </td>
                                            <td align="left">
                                                <%#Eval("TrainingGroupName")%>
                                                <asp:HiddenField ID="hfTrainingGroup" Value='<%#Eval("TrainingGroupName")%>' runat="server" />
                                                </td>
                                            <td align="center">
                                                <%#Eval("ValidityPeriod")%>
                                            </td>
                                            <td align="left">
                                                <%#Eval("StatusId")%></td>
                                           <td><%#Eval("ShowInMatrix")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                              <div id = "divAddEdit" runat="server" visible = "false">
                        <fieldset style=" height :100px">
                         <legend><strong>Training Master</strong></legend> 
                          <table border="0" cellpadding="2" cellspacing="0" style="width:100%;">
                              <tr>
                                  <td style="text-align :right">
                                      &nbsp;Training Name :</td>
                                  <td>
                                      <asp:TextBox ID="txt_TrainingName" runat="server" MaxLength="255" TabIndex="1" 
                                          Width="450px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_TrainingName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>
                                  <td style="text-align :right">
                                      &nbsp;Training Group :</td>
                                  <td>
                                      <asp:DropDownList ID="ddlTrainingGroup" runat="server" TabIndex="2" Width="205px"></asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTrainingGroup" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>
                                  
                              </tr>
                              <tr>
                                 <td style="text-align :right">
                                      Short Name :</td>
                                  <td>
                                      <asp:TextBox ID="txtShortName" runat="server" MaxLength="15" TabIndex="3" 
                                          Width="450px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShortName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                      </td>
                                  <td style="text-align :right">
                                      Status :</td>
                                  <td>
                                      <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="3" Width="80px">
                                          <asp:ListItem Selected="True" Text="Active" Value="A"></asp:ListItem>
                                          <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                      </asp:DropDownList>
                                      
                                  </td>                                  
                              </tr>
                           
                           <tr>
                              <td style="text-align :right;">Validity :</td>
                              <td>
                                    <asp:TextBox runat="server" ID="txtValidity" Width="50px" MaxLength="2"></asp:TextBox><b>(Month)</b>
                              </td>  
                              <td style="text-align :right">Show in Matrix</td>
                              <td><asp:CheckBox runat="server" ID="chkShowInMatrix" /></td>                              
                          </tr>
                                                   
                          <tr>
                              <td></td>
                              <td></td>
                              <td></td>
                              <td></td>
                              
                          </tr>
                        </table>
                         <br />
                        </fieldset>
                        </div>
                        <table width="100%" cellpadding="0" cellspacing="0" > 
                    <tr>
                    <td>
                      <asp:Label ID="lbl_TrainingType_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;
                      </td>
                    <td align="right">
                    <asp:Button ID="btnAddNew" CssClass="btn" runat="server" Text="Add New" Width = "100px" 
                                            CausesValidation="false" onclick="btnAddNew_Click"></asp:Button>
                    <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" Visible="false"
                            onclick="btnsave_Click" ></asp:Button>
                    <asp:Button ID="btncancel" CssClass="btn"  runat="server" Text="Cancel" Visible="false"
                            CausesValidation="false" onclick="btncancel_Click"></asp:Button>
                    </td>
                    </tr>
                    </table>
                        <div>
                        
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
