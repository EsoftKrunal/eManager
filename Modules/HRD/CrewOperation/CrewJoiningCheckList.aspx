<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewJoiningCheckList.aspx.cs" Inherits="CrewOperation_CrewJoiningCheckList" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title></title>
 <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>

<body>
    <form id="form1" runat="server">

<table cellpadding="0" cellspacing="0" width="100%">
     <tr>
      <td style="padding-right: 0px; text-align:center;  padding-left: 0px; padding-bottom: 0px; padding-top: 0px;" class="textregisters" colspan="2" >
            <strong> Crew</strong> Joining CheckList</td>
            
     </tr>
     <tr><td style="text-align: center">
         <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000" Visible="False">Record Successfully Saved.</asp:Label>&nbsp;</td></tr>
         <tr><td style="padding-left:10px; padding-right:10px">
         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;" class="">
                <legend><strong>Joining CheckList</strong></legend>
             <table cellpadding="2" cellspacing="0" width="100%">
                 <tr>
                     <td style="text-align: right">
                         Name :</td>
                     <td style="height: 13px; text-align: left">
                         <asp:Label ID="lbl_Name" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                         Rank :</td>
                     <td style="text-align: left">
                         <asp:Label ID="lbl_Rank" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                         Nationality :</td>
                     <td style="text-align: left">
                         <asp:Label ID="lbl_Nationality" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="height: 10px; text-align: right">
                         Date Of Birth :</td>
                     <td style="height: 10px; text-align: left">
                         <asp:Label ID="lbl_DOB" runat="server"></asp:Label></td>
                     <td style="height: 10px; text-align: right">
                         Place Of Birth :</td>
                     <td style="height: 10px; text-align: left">
                         <asp:Label ID="lbl_POB" runat="server"></asp:Label></td>
                     <td style="height: 10px; text-align: right">
                         Name Of Vessel :</td>
                     <td style="height: 10px; text-align: left">
                         <asp:Label ID="lbl_Vessel" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="text-align: right">
                         Date Of Joining :</td>
                     <td style="height: 13px; text-align: left">
                         <asp:Label ID="lbl_DOJ" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                     </td>
                     <td style="text-align: left">
                     </td>
                     <td style="text-align: right">
                     </td>
                     <td style="text-align: left">
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align: right">
                         Passport No :</td>
                     <td style="height: 13px; text-align: left">
                         <asp:Label ID="lbl_PPTNo" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                         Issue Date :</td>
                     <td style="text-align: left">
                         <asp:Label ID="lbl_PPT_IssueDate" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                         Place Of Issue :</td>
                     <td style="text-align: left">
                         <asp:Label ID="lbl_PPT_IssuePlace" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="text-align: right">
                         Expiry Date :</td>
                     <td style="height: 13px; text-align: left">
                         <asp:Label ID="lbl_PPT_ExpDate" runat="server"></asp:Label></td>
                     <td style="text-align: right">
                     </td>
                     <td style="text-align: left">
                     </td>
                     <td style="text-align: right">
                     </td>
                     <td style="text-align: left">
                     </td>
                 </tr>
                 <tr>
                     <td colspan="3" style="text-align: right">
                         Training &amp; Familiarization record Book Number :</td>
                     <td style="text-align: left">
                         <asp:TextBox ID="txt_bookno" runat="server" CssClass="input_box" MaxLength="5" Width="79px"></asp:TextBox></td>
                     <td style="text-align: right">
                         English Test:</td>
                     <td style="text-align: left">
                         <asp:DropDownList ID="ddlenglishtest" runat="server" CssClass="input_box">
                             <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                             <asp:ListItem Value="1">Poor</asp:ListItem>
                             <asp:ListItem Value="2">Fair</asp:ListItem>
                             <asp:ListItem Value="3">Good</asp:ListItem>
                             <asp:ListItem Value="4">Very Good</asp:ListItem>
                         </asp:DropDownList></td>
                 </tr>
                 <tr>
                     <td style="text-align: right">
                         Date Issue :</td>
                     <td style="height: 13px; text-align: left">
                         <asp:TextBox ID="txt_dateofIssue" runat="server" CssClass="input_box" MaxLength="10" Width="79px"></asp:TextBox><asp:CompareValidator
                             ID="CompareValidator29" runat="server" ControlToValidate="txt_dateofIssue" ErrorMessage="Invalid Date"
                             Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
                     <td style="text-align: right">
                         Place Issue :</td>
                     <td style="text-align: left">
                         <asp:TextBox ID="txt_pob" runat="server" CssClass="input_box" MaxLength="5" Width="79px"></asp:TextBox></td>
                     <td style="text-align: right">
                     </td>
                     <td style="text-align: left">
                     </td>
                 </tr>
             </table>
             </fieldset>
         </td></tr>
    <tr>
        <td align="right" style="padding-right: 10px; padding-bottom: 10px; padding-top: 10px">
            <table cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td valign="top">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="1" style="height: 13px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="1" style="height: 13px">
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Seaman Book :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Medical :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Drug&nbsp; &amp; Alcohal&nbsp; Tests:</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Certificate Of&nbsp; Complences :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    D.O.E(Chemical) :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    D.O.E(Oil) :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    D.O.E(Gas):</td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2" rowspan="1" style="text-align: center" valign="top">
                        <table cellpadding="0" cellspacing="0" width="100%" >
                            <tr>
                                <td colspan="2">
                                    <strong>National</strong></td>
                            </tr>
                            <tr>
                                <td style="height: 13px;text-align: center;">
                                    <strong>Number</strong></td>
                                <td style="height: 13px;">
                                    <strong>Date-Expiry</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="nat1" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td >
                                    <asp:TextBox ID="natdate1" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ControlToValidate="natdate1" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="nat2" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="">
                                    <asp:TextBox ID="natdate2" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator2" runat="server" ControlToValidate="natdate2" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="nat3" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="natdate3" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator3" runat="server" ControlToValidate="natdate3" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="nat4" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="natdate4" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator4" runat="server" ControlToValidate="natdate4" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="">
                                    <asp:TextBox ID="nat5" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="natdate5" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator5" runat="server" ControlToValidate="natdate5" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="">
                                    <asp:TextBox ID="nat6" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="">
                                    <asp:TextBox ID="natdate6" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator6" runat="server" ControlToValidate="natdate6" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="nat7" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="">
                                    <asp:TextBox ID="natdate7" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator7" runat="server" ControlToValidate="natdate7" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2" rowspan="1" style="text-align: center" valign="top">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="2">
                                    <strong>
                                    Liberia</strong></td>
                            </tr>
                            <tr>
                                <td style="height: 13px">
                                    <strong>Number</strong></td>
                                <td style="height: 13px">
                                    <strong>Date-Expiry</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Lib1" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="Libdate1" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator8" runat="server" ControlToValidate="Libdate1" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Lib2" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="Libdate2" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator9" runat="server" ControlToValidate="Libdate2" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Lib3" runat="server" CssClass="input_box" MaxLength="5" Width="79px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="Libdate3" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator10" runat="server" ControlToValidate="Libdate3" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Lib4" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="Libdate4" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator11" runat="server" ControlToValidate="Libdate4" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Lib5" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="Libdate5" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator12" runat="server" ControlToValidate="Libdate5" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Lib6" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="Libdate6" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator13" runat="server" ControlToValidate="Libdate6" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    <asp:TextBox ID="Lib7" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                                <td style="height: 19px">
                                    <asp:TextBox ID="Libdate7" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                        ID="CompareValidator14" runat="server" ControlToValidate="Libdate7" ErrorMessage="Invalid Date"
                                        Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                    <td valign="top">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="1" style="height: 13px">
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="height: 13px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Seaman Book :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Medical :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Drug&nbsp; &amp; Alcohal&nbsp; Tests:</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    Certificate Of&nbsp; Complences :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    D.O.E(Chemical) :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    D.O.E(Oil) :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px">
                                    D.O.E(Gas):</td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="1" style="text-align: center" valign="top" colspan="2">
                    <table cellpadding="0" cellspacing="0" width="100%" >
                        <tr>
                            <td colspan="2">
                                <strong>
                                Panama</strong></td>
                        </tr>
                        <tr>
                            <td style="height: 13px">
                                <strong>Number</strong></td>
                            <td style="height: 13px">
                                <strong>Date-Expiry</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Panama1" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Panamadate1" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator15" runat="server" ControlToValidate="Panamadate1" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Panama2" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Panamadate2" runat="server" CssClass="input_box" MaxLength="5" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator16" runat="server" ControlToValidate="Panamadate3" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Panama3" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Panamadate3" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator17" runat="server" ControlToValidate="Panamadate1" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Panama4" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Panamadate4" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator18" runat="server" ControlToValidate="Panamadate4" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Panama5" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Panamadate5" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator19" runat="server" ControlToValidate="Panamadate5" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Panama6" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Panamadate6" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator20" runat="server" ControlToValidate="Panamadate6" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="height: 19px">
                                <asp:TextBox ID="Panama7" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td style="height: 19px">
                                <asp:TextBox ID="Panamadate7" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator21" runat="server" ControlToValidate="Panamadate7" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                    </table>
                    </td>
                    <td rowspan="1" style="text-align: center" valign="top" colspan="2"><table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2" style="height: 13px">
                                <strong>Singapore</strong></td>
                        </tr>
                        <tr>
                            <td style="height: 13px">
                                <strong>Number</strong></td>
                            <td style="height: 13px">
                                <strong>Date-Expiry</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Sing1" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Singdate1" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator22" runat="server" ControlToValidate="Singdate1" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="height: 19px">
                                <asp:TextBox ID="Sing2" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td style="height: 19px">
                                <asp:TextBox ID="Singdate2" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator23" runat="server" ControlToValidate="Singdate2" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Sing3" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Singdate3" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator24" runat="server" ControlToValidate="Singdate3" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="height: 19px">
                                <asp:TextBox ID="Sing4" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td style="height: 19px">
                                <asp:TextBox ID="Singdate4" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator25" runat="server" ControlToValidate="Singdate4" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Sing5" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Singdate5" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator26" runat="server" ControlToValidate="Singdate5" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="height: 19px">
                                <asp:TextBox ID="Sing6" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td style="height: 19px">
                                <asp:TextBox ID="Singdate6" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator27" runat="server" ControlToValidate="Singdate6" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Sing7" runat="server" CssClass="input_box" MaxLength="49" Width="79px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="Singdate7" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox><asp:CompareValidator
                                    ID="CompareValidator28" runat="server" ControlToValidate="Singdate7" ErrorMessage="Invalid Date"
                                    Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator></td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Solas Cettificate</strong></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        First Aid Course :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        Fast Rescue Boats :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Fire Flighting Course :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        Advance Fire Flighting :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Survival Course :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        Medical Care :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Survival Craft Course :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">
                        <strong>
                        Other Cettificate</strong></td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        GMDSS(G.O.C) :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        C.O.W :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td style="height: 43px">
                    </td>
                    <td style="height: 43px">
                        Radio Telephony :</td>
                    <td style="text-align: left; height: 43px;">
                        <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="height: 43px">
                        Inart Gas :</td>
                    <td style="text-align: left; height: 43px;">
                        <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Radar Observer :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        Tanker Safety :</td>
                    <td style="text-align: left; height: 24px;">
                        <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        ARPA :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        Chemical Tanker Safety :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>
                        </td>
                    <td>
                        Bridge Tam ManageMent :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        Gas Tanker Safety :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td style="height: 17px">
                    </td>
                    <td style="height: 17px">
                        Advance Fire Flighting :</td>
                    <td style="text-align: left; height: 17px;">
                        <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="height: 17px">
                        </td>
                    <td style="text-align: left; height: 17px;">
                        </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                    </td>
                    <td style="height: 17px">
                        Efficient Deck Hand :</td>
                    <td style="text-align: left; height: 17px;">
                        <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="height: 17px">
                        </td>
                    <td style="text-align: left; height: 17px;">
                        </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Watch Keeping Cert For Deck Rattings :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                        </td>
                    <td style="text-align: left">
                        </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Watch Keeping Cert For Engine Rattings :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Welding / Fitting :</td>
                    <td style="text-align: left">
                        <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 7px">
                    </td>
                    <td style="height: 7px">
                    </td>
                    <td style="text-align: left; height: 7px;">
                    </td>
                    <td style="height: 7px">
                    </td>
                    <td style="height: 7px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 17px">
                        Any Licence Application required.</td>
                    <td style="text-align: left; height: 17px;">
                        <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="height: 17px">
                    </td>
                    <td style="height: 17px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        if Answer is Yes please state below the type of document requred.</td>
                    <td style="text-align: left">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="Doc1" runat="server" CssClass="input_box" MaxLength="49" Width="100%"></asp:TextBox></td>
                    <td style="text-align: left">
                    </td>
                    <td>
                        Date Of Application made :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Docdate1" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                    <td style="height: 10px">
                        <asp:TextBox ID="Doc2" runat="server" CssClass="input_box" MaxLength="49" Width="100%"></asp:TextBox></td>
                    <td style="text-align: left; height: 10px;">
                    </td>
                    <td style="height: 10px">
                        Date Of Application made :</td>
                    <td style="text-align: left; height: 10px;">
                        <asp:TextBox ID="Docdate2" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="Doc3" runat="server" CssClass="input_box" MaxLength="49" Width="100%"></asp:TextBox></td>
                    <td style="text-align: left">
                    </td>
                    <td>
                        Date Of Application made :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Docdate3" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="Doc4" runat="server" CssClass="input_box" MaxLength="49" Width="100%"></asp:TextBox></td>
                    <td style="text-align: left">
                    </td>
                    <td>
                        Date Of Application made :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Docdate4" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="Doc5" runat="server" CssClass="input_box" MaxLength="49" Width="100%"></asp:TextBox></td>
                    <td style="text-align: left">
                    </td>
                    <td>
                        Date Of Application made :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="Docdate5" runat="server" CssClass="input_box" MaxLength="10" Width="70px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" style="padding-top: 10px; padding-bottom:10px; padding-right:10px">
            <asp:Button ID="btn_Save" OnClick="btn_Save_Click" runat="server" CssClass="btn" TabIndex="9" Text="Save" Width="59px" /></td>
    </tr>
     </table>
     </form></body></html>
<%--</asp:Content>--%>

