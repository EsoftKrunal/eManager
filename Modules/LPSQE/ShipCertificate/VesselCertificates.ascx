<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselCertificates.ascx.cs" Inherits="VesselCertificates" %>

<script language="javascript" type="text/javascript">
    function CheckClick(obj) {
        if (obj.checked) {
            document.getElementById('<%=txt_ExpDate.ClientID%>').value = "";
        document.getElementById('<%=txt_ExpDate.ClientID%>').disabled = 'disabled';
    }
    else {
        document.getElementById('<%=txt_ExpDate.ClientID%>').disabled = '';
        }
    }
    month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
    function checkDate(theField) {
        dPart = theField.value.split("-");
        if (dPart.length != 3) {
            alert("Enter Date in this format: dd-mmm-yyyy!");
            theField.focus();
            return false;
        }
        var check = 0;
        for (i = 0; i < month.length; i++) {
            if (dPart[1].toLowerCase() == month[i].toLowerCase()) {
                check = 1;
                dPart[1] = i;
                break;
            }
        }
        if (check == 0) {
            alert("Enter Date in this format: dd-mmm-yyyy!");
            return false;
        }
        nDate = new Date(dPart[2], dPart[1], dPart[0]);
        // nDate = new Date(dPart[0], dPart[1], dPart[2]);
        if (isNaN(nDate) || dPart[2] != nDate.getFullYear() || dPart[1] != nDate.getMonth() || dPart[0] != nDate.getDate()) {
            alert("Enter Date in this format: dd-mmm-yyyy!");
            theField.select();
            theField.focus();
            return false;
        } else {
            return true;
        }
    }
    function trimAll(sString) {
        while (sString.substring(0, 1) == ' ') {
            sString = sString.substring(1, sString.length);
        }
        while (sString.substring(sString.length - 1, sString.length) == ' ') {
            sString = sString.substring(0, sString.length - 1);
        }
        return sString;
    }

    function checkform() {
        if (trimAll(document.getElementById('<%=txt_Cert.ClientID%>').value) == "") {
        alert('Please enter Certificate Name.');
        document.getElementById('<%=txt_Cert.ClientID%>').focus();
        return false;
    }
    if (trimAll(document.getElementById('<%=txt_CertNumber.ClientID%>').value) == "") {
        alert('Please enter Certificate Number.');
        document.getElementById('<%=txt_CertNumber.ClientID%>').focus();
        return false;
    }
    if (trimAll(document.getElementById('<%=txt_IssueDate.ClientID%>').value) == "") {
        alert('Please enter Issue Date.');
        document.getElementById('<%=txt_IssueDate.ClientID%>').focus();
        return false;
    }
    if (!checkDate(document.getElementById('<%=txt_IssueDate.ClientID%>')))
        return false;

//    if(trimAll(document.getElementById('<%=txt_ExpDate.ClientID%>').value)=="")
//    {
//        alert('Please enter Expiry Date.');
//        document.getElementById('<%=txt_ExpDate.ClientID%>').focus();
    //        return false;
    //    }

    if (document.getElementById('<%=txt_NSD.ClientID%>').value != "") {
        if (!checkDate(document.getElementById('<%=txt_ExpDate.ClientID%>')))
            return false;

        if (!checkDate(document.getElementById('<%=txt_NSD.ClientID%>')))
                return false;
        }
    }
</script>
<script type="text/javascript">
    function closeWindow() {
        try {
            
            if (window.opener != null) {       
                window.opener.location.reload();
                /*c*/     
            }
               
        }
        catch (err) {

        }
    }
