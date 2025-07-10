<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuelTypes.aspx.cs" Inherits="MRV_FuelTypes" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <link href="style.css?14" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../CSS/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../../CSS/StyleSheet.css" />
	 <link rel="stylesheet" type="text/css" href="../../CSS/style.css" />
</head>
<body style="margin: 0 0 0 0;">

<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
           <%-- <div class="pagename">
                MRV - Monitoring , Reporting & Verfication
            </div>--%>
            <div style="border-bottom:solid 5px #4371a5;"></div>
            <h1>
                <asp:ImageButton runat="server" ID="btn_Add" ImageUrl="~/Modules/HRD/Images/add_16.gif" style="float:left;margin-left:0px;" OnClick="btn_Add_Click" />
                <span style="margin-left:10px;">Fuel Types</span>                
            </h1>
            <div style="height:400px;overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight  padded" width="100%">
                     <tr>
                            <th style="width:30px;">Edit</th>
                            <th style="text-align:left">Fuel Type</th>
                            <th style="text-align:left">Short Name</th>
                            <th style="text-align:left">Tonnes Co<sub>2</sub> / Tonne Fuel</th>
                    </tr>                 
                <asp:Repeater runat="server" ID="rptData">
                   
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btn_Edit" ImageUrl="~/Images/editx12.jpg" OnClick="btn_Edit_Click" CommandArgument='<%#Eval("FuelTypeId")%>' /></td>
                            <td style="text-align:left"><%#Eval("FuelTypeName")%></td>
                            <td style="text-align:left"><%#Eval("ShortName")%></td>
                            <td style="text-align:left"><%#Eval("Co2EmissionPerMT")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
            </div>
        </div>
    <div class="modal_frame" runat="server" id="dvAddEdit" visible="false">
        <div class="modal_header">Add / Edit Fuel Type</div>
        <div class="modal_content">
            <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%">
                       <tr>
                           <td style="text-align:left;width:160px;">Fuel Type Name</td>
                           <td><asp:TextBox runat="server" ID="txtFuelTypeName" CssClass="input input_text large" ></asp:TextBox>
                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="txtFuelTypeName"></asp:RequiredFieldValidator>
                           </td>
                       </tr>
                        <tr>
                           <td style="text-align:left;width:160px;">Short Name</td>
                           <td><asp:TextBox runat="server" ID="txtShortName" CssClass="input input_text medium" ></asp:TextBox>
                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*" ControlToValidate="txtShortName"></asp:RequiredFieldValidator>
                           </td>
                       </tr>
                        <tr>
                           <td style="text-align:left;">CO<sub>2</sub> Tonnes Co<sub>2</sub> / Tonne Fuel </td>
                           <td><asp:TextBox runat="server" ID="txtCo2Per"  CssClass="input input_number small" ></asp:TextBox>
                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*" ControlToValidate="txtCo2Per"></asp:RequiredFieldValidator>
                           </td>
                       </tr>
            </table>
        </div>
        <div class="modal_footer"">
            <asp:Button runat="server" id="btnSave" CssClass="btn" Text="Save" OnClick="btnSave_Click" />
            <asp:Button runat="server" id="btnCancel" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
        </div>
    </div>
    <div class="message">
        <asp:Label runat="server" ID="lblMsg" CssClass="modal_error"></asp:Label>                
    </div>
    </form>
</body>
</html>
                                        
