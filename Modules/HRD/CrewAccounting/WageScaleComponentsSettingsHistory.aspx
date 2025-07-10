<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WageScaleComponentsSettingsHistory.aspx.cs" Inherits="Modules_HRD_CrewAccounting_WageScaleComponentsSettingsHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
 <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <%--  <link rel="stylesheet" type="text/css" href="../Styles/style.css" />
   <link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
   <%--  <link href="../styles/mystyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
        .FixedHeader {
            position: sticky !important;
            font-weight: bold;
        }
    .gridview td {
    padding: 2px;
}


.gridview th {
    padding: 2px;
}
 .columnscss
{

padding-left: 5px; 
background-color: Black;
	height: 23px;
	border: 1px solid #bbbbbb;
	font-family: Arial, sans-serif;
	font-weight: bold;
	font-size: 12px;
	position: relative;
	cursor: default;
	top: expression(this.parentNode.parentNode.parentNode.scrollTop-2);
	z-index: 0;
	padding-left: 5px;
	text-align: left;
	color:#fff;
    width:200px;
}     
</style> 
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family:Arial, Helvetica, sans-serif;font-size:12px;">
               <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <script language="javascript" type="text/javascript">

            function CheckUnit(txtname) {
                var txt = txtname;

                if (isNaN(document.getElementById(txtname.id).value)) {
                    alert("Please enter numbers only");
                    document.getElementById(txtname.id).value = "0.00";
                    document.getElementById(txtname.id).focus();
                    return false;
                }

                else if (parseFloat(document.getElementById(txtname.id).value) < 0) {
                    alert("Value should be greater than or equal to zero");
                    document.getElementById(txtname.id).value = "0.00";
                    document.getElementById(txtname.id).focus();
                    return false;
                }
                else {
                    document.getElementById(txtname.id).value = roundNumber(document.getElementById(txtname.id).value, 2);
                }
            }
            function roundNumber(num, dec) {
                var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
                return result;


            } 
           
        </script>
   <div class="stickyHeader" style="height:70px">
       <div class="text headerband">Wage Scale History</div>
<div style="padding:5px">
        <table width="100%" cellpadding="3" cellspacing="0" >
        <tr>
            <td style=" text-align: right;width:100px;">Wage Scale :&nbsp;</td>
            <td style="text-align: left;width:150px;"><asp:Label runat="server" ID="lblWageScaleName"></asp:Label></td>
            <td style="text-align: right;width:120px;">Seniority ( Year ) :&nbsp;</td>
            <td style="text-align: left; width:70px;"><asp:Label runat="server" ID="lblSeniority"></asp:Label></td>
            <td style="text-align: right;width:100px;">Effective From :&nbsp;</td>
            <td style="text-align: left;width:100px;"> <asp:Label runat="server" ID="lblEffectiveFrom"></asp:Label> </td>
             <td style="text-align: right;width:100px;"> Currency : &nbsp;
            </td>
            <td style="text-align: left;width:150px;"> 
                <asp:Label runat="server" ID="lblWageScaleCurrency"></asp:Label>
            </td>
        </tr>
        </table>
     </div>
</div>
<div style="height:70px">&nbsp;</div>
<div style='padding-bottom:50px;padding-left:50px; font-family:Arial;font-size:12px;'>
    <table width="98%" height="500px;" >
        <tr>
            <td style="vertical-align:top;padding-right:10px;">
                <div style="overflow-x:hidden; overflow-y :auto; height:475px;  border:solid 1px gray;" >
                <table  cellspacing="0" border="0" style="border-collapse: collapse; color: black;width:95%; " class="bordered">
                
                <tr style="font-family: Arial, sans-serif;	font-weight:bold;">
               <td>
                   <asp:GridView ID="gvWageScaleRankHistory" runat="server" AutoGenerateColumns="true" HeaderStyle-CssClass="FixedHeader" OnRowDataBound="gvWageScaleRankHistory_RowDataBound" >
                     
                                                                           
                         <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                            
                                                                       
                                                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                            <RowStyle CssClass="rowstyle" />
                   </asp:GridView>
               </td>           
                </tr>
                </table>
                </div>
            </td>
           
        </tr>
        
        
    </table>
    
</div>

        


        </div>
    </form>
</body>
</html>
