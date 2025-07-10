<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewVisit.aspx.cs" Inherits="CrewRecord_NewVisit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eManager-HRD</title>
    
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
    function fncReadOnly(evnt)
    {
      event.returnValue = false;
    }
    function reload()
    {
       window.opener.refreshme();
    }
    </script>
    
    <style type="text/css">
        .style1
        {
            width: 159px;
        }
        .style3
        {
        }
        .style4
        {
            width: 154px;
        }
        .style5
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager  ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div> 
    <center>
     <table cellpadding="2" cellspacing="0" style="width: 700px; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9;font-family:Arial;font-size:12px;">
                                        <tr>
                                            <td colspan="6" style=" text-align:center; height:23px" class="text headerband">
                                                <asp:Label runat="server" ID="lblPageName"></asp:Label> 
                                                </td>
                                        </tr>
                                        <tr>                                            
                                            <td style="text-align: center" colspan="6"><asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>&nbsp; </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right;">
                                                Emp # :</td>
                                            <td style="text-align: left;" class="style5" colspan="4"  >
                                                <asp:TextBox ID="txt_CrewNo" runat="server" MaxLength="6" CssClass="input_box" style="text-transform:capitalize;" 
                                                    Width="55px" AutoPostBack="True" ontextchanged="txt_CrewNo_TextChanged" ></asp:TextBox>
                                               <asp:Label ID="lblCrewName" runat="server"></asp:Label>
                                               </td>
                                            <td style="text-align: left"></td>                                            
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" >
                                                                                Occasion :</td>
                                            <td style="text-align: left" class="style3" colspan="4"   >
                                                <asp:DropDownList ID="ddl_Category" runat="server" AutoPostBack="true" 
                                                    onselectedindexchanged="ddl_Category_SelectedIndexChanged" CssClass="input_box" 
                                                    Width="195px">
                                                <asp:ListItem Text="< SELECT >" Value="0" ></asp:ListItem>
                                                <asp:ListItem Text="PRE- JOINING" Value="1" ></asp:ListItem>
                                                <asp:ListItem Text="POST-SIGNOFF" Value="2" ></asp:ListItem>
                                                <asp:ListItem Text="OTHER [SPECIFY]" Value="3" ></asp:ListItem>
                                                </asp:DropDownList>&nbsp;<asp:TextBox ID="txt_Other" runat="server" CssClass="input_box" Width="158px" Visible="false" ></asp:TextBox></td>
                                            <td style="text-align: left"></td>                                                
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" >
                                                <asp:Label runat="Server" ID="lblVessel" Text="Vessel" ></asp:Label> :</td>
                                            <td style="text-align: left" class="style3" colspan="4"   >
                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="195px"></asp:DropDownList> 
                                            </td>
                                            <td style="text-align: left"></td>                                                
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                &nbsp;</td>
                                            <td style="text-align: left; color :Gray; font-size:10px; font-style :italic; " >
                                            <div style ="width :92px ;float:left ">dd-MMM-yyyy</div>
                                            <div style ="width :48px;float:left  ">HH</div> 
                                            <div style ="width :40px; float :left  ">MM</div> 
                                            </td>
                                            <td style="text-align: left" class="style1"  >
                                                &nbsp;</td>                                            
                                            <td style="padding-right: 10px; text-align: right" >
                                                &nbsp;</td>
                                            <td style="text-align: left" class="style4" >
                                                &nbsp;</td>
                                            <td style="text-align: left">&nbsp;</td>                                            
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                From Date & Time :</td>
                                            <td style="text-align: left" class="style5"  >
                                                <asp:TextBox ID="txt_FromDate" runat="server" onkeypress="fncReadOnly(event)" CssClass="input_box" 
                                                    Width="80px" ></asp:TextBox>
                                                <asp:DropDownList ID="ddlFromHour" runat="server"  CssClass="input_box" Width="40px"></asp:DropDownList>:
                                                <asp:DropDownList ID="ddlFromMin" runat="server" CssClass="input_box" Width="40px" ></asp:DropDownList></td>
                                            <td style="text-align: left" class="style1"  >
                                                &nbsp;</td>                                            
                                            <td style="padding-right: 10px; text-align: right" >
                                                &nbsp;</td>
                                            <td style="text-align: left" class="style4" >
                                                &nbsp;</td>
                                            <td style="text-align: left">&nbsp;</td>                                            
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                To Date & Time :</td>
                                            <td style="text-align: left" class="style5"  >
                                                <asp:TextBox ID="txt_ToDate" runat="server" onkeypress="fncReadOnly(event)" 
                                                    CssClass="input_box" Width="80px" 
                                                     ></asp:TextBox>
                                                <asp:DropDownList ID="ddlToHour" runat="server"  CssClass="input_box" Width="40px"></asp:DropDownList>:
                                                <asp:DropDownList ID="ddlToMin" runat="server" CssClass="input_box" Width="40px"></asp:DropDownList></td>
                                            <td style="text-align: left" class="style1"  >
                                                &nbsp;</td>                                            
                                            <td style="padding-right: 10px; text-align: right" >
                                                &nbsp;</td>
                                            <td style="text-align: left" class="style4" >
                                                &nbsp;</td>
                                            <td style="text-align: left">&nbsp;</td>                                            
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                Location :</td>
                                            <td style="text-align: left; " class="style5"  >
                                            <asp:DropDownList ID="ddl_Location" runat="server" CssClass="input_box" Width="195px">
                                                <asp:ListItem Text="< SELECT >" Value="0" ></asp:ListItem>
                                                <asp:ListItem Text="Manila" Value="Manila"></asp:ListItem>
                                                <asp:ListItem Text="Mumbai" Value="Mumbai"></asp:ListItem>
                                                <asp:ListItem Text="Singapore" Value="Singapore"></asp:ListItem>
                                                <asp:ListItem Text="Yangon" Value="Yangon"></asp:ListItem>                                                
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding-right: 10px; text-align: right" class="style1"  >
                                            </td>
                                            <td style="text-align: left" >
                                            </td>
                                            <td style="padding-right: 10px; text-align: right" class="style4" >
                                            </td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                Remark :</td>
                                            <td colspan="5" style="text-align: left; height:5px"><asp:TextBox ID="txt_Remark" 
                                                    TextMode="MultiLine" runat="server" CssClass="input_box" Width="383px" 
                                                    Height="76px" ></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                Created&nbsp;By&nbsp;/&nbsp;On&nbsp;:</td>
                                            <td colspan="5" style="text-align: left; height:5px">
                                            <asp:Label runat="server" ID="lbCreatedBy"></asp:Label> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                Modified&nbsp;By&nbsp;/&nbsp;On&nbsp;:</td>
                                            <td colspan="5" style="text-align: left; height:5px"><asp:Label runat="server" ID="lblModifiedBy"></asp:Label></td>
                                        </tr>
                                        <tr>
                                        <td></td>
                                        <td colspan="5" style=" padding-right:10px; text-align:left" >
                                            <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" onclick="btnSave_Click" Width="70px" /> &nbsp;
                                            <input type="button" onclick="window.close();" class="btn" value="Close" style="width :70px"  /> &nbsp; 
                                            <asp:Button ID="btnNotify" Text="Notify" CssClass="btn" runat="server" onclick="btnNotify_Click" Width="70px" />
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="6">&nbsp;</td>
                                        </tr>
                                    </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_FromDate">
    </ajaxToolkit:CalendarExtender>
     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_ToDate">
    </ajaxToolkit:CalendarExtender>
     </center>    
    </div>
    </form>
</body>
</html>
