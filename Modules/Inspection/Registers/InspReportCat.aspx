<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspReportCat.aspx.cs" Inherits="InspReportCat"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
     </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
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
                                &nbsp;&nbsp;Select Inspection : &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlInspection" runat="server" Width="200px" CssClass="input_box" OnSelectedIndexChanged="ddlInspection_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <asp:Button ID="btnNew" runat="server" CssClass="btn" Text="Add New Heading" Width="140px" CausesValidation="False" TabIndex="6" OnClick="btnAddNewHead_Click" />
                        </td>
                    </tr>
                </table>
           <asp:Label ID="lblMessege" runat="server" style="color:Red;"></asp:Label>
           <div style=" padding:5px;">
                <div style="border:solid 1px #c2c2c2;overflow-x:hidden; overflow-y:hidden;">
                            <table cellpadding="2" cellspacing="0" width="100%">
                                <colgroup>
                                <col width="50px"/>
                                <col width="50px"/>
                                <col width="50px"/>
                                <col width="400px"/>
                                <col />
                                </colgroup>
                                <tr class= "headerstylegrid" style="font-weight:bold; text-align:center;">
                                    <td style="text-align:center">Edit</td>
                                    <td style="text-align:center">Delete</td>
                                    <td style="text-align:center">Sr#</td>
                                    <td>Main Heading</td>
                                    <td>Sub Heading</td>
                                </tr>
                            </table>
                            </div>
                <div style="border:solid 1px #c2c2c2;overflow-x:hidden; overflow-y:scroll; height:330px;">
                <table cellpadding="2" cellspacing="0" width="100%" border="1" rules="all" style="border-bottom:solid 1px #c2c2c2; border-collapse:collapse;" bordercolor="#c2c2c2">
                    <colgroup>
                    <col width="50px"/>
                    <col width="50px"/>
                    <col width="50px"/>
                    <col width="400px"/>
                    <col />
                    </colgroup>
                    <asp:Repeater ID="rptCategories" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center">
                                    <asp:ImageButton runat="server" ID="bthEdit" OnClick="btnEdit_Click" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%# Eval("Sno") %>' />
                                </td>
                                <td style="text-align:center">
                                    <asp:ImageButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%# Eval("Sno") %>' OnClientClick="return window.confirm('Are you sure to delete this record?')" />
                                </td>
                                <td style="text-align:center"><asp:Label ID="lblID" runat="server" Text='<%# Eval("Sno") %>'/></td>
                                <td><%#Eval("MainHeading")%></td>
                                <td> <%#Eval("SubHeading")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>   
                </div>
           </fieldset>                              
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
    
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="DivAddNewHead" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :520px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:300px; padding :3px; text-align :center; border :solid 1px ; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <col width="120px" />
                    <col />
                    <tr>
                        <td class="text headerband" style="text-align:center; font-size:14px;" colspan="2" > <b>Add Edit Heading </b> </td>
                    </tr>
                    <tr>
                          <td >Sr# :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtSrno" runat="server" CssClass="required_box" MaxLength="10" Width="50px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtSrno"></asp:RequiredFieldValidator>
                           </td>
                      </tr>
                    <tr>
                          <td >Main Heading :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtMainHeading" runat="server" CssClass="required_box" MaxLength="100" Width="400px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtMainHeading"></asp:RequiredFieldValidator>
                           </td>
                      </tr>
                    <tr>
                    <tr>
                          <td >Sub Heading :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtSubHeading" runat="server" CssClass="input_box" MaxLength="100" Width="400px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                           </td>
                      </tr>
                    <tr>
                        <td colspan="2" style="text-align:center; text-align:right;"> 
                            <asp:Label runat="server" ID="lblMSGPopUp" ForeColor="Red" style="float:left"></asp:Label>        
                        
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="7" OnClick="btn_Save_InspGrp_Click" />
                            <asp:Button ID="btnCloseAddHeadPopup" runat="server" CssClass="btn" Text="Close" Width="59px" TabIndex="7" OnClick="btnCloseAddHeadPopup_Click"  CausesValidation="false"/>
                        </td>
                    </tr>
             
                </table>
            </div>
        </center>
    </div>
   </form>
</body>
</html>

