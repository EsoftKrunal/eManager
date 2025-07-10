<%@ Page Title="Vessel Report Group" Language="C#" AutoEventWireup="true" CodeFile="VesselReportGroup.aspx.cs" Inherits="Registers_VesselReportGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
      <%-- <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
<script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    function ShowAllVessels() {
        $('#ol_ActiveVessels').css('display', 'none');
        jQuery("#ul_Allvessels").detach().appendTo('#dv_List');
    }
</script>
</head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <br />
<table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
            <asp:Panel ID="pnl_InspectionGroup" runat="server" Width="100%">
                    
                    <table cellpadding="3" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width:100px;padding:5px; text-align:right;">
                                Group Name :&nbsp;
                            </td>
                            <td style="width:500px;padding:5px; text-align:left;">
                                <asp:TextBox ID="txtGroupName" runat="server" style="background-color:Yellow;" CssClass="input_box" MaxLength="100" Width="430px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtGroupName"></asp:RequiredFieldValidator>
                            </td>
                            <td style="padding:5px; text-align:right;">                                
                                <asp:Button ID="btnAddGroup" runat="server"  Text="Add Group" CssClass="btn"  onclick="btnAddGroup_Click"/>&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server"  Text="Cancel" CssClass="btn"  onclick="btnCancel_Click" Visible="false" CausesValidation="false" />
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
                                                &nbsp;<asp:Label ID="lblPscCode" runat="server" Text='<%#Eval("GroupName") %>'></asp:Label>
                                            </ItemTemplate>  
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" >
                                            <ItemTemplate>
                                                <center>
                                                <asp:ImageButton ID="BtnEdit" runat="server" OnClick="BtnEdit_OnClick" CommandArgument='<%# Eval("GroupId") %>' ImageUrl="~/Images/edit.jpg" CausesValidation="false" />
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                </Columns>
                                <pagerstyle horizontalalign="Center" />
                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
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
</form>
</body>
</html>

