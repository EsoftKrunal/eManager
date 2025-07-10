<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_Activity.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_Activity" %>

<%@ Register src="HR_ActivityHeader.ascx" tagname="HR_Activity" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function Show_Image_Large(obj)
    {
    window.open(obj.src,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
    }
    function Show_Image_Large1(path)
    {
    window.open(path,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
    }
    </script>

    <style type="text/css">
    .btn11sel
    {
        font-size:14px;
        background-color:#c2c2c2;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #c2c2c2;
        padding:5px;
    }
    .btn11
    {
        font-size:14px;
        background-color:#e2e2e2;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #c2c2c2;
        padding:5px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--<asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>--%>
          <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                Activity : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                            </div>
                            
                            <div >
                                <uc2:HR_Activity ID="Emtm_HR_ActivityHeaderMenu" runat="server" />
                             </div> 
                             <div >
                                    <asp:Button runat="server" Text=" Resign " ID="btnResign" onclick="btnResign_Click"  CssClass="btn11sel" CausesValidation="false" />
                                    <asp:Button runat="server" Text=" Transfer " ID="btnTranster" onclick="btnTranster_Click"  CssClass="btn11"  CausesValidation="false"/>
                                    <asp:Button runat="server" Text=" Confirmation " ID="btnConfirm" onclick="btnConfirmation_Click"  CssClass="btn11"  CausesValidation="false"/>
                                    <asp:Button runat="server" Text=" Promotion " ID="btnPromotion" onclick="btnPromotion_Click"  CssClass="btn11"  CausesValidation="false"/>
                                    <asp:Button runat="server" Text=" ReJoin " ID="btnReJoin" onclick="btnReJoin_Click"  CssClass="btn11"  CausesValidation="false"/>
                                </div>
                            </td>
                        </tr>
                        </table>  
                        <div style="text-align:left; background-color: #c2c2c2; height:400px; border:solid 1px black;border-top:solid 0px black; padding:10px 5px 5px 5px;">
                            <div style="text-align:left; background-color: white; height:395px;">
                                <asp:Panel runat="server" ID="pnlResign" Visible="true">
                                  <table cellpadding="3" cellspacing="0" width="100%" border="0" >
                                        <tr>
                                        <td>
                          <tr>
                              <td style="text-align :right">&nbsp;</td>
                              <td>
                                  &nbsp; &nbsp;</td>  
                              <td style="text-align :right">&nbsp;</td>
                              <td>
                                  &nbsp;</td> 
                              <td style="text-align :right">&nbsp;</td>
                              <td>
                                  &nbsp;</td>
                              
                          </tr>
                          
                          <tr>
                            <td style="text-align :right">Resign Date :</td>
                            <td>
                                <asp:TextBox ID="txtResignedDate" runat="server" required='yes' Width="85px" ></asp:TextBox>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtResignedDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false"/>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" TargetControlID="txtResignedDate" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td></td>
                            <td></td>
                          </tr>
                          
                              <tr>
                                  <td style="text-align :right">
                                      &nbsp;Updated By :</td>
                                  <td>
                                      <asp:TextBox ID="txt_InactiveBy" runat="server" MaxLength="24" TabIndex="1" Enabled="false"></asp:TextBox>
                                      
                                  </td>
                                  <td style="text-align :right">
                                      &nbsp;Updated On :</td>
                                  <td>
                                      <asp:TextBox ID="txt_InactiveOn" runat="server" MaxLength="24" TabIndex="2" Enabled="false" Width="85px"></asp:TextBox>
                                  </td>                                  
                                  <%--<td style="text-align :right">
                                      &nbsp;Status :</td>
                                  <td>
                                      <asp:TextBox ID="txt_Status" runat="server" required='yes' MaxLength="24" TabIndex="3"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_familyName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>--%>
                              </tr>
                          <tr>
                               <td style="text-align :right">Remarks :</td>
                               <td colspan="3">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="99%" Height="100px" ></asp:TextBox>
                               </td>
                               
                          </tr>
                          <tr>
                               <td colspan="4">
                                    
                               </td>                               
                          </tr>
                          
                          <tr>
                              <td>&nbsp;</td>
                              <td>
                               
                              </td>
                               <td></td>
                              <td>
                                  
                              </td>
                              <td></td>
                              <td></td>
                              
                          </tr>
                        </table>
                                  <table width="100%" cellpadding="0" cellspacing="0" > 
                                        <tr>
                                        <td align="right" style="padding-right:10px;">
                                        <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" onclick="btnsave_Click"></asp:Button>
                                        <asp:Button ID="brncancel" CssClass="btn"  runat="server" Text="Cancel" PostBackUrl="~/emtm/staffadmin/SearchDetail.aspx" CausesValidation="false"></asp:Button>
                                        </td>
                                        </tr>
                                   </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlTranster" Visible="false">
                                    <table cellpadding="1" cellspacing="1" width="100%" border="0"  >
                                    <col width="400px" />
                                    <col />
                                    <tr>
                                        <td style="vertical-align:top; padding:5px;">
                                        <span style="font-size:15px;">From Office :</span>
                                        <hr />
                                            <table cellpadding="3" cellspacing="2" border="0" >
                                                <col width="120px" />
                                                <col width="300px" />
                                                <tr>
                                                    <td>Office :</td>
                                                    <td>
                                                        <asp:Label ID="lblOffice" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> Department : </td>
                                                    <td>
                                                        <asp:Label ID="lblDepartment" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> Position :</td>
                                                    <td>
                                                        <asp:Label ID="lblPosition" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td style="vertical-align:top; padding:5px;">
                                        <span style="font-size:15px;"> To Office :</span>
                                        <hr />
                                            <table cellpadding="3" cellspacing="2" border="0" width="100%">
                                                <col width="120px" />
                                                <col />
                                                <tr>
                                                    <td>Office :</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlOffice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"  required='yes'></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Department :</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" required='yes'></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Position :</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPosition" runat="server" required='yes'></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Transfer Date :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtTansferDate" runat="server" Width="85px"  required='yes'></asp:TextBox>
                                                        
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false"/>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtTansferDate" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align:top"> Comments :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtTransferComments" runat="server" Width="500px" Height="50px"  required='yes'></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align:center;">
                                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn" Text=" Transfer "  OnClick="btnSaveTransfer_OnClick" />                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                    <table cellpadding="1" cellspacing="1" border="1" width="100%">
                                                            <tr style=" background-color:#c2c2c2; font-weight:bold; ">
                                                            <td> Transfer Dt.</td>
                                                                <td> From Office</td>
                                                                <td> From Department</td>
                                                                <td> From Position</td>
                                                                <td> To Office</td>
                                                                <td> To Department</td>
                                                                <td> To Position</td>
                                                                <td> Transfered By</td>
                                                            </tr>
                                                            <asp:Repeater ID="rptTransferedData" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td> <%# Common.ToDateString( Eval("TRANSFERDATE"))%> </td>
                                                                        <td> <%#Eval("FROMOFFICE")%> </td>
                                                                        <td> <%#Eval("FROMDEPT")%> </td>
                                                                        <td> <%#Eval("FROMPOSITION")%> </td>
                                                                        <td> <%#Eval("TOOFFICE")%> </td>
                                                                        <td> <%#Eval("TODEPT")%> </td>
                                                                        <td> <%#Eval("TOPOSITION")%> </td>
                                                                        <td> <%#Eval("TRANSFERBY")%> </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                    </td>
                                    </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlConfirmation" Visible="false">
                                <table cellpadding="3" cellspacing="2" border="0" >
                                                <colgroup>
                                                    <col width="120px" />
                                                    <col width="300px" />
                                                    <tr>
                                                        <td>
                                                            Confirmation Date :</td>
                                                        <td>
                                                            <asp:TextBox ID="txtConfirmDate" runat="server" required="yes" Width="85px" MaxLength="15"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="txtConfirmDate_CalendarExtender" 
                                                                runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton5" 
                                                                PopupPosition="TopRight" TargetControlID="txtConfirmDate">
                                                            </ajaxToolkit:CalendarExtender>
                                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                ControlToValidate="txtResignedDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" 
                                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Atatchment File :
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload runat="server" ID="flpC" Width="200px" />
                                                            <asp:ImageButton runat="server" id="lnlAttachment" OnClick="lnlAttachment_Click" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Updated By / On :</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblCUpdatedOn"></asp:Label> </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                             <asp:Button ID="btnConfirm_Save" runat="server" CssClass="btn" Text=" Save "  OnClick="btnConfirm_Save_OnClick" />              
                                                        </td>
                                                    </tr>
                                                </colgroup>

                                            </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlPromotiom" Visible="false">
                                <table cellpadding="3" cellspacing="2" border="0" >
                                                <colgroup>
                                                    <col width="120px" />
                                                    <col width="300px" />
                                                    <tr>
                                                        <td>
                                                            Promotiom Date :</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPromotionDate" runat="server" required="yes" Width="85px" MaxLength="15"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtPromotionDate"></ajaxToolkit:CalendarExtender>
                                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPromotionDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            Current Position :
                                                        </td>
                                                        <td>
                                                           <asp:Label runat="server" ID="lblCurrPosition"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            New Position :
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="ddlNewPosition" runat="server" required='yes' Width="160px" TabIndex="18"></asp:DropDownList>
                                                            <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="ddlNewPosition" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                             <asp:Button ID="btnSavePromotion" runat="server" CssClass="btn" Text=" Save "  OnClick="btnSavePromotion_OnClick" />              
                                                        </td>
                                                    </tr>
                                                </colgroup>

                                            </table>
                                           <table cellpadding="1" cellspacing="1" border="1" width="100%">
                                                            <tr style=" background-color:#c2c2c2; font-weight:bold; ">
                                                                <td> Promotion Dt.</td>
                                                                <td> From Position</td>
                                                                <td> To Position</td>
                                                                <td> Promoted By</td>
                                                            </tr>
                                                            <asp:Repeater ID="rptPromotion" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td> <%# Common.ToDateString(Eval("PROMOTIONDATE"))%> </td>
                                                                        <td> <%#Eval("FromPosition")%> </td>
                                                                        <td> <%#Eval("ToPosition")%> </td>
                                                                        <td> <%#Eval("PromotedBy")%> </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                </td>
                                                </tr>
                                                </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlRejoin" Visible="false">
                               <table cellpadding="3" cellspacing="2" border="0" >
                                                <colgroup>
                                                    <col width="120px" />
                                                    <col width="300px" />
                                                    <tr>
                                                        <td>
                                                            Rejoin Date :</td>
                                                        <td>
                                                            <asp:TextBox ID="txtRejoinDate" runat="server" required="yes" Width="85px" MaxLength="15"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtRejoinDate"></ajaxToolkit:CalendarExtender>
                                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRejoinDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            Last Position :
                                                        </td>
                                                        <td>
                                                           <asp:Label runat="server" ID="lblLastPosition"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            New Position :
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="ddlNewPosition1" runat="server" required='yes' Width="160px" TabIndex="18"></asp:DropDownList>
                                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlNewPosition1" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                             <asp:Button ID="btnSaveRejoin" runat="server" CssClass="btn" Text=" Save "  OnClick="btnSaveRejoin_OnClick" />              
                                                        </td>
                                                    </tr>
                                                </colgroup>

                                            </table>
                                           <table cellpadding="1" cellspacing="1" border="1" width="100%">
                                                            <tr style=" background-color:#c2c2c2; font-weight:bold; ">
                                                                <td> Rejoin Dt.</td>
                                                                <td> Last Position</td>
                                                                <td> New Position</td>
                                                                <td> Rejoin By</td>
                                                            </tr>
                                                            <asp:Repeater ID="rptRejoin" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td> <%# Common.ToDateString(Eval("REJOINDATE"))%> </td>
                                                                        <td> <%#Eval("LastPosition")%> </td>
                                                                        <td> <%#Eval("ToPosition")%> </td>
                                                                        <td> <%#Eval("RejoinBy")%> </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                </td>
                                                </tr>
                                                </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </td>
                    </tr>
            </table>
            
        <%--</ContentTemplate>
        <Triggers  >
            <asp:PostBackTrigger ControlID="btnsave" />
            <asp:PostBackTrigger ControlID="btnConfirm_Save" />
        </Triggers>
        </asp:UpdatePanel> --%>
        
    </div>
    </form>
</body>
</html>
