<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosureMarks_PopUp.aspx.cs" Inherits="ClosureMarks_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function SaveWin()
        {
            var aa = document.getElementById("txtObsDef").value;
            window.opener.document.getElementById("txtdeficiency").value = aa;         
            window.close();
        }
    </script>
    <style type="text/css">
    .required_box
    {
    width:50px;
    }
    </style> 
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" ></ajaxToolkit:ToolkitScriptManager> 
    <div>
        <br />
        <br />
        <center>
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="3" 
                    style="height:23px; text-align :center " 
                    class="text headerband">Marks Details</td>
            </tr>
            <tr>
                <td style ="text-align :center " colspan="3" ><asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style=" text-align :center; text-decoration :underline "><strong><em>Calculation Inclusive N/A Values</em></strong></td>
                <td style=" text-align :center ">&nbsp;</td>
                <td style=" text-align :center; text-decoration :underline "><strong><em>Calculation Exclusive N/A Values</em></strong></td>
            </tr>
            <tr>
                <td style="text-align: right">
                <table>
                    <tr>
                        <td style="text-align: right">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td style="text-align: right">Total Questions :</td>
                        <td><asp:TextBox ID="txtQustions" runat="server" CssClass="required_box" MaxLength="5"></asp:TextBox></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQustions" ErrorMessage="*" ></asp:RequiredFieldValidator> </td>
                        <td style="text-align: right">Negative Stat :</td>
                        <td><asp:TextBox ID="txtNeg" runat="server" CssClass="required_box" MaxLength="2"></asp:TextBox></td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNeg" ErrorMessage="*"></asp:RequiredFieldValidator> </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">Total Negative :</td>
                        <td><asp:TextBox ID="txtTotNeg" runat="server" CssClass="required_box" MaxLength="5"></asp:TextBox></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTotNeg" ErrorMessage="*"></asp:RequiredFieldValidator> </td>
                        <td style="text-align: right">Negative Reco :</td>
                        <td><asp:TextBox ID="txtNegReco" runat="server" CssClass="required_box" MaxLength="2"></asp:TextBox></td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNegReco" ErrorMessage="*"></asp:RequiredFieldValidator> </td>
                    </tr> 
                    <tr>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: right">Compliance :</td>
                        <td><asp:TextBox ID="txtPercComp" runat="server" CssClass="required_box" MaxLength="5"></asp:TextBox></td>
                        <td style="text-align: left">%</td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPercComp" ErrorMessage="*"></asp:RequiredFieldValidator> </td>
                        <td style="text-align: right">Negative Desi :</td>
                        <td><asp:TextBox ID="txtNegDesi" runat="server" CssClass="required_box" MaxLength="2"></asp:TextBox></td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNegDesi" ErrorMessage="*"></asp:RequiredFieldValidator> </td>
                    </tr>
                </table> 
                </td>
                <td>
                    &nbsp;</td>
                <td>
                <table>
                    <tr>
                        <td style="text-align: right">Total Questions :</td>
                        <td><asp:TextBox ID="txtQustions1" runat="server" CssClass="required_box" MaxLength="5"></asp:TextBox></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQustions1" ErrorMessage="*" ></asp:RequiredFieldValidator> </td>
                        <td style="text-align: right">Negative Stat :</td>
                        <td><asp:TextBox ID="txtNeg1" runat="server" CssClass="required_box" MaxLength="2"></asp:TextBox></td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNeg1" ErrorMessage="*"></asp:RequiredFieldValidator> </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Total Negative :</td>
                        <td><asp:TextBox ID="txtTotNeg1" runat="server" CssClass="required_box" MaxLength="5"></asp:TextBox></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTotNeg1" ErrorMessage="*" ></asp:RequiredFieldValidator> </td>
                        <td style="text-align: right">Negative Reco :</td>
                        <td><asp:TextBox ID="txtNegReco1" runat="server" CssClass="required_box" MaxLength="2"></asp:TextBox></td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNegReco1" ErrorMessage="*" ></asp:RequiredFieldValidator> </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Compliance :</td>
                        <td><asp:TextBox ID="txtPercComp1" runat="server" CssClass="required_box" MaxLength="5"></asp:TextBox></td>
                        <td style="text-align: left">%</td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPercComp1" ErrorMessage="*" ></asp:RequiredFieldValidator> </td>
                        <td style="text-align: right">Negative Desi :</td>
                        <td><asp:TextBox ID="txtNegDesi1" runat="server" CssClass="required_box" MaxLength="2"></asp:TextBox></td>
                        <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNegDesi1" ErrorMessage="*" ></asp:RequiredFieldValidator> </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style=" text-align :center "><asp:Button ID="btnSave" CssClass="btn" runat="server" Text="Save" onclick="btnSave_Click"/></td>
                <td style="text-align :right; padding-right:15px; " ><span style="color :Red "> * Required Fields </span>
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789" ID="FilteredTextBoxExtender1" TargetControlID="txtNeg" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789" ID="FilteredTextBoxExtender2" TargetControlID="txtNegReco" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789" ID="FilteredTextBoxExtender3" TargetControlID="txtNegDesi" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   


                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789" ID="FilteredTextBoxExtender4" TargetControlID="txtNeg1" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789" ID="FilteredTextBoxExtender5" TargetControlID="txtNegReco1" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789" ID="FilteredTextBoxExtender6" TargetControlID="txtNegDesi1" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    
                    
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789." ID="FilteredTextBoxExtender7" TargetControlID="txtQustions" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789." ID="FilteredTextBoxExtender8" TargetControlID="txtTotNeg" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789." ID="FilteredTextBoxExtender9" TargetControlID="txtPercComp" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   


                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789." ID="FilteredTextBoxExtender10" TargetControlID="txtQustions1" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789." ID="FilteredTextBoxExtender11" TargetControlID="txtTotNeg1" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                    <ajaxToolkit:FilteredTextBoxExtender FilterMode="ValidChars" ValidChars="0123456789." ID="FilteredTextBoxExtender12" TargetControlID="txtPercComp1" runat="server"></ajaxToolkit:FilteredTextBoxExtender>   
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td style=" text-align :center ">
                    &nbsp;</td>
            </tr>
            </table> 
    </center>
    </div>
    </form>
    </body>
</html>
