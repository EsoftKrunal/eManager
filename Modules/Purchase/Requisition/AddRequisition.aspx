<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRequisition.aspx.cs" ValidateRequest="false" EnableEventValidation="true" Inherits="AddSpareRequisition"  MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/AddSparePR.ascx" TagName="Spare" TagPrefix="uc1"  %>
<%@ Register Src="~/Modules/Purchase/UserControls/AddStorePR.ascx" TagName="Store" TagPrefix="uc3" %>
<%@ Register Src="~/Modules/Purchase/UserControls/AddLandedGoods.ascx" TagName="LND" TagPrefix="uc4" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<html xmlns="http://www.w3.org/1999/xhtml" >--%>
<%--<head runat="server">--%>
<title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" >

            function fncInputNumericValuesOnly(evnt) {
                
                if (!(event.keyCode == 13 || event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                    event.returnValue = false;

                }

            }

        </script>
    
	    <script language="javascript" type="text/javascript">
	        //function for file name
	        function FileName(Objsku) {


	            if (IsFileChar(Objsku.value) == false) {
	                alert('Invalid char for file name ');
	                var objId = Objsku.id;
	                var vals = document.getElementById(objId).value;
	                //alert(vals.length);
	                vals = vals.substring(0, vals.length - 1);
	                document.getElementById(objId).value = vals;
	                document.getElementById(objId).focus();
	                return false;
	            }

	        }

	        function IsFileChar(sText) {
	            //alert('file char');
	            var ValidChars = "1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM_-.";
	            var IsNumber = true;
	            var Char;


	            for (i = 0; i < sText.length && IsNumber == true; i++) {
	                Char = sText.charAt(i);

	                if (ValidChars.indexOf(Char) == -1) {

	                    IsNumber = false;
	                }
	            }
	            return IsNumber;

	        }
	        //end file name function
	        function numVal(Objsku) {
	            if (IsNumeric(Objsku.value) == false) {
	                var objId = Objsku.id;
	                var vals = document.getElementById(objId).value;
	                //alert(vals.length);
	                vals = vals.substring(0, vals.length - 1);
	                document.getElementById(objId).value = vals;
	                document.getElementById(objId).focus();
	                return false;
	            }

    	        
    	       
	        }

	        function IsNumeric(sText) {
	            var ValidChars = "0123456789.";
	            var IsNumber = true;
	            var Char;


	            for (i = 0; i < sText.length && IsNumber == true; i++) {
	                Char = sText.charAt(i);
	                if (ValidChars.indexOf(Char) == -1) {
	                    IsNumber = false;
	                }
	            }
	            return IsNumber;

	        }

	        function do_totals1() {

	            document.all.pleasewaitScreen.style.pixelTop = (document.body.scrollTop + 50);

	            document.all.pleasewaitScreen.style.visibility = "visible";

	            window.setTimeout('do_totals2()', 1);

	        }



	        function do_totals2() {

	            lengthy_calculation();

	            document.all.pleasewaitScreen.style.visibility = "hidden";

	        }



	        function lengthy_calculation() {

	            var x, y



	            for (x = 0; x < 1000000; x++) {

	                y += (x * y) / (y - x);

	            }

	        }

	        var currentPosition = 0;
	        var newPosition = 0;
	        var direction = "Released";
	        var currentHeight = 0;
	        var offX = 15;          // X offset from mouse position
	        var offY = 15;          // Y offset from mouse position
	        var divHeight;
	        var ie5 = document.all && document.getElementById
	        var ns6 = document.getElementById && !document.all
	        function updatebox1(mouse, SKU) {

	            //			var DivName='pop1'+SKU;
	            var DivName = SKU;
	            var divObj = document.getElementById(DivName)
	            direction = "Pressed";
	            //alert(DivName)
	            currentPosition = mouse.clientY;
	            document.getElementById(DivName).style.zIndex = 1;
	            document.getElementById(DivName).style.display = "block";
	            document.getElementById(DivName).style.backgroundColor = "#ffffff";
	            document.getElementById(DivName).style.position = "absolute";


	        }

	        function SetDivPosition1(mouse, SKU) {


	            var DivName = SKU;

	            var divObj = document.getElementById(DivName);
	            document.getElementById(DivName).style.display = "block";
	            document.getElementById(DivName).style.backgroundColor = "#ffffff";
	            document.getElementById(DivName).style.position = "absolute";

	            document.getElementById(DivName).style.zIndex = 1;
	            if ((parseInt(mouseX(mouse)) + offX) + 420 > window.screen.width)
	                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX) - 420) + 'px';
	            else
	                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX)) + 'px';
	            document.getElementById(DivName).style.top = ((parseInt(mouseY(mouse)) + offY) - 200) + 'px';



	        }


	        function HideBox1(evt, SKU) {

	            var DivName = SKU;

	            document.getElementById(DivName).style.display = "none";
	        }
	        //           -----------------------------------------------------------------
	        function updatebox11(mouse, SKU) {

	            //var DivName='pop11'+SKU;
	            var DivName = SKU;

	            var divObj = document.getElementById(DivName)
	            direction = "Pressed";
	            //alert(DivName)
	            currentPosition = mouse.clientY;
	            document.getElementById(DivName).style.zIndex = 1;
	            document.getElementById(DivName).style.display = "block";
	            document.getElementById(DivName).style.backgroundColor = "#ffffff";
	            document.getElementById(DivName).style.position = "absolute";


	        }

	        function SetDivPosition11(mouse, SKU) {


	            //			var DivName='pop11'+SKU;
	            var DivName = SKU;
	            var divObj = document.getElementById(DivName);
	            document.getElementById(DivName).style.display = "block";
	            document.getElementById(DivName).style.backgroundColor = "#ffffff";
	            document.getElementById(DivName).style.position = "absolute";

	            document.getElementById(DivName).style.zIndex = 1;
	            if ((parseInt(mouseX(mouse)) + offX) + 420 > window.screen.width)
	                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX) - 420) + 'px';
	            else
	                document.getElementById(DivName).style.left = ((parseInt(mouseX(mouse)) + offX)) + 'px';
	            document.getElementById(DivName).style.top = ((parseInt(mouseY(mouse)) + offY) - 200) + 'px';



	        }


	        function HideBox11(evt, SKU) {

	            //			var DivName='pop11'+SKU;
	            var DivName = SKU;
	            document.getElementById(DivName).style.display = "none";
	        }


	        function mouseX(evt) { if (!evt) evt = window.event; if (evt.pageX) return evt.pageX; else if (evt.clientX) return evt.clientX + (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft); else return 0; }
	        function mouseY(evt) { if (!evt) evt = window.event; if (evt.pageY) return evt.pageY; else if (evt.clientY) return evt.clientY + (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop); else return 0; }
    </script>
	    <script type="text/javascript" >
	        function ShowHideAddedDesc(TxtObj) 
	        {

                    //document.getElementById ('d').parentNode.nextSibling.childNodes
	            var txtDesc = TxtObj.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.nextSibling.childNodes[1].childNodes[0]//.nextSibling;//.childNodes[1].childNodes;
	            txtDesc.style.display = (txtDesc.style.display == "") ? "none" : "";
	        }
	        function AddRows() 
	        {
	            if (window.event.keyCode == 13) 
	            {
                    document.getElementById('ctl00_ContentMainMaster_ucSpare_btnaddnew').focus();
                    document.getElementById('ctl00_ContentMainMaster_ucSpare_btnaddnew').click();
	            }
	        }
	        function DeleteSpareRows(obj) 
	        {
	            if (window.event.keyCode == 13) 
	            {
	                document.getElementById(obj.id).focus();
	                document.getElementById(obj.id).click();
	            }
	        }
	        function AddStoreRows() 
	        {
	            if (window.event.keyCode == 13) 
	            {
                    document.getElementById('ctl00_ContentMainMaster_ucStore_btnaddnew').focus();
                    document.getElementById('ctl00_ContentMainMaster_ucStore_btnaddnew').click();
	            }
	        }
	        function DeleteStoreRows(obj) 
	        {
	            if (window.event.keyCode == 13) 
	            {
	               document.getElementById(obj.id).focus();
	               document.getElementById(obj.id).click();
	            }
	        }
	        //for Landed Goods
	        function AddLNDRows() 
	        {
	            if (window.event.keyCode == 13) 
	            {
                    document.getElementById('ctl00_ContentMainMaster_UcLND_btnaddnew').focus();
                    document.getElementById('ctl00_ContentMainMaster_UcLND_btnaddnew').click();
	            }
	        }
	        function DeleteLNDRows(obj) {
	            if (window.event.keyCode == 13) {
	                document.getElementById(obj.id).focus();
	                document.getElementById(obj.id).click();
	            }
	        }
        </script>
	    <script type="text/javascript" language="javascript">
            function doSearch(url,control)
            {
                tb_show("Add New Item", url+'?KeepThis=true&id='+control+'&TB_iframe=true&height=250&width=900');
            }
