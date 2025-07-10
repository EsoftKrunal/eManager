<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetTracking1.aspx.cs" Inherits="BudgetTracking1" MasterPageFile="~/MasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
    <title>
    </title>
    <%--<link href="CSS/style.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/jquery-1.4.2.min.js" type="text/javascript"></script>
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

.msg
{
    background-color:#56bf5b !important
}

.headerdatacell
{
    width:110px;
    text-align:center;
    color:white;
    vertical-align:middle;
}

.headerdatacell1
{
    width:100px;
    text-align:center;
    color:white;
    vertical-align:middle;
}

.contentdatacell
{
    width:110px;
    text-align:right;
    color:white;
    vertical-align:middle;
}

.contentdatacell1
{
    width:100px;
    text-align:right;
    color:white;
    vertical-align:middle;
}
    </style>
   <%-- </head>
<body >
    <form id="form1" runat="server"> --%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
       <%-- <div style="position:fixed;top:0px;left:0px;width:100%;height:100%;background:rgba(0,0,0,0.4);text-align:center;z-index:500;">
            <img style="margin:0 auto;margin-top:45%;z-index:501;" src="Images/loading.gif" />
        </div>  --%> 
        <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>
    <div >
        <div class="Text headerband"> OPEX Tracking </div>
        <div style="background-color:#f6fafd;color:#222;">
            <table cellpadding="5" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;margin-left:10px; font-size:14px;" >
                <colgroup>
                <col width="90px"/>
                <col width="280px"/>
                <col width="70px"/>
                <col width="150px"/>
                <col width="110px"/>
                <col width="100px"/>
                <col width="120px"/>
                <col width="65px"/>
                <col />
                </colgroup>
                <tr >
                    <td>Company :</td>
                    <td><asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true"  Width="250px" Height="23px" ></asp:DropDownList></td>
                    <td>Vessel :</td>
                    <td><asp:DropDownList ID="ddlShip" runat="server"  OnSelectedIndexChanged="ddlShip_OnSelectedIndexChanged" AutoPostBack="false"  Width="250px" Height="23px"></asp:DropDownList></td>
                    <td style="text-align:right;padding-right:5px;">Budget Year : </td>
                    <td><asp:DropDownList ID="ddlYear" runat="server"  OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged" AutoPostBack="true"  Width="100px" Height="23px"></asp:DropDownList> 
                        <asp:TextBox runat="server" ID="lblBudgetYear" Visible="false" Enabled="false" Width="50px" ></asp:TextBox></td>    <td style="text-align:right;padding-right:5px;">Month : 
                            <asp:HiddenField ID="hdnIsIndianFinYr" runat="server" Value="" />
                                                                                                                                            </td>  
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server"  AutoPostBack="false"  Width="100px" Height="23px">
                            <asp:ListItem Text="< ALL >" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                    <td> &nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_OnClick" CssClass="btn" Width="60px" />
                      <%--  <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CssClass="btn" Width="60px" /> --%>                       
                    </td>                               
                </tr>
                    
            </table> 
        </div>
        <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:50px; font-weight:bold;" >
            <tr class= "headerstylegrid">
                <td > <b> </b>  </td>
                <td > <b> </b>  </td>
               <%-- <td colspan="5" style="text-align:center"> CURRENT MONTH </td>--%>
                <td colspan="6" style="text-align:center" > YEAR TO DATE </td>
                <td colspan="2" style="text-align:center" > ANNUAL </td>
                
            </tr>
            <tr class= "headerstylegrid">
                <td  class='actiondatacell'></td>
                <td  class="leftalign"> <b>Account Name </b>  </td>

               <%-- <td  class="headerdatacell"> Actual </td>
                <td  class="headerdatacell"> Committed </td>
                <td  class="headerdatacell"> Consumed </td>
                <td  class="headerdatacell"> Budget </td>
                <td  class="headerdatacell1"> Var(US$) </td>--%>

                <td  class="headerdatacell"> Actual </td>
                <td  class="headerdatacell"> Accrual </td>
                <td  class="headerdatacell"> Total </td>
                <td  class="headerdatacell"> Budget </td>
                <td  class="headerdatacell"> Variance (US$) </td>
                <td  class="headerdatacell1"> Variance (%) </td>

                <td  class="headerdatacell"> Annnual Budget
                    <br /> ( <asp:Label ID="lblDays" runat="server" ></asp:Label> days )
                </td>
                <td  class="headerdatacell1"> <b>Consumed (%)</b> </td>
            </tr>
        </table>
    </div>
    <div >
    <asp:Repeater ID="rptData" runat="server">
            <ItemTemplate>
                <div class='section'>
                <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%;" class="rpt-table" >
                     
                    <tr class="group-1">
                        <td class='actiondatacell'><img style="cursor:pointer" class="imgMajor" majcatid='<%#Eval("MAJCATID")%>' onclick="ShowMidCat(this)" /></td>
                        <td class='leftalign'><%# Eval("MAJORCAT") %></td>
                      <%--  <%# (Common.CastAsInt32(Eval("ConsumedAmount"))>Common.CastAsInt32(Eval("BudgetAmount")))?"red":"" %>
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2 (Eval("AcctCMAct"))%></td>                                
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%></td>                                
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%> </td>
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%> </td>
                        <td class="contentdatacell1"><%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%> </td>--%>
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%></td>                                
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%></td>                                
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%> </td>
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%> </td>
                        <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%> </td>
                        <td class='contentdatacell1 <%# SetColorForVPer(Eval("Col1"))%>'>
                            <span class=''> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                        </td>
                        <td class="contentdatacell">
                            <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                        </td>
                        <td class='contentdatacell1 <%# SetColorForYearPer(Eval("Col2"))%>'>
                            <span class=''> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>   
                        </td>
                    </tr>                       
                </table> 
                <div class='expand' ></div>
                </div>    
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="b-PageFooter" style="display:none;">
            <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:34px; background-color:#5f5f5f;color:white;" class="rpt-table">
                <colgroup>
               <col />
                <col width="110px" />
                <col width="110px" />
                <col width="110px" />
                <col width="110px" />
                <col width="95px" />
                <col width="300px" />
                <col width="60px" />
                    </colgroup>
            <tr>                
                <td style="text-align:left;" ></td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalAmount" runat="server"></asp:Label> </td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalConsumedAmount" runat="server"></asp:Label> </td>
                <td class="">  </td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalVarianceAmount" runat="server"></asp:Label> </td>
                <td class="" style="color:white;"> <asp:Label ID="lblTotalVariancePercentage" runat="server"></asp:Label> </td>
                <td class=""> </td>
                <td class=""> </td>
            </tr>
        </table>
            <table cellspacing="0" cellpadding="0" border="0" style="border-collapse:collapse; width:100%; height:34px; background-color:#5f5f5f;color:white;font-weight:bold;" >
                <colgroup>
                <col width="33%"  />
                <col width="33%"  />
                <col  />
                    </colgroup>
                <tr>
                    <td style="text-align:center;">
                        Total Budgeted : <asp:Label ID="lblTotalBudgetdAmount" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:center;">
                        Total Unnudgeted : <asp:Label ID="lblTotalUnbudgetdAmount" runat="server"></asp:Label>
                    </td>                    
                    <td style="text-align:center;">
                        Ship Total : <asp:Label ID="lblShipTotal" runat="server"></asp:Label>
                    </td>                    
                </tr>
            </table>
        </div> 

    <asp:Repeater ID="rptMidCats" runat="server">
    <ItemTemplate>
            <div class='section'>
            <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%;" class="rpt-table" >
                    <tr class="group-2"> 
                    <td class='actiondatacell'>
                        <img style="cursor:pointer" class="imgMajor" majcatid='<%#Eval("MAJCATID")%>' midcatid='<%#Eval("midcatid")%>' onclick="ShowMinCat(this)" />
                    </td>
                    <td class='leftalign ' ><%#Eval("MIDCAT") %></td>  
                     <%--   <%# (Common.CastAsInt32(Eval("ConsumedAmount"))>Common.CastAsInt32(Eval("BudgetAmount")))?"red":"" %>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctCMAct"))%></td>                                
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%></td>                                
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%> </td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%> </td>
                    <td class="contentdatacell1"><%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%> </td>--%>
                                          
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%></td>
                    <td class='contentdatacell1 <%#SetColorForVPer(Eval("Col1"))%>'><%#Eval("Col1")%> %</td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%></td>
                    <td class='contentdatacell1 <%#SetColorForYearPer(Eval("Col2"))%>'><%#Eval("Col2")%> %</td>
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
                    <td class='actiondatacell'>
                        <img style="cursor:pointer" class="imgMajor" majcatid='<%#Eval("MAJCATID")%>' midcatid='<%#Eval("midcatid")%>' mincatid='<%#Eval("mincatid")%>' onclick="ShowAccounts(this)" />
                    </td>
                    <td class='leftalign ' ><%#Eval("MINORCAT") %></td>      
                     <%--   <%# (Common.CastAsInt32(Eval("ConsumedAmount"))>Common.CastAsInt32(Eval("BudgetAmount")))?"red":"" %>
                    <td class="contentdatacell">  <%#ProjectCommon.FormatCurrency2(Eval("AcctCMAct"))%></td>                                
                    <td class="contentdatacell">  <%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%></td>                                
                    <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%> </td>
                    <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%> </td>
                    <td class="contentdatacell1"> <%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%> </td>--%>
                                      
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%></td>
                    <td class='contentdatacell1 <%# SetColorForVPer(Eval("Col1"))%>'><%#Eval("Col1")%> %</td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%></td>
                    <td class='contentdatacell1 <%# SetColorForYearPer(Eval("Col2"))%>'><%#Eval("Col2")%> %</td>
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
                    <td class='actiondatacell'><img src="../HRD/Images/magnifier.png" class="imgViewTaskList" majcatid='<%#Eval("MAJCATID") %>'   midcatid='<%#Eval("MIDCATID") %>' mincatid='<%#Eval("mincatid") %>' accountid='<%#Eval("ACCOUNTID") %>' onclick="OpenTaskList(this)" style="cursor:pointer;"  /></td>
                    <td class='leftalign '><%#Eval("ACCOUNTNumber") %> - <%#Eval("ACCOUNTNAME") %></td>    
                       <%-- <%# (Common.CastAsInt32(Eval("ConsumedAmount"))>Common.CastAsInt32(Eval("BudgetAmount")))?"red":"" %>
                    <td class="contentdatacell">  <%#ProjectCommon.FormatCurrency2(Eval("AcctCMAct"))%></td>                                
                    <td class="contentdatacell">  <%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%></td>                                
                    <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%> </td>
                    <td class="contentdatacell"> <%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%> </td>
                    <td class="contentdatacell1"> <%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%> </td>--%>
                        
                                        
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%></td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%></td>
                    <td class='contentdatacell1 <%# SetColorForVPer(Eval("Col1"))%>'><%#Eval("Col1")%> %</td>
                    <td class="contentdatacell"><%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%></td>
                    <td class='contentdatacell1 <%# SetColorForYearPer(Eval("Col2"))%>'><%#Eval("Col2")%> %</td>
                </tr>
            </table>
            <div class='expand' ></div>
            </div>
    </ItemTemplate>
    </asp:Repeater>

    
    <%-----------------------------------------------------------------------------------------------------------------------%>
         <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvClosure" visible="false" >
            <center>
            <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:500px;padding :0px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:120px;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size:15px;font-weight:bold;padding:4px;" class="header">
                    Closure
                    <asp:ImageButton ID="btnCloseClosurePopup" runat="server" OnClick="btnCloseClosurePopup_OnClick"  ImageUrl="~/Images/Close.gif" style="float:right;"/>
                </div>
                <table cellpadding="3" cellspacing="3" border="0" width="100%">
                    <tr>
                        <td> <b> Remarks</b></td>
                    </tr>
                    <tr>
                        <td> <asp:TextBox ID="txtClosureRemarks" runat="server" TextMode="MultiLine" Width="99%" Height="100px" ></asp:TextBox> </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Button ID="btnSaveClosure" runat="server" Text="Closure" OnClick="btnSaveClosure_OnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lblMsgClosure" runat="server" style="color:red;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
            </div>
            </center>
        </div>
    <%--Open Task List---------------------------------------------------------------------------------------------------------------------%>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>            
            <asp:Button ID="btnTempOpenTaskListPopup" runat="server" OnClick="btnTempOpenTaskListPopup_OnClick" style="display:none;" />
            <asp:HiddenField ID="hfSelAccountID" runat="server" />
            <asp:HiddenField ID="hfSelMajCatID" runat="server" />
            <asp:HiddenField ID="hfSelMidCatID" runat="server" />
            <asp:HiddenField ID="hfSelMinCatID" runat="server" />
                <style type="text/css">
                    .menu{
                        margin:0px;
                        padding:0px;
                        display:block;
                    }
                    .menu li
                    {
                        display:inline-block;
                        float:left;
                        margin-left:5px;
                    }
                    .menu li a{
                        display:block;
                        width:150px;
                        height:35px;
                        line-height:35px;
                        background-color:#c2c2c2;
                        text-decoration:none;
                        color:#333;
                    }
                     .menu li a:hover,.menu li a:active,.menu li a:focus,.menu li a.active{
                        background-color:#f63f2d;
                        color:white;
                    }
                     .clear{
                         clear:both;
                     }
                     .data-label
                     {
                         background-color:#c2c2c2;
                         color:#333;
                     }
                </style>

            <div style="position:fixed;top:50px;left:150px; height :100%; width:90%;z-index:100;" runat="server" id="divTaskList" visible="false" >
            <center>
            <div style="position:fixed;top:50px;left:150px; height :100%; width:90%; background-color :#2e2828;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:95%; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:20px;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size:16px;font-weight:bold; line-height:30px;" class="Text headerband">
                    
                    <asp:ImageButton runat="server" ID="btnClose" OnClick="btnClose_Click" ImageUrl="~/Modules/HRD/Images/closewindow.png" style="float:right;width:24px;" />
                    <span style="font-size:16px;">Budget Summary</span>
                </div>
                <table cellpadding="10" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; background-color:#8fafdb">
                    <tr>
                        <td style="font-size:16px;text-align:right;" ><b>Company :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblCompany" runat="server"></asp:Label></td>
                        <td style="font-size:16px;text-align:right;" > <b>Vessel :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblVessel" runat="server"></asp:Label></td>
                        <td style="font-size:16px;text-align:right;" ><b>Year :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblYear" runat="server"></asp:Label></td>
                        <td style="font-size:16px;text-align:right;" ><b>Account :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblAccountNoNameTaskList" runat="server"></asp:Label></td>
                    </tr>
                </table>
              
                <div>
                    <table width="100%">
                    <tr>
                        <td>
                        <table width="100%" border="0" cellpadding="6" class="bordered" cellspacing="0" style="border-collapse:collapse;">
                             <tr style="background-color:#c2c2c2;font-weight:bold;">
                                <td>Year To Date</td>
                                <td style="width:10px">:</td>
                                <td style="text-align:right">Amount (US$)</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Actual Amount </td>
                                <td style="width:10px">:</td>
                                <td style="text-align:right"><asp:Label ID="lblYTDActule" runat="server"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Accrual Amount </td>
                                <td style="width:10px">:</td>
                                <td style="text-align:right"><asp:Label ID="lblYTDCommitted" runat="server"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr>
                            <td>Total Amount </td>
                            <td style="width:10px">:</td>
                            <td style="text-align:right"><asp:Label ID="lblYTDConsumed" runat="server"></asp:Label> </td>
                            <td>
                                <div runat="server" id="dvbudcons" style="height:15px; background-color:#ea9226;left:0px;top:0px; display:inline-table;"></div>
                            </td>
                        </tr>
                             <tr>
                            <td >Budget Amount </td>
                            <td style="width:10px">:</td>
                            <td style="text-align:right"><asp:Label ID="lblYTDBudget" runat="server"></asp:Label></td>
                            <td><div runat="server" id="dvbud" style="height:15px; background-color:#4988cc; left:0px;top:0px;"></div></td>
                        </tr>
                        
                          <tr>
                            <td style="width:160px;">Variance Amount </td>
                            <td style="width:10px">:</td>
                            <td style="width:130px; text-align:right">
                                <asp:Label ID="lblYTDVariance" runat="server"></asp:Label>
                            </td>
                            <td>                                    
                            </td>
                        </tr>
                            <tr>
                            <td style="width:160px;">Variance (%) </td>
                            <td style="width:10px">:</td>
                            <td style="width:130px; text-align:right">
                                <asp:Label ID="lblYTDVariancePer" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>                
                    </table>
                            </td>
                            <td style="border:solid 0px red;vertical-align:top; width:800px;" >
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border:solid 0px green;float:left;">
                                    <colgroup>
                                        <col width="50%" />
                                        <col />
                                        <tr>
                                            <td style="padding-right:5px;padding-bottom:5px;">
                                                <div style="padding:6px; background-color:#ccc7c7">
                                                    Annual Budget (US$)
                                                    <br />
                                                    <div style="padding:5px;">
                                                        <asp:Label ID="lblYTDAnnualBudget" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="padding-right:5px;padding-bottom:5px;">
                                                <div style="padding:6px; background-color:#ccc7c7;">
                                                    Annual Budget Utilization
                                                    <br />
                                                    <div style="padding:5px;">
                                                        <asp:Label ID="lblYTDAnnualUtilization" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right:5px;padding-bottom:5px;">
                                                <asp:RadioButtonList runat="server" ID="rblBudgetValue" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true"  Font-Name="Verdana" Font-Size="12px" CssClass="" OnSelectedIndexChanged="rblBudgetValue_SelectedIndexChanged" > 
            
            <asp:ListItem Text="Accrual" Selected="True"  Value="1"></asp:ListItem> 
            <asp:ListItem Text="Actual" Value="2"></asp:ListItem> 
        </asp:RadioButtonList>
                                                <%-- <div style="padding:6px; background-color:#ccc7c7; margin-top:5px;">
                                                No of Orders Not Linked With Any Allocation
                                                <br />
                                                <div style="padding:5px;">
                                                <asp:LinkButton ID="lblOrdersCount" runat="server" Font-Size="16px" Font-Bold="true" OnClick="lblOrdersCount_Click"></asp:LinkButton>
                                                </div>
                                            </div>--%></td>
                                            <td style="padding-right:5px;padding-bottom:5px;"><%-- <div style="padding:6px; background-color:#ccc7c7; margin-top:5px;">
                                                No of NON PO Orders Not Linked With Any Task
                                                <br />
                                                <div style="padding:5px;">
                                                <asp:LinkButton ID="lblNONPoOrders" runat="server" Font-Size="16px" Font-Bold="true" OnClick="lblOrdersCount1_Click"></asp:LinkButton>
                                                </div>
                                            </div>--%></td>
                                        </tr>
                                    </colgroup>
                                </table>                             
                                <div style="padding-top:5px;text-align:right;margin-right:20px;">
                                    <asp:Button ID="btnOpenAddTaskPopup" runat="server" OnClick="btnOpenAddTaskPopup_OnClick"  Text=" + Add New Budget Allocation" CssClass="btn" Visible="false"/>
                                    <asp:Button ID="btnCrewlist"  runat="server" Text="Crew Wages Details" CssClass="btn" OnClick="btnCrewlist_Click" />
                                </div>

                            </td>
                        </tr>
                    </table>
                </div>
               
            

                <div id="divCommited" runat="server">
                <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;">

                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                               <colgroup>
                                   <col width="110px" />
                                   <col width="85px" />
                                   <col width="160px" />
                                   <col width="150px" />
                                   <col width="100px"/>
                                   <col width="100px" />
                                   <col width="80px" />
                                   <col width="85px" />
                                   <col width="50px" />
                                   <tr class="headerstylegrid">
                                       <td>PO Number</td>
                                       <td>PO Date</td>
                                       <td>Requisition Title</td>
                                       <td>Supplier</td>
                                       <td>Status</td>
                                       <td>PO Amount (US$)</td>
                                       <td>Inv. Ref. No</td>
                                       <td>Invoice Date</td>
                                       <td>Invoice Status </td>
                                   </tr>
                               </colgroup>
                    </table>
                </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:300px;border:solid 1px #d5d2d2;">
                    <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" class="rpt-table">
                               <colgroup>
                               <col width="110px" />
                               <col width="85px" />
                               <col width="160px" />
                               <col width="150px" />
                               <col width="100px"/>
                               <col width="100px" /> 
                               <col width="80px" />
                               <col width="85px" />
                               <col width="50px" />
                                   
                                  
                     
                    <asp:Repeater ID="rptPoDetails" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:left"><%#Eval("BIDPONUM")%></td>
                                <td><%#Eval("PODATE")%></td>
                                <td style="text-align:left"><%#Eval("RequisitionTitle")%></td>
                                <td style="text-align:left"><%#Eval("SupplierName")%></td>
                                <td><%#Eval("BIDSTATUSNAME")%></td>
                                <td><%# ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Eval("Amount")) %></td>
                                <td style="text-align:left;"><a href='../Purchase/Invoice/ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>' target="_blank"><%#Eval("RefNo")%></a></td>
                                <td><%#Eval("BIDINVOICEDATE")%></td>
                                <td><%#Eval("Status")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                   </colgroup>
                            </table>
                    <%--<asp:Repeater ID="rptTrackingTaskList" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td style="text-align:center">
                                               <asp:ImageButton runat="server" ID="btnedittask" CommandArgument='<%#Eval("TaskID") %>' OnClick="btnEdit_Task_Click" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" ToolTip="Modify Task" />
                                           </td>
                                           <td style="text-align:center">
                                               
                                               <asp:ImageButton runat="server" ID="ImageButton2" Visible='<%#CheckTaskDeleteAuthorityForDeleteBtn( Common.CastAsInt32(Eval("TaskID"))) %>' CommandArgument='<%#Eval("TaskID") %>' OnClick="btnDelete_Task_Click" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete Task" OnClientClick="return confirm('Are you sure to delete this task ?');" />
                                           </td>
                                           <td>
                                               <span class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'><%# ((Eval("budgeted").ToString()=="True")?"B":"U") %></span>
                                           </td>
                                           <td style="text-align:left;"> 
                                               <%#Eval("TaskDescription") %> 
                                           </td>
                                           <td style="text-align:right;">
                                                <%# ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(Eval("Amount"))) %> 
                                               <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID") %>'  />
                                           </td>
                                           <td style="text-align:right;">
                                             <a target="_blank" href="BudgetTrackingPOList.aspx?company=<%#Eval("company")%>&vessel=<%#Eval("vesselcode")%>&year=<%#Eval("budgetyear")%>&AccountId=<%#Eval("AccountId")%>&majcatid=<%#hfSelMajCatID.Value%>&midcatid=<%#hfSelMidCatID.Value%>&mincatid=<%#hfSelMinCatID.Value%>&TaskId=<%#Eval("TaskId")%>"> 
                                                 <%#  ProjectCommon.FormatCurrencyWithoutSign(Eval("TotConsume")) %> 
                                                                                              </a>
                                           </td>
                                           <td style="text-align:left;"> <%#Eval("ModifiedBy")%> / <%# Common.ToDateString(Eval("ModifiedOn")) %> </td>
                                       </tr>   
                                            
                                   </ItemTemplate>
                               </asp:Repeater>--%>
                  
                </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:300px;border:solid 1px #d5d2d2;">
                    <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" class="rpt-table">
                               <colgroup>
                               <col width="110px" />
                               <col width="85px" />
                               <col width="160px" />
                               <col width="150px" />
                               <col width="100px"/>
                               <col width="100px" /> 
                               <col width="80px" />
                               <col width="85px" />
                               <col width="50px" />
                            <tr class="headerstylegrid">
                                <td style="text-align:left"></td>
                                <td></td>
                                <td style="text-align:left"></td>
                                <td style="text-align:left"></td>
                                <td>Total : </td>
                                <td><asp:Label ID="lbltotalPoCommitedAmt" runat="server"></asp:Label></td>
                                <td style="text-align:left;"></td>
                                <td></td>
                                <td></td>
                            </tr>
                       
                     </colgroup>
                            </table>
                <%--<div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;">

                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                       
                        <tr >
                            <td></td>
                            <td></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Task Budget : </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lbltaskbudgettotal"></asp:Label></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Consumed <i style="font-size:11px;color:#4cff00">(Budgeted)</i> : </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lblconsumedtotal_b"></asp:Label></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Consumed <i style="font-size:11px;color:#e55454">(Unbudgeted)</i>: </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lblconsumedtotal_u"></asp:Label></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Consumed : </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lblconsumedtotal"></asp:Label></td>
                            <td style="text-align:left;color:white;vertical-align:central;"></td>           
                        </tr>
                    </table>
                </div>--%>
               
                </div>
                </div>
                <div id="divActual" runat="server">
                <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;">

                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                               <colgroup>
                                   <col width="110px" />
                                   <col width="85px" />
                                   <col width="160px" />
                                   <col width="150px" />
                                   <col width="100px"/>
                                   <col width="100px" />
                                   <col width="80px" />
                                   <col width="85px" />
                                   <col width="50px" />
                                   <tr class="headerstylegrid">
                                       <td>PO Number</td>
                                       <td>PO Date</td>
                                       <td>Requisition Title</td>
                                       <td>Supplier</td>
                                       <td>Status</td>
                                       <td>PO Amount (US$)</td>
                                       <td>Inv. Ref. No</td>
                                       <td>Invoice Date</td>
                                       <td>Invoice Status </td>
                                   </tr>
                               </colgroup>
                    </table>
                </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:300px;border:solid 1px #d5d2d2;">
                    <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" class="rpt-table">
                               <colgroup>
                               <col width="110px" />
                               <col width="85px" />
                               <col width="160px" />
                               <col width="150px" />
                               <col width="100px"/>
                               <col width="100px" /> 
                               <col width="80px" />
                               <col width="85px" />
                               <col width="50px" />
                                   
                                  
                     
                    <asp:Repeater ID="rptActualPoDetails" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:left"><%#Eval("BIDPONUM")%></td>
                                <td><%#Eval("PODATE")%></td>
                                <td style="text-align:left"><%#Eval("RequisitionTitle")%></td>
                                <td style="text-align:left"><%#Eval("SupplierName")%></td>
                                <td><%#Eval("BIDSTATUSNAME")%></td>
                                <td><%# ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Eval("Amount")) %></td>
                                <td style="text-align:left;"><a href='../Purchase/Invoice/ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>' target="_blank"><%#Eval("RefNo")%></a></td>
                                <td><%#Eval("BIDINVOICEDATE")%></td>
                                <td><%#Eval("Status")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </colgroup>
                            </table>
                    
                    <%--<asp:Repeater ID="rptTrackingTaskList" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td style="text-align:center">
                                               <asp:ImageButton runat="server" ID="btnedittask" CommandArgument='<%#Eval("TaskID") %>' OnClick="btnEdit_Task_Click" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" ToolTip="Modify Task" />
                                           </td>
                                           <td style="text-align:center">
                                               
                                               <asp:ImageButton runat="server" ID="ImageButton2" Visible='<%#CheckTaskDeleteAuthorityForDeleteBtn( Common.CastAsInt32(Eval("TaskID"))) %>' CommandArgument='<%#Eval("TaskID") %>' OnClick="btnDelete_Task_Click" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete Task" OnClientClick="return confirm('Are you sure to delete this task ?');" />
                                           </td>
                                           <td>
                                               <span class='<%# ((Eval("budgeted").ToString()=="True")?"Budgeted":"UnBudgeted") %>'><%# ((Eval("budgeted").ToString()=="True")?"B":"U") %></span>
                                           </td>
                                           <td style="text-align:left;"> 
                                               <%#Eval("TaskDescription") %> 
                                           </td>
                                           <td style="text-align:right;">
                                                <%# ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(Eval("Amount"))) %> 
                                               <asp:HiddenField ID="hfTaskID" runat="server" Value='<%#Eval("TaskID") %>'  />
                                           </td>
                                           <td style="text-align:right;">
                                             <a target="_blank" href="BudgetTrackingPOList.aspx?company=<%#Eval("company")%>&vessel=<%#Eval("vesselcode")%>&year=<%#Eval("budgetyear")%>&AccountId=<%#Eval("AccountId")%>&majcatid=<%#hfSelMajCatID.Value%>&midcatid=<%#hfSelMidCatID.Value%>&mincatid=<%#hfSelMinCatID.Value%>&TaskId=<%#Eval("TaskId")%>"> 
                                                 <%#  ProjectCommon.FormatCurrencyWithoutSign(Eval("TotConsume")) %> 
                                                                                              </a>
                                           </td>
                                           <td style="text-align:left;"> <%#Eval("ModifiedBy")%> / <%# Common.ToDateString(Eval("ModifiedOn")) %> </td>
                                       </tr>   
                                            
                                   </ItemTemplate>
                               </asp:Repeater>--%>
                  
                </div>
                    <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:100%;" class="rpt-table">
                               <colgroup>
                               <col width="110px" />
                               <col width="85px" />
                               <col width="160px" />
                               <col width="150px" />
                               <col width="100px"/>
                               <col width="100px" /> 
                               <col width="80px" />
                               <col width="85px" />
                               <col width="50px" />
                            <tr class="headerstylegrid">
                                <td style="text-align:left"></td>
                                <td></td>
                                <td style="text-align:left"></td>
                                <td style="text-align:left"></td>
                                <td>Total : </td>
                                <td><asp:Label ID="lblTotalActualPoDetailsAmt" runat="server"></asp:Label></td>
                                <td style="text-align:left;"></td>
                                <td></td>
                                <td></td>
                            </tr>
                       
                     </colgroup>
                            </table>
                </div>
                    
                <%--<div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;">

                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:100%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                       
                        <tr >
                            <td></td>
                            <td></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Task Budget : </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lbltaskbudgettotal"></asp:Label></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Consumed <i style="font-size:11px;color:#4cff00">(Budgeted)</i> : </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lblconsumedtotal_b"></asp:Label></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Consumed <i style="font-size:11px;color:#e55454">(Unbudgeted)</i>: </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lblconsumedtotal_u"></asp:Label></td>
                            <td style="text-align:right;color:white;vertical-align:central;">Total Consumed : </td>
                            <td style="text-align:left;color:white;vertical-align:central;"><asp:Label runat="server" ID="lblconsumedtotal"></asp:Label></td>
                            <td style="text-align:left;color:white;vertical-align:central;"></td>           
                        </tr>
                    </table>
                </div>--%>
                 <div style="font-size:15px;font-weight:bold;padding:4px;background-color:#dddddd;color:#141212;text-align:left;" class="PageHeader">
                    &nbsp;<asp:Label ID="lblMsgTaskListPopup" runat="server" CssClass="error"></asp:Label>
                    
                </div>
                </div>
                </div>
            </center>
            </div>
            </center>
        </div>

                <div style="position:fixed;top:50px;left:150px; height :100%; width:90%;z-index:100;" runat="server" id="divCrewList" visible="false" >
            <center>
            <div style="position:fixed;top:50px;left:150px; height :100%; width:90%; background-color :#2e2828;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:95%; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:20px;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div style="font-size:16px;font-weight:bold; line-height:30px;" class="Text headerband">
                    
                    <asp:ImageButton runat="server" ID="imgbtnCrewlistClose"  ImageUrl="~/Modules/HRD/Images/closewindow.png" style="float:right;width:24px;" OnClick="imgbtnCrewlistClose_Click" />
                    <span style="font-size:16px;">Crew List</span>
                </div>
                <table cellpadding="10" cellspacing="0" border="0" width="100%" style="border-collapse:collapse; background-color:#8fafdb">
                    <tr>
                        <td style="font-size:16px;text-align:right;" ><b>Company :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblCompanyforCrew" runat="server"></asp:Label></td>
                        <td style="font-size:16px;text-align:right;" > <b>Vessel :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblVesselforCrew" runat="server"></asp:Label></td>
                        <td style="font-size:16px;text-align:right;" ><b>Year :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblYearforCrew" runat="server"></asp:Label></td>
                        <td style="font-size:16px;text-align:right;" ><b>Account :</b></td>
                        <td style="font-size:16px;text-align:left;" ><asp:Label ID="lblAccountforCrew" runat="server"></asp:Label></td>
                    </tr>
                </table>              
              <div>
                
                <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px fff;padding-left:10px;">

                    <table cellspacing="0" cellpadding="0" style="border-collapse:collapse; width:75%; height:30px; background-color:#5f5f5f;color:white;font-weight:bold;" class="rpt-table">
                        <colgroup>
                               <col width="110px" />
                               <col width="200px" />
                               <col width="100px" />
                               <col width="100px" />
                               
                        <tr class= "headerstylegrid">
                            <td>Crew #</td>
                            <td>Name</td>
                            <td>Rank</td>
                            <td>Amount (US$)</td>
                               
                        </tr>
                            </colgroup>
                    </table>
                </div>
                <div style="overflow-x:hidden;overflow-y:scroll;height:400px;border:solid 1px #d5d2d2;padding-left:10px;">
                    <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:75%;" class="rpt-table">
                               <colgroup>
                               <col width="110px" />
                               <col width="200px" />
                               <col width="100px" />
                               <col width="100px" />
                    <asp:Repeater ID="rptCrewList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:left"><%#Eval("CrewNumber")%></td>
                                <td style="text-align:left"><%#Eval("CrewName")%></td>
                                <td style="text-align:left"><%#Eval("RankName")%></td>
                                <td style="text-align:left"><%# ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Eval("Amount")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                     </colgroup>
                                                      
                            </table>
                    
                </div>
                  <div style="overflow-x:hidden;overflow-y:scroll;height:30px;border:solid 1px #d5d2d2;padding-left:10px;">
                    <table cellspacing="0" cellpadding="" style="border-collapse:collapse; width:75%;" class="rpt-table">
                               <colgroup>
                               <col width="110px" />
                               <col width="200px" />
                               <col width="100px" />
                               <col width="100px" />
                            <tr class= "headerstylegrid">
                                <td style="text-align:left"></td>
                                <td style="text-align:left"></td>
                                <td style="text-align:left">Total : </td>
                                <td style="text-align:left"><asp:Label ID="lblTotalCrewWages" runat="server"></asp:Label> </td>
                            </tr>
                   </colgroup>
                                                      
                            </table>
                    
                </div>
                     
            </div>
                
                
                </div>
                
            </center>
            </div>
            </center>
        </div>


                <%-- TrackingTask-----------------------------------------%>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddTrackingTask" visible="false" >
        <center>
        <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:750px; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:110px;opacity:1;filter:alpha(opacity=100)">
        <center>
           
             <div style="font-size:16px;font-weight:bold;padding:8px; text-align:left;line-height:30px;" class="text headerband">
                        <asp:ImageButton runat="server" ID="btnClsose1" OnClick="btnCloseAddTrackingTaskPopup_OnClick" ImageUrl="~/Modules/HRD/Images/closewindow.png" style="float:right;width:24px;" />
                    <span style="font-size:18px;">  Add New Budget Allocation</span>
                    </div>               
            <div >
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; text-align:left; border-collapse:collapse;">
                            <col width="120px" />
                            <col />
                            <tr>
                                <td>
                                    <br />
                                    Allocation Type :
                                </td>
                            </tr>                                          
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlTaskType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskType_OnSelectedIndexChanged" Height="23px">
                                        <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Budgeted"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Unbudgeted"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr> 
                            <tr>
                                <td >
                                    Allocation Description :                                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTtDescription" runat="server" TextMode="MultiLine" Width="99%" Rows="2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Budget Amount :                                       
                                </td>                                   
                            </tr>  
                            <tr>
                                <td >
                                    <asp:TextBox ID="txtTtAmount" runat="server"></asp:TextBox>                                   
                                </td>
                            </tr>                             
                            <%--<tr>
                                <td>
                                    Expenses scheduled for months :                                      
                                </td>                                   
                            </tr>   
                            <tr>
                                <td>
                                    <table cellpadding="4" cellspacing="0" border="0" class="rpt-table table-centered" style="text-align:center;width:100%;">
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <col width="50px" />
                                        <tr class="group-3" >
                                            <td>Jan</td>
                                            <td>Feb</td>
                                            <td>Mar</td>
                                            <td>Apr</td>
                                            <td>May</td>
                                            <td>Jun</td>
                                            <td>Jul</td>
                                            <td>Aug</td>
                                            <td>Sep</td>
                                            <td>Oct</td>
                                            <td>Nov</td>
                                            <td>Dec</td>
                                        </tr>
                                        <tr>
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtJan" runat="server" />
                                </td>
                               
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtFeb" runat="server" />
                                </td>
                               
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtMar" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtApr" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtMay" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtJun" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtJul" runat="server" />
                                </td>
                               
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtAug" runat="server" />
                                </td>
                                    
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtSep" runat="server" />
                                </td>
                               
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtOct" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtNov" runat="server" />
                                </td>
                              
                                   
                                <td>
                                    <asp:CheckBox ID="chkTtDec" runat="server" />
                                </td>
                            </tr>
                                    </table>
                                </td>
                            </tr>--%>
                                                                    
                        </table>
                        
                <div style="text-align:center;padding:5px;">
                    <asp:Button ID="btnSaveTrackingTask" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveTrackingTask_OnClick" />
                    
                </div>
                <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                    &nbsp; <asp:Label ID="lblMsgTrackingTask" runat="server" CssClass="error"></asp:Label>
                </div>
                </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnClsose1" />
                    </Triggers>
                </asp:UpdatePanel>           
            </div>
        </center>
        </div>
        </center>
    </div>


            </ContentTemplate>
        </asp:UpdatePanel>

    <script type="text/javascript">
        var company = $("#ctl00_ContentMainMaster_ddlCompany").val();
        var vessel = $("#ctl00_ContentMainMaster_ddlShip").val();
        var year = $("#ctl00_ContentMainMaster_ddlYear").val();
        var month = $("#ctl00_ContentMainMaster_ddlMonth").val();
        var IsIndianFinYear = $("#ctl00_ContentMainMaster_hdnIsIndianFinYr").val(); 
        //alert(company);
        //alert(vessel);
        //alert(year);
        function SetDiv(id,self)
        {
            $(".menu li a").removeClass('active');
            $(self).addClass('active');

            $("#ctl00_ContentMainMaster_dv1").css('display','none');
            $("#ctl00_ContentMainMaster_dv2").css('display','none');

            $("#" + id).css('display', '');
        }
        function ShowMidCat(ctl)
        {
            var LoadIn = $(ctl).parentsUntil('.section').parent().first().children(".expand");
            var majcatid = $(ctl).attr('majcatid');
            //alert(LoadIn);
            //alert(majcatid);
            if($(ctl).hasClass("arrow-down"))
            {
                $(LoadIn).html("");
                $(ctl).toggleClass("arrow-down");
                return;
            }            

            $.ajax({
                url: "./BudgetTracking1.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid, month: month, IsIndianFinYear: IsIndianFinYear },
                dataType: "html",
                success:function(result)
                {
                    $(ctl).toggleClass("arrow-down");
                    $(LoadIn).html(result);
                },
                error:function(result)
                {
                    
                },
                complete:function(result)
                {
                   
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
                url: "./BudgetTracking1.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid, midcatid: midcatid, month: month, IsIndianFinYear: IsIndianFinYear },
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
                url: "./BudgetTracking1.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid, midcatid: midcatid, mincatid: mincatid, month: month, IsIndianFinYear: IsIndianFinYear },
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

        function ShowTaskList(ctl) {
            var LoadIn = $(ctl).parentsUntil('.section').parent().first().children(".expand");
            var majcatid = $(ctl).attr('majcatid');
            var midcatid = $(ctl).attr('midcatid');
            var mincatid = $(ctl).attr('mincatid');
            var accountid = $(ctl).attr('accountid');

            if ($(ctl).hasClass("arrow-down")) {
                $(LoadIn).html("");
                $(ctl).toggleClass("arrow-down");
                return;
            }

            $.ajax({
                url: "./BudgetTracking1.aspx",
                method: "POST",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: { company: company, vessel: vessel, year: year, majcatid: majcatid, accountid: accountid, month: month, IsIndianFinYear: IsIndianFinYear },
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
        <script type="text/javascript">
            function OpenTaskList(ctrl) {
                $("#ctl00_ContentMainMaster_hfSelMajCatID").val($(ctrl).attr("majcatid"));
                $("#ctl00_ContentMainMaster_hfSelMidCatID").val($(ctrl).attr("midcatid"));
                $("#ctl00_ContentMainMaster_hfSelMinCatID").val($(ctrl).attr("mincatid"));
                $("#ctl00_ContentMainMaster_hfSelAccountID").val($(ctrl).attr("accountid"));

                $("#ctl00_ContentMainMaster_btnTempOpenTaskListPopup").click();             
            }
            $(document).ready(function () {
                $(".imgViewTaskList").click(function () {
                    
                });
            })
            function ShowPOList(taskid,month)
            {
                $("#ctl00_ContentMainMaster_hfdtaskid").val(taskid);
                $("#ctl00_ContentMainMaster_hfdmonth").val(month);
                $("#ctl00_ContentMainMaster_btnpost").click();
            }
            function LinkPoToTask(TaskID) {                
                $("#ctl00_ContentMainMaster_hfdSelTaskID_Linkpo").val(TaskID);
                $("#ctl00_ContentMainMaster_btnTempUpdateBIdID").click();
            }

            
        </script>
<%-- </form>
</body>
</html>--%>
    </asp:Content>

