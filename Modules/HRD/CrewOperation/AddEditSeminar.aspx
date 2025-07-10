<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditSeminar.aspx.cs" Inherits="AddEditSeminar" Title="Add Edit Seminar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <script src="../JS/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .highlight
        {
            margin:3px 0px 3px 0px;
            background-color:#F0F5FF;
        }
        .highlight:hover
        {
           background-color:#FFB2D1;
        }
        
       *{
          font-family:calibri;  
          font-size:14px;
        }
        body
        {
            margin:0px;
        }
        .selectedrow
        {
            background-color : lightgray;
            color :White; 
            cursor:pointer;
        }
        .row
        {
            background-color : White;
            color :Black;
            cursor:pointer; 
        }
        hr
        {
            margin:0px;
            padding:2px;
        }
        .btn
        {
            background-color:#005CE6;
            color:White;
            border:solid 1px #005CE6;
            padding:4px;
        }
        .btnred
        {
            background-color:red;
            color:White;
            border:solid 1px red;
            padding:4px;
        }
        .mandate
        {
            background-color:#ffffcc !important;
            border:solid 1px grey;
            padding:2px;
        }
       .aquaScroll {
          scrollbar-base-color: bisque;
          scrollbar-arrow-color: #7094FF;
          border-color: orange;
          overflow-x:hidden; 
          overflow-y:scroll;
        }
        .changed
        {
            border:solid 1px red;
        }
        .saved
        {
            border:solid 1px green;
        }
    </style>
    <script type="text/javascript" src="../JS/KPIScript.js"></script>
    
   <link rel="stylesheet" type="text/css" href="../Styles/jquery.datetimepicker.css"/>
        <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</head>
<body >
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
            <table cellpadding="0" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align: center;">
            <tr>
                <td style="text-align: center; padding:8px; font-size:14px;  "  colspan="3" class="text headerband">
                    <b>MISS &amp; Seminar</b>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; padding:8px; font-size:14px; color:#666666; vertical-align:top; border-right:solid 1px #c2c2c2;">
                    <table style="border-collapse:collapse;width:100%" cellpadding="2" cellspacing="0" border="0">
                    <colgroup>
                    <col />
                    </colgroup>
                    <tr>
                    <td style="text-align:left">
                        <b>Recruiting Office :</b></td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                       <asp:DropDownList runat="server" ID="ddloffice"  CssClass="mandate" Width="95%"></asp:DropDownList>
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddloffice" ErrorMessage="*" ValidationGroup="main"></asp:RequiredFieldValidator>
                        </td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <b>Category :</b></td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <asp:DropDownList runat="server"  ID="ddlCategory" CssClass="mandate" Width="95%"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="ddlCategory" ErrorMessage="*" ValidationGroup="main"></asp:RequiredFieldValidator>
                        </td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <b>Plan Duration :</b></td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <asp:TextBox runat="server" ID="txtFdt" MaxLength="15" Width="90px" CssClass="dateonly mandate"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtFdt" ErrorMessage="*" ValidationGroup="main"></asp:RequiredFieldValidator>
                        &nbsp;
                        <asp:TextBox runat="server" ID="txtTdt" MaxLength="15" Width="90px"  CssClass="dateonly mandate"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtTdt" ErrorMessage="*" ValidationGroup="main"></asp:RequiredFieldValidator>
                        </td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <b>Event Location :</b></td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <asp:TextBox runat="server" ID="txtLocation" MaxLength="50" Width="95%"  
                            CssClass="mandate"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtLocation" ErrorMessage="*" ValidationGroup="main"></asp:RequiredFieldValidator>
                        </td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                        <b>Remarks :</b></td> 
                    </tr>
                    <tr>
                    <td style="text-align:left">
                    <asp:TextBox runat="server" ID="txtTopic" TextMode="MultiLine" Rows="3" 
                            Width="95%"  CssClass="mandate"></asp:TextBox>
                        </td> 
                    </tr>
                    </table>
                    <div style="">
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtTopic" ErrorMessage="*" ValidationGroup="main"></asp:RequiredFieldValidator>
                    </div>
                 
                </td>
                <td style="vertical-align:top; width:300px; padding-left:5px;">
                <div style="text-align: center; padding:8px; font-size:14px;  margin-top:2px;" class="text headerband">
                <b>Presenters</b>
                </div>
                <div>
                <div style="height:310px; overflow-x:hidden; overflow-y:scroll; text-align:left;">
                    <asp:CheckBoxList runat="server" id="chkPresenters"></asp:CheckBoxList>
                </div>
                </div>
                 
                </td>
                <td style="width:130px; vertical-align:bottom;">
                   <div style="padding:5px">

                    <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" class="btn" width="100px" CausesValidation="true" ValidationGroup="main"/>   
                    </div>
                </td>
            </tr>
           
            </table>
            <div style="position:fixed;bottom:0px; left:0px; text-align:left;background-color:#E2EAFF; width:100%; padding:5px;">
                 <asp:Label runat="server" ID="lblMessage" Font-Size="20px" Font-Bold="true"></asp:Label>
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
    </form>
</body>
</html>
