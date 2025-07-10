<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Documents.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Documents" %>

<%@ Register src="~/Modules/eOffice/StaffAdmin/Compensation/CB_Menu.ascx" tagname="CB_Menu" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="font-family:Arial;font-size:12px;"><%--onkeydown="javascript:EnterToClick();"--%>
    
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="border:solid 0px #4371a5;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :5px; font-weight: bold;">
                            Compensation and Benifits - Documents
                        </div>
                        <div style="background-color:white;">
                            
                        </div>
                        <%--<asp:UpdatePanel id="UpdatePanel" runat="server" >
                        <ContentTemplate>--%>
                            <div class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden; WIDTH: 95%;text-align:left; border-bottom:none;margin:0px auto;">
                                
                            <table border="0" cellpadding="5" cellspacing="5" style="width:100%;border-collapse:collapse;" >
                                <col  width="55px"/>
                                <col  width="200px"/>
                                <col  width="95px"/>
                                <col  width="300px"/>
                                <col  width="80px"/>
                                <col  />
                                <tr >
                                    <td><b>Code :</b> </td>
                                    <td>
                                        <asp:Label id="lblEmpCode" runat="server"></asp:Label>
                                    </td>
                                    <td><b>Emp Name :</b></td>
                                    <td>
                                        <asp:Label id="lblEmpName" runat="server"></asp:Label>
                                    </td>
                                    <td><b>Position :</b></td>
                                    <td >
                                        <asp:Label id="lblEmpPosition" runat="server"></asp:Label>
                                    </td>
                                </tr>    
                             </table>

                            </div>

                            <div class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden; WIDTH: 95%;text-align:right; border-bottom:none;margin:0px auto;">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <col width="150px" />
                                    <col />
                                    <tr>
                                        <td>
                                            <asp:Label ID="EmpCount" runat="server" style="float:left;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAddNew" CssClass="btn" runat="server" Text="Add New" Width="80px" onclick="btnAddNew_Click" CausesValidation="false" Style="margin:3px;"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                                    
                                    
                            </div>

                            <div id="divTraveldocument" runat="server" style="padding:0px 5px 5px 5px;" >
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridheader">
                                    <colgroup>
                                        <col style="width:30px;"/>
                                        <col style="width:30px;"/>
                                        <col  />                                        
                                        <col style="width:150px;"/>
                                        <col style="width:90px;" />
                                        <col style="width:90px;" />                                          
                                        <col style="width:30px;" />  
                                        <tr class= "headerstylegrid">
                                            <td></td>
                                            <td></td>
                                            <td>Document Name</td>
                                            <td>Document #</td>
                                            <td>Issue Date</td>
                                            <td>Expiry Date</td>                                            
                                            <td></td>
                                        </tr>
                                    </colgroup>
                                </table>
                                </div>      
                                
                            <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;height:500px;text-align:center;margin-bottom:10px;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridrow">
                                <colgroup>
                                        <col style="width:30px;"/>
                                        <col style="width:30px;"/>
                                        <col  />                                        
                                        <col style="width:150px;"/>
                                        <col style="width:90px;" />
                                        <col style="width:90px;" />
                                          <col style="width:30px;" />  
                                </colgroup>
                                <asp:Repeater ID="RptLeaveSearch" runat="server" >
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("EmpId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td>
                                                <asp:ImageButton ID="btndocedit" runat="server" CommandArgument='<%# Eval("OtherDocId") %>' 
                                                    Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                OnClick="btndocedit_Click" ToolTip="Edit" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("OtherDocId") %>'  Visible="<%#auth.IsDelete%>" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                            OnClientClick="javascript:return window.confirm('Are you sure to delete?');"  
                                            OnClick="btndocDelete_Click" ToolTip="Delete"/>
                                            </td>
                                            <td align="left"><%#Eval("DocumentName")%></td>
                                            <td align="left"><%#Eval("DocumentNo")%></td>
                                            <td align="center"><%#Eval("IssueDate")%></td>
                                            <td align="center"><%#Eval("ExpiryDate")%></td>
                                            <td>
                                                <a ID="ancFile" runat="server" href='<%#"../PopupAttachment.aspx?OtherDocId=" + Eval("OtherDocId").ToString() %>' 
                                                target="_blank" title="Show Document" visible='<%#Eval("FileName").ToString()!= "" %>'>
                                            <img src="../../../HRD/Images/paperclip12.gif" style="border:none" /></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                               </table>
                            </div> 
                                </div>

                            <%--------------------------------------------------------------------%>
                            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddDocuments" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position :relative; width:750px; height:200px;text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
        <center>
            <div style="background-color:#333;padding:5px;font-weight:bold;color:white;"> Add Documents </div>
                <table id="tblview" runat="server" width="100%" cellpadding="4" cellspacing ="4" border="0">
                               <colgroup>
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <tr>
                                       <td style="text-align :right">
                                           Document Name :</td>
                                       <td style="text-align :left">
                                       
                                           <%--<asp:DropDownList ID="ddlMedicalDocName" runat="server" Width="190px" required="yes"></asp:DropDownList> --%>
                                           <asp:TextBox ID="txtOtherDocName" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RfvDocName" runat="server" 
                                               ControlToValidate="txtOtherDocName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                           </td>
                                       <td style="text-align :right">
                                           Document # :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtDocumentNo" runat="server" MaxLength="30" ></asp:TextBox>
                                           <%--<asp:RequiredFieldValidator ID="RfvDocumentNo" runat="server" 
                                               ControlToValidate="txtDocumentNo" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">
                                           Issue Date :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtIssuedate" runat="server" MaxLength="11" ></asp:TextBox>
                                           
                                           <%--<asp:RequiredFieldValidator ID="RfvIssuedate" runat="server" 
                                               ControlToValidate="txtIssuedate" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                               
                                           <asp:ImageButton ID="imgIssuedate" runat="server" 
                                               ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       </td>
                                       <td style="text-align :right">
                                           Expiry date :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtExpirydate" runat="server" MaxLength="11"></asp:TextBox>
                                           <asp:ImageButton ID="imgExpirydate" runat="server" 
                                               ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       </td>
                                   </tr>
                                   
                                   <tr>
                                       <td style="text-align :right">
                                           Attach Document :
                                       </td>
                                       <td style="text-align :left">
                                           <asp:FileUpload ID="fldocument" runat="server" />
                                       </td>
                                       <td style="text-align :right">
                                           </td>
                                       <td>
                                           
                                       </td>
                                   </tr>
                                   <tr>
                                       <td valign="top">
                                           &nbsp;</td>
                                       <td valign="top">
                                           &nbsp;</td>
                                       <td>
                                           &nbsp;</td>
                                       <td>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgIssuedate" PopupPosition="TopLeft" 
                                               TargetControlID="txtIssuedate">
                                           </ajaxToolkit:CalendarExtender>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgExpirydate" PopupPosition="TopLeft" 
                                               TargetControlID="txtExpirydate">
                                           </ajaxToolkit:CalendarExtender>
                                       </td>
                                   </tr>
                                   </col>
                                   </col>
                               </colgroup>
                            </table>
                            <table cellpadding="2" cellspacing ="2" border="0" width ="100%">
                            <tr>
                                <td align="right">
                                    
                                    <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" onclick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btnClose" CssClass="btn"  runat="server" Text="Close" onclick="btnClose_Click" CausesValidation="false"></asp:Button>
                                </td>
                            </tr>
                            </table>
                
            </center>
    </div>
        </center>
            </div>
                                <%--</ContentTemplate>
                         </asp:UpdatePanel>--%>
                        </td>
                    </tr>
            </table>      
  
     
    </div>


        
    </form>
</body>
</html>
