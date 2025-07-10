<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ER_Accident_Analysis.aspx.cs" Inherits="ER_S115_Analysis" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>eMANAGER</title>
    <style type="text/css">
        .FixedToolbar
        {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 10;
            background-color: #d3d7e4;
        }
        *{
        font-family:Calibri;
        font-size:13px;
        }
      </style>
      <script language="javascript" type="text/javascript">
          function CheckAll(self) {
              for (i = 0; i <= document.getElementsByTagName("input").length - 1; i++) {
                  if (document.getElementsByTagName("input").item(i).getAttribute("type") == "checkbox" && document.getElementsByTagName("input").item(i).getAttribute("id") != self.id) {
                      document.getElementsByTagName("input").item(i).checked = self.checked;
                  }
              }
          }
          function UnCheckAll(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
          {
              for (i = 0; i <= document.getElementById("chklst_Vsls").cells.length - 1; i++) {
                  if (document.getElementById("chklst_Vsls_" + i).checked == false) {
                      document.getElementById("chklst_AllVsl").checked = false;
                  }
              }
          }
    </script>
    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:0px" >
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
    <div style="  font-size:18px; text-align:center; padding:5px;" class="text headerband">Accident LTI Report</div>
    <div style=" text-align:center; font-weight:bold; padding:5px; "><asp:RadioButton ID="rdoIR" Checked="true" AutoPostBack="true" OnCheckedChanged="rdoType_CheckedChanged" GroupName="RT" Text="Injury Analysis" runat="server" /><asp:RadioButton ID="rdoLTI" AutoPostBack="true" OnCheckedChanged="rdoType_CheckedChanged" GroupName="RT" Text="LTI Report" runat="server" />  </div>
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
     <tr>
         <td style="vertical-align:top; background-color:#eeeeee; border-right:solid 1px gray;">
             <div id="dv_InjuryAnalysis" runat="server" >
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr >
                    <td style="text-align:right;font-weight:bold;">Fleet :&nbsp;</td>
                    <td style="text-align:left"><asp:DropDownList runat="server" ID="ddlFleet" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"></asp:DropDownList> </td>
                    
                    </tr>
                    <tr>
                    <td style="text-align:right;font-weight:bold;">Vessel :&nbsp;</td>
                    <td style="text-align:left"><asp:DropDownList runat="server" Width="160px" ID="ddlVessel"></asp:DropDownList> </td>
                    </tr>
                    <tr >
                    <td style="text-align:right;font-weight:bold;">From Date :&nbsp;</td>
                    <td style="text-align:left">
                        <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="155px" ></asp:TextBox>
                        <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
                        <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                        </td>
                    
                    </tr>
                    <tr >
                    <td style="text-align:right;font-weight:bold;">To Date :</td>
                    <td style="text-align:left">
                        <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="155px"  ></asp:TextBox>
                        <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
                        </td>
                    
                    </tr>
                    <tr>
                        
                        <td colspan="2" style="text-align:center;">
                        <asp:Button runat="server" ID="btnShowReport" Text="Show Report" onclick="btnShowReport_Click" CssClass="btn" />
                        </td>
                    </tr>
                    <tr>
                    <td style="text-align:center" colspan="2"><asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label> </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                </table>
    </div>
    <div id="dv_LTI" runat="server" visible="false" >
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                    <table border="0" cellpadding="3" cellspacing="1" width="100%">
                    <tr>
                    <td style="text-align:left;">
                        <asp:CheckBox ID="chklst_AllVsl" runat="server" onclick="javascript:CheckAll(this);" Text="All Vessels" /><br />
                        <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;overflow-x: hidden; border-left: #8fafdb 1px solid; width: 218px; border-bottom: #8fafdb 1px solid; height: 120px; text-align: left">
                            <asp:CheckBoxList ID="chklst_Vsls" runat="server" onclick="return UnCheckAll(this);" Width="216px">
                            </asp:CheckBoxList>
                       </div>
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align:left;"><b>Year :</b>&nbsp;<asp:DropDownList ID="ddl_Year" runat="server" Width="185px" CssClass="input_box"></asp:DropDownList> </td>
                    </tr>
                    <tr>
                    <td style="text-align:center;">
                        <asp:Button runat="server" ID="btnShow_LTI" Text="Show Report" onclick="btnShow_LTI_Click" CssClass="btn" />
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align:center" ><asp:Label ID="lblMsg_LTI" runat="server" ForeColor="Red"></asp:Label> </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                </table>
    </div>
         </td>

         <td>
            <div style=" padding:5px;">
            <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedExpensesToolbar" ID="CrystalReportViewer1" runat="server"  />
            </div>
         </td>
     </tr>
    </table>
    </div>
    </div>
    </form>
</body>
</html>
