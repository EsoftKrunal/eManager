<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchDetail.aspx.cs" Inherits="SearchDetail" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
<%--</head>
<body>
    <form id="form1" runat="server" defaultbutton="btn_Search"> --%>
    <div>
       <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
          <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div style="">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="width:75%;">
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :3px; font-weight: bold;">
                            Search
                        </div>
                        </td> 
                        </tr>
                        <tr>
                            <td style="padding-right:10px; text-align:center; color:Red">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing ="0" border="1" style=" border-collapse:collapse;" width ="100%">
                                <tr>
                                <td>
                                    <table border="0" cellspacing="0" cellpadding="0px"  style="text-align: center  ; padding-right:5px; height :90px;" width="100%">
                                        <tr>
                                            <td  style="width: 120px; height: 3px; text-align: right;">
                                                Emp. #:
                                            </td>
                                            <td  style="width: 148px; height: 3px; text-align: left; padding-left: 5px;">
                                                <asp:TextBox ID="txtempno" runat="server" MaxLength="6"
                                                    Width="160px" TabIndex="1"></asp:TextBox></td>
                                            <td  class="tablepad" style="width: 139px; height: 5px; text-align: right;">
                                                Emp. Name:
                                            </td>
                                            <td style="padding-left: 5px; width: 159px; height: 5px; text-align: left">
                                                <asp:TextBox ID="txt_FirstName_Search" runat="server" MaxLength="24"
                                                    Width="160px" TabIndex="2"></asp:TextBox></td>
                                            <%--<td  style="width: 129px; height: 5px; text-align: right;">
                                                Family Name:</td>
                                            <td style="padding-left: 5px; width: 191px; height: 5px; text-align: left">
                                                <asp:TextBox ID="txt_LastName_Search" runat="server" MaxLength="24"
                                                    Width="160px" TabIndex="3"></asp:TextBox></td>--%>
                                            <td  style="width: 129px; height: 5px; text-align: right;">
                                              Gender :
                                                </td>
                                            <td style="padding-left: 5px; width: 191px; height: 5px; text-align: left">
                                            <asp:DropDownList ID="ddlgender" runat="server" Width="165px" >
                                                </asp:DropDownList>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 3px; text-align: right;">
                                             Off.:
                                            </td>
                                            <td style="padding-left: 5px; text-align: left">
                                               <asp:DropDownList ID="ddloffice" runat="server" Width="165px" TabIndex="16" AutoPostBack="true" OnSelectedIndexChanged="ddloffice_SelectedIndexChanged" >
                                                </asp:DropDownList>  
                                            </td>
                                            <td  style="width: 120px; height: 5px; text-align: right;">
                                            Dept. :
                                                </td>
                                            <td style="padding-left: 5px; text-align: left">
                                               <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="18" Width="165px">
                                                </asp:DropDownList> 
                                            </td>
                                            <td  style="text-align: right;">
                                                Joining Date:</td>
                                            <td style="padding-left: 5px; text-align: left">
                                                <asp:TextBox ID="txt_djc_from" runat="server" MaxLength="12" Width="72px" ToolTip="Period (From Date)"></asp:TextBox>
                                                -
                                                <asp:TextBox ID="txt_djc_to" runat="server" MaxLength="12" Width="72px" ToolTip="Period (To Date)"></asp:TextBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 1px; text-align: right;">
                                                Age:</td>
                                            <td style="padding-left: 5px; height: 1px; text-align: left;">
                                                From: &nbsp;&nbsp;
                                                <asp:TextBox ID="txt_Age_From" runat="server" MaxLength="2"
                                                    Width="16px" TabIndex="12"></asp:TextBox>
                                                &nbsp; &nbsp;&nbsp; To: &nbsp;
                                                <asp:TextBox ID="txt_Age_To" runat="server" CssClass="input_box"
                                                        MaxLength="2" Width="16px" TabIndex="13"></asp:TextBox>
                                            </td>
                                            <td style="width: 139px; height: 1px; text-align: right;">
                                                Passport #:</td>
                                            <td style="padding-left: 5px; height: 1px; text-align: left;">
                                                <asp:TextBox ID="txt_PassportNo" runat="server" MaxLength="20" Width="160px" TabIndex="14"></asp:TextBox>
                                            </td>
                                            <td style="width: 129px; height: 1px; text-align: right;">
                                                NRIC/Fin :</td>
                                            <td style="padding-left: 5px; height: 1px; text-align: left; width: 191px;">
                                                <asp:TextBox ID="txt_NricFin" runat="server" MaxLength="6"
                                                    Width="160px" TabIndex="1"></asp:TextBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td  style="width: 120px; height: 1px; text-align: right;">
                                            Nationality:
                                                </td>
                                            <td style="padding-left: 5px; width: 148px; height: 1px; text-align: left">
                                              <asp:DropDownList ID="ddlNationality" runat="server" Width="165px" TabIndex="7">
                                                </asp:DropDownList>
                                                </td>
                                             <td  style="width: 129px; height: 1px; text-align: right;">
                                             Emp.Status:
                                                </td>
                                            <td style="padding-left: 5px; width: 191px; height: 1px; text-align: left">
                                            <asp:DropDownList ID="ddlemployeestatus" runat="server" CssClass="input_box"
                                                    Width="165px" TabIndex="11">
                                                </asp:DropDownList>
                                                </td>   
                                            <td  style="width: 139px; height: 1px; text-align: right;">
                                                </td>
                                            <td style="padding-left: 5px; height: 1px; text-align: left; width: 159px;">
                                                
                                                </td>
                                        </tr>
                                                                                                                        
                                        </table>
                                </td>
                                </tr>
                                <tr>
                                <td style=" padding :2px;" > 
                                   <table cellpadding="0" cellspacing ="0" width="100%">
                                        <tr>
                                            <td align="center" colspan="6" style="padding-right: 5px; text-align: right;">
                                                <strong>Total Filterd Records :&nbsp;<asp:Label ID="EmpCount" runat="server" ></asp:Label>&nbsp;</strong>
                                                <asp:Button ID="btn_Search" runat="server" CausesValidation="true" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="19" />
                                                <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="20" />
                                                <asp:Button ID="btn_add" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_add_Click" Text="Add New" Width="75px" TabIndex="21" />
                                                <asp:Button ID="btn_Print" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Print_Click" Text="Print" Width="75px" TabIndex="19" />
                                                <asp:Button ID="Button1" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Print_Experience_Click" Text="Print Experience" Width="125px" TabIndex="19" />
                                            </td>
                                        </tr>
                                        </table>
                                 </td>
                                 </tr>
                                 <tr>
                                 <td>
                                   <table cellpadding="0" cellspacing ="0" border ="0" width="100%"  >
                                        <tr>
                                            <td colspan="6">
                                                <div style=" width:100%; height:350px; overflow:hidden;" > 
                                                <center>
                                                    <asp:Label ID="lblGrid" runat="Server"></asp:Label>
                                                </center>
                                                <div style="padding:5px 5px 5px 5px;" >
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                                    <colgroup>
                               <col style="width:25px;" />
                               <col style="width:25px;" />
                               <col style="width:60px;" />
