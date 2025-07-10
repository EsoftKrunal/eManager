<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMSCrewListRestHour.aspx.cs" Inherits="Vims_CMSCrewListRestHour" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
     <style type="text/css" >
    .btnNormal
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;
	    font-weight: bold;
	    background-color:#A3C8EC;
	    height: 25px;	        
	    border:none;	    
	    color:#000000;
    }
    
    .btnSelected
    {
        font-family: Arial, Helvetica, sans-serifl;
	    cursor: pointer;
	    font-size: 12px;	    
	    background-color:#007ACC;
	    color:White;
	    font-weight: bold;
	    height: 25px;	    
	    border:none;	    
    }
    
    .color_tab{
      background: #3498db;
      background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
      background-image: -moz-linear-gradient(top, #3498db, #2980b9);
      background-image: -ms-linear-gradient(top, #3498db, #2980b9);
      background-image: -o-linear-gradient(top, #3498db, #2980b9);
      background-image: linear-gradient(to bottom, #3498db, #2980b9);
      -webkit-border-radius: 0;
      -moz-border-radius: 0;
      border-radius: 0px;
      font-family: Arial;
      color: #ffffff;
      font-size: 12px;
      padding: 5px 9px 5px 9px;
      text-decoration: none;
    }

    .color_tab:hover {
     background: #3cb0fd;
      background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
      background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
      text-decoration: none;
    }

    .color_tab_sel{
      background: #facc8c;
      background-image: -webkit-linear-gradient(top, #f7af51, #facc8c);
      background-image: -moz-linear-gradient(top, #f7af51, #facc8c);
      background-image: -ms-linear-gradient(top, #f7af51, #facc8c);
      background-image: -o-linear-gradient(top, #f7af51, #facc8c);
      background-image: linear-gradient(to bottom, #f7af51, #facc8c);
      font-family: Arial;
      color: black;
      font-size: 12px;
      padding: 5px 9px 5px 9px;
      text-decoration: none;
      border-bottom:solid 1px #facc8c;
    }
</style>
<script type="text/javascript">

    function ShowModal() {
        $("#dvModal").css('display','');
    }
    function HideModel() {
        $("#dvModal").css('display', 'none');
    }
    
</script>
</head>
<body style='font-size:12px;' >
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

   <div style="background-color:#ffffff; text-align:left; position:fixed;top:0px;left:0px; width:100%;" id="dvheader">
        <table border="0" cellpadding="4" cellspacing="0" style="border: #e6dcdc 1px solid; text-align: center" width="100%">
                <col width="100px" />
                <col width="250px" />
                <col width="150px"  />
                <col width="250px" />
                <col width="70px" />
                <col width="120px" />
                <col  />
                <col width="120px" />
                <tr>
                    <td style="text-align:right;"> On Board In : </td>
                    <td style="text-align:left;">


                        <asp:DropDownList ID="ddlYearFilter" runat="server" Width="100px"></asp:DropDownList>
                        <asp:DropDownList ID="ddlMonthFilter" runat="server" Width="100px">
                            <%--<asp:ListItem Value="select" Text=""></asp:ListItem>--%>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtSignOnFrom" runat="server" Width="80px"></asp:TextBox> :
                        <asp:TextBox ID="txtSignOnTo" runat="server" Width="80px"></asp:TextBox>

                        <asp:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtSignOnFrom" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                        <asp:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtSignOnTo"  Format="dd-MMM-yyyy"></asp:CalendarExtender>--%>
                    </td>
                    <%--<td>
                        <asp:RadioButtonList ID="rdoOnBoardOnLeave" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                            <asp:ListItem Value="1" Text="On Board" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Signed Off"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>--%>
                    <td style="text-align:right;">
                        Crew# / Name :
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtCrewNoName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td style="text-align:right;">Rank :</td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlRank" runat="server" Width="140px"></asp:DropDownList>
                    </td>
                    
                    <td style="text-align:right;padding-right:20px;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" />
                    </td>
                    
                </tr>
            </table>
        <div style="overflow-x: hidden; overflow-y: scroll;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color: #00ABE1; border-collapse: collapse;" bordercolor="white">
                <thead>
                    <tr style='color: White; height: 32px; vertical-align:middle;'>
                        <td style="width: 70px; color: White;text-align:center;"><b>Crew#</b></td>
                        <%--<td style="width: 70px; color: White;text-align:center;"><b>RH Report</b></td>--%>
                        <td style="color: White;text-align:left;"><b>Crew Name</b></td>

                        <td style="width: 100px; color: White;"><b>Days Required</b></td>
                        <td style="width: 100px; color: White;"><b>Missing Days </b></td>

                        <td style="width: 180px;text-align:left;color: White;"><b>Rank</b></td>
                        <td style="width: 100px; color: White;"><b>DJC</b></td>
                        <td style="width: 100px; color: White;"><b>Sign On Dt.</b></td>
                        <td style="width: 110px; color: White;"><b>Relief Due Dt.</b></td>
                        <td style="width: 100px; color: White;"><b>Sign Off Dt.</b></td>
                        <td style="width:30px;">&nbsp;</td>
                        <td style="width:30px;">&nbsp;</td>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <div style="height: 355px; left:0px;top:65px;width:100%; position:fixed; border-bottom: none; border: solid 1px #00ABE1; overflow-x: hidden; overflow-y: scroll;" class='ScrollAutoReset' id='dv_FocusCamp_List'>
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color: #F5FCFE; border-collapse: collapse;" class='newformat'>
                <tbody>
                    <asp:Repeater runat="server" ID="rptCrewList">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 70px; text-align: center;">
                                    <%--<a target="_blank" href='RestHourEntry.aspx?CrewNumber=<%#Eval("CrewNumber")%>&CID=<%#Eval("ContractId")%>'><%#Eval("CrewNumber")%></a>--%>
                                    <%#Eval("CrewNumber")%>
                                </td>
                                <%--<td style="width: 70px; text-align: center;">
                                    <asp:ImageButton runat="server" ID="btnreport" ImageUrl="~/Images/printer16x16.png" OnClick="btnreport_Click" CommandArgument='<%#Eval("ContractId").ToString() + "," + Eval("CrewNumber").ToString()%>' />
                                </td>--%>
                                <td style="text-align:left;"><%#Eval("CrewName")%></td>

                                <td style="width: 100px;text-align:center;"><%#Eval("DaysRequired")%></td>
                                <td style='width: 100px;text-align:center;<%# ((Common.CastAsInt32( Eval("DaysRequired"))-Common.CastAsInt32( Eval("LogDays"))>0)?";background-color:#f78c8c;":"" ) %>'>
                                     <%#(Common.CastAsInt32(Eval("DaysRequired"))-Common.CastAsInt32(Eval("LogDays"))).ToString()%>  

                                </td>

                                <td style="width: 180px;text-align:left;"><%#Eval("RankName")%></td>
                                <td style="width: 100px;"><%#Common.ToDateString(Eval("DJC"))%></td>
                                <td style="width: 100px;"><%#Common.ToDateString(Eval("SignOnDate"))%></td>
                                <td style="width: 110px;"><%#Common.ToDateString(Eval("ReliefDuedate"))%></td>
                                <td style="width: 100px;"><%#Common.ToDateString(Eval("SignOffDate"))%></td>    
                                <td style="width:30px;text-align:center;">
                                    <a target="_blank" href='RestHourEntry.aspx?CrewNumber=<%#Eval("CrewNumber")%>&CID=<%#Eval("ContractId")%>' title="R/W Hour Upadate">  <img  src="../Images/AddPencil.gif" style="border:none;"/>  </a>
                                </td>
                                <td style="width:30px;">&nbsp;</td>                            
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
    </div>
    <div style="padding:5px; background-color:#FFFFCC; text-align:left; position:fixed;bottom:0px; width:100%;" id="dvfooter">
               &nbsp;
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
      </div>

        <div id="dvModal" runat="server" visible="false">
            <div style="position:absolute;top:0px; left:0px; width:100%; height:100%; z-index:50; background-color:black; opacity: .5;filter: alpha(opacity=50);" ></div>
            <div style="position : absolute; top:80px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue;z-index:52; ">
            <center>
                <div style="background:white; width:300px; color:#333; ">
                    <div style="padding:5px; background-color:#dbd2d2">Report Type</div>
                    <div style="padding:8px;">
                        <asp:RadioButtonList runat="server" ID="radtype" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Record of Hours" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Non Conformities" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="padding:5px; background-color:#dbd2d2">Report Period</div>
                    <div style="padding:5px;">
                         <asp:DropDownList runat="server" ID="ddlMonth" CssClass="control" Width="80px" ></asp:DropDownList>
                         <asp:DropDownList runat="server" ID="ddlYear" CssClass="control" Width="80px"></asp:DropDownList>
           
                    </div>
                    <br />
                    <asp:Button runat="server" ID="btnprint" Text="Print" OnClick="btnprint_Click" CssClass="btn"/>
                    <asp:Button runat="server" ID="btnclosemodal" Text="Close" OnClick="btnclosemodal_Click"  CssClass="btn"/>
                    <br />
                    <br />
                </div>
            </center>
            </div>
         </div>

       
     <mtm:footer ID="footer1" runat ="server" />
        <div style="display:none; "  id="dvModal">
            <div style="position:absolute;top:0px; left:0px; width:100%; height:100%; z-index:50; background-color:black; opacity: .5;filter: alpha(opacity=50);" ></div>
            <div style="position : absolute; top:170px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue;z-index:52; ">
            <center>
            <div style="border:solid 2px orange; width :200px;background-color :White;opacity: 1.0;filter: alpha(opacity=100); padding:20px; overflow:auto; " >
            <div style='font-size:17px; color:Gray;'>Processing... Please Wait !</div>
            <br />
            <img src="Images/loading.gif" alt="loading">
            <br />
                <input type="button" value="Close" onclick="HideModel();" style=" background-color:orange; color:White; border:none; padding:4px; width:120px" />
            </div>
            </center>
            </div>
        </div>
     </div>
        <script type="text/javascript">
            //var windowheight = 442;
            //$(window).resize(function () {

            //    $("#dv_FocusCamp_List").height(windowheight - $("#dvheader").height() - $("#dvfooter").height());
            //    $("#dv_FocusCamp_List").css("margin-top", $("#dvheader").height());

            //});
            

            //$("#dv_FocusCamp_List").height(windowheight - $("#dvheader").height() - $("#dvfooter").height());
            //$("#dv_FocusCamp_List").css("margin-top", $("#dvheader").height());
        </script>
    </form>
</body>
</html>
