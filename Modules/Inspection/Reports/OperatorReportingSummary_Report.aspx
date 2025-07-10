<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Reports/ReportsMaster.master" AutoEventWireup="true" CodeFile="OperatorReportingSummary_Report.aspx.cs" Inherits="OperatorReportingSummary_Report" Title="Untitled Page" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    function CheckAll(self)
    {
        for(i=0;i<=document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls").cells.length-1;i++)  
        {
            if(document.getElementById("ctl00_ContentPlaceHolder1_chklst_AllVsl").checked==true)
            {
                document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls_" + i).checked=true;
            } 
            else
            {
                document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls_" + i).checked=false;
            }
        }
    }
    function UnCheckAll(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
    {
        for(i=0;i<=document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls").cells.length-1;i++)
        {
            if(document.getElementById("ctl00_ContentPlaceHolder1_chklst_Vsls_" + i).checked==false)
            {
                 document.getElementById("ctl00_ContentPlaceHolder1_chklst_AllVsl").checked=false;
            }
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
    
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" >
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="text-align: center; padding-top: 5px; width:200px" valign="top" >
                                                    <table style="width:100%">
                                                    <tr>
                                                            <td>
                                                               Select Owner :
                                                            </td>
                                                            </tr>
                                                              <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_Owner_SelectedIndexChanged" Width="202px">
                                                    </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                            <tr>
                                                            <td style="text-align:center">
                                                            <asp:RadioButton runat="server" ID="rad_Summary" Text="Summary" GroupName="sd" Checked="true" />
                                                            <asp:RadioButton runat="server" ID="rad_Details" Text="Details" GroupName="sd"/>
                                                            </td>
                                                            </tr>
                                                      
                                                        <tr>
                                                            <td>
                                                    <asp:CheckBox ID="chklst_AllVsl" runat="server" Text="All Vessels" onClick="javascript:CheckAll(this);" Checked="true"/>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                    <div style="border: 1px solid #8fafdb; overflow-y: scroll;
                                                        overflow-x: hidden; width: 202px; height: 314px">
                                                        <asp:CheckBoxList id="chklst_Vsls" runat="server" onClick="return UnCheckAll(this);" Width="200px">
                                                        </asp:CheckBoxList>
                                                   </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:center">
                                                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_Show_Click"  />
                                                            </td>
                                                        </tr>
                                                        </table>
                                                </td>
                                                <td rowspan="1" style="text-align: left" valign="top">
                                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label>
                                                    <IFRAME src="" style="WIDTH: 100%; HEIGHT: 440px" id="IFRAME1" frameBorder="0" runat="server" name="I1"></IFRAME>
                                                </td>
                                            </tr>
                                            </table>
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

