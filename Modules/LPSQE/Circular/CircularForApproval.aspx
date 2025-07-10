<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CircularForApproval.aspx.cs" Inherits="CircularForApproval" Title="Create Circular" %>
<%@ Register Src="~/Modules/LPSQE/Circular/IncidentReport.ascx" TagName="IncidentReport" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/LPSQE/Circular/Incident.ascx" TagName="Incident" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Circular </title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript">
        var SelNCRID=0;
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
        function Reload() {

            document.getElementById('btnSearch').click();
//            document.getElementById('ctl00_ContentPlaceHolder1_btnRefereshThePage').click();
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
            window.open('CircularForm.aspx?CreateCIR=CreateCIR&CID='+CID+'','Circular','title=no,toolbars=no,scrollbars=yes,width=770,height=640,left=150,top=50,addressbar=no');
        }
        function ClickSearchButton(obj)
        {
           if(event.keyCode==13)
           {
                document.getElementById('ctl00_ContentPlaceHolder1_btnSearch').click();
           }
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
        function OpenSendCircularForm(CID,CTyype,CircularNumber)
        {
            window.open('SendCircularForm.aspx?CID='+CID+'&CircularNumber='+CircularNumber+'&CTyype='+CTyype+'','','resizable=1,title=no,toolbars=no,scrollbars=yes,width=1200,height=400,left=50,top=50,addressbar=no');
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server" AsyncPostBackTimeout="300" ></ajaxToolkit:ToolkitScriptManager>
   
    
    <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>
        
        <table style="width:100%;">
        <tr>
            <td>
                <table  style="width:100%;" border="1" cellpadding="2" cellspacing="2"  rules="all">
                    <colgroup>
                        <col width="40px;"/>
                        <col width="110px;"/>
                        <col width="30px;"/>
                        <col width="110px;"/>
                        <col width="50px;"/>
                        <col width="90px;"/>
                        <col width="60px;"/>
                        <col width="110px;"/>
                        <col width="30px;"/>
                        <col width="84px;"/>
                        <col width="30px;"/>
                        <col width="80px;"/>
                        <col />
                        <tr>
                            <td>
                                From :
                                <asp:HiddenField ID="hfTopicID" runat="server" />
                                <asp:Button ID="btnRefereshThePage" runat="server" 
                                    OnClick="BtnReferesh_OnClick" style="display:none;" />
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="input_box" Width="80"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" 
                                    ImageUrl="~/Images/Calendar.gif" />
                            </td>
                            <td style="text-align:right;">
                                To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" CssClass="input_box" Width="80"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                    ImageUrl="~/Images/Calendar.gif" />
                            </td>
                            <td style="text-align:right;">
                                Category :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="input_box" 
                                    Width="110px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Search :
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="input_box" 
                                    onkeypress="ClickSearchButton(this);" Width="120"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">
                                Status :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" 
                                    Width="80px">
                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                    <asp:ListItem Value="3">In SMS</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Type :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="input_box" Width="80px">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Internal" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="External" Value="E"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:right;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                                    OnClick="btnSearch_OnClick" Text="Search" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn" 
                                    OnClick="btnClear_OnClick" Text="Clear" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTotalRec" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td colspan="9" style="text-align:center; font-weight:bold;">
                                <%--<asp:LinkButton ID="lnlAddnewCircular" runat="server" Text="Create New Circular"  ></asp:LinkButton>--%>
                                <a href="#" onclick="CreatCircular(0)">
                                <asp:Label ID="lblCreateNewCircular" runat="server" Text="Create New Circular"></asp:Label>
                                </a>
                            </td>
                            <td colspan="2">
                                <%--<asp:CheckBox ID="chkAwaitingApproval" runat="server" Text="Awaiting Approval" />--%>
                                <asp:LinkButton ID="lnkAwatingApproval" runat="server" 
                                    OnClick="lnkShowAwatingApproval_OnClick" Text="Awaiting Approval"> </asp:LinkButton>
                                <span ID="spanAwaitingApp" runat="server" style="color:Red"></span>&nbsp;&nbsp;
                                <%--<asp:CheckBox ID="chkRejected" runat="server" Text="Rejected" />--%>
                                <asp:LinkButton ID="lnkRejected" runat="server" 
                                    OnClick="lnkShowRejected_OnClick" Text="Rejected"></asp:LinkButton>
                                <span ID="spanrejected" runat="server" style="color:Red"></span>
                            </td>
                        </tr>
                    </colgroup>
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
                <div style="width:100%;height:395px;overflow-x:hidden;overflow-y:scroll;">
                    <asp:GridView style="TEXT-ALIGN: center" id="grdNCRDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="10" EmptyDataText="No Data Found." EmptyDataRowStyle-Font-Size="Medium" OnPageIndexChanging="grdNCRDetails_OnPageIndexChanging" OnSorting="grdNCRDetails_OnSorting" AllowSorting="true" BorderColor="#5CB3FF" BorderWidth="2px" OnRowDataBound="OnRowDataBound_grdNCRDetails"  OnSorted="grdNCRDetails_OnSorted"> 
                 <RowStyle CssClass="rowstyle"></RowStyle>
                 <Columns>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>  
                                &nbsp;
                                <asp:ImageButton ID="imgEditTopic" runat="server" OnClick="imgEditTopic_OnClick" ImageUrl="~/Images/hourglass.png" ToolTip="Click to view/edit the comment." style='<%#"margin-top:3px;display:"+Eval("Visibility").ToString()%>'    RowIndex='<%# Container.DisplayIndex %>'   />
                                <asp:HiddenField ID="hfCID" runat="server" Value='<%#Eval("CID") %>' />
                                <asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("CFileName") %>' />
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--<asp:ImageButton ID="imgEditTopic" runat="server" OnClick="imgEditTopic_OnClick" ImageUrl="~/Images/hourglass.gif" ToolTip="Click to view/edit the comment." style='<%#"display:"+Eval("Visibility").ToString()%>'    />--%>
                                <a onclick="OpenSendCircularForm('<%#Eval("CID") %>','<%#Eval("CType") %>','<%#Eval("CircularNumber") %>')"> <img  src="../Images/Mail.png" style="margin-top:3px;" /> </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                        </asp:TemplateField>     
                        
                        <asp:TemplateField HeaderText="Circular#" SortExpression="CNForSorting" >
                            <ItemTemplate>
                                <%#Eval("CircularNumber")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="120px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date" SortExpression="CreatedOn" >
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOnText") %>'  ></asp:Label>
                                  
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Top" Width="100px"  ForeColor="#8D38C9"></ItemStyle>
                         </asp:TemplateField>
                                          
                        <%--<asp:TemplateField HeaderText="Source" HeaderStyle-HorizontalAlign="Center" SortExpression="Source">
                            <ItemTemplate >
                                <asp:Label ID="lblSourceName" runat="server" Text='<%#Eval("Source") %>'  ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="180px" ForeColor="#8D38C9"></ItemStyle>
                         </asp:TemplateField>--%>
                         
                         
                         
                        <asp:TemplateField HeaderText="Category" SortExpression="CircularCatName">
                            <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CircularCatName") %>'  ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="120px" Font-Italic="true" ForeColor="#2554C7"></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Topic" >
                            <ItemTemplate>
                                <table width="500px" >
                                    <tr title="Click to show details">
                                        <td >  <%--onclick="ShowFullComment(this)"  style="cursor:pointer;" --%>
                                            <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("Topic")%>'></asp:Label><asp:Label ID="lblFullCirculer" runat="server" Text='<%#Eval("Details")%>' style="display:none; background-color:#ffffe0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  ></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField >
                            <ItemTemplate>
                               <a target="_blank" href="../UserUploadedDocuments/Circular/<%#Eval("CFileName")%>?rand=<%#Eval("rand").ToString()%>">
                                    <img src="../Images/paperclip.png" style=" margin-top:3px;border:none; display:<%#Eval("ClipVisibility")%>" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10"  ></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStauts" runat="server" Text='<%#Eval("StatusText")%>' style='<%#"display:"+Eval("StatusLableVisibility").ToString()%>' ></asp:Label>
                                <asp:LinkButton ID="imgAppOrReject" runat="server" Text='<%#Eval("StatusText")%>' OnClick="imgAppOrReject_OnClick" ToolTip="Click to approve or reject circuler." style='<%#"display:"+Eval("CommVisibility").ToString()%>'    RowIndex='<%# Container.DisplayIndex %>' Enabled='<%#Auth.isSecondApproval %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125" ></ItemStyle>
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
            <div style="position :relative; width:700px; height:250px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
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
</form>
</body>
</html>