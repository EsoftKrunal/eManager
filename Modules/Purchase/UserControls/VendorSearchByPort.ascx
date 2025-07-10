<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VendorSearchByPort.ascx.cs" Inherits="VendorSearchByPort" %>
<style type="text/css">
            .Dsel
            {
                background-color:#DFEEF7;
            }
            .Sel
            {
                background-color:White;
            }
            .Sel:hover
            {   
                background-color:#DFEEF7
            }
            .Page
            {
                background-color:#DFEEF7; 
                width:20px; 
                text-align:center;
            }
            .Page_Selected
            {
                background-color:#0174BB; 
                width:20px; 
                text-align:center;
                color:White;
            }
            .lnkBtn
            {
                color:Black;
            }
            .lnkBtn_Selected
            {
                color:White;                
            }
    </style>
    <script type="text/javascript">
        var SelRow;
        function MouseOver_Port(Row) {
            SelRow = null;
            var DvMainDiv_Port = '<%= MainDiv_Port.ClientID %>';
            var AllRows = document.getElementById(DvMainDiv_Port).getElementsByTagName('tr');
            for (i = 0; i < AllRows.length; i++) {
                AllRows[i].className = 'Sel';
            }
            SelRow = Row;
        }
        function MouseOut_Port() {
            SelRow = null;
            var DvMainDiv_Port = '<%= MainDiv_Port.ClientID %>';
            var AllRows = document.getElementById(DvMainDiv_Port).getElementsByTagName('tr');
            for (i = 0; i < AllRows.length; i++) {
                AllRows[i].className = 'Sel';
            }
        }
        function SelectText_Port() {
            if (SelRow != null) {
                var txtVendorPortID = '<%= txtVendorPort.ClientID %>';

                var tDesc = SelRow.getElementsByTagName('label');
                document.getElementById(txtVendorPortID).value = tDesc[0].innerHTML;
                document.getElementById(txtVendorPortID).focus();
                document.getElementById('dvDescription_Port').style.display = 'none';
            }
        }
    </script>
    <script type="text/javascript">
        var myVar;
        function SearchDescription_Port() {
            var DvMainDiv_Port = '<%= MainDiv_Port.ClientID %>';
            if (event.keyCode == 38) {
                if (SelRow == null) {
                    var AllRows = document.getElementById(DvMainDiv_Port).getElementsByTagName('tr');
                    if (AllRows != null) {
                        AllRows[AllRows.length - 1].className = 'Dsel';
                    }
                    SelRow = AllRows[AllRows.length - 1];
                    return;
                }
                else if (SelRow.previousSibling != null) {
                    SelRow.className = 'sel';
                    SelRow = SelRow.previousSibling;
                    SelRow.className = 'Dsel';
                    return;
                }
            }
            else if (event.keyCode == 40) {
                if (SelRow == null) {
                    var AllRows = document.getElementById(DvMainDiv_Port).getElementsByTagName('tr');
                    if (AllRows != null) {
                        AllRows[0].className = 'Dsel';
                    }
                    SelRow = AllRows[0];
                    return;
                }
                else if (SelRow.nextSibling != null) {
                    SelRow.className = 'sel';
                    SelRow = SelRow.nextSibling;
                    SelRow.className = 'Dsel';
                    return;
                }

            }
            if (myVar != null)
                clearTimeout(myVar);
            myVar = setTimeout('Search_Port(' + event.keyCode + ')', 300);
        }

        function Search_Port(KeyCode) {
            myVar = null;
            var txtVendorPortID = '<%= txtVendorPort.ClientID %>';
            var DvMainDiv_Port = '<%= MainDiv_Port.ClientID %>';

            if (document.getElementById(txtVendorPortID).value.length < 3) {
                document.getElementById('dvDescription_Port').style.display = 'none';
                return;
            }

            
            if (KeyCode == 13) {
                SelectText_Port();
            }
            else {
                if ((KeyCode >= 49 && KeyCode <= 57) || (KeyCode >= 65 && KeyCode <= 90) || KeyCode <= 8) {
                    document.getElementById('dvDescription_Port').style.display = 'block'
                    var X = document.getElementById('dvDescription_Port').getElementsByTagName('input');
                    SelRow = null;
                    if (X != null)
                        X[0].click();
                }
            }
        }
        function CloseDiv_Port() {
            document.getElementById('dvDescription_Port').style.display = 'none';
        }
    </script>


    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <%--<input type="text" onkeypress="SearchDesc()" style="width:200px; display:none;"   />  --%>
                <asp:TextBox ID="txtVendorPort" runat="server" Width="180px"  onkeyup="SearchDescription_Port()" autocomplete="off" style="float:left;" ondblclick="CloseDiv_Port()"></asp:TextBox>    
            </td>
        </tr>
        <tr>
             <td style="position:relative; text-align:left;">
                <div id="dvDescription_Port" style="width:200px;position:absolute; display:inline; padding:0px;margin:0px; border :none;top:-1px; float:left; display:none;">

        <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div id="MainDiv_Port" runat="server" visible="false" style="border:solid 1px #DFEEF7;max-height:340px; width:200px; border-bottom:none; float:left; background-color:white;"> 
                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : relative; top:50px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
                </asp:UpdateProgress> 

                <table cellpadding="1" cellspacing="0" border="0" style="border-collapse:collapse;width:100%; font-size:12px;" rules="rows" >
                    <asp:Repeater ID="rptDesc" runat="server" >
                    <ItemTemplate>
                        <tr class="Sel" onmouseover="MouseOver_Port(this)" onmouseout="MouseOut_Port()" onclick="SelectText_Port();" >
                            <td style="border-bottom:1px dashed #DFEEF7;text-align:left;" >
                                <label id="lblDesc" > <%#Eval("SupplierPort")%> </label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>                
                </table>

            </div>
            <div id="dvPaging_Port" runat="server"  style="border:solid 1px #DFEEF7;max-height:340px; width:200px; border-top:none;padding-top:10px; background-color:white;" visible="false"> 
                <table cellpadding="0" cellspacing="0" border="0" style="border-collapse:collapse;width:100%; font-size:12px;" >
                <tr>
                <td>
                    <div style="width:100%;" >
                        <table cellpadding="0" cellspacing="1" style="margin:auto;">
                            <tr>
                                <asp:Repeater ID="rptPaging" runat="server">
                                <ItemTemplate>
                                    <td style="" class='<%#(Convert.ToInt32(Eval("PN"))!=PageNo)?"Page":"Page_Selected"%>'>
                                        <asp:LinkButton ID="btnPaging" runat="server" Text='<%#Eval("PN")%>' OnClick="btnPaging_OnClick" class='<%#(Convert.ToInt32(Eval("PN"))!=PageNo)?"Page":"Page_Selected"%>' style="text-decoration:none;"></asp:LinkButton>
                                            
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            </table>
            </div>
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_OnClick" Text="" style="display:none;" />            
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>      
            </td>
        </tr>
    </table>
    