<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_TrainingRecords.aspx.cs" Inherits="emtm_MyProfile_Emtm_Profile_TrainingRecords" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master" %>
<%@ Register Src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Show_Image_Large(obj) {
            window.open(obj.src, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
    </script>

        <div style="font-family:Arial;font-size:12px;">
       
        <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
        <ContentTemplate>
          <table width="100%">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                Training Record : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                            </div>
                             <div>
                                <uc2:Profile_PersonalHeaderMenu ID="Emtm_HR_PersonalHeaderMenu1" runat="server" /> 
                             </div> 
                            </td>
                        </tr>
                        </table>  
                        <br />
                        <div style=" padding-top :5px;" >
                        <fieldset>
                        <legend></legend>
                        <table border="0" cellpadding="" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="6">&nbsp;</td></tr>
                        <tr>
                        <td>
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                             <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup> 
                                    <col  /> 
                                    <col style="width:100px;" />                              
                                    <col style="width:200px;" />
                                    <col style="width:150px;" /> 
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />                                                                       
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class= "headerstylegrid">                                  
                                        <td>Training Title</td>
                                        <td>Status</td>
                                        <td>Duration</td>
                                        <td>Location</td>
                                        <td>Source</td>
                                        <td></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                            <div id="dvEmp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                    <col  /> 
                                    <col style="width:100px;" />                              
                                    <col style="width:200px;" />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />                                    
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                               </colgroup>
                                <asp:Repeater ID="rptRecordList" runat="server">
                                    <ItemTemplate>
                                          <tr class='<%# (Common.CastAsInt32(Eval("TrainingPlanningId"))==TrainingPlanningId)?"selectedrow":"row"%>'>
                                            <td align="left">
                                                <%#Eval("TrainingName")%>
                                            </td>
                                            
                                            <td align="left">
                                                <%#Eval("Status")%>
                                            </td>
                                            <td align="left">
                                                <%#Eval("Duration")%>
                                            </td>
                                             
                                            <td align="left">
                                                <%#Eval("Location")%>
                                                </td>
                                            <td align="left">
                                                <%#Eval("SOURCE")%>
                                                </td>
                                            <td align="center">
                                               <a runat="server" ID="ancFile" href='<%# "~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + Eval("FileName").ToString() %>' target="_blank" visible='<%#Eval("FileName").ToString()!= "" %>'  title="Show Certificates" >
                                                <img src="../../HRD/Images/paperclip12.gif" style="border:none"  />
                                                </a>
                                            </td>
                                            <td align="center">
                                               <asp:ImageButton ID="btnEdit" ImageUrl="~/Modules/HRD/Images/upload.jpg" ToolTip="Upload Certificates" CommandArgument='<%#Eval("TrainingPlanningId")%>' Visible='<%#Eval("TrainingPlanningId").ToString()!= "" %>' OnClick="btnEdit_Click" runat="server" /> 
                                               </td>
                                            
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>                                    
                                </td>
                            </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                        </fieldset> 
                        </div>
                    <br />
                        <div id="divUploadDocs" runat="server" visible="false" style=" padding-top :5px;" >
                        <fieldset>
                        <legend><strong>Upload Document</strong></legend>
                        <table border="0" cellpadding="" cellspacing="0" width="100%">
                        <tr>
                             <td style="width:100px;">&nbsp;</td>
                             <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" >Select File :&nbsp;</td>
                            <td align="left">
                              <asp:FileUpload ID="FileUpload1" CssClass="btn" runat="server" Width="438px"/>    
                           </td>
                        </tr>
                        <tr>
                                <td colspan="2" style=" float:right; padding-right:20px;">
                                    <asp:Label ID="lblMsg" Style="color:Red;" runat="server"></asp:Label>
                                    <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" OnClick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btncancel" CssClass="btn" runat="server" OnClick="btncancel_Click" Text="Cancel" CausesValidation="false"></asp:Button>
                                </td>
                            </tr>
                        
                    </table>
                        </fieldset> 
                        </div>

                    
                    </td>
                    </tr>
            </table>
            
        </ContentTemplate>
        <Triggers  >
            <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers>
        </asp:UpdatePanel> 
        
    </div>
</asp:Content>
   
