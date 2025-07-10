<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="KST.aspx.cs" Inherits="CrewOperatin_KST"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <asp:ScriptManagerProxy ID="ScriptManager1" runat="server"></asp:ScriptManagerProxy>
   <table cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td style="text-align: right; width: 100px">Category :</td>
            <td style="width:200px;"><asp:DropDownList runat="server" ID="ddlcat" Width="200px">
                                   <asp:ListItem Text=" Select " Value="0"></asp:ListItem>
                                   <asp:ListItem Text="Injury to People" Value="1"></asp:ListItem>
                                   <asp:ListItem Text="Mooring" Value="2"></asp:ListItem>
                                   <asp:ListItem Text="Security" Value="3"></asp:ListItem>
                                   <asp:ListItem Text="Cargo Contamination" Value="4"></asp:ListItem>
                                   <asp:ListItem Text="Equipment Failure" Value="6"></asp:ListItem>
                                   <asp:ListItem Text="Navigation" Value="8"></asp:ListItem>
                                   <asp:ListItem Text="Damage to Property" Value="9"></asp:ListItem>
                                   <asp:ListItem Text="Pollution" Value="10"></asp:ListItem>
                                   <asp:ListItem Text="Fire" Value="11"></asp:ListItem>
                                     </asp:DropDownList></td>
            <td style="text-align: right; width: 50px">Topic :</td>
            <td><asp:TextBox ID="txtFilterTopic" MaxLength="100" runat="server" CssClass="dateonly" ValidationGroup="addedit" Width="200px"></asp:TextBox></td>            
            <td style="text-align: right">
                 <asp:Button ID="btnFilterRecords" runat="server" OnClick="Filter_Visits" CssClass="btn" Text="Search" />
                 <asp:Button ID="btnAddNew" runat="server" OnClick="btnAdd_Click" CssClass="btn"  Text="+ Add New" />
            </td>
        </tr>
    </table>
    <table cellpadding="6" cellspacing="0" width="100%" border="1" style='border-collapse: collapse;
        background-color: #4da6ff;' bordercolor="#fffff">
        <colgroup>
            <col width="40px" />
            <col width="100px" />
            <col />
            <col width="180px" />
            <col width="200px" />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
        </colgroup>
        <tr class= "headerstylegrid">
            <td style="color: White; text-align: center;">
                Sr#
            </td>
            <td style="color: White;">
                KST #
            </td>
            <td style="color: White;">
                Topic
            </td>
            <td style="color: White;">
                Vessel Name
            </td>
            <td style="color: White;">
                Category
            </td>
            <td style="color: White;">
                Incident Dt.
            </td>
            <td style="color: White;">
                Severity
            </td>
             <td style="color: White;">
                Attachment
            </td>
            <td style="color: White; text-align: center">
                Action
            </td>
        </tr>
    </table>
    <table cellpadding="6" cellspacing="0" width="100%" border="1" style='border-collapse: collapse' bordercolor="#e2e2e2">
       <colgroup>
           <col width="40px" />
            <col width="40px" />
            <col width="100px" />
            <col />
            <col width="100px" />
            <col width="200px" />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
           <col width="100px" />
        </colgroup>
        <asp:Repeater runat="server" ID="rptSeminars">
            <ItemTemplate>
                <tr>
                    <td style="text-align: center;">
                        <a href='KSTDetails.aspx?KSTId=<%#Eval("kst_id")%>' target="_blank">
                            <img src="../Images/magnifier.png" style="border:none;" />
                        </a>
                    </td>
                    <td style="text-align: center;"><%#Eval("SNO")%></td>
                    <td style="text-align: center;"><%#Eval("kst_no")%></td>
                    <td><%#Eval("Topic")%></td>
                    <td><%#Eval("vesselname")%></td>
                    <td><%#Eval("CategoryName")%></td>
                    <td><%#Common.ToDateString(Eval("IncidentDate"))%></td>
                    <td><%#Eval("accidentseverity")%></td>
                    <td>
                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Modules/HRD/Images/paperclip.png" CommandArgument='<%#Eval("kst_id")%>' OnClick="btndownload_click" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ></asp:ImageButton>
                    </td>
                    <td style="text-align: center">
                      <asp:ImageButton runat="server" OnClick="btnEdit_Click" ImageUrl="~/Modules/HRD/Images/edit.png" CommandArgument='<%#Eval("kst_id")%>'></asp:ImageButton>
                      <asp:ImageButton runat="server" OnClick="btnDelete_Click" ImageUrl="~/Modules/HRD/Images/delete_12.gif" CommandArgument='<%#Eval("kst_id")%>' OnClientClick="return confirm('Are you sure to delete?');" Visible='<%#(Eval("Status").ToString()=="O")%>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <!-- IFRAME SECTION -->
    <div style="position: fixed; top: 25px; left: 25px; height: 100%; width: 100%;" id="dvFrame"
        runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 0px; left: 0px; min-height: 100%; width: 100%;
                background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)">
            </div>
            <div style="position: relative; text-align: center; background: white; z-index: 150;top: 30px; border: solid 5px black; width: 900px;">
                <center>
                    <div style="padding:7px; background-color:#4da6ff;color:white; font-size:15px;">
                        Add Modify KST
                    </div>
                    <div style="padding:10px;">
                       <table width="100%" cellpadding="5" cellspacing="0"  runat="server" id="tbl_mst"  style="font-size:14px;">
                           <tr>
                               <td style="width:150px">Enter Incident # </td>                               
                               <td>
                                   <asp:TextBox ID="txtFIncidentNo" MaxLength="100" runat="server" ValidationGroup="addedit" Width="200px" ></asp:TextBox>
                                   <asp:CheckBox runat="server" ID="chkLinked" Text="Please tick if this incident saved in the HSQE database." AutoPostBack="true" OnCheckedChanged="chkLinked_OnCheckedChanged" Checked="false" />
                                   <asp:RequiredFieldValidator runat="server" ValidationGroup="c11" ErrorMessage="*" ControlToValidate="txtFIncidentNo"></asp:RequiredFieldValidator>
                               </td>
                           </tr>
                       </table> 
                       <asp:HiddenField runat="server" ID="hfdIncidentId" />

                       <table width="100%" cellpadding="5" cellspacing="0" runat="server" id="tbl_det" visible="false" style="font-size:14px;"> 
                           <tr>
                               <td style="width:150px">Vessel Name </td>                               
                                <td><asp:DropDownList ID="ddlVesselName" runat="server" Width="200px" ValidationGroup="addedit"></asp:DropDownList> 
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="c11" ErrorMessage="*" ControlToValidate="ddlVesselName"></asp:RequiredFieldValidator>

                                </td>
                           </tr>
                           <tr>
                               <td style="">Incident Date </td>                               
                               <td><asp:TextBox ID="txtIncidentDate" runat="server" Width="200px" CssClass="dateonly"  ValidationGroup="addedit"></asp:TextBox> 
                                   <asp:RequiredFieldValidator runat="server" CssClass="dateonly" ValidationGroup="c11" ErrorMessage="*" ControlToValidate="txtIncidentDate"></asp:RequiredFieldValidator>
                                   
                               </td>
                           </tr>
                           <tr>
                               <td style="">Severity </td>                               
                               <td><asp:DropDownList ID="ddlSeverity" runat="server" Width="200px" ValidationGroup="addedit">
                                   <asp:ListItem Text=" Select " Value=""></asp:ListItem>
                                   <asp:ListItem Text="Minor" Value="1"></asp:ListItem>
                                   <asp:ListItem Text="Major" Value="2"></asp:ListItem>
                                   <asp:ListItem Text="Severe" Value="3"></asp:ListItem>
                                   </asp:DropDownList>
                                   <asp:RequiredFieldValidator runat="server" ValidationGroup="c11" ErrorMessage="*" ControlToValidate="ddlSeverity"></asp:RequiredFieldValidator>

                               </td>
                           </tr>
                            <tr>
                               <td style="">Classification </td>                               
                               <td><asp:CheckBoxList ID="chkClassification" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">

                                   <asp:ListItem Text="Injury to People" Value="1"></asp:ListItem>
                                   <asp:ListItem Text="Mooring" Value="2"></asp:ListItem>
                                   <asp:ListItem Text="Security" Value="3"></asp:ListItem>
                                   <asp:ListItem Text="Cargo Contamination" Value="4"></asp:ListItem>
                                   <asp:ListItem Text="Equipment Failure" Value="6"></asp:ListItem>
                                   <asp:ListItem Text="Navigation" Value="8"></asp:ListItem>
                                   <asp:ListItem Text="Damage to Property" Value="9"></asp:ListItem>
                                   <asp:ListItem Text="Pollution" Value="10"></asp:ListItem>
                                   <asp:ListItem Text="Fire" Value="11"></asp:ListItem>
                                   </asp:CheckBoxList>                                   

                               </td>
                           </tr>                           
                            <tr>
                               <td style=" vertical-align:top;">KST Topic </td>                               
                               <td>
                                   <asp:TextBox ID="txtTopic" runat="server" ValidationGroup="addedit" Width="96%" Rows="1" TextMode="MultiLine"></asp:TextBox> 
                                   <asp:RequiredFieldValidator runat="server" ValidationGroup="c11" ErrorMessage="*" ControlToValidate="txtTopic"></asp:RequiredFieldValidator>
                               </td>
                           </tr>
                           <tr>
                               <td style="">Attachment </td>                               
                               <td><asp:FileUpload runat="server" ID="flpupload" Width="97%" />
                               </td>
                           </tr> 

                       </table> 
                    </div>
                    <div style="padding: 5px; text-align: right; background-color: #E2EAFF;">
                        <asp:Label runat="server" ID="lblmsg" style="float:left;color:red;font-size:13px;"></asp:Label>
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" Style="background-color: #2e8237;color: White; border: none; padding: 4px;" Width="100px" CausesValidation="true" ValidationGroup="c11" />
                        <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" Style="background-color: Red;color: White; border: none; padding: 4px;" Width="100px" />
                    </div>
                </center>
            </div>
        </center>
    </div>
    
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
    <script type="text/javascript">

        function SetCalender() {
            $('.dateonly').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
            $('.datetime').datetimepicker({ format: 'd-M-Y H:i', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
        }
        function Page_CallAfterRefresh() {
            SetCalender();
        }
        SetCalender();
    </script>
</asp:Content>
 