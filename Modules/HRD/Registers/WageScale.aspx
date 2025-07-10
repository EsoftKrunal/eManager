<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="WageScale.aspx.cs" Inherits="Registers_WageScale" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
  <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Label ID="Labelupper1" runat="server" CssClass="textregisters" Text="Wage Scale">
                </asp:Label></td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Label ID="lb_msg" runat="server" ForeColor="Red" Width="444px" Height="16px"></asp:Label></td>
        </tr>
      <tr>
          <td colspan="3" style="text-align: center;" width="100%">
          <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Wage Scale</strong></legend>
                <div id="div-datagrid" style=" width:100%; height :150px; overflow-y:Scroll; overflow-x:hidden; text-align:center">
                    <asp:GridView ID="Gd_search" runat="server" AutoGenerateColumns="False"  OnRowCommand="Gd_search_RowCommand"  GridLines="Horizontal" Style="text-align: center" Width="98%" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Wage Scale">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                               <asp:LinkButton ID="lnk_wage" CommandName="Select" CausesValidation="false" runat="server" Text='<%# Eval("WageScaleName") %>'></asp:LinkButton>
                                               <asp:HiddenField ID="hiddenwagescaleId" runat="server" Value='<%# Eval("WageScaleId") %>' />
                                               <asp:HiddenField ID="hiddenwagescaleName" runat="server" Value='<%# Eval("WageScaleName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wage Scale Components Name" ItemStyle-HorizontalAlign="Left" >
                                           <ItemStyle />
                                            <ItemTemplate>
                                            <%--<table><tr><td style=" text-align:left">--%>
                                           <%-- <%for (int i = 1; i <= count_components; i++)
                                              {%>--%>
                                               <asp:Label ID="lbcomponent" runat="server" ></asp:Label>
                                               <%--<asp:HiddenField ID="hiddenwagescaleComponentId" runat="server" Value='<%# Eval("WageScaleComponentId") %>' />--%>
                                            <%-- <%} %>--%>
                                               <%--</td></tr></table>--%>
                                            </ItemTemplate>                                  
                                        </asp:TemplateField>    
                                    </Columns>
                                    <RowStyle CssClass="rowstyle" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                </asp:GridView></div>
                       
        </fieldset>
          </td>
      </tr>
      <tr>
          <td style="width: 100px; height: 21px; text-align: left">
          </td>
          <td align="right" style="padding-right: 15px; width: 88px">
              &nbsp;
          </td>
          <td style="width: 100px; height: 21px; text-align: left">
          </td>
      </tr>
      <tr>
          <td style="padding-right: 10px; height: 39px" valign="top">
                Wage Scale:</td>
          <td align="left" style="padding-right: 15px; width: 88px; height: 39px;" valign="top">
              <asp:TextBox id="txt_wgscale" runat="server" CssClass="required_box"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_wgscale"
                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
          <td style="width: 100px; height: 39px; text-align: left">
            <div id="div-datagrid2">
              <asp:CheckBoxList ID="chkcomponent" runat="server"
                  Width="570px" RepeatColumns="2">
              </asp:CheckBoxList></div>
              <br />
              <br />
              <br />
              
            </td>
      </tr>
        <tr>
            <td style="height: 10px; padding-right: 10px;" align="right">
            </td>
            <td style="padding-right: 15px; width: 88px; height: 10px;" align="left">
                </td>
            <td style="width: 100px; text-align: left; height: 21px;" rowspan="3">
                </td>
        </tr>
        <tr>
            <td style="vertical-align:top" align="center" colspan="3">
            
            </td>
        </tr>
      <tr>
          <td colspan="3" style="padding-bottom: 15px; padding-top: 15px; text-align: right">
          </td>
      </tr>
        <tr>
            <td colspan="3" style="padding-bottom: 15px; padding-top: 15px; text-align: right">
                <asp:Button ID="btn_Add" runat="server" CssClass="btn" OnClick="Button1_Click"
                    Text="Add" Width="56px" CausesValidation="False" />
                <asp:Button ID="btn_save" runat="server" CssClass="btn" OnClick="btn_save_Click"
                    Text="Save" Width="56px" />
                <asp:Button ID="btn_clear" runat="server" CssClass="btn" OnClick="btn_clear_Click"
                    Text="Clear" Width="56px" CausesValidation="False" /></td>
        </tr>
    </table>
</asp:Content>

