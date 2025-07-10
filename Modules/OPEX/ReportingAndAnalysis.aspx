<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportingAndAnalysis.aspx.cs" Inherits="ReportingAndAnalysis"  Title="EMANAGER" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

       <meta http-equiv="x-ua-compatible" content="IE=9" />
    <%--<link href="CSS/style.css" rel="Stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
     <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <style type="text/css">
 
         .withpad tbody td
{
    padding-top: 2px;
    padding-bottom: 2px;
    text-align:right;
}
         .error_msg
{
background-color:#fbb3b3 !important
}

.msg
{
    background-color:#56bf5b !important
}
         </style>
    <script type="text/javascript"  >
        function ReloadPage()
        {
            document.getElementById('imgSearch').click();
        }
        function ShowVARReport()
        {
            window.open("POVarianceReport.aspx?Company=" + document.getElementById('ddlCompany').value + "&VesselID=" + document.getElementById('ddlVessel').value + "&ForMonth=" + document.getElementById('ddlMonth').value + "&ForYear=" + document.getElementById('ddlyear').value + "&Mode=1");
        }
    
        function ViewComments(obj) 
        {

            obj.nextSibling.nextSibling.style.display = (obj.nextSibling.nextSibling.style.display == 'block') ? 'none' : 'block';
            if (obj.nextSibling.nextSibling.style.display == 'block') 
            {
                obj.previousSibling.previousSibling.style.display = 'none';
            }
            else
                obj.previousSibling.previousSibling.style.display = 'block';
        }

        function ViewBudgetItem(CommMajID ,CommMidID,CommActual,CommConsumed,CommBudget) 
        {
          
            window.open("BudgetItemComments.aspx?CommMajID=" + CommMajID + "&CommMidID=" + CommMidID + "&year=" + document.getElementById('ddlyear').value + "&Company=" + document.getElementById('ddlCompany').value + "&Vessel=" + document.getElementById('ddlVessel').value + "&Month=" + document.getElementById('ddlMonth').value + "&CompanyName=" + document.getElementById('ddlCompany').options[document.getElementById('ddlCompany').selectedIndex].text + "&CommActual=" + CommActual + "&CommConsumed=" + CommConsumed + "&CommBudget=" + CommBudget + "");
        }
        function ViewMajorTable(obj,ctls)
        {
              var result=(obj.innerHTML=='[+]')?"block":"none";
              var resultsign = (obj.innerHTML == '[+]') ? "[-]" : "[+]";
              var parname='maj'+ctls;
              var len=parname.length; 
              var rw=obj.parentNode.nextSibling;
              while(true)
              { 
                if(rw.name==null)
                {
                    break;
                }
                //---------------
                if(rw.name.substring(0,len)!=parname)
                {
                    break;
                }
                else
                {
                    
                    if(len==rw.name.length) // accessing sibling
                    {
                        rw.style.display = result;
                        obj.innerHTML = resultsign;
                        rw.childNodes[0].innerHTML= ((<%=ddlReportLevel.SelectedIndex%>==3)?"[+]":""); 
                    }
                    else if(result=="none") // accessing child nodes
                    {
                        rw.style.display = result;
                        obj.innerHTML = resultsign;
                    }
                    rw=rw.nextSibling;
                }
              }
        }
        function ViewMidTable(obj,ctls,ctls1)
        {
              var result=(obj.innerHTML=='[+]')?"block":"none";
              var resultsign=(obj.innerHTML=='[+]')?"[-]":"[+]";
              var parname='maj'+ctls+'mid'+ctls1;
              var len=parname.length; 
              var rw=obj.parentNode.nextSibling;
              while(true)
              { 
                if(rw.name==null)
                {
                    break;
                }
                //---------------
                if(rw.name.substring(0,len)!=parname)
                {
                    break;
                }
                else
                {
                    rw.style.display = result;
                    rw=rw.nextSibling;
                    obj.innerHTML = resultsign;
                }
              }
        }
    </script>
    <script type="text/javascript" >
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
    </script>
    <script type="text/javascript">
        function ExpandLevel2() 
        {
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300" EnablePartialRendering="false" ></asp:ToolkitScriptManager>   
  
    <div>
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        
        <tr>
            <td>
                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1" >
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="../HRD/Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress>  
                <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="Text headerband" >
                       Variance Report
                       <%-- <asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" Visible="false"  ImageUrl="~/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " CausesValidation="false" />  --%>
                   </td>
                </tr>
                </table>
                <table cellpadding="2" cellspacing="1" width="100%" border="1" style="border-collapse:collapse;">
                    <colgroup>
                        <col width="20%" />
                        <col width="15%" />
                        <col width="10%" />
                        <col width="8%" />
                        <col width="12%" />
                        <col width="20%"/>
                        <col width="15%"/>
                        <tr align="center" style="font-weight:bold">
                            <td>Company</td>
                            <td>Vessel</td>
                            <td>Year</td>
                            <td>Month</td>
                            <td>Report Level</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="text-align:center;background-color:#F5FAFF;">
                            <td style="text-align:left;">
                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" Width="220px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompany" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlyear_OnSelectedIndexChanged" Width="95px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged" Width="50px">
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td valign="middle">
                                <asp:DropDownList ID="ddlReportLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReportLevel_OnSelectedIndexChanged" style="display:none;" Width="160px">
                                    <asp:ListItem Value=""> Select</asp:ListItem>
                                    <asp:ListItem Value="1"> General Summary</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2"> Budget Summary</asp:ListItem>
                                    <asp:ListItem Value="3"> Account Summary</asp:ListItem>
                                    <asp:ListItem Value="4"> Account Details</asp:ListItem>
                                    <asp:ListItem Value="5"> Detail Activity Report</asp:ListItem>
                                    <asp:ListItem Value="6"> CLS Report</asp:ListItem>
                                    <%--<asp:ListItem  Value="7"> Budget Comment Report</asp:ListItem>--%>
                                </asp:DropDownList>
                                Budget Summary <%--<asp:RequiredFieldValidator ID="rfreport" runat="server" ControlToValidate="ddlReportLevel" ErrorMessage="*" ></asp:RequiredFieldValidator>--%></td>
                            <td style="text-align:right;">
                                <asp:Button ID="imgSearch" runat="server" CssClass="btn" onclick="imgSearch_Click" style="display:none;" Text="Search" />
                                <asp:Button ID="imgPrint" runat="server" CssClass="btn" onclick="imgPrint_Click" Text="Variance Report" />
                                &nbsp;
                                <asp:Button ID="imgClear" runat="server" CausesValidation="false" CssClass="btn" onclick="imgClear_Click" Text="Clear" />
                            </td>
                            <td style="text-align:center;">
                                <asp:Label ID="lblTargetUtilisation" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </colgroup>
                </table>
                <div style="overflow-y:scroll;overflow-x:hidden; height:50px;">
                <table cellpadding="0" cellspacing="0" width='100%' class='newformat' height="50px" border='1' style='border-collapse:collapse; ' bordercolor="white">
                <thead>
                <tr>
                    <td style="width:25px;"></td>
                    <td > <asp:Label ID="lblSearchText" Font-Bold="true" runat="server" ></asp:Label> </td>
                    <td colspan="5"  style="background-color : #EEE8AA; color:Black; border-color:Black; width:350px; font-weight:bold;">CURRENT MONTH ( <asp:Label ID="lblMonthDays" runat="server" ></asp:Label> days )</td>
                    <td colspan="6"  style="background-color:#FFEFD5;color:Black;border-color:Black; width:410px; font-weight:bold;">YEAR TO DATE ( <asp:Label ID="lblBudgetDays" runat="server" ></asp:Label> days ) </td>
                    <td colspan="2" style="width:130px; font-weight:bold;" >ANNUAL  ( <asp:Label ID="lblDays" runat="server" ></asp:Label> days )

                    </td>
               </tr>
                <tr align="left"  >
                    <td style="width:25px;"></td>
                    <td><b>Account Name </b>
                       
                       <%-- <asp:ImageButton ID="imgBudgetCommentReport" runat="server" OnClick="imgBudgetCommentReport_OnClick" ImageUrl="~/Modules/HRD/Images/ViewReport.gif" CausesValidation="false" style="float:right;"  ToolTip="Budget Comment Report" />--%>
                    </td>
                    <td style="background-color : #EEE8AA;color:Black;border-color:Black;width:70px;">
                        <asp:LinkButton ID="lnkActual" runat="server" Text="Actual" OnClick="lnkActual_OnClick" ToolTip="Detail Activity Report" ></asp:LinkButton>
                    </td>
                    <td style="background-color : #EEE8AA;color:Black;border-color:Black;width:70px;">
                        <b>
                        <span onclick="ShowVARReport();" >
                        <a href="#" >
                        Accrual
                        </a>
                        </span>
                        </b>
                    </td>
                    <td style="background-color : #EEE8AA;color:Black;border-color:Black;width:70px;">
                        <b>Total</b>
                    </td>
                    <td style="background-color : #EEE8AA;color:Black;border-color:Black;width:70px;">
                        <b>Budget</b>
                    </td>
                    <td style="background-color : #EEE8AA;color:Black;border-color:Black;width:70px;">
                        <b>V$</b>
                    </td>
                    <td style="background-color:#FFEFD5;color:Black;border-color:Black;width:70px;">
                       <b>Actual</b>
                    </td>
                    <td style="background-color:#FFEFD5;color:Black;border-color:Black;width:70px;">
                          <b>Accrual</b>
                    </td>
                    <td style="background-color:#FFEFD5;color:Black;border-color:Black;width:70px;">
                        <b>Total</b>
                    </td>
                    <td style="background-color:#FFEFD5;color:Black;border-color:Black;width:70px;">
                        <b>Budget</b>
                    </td>
                    <td style="background-color:#FFEFD5;color:Black;border-color:Black;width:70px;">
                        <b>V$</b>
                    </td>
                    <td style="background-color:#FFEFD5;color:Black;border-color:Black;width:70px;">
                       <b>V%</b>
                    </td>
                    <td style="width:70px;">
                       <b>Budget</b>
                    </td>
                    <td style="width:60px;">
                       <b>Util'n</b>
                    </td>
                    <%--<td style="width:17px;">&nbsp;</td>--%>
                </tr>
                </thead>
                </table>
                </div>
                <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center; font-size:9px; vertical-align:top;padding-top:0px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="1" class="newformat withpad" style='border-collapse:collapse; text-align:right;'>
                <tbody>
                <asp:Repeater ID="rptItems" runat="server">
                <ItemTemplate>
                <tr class="" style="font-weight:bold; background-color : #e2e2e2" onmouseout="SetRow(this);" onmouseover="SetAlterNateRow(this);">  
                    <td style="width:25px;" onclick='ViewMajorTable(this,<%#Eval("MAJCATID")%>)'>
                        <%# ((ddlReportLevel.SelectedIndex >= 2) ? "[-]" : "")%>
                    </td>
                    <td style="text-align :left" ><%#Eval("MAJORCAT") %> </td>
                    <td style="width:70px;"><%#ProjectCommon.FormatCurrency2(Eval("AcctCMAct"))%></td>
                    <td style="width:70px;" >
                        <asp:Label ID="lblBidQty" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%>'></asp:Label>
                    </td> 
                    <td style="width:70px;">
                        <%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%>
                    </td>
                    <td  style="width:70px;">
                        <asp:Label ID="lblV" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%>'></asp:Label>
                    </td> 
                    <td style="width:70px;">
                        <asp:Label ID="lblPer" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%>' ></asp:Label>
                    </td>
                    <%--------------------------------------------------------------------------------------------------------------------%>
                    <td style="width:70px;">
                        <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%>'></asp:Label>
                    </td>
                    <td style="width:70px;">
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td style="width:70px;">
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td style="width:70px;">
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td style="width:70px;">
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td style="width:70px;">
                       <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                    <%--------------------------------------------------------------------------------------------------------------------%>
                    <td style="width:70px;">
                       <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                    </td>
                    <td style="width:60px;">
                       <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>   
                    </td>
                    <%--<td style='width:17px'>&nbsp;</td>--%>
                </tr>
                <asp:Repeater ID="rptItems1" runat="server" DataSource='<%#getItems1(Eval("MAJCATID"))%>' OnItemDataBound="rptItems1_OnItemDataBound">
                            <ItemTemplate>
                            <tr  name='maj<%#Eval("MAJCATID")%>' onmouseout="SetRow(this);" onmouseover="SetAlterNateRow(this);">  
                            <td onclick='ViewMidTable(this,<%#Eval("MAJCATID")%>,<%#Eval("MIDCATID")%>)'>
                                   <%# ((ddlReportLevel.SelectedIndex >= 3) ? "[-]" : "")%>
                                   <asp:ImageButton ID="btnViewComment" runat="server" ImageUrl="~/Images/AddPencil.gif" Visible="false"  OnClick="btnViewComment_OnClick" ToolTip="Add/Edit Comments" style="float:left;" />
                                   <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/AddPencil.gif" Visible="false" OnClick="btnAdd_OnClick" ToolTip="Add Comments" style="float:left;"/>
                            </td>
                            <td style="text-align :left;">
                            
                            
                            <span style="float :left"><%#Eval("MIDCAT") %> </span>
                            <%--<img src="Images/icon_note.png" alt="key" title="View/Edit Comments" style='float:right;display:<%#(authRecInv.IsUpdate)?"block":"none"%>' onclick='ViewBudgetItem(<%#Eval("MajCatID") %>,<%#Eval("MidCatID").ToString() %>,<%#Eval("AcctYTDAct")%>,<%#Eval("AcctYTDCons")%>,<%#Eval("AcctYTDBgt")%>);'>--%>
                            <asp:ImageButton ID="imgView" runat="server" ToolTip="View Comments" Visible="false" style='float:right;' OnClick="imgView_OnClick" ImageUrl="~/Images/icon_comment.gif" CausesValidation="false" />
                            
                            <asp:HiddenField ID="hfMajCatID" runat="server" Value='<%#Eval("MajCatID") %>' />
                            <asp:HiddenField ID="hfMidCatID" runat="server" Value='<%#Eval("MIDCATID")%>'/>
                            <asp:HiddenField ID="hfCommActual" runat="server" Value='<%#Eval("AcctYTDAct")%>'/>
                            <asp:HiddenField ID="hfCommConsumed" runat="server" Value='<%#Eval("AcctYTDCons")%>'/>
                            <asp:HiddenField ID="hfCommBudget" runat="server" Value='<%#Eval("AcctYTDBgt")%>'/>
                            
                            <asp:HiddenField ID="hfCommentID" runat="server" Value='<%#Eval("CommentID")%>'/>
                            <asp:HiddenField ID="hfComment" runat="server" Value='<%#Eval("Comment")%>'/>
                            </td>
                    <td ><%#ProjectCommon.FormatCurrency2(Eval("AcctCMAct"))%></td>
                    <td >
                        <asp:Label ID="lblBidQty" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%>'></asp:Label>
                    </td> 
                    <td >
                        <%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%>
                    </td>
                    <td >
                        <asp:Label ID="lblV" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%>'></asp:Label>
                    </td> 
                    <td >
                        <asp:Label ID="lblPer" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%>' ></asp:Label>
                    </td>
                    <%--------------------------------------------------------------------------------------------------------------------%>
                    <td >
                        <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%>'></asp:Label>
                    </td>
                    <td >
                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                    </td>
                    <td >
                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                    </td>
                    <td >
                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                    </td>
                    <td >
                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                    </td>
                    <td >
                       <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                    </td>
                        <%--------------------------------------------------------------------------------------------------------------------%>
                        <td >
                           <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                        </td>
                        <td >
                           <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>   
                        </td>
                            <%--<td style='width:17px'>&nbsp;</td>--%>
                        </tr>
                            <asp:Repeater ID="rptItems2" runat="server" DataSource='<%#getItems2(Eval("MIDCATID"))%>'>
                                    <ItemTemplate>
                                    <tr class="alternaterow" style="display:block;" name='maj<%#Eval("MAJCATID")%>mid<%#Eval("MIDCATID")%>' onmouseout="SetRow(this);" onmouseover="SetAlterNateRow(this);">  
                                    <td>&nbsp;</td>
                                    <td style="text-align :left"><%#Eval("MINORCAT") %></td>
                                    <td ><%#ProjectCommon.FormatCurrency2(Eval("AcctCMAct"))%></td>
                                    <td >
                                        <asp:Label ID="lblBidQty" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctCMCY_Comm"))%>'></asp:Label>
                                    </td> 
                                    <td >
                                        <%#ProjectCommon.FormatCurrency2(Eval("AcctCMYCons"))%>
                                    </td>
                                    <td >
                                        <asp:Label ID="lblV" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AccCMCYBgt"))%>'></asp:Label>
                                    </td> 
                                    <td >
                                        <asp:Label ID="lblPer" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctCYVar"))%>' ></asp:Label>
                                    </td>
                                    <%--------------------------------------------------------------------------------------------------------------------%>
                                    <td >
                                        <asp:Label ID="lblLC" runat="server"  Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDAct"))%>'></asp:Label>
                                    </td>
                                    <td >
                                       <asp:Label ID="lblUsd" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTD_Comm"))%>'></asp:Label>
                                    </td>
                                    <td >
                                       <asp:Label ID="Label1" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDCons"))%>'></asp:Label>
                                    </td>
                                    <td >
                                       <asp:Label ID="Label2" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDBgt"))%>'></asp:Label>
                                    </td>
                                    <td>
                                       <asp:Label ID="Label3" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctYTDVar"))%>'></asp:Label>
                                    </td>
                                    <td >
                                       <span class='<%# SetColorForVPer(Eval("Col1"))%>'> <asp:Label ID="Label4" runat="server" Text='<%#Eval("Col1")%>'></asp:Label> %</span>
                                    </td>
                                <%--------------------------------------------------------------------------------------------------------------------%>
                                <td >
                                   <asp:Label ID="Label5" runat="server" Text='<%#ProjectCommon.FormatCurrency2(Eval("AcctFYBudget"))%>'></asp:Label>
                                </td>
                                <td >
                                   <span class='<%# SetColorForYearPer(Eval("Col2"))%>'> <asp:Label ID="Label6" runat="server" Text='<%#Eval("Col2")%>'></asp:Label> %</span>   
                                </td>
                                    <%--<td style='width:17px'>&nbsp;</td>--%>
                                    </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                            </ItemTemplate>
                            </asp:Repeater>
                </ItemTemplate>
                </asp:Repeater>
                </tbody>
                </table>
                
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblNoRow" runat="server" CssClass="error" style=" padding-top:55px;" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                
             </div>  
             <%-- <div style="height:25px; overflow-y:scroll;overflow-x:hidden;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                    <tfoot>
                        <tr>
                            <td style="width:25px">&nbsp;</td>
                            <td >&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:50px">&nbsp;</td>
                            <td style="width:60px">&nbsp;</td>
                            <td style="width:50px">&nbsp;</td>
                            <td style="width:17px">&nbsp;</td>
                        </tr>
                    </tfoot>
                    </table>
                    </div>  --%>  
                </ContentTemplate> 
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
           
            </td>
        </tr>
        </table>
    </div>
  </form>
</body>
</html>