<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssessmentMail.aspx.cs" Inherits="AssessmentMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Crew Assessment</title>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:13px;
    }
        
    </style>
    <script type="text/javascript" language="javascript">
        function refreshParent() {
            if (window.opener.document.getElementById('btnRefresh') != null) {
                window.opener.document.getElementById('btnRefresh').click();    
            }
        }
    </script>
</head>
<body style="margin: 0 0 0 0">
    <form id="form1" runat="server">
    <center>
    <div style="text-align: center; width:900px;" runat="server" id="dvAll">
        <table border="0" cellpadding="0" cellspacing="0" style="background-color:#f9f9f9; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; padding-right: 10px; padding-left: 10px; width:100%" >
            <tr>
                <td style="background-color:#4371a5; height: 35px; text-align:center; width: 980px; color:White;"  >
                    <b>Crew Assessment</b></td>
            </tr>
            <tr>
                <td style="width: 980px">
                    <table cellpadding="4" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="text-align: right; width:200px;" >Crew Name :</td>
                    <td style="text-align: left"><asp:Label ID="lblCrewName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td style="text-align: right" >Rank:</td>
                    <td style="text-align: left"><asp:Label ID="lblRank" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td style="text-align: right" >Crew #:</td>
                    <td style="text-align: left"><asp:Label ID="lblCrewNo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td style="text-align: right" >On Board Vessel:</td>
                    <td style="text-align: left"><asp:Label ID="lblVessel" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td style="text-align: right" >Contract #:</td>
                    <td style="text-align: left"><asp:Label ID="lblContactRefNo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td style="text-align: right" >Relief Due Date :</td>
                    <td style="text-align: left"><asp:Label ID="lblReliefDueDt" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="background-color:#4371a5; height:2px; text-align:center; color:White;">
                    </td>
            </tr>
            <tr>
                <td>
                <div style="text-align:left;padding:5px;">
                Rate the performance of the above crew on vessel: <asp:Label ID="lblCurrVessel" runat="server"></asp:Label>
                </div>
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:200px; text-align:right;" >Select Grade :</td>
                    <td style="text-align: left">
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                    <td style="width:150px">
                        <asp:RadioButtonList ID="rdoGrade" RepeatDirection="Horizontal" runat="server" >
                            <asp:ListItem Text="A" Value="A" ></asp:ListItem>
                            <asp:ListItem Text="B" Value="B" ></asp:ListItem>
                            <asp:ListItem Text="C" Value="C" ></asp:ListItem>
                            <asp:ListItem Text="D" Value="D" ></asp:ListItem>
                        </asp:RadioButtonList>
                        <td>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdoGrade" InitialValue="" ErrorMessage="*" ForeColor="Red" Display="Dynamic"  runat="server"></asp:RequiredFieldValidator>                        
                        </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    <tr>
                       <td style="text-align: right">
                           Remarks :
                      </td>
                      <td style="text-align: left">
                          <asp:TextBox ID="txtRemarks" TextMode="MultiLine" MaxLength="500" 
                              runat="server" Width="500px" Height="109px"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left; font-style:italic;">
                        <div style="background:#CCFF66; color:Black ; padding:5px;">Grade A : Very Good. Needs no supervision</div>
                        <div style="background:yellow; color:Black; padding:5px;">Grade B : Good but needs supervision at times</div>
                        <div style="background:#FFC2B2; color:Black; padding:5px;">Grade C : Average Performance. May return to the owner's fleet with adequate training</div>
                        <div style="background:red; color:White; padding:5px;">Grade D : Poor performance. Should not be assigned to any of the Owner's Fleet</div>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding: 5px;">
                    Requested On : <asp:Label ID="lblRequestedOn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                    <td  style="background-color:#4371a5; height: 35px; text-align:center; width: 980px; color:White; padding:8px;"  >
                    
                    <asp:Button ID="btnSave" Text="Submit" Width="100px" 
                            style="background-color:#FF7519; border:none; color:White; padding:2px;" 
                            OnClick="btnSave_Click" runat="server" />&nbsp;
                    <asp:Button ID="btnClose" Text="Close" Width="100px"  style="background-color:#FF7519; border:none;color:White;  padding:2px;" OnClientClick="window.close();" runat="server" />
                    </td>
                    </tr>
                    <tr runat="server" id="trPreSubmitted" visible="false">
                    <td style="padding:3px" >
                    <span style='color:Red'>Grade Already Submitted</span>

                    </td>
                    </tr>
        </table>
    </div>
    </center>
    </form>
</body>
</html>
