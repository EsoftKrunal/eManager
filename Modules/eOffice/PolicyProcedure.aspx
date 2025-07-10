<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PolicyProcedure.aspx.cs" Inherits="emtm_Emtm_Performance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emtm > Performance</title>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <div>
          <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
          <td style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :6px; font-weight: bold;">
              Policies &amp; Procedures</td>
          </tr>
          <tr>
          <td > 
                <table cellpadding="0" cellspacing="3" border="0" width="100%" style="height: 101px">
                  <tr style="background-color:#FFCC80; color:gray; height:20px">
                      <td style="padding:5px; color:#333333; font-size:14px; text-align: center;">Office Policies &amp; Procedures</td>
                      <td style="padding:5px; color:#333333; font-size:14px; text-align: center;">SMD Procedures</td>
                 </tr>
                <tr>
                <td style="vertical-align:top; width:50%; text-align: center;">
                    <br />
                    <asp:ImageButton runat="server" OnClick="imgDownloadManual_OnClick" ImageUrl="~/Modules/HRD/Images/officeemp_32.png" /> 
                    
                </td>
                <td style="vertical-align:top; text-align: center;">
                    <br />
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="imgDownloadManual2_OnClick" ImageUrl="~/Modules/HRD/Images/tools_32.png" /> 
                </tr>
                </table>
          </td>
          </tr>
          </table>

            <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
          <td style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :6px; font-weight: bold;">
              Company Policies </td>
          </tr>
          <tr>
          <td > 
                <table cellpadding="0" cellspacing="5" border="0" width="100%">
                <tr><td colspan="8">&nbsp;</td></tr>
                <tr>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/safety_p.pdf" target="_blank">Safety and Quality Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/Drug_p.pdf" target="_blank">Drug and Alcohol Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/hr_p.pdf" target="_blank">Human Resource and Health Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/ethics_p.pdf" target="_blank">Ethics Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/env_p.pdf" target="_blank">Environment Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/security_p.pdf" target="_blank">Security Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/ecdis_p.pdf" target="_blank">ECDIS Policy</a></td>
                <td style="vertical-align:top; text-align: center;"><a href="../policies/energy_p.pdf" target="_blank">Energy Management Policy</a></td>
               
                </tr>
                </table>
          </td>
          </tr>
          <tr>
          <td>
          <br />
           <table cellpadding="0" cellspacing="3" border="0" width="100%" style="height: 101px">
                  <tr style="background-color:#FFCC80; color:gray; height:20px">
                      <td style="padding:5px; color:#333333; font-size:14px; text-align: center;">Vision</td>
                      <td style="padding:5px; color:#333333; font-size:14px; text-align: center;">Mission</td>
                 </tr>
                <tr>
                <td style="vertical-align:top; width:50%; text-align: center; padding:10px;">
                   <b>To achieve excellence in Ship Management</b></td>
                <td style="vertical-align:top; text-align: left;padding:10px;">
                    <b>M.T.M. is committed to</b>
                    <ul>
                    <li>Holistic development of both ship and shore personnel</li>
                    <li>Dedication to safety and preservation of the environment through targets of zero spills and incidents</li>
                    <li>Self assessment for continuous improvement</li>
                    <li>Innovation</li>
                    <li>Integrity</li>
                    <li>Leading by Example</li>
                    </ul>
                    </tr>
                </table>
          </td>
          </tr>
          </table>
          </div>
    </div>
    </form>
</body>
</html>
