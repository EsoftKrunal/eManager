<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrentYearProjection.aspx.cs" Inherits="Modules_OPEX_CurrentYearProjection" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
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
            var Budget = crtl.parentNode.previousSibling.childNodes[0].value;
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
            BType = "< All >";
            var MajCatID = 6;
            
            var StartDate = document.getElementById('txtStartDate').innerHTML;
            var EndDate = document.getElementById('txtEndDate').innerHTML;
            var YearDays = document.getElementById('lblDays').innerText;

            var Total = 0;
            
            if(document.getElementById("radVesselView").checked && VessObj.selectedIndex > 0)  
            {
                window.open("Print.aspx?BudgetForeCast=true&Comp=" + Co + "&Vessel=" + Vess + "&BType=" + BType + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&year=" + year + "&YearDays=" + YearDays + "&MajCatID=" + MajCatID + "", "", "");  //height=490,width=350,
                
            }
            return false;
        }
         function ShowBox(ctl) 
        {
            alert(document.getElementById(ctl).innerHTML);  
        }
    </script>

     <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family:Arial;font-size:12px;">
             
     <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up3" ID="UpdateProgress1">
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
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>        
     <asp:UpdatePanel ID="UP3" runat="server" >
     <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfd_Ships"></asp:HiddenField> 
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;  font-size :12px; ">
                <tr>
                      <td style="padding-right:10px;height:23px;">
                        <div runat="server" visible="false">
                            <asp:RadioButton runat="server" ID="radVesselView" Text="Vessel View" GroupName="a" OnCheckedChanged="Vessel_Select" AutoPostBack="true" style="float:right;" Visible="false"/>  
                            <asp:RadioButton runat="server" ID="radFleetView" Font-Bold="true" Checked="true" Text="Fleet View" GroupName="a" OnCheckedChanged="Fleet_Select" AutoPostBack="true" style="float:right;"  Visible="false"/>  
                        </div>
                       
                      </td>
                 </tr>
                <tr>    
                    <td style="padding:2px">
                    <asp:Panel runat="server" ID="pnl_Fleet">
                            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" >
                                <colgroup>
                                    <col width="260px" />
                                    <col width="260px" />
                                    <col />
                                    <tr>
                                        <td colspan="3">
                                            
                                                
                                            <table width="100%" border="1">
                                                <col width="250px" />
                                                <col width="250px" />
                                                <col />
                                                <tr class="header" style="height:20px; padding-top :2px;">
                                                    <td>
                                                        </td>
                                                    <td>
                                                        <asp:Label ID="lblFleetOrCompany" runat="server" Text="Fleet" ></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                            <asp:RadioButtonList ID="rdoList" runat="server" OnSelectedIndexChanged="rdoList_OnSelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Fleet" Value="1" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Company" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" 
                                                            OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged">
                                                        </asp:DropDownList>    
                                                            
                                                        <asp:DropDownList ID="ddlCompanyF" runat="server" AutoPostBack="true"  Visible="false"
                                                            OnSelectedIndexChanged="ddlFleetComp_OnSelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                            <asp:DropDownList ID="ddlVesselF" runat="server" Visible="false">
                                            </asp:DropDownList>
                                                <%--<asp:ImageButton ID="btnEdit2" runat="server" ImageUrl="~/Images/Edit.jpg" onclick="btnEdit2_Click" />--%>
                                                <asp:Label ID="lblsummary" style=" float :left; padding-top :3px; padding-left :5px;" runat="server"></asp:Label>
                                                <asp:Button runat="server" ID="btnEdit2" Text="Edit Budget" Height="25px" Width="120px" OnClick="btnEdit2_Click"/>
                                                <asp:Button runat="server" ID="btnPrntSummary" Text="Print Fleet Summary" Height="25px" Width="160px" OnClick="btnPrntSummary_Click"/>
                                                <asp:Button runat="server" ID="btnPrntDetails" Text="Print Details" Height="25px" Width="120px" OnClick="btnPrntDetails_Click" Visible="false"/>
                                                <asp:ImageButton ID="ImageButton3" Visible="false" runat="server" ImageUrl="~/Modules/HRD/Images/print.jpg" onclick="Print_Click" />
                                                    </td>
                                                </tr>
                                                
                                                
                                            </table>
                                            
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr style="text-align:center;">
                                        <td style=" text-align :right">
                                            
                                        </td>
                                    </tr>
                                </colgroup>
                        </table>
                        <asp:Literal runat="server" ID="lit1"></asp:Literal>  
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnl_Vessel">
                        <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" >
                            <col width="260px"/>
                            <col width="260px"/>
                            <col width="100px"/>
                            <col width="100px"/>
                            <col width="100px"/>
                            <col width="50px"/>
                            <col />
                            <tr class="header" style="height:20px; padding-top :2px;" >
                                <td>Company</td>
                                <td>Vessel</td>
                                <td>Budget Year</td>
                                <td>Start Date</td>
                                <td>End Date</td>
                                <td>Days</td>
                                <td>&nbsp;</td>
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
                               <td><asp:Label ID="txtStartDate" runat="server" ></asp:Label></td>
                                <td><asp:Label ID="txtEndDate" runat="server" ></asp:Label></td>
                                <td><asp:Label ID="lblDays" runat="server" ></asp:Label></td>
                                <td>
                                   <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/Images/Edit.jpg" PostBackUrl="~/BudgetForecastingNextYear.aspx" /> 
                                   <asp:ImageButton ID="Print" runat="server" ImageUrl="~/Images/print.jpg" OnClientClick="return ViewPrintRPT();" onclick="Print_Click"/>
                                   </td>
                            </tr>
                        </table>
                        <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; text-align:center;">
                            <col width="250px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="120px" />
                            <col width="120px" />
                            <col  />
                            <col width="17px" />
                            <tr class= "headerstylegrid" >
                                <td style="vertical-align:middle;" >Account Head</td>
                                <td style="vertical-align:middle;" ><asp:Label runat="server" ID="lblYr1"></asp:Label><br />Act & Comm </td>
                                <td style="vertical-align:middle;" ><asp:Label runat="server" ID="lblYr3"></asp:Label><br />Projected  </td>
                                <td style="vertical-align:middle;" ><asp:Label runat="server" ID="lblYr"></asp:Label><br />Budget  </td>
                                <td style="vertical-align:middle;" ><asp:Label runat="server" ID="lblYrNext"></asp:Label><br />Forecast</td>
                                <td><asp:Label ID="lblYrCurrCY" runat="server"></asp:Label> -Var.%<hr/>B <span style="color : Blue; font-family: Arial Narrow" >v/s</span> P</td>
                                <td colspan="2"  ><asp:Label runat="server" ID="lblYr1_" Width="40px" ></asp:Label>-Forecast Var.% <span style="color : Blue; font-family: Arial Narrow" >v/s</span><hr /><asp:Label ID="lblYrCurrCY1" runat="server"></asp:Label>[B]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblYrCurrCY2" runat="server"></asp:Label>[P]</td>
                                <td style="vertical-align:middle;" >Comments</td>
                                <td></td>
                            </tr>
                            <asp:Repeater runat="server" ID="Repeater1">
                                <ItemTemplate>
                                <tr>
                                    <td style="text-align:left;" ><%#Eval("AccountHead")%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("ActComm"))%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("Proj"))%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("Bud"))%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("Fcast"))%></td>
                                    <td style="text-align:right;"><%#Eval("Var1")%>%</td>
                                    <td style="width :80px; text-align :right; "><%#Eval("Var2")%>%</td> 
                                    <td style="width :80px; text-align :right;"><%#Eval("Var3")%>%</td>
                                    <td><asp:ImageButton runat="server" ID="img_Comments" CommandArgument='<%#Eval("MidcatId")%>' ToolTip='<%#Eval("AccountHead").ToString().Replace("&","_")%>' OnClick="Comment_Click" ImageUrl="~/Modules/HRD/Images/link-icon.png"/></td>
                                    <td></td>
                                </tr>
                                </ItemTemplate> 
                            </asp:Repeater>
                            <tr class= "headerstylegrid" >
                                    <td style="text-align:left;" >Total (US$):</td>
                                    <td style="text-align:right;" ><asp:Label ID="lblActComm_Sum" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblProjected" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblBudget" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblForeCast" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;"><asp:Label ID="lblVar1" runat="server"></asp:Label>%</td>
                                    <td style="width :80px; text-align :right; "><asp:Label ID="lblVar2" runat="server"></asp:Label>%</td>
                                    <td style="width :80px; text-align :right;"><asp:Label ID="lblVar3" runat="server"></asp:Label>%</td>
                                    <td></td>
                                    <td></td>
                            </tr>
                            <tr class= "headerstylegrid" >
                                    <td style="text-align:left;" >Avg Daily Cost(US$):</td>
                                    <td style="text-align:right;" ><asp:Label ID="lblActComm_Sum1" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblProjected1" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblBudget1" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblForeCast1" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;"></td>
                                    <td style="width :80px; text-align :right; "></td>
                                    <td style="width :80px; text-align :right;"></td>
                                    <td></td>
                                    <td></td>
                            </tr>
                            <asp:Repeater runat="server" ID="Repeater2">
                                <ItemTemplate>
                                <tr >
                                   <td style="text-align:left;" ><%#Eval("AccountHead")%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("ActComm"))%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("Proj"))%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("Bud"))%></td>
                                    <td style="text-align:right;" ><%#FormatCurrency(Eval("Fcast"))%></td>
                                    <td style="text-align:right;"><%#Eval("Var1")%>%</td>
                                    <td style="width :100px; text-align :right; "><%#Eval("Var2")%>%</td> 
                                    <td style="width :100px; text-align :right;"><%#Eval("Var3")%>%</td>
                                    <td><asp:ImageButton runat="server" ID="img_Comments" CommandArgument='<%#Eval("MidcatId")%>' ToolTip='<%#Eval("AccountHead").ToString().Replace("&","_")%>' OnClick="Comment_Click" ImageUrl="~/Modules/HRD/Images/link-icon.png"/></td>
                                    <td></td>
                                </tr>
                                </ItemTemplate> 
                            </asp:Repeater> 
                            <tr class= "headerstylegrid" style=" font-weight: bold;  height :30px; padding:5px;  " >
                                    <td style="text-align:left;" >Gross Total (US$):</td>
                                    <td style="text-align:right;" ><asp:Label ID="lblActComm_Total" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblProj_Total" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblBdg_Total" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblFcast_Total" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;"><asp:Label ID="lblVar1_Total" runat="server"></asp:Label>%</td>
                                    <td style="width :80px; text-align :right; "><asp:Label ID="lblVar2_Total" runat="server"></asp:Label>%</td>
                                    <td style="width :80px; text-align :right;"><asp:Label ID="lblVar3_Total" runat="server"></asp:Label>%</td>
                                    <td><asp:ImageButton runat="server" ID="img_Comments" ToolTip="Budget Coments" CommandArgument='0' OnClick="Comment_Click"  ImageUrl="~/Modules/HRD/Images/link-icon.png"/></td>
                                    <td></td>
                            </tr>

                        </table> 
                    </asp:Panel>
                    <asp:Panel runat="server" ID="PanelCurrentYP">
                        <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" >                            
                            <tr style="height:20px; padding-top :2px;font-weight:bold;" >
                                <td style="width:260px;">Company </td>
                                <td style="width:260px;">Vessel</td>
                                <td style="width:100px;">Budget Year</td>                                
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="text-align:center;background-color:#F5FAFF;">
                                <td style="vertical-align:middle;">
                                    <asp:DropDownList ID="ddlCompanyCYP" runat="server" OnSelectedIndexChanged="ddlCompanyCYP_OnSelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
                                </td>
                                <td style="vertical-align:middle;">
                                    <asp:DropDownList ID="ddlVesselCYP" runat="server"  OnSelectedIndexChanged="ddlVesselCYP_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                </td>
                                <td style="vertical-align:middle;">
                                    <asp:DropDownList runat="server" ID="ddlMonthCYP" OnSelectedIndexChanged="ddlMonthCYP_OnSelectedIndexChanged" AutoPostBack="true" Width="50px" >
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
                                    <asp:Label ID="lblBudgetYearCYP" runat="server" ></asp:Label>
                                </td>
                                <td style="vertical-align:middle;">
                                   <%--<asp:ImageButton runat="server" ID="btnSaveCYP" ImageUrl="~/Images/Save.jpg"  OnClick="btnSaveCYP_OnClick" /> --%>
                                   
                                   <asp:Button ID="btnExportCYP" runat="server" Text="Publish" OnClick="btnExportCYP_OnClick" CssClass="btn" />
                                   <asp:Button ID="btnPrintCYP" runat="server" Text="Print" onclick="Print_Click" CssClass="btn"/>
                                   </td>
                            </tr>
                        </table>

                        <div style="height:53px; overflow-y:scroll;overflow-x:hidden;">
                        <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:48px;' bordercolor="white">
                        <thead>
                            <tr class= "headerstylegrid">
                                <td style="vertical-align:middle; width:250px;" >Account Head</td>
                                <td style="vertical-align:middle; width:110px;" ><asp:Label runat="server" ID="lblYr1CYP"></asp:Label><br />Act & Comm </td>
                                <td style="vertical-align:middle; width:110px;" ><asp:Label runat="server" ID="lblYrCYP"></asp:Label><br />Budget  </td>
                                <td style="vertical-align:middle; width:140px;" ><asp:Label runat="server" ID="lblYr3CYP"></asp:Label><br />Projected  </td>
                                <td style="vertical-align:middle; width:110px;"><asp:Label runat="server" ID="lblYrCurrCYP"></asp:Label>-Var.%<hr/>B <span style="color : white; font-family: Arial NarrowCYP" >v/s</span> P</td>
                                <td style="vertical-align:middle;" colspan="2" >Comments</td>
                            </tr>
                            </thead>
                            </table>
                            </div>
                            <div style="height:133px;" class='ScrollAutoReset' id='dv_CurrentYearProjection'>
                            <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                            <tbody>
                            <asp:Repeater runat="server" ID="rptCurrentYearProjectionHeades">
                                <ItemTemplate>
                                <tr onmouseover="">
                                    <td style="text-align:left;width:250px;" ><%#Eval("AccountHead")%></td>
                                    <td style="text-align:right;width:110px;" >
                                    <asp:TextBox ID="txtActComm" runat="server" Text='<%#Eval("ActComm")%>' style="text-align:right;background-color:White; border:white;" ReadOnly="true"  MaxLength="10" Width="80px" ></asp:TextBox>
                                    <asp:ImageButton runat="server" ID="imgEdit1" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="Update_ActComm" CommandArgument='<%#Eval("MidcatId")%>'/>                                     
                                    </td>
                                    <td style="text-align:right;width:110px;" ><%#FormatCurrency(Eval("Bud"))%></td>
                                    <td style="text-align:center; width:140px;" >
                                        <asp:TextBox ID="txtProj" runat="server" Text='<%#Eval("Proj")%>' style="text-align:right; background-color:White; border:white;" ReadOnly="true" MaxLength="10" Width="110px" ></asp:TextBox>
                                        <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="Update_Amount" CommandArgument='<%#Eval("MidcatId")%>'/> 
                                        <asp:HiddenField ID="hfMajCatID" runat="server" Value='<%#Eval("MidcatId")%>' />
                                        <asp:HiddenField ID="hfdComm" runat="server" Value='<%#Eval("Comments")%>' />
                                        <asp:HiddenField ID="hfdBud" runat="server" Value='<%#Eval("Bud")%>' />
                                    </td>
                                    <td style="text-align:right;width:110px;"><%#Eval("Var1")%>%</td>
                                    <td colspan="2" style="text-align:left">
                                    <div style="width:90%; height:12px;overflow:hidden; float:left" id='dv<%#Eval("MidcatId")%>'><%#Eval("Comments")%></div>
                                    
                                    <img onclick='<%#"ShowBox(\"dv" + Eval("MidcatId") + "\");" %>' src="../HRD/Images/icon_comment.gif" style='float:right;Display:<%# (Eval("Comments").ToString()=="")?"none":"block"%>'/>
                                </tr>
                                </ItemTemplate> 
                            </asp:Repeater>
                           </tbody>
                            </table> 
                            </div>
                            <div style="height:53px; overflow-y:scroll;overflow-x:hidden;">
                            <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:49px;' bordercolor="white">
                            <tfoot>
                            <tr class= "headerstylegrid" >
                                    <td style="text-align:left;width:250px;" >Total (US$):</td>
                                    <td style="text-align:right;width:110px;" ><asp:Label ID="lblActComm_SumCYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;width:110px;" ><asp:Label ID="lblBudgetCYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;width:140px;" ><asp:Label ID="lblProjectedCYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;width:110px;"><asp:Label ID="lblVar1CYP" runat="server"></asp:Label>%</td>
                                    <td colspan="2" ></td>
                            </tr>
                            <tr class= "headerstylegrid" >
                                    <td style="text-align:left;" >Avg Daily Cost(US$):</td>
                                    <td style="text-align:right;" ><asp:Label ID="lblActComm_Sum1CYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblBudget1CYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;" ><asp:Label ID="lblProjected1CYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;"></td>
                                    <td colspan="2"></td>
                            </tr>
                            </tfoot>
                            </table>
                            </div>
                            <div style="height:120px;" class='ScrollAutoReset' id='dv_CurrentYearProjectionValues'>
                            <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                            <tbody>
                            <asp:Repeater runat="server" ID="rptCurrentYearProjectionValues">
                                <ItemTemplate>
                                <tr onmouseover="">
                                   <td style="text-align:left;width:250px;" ><%#Eval("AccountHead")%></td>
                                    <td style="text-align:right;width:110px;" >
                                        <asp:TextBox ID="txtActComm" runat="server" Text='<%#Eval("ActComm")%>' style="text-align:right;background-color:White; border:white;" ReadOnly="true"  MaxLength="10" Width="80px" ></asp:TextBox>
                                        <asp:ImageButton runat="server" ID="imgEdit1" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="Update_ActComm" CommandArgument='<%#Eval("MidcatId")%>'/>                                     
                                    </td>
                                    <td style="text-align:right;width:110px;" ><%#FormatCurrency(Eval("Bud"))%></td>
                                    <td style="text-align:center; width:140px;" >
                                        <asp:TextBox ID="txtProj" runat="server" Text='<%#Eval("Proj")%>' style="text-align:right;background-color:White; border:white;" ReadOnly="true"  MaxLength="10" Width="110px" ></asp:TextBox>
                                        <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="Update_Amount" CommandArgument='<%#Eval("MidcatId")%>'/> 
                                        <asp:HiddenField ID="hfMajCatID" runat="server" Value='<%#Eval("MidcatId")%>' />
                                        <asp:HiddenField ID="hfdComm" runat="server" Value='<%#Eval("Comments")%>' />
                                        <asp:HiddenField ID="hfdBud" runat="server" Value='<%#Eval("Bud")%>' />
                                    </td>
                                    <td style="text-align:right;width:110px;"><%#Eval("Var1")%>%</td>
                                    <td colspan="2" style="text-align:left">
                                    <div style="width:90%; height:12px;overflow:hidden; float:left" id='dv<%#Eval("MidcatId")%>'><%#Eval("Comments")%></div>
                                    <img onclick='<%#"ShowBox(\"dv" + Eval("MidcatId") + "\");" %>' src="../HRD/Images/icon_comment.gif" style='float:right;Display:<%# (Eval("Comments").ToString()=="")?"none":"block"%>' />
                                </tr>
                                </ItemTemplate> 
                            </asp:Repeater> 
                            </tbody>
                            </table>
                            </div>
                            <div style=" overflow-y:scroll;overflow-x:hidden;">
                            <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; ' bordercolor="white">
                            <tfoot>
                            <tr class= "headerstylegrid" >
                                    <td style="text-align:left;width:250px;" >Gross Total (US$):</td>
                                    <td style="text-align:right;width:110px;" ><asp:Label ID="lblActComm_TotalCYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;width:110px;" ><asp:Label ID="lblBdg_TotalCYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right; width:140px;" ><asp:Label ID="lblProj_TotalCYP" runat="server"></asp:Label> </td>
                                    <td style="text-align:right;width:110px;"><asp:Label ID="lblVar1_TotalCYP" runat="server"></asp:Label>%</td>
                                    <td colspan="2" ></td>
                            </tr>
                            </tfoot>
                        </table> 
                        </div>
                        <asp:Label ID="lblPublishedByOn" runat="server" ></asp:Label>
                    </asp:Panel>
                   
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        </table>
        <center>
        
        <!-- Section to Update Projected Amount -->    
        <div style="position:absolute;top:0px;left:0px; height :425px; width:100%;" runat="server" id="dvUpdateBox" visible="false" >
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
             <div style="position :relative;width:650px; height:300px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
             <div style="float:right"><asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose2_Click"/> </div> 
             <center >
             <div style="border:solid 1px gray;  padding:3px; font-size:13px;" class="text headerband"><b>Update Projected Amount</b></div>
             <div style="border:solid 1px gray; height:240px;">
                 <div>
                    <div style="display:inline; text-align:center; float:left;width:49%;padding:2px;"><b>Old Amount</b></div>
                    <div style="display:inline;text-align:center; float:right;width:49%;padding:2px;"><b>New Amount</b></div>
                 </div>
                 <div>
                    <div style="display:inline; text-align:center; float:left;width:49%;padding:2px;">
                    <asp:TextBox runat="server" ID="txtOldAmt" style="text-align:right" ReadOnly="true"></asp:TextBox> 
                    </div>
                    <div style="display:inline;text-align:center; float:right;width:49%;padding:2px;">
                    <asp:TextBox runat="server" ID="txtNewAmt" style="text-align:right"></asp:TextBox> 
                    </div>
                 </div>
                 <b style="padding:5px;">Enter Comments Below</b>
                 <asp:TextBox runat="server" ID="txtComments" style="width:97%" TextMode="MultiLine" Height="170px"></asp:TextBox>
                 <br/>
             </div> 
             <div style="text-align:left; padding:5px;">
                <asp:Label runat="server" ID="lblPMessage" ForeColor="Red"></asp:Label>  
                <asp:Button runat="server" style="float:right" ValidationGroup="acc1" ID="btnNewCurrency" OnClick="btnSaveProjected_click" CssClass="btn" Text="Save" /> 
             </div>
             </fieldset>
             </center>
             </div> 
        </div> 
        
        <!-- Section to Update Act Comm -->    
        <div style="position:absolute;top:0px;left:0px; height :425px; width:100%;" runat="server" id="dvUpdateActComm" visible="false" >
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
             <div style="position :relative;width:450px; height:110px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
             <div style="float:right"><asp:ImageButton runat="server" ID="btnCloseActComm" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnCloseActComm_Click"/> </div> 
             <center >
             <div style="border:solid 0px gray;  padding:3px; font-size:13px;" class="text headerband"><b>Update Actual Comm. Amount</b></div>
             <div style="border:solid 1px gray; height:45px;">
                 <div>
                    <div style="display:inline; text-align:center; float:left;width:49%;padding:2px;"><b>Old Amount</b></div>
                    <div style="display:inline;text-align:center; float:right;width:49%;padding:2px;"><b>New Amount</b></div>
                 </div>
                 <div>
                    <div style="display:inline; text-align:center; float:left;width:49%;padding:2px;">
                    <asp:TextBox runat="server" ID="txtOld1" style="text-align:right" ReadOnly="true"></asp:TextBox> 
                    </div>
                    <div style="display:inline;text-align:center; float:right;width:49%;padding:2px;">
                    <asp:TextBox runat="server" ID="txtNew1" style="text-align:right"></asp:TextBox> 
                    </div>
                 </div>
                 <%--<b style="padding:5px;">Enter Comments Below</b>--%>
                 <asp:TextBox runat="server" ID="txtComments1" style="width:97%" TextMode="MultiLine" Height="130px" Visible="false"></asp:TextBox>
                 <br/>
             </div> 
             <div style="text-align:left; padding:5px;">
                <asp:Label runat="server" ID="lblPMessage1" ForeColor="Red"></asp:Label>  
                <asp:Button  runat="server" style="float:right" ValidationGroup="acc1" Text="Save" ID="btnSaveActComm" OnClick="btnSaveActComm_click" CssClass="btn"/> 
             </div>
             </fieldset>
             </center>
             </div> 
        </div> 
        </center>
        
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
