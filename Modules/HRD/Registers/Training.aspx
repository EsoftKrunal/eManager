<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Training.aspx.cs" Inherits="Registers_Training" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
   <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
            function setOptionValue()
        {
            var txt=document.getElementById("txtTraining");
            var ddl=document.getElementById("ddlTrainings");
            txt.value=ddl.options[ddl.selectedIndex].value;
            if(ddl.selectedIndex==0)
            {
                alert ("Please Select Training.");
                ddl.focus();
                return false; 
            }
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;">
        <tr>
            <td>
                <table cellpadding="2" cellspacing="0" border="0" width="100%" style="margin-top:4px; margin-bottom:3px;">
                    <col width="70px" />
                    <col width="120px" />
                  <%--  <col width="100px" />--%>
                    <col width="200px"  />
                    <col align="left" style="padding-left:15px;" />
                    <tr style="text-align:center; font-weight:bold;">
                        <td>Training Group </td>
                        <td>CBT# </td>
                      <%--  <td>Sire Chapter </td>--%>
                        <td>Training Name </td>
                        <td></td>
                    </tr>
                    <tr style="text-align:center;">
                        
                        <td>
                            <asp:DropDownList ID="ddlTrainingTypeSrch" runat="server" CssClass="input_box" ></asp:DropDownList>
                        </td>
                        
                        
                        
                        <td>
                            <asp:TextBox ID="txtCBTNoSrch" runat="server"  CssClass="input_box" Width="80px" MaxLength="20"></asp:TextBox>
                        </td>
                        
                        <%--<td>
                            <asp:DropDownList ID="ddlSireChp" runat="server" CssClass="input_box" ></asp:DropDownList>
                        </td>--%>
                        
                        <td>
                            <asp:TextBox ID="txtTrianingNameSrch" runat="server"  CssClass="input_box" Width="400px" MaxLength="27"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtTrianingNameSrch" ServicePath="~/WebService.asmx" ServiceMethod="GetTrainingName" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters="" CompletionListCssClass="CList"  CompletionListItemCssClass="CListItem" CompletionListHighlightedItemCssClass="CListItemH"></ajaxToolkit:AutoCompleteExtender>
                        </td>
                        
                        <td>
                           <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" OnClick="btnSearch_OnClick" />
                           <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" OnClick="btnClear_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>  
        <tr>
            <td>
                        <table cellpadding="2" cellspacing="0" width="100%" rules="all" style="border-collapse:collapse;">
                            <col width="30px" />
                            <col width="30px" />
                            <col width="30px" />
                            <col width="120px" />
                            <col width="200px"/>
                            <col width="180px" />
                           <%-- <col width="180px" />--%>
                            <col width="100px" />
                            <col width="17px" />
                            <tr class="headerstylegrid" style="font-weight:bold;">
                                <td ></td>
                                <td></td>
                                <td></td>
                                <td>CBT#</td>
                                <td>Training</td>
                                <td>Training Type</td>
                               <%-- <td>Sire Chapter</td>--%>
                                <td>Status</td>
                                <td>&nbsp;</td>
                            </tr>
                            </table>
                            
               <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 270px">
                        <table cellpadding="2" cellspacing="0" width="100%" rules="all" style="border-collapse:collapse;">
                            <col width="30px" />
                            <col width="30px" />
                            <col width="30px" />
                            <col width="120px" align="left" />
                            <col width="200px" align="left" />
                            <col width="180px" align="left" />
                            <%--<col width="120px" align="left" />--%>
                            <col width="100px" align="left" />
                            <col width="17px" align="left" />
                            
                            <asp:Repeater ID="GvTraining" runat="server"  >
                            <ItemTemplate>
                                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                                    <td> 
                                        <asp:ImageButton ID="imgView" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="GvTraining_SelectIndexChanged" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="GvTraining_Row_Editing" Visible='<% #Auth.isEdit %>' />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" OnClick="GvTraining_Row_Deleting" Visible='<% #Auth.isDelete %>' />
                                    </td>
                                    <td style="text-align:left"><%#Eval("CBTNo")%> </td>
                                    
                                    <td style="text-align:left">
                                        <asp:Label ID="lblTrainingname" runat="server" Text='<%#Eval("TrainingName")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenTrainingId" runat="server" Value='<%#Eval("TrainingId")%>' />
                                        <asp:HiddenField ID="HiddenTrainingName" runat="server" Value='<%#Eval("TrainingName")%>' />
                                    </td>
                                    <td style="text-align:left">
                                        <%#Eval("TypeOfTraining")%>
                                    </td>
                                  <%--  <td style="text-align:left">
                                        <asp:Label ID="lblSireChap" runat="server" Text='<%#Eval("SireChap")%>' ></asp:Label>
                                    </td>--%>
                                    <td style="text-align:left">
                                        <%#Eval("StatusId")%>
                                    </td>
                                
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
                <td style="padding:5px">
                    
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
                        OnClick="btn_Training_add_Click" Text="Add" Width="59px" TabIndex="3" style="margin-right:10px;" />
                    
                    <asp:Button ID="btn_Print_Training" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Training_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Trainingpanel');" Visible="False" />                 
               </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
</div>

                    <div id="divAddTraining" style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100; font-family:Arial;font-size:12px;" runat="server" visible="false" >
                        <center>
                            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                            <div style="position :relative; width:1000px; height:330px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:5px;opacity:1;filter:alpha(opacity=100)">
                                
                                <asp:Panel ID="Trainingpanel" runat="server" Visible="false" Width="100%">
                                     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                <legend><strong>Training Details</strong></legend>
                                                      <table cellpadding="2" cellspacing="1" width="100%">
                                                          <tr>
                                                             <td colspan="4">
                                                                 <asp:HiddenField ID="HiddenTrainingpk" runat="server" />
                                                                 &nbsp;
                                                             </td>
                                                          </tr>
                                                          <tr>
                                                             <td align="right" style="padding-right:15px; height: 19px;">
                                                                 Training:</td>
                                                                 
                                                             <td align="left">
                                                                 <asp:TextBox ID="txtTrainingname" runat="server" CssClass="required_box" MaxLength="255" TabIndex="1"
                                                                     Width="240px"></asp:TextBox>
                                                                     
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                     ControlToValidate="txtTrainingname" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                                                     
                                                             </td>
                                                             <td align="right" style="padding-right: 15px; height: 19px">
                                                                 Training Group:</td>
                                                             <td align="left">
                                                                 <asp:DropDownList ID="dd_trainingType" runat="server" CssClass="required_box" Width="245px" TabIndex="2">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                     ControlToValidate="dd_trainingType" ErrorMessage="Required."></asp:RequiredFieldValidator>--%>
                                                             </td>
                                                          </tr>
                                                          <%--<tr>
                                                              <td align="right" style="padding-right:15px; height: 19px;">
                                                                  &nbsp;</td>
                                                              <td align="left">
                                                                  &nbsp;</td>
                                                              <td align="right" style="padding-right: 15px; height: 19px">
                                                                  &nbsp;</td>
                                                              <td align="left">
                                                                  <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                                      ControlToValidate="dd_trainingType" ErrorMessage="Required." 
                                                                      MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                              </td>
                                                          </tr>--%>
                                                          <tr>
                                                              <%--<td align="right" style="padding-right: 15px; height: 19px">
                                                                  MTM Training:</td>
                                                              <td align="left">
                                                                  <asp:DropDownList ID="dd_MTMTraining" runat="server" CssClass="input_box" 
                                                                      TabIndex="2" Width="245px">
                                                                  </asp:DropDownList>
                                                              </td>--%>
                                                              <td align="right" style="height: 19px; padding-right: 15px;">
                                                                  Status:</td>
                                                              <td align="left">
                                                                  <asp:DropDownList ID="ddstatus_Training" runat="server" CssClass="input_box" 
                                                                      TabIndex="2" Width="129px">
                                                                  </asp:DropDownList>
                                                              </td>
                                                              <td></td>
                                                              <td></td>
                                                          </tr>
                                                           <%--<tr>
                                                              <td align="right" style="padding-right:15px; height: 19px;">
                                                                  &nbsp;</td>
                                                              <td align="left">
                                                                  &nbsp;</td>
                                                              <td align="right" style="padding-right: 15px; height: 19px">
                                                                  &nbsp;</td>
                                                              <td align="left">
                                                                  <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                                                      ControlToValidate="dd_trainingType" ErrorMessage="Required." 
                                                                      MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                              </td>
                                                          </tr>--%>
                                                          <tr>
                                                                <td align="right" style="padding-right:15px; height: 19px;"> CBT# :</td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCBTNo" runat="server" MaxLength="15" CssClass="input_box" ></asp:TextBox>
                                                                </td>
                                                                <td align="right" style="padding-right:15px; height: 19px;"> <%--Sire Chapter :--%></td>
                                                                <td align="left">
                                                                   <%-- <asp:DropDownList ID="ddlSireChap" runat="server" CssClass="input_box" Width="330px" ></asp:DropDownList>--%>
                                                                </td>
                                                          </tr>
                                                          <%--<tr>
                                                             <td align="right" style="padding-right:15px; height: 13px;">
                                                                 </td>
                                                                
                                                             <td align="left" style="height: 13px">
                                                                 
                                                              </td>
                                                             <td align="right" style="height: 13px;">
                                                                 </td>
                                                             <td align="left" style="height: 13px">
                                                                 &nbsp;</td>
                                                          </tr>--%>
                                                          <tr runat="server" visible="false" >
                                                              <td align="right" style="padding-right:15px; height: 19px;">
                                                                  Created By:</td>
                                                              <td align="left" style="height: 20px">
                                                                  <asp:TextBox ID="txtcreatedby_Training" runat="server" CssClass="input_box" 
                                                                      MaxLength="24" ReadOnly="True" Width="240px"></asp:TextBox>
                                                              </td>
                                                              <td align="right" style="padding-right:15px; height: 19px;">
                                                                  Created On:</td>
                                                              <td align="left" style="height: 20px">
                                                                  <asp:TextBox ID="txtcreatedon_Training" runat="server" CssClass="input_box" 
                                                                      MaxLength="24" ReadOnly="True" Width="240px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <%--<tr>
                                                             <td colspan="4">
                                                                 &nbsp; &nbsp; </td>
                                                                
                                                          </tr>--%>
                                                          <tr id="Tr1" runat="server" visible="false" >
                                                              <td align="right" style="padding-right:15px; height: 19px;">
                                                                  Modified By:</td>
                                                              <td align="left" style="height: 20px">
                                                                  <asp:TextBox ID="txtmodifiedby_Training" runat="server" CssClass="input_box" 
                                                                      MaxLength="24" ReadOnly="True" Width="240px"></asp:TextBox>
                                                              </td>
                                                              <td align="right" style="padding-right:15px; height: 19px;">
                                                                  Modified On:</td>
                                                              <td align="left" style="height: 20px">
                                                                  <asp:TextBox ID="txtmodifiedon_Training" runat="server" CssClass="input_box" 
                                                                      MaxLength="24" ReadOnly="True" Width="240px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                          </tr>                                                          
                                                          <tr >
                                                              <td colspan="4">
                                                                <asp:Button ID="btn_Training_save" runat="server" CssClass="btn" OnClick="btn_Training_save_Click"
                                                                        Text="Save" Width="59px" Visible="False" TabIndex="4" />
                                                                <asp:Button ID="btn_Training_Cancel" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Training_Cancel_Click" Text="Close"
                                                                        Width="59px" Visible="False" TabIndex="5" />
                                                              </td>
                                                          </tr>                                                          
                                                      </table>
                                     </fieldset>
                                     
                                     <fieldset id="fldSimilarTraining" runat="server" visible="false" style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; height:180px;">
                                     <legend><strong> Add Similar Trainings</strong></legend>
                                     <asp:UpdatePanel runat="server" id="UpdatePanel1">
                                     <ContentTemplate>
                                            <table cellpadding="2" cellspacing="0"  width="800px" border="0"  >
                                                <col width="350px" />
                                                <col width="150px" />
                                                <col width="420px" />
                                                <col />
                                                <tr style="font-weight:bold;">
                                                    <td>Training Group</td>
                                                    <td>CBT#</td>
                                                    <td>Similar Training</td>
                                                    <td></td>
                                                </tr>    
                                                <tr valign="top">
                                                    <td>
                                                        <asp:DropDownList ID="ddlTrainingTypeAddTrng" runat="server" Width="250px" CssClass="required_box" AutoPostBack="true" OnSelectedIndexChanged="ddlTrainingType_SelectedIndexChanged" ></asp:DropDownList>
                                                        <%--<asp:DropDownList ID="ddlSireChap1" runat="server" Width="250px" CssClass="required_box" AutoPostBack="true" OnSelectedIndexChanged="ddlSireChap1_SelectedIndexChanged" ></asp:DropDownList>--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CBTNumber" runat="server" CssClass="input_box" Width="100px" MaxLength="10" AutoPostBack="true" OnTextChanged="CBTNumber_OnTextChanged" ></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTrainingAddTrng" runat="server" Width="250px" CssClass="required_box" AutoPostBack="true" OnSelectedIndexChanged="ddlTrainingAddTrng_OnSelectedIndexChanged" ></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSaveSimilarTraining" runat="server" Text="  Add Training  " OnClick="btnSaveSimilarTraining_OnClick" CssClass="btn" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table cellpadding="2" cellspacing="0" rules="none" width="790px"  >
                                                            <col width="30px" />
                                                            <col width="130px" />
                                                            <col width="80" />                                                            
                                                            <col />
                                                        <tr class="headerstylegrid" style="font-weight:bold; height:15px;text-align:left; padding-left:10px;">
                                                            <td></td>
                                                            <td style="text-align:left;">Training Type</td>
                                                            <td>CBT#</td>
                                                            <td>Similar Training Added</td>
                                                        </tr >
                                                        </table>
                                                        <div style="overflow-x:hidden;overflow-y:scroll; width: 790px; height: 100px">
                                                            <table cellpadding="2" cellspacing="0" rules="rows" width="100%">
                                                            <col width="30px" />
                                                            <col width="130px" />
                                                            <col width="80" />                                                            
                                                            <col />
                                                                <asp:Repeater ID="rptSimilarTraining" runat="server" >
                                                                    <ItemTemplate>
                                                                        <tr style="text-align:left; padding-left:10px;">
                                                                          <td>
                                                                                <asp:ImageButton runat="server" id="imgdel" ImageUrl="~/Modules/HRD/Images/delete1.gif" style=" height:12px;float:left" ToolTip="Delete this Training." OnClick="DeleteSimiler" CommandArgument='<%#Eval("TrainingID")%>' OnClientClick="javascript:confirm('Are you sure to delete?')"/> 
                                                                          </td>
                                                                          <td><%#Eval("TrainingType")%></td>
                                                                          <td><%#Eval("CBTNo") %></td>
                                                                           <td>
                                                                                <%#Eval("TrainingName")%> 
                                                                           </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                     </fieldset>
                                </asp:Panel> 
                                                          
                            </div>
                            
                        </center>
                     </div>
</form>
</body>
</html>