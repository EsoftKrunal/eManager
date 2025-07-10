<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CircularData.aspx.cs" Inherits="CircularData" Title="Circular Data" %>
<%@ Register Src="~/Modules/LPSQE/Circular/IncidentReport.ascx" TagName="IncidentReport" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/LPSQE/Circular/Incident.ascx" TagName="Incident" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var SelNCRID=0;
        function OpenNCRk(NCRID)
        {
            window.open('..\\FormReporting\\ModifyNCR.aspx?NCRID='+NCRID+'','NCRDetail','title=no,toolbars=no,scrollbars=yes,width=1000,height=800,left=150,top=50,addressbar=no');        
        }
        
        function OpenTopicDetail()
        {
            var TID=document.getElementById('hfTopicID').value;
            if(TID!='')
            window.open('..\\FormReporting\\AddNewTopic.aspx?TID='+TID+'','TID','title=no,toolbars=no,scrollbars=yes,width=1000,height=270,left=150,top=50,addressbar=no');        
        }
        function OpenNCRClosure()
        {
            
            var NCRID=document.getElementById('hfTopicID').value;
            if(NCRID!='')
                window.open('..\\FormReporting\\NCRClosure.aspx?NCRID='+NCRID+'','NCRDetail','title=no,toolbars=no,scrollbars=yes,width=750,height=260,left=150,top=50,addressbar=no');                
        }
        function OpenNCRReport()
        {
            if(SelNCRID==0)
            {
                window.open('..\\FormReporting\\PrintNCR.aspx?NCRID=NCRID','NCRDetail','title=no,toolbars=no,scrollbars=yes,width=950,height=700,left=150,top=50,addressbar=no');                
            }
            else
            {
                window.open('..\\FormReporting\\PrintNCR.aspx?SelNCRID='+SelNCRID+'','NCRDetail','title=no,toolbars=no,scrollbars=yes,width=950,height=700,left=150,top=50,addressbar=no');                
            }
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
            if(X.style.display=='block' || X.style.display=='' )
            {
                X.style.display='none';  
                Y.style.display='block';
            }
            else
            {
                X.style.display='block';  
                Y.style.display='none';
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
            var x= confirm('Do you want to create circular now ?');
            if(x)
            {
                window.open('CircularForm.aspx?CID='+CID+'','Circular','title=no,toolbars=no,scrollbars=yes,width=750,height=630,left=150,top=50,addressbar=no');
                //window.open('..\\FormReporting\\PrintReport.aspx?ReportType=Topic&StatusValue='+StatusValue+'&VesselName='+VesselName+'','Topic','title=no,toolbars=no,scrollbars=yes,width=950,height=700,left=150,top=50,addressbar=no');                
            }
        }
        function CreatCircular1(CID)
        {
            window.open('CircularForm.aspx?CID='+CID+'','Circular','title=no,toolbars=no,scrollbars=yes,width=750,height=630,left=150,top=50,addressbar=no');
            //window.open('..\\FormReporting\\PrintReport.aspx?ReportType=Topic&StatusValue='+StatusValue+'&VesselName='+VesselName+'','Topic','title=no,toolbars=no,scrollbars=yes,width=950,height=700,left=150,top=50,addressbar=no');                
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
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div class="text headerband">
       
    </div>
    <br />
 <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>
        
        <table style="width:100%;"  >
        <tr>
            <td>
                <table  style="width:100%;" border="1" cellpadding="5" cellspacing="5"  rules="all">
                    <colgroup>
                        <col width="40px;" />
                        <col width="120px;" />
                        <col width="30px;" />
                        <col width="120px;" />
                        <col width="50px;" />
                        <col width="90px;" />
                        <col width="130px;" />
                        <col width="110px;" />
                        <col width="30px;" />
                        <col width="100px;" />
                        <col />
                        <tr>
                            <td>
                                From :
                                <asp:HiddenField ID="hfTopicID" runat="server" />
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="input_box" Width="80"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" 
                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                            </td>
                            <td style="text-align:right;">
                                To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" CssClass="input_box" Width="80"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
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
                                Key Word Search :
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="input_box" Width="120"></asp:TextBox>
                            </td>
                            <td style="text-align:right;">
                                Status :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" 
                                    Width="90px">
                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Request</asp:ListItem>
                                    <asp:ListItem Value="2">Accepted</asp:ListItem>
                                    <asp:ListItem Value="3">Rejected</asp:ListItem>
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
                                <asp:LinkButton ID="lnlAddnewCircular" runat="server" 
                                    OnClick="lnlAddnewCircular_OnClick" Text="Request New Circular"></asp:LinkButton>
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
                <div style="width:100%;height:300px;overflow-x:hidden;overflow-y:scroll;">
                    <asp:GridView style="TEXT-ALIGN: center" id="grdNCRDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="10" EmptyDataText="No Data Found." EmptyDataRowStyle-Font-Size="Medium" OnPageIndexChanging="grdNCRDetails_OnPageIndexChanging" OnSorting="grdNCRDetails_OnSorting" AllowSorting="true"  BorderColor="#5CB3FF" BorderWidth="2px"  >
                 <RowStyle CssClass="rowstyle"></RowStyle>
                 <Columns>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>  
                                &nbsp;
                                <asp:ImageButton ID="imgEditTopic" runat="server" OnClick="imgEditTopic_OnClick" ImageUrl="~/Modules/HRD/Images/hourglass.gif" ToolTip="Click to edit the comment." style='<%#"display:"+Eval("Visibility").ToString()%>'    RowIndex='<%# Container.DisplayIndex %>'   />
                                <asp:HiddenField ID="hfCID" runat="server" Value='<%#Eval("ID") %>'/>
                                <asp:HiddenField ID="hfUploadedFile" runat="server" Value='<%#Eval("CFileName") %>'/>
                                <asp:HiddenField ID="hfCatID" runat="server" Value='<%#Eval("CircularCat") %>'/>
                                
                                <asp:HiddenField ID="hfAccBy" runat="server" Value='<%#Eval("AppOrRejectedByText") %>'/>
                                <asp:HiddenField ID="hfAccOn" runat="server" Value='<%#Eval("AppOrRejectedDate ") %>'/>
                                
                                <asp:HiddenField ID="hfStatusID" runat="server" Value='<%#Eval("Status") %>'/>
                                
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                        </asp:TemplateField>
                        
                        <%--<asp:TemplateField>
                            <ItemTemplate>  
                                <asp:ImageButton ID="imgApprove" runat="server" OnClick="imgApprove_OnClick" ImageUrl="~/Images/check.gif" ToolTip=""   />
                                
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="25px" />
                        </asp:TemplateField>--%>
                        
                        <%--<asp:TemplateField>
                            <ItemTemplate>  
                                <asp:ImageButton ID="imgApp" runat="server" ImageUrl="~/Images/delete1.gif"  ToolTip="" Visible="false" />
                                
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="25px" />
                        </asp:TemplateField>--%>
                        
                        <asp:TemplateField HeaderText="Source" HeaderStyle-HorizontalAlign="Center" SortExpression="CreatedByName">
                            <ItemTemplate >
                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedByName") %>'  ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110px" ForeColor="#8D38C9"></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Date" SortExpression="CreatedOn">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOnText") %>'  ></asp:Label>
                                  
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Top" Width="90px"  ForeColor="#8D38C9"></ItemStyle>
                         </asp:TemplateField>
                         
                        <asp:TemplateField HeaderText="Category" SortExpression="CircularCat">
                            <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CircularCatName") %>'  ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110px" Font-Italic="true" ForeColor="#2554C7"></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Description" >
                            <ItemTemplate>
                                <table width="600px" >
                                    <tr>
                                        <td onclick="ShowFullComment(this)" style="cursor:pointer;">
                                            <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("ShortCircular")%>'></asp:Label><asp:Label ID="lblFullCirculer" runat="server" Text='<%#Eval("Circular")%>' style="display:none; background-color:#ffffe0"></asp:Label>                                            
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  ></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField >
                            <ItemTemplate>
                               <a target="_blank" href="../UserUploadedDocuments/Circular/<%#Eval("CFileName")%>">
                                    <img src="../../HRD/Images/paperclipx12.png" style="border:none; display:<%#Eval("ClipVisibility")%>" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20"  ></ItemStyle>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStauts" runat="server" Text='<%#Eval("StatusText")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60" ></ItemStyle>
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



<div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvConfirmCancel" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:880px; height:425px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;  ;opacity:1;filter:alpha(opacity=100)">
            <!-- DATA SECTION -->
            <div id="DivUpdate"  runat="server"  style="width:100%;"> 
            <div id="tblViewData" runat="server">
                <table width="100%" cellpadding="4" cellspacing="0" border="1" rules="none" style="border:solid 1px #4371a5; padding-top:100px;">
                    <colgroup>
                        <col width="140px" />
                        <col width="120px" />
                        <col width="50px" />
                        <col />
                        <tr align="left">
                            <td style="text-align:right;">
                                <b>Category :</b></td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblviewCategory" runat="server"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <b>Status :</b>&nbsp;
                                <asp:Label ID="lblviewStatus" runat="server"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top;text-align:right;">
                                <b>Description : </b>
                            </td>
                            <td colspan="3" style="text-align:left;">
                                <div ID="divViewDesc" runat="server" 
                                    style="border:solid 1px #eee9e9;overflow-x:hidden;overflow-y:scroll; height:300px;vertical-align:top;">
                                    <asp:Label ID="lblviewDesc" runat="server" style="padding:2px;" Width="710px"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align :right;">
                                <b>Attachment :</b>
                            </td>
                            <td style="text-align :left;">
                                <a ID="aviewUploadedFile" runat="server" target="_blank">
                                <img src="../../HRD/Images/paperclipx12.png" style="border:none;" /> </a>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align :right;">
                                <b>Requested By/on : </b>
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblviewCreatedBy" runat="server"></asp:Label>
                                <asp:Label ID="lblviewCreatedOn" runat="server"></asp:Label>
                            </td>
                            <td style="text-align :right;">
                                <b>
                                <asp:Label ID="lblAccepetedOrRejectText" runat="server"></asp:Label>
                                </b>
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblviewAccBy" runat="server"></asp:Label>
                                <asp:Label ID="lblviewAccOn" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align:right;">
                                <asp:Button ID="btnApprove" runat="server" CssClass="btn" OnClick="btnApprove_OnClick" style="font-weight:bold; " Text="Approve"  />
                                <asp:Button ID="btnReject" runat="server" CssClass="btn" OnClick="btnReject_OnClick" style=" font-weight:bold; " Text="Reject" />
                            </td>
                            <td colspan="1" 
                                style="text-align:right; vertical-align:bottom;  padding:3px;"> 
                                <asp:Button ID="btnEditCircular" runat="server" CssClass="btn" OnClick="btnEditCircular_OnClick" Text=" Edit " />
                                <asp:Button ID="btnCreateCircular" runat="server" CssClass="btn" OnClick="btnCreateCircular_OnClick" Text="Create Circular"  />
                                <asp:Button ID="btnViewClose" runat="server" CssClass="btn" OnClick="btnClose_OnClick" Text=" Close " />
                            </td>
                        </tr>
                        <tr ID="trAppRej" runat="server" visible="false">
                            <td colspan="4">
                                <table cellpadding="0" cellspacing="" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAppRejTxtEntry" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtAppRejComments" runat="server" 
                                                style="height:90px; width:99%;" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; padding-right:5px;">
                                            <asp:Label ID="lblMsgAppRej" runat="server" ForeColor="Red"></asp:Label>
                                            <asp:Button ID="btnSaveAppRejComm" runat="server" CssClass="btn" OnClick="btnSaveAppRejComm_OnClick" Text="Save" />
                                                
                                            <asp:Button ID="btnCancelAppRejComm" runat="server" CssClass="btn" 
                                                OnClick="btnCancelAppRejComm_OnClick" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </colgroup>
                
             </table>
             </div>
             
                 <table width="100%" id="tblEnterNotes" runat="server"  cellpadding="0" cellspacing="0" visible="false" >  <%--style="border:solid 1px #4371a5;"--%>
                    <tr>
                        
                        <td style="padding:5px;">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Category : &nbsp;
                                        <asp:DropDownList ID="ddlSavingCat" runat="server"  Width="110px" CssClass="input_box">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-left:50px;">
                                        Upload file :&nbsp;&nbsp;&nbsp;
                                        <asp:FileUpload ID="fuFile" runat="server" Width="380px"  CssClass="input_box"/>
                                        
                                        <a id="aAddFile" runat="server"  target="_blank" > <img src="../../HRD/Images/paperclipx12.png" style="border:none;" /> </a>
                                        <asp:LinkButton ID="lnkClearFile" runat="server" Text="Delete Attachment" OnClick="lnkClearFile_OnClick" style="text-decoration:none;" ></asp:LinkButton>
                                        
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left:5px;font-weight:bold; ">
                            Description
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left:5px; padding-right:10px;">
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="100%" Height="340px" CssClass="input_box" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left:5px;padding-right:4px; padding-bottom:2px; text-align:right;">
                            <asp:Label ID="lblMsgAddTopic" runat="server" ForeColor="Red" ></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn"  OnClick="Save_OnClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn"  OnClick="Cancel_OnClick" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn"  OnClick="btnClose_OnClick" />
                            
                        </td>
                    </tr>
                </table>
            
            <!-- DATA SECTION END -->
       </div> 
       <br />
       <asp:Label ID="Label1" runat="server" CssClass="PageError" style="float:left;" ></asp:Label>
    </div> 
    </center>
    </div>
</ContentTemplate>
<Triggers ><asp:PostBackTrigger ControlID="btnSave"/></Triggers>
</asp:UpdatePanel>
    </asp:Content>