<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="eReports_Home" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/HSSQE/mainmene.ascx" tagname="mrvmenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipSoft - eReports</title>
    <script src="../eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../eReports/JS/KPIScript.js" type="text/javascript"></script>

    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="../eReports/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../eReports/CSS/KPIStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../HSSQE/style.css"/>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="menuframe">                
        <uc1:mrvmenu ID="mrvmenu1" runat="server" />                
    </div>
    <div style="border-bottom:solid 5px #4371a5;"></div>
    <%--------------------------------------------------------------------------------------%>
    <div>   
     <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width:40%; border-right:solid 1px #f2f2f2">
                <table border="0" cellpadding="0" cellspacing="0" style="" width="100%">
                    <%--<tr>
                         <td style="text-align:center; font-weight:bold; vertical-align:middle; background-color:#00ABE1; color:White; height:25px;">Inspections List</td>
                    </tr>--%>
                    <tr>
                     <td>
                         <div>
                         <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                         <ContentTemplate>
                         <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;display:none; " >
                         <tr >
                         <td style="text-align:center; padding:5px;" >
                         Search Form : <asp:TextBox runat="server" ID="txtFormNoName" Width="70%" MaxLength="50" OnTextChanged="txtFormNoName_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                         </td>
                         </tr>
                         <tr >
                         <td style="text-align:center; padding:8px;" >
                                    <asp:LinkButton runat="server" ID="LinkButtonA" Text="A" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonB" Text="B" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonC" Text="C" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonD" Text="D" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonE" Text="E" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonF" Text="F" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonG" Text="G" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonH" Text="H" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonI" Text="I" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonJ" Text="J" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonK" Text="K" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonL" Text="L" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonM" Text="M" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonN" Text="N" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonO" Text="O" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonP" Text="P" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonQ" Text="Q" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonR" Text="R" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonS" Text="S" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonT" Text="T" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonU" Text="U" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonV" Text="V" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonW" Text="W" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonX" Text="X" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonY" Text="Y" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonZ" Text="Z" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    </td>
                         </tr>
                         </table>
                         
                         <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;">
                           <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Reports List</td>
                                    <td style="color:White;vertical-align:middle;text-align:center;">Version</td>
                                    <td style="color:White;vertical-align:middle;text-align:center;">Create New</td>
                                    <td></td>
                                </tr>
                                
                                </table>
                           </div>
                         <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 395px ; text-align:center;border:solid 0px red;" class="ScrollAutoReset" id='dv_FRMS'>
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;" class="newformat">
                                <colgroup>
                                    <col />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tbody>
                                <asp:Repeater ID="rptFormList" runat="server">
                                    <ItemTemplate>
                                           <tr runat="server" visible='<%#(lastGroupName != Eval("ReportGroupName").ToString().Trim())%>' >
                                                <td colspan="4" style='background-color:#CCE6FF; text-align:left; padding:4px; font-size:13px;'>&bull; &nbsp;<%#Eval("ReportGroupName")%></td>
                                           </tr>
                                           <tr>
                                                <td style="text-align:left">
                                                    <asp:LinkButton ID="lnkFormNo" runat="server" OnClick="lnkFormNo_OnClick" CommandArgument='<%#Eval("FormNo")%>' Text='<%#(Eval("FormNo").ToString() + "  " + Eval("FormName").ToString())%>' Font-Underline="false"></asp:LinkButton>
                                                    <asp:HiddenField ID="hfdPageExt" runat="server" value='<%#Eval("PageExt")%>'/>
                                                </td>
                                                <td><%#Eval("VersionNo")%></td>
                                                <td>
                                                    <asp:ImageButton ID="btnAddNewReport" runat="server" ImageUrl="~/Modules/PMS/Images/add.png" OnClick="btnAddNewReport_OnClick" Visible='<%# (Eval("FormNo").ToString()=="G113") %>' />
                                                    <a href='<%#".\\" + Eval("FormNo").ToString() + "\\eReport_" + Eval("FormNo").ToString() + ".aspx?Type=E"%>' target="_blank" style='<%# "display:"+((Eval("FormNo").ToString()=="G113" || Eval("PageExt").ToString()!="" )?"none":"block") %>'><img src="../Images/add.png" title="Add New Report" alt="Add New Report" style="border:none;"  /></a>

                                                </td>

                                                <td><span style="display:none"><%# lastGroupName = Eval("ReportGroupName").ToString()%></span></td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                           <tr runat="server" visible='<%#(lastGroupName != Eval("ReportGroupName").ToString().Trim())%>' >
                                                <td colspan="3" style='background-color:#CCE6FF; text-align:left; padding:4px; font-size:13px;'>&bull; &nbsp;<%#Eval("ReportGroupName")%></td>
                                           </tr>
                                           <tr style="background-color:#FFF5E6">
                                                <td style="text-align:left"><asp:LinkButton ID="lnkFormNo" runat="server" OnClick="lnkFormNo_OnClick" CommandArgument='<%#Eval("FormNo")%>' Text='<%#(Eval("FormNo").ToString() + "  " + Eval("FormName").ToString())%>' Font-Underline="false" ></asp:LinkButton>
                                                    <asp:HiddenField ID="hfdPageExt" runat="server" value='<%#Eval("PageExt")%>'/>
                                                </td>
                                                <td><%#Eval("VersionNo")%></td>
                                                <td>
                                                    <%--<asp:ImageButton ID="btnAddNewReport" runat="server" ImageUrl="~/Modules/PMS/Images/add.png" OnClick="btnAddNewReport_OnClick" Visible='<%# (Eval("FormNo").ToString()=="G113") %>' />
                                                    <a href='<%#".\\" + Eval("FormNo").ToString() + "\\eReport_" + Eval("FormNo").ToString() + ".aspx?Type=E"%>' target="_blank"><img src="../Images/add.png" title="Add New Report" alt="Add New Report" style='<%# "display:"+((Eval("FormNo").ToString()=="G113")?"none":"block") %>'  /></a>--%>

                                                    <asp:ImageButton ID="btnAddNewReport" runat="server" ImageUrl="~/Modules/PMS/Images/add.png" OnClick="btnAddNewReport_OnClick" Visible='<%# (Eval("FormNo").ToString()=="G113") %>' />
                                                    <%--<a href='<%#".\\" + Eval("FormNo").ToString() + "\\eReport_" + Eval("FormNo").ToString() + ".aspx?Type=E"%>' target="_blank" style='<%# "display:"+((Eval("FormNo").ToString()=="G113")?"none":"block") %>'><img src="../Images/add.png" title="Add New Report" alt="Add New Report" style="border:none;"  /></a>--%>
                                                    <a href='<%#".\\" + Eval("FormNo").ToString() + "\\eReport_" + Eval("FormNo").ToString() + ".aspx?Type=E"%>' target="_blank" style='<%# "display:"+((Eval("FormNo").ToString()=="G113" || Eval("PageExt").ToString()!="" )?"none":"block") %>'><img src="../Images/add.png" title="Add New Report" alt="Add New Report" style="border:none;"  /></a>

                                                </td>
                                                <td><span style="display:none"><%# lastGroupName = Eval("ReportGroupName").ToString()%></span></td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>                                    
                                </tbody>
                            </table> 
                           </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
            <iframe src="" runat="server" id="frm_Details" width="100%" frameborder="0" scrolling="no" height="485px">
            
            </iframe>


                
            </ContentTemplate>
            </asp:UpdatePanel>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <%--------------------------------------------------------------%>

        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_CrewList" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:900px;  height:380px;padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#FFE6B2; font-size:14px; "><strong>Select Onboard Crew</strong></div>
            <div style="overflow-y:hidden;overflow-X:hidden;border:solid 1px #f2f2f2;" >
               <table border="0" cellpadding="5" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                   <col width="30px" />
                   <col width="80px" />
                   <col width="380px" />
                   <col width="200px" />
                   <col width="110px" />
                   <col  />
                <tr style="background-color:#f2e6e6;color:!important white;">                  
                    <td> <b></b></td>
                    <td> <b>Crew#</b></td>
                    <td> <b>Crew Name</b></td>
                    <td> <b>Rank</b></td>
                    <td style="text-align:left;"> <b>Sign On Dt.</b></td>
                    <td style="text-align:left;"> <b> Relief Due Dt. </b></td>
                </tr>
                </table>
             </div>
                <div style="height:300px;overflow-y:scroll;overflow-X:hidden;border:solid 1px #f2f2f2;" >
                    <asp:HiddenField ID="hfSelectedCrewNumber" runat="server" Value="" />
                    <table border="0"  cellpadding="2" cellspacing="0" rules="rows" style="text-align: center; border-collapse:collapse; width:100%;">
                        <col width="30px" />
                        <col width="80px" />
                       <col width="380px" />
                       <col width="200px" />
                       <col width="110px" />
                       <col  />
                        <asp:Repeater ID="rptCrewList" runat="server">
                    <ItemTemplate>
                    <tr>
                        <td>
                            <input type="radio" name="selectcrew" id="rdocrew" onclick='SetHidden("<%#Eval("CrewNumber") %>")'/>
                        </td>
                       <td> 
                           <%--<a href='G113/ER_G113_Report.aspx?CrewNumber=<%#Eval("CrewNumber") %>' target="_blank" style="text-decoration:none;"> <%#Eval("CrewNumber") %> </a>  --%>
                           <%#Eval("CrewNumber") %>
                       </td>
                       <td><%#Eval("CrewName") %></td>
                       <td><%#Eval("RankName") %></td>
                        <td style="text-align:left;"><%# Common.ToDateString( Eval("SignOnDate")) %></td>
                        <td style="text-align:left;"><%# Common.ToDateString(Eval("ReliefDuedate")) %></td>
                   </tr>    
                        </ItemTemplate>
                </asp:Repeater>
                    </table>
                </div>
                <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style=" text-align:center;padding-top:5px;">
                              <asp:Button ID="btnGoToAppraisal" runat="server" Text="Go for appraisal" Width="120px" OnClick="btnGoToAppraisal_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>
                              <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Close_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>                              
                          </td>
                        </tr>
                      </table>
             </center>
        </div>
    </center>
    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>

        
    </form>

    
</body>
    <script type="text/javascript">
        function SetHidden(CrewNumber) {
            $("#hfSelectedCrewNumber").val(CrewNumber);
        }
        $(document).ready(function () {
            $("#rdocrew").click(function () {
                alert('a');
                //$("#hfSelectedCrewNumber").val($(this).attr("CrewNumber"))
            });
        });
    </script>
</html>
