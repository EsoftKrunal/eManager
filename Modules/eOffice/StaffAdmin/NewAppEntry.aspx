<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewAppEntry.aspx.cs" Inherits="NewAppEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Applicant Details</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="vstylesheet" type="text/css" />
    
    <style>
    .selecteddiv
    {
    	width:100%; padding:5px; padding-bottom :5px;background-color:wheat;border:dotted 1px #4371a5;
    }
    .normaldiv
    {
    	width:100%; padding:5px; padding-bottom :5px;background-color:none;
    }
        </style>
    <script type="text/javascript" >
    function getCookie(c_name)
    {
    var i,x,y,ARRcookies=document.cookie.split(";");
    for (i=0;i<ARRcookies.length;i++)
    {
      x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
      y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
      x=x.replace(/^\s+|\s+$/g,"");
      if (x==c_name)
        {
        return unescape(y);
        }
      }
    }
    function setCookie(c_name,value,exdays)
    {
    var exdate=new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
    document.cookie=c_name + "=" + c_value;
    }
    function SetLastFocus(ctlid)
    {
        pos=getCookie(ctlid);
        if(isNaN(pos)) 
        {pos=0;}
        if(pos>0)
        {
        document.getElementById(ctlid).scrollTop=pos;
        }
    }
    function SetScrollPos(ctl)
    {
        setCookie(ctl.id,ctl.scrollTop,1); 
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
    function RefereshParrentPageData() {
        window.opener.refreshPage();
        
    }
    
    </script> 
    <script type="text/javascript">
        window.resizeTo(1000, 645);            
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>
        <div style="text-align: center; width :100%;font-family:Arial;font-size:12px;" >
        <center >
        
            <table width="750px;" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;">
            <tr>
            <td align="center" style=" height: 23px" class="text headerband">Applicant Details
            </td>
            </tr>
                <tr>    
                <td >
                    <table cellpadding="0" cellspacing="0" width ="100%;" rules="all" border="0" >
                    <tr>
                        <td><asp:Label ID="lbl_info" runat="server" Width="100%" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="padding-right: 10px; padding-left: 10px;text-align: center;" align="center">
                        <div style ="float:left ">
                            <table ID="Table1" border="0" cellpadding="2" cellspacing="0" rules="rows" style="width:660px;border-collapse:collapse;" >
                            <colgroup>
                                <col style="text-align :left" />
                                <col style="text-align :right" />
                                <tr class="rowstyle">
                                    <td style="width:150px; text-align :left">
                                        Applicant Name :</td>
                                    <td style="text-align :left" colspan="3">
                                        <div style="width :100%; font-style :italic; color :Gray;">
                                            <div style="width :155px; display :inline-block;" >
                                                First Name
                                            </div>
                                            <div style="width :155px; display :inline-block">
                                                Middle Name
                                            </div>
                                            <div style="width :125px; display :inline-block">
                                                Last Name</div>
                                        </div>
                                        <asp:TextBox ID="txtFName" runat="server" style="background-color:lightyellow" CssClass="input_box" MaxLength="50"></asp:TextBox>
                                        <asp:TextBox ID="txtMName" runat="server" CssClass="input_box" MaxLength="50"></asp:TextBox>
                                        <asp:TextBox ID="txtLName" runat="server" style="background-color:lightyellow" CssClass="input_box" MaxLength="50"></asp:TextBox>
                                        <div style="width :100%;">
                                            <div style="width :125px; display :inline-block">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                    ControlToValidate="txtFName" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                            </div>
                                            <div style="width :125px; display :inline-block">
                                            </div>
                                            <div style="width :125px; display :inline-block">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                    ControlToValidate="txtLName" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align :left">
                                        Date Of Birth :</td>
                                    <td style="text-align :left">
                                        <asp:TextBox ID="txtDOB"  runat="server" CssClass="input_box" MaxLength="15" style=" text-align :center;background-color:lightyellow" width="80px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDOB" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                    
                                    <td style="text-align :left"></td>
                                    <td style="text-align :left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align :left">
                                        <span lang="en-us">Passport # :</span></td>
                                    <td style="text-align :left">
                                        <asp:TextBox ID="txtPassportNo" runat="server" CssClass="input_box" MaxLength="50" style="background-color:lightyellow;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassportNo" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                    </td>                                    
                                    <td style="text-align :left">
                                        &nbsp;<td style="text-align :left">
                                            &nbsp;</td>
                                    </td>
                                </tr>
                                <tr runat="server" id="tr1" visible="false" >
                                    <td style="text-align :left">
                                        Gender :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:RadioButtonList runat="server" ID="radSex" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Male" Value="1" Selected="True"></asp:ListItem>  
                                        <asp:ListItem Text="FeMale" Value="2"></asp:ListItem>  
                                        </asp:RadioButtonList> 
                                        
                                    </td>
                                </tr>
                                <tr >
                                    <td style="text-align :left">
                                        Position Applied :</td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="true" style="background-color:lightyellow" CssClass="input_box" Width="252px" >
                                        </asp:DropDownList>
                                        <asp:CompareValidator runat="server" ID="cpmval1" ValueToCompare="0" ErrorMessage="Required." ControlToValidate="ddlRank" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                    </td>
                                    <td style="text-align :left;" colspan="2">
                                        Available From : 
                                        <asp:TextBox ID="txtAvalFrom" runat="server" CssClass="input_box" MaxLength="15" style=" text-align :center;" width="80px"></asp:TextBox>
                                        </td>
                                </tr>
                                 <tr>
                                    <td style="text-align :left">
                                        Nationality :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:DropDownList ID="ddlNat" runat="server" AutoPostBack="true" style="background-color:lightyellow" 
                                            CssClass="input_box" Width="252px">
                                        </asp:DropDownList>
                                        <asp:CompareValidator runat="server" ID="CompareValidator1" ValueToCompare="0" ErrorMessage="Required." ControlToValidate="ddlNat" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                    </td>
                                </tr>
                                 <tr>
                                     <td style="text-align :left">
                                         Address :</td>
                                     <td colspan="3" style="text-align :left">
                                         <asp:TextBox ID="txtAddress1" runat="server" CssClass="input_box" Width="400px" Height="65px" 
                                             TextMode="MultiLine" ></asp:TextBox>
                                     </td>
                                </tr>
                                <%--<tr>
                                     <td style="text-align :left">
                                         Address-II :</td>
                                     <td colspan="3" style="text-align :left">
                                         <asp:TextBox ID="txtAddress2" runat="server" CssClass="input_box" Height="45px" 
                                             TextMode="MultiLine" width="300px"></asp:TextBox>
                                     </td>
                                </tr>
                                <tr>
                                     <td style="text-align :left">
                                         Address-III :</td>
                                     <td colspan="3" style="text-align :left">
                                         <asp:TextBox ID="txtAddress3" runat="server" CssClass="input_box" Height="45px" 
                                             TextMode="MultiLine" width="300px"></asp:TextBox>
                                     </td>
                                </tr>--%>
                                
                                
                                <tr>
                                    <td style="text-align :left">
                                        Country :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:DropDownList ID="ddlCounty" runat="server" AutoPostBack="true" style="background-color:lightyellow" CssClass="input_box" Width="252px">
                                        </asp:DropDownList>
                                        <asp:CompareValidator runat="server" ID="CompareValidator2" ValueToCompare="0" ErrorMessage="Required." ControlToValidate="ddlCounty" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align :left">
                                        State :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:TextBox ID="txtState" runat="server" CssClass="input_box" width="249px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="text-align :left">
                                        City :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="input_box" width="249px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align :left">
                                        Zip Code :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="input_box" width="249px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="text-align :left" >
                                        Telephone :</td>
                                    <td style="text-align :left"  colspan="3">
                                        <div style="width :100%; font-style :italic; color :Gray;">
                                            <div style="width :60px; display :inline-block;font-size:9px;">
                                                CountryCode
                                            </div>
                                            <div style="width :50px; display :inline-block;font-size:9px;">
                                                AreaCode
                                            </div>
                                            <div style="width :80px; display :inline-block;font-size:9px;">
                                                Number
                                            </div>
                                        </div>
                                        <asp:TextBox ID="txtTelCntCode" runat="server" CssClass="input_box"  Width="50px" MaxLength="3"></asp:TextBox>
                                        <asp:TextBox ID="txtTelAreaCode" runat="server" CssClass="input_box" Width="50px" MaxLength="6"></asp:TextBox>
                                        
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="input_box" MaxLength="15" Width="120px"></asp:TextBox>                                        
                                        <asp:Label ID="lblPnmess" runat="server" ForeColor="Red"></asp:Label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align :left" >
                                        Mobile :
                                    </td>
                                    <td style="text-align :left"  colspan="3">
                                        <div style="width :100%; font-style :italic; color :Gray; ">
                                            <div style="width :60px; display :inline-block;font-size:9px;">
                                                CountryCode
                                            </div>
                                            <div style="width :50px; display :inline-block;font-size:9px;">
                                                AreaCode
                                            </div>
                                            <div style="width :80px; display :inline-block;font-size:9px;">
                                                Number
                                            </div>
                                        </div>
                                        
                                        <asp:TextBox ID="txtMobCntCode" runat="server" CssClass="input_box"  Width="50px" MaxLength="3"></asp:TextBox>
                                        <asp:TextBox ID="txtMobAreaCode" runat="server" CssClass="input_box" Width="50px" MaxLength="6"></asp:TextBox>
                                        
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="input_box" MaxLength="15" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td style="text-align :left">
                                        Fax :</td>
                                    <td style="text-align :left" colspan="3">
                                        <div style="width :100%; font-style :italic; color :Gray; ">
                                            <div style="width :60px; display :inline-block;font-size:9px;">
                                                CountryCode
                                            </div>
                                            <div style="width :50px; display :inline-block;font-size:9px;">
                                                AreaCode
                                            </div>
                                            <div style="width :80px; display :inline-block;font-size:9px;">
                                                Number
                                            </div>
                                        </div>
                                        <asp:TextBox ID="txtFaxCntCode" runat="server" CssClass="input_box" width="50px" MaxLength="3"></asp:TextBox>
                                        <asp:TextBox ID="txtFaxAreaCode" runat="server"  CssClass="input_box" width="50px" MaxLength="6"></asp:TextBox>
                                        
                                        <asp:TextBox ID="txtFax" runat="server" CssClass="input_box" MaxLength="15" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align :left">
                                        E-Mail :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:TextBox ID="txtEMail1" runat="server" CssClass="input_box" width="249px"  MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td style="text-align :left">
                                        E-Mail Address-II :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:TextBox ID="txtEmail2" runat="server" MaxLength="100" CssClass="input_box" width="249px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr id="trStatus" runat="server" visible="false">
                                    <td style="text-align :left">
                                        Status :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                                        <%--<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" style="background-color:lightyellow" CssClass="input_box" Width="120px">
                                        </asp:DropDownList>--%>
                                        <%--<asp:CompareValidator runat="server" ID="CompareValidator3" ValueToCompare="0" ErrorMessage="Required." ControlToValidate="ddlStatus" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align :left">
                                        Date of Application :</td>
                                    <td style="text-align :left" colspan="3">
                                        <asp:TextBox ID="txtDoa" runat="server" CssClass="input_box" MaxLength="15" style=" text-align :center;" width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td style="text-align :left">
                                        <span lang="en-us">Resume :</span></td>
                                    <td colspan="3" style="text-align :left">
                                    <asp:FileUpload runat="server" id="FileUpload1" style="background-color:white;border:solid 1px #4371a5; font-size:10px; width:252px;" /> 
                                        &nbsp;</td>
                        </tr>
                                <%--<tr>
                                    <td style="text-align :left; vertical-align :top">
                                        Vessel Experience :</td>
                                    <td style="text-align : left">
                                        <asp:CheckBoxList ID="chkVeselType" runat="server" CellPadding="0" 
                                            CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td style="text-align :left">
                                        &nbsp;</td>
                                    <td style="text-align :left">
                                        &nbsp;</td>
                                </tr>--%>
                            </colgroup>
                                    </table>
                        </div>
                        </td>
                    </tr>
                    </table>
                    </td>
                </tr>
               <tr runat="server" id="trAction">
                 <td style="text-align: left ; padding:5px 0px 5px 0px; padding-left : 175px;">
                 <asp:Button runat="server" ID="btnSaveApplicant" Text=" Save " CssClass="btn" onclick="btnSaveApplicant_Click"  BorderColor ="#4371a5" BorderWidth="2px" Font-Size="12px" Height="20px" /> 
                  <a runat="server" id="ancCV" target="_blank"> <img src="../../HRD/Images/paperclip12.gif" style="border : none" /> </a>
                 </td>
             </tr>
             <tr>
            <td align="center" style=" height: 23px" class="text headerband" >Communication Summary</td>
            </tr>
            <tr runat="server" id="trAction1">
                    <td style="background-color : Wheat">
                    <table border ="0" cellspacing="0" cellpadding="2" width="100%" >
                    <tr>
                    <td style="vertical-align :top">
                        Date</td>
                    <td style=" vertical-align :top; text-align :center " valign="top">
                        Details</td>
                    <td style=" text-align :left;" rowspan="2" >
                        <asp:RadioButtonList ID="radCommType" runat="server" RepeatDirection="Vertical">
                            <asp:ListItem Text="Phone" Value="P"></asp:ListItem>
                            <asp:ListItem Text="EMail" Value="E"></asp:ListItem>
                            <asp:ListItem Text="InPerson" Value="I"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div style=" width : 150px; text-align :left"  >
                        &nbsp;&nbsp;
                            <asp:ImageButton ID="btnAddComSummary" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif" 
                            onclick="btnAddComSummary_Click" 
                            ToolTip="Add/Udpate this conversation." ValidationGroup="det" Width="16px" />&nbsp;
                            <asp:ImageButton ID="btnClear" runat="server" CausesValidation="false" 
                            ImageUrl="~/Modules/eOffice/Images/clear.png" onclick="btnClear_Click" style="height: 16px" 
                            ToolTip="Clear Text." />
                            </div>
                        </td>
                    </tr>
                    <tr>
                            <td style="vertical-align :top">
                                <asp:TextBox ID="txtConDate" runat="server" CssClass="input_box" MaxLength="15" 
                                    style=" text-align :center" width="80px" AutoPostBack="true" OnTextChanged="Validate" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtConDate" Display="Static" ErrorMessage="*" 
                                    ValidationGroup="det"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:Label ForeColor="Red" Text="Invalid date." runat="server" ID="lblDateMess" ></asp:Label> 
                            </td>
                            <td style=" vertical-align :top; text-align :center;" valign="top" >
                                <asp:TextBox ID="txtCon" runat="server" CssClass="input_box"  Height="65px" TextMode="MultiLine" 
                                    Width="570px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtCon" Display="Static" ErrorMessage="*" 
                                    ValidationGroup="det"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                    <div id="dvscroll_cdpopup" style="width :95%; overflow-x :hidden ; overflow-y:scroll; height :150px;padding-left :15px;padding-right :15px;" onscroll="SetScrollPos(this)">
                    <asp:Repeater runat="server" ID="rptData">
                    <ItemTemplate >
                    <div class='<%#(Eval("TABLEID").ToString().Trim()==SelectedDisc.ToString().Trim())?"selecteddiv":"normaldiv"%>'>
                    <asp:ImageButton runat="server" ID="btnDelComment" CssClass='<%# Eval("CALLEDBY")%>' CommandArgument='<%# Eval("TABLEID") %>' ImageUrl="~/Modules/HRD/Images/Emtm/trash.gif" style=" float :right" OnClientClick="return confirm('Are you sure to remove this record?');" OnClick="btnDelComment_Click"/> 
                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass='<%# Eval("CALLEDBY")%>' Text='<%# Eval("DISC_DATE_STR") %>' Font-Italic="true" ForeColor="Blue" CommandArgument='<%# Eval("TABLEID") %>' OnClick="rptEdit_Click" ></asp:LinkButton> 
                    <span style =" color :Purple">[ <%# Eval("USERNAME")%> ]</span> 
                    <span>
                    <img src="../Images/icon_phone.jpg" style='display :<%# (Eval("DISCTYPE").ToString()=="P")?"":"none"%>' />
                    <img src='../Images/icon-email.gif' style='display :<%# (Eval("DISCTYPE").ToString()=="E")?"":"none"%>' />
                    <img src='../Images/icon_user.gif' style='display :<%# (Eval("DISCTYPE").ToString()=="I")?"":"none"%>' />
                    </span>
                    <%# Eval("DISC")%></div>
                    </ItemTemplate>
                    </asp:Repeater> 
                    </div>
                    </td>
                </tr>
            </table>
            </center>
        </div>
        <ajaxToolkit:CalendarExtender runat="server"  ID="CalendarExtender1" TargetControlID="txtAvalFrom" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>  
        <ajaxToolkit:CalendarExtender runat="server"  ID="CalendarExtender4" TargetControlID="txtDoa" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>  
        <ajaxToolkit:CalendarExtender runat="server"  ID="CalendarExtender2" TargetControlID="txtDOB" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>  
        <ajaxToolkit:CalendarExtender runat="server"  ID="CalendarExtender3" TargetControlID="txtConDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>  
        </ContentTemplate> 
        <Triggers>
        <asp:PostBackTrigger ControlID="btnSaveApplicant" />
        </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
