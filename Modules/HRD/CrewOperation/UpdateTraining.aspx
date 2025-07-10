<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTraining.aspx.cs" Inherits="CrewOperation_UpdateTraining" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type ="text/javascript" >
    
function CheckAll(self)
{
    for(i=0;i<=document.getElementsByTagName("input").length-1;i++)  
    {
        if(document.getElementsByTagName("input").item(i).getAttribute("type")=="checkbox" && document.getElementsByTagName("input").item(i).getAttribute("id")!=self.id)
        {
            if(document.getElementsByTagName("input").item(i).getAttribute("id")!="ctl00_contentPlaceHolder1_chk_Attended")
                document.getElementsByTagName("input").item(i).checked=self.checked;
        } 
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
      <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
            <td style="text-align: center;">
                    <table cellpadding="0" cellspacing="0" width="100%"  >
                     <tr>
                      <td >
                          <table cellpadding="2" cellspacing="0" width="100%" style="padding:5px; background-color:#c2c2c2; font-weight:bold;">
                              <tr>
                                  <td style="height: 11px; width: 50px; text-align: right;">
                                      Emp # :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:TextBox ID="txt_MemberId" runat="server" CssClass="input_box" MaxLength="6"
                                          TabIndex="1" Width="60px"></asp:TextBox></td>
                                  
                                  <td style="height: 11px; width: 77px; text-align: right;">
                                      Training :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:TextBox ID="txtTrainingName" runat="server" CssClass="input_box" TabIndex="1" Width="240px"></asp:TextBox>
                                  </td>    
                                  <td style="height: 11px">
                                      Crew Status :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_CrewStatus_Search" runat="server" CssClass="input_box"
                                          TabIndex="11" Width="80px">
                                          <asp:ListItem Text="&lt; All &gt;"></asp:ListItem>
                                          <asp:ListItem Text="New" Value="1" ></asp:ListItem>
                                          <asp:ListItem Text="On Leave" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="On Board" Value="3"  ></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td style="height: 11px">
                                      Vessel :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" TabIndex="10"
                                          Width="187px">
                                          <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                      </asp:DropDownList></td>
                                      <td></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                                        Text="Search" Width="60px" TabIndex="9" OnClick="btn_Search_Click" CausesValidation="False" />
                                </td>
                                  <td></td>
                              </tr>
                              <tr>
                                <td style="height: 11px">
                                      Rank :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="input_box" TabIndex="4"
                                          Width="80px">
                                          <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  
                                  <td style="text-align: right;">Source :</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlSource" runat="server" CssClass="input_box" Width="120px" >
                                        <asp:ListItem Text="< All >" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="MATRIX" Value="2" ></asp:ListItem>
                                        <asp:ListItem Text="PEAP" Value="0" ></asp:ListItem>
                                        <asp:ListItem Text="ASSIGNED" Value="1" ></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                              </tr>
                              
                          </table>
                      </td>
                 </tr>
                </table>        
                  <%--<asp:Label ID="lbl_Message" runat="server" ForeColor="#C00000" ></asp:Label>--%>
            </td>
         </tr>
          <tr>
            <td style="text-align: center;">
                   
                  <center >
                  <div style="width:100%; border:solid 1px #c2c2c2;">
                      <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows" style="margin-top:3px;">
                    <col width="30px" />
                    <col width="55px" />
                    <col width="150px" />
                    <col />
                    <col width="80px" />
                    <col width="90px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="90px" />
                    <col width="90px" />
                    <col width="150px" />
                    <col width="17px" />
                    <tr class="headerstylegrid" style="font-weight:bold;" >
                        <td>
                            <input type="checkbox" id="chk_All" onclick="javascript:CheckAll(this);" title="Select All."/>
                        </td>
                        <td>Crew #</td>
                        <td style="text-align:left;padding-left:10px;">Crew Name</td>
                        <td style="text-align:left;">Training Name</td>
                        <td style="text-align:left;">Source</td>
                        <td>Due Date</td>
                        <td>Plan Date</td>
                        <td>Attended</td>
                        <td>From Date</td>
                        <td>To Date</td>
                        <td style="text-align:left;padding-left:5px;">Institute</td>
                        <td></td>
                    </tr>
                  </table>
                  </div>
                  <div style="height:268px; overflow-y :scroll;overflow-x:hidden;width:100%; border:solid 1px gray;">
                    <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows">
                    <col width="30px" />
                    <col width="55px" />
                    <col width="150px" />
                    <col />
                    <col width="80px" />
                    <col width="90px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="90px" />
                    <col width="90px" />
                    <col width="150px" />
                    
                        <asp:Repeater ID="GridView_PlanTraining" runat="server" >
                            <ItemTemplate>
                                <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkSelect" />   
                                        <asp:HiddenField ID="hfdCrewId" runat="server" Value='<%#Eval("TRAININGREQUIREMENTID")%>' />
                                    </td>
                                    <td> <%#Eval("CrewNumber")%></td>
                                    <td style="text-align:left;padding-left:10px;"> <%#Eval("FullName")%></td>
                                    <td style="text-align:left"> <%#Eval("TrainingName")%></td>
                                    
                                    <td> <%#Eval("Source")%></td>
                                    <td> <%#Eval("DueDate")%></td>
                                    <td> <%#Eval("PlanDate")%></td>
                                    <td> <%#Eval("Attended")%></td>
                                    <td> <%#Eval("FromDate")%></td>
                                    <td> <%#Eval("ToDate")%></td>
                                    <td style="text-align:left;padding-left:5px;">  <%#Eval("Institute")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    
                    <%--<asp:GridView ID="GridView_PlanTraining1" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="TRAININGREQUIREMENTID" GridLines="Horizontal" >
                        <Columns>
                           <asp:TemplateField HeaderText="">
                           <ItemStyle HorizontalAlign="Left" Width="20px" />
                              <ItemTemplate>  
                               <asp:CheckBox runat="server" ID="chkSelect" />   
                               <asp:HiddenField ID="hfdCrewId" runat="server" Value='<%#Eval("TRAININGREQUIREMENTID")%>' />
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="CrewNumber" HeaderText="Crew #">
                             <ItemStyle HorizontalAlign="Left" Width="50px"  />
                           </asp:BoundField>
                             <asp:BoundField DataField="FullName" HeaderText="Crew Name">
                             <ItemStyle HorizontalAlign="Left" />
                           </asp:BoundField>
                            <asp:BoundField DataField="TrainingName" HeaderText="Training Name">
                             <ItemStyle HorizontalAlign="Left"/>
                           </asp:BoundField>
                            <asp:BoundField DataField="DueDate" HeaderText="Due Date">
                             <ItemStyle HorizontalAlign="Center" Width="90px"/>
                           </asp:BoundField>
                           <asp:BoundField DataField="Attended" HeaderText="Attended">
                             <ItemStyle HorizontalAlign="Center" Width="60px"/>
                           </asp:BoundField>
                           <asp:BoundField DataField="FromDate" HeaderText="From Date">
                             <ItemStyle HorizontalAlign="Center" Width="90px"/>
                           </asp:BoundField>
                           <asp:BoundField DataField="ToDate" HeaderText="To Date">
                             <ItemStyle HorizontalAlign="Center" Width="90px"/>
                           </asp:BoundField>
                           <asp:BoundField DataField="Institute" HeaderText="Institute">
                             <ItemStyle HorizontalAlign="Center" />
                           </asp:BoundField>
                        </Columns>
                        
                     </asp:GridView>--%>
                     
                     
                 </div>
                 </center>              
                
           <table style="width: 100%; margin-top:5px;">
               <tr>
                   <td style="text-align: right">
                       <table cellpadding="0" cellspacing="0" width="100%">
                           <tr>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   From Date :</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_FromDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                                   <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                               <td>
                                   To Date :</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_ToDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                                   <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                               <td>
                                   Institute :</td>
                               <td style="text-align: left">
                                   <asp:DropDownList ID="ddl_TrainingReq_Training" runat="server" CssClass="input_box"
                                       TabIndex="2" Width="150px">
                                   </asp:DropDownList></td>
                               <td style="text-align: left">
                                   &nbsp;</td>
                               <td>
                <asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" 
                    Text="Update Training" Width="110px" TabIndex="9" OnClick="btn_Save_PlanTraining_Click" /></td>
                           </tr>
                           <tr>
                               <td>
                               </td>
                               <td style="text-align: left">
                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_FromDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_FromDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                               </td>
                               <td>
                               </td>
                               <td style="text-align: left">
                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_ToDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="txt_ToDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                           </tr>
                           <tr>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                               <td>
                               </td>
                           </tr>
                       </table>
                       <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton5" PopupPosition="TopLeft" TargetControlID="txt_FromDate">
                       </ajaxToolkit:CalendarExtender>
                       <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_ToDate">
                       </ajaxToolkit:CalendarExtender>
                     </td>
               </tr>
           </table>
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
    </div>
    </form>
</body>
</html>