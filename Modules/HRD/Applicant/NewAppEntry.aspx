<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewAppEntry.aspx.cs" Inherits="NewAppEntry" Title="New Applicant Details" %>

<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Applicant Details</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <style>
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
        //    function checkNos()
        //    {
        //        var phn= document.getElementById("txtPhone");
        //        var mob= document.getElementById("txtMobile");
        //        if(phn.value=="" && mob.value=="")
        //        {
        //            document.getElementById("lblPnmess").innerHTML="Required.";
        //            //return false ;
        //        }
        //        else
        //        {
        //            document.getElementById("lblPnmess").innerHTML="";
        //        }                               
        //    }
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
      <%--  <asp:UpdatePanel runat="server" ID="upd1">
            <ContentTemplate>--%>
                <div style=" text-align: center; " class="text headerband">
                   New Applicant Details
                </div>
            <div style="padding:5px">
             <asp:Label ID="lbl_info" runat="server" ForeColor="Red" Width="100%" Font-Size="Larger"></asp:Label>
           </div>
                 <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse;font-family:Arial;font-size:12px;">
                    <tr>
                        <td style="width: 490px;">
                            <table cellpadding="2" width="100%" border="0" style="text-align:left;">
 <%--                               <colgroup>
                                    <col width="150px"  />
                                    <col />
                                    <col width="100px" />--%>
                                    <%-- <table id="Table1" border="0" cellpadding="2" cellspacing="0" rules="rows" style="width: 600px; border-collapse: collapse;">--%>
                                    
                                    <tr>
                                        <td>First Name :</td>
                                        <td>
                                            <asp:TextBox ID="txtFName" runat="server" CssClass="input_box" MaxLength="50" Style="background-color: lightyellow;" Width="250px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Middle Name :</td>
                                        <td>
                                            <asp:TextBox ID="txtMName" runat="server" CssClass="input_box" MaxLength="50" Width="250px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Last Name :</td>
                                        <td>
                                            <asp:TextBox ID="txtLName" runat="server" CssClass="input_box" MaxLength="50" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLName" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">DOB :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtDOB" runat="server" CssClass="input_box" MaxLength="15" Style="text-align: center; background-color: lightyellow" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDOB" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left"><span lang="en-us">Passport # :</span></td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtPassportNo" runat="server" CssClass="input_box" MaxLength="50" Style="background-color: lightyellow;" Width="250px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassportNo" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="tr1" runat="server" visible="false">
                                        <td style="text-align: left">Gender :</td>
                                        <td style="text-align: left">
                                            <asp:RadioButtonList ID="radSex" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="Male" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="FeMale" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Rank Applied :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="true" CssClass="input_box" Style="background-color: lightyellow" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CompareValidator ID="cpmval1" runat="server" ControlToValidate="ddlRank" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Available From :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtAvalFrom" runat="server" CssClass="input_box" MaxLength="15" Style="text-align: center;" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAvalFrom" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Nationality :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlNat" runat="server" AutoPostBack="true" CssClass="input_box" Style="background-color: lightyellow" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlNat" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Address :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="input_box" Height="50px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">City :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="input_box" MaxLength="100" Width="250px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Phone # :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="input_box" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Mobile # :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="input_box" Style="background-color: lightyellow" Width="250px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPnmess" runat="server" ForeColor="Red"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">E Mail Address :</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtEMail" runat="server" CssClass="input_box" MaxLength="100" Width="249px" style="background-color: lightyellow" ></asp:TextBox>
                                            
                                        </td>
                                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEMail" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left"><span lang="en-us">Resume :</span></td>
                                        <td style="text-align: left">
                                            <asp:FileUpload ID="FileUpload1" runat="server" Style="background-color: white; border: solid 1px #4371a5; font-size: 10px; width: 252px;" />
                                            <%--<a runat="server" id="ancCV" target="_blank"><img src="../Images/paperclip.gif" style="border : none" /> </a>--%></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: top">Vessel Experience :</td>
                                        <td style="text-align: left">
                                            <asp:CheckBoxList ID="chkVeselType" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr id="trAction" runat="server">
                                        <td colspan="3" style="text-align: left; padding: 5px 0px 5px 0px; padding-left: 175px;">
                                            <asp:Button ID="btnSave" runat="server"   CssClass="btn" Height="25px" OnClick="btnSave_Click" Text=" Save " />
                                        </td>
                                    </tr>
                                <%--</colgroup>--%>
                                </table>
                                </td>
                        <td style="vertical-align: top; padding: 5px;">
                            <table width="100%">
                                <tr runat="server" id="trAction1">
                                    <td>
                                        <div style="text-align: left;">
                                            <asp:Button runat="server" Text="Communication" ID="btnComm" OnClick="btnComm_Click" CssClass="btn11sel" CausesValidation="false" />
                                            <asp:Button runat="server" Text="Attachments" ID="btnAtt" OnClick="btnAtt_Click" CssClass="btn11" CausesValidation="false" />
                                        </div>
                                        <div style="text-align: left; height: 510px;">
                                        <div style="text-align: left;">
                                                <asp:Panel runat="server" ID="pnlComm" Visible="true">
                                                    <table width="100%;" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center; background-color: #f9f9f9">
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <div id="dvscroll_cdpopup" onscroll="SetScrollPos(this)" style="overflow-x: hidden; overflow-y: scroll; height: 475px; padding-left: 15px; padding-right: 15px;">
                                                                    <asp:Repeater runat="server" ID="rptData">
                                                                        <ItemTemplate>
                                                                            <div class='<%#(Eval("TABLEID").ToString().Trim()==SelectedDisc.ToString().Trim())?"selecteddiv":"normaldiv"%>'>
                                                                                <asp:ImageButton runat="server" ID="btnDel" CssClass='<%# Eval("CALLEDBY")%>' CommandArgument='<%# Eval("TABLEID") %>' ImageUrl="~/Modules/HRD/Images/trash.gif" Style="float: right; margin-right: 10px;" OnClientClick="return confirm('Are you sure to remove this record?');" OnClick="rptDel_Click" Visible="false" />
                                                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass='<%# Eval("CALLEDBY")%>' Text='<%# Eval("DISC_DATE_STR") %>' Font-Italic="true" ForeColor="Blue" CommandArgument='<%# Eval("TABLEID") %>' OnClick="rptEdit_Click"></asp:LinkButton>
                                                                                <span style="color: Purple">[ <%# Eval("USERNAME")%> ]</span>
                                                                                <span>
                                                                                    <img src='../Images/icon_phone.jpg' style='display: <%# (Eval("DISCTYPE").ToString()=="P")?"":"none"%>' />
                                                                                    <img src='../Images/icon-email.gif' style='display: <%# (Eval("DISCTYPE").ToString()=="E")?"":"none"%>' />
                                                                                    <img src='../Images/icon_user.gif' style='display: <%# (Eval("DISCTYPE").ToString()=="I")?"":"none"%>' />
                                                                                </span>
                                                                                <%# Eval("DISC")%>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                      <div style="padding: 5px; text-align: right">
                                                            <asp:Button Text=" + Add" OnClick="btnAddComm_Click" CssClass="btn" Height="25px" runat="server" CausesValidation="false" />
                                                        </div>
                                                </asp:Panel>

                                                <asp:Panel runat="server" ID="pnlAtt" Visible="false">


                                                    <div style="overflow-x: hidden; overflow-y: hidden; background-color: #0094ff">
                                                <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; height: 30px; border-collapse: collapse;" class="bordered">
                                                    <colgroup>
                                                        <col width="40px" />
                                                        <col />
                                                        <col width="60px" />
                                                        <tr style="font-weight: bold; text-align: left; color: white;">
                                                            <td>File</td>
                                                            <td>Description </td>
                                                            <td>Delete </td>
                                                        </tr>
                                                    </colgroup>
                                                </table>
                                            </div>

                                                   <div style="height: 450px; overflow-x: hidden; overflow-y: scroll; background-color:white; border:solid 1px #c2c2c2">
                                                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                                                        <colgroup>
                                                            <col width="40px" />
                                                            <col />
                                                            <col width="60px" />
                                                        </colgroup>
                                                            <asp:Repeater ID="rptCommAtt" runat="server">
                                                                <ItemTemplate>
                                                                    <tr style="text-align: left;">
                                                                        <td style="text-align: center;">
                                                                            <%--<asp:ImageButton ID="btnCommFile" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btnDownloadClick" CommandArgument='<%#Eval("TableID")%>' ToolTip='<%#Eval("FileName") %>' />  --%>
                                                                            <a onclick="DownLoadFile(<%#Eval("TableID")%>,'<%#Eval("FileName")%>')" style="cursor: pointer;">
                                                                                <img src="../Images/paperclip.gif" title=" Download " />
                                                                            </a>
                                                                        </td>
                                                                        <td><%#Eval("Descr")%> </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnDeleteCommFile" runat="server" ImageUrl="~/Modules/HRD/Images/trash.gif" OnClientClick="return confirm('Are you sure to delete this file?');" OnClick="btnDeleteCommFile_OnClick" CommandArgument='<%#Eval("TableID")%>' Style="cursor: pointer;" ToolTip="Delete" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </div>
                                                    <asp:ImageButton ID="btnDownLoadFile" runat="server" OnClick="btnDownLoadFile_OnClick" Style="display: none;" />
                                                    <asp:HiddenField ID="hfTableID" runat="server" Value="" />
                                                    <asp:HiddenField ID="hfFileName" runat="server" Value="" />
                                                      <div style="padding: 5px; text-align: right;height:23px;">
                                    <asp:Button Text=" + Add" OnClick="btnAddAtt_Click" ID="btnAddAtt" CssClass="btn"  Height="25px" runat="server" CausesValidation="false" />
                                </div>
                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                </tr>
                </table>

                   <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddComm" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 180px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 10px #E0E0FF;">
    

                <table border="0" cellspacing="0" cellpadding="2" width="100%" style="font-family:Arial;font-size:12px;">
                                                                    <tr>
                                                                        <td style="vertical-align: top">Date</td>
                                                                        <td style="vertical-align: top; text-align: center" valign="top">Details</td>
                                                                        <td style="text-align: left;" rowspan="2">
                                                                            <asp:RadioButtonList ID="radCommType" runat="server" RepeatDirection="Vertical">
                                                                                <asp:ListItem Text="Phone" Value="P"></asp:ListItem>
                                                                                <asp:ListItem Text="EMail" Value="E"></asp:ListItem>
                                                                                <asp:ListItem Text="InPerson" Value="I"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <div style="width: 150px; text-align: left">
                                                                                &nbsp;&nbsp;
                           <%-- <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                                OnClick="btnAdd_Click" Style="height: 16px"
                                ToolTip="Add/Udpate this conversation." ValidationGroup="det" />&nbsp;--%>
                            <asp:ImageButton ID="btnClear" runat="server" CausesValidation="false"
                                ImageUrl="~/Modules/HRD/Images/clear.png" OnClick="btnClear_Click" Style="height: 16px"
                                ToolTip="Clear Text." />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top">
                                                                            <asp:TextBox ID="txtConDate" runat="server" CssClass="input_box" MaxLength="15"
                                                                                Style="text-align: center" Width="80px" AutoPostBack="true" OnTextChanged="Validate"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                ControlToValidate="txtConDate" Display="Static" ErrorMessage="*"
                                                                                ValidationGroup="det"></asp:RequiredFieldValidator>
                                                                            <br />
                                                                            <asp:Label ForeColor="Red" Text="Invalid date." runat="server" ID="lblDateMess"></asp:Label>
                                                                        </td>
                                                                        <td style="vertical-align: top; text-align: center;" valign="top">
                                                                            <asp:TextBox ID="txtCon" runat="server" CssClass="input_box" Height="75px" TextMode="MultiLine"
                                                                                Width="570px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                ControlToValidate="txtCon" Display="Static" ErrorMessage="*"
                                                                                ValidationGroup="det"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                     <div style="padding: 2px;">
                        <asp:Button ID="Button3" runat="server" CssClass="btn" Height="25px" OnClick="btnAdd_Click" Text=" Save " ValidationGroup="det" />
                        <asp:Button ID="Button4" runat="server" CssClass="btn" Height="25px" OnClick="btnClose2_OnClick" Text=" Close " CausesValidation="false" />
                    </div>
                    </div>
                    </center>
                       
                       </div>


                   <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddAttachment" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 100px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 10px #E0E0FF;">

                                                    <table id="tblAttachFiles" runat="server" cellpadding="1" cellspacing="3" border="0" style="width: 100%; border: solid 1px gray; padding: 1px; width: 100%;">
                                                        <tr>
                                                            <td colspan="4"><b>Attach Files Here :-</b> </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtAttDesc" runat="server" Width="350px" CssClass="input_box"  ValidationGroup="v001"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fuCommAttachment" runat="server" Width="350px" CssClass="input_box"   ValidationGroup="v001"/>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnAddCommAtt" runat="server" Height="25px" Text=" Add " OnClick="btnAddCommAtt_OnClick" CssClass="btn" CausesValidation="false" />
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                    <div style="padding: 2px;">
                        <asp:Button ID="Button1" runat="server"  OnClick="btnAddCommAtt_OnClick" CssClass="btn" Height="25px" Text=" Save " CausesValidation="true" ValidationGroup="v001"/>
                        <asp:Button ID="Button2" runat="server" CssClass="btn" Height="25px" OnClick="btnClose1_OnClick" Text=" Close " CausesValidation="false" />
                    </div>
                     </div>
                    </center>
                       </div>

                <%--//-------------------------------%>




                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtAvalFrom" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtDOB" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtConDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
          <%--  </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnAddCommAtt"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnDownLoadFile"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnAddCommAtt"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnDownLoadFile"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnAddCommAtt"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnDownLoadFile"></asp:PostBackTrigger>
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddCommAtt" />
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDownLoadFile" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </form>
</body>
</html>