<col />                               
<col style="width:180px;" />
                               
                               <col style="width:90px;" />
                               <col style="width:70px;" />
                               <col style="width:100px;" />
                               <col style="width:90px;"/>
                               <col style="width:90px;"/>
                               
                               <col style="width:25px;" />
                                                        </colgroup>
                               <tr align="left" class= "headerstylegrid"> 
                               <td></td>   
                               <td></td>
                               <td><asp:LinkButton runat="server" Text="EMP#" ID="lnkEmp" CommandArgument="EmpCode" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Name" ID="lnkName" CommandArgument="EMPNAME" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Position" ID="lnkPosition" CommandArgument="POSITIONCode" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Nationality" ID="lnkNationality" CommandArgument="CountryName" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Office" ID="lnkOFFICECODE" CommandArgument="OFFICECODE" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Dept." ID="lnkDept" CommandArgument="DeptName" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Dt.Joined" ID="lnkDtJoined" CommandArgument="DJC" OnClick="lnkSort_Click"/></td>
                               <td><asp:LinkButton runat="server" Text="Dt.Resign" ID="lnkDtResigned" CommandArgument="DRC" OnClick="lnkSort_Click"/></td>
                               <td>&nbsp;</td>
                               </tr>
                       </table> 
                       </div>          
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 315px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                               <col style="width:25px;" />
                               <col style="width:25px;" />
                               <col style="width:60px;" />
                               <col />
                               <col style="width:180px;" />
                               <col style="width:90px;" />
                               <col style="width:70px;" />
                               <col style="width:100px;" />
                               <col style="width:90px;"/>
                               <col style="width:90px;"/>
                               <col style="width:25px;" />
                                        </colgroup>
                                   
                                 <asp:Repeater ID="rptData" runat="server" >
                               <ItemTemplate>
                                  <tr class='<%# (Common.CastAsInt32(Eval("EMPID"))==SelectedId)?"selectedrow":"row"%>'>
                                       <td align="center"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("EMPID") %>' OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"/></td>
                                       <td align="center"><asp:ImageButton ID="btnedit" Visible="<%#auth.IsUpdate%>"  runat="server" CommandArgument='<%# Eval("EMPID") %>' OnClick="btnedit_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/edit.jpg"/></td>
                                       <td align="left"><%#Eval("EmpCode")%></td>
                                       <td align="left"><%#Eval("EMPNAME")%></td>
                                       <td align="left"><%#Eval("POSITIONNAME")%></td>
                                       <td align="left"><%#Eval("CountryName")%></td>
                                       
                                        <td align="left"><%#Eval("OFFICECODE")%></td>
                                        <td align="left"><%#Eval("DeptName")%></td>
                                        <td align="center"><%#Eval("DJC")%></td>
                                        <td align="center"><%#Eval("DRC")%></td>
                                           
                                      <td>&nbsp;</td>
                                   </tr>
                               </ItemTemplate>
                              </asp:Repeater>
                                                </table>
                                                </div> 
                                                </div>
                                               </div>
                                            </td>
                                        </tr>
                                    </table>
                                 </td>
                                 </tr>
                                 <tr>
                                 <td>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txt_Age_From"> </ajaxToolkit:FilteredTextBoxExtender>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txt_Age_To"> </ajaxToolkit:FilteredTextBoxExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_djc_from" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_djc_to" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                                    </td>
                                 </tr>
                                 </table>
                            </td>
                        </tr> 
                     </table>      
                       </div>  
                    </td>
               </tr>
         </table>  
         
         <div style="position:absolute;top:50px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvConfirmCancel" visible="false" >
    <center>
    <div style="position:absolute;top:50px;left:0px; height :510px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position :relative; width:500px; height:150px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:150px;  opacity:1;filter:alpha(opacity=100)">
        <center>
   
        <br />
        <b>Please select Office Location and Status for New Employee</b>
        <br />
       <asp:DropDownList ID="ddlnewoffice" runat="server" Width="165px" TabIndex="16">
                                                </asp:DropDownList>
        <br />                                                
            <asp:RangeValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlnewoffice" Display="Dynamic" MinimumValue="1" MaximumValue="5000"  ErrorMessage="Required." ValidationGroup="mnth"></asp:RangeValidator>
        <br />
            <asp:Label runat="server" ForeColor ="Red" ID="lblPopError" ></asp:Label>  
        <br />
        <asp:DropDownList ID="ddlnewstatus" runat="server" Width="165px" TabIndex="16" CausesValidation="false">
                                                </asp:DropDownList>
        <br /> 
        <br />                                                
            <asp:Button ID="btnSet" runat="server" CssClass="btn" onclick="btnAddNewSubmit_Click" Text="Submit" ValidationGroup="mnth" Width="100px" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" onclick="btnAddNewCancel_Click" Text="Cancel" CausesValidation="false" Width="100px" />
        <br/>
        <br/>
    </div> 
    </center>
    </div>
    
    </div>
 </asp:Content>
