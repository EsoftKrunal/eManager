<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IssuedCircular.aspx.cs" Inherits="IssuedCircular" Title="Create Circular" %>
<%@ Register Src="~/Modules/LPSQE/Circular/IncidentReport.ascx" TagName="IncidentReport" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/LPSQE/Circular/Incident.ascx" TagName="Incident" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var SelNCRID=0;
        function OpenTopicDetail()
        {
            var TID=document.getElementById('hfTopicID').value;
            if(TID!='')
            window.open('..\\FormReporting\\AddNewTopic.aspx?TID='+TID+'','TID','title=no,toolbars=no,scrollbars=yes,width=1000,height=270,left=150,top=50,addressbar=no');        
        }
        
        function SetTpoicIDToHiddenFields(Tid)
        {
            SelNCRID=Tid;
            document.getElementById('hfTopicID').value=Tid;
        }
      
        function OpenTopicReport()
        {
            var Status= document.getElementById('ddlStatus');
            var StatusValue=Status.options[Status.selectedIndex].text;
            
            
            var VesselName= document.getElementById('lblVessel').innerHTML;
            window.open('..\\FormReporting\\PrintReport.aspx?ReportType=Topic&StatusValue='+StatusValue+'&VesselName='+VesselName+'','Topic','title=no,toolbars=no,scrollbars=yes,width=950,height=700,left=150,top=50,addressbar=no');                
        }
        function Reload()
        {
            document.getElementById('BtnReferesh').click();
        }
        function ShowFullComment(obj)
        {
            
            var X= obj.childNodes[0];
            var Y= obj.childNodes[1];
            if(Y.style.display=='block' || Y.style.display=='' )
            {
                Y.style.display='none';
            }
            else
            {
                Y.style.display='block';
            }
        }
        
        function RefereshParrentPage()
        {
            window.opener.ReloadThePage();
        }
        function CallClosure(chk)
        {
            if(chk.checked)
            {
                var x=confirm('Are you sure to close ?');
                if(x)
                {
                    chk.parentNode.nextSibling.click();
                }
                else
                {
                    chk.checked=false;
                    return false;
                    
                }
            }   
            return ;
        }
        function CreatCircular(CID)
        {   
            window.open('CircularForm.aspx?CreateCIR=CreateCIR&CID='+CID+'','Circular','title=no,toolbars=no,scrollbars=yes,width=750,height=630,left=150,top=50,addressbar=no');
        }
    </script>
    <script type="text/javascript" >
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
                        trSel.setAttribute(CSSName, "selectedrow");
                        lastSel=trSel;
                        document.getElementById('hfPRID').value = prid;
                    
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
        .Pink
        {
        	background-color:#FFCCCC;
        }
    </style>
    
    <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>
        
        <table style="width:100%;">
        <tr>
            <td>
                <table  style="width:100%;" border="1" cellpadding="5" cellspacing="5"  rules="all">
                    <col width="40px;"/>
                    <col width="120px;"/>
                    <col width="30px;"/>
                    <col width="120px;"/>
                    <col width="50px;"/>
                    <col width="150px;"/>
                    <col width="125px;"/>
                    <col width="210px;"/>
                    <col width="30px;"/>
                    <col width="100px;"/>
                    <col />
                    <tr>
                        <td >
                            From :
                            <asp:HiddenField ID="hfTopicID" runat="server" />
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="input_box" Width="80" ></asp:TextBox>
                            <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> 
                        </td>
                        <td style="text-align:right;">
                            To:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server" CssClass="input_box" Width="80" ></asp:TextBox>
                            <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton>
                        </td>
                        <td style="text-align:right;">
                            Category :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server"  Width="145px" CssClass="input_box">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Key word Search :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="input_box" Width="200" ></asp:TextBox>
                        </td>
                        <td style="text-align:right;" colspan="3">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" OnClick="btnSearch_OnClick" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" OnClick="btnClear_OnClick"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                             <asp:Label id="lblTotalRec" runat="server" Font-Bold="true"  ></asp:Label>
                        </td>
                        <td colspan="9" style="text-align:center; font-weight:bold;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="text-align:right;">
                <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                <%--<input type="button" value="Open" class="btn" onclick="OpenTopicDetail()" />  --%>
                <%--<input type="button" value="Closure" class="btn" onclick="OpenNCRClosure()" />--%>
                <%--<asp:Button ID="btnCloser" runat="server" Text="Close" class="btn"  OnClick="btnCloser_OnClick" />--%>
                
                
            </td>
        </tr>
        <tr>
            <td>
                <div style="color:#2554C7; font-weight:bold; display:none;">
                    Sort By <asp:LinkButton ID="lnkNameSort" runat="server" Text="Name" style="text-decoration:none; color:#2554C7;" OnClick="lnkNameSort_OnClick" ></asp:LinkButton> / <asp:LinkButton ID="lnlDateSort" runat="server" Text="Date" style="text-decoration:none;color:#2554C7;" OnClick="lnlDateSort_OnClick"  ></asp:LinkButton>
                </div>
                <div style="width:100%;height:315px;overflow-x:hidden;overflow-y:scroll;">
                    <asp:GridView style="TEXT-ALIGN: center" id="grdNCRDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="10" EmptyDataText="No Data Found." EmptyDataRowStyle-Font-Size="Medium" OnPageIndexChanging="grdNCRDetails_OnPageIndexChanging" OnSorting="grdNCRDetails_OnSorting" AllowSorting="true"  BorderColor="#5CB3FF" BorderWidth="2px" >
                 <RowStyle CssClass="rowstyle"></RowStyle>
                 <Columns>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>  
                                &nbsp;
                                <asp:ImageButton ID="imgEditTopic" runat="server" OnClick="imgEditTopic_OnClick" ImageUrl="~/Images/hourglass.gif" ToolTip="Click to view/edit the comment." style='<%#"display:"+Eval("Visibility").ToString()%>'    RowIndex='<%# Container.DisplayIndex %>'   />
                                <asp:HiddenField ID="hfCID" runat="server" Value='<%#Eval("CID") %>' />
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgAppOrReject" runat="server" OnClick="imgAppOrReject_OnClick" ImageUrl="~/Images/Comments.jpg" ToolTip="Click to approve or reject circuler." style='<%#"display:"+Eval("CommVisibility").ToString()%>'    RowIndex='<%# Container.DisplayIndex %>'   />
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="CircularCatName">
                            <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CircularCatName") %>'  ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110px" Font-Italic="true" ForeColor="#2554C7"></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Date" SortExpression="CreatedOn" >
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOnText") %>'  ></asp:Label>
                                  
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Top" Width="90px"  ForeColor="#8D38C9"></ItemStyle>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Title" >
                            <ItemTemplate>
                                <table width="770px" >
                                    <tr title="Click to show details">
                                        <td onclick="ShowFullComment(this)" style="cursor:pointer;">
                                            <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("Topic")%>'></asp:Label><asp:Label ID="lblFullCirculer" runat="server" Text='<%#Eval("Details")%>' style="display:none; background-color:#ffffe0"></asp:Label>                                            
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  ></ItemStyle>
                         </asp:TemplateField>
                 </Columns>
                 
                 <FooterStyle HorizontalAlign="Center"></FooterStyle>
                 <PagerStyle HorizontalAlign="Center"></PagerStyle>
                 <SelectedRowStyle CssClass="selectedtowstyle"></SelectedRowStyle>
                 <HeaderStyle Wrap="False" CssClass="headerstylefixedheader" ForeColor="#0E64A0"></HeaderStyle>
            </asp:GridView>
                </div>
            </td>
        </tr>
        
    </table>
    
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFrom">
</ajaxToolkit:CalendarExtender> 

<ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtTo"> 
</ajaxToolkit:CalendarExtender>


    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="DivApproveReject" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:250px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;  ;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <b>Enter Comments</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAppOrRejComments" runat="server" TextMode="MultiLine" Width="99%" Height="190px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            <asp:Label ID="lblMsgAppRej" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Button ID="btnApproveComments" runat="server" Text="Approve" OnClick="btnApproveComments_OnClick" CssClass="btn" style="background-color:Green;color:White; font-weight:bold; height:20px;" />
                                <asp:Button ID="btnRejectComments" runat="server" Text="Reject" OnClick="btnRejectComments_OnClick" CssClass="btn" style="background-color:Red;color:White; font-weight:bold;height:20px;" />
                                <asp:Button ID="btnCancelComments" runat="server" Text="Cancel" OnClick="btnCancelComments_OnClick" CssClass="btn" style="height:20px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
     </div>
</ContentTemplate>

</asp:UpdatePanel>
</asp:Content>