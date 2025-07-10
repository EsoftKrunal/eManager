<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Reports/ReportsMaster.master" AutoEventWireup="true" CodeFile="InspHistory_Report.aspx.cs" Inherits="Reports_InspHistory_Report" Title="Untitled Page" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
        function checkDate(theField){
          dPart = theField.value.split("-");
          if(dPart.length!=3){
            alert("Enter Date in this format: dd mmm yyyy!");
            theField.focus();
            return false;
          }
            var check=0;
            for(i=0;i<month.length;i++){
            if(dPart[1].toLowerCase()==month[i].toLowerCase()){
             check=1;
              dPart[1]=i;
              break;
            }
          }
          if(check==0)
          {
          alert("Enter Date in this format: dd mmm yyyy!");
          return false;
          }
          nDate = new Date(dPart[2], dPart[1], dPart[0]);
         // nDate = new Date(dPart[0], dPart[1], dPart[2]);
          if(isNaN(nDate) || dPart[2]!=nDate.getFullYear() || dPart[1]!=nDate.getMonth() || dPart[0]!=nDate.getDate()){
            alert("Enter1 Date in this format: dd mmm yyyy!");
            theField.select();
            theField.focus();
            return false;
          } else {
            return true;
          }
        }
        function ValidateDate()
        {
            if(document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate').value=='')
            {
                alert("Please Enter From Date!");
                document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate').focus();
                return false;
            }
            if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txtfromdate')))
            return false;
            if(document.getElementById('ctl00_ContentPlaceHolder1_txttodate').value=='')
            {
                alert("Please Enter To Date!");
                document.getElementById('ctl00_ContentPlaceHolder1_txttodate').focus();
                return false;
            }
            if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txttodate')))
            return false;
        }
    </script>
    
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" >
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <%--<tr>
                        <td align="center" class="text" style="height: 23px; background-color: #4371a5">
                            Inspection Misc. Report</td>
                    </tr>--%>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-right: 10px; color: red; text-align: center">
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="text-align: left; padding-left: 20px;" valign="top">
                                                    <asp:CheckBoxList id="chkallgp" runat="server" AutoPostBack="True" Width="180" OnSelectedIndexChanged="chkallgp_SelectedIndexChanged">
                                                        <asp:ListItem>All Groups</asp:ListItem>
                                                    </asp:CheckBoxList></td>
                                                <td style="text-align: left" valign="top">
                                                    <asp:CheckBoxList id="chkallinsp" runat="server" AutoPostBack="True" Width="180" OnSelectedIndexChanged="chkallinsp_SelectedIndexChanged">
                                                        <asp:ListItem>All Inspections</asp:ListItem>
                                                    </asp:CheckBoxList></td>
                                                <td style="text-align: right" valign="bottom">
                                                    Owner :</td>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_Owner_SelectedIndexChanged" Width="202px">
                                                    </asp:DropDownList></td>
                                                <td style="text-align: left" valign="top"><asp:CheckBoxList id="chklst_AllVsl" runat="server" AutoPostBack="True" Width="180" OnSelectedIndexChanged="chklst_AllVsl_SelectedIndexChanged">
                                                    <asp:ListItem>All Vessels</asp:ListItem>
                                                </asp:CheckBoxList></td>
                                                <td rowspan="3" style="text-align: left" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="5" style="padding-left: 20px; text-align: left" valign="top">
                                                    <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                        overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                                        height: 76px; text-align: left">
                                                        <asp:CheckBoxList id="chkgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkgroup_SelectedIndexChanged"
                                                            Width="180">
                                                        </asp:CheckBoxList></div>
                                                </td>
                                                <td rowspan="5" style="text-align: left" valign="top">
                                                    <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                        overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                                        height: 76px">
                                                        <asp:CheckBoxList id="chk_inspection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chk_inspection_SelectedIndexChanged"
                                                            Width="180">
                                                        </asp:CheckBoxList></div>
                                                    </td>
                                                <td style="text-align: right;" valign="bottom">
                                                    From Date :</td>
                                                <td style="text-align: left;" valign="bottom">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="93px"></asp:TextBox>
                                                    <asp:ImageButton
                                                    ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"
                                                    TabIndex="79" /></td>
                                                <td rowspan="5" valign="top">
                                                    <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                        overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                                        height: 76px">
                                                        <asp:CheckBoxList id="chklst_Vsls" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chk_inspection_SelectedIndexChanged"
                                                            Width="180">
                                                        </asp:CheckBoxList></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" valign="bottom">
                                                    To Date :</td>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="93px"></asp:TextBox>
                                                    <asp:ImageButton
                                                    ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"
                                                    TabIndex="79" /></td>
                                                <%--OnClientClick="return ValidateDate();"--%>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" valign="top">
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="chk_History" runat="server" Text="History" /></td>
                                                <td rowspan="1" style="text-align: left" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" valign="top">
                                                </td>
                                                <td style="text-align: left; padding-top: 5px;" valign="top">
                                                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_Show_Click"  /></td>
                                                <td rowspan="1" style="text-align: left" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" valign="top">
                                                </td>
                                                <td style="text-align: left" valign="top">
                                                </td>
                                                <td rowspan="1" style="text-align: left" valign="top">
                                                </td>
                                            </tr>
                                                <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="false"
                                                    ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
                                                    TargetControlID="txtfromdate">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="false"
                                                    ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
                                                    TargetControlID="txttodate">
                                                    </ajaxToolkit:MaskedEditExtender>--%>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;<br />
                                                    <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="202px" Visible="False">
                                                    </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                            </tr>
                                            <tr><td colspan="6" style="padding-right: 10px; padding-left: 10px;">
                                                <%--<cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" DisplayGroupTree="False"></cr:crystalreportviewer>--%>
                                                <IFRAME style="WIDTH: 100%; HEIGHT: 280px" id="IFRAME1" frameBorder="0" runat="server"></IFRAME>
                                            </td>
                                            </tr>
                                        </table>
                                        <div id="divPrint">
                                            &nbsp;</div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>

