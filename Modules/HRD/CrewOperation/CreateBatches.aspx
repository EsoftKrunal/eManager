<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="CreateBatches.aspx.cs" Inherits="CrewOperation_CreateBatches" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
   <table cellpadding="0" cellspacing="0" width="800px">
       <tr>
           <td class="textregisters" colspan="2" style="padding-right: 0px; padding-left: 0px;
               padding-bottom: 0px; padding-top: 0px">
               <strong>Create Batches</strong>
           </td>
       </tr>
     <tr><td>
      <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Plan Training</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow: auto; width: 780px; height: 150px">
                       <asp:GridView ID="GridView_PlanTraining" runat="server" OnRowCommand="GridView_PlanTraining_RowCommand"  AutoGenerateColumns="False" Width="100%" DataKeyNames="TrainingPlanningId"  GridLines="Horizontal" >
                         <Columns>
                               <asp:TemplateField HeaderText="Training Name"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                  <asp:LinkButton ID="btntrainingname" runat="server" Text='<%#Eval("TrainingName") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                                   <asp:HiddenField ID="HiddenTrainingId" runat="server" Value='<%#Eval("TrainingId")%>' />
                                   <asp:HiddenField ID="HiddenTrainingPlanningId" runat="server" Value='<%#Eval("TrainingPlanningId")%>' />
                                   <asp:HiddenField ID="Hiddenbatches" runat="server" Value='<%#Eval("TotalBatches")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                                <asp:BoundField DataField="InstituteName" HeaderText="Institute Name">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                                 <asp:BoundField DataField="TypeOfTraining" HeaderText="Type of Training">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>
                                <asp:BoundField DataField="FromDate" HeaderText="From Date">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                                <asp:BoundField DataField="ToDate" HeaderText="To Date">
                                 <ItemStyle HorizontalAlign="Left" Width="110px"/>
                               </asp:BoundField>
                                <asp:BoundField DataField="TrainingStatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>
                         </Columns>
                              <RowStyle CssClass="rowstyle" />
                              <SelectedRowStyle CssClass="selectedtowstyle" />
                              <PagerStyle CssClass="pagerstyle" />
                              <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        </td>
                 </tr>
                </table>        
          <asp:Label ID="lbl_CreateBatch_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
     </td></tr>
   <tr><td style="padding-top: 5px">
   <asp:Panel ID="panel_batch" runat="server" Visible="false" Width="100%">
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
        padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
        <legend><strong>Create Batches</strong></legend>
        <asp:Label ID="Label1" runat="server"></asp:Label><br />
        <div style="overflow-y: scroll; overflow-x: hidden; width: 780px; height: 150px">
            <asp:GridView ID="gvbatch" runat="server" AutoGenerateColumns="False" OnPreRender="gvbatch_PreRender" 
                GridLines="Horizontal" Style="text-align: center"
                Width="750px">
                <Columns>
                    <asp:BoundField DataField="CrewName" HeaderText="Name">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Rank" HeaderText="Rank">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Batch">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                     <ItemTemplate>
                      <asp:DropDownList ID="dd_batch" CssClass="input_box" runat="server" Width="150px">
                      <asp:ListItem Text="<Select>" Value="0"></asp:ListItem>
                      </asp:DropDownList>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm">
                     <ItemStyle HorizontalAlign="Center" Width="60px" />
                     <ItemTemplate>
                      <asp:CheckBox ID="chk_confirm" runat="server" />
                      <asp:HiddenField ID="HiddenTrainingRequirementId" runat="server" Value='<%#Eval("TrainingRequirementId")%>' />
                      <asp:HiddenField ID="Hiddencrewid" runat="server" Value='<%#Eval("CrewId")%>' />
                     </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
    </fieldset>
    </asp:Panel>
    </td></tr>
    <tr><td align="right" style="padding-bottom: 10px; padding-top: 10px">
        &nbsp;
        <asp:Button ID="btn_save" runat="server" CssClass="btn" OnClick="btn_save_Click"
            TabIndex="5" Text="Save" Width="59px" /></td></tr>
    </table>
</asp:Content>
<%--
 SelectedValue='<%#Eval("BatchNumber")%>'--%>