<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupMonitorTrainingDetails.aspx.cs" Inherits="CrewOperation_popupMonitorTrainingDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="~/Styles/StyleSheet.css" />
    <style type="text/css">
        .MainRow
        {
            padding:3px; 
            background-color: #80CCFF;
            font-size: 13px;
            text-align:left;
        }
         .spanCrewNumber 
        {
            
        }
          .spanCrewName
        {
            color:Blue;
        }
           .spanRank
        {
             color:#FF00FF;
        }
        .ChildRow
        {
            padding:5px; 
            font-size: 12px;
            text-align:left;
        }
         .ChildRow table thead tr td
        {
            font-weight:bold;
            background-color: #1C5E55;
            color: #ffffff;
        }
       
        .ChildRow table tbody tr td
        {
            <%--border:solid 1px #33ADFF;--%>
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border-collapse: collapse; border: solid 2px #4371a5" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="border: #4371a5 1px solid; background-color: #4371a5; color: White; height: 25px;">
                    <span style="font-size: 15px;"><asp:Label runat="server" ID="lblPageTitle"></asp:Label></span>
                </td>
            </tr>
           <tr>
           <td style="border: #FFECFF 1px solid; background-color: #FFECFF; color:Black; height: 25px;">
               <span style="font-size: 15px;"><asp:Label runat="server" ID="lblVesselName"></asp:Label></span>
           </td>
           </tr>
            <tr>
                <td><asp:Literal runat="server" ID="litTraining"></asp:Literal></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
