<%@ Page Title="New Vessel Planning" Language="C#"  AutoEventWireup="true" CodeFile="VesselPlanning.aspx.cs" Inherits="CrewOperation_VesselPlanning"  %>

<body>
 <form id="frm" runat="server">  
<div>
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script type="text/javascript">
    function ShowRequirement(crewid, rankid) {
        var vslClientId = '<%=ddl_VesselName.ClientID %>';
        var ind = document.getElementById(vslClientId).selectedIndex;
        var vesselid = 0;
        if (parseFloat(ind) > 0) {
            vesselid = document.getElementById(vslClientId).options[ind].value;
        }
        window.open('Crew_Required_Docs.aspx?crewid=' + crewid + '&vesselid=' + vesselid + '&rankid=' + rankid, '', '');
    }
</script>
     <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
        <ProgressTemplate>
            <div style="background-color: Black; opacity: 0.4; filter: alpha(opacity=40); width: 100%;
                z-index: 50; min-height: 100%; position: absolute; top: 0px; left: 0px;">
            </div>
            <div style="position: absolute; top: 300px; left: 0px; width: 100%; z-index: 100;
                text-align: center; color: Blue;">
                <center>
                    <div style="border: solid 2px blue; height: 50px; width: 120px; background-color: White;">
                        <img src="../../HRD/Images/loading.gif" alt="loading">
                        Loading ...
                    </div>
                </center>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up1" runat="server">
             <ContentTemplate>
    <table cellspacing="0" width="100%" style=" background-color:#f9f9f9;font-family:Arial;font-size:12px;" >
        <tr>
        <td style="text-align: center">
            <asp:Label ID="lb_msg" runat="server" ForeColor="Red" ></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align:center">
            <table border="0" cellpadding="0" cellspacing="0" style="text-align:center" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="table table-condensed table-bordered table-responsive" style=" font-family:Arial;font-size:12px;">
                         <tr>
            <td class="col-xs-1">Vessel: </td>
            <td class="col-xs-2">
                 <asp:DropDownList ID="ddl_VesselName" Width="248px" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselName_SelectedIndexChanged"> </asp:DropDownList>
            </td>
            <td class="text-left">
                <asp:Button ID="btnAddCrew" OnClick="btnAddCrew_Click" Text="Add Crew" CssClass="btn" style=" padding:1px; font-size:12px; width:100px; "  runat="server" /> &nbsp;
                <asp:Button ID="btnCrewListforImoVessel" Text="IMO Crew List" CssClass="btn" style=" padding:1px; font-size:12px; width:120px; "  runat="server" OnClick="btnCrewListforImoVessel_Click" />
            </td>
        </tr>
                    </table>
                      <table border="0" cellpadding="0" cellspacing="0" style="text-align:center" width="100%">
                       <tr>
                        <td >
                          <%--  <div style="height:25px; overflow-x:hidden; overflow-y:scroll;border:solid 1px #c2c2c2;">
                                <table cellpadding="2" cellspacing="0" width="100%" border="0" style=" height:25px">
                               
                                </table>
                            </div>--%>
                            <div style="height:450px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2;">
                        <table cellpadding="2" cellspacing="0" width="100%" border="0">
                            <tr class="headerstylegrid"> 
                                    <td style="width:50px; text-align:center;">Delete</td>
                                    <td style="width:70px; text-align:left;">Emp. #</td>
                                    <td style="text-align:left;">Name</td>
                                    <td style="width:100px; text-align:left;">Rank</td>
                                    <td style="width:100px; text-align:left;">Nationality</td>
                                    <td style="width:100px; text-align:left;">Last VSL</td>
                                    <%--<td style="width:90px; text-align:center;">EOC</td>
                                    <td style="width:90px; text-align:center;">Exp. Join Dt.</td>--%>
                                    <td style="width:100px; ">Grading</td>
                                    <td style="width:100px; ">PP Expiry</td>
                                    <td style="width:100px; ">Approval</td>
                                    <td style="width:90px; text-align:center;">Checklist</td>
                                    <td style="width:20px"></td>
                                </tr>
                        <asp:Repeater runat="server" ID="rpt_CrewList">
                        <ItemTemplate>
                        <tr>
                        <td style="width:50px; text-align:center;">
                           <asp:ImageButton ID="imgbtnDelete" CommandArgument='<%# Eval("crewid") %>' OnClick="imgbtnDelete_Click" runat="Server" OnClientClick="return confirm('Are you sure you want to delete this record?');"  ImageUrl="~/Modules/HRD/Images/icon_delete_12.png" Visible='<%# (authPage.IsDelete) %>' />
                        </td>
                        <td style="width:70px; text-align:left;"><%#Eval("CrewNumber")%> </td>
                        <td style="text-align:left;padding-left:10px;"><%# Eval("CrewName") %></td>
                        <td style="width:100px; text-align:left;"><%# Eval("RankName") %></td>
                        <td style="width:100px; text-align:left;"><%#Eval("Nationality")%></td>
                        <td style="width:100px; text-align:left;"><%#Eval("LastVesselName")%></td>
                        <%--<td style="width:90px; text-align:center;"><%#Eval("EOC")%></td>
                        <td style="width:90px; text-align:center;"><%#Eval("ExpectedJoinDate")%></td>--%>
                        <td style="width:100px;"><span class='Grade_<%#Eval("Grading")%>'></span><%#Eval("Grading")%></td>
                        <td style="width:100px;"><%#Common.ToDateString(Eval("P_EXPIRY"))%></td>
                        <td style="width:100px;"><%#(Eval("APP_STATUS").ToString()=="A")?"Approved":((Eval("APP_STATUS").ToString()=="R")?"Rejected":((Eval("APP_STATUS").ToString()=="N")?"Pending":""))%></td>
                        <td style='width:90px; text-align:center;'><asp:ImageButton ID="btnCL" CommandArgument='<%#Eval("PlanningId")%>' runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Document CheckList" OnClick='btnCL_Click'/></td>                        
                        <td style="width:20px"></td>
                        </tr>
                        </ItemTemplate>
                        <%--<AlternatingItemTemplate>
                        <tr style="background-color:#E0F5FF">
                        <td style="width:50px; text-align:center;">
                           <asp:ImageButton ID="imgbtnDelete" CommandArgument='<%# Eval("crewid") %>' OnClick="imgbtnDelete_Click" runat="Server" OnClientClick="return confirm('Are you sure you want to delete this record?');"  ImageUrl="~/Modules/HRD/Images/icon_delete_12.png" />
                        </td>
                        <td style="width:70px; text-align:left;"><%#Eval("CrewNumber")%> </td>
                        <td style="text-align:left;"><%# Eval("CrewName") %></td>
                        <td style="width:80px; text-align:left;"><%# Eval("RankName") %></td>
                        <td style="width:100px; text-align:left;"><%#Eval("Nationality")%></td>
                        <td style="width:90px; text-align:left;"><%#Eval("VesselCode")%></td>
                        <td style="width:90px; text-align:center;"><%#Eval("EOC")%></td>
                        <td style="width:90px; text-align:center;"><%#Eval("ExpectedJoinDate")%></td>
                        <td style='width:90px; text-align:center;'><asp:ImageButton ID="btnCL" CommandArgument='<%#Eval("PlanningId")%>' runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Document CheckList" OnClick='btnCL_Click'/></td>                        
                        <td style="width:20px"></td>

                        </tr>
                        </AlternatingItemTemplate>--%>
                        </asp:Repeater>
                        </table>
                        </div>
                           <%-- <div style="width:100%;overflow-x:hidden; overflow-y:scroll; height:400px" >
                            <asp:GridView ID="gvsearch" runat="server" OnRowCommand="gvsearch_RowCommand"  AllowSorting="True" OnSorted="on_Sorted1" OnSorting="on_Sorting1"  AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Height="32px" Style="text-align: center" Width="98%" OnRowDataBound="GV_OnRowDataBound">
                                    <Columns>
                                     <asp:TemplateField Visible="false" >
                                       <ItemTemplate>
                                                                                          
                                       </ItemTemplate>
                                       </asp:TemplateField>
                                      
                                      <asp:CommandField ButtonType="Image" Visible="false"   HeaderText="Select" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
                                          <ItemStyle Width="30px" />
                                      </asp:CommandField>
                                       <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                               
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                      
                                      
                                      <asp:BoundField DataField="CrewNumber"  SortExpression="CrewNumber" HeaderText="Emp. #" >
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                      </asp:BoundField>
                             
                                                                      
                                        
                                        
                                         <asp:TemplateField HeaderText="Name" SortExpression="CrewName" >
                                            <ItemTemplate>
                                                
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                                              
                                       
                                       <asp:TemplateField HeaderText="Rank" SortExpression="RankName" >
                                       <ItemTemplate>
                                       
                                       </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Left" />
                                       </asp:TemplateField>
                                        <asp:BoundField DataField="Nationality"  SortExpression="Nationality" HeaderText="Nationality">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                             <asp:BoundField DataField="VesselCode"  SortExpression="VesselCode" HeaderText="Vessel Code" >
                                            <ItemStyle HorizontalAlign="Left" />
                                      </asp:BoundField>
                                        
                                        <asp:BoundField DataField="EOC"  SortExpression="EOC" HeaderText="EOC">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExpectedJoinDate"  SortExpression="ExpectedJoinDate" HeaderText="Exp. Join Dt.">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Checklist">
                                        <ItemTemplate>
                                                       
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheader" ForeColor="#0E64A0" />
                                      <RowStyle CssClass="rowstyle" />
                                                      
                                </asp:GridView>
                            </div>--%>
                            
                        </td>
                    </tr>
                </table>
                
            </td>
            </tr>
          </table>
            </td>
        </tr>
    </table>

    <!--  Sign On Region -->

    <div>
    <div style="position:absolute;top:40px;left:0px; height :510px; width:100%; " id="dv_CrewSearch" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:98%; padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
                <left >
                <iframe id="frmCrewSearch" runat="server" src="" height="400px" width="100%" scrolling="no" frameborder="0">

                </iframe>
                    <br />
                <asp:Button ID="btnClose" CausesValidation="false" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnClose_Click" />
                </left>
            </div>
        </center>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</form>
      </body>

    