</script>
<table cellpadding="0" cellspacing="0" width="100%" border="0" style="background-color: #f9f9f9; vertical-align: top; border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: right; padding: 5px 10px 5px 10px;font-family:Arial;font-size:12px;">
    <tr>
        <td>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ImageButton2" PopupPosition="TopLeft" TargetControlID="txt_IssueDate" Enabled="True">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ImageButton3" PopupPosition="TopLeft" TargetControlID="txt_ExpDate" Enabled="True">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ImageButton6" PopupPosition="TopLeft" TargetControlID="txt_NSD" Enabled="True">
            </ajaxToolkit:CalendarExtender>

        </td>
    </tr>
    <tr>

        <td style="text-align: center;">
            <asp:Panel ID="pnl_Documents" runat="server" Width="100%"
                meta:resourcekey="pnl_DocumentsResource1">
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;font-family:Arial;font-size:12px;">
                    <legend><strong>Certificate Details</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:20%"></td>
                            <td style="width:25%">&nbsp;</td>
                            <td style="width:20%"></td>
                            <td style="width:35%"></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">Vessel :</td>
                            <td style="text-align: left" >
                                <asp:DropDownList ID="ddlVessel" runat="server" Width="170px" CssClass="required_box"></asp:DropDownList></td>
                            <td style="text-align: right;" >Category : </td>
                            <td style="text-align: left;" > <asp:DropDownList ID="ddlCategory" runat="server" Width="170px" CssClass="required_box"></asp:DropDownList></td>
                        </tr>
                         <tr>
                            <td style="height: 3px;"></td>
                            <td style="text-align: left;" ></td>
                            <td ></td>
                            <td  style="text-align: left; height: 3px;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">Certificate Name: </td>
                            <td style="text-align: left" >
                                <asp:TextBox ID="txt_Cert" runat="server" CssClass="required_box"
                                    MaxLength="255" Width="297px"></asp:TextBox>
                            </td>
                            <td style="text-align: right;" >Certificate Number :</td>
                            <td  style="text-align: left;">
                                <asp:TextBox ID="txt_CertNumber" runat="server" CssClass="required_box"
                                    MaxLength="255" meta:resourcekey="txt_CertNumberResource1" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 3px;"></td>
                            <td style="text-align: left;" ></td>
                            <td ></td>
                            <td  style="text-align: left; height: 3px;"></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Issue Date :</td>
                            <td style="text-align: left;" >
                                <asp:TextBox ID="txt_IssueDate" runat="server" CssClass="required_box"
                                    MaxLength="11" meta:resourcekey="txt_IssueDateResource1" TabIndex="1"
                                    Width="111px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" runat="server"
                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" meta:resourcekey="ImageButton2Resource1" />
                            </td>
                            <td style="text-align: right;" >Expiry Date :</td>
                            <td style="text-align: left;" >
                                <asp:TextBox ID="txt_ExpDate" runat="server" CssClass="input_box"
                                    MaxLength="11" meta:resourcekey="txt_ExpDateResource1" TabIndex="1"
                                    Width="111px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton3" runat="server"
                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" meta:resourcekey="ImageButton3Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 3px;"></td>
                            <td style="text-align: left;" ></td>
                            <td ></td>
                            <td style="text-align: left; height: 3px;" ></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Issued By :</td>
                            <td style="text-align: left;" >
                                <asp:TextBox ID="txt_IssueBy" runat="server" CssClass="input_box"
                                    MaxLength="255" meta:resourcekey="txt_IssueByResource1" Width="291px"></asp:TextBox>
                            </td>
                            <td style="text-align: right;" >Place Issued:</td>
                            <td style="text-align: left;" >
                                <asp:TextBox ID="txt_PlaceIssued" runat="server" CssClass="input_box"
                                    MaxLength="50" meta:resourcekey="txt_PlaceIssuedResource1" Width="296px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 3px;"></td>
                            <td style="text-align: left;" ></td>
                            <td ></td>
                            <td style="height: 3px;" ></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" >Certificate Type :</td>
                            <td style="text-align: left;" >
                                <asp:DropDownList ID="ddl_CertType" runat="server" CssClass="input_box"
                                    meta:resourcekey="ddl_CertTypeResource1" Width="199px">
                                    <asp:ListItem meta:resourcekey="ListItemResource1" Text="Provisional" Value="P"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource2" Text="Interim" Value="I"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource3" Text="Fullterm" Value="F"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource4" Text="Conditional" Value="C"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource5" Text="Permanent" Value="A"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right;" >Next Survey Interval :</td>
                            <td  style="text-align: left;">
                                <asp:TextBox ID="txt_NextSurInt" runat="server" AutoPostBack="True" CssClass="input_box" MaxLength="2" meta:resourcekey="txt_NextSurIntResource1" OnTextChanged="txt_NextSurInt_TextChanged" Width="36px"></asp:TextBox>
                                (month)</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 3px;"></td>
                            <td style="text-align: left;" ></td>
                            <td ></td>
                            <td style="height: 3px;" ></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Last Annual Survey :</td>
                            <td style="text-align: left;" >
                                <asp:TextBox ID="txtLastAnnSurvey" runat="server" CssClass="input_box" MaxLength="11" meta:resourcekey="txt_NSDResource1" TabIndex="1" Width="111px" AutoPostBack="true" OnTextChanged="txt_NextSurInt_TextChanged"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtLastAnnSurvey_CalendarExtender"
                                    runat="server" Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1"
                                    PopupPosition="TopLeft" TargetControlID="txtLastAnnSurvey">
                                </ajaxToolkit:CalendarExtender>
                                <asp:ImageButton ID="ImageButton7" runat="server"
                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" meta:resourcekey="ImageButton6Resource1" />
                            </td>
                            <td style="text-align: right;" >Survey &amp; Expiry Alert :</td>
                            <td  style="text-align: left;">
                                <asp:TextBox ID="txt_AFS" runat="server" CssClass="input_box" MaxLength="3"
                                    meta:resourcekey="txt_AFSResource1" TabIndex="1" Width="36px"></asp:TextBox>
                                (days) &nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txt_AFE" runat="server" CssClass="input_box" MaxLength="3"
                                meta:resourcekey="txt_AFEResource1" TabIndex="1" Width="36px"></asp:TextBox>
                                (days)
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 3px;"></td>
                            <td style="text-align: left;" ></td>
                            <td ></td>
                            <td style="text-align: left; height: 3px;" ></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Next Survey Due :</td>
                            <td style="text-align: left;" >
                                <asp:TextBox ID="txt_NSD" runat="server" CssClass="input_box" MaxLength="11"
                                    meta:resourcekey="txt_NSDResource1" TabIndex="1" Width="111px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton6" runat="server"
                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" meta:resourcekey="ImageButton6Resource1" />
                            </td>
                            <td style="text-align: right;" >File Upload :</td>
                            <td style="text-align: left;" >
                                <asp:HiddenField ID="hfdFileName" runat="server" />
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input_box"
                                    Width="300px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 6px;"></td>
                            <td style="text-align: left;" class="style8"></td>
                            <td class="style11"></td>
                            <td  style="height: 6px;"></td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_NSI0" runat="server" TargetControlID="txt_NextSurInt" FilterType="Custom, Numbers" ValidChars="0123456789" Enabled="True"></ajaxToolkit:FilteredTextBoxExtender>
            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_AFS" runat="server" TargetControlID="txt_AFS" FilterType="Custom, Numbers" ValidChars="0123456789" Enabled="True"></ajaxToolkit:FilteredTextBoxExtender>
            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_AFE" runat="server" TargetControlID="txt_AFE" FilterType="Custom, Numbers" ValidChars="0123456789" Enabled="True"></ajaxToolkit:FilteredTextBoxExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_Cert" ServicePath="~/Modules/LPSQE/WebService.asmx" ServiceMethod="GetCertificates" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></ajaxToolkit:AutoCompleteExtender>
        </td>
    </tr>
    <tr>
        <td style="text-align: right; padding-top: 5px;">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lbl_message_documents" runat="server" Text="" Visible="true" ForeColor="#C00000" meta:resourcekey="lbl_message_documentsResource1"></asp:Label></td>
                    <td style="text-align: right">
                        <asp:Button ID="btn_Add" runat="server" CssClass="btn" Text="Add" Width="59px"
                            OnClick="btn_Add_Click" CausesValidation="False"
                            meta:resourcekey="btn_AddResource1" />
                        <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save"
                            Width="59px" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" OnClientClick="return checkform();" />
                        <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Close" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" meta:resourcekey="btn_CancelResource1" OnClientClick="window.close();"  />
                       
        <%--ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadfa", "window.opener.ReloadPage();window.close();", true);--%>

                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
