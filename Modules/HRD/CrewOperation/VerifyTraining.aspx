<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifyTraining.aspx.cs" Inherits="CrewOperation_VerifyTraining" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="font-family:Arial;font-size:12px;"> 
      <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
            <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;" class="textregisters" colspan="2" >
            <strong>Verify Training</strong>
            </td>
         </tr>
         <tr>
            <td style="text-align: center;padding-left:10px; padding-right:10px;">
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="" >
                 <legend><strong>Select Filter</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%" style="margin-bottom:4px;">
                     <tr>
                      <td style=" padding-top:5px;">
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td style="height: 11px; width: 77px; text-align: right;">
                                      Emp # :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:TextBox ID="txt_MemberId" runat="server" CssClass="input_box" MaxLength="6"
                                          TabIndex="1" Width="70px"></asp:TextBox></td>
                                  <td style="height: 11px">
                                      Crew Status :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_CrewStatus_Search" runat="server" CssClass="input_box"
                                          TabIndex="11" Width="113px">
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
                                  <td style="height: 11px">
                                      Rank :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="input_box" TabIndex="4"
                                          Width="123px">
                                          <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td style="height: 11px">
                                      &nbsp;
                                      <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                    Text="Search" Width="60px" TabIndex="9" OnClick="btn_Search_Click" CausesValidation="False" />&nbsp;&nbsp; </td>
                              </tr>
                          </table>
                 </tr>
                </table>        
                  <asp:Label ID="lbl_Message" runat="server" ForeColor="#C00000" ></asp:Label></fieldset>
                  <%--Same Training already exist For Same Period.--%>
            </td>
         </tr>
          <tr>
            <td style="text-align: center;padding:10px;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Update Training</strong></legend>
                   <div style="text-align :left ; width :100%; padding-left :3px;" >
                  <input type="checkbox" id="chk_All" onclick="javascript:CheckAll(this);"/><label for="chk_All" >&nbsp;Select All</label>
                  </div>
                  <center >
                  <div style="height:175px; overflow-y :scroll">
                        <asp:GridView ID="GridView_PlanTraining" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="TRAININGREQUIREMENTID" GridLines="Horizontal" >
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
                            <asp:BoundField DataField="Verified" HeaderText="Verified">
                             <ItemStyle HorizontalAlign="Center" Width="60px"/>
                           </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="Remark">
                             <ItemStyle HorizontalAlign="Left" />
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
                        <RowStyle CssClass="rowstyle" />
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <PagerStyle CssClass="pagerstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                 </div>
                 </center>
                </fieldset>
           <table style="width: 100%">
               <tr>
                   <td style="text-align: right">
                       <table cellpadding="0" cellspacing="0" width="100%">
                           <tr>
                               <td style="height: 27px; text-align: left"></td>
                               <td style="width: 123px; height: 27px">Verified :</td>
                               <td rowspan="1" style="width: 451px; height: 27px; text-align: left"><asp:CheckBox runat="Server" ID="chk_Attended" temp="1"  /></td>
                               <td style="height: 27px"></td>
                           </tr>
                           <tr>
                               <td style="height: 1px; text-align: left;"></td>
                               <td style="height: 1px; width: 123px;">Verification Remark :</td>
                               <td style="width: 451px; text-align: left;" rowspan="2"><asp:TextBox ID="txtRemark" CssClass="input_box" TextMode="MultiLine" runat="server" Height="65px" Width="100%"></asp:TextBox></td>
                               <td style="height: 1px"></td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; height: 45px;">&nbsp; &nbsp;</td>
                               <td style="vertical-align: top; width: 123px; height: 45px;">&nbsp;</td>
                               <td style="vertical-align: top; height: 45px"><asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text="Verifiy Training" Width="110px" TabIndex="9" OnClick="btn_Save_PlanTraining_Click" /></td>
                           </tr>
                       </table>
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
