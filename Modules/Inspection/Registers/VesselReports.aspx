<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Registers/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="VesselReports.aspx.cs" Inherits="Register_VesselReports" Title="VesselReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    function ShowAllVessels() {
        $('#ol_ActiveVessels').css('display','none');
        jQuery("#ul_Allvessels").detach(). appendTo('#dv_List');
    }
</script>

<table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
            <asp:Panel ID="pnl_InspectionGroup" runat="server" Width="100%">
                    
                    <table cellpadding="3" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="padding:5px;">
                                <asp:Button ID="btnAddReport" runat="server" OnClick="btnAddReport_OnClick" Text="Add New Report" CssClass="btn"  style="float:right;"/>
                            </td>
                        </tr>
                    </table>


                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 365px; overflow-x:hidden;overflow-y:scroll;">
                              <asp:GridView ID="grdReportsCode" runat="server" GridLines="Both" AutoGenerateColumns="False" Width="98%"><RowStyle CssClass="rowstyle" />
                                <Columns>
                                        <asp:TemplateField HeaderText="Group Name" >
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblGroup" runat="server" Text='<%#Eval("GroupName") %>'></asp:Label>
                                            </ItemTemplate>  
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Report Name" >
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblPscCode" runat="server" Text='<%#Eval("ReportName") %>'></asp:Label>
                                            </ItemTemplate>  
                                            
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Frequency" >
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblDescription" runat="server" Text='<%#Eval("PeriodName") %>'></asp:Label>
                                            </ItemTemplate>  
                                            <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" >
                                            <ItemTemplate>
                                                <center>
                                                <asp:ImageButton ID="BtnEdit" runat="server" OnClick="BtnEdit_OnClick" CommandArgument='<%# Eval("ReportId") %>' ImageUrl="~/Images/edit.jpg" CausesValidation="false" />
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                </Columns>
                                <pagerstyle horizontalalign="Center" />
                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                <HeaderStyle CssClass="headerstylefixedheader" />
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                 <asp:Label ID="lblMessege" runat="server" style="color:Red; font-size:12px;"></asp:Label>
                &nbsp;
           </asp:Panel></fieldset>                              
          </td>
         </tr>
        </table>
           &nbsp;
       </td>
      </tr>
     </table>
     
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvAddReport" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:388px; padding :5px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:75px;opacity:1;filter:alpha(opacity=100)">
            <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border:solid 1px #c2c2c2">
                    <col width="120px" />
                    <col />
                    <tr>
                        <td class="headerstyle" style="text-align:center;" colspan="2" > <b>Add Report</b> </td>
                    </tr>
                    <tr>
                          <td >Group :</td>
                          <td style="text-align: left"> 
                             <asp:DropDownList ID="ddlGroup" runat="server" CssClass="input_box" Width="435px"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlGroup" InitialValue="0"></asp:RequiredFieldValidator>
                           </td>
                    </tr>
                    <tr>
                    <td >Report Name :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtReportName" runat="server" CssClass="input_box" MaxLength="100" Width="430px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtReportName"></asp:RequiredFieldValidator>
                           </td>
                    </tr>
                    <tr>
                          <td >Frequency :</td>
                          <td style="text-align: left"> 
                             <asp:DropDownList ID="ddlFreq" runat="server" CssClass="input_box" Width="435px"></asp:DropDownList>
                             <asp:CompareValidator runat="server" ID="c1" ControlToValidate="ddlFreq" Operator="GreaterThan" ValueToCompare="0" ErrorMessage="*"></asp:CompareValidator>
                           </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btnSaveReport_OnClick" />
                        </td>
                    </tr>
                    <tr id="trVessels" runat="server" style="display:none;">
                          <td style="vertical-align:top" > Vessels :</td>
                          <td style="text-align: left"> 
                          <div style="width:480px">
                          <div style="overflow-x:hidden;overflow-y:hidden; height:190px; width:200px;border :solid 1px #4371a5; float:left;">
                          <div style="background-color:#B2E6FF; text-align:center;">
                                Available Vessels
                          </div>
                          <div style="overflow-x:hidden;overflow-y:scroll; height:170px; width:100%;">
                          <ul style='margin:0px; padding-left:10px; list-style-type:none;' id="ul_Allvessels">
                              <asp:Repeater runat="server" ID="rpt_AllVessels">
                              <ItemTemplate>
                                <li><%#Eval("VesselName")%>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/right_arrow.png" runat="server" OnClick="Add_Vessel" CommandArgument='<%#Eval("VesselId")%> ' style="float:right;margin:2px;" /></li>
                                <div style="clear:both"></div>
                                </li>
                              </ItemTemplate>
                              </asp:Repeater>
                          </ul>
                          </div>
                          </div>
                          <div style="overflow-x:hidden;overflow-y:hidden; height:190px; width:250px;border :solid 1px #4371a5; float:right;">
                          <div style="background-color:#B2E6FF; text-align:center;">
                                Reports Applied to following Vessel
                          </div>
                          <div style="overflow-x:hidden;overflow-y:scroll; height:170px; width:100%;">
                              <ul style='margin:0px; padding-left:10px; list-style-type:none;' id="ol_ActiveVessels">
                              <asp:Repeater runat="server" ID="rptVList">
                              <ItemTemplate>
                                <li><%#Eval("VesselName")%> 
                                <asp:ImageButton ImageUrl="~/Images/left_arrow.png" runat="server" OnClick="Delete_Vessel" CommandArgument='<%#Eval("VesselId")%> ' style="float:left;margin:2px;"/></li>
                                <div style="clear:both"></div>
                              </ItemTemplate>
                              </asp:Repeater>
                              </ul>
                          </div>
                          </div>
                          
                          <div style="height:90px;vertical-align:middle; text-align:center; padding-top:70px;">
                            <asp:ImageButton ImageUrl="~/Images/right-all.png" ID="btnApplyAll" runat="server" OnClick="btnApplytoAll_OnClick"/>
                              <br />
                              <br />
                             <asp:ImageButton ImageUrl="~/Images/left-all.png" ID="btnRemoveAll" runat="server" OnClick="btnRemoveAll_OnClick"/>
                          </div>
                          </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"> 
                            <%--<asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="7" OnClick="btnSaveReport_OnClick" />--%>
                            <asp:Button ID="btnCloseAddReport" runat="server" BackColor="Red" ForeColor="White" CssClass="btn" Text="Close" Width="59px"  TabIndex="7" OnClick="btnCloseAddReport_Click"  CausesValidation="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:left;display:none">  
                             
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCloseAddReport" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
        </center>
    </div>
</asp:Content>

