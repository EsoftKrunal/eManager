<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewAppraisal.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="CrewAppraisal" Title="Crew Appraisal"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <script type="text/javascript">
        function ShowPeeap_Office(PeapID, NeedUpdate, VesselCode, Location) {
            //window.open("ViewAppraisalData.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location + "&NeedUpdate=" + NeedUpdate + "", '');
            //window.open("ViewPeap.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, '');
            window.open('AddPeap.aspx?PeapID=' + PeapID + '&VesselCode=' + VesselCode + '&Location=' + Location, '');
        }
        function ShowPeeap(PeapID, NeedUpdate, VesselCode, Location) {
            //window.open("ViewAppraisalData.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location + "&NeedUpdate=" + NeedUpdate + "", '');
            window.open("ViewPeap.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, '');
        }

        function ReloadPage() {
            document.getElementById('ctl00$contentPlaceHolder1$btnReload').click();
        }
        function ShowReport(PeapID, VesselCode, Location) {
            window.open("../Reporting/AppraisalReport.aspx?PeapID=" + PeapID + "&VesselCode=" + VesselCode + "&Location=" + Location, "Report", "resizable=1,toolbar=0,scrollbars=1,top=70,left=220");
        }
    </script>
    <script type="text/javascript" >
        var lastSel = null;
        function Selectrow(trSel, prid) {
            if (lastSel == null) {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel = trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else {
                if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
                {
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel = trSel;
                    document.getElementById('hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel = trSel;
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
    /*.tab{
        background-color:Gray;
        color:white;
        border:none;
        padding:5px;
        min-width:80px;
        font-weight:bold;
    }
    .activetab{
         background-color:#4681c1;
        color:white;
         border:none;
        padding:5px;
        min-width:80px;
        font-weight:bold;
    }*/
    .selbtn {
             background-color: #669900;
            color: White;
            border: none;
            padding:5px 10px 5px 10px;
             min-width:80px;
        }

        .btn1 {
             background-color: #c2c2c2;
            border: solid 1px gray;
            border: none;
            padding:5px 10px 5px 10px;
            min-width:80px;
        }
</style>
    <%--<link rel="Stylesheet"  href="../Styles/style.css" />--%>
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td  class="text headerband" >
            <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
            Appraisal</td>
            </tr>
         </table>
        <div style="border-bottom:solid 5px #4681c1; padding:5px 5px 0px 5px; text-align:left">
            <asp:Button runat="server" ID="btnOffice" Text="In Office (Master/CE)" CssClass="selbtn" OnClick="btnOffice_Click" />
            <asp:Button runat="server" ID="btnShip" Text="Onboard" CssClass="btn1" OnClick="btnShip_Click" />
            <asp:Button runat="server" ID="btnCrewAssessment" Text="Crew Assessment" CssClass="btn1" OnClick="btnCrewAssessment_Click"  />
        </div>
    
    <table cellpadding="0" cellspacing="2" width="100%" border="0" style="border-collapse:collapse;" id="tblmain" runat="server" >
    <tr>
        <td>
            <table cellpadding="4" cellspacing="0"  width="100%" >
                <tr >
                    <td style="text-align:right;">
                        Vessel :
                        <asp:Button ID="btnReload" runat="server" OnClick="btnReload_OnClick" style="display:none;" />
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddl_Vessel"  runat="server" CssClass="input_box" Width="165px" TabIndex="10"><asp:ListItem Text="&lt; Select &gt;"></asp:ListItem></asp:DropDownList>
                    </td>
                    <td style="text-align:right;">Peap Level :</td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlPeepLevel" runat="server" CssClass="input_box" Width="100px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Management</asp:ListItem>
                            <asp:ListItem Value="2">Support</asp:ListItem>
                            <asp:ListItem Value="3">Operation</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">Occasion :</td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlOccation" runat="server"  CssClass="input_box" Width="100px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="101">ROUTINE</asp:ListItem>
                            <asp:ListItem Value="102">ON DEMAND</asp:ListItem>
                            <asp:ListItem Value="103">INTERIM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">Emp# :</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtCrewNo" runat="server" CssClass="input_box" MaxLength="6" Width="50px" ></asp:TextBox>
                    </td>
                    <td>Status :</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" >
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Closed/Verified" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnFilter" runat="server" Text="Search" CssClass="btn" Width="100px" OnClick="btnFilter_OnClick" />
                        <%--<asp:Button ID="btnAddPeapPopup" runat="server" Text="Add Peap" CssClass="btn" Width="100px" OnClick="btnAddPeapPopup_OnClick" Visible="false" />--%>
                    </td>
                   
                </tr>
            </table>               
        </td>
    </tr>
     <tr>
        <td  style="background-color:#e4e4e3;padding-top:7px;padding-bottom:7px;padding-left:5px;">
            <asp:Label ID="lblNoOfrec" runat="server" style="font-size:12px; font-weight:bold; margin:3px;float:right;" Visible="false"></asp:Label>
            <asp:LinkButton ID="lblMessage" runat="server" style="font-size:12px; color:Red; font-weight:bold; margin:3px;float:left;" Font-Underline="false" OnClick="lblMessage_OnClick"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
        <div style="height :452px; overflow-x:hidden;overflow-y:scroll;width:100%;">
        <asp:GridView CellPadding="4" CellSpacing="0" ID="grdAppairasal" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No Data Found" EmptyDataRowStyle-ForeColor="Gray" EmptyDataRowStyle-Font-Bold="true" >
            <Columns>
                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        
                        <%--<asp:ImageButton ID="imgView" ImageUrl="../Images/HourGlass.gif" runat="server" OnClick="imgView_onclick" style="cursor:pointer;"  />--%>

                        <asp:ImageButton ID="imgView" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"  runat="server" OnClick="imgView_onclick" style="cursor:pointer;"  />
                        
                        <asp:HiddenField ID="hfAssMgntID" runat="server" Value='<%#Eval("AssMgntID") %>' />
                        <asp:HiddenField ID="hfVesselCode" runat="server" Value='<%#Eval("VesselCode") %>' />
                        <asp:HiddenField ID="hfCrewNo" runat="server" Value='<%#Eval("CrewNo") %>' />
                        <asp:HiddenField ID="hfFname" runat="server" Value='<%#Eval("AssName") %>' />
                        <asp:HiddenField ID="hfLname" runat="server" Value='<%#Eval("AssLname") %>' />
                        <asp:HiddenField ID="hfLocation" runat="server" Value='<%#Eval("Location") %>' />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("AssLname") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Crew#" HeaderStyle-Width="60px">
                    <ItemTemplate>
                            <asp:Label id="lblMarking" runat="server" style="color:Red;" visible="false" >*</asp:Label>
                            <%--<asp:Label id="lblMarkingWhite" runat="server" style="color:White;" visible="false" >*</asp:Label>--%>
                            <%#Eval("CrewNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AssNameMod" HeaderText="Name" SortExpression="RankLevel"><ItemStyle HorizontalAlign="Left"  /></asp:BoundField>
                <asp:BoundField DataField="ShipSoftRank" HeaderText="Rank" SortExpression="CountryName"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                <asp:BoundField DataField="Occasion" HeaderText="Occasion" SortExpression="FullName"><ItemStyle HorizontalAlign="Left" Width="110px" Height="20px" /></asp:BoundField>
                <asp:BoundField DataField="VesselCode" HeaderText="Vsl" SortExpression="FullName"><ItemStyle HorizontalAlign="Center" Width="50px" Height="20px" /></asp:BoundField>
                <asp:BoundField DataField="PeapType" HeaderText="Peap Level" SortExpression="FullName"><ItemStyle HorizontalAlign="Left" Width="100px" Height="20px" /></asp:BoundField>
                <%--<asp:BoundField DataField="CrewNo" HeaderText="Crew#" SortExpression="FullName"><ItemStyle HorizontalAlign="Left" Width="100px" Height="20px" /></asp:BoundField>--%>
                
                <asp:BoundField DataField="AppraisalFromDate" HeaderText="App From Date" SortExpression="LastVessel"><ItemStyle HorizontalAlign="Center" Width="95px" /></asp:BoundField>
                <asp:BoundField DataField="AppraisalToDate" HeaderText="App To Date" SortExpression="SignedOnDt"><ItemStyle HorizontalAlign="Center" Width="90px"/></asp:BoundField>
                <asp:BoundField DataField="AppraisalRecievedDate" HeaderText="Date Recd." ><ItemStyle HorizontalAlign="Left" Width="90px"/></asp:BoundField>
                
                <%--<asp:BoundField DataField="StatusText" HeaderText="Status" ><ItemStyle HorizontalAlign="Left" Width="60px" /></asp:BoundField>--%>
                <%--<asp:BoundField DataField="DatejoinedVessel" HeaderText="Dt Joined Vessel" SortExpression="ReliefDueDate"><ItemStyle HorizontalAlign="Center" Width="110px" /></asp:BoundField>--%>
            </Columns>
            <SelectedRowStyle CssClass="selectedtowstyle" />
            <PagerStyle CssClass="pagerstyle" />
            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
            <RowStyle CssClass="rowstyle" />
        </asp:GridView>                        
        </div>
        </td>
    </tr>
   
</table>
        <div style="position:absolute;top:25px;left:0px;  width:100%;" id="dvFrame" runat="server" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:80%; padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 2px #4681c1;">
          <div style="padding:0px; text-align:left; height:40px;line-height:40px; padding-left:10px;" class="text headerband">
              <asp:ImageButton runat="server" ID="btnClose" OnClick="btnClose_Click" ImageUrl="~/Modules/HRD/Images/closewindow.png" style="float:right; width:32px;" />
              Crew has approval pending in office.
          </div>  
<div style="overflow-y:scroll;overflow-x:hidden;height:30px; border:solid 1px gray;" >
<table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:13px;  height:30px; background-color:#FFE6B2'>
<colgroup>
    <col width='25px' />
    <col width='80px' />
    <col />
    <col width='150px' />    
    <col width='150px' />
    <col width='100px' />
    <col width='100px' />
</colgroup>
<tr class= "headerstylegrid">
    <td><img src="../Images/forward.png" alt="" /></td>
    <td>Crew #</td>
    <td>Crew Name</td>
    <td>Rank</td>
    <td>Vessel Name</td>    
    <td>SignOn Dt.</td>
    <td>Rel Due Dt.</td>
</tr>
</table>
</div>
<div style="overflow-y:scroll;overflow-x:hidden;height:395px; border:solid 1px gray;" >
<table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
<colgroup>
    <col width='25px' />
    <col width='80px' />   
    <col />
     <col width='150px' />
    <col width='150px' />
    <col width='100px' />
    <col width='100px' />
</colgroup>
<asp:Repeater runat="server" ID="rprData1">
<ItemTemplate>
<tr>
     <td style="text-align:center;">
         <asp:ImageButton ID="imgAddPeap" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClick="imgAddPeap_OnClick" ToolTip="Forward for Appraisal !" Visible='<%#(Eval("Mode").ToString()=="Bonus")%>' CommandArgument='<%#Eval("CREWBONUSID")%>'  />
         <asp:ImageButton ID="imgAddPeap1" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClick="imgAddPeap1_OnClick" ToolTip="Forward for Appraisal !" Visible='<%#(Eval("Mode").ToString()=="PreSignOff")%>' CommandArgument='<%#Eval("CREWID")%>'  />
    </td>
    <td style="text-align:center"><%#Eval("CREWNUMBER")%></td>
    <td style="text-align:left"><%#Eval("CREWNAME")%></td>
    <td style="text-align:left"><%#Eval("RANKCODE")%></td>
    <td style="text-align:left"><%#Eval("VESSELNAME")%></td>
    
    <td style="text-align:center"><%#Common.ToDateString(Eval("SignOnDate"))%></td>
    <td style="text-align:center"><%#Common.ToDateString(Eval("SignOffDate"))%></td>
   
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
        </div>
            
        </center>
        </div>
     <div style="text-align: center" id="dvfrm" runat="server" visible="false">
         <div>
            <iframe runat="server" id="frm" width="100%" height="556px" scrolling="no" frameborder="1"></iframe>    
            </div>
         </div>
     
    </asp:Content>


