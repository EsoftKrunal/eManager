<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewCRMDocument.aspx.cs" Inherits="CrewCRMDocument" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Title="Notes & History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <%--<img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> --%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
            <style type="text/css">
.hd
{
	background-color : #c2c2c2;
}
a img
{
border:none;	
}
</style>
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            // prtContent.innerHTML=strOldOne;
        }
        function Show_Image_Large(obj) {
            window.open(obj.src, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function opennewvisit() {
            window.open('NewVisit.aspx', '', 'letf=0,top=0,width=730,height=370,toolbar=0,scrollbars=0,status=0');
        }
        function refreshme() {
            document.getElementById('btn_refresh').click();
        }
    </script>
    <script language="javascript" type="text/javascript">
        function ConfirmDelete(ctl) {
            if (confirm('Are you sure to delete ?')) {
                ctl.src = '../Images/inprocss.gif';
                return true;
            }
            else {
                return false;
            }
        }
        function Show_Amount() {
            try {
                var Amt = 0.0;
                var deduct = 0.0;
                var temp = 0;
                var ob = null;
                var CtlList = new Array("02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13","14","15","16","17","18","19","20");
                for (i = 0; i <= 18; i++) {
                    ob = document.getElementById("ctl00_ContentMainMaster_Gd_AssignWages_ctl" + CtlList[i] + "_txtAmount");
                    if (ob != null) {
                        if (ob.getAttribute('Wid') != "12") {
                            temp = parseFloat(ob.value);
                            if (!isNaN(temp)) {
                                Amt = Amt + parseFloat(temp);
                            }
                        }
                    }
                }
                
                for (i = 0; i <= 18; i++) {
                    ob = document.getElementById("ctl00_ContentMainMaster_Gd_AssignWagesDeductions_ctl" + CtlList[i] + "_txtAmount");
                    if (ob != null) {
                        if (ob.getAttribute('Wid') != "12") {
                            temp = parseFloat(ob.value);
                            if (!isNaN(temp)) {
                                deduct = deduct + parseFloat(temp);
                            }
                        }
                    }
                }
               
                var Gross = 'ctl00_ContentMainMaster_lb_Gross';
                var Ded ='ctl00_ContentMainMaster_lb_deduction';
                var Net = 'ctl00_ContentMainMaster_lb_NewEarning1';
                var Txt = 'ctl00_ContentMainMaster_txt_Other_Amount';

                var Gross_Val = Amt;//"0" + document.getElementById(Gross).innerHTML;
                var Ded_Val = deduct;// + document.getElementById(Ded).innerHTML;
                var Amount = "0";// + document.getElementById(Txt).value;
                
                if (isNaN(Gross_Val)) { Gross_Val = "0"; }
                if (isNaN(Ded_Val)) { Ded_Val = "0"; }
                if (isNaN(Amount)) { Amount = "0"; }

                Amount = parseFloat(Gross_Val) - parseFloat(Ded_Val) + parseFloat(Amount);
                if (document.getElementById(Txt).value != "") {
                    Amount = Amount + parseFloat(document.getElementById(Txt).value);
                }
                Amount = Math.round(Amount * 100) / 100;
                if (isNaN(Amount)) { Amount = "0"; }
                document.getElementById(Net).innerHTML = Amount;
            }
            catch (err) {
            }
        }

        var ScreenW = window.screen.availWidth - 50;
        var ScreenH = window.screen.availHeight - 50;
        var selDiv;
        function showUpdate(ctl, pkid, tid, dd, mode, TRid) {
            //        var maxpos=0;
            //        var ctl_width=700;
            //        var ctl_height=220;
            //        
            //        var left=event.clientX;
            //        var top=event.clientY;
            //        
            //        if(left<ScreenW/2)
            //        {
            //            left=left+(ctl_width/2);
            //        }
            //        else
            //        {
            //            left=left-(ctl_width/2);
            //        }
            //        maxpos=left+ctl_width;
            //        if(maxpos>=ScreenW)
            //        {
            //        left=left-(maxpos-ScreenW);
            //        }
            //        if(left<=0)
            //        {
            //        left=0;
            //        }
            //        if(top<ScreenH/2)
            //        {
            //            top=top+30;
            //        }
            //        else
            //        {
            //            top=top-30-ctl_height;
            //        }
            //        
            //        document.getElementById("hfdPKId").value=pkid;
            //        document.getElementById("hfdTId").value=tid;
            //        document.getElementById("hfdDD").value=dd;
            //        
            //        //document.getElementById("dvInf1").innerHTML=ctl.getAttribute('data');
            //        document.getElementById("txtTrId").value=TRid;
            //        document.getElementById("btnLoadLit").click();
            //        document.getElementById("dvUP").style.display=((mode=="P")?"":"none");
            //        
            //        dvSch.style.left =left+'px';
            //        dvSch.style.top =top+'px';
            //        
            //        $("#dvSch").slideDown();
            //        document.getElementById("txt_FromDate").focus();
            //        selDiv="#dvSch";
            window.open('UpdatePlannedTrainingPopUp.aspx?PkId=' + pkid + '&Tid=' + tid + '&DD=' + dd + '&mode=' + mode, '', 'width=700,height=220');
        }

        function hideUpdate(val) {
            if (event.keyCode == 27)
                $("#dvSch").slideUp();
        }
        function hideLast() {
            if (event.keyCode == 27) {
                $(selDiv).slideUp();
            }
        }
    </script>
    <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;	
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
}
</style>
    </asp:Content>


<%--<body onkeydown='hideLast();'>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    

    <div style="text-align: left;">
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
        
        <td style=" text-align :left; vertical-align : top;" >
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%;" colspan="2">
                    <%--Average Score:--%>Notes and History
                </td>
            </tr>
            <tr>
                <td align="Left" style="text-align:left;height:25px;padding-left:10px;background-color:#fff" >
                    <asp:LinkButton ID="b4" runat="server" Font-Bold="true" Text="Personal" OnClick="imgbtn_Personal_Click"  CausesValidation="False" ForeColor="#206020"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                     <asp:LinkButton ID="b5" runat="server" Font-Bold="true"  Text="Documents" OnClick="imgbtn_Document_Click"  CausesValidation="False" ForeColor="#206020"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                     <asp:Label ID="crm_Alert" runat="server" ForeColor="Red"></asp:Label>
                    <asp:HyperLink runat="Server" ID="doc_Alert" Target="_blank" NavigateUrl="DocumentAlerts.aspx" ForeColor="Red" ></asp:HyperLink>  &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lnkPopUp" Visible ="false" ForeColor="Red" Font-Size="14pt" OnClientClick ="window.open('../CriticalRemarkPopUp.aspx');return false;" Text ="Remarks" Font-Bold="True" ></asp:LinkButton>
                    
                </td>
                <td style="text-align:center;font-size:14px;background-color:#fff">
                    <strong>[<asp:Label ID="txt_MemberId" runat="server"></asp:Label>]</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 75%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9" width="100%">
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;
                                text-align: left">
                                 
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td style=" text-align :center " colspan="4">
                                            <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1" Width="100%" Font-Bold="True"></asp:Label></td>
                                         
                                     </tr>
                                </table>
                                
                                 <table cellpadding="0" cellspacing="0" width="100%" style="display:none;">
                                    <tr>
                                        <td style="height: 13px">
                                        </td>
                                    </tr>
                                    <tr>
                                <td style="height: 13px">
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                   
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr>
                                                <td colspan="6" style="padding-left: 5px; height: 3px; text-align: left">
                                                    &nbsp; &nbsp;</td>
                                                <td colspan="1" rowspan="7" style="padding-right:27px;padding-left:15px; text-align: center" valign="middle">
                                               
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                    <asp:Label ID="Label43" runat="server" meta:resourcekey="Label43Resource1" Text="First Name:"
                                                        Width="72px"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_FirstName" runat="server" CssClass="required_box" MaxLength="24"
                                                        meta:resourcekey="txt_FirstNameResource1" Width="160px" ReadOnly="True"></asp:TextBox></td>
                                                <td style="padding-left: 5px; width: 121px; height: 13px; text-align: right">
                                                    <asp:Label ID="Label37" runat="server" meta:resourcekey="Label37Resource1" Text="Middle Name:"
                                                        Width="82px"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_MiddleName" runat="server" CssClass="input_box" MaxLength="24" Width="160px"></asp:TextBox></td>
                                                <td align="right" style="width: 174px; height: 13px">
                                                    <asp:Label ID="Label38" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:"
                                                        Width="100%"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_LastName" runat="server" CssClass="required_box" MaxLength="24" Width="160px" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_FirstName"
                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator></td>
                                                <td style="padding-left: 5px; width: 121px; height: 13px; text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                </td>
                                                <td align="right" style="width: 174px; height: 13px">
                                                </td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_LastName"
                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                    <asp:Label ID="Label15" runat="server" meta:resourcekey="Label15Resource1" Text="Current Rank:"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="ddcurrentrank" runat="server" CssClass="input_box" meta:resourcekey="ddcurrentrankResource1"
                                                        ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                <td style="padding-left: 5px; height: 13px; text-align: right; width: 121px;">
                                                    <asp:Label ID="Label44" runat="server" meta:resourcekey="Label44Resource1" Text="Last Vessel:"
                                                        Width="72px"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_LastVessel" runat="server" CssClass="input_box" MaxLength="24"
                                                        ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                <td align="right" style="width: 174px; height: 13px">
                                                    Passport No :</td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_Passport" runat="server" CssClass="input_box" MaxLength="49"
                                                        ReadOnly="True" TabIndex="0" Width="160px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                </td>
                                                <td style="padding-left: 5px; width: 121px; height: 13px; text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                </td>
                                                <td align="right" style="width: 174px; height: 13px">
                                                </td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                    Status:</td>
                                                <td colspan="5" style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_Status" runat="server" CssClass="input_box" MaxLength="10" meta:resourcekey="txt_MemberIdResource1"
                                                        ReadOnly="True" Width="520px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 151px; height: 13px">
                                                </td>
                                                <td style="padding-left: 5px; visibility: hidden; width: 178px; height: 13px; text-align: left">
                                                </td>
                                                <td style="padding-left: 5px; width: 121px; height: 13px; text-align: left">
                                                </td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                    &nbsp;</td>
                                                <td align="right" style="width: 174px; height: 13px">
                                                </td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: right">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <div id="divPrint">
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="Tab1" runat="server">
                                       <%-- <table cellpadding="0" cellspacing="0">
                                        <tr><td>
                                        <asp:RadioButtonList AutoPostBack="true" ID="r1" runat="server" OnSelectedIndexChanged="r1_OnSelectedIndexChanged" RepeatDirection="Horizontal" >
                                        <asp:ListItem Value="0" Text="CRM Notes" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Crew Availablity"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                                        </tr>
                                        </table>--%>
                                        <asp:Panel runat="server" ID="p1">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td style="">
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;">
                                                        <legend><strong>Notes</strong></legend>
                                                        <asp:Label ID="lbl_crm" runat="server"></asp:Label>
                                                        <div style=" width:100%; height:250px; overflow-y:scroll; overflow-x:hidden " > 
                                                             <table cellpadding="3" cellspacing="0" border="0" width="100%">
                                                                 <thead>
                                                                     <tr class="headerstylegrid">
                                                                         <th style="width:50px">View</th>
                                                                         <th style="width:50px">Edit</th>
                                                                         <th style="width:50px">Delete</th>
                                                                         <th style="width:250px">Category</th>
                                                                         <th>Description</th>
                                                                         
                                                                      <%--   <th style="width:150px">CreatedBy</th>
									                                    <th style="width:100px">CreatedOn</th>
                                                                         <th style="width:100px">AlertOn</th>--%>
                                                                         <th style="width:80px">Attachment</th>
                                                                     </tr>
                                                                 </thead>
                                                            <asp:Repeater runat="server" ID="gvcrm">
                                                                <ItemTemplate>
                                                                   <tr>
                                                                       <td>
                                                                           <asp:ImageButton runat="server" ID="imgAtt1" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="gvcrm_OnViewClick" CommandArgument='<%#Eval("CrewCRMId")%>' />
                                                                       </td>
                                                                       <td>
                                                                           <asp:ImageButton runat="server" ID="ImageButton6" Visible='<%#(((Mode == "New") || (Mode == "Edit")) && Auth.isEdit)%>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="gvcrm_OnEditClick" CommandArgument='<%#Eval("CrewCRMId")%>' />
                                                                       </td>
                                                                       <td><asp:ImageButton runat="server" ID="ImageButton7" Visible='<%#(((Mode == "New") || (Mode == "Edit")) && Auth.isDelete)%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" OnClick="gvcrm_OnDeleteClick" CommandArgument='<%#Eval("CrewCRMId")%>' />

                                                                       </td>
                                                                       <td><%#Eval("CRMCategoryName")%></td>
                                                                       <td style="text-align:left">
                                                                           
                                                                           <asp:HiddenField ID="HiddencrewcrmId" runat="server" Value='<%#Eval("CrewCRMId")%>' />
                                                                            <asp:HiddenField ID="hfdCreatedBy" runat="server" Value='<%#Eval("CreatedBy")%>' />
                                                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description").ToString().Replace("\n","<br/>") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hiddenCRMvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                       </td>
                                                                       
                                                                      <%-- <td><%#Eval("CreatedBy")%></td>
									                                    <td><%#Common.ToDateString(Eval("Createdon"))%></td>
                                                                       <td><%#Common.ToDateString(Eval("AlertExpiryDate"))%></td>--%>
                                                                        <td><asp:ImageButton runat="server" ID="imgAtt" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="imgAtt_OnClick" Visible='<%#(Eval("FileName").ToString()!="")%>' CommandArgument='<%#Eval("CrewCRMId")%>'  /></td>
                                                                   </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                                 </table>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="crmpanel" runat="server" Visible="false" Width="100%">
                                                        <br />
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>Notes Details</strong></legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 117px">
                                                                        Description:</td>
                                                                    <td align="left" colspan="6" rowspan="4" style="padding-top: 5px">
                                                                        <asp:TextBox ID="txtdescription" runat="server" CssClass="input_box" Height="45px"
                                                                            MaxLength="19" TabIndex="2" TextMode="MultiLine" Width="550px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px; height: 15px;">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="width: 152px; height: 15px">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                                            ControlToValidate="txtdescription" Display="Dynamic" ErrorMessage="Required" 
                                                                            Width="57px"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px; width: 250px;">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="width: 214px; height: 15px">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px; width: 191px;">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px; height: 18px;">
                                                                    Show In Alert:</td>
                                                                    <td align="left" rowspan="1" style="width: 152px; height: 18px;">
                                                                        <asp:CheckBox ID="chk_show_alert" runat="server" AutoPostBack="True" OnCheckedChanged="chk_show_alert_CheckedChanged" TabIndex="3" /></td>
                                                                    <td align="right" rowspan="1" style="height: 18px; width: 250px;">
                                                                        Alert Expiry Date:</td>
                                                                    <td align="right" rowspan="1" style="height: 18px">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="width: 214px; padding-left: 5px; height: 18px;">
                                                                        <asp:TextBox ID="txt_crm_expirydate" runat="server" CssClass="required_box" Enabled="False"
                                                                            MaxLength="20" Width="106px" TabIndex="4"></asp:TextBox>
                                                                        <asp:ImageButton ID="img_crm_expiry" runat="server" Enabled="false" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                    </td>
                                                                    <td align="right" style="width: 191px; height: 18px;">
                                                                        &nbsp;</td>
                                                                    <td align="left" rowspan="1" style="height: 18px; width: 187px; padding-left: 5px;">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px; height: 13px;">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="width: 152px; height: 13px;">
                                                                    </td>
                                                                    <td align="right" rowspan="1" style="width: 250px; height: 13px;">
                                                                    </td>
                                                                    <td align="right" rowspan="1" style="height: 13px">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="width: 214px; height: 13px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_crm_expirydate" ErrorMessage="Required" Width="57px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_crm_expirydate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    <td align="left" rowspan="1" style="width: 191px; height: 13px;">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 13px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 117px; height: 15px;">
                                                                        Notes Category:</td>
                                                                    <td align="left" rowspan="1" style="width: 152px; height: 15px">
                                                                        
                                                                        <asp:DropDownList ID="ddl_crm_category" runat="server" CssClass="input_box" TabIndex="5" Width="150px">
                                                                        </asp:DropDownList>
                                                                        
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px; width: 250px; text-align: right;">
                                                                        Attachment :</td>
                                                                    <td align="left" rowspan="1" style="height: 15px">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="width: 214px; height: 15px">
                                                                        <asp:FileUpload runat="server" ID="flpcrm" />

                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px; width: 191px;">
                                                                    </td>
                                                                    <td align="left" rowspan="1" style="height: 15px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                            PopupButtonID="img_crm_expiry" TargetControlID="txt_crm_expirydate" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                                    <asp:HiddenField ID="HiddenCRMPK" runat="server" />
                                                                       
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="height: 18px; padding-right:2px">
                                                    <asp:Button ID="btn_crm_add" runat="server" CausesValidation="False" CssClass="btn"
                                                        OnClick="btn_crm_add_Click" Text="Add" Width="59px" TabIndex="6" />
                                                    <asp:Button ID="btn_CRM_save" runat="server" CausesValidation="False" CssClass="btn"
                                                        OnClick="btn_CRM_save_Click" Text="Save" Visible="true" Width="59px" TabIndex="7" />
                                                    <asp:Button ID="btn_crm_Cancel" runat="server" CausesValidation="false" CssClass="btn"
                                                        OnClick="btn_crm_Cancel_Click" Text="Cancel" Visible="False" Width="59px" TabIndex="8" />
                                                    <asp:Button ID="btn_crm_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                         OnClientClick="javascript:CallPrint('divPrint');" TabIndex="9"
                                                        Text="Print" Width="59px" /></td>
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="p2" Visible="false">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td style="">
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;">
                                                        <legend><strong>Crew Availability</strong></legend>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="style1">
                                                                </td>
                                                                <td class="style3">
                                                                </td>
                                                                <td class="style5">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; " class="style2">
                                                                    Next Available Date :</td>
                                                                <td style="text-align: left; " class="style4">
                                                                    <asp:TextBox ID="txt_AvailableDt" runat="server" CssClass="required_box" MaxLength="20" TabIndex="4" Width="80px"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                <td style="text-align: left" class="style6">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style2">
                                                                </td>
                                                                <td style="text-align: left; " class="style4">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_AvailableDt" Display="Dynamic" ErrorMessage="Required" Width="57px"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_AvailableDt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                    </td>
                                                                <td class="style6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style2">
                                                                    Remark :</td>
                                                                <td class="style4" style="text-align: left; ">
                                                                    <asp:TextBox ID="txtAvlRem" runat="server" CssClass="input_box" Height="43px" 
                                                                        MaxLength="19" TabIndex="2" TextMode="MultiLine" Width="439px"></asp:TextBox>
                                                                </td>
                                                                <td class="style6">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style2">
                                                                    &nbsp;</td>
                                                                <td class="style4" style="text-align: left; ">
                                                                    &nbsp;</td>
                                                                <td class="style6">
                                                                    <asp:Button ID="Button2" runat="server" CssClass="btn" 
                                                                        OnClick="btn_Update_AvailabelDate_Click" TabIndex="7" Text="Update" 
                                                                        Visible="true" Width="59px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset><ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy"
                                                                            PopupButtonID="ImageButton2" TargetControlID="txt_AvailableDt" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="height: 18px; padding-right:2px">
                                                    </td>
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                    </asp:View>
                                    <asp:View ID="Tab2" runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center; width:100%" >
                                            <tr>
                                                <td>
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                        <legend><strong>Training Details</strong></legend>
                                                        <asp:Label ID="lbl_trainingrequirement_message" runat="server"></asp:Label>
                                                        <div style=" width:100%; height:250px; overflow-x:hidden;overflow-y:scroll;" > 
                                                            <asp:GridView ID="gvtrainingrequirement" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="TrainingRequirementId" OnPreRender="gvtrainingrequirement_PreRender"
                                                                OnRowDataBound="gvtrainingrequirement_RowDataBound" OnRowDeleting="gvtrainingrequirement_RowDeleting"
                                                                OnRowEditing="gvtrainingrequirement_RowEditing" OnSelectedIndexChanged="gvtrainingrequirement_SelectedIndexChanged"
                                                                Width="98%" GridLines="horizontal" OnRowCancelingEdit="gvtrainingrequirement_RowCancelingEdit" OnRowCommand="gvtrainingrequirement_RowCommand">
                                                                <RowStyle CssClass="rowstyle" />
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                        ShowSelectButton="True"><HeaderStyle Width="30px" /><ItemStyle Width="30px" /></asp:CommandField>
                                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                        ShowEditButton="True"><ItemStyle Width="30px" /></asp:CommandField>
                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" /><ItemTemplate><asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                Text="Delete" /></ItemTemplate></asp:TemplateField>
                                                                    
                                                                    <asp:BoundField DataField="N_TrainingTypeName" HeaderText="Training Type"><ItemStyle HorizontalAlign="Left" Width="180px"/></asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Training Name"><ItemTemplate><asp:HiddenField ID="HiddenTrainingType" runat="server" Value='<%#Eval("N_TrainingType")%>' /><asp:HiddenField ID="HiddenDueDate" runat="server" Value='<%#Eval("N_DueDate")%>' /><asp:HiddenField ID="HiddenTrainingStatus" runat="server" Value='<%#Eval("N_CrewTrainingStatus")%>' /><asp:HiddenField ID="HiddenTrainingVerified" runat="server" Value='<%#Eval("N_CrewVerified")%>' /><asp:HiddenField ID="HiddenVerifiedBy" runat="server" Value='<%#Eval("N_VerifiedBy")%>' /><asp:HiddenField ID="HiddenVerifiedOn" runat="server" Value='<%#Eval("N_VerifiedOn")%>' /><asp:HiddenField ID="htrainingid" runat="server" Value='<%#Eval("TrainingId")%>' /><asp:HiddenField ID="HiddenRemark" runat="server" Value='<%#Eval("Remark")%>' /><asp:Label ID="lbltraining" runat="server" Text='<%#Eval("TrainingName") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Left" /><HeaderStyle Width="180px" HorizontalAlign="Left" /></asp:TemplateField>

                                                                    <asp:BoundField DataField="RemarkShow" Visible="False" HeaderText="Remark"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                                    <asp:BoundField DataField="Attended" HeaderText="Attended"><ItemStyle HorizontalAlign="Left" Width="60px" /></asp:BoundField>
                                                                     <asp:TemplateField HeaderText="From Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                                                    <asp:BoundField DataField="Location" HeaderText="Rec.Ofice"/>
                                                                    <asp:BoundField Visible="False" DataField="TrainingCost" HeaderText="Investment ($)"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                                                     <asp:BoundField DataField="N_VerifiedBy" HeaderText="Verifed By"><ItemStyle Width="150px" HorizontalAlign="Left"/></asp:BoundField>
                                                                     <asp:BoundField DataField="N_VerifiedOn" HeaderText="Verified On"><ItemStyle Width="80px" /></asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="headerstylefixedheadergrid" />
                                                            </asp:GridView>
                                                        </div>
                                                    </fieldset>
                                                    <asp:Panel runat="server" id="trtrainingreqfields" Width="100%">
                                                    <br />
                                                    
                                                    <fieldset style="border-right: #8fafdb 1px solid;
                                                        border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; height: 265px; ">
                                                        <legend><strong>Training Details</strong></legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;padding-top: 5px; text-align: center; width: 100%;">
                                                            <tr>
                                                                <td style="height: 182px;">
                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td style="width: 242px; height: 19px; text-align: right">
                                                                                Training Type:</td>
                                                                            <td style="width: 172px; height: 19px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_TrainingType" runat="server" CssClass="required_box" Width="150px" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_TrainingType_SelectedIndexChanged">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 101px; height: 19px; text-align: right">
                                                                                Training Name:</td>
                                                                            <td style="width: 250px; height: 19px; text-align: left">
                                                                            <asp:DropDownList ID="ddl_TrainingReq_Training" runat="server" CssClass="required_box" Width="264px" TabIndex="2"></asp:DropDownList></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddl_TrainingType"
                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_TrainingReq_Training"
                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                                Due Date:</td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                                <asp:TextBox ID="txt_DueDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3"
                                                                                    Width="90px"></asp:TextBox>
                                                                                <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                                Training Status:</td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                                <asp:Label ID="lbl_TrainingStatus" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_crm_expirydate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                 </td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                                Training Verified:</td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                                <asp:CheckBox ID="chk_TrainingVerified" runat="server" /></td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                            </td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 242px; height: 53px;" valign="top">
                                                                                Remark:</td>
                                                                            <td colspan="3" style="height: 53px; text-align: left">
                                                                                <asp:TextBox ID="txt_Remark" runat="server" CssClass="input_box"
                                                                                    MaxLength="30" Width="530px" Height="47px" TextMode="MultiLine" TabIndex="2"></asp:TextBox>&nbsp;</td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                            </td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                          <tr>
                                                                            <td style="width: 242px; height: 16px; text-align: right">
                                                                            Verified By :
                                                                            </td>
                                                                            <td style="width: 172px; height: 16px; text-align: left">
                                                                                <asp:Label ID="lblVerifiedBy" runat="server"></asp:Label></td>
                                                                            <td style="width: 101px; height: 16px; text-align: right">
                                                                            Verified On :
                                                                            </td>
                                                                            <td style="width: 250px; height: 16px; text-align: left">
                                                                                <asp:Label ID="lblverifiedOn" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="height: 53px; text-align: left" valign="top">
                                                                            <b>&nbsp;&nbsp;Summary :</b>
                                                                            <hr width="98%"  />
                                                                            <table cellpadding="0" cellspacing="0" border ="0" width="100%" >
                                                                            <tr>
                                                                            <td style=" text-align: right" >Total Identified Training :</td>
                                                                            <td style=" text-align: left">&nbsp;<asp:Label runat="server" ID="lblTotalTraining"></asp:Label> </td>
                                                                            <td style=" text-align: right">Total Attended :</td>
                                                                            <td style=" text-align: left">&nbsp;<asp:Label runat="server" ID="lblTotalAttended"></asp:Label> </td>
                                                                            <td style=" text-align: right">Total Remaining :</td>
                                                                            <td style=" text-align: left">&nbsp;<asp:Label runat="server" ID="lblTotalRemaining"></asp:Label> </td>
                                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                            </tr>
                                                                            </table>
                                                                            
                                                                           </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                            
                                                    </fieldset></asp:Panel>
                                                    <table width="100%">
                                                        <tr>
                                                            <td id="TD1" runat="server" align="right" style="padding-top:14px;
                                                                text-align: right">
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                                                                                    PopupButtonID="ImageButton5" PopupPosition="TopLeft" TargetControlID="txt_DueDate"></ajaxToolkit:CalendarExtender>
                                                                <asp:HiddenField ID="htrainreqid" runat="server" />
                                                                &nbsp; &nbsp;
                                                                &nbsp; &nbsp;<asp:Button ID="btn_TrainingReq_Add" runat="server" CausesValidation="False"
                                                                    CssClass="btn" OnClick="btn_TrainingReq_Add_Click" Text="Add" Width="59px" TabIndex="3" />
                                                                <asp:Button ID="btn_TrainingReq_Save" runat="server" CssClass="btn" OnClick="btn_TrainingReq_Save_Click"
                                                                    Text="Save" Width="59px" TabIndex="4" />
                                                                <asp:Button ID="btn_TrainingReq_Cancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClick="btn_TrainingReq_Cancel_Click" Text="Cancel" Width="59px" TabIndex="5" />
                                                                <asp:Button ID="btn_TrainingReq_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClientClick="javascript:CallPrint('divPrint');" TabIndex="6"
                                                                    Text="Print" Width="59px" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="Tab3" runat="server">
                                       <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;
                                            text-align: center; width: 100%;" >
                                            <tr>
                                                <td style="height:auto" valign="top">
                                                    
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                        <legend><strong>Appraisal&nbsp; Details </strong></legend>
                                                        <asp:Label ID="lbl_apprasial_message" runat="server"></asp:Label>
                                                        <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" > 
                                                            <asp:GridView ID="gvApprasial" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewAppraisalId"
                                                                OnPreRender="gvApprasial_PreRender" OnRowDataBound="gvApprasial_RowDataBound"
                                                                OnRowDeleting="gvApprasial_RowDeleting" OnRowEditing="gvApprasial_RowEditing"
                                                                OnSelectedIndexChanged="gvApprasial_SelectedIndexChanged" Width="98%" GridLines="Horizontal">
                                                                <RowStyle CssClass="rowstyle" />
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                        ShowSelectButton="True"><HeaderStyle Width="30px" /><ItemStyle Width="30px" /></asp:CommandField>
                                                                        
                                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                        ShowEditButton="True"><ItemStyle Width="30px" /></asp:CommandField>
                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="30px" /><ItemTemplate><asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                Text="Delete" /></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=""><ItemTemplate><asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" /></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="30px" /></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Appraisal Occasion"><ItemTemplate><asp:HiddenField ID="HdocId" runat="server" Value='<%#Eval("AppraisalOccasionId")%>' /><asp:Label ID="lbldocument" runat="server" Text='<%#Eval("ApprasialOccasion") %>' Width="250px" ></asp:Label><asp:HiddenField ID="himag" runat="server" Value='<%#Eval("ImagePath") %>' /></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="70px" /><HeaderStyle HorizontalAlign="Left" Width="200px" /></asp:TemplateField>
                                                                    <asp:BoundField Visible="False" DataField="AvgMarks" HeaderText="Avg. Marks"><ItemStyle HorizontalAlign="Right" Width="110px" /></asp:BoundField>
                                                                    <%----------------------%>
                                                                    <asp:BoundField DataField="FromDate" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" HeaderText="Appraisal From" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                    <asp:BoundField DataField="ToDate" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" HeaderText="Appraisal To" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                    
                                                                    
                                                                    <asp:BoundField DataField="VesselName" HeaderText="Vessel"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                                                                    <%--<asp:BoundField DataField="N_RecommendedNew" HeaderText="Recommended"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>--%>
                                                                    <asp:BoundField Visible="False" DataField="AppraiserRemarks" HeaderText="Appraiser Remarks"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                     <asp:BoundField Visible="False" DataField="AppraiseeRemarks" HeaderText="Appraisee Remarks"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                    <%--<asp:BoundField DataField="OfficeRemarks" HeaderText="Office Remark"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>--%>
                                                                </Columns>
                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>
                                                    </fieldset>
                                                    <asp:Panel ID="trapprasial" runat="server" Width="100%">
                                                    <br />
                                                    <fieldset style="border-right: #8fafdb 1px solid;border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                        <legend><strong>Appraisal Details </strong></legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                            padding-top: 5px; height: auto; text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 1px; text-align: right">
                                                                                Occasion:</td>
                                                                            <td style="padding-left: 5px; width: 17%; height: 1px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_apprasial_occasion" runat="server" CssClass="required_box"
                                                                                    Width="153px" TabIndex="1">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td align="right" style="width: 17%; height: 1px; text-align: right">
                                                                                Period From:&nbsp;
                                                                                <%--Average Score:--%></td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_apprasial_from" runat="server" CssClass="required_box" MaxLength="20"
                                                                                    Width="90px" TabIndex="3"></asp:TextBox>
                                                                                <asp:ImageButton ID="img_apprasial_issue" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 1px; text-align: right">
                                                                                Period &nbsp;To:</td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasial_To" runat="server" CssClass="required_box" MaxLength="20"
                                                                                    Width="90px" TabIndex="4"></asp:TextBox>
                                                                                <asp:ImageButton ID="img_apprasial_expiry" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                                &nbsp;&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 17%; height: 1px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddl_apprasial_occasion"
                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                            <td align="right" style="width: 17%; height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 1px; text-align: left">
                                                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_apprasial_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txt_apprasial_from" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                         </td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 1px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 1px; text-align: left">
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_Apprasial_To" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_Apprasial_To" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                         </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 1px; text-align: right">
                                                                                Perf. Score:</td>
                                                                            <td style="padding-left: 5px; width: 17%; height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_PerfScore" runat="server" CssClass="input_box" MaxLength="49"
                                                                                    Width="102px" TabIndex="2"></asp:TextBox><asp:TextBox ID="txt_Apprasial_Score" runat="server" CssClass="required_box" MaxLength="5"
                                                                                    Width="102px" TabIndex="2" style="display: none"></asp:TextBox></td>
                                                                            <td align="right" style="width: 17%; height: 1px; text-align: right">
                                                                                Comp. Score :</td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_CompScore" runat="server" CssClass="input_box" MaxLength="49" TabIndex="5"
                                                                                    Width="102px"></asp:TextBox></td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 1px; text-align: right">
                                                                                Vessel :</td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 1px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box"
                                                                                    Width="153px" TabIndex="5">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 17%; height: 1px; text-align: left">
                                                                                <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Apprasial_Score"
                                                                                    ErrorMessage="Required." Width="55px"></asp:RequiredFieldValidator></td>
                                                                            <td align="right" style="width: 17%; height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 1px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 1px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 1px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 19px; text-align: right">
                                                                                Promo. Recommended :</td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 19px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_PromoRecomm" runat="server" CssClass="input_box" TabIndex="5" Width="107px">
                                                                                    <asp:ListItem Value="">< Select ></asp:ListItem>
                                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                                    <asp:ListItem Value="L">Latter</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:CheckBox ID="chk_Recommended" runat="server" style="display: none" TabIndex="6"/></td>
                                                                            <td align="right" style="width: 17%; height: 19px">
                                                                                Report# :</td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 19px; text-align: left">
                                                                                <asp:TextBox ID="txt_ReportNo" runat="server" CssClass="input_box" MaxLength="49" TabIndex="5"
                                                                                    Width="102px"></asp:TextBox></td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 19px; text-align: left">
                                                                            </td>
                                                                            <td rowspan="5" style="padding-left: 5px; width: 20%; text-align: center">
                                                                                <asp:Image ID="img_Apprasial" style="cursor:hand" ToolTip="Click to Preview" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" CausesValidation="false" runat="server" Height="90px" Width="60px" Visible="False" /><div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="Apprasial_fileupload" size="1" runat="server" style="left:-2px; position:relative; border:0px solid;" BackColor="#F9F9F9" Width="81px" />                                            
                                                                                                </div> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 3px; text-align: right">
                                                                            </td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                &nbsp;</td>
                                                                            <td align="right" style="width: 17%; height: 3px">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 3px; text-align: left">
                                                                                </td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="vertical-align: top; width: 64%; height: 3px; text-align: right">
                                                                                Office Remarks:</td>
                                                                            <td colspan="4" rowspan="1" style="padding-left: 5px; height: 3px; text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasial_OfficeRemarks" runat="server" CssClass="input_box"
                                                                                    MaxLength="50" Rows="2" TextMode="MultiLine" Width="450px" TabIndex="9" Height="84px"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 3px; text-align: right">
                                                                            </td>
                                                                            <td colspan="4" rowspan="1" style="padding-left: 5px; height: 3px; text-align: left">
                                                                </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 3px; text-align: right">
                                                                                Training Required :</td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_TrainingReq" runat="server" TabIndex="5" CssClass="input_box" Width="107px">
                                                                                <asp:ListItem Value="">< Select ></asp:ListItem>
                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                            <td align="right" style="width: 17%; height: 3px">
                                                                                Date Join Company :</td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 3px; text-align: left">
                                                                                <asp:Label ID="lbl_DJC" runat="server"></asp:Label></td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 3px; text-align: right">
                                                                                </td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                &nbsp;</td>
                                                                            <td align="right" style="width: 17%; height: 3px">
                                                                                </td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 3px; text-align: left">
                                                                                </td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 3px; text-align: right">
                                                                                Updated By :</td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                <asp:Label ID="lbl_UpdatedBy" runat="server"></asp:Label></td>
                                                                            <td align="right" style="width: 17%; height: 3px">
                                                                                Updated On :</td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 3px; text-align: left">
                                                                                <asp:Label ID="lbl_UpdatedOn" runat="server"></asp:Label></td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 3px; text-align: left">
                                                                                <asp:Button ID="btn_ShowTrainings" Visible="false" runat="server" OnClick="btn_ShowTrainings_Click" Text="View Trainings" Width="100px" CssClass="btn" CausesValidation="False" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%; height: 3px; text-align: right">
                                                                            </td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="right" style="width: 17%; height: 3px">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 19%; height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 10%; height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 20%; height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%;">
                                                                                <%--Appraisier Remarks:&nbsp;--%></td>
                                                                            <td colspan="3" style="padding-left: 5px; width: 24%; text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasial_remarks" runat="server" CssClass="input_box" MaxLength="50"
                                                                                    Rows="2" TextMode="MultiLine" Width="450px" TabIndex="7" style="display: none"></asp:TextBox></td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 10%; text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 24%; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%;">
                                                                            </td>
                                                                            <td colspan="3" style="padding-left: 5px; width: 24%; text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 10%; text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 24%; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%;">
                                                                                <%--Appraisee's Remark:--%></td>
                                                                            <td colspan="3" style="padding-left: 5px; width: 24%; text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasiee_remarks" runat="server" CssClass="input_box" MaxLength="50"
                                                                                    Rows="2" TextMode="MultiLine" Width="450px" TabIndex="8" style="display: none"></asp:TextBox></td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 10%; text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 24%; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 64%;">
                                                                            </td>
                                                                            <td colspan="3" style="padding-left: 5px; width: 24%; text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 10%; text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px; width: 24%; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                      <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" >
                                                       <asp:GridView ID="gvTrainingsWithAppraisal" runat="server" AutoGenerateColumns="False" Width="98%" GridLines="horizontal">
                                                        <RowStyle CssClass="rowstyle" />
                                                        <PagerStyle CssClass="pagerstyle" />
                                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                                        <Columns>
                                                        <asp:BoundField DataField="N_TrainingTypeName" HeaderText="Training Type"><ItemStyle HorizontalAlign="Left" Width="300px" /></asp:BoundField>
                                                        <asp:BoundField DataField="TrainingName" HeaderText="Training Name"><ItemStyle HorizontalAlign="Left" Width="300px" /></asp:BoundField>
                                                        <asp:BoundField Visible="False" DataField="TrainingCost" HeaderText="Investment ($)"><ItemStyle HorizontalAlign="Right" Width="110px" /></asp:BoundField>
                                                         <asp:TemplateField HeaderText="Due Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                                        <asp:BoundField DataField="Location"  HeaderText="Rec. Office" ><ItemStyle Width="100px" /></asp:BoundField>
                                                        <asp:BoundField DataField="Attended" HeaderText="Attended"><ItemStyle HorizontalAlign="Left" Width="80px" /></asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="headerstylefixedheadergrid" />
                                                    </asp:GridView>
                                                    </div>
                                                    </asp:Panel>
                                                    <table style="background-color: #f9f9f9" width="100%">
                                                        <tr>
                                                            <td id="Td2" runat="server" align="right" style="height: auto;padding-top:14px;
                                                                text-align: right" valign="top">
                                                                                
                                                                               
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                                                    PopupButtonID="img_apprasial_issue" PopupPosition="TopLeft" TargetControlID="txt_apprasial_from"></ajaxToolkit:CalendarExtender>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                                    FilterType="Numbers,Custom" TargetControlID="txt_Apprasial_Score" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                                                    PopupButtonID="img_apprasial_expiry" PopupPosition="TopLeft" TargetControlID="txt_Apprasial_To"></ajaxToolkit:CalendarExtender>
                                                                <asp:HiddenField ID="h_apprasial_hfile" runat="server" />
                                                                <asp:HiddenField ID="h_apprasial_id" runat="server" />
                                                                &nbsp; &nbsp;
                                                                &nbsp; &nbsp;<asp:Button ID="btn_Apprasial_Add" runat="server" CausesValidation="False"
                                                                    CssClass="btn" OnClick="btn_Apprasial_Add_Click" Text="Add" Width="59px" TabIndex="10" />
                                                                <asp:Button ID="btn_Apprasial_Save" runat="server" CssClass="btn" OnClick="btn_Apprasial_Save_Click"
                                                                    Text="Save" Width="59px" TabIndex="11" />
                                                                <asp:Button ID="btn_Apprasial_Cancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClick="btn_Apprasial_Cancel_Click" Text="Cancel" Width="59px" TabIndex="12" />
                                                                <asp:Button ID="btn_Apprasial_Print" runat="server" CausesValidation="False" CssClass="btn" 
                                                                    OnClientClick="javascript:CallPrint('divPrint');" TabIndex="13" Text="Print" Width="59px" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="Tab4" runat="server">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList AutoPostBack="true" ID="rbhistory" runat="server" RepeatDirection="Horizontal" Font-Bold="True" OnSelectedIndexChanged="rbhistory_SelectedIndexChanged">
                                                        <%-- <asp:ListItem >Contract Details</asp:ListItem>
                                                       <asp:ListItem>Recoverable Expenses</asp:ListItem>--%>
                                                        <asp:ListItem Selected="True">Summary Of Experience</asp:ListItem>
                                                        <asp:ListItem>Office Visit</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:Panel ID="pnl_history_1" runat="server" Height="100%" Width="100%">
                                                       <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Contract Details</strong></legend>
                                                                        <asp:Label ID="lbl_contract_message" runat="server"></asp:Label>
                                                                         <div style=" width:100%; height:165px; overflow-y: scroll; overflow-x: hidden;" > 
<asp:GridView style="TEXT-ALIGN: center" id="gv_Contract" runat="server" Width="98%" GridLines="Horizontal" AutoGenerateColumns="False"  OnSelectedIndexChanged="gv_Contract_SelectedIndexChanged" OnPreRender="gv_Contract_PreRender" OnRowCancelingEdit="gv_Contract_RowCancelingEdit" OnRowCommand="gv_Contract_RowCommand">
                                         <Columns>
                                           <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True"><ItemStyle Width="30px" /></asp:CommandField>
                                    
                                              <asp:TemplateField HeaderText="Edit">
                                                                <ItemStyle Width="25px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Imgbtngv_ContractEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Ref.#" ><ItemTemplate>
                                                <asp:Label ID="lbl_Contractid" runat="server" Text='<%# Eval("RefNo") %>'>
                                                </asp:Label><asp:HiddenField ID="hfd_ContractId" runat="server" Value='<%# Eval("ContractId") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hfd_OtherAmount" runat="server" Value='<%# Eval("OtherAmount") %>' />
                                                <asp:HiddenField ID="hfContractStauts" runat="server" Value='<%# Eval("Status") %>' />
                                             </ItemTemplate>
                                             <ItemStyle Width="70px" />
                                          </asp:TemplateField>
                                             <asp:BoundField DataField="Name" HeaderText="Name" ></asp:BoundField>
                                             <asp:BoundField DataField="RankCode" HeaderText="Planned Rank" ></asp:BoundField>
                                              <asp:BoundField DataField="NewRankCode" HeaderText="Rank" ></asp:BoundField>
                                              <asp:BoundField DataField="VesselName" HeaderText="Vessel" ></asp:BoundField>
                                              <asp:TemplateField HeaderText="Issue Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Start Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                             <asp:BoundField DataField="CountryName" HeaderText="Nationality" ></asp:BoundField>
                                             <asp:BoundField DataField="Status" HeaderText="Status" ></asp:BoundField>
                                         </Columns>
                                         <RowStyle CssClass="rowstyle" HorizontalAlign="Left"  />
                                         <SelectedRowStyle CssClass="selectedtowstyle"  />
                                         <HeaderStyle CssClass="headerstylefixedheadergrid"   />
                                     </asp:GridView><asp:HiddenField ID="hfcontractid" runat="server" />
                                                                        </div>
                                                                    </fieldset>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                          <tr id="trcontractdetails" runat="server"><td style="padding-bottom: 0px; padding-top: 0px;" align="right">
                                                                          
                                                                                  <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center">
             <br />
             <table cellpadding="0" cellspacing="0" width="100%" border="1" >
                <tr>
                    <td>
                        <div style="color:Black;width:100%; background-color:#c2c2c2; font-size:13px; padding-bottom:4px;"><b>Crew Details</b></div>
                    </td>
                    <td>
                        <div style="color:Black;width:100%; background-color:#c2c2c2; font-size:13px; padding-bottom:4px;"><b>Wages Details</b>&nbsp;</div>
                    </td>
                </tr>
                <tr>
                    <td style="width:400px; font-size:12px;vertical-align:top;">
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="text-align:left;" >
                             <tr>
                                 <td align="right" >Crew Name : </td>
                                 <td >
                                    <asp:Label ID="lb_name" runat="server" Width="100%"></asp:Label>
                                 </td>
                                 
                             </tr>
                             <tr>
                                <td align="right">Planned Vessel : </td>
                                 <td >
                                    <asp:Label ID="lb_PlanVessel" runat="server" Width="100%"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <td align="right">Planned Rank : </td>
                                 <td >
                                    <asp:Label ID="Lb_PlanRank" runat="server" Width="100%"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <td align="right">Nationality : </td>
                                <td >
                                     <asp:Label ID="txt_contact_nationality" runat="server" Width="100%" ></asp:Label>
                                </td>
                             </tr>
                              <tr>
                                <td align="right">
                                    Wage Scale : 
                                    </td>
                                 <td >
                                     <asp:DropDownList ID="dp_wagescale" runat="server"  ValidationGroup="abc" Enabled="false" Width="200px" CssClass="input_box"></asp:DropDownList>
                                     <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="dp_wagescale" ErrorMessage="*" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                 </td>                     
                             </tr>
                                   <tr>
                                <td align="right">Contract Period(nos.) : </td>
                                 <td ><asp:Label ID="Txt_ContractPeriod" runat="server" Width="100%"></asp:Label></td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     Seniority(Year) : </td>
                                 <td >
                                     <asp:Label ID="Txt_Seniority" runat="server" Width="100%"></asp:Label></td>
                             </tr>
                             <tr>
                                 <td align="right"> Issue Date : </td>
                                 <td >
                                     <asp:Label ID="txt_IssueDate" runat="server"  Width="100%"></asp:Label>
                                 </td>
                                 
                             </tr>
                             <tr>
                                <td align="right"> Start Date : </td>
                                 <td >
                                     <asp:Label ID="txt_StartDate" runat="server"  Width="100%"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right" >
                                     &nbsp; Rank : </td>
                                 <td >
                                     <asp:Label ID="txt_contract_rank" runat="server" Width="100%"></asp:Label></td>
                                 
                             </tr>
                             <tr runat="server" visible="false">
                                <td align="right">
                                     Ref. No. : </td>
                                 <td >
                                     <asp:Label ID="lbl_RefNo" runat="server"></asp:Label></td>
                                 
                             </tr>
                             <tr runat="server" visible="false">
                                <td align="right" >
                                     Other Amount : </td>
                                 <td >
                                     <asp:Label ID="txt_OtherAmount" runat="server"  Width="100%"></asp:Label></td>
                             </tr>
                             <tr style="color: #0e64a0">
                                 <td >
                                     <asp:Label ID="txt_wagescale" runat="server" Width="100%" Height="35px" Visible="False"></asp:Label></td>
                                 <td >
                                    

                                 </td>
                             </tr>
                            <tr id="Tr1" runat="server" visible="false">
                                 <td align="right" >Sup. Cert. Allow. : </td>
                                 <td ><asp:CheckBox ID="chk_SupCert" runat="server" Enabled="false" /></td>                     
                             </tr>
                             
                         </table>           
                    </td>
                    <td style="vertical-align:top;">
                        <table width="100%">
                            <tr>
                                <td style="text-align:center;">
                                    <b> Earnings </b>
                                </td>
                                <td style="text-align:center;">
                                    <b> Deductions </b>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%">
                                   
                                    <asp:GridView ID="Gd_AssignWages"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                            Width="100%">
                             <Columns>
                               <asp:TemplateField HeaderText="Component Type" Visible="false">
                               <ItemTemplate>
                                 <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' /> 
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"  />               
                               <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right">
                               <ItemTemplate >   
                                <asp:TextBox runat="server" onchange="Show_Amount(); " Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" style="text-align :right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>' ></asp:TextBox>
                                <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' /> 
                                <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractId")%>' /> 
                                
                               </ItemTemplate>
                               </asp:TemplateField> 
                            </Columns>
                            <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                        </asp:GridView>
                                        
                                </td>
                                <td width="50%">
                                  
                                    <asp:GridView ID="Gd_AssignWagesDeductions"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                            Width="100%">
                             <Columns>
                               <asp:TemplateField HeaderText="Component Type" Visible="false">
                               <ItemTemplate>
                                 <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' /> 
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="ComponentName" HeaderText="Wage Component" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"  />               
                               <asp:TemplateField  HeaderText="" ItemStyle-HorizontalAlign="Right">
                               <ItemTemplate >   
                                <asp:TextBox runat="server" onchange="Show_Amount(); " Wid='<%#Eval("WageScaleComponentId")%>' Width="80px" style="text-align :right" CssClass="input_box" ID="txtAmount" Text='<%#Eval("WageScaleComponentValue") %>' ></asp:TextBox>
                                <asp:HiddenField ID="hfdCompId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' /> 
                                <asp:HiddenField ID="hfContractID" runat="server" Value='<%#Eval("ContractId")%>' /> 
                                
                               </ItemTemplate>
                               </asp:TemplateField> 
                            </Columns>
                            <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                        </asp:GridView>
                                       
                                </td>
                            </tr>
                        </table>
                           
                    
                         <table width="100%" cellpadding="2" cellspacing="0" style="text-align : center; border-collapse:collapse;margin-top:10px;" bordercolor="Gray" border="1">
                                <tr>
                                    <td style="text-align: left;width:270px; ">EXTRA ALLOW (M) :</td>
                                    <td style="text-align: right;" colspan="3"><b><asp:TextBox ID="txt_Other_Amount" style="text-align :right " onkeyup="Show_Amount();" CssClass="input_box" width="80px" runat="server"></asp:TextBox></b></td>
                                   
                                </tr>
                                
                                <tr>
                                    <td style="text-align: left;"><b>Net Earnings (Total Earning - Total Deduction) :</b></td>
                                    <td style="text-align: left;padding-left:5px;" colspan="3"><asp:Label Font-Bold="true" ID="lb_NewEarning1" ForeColor="Green" runat="server" Width="80px"></asp:Label></td>
                                </tr>
                             <tr>
                                    <td style="text-align: left;width:270px; ">EXTRA OT RATE (Hourly) :</td>
                                    <td style="text-align: right;width:150px; "><b><asp:TextBox ID="txtExtraOTRate" style="text-align :right "  CssClass="input_box" width="80px" runat="server"></asp:TextBox></b></td>
                                     <td style="text-align: left;width:150px; "> Travel Pay (Per Day) :</td>
                                    <td style="text-align: left; "> <asp:DropDownList ID="ddlTravelPay" runat="server" Width="150px">
                                        <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem  Text="Full Salary Basis" Value="1"></asp:ListItem>
                                        <asp:ListItem  Text="Basic Wages Basis" Value="2"></asp:ListItem>
                                      
                                                                     </asp:DropDownList></td>
                                </tr>
                        </table><b>Remarks</b>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="input_box" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                        <br />
                            
                            <b>  <asp:Label ID="lblLockbyOn" runat="server" ></asp:Label> </b>
                           
                            <asp:Button ID="btn_contract_cancel" runat="server" CausesValidation="False" CssClass="btn" Text="Cancel" Width="59px" TabIndex="10" OnClick="btn_contract_cancel_Click" style="float:right;margin:1px;"  />
                            <asp:Button ID="btnUpdateWages" runat="server" Text="Update Wages" CssClass="btn" OnClick="btnUpdateWages_OnClick" style="float:right;margin:1px;" CausesValidation="false"  />
                         <asp:Button ID="btnConRevision" runat="server" Text="Contract Revision" CssClass="btn"  style="float:right;margin:1px;" CausesValidation="false" OnClick="btnConRevision_Click"  />
                            <asp:Label ID="lblMsgWages" runat="server" ForeColor="Red" style="float:right;margin:1px;" ></asp:Label>
                        </td>
                    </tr>
                 <tr>
                     <td colspan="2">
                         <table style="width:100%;">
                             <tr>
                                 <td style="width:150px; text-align:right; padding-right:5px;">
                                     <asp:Label ID="lblContractTemplate" runat="server" Text="Contract Template :"></asp:Label>
                                 </td>
                                 <td style="width:275px; text-align:Left; padding-Left:10px;">
                                     <asp:DropDownList ID="ddl_ContractTemplate" runat="server" width="200px" CssClass="required_box"  > 
                                         <asp:ListItem Value="0">< Select ></asp:ListItem>
                                     </asp:DropDownList>
                                     &nbsp;
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddl_ContractTemplate"
                                                                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                 </td>
                                 <td style="text-align:Left; padding-Left:10px;">
                                      <asp:Button ID="btn_License_Print" runat="server" CssClass="btn"  Text="Print" Width="70px" OnClick="btn_License_Print_Click" CausesValidation="false" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                    
                 </tr>
                 </table>
             
             
             
             
             
             
             
    </fieldset>
                                                                              <br />
                                                                              <asp:HiddenField ID="HiddenField_NationalityId" runat="server" />
                                                                              <asp:HiddenField ID="HiddenField_RankId" runat="server" />
                                                                                  <br />
                                                                                  <br />
                                                                                  <table style="width:100%;">
                                                                                      <tr>
                                                                                          <td style="text-align: left; width :50%">
                                                                                              
                                                                                          </td>
                                                                                          <td style="width :50%" >
                                                                                          </td>
                                                                                      </tr>
                                                                                  </table>
                                                                                  <br />
    </td></tr></table>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="text-align: right;">
                                                                                <asp:HiddenField ID="hfd_vesselid" runat="server" />
                                                                                <asp:Button style="display:none" ID="btn_CrewExt" runat="server" CausesValidation="False" CssClass="btn" Text="Crew Extension" Width="100px" TabIndex="10" OnClick="btn_CrewExt_Click" />
                                                                                <asp:Button Visible="false" ID="btn_close" runat="server" CausesValidation="False" CssClass="btn" Text="Close Contract" Width="100px" TabIndex="10" OnClick="btn_close_Click"  OnClientClick="return confirm('Are you Sure to Close this Contract?')" />
                                                                                <asp:Button ID="btn_ContractSave" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="10" OnClick="btn_contract_Save_Click"  />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnl_history_2" runat="server" Height="100%" Width="100%">
                                                          <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Recoverable Expenses</strong></legend>
                                                                        <asp:Label ID="lblrecoverableexpenses" runat="server"></asp:Label>
                                                                         <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" > <asp:GridView  ShowFooter="true" style="TEXT-ALIGN: center" id="gv_RecoverableExpenses" runat="server" Width="98%" GridLines="Horizontal" AutoGenerateColumns="False" OnPreRender="gv_RecoverableExpenses_PreRender">
                                                                         <Columns>
                                                                            <asp:BoundField DataField="accountcode" HeaderText="Account Head Code"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                                            <asp:BoundField DataField="accountheadname" HeaderText="Account Head Name"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                                            <asp:BoundField DataField="totalamount" HeaderText="Recoverable Amount"  DataFormatString="{0:0.00}" HtmlEncode=False><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                                            </Columns>
                                                                            <RowStyle CssClass="rowstyle" HorizontalAlign="Left"  />
                                                                            <SelectedRowStyle CssClass="selectedtowstyle"  />
                                                                            <HeaderStyle CssClass="headerstylefixedheadergrid"   />
                                                                         </asp:GridView>
                                                                        </div>
                                                                    </fieldset>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnl_history_3" runat="server" Height="100%" Width="100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Summary Of Experience</strong></legend>
                                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                        <br />
                                                                        <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" > 
                                         <asp:GridView  ShowFooter="true" style="TEXT-ALIGN: center" id="GVSummaryExperience" runat="server" Width="98%" GridLines="Horizontal" AutoGenerateColumns="False" OnSelectedIndexChanged="gv_Contract_SelectedIndexChanged" OnPreRender="gv_Contract_PreRender" OnRowDataBound="GVSummaryExperience_RowDataBound">
                                         <Columns>
                                         <asp:TemplateField HeaderText="Sr.No."><ItemStyle HorizontalAlign="Right" Width="10%" /><ItemTemplate><asp:Label ID="lbsrno"  runat="server" Text=''></asp:Label></ItemTemplate></asp:TemplateField>
                                             <asp:BoundField DataField="Vessel" HeaderText="Vessel Name"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                             <asp:BoundField DataField="Rank" HeaderText="Rank" ><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                             <asp:TemplateField HeaderText="Total Sea Time(Months)">
                                                 <ItemStyle HorizontalAlign="Left"/>
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbexperience"  runat="server" Text='<%# Eval("TotalExp") %>'></asp:Label>

                                                 </ItemTemplate>
                                                 <FooterTemplate><asp:Label ID="lbtotalexp"  runat="server" Text=''></asp:Label></FooterTemplate><FooterStyle HorizontalAlign="Left"/></asp:TemplateField>
                                              
                                         </Columns>
                                         <RowStyle CssClass="rowstyle" HorizontalAlign="Left"  />
                                         <SelectedRowStyle CssClass="selectedtowstyle"  />
                                         <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                                     </asp:GridView>
                                                                        </div>
                                                                    </fieldset>
                                                                    <br />
                                                                   
                                                               
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnl_history_4" runat="server" Height="100%" Width="100%" >
                                                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Summary Of Office Visits</strong></legend>
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                        <br />
                                                                        <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" >
                                                                         <asp:GridView style="TEXT-ALIGN: center" id="grdOfficeVisit" runat="server" 
                                                                                Width="98%" GridLines="Horizontal" AutoGenerateColumns="False" >
                                                                             <Columns>
                                                                                 <asp:TemplateField HeaderText="View">
                                                                                     <ItemTemplate>
                                                                                          <a runat="server" ID="ancReport"  href='<%#"http://emanagershore.energiosmaritime.com/vims/Reports/OfficeVisitReport.aspx?VisitId=" + Eval("Id").ToString() + "&Qp=" + Eval("IntCreatedOn").ToString()%>'  target="_blank" visible='<%#Eval("Status").ToString()== "Closed" %>' title="Show Report" >
                                                                                           <img src="../Images/HourGlass.gif" style="border:none"  /></a>                                                      
                                                                                     </ItemTemplate>
                                                                                     <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                 </asp:TemplateField>                                         
                                                                                 <asp:TemplateField HeaderText="Sr.No."><ItemStyle HorizontalAlign="Center" Width="40px" /><ItemTemplate><asp:Label ID="lblSrNo"  runat="server" Text='<%#Eval("SrNo") %>'></asp:Label><asp:HiddenField ID="hfId" Value='<%#Eval("Id") %>' runat="server" /></ItemTemplate></asp:TemplateField>
                                                                                 <asp:BoundField DataField="Occasion" HeaderText="Occasion" ><ItemStyle HorizontalAlign="Left" Width="150px"/></asp:BoundField>
                                                                                 <asp:BoundField DataField="VesselName" HeaderText="Vessel Name"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                                                 <asp:BoundField DataField="FromDate" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" HeaderText="From Date" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                                 <asp:BoundField DataField="ToDate" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" HeaderText="To Date" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                                 <asp:BoundField DataField="TotalFollowUp" HeaderText="Total Followup" ><ItemStyle HorizontalAlign="Center" Width="150px" /></asp:BoundField>
                                                                                 <asp:BoundField DataField="OpenFollowUp" HeaderText="Open Followup" ><ItemStyle HorizontalAlign="Center" Width="150px" /></asp:BoundField>
                                                                                 <asp:BoundField DataField="Status" HeaderText="Status" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                             </Columns>
                                                                             <RowStyle CssClass="rowstyle" HorizontalAlign="Left"  />
                                                                             <SelectedRowStyle CssClass="selectedtowstyle"  />
                                                                             <HeaderStyle CssClass="headerstylefixedheadergrid"   />
                                                                         </asp:GridView> 
                                                                        </div>
                                                                         </fieldset>
                                                                    <br />                                                                  
                                                               
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                            <td style="text-align:left; padding-left:5px "><asp:Label ID="lblTotRecord" runat="server"></asp:Label>
                                                                <asp:Button ID="btn_refresh" runat="server" style="display:none" OnClick="btn_refresh_Click" />
                                                                </td>
                                                            <td style="padding-right:5px"><asp:Button ID="btnNewVisit" Text="New Visit" style="float:right" CssClass="btn" OnClientClick="javascript:opennewvisit();" runat="server" /></td></tr>
                                                        </table>                                                    
                                                    </asp:Panel>
                                                      <div style="position:absolute;top:50px;left:0px; height :300px; width:100%;" id="dvContractRevision" runat="server" visible="false">
                                    <center>
                                        <div style="position:absolute;top:0px;left:0px; height :290px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                         <div style="position :relative;width:800px; height:280px;padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 0px #4371a5;">
                                         <center >
                                         <div  class="text headerband">
                                          <b>  Contract Revision </b>
                                         </div>
                                         <div style=' height:270px; margin-left:10px;'>
                                         <table cellspacing="0" width="100%" border="1" >
                                        <tr>
                                            <td style=" text-align :center ">
                                                <asp:Label ID="lblContractRevisionmessage" runat="server" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td runat="server" id="trContractRevision">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 15px; text-align :right ">
                                                    Emp.#:</td>
                                                <td style="text-align :left">
                                                    <asp:TextBox ID="txtPEmpNo" MaxLength="6" runat="server" CssClass="input_box" 
                                                        Enabled="false" TabIndex="1"></asp:TextBox></td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Name:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPName" runat="server" Width="100%"></asp:Label></td>
                                                
                                            </tr>
                                            
                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    Current Rank:</td>
                                                <td  style="text-align: left;">
                                                    <asp:Label ID="lblPPresentRank" runat="server"></asp:Label>
                                                    </td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Status:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPStatus" runat="server" ></asp:Label></td>
                                               
                                            </tr>
                                           
                                            <tr>
                                                 <td style="padding-right: 15px;text-align :right ">
                                                    Vessel:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPVessel" runat="server" ></asp:Label></td>
                                                
                                                <td style="padding-right: 15px;text-align :right ">
                                                 Contract Revision Start Dt:  </td>
                                                <td style="text-align :left">
                                                    <asp:TextBox ID="txt_ContRevisionDt" runat="server" CssClass="required_box" 
                                                        MaxLength="15" Width="80px" TabIndex="3"></asp:TextBox>
                                                    <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                   </td>
                                                
                                            </tr>
                                            <tr>
                                                <td ><asp:Label ID="lblPAvailableDate" runat="server" Width="122px" Visible="false"></asp:Label></td>
                                                <td > <asp:Label ID="lblPSignedOff" runat="server" Visible="false" ></asp:Label></td>
                                                <td ></td>
                                                <td style="text-align: left">   <asp:RequiredFieldValidator runat="server" ID="Req1" Display="Dynamic" 
                                                        ControlToValidate="txt_ContRevisionDt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                                        ControlToValidate="txt_ContRevisionDt" 
                                                        ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" 
                                                        ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> 
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" 
                                            TargetControlID="txt_ContRevisionDt"></ajaxToolkit:CalendarExtender>
                                                </td>
                                               
                                                </tr>
                                                <tr>
                                                <td colspan="4" style="border: solid 1px gray; text-align :right ; height :30px;" >
                                                <asp:Button ID="btn_SaveContractRevision" runat="server" CssClass="btn"  Text="Save" Width="80px" CausesValidation="false" OnClick="btn_SaveContractRevision_Click"/>
                                        &nbsp;   <asp:Button runat="server" ID="btnCloseContractRevision" Text="Close" CssClass="btn" OnClick="btnCloseContractRevision_Click"  Width='100px' CausesValidation="false"/>
                                        
                                                </td>
                                                </tr>
                                           
                                        </table>
                                        </td>
                                     </tr>
                               
                            </table>
                                         </div>
                                      
                                         </center>
                                         </div>
                                    </center>
                                    </div>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </asp:View>
                                    <asp:View ID="Tab5" runat="server">
                                    <asp:UpdatePanel runat="server" ID="up001">
                                    <ContentTemplate>
                                    <div>
                                    <div style="font-weight:bold; padding-bottom:5px;">
                                        <asp:RadioButton Text="Trainings to be completed during onboard" runat="server" id="radR" Visible="false" GroupName="A" OnCheckedChanged="ShowTrainings" AutoPostBack="true"/>
                                        <asp:RadioButton Text="Total Trainings Due" runat="server" id="radD" GroupName="A" OnCheckedChanged="ShowTrainings" Checked="true"  AutoPostBack="true"/>
                                        <asp:RadioButton Text="Trainings Done" runat="server" id="radA" GroupName="A" OnCheckedChanged="ShowTrainings" AutoPostBack="true"/>
                                    </div>
                                    <table cellpadding="0" cellspacing="0"  width="100%" border="1" style="border:solid 1px #c2c2c2;border-collapse:collapse;">
                                    <tr>
                                    <td>
                                          <div runat="server" id="dv_Due">
                                              <div style="overflow-x:hidden; overflow-y:scroll; height:20px;">
                                                  <table cellpadding="3" cellspacing="0"  width="100%" border="1" style="border:solid 1px #c2c2c2;border-collapse:collapse;">
                                                  <tr class= "headerstylegrid">
                                                    <td style="width:30px;">SR#</td>
                                                    <td style="">Training Name&nbsp;&nbsp;<asp:Label runat="server" ID="lblDueCnt"></asp:Label></td>
                                                    <td style="width:80px;">Last Done</td>
                                                    <td style="width:80px;">Source</td>
                                                    <td style="width:80px;">Next Due</td>
                                                    <td style="width:80px;">Plan Date</td>
                                                    <td style="width:20px;">&nbsp;</td>
                                                  </tr>
                                                  </table>
                                              </div>
                                              <div style="overflow-x:hidden; overflow-y:scroll; height:260px;">
                                                  <table cellpadding="3" cellspacing="0"  width="100%" border="1" style="border:solid 1px #c2c2c2;border-collapse:collapse;">
                                                  <asp:Repeater runat="server" ID="rpt_Due">
                                                  <ItemTemplate>
                                                    <tr>
                                                        <td style="width:30px; text-align:center;"><%#Eval("SNO").ToString()%></td>   
                                                        <td style="color:Blue;"><%#Eval("TrainingName").ToString().Replace(",","<b style='color:red'> OR </b>")%></td>
                                                        <td style="width:80px;"><%#Common.ToDateString(Eval("LastDoneDate"))%></td>
                                                        <td style="width:80px;"><%#Eval("Source")%></td>
                                                        <td style="width:80px;<%#(Common.ToDateString(Eval("DueDate"))!="")?( ( Convert.ToDateTime(Eval("DueDate")) < DateTime.Today )?"background-color:red":"background-color:green" ):""%>"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                        <td style='width:80px;<%#(Common.ToDateString(Eval("PlanDate"))!="")?"background-color:yellow":""%>'><%#Common.ToDateString(Eval("PlanDate"))%></td>
                                                        <td style="width:20px;">&nbsp;</td>
                                                    </tr>
                                                 </ItemTemplate>
                                                  </asp:Repeater>
                                                  </table>
                                              </div>
                                          </div>
                                          <div runat="server" id="dvDone" visible="false" >
                                              <div style="overflow-x:hidden; overflow-y:scroll; height:20px;">
                                                  <table cellpadding="3" cellspacing="0"  width="100%" border="1" style="border:solid 1px #c2c2c2;border-collapse:collapse;">
                                                  <tr class= "headerstylegrid">
                                                    <td style="width:30px;">SR#</td>
                                                    <td style="">Training Name&nbsp;&nbsp;<asp:Label runat="server" ID="lblDoneCnt"></asp:Label></td>
                                                    <td style="width:100px;">Location</td>
                                                    <td style="width:80px;">Done Date</td>
                                                    <td style="width:80px;">Source</td>
                                                    <td style="width:60px;">Delete</td>
                                                    <td style="width:20px;">&nbsp;</td>
                                                  </tr>
                                                  </table>
                                              </div>
                                              <div style="overflow-x:hidden; overflow-y:scroll; height:260px;">
                                                  <table cellpadding="3" cellspacing="0"  width="100%" border="1" style="border:solid 1px #c2c2c2;border-collapse:collapse;">
                                                  <asp:Repeater runat="server" ID="rpt_Done">
                                                  <ItemTemplate>
                                                    <tr>
                                                        <td style="width:30px;text-align:center;"><%#Eval("SNO").ToString()%></td>   
                                                        <td style="color:Blue;"><%#Eval("TrainingName").ToString()%></td>
                                                        <td style="width:100px"><%#Eval("Location").ToString()%></td>
                                                        <td style="width:80px;"><%#Common.ToDateString(Eval("TODATE"))%></td>
                                                        <td style="width:80px;"><%#Eval("Source")%></td>
                                                        <td style="width:60px; text-align:center;"> <asp:ImageButton runat="server" ID="btnDel" CommandArgument='<%#Eval("TrainingRequirementId").ToString()%>' Visible='<%#TrainingDelete && Common.CastAsInt32(Eval("DUPLICATE"))>1 %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDel_Click" OnClientClick="return ConfirmDelete(this);" /></td>
                                                        <td style="width:20px;">&nbsp;</td>
                                                    </tr>
                                                 </ItemTemplate>
                                                  </asp:Repeater>
                                                  </table>
                                              </div>
                                          </div>
                                    </td>
                                    <td style="display:none">
                                        <asp:Literal runat="server" ID="litTraining"></asp:Literal>
                                    </td>
                                    </tr>
                                    </table>
                                    </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                        
                                    <table style="border-collapse:collapse;" cellpadding="1" cellspacing="0" border="1" width="100%">  
                                    <tr>
                                    <td style=" background-color:green;color:White; text-align:center">Normal</td>
                                    <td style=" background-color:red;color:White;text-align:center">Overdue</td>
                                    <td style=" background-color:yellow;text-align:center">Planned</td>
                                    <td style="text-align:center">Done</td>
                                    <td>
                                       <asp:Button runat="server" ID="btnExcel" Text="Download Training Requirement" style="float:right;padding-right:7px;margin:3px;" CssClass="btn" Width="200px" Visible="false" OnClick="TrainingExcel_Click" CausesValidation="false"/>
									<asp:Button runat="server" ID="btnManageTraining" Text="Manage Training" style="float:right;padding-right:7px;margin:3px;" CssClass="btn" OnClick="btnManageTraining_Click" Width="200px" CausesValidation="false"  />
                                                                        <asp:Button runat="server" ID="btnExportToPDF" Text="Print" style="float:right;padding-right:7px;margin:3px;" CssClass="btn" OnClick="Export_PDF" Width="200px" CausesValidation="false"  />
                                                                        
                                                                        <a id="aDownLoadFile" runat="server" visible="false" style="float:right;" > Download(Right click and select Save target as)</a>
                                    </td>
                                    </tr>
                                    </table>
                                        <!-- DIV FOR SHOW UPDATE TRAINING -->
                                        <div id="dvSch" onkeydown='hideUpdate();' style="border :solid 1px gray; border-bottom:solid 4px gray;border-right:solid 4px gray; width :700px; height :220px; padding :10px; position :absolute; top:400px; left:200px; background-color:#FFE4B5; display :none; text-align :center">
                <span onclick='$(selDiv).slideUp();' style="cursor:pointer;float:right;"><img src="../Images/critical.gif" title="Close" /></span>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                <span style="font-size:11px">
                <asp:Button runat="server" ID="btnLoadLit" OnClick="Load_Lit" CausesValidation="false" style="display:none" />
                <asp:TextBox ID="txtTrId" runat="server" style="display:none"></asp:TextBox>
                    <asp:Literal runat="server" ID="litSummary"></asp:Literal> 
                </span>
                </ContentTemplate>
                </asp:UpdatePanel>
                <div id="dvUP">
                <hr />
                <span style="font-size:12px">| Update Training |</span> 
                <hr />
                <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                <table style="border-collapse:collapse" border="0" cellpadding="3" cellspacing="0">
                           <tr>
                               <td style="text-align:right">From Date :&nbsp;</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_FromDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="75px"></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="rf1" ControlToValidate="txt_FromDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                               <td style="text-align:right">To Date :&nbsp;</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_ToDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="75px"></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txt_ToDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                           <td style="text-align:right">Institute :</td>
                               <td style="text-align: left"><asp:DropDownList ID="DropDownList1" runat="server" CssClass="required_box" TabIndex="2" Width="150px"></asp:DropDownList>
                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="DropDownList1" ErrorMessage="*"></asp:RequiredFieldValidator>
                               
                               </td>
                               <td style="text-align: left">&nbsp;</td>
                               <td><asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text=" Update " Width="60px" TabIndex="9" OnClick="btn_UpdateTraining_Click" /></td>
                           </tr>
                        </table>      
                <asp:HiddenField runat="server" ID="hfdPKId" /><asp:HiddenField runat="server" ID="hfdTId" /><asp:HiddenField runat="server" ID="hfdDD" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_FromDate">
                       </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton4" PopupPosition="TopLeft" TargetControlID="txt_ToDate">
                       </ajaxToolkit:CalendarExtender>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="btn_Save_PlanTraining" />
                </Triggers>
                </asp:UpdatePanel>
                </div>
                </div>
                                    </asp:View>
                                    <asp:View ID="View6" runat="server">
                                    <div style="height:25px; overflow-x:hidden; overflow-y:scroll; border:solid 1px black;">
                                    <table cellpadding="2" cellspacing="0" width='100%' style=" background-color:#c2c2c2; height:25px; font-weight:bold; border-collapse:collapse; " border="1">
                                      <colgroup>
                                      <col width='50px' />
                                      <col width='50px' />
                                      <col width='50px' />
                                      <col />
                                      <col width='130px' />
                                      <col width='50px' />
                                      <col width='17px' />
                                      </colgroup>
                                        <tr>
                                            <td style='text-align:center'>
                                               &nbsp;View
                                            </td>
                                            <td style='text-align:center'>
                                               &nbsp;Edit
                                            </td>
                                            <td style='text-align:center'>
                                               &nbsp;Delete
                                            </td>
                                            <td>
                                               &nbsp;Menu Name     
                                            </td>
                                             <td style='text-align:center'>
                                               &nbsp;View Contracts
                                            </td>
                                            <td style='text-align:center'>
                                               &nbsp;Print
                                            </td>
                                            <td></td>
                                        </tr>
                                      </table>
                                    </div>
                                    <div style="height:244px; overflow-x:hidden; overflow-y:scroll; border:solid 1px black;">
                                    <table cellpadding="2" cellspacing="0" width='100%' style=" height:25px; border:solid 1px gray; border-collapse:collapse;" border="1">
                                        <colgroup>
                                          <col width='50px' />
                                          <col width='50px' />
                                          <col width='50px' />
                                          <col />
                                          <col width='130px' />
                                          <col width='50px' />
                                          <col width='17px' />
                                          </colgroup>
                                            <asp:Repeater runat="server" ID="rpt_Menus">
                                            <ItemTemplate>
                                            <tr >
                                                <td style='text-align:center'><asp:LinkButton Text="View" CommandArgument='<%#Eval("MenuId")%>' runat="server" ID="lnkView" OnClick="lnkView_Click" ForeColor="Blue"></asp:LinkButton></td>
                                                <td style='text-align:center'><asp:LinkButton Text="Edit" CommandArgument='<%#Eval("MenuId")%>' runat="server" ID="lnkEdit" OnClick="lnkEdit_Click" ForeColor="Blue"></asp:LinkButton></td>
                                                <td style='text-align:center'><asp:LinkButton Text="Delete" CommandArgument='<%#Eval("MenuId")%>' runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" ForeColor="Red" OnClientClick="return window.confirm('Are you sure to delete?');"></asp:LinkButton></td>
                                                <td><%#Eval("MenuName")%></td>
                                                <td style="text-align:center">
                                                    <asp:LinkButton Text="View Contracts" CommandArgument='<%#Eval("MenuId")%>' runat="server" ID="lnkContracts" OnClick="lnkViewContracts_Click" ForeColor="Blue"></asp:LinkButton>
                                                </td>
                                                <td style="text-align:center">
                                                    <img src="../Images/print_16.png" style='cursor:pointer' onclick="window.open('../Reporting/CrewFavouriteFood.aspx?FoodId=<%#Eval("MenuId")%>');" />
                                                </td>
                                                <td></td>
                                            </tr>
                                            </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <asp:Button runat="server" ID="btnAdd" Text="Add Menu" CssClass="btn" style="float:right; margin-top:5px;" OnClick="btnAddMenu_Click"/>
                                    <!-- Section to Add Menu-->    
                                    <div style="position:absolute;top:40px;left:0px; height :470px; width:100%;" id="dvAddEditMenu" runat="server" visible="false">
                                    <center>
                                        <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                         <div style="position :relative;width:800px; height:450px;padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 5px #4371a5;">
                                         <center >
                                         <div style='padding:5px; background-color:#e2e2e2; font-weight:bold;'>
                                            Add / Edit Menu
                                         </div>
                                         <div style=' height:400px; margin-left:10px;'>
                                         <table cellpadding="2" cellspacing="0" width='100%'>
                                         <tr>
                                            <td style='width:120px; text-align:left'>Menu Name :</td>
                                            <td style='text-align:left;width:300px;'>
                                            <asp:TextBox runat="server" ID="txtMenuName" Width="200px" CssClass="required_box" MaxLength='100'></asp:TextBox>
                                            </td>
                                            <td style='width:120px;text-align:left'>
                                            Serving Size :
                                            </td>
                                             <td style='text-align:left'>
                                            <asp:TextBox runat="server" ID="txtSize" Width="200px" CssClass="input_box" MaxLength='5'></asp:TextBox>
                                            </td>
                                         </tr>
                                         <tr>
                                            <td style='text-align:left' colspan="2">Ingredients :</td>
                                            <td style='text-align:left' colspan="2">Method :</td>
                                         </tr>
                                         <tr>
                                            <td style='text-align:left'  colspan="2">
                                                <asp:TextBox runat="server" ID="txtIng" CssClass="required_box" TextMode="MultiLine" Height="347px" Width='95%'></asp:TextBox>
                                            </td>
                                            <td style='text-align:left'  colspan="2">
                                                <asp:TextBox runat="server" ID="txtMethod" CssClass="input_box" TextMode="MultiLine" Height="347px" Width='95%'></asp:TextBox>
                                            </td>
                                         </tr>
                                         </table>
                                         </div>
                                         <div style="float:right; padding-right:10px; text-align:left">
                                         <asp:Label runat="server" ID="lblMsg" ForeColor="Red" style="float:left" ></asp:Label>
                                         <asp:Button runat="server" ID="btnMenuSave" Text="Save" CssClass="btn" OnClick="btnMenuSave_Click" Width='100px'/>
                                         <asp:Button runat="server" ID="btnAddCancel" Text="Close" CssClass="btn" OnClick="btnAddCancel_Click"  Width='100px'/>
                                         <asp:HiddenField runat="server" ID="hfdMenuId" />
                                         </div>
                                         </center>
                                         </div>
                                    </center>
                                    </div> 

                                     <!-- Section to view contracts -->    
                                    <div style="position:absolute;top:700px;left:0px; height :470px; width:100%;" id="dvContracts" runat="server" visible="false">
                                    <center>
                                        <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                         <div style="position :relative;width:800px; height:450px;padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 5px #4371a5;">
                                         <center >
                                         <div style='padding:5px; background-color:#e2e2e2; font-weight:bold;'>
                                          <b>  View Contracts </b>
                                         </div>
                                         <div style=' height:400px; margin-left:10px;'>
                                         <table cellpadding="3" cellspacing="0" width='95%' border='1' style="border-collapse:collapse;">
                                         <tr>
                                         <td style='text-align:left'><b>Contract Ref #</b></td>
                                         </tr>
                                         <asp:Repeater runat="server" ID='rptContracts' >
                                         <ItemTemplate>
                                         <tr>
                                         <td style="text-align:left">
                                         <%#Eval("CONTRACTREFERENCENUMBER")%> - 
                                         [ 
                                         <%#Common.ToDateString(Eval("STARTDATE"))%> to  
                                         <%#Common.ToDateString(Eval("ENDDATE"))%> 
                                         ]
                                         </td>
                                         </tr>
                                         </ItemTemplate>
                                         </asp:Repeater>
                                         </table>
                                         </div>
                                         <div style="float:right; padding-right:10px; text-align:left">
                                            <asp:Button runat="server" ID="btnClose1" Text="Close" CssClass="btn" OnClick="btnClose1_Click"  Width='100px'/>
                                         </div>
                                         </center>
                                         </div>
                                    </center>
                                    </div> 

                                        
                                    </asp:View>
                                </asp:MultiView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:25%">
                                                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                 <asp:Image ID="img_Crew" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="108px" Width="100px" ImageUrl="" CausesValidation="False" />  
                                <asp:HiddenField ID="HiddenPK" runat="server" />
                            </td>  
                         </tr>
                         </table>
                     <table style="background-color:#f9f9f9;border:1px dashed;" cellpadding="0" cellspacing="0" width="100%" class="table table-bordered">
                         
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">    
                                <strong>Name :</strong> </td>
                             <td style="text-align:left;padding:5px;width:125px;">
                                 <asp:Label ID="lblName" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                 <strong>Rank :</strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblCurrRank" runat="server"></asp:Label>
                                 <%#Eval("TrainingName").ToString().Replace(",","<b style='color:red'> OR </b>")%>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                 <strong> Age : </strong></td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblAge" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                  <strong>Current Vessel :</strong>
                                   </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblCurrentVessel" runat="server"></asp:Label>
                                 <%#Common.ToDateString(Eval("LastDoneDate"))%>
                                  </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                <strong> Rank Exp. : </strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                <asp:Label ID="lblRankExp" runat="server"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                               <strong> Rating :</strong>
                             </td>
                             <td style="text-align:left;padding:5px;">

                                 <asp:Label ID="lblRating" runat="server"></asp:Label>

                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                               <strong> Status :  </strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblStatus" runat="server"></asp:Label>
                             </td>
                         </tr>
                     </table> 
                     <br />
                     <table width="100%" style="background-color: #f9f9f9; vertical-align:top; overflow:visible;">
                        <tr>
                           <td ><asp:Button runat="server"  CommandArgument="0" Text="Notes" OnClick="Menu1_MenuItemClick" ID="b1" CssClass="btn1"  Font-Bold="True" Width="100px" /></td>
                            </tr>
                             <tr>
                                <td><asp:Button runat="server"  CommandArgument="2" Text="Appraisal" OnClick="Menu1_MenuItemClick"  ID="b2"  CssClass="btn1"  Font-Bold="True" Width="100px" Visible="false"/></td>
                                  </tr>
                        <tr>
                                <td><asp:Button runat="server"  CommandArgument="3" Text="History" OnClick="Menu1_MenuItemClick" ID="b3"  CssClass="btn1"  Font-Bold="True" Width="100px"/></td> </tr>
                         
                        <%#Eval("Source")%>
                        
                    </table>
                </td>
            </tr>
        </table>
            </td>
        </tr>
    </table>
       </td>
        </tr>
      </table>
        <script language="javascript"  type="text/javascript">
        Show_Amount();
        </script>
    </div>
    <%#Common.ToDateString(Eval("DueDate"))%>
    </asp:Content>

