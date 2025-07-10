<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Communication.aspx.cs" Inherits="Communication" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="eReports/JS/jquery.min.js"></script>
     <style type="text/css" >
    .btnNormal
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;
	    font-weight: bold;
	    background-color:#A3C8EC;
	    height: 25px;	        
	    border:none;	    
	    color:#000000;
    }
    
    .btnSelected
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;	    
	    background-color:#007ACC;
	    color:White;
	    font-weight: bold;
	    height: 25px;	    
	    border:none;	    
    }
    
    .color_tab{
      background: #3498db;
      background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
      background-image: -moz-linear-gradient(top, #3498db, #2980b9);
      background-image: -ms-linear-gradient(top, #3498db, #2980b9);
      background-image: -o-linear-gradient(top, #3498db, #2980b9);
      background-image: linear-gradient(to bottom, #3498db, #2980b9);
      -webkit-border-radius: 0;
      -moz-border-radius: 0;
      border-radius: 0px;
      font-family: Arial;
      color: #ffffff;
      font-size: 12px;
      padding: 5px 9px 5px 9px;
      text-decoration: none;
    }

    .color_tab:hover {
     background: #3cb0fd;
      background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
      background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
      text-decoration: none;
    }

    .color_tab_sel{
      background: #facc8c;
      background-image: -webkit-linear-gradient(top, #f7af51, #facc8c);
      background-image: -moz-linear-gradient(top, #f7af51, #facc8c);
      background-image: -ms-linear-gradient(top, #f7af51, #facc8c);
      background-image: -o-linear-gradient(top, #f7af51, #facc8c);
      background-image: linear-gradient(to bottom, #f7af51, #facc8c);
      font-family: Arial;
      color: black;
      font-size: 12px;
      padding: 5px 9px 5px 9px;
      text-decoration: none;
      border-bottom:solid 1px #facc8c;
    }
</style>
<script type="text/javascript">

    function ShowModal() {
        $("#dvModal").css('display','');
    }
    function HideModel() {
        $("#dvModal").css('display', 'none');
    }
    
</script>
</head>
<body style='font-family:Calibri; font-size:14px;'>
    <form id="form1" runat="server">
    <div>

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

         <div style="display:none; "  id="dvModal">
            <div style="position:absolute;top:0px; left:0px; width:100%; height:100%; z-index:50; background-color:black; opacity: .5;filter: alpha(opacity=50);" ></div>
            <div style="position : absolute; top:170px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue;z-index:52; ">
            <center>
            <div style="border:solid 2px orange; width :200px;background-color :White;opacity: 1.0;filter: alpha(opacity=100); padding:20px; overflow:auto; " >
            <div style='font-size:17px; color:Gray;'>Processing... Please Wait !</div>
            <br />
            <img src="Images/loading.gif" alt="loading">
            <br />
                <input type="button" value="Close" onclick="HideModel();" style=" background-color:orange; color:White; border:none; padding:4px; width:120px" />
            </div>
            </center>
            </div>
        </div>

        <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0">
        <tr>
        <td >
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; position :relative;">
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
        <tr>
          <td  style=" background-color:#FFA366;">
             <table style="width:100%; text-align : center " cellpadding="0" cellspacing="0" border="0">
                <tr >
                    <td id="tdBackup" runat="server" style="border-right: 1px solid #000000;width:100px;">
                        <asp:Button ID="btnBackUp" CssClass="btnNormal"  Text=" Backup " Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="1" />
                    </td>
                    <td id="tdExport" runat="server" style="border-right: 1px solid #000000;width:100px   ;">
                        <asp:Button ID="btnExport" CssClass="btnNormal"  Text=" Export " Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="2" />
                    </td>
                    <td id="tdImport" runat="server" style="border-right: 1px solid #000000;width:100px   ;">
                        <asp:Button ID="btnImport" CssClass="btnNormal"  Text=" Import " Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="3" />
                    </td>
                    <td id="tdMaintenance" runat="server" style="border-right: 1px solid #000000;width:100px   ;">
                        <asp:Button ID="btnMaintenance" CssClass="btnNormal"  Text=" Maintenance " Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="4" />
                    </td>
                    <td id="tdSmtpSettings" runat="server" style="border-right: 1px solid #000000;width:100px   ;">
                        <asp:Button ID="btnSmtpSettings" CssClass="btnNormal"  Text=" Smtp Settings " Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="5" />
                    </td>
                    <td id="tdRestHour" runat="server" style="border-right: 1px solid #000000;width:150px   ;">
                        <asp:Button ID="btnRestHour" CssClass="btnNormal"  Text=" Rest Hour Maintenance" Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="6" />
                    </td>
                    <td class="btnNormal">&nbsp;</td>
                </tr>
            </table>
          </td>
        </tr>
        
        <tr>                    
            <td style="border-top: solid 5px #007ACC;">
               <asp:Panel ID="pnlBackup" runat="server" Visible="false">
               <div style="height:430px; vertical-align:middle;">
                    <div style="padding:10px; font-size:larger;">
                      <b>  Select the database from below list and click on button to take database backup </b>
                    </div>
                    <br/><br/><br/><br/><br/>
                    <table border="0" width="100%" cellpadding="3">
                    <tr>
                     <td style="vertical-align:middle;">
                        <asp:DropDownList ID="ddlDB" runat="server" Width="200px">
                         <asp:ListItem Text="< Select Database > " Value=""></asp:ListItem>
                         <asp:ListItem Text="eMANAGER" Value="eMANAGER"></asp:ListItem>
                        <%-- <asp:ListItem Text="MTM-WatchKeeper" Value="MTM-WatchKeeper"></asp:ListItem>--%>
                        </asp:DropDownList>
                        </td>
                        </tr>
                    <tr>
                     <td style="vertical-align:middle;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="BU" ControlToValidate="ddlDB" ErrorMessage="Please select database to continue." runat="server"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                    <tr>
                     <td style="vertical-align:middle;">
                        <asp:Button ID="btnDBBackup" Text ="Take DataBase Backup" CssClass="color_tab" ValidationGroup="BU" OnClientClick="ShowModal();" OnClick="btnDBBackup_Click" Width="200px" runat="server" />
                     </td>
                     </tr>
                     </table>
               </div>
               </asp:Panel>
               <asp:Panel ID="pnlExport" runat="server" Visible="false">
               <div style="height:457px;">
                <iframe src="exporthome.aspx" frameborder="0" width="100%" height="457px" scrolling="no"></iframe>
               </div>
               </asp:Panel>
               <asp:Panel ID="pnlImport" runat="server" Visible="false">
               <div>
                   <table width="100%"  style="height:430px;">
                       <tr>
                           <td style="background-color:#a3ecdb;">
                           
                    <div style="padding:10px; font-size:larger; background-color:#32cea9"><b>  Import Packet From Office ( Auto Import from E-Mail Server )</b> </div>
                       <div style="padding:10px; margin:0 auto; text-align:center">
                       <asp:Button ID="btnImport2" Text =" Auto Import " OnClick="btnImport2_Click" OnClientClick="ShowModal();" CssClass="color_tab" runat="server" Width="150px" />
                             <asp:Button ID="btnImportedLog" Text =" View Log " OnClick="btnImportedLog_Refresh" CssClass="color_tab" runat="server" Width="150px" />
                       
                           <div id="messagebox" style="font-size:16px; text-align:center;font-weight:bold;"></div>
                     </div>
                     <div style="text-align:left;padding:5px;font-weight:bold;font-size:18px;border-bottom:solid 1px #55b19c; background-color:White"> Received Packet Log 
                          <div>
                             <asp:Repeater runat="server"></asp:Repeater>
                         </div>
                     </div>
                    <div style="height:300px;overflow-y:scroll;overflow-x:hidden;background-color:White" id="histmessagebox">
                        <table cellpadding="4" cellspacing="0" border="1" style="border-collapse:collapse; width:100%;" >
                            <tr style="font-weight:bold; background-color:#dcd9d9">
                                <td style="width:40px;">Sr#</td>
                                <td style="width:150px; ">Date Received</td>
                                <td style="text-align:left">Packet Name</td>
                      <%--          <td style="width:150px;">Imported By</td>--%>
                            </tr>
                       
                          <asp:Repeater runat="server" ID="rptpacketlog">
                              <ItemTemplate>
                               <tr>
                                <td style="text-align:center"><%#Eval("sno")%></td>
                                <td style="text-align:center"><%#ECommon.ToDateTimeString(Eval("ImportDate"))%></td>
                                <td style="text-align:left"><%#Eval("PacketName")%></td>
                               <%-- <td style="text-align:left"><%#Eval("ImportedBy")%></td>--%>
                            </tr>
                            </ItemTemplate>
                          </asp:Repeater>
                             </table>
                    </div>
                    </td>
                            <td style="width:400px; background-color:#f9e9d3">
                    <div style="padding:10px; font-size:larger;background-color:#f7af51;"><b>  Import Packet From Office ( Browse File & Import )</b> </div>
                         <div style="height:100px; padding-top:15px;margin:0 auto;">
                       <b>Select File :</b>&nbsp;<asp:FileUpload ID="flp_Import" runat="server" /><br /><br /> 
                       <asp:Button ID="btnImport1" Text =" Import Packet " OnClick="btnImport1_Click" OnClientClick="ShowModal();" CssClass="color_tab" runat="server" Width="150px" />
                      </div>
                          </td>
                       </tr>
                   </table>
                    </div>
                     
               </asp:Panel>
               <asp:Panel ID="pnlMaintenance" runat="server" Visible="false">
               <div style="height:430px;">
                 <table border="0" width="100%" height="430px" cellpadding="0" cellspacing="0">
                    <tr>
                     <%--<td style="width:50%;">
                     <div style="padding:10px; font-size:larger;">
                      <b> Maintenance from file given by office</b>
                     </div>
                      <div style="height:200px; padding-top:150px;">
                       <b>Select File :</b>&nbsp;<asp:FileUpload ID="fup" runat="server" /><br /><br /> 
                       <asp:Button ID="btnExecute" Text =" Execute " OnClick="btnExecute_Click" OnClientClick="ShowModal();" CssClass="color_tab" runat="server" />
                      </div>
                      </td>
                     <td style="width:1px; background-color:orange;">&nbsp;</td>--%>
                     <td>
                     <div style="padding:10px; font-size:larger;">
                      <b> Routine Maintenance </b>
                     </div>
                     <div style="height:200px; padding-top:150px;">
                     <asp:Button ID="btnDBMaintenance" Text =" PMS Due Date ReCalculation " OnClick="btnDBMaintenance_Click" OnClientClick="ShowModal();" CssClass="color_tab" runat="server" />
                     </div>
                     </td>
                    </tr>
                 </table>
                     
               </div>
               </asp:Panel>
               <asp:Panel ID="pnlSmtpSettings" runat="server" Visible="false">
               <div style="height:430px;">
                    <center>
                    <div style="padding:10px; font-size:larger;">
                      <b>  SMTP Settings For Mail Communication </b>
                    </div>
                    <br />
                    <table border="0" width="450px" cellpadding="3" cellspacing="0">
                    <tr><td style="text-align:left; width:120px;"><b>To Mail Address</b></td>
                        <td style="text-align:center; width:10px;">
                            :</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" id="txtToAddress" MaxLength="50" Width="250px" ReadOnly="true" ></asp:TextBox>
                    </td>
                    </tr>
                    <tr><td style="text-align:left; width:120px;"><b>From Address</b></td>
                        <td style="text-align:center; width:10px;">
                            :</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" id="txtSenderAddress" MaxLength="50" Width="250px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="MS" ControlToValidate="txtSenderAddress" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    </tr><tr><td style="text-align:left;"><b>SMTP Server</b></td>
                        <td style="text-align:center;">
                            :</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" id="txtMailClient" MaxLength="50" Width="250px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMailClient" ValidationGroup="MS" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                    <tr><td style="text-align:left; width:120px;"><b>SMTP User Name</b></td>
                        <td style="text-align:center; width:10px;">
                            :</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" id="txtSenderUserName" MaxLength="50" Width="250px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="MS" ControlToValidate="txtSenderUserName" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                    <tr><td style="text-align:left;"><b> Password</b></td>
                        <td style="text-align:center;">
                            :</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" id="txtSenderPassword" MaxLength="50" TextMode="Password" Width="250px"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSenderPassword" ValidationGroup="MS" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    </tr>

                    <tr><td style="text-align:left;"><b>Port </b></td>
                        <td style="text-align:center;">
                            :</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" id="txtPort" MaxLength="50" Width="250px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPort" ValidationGroup="MS" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                      <tr><td style="text-align:left;"><b>Encryption</b></td>
                          <td style="text-align:center;">
                              :</td>
                    <td style="text-align:left;">
                        <asp:DropDownList runat="server" ID="ddlEncryption" Width="255px">
                        <asp:ListItem Text="NA" Value=""></asp:ListItem>
                        <asp:ListItem Text="SSL" Value="SSL"></asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    </tr>
       
                     <tr><td style="text-align:left;"><b>Mode of Communication</b></td>
                          <td style="text-align:center;">
                              :</td>
                    <td style="text-align:left;">
                        <asp:DropDownList runat="server" ID="ddlMode" Width="255px">
                            <asp:ListItem Text="Auto" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Manual" Value="M"></asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    </tr>
       
                    <tr><td colspan="3" style="text-align:center; ">
                        <asp:Button runat="server" ID="btnSaveSettings" OnClientClick="ShowModal();" ValidationGroup="MS" Width="130px" CssClass="color_tab" Text="Save" OnClick="btnSaveSettings_Click" />
                        &nbsp;<asp:Button runat="server" ID="btnSendTestMail" ValidationGroup="MS" OnClientClick="ShowModal();" Width="130px" CssClass="color_tab" Text="Send Test Mail" OnClick="btnSendTestMail_Click" />
                    </td>
                    </tr>
                    </table>
                    </center>
               </div>
               </asp:Panel>
               <asp:Panel ID="pnlRestHour" runat="server" Visible="false">
               <div style="height:430px;">
                    <center>
                    <div style="padding:10px; font-size:larger;">
                      <b>  Update CrewNumber </b>
                    </div>
                    <br />
                    <table border="0" width="600px" cellpadding="3" cellspacing="0">
                    <tr>
                    <td style="text-align:left;"><b>Seelct Crew Member</b></td>
                    <td style="text-align:center;">:</td>
                    <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlCrew" Width="255px"></asp:DropDownList> 
                    <asp:CheckBox runat="server" ID="chkIna" Text="Include Inactive" AutoPostBack="true" OnCheckedChanged="chkIna_OnCheckedChanged" />
                    </td>
                    </tr>
                    <tr><td style="text-align:left;"><b>New Crew Number </b></td>
                    <td style="text-align:center;">:</td>
                    <td style="text-align:left;"><asp:TextBox runat="server" ID="txtCrew" Width="100px" MaxLength="6"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:center;">
                        <asp:Button runat="server" ID="btnUpdate" OnClientClick="ShowModal();" ValidationGroup="MS" Width="130px" CssClass="color_tab" Text="Update Crew" OnClick="btnbtnUpdateCrewNumber_Click" />
                        </td>
                    </tr>
                    </table>
                    </center>
               </div>
               </asp:Panel>
               
               <div style="padding:5px; background-color:#FFFFCC; text-align:left;" runat="server" id="dvMSG">
               &nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
               </div>
            </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        </td>
        </tr>
     </table>
     <div>
     </div>
     <mtm:footer ID="footer1" runat ="server" />
     </div>
        <script type="text/javascript">
            function getdata()
            {
                $("#messagebox").load("./Handler.ashx?key=POP3&" + Math.random()); 
                window.setTimeout(getdata, 3000);
            }
            getdata();
        </script>
    </form>
</body>
</html>
