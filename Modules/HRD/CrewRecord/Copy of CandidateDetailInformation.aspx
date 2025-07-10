<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Copy of CandidateDetailInformation.aspx.cs" Inherits="CandidateDetailInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
       <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
   <script language="javascript" type="text/javascript">
   function shownew()
   {
   //window.open('CrewPersonalDetails.aspx',null,'title=no,resizable=yes,menubar=yes,toolbars=yes,scrollbars=yes,width=400,height=400,left=20,top=10,addressbar=yes,location=yes');
   window.open('CrewPersonalDetails.aspx',null,'title=no,resizable=yes,menubar=yes,toolbars=yes,scrollbars=yes,addressbar=yes,location=yes');
   }
   </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        
      <%--  <asp:UpdatePanel ID="up1" runat="server"><ContentTemplate>--%>
        <div style="text-align: center" >
            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9">
                         <tr>
                <td align="center" style="background-color:#4371a5; height: 23px" class="text" >
                    Candidate Details</td>
            </tr>
                <tr>
                    <td >
                    <table cellpadding="0" cellspacing="0">
                    <tr><td>  
                       
                        <asp:Label ID="lbl_info" runat="server" Width="303px" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td style="padding-right: 10px; padding-left: 10px;text-align: center;" align="center">
                  
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                    <legend><strong>Candidate Details </strong></legend>
                        <br />
                   
                      <div style="overflow:auto; width:785px; height:135px;" >
                      <asp:Label ID="lbl_License_Message" runat="server"></asp:Label>
                        <asp:GridView ID="gvcandidate" runat="server"  AutoGenerateColumns="False" DataKeyNames="CandidateId" GridLines="Horizontal"
                         Width="220%" OnPreRender="gvcandidate_PreRender" OnSelectedIndexChanged="gvcandidate_SelectedIndexChanged">
                              <Columns>
                              <asp:TemplateField HeaderText="Select">
                              <ItemTemplate>
                              <asp:CheckBox ID="chk1" runat="server" />
                              </ItemTemplate>
                                   <ItemStyle Width="50px" />
                              </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" HeaderText="View " meta:resourcekey="CommandFieldResource1"
                                                        SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" >
                                    <ItemStyle Width="50px" />
                                </asp:CommandField>
                                  <asp:BoundField DataField="FirstName" HeaderText="First Name">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                   <asp:BoundField DataField="MiddleName" HeaderText="Middle Name">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                    <asp:BoundField DataField="LastName" HeaderText="Family Name">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                    <asp:BoundField DataField="rankname" HeaderText="Rank Applied">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Gender" HeaderText="Gender">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                    <asp:BoundField DataField="AvailableFrom" HeaderText="Available From">
                                      <ItemStyle HorizontalAlign="Left" Width="170px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="EmailId" HeaderText="Email  ">
                                      <ItemStyle HorizontalAlign="Left" />
                                  </asp:BoundField>
                                    <asp:BoundField DataField="DateOfBirth" HeaderText="DOB">
                                      <ItemStyle HorizontalAlign="Left" Width="80px" />
                                  </asp:BoundField>
                                    <asp:BoundField DataField="nationality" HeaderText="Nationality">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact No">
                                      <ItemStyle HorizontalAlign="Left" Width="250px" />
                                  </asp:BoundField>
                                    <asp:BoundField DataField="PlaceOfBirth" HeaderText="POB">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                    <asp:BoundField DataField="qualification" HeaderText="Qualification">
                                      <ItemStyle HorizontalAlign="Left"/>
                                  </asp:BoundField>
                                  
                                  
                              </Columns>
                            
                                  <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                          </asp:GridView>
                    </div>
                    </fieldset>
                  

         <table width="100%" style="background-color:#f9f9f9;" cellpadding="0" cellspacing="0"  >
             <tr id="tblexp" runat="server">
                 <td align="right" style="height: 50px; text-align: right">
                     <br />
                   <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                         <legend><strong>Other Company Exp.</strong></legend>
                         <table cellpadding="0" cellspacing="0" style="width: 100%">
                             <tr >
                                 <td colspan="1" style="padding-bottom: 5px; padding-top: 5px; text-align: center">
                                     <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Auto" Width="100%">
                                     <asp:Label ID="lblexperience" runat="server"></asp:Label>
                                         <asp:GridView ID="gvexperience" runat="server" AutoGenerateColumns="False" DataKeyNames="CandidateExperienceId" GridLines="Horizontal"
                                             Height="9px" meta:resourcekey="GridView3_1Resource1"
                                             PageSize="3" Style="text-align: center" Width="150%">
                                             <Columns>
                                                
                                                 <asp:TemplateField HeaderText="Company Name" meta:resourcekey="TemplateFieldResource2">
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblCompanyName" runat="server" meta:resourcekey="lblCompanyNameResource2"
                                                             Text='<%# Eval("CompanyName") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:BoundField DataField="RankName" HeaderText="Rank" meta:resourcekey="BoundFieldResource7">
                                                     <ItemStyle HorizontalAlign="Left" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="VesselName" HeaderText="Vessel Name" meta:resourcekey="BoundFieldResource11">
                                                     <ItemStyle HorizontalAlign="Left" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="VesselType" HeaderText="Vessel Type" meta:resourcekey="BoundFieldResource12">
                                                     <ItemStyle HorizontalAlign="Left" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="SignOn" HeaderText="Sign On Dt." meta:resourcekey="BoundFieldResource8">
                                                     <ItemStyle Width="80px" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="SignOff" HeaderText="Sign Off Dt." meta:resourcekey="BoundFieldResource9">
                                                     <ItemStyle Width="90px" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="Duration" HeaderText="Duration(Month)" meta:resourcekey="BoundFieldResource10">
                                                     <ItemStyle HorizontalAlign="Center" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="GWT" HeaderText="GRT">
                                                     <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="BHP" HeaderText="BHP(Kw)">
                                                     <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="SOFReason" HeaderText="Sign Off Reason">
                                                     <ItemStyle HorizontalAlign="Left" />
                                                 </asp:BoundField>
                                             </Columns>
                               <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                         </asp:GridView>
                                     </asp:Panel>
                                 </td>
                             </tr>
                         </table>
                     </fieldset>
                 </td>
             </tr>
             <tr></tr>
             <tr id="Trcargo" runat="server">
                 <td align="right" style="height: 50px; text-align: right">
                     <br />
                   <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                         <legend><strong>Dangerous Cargo Endorsement</strong></legend>
                         <table cellpadding="0" cellspacing="0" style="width: 100%">
                             <tr >
                                 <td colspan="1" style="padding-bottom: 5px; padding-top: 5px; text-align: center">
                                     <asp:Panel ID="Panel2" runat="server" Height="100px" ScrollBars="Auto" Width="100%">
                                     <asp:Label ID="lbl_cargo" runat="server"></asp:Label>
                                      <asp:GridView ID="GvDCE" runat="server" AutoGenerateColumns="False" PageSize="3" Style="text-align: center" Width="150%" GridLines="Horizontal"
                                                                                DataKeyNames="CandidateDCEId" >
                                                                                 <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                                <Columns>
                                                                                    <%--<asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                                        ShowSelectButton="True">
                                                                                        <ItemStyle Width="50px" />
                                                                                    </asp:CommandField>
                                                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                                        ShowEditButton="True">
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:CommandField>
                                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                        <ItemStyle Width="30px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                                Text="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Cargo Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblcargoname" runat="server" Text='<%#Eval("DCE")%>'></asp:Label>
                                                                                                                                                                                       
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Number" HeaderText="Number">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="nationality" HeaderText="Nationality">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="GradeLevel" HeaderText="Grade Level">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Issue">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                     </asp:Panel>
                                 </td>
                             </tr>
                         </table>
                     </fieldset>
                 </td>
             </tr>
             <tr>
                <td align="right" style="text-align: right; height: 50px;">
                    <asp:HiddenField ID="HiddenPK" runat="server"/><asp:HiddenField ID="h_Licenceid" runat="server"/>
                    &nbsp;<asp:Button ID="btn_Transfer" runat="server" Width="70px" Text="Transfer" CssClass="btn" CausesValidation="False" OnClick="btn_Transfer_Click"  />
                    <asp:Button ID="btn_delete"
                        runat="server" Text="Delete" CssClass="btn" Width="70px" OnClick="btn_delete_Click"  />
                        <asp:Button  ID="btn_New" runat="server" Width="70px" Text="Add New" CssClass="btn"  CausesValidation="False" OnClientClick="javascript:shownew();" />
                        </td>
            </tr></table> </td></tr></table>

                    </td>
                </tr>
            </table>
        </div>              
       <%--</ContentTemplate></asp:UpdatePanel>--%>       
        <%--<table style="width: 755px">
            <tr>
                <td style="width: 100px">--%>
        </div>
    </form>
</body>
</html>
