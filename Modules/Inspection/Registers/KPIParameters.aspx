<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Registers/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="KPIParameters.aspx.cs" Inherits="KPIParameters" Title="MTM KPI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
            <asp:Panel ID="pnl_InspectionGroup" runat="server" Width="100%">
              
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          <asp:HiddenField ID="HiddenInspectionGroup" runat="server" />
                          <asp:HiddenField id="HiddenFieldGridRowCount" runat="server"></asp:HiddenField>
                      </td>
                    </tr>
                     <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                          </td>
                          <td style="text-align: left; height: 3px;"></td>
                      </tr>
                      
                         <tr>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right; width: 287px;">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      </table>
              
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right:17px;">
                            <div style="float:left;">
                                Year : &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="input_box" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                <asp:LinkButton ID="lnkAddParameterValues" runat="server" Text="Modify KPI" OnClick="lnkAddParameterValues_OnClick" CausesValidation="false"></asp:LinkButton>
                            </div>
                                <asp:Button ID="btnNew" runat="server" CssClass="btn" Text="Add New Head" Width="120px" CausesValidation="False" TabIndex="6" OnClick="btnAddNewHead_Click" />
                        </td>
                    </tr>
                </table>
                
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 255px">
                              <asp:GridView ID="GridView_InsGrp" runat="server" GridLines="Both" AutoGenerateColumns="False" Width="98%" AllowPaging="True" OnPageIndexChanging="GridView_InsGrp_PageIndexChanging" PageSize="8"><RowStyle CssClass="rowstyle" />
                                <Columns>
                                        <asp:TemplateField HeaderText="Sr#">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfKPIID" runat="server" Value='<%# Eval("Id") %>' />
                                            <%#Eval("Row")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="KPI Head" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblKPI" runat="server" Text='<%#Eval("KPI") %>'></asp:Label>
                                            </ItemTemplate>  
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="KPI" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblKPIVALUE" runat="server" Text='<%#Eval("KPIVALUE") %>'></asp:Label>
                                            </ItemTemplate>  
                                            <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnEdit" runat="server" OnClick="BtnEdit_OnClick" ImageUrl="~/Images/edit.jpg" CausesValidation="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CausesValidation="False" ImageUrl="~/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" />
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" ></ItemStyle>
                                        </asp:TemplateField>
                                </Columns>
                                <pagerstyle horizontalalign="Center" />
                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                <HeaderStyle CssClass="headerstylefixedheader" />
                            </asp:GridView>
                         </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessege" runat="server" style="color:Red;"></asp:Label>
                        </td>
                    </tr>
                </table>
                &nbsp;
           </asp:Panel></fieldset>                              
          </td>
         </tr>
        </table>
           &nbsp;
       </td>
      </tr>
     </table>
     <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="DivAddChamgeParameterValues" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:350px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                
                <table cellpadding="2" cellspacing="0" width="100%">
                    <tr>
                        <td class="headerstyle" style="text-align:center;" > 
                            <asp:Label ID="Label1" runat="server" style="font-weight:bold; font-size:14px;" Text="KPI Parameters Value" ></asp:Label>
                       </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblYear" runat="server" style="font-weight:bold; font-size:12px;" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="border:solid 1px #c2c2c2;overflow-x:hidden; overflow-y:hidden;">
                <table cellpadding="2" cellspacing="0" width="100%">
                    <col  width="50px"/><col /><col  width="210px"/>
                    <tr class="headerstyle" style="font-weight:bold;">
                        <td>Sr#</td>
                        <td>KPI Head</td>
                        <td>KPI</td>
                    </tr>
                </table>
                </div>

                <div style="border:solid 1px #c2c2c2;overflow-x:hidden; overflow-y:scroll; height:230px;">
                <table cellpadding="2" cellspacing="0" width="100%" rules="all" style="border-bottom:solid 1px #c2c2c2;" bordercolor="#c2c2c2">
                    <col  width="50px"/><col /><col  width="210px"/>
                    <asp:Repeater ID="rptKPIValues" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfKPIID" runat="server" Value='<%# Eval("Id") %>' />
                                     <%#Eval("ROW")%>  
                                </td>
                                <td> <%#Eval("KPI")%>  </td>
                                <td> 
                                    <asp:TextBox ID="txtKPIValues" runat="server" CssClass="input_box" Text='<%#Eval("KPIVALUE")%>' Width="200" MaxLength="100" style="text-align:right;"></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>    
                <table cellpadding="2" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="3">
                            <div style="float:right;">
                                <asp:Label ID="lblMsg" runat="server" style="color:Red;" ></asp:Label>
                                <asp:Button ID="btnSaveKPIParameterValue" runat="server" Text=" Save " OnClick="btnSaveKPIParameterValue_OnClick" CausesValidation="false" CssClass="btn" />
                                <asp:Button ID="btnCloseKPIParameterValue" runat="server" Text=" Close " OnClick="btnCloseKPIParameterValue_OnClick"  CausesValidation="false" CssClass="btn"/>
                            </div>
                        </td>
                    </tr>
                </table>

                
            </div>
        </center>
    </div>


    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="DivAddNewHead" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:110px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <col width="120px" />
                    <col />
                    <tr>
                        <td class="headerstyle" style="text-align:center;" colspan="2" > <b>Add KPI Head </b> </td>
                    </tr>
                    <tr>
                        <td colspan="2"> &nbsp; </td>
                    </tr>
                    <tr>
                          <td >KPI Head :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtMTMKPI" runat="server" CssClass="input_box" MaxLength="100" Width="400px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtMTMKPI"></asp:RequiredFieldValidator>
                           </td>
                      </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;"> 
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="7" OnClick="btn_Save_InspGrp_Click" />
                            <asp:Button ID="btnCloseAddHeadPopup" runat="server" CssClass="btn" Text="Close" Width="59px" TabIndex="7" OnClick="btnCloseAddHeadPopup_Click"  CausesValidation="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"> 
                            
                        </td>
                    </tr>
                </table>

            </div>
        </center>
    </div>
</asp:Content>

