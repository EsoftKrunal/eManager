<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewPlanningApproval.aspx.cs" Inherits="CrewOperation_CrewPlanningApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eManager</title>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" type="text/css" rel="Stylesheet" />
    
    <style type="text/css">
    body
    {
        font-family:Calibri; 
        font-size:14px;
        margin:0px;
        color:#444;
    }
    h1 {
        font-size: 18px;
        /*background-color: rgba(13, 93, 140, 1);*/
        background-color: #0D5D8C;
        padding: 8px;
        margin: 0px;
        color: #fff;
    }
    
    h2 {
        font-size: 17px;
        color: rgb(13, 93, 140);
        margin: 0px;
        border-bottom: solid 1px #e2e2e2;
        text-align: left;
        padding: 10px;
        /*background-color: rgb(223, 243, 255);*/
        background-color: #DFF3FF;
        margin-top: 20px;
    }
    
    
    .data
    {
        font-size:12px;
    }
    .dataheader
    {
        font-size:14px;
        background-color:#FFE0C2;
    }
    a img
    {
        border:none;
    }
    
    .newbtn
    {
        border:solid 0px #c2c2c2;
        /*background-color:rgba(13, 93, 140, 1);*/
        background-color: #0D5D8C;
        color:white;
        padding:8px 15px;
        
        font-size:15px;
        margin-top:2px;
    }
    .heading
    {
        /*color:rgb(88, 88, 88);*/
        color:#585858;
        font-weight:bold;
        text-align:right;
    }
    .headingvalue
    {
        text-align:left;
        font-weight:bold;
    }
    table
    {
        border-collapse:collapse;
       
    }
    .bordered tr td
    {
        border:solid 1px #e2e2e2;
        padding:8px;
        text-align:left;
    }
    .trs
    {
        font-weight:bold;
    }
    .success{
        float:left;margin-top:-5px;color:#31c315;width:35px;display:block;text-align:center;margin-right:10px;
    }
    .error{
            float:left;margin-top:-5px;color:RED;width:35px;display:block; text-align:center;margin-right:10px;
    }


     .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_B
       {
           background:yellow; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_D
       {
           background:red; 
           width:15px;
           height:15px;
           color:white;
           border:solid 1px grey;
       }
    </style>
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="margin:0px 50px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
    <center>
    <div id="header" style="position:fixed;top:0px;left:0px;width:100%; border-bottom:solid 1px #eee;border-bottom: solid 1px #c2c2c2;box-shadow: 0px 0px 22px #0d5d8c;">
    <div style="text-align: center;" class="text headerband">
        <h1> Crew Approval for Vessel Assignment  </h1>
    </div> 
                    <div style="margin:0px; ">
                            <table width='100%' style="text-align:left;" >         
                                <tr>
                                    <td>
                                            <table width='100%' style="text-align:left;" class="bordered" >
                                                    <colgroup>
                                                    <col width='200px' />
                                                    <col width='20px' />
                                                    <col />
                                                    <col width='120px' />
                                                    <col width='20px' />
                                                    <col />
                                                    <col width='120px' />
                                                    <col width='20px' />
                                                    <col />
                                                    </colgroup>
                                                    <tr class= "headerstylegrid">
                                                        <td class="heading">Crew Number&nbsp; </td>
                                                        <td>:</td>
                                                        <td><asp:Label runat="server" ID="lblID"></asp:Label>  </td>
                                                        <td class="heading">Crew Name </td>
                                                        <td>:</td>
                                                        <td> <asp:Label runat="server" ID="lblName"></asp:Label> 
                                                             <b>( <asp:Label runat="server" ID="lblRank"></asp:Label> )</b>
                                                        </td>

                                                        <td class="heading">Planned Rank  </td>
                                                        <td>:</td>
                                                        <td>  <asp:Label runat="server" ID="lblPlannedRank"></asp:Label>  </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td class="heading">Nationality  </td>
                                                        <td>:</td>
                                                        <td>  <asp:Label runat="server" ID="lblNationality"></asp:Label>  </td>
                                                        <td class="heading">DOB </td>
                                                        <td>:</td>
                                                        <td><asp:Label runat="server" ID="lblDOB"></asp:Label>  </td>
                                                         <td class="heading"> DJC </td>
                                                        <td>:</td>
                                                         <td> <asp:Label runat="server" ID="lblDJC" Text=""></asp:Label>  </td>                                                         
                                                    </tr>
                                                     <tr>                                                        
                                                        <td class="heading">Planned Vessel</td>
                                                        <td> :  </td>
                                                        <td> <asp:Label runat="server" id="lblplannedvessel"></asp:Label></td>
                                                        <td class="heading">Crew Type </td>
                                                        <td>:</td>
                                                        <td> <asp:Label runat="server" ID="lblCrewStatus"></asp:Label>  </td>
                                                        <td class="heading">Approval Status </td>
                                                        <td>:</td>
                                                        <td> <asp:Label runat="server" ID="lblApprovalStatus"></asp:Label>  </td>
                                                    </tr>
                                                 <tr>
                                                        <td class="heading">Planning Remarks&nbsp; </td>
                                                        <td>:</td>
                                                        <td colspan="7"><asp:Label runat="server" ID="lblPlanningRemarks"></asp:Label>  </td>                                                        
                                                    </tr>
                                                
                                                  
                                                     </table>
                                    </td>
                                    <td style="text-align:Center;">
                                        <div>
                                            <asp:Image runat="server" id="imgcrewpic" width="80px" height="120px"></asp:Image>
                                            </div>
                                        <div style="margin-top:5px;">
                                            <a href="../Reporting/PrintCV.aspx?crewid=<%=CrewID %>" target="_blank"> <b>Print CV</b>  </a> 
                                        </div>
                                    </td>
                                </tr>
                            </table>
                          
                        </div>
        </div>
        
        <h2 style="margin-top:230px;" > Crew Particular Updated
            <asp:Literal ID="litCrewParticularUpdated" runat="server"></asp:Literal>
            
        </h2>
        <table width="100%" class="bordered">
            <col  width="150px"/>
            <col  width="20px"/>
            <tr>
                <td class="heading">Last Verified By/On</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblVerifiedByOn" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <%-- <h2> Last 2 PEAP <asp:Label runat="server" ID="lblpeapmsg"></asp:Label>
             <asp:Literal ID="litLastPEAP" runat="server"></asp:Literal>
             
         </h2>
         <div runat="server" id="pnllastpeap">
              <table width="100%" class="bordered">
                <col width="50px" />
                <col width="250px" />
                <col width="150px" />
                             
                <col />
             
                <col width="150px" />
                <col width="150px" />  
                  <col width="100px" /> 
                                
             
                  <tr class= "headerstylegrid">
                    <td class="heading" style="text-align:left;"> <b>Sr#</b></td>
                    <td class="heading" style="text-align:left;"> <b>Peap Period</b></td>
                    <td class="heading" style="text-align:left;"><b>Vessel Name</b></td>
                    <td class="heading" style="text-align:left;"><b>Performance Score</b></td>
                    <td class="heading" style="text-align:left;"><b>Competency Score</b></td>
                    <td class="heading" style="text-align:center;"><b>Attachment</b></td>                                        
                      <td class="heading" style="text-align:center;"><b>Grade</b></td>                                        
                </tr>
                <asp:Repeater ID="rptLastPeap" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("RowNo") %></td>
                            <td> <%#Common.ToDateString(Eval("FromDate")) %> <b>-</b>  <%# Common.ToDateString(Eval("ToDate")) %></td>
                            <td> <%#Eval("VesselName") %> </td>                            
                            <td> <i><%#Eval("N_PerfScrore") %></i>  </td>
                            <td> <%#Eval("N_CompScore") %> </td>
                            <td style="text-align:center"> <asp:ImageButton ID="btnDownloadLastPeap" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.png"  onClick="btnPeapAttachment_Click" CommandArgument='<%#Eval("ImagePath") %>' style="cursor:pointer;"  /></td>
                            <td style="text-align:center"> <div class="Grade_<%#Eval("grade") %>"><%#Eval("grade") %></div> </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
         </div>  --%>        
        
        <div id="divMatrixCompliance" runat="server">
         <h2> Matrix Compliance
             <asp:Literal ID="lblMatrixCompliance" runat="server"></asp:Literal>
         </h2>
        <table width="100%" class="bordered">                    
        <tr  class= "headerstylegrid">
        <td class="heading" style="text-align:left;"> <b> Vessel Matrix Status</b></td>
        <td class="heading" style="text-align:left;"><b>Master + C/O(Yrs)</b></td>
        <td class="heading" style="text-align:left;"><b>Chief Engineer + 1 A/E(Yrs)</b></td>
        </tr>
        <tr>
        <td>Total Tanker Experience</td>
        <td><asp:Label ID="Label10" runat="server" ></asp:Label> </td>
        <td><asp:Label ID="Label11" runat="server"></asp:Label> </td>
        </tr>
        <tr>
        <td>On Board Actual Rank Experience </td>
        <td><asp:Label ID="Label20" runat="server"></asp:Label> </td>
        <td><asp:Label ID="Label21" runat="server"></asp:Label> </td>
        </tr>
        <tr>
        <td>Service With The Company</td>
        <td><asp:Label ID="Label30" runat="server"></asp:Label> </td>
        <td><asp:Label ID="Label31" runat="server"></asp:Label> </td>
        </tr>
        </table>
        </div>

         <h2> Notes 
             <asp:Literal ID="litCRM" runat="server"></asp:Literal>
         </h2>
         <table width='100%' style="text-align:left;" class="bordered" >
                <colgroup>
                <col width='120px' />
                <col />
                <col width='120px' />
                <col width='120px' />
                </colgroup>
                <tr class= "headerstylegrid">
                    <td class="heading" style="text-align:center"> CRM Category </td>
                    <td class="heading" style="text-align:left"> Description </td>
                    <td class="heading" style="text-align:center"> Created By </td>
                    <td class="heading" style="text-align:center"> Created On </td>                
                </tr>
                <asp:Repeater runat="server" id="rptCRM">
                        <ItemTemplate>
                                <tr>
                                        <td> <%#Eval("CRMCategoryName")%> </td>
                                        <td> <i> <%#Eval("Description")%> </i> </td>
                                        <td> <%#Eval("username")%> </td>
                                        <td> <%#Common.ToDateString(Eval("CreatedOn"))%> </td>                
                                    </tr>         
                        </ItemTemplate>
                </asp:Repeater>
        </table>

        
        <h2>Checklist</h2>
        
        <table width="100%" class="bordered">
            <col width='30px' />
            <col />
            <col width='250px' />
            <col width='120px' />
            <tr class= "headerstylegrid">
                <td class="heading" style="text-align:left;"></td>
                <td class="heading" style="text-align:left;">Checklist Name</td>
                <td class="heading" style="text-align:left;">Completed By/On</td>
                <td class="heading" style="text-align:left;"></td>
            </tr>
            <tr>
                <td> <asp:Literal ID="litDocumentChecklist" runat="server"></asp:Literal> </td>
                <td >Document Checklist</td>
                <td ><asp:Label runat="server" ID="lblDC_CompletedByOn"></asp:Label></td>                
                <td >
                    <a target="_blank" href="ViewCrewCheckList.aspx?_p=<%=planningid%>"> (Checklist) </a>
                </td>
            </tr>
            <asp:Repeater ID="rptCheckList" runat="server" >
                <ItemTemplate>
                    <tr>
                        <td> 
                            <i class='fa fa-1x <%# ((Eval("CompletedBy").ToString()=="")?" fa-exclamation error ":" fa-check success ") %>' ></i>
                        </td>
                        <td> <%#Eval("CheckListname") %></td>
                        <td> <%#Eval("CompletedBy")%> / <%# Common.ToDateString( Eval("CompletedOn"))%></td>
                        <td>
                            <a target="_blank" href="ViewCrewCheckList1.aspx?type=<%#Eval("CheckListMasterId")%>&_p=<%=planningid%>"> (Checklist) </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>   
        
       
       
        <%----------------------------------------------------------------------------------------------------------------------------%>
        <%--<div id="divPC" runat="server">
            <h2>Promotion Checklist</h2>
            <table width="100%" class="bordered">
                <colgroup>
                    <col width='40px' />                 
                    <col />        
                </colgroup>
                <tr style="background-color: rgb(245, 245, 245)">
                    <td class="heading">Sr#</td>
                    <td class="heading" style="text-align:left">Checklist</td>
                </tr>
                <asp:Repeater runat="server" ID="rptPromotionChecklist">
                <ItemTemplate>
                <tr style="height:25px;">
                    <td style="text-align:left;"><%#Eval("Sr")%></td>
                    <td style="text-align:left">&nbsp;            
                        <%#Eval("CheckListItemName") %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div id="divNC" runat="server">
            <h2>New Crew Checklist</h2>
            <table width="100%" class="bordered">
                <colgroup>
                    <col width='40px' />                 
                    <col />        
                </colgroup>
                <tr style="background-color: rgb(245, 245, 245)" >
                    <td class="heading">Sr#</td>
                    <td class="heading" style="text-align:left">Checklist</td>
                </tr>
                <asp:Repeater runat="server" ID="rptNewCrewChecklist">
                <ItemTemplate>
                <tr style="height:25px;">
                    <td style="text-align:left;"><%#Eval("Sr")%></td>
                    <td style="text-align:left">&nbsp;            
                        <%#Eval("CheckListItemName") %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>--%>
        <%----------------------------------------------------------------------------------------------------------------------------%>

<%--         <h2> Cargo Sheet ( File Upload )
             <asp:Literal ID="litCargoSheet" runat="server"></asp:Literal>            
         </h2>
            <div style="padding:5px;">
                <table width="100%" border="0"  >
                    <col width="500px" />
                    <col />
                    <col width="100px" />
                    <tr >
                        <td>
                            <b> File Description  : </b><asp:TextBox ID="txtDiscription" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpload1" CssClass="newbtn" runat="server" />
                        </td>
                        
                        <td style="text-align:right;">
                             <asp:Button ID="btnUploadFile" runat="server" Text="Upload" CssClass="newbtn" OnClick="btnUploadFile_OnClick" />
                        </td>
                    </tr>
                </table>
                
            </div>
            <table width="100%" class="bordered">
                <col width="60px" />
                <col />
                <col width="300px" />
                <col width="40px" />
                <col width="40px" />
                <tr style="background-color: rgb(245, 245, 245)">
                    <td class="heading" style="text-align:left;"> <b>Sr#</b></td>
                    <td class="heading" style="text-align:left;"> <b>Description</b></td>
                    <td class="heading" style="text-align:left;"><b>File Name</b></td>
                    <td class="heading" style="text-align:left;"><b></b></td>
                    <td class="heading" style="text-align:left;"><b></b></td>
                </tr>                
                     <asp:Repeater ID="rptUploadedFiles" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("RowNo") %> 
                                <asp:HiddenField ID="hdfFileName" runat="server" Value='<%#Eval("FileName") %> ' />
                            </td>
                            <td><%#Eval("Discription") %></td>
                            <td> <%#Eval("FileName") %> </td>
                            <td style="text-align:center;">
                                <asp:ImageButton runat="server" ID="btnDownloaFile" onClick="btnDownloaFile_Click" ImageUrl="~/Modules/HRD/Images/paperclip.gif" CommandArgument='<%#Eval("ID") %>'  ToolTip="Download" style="cursor:pointer;"></asp:ImageButton>
                            </td>
                            <td style="text-align:center;">
                                <asp:ImageButton runat="server" ID="btnDeleteUploadedFile" onClick="btnDeleteUploadedFile_Click" ImageUrl="~/Modules/HRD/Images/delete_12.gif" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('Are you sure to delete this file?');" ToolTip="Delete"  style="cursor:pointer;"></asp:ImageButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    
            </table>


         
        --%>

        <div id="divproposer" runat="server">
        <h2 style="" > Proposer's Remarks</h2>
        <table width="100%" class="bordered">
            <col  width="150px"/>
            <col  width="20px"/>
            <tr>
                <td class="heading">Proposed By/On</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblProposedByOn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                
                <td class="heading">Remarks</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblFwdRemark" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <h2> Approval  
            <asp:Literal ID="litApprovalSummary" runat="server"></asp:Literal>            
        </h2>
        <table width="100%" class="bordered">
                <col width="50px" />
                <col width="150px" />
                <col width="150px" />
                             
                <col />
             
                <col width="80px" />
            <col width="300px" />  
                <col width="100px" />
                <col width="70px" />
                <tr class= "headerstylegrid">
                    <td class="heading" style="text-align:left;"> <b>Sr#</b></td>
                    <td class="heading" style="text-align:left;"> <b>Approval Level</b></td>
                    <td class="heading" style="text-align:left;"><b>Forwarded To</b></td>
                    <td class="heading" style="text-align:left;"><b>Comments</b></td>
                    <td class="heading" style="text-align:left;"><b>Status</b></td>
                    <td class="heading" style="text-align:left;"><b>Done By</b></td>
                    <td class="heading" style="text-align:left;"><b>Done On</b></td>
                    <td class="heading" style="text-align:left;"></td>
                </tr>
                <asp:Repeater ID="rptApprovalList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("RowNo") %>                                       
                            </td>
                            <td> <%#Eval("ApprovalName") %></td>
                            <td> <%#Eval("ApprovalFwdToName") %> </td>
                            
                            <td> <i><%#Eval("Comments") %></i>  </td>
                            <td> <%#Eval("Result") %> </td>
                            <td> <%#Eval("ApprovedBy") %> ( <%#Eval("PositionName") %> ) </td>
                            <td style="text-align:center;"> <%#Common.ToDateString(Eval("ApprovedOn")) %> </td>
                            
                            <td style="text-align:center;">

<%--                                <asp:Button runat="server" ID="btnPopupApprove" onClick="btnPopupApprove_Click" CommandArgument='<%#Eval("TableID") %>' CssClass="newbtn" Text="Approve" Visible='<%#(((Common.CastAsInt32(Eval("ApprovalFwdTo"))==LoginId) || ((Common.CastAsInt32(Eval("ApprovalFwdTo"))==-1) && (Common.CastAsInt32(Session["loginid"])==19 || Common.CastAsInt32(Session["loginid"])==116))) && (Eval("Result").ToString()==""))%>' ></asp:Button>--%>
                                <asp:Button runat="server" ID="btnPopupApprove" onClick="btnPopupApprove_Click" CommandArgument='<%#Eval("TableID") %>' CssClass="btn" Text="Approve" Visible='<%# ApprovaVisibility(Common.CastAsInt32(Eval("ApprovalFwdTo")),Eval("Result").ToString(),Common.CastAsInt32(Eval("ApprovalLevelID")) ) %>' ></asp:Button>                                
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        <%--</div>--%>
         <div style="clear:both"></div>
        <%--</div>--%>
        
        <div style="padding:5px;">
              <asp:Button ID="btnPopupSendForApproval" CssClass="btn" runat="server" Text="Submit For Approval" OnClick="btnPopupSendForApproval_OnClick" />
              <asp:Button ID="btnSendBacktoProposal" CssClass="btn" runat="server" Text="Back To Proposal Stage" OnClientClick="return window.confirm('Are you sure to send back to proposal stage?');"  OnClick="btnSendBacktoProposal_OnClick" />
        </div>
        

    <%------------------------------------------------------------------------------------------------------------%>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; " id="divSenForApprovalList" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:800px; padding :0px; text-align :center;background : white; z-index:150;top:20px; border:solid 3px #d8e9f5;">
                <center >

                    <div style="padding:5px; " class="text headerband"><b>
                        Submit For Apporoval</b></div>
                    
                    <%--<div style="text-align:center;color:red;padding:10px;" >
                        <a href="Top4_Approval_Authority.pdf" target="_blank"> For Top 4 Approval authority please click here </a>
                    </div>--%>
                    <table width="100%" class="bordered">
                        <col width="150px" />
                        <col />
                    <asp:Repeater ID="rptApprovalLevelEntry" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td> <%#Eval("ApprovalLevelText") %> 
                                    <b> <asp:HiddenField ID="hfdApprovalLevel" runat ="server" Value='<%#Eval("ApprovalLevelID") %> ' /></b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlApprovalPerson" runat="server" DataSource=<%# BindUserList( Common.CastAsInt32(Eval("ApprovalLevelID")) ) %> DataTextField="UserName" DataValueField="LoginID" Width="50%" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovalPerson_OnSelectedIndexChanged"  > </asp:DropDownList>

                                    <div style="color:red;width:150px;display:inline;">
                                        <asp:Label ID="lblCrewLeaveStatus" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                        <tr>
                            <td colspan="2">
                                <div style="padding:2px;"><b>Remarks:-</b></div>
                                <asp:TextBox ID="txtCommentSendForApproval" runat="server" TextMode="MultiLine" Rows="7" Width="95%"> </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="padding:5px;">
                            <asp:Label ID="lblMsgSendForApprovalList" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="padding:5px;">
                        <asp:Button ID="btnSendForApproval" style=" background-color:#333; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Save" OnClick="btnSendForApproval_OnClick"  />
                        <asp:Button ID="btnClose_divSenForApprovalList" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnClose_divSenForApprovalList_Click" />
                    </div>
                    </div>
                </center>
            </div>        
    <%------------------------------------------------------------------------------------------------------------%>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; " id="divApprovalScreen" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:800px; padding :0px; text-align :center;background : white; z-index:150;top:20px; border:solid 3px #d8e9f5;">
                <center >
                     <div style="padding:5px; background:#d8e9f5"><b>
                        Crew Approval</b>
                     </div>
                   
                    <table width="100%" border="0" cellpadding="4" cellspacing="3" >
                        <col width="80px" />
                        <col />
                        <tr>
                            <td colspan="2">
                                <div style="padding:2px;"><b>Comments :-</b> </div> 
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="8" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Result</td>
                            <td>
                                <asp:RadioButtonList ID="rdolistResult" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="A" Text="Approval" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="R" Text="Reject" ></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <div style="padding:5px;">
                        <asp:Label ID="lblMsgApprovalScreen" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="padding:5px;">
                        <asp:Button ID="btnSaveApprovalScreen" style=" background-color:#333; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Save" OnClick="btnSaveApprovalScreen_OnClick"  />
                        <asp:Button ID="btnClosedivApprovalScreen" style=" background-color:RED; color:White; border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnClosedivApprovalScreen_OnClick" />
                    </div>
                    </center>
                    </div>
                </center>
            </div>        

    <%--</div>--%>
    <div style="text-align: center; width:90%; padding:5px; text-align:right">
    <asp:Label runat="server" ID="lblMess" style="float:left" ForeColor="Red" Font-Bold="true"></asp:Label>
     
    </div>
    <%--</center>--%>
    </form>
</body>
</html>
