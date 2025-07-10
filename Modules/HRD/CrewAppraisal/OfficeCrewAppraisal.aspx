<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeCrewAppraisal.aspx.cs" Inherits="CrewAppraisal_OficeCrewAppraisal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script type="text/javascript">
    function ShowPeeap_Office(PeapID,NeedUpdate,VesselCode,Location)
    {
        //window.open("ViewAppraisalData.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location + "&NeedUpdate=" + NeedUpdate + "", '');
        window.open("AddPeap.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, '');
    }
    function ShowPeeap(PeapID, NeedUpdate, VesselCode, Location) {
        window.open("AddPeap.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, '');
    }
    function UpdateCrewDetails(PeapID, VesselCode, Location)
    {
        window.open("UpdateCrewDetails.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, "pppp", "resizable=1,toolbar=0,scrollbars=1,width=550,top=70,left=220,height=120");
    }
    function ReloadPage()
    {
        document.getElementById('ctl00$contentPlaceHolder1$btnReload').click();        
    }
    function ShowReport(PeapID, VesselCode, Location)
    {
        window.open("../Reporting/AppraisalReport.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, "Report", "resizable=1,toolbar=0,scrollbars=1,top=70,left=220");
    }
</script>
<script type="text/javascript" >
    function SelectRow(cbid)
    {

         document.getElementById('hfdcbid').value = cbid;
        
        
    }
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                        trSel.setAttribute(CSSName, "selectedrow");
                        lastSel=trSel;
                        document.getElementById('hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('hfPRID').value = prid;
                }
            }
        }
        
       
    </script>
<style type="text/css">
   .selectedrow
   {
   	    background-color:Gray;
   }
    .table {
    width: 100%;margin:10px;
        }

    .table td {
        height: 30px;
        text-align: left;

    }
    input,select
    {
        padding:3px;
        line-height:15px;
    }
</style>
    <link rel="Stylesheet"  href="../Styles/style.css" />
    </head>
    <body style="margin: 0 0 0 0">
    <form id="form1" runat="server">
<table cellpadding="0" cellspacing="2" width="100%" border="0" style="border-collapse:collapse;" >
    <tr>
        <td>
                <table cellpadding="4" cellspacing="0"  width="100%">
                <tr >
                    <td style="text-align:right;">
                        Vessel :
                        <asp:Button ID="btnReload" runat="server" OnClick="btnReload_OnClick" style="display:none;" />
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddl_Vessel"  runat="server" CssClass="input_box" Width="165px" TabIndex="10"><asp:ListItem Text="&lt; Select &gt;"></asp:ListItem></asp:DropDownList>
                    </td>
                    <td style="text-align:right;">
                        Crew Status :
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlPeepLevel" runat="server" CssClass="input_box" Width="100px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="2" Selected="true">On Leave</asp:ListItem>
                            <asp:ListItem Value="3">On Board</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">
                        Occasion :
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlOccation" runat="server"  CssClass="input_box" Width="100px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="101">ROUTINE</asp:ListItem>
                            <asp:ListItem Value="102">ON DEMAND</asp:ListItem>
                            <asp:ListItem Value="103">INTERIM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">
                        Emp# :
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtCrewNo" runat="server" CssClass="input_box" MaxLength="6" Width="50px" ></asp:TextBox>
                    </td>
                    <td>Status :</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" >
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Verified" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnFilter" runat="server" Text="Search" CssClass="btn" Width="100px" OnClick="btnFilter_OnClick" />
                        <asp:Button ID="btnAddPeapPopup" runat="server" Text="+ Add Peap" CssClass="btn" Width="100px" OnClick="btnAddPeapPopup_OnClick" Visible="false"  />
                    </td>
                   
                </tr>
            </table>   
            
        </td>
    </tr>
        <tr>
        <td>
        <div style="height :452px; overflow-x:hidden;overflow-y:scroll;width:100%;">
        <%--OnRowDataBound="grdAppairasal_OnRowDataBound"--%>
        <asp:GridView CellPadding="4" CellSpacing="0" ID="grdAppairasal" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No Data Found" EmptyDataRowStyle-ForeColor="Gray" EmptyDataRowStyle-Font-Bold="true" >
            <Columns>
                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgView" ImageUrl="../Images/HourGlass.gif"  runat="server" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgView_onclick" style="cursor:pointer;"  />                        
                        <asp:HiddenField ID="hfAssMgntID" runat="server" Value='<%#Eval("AssMgntID")%>' />
                        <asp:HiddenField ID="hfVSL" runat="server" Value='<%#Eval("VesselCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Crew#" HeaderStyle-Width="60px">
                    <ItemTemplate>
                            <asp:Label id="lblMarking" runat="server" style="color:Red;" visible="false" >*</asp:Label>
                            <%--<asp:Label id="lblMarkingWhite" runat="server" style="color:White;" visible="false" >*</asp:Label>--%>
                            <%#Eval("CrewNumber")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CrewName" HeaderText="Crew Name" SortExpression="CrewName"><ItemStyle HorizontalAlign="Left"  /></asp:BoundField>
                <asp:BoundField DataField="RankCode" HeaderText="Rank" SortExpression="RankCode"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                <asp:BoundField DataField="VesselName" HeaderText="Vessel" SortExpression="VesselName"><ItemStyle HorizontalAlign="Left" Width="150px" Height="20px" /></asp:BoundField>
                <asp:BoundField DataField="SignOnDate" HeaderText="SignOn Date" SortExpression="SignOnDate"  DataFormatString="{0:dd-MMM-yyyy}"><ItemStyle HorizontalAlign="Center" Width="95px" /></asp:BoundField>
                <asp:BoundField DataField="SignOffDate" HeaderText="SignOff Date" SortExpression="SignedOnDt"  DataFormatString="{0:dd-MMM-yyyy}"><ItemStyle HorizontalAlign="Center" Width="90px"/></asp:BoundField>
		<asp:BoundField DataField="crewstatusname" HeaderText="Current Status" SortExpression="PeapType"><ItemStyle HorizontalAlign="Left" Width="100px" Height="20px" /></asp:BoundField>

		<%--<asp:BoundField DataField="PeapType" HeaderText="Peap Level" SortExpression="PeapType"><ItemStyle HorizontalAlign="Left" Width="100px" Height="20px" /></asp:BoundField>
                <asp:BoundField DataField="AppraisalFromDate" HeaderText="App From Date" SortExpression="LastVessel"><ItemStyle HorizontalAlign="Center" Width="95px" /></asp:BoundField>
                <asp:BoundField DataField="AppraisalToDate" HeaderText="App To Date" SortExpression="AppraisalToDate"><ItemStyle HorizontalAlign="Center" Width="90px"/></asp:BoundField>
                <asp:BoundField DataField="AppraisalRecievedDate" HeaderText="Date Recd." ><ItemStyle HorizontalAlign="Left" Width="90px"/></asp:BoundField>--%>
                <asp:BoundField DataField="StatusText" HeaderText="Status" ><ItemStyle HorizontalAlign="Left" Width="60px"/></asp:BoundField>
            </Columns>
            <SelectedRowStyle CssClass="selectedtowstyle" />
            <PagerStyle CssClass="pagerstyle" />
            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
            <RowStyle CssClass="rowstyle" />
        </asp:GridView>                        
        </div>
        </td>
    </tr>
    <tr>
        <td  style="background-color:#e5e5e5;padding-top:7px;padding-bottom:7px;padding-left:5px;">
            <asp:Label ID="lblNoOfrec" runat="server"  style="font-size:12px; font-weight:bold; margin:3px;float:left;"></asp:Label>
            <asp:Label ID="lblMessage" runat="server"  style="font-size:12px; color:Red; font-weight:bold; margin:3px;float:right;"></asp:Label>
        </td>
    </tr>
</table>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAdd" visible="false" >
<center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position :relative; width:80%;text-align :center; border :solid 10px #333;padding-bottom:5px; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
    <center>
        <div>
            <div style="overflow-y:scroll;overflow-x:hidden;height:30px; border:solid 0px gray;" >
<table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:13px;  height:30px; background-color:#FFE6B2'>
<colgroup>
    <col width='60px' />
    <col width='60px' />
    <col width='90px' />
     <col />
    <col width='100px' />
    <col width='100px' />
    <col width='20px' />
</colgroup>
<tr class= "headerstylegrid">
    <td>Select</td>
    <td>Crew #</td>
    <td>Vessel </td>
    <td>Crew Name</td>
    <td>Rank</td>
    <td></td>
</tr>
</table>
</div>
<div style="overflow-y:scroll;overflow-x:hidden;height:350px; border:solid 1px gray;" >
    <asp:TextBox runat="server" ID="hfdcbid"></asp:TextBox>
    <asp:Button ID="btnSelectRow" OnClick="btnSelectRow_Click" runat="server"/>

<table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
<colgroup>
    <col width='60px' />
    <col width='60px' />
    <col width='90px' />
    <col />
    <col width='100px' />
    <col width='100px' />
    <col width='20px' />
</colgroup>
<asp:Repeater runat="server" ID="rprData1">
<ItemTemplate>
<tr>
    <td style="text-align:center"><input type="radio" name="radselect" onclick='<%#"SelectRow(" + Eval("CREWBONUSID").ToString() + ");"%>'/></td>
    <td style="text-align:center"><%#Eval("CREWNUMBER")%></td>
    <td style="text-align:center"><%#Eval("CREWNUMBER")%></td>
    <td style="text-align:left"><%#Eval("VESSELNAME")%></td>
    <td style="text-align:left"><%#Eval("CREWNAME")%></td>
    <td style="text-align:left"><%#Eval("RANKCODE")%></td>
    <td style="text-align:center"><%#Common.ToDateString(Eval("SignOnDate"))%></td>
    <td></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
        </div>
        <asp:Button runat="server" ID="btnclose" Text="Close" OnClick="btnclose_Click" />
    </center>
</div>
</center>
</div>
</form>
        </body>
  