//            
        </script>
      
<%--</head>--%>
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
<%--<body>
    <form id="form1" runat="server" defaultbutton="free">--%>    
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
            <asp:Button runat="server" ID="free" style="display :none" OnClientClick="return false;" /> 
            <div style="border:1px solid #4371a5;font-family:Arial;font-size:12px;" >
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr><td class="text headerband" style=" padding:4px;" >Requisition 
                    <asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Modules/HRD/Images/home.png" style="float :right; padding-right :5px; background-color:Transparent" />  
                    <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
                    </td></tr>
                    <tr>
                    <td>
                      <table cellpadding="0" cellspacing="0" width="100%" border="0" bgcolor="white">
                        <tr>
                        <td style="border-bottom: solid 1px #4371a5;">
                            <table cellpadding="0" cellspacing="0" style="width: 100%" border="0" >
                                <tr>
                                    <td style="text-align: left; width:350px; padding:1px">
                                        <%--<uc5:AddRequisitionTypes ID="AddRequisitionTypes1" runat="server" />--%>
                                        <asp:RadioButtonList id="rdPRTYpe" runat="server" AutoPostBack="true"  onselectedindexchanged="rdPRTYpe_onselectedindexchanged" RepeatDirection="Horizontal" >
                                            <asp:ListItem Selected="True">Stores & Services</asp:ListItem>
                                            <asp:ListItem>Spare</asp:ListItem>
                                            <asp:ListItem>Landed Goods</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:button ID="imgEdit" runat="server" Text="Edit" OnClick="imgEdit_OnClick" Visible="false" CssClass="btn" /> &nbsp;
                                        <asp:button ID="ImgCreateRFQ" runat="server" Text="Make Inquiry" OnClick="ImgCreateRFQ_OnClick" CssClass="btn" /> &nbsp;
                                        <asp:button ID="imgPrint" runat="server" Text="Print" CssClass="btn" /> &nbsp;
                                        <asp:button ID="btnStatusNote" runat="server" Text="Update Status Notes" OnClick="btnStatusNote_OnClick"  CssClass="btn" Visible="false" /> &nbsp;
										&nbsp;&nbsp;
                                   
                                    </td>
                                     <td style="text-align:left;text-align:center; " >
                                       <asp:Label runat="server" ID="lblORNo" Font-Bold="true" style="text-align:center;font-size:17px;"></asp:Label>
                                     </td>
                                     
                                     <td style="width:185px; text-align :right; padding:5px;" >
                                        <asp:DropDownList ID="ddlCopyVessel" runat="server" Width="180px" ></asp:DropDownList>
                                        </td>
                                     <td style="width:150px; text-align:left;padding-left:3px;">
                                          <asp:Button ID="imgCopyVessel" runat="server" Text="Copy Requisition" OnClick="imgCopyVessel_OnClick" CssClass="btn" />
                                     </td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                        <tr>
                        <td valign="top">
                            <uc1:Spare ID="ucSpare" runat="server"  Visible="false" />
                            <uc3:Store ID="ucStore" runat="server" Visible="false" />
                            <uc4:LND ID="UcLND" runat="server" Visible="false" />
                        </td>
                        </tr>
                          <tr>
                              <td style="text-align:center"> <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label></td>
                          </tr>
                        
                    </table>
                    </td>
                    </tr>
                    </table>
				   
            </div> 
		<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="dvOfficeRemarks" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
        <br />
        <div class="text headerband"> <b>Update Status Notes..</b> </div>
        <br /><br />
        <asp:TextBox runat="server" CssClass="input_box" ID="tctOfficeRemarks" Width="400px" TextMode="MultiLine" Rows="7"></asp:TextBox> 
        <div style="padding:5px">
        Updated By : <asp:Label runat="server" ID="lblupdatedby"></asp:Label>
        </div>
        <div style="padding:5px">
        Updated On : <asp:Label runat="server" ID="lblupdatedon"></asp:Label>
        </div>
        <br />
        <asp:Label runat="server" ForeColor ="Red" ID="Label1" ></asp:Label>  
        <br />
        <asp:Button ID="btnOfficeRemSave" runat="server" CssClass="btn" onclick="btnOfficeRemSave_Click" Text="Submit" Width="100px" />
        <asp:Button ID="btnOfficeRemCancel" runat="server" CssClass="btn" onclick="btnOfficeRemCancel_Click" Text="Cancel" CausesValidation="false" Width="100px" />
         </center>
        </div> 
    </center>
    </div>
	    
    <%--</form>
</body>
</html>--%>
</asp:Content>

    

