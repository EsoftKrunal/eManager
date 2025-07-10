<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetForecasting.aspx.cs" Inherits="BudgetForecasting" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Year Budget</title>
    <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
     <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
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

        function ViewBudgetItem(CommMajID ,CommMidID) 
        {
            window.open("BudgetItemComments.aspx?CommMajID=" + CommMajID + "&CommMidID=" + CommMidID + "&year=" + document.getElementById('ddlyear').value + "&Company=" + document.getElementById('ddlCompany').value + "&Vessel=" + document.getElementById('ddlVessel').value + "&Month=" + document.getElementById('ddlMonth').value + "&CompanyName=" + document.getElementById('ddlCompany').options[document.getElementById('ddlCompany').selectedIndex].text + "");
        }
        function ViewMajorTable(obj,ctls)
        {
              var result=(obj.innerHTML=='[+]')?"block":"none";
              var resultsign=(obj.innerHTML=='[+]')?"[-]":"[+]";
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
                        rw.childNodes[0].innerHTML="[+]"; 
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
//            .setAttribute("oldclassName", row.getAttribute("className"));            
//            row.setAttribute("className", "selectedrow1");    
        }
        function SetRow(row) {
            row.style.color = 'Black';
            row.style.textDecoration = 'none';
//row.setAttribute("className", row.getAttribute("oldclassName"));
        }
       function fncInputNumericValuesOnly(ctl) 
       {
            if(event.keyCode==13)
            {  
                var first=ctl.id.indexOf("_",0);
                var sec=ctl.id.indexOf("_",first+1);
                first=first+4; 
                var thisctl=ctl.id.substr(first,sec-first);
                var a=parseFloat(thisctl)+1;
                var nid;
                if(a<=9)
                    nid='rptBudget_ctl0' + a + '_txtForeCast';  
                else
                    nid='rptBudget_ctl' + a + '_txtForeCast';  
                    
                var nctl=document.getElementById(nid);  
                if(nctl!=null)
                {
                 nctl.focus();  
                }
                event.returnValue = false;
            } 
            else
            {
                if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) 
                {
                    event.returnValue = false;
                }
            } 
        }
    </script>
    <script type="text/javascript" >
        function getElementByLike(In_Cont, FindCtl, TagName) {
            var ret = null;
            var ctrlen = FindCtl.length;
            var totctls = In_Cont.getElementsByTagName(TagName);
            var i = 0;
            for (i = 0; i <= totctls.length - 1; i++) {
                if (totctls[i].id.length >= ctrlen) {
                    var FindPart = totctls[i].id.substr(totctls[i].id.length - ctrlen, totctls[i].id.length - (totctls[i].id.length - ctrlen));
                    if (FindPart == FindCtl) {
                        ret = totctls[i];
                    }
                }
            }
            return ret;
        }
        function ViewMonthlyBudget(crtl, AcctID, AccountID, AccountNumber, AccountName, AnnAmt, midcatid) {
            var SelMonth=-1;
            var year = document.getElementById('lblBudgetYear').innerText; 
            var Co = document.getElementById('ddlCompany').value;
            var Vess = document.getElementById('ddlShip').value;
            var BType = document.getElementById('ddlBudgetType').value;
            var BudgetCtl = getElementByLike(crtl.parentNode.parentNode, 'txtAnnAmt', 'input');
            var Budget = BudgetCtl.value;
            var StartDate = document.getElementById('txtStartDate').value;
            var EndDate = document.getElementById('txtEndDate').value;
            var YearDays = document.getElementById('lblDays').innerText;
            window.open("MonthlyBudgetForecasting.aspx?AcctID=" + AcctID + "&AccountID=" + AccountID + "&AccountNumber=" + AccountNumber + "&year=" + year + "&Co=" + Co + "&Vess=" + Vess + "&MajCatID=" + BType + "&Budget=" + Budget + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&AnnAmt=" + AnnAmt + "&midcatid=" + midcatid + "", "", "height=520,width=350,resizable=no , toolbar=no,location=no"); 
        }
        function RefereshPage() 
        {
            document.getElementById('btnreload').click();
        }
        function ViewPrintRPT() 
        {
            var year = document.getElementById('lblBudgetYear').innerText;
            var CoObj = document.getElementById('ddlCompany');
            var Co = CoObj.options[CoObj.selectedIndex].text;
            var VessObj = document.getElementById('ddlShip');
            var Vess = VessObj.options[VessObj.selectedIndex].text;
            var BTypeObj = document.getElementById('ddlBudgetType');
            BType = BTypeObj.options[BTypeObj.selectedIndex].text;
            var MajCatID = BTypeObj.options[BTypeObj.selectedIndex].value;
            
            var StartDate = document.getElementById('txtStartDate').value;
            var EndDate = document.getElementById('txtEndDate').value;
            var YearDays = document.getElementById('lblDays').innerText;

            var Total = document.getElementById('lblTotal').value;
            window.open("Print.aspx?CYBudget=true&Comp=" + Co + "&Vessel=" + Vess + "&BType=" + BType + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + year + "&YearDays=" + YearDays + "&MajCatID=" + MajCatID + "", "", "");  //height=490,width=350,
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>        
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td style="width:177px;">
                <div style="text-align : center;background-color : #4371a5;width:177px;min-height:465px; vertical-align : top; padding-bottom :15px;">
                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/logo.jpg" Visible ="false"  />
                <br />
                <div style="min-height:200px; width :177px" >
                    <table style="width:177px; text-align : center " cellpadding="5" cellspacing="0" border="0">
                    <tr id="trCurrBudget" runat="server">
                        <td> 
                            <a href='<%=Page.ResolveUrl("~/BudgetForecastingNew.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/curryearbudget.jpg")%>'  border="0"/> </a>
                        </td>
                    </tr>
                    <tr id="trAnalysis" runat="server">
                        <td>
                            <a href='<%=Page.ResolveUrl("~/ReportingAndAnalysis.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/analysis_comments.jpg")%>'  border="0"/></a>
                        </td>
                    </tr>
                    <tr id="trBudgetForecast" runat="server">
                        <td>
                            <a href='<%=Page.ResolveUrl("~/BudgetForecastingNextYearNew.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/budgetforecast.jpg")%>'  border="0"/></a>
                        </td>
                    </tr>
                   <tr id="trPublish" runat="server">
                        <td>
                            <a href='<%=Page.ResolveUrl("~/PublishReport.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/publishreport.jpg")%>'  border="0"/></a>
                        </td>
                    </tr>
                    </table>
                </div> 
             </div>
            </td>
            <td>
                <table cellpadding="1" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="PageHeader">
                        Current Year Budget
                       <asp:Button ID="btnreload" runat="server" OnClick="btnreload_OnClick" style="display:none;" />
                   </td>
                </tr>
                <tr>
                      <td>
                        <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" >
                            <col  width="260px"/>
                            <col  width="260px"/>
                            <col  width="90px"/>
                            <col  width="90px"/>
                            <col  width="90px"/>
                            <col  width="50px"/>
                            <col />
                            <tr style="font-weight:bold" >
                                <td>
                                    Company
                                </td>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    Budget Year
                                </td>
                                <td>
                                    Start Date
                                </td>
                                <td>
                                    End Date
                                </td>
                                <td>
                                    Days
                                </td>
                                <td>
                                     Budget Type 
                                </td>
                            </tr>
                            <tr style="text-align:center;background-color:#F5FAFF;">
                                <td>
                                    <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
                                    
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlShip" runat="server"  OnSelectedIndexChanged="ddlShip_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblBudgetYear" runat="server" ></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="80px" MaxLength="15"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="80px" MaxLength="15"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblDays" runat="server" ></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBudgetType" runat="server" Width="150px" OnSelectedIndexChanged="ddlBudgetType_OnSelectedIndexChanged" AutoPostBack="true"     ></asp:DropDownList>
                                </td>
                            </tr>
                        </table> 
                      </td>
                 </tr>
                <tr style="background-color:#FF6600;">
                    <td style="color:White;font-weight:bold; text-align: center;">
                        <asp:Label ID="lblCompany" runat="server" ></asp:Label>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtStartDate"></asp:CalendarExtender>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEndDate"></asp:CalendarExtender>
                    </td>
                </tr> 
                <tr>
                    <td style="padding:2px; ">
                    <div style="height:25px; overflow-y:scroll;overflow-x:hidden;">
                     <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:25px;' bordercolor="white">
                    <thead>
                            <tr >
                                <td style="vertical-align:middle; width:60px;" >Acct. #</td>
                                <td style="vertical-align:middle;width:250px;" >Account Name</td>
                                
                                <td style="vertical-align:middle; width:120px;" >Annual Amt(US$)</td>
                                <td style="width:30px;"></td>
                                <td style="vertical-align:middle;width:120px;" >CY Amt(US$)</td>
                                <td style="vertical-align:middle;" >Comments</td>
                            </tr>
                            </thead>
                            </table>
                            </div> 
                        <div id="dvscroll_BF" class='ScrollAutoReset'  style="HEIGHT: 270px;">
                        <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                        <tbody>
                            <asp:Repeater ID="rptBudget" runat="server"  >
                                <ItemTemplate>
                                    <tr onmouseover="">
                                        <td style="text-align:left;vertical-align:middle;  text-align:center;width:60px;" >
                                            <%#Eval("AccountNumber")%> 
                                        </td>
                                        <td style="text-align:left;vertical-align:middle; width:250px;" valign ="middle">
                                            <%#Eval("AccountName1")%> 
                                            <asp:HiddenField ID="hfAccID" runat="server" Value='<%#Eval("AcctID")%>' />
                                            <asp:HiddenField ID="hfAccountID" runat="server" Value='<%#Eval("AccountID")%>' />
                                            <asp:HiddenField ID="hfAccountNumber" runat="server" Value='<%#Eval("AccountNumber")%>' />
                                        </td>
                                        
                                        <td style="vertical-align:middle;width:120px;" >
                                            <asp:TextBox ID="txtAnnAmt" onkeypress='fncInputNumericValuesOnly(this)' onfocus="this.style.backgroundColor='lightgreen';" onblur="this.style.backgroundColor='#FFFFCC';" runat="server" Text='<%#Eval("AnnAmt")%> ' Width="100px" MaxLength="20" style="text-align:right;"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align:middle;width:30px; text-align:center;" >
                                            <%--<asp:Image ID="imgMo" runat="server" ImageUrl="~/Images/edit.png" OnClientClick='ViewMonthlyBudget(<%#Eval("AcctID")%>)'/>  --%>
                                            <%--OnClick="imgMo_OnClick"--%>
                                            <img src="Images/edit.png" style='display:<%#(authRecInv.IsUpdate)?"block":"none"%>' title="Monthly Allocation." onclick='ViewMonthlyBudget(this,<%#Eval("AcctID")%>,<%#Eval("AccountID")%>,<%#Eval("AccountNumber")%>,<%#Eval("AccountNumber")%>,<%#Eval("AnnAmt")%>,<%#Eval("midcatid")%>)' />
                                        </td>
                                        <td style =" text-align :right ; width:120px; ">
                                        <%# (Common.CastAsDecimal(Eval("AnnAmt"))!=Common.CastAsDecimal(Eval("Forecast")))?"<div style='background-color:orange;width:100%'>":""%>
                                        <asp:Label runat="server" ID="lblActAmt" Text='<%#String.Format("{0:C}", long.Parse(Eval("Forecast").ToString())).Replace("$", "").Replace("Rs.", "").Replace(".00", "")%>' ></asp:Label> 
                                        <%# (Common.CastAsDecimal(Eval("AnnAmt")) != Common.CastAsDecimal(Eval("Forecast"))) ? "</div>" : ""%>
                                        </td>
                                        <td style="text-align:left; padding-left:1px;">
                                            <%#getCommentString(Eval("YearComment"))%>
                                            <img style='cursor:pointer;float:right;<%#(Eval("YearComment").ToString().Trim()=="")?"display:none":""%>'  src="Images/link-icon.png" onclick='alert(<%#"\"" + Eval("YearComment").ToString().Replace("'","`") + "\""%>);'/> 
                                        </td>                    
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tbody>
                            
                        </table>
                        </div> 
                    </td>
                </tr>
                </table> 
                <div style="padding:2px;">
                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100; text-align :center; color :Red; font-size:11px; ">
                        <center>
                        <div style="border:dotted 1px blue; height :80px; width :150px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"><br /> Processing ... <br />Please Wait !
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
                </asp:UpdateProgress> 
                <asp:UpdatePanel  runat="server" ID="up1" >
                        <ContentTemplate >
                        <div style="overflow-y:scroll;overflow-x:hidden;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                    <tfoot>
                                <tr style="height:22px;">
                                    <td style="text-align:left;width:312px; vertical-align:middle;">
                                        <span style="float:right;"><b>Total (US$)</b> </span>
                                    </td>
                                    <td style="text-align:right; width:120px;vertical-align:middle;">
                                        <asp:Label ID="lblTotalAnnAmt" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                    <td style="width:30px;">
                                    </td>
                                    <td style="text-align:right; width:120px;vertical-align:middle;">
                                        <asp:Label ID="lblTotal" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                    <td style=" text-align:right;width:150px;vertical-align:middle;">
                                        <b>App. By/On : </b>
                                    </td>
                                    <td style=" text-align :left;vertical-align:middle; ">
                                        <asp:Label ID="lblUppByAndOn" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height:22px;">
                                    <td style="text-align:left;">
                                        <span style="float:right;"><b></b></span>
                                    </td>
                                    <td style=" text-align: right;">
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align:right; ">
                                    </td>
                                    <td style=" text-align:right;vertical-align:middle;">
                                        <b>Exported By/On : </b>
                                    </td>
                                    <td style=" text-align :left;vertical-align:middle; ">
                                        <asp:Label ID="lblExportedBy" runat="server" style="font-weight:bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height:35px;">
                                    <td colspan="6" style="text-align:right;vertical-align:middle;">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="error"></asp:Label>
                                        <asp:ImageButton ID="imgImport" runat="server" 
                                            ImageUrl="~/Images/importbudget.jpg" OnClick="imgImport_OnClick" 
                                            OnClientClick="return confirm('Are you sure to import budget for selected vessel?');" />
                                        <asp:ImageButton ID="imgLockBudget" runat="server" 
                                            ImageUrl="~/Images/lockbudget.jpg" OnClick="LockBudget_OnClick" 
                                            OnClientClick="return confirm('Are you sure to lock budget ?');" />
                                        <asp:ImageButton ID="imgSave" Visible="false" runat="server" ImageUrl="~/Images/save.jpg" 
                                            OnClick="imgSave_OnClick" />
                                        <asp:ImageButton ID="Print" runat="server" ImageUrl="~/Images/print.jpg" 
                                            OnClientClick="ViewPrintRPT();" />
                                            <div style="float :right; padding-right:5px;" >
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgbtnPublish" runat="server" ImageUrl="~/Images/publish.jpg" onclick="imgbtnPublish_Click" OnClientClick="return confirm('Are you sure to publish?')" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </div>&nbsp;
                                    </td>
                                </tr>
                            
                            </tfoot>
                        </table> 
                        </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
                 </div>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
