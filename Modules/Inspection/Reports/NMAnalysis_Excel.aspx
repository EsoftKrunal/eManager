<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NMAnalysis_Excel.aspx.cs" Inherits="NMAnalysis_Excel" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VPRS Noon Report</title>
</head>
<body>
<form id="form1" runat="server">




    <table width="98%" cellspacing="1" border="1" style="border: solid 1px #4371a5; border-collapse: collapse; padding: 4px;">
        <tr style="background-color: #4371a5; font-weight: bold;">
            <td style="color: White;" >
                Vessel Code
            </td>
            <td style="color: White;" >
                NM Month
            </td>
            <td style="color: White;" >
                NM Details
            </td>
           <td style="color: White;" >Lack of Knowledge/Skill</td>
        <td style="color: White;" >Lack of Experience</td>
        <td style="color: White;" >Lack of Motivation</td>
        <td style="color: White;" >Lack of Rest Period</td>
        <td style="color: White;" >Heavy Workload</td>
        <td style="color: White;" >Stress</td>
        <td style="color: White;" >Physical Capability/Health Factors</td>
        <td style="color: White;" >Inadequate design/material/construction/installation</td>
        <td style="color: White;" >Inadequate/Lack of Procedures</td>
        <td style="color: White;" >Inadequate Tools & Equipments</td>
        <td style="color: White;" >Inadequate Maintenance</td>
        <td style="color: White;" >Inadequate Leadership/Supervision</td>
        <td style="color: White;" >Ineffective Training</td>
        <td style="color: White;" >Inadequate Selection/Recruitment</td>
        <td style="color: White;" >Incorrect Purchase</td>
        <td style="color: White;" >Inadequate Management of Change</td>
            
        </tr>
        <asp:Literal ID="ltr" runat="server"></asp:Literal>
    </table>
</form>
</body>
</html>
