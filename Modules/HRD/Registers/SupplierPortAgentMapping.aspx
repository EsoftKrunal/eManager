<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierPortAgentMapping.aspx.cs" Inherits="Registers_SupplierPortAgentMapping" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">--%>
    <div style="text-align: center">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Supplier - Port Agent Mapping"></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
            
                &nbsp;<br />
           <table style="width: 100%">
               <tr>
                   <td style="text-align: center; height: 20px;">
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="5">
                          &nbsp;
                       </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 24px; text-align: right">
                          </td>
                          <td style="height: 24px; text-align: right">
                              Select Supplier:</td>
                          <td align="right" style="height: 24px; text-align: left; width: 3px;">
                          </td>
                          <td align="right" style="padding-right: 15px; height: 24px; text-align: left">
                              <asp:DropDownList ID="ddlSuppliers" AutoPostBack="true" runat="server" CssClass="input_box" Width="205px" TabIndex="4" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                              </asp:DropDownList></td>
                          <td style="height: 24px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 26px;">
                          </td>
                          <td style="text-align: center; height: 26px;">
                              &nbsp; Ungrouped Port Agents List</td>
                          <td align="right" style="padding-right: 15px; height: 26px; text-align: center; width: 3px;">
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: center; height: 26px;">
                              Grouped List</td>
                          <td style="text-align: left; height: 26px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              </td>
                          <td style="text-align: left">
                              </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 3px;">
                          </td>
                          <td align="right" style="text-align: right; padding-right:15px">
                              </td>
                          <td style="text-align: left">
                              </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: center; height: 13px;">
                              <asp:ListBox ID="lblRest" runat="server" CssClass="input_box" Height="230px" Width="453px" SelectionMode="Multiple">
                              </asp:ListBox></td>
                          <td align="right" style="height: 13px; text-align: center; width: 3px;">
                              <asp:ImageButton  ID="btnRight" runat="server" TabIndex="7" OnClick="btnRight_Click" ImageUrl="~/Modules/HRD/Images/Next.png" /><br />
                              <br />
                              <asp:ImageButton ID="btnLeft" runat="server" TabIndex="7" OnClick="btnLeft_Click" ImageUrl="~/Modules/HRD/Images/prev.png"/></td>
                          <td align="right" style="padding-right: 15px; text-align: center; height: 13px;">
                              <asp:ListBox ID="lstUsed" runat="server" CssClass="input_box" Height="230px"
                                  Width="453px" SelectionMode="Multiple"></asp:ListBox></td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              </td>
                      </tr>
                                                        </table>
                       &nbsp; &nbsp;
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
    </asp:Content>
    <%--</form>
</body>
</html>--%>
