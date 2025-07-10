<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="BudgetForecastingNextYearNew.aspx.cs" Inherits="BudgetForecastingNextYearNew" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
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
                    <td class="Text headerband" >
                       Next Year Budget
                    </td>
                </tr>
              
                <tr>    
                    <td style="padding:2px">
                    <asp:Panel runat="server" ID="pnlCYPNew">
                        <div style=" margin-top:3px; margin-left:2px;">
                            <asp:Button runat="server" ID="btnUpdateBudget" Text="Update Budget" OnClick="btnUpdateBudget_Click" CssClass="selbtn" style="border:none" />
                            <asp:Button runat="server" ID="btnReports" Text="Reports" OnClick="btnReports_Click" style="border:none" CssClass="btn1"/>
                            <asp:Button runat="server" ID="btnPublish" Text="Publish" OnClick="btnPublish_Click" style="border:none;" CssClass="btn1"/>
                            <div style="background-color:Orange; height:3px;"></div>
                         </div>
                        <iframe runat="server" src="NextYearBudgetForecastEntry.aspx" id="frm1" width='100%' height="490px" frameborder="no"></iframe>
                    </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        </table>
       
        
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
