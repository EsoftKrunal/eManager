<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSafetyAlert.aspx.cs" Inherits="AddSafetyAlert" Title="Add Safety Alert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseThisWindow()
        {
            this.close();
        }
        function RefereshParentPage()
        {
            window.opener.Reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>

     <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
        <center>
        <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
            <div style="border:none; height :50px; width :120px;" >
            <img src="../../HRD/Images/progress.gif" alt="Loading..."/> Loading ...
            </div>
            </center>
        </div>
        </center>
        </ProgressTemplate>
        </asp:UpdateProgress>  

    <asp:UpdatePanel ID="UP1"  runat="server" >
        <ContentTemplate>
            <div>
            <table cellpadding="2" cellspacing="2" border="0" width="750px" rules="none" style="margin:auto;">
                <colgroup>
                    <col width="160px" />
                    <col width="230px" />
                    <col width="99px" />
                    <col />
                    <tr>
                        <td class="text headerband" colspan="4" style=" height: 23px;text-align :center; font-size:15px;" >New Safety Alert </td>
                    </tr>
                    <tr>
                        <td><b>Date :</b> </td>
                        <td>
                            <asp:TextBox ID="txtSADate" runat="server" CssClass="input_box" Enabled="false" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txtSADate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><b>Source :</b> </td>
                        <td>
                            <asp:TextBox ID="lblSource" runat="server" CssClass="input_box" MaxLength="70" Width="400px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;"><b>Topic:</b> </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSATopic" runat="server" CssClass="input_box" Height="40px" MaxLength="50" TextMode="MultiLine" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;"><b>Details :</b> </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSADetails" runat="server" CssClass="input_box" Height="340px" TextMode="MultiLine" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><b>Attach Document : </b></td>
                        <td colspan="3">
                            <asp:FileUpload ID="fuAddFile" runat="server" CssClass="input_box" Width="300px" />
                            <span style="font-size:9px; font-style:italic;color:Maroon;">( Only PDF is allowed.)</span> <a id="aFile" runat="server" target="_blank" visible="false">
                            <img src="../../HRD/Images/paperclipx12.png" style="border:none;" />
                            </a>
                            <br />
                            <span style="font-size:11px; font-style:italic;color:Maroon;"><b>Remark :</b> Please note that the attached pdf will be merged into one circular document. </span></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:right;">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn" OnClick="btnEdit_OnClick" Text="Edit" Visible="false" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_OnClick" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancel_OnClick" Text="Cancel" />
                        </td>
                    </tr>
                </colgroup>
        </table>
        
        <table id="tblNotify" runat="server" cellpadding="3" cellspacing="0" border="0" rules="none" width="750px" style="margin:auto; padding-top:10px;">
            <colgroup>
                <col width="25%" />
                <col width="25%" />
                <col width="25%" />
                <col width="25%" />
                <tr>
                    <td class="text headerband" colspan="4" style=" height: 23px;text-align :center; font-size:15px;" >Notification </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center;">
                        <asp:Button ID="btnNotify" runat="server" CssClass="btn" OnClick="btnNotify_OnClick" style="margin:auto;" Text="Notify To All Ship" Width="150px" />
                        <asp:Label ID="lblMsgNotify" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNotifyBy" runat="server" style="float:right;" Text="Notified By :" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNotifyByDB" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblNotifyOn" runat="server" style="float:right;" Text="Notified On :" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNotifyOnDB" runat="server" Text=""></asp:Label>
                    </td>
                    <td>&nbsp; </td>
                </tr>
            </colgroup>
        </table>
            </div>        
        </ContentTemplate>
     <Triggers>
     <asp:PostBackTrigger ControlID="btnSave" />  
     </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
