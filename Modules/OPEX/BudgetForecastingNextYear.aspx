<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="BudgetForecastingNextYear.aspx.cs" Inherits="BudgetForecastingNextYear" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Year Budget</title>
    <link href="CSS/style.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript" src="JS/Common.js"></script>
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
        function ViewMonthlyBudget(crtl, AcctID, AccountID, AccountNumber, AccountName, AnnAmt, midcatid) {

            var SelMonth=-1;
            
            var year = document.getElementById('lblBudgetYear').innerText; 
            var Co = document.getElementById('ddlCompany').value;
            var Vess = document.getElementById('ddlShip').value;
            var BType = document.getElementById('ddlBudgetType').value;
            //var Budget = crtl.parentNode.previousSibling.childNodes[0].value;
            var Budget = crtl.parentNode.parentNode.childNodes[11].childNodes[1].value;
            var StartDate = document.getElementById('txtStartDate').value;
            var EndDate = document.getElementById('txtEndDate').value;
            var YearDays = document.getElementById('lblDays').innerText;
            window.open("MonthlyBudgetForecasting_NextYear.aspx?AcctID=" + AcctID + "&AccountID=" + AccountID + "&AccountNumber=" + AccountNumber + "&year=" + year + "&Co=" + Co + "&Vess=" + Vess + "&MajCatID=" + BType + "&Budget=" + Budget + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&AnnAmt=" + AnnAmt + "&midcatid=" + midcatid + "", "", "height=520,width=350,resizable=no , toolbar=no,location=no"); 
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
            window.open("Print.aspx?BudgetForeCast=true&Comp=" + Co + "&Vessel=" + Vess + "&BType=" + BType + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + year + "&YearDays=" + YearDays + "&MajCatID=" + MajCatID + "", "", "");  //height=490,width=350,
        }
        function PUBLISHRPT() {
            if (confirm("Are you sure to publish?")) {
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
                window.open("Print2.aspx?BudgetForeCast=true&Comp=" + Co + "&Vessel=" + Vess + "&BType=" + BType + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + year + "&YearDays=" + YearDays + "&MajCatID=" + MajCatID + "", "", "");  //height=490,width=350,
            }
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
                   <td class="header" style=" padding:4px;">
                        Budget Forecast
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
                            <tr class="header" >
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
                            <tr style="text-align:center;">
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
                <tr class="header">
                    <td>
                        <asp:Label ID="lblCompany" runat="server" ></asp:Label>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtStartDate"></asp:CalendarExtender>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEndDate"></asp:CalendarExtender>
                    </td>
                </tr> 
                <tr>
                    <td style="padding:2px; ">
                     <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center;">
                            <col width="60px" />
                            <col width="250px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="90px" />
                            <col width="30px" />
                            <col width="80px" />
                            <col width="180px" />
                            <col  />
                            <col width="17px" />
                            <tr style=" height :20px; background-color : Gray ; font-weight: bold; color :White " >
                                <td style="vertical-align:middle;" >Acct.#</td>
                                <td style="vertical-align:middle;" >Account Name</td>
                                
                                <td style="vertical-align:middle;" >
                                    <asp:Label runat="server" ID="lblYr1"></asp:Label><br />Act & Comm 
                                </td>
                                <td style="vertical-align:middle;" >
                                    <asp:Label runat="server" ID="lblYr3"></asp:Label><br />Projected  
                                </td>
                                <td style="vertical-align:middle;" >
                                    <asp:Label runat="server" ID="lblYr"></asp:Label><br />Budget  
                                </td>
                                <td style="vertical-align:middle;" ><asp:Label runat="server" ID="lblYrNext"></asp:Label><br />Forecast</td>
                                <td></td>
                                <td><asp:Label runat="server" ID="lblYrr11"></asp:Label>-Var.%<hr/>B <span style="color : Blue; font-family: Arial Narrow" >v/s</span> P</td>
                                <td>
                                <asp:Label runat="server" ID="lblYr1_" Width="40px" ></asp:Label>-Forecast Var.% <span style="color : Blue; font-family: Arial Narrow" >v/s</span><hr />
                                <asp:Label runat="server" ID="lblYrr12"></asp:Label>[B]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblYrr13"></asp:Label>[P]
                                </td>
                                <td style="vertical-align:middle;" >Comments</td>
                                <td></td>
                            </tr>
                            </table> 
                        <div id="dvscroll_BF"  style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 290px ; text-align:center;" onscroll="SetScrollPos(this)">
                        <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center; font-size:10px; ">
                            <col width="60px" />
                            <col width="250px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="90px" />
                            <col width="30px" />
                            <col width="80px" />
                            <col width="90px" />
                            <col width="90px" />
                            <col  />
                            <col width="17px" />
                            <asp:Repeater ID="rptBudget" runat="server"  >
                                <ItemTemplate>
                                    <tr >
                                        <td style="text-align:left;vertical-align:middle; padding-left :5px; text-align:center;" >
                                            <%#Eval("AccountNumber")%> 
                                        </td>
                                        <td style="text-align:left;vertical-align:middle; padding-left :5px;" valign ="middle">
                                            <%#Eval("AccountName1")%> 
                                            <asp:HiddenField ID="hfAccID" runat="server" Value='<%#Eval("AcctID")%>' />
                                            <asp:HiddenField ID="hfAccountID" runat="server" Value='<%#Eval("AccountID")%>' />
                                            <asp:HiddenField ID="hfAccountNumber" runat="server" Value='<%#Eval("AccountNumber")%>' />
                                        </td>
                                       
                                         <td style="text-align:right">
                                            <asp:Label runat ="server" ID="lblActAmt"></asp:Label>
                                        </td>
                                        <td style="text-align:right">
                                            <asp:Label runat ="server" ID="lblActAmtCal"></asp:Label>
                                        </td>
                                         <td style="text-align:right">
                                            <asp:Label runat ="server" ID="lblBudgetCal" Text='<%#Eval("Budget")%>'></asp:Label>
                                        </td>
                                       
                                       <td style="vertical-align:middle;" >
                                            <asp:TextBox ID="txtAnnAmt" onkeypress='fncInputNumericValuesOnly(this)' onfocus="this.style.backgroundColor='lightgreen';" onblur="this.style.backgroundColor='#FFFFCC';" runat="server" Text='<%#Eval("AnnAmt")%> ' Width="83px" MaxLength="20" style="text-align:right;"></asp:TextBox>
                                        </td>
                                         
                                        <td style="vertical-align:middle;" >
                                            <img src="Images/edit.png" style='display:<%#(authRecInv.IsUpdate)?"block":"none"%>' title="Monthly Allocation." onclick='ViewMonthlyBudget(this,<%#Eval("AcctID")%>,<%#Eval("AccountID")%>,<%#Eval("AccountNumber")%>,<%#Eval("AccountNumber")%>,<%#Eval("AnnAmt")%>,<%#Eval("midcatid")%>)' />
                                        </td>
                                        <td><asp:Label runat="server" ID="lblVarAC"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblVarBug"></asp:Label></td>
                                        <td><asp:Label runat="server" ID="lblVarProj"></asp:Label></td>
                                        <td style="text-align:left; padding-left:1px;"><img style='cursor:pointer;float:right;<%#(Eval("ForeCastComment").ToString().Trim()=="")?"display:none":""%>'  src="Images/link-icon.png" mess='<%#Eval("ForeCastComment").ToString().Replace("'","`")%>' onclick="alert(this.mess);"/></td>
                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>                          
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            
                        </table>
                        </div> 
                    </td>
                </tr>
                </table> 
                <div>
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
                        <table cellpadding="0" cellspacing="0" width="100%" border="1px" style="border-collapse:collapse; text-align:center;">
                                <colgroup>
                                    <col width="310px" />
                                    <col width="100px" />
                                    <col width="100px" />
                                    <col width="100px" />
                                    <col width="90px" />
                                    <col width="120px" />
                                    <col />
                                    <col width="17px" />
                                    <tr style="background-color:#4371a5; color:White;">
                                        <td style="text-align:right; padding-right:5px;padding-top:4px;">
                                            <b>Total (US$)</b>
                                        </td>
                                        <td style="text-align:right; padding:4px 2px 4px 0px;">
                                            <asp:Label ID="lblActTotal" runat="server" style="font-weight:bold;"></asp:Label>
                                        </td>
                                        <td style="text-align:right; padding:4px 2px 4px 0px;">
                                            <asp:Label ID="lblActCalTotal" runat="server" style="font-weight:bold;"></asp:Label>
                                        </td>
                                        <td style="text-align:right; padding:4px 2px 4px 0px;">
                                            <asp:Label ID="lblBudgetTotal" runat="server" style="font-weight:bold;"></asp:Label>
                                        </td>
                                        <td style="text-align:right; padding:4px 2px 4px 0px;">
                                            <asp:Label ID="lblTotal" runat="server" style="font-weight:bold;"></asp:Label>
                                        </td>
                                        <td style="text-align:right;padding:4px 2px 4px 0px;">
                                            <b>Approved By/On :</b>
                                        </td>
                                        <td style="text-align:left;padding:4px 2px 4px 2px;">
                                            <asp:Label ID="lblUppByAndOn" runat="server" style="font-weight:bold;"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="background-color:#4371a5; color:White;">
                                        <td colspan="3" style="text-align:right; padding:4px 2px 4px 0px;">
                                            <b></b>
                                        </td>
                                        <td style="text-align:right; padding:4px 2px 4px 0px;">
                                        </td>
                                        <td style="text-align:right; padding:4px 2px 4px 0px;">
                                        </td>
                                        <td style="text-align:right;padding:4px 2px 4px 0px;">
                                            <b>Created By/On :</b>
                                        </td>
                                        <td style="text-align:left;padding:4px 2px 4px 2px;">
                                            <asp:Label ID="lblCreatedBy" runat="server" style="font-weight:bold;"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="text-align:right;">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="error"></asp:Label>
                                            <asp:ImageButton ID="imgImport" runat="server" 
                                                ImageUrl="~/Images/importbudget.jpg" OnClick="imgImport_OnClick" 
                                                OnClientClick="return confirm('Are you sure to import budget for selected vessel?');" 
                                                Visible="false" />
                                            <asp:ImageButton ID="imgApprove" runat="server" 
                                                ImageUrl="~/Images/approvebudget.jpg" OnClick="imgApprove_OnClick" />
                                            <%--<asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Images/save.jpg" OnClick="imgSave_OnClick" />--%>
                                            <asp:ImageButton ID="Print" runat="server" ImageUrl="~/Images/print.jpg" 
                                                OnClientClick="ViewPrintRPT();" />
                                                <div style="float :right" >
                                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                                <ContentTemplate>--%>
                                                    <asp:ImageButton ID="imgbtnPublish" runat="server" ImageUrl="~/Images/publish.jpg" OnClientClick="PUBLISHRPT(); return false;" />
                                                <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                            </div>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table> 
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div> 
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
