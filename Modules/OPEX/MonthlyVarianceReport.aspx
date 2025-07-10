<%@ Page Title="EMANAGER" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MonthlyVarianceReport.aspx.cs" Inherits="Modules_OPEX_MonthlyVarianceReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <%--<link href="CSS/style.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/Common.js"></script>--%>
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
      <script src="JS/jquery-1.4.2.min.js" type="text/javascript"></script>
     <%--<script type="text/javascript" src="JS/BudgetScript.js"></script>--%>
     
  <%--  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet" />--%>
     <style type="text/css">
 
         .withpad tbody td
{
    padding-top: 2px;
    padding-bottom: 2px;
    text-align:right;
}
        
     </style>
    <style type="text/css">
        .table-responsive {
	display: block;
	width: 100%;
	overflow-x: auto;
	-webkit-overflow-scrolling: touch;
	-ms-overflow-style: -ms-autohiding-scrollbar;
}
    </style>
    <style>
        @page {
            size: A4 landscape;
            margin-top: 8px;
            margin-bottom: 10px;
            margin-left: 20px;
        }
    </style>
    <style>
        /* Three image containers (use 25% for four, and 50% for two, etc) */ 
        @media print {
            .box-body {
                margin-top: 10px !important;
                margin-bottom: 10px;
            }
            .divHeader {
                margin-top: 40px !important;
                margin-bottom: 10px;
            }
        }
    </style>
   <%-- <script type="text/javascript" >
        function SetAlterNateRow(row) {
            row.style.color = 'Red';
            row.style.textDecoration = 'underline';
            row.style.cursor= 'default';
            
//            .setAttribute("oldclassName", row.getAttribute("className"));            
//            row.setAttribute("className", "selectedrow1");    
        }
        function SetRow(row) {
            row.style.color = 'Black';
            row.style.textDecoration = 'none';
        }
    </script>--%>
    <script type="text/javascript">
        function printPageArea(areaID) {
            var printContent = document.getElementById(areaID).innerHTML;
            var originalContent = document.body.innerHTML;
            document.body.innerHTML = printContent;
            window.print();
            document.body.innerHTML = originalContent;
        }
    </script>
    <style type="text/css">
       
        .rpt-table1 td {
            border:none;
        }
    .imgMajor{
    background-image:url('../HRD/Images/arrow-right.png');
    background-repeat:no-repeat;
    background-position:center;
    width: 16px;
    height: 16px;
      }
      .arrow-down{
          background-image:url('../HRD/Images/arrow-down.png');
          background-repeat:no-repeat;
          background-position:center;
          width: 16px;
          height: 16px;
      }
    body
    {
        font-family:Arial;
        font-size:13px !important;
        margin:0px;
    }
    td,th
    {
        font-size:12px !important;
    }
    *
    {
        box-sizing:border-box;
    }
    .normalcell,.red,.green{
        cursor:pointer;
    }
    .normalcell:hover,.red:hover,.green:hover{
        background-color:yellow;
    }
    .tblMonth td{
        text-align:center;
        font-weight:bold;
    }
    .Budgeted {
         background-color: #148a1a;
    border: 0px solid #a0f161;
    display: inline-block;
    padding:4px 7px;
    text-align:center;
    color:white;
    }
    .UnBudgeted {
    background-color: #f63f2d;
    border: 0px solid #a0f161;
    display: inline-block;    
    padding:4px 7px;
    text-align:center;
    color:white;
    }
        .green {
            background-color:#148a1a;
        }
        .red {
            background-color:#f63f2d;
        }
   /* .btn
    {
        background-color:#394b65;
        color:White;
        border:none;
        padding:5px 10px 5px 10px;        
    }   
    .btn:hover
    {
        background-color:#0052CC;
    }*/ 
    .right_align
    {
        text-align:right;
    }
    .columnHead
    {
        background-color:#c2c2c2;
        height:50px;
        color:Black;
    }
    .newinput
    {
        border:solid 1px #c2c2c2;
        font-size:12px;
        padding:2px;
        text-align:right;
    }
   /* .nav
    {
        margin:0px;
        padding:0px;
        width:100%;
    }*/
    .inactive_tab
    {
        padding:4px;
        background-color:White;
        border:solid 1px #4D70DB;
        border-bottom:solid 1px white;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        color:Black;
    }
     .inactive_tab a
    {
        color:Grey;
    }
    .active_tab
    {
        padding:4px;
        background-color:#4D70DB;
        border:solid 1px #4D70DB;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        border-bottom:solid 1px #4D70DB;
    }
     .active_tab a
    { color:White; }
    .bl
    {   border-left:solid 1px #dddddd;height:25px;}
    .br
    {   border-right:solid 1px #dddddd;height:25px;}

    
    input[type='text'],select
    {
        line-height:20px;
        height:20px;
        padding-left:5px;
        border:solid 1px #c2c2c2;
        vertical-align:middle;
    }
    
    td{
        vertical-align:middle;
    }
    .table-centered tr td
    {
        text-align:center !important;
    }
    .red{
        background-color:#f38585;
    }
.error_msg
{
background-color:#fbb3b3 !important
}
    </style>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js "></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js "></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js "></script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300" EnablePartialRendering="false" ></asp:ToolkitScriptManager> --%>  
     <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>
    <div style="font-family:Arial;font-size:12px;">
        <div>
            <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="Text headerband">
                       Budget Variance Report
                   </td>
                </tr>
                </table>                
                <table cellpadding="2" cellspacing="1" width="100%" border="1" style="border-collapse:collapse;">
                    <colgroup>
                <col width="15%;" />
                <col width="15%;" />
                <col width="15%;" />
                <col width="15%;" />
                <col />
                <col />
                        </colgroup>
                 <tr align="center" style="font-weight:bold">
                        <td >Company</td>
                        <td >Year</td>   
                        <td >Vessel</td>
                        <td ><%-- Report Level--%></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr style="text-align:left;padding-left:5px;">
                       
                        <td style="text-align:left;padding-left:5px;">
                            <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" Width="100px" Height="25px" ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="ddlCompany" ErrorMessage="*" ></asp:RequiredFieldValidator>
                        </td>
                         <td>
                            <asp:DropDownList ID="ddlyear" runat="server" Width="100px" OnSelectedIndexChanged="ddlyear_OnSelectedIndexChanged" AutoPostBack="true" Height="25px" ></asp:DropDownList>
                        </td>
                        <td style="text-align:left;padding-left:5px;">
                            <asp:DropDownList ID="ddlVessel" runat="server" OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged" AutoPostBack="true" Width="160px" Height="25px" ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="ddlVessel" ErrorMessage="*" ></asp:RequiredFieldValidator>
                        </td>
                        <td  valign="middle" style="text-align:left;padding-left:5px;">
                           <%-- <asp:DropDownList ID="ddlReportLevel" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportLevel_OnSelectedIndexChanged"  >
                             <asp:ListItem  Value="" Selected="True"> Select</asp:ListItem>
                            <asp:ListItem  Value="1"> General Summary</asp:ListItem>
                            <asp:ListItem  Value="2"> Budget Summary</asp:ListItem>
                            <asp:ListItem  Value="3"> Account Summary</asp:ListItem>
                            <asp:ListItem  Value="4"> Account Details</asp:ListItem>
                           
                            </asp:DropDownList>--%>
                            
                        </td>
                        <td style="text-align:left;padding-left:10px;">
                            <asp:Button ID="imgSearch" runat="server" Text="Search" CssClass="btn" onclick="imgSearch_Click" />
                            <asp:Button ID="imgClear" runat="server" Text="Clear" onclick="imgClear_Click" CssClass="btn" CausesValidation="false"  />
                            <%--<asp:Button ID="imgPrint" runat="server" Text="Variance Report" CssClass="btn"  />--%>
                             <a href="javascript:void(0);" onclick="printPageArea('printableArea')" class="btn btn-create">Print</a>
                        </td>
                        <td style="text-align:center;">
                            <asp:Label ID="lblTargetUtilisation" runat="server" ForeColor="Red" ></asp:Label>
                        </td>
                    </tr>
                </table>
        </div>
        <br />
        <div id="printableArea">
                        
                        <div id="divHeader" runat="server" visible="false" class="Text headerband divHeader" style="text-align:center;font-family:Arial;font-size:14px;">
                           Budget Variance Report :  <asp:Label ID="lblSearchText" Font-Bold="true" runat="server" ></asp:Label>
                        </div>
                       <div style="font-family:Arial;font-size:11px;" class="table-responsive"> 
                      
                <table cellpadding="0" cellspacing="0"     border='1' style='border-collapse:collapse;' bordercolor="white">
                <thead>
                <tr  class= "headerstylegrid">
                    <td style="width:50px;"> <b> </b>  </td>
                    <td style="width:150px;" >  </td>
                    <td colspan="12" style="width:1200px; font-weight:bold;">Actuals (Month wise) </td>
                    <td colspan="6"  style="width:600px; font-weight:bold;">YEAR TO DATE</td>
                    <td colspan="3" style="width:300px; font-weight:bold;">ANNUAL</td>
               </tr>
                <tr align="left"    class= "headerstylegrid">
                    <td style="width:50px;"></td>
                    <td style="width:150px;"><b>Account Head </b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon1" runat="server" Text="JAN"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon2" runat="server" Text="FEB"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon3" runat="server" Text="MAR"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon4" runat="server" Text="APR"></asp:Label> </b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon5" runat="server" Text="MAY"></asp:Label> </b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon6" runat="server" Text="JUN"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon7" runat="server" Text="JUL"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon8" runat="server" Text="AUG"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon9" runat="server" Text="SEP"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon10" runat="server" Text="OCT"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon11" runat="server" Text="NOV"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b><asp:Label ID="lblMon12" runat="server" Text="DEC"></asp:Label></b>
                    </td>
                    <td style="width:100px;">
                        <b>Total Actuals</b>
                    </td>
                    <td style="width:100px;">
                        <b>Comm.</b>
                    </td>
                    <td style="width:100px;">
                       <b>Cons.</b>
                    </td>
                    <td style="width:100px;">
                          <b>Budget</b>
                    </td>
                    <td style="width:100px;">
                        <b>Var $</b>
                    </td>
                    <td style="width:100px;">
                       <b>V %</b>
                    </td>
                    <td style="width:100px;">
                       <b>Budget</b>
                    </td>
                    <td  style="width:100px;">
                       <b>Util'n</b>
                    </td>
                    <td style="width:20px;">

                    </td>
                    
                </tr>
                </thead>
                   
                     </table>
                
                       <div>
                             <asp:Repeater ID="rptData" runat="server">
                <ItemTemplate>
                     <div class='section'>
                     <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; " class="rpt-table" >
                <tr class="group-1" style="font-weight:bold; "  > 
                    <td style="width:50px;" ><img style="cursor:pointer" class="imgMajor" majcatid='<%#Eval("MAJCATID")%>' onclick="ShowMidCat(this)" /></td>
                    <td style="width:150px;text-align:left;" > <%#Eval("MajorCat")%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
                    <td style="width:100px;">
                       <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                         <span class='<%# SetColorForVPer(Eval("Col1"))%>'> 
                             <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> 
                           <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
                    </td>
                    <td style="width:20px;">

                    </td>
                </tr>
    </table> 
                    <div class='expand' ></div>
                    </div>
                </ItemTemplate>
                </asp:Repeater>
                <%--<table cellpadding="0" cellspacing="0" width="100%" border="1" class="newformat withpad" style='border-collapse:collapse; text-align:right;'>
                <tbody>--%>
                                                       
               <%-- </tbody>
                </table>--%>
                </div>
           <asp:Repeater ID="rptMidCats" runat="server">
        <ItemTemplate>
        <div class='section'>
        <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; " class="rpt-table" >
        <tr class="group-2">
        <td style="width:30px;">
            <img style="cursor:pointer" class="imgMajor" majcatid='<%#Eval("MAJCATID")%>' midcatid='<%#Eval("midcatid")%>' onclick="ShowMinCat(this)" />
        </td>
        <td style="width:135px;text-align:left;"  > <%#Eval("MIDCAT") %> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
        <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
        <td style="width:100px;">
            <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
        </td>
        <td style="width:100px;">
            <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
        </td>
        <td style="width:100px;">
            <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
        </td>
        <td style="width:100px;">
            <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
        </td>
        <td style="width:100px;">
            <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
        </td>
        <td style="width:100px;">
                <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
        </td>
                   
        <td style="width:100px;">
            <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
        </td>
        <td style="width:100px;">
            <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
        </td>
                    
        </tr>
        </table>
        <div class='expand' ></div>
        </div>
        </ItemTemplate>
        </asp:Repeater>
           <asp:Repeater ID="rptMinCats" runat="server">
                <ItemTemplate>
                <div class='section'>
            <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%;" class="rpt-table" >
                    <tr class="group-3"> 
                    <td style="width:30px;">
                        <img style="cursor:pointer" class="imgMajor" majcatid='<%#Eval("MAJCATID")%>' midcatid='<%#Eval("midcatid")%>' mincatid='<%#Eval("mincatid")%>' onclick="ShowAccounts(this)" />
                    </td>  
                    <td style="width:130px;text-align:left;"  > <%#Eval("MINORCAT") %> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
                    <td style="width:100px;">
                       <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                         <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                    
                    <td style="width:100px;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
                    </td>
                    
                </tr>
                </table>
            <div class='expand' ></div>
            </div>
                </ItemTemplate>
                </asp:Repeater>      
               <asp:Repeater ID="rptAccounts" runat="server">
                <ItemTemplate>
                <div class='section'>
            <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%;" class="rpt-table" >
                    <tr class="group-4"> 
                    <%--<td style="width:2%;"></td> --%>
                    <td style="width:110px;text-align:left;" colspan="2" > <%#Eval("ACCOUNTNumber") %> - <%#Eval("ACCOUNTNAME") %> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
                    <td style="width:100px;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
                    <td style="width:100px;">
                       <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                         <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                    
                    <td style="width:100px;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:100px;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
                    </td>
                    
                </tr>
                 </table>
            <div class='expand' ></div>
            </div>
                </ItemTemplate>
                </asp:Repeater>
                    <%--<div id="divBudgetSummary" runat="server" visible="false" style="padding-top:5px;">  
                <div style="overflow-y:scroll;overflow-x:hidden; height:60px;">
                <table cellpadding="0" cellspacing="0" width='100%' class='newformat' height="60px" border='1' style='border-collapse:collapse; ' bordercolor="white">
                <thead>
                <tr style="height:25px;">
                    <td style="width:15%;border-color:Black;" >  </td>
                    <td colspan="12" style=" border-color:Black; width:51%; font-weight:bold;">Actuals (Month wise) </td>
                    <td colspan="6"  style="border-color:Black; width:25.5%; font-weight:bold;">YEAR TO DATE</td>
                    <td colspan="2" style="border-color:Black;width:8.5%; font-weight:bold;">ANNUAL</td>
               </tr>
                <tr align="left" style="height:35px;" >
                    <td style="width:15%;border-color:Black;padding-left:3px;"><b>Account Head </b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JAN</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>FEB</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>MAR</b>
                        415263+
                        BNM,.;/'

                    <td style="border-color:Black;width:4%;">
                        <b>APR</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>MAY</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JUN</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JUL</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>AUG</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>SEP</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>OCT</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>NOV</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>DEC</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Total Actuals</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Comm.</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                       <b>Cons.</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                          <b>Budget</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Var $</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                       <b>V %</b>
                    </td>
                    <td style="width:4%;border-color:Black;">
                       <b>Budget</b>
                    </td>
                    <td style="width:4%;border-color:Black;">
                       <b>Util'n</b>
                    </td>
                    
                </tr>
                </thead>
                </table>
                </div>
                <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center; font-size:9px; vertical-align:top;padding-top:0px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="1" class="newformat withpad" style='border-collapse:collapse; text-align:right;'>
                <tbody>
                <asp:Repeater ID="rptBudgetSummary" runat="server">
                <ItemTemplate>
                <tr  class="" style="font-weight:bold; " onmouseout="SetRow(this);" onmouseover="SetAlterNateRow(this);" >  
                    <td style="text-align :left;width:15%;padding-left:3px;" > <%#Eval("MajorCat")%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
                    <td style="width:4%;">
                       <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                         <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                   
                    <td style="width:4%;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
                    </td>
                    
                </tr>

                </ItemTemplate>
                </asp:Repeater>
                </tbody>
                </table>
             </div>  
           </div>
                    <div id="divAccountSummary" runat="server" visible="false" style="padding-top:5px;">  
                <div style="overflow-y:scroll;overflow-x:hidden; height:60px;">
                <table cellpadding="0" cellspacing="0" width='100%' class='newformat' height="60px" border='1' style='border-collapse:collapse; ' bordercolor="white">
                <thead>
                <tr style="height:25px;">
                    <td style="width:15%;border-color:Black;" >  </td>
                    <td colspan="12" style=" border-color:Black; width:51%; font-weight:bold;">Actuals (Month wise) </td>
                    <td colspan="6"  style="border-color:Black; width:25.5%; font-weight:bold;">YEAR TO DATE</td>
                    <td colspan="2" style="border-color:Black;width:8.5%; font-weight:bold;">ANNUAL</td>
               </tr>
                <tr align="left" style="height:35px;" >
                    <td style="width:15%;border-color:Black;padding-left:3px;"><b>Account Head </b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JAN</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>FEB</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>MAR</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>APR</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>MAY</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JUN</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JUL</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>AUG</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>SEP</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>OCT</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>NOV</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>DEC</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Total Actuals</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Comm.</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                       <b>Cons.</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                          <b>Budget</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Var $</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                       <b>V %</b>
                    </td>
                    <td style="width:4%;border-color:Black;">
                       <b>Budget</b>
                    </td>
                    <td style="width:4%;border-color:Black;">
                       <b>Util'n</b>
                    </td>
                    
                </tr>
                </thead>
                </table>
                </div>
                <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center; font-size:9px; vertical-align:top;padding-top:0px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="1" class="newformat withpad" style='border-collapse:collapse; text-align:right;'>
                <tbody>
                <asp:Repeater ID="rptAccountsummary" runat="server">
                <ItemTemplate>
                <tr  class="" style="font-weight:bold; " onmouseout="SetRow(this);" onmouseover="SetAlterNateRow(this);" >  
                    <td style="text-align :left;width:15%;padding-left:3px;" > <%#Eval("MajorCat")%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
                    <td style="width:4%;">
                       <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                         <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                    
                    <td style="width:4%;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
                    </td>
                    
                </tr>

                </ItemTemplate>
                </asp:Repeater>
                </tbody>
                </table>
             </div>  
           </div>
                    <div id="divAccountDetails" runat="server" visible="false" style="padding-top:5px;">  
                <div style="overflow-y:scroll;overflow-x:hidden; height:60px;">
                <table cellpadding="0" cellspacing="0" width='100%' class='newformat' height="60px" border='1' style='border-collapse:collapse; ' bordercolor="white">
                <thead>
                <tr style="height:25px;">
                    <td style="width:15%;border-color:Black;" >  </td>
                    <td colspan="12" style=" border-color:Black; width:51%; font-weight:bold;">Actuals (Month wise) </td>
                    <td colspan="6"  style="border-color:Black; width:25.5%; font-weight:bold;">YEAR TO DATE</td>
                    <td colspan="2" style="border-color:Black;width:8.5%; font-weight:bold;">ANNUAL</td>
               </tr>
                <tr align="left" style="height:35px;" >
                    <td style="width:15%;border-color:Black;padding-left:3px;"><b>Account Head </b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JAN</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>FEB</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>MAR</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>APR</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>MAY</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JUN</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>JUL</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>AUG</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>SEP</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>OCT</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>NOV</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>DEC</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Total Actuals</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Comm.</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                       <b>Cons.</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                          <b>Budget</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                        <b>Var $</b>
                    </td>
                    <td style="border-color:Black;width:4%;">
                       <b>V %</b>
                    </td>
                    <td style="width:4%;border-color:Black;">
                       <b>Budget</b>
                    </td>
                    <td style="width:4%;border-color:Black;">
                       <b>Util'n</b>
                    </td>
                    
                </tr>
                </thead>
                </table>
                </div>
                <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center; font-size:9px; vertical-align:top;padding-top:0px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="1" class="newformat withpad" style='border-collapse:collapse; text-align:right;'>
                <tbody>
                <asp:Repeater ID="rptAccountDetails" runat="server">
                <ItemTemplate>
                <tr  class="" style="font-weight:bold; " onmouseout="SetRow(this);" onmouseover="SetAlterNateRow(this);" >  
                    <td style="text-align :left;width:15%;padding-left:3px;" > <%#Eval("MajorCat")%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jan"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Feb"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Mar"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Apr"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("May"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jun"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Jul"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Aug"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Sep"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Oct"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Nov"))%> </td>
                    <td style="width:4%;"> <%#ProjectCommon.FormatCurrency2(Eval("Dec"))%> </td>
                    <td style="width:4%;">
                       <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("TotalActual"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                         <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                    
                    <td style="width:4%;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:4%;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>    
                    </td>
                    
                </tr>

                </ItemTemplate>
                </asp:Repeater>
                </tbody>
                </table>
                
                    
             </div>  
           </div>--%>
                           </div>  
                        <%--</div>--%>
       
    </div>
    <script type="text/javascript">
        var company = $("#ctl00_ContentMainMaster_ddlCompany").val();
        var vessel = $("#ctl00_ContentMainMaster_ddlVessel").val();
        var year = $("#ctl00_ContentMainMaster_ddlyear").val();
        //alert(company);
        //alert(vessel);
        //alert(year);
        //function SetDiv(id, self) {
        //    $(".menu li a").removeClass('active');
        //    $(self).addClass('active');

        //    $("#ctl00_ContentMainMaster_dv1").css('display', 'none');
        //    $("#ctl00_ContentMainMaster_dv2").css('display', 'none');

        //    $("#" + id).css('display', '');
        //}
        function ShowMidCat(ctl) {
            var LoadIn = $(ctl).parentsUntil('.section').parent().first().children(".expand");
            var majcatid = $(ctl).attr('majcatid');
          
            if ($(ctl).hasClass("arrow-down")) {
                $(LoadIn).html("");
                $(ctl).toggleClass("arrow-down");
                return;
            }
            //alert(LoadIn);
            //alert(majcatid);

            $.ajax({
                url: "./MonthlyVarianceReport.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid },
                dataType: "html",
                success: function (result) {
                    $(ctl).toggleClass("arrow-down");
                    $(LoadIn).html(result);
                },
                error: function (result) {

                },
                complete: function (result) {

                }
            });
        }

        function ShowMinCat(ctl) {
            var LoadIn = $(ctl).parentsUntil('.section').parent().first().children(".expand");
            var majcatid = $(ctl).attr('majcatid');
            var midcatid = $(ctl).attr('midcatid');

            if ($(ctl).hasClass("arrow-down")) {
                $(LoadIn).html("");
                $(ctl).toggleClass("arrow-down");
                return;
            }

            $.ajax({
                url: "./MonthlyVarianceReport.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid, midcatid: midcatid },
                dataType: "html",
                success: function (result) {
                    $(ctl).toggleClass("arrow-down");
                    $(LoadIn).html(result);
                },
                error: function (result) {

                },
                complete: function (result) {

                }
            });
        }

        function ShowAccounts(ctl) {
            var LoadIn = $(ctl).parentsUntil('.section').parent().first().children(".expand");
            var majcatid = $(ctl).attr('majcatid');
            var midcatid = $(ctl).attr('midcatid');
            var mincatid = $(ctl).attr('mincatid');

            if ($(ctl).hasClass("arrow-down")) {
                $(LoadIn).html("");
                $(ctl).toggleClass("arrow-down");
                return;
            }

            $.ajax({
                url: "./MonthlyVarianceReport.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid, midcatid: midcatid, mincatid: mincatid },
                dataType: "html",
                success: function (result) {
                    $(ctl).toggleClass("arrow-down");
                    $(LoadIn).html(result);
                },
                error: function (result) {

                },
                complete: function (result) {

                }
            });
        }

        
    </script>
    </div>
</asp:Content>


