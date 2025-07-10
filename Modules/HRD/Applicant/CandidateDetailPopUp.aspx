<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CandidateDetailPopUp.aspx.cs" Inherits="CandidateDetailPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Applicant Details</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <style type="text/css">
        .selecteddiv {
            width: 100%;
            padding: 5px;
            padding-bottom: 5px;
            background-color: wheat;
            border: dotted 1px #4371a5;
        }

        .normaldiv {
            width: 100%;
            padding: 5px;
            padding-bottom: 5px;
            background-color: none;
        }

        .rem {
            border: solid 1px #c2c2c2;
            width: 650px;
        }

            .rem:focus {
                background-color: #FFFFCC;
            }

        .c2 + div:after {
            content: "*";
            font-size: small;
            color: red;
        }


        .newbtn {
            border: solid 1px #c2c2c2;
            background-color: Orange;
            padding: 5px;
            width: 100px;
            margin-top: 2px;
            height:30px;
            margin-bottom:5px;
        }

        .c1 {
            width: 400px;
            float: left;
        }

        .c2 {
            width: 80px;
            float: left;
        }
    </style>
    <script type="text/javascript">
        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }
        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }
        function SetLastFocus(ctlid) {
            pos = getCookie(ctlid);
            if (isNaN(pos))
            { pos = 0; }
            if (pos > 0) {
                document.getElementById(ctlid).scrollTop = pos;
            }
        }
        function SetScrollPos(ctl) {
            setCookie(ctl.id, ctl.scrollTop, 1);
        }

        function DownLoadFile(TableID, FileName) {

            document.getElementById('hfTableID').value = TableID;
            document.getElementById('hfFileName').value = FileName;

            document.getElementById('btnDownLoadFile').click();
        }

        function OpenPrint(CandidateID) {
            window.open('SendMail.aspx?CandidateID=' + CandidateID, null, 'title=no,toolbars=no,scrollbars=yes,width=1000,height=650,left=20,top=20,addressbar=no');
        }

    </script>
    <style type="text/css">
        .btn11sel {
            font-size: 14px;
            background-color: #808080;
            border-top: solid 1px black;
            border-right: solid 1px black;
            border-left: solid 1px black;
            border-bottom: solid 1px #4371a5;
            padding: 5px;
            height: auto;
            color: White;
        }

        .btn11 {
            font-size: 14px;
            background-color: #e2e2e2;
            border-top: solid 1px black;
            border-right: solid 1px black;
            border-left: solid 1px black;
            border-bottom: solid 1px #c2c2c2;
            padding: 5px;
            height: auto;
        }

        input {
            padding: 2px;
            height: 16px;
        }

        .mybutton {
            background-color: #0094ff;
            color: white;
            padding: 5px 10px 5px 10px;
            border: none;
            height: auto;
        }

        select {
            padding: 2px;
            height: 23px;
            line-height: 23px;
        }

        .bordered tr td {
            border: solid 1px #e5e5e5;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

        <div style="padding: 5px; font-size: 15px; text-align: center; font-weight: bold;  color: white;" class="text headerband">
            Applicant Details -  <%=candidateid.ToString()%>
        </div>
        <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse;">
            <tr>
                <td style="width: 30%;">
                    <table width="100%" cellpadding="2" border="0">

                        <tr>
                            <td style="text-align: left">First Name :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtFName" runat="server" CssClass="input_box" MaxLength="50" placehoder="First Name" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td style="text-align: left">Middle Name :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtMName" runat="server" CssClass="input_box" MaxLength="50" placehoder="Middle Name" Width="250px"></asp:TextBox>
                            </td>
                            <td style="text-align: left"> </td>

                        </tr>
                        <tr>
                            <td style="text-align: left">Last Name :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtLName" runat="server" CssClass="input_box" MaxLength="50" placehoder="Last Name" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                                &nbsp;</td>
                            <td style="text-align: left">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLName" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td style="text-align: left">DOB :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="input_box" MaxLength="15" Style="text-align: center; background-color: lightyellow" Width="80px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDOB" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td style="text-align: left"><span lang="en-us">Passport # :</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtPassportNo" runat="server" CssClass="input_box" MaxLength="50" Width="250px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassportNo" Display="Dynamic" Enabled="false" ErrorMessage="Required."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Rank Applied :</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="true" CssClass="input_box" Style="background-color: lightyellow" Width="255px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                <asp:CompareValidator ID="cpmval1" runat="server" ControlToValidate="ddlRank" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Available From :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtAvalFrom" runat="server" CssClass="input_box" MaxLength="15" Style="text-align: center" Width="80px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAvalFrom" Display="Dynamic" Enabled="false" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Nationality :</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlNat" runat="server" AutoPostBack="true" CssClass="input_box" Style="background-color: lightyellow" Width="255px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlNat" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Address :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="input_box" Height="50px" TextMode="MultiLine" Width="255px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">City :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="input_box" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Phone # :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="input_box" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                                &nbsp;</td>
                            <td style="text-align: left"> </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Mobile # :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtMobile" runat="server" CssClass="input_box" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                                &nbsp;</td>
                            <td style="text-align: left">
                                                            <asp:Label ID="lblPnmess" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">E Mail Address :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtEMail" runat="server" CssClass="input_box" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEMail" Display="Dynamic" Enabled="false" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: left"><span lang="en-us">Resume :</span></td>
                            <td style="text-align: left">
                                <asp:FileUpload ID="FileUpload1" runat="server" Style="background-color: white; border: solid 1px #4371a5; font-size: 10px; width: 252px;" Width="250px" />
                                <a id="ancCV" runat="server" target="_blank">
                                    <img src="../Images/paperclip.gif" style="border: none" />
                                </a>
                            </td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Vessel Experience :</td>
                            <td style="text-align: left">
                                <asp:CheckBoxList ID="chkVeselType" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal">
                                </asp:CheckBoxList></td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Created By/On : </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblUpdatedByOn" runat="server"></asp:Label></td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr id="tr_Comm" runat="server">
                            <td style="text-align: left">Req. Comments :</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtComm" runat="server" Height="50px" ReadOnly="True" Style="border: solid 1px gray" TextMode="MultiLine" Width="255px"></asp:TextBox></td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                    </table>

 <div style="padding: 10px;">
  <asp:Button ID="btnEdit" runat="server" CssClass="btn" Height="25px" OnClick="btnEdit_Click" Text=" Edit " />
  <asp:Button ID="btnSave" runat="server" CssClass="btn" Height="25px" OnClick="btnSave_Click" Text=" Save " Visible="false" />
  
 </div>
                </td>
                <td style="vertical-align: top; padding: 5px;width:40%;">
                    <div style="text-align: left;" runat="server"  visible="false">
                        <asp:Button ID="btnComm" runat="server" CssClass="btn11sel" OnClick="btnComm_Click" Text="Communication" />
                        <asp:Button ID="btnAtt" runat="server" CssClass="btn11" OnClick="btnAtt_Click" Text="Attachments" />
                        <asp:Button ID="btnHist" runat="server" CssClass="btn11" OnClick="btnHist_Click" Text="History" />
                    </div>
                    <div style="text-align: left;">
                        
                        <div style="text-align: left;">
                        <div style="padding:5px; font-size:larger">
                            <b style="position:relative;top:-5px;">Communication with crew</b>
                            <asp:ImageButton Text=" + Add" OnClick="btnAddComm_Click" ImageUrl="~/Modules/HRD/Images/add_16.gif"  runat="server" CausesValidation="false" />
                        </div>
                        
                            <asp:Panel ID="pnlComm" runat="server" Visible="true">
                                <div id="dvscroll_cdpopup">
                                    <asp:Repeater ID="rptData" runat="server">
                                        <ItemTemplate>
                                            <div class='<%#(Eval("TABLEID").ToString().Trim()==SelectedDisc.ToString().Trim())?"selecteddiv":"normaldiv"%>'>
                                                <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%# Eval("TABLEID") %>' CssClass='<%# Eval("CALLEDBY")%>' ImageUrl="~/Modules/HRD/Images/trash.gif" OnClick="rptDel_Click" OnClientClick="return confirm('Are you sure to remove this record?');" Style="float: right; margin-right: 10px;" Visible="false" />
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("TABLEID") %>' CssClass='<%# Eval("CALLEDBY")%>' Font-Italic="true" ForeColor="Blue" OnClick="rptEdit_Click" Text='<%# Eval("DISC_DATE_STR") %>'></asp:LinkButton>
                                                <span style="color: Purple">[ <%# Eval("USERNAME")%>]</span> <span>
                                                    <img src='../Images/icon_phone.jpg' style='display: <%# (Eval("DISCTYPE").ToString()=="P")?"":"none"%>' />
                                                    <img src='../Images/icon-email.gif' style='display: <%# (Eval("DISCTYPE").ToString()=="E")?"":"none"%>' />
                                                    <img src='../Images/icon_user.gif' style='display: <%# (Eval("DISCTYPE").ToString()=="I")?"":"none"%>' />
                                                </span><%# Eval("DISC")%>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div style="padding: 5px; text-align: right">
                                    
                                </div>
                            </asp:Panel>
                            <div style="padding:5px; font-size:larger">
                            <b style="position:relative;top:-5px;">Attachment / Extra Documents</b>
                                <asp:ImageButton Text=" + Add" OnClick="btnAddAtt_Click" ID="btnAddAtt" ImageUrl="~/Modules/HRD/Images/add_16.gif"  runat="server" CausesValidation="false" />
                            </div>
                            <asp:Panel ID="pnlAtt" runat="server" Visible="true">
                                <div style="">
                                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; height: 30px; border-collapse: collapse;" class="bordered">
                                        <colgroup>
                                            <col width="40px" />
                                            <col />
                                            <col width="60px" />
                                            <tr style="font-weight: bold; text-align: left; color:#333">
                                                <td>File</td>
                                                <td>Description </td>
                                                <td>Delete </td>
                                            </tr>
                                        </colgroup>
                                    </table>
                                </div>
                               
                                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                                        <colgroup>
                                            <col width="40px" />
                                            <col />
                                            <col width="60px" />
                                        </colgroup>
                                        <asp:Repeater ID="rptCommAtt" runat="server">
                                            <ItemTemplate>
                                                <tr style="text-align: left;">
                                                    <td style="text-align: center;"><%--<asp:ImageButton ID="btnCommFile" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btnDownloadClick" CommandArgument='<%#Eval("TableID")%>' ToolTip='<%#Eval("FileName") %>' />  --%><a onclick='DownLoadFile(<%#Eval("TableID")%>,&#039;<%#Eval("FileName")%>&#039;)' style="cursor: pointer;">
                                                        <img src="../Images/paperclip.gif" title=" Download " />
                                                    </a></td>
                                                    <td><%#Eval("Descr")%></td>
                                                    <td style="text-align: center;">
                                                        <asp:ImageButton ID="btnDeleteCommFile" runat="server" CommandArgument='<%#Eval("TableID")%>' ImageUrl="~/Modules/HRD/Images/trash.gif" OnClick="btnDeleteCommFile_OnClick" OnClientClick="return confirm('Are you sure to delete this file?');" Style="cursor: pointer;" ToolTip="Delete" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                <asp:ImageButton ID="btnDownLoadFile" runat="server" OnClick="btnDownLoadFile_OnClick" Style="display: none;" />
                                <asp:HiddenField ID="hfTableID" runat="server" Value="" />
                                <asp:HiddenField ID="hfFileName" runat="server" Value="" />
                                
                            </asp:Panel>
                        </div>
                    </div>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtAvalFrom">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDOB">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtConDate">
                    </ajaxToolkit:CalendarExtender>
                    
                </td>
                <td style="vertical-align: top; padding: 5px; width:30%; text-align:left;">
                        <asp:Panel ID="pnlHist" runat="server" Visible="false">
                               
                            </asp:Panel>
                             <div style="color: Black; padding: 5px;">
                                 <span id="lblActivity" runat="server" style="font-size: 13px;" visible="false"></span>
                                <div  style='padding:5px;line-height:20px;'> <b> Application Received On :</b> 
                                    <asp:Label ID="lblAppRecivedOn" runat="server"></asp:Label> 

                                </div>
                                 <div id="divMain" runat="server" visible="false"   style='padding:5px;line-height:20px;font-size:12px;'>
                                     <span style="color:red;"> <b> *Check the applicant CV.* </b> <br />
                                         A - If Applicant is suitable for employment:
                                         <br />
                                         <asp:Button ID="btnReqForApprove" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnRApprove_Click" Text="Submit for approval" /> 
                                         <br />
                                         B - If Applicant is not suitable for employment:
                                         <br />
                                         <asp:Button ID="btnArchive" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnArchive_Click" OnClientClick="return confirm('Are you sure to archive this applicant?');" Text="Archive" Visible="false" />
                                     </span>  <br />
                                      
                                 </div>
                                 
                                 <div id="div1" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'> <b> 
                                    <%-- Requested for App. By/On--%> Approval Requested By/On :

                                                                                                                       </b> 
                                    <asp:Label ID="lblReqforAppBy" runat="server"></asp:Label> <br />
                                     <div style="vertical-align:top;"> 
                                         <b>Remarks :  </b> <asp:TextBox ID="txtApprovalRemarks" runat="server" ReadOnly="true" Width="200px"  ToolTip="" Height="50px" Style="border: solid 1px gray" TextMode="MultiLine" ></asp:TextBox> <br />
                                     <b> Status : </b> <asp:Label ID="lblStatus" runat="server"></asp:Label> <br />
                                     </div>
                                 </div>
                                 <div id="divAppRej" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'>
                                     <asp:Button ID="btnApprove" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnApprove_Click" Text="Approve" Visible="false" />
                        <asp:Button ID="btnReject" runat="server" CssClass="btn" OnClick="btnReject_Click" Height="25px" OnClientClick="confirm('Are you sure to reject this application?');" Text=" Reject " Visible="false" />
                                 </div>
                                  <div id="div2" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'> <b> Approved By/On : </b> 
                                    <asp:Label ID="lblApprovedBy" runat="server"></asp:Label> <br />
                                      <div style='color:#FF0066;font-size:15px;margin-top:10px;'>
                                          Approval No : <asp:Label ID="lblApprovalId" runat="server" ></asp:Label>
                                      </div>
                                        
                                </div>
                                 
                                 <div id="div3" runat="server" visible="false" style='padding:5px;line-height:20px;font-size:12px;'> <b> Rejected By/On : </b> 
                                     <asp:Label ID="lblRejBy" runat="server"></asp:Label>
                                     </div> 
                                  <div id="div4" runat="server" visible="false" style='padding:5px;line-height:20px;font-size:12px;'> <b> Archived By/On : </b> 
                                     <asp:Label ID="lblArchivedby" runat="server"></asp:Label>
                                 </div>
                                 <div id="divnotify" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'>
                                     <span runat="server" visible="true">
                                        <asp:Button ID="btnNotify" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnNotify_Click" Text="Notify" Visible="false" />
                                            </span>
                                 </div>
                             </div>
                </td>
            </tr>
            <tr>
                <asp:Label ID="lblMessage" runat="server" Style="font-weight: bold; color: Red;" Font-Size="12px"></asp:Label>
            </tr>

        </table>
        <center>
            <table border="0" id="trAction" runat="server">
                <tr>
                    <td style="padding: 5px;">
                       
                        
                        <asp:Button ID="btnDocChk" runat="server" CausesValidation="false" CssClass="btn" Height="25px"  OnClick="btnDocCheck_Click" Text="Update Document Checklist" Visible="false" />
                       
                        
                        
                        <asp:Button ID="btnViewDocChk" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnViewDocCheck_Click" Text="View Checklist" Visible="false" />
                       
                       
                        <%--<asp:Button runat="server" ID="btnTransfer" Text="Transfer to CREW-DB" CssClass="text" onclick="btnTransfer_Click" CausesValidation="false" BackColor="#4371a5" ForeColor="White" BorderColor ="#4371a5" BorderWidth="2px"  Height="20px"  Visible="false" /> --%>
                        
                    </td>
                </tr>

            </table>

        </center>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddAttachment" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 100px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 10px #E0E0FF;">

                    <table border="0" cellpadding="1" cellspacing="3" style="width: 100%; width: 100%;">
                        <tr>
                            <td colspan="4"><b>Attach Files </b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAttDesc" runat="server" CssClass="input_box" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:FileUpload ID="fuCommAttachment" runat="server" CssClass="input_box" Width="350px" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>

                    <div style="padding: 10px;">
                        <asp:Button ID="btnAddCommAtt" runat="server" CssClass="btn" Height="25px" OnClick="btnAddCommAtt_OnClick" Text=" Save " />
                        <asp:Button ID="Button1" runat="server" CssClass="btn" Height="25px" OnClick="btnClose1_OnClick" Text=" Close " />
                    </div>
                </div>
            </center>
        </div>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddComm" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 180px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 10px #E0E0FF;">
                    <asp:Label ID="lbl_info" runat="server" ForeColor="Red" Width="100%"></asp:Label>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr>
                            <td style="vertical-align: top">Date</td>
                            <td style="vertical-align: top; text-align: center" valign="top">Details</td>
                            <td rowspan="2" style="text-align: left;">
                                <asp:RadioButtonList ID="radCommType" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem Text="Phone" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="EMail" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="InPerson" Value="I"></asp:ListItem>
                                </asp:RadioButtonList>
                                <div style="width: 150px; text-align: left">
                                    &nbsp;&nbsp;
                            <%--<asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif" onclick="" style="height: 16px" ToolTip="Add/Udpate this conversation."  />--%>
                            &nbsp;
                            <asp:ImageButton ID="btnClear" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/clear.png" OnClick="btnClear_Click" Style="height: 16px" ToolTip="Clear Text." />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <asp:TextBox ID="txtConDate" runat="server" AutoPostBack="true" CssClass="input_box" MaxLength="15" OnTextChanged="Validate" Style="text-align: center" Width="80px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConDate" Display="Static" ErrorMessage="*" ValidationGroup="det"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblDateMess" runat="server" ForeColor="Red" Text="Invalid date."></asp:Label>
                            </td>
                            <td style="vertical-align: top; text-align: center;" valign="top">
                                <asp:TextBox ID="txtCon" runat="server" CssClass="input_box" Height="75px" TextMode="MultiLine" Width="570px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCon" Display="Static" ErrorMessage="*" ValidationGroup="det"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>


                    <div style="padding: 10px;">
                        <asp:Button ID="Button2" runat="server" CssClass="btn" OnClick="btnAdd_Click" Text=" Save " ValidationGroup="det" />
                        <asp:Button ID="Button3" runat="server" CssClass="btn" OnClick="btnClose2_OnClick" Text=" Close " />
                    </div>
                </div>
            </center>
        </div>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvComments" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 4100%50px; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 180px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px;">
                    <div style="border: solid 10px #E0E0FF;">
                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                            <tr>
                                <td style="text-align: left; font-weight: bold;">Enter your comments here :-</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtComments" runat="server" Style="width: 98%; height: 100px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSaveComments" runat="server" CssClass="btn" Height="25px" Text=" Save " OnClick="btnSaveComments_OnClick" />
                                    <asp:Button ID="btnCloseSaveComments" runat="server" CssClass="btn" Height="25px" Text=" Close " OnClick="btnCloseSaveComments_OnClick" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCommentsMsg" runat="server" Style="font-weight: bold; color: Red;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </center>
        </div>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvDocCheckList" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 90%; padding: 0px; text-align: center; background: white; z-index: 150; top: 50px;">
                
                    <div style="border: solid 10px #E0E0FF;">
                    
                        <div style=' font-weight: bold; color: White;  ' class="text headerband">Document Check List</div>
                        <table width="100%" style="background-color:#c5c5c5;" cellpadding="4">
                        <tr>
                        <td class='c1'>Document Name</td>
                        <td class='c2'>Checked</td>
                        <td>Remarks</td>
                        </tr>
                        </table>
                        
                        <div style="text-align: left; height: 505px; overflow-x: hidden; overflow-y: scroll; border: solid 1px #c2c2c2">
                            <div style="font-size: 12px; color: Purple; font-family: Verdana; padding: 2px; background-color: #EDEDF5">&nbsp;Licenses</div>
                            <asp:Repeater runat="server" ID="rptL">
                                <ItemTemplate>
                                    <div style="text-align: left; width: 100%; text-align: left; padding: 2px;">
                                        <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                                        <div class='c2'>
                                            <asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                                        <div>
                                            <asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="font-size: 12px; color: Purple; font-family: Verdana; padding: 2px; background-color: #EDEDF5">&nbsp;Course & Certificates</div>
                            <asp:Repeater runat="server" ID="rptC">
                                <ItemTemplate>
                                    <div style="text-align: left; width: 100%; text-align: left; padding: 2px;">
                                        <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                                        <div class='c2'>
                                            <asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                                        <div>
                                            <asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="font-size: 12px; color: Purple; font-family: Verdana; padding: 2px; background-color: #EDEDF5">&nbsp;Endorsements</div>
                            <asp:Repeater runat="server" ID="rptE">
                                <ItemTemplate>
                                    <div style="text-align: left; width: 100%; text-align: left; padding: 2px;">
                                        <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                                        <div class='c2'>
                                            <asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                                        <div>
                                            <asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="font-size: 12px; color: Purple; font-family: Verdana; padding: 2px; background-color: #EDEDF5">&nbsp;Travel Documents</div>
                            <asp:Repeater runat="server" ID="rptT">
                                <ItemTemplate>
                                    <div style="text-align: left; width: 100%; text-align: left; padding: 2px;">
                                        <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                                        <div class='c2'>
                                            <asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                                        <div>
                                            <asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="font-size: 12px; color: Purple; font-family: Verdana; padding: 2px; background-color: #EDEDF5">&nbsp;Medical Documents</div>
                            <asp:Repeater runat="server" ID="rptM">
                                <ItemTemplate>
                                    <div style="text-align: left; width: 100%; text-align: left; padding: 2px;">
                                        <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                                        <div class='c2'>
                                            <asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                                        <div>
                                            <asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div style="font-size: 12px; color: red; font-family: Verdana; padding: 2px; background-color: #Dd2d2d2;">* -  Remarks are mandatory if not checked.</div>
                        <div style="text-align: right; padding-right: 2px;">
                            <asp:Label runat="server" ID="lblMess" ForeColor="Red" Style="float: left" Font-Size="14px"></asp:Label>
                            <asp:Button ID="btnSaveCheckList" runat="server" CssClass="btn" Text=" Save " Height="25px" OnClick="btnSaveCC_OnClick" />
                            <asp:Button ID="btnCancelChkList" runat="server" CssClass="btn" Text=" Close " Height="25px" OnClick="btnCloseCC_OnClick" />
                        </div>
                    </div>
                </div>
            </center>
        </div>


    </form>
</body>
</html>
