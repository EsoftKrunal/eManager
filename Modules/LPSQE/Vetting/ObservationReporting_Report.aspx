<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObservationReporting_Report.aspx.cs" Inherits="Reports_ObservationReporting_Report" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VettingScript.js" type="text/javascript"></script>
    <link href="Vettingstyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function CheckUncheckAll(self) {

            var chks = document.getElementById("dvvsl").getElementsByTagName("input");

            for (i = 0; i <= chks.length - 1; i++) {
                if (chks[i].getAttribute("type") == "checkbox")

                    chks[i].checked = self.checked;
            }
        }
    
    function SetMode(Mode)
    {
        if(parseInt(Mode)==1)
        {
        document.getElementById('ctl00_ContentPlaceHolder1_txtInspector').value='';
        document.getElementById('ctl00_ContentPlaceHolder1_txtChapter').value='';
        }
        if(parseInt(Mode)==2)
        {
        document.getElementById('ctl00_ContentPlaceHolder1_txtChapter').value='';
        document.getElementById('ctl00_ContentPlaceHolder1_txtCrewNumber').value='';
        }
        if(parseInt(Mode)==3)
        {
        document.getElementById('ctl00_ContentPlaceHolder1_txtInspector').value='';
        document.getElementById('ctl00_ContentPlaceHolder1_txtCrewNumber').value='';
        }
    }
</script>
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
    </head>
    <body>
<form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" >
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <%--<cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" DisplayGroupTree="False"></cr:crystalreportviewer>--%>
                    <tr>
                        <td>
                        
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-right: 10px; color: red; text-align: center">
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                    <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
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
                                                <td style="text-align: left" valign="top" colspan="2">
                                                <div style ="float :left ">
                                                    <asp:CheckBox ID="chklst_AllVsl" runat="server" Text="All Vessels" onClick="javascript:CheckUncheckAll(this);" Checked="true"/>
                                                    <asp:CheckBox runat="server" ID="chkInactive" Text ="Include Inactive Vessel" 
                                                        OnCheckedChanged="Checked_Changed" AutoPostBack="True"/>  
                                                        </div>
                                                        <div style=" float :right" >Period :&nbsp;</div>
                                                </td>
                                                <td style="text-align: left;valign="top">
                                                   <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="93px"></asp:TextBox>
                                                    <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif" TabIndex="79" />
                                                    - <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="93px"></asp:TextBox>
                                                    <asp:ImageButton
                                                    ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"
                                                    TabIndex="79" />
                                                    </td>
                                                <td style="text-align: left; padding-left: 4px;" valign="top">
                                                    &nbsp;</td>
                                                <td rowspan="4" style="text-align: left" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="4" style="padding-left: 20px; text-align: left" valign="top">
                                                    <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                        overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                                        height: 76px; text-align: left">
                                                        <asp:CheckBoxList id="chkgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkgroup_SelectedIndexChanged"
                                                            Width="180">
                                                        </asp:CheckBoxList></div>
                                                </td>
                                                <td rowspan="4" style="text-align: left" valign="top">
                                                    <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                        overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                                        height: 76px">
                                                        <asp:CheckBoxList id="chk_inspection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chk_inspection_SelectedIndexChanged"
                                                            Width="180">
                                                        </asp:CheckBoxList></div>
                                                    </td>
                                                <td rowspan="4" style="text-align: left" valign="top">
                                                    <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                        overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                                        height: 76px" id="dvvsl">
                                                        <asp:CheckBoxList id="chklst_Vsls" runat="server" Width="180"></asp:CheckBoxList>
                                                   </div>
                                                    </td>
                                                <td style="text-align: left;" valign="bottom">
                                                    <asp:RadioButton ID="RadioButton1" runat="server" onclick="javascript:SetMode(1);" GroupName="one" 
                                                        Text="By Crew" />
                                                </td>
                                                <td style="text-align: left;" valign="bottom">
                                                    <asp:TextBox ID="txtCrewNumber" onclick="document.getElementById('ctl00_ContentPlaceHolder1_RadioButton1').checked=true;" runat="server" CssClass="input_box" Width="50px" MaxLength="6"  ></asp:TextBox>
                                                </td>
                                                <td rowspan="4" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="one" onclick="javascript:SetMode(2);"
                                                        Text="By Inspector" />
                                                </td>
                                               <td> <asp:TextBox ID="txtInspector" onclick="document.getElementById('ctl00_ContentPlaceHolder1_RadioButton2').checked=true;" runat="server" CssClass="input_box" Width="200px" MaxLength="100"></asp:TextBox>
                                               </td>
                                                <td style="text-align: left" valign="bottom">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="one" onclick="javascript:SetMode(3);"
                                                        Text="By Chapter" />
                                                </td>
                                               <td> <asp:TextBox ID="txtChapter" onclick="document.getElementById('ctl00_ContentPlaceHolder1_RadioButton3').checked=true;" runat="server" CssClass="input_box" Width="50px" MaxLength="50"></asp:TextBox>
                                               </td>
                                                <td style="text-align: left" valign="bottom">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; padding-top: 5px; height: 10px;" valign="top">
                                                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName="one" onclick="javascript:SetMode(3);"
                                                        Text="Open Search" />
                                                </td>
                                                <td> <asp:TextBox ID="txtOpenSearch" 
                                                        onclick="document.getElementById('ctl00_ContentPlaceHolder1_RadioButton4').checked=true;" 
                                                        runat="server" CssClass="input_box" Width="200px" MaxLength="100"></asp:TextBox>
                                                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" 
                                                        OnClick="btn_Show_Click" Width="120px"  /></td>
                                                <td rowspan="1" style="text-align: left; height: 10px;" valign="top">
                                                </td>
                                            </tr>
                                        </table>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate">
                                                    </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_Show" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            
            </td>
        </tr>
    </table>
      <IFRAME style="WIDTH: 100%; HEIGHT: 325px" id="IFRAME1" frameBorder="0" runat="server"></IFRAME>
</form>
</body>
</html>

