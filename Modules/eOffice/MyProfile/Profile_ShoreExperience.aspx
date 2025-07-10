<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_ShoreExperience.aspx.cs" Inherits="Emtm_Profile_ShoreExperience" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shore Experience</title>
     <link href="../style.css" rel="stylesheet" type="text/css" />
     
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             window.opener.document.getElementById("btnhdn").click();
             window.close();
         }
     </script>  
     
</head>
<body>
    <form id="form1" runat="server">
    <div style="vertical-align: top">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>  
                     <table width="100%">
                    <tr>
                        <td valign="top" style="border:solid 1px #4371a5; text-align :center">
                        <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                            Add/Modify Experience 
                        </div>
                        </td> 
                     </tr>    
             </table>
             <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                   <tr>
                    <td style=" font-weight :bold " >
                        <asp:RadioButton ID="rdomtmexp" runat="server" GroupName="exp"  AutoPostBack="true" Text="MTM Experience" oncheckedchanged="rdomtmexp_CheckedChanged" Checked="true" />
                        <asp:RadioButton ID="rdootherexp" runat="server" GroupName="exp" AutoPostBack="true" Text="Other Experience" oncheckedchanged="rdootherexp_CheckedChanged" />
                     </td>
                   </tr>
              </table>
             <asp:Panel ID="PnlOtherExp" runat="server" Visible="false" >
              <div id="divotherexp" runat="server" style="padding:5px 5px 5px 5px;">
                <table id="tblOherxp" runat="server" width="100%"> 
                        <tr>
                            <td>
                                <table cellpadding="2" cellspacing ="0" border="0" style="text-align: center;" width ="100%">
                                <tr>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                       &nbsp;&nbsp;</td>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                    &nbsp;</td>
                            </tr>
                                <tr>
                                <td style="text-align :right">
                                    Company :&nbsp;&nbsp; </td>
                                <td style="text-align :left">
                                       <asp:TextBox ID="txtOthercompany" runat="server" required='yes' MaxLength="100"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="Rfvcompany" runat="server" ControlToValidate="txtOthercompany" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align :right">
                                    Position :&nbsp;&nbsp; </td>
                                <td style="text-align :left">
                                    <asp:TextBox ID="txtOtherposition" runat="server" required='yes' MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rfvposition" runat="server" ControlToValidate="txtOtherposition" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                <tr>
                                <td style="text-align :right">&nbsp;</td>
                                <td style="text-align :left"></td>
                                <td style="text-align :right">&nbsp;</td>
                                <td style="text-align :left"> </td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    From Date :&nbsp;&nbsp; </td>
                                <td style="text-align :left">
                                    <asp:TextBox ID="txtOtherfrmdate" runat="server" required='yes' MaxLength="11"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rfvfrmdate" runat="server" 
                                    ControlToValidate="txtOtherfrmdate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    &nbsp;
                                    <asp:ImageButton ID="imgOtherfrmdate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                </td>
                                <td style="text-align :right">
                                    To Date :&nbsp;&nbsp;
                                </td>
                                <td style="text-align :left">
                                    <asp:TextBox ID="txtOthertodate" runat="server" MaxLength="11"></asp:TextBox>&nbsp;
                                    <asp:ImageButton ID="imgOthertodate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align :right">&nbsp;
                                    </td>
                                <td style="text-align :left">
                                    </td>
                                <td style="text-align :right">
                                    </td>
                                <td style="text-align :left">
                                    </td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    Location :&nbsp;&nbsp; </td>
                                <td style="text-align :left">
                                    <asp:TextBox ID="txtOtherlocation" required='yes' runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rfvlocation" runat="server" ControlToValidate="txtOtherlocation" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    </td>
                                <td style="text-align :right" rowspan="2">
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupPosition="TopLeft" 
                                    runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgOtherfrmdate" 
                                    TargetControlID="txtOtherfrmdate"></ajaxToolkit:CalendarExtender>
                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" 
                                    runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgOthertodate" 
                                    TargetControlID="txtOthertodate"></ajaxToolkit:CalendarExtender>
                                    
                                    <asp:Button ID="btnOtherExpSave" runat="server" CausesValidation="true" CssClass="btn" OnClick="btnOtherExpSave_Click" Text="Save" Width="75px" TabIndex="19" />
                                    <asp:Button ID="btnOhterExpCancel" runat="server" CausesValidation="false" CssClass="btn" Text="Close" Width="75px" TabIndex="20" OnClientClick="CloseWindow();"  />
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align :left">
                                    </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                                 </table>
                            </td>
                        </tr> 
                     </table> 
               </div> 
             </asp:Panel> 
             <asp:Panel ID="PnlMtmExp" runat="server" Visible="false">
               <div id="divmtmexp" runat="server" style="padding:5px 5px 5px 5px;" >
                <table id="tblMTMExp" runat="server" cellpadding="2" cellspacing ="0" border="0" style="text-align: center;" width ="100%">
                        <colgroup>
                            <col align="left" style="text-align :left" width="120px">
                            <col />
                            <col align="left" style="text-align :left" width="120px">
                            <col />
                            <tr>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                    <span lang="en-us">&nbsp; </span>&nbsp;</td>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    Office Location :
                                </td>
                                <td style="text-align :left">
                                    <asp:DropDownList ID="ddlmtmoffice" runat="server" AutoPostBack="true" required='yes' onselectedindexchanged="ddlmtmoffice_SelectedIndexChanged"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="Rfvoffice" runat="server" ControlToValidate="ddlmtmoffice" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align :right">
                                    Position :
                                </td>
                                <td style="text-align :left">
                                    <asp:DropDownList ID="ddlmtmdesignation" runat="server" Width="240px" required='yes'></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Rfvdesignation" runat="server" ControlToValidate="ddlmtmdesignation" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                   
                                </td>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    From Date :</td>
                                <td style="text-align :left">
                                    <asp:TextBox ID="txtmtmfrmdate" runat="server" required='yes' MaxLength="11"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rfvmtmfrmdate" runat="server" ControlToValidate="txtmtmfrmdate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgmtmfrmdate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                </td>
                                <td style="text-align :right">
                                    To Date :
                                </td>
                                <td style="text-align :left">
                                    <asp:TextBox ID="txtmtmtodate" runat="server" MaxLength="11"></asp:TextBox>
                                    <asp:ImageButton ID="imgmtmtodate" runat="server" 
                                        ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                    
                                </td>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td style="text-align :left">
                                <asp:HiddenField ID="hdnexperience" runat="server" Value=""/> 
                                     </td>
                                <td style="text-align :right">
                                    &nbsp;</td>
                                <td rowspan="2" style="text-align :right">
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                        Format="dd-MMM-yyyy" PopupButtonID="imgmtmfrmdate" PopupPosition="TopLeft" 
                                        TargetControlID="txtmtmfrmdate">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" 
                                        Format="dd-MMM-yyyy" PopupButtonID="imgmtmtodate" PopupPosition="TopLeft" 
                                        TargetControlID="txtmtmtodate">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:Button ID="btnMtmExpSave" runat="server" CausesValidation="true" 
                                        CssClass="btn" OnClick="btnMtmExpSave_Click" TabIndex="19" Text="Save" 
                                        Width="75px" />
                                    <asp:Button ID="btnMtmExpCancel" runat="server" CausesValidation="false" 
                                        CssClass="btn" OnClientClick="CloseWindow();" TabIndex="20" Text="Close" 
                                        Width="75px" />
                                </td>
                            </tr>
                            </col>
                            </col>
                        </colgroup>
                        </table>
               </div> 
             </asp:Panel> 
             </ContentTemplate>
      </asp:UpdatePanel>      
     </div>  
    </form>
</body>
</html>
