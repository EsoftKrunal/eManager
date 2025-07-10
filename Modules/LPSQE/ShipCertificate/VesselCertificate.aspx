<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VesselCertificate.aspx.cs" Inherits="UserUploadedDocuments_VesselCertificate" Title="EMANAGER" EnableEventValidation="false" %>

<%@ Register Src="VesselCertificates.ascx" TagName="VesselCertificates" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript">
        function OpenPopup(vslid, recid, mode, Archive) {

            if (parseInt(recid) > 0)
                window.open('CertPopUp.aspx?VId=' + vslid + "&RecId=" + recid + "&Mode=" + mode + "&IsArchive=" + Archive, null, 'title=no,toolbars=no,scrollbars=yes,width=1150,height=600,left=20,top=20,addressbar=no');
        }
        function OpenPopup2(vslid, recid, mode, Archive) {
            window.open('CertPopUp.aspx?VId=' + vslid + "&RecId=" + recid + "&Mode=" + mode + "&IsArchive=" + Archive, null, 'title=no,toolbars=no,scrollbars=yes,width=1150,height=600,left=20,top=20,addressbar=no');
        }
        function OpenPrint(vslid, vslName) {
            window.open('../Reports/CertPrint.aspx?VSLId=' + vslid + "&VSLCode=" + vslName, null, 'title=no,toolbars=no,scrollbars=yes,width=1000,height=600,left=20,top=20,addressbar=no');
        }
    </script>
    <style type="text/css">
        .highlightRow
        {
            background-color: #9999FF;
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
<script>
    $(function () {
        $(".trclass").click(function () {
            $(this).addClass("highlightRow").siblings().removeClass("highlightRow");
        });
    });
</script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
        <div class="text headerband" >
            Ship Certificates
           </div>
        <br />
    <table cellpadding="0" cellspacing="0" width="100%" border="0" style="font-family:Arial;font-size:12px;">
        <tr>
            <td>
                <div style="float: left;">
                    <table width="100%">
                        <tr>
                            <td style="width:125px;text-align:right;padding-right:5px;">
                                Fleet :
                            </td>
                            <td style="width:125px;text-align:left;padding-left:5px;">
                                <asp:DropDownList ID="ddl_Fleet" runat="server" Width="100px" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_Fleet_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width:125px;text-align:right;padding-right:5px;">
                                Vessel :
                            </td>
                            <td style="width:125px;text-align:left;padding-left:5px;">
                               <asp:DropDownList ID="ddlVessel" runat="server" Width="170px" CssClass="input_box"></asp:DropDownList>
                            </td>
                            <td style="width:125px;text-align:right;padding-right:5px;">
                                <span style="color: Navy">Certificate Type : </span>
                            </td>
                            <td style="width:125px;text-align:left;padding-left:5px;">
                                <asp:DropDownList runat="server" ID="ddlCType" Width="100px" OnSelectedIndexChanged="ddlCT_SelectedIndexChanged" CssClass="input_box">
                        <asp:ListItem Text="All" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Provisional" Value="P" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Interim" Value="I" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Fullterm" Value="F" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Conditional" Value="C" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Permanent" Value="A" meta:resourcekey="ListItemResource5"></asp:ListItem>
                    </asp:DropDownList>
                            </td>
                            <td style="width:125px;text-align:right;padding-right:5px;">
                                Certificate Category :
                            </td>
                            <td style="width:125px;text-align:left;padding-left:5px;">
                                 <asp:DropDownList runat="server" ID="ddlCertCategory" Width="100px" AutoPostBack="true" CssClass="input_box" OnSelectedIndexChanged="ddlCertCategory_SelectedIndexChanged">

                                     </asp:DropDownList>
                            </td>
                            <td style="width:300px;text-align:right;padding-right:10px;">
                                <div style="float: right">
                    <asp:Button runat="server" CssClass="btn" ID="btnShow" Text=" Show " OnClick="btnShow_OnClick" />
                    <asp:Button runat="server" ID="btnCert" CssClass="btn" Text="Add Certificate" OnClick="AddCert" />
                    <asp:Button runat="server" ID="btn_Print" CssClass="btn" Text='Print' OnClick="PrintCerts" />
                </div>
                            </td>

                        </tr>
                        <tr>
                            <td  style="width:125px;text-align:right;padding-right:5px;">
                                Archived :
                            </td>
                            <td style="width:125px;text-align:left;padding-left:5px;">
                                <asp:CheckBox ID="chkArchived" runat="server" Text="" OnCheckedChanged="chkArchive_CheckedChanged" AutoPostBack="True" />
                            </td>
                            
                            <td colspan="3" style="text-align:right;padding-right:5px;">
                                 <asp:TextBox runat="server" ID="lblRecordCount" ReadOnly="true" Style="border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                            </td>
                            <td colspan="4" style="padding:2px 0px 2px 0px;">
                                 <div class="ayellow" style="width: 150px; float: right;">Due for Renewal</div>
                <div class="ared" style="width: 100px; float: right;">Expired</div> 
                <div class="agreen" style="width: 100px; float: right;">Valid</div>
                            </td>
                        </tr>
                    </table>
                </div>
                
            </td>
        </tr>
       <%-- <tr>
            <td >
               
               
            </td>
        </tr>--%>
        <tr>
            <td>
                <style type="text/css">
                    .ared {
                        background-color: red;
                        color: White;
                        text-align: center;
                    }

                    .ayellow {
                        background-color: yellow;
                        color: red;
                        text-align: center;
                    }

                    .agreen {
                        background-color: Green;
                        color: White;
                        text-align: center;
                    }
                </style>
                <div style="width: 100%; overflow-x: hidden; overflow-y: scroll; height: 27px;">
                    <table cellspacing="0" cellpadding="1" rules="all" border="1" id="Table2" style="width: 100%; border-collapse: collapse;">
                        <%--#4371a5--%>
                        <col width="35px" />
                        <col width="35px" />
                        <col width="50px" />
                        <col width="50px" />
                        <col width="70px" />
                        <col />
                        <col width="130px" />
                        <col width="90px" />
                        <col width="90px" />
                        <col width="90px" />
                        <col width="90px" />
                        <col width="50px" />
                        <tr class= "headerstylegrid">
                            <td>View</td>
                            <td>Edit</td>
                            <td>Delete</td>
                            <td>Attach</td>
                            <td>VSL</td>
                            <td>Certificate Name</td>
                            <td>Certificate Number</td>
                            <td>Issue Date</td>
                            <td>Expiry Date</td>
                            <td>Cert. Type</td>
                            <td>Next Survey</td>
                            <td>Archive</td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%; overflow-x: hidden; overflow-y: scroll; height: 450px;">
                    <table cellspacing="0" cellpadding="1" rules="rows" border="1" id="Table1" style="width: 100%; border-collapse: collapse;">
                        <%--#4371a5--%>
                        <colgroup>
                        <col width="35px" />
                        <col width="35px" />
                        <col width="50px" />
                        <col width="50px" />
                        <col width="70px" />
                        <col />
                        <col width="130px" />
                        <col width="90px" />
                        <col width="90px" />
                        <col width="90px" />
                        <col width="90px" />
                        <col width="50px" />
                         </colgroup>
                        <asp:Repeater ID="rptVesselCertificate" runat="server">
                            <ItemTemplate>
                                <tr valign="top" class="trclass">
                                    <td style="text-align: center;" >
                                        <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="gv_VDoc_SelectIndexChanged" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:ImageButton ID="btnEidt" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="gv_VDoc_Row_Editing" Visible='<%# Convert.ToInt32(Eval("Archived")) == 0 %>'/>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="gv_VDoc_Row_Deleting" CausesValidation="false" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Visible='<%# Convert.ToInt32(Eval("Archived")) == 0 %>' />
                                    </td>
                                    <td>
                                        <a target="_blank" href='<%# GetPath(Eval("FileName").ToString()).ToString() %>' style='display: <%#FileExists(Eval("FileName").ToString())%>; cursor: hand'>
                                            <img src="../../HRD/Images/paperclip12.gif" border="0" /></a>
                                    </td>
                                    <td><%#Eval("vesselCode")%> </td>
                                    <td>
                                        <asp:Label ID="lbl_Doc_Type" runat="server" Text='<%# Eval("CertificateName") %>' meta:resourcekey="lbl_Doc_TypeResource1"></asp:Label>
                                        <asp:HiddenField ID="HiddenId" runat="server" Value='<%# Eval("VesselCertId") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hfVesselID" runat="server" Value='<%# Eval("VesselId") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hfdArchive" runat="server" Value='<%# Eval("Archived") %>'></asp:HiddenField>
                                    </td>
                                    <td><%#Eval("CertificateNumber")%> </td>
                                    <td><%#Eval("IssueDate")%> </td>
                                    <td class='<%#getColor(Eval("ExpiryDate").ToString()) %>'><%#Eval("ExpiryDate")%></td>
                                    <td><%#Eval("CertType")%> </td>
                                    <td class='<%#getColor(Eval("NSD").ToString()) %>'><%#Eval("NSD")%></td>
                                    <td style="text-align:center;" class='<%#getArchivedColor(Convert.ToInt32(Eval("Archived"))) %>'>  <asp:ImageButton runat="server" ID="ibArchiveCertificate" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveCertificate" Visible='<%# Convert.ToInt32(Eval("Archived")) == 0 %>' CausesValidation="false" OnClientClick="javascript:return window.confirm('Are you Sure to Archive Vessel Certificate ?');" />  </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>


