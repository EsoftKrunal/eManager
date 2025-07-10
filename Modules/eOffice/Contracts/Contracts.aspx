<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contracts.aspx.cs" Inherits="emtm_Contracts_Emtm_Contracts" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">


    <div style="font-family:Arial;font-size:12px;">
       
          <table width="100%">
                <tr>
                  
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :7px; font-weight: bold;">
                            Contract Management
                        </div>
                        <div>
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </div>
                            <div style="">
                                <table cellpadding="0" cellspacing ="0" border="0" style=" border-collapse:collapse;" width ="100%">
                                <tr>
                                <td>
                                    <table border="0" cellspacing="0" cellpadding="2"  style="text-align: left; " width="100%">
                                        <col width="60px" />     
                                        <col />                                   
                                        <col width="60px" />                                        
                                        <col />
                                        <col width="100px" />                                        
                                        <col />
                                        <col width="200px" />                                        
                                        <tr>
                                            <td>Topic </td>
                                            <td>:&nbsp;<asp:TextBox ID="txtTopicFilter" runat="server" Width="250px"></asp:TextBox></td>
                                            <td> Category </td>
                                            <td>:&nbsp;<asp:DropDownList ID="ddlContractCategory" runat="server" Width="100px"></asp:DropDownList></td>
                                            <td> Applicable To </td>
                                            <td>:&nbsp;
                                                <asp:CheckBox runat="server" ID="chkOffice_s" Text="Office" />
                                                <asp:CheckBox runat="server" ID="chkShip_s" Text="Ship" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_Search" runat="server" CausesValidation="true" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="19" />
                                                <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="20" />
                                            </td>
                                        </tr>
                                        <tr>
                                           
                                            <td>Vendor</td>
                                            <td>:&nbsp;<asp:DropDownList ID="ddlSupplierFilter" runat="server" Width="250px"></asp:DropDownList></td>
                                            <td> Issued In </td>
                                            <td >
                                                :&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" Width="80px"></asp:TextBox> - <asp:TextBox ID="txtDateTo" runat="server" Width="80px"></asp:TextBox> 
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateTo" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>Expired In</td>
                                            <td>:&nbsp;<asp:TextBox ID="txtExpiredInDays" runat="server" Width="40px" style="text-align:center"></asp:TextBox>&nbsp;Days</td> 
                                            <td> <asp:Button ID="btnAddContractPopup" runat="server" CausesValidation="false" CssClass="btn" OnClick="btnAddContractPopup_Click" Text="Add" Width="150px" TabIndex="20" /></td>
                                        </tr>                                                                                                                        
                                        </table>
                                </td>
                                </tr>
                                 <tr>
                                 <td>
                                   <table cellpadding="0" cellspacing ="0" border ="0" width="100%"  >
                                        <tr>
                                            <td colspan="6">
                                                
                                                <center>
                                                    <asp:Label ID="lblGrid" runat="Server"></asp:Label>
                                                </center>
                                                <div style="padding:5px 5px 5px 5px;" >
                                                <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 32px ; text-align:center; border-bottom:none; background-color:#4371a5;">
                                                    <table border="0" cellpadding="4" cellspacing="0" class="bordered" style="width:100%;border-collapse:collapse; height:32px;">                               
                                                    <col style="width:40px;" />   
                                                    <col style="width:250px;" />
                                                    <col style="width:150px;" />
                                                    <col />     
                                                    <col style="width:85px;" />                                                        
                                                    <col style="width:85px;" /> 
                                                    <col style="width:70px;" />  
                                                       <tr align="left" class= "headerstylegrid"> 
                                                        <td>Sr#</td>
                                                        <td style="text-align:left;">Vendor</td>   
                                                        <td style="text-align:left;">Category</td>
                                                        <td style="text-align:left;">Topic</td>   
                                                       <td>Start Date</td>
                                                       <td>End Date</td> 
                                                       <td></td>
                                                       </tr>
                                               </table> 
                                            </div>          
                                            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT:360px ; text-align:center;">
                                                <table border="0" cellpadding="4" cellspacing="0" class="bordered" style="width:100%;border-collapse:collapse;">
                                                    <colgroup>
                                                    <col style="width:40px;" />   
                                                    <col style="width:250px;" />
                                                    <col style="width:150px;" />
                                                    <col />     
                                                   <col style="width:85px;" />                                                        
                                                   <col style="width:85px;" /> 
                                                        <col style="width:70px;" />                              
                                                                          
                                                 </colgroup>
                                                                          
                                                 <asp:Repeater ID="rptContractList" runat="server" >
                                                   <ItemTemplate>
                                                      <tr >
                                                          <td><%#Eval("sno")%></td>              
                                                                                 
                                                          <td align="left"><%#Eval("SupplierName")%></td>        
                                                          <td align="left"><%#Eval("ContractCatName")%></td>     
                                                           <td align="left"><%#Eval("Topic")%></td>                                                           
                                                           <td align="center"><%#Common.ToDateString(Eval("StartDate"))%></td>
                                                           <td align="center"><%#Common.ToDateString(Eval("EndDate"))%></td>
                                                           <td>
                                                              <asp:ImageButton ID="btnEditContract" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditContract_OnClick" CommandArgument='<%#Eval("ContractID") %>' Visible='<%#auth.IsUpdate%>' />
                                                              <asp:ImageButton ID="btnDeleteContract" runat="server" ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDeleteContract_OnClick" CommandArgument='<%#Eval("ContractID") %>'  Visible='<%#auth.IsDelete%>' OnClientClick="return confirm('Are you sure to delete this record?')" />
                                                              <asp:ImageButton ID="btnOpenDocumentPopup" runat="server" ImageUrl="~/Modules/HRD/Images/doc.png" OnClick="btnOpenDocumentPopup_OnClick" CommandArgument='<%#Eval("ContractID") %>'  />
                                                          </td> 
                                                       </tr>
                                                   </ItemTemplate>
                                                  </asp:Repeater>
                                            </table>
                                            </div> 
                                                </div>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                 </td>
                                 </tr>
                                 <tr>
                                 <td>
                                    </td>
                                 </tr>
                                 </table>
                       </div>  
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>

                                

                                <%-----------------------------------------------------------------------------------------%>
                                <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddContract" visible="false" >
                                    <center>
                                    <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
                                    <div style="position :relative; width:800px; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:40px;opacity:1;filter:alpha(opacity=100)">
                                    <center>
                                        <div style=" text-align :center; font-size :15px; background-color:#4371a5; color :White; padding :5px; font-weight: bold;">
                                            Add New Contract
                                        </div>                
                                        <div >
                                            <table cellpadding="4" cellspacing="4"border="0" width="100%">
                                                <colgroup>
                                                    <col width="150px" />
                                                    <col />
                                                    <tr>
                                                        <td>Topic</td>
                                                        <td>
                                                            <asp:TextBox ID="txtTopic" runat="server" Width="99%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Contract Category</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCategoryEntry" runat="server" Width="250px">
                                                            </asp:DropDownList>
                                                            <asp:ImageButton ID="ddlAddCategoryPopup" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClick="ddlAddCategoryPopup_OnClick" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>ContractStart Date</td>
                                                        <td>
                                                            <asp:TextBox ID="txtStartDate" runat="server" Width="80px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="BottomRight" TargetControlID="txtStartDate">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Contract End Date</td>
                                                        <td>
                                                            <asp:TextBox ID="txtEndDate" runat="server" Width="80px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="BottomRight" TargetControlID="txtEndDate">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Renewal Date</td>
                                                        <td>
                                                            <asp:TextBox ID="txtRenewal" runat="server" Width="80px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" PopupPosition="BottomRight" TargetControlID="txtRenewal">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Vendor</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSupplier" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Proposed By</td>
                                                        <td>
                                                            <asp:TextBox ID="txtProposedBy" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Approved By</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align:top;">Certificate applied on</td>
                                                        <td>
                                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOffice" runat="server" AutoPostBack="true" OnCheckedChanged="chkOffice_OnCheckedChanged" Text="Office" />
                                                                        <asp:CheckBox ID="chkShip" runat="server" AutoPostBack="true" OnCheckedChanged="chkShip_OnCheckedChanged" Text="Ship" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div id="divchklistOffice" runat="server" style="padding:0px;overflow-x:hidden;overflow-y:hidden;border:solid 1px #c2c2c2" visible="false">
                                                                            <div style="padding:4px;background-color:#c2c2c2;color:black;font-weight:bold;">
                                                                                Select office
                                                                            </div>
                                                                            <asp:CheckBoxList ID="chklistOffice" runat="server" CellPadding="5" RepeatDirection="Horizontal" Width="100%">
                                                                            </asp:CheckBoxList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div id="divchklistShip" runat="server" style="height:80px;padding:0px;overflow-x:hidden;overflow-y:scroll;border:solid 1px #c2c2c2" visible="false">
                                                                            <div style="padding:4px;background-color:#c2c2c2;color:black;font-weight:bold;">
                                                                                Select ship
                                                                            </div>
                                                                            <asp:CheckBoxList ID="chklistShip" runat="server" CellPadding="5" RepeatColumns="14" RepeatDirection="Horizontal" Width="100%">
                                                                            </asp:CheckBoxList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                                
                                            </table>

                                            <div style="text-align:center;padding:5px;">
                                                <asp:Button ID="btnSvaeContract" runat="server" Text="Save"  CssClass="btn" OnClick="btnSvaeContract_Click" />
                                                <asp:Button ID="btnCloseContractPopup" runat="server" Text="Close"  CssClass="btn" OnClick="btnCloseContractPopup_Click" />
                                            </div>
                                            <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                                                &nbsp; <asp:Label ID="lblMsgAddContract" runat="server" CssClass="error" style="color:red;font-weight:bold;"></asp:Label>
                                            </div>
                        
                                        </div>
                                    </center>
                                    </div>
                                    </center>
                                </div>

                                <%-----------------------------------------------------------------------------------------%>
                                <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="divAddCagegory" visible="false" >
                                <center>
                                    <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
                                    <div style="position :relative; width:450px; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:110px;opacity:1;filter:alpha(opacity=100)">
                                    <center>
                                        <div style=" text-align :center; font-size :15px; background-color:#4371a5; color :White; padding :5px; font-weight: bold;">
                                            Add New Category
                                        </div>    
                                        <br />
                                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                            <colgroup>
                                                <col width="120px" />
                                                <col />
                                                <tr>
                                                    <td style="text-align:right">Category</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCategory" runat="server" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        <br />
                                        <div style="text-align:center;padding:5px;">                    
                                            <asp:Button ID="btnAddCategory" runat="server" Text="Save"  CssClass="btn" OnClick="btnAddCategory_Click" />
                                            <asp:Button ID="btnCloseCategoryPopup" runat="server" Text="Close"  CssClass="btn" OnClick="btnCloseCategoryPopup_Click" />
                                        </div>
                                        <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                                            &nbsp; <asp:Label ID="lblMsgAddCategory" runat="server" CssClass="error" style="color:red;font-weight:bold;"></asp:Label>
                                        </div>
                                    
                                    </center>
                                    </div>
                                </center>
                                </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnCloseContractPopup" />
                                </Triggers>
                            </asp:UpdatePanel>

                                <%-- Documents 111111111111---------------------------------------------------------------------------------------%>
                                 <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="divDocuments" visible="false" >
                                <center>
                                    <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)"></div>
                                    <div style="position :relative; width:650px; padding :0px; text-align :center; border :solid 3px #4371a5; background : white; z-index:150;top:50px;opacity:1;filter:alpha(opacity=100)">
                                    <center>
                                        <div style=" text-align :center; font-size :15px; background-color:#4371a5; color :White; padding :5px; font-weight: bold;">
                                            <div>Attached Documents</div>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                               <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDocumentName" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuDocuments" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSaveDocuments" runat="server" Text="Add Document" OnClick="btnSaveDocuments_OnClick" />
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                         <div class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden;text-align:left;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                    <col style="width:30px;" /> 
                                                    <col style="width:40px;" /> 
                                                    <col style="width:370px;" />                       
                                                    <col /> 
                                                    <tr align="left" class= "headerstylegrid"> 
                                                        <td>
                                                            
                                                        </td>
                                                        <td style="text-align:left;" >Sr.#</td>
                                                        <td style="text-align:left;" >Document Name</td>
                                                        <td style="text-align:left;" >File Name</td>
                                                    </tr>
                                                    </table>
                                            </div>
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT:200px; text-align:center;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                    <colgroup>
                                                    <col style="width:30px;" /> 
                                                    <col style="width:40px;" /> 
                                                    <col style="width:370px;" />                                          
                                                    <col /> 
                                                 </colgroup>
                                                 <asp:Repeater ID="rptDocuments" runat="server" >
                                                   <ItemTemplate>
                                                      <tr >              
                                                          <td>
                                                              <asp:ImageButton ID="btnDeleteDocument" runat="server" ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDeleteDocument_OnClick" CommandArgument=<%#Eval("documentID") %> OnClientClick="return confirm('Are you sure to delete this record?')" />
                                                          </td>
                                                          <td align="left"><%#Eval("RowNo")%></td>
                                                           <td align="left"><%#Eval("DocumentName")%></td>
                                                           <td align="left">
                                                               <asp:LinkButton ID="lnkDownloadDocuments" runat="server" Text='<%#Eval("FileName")%>' OnClick="lnkDownloadDocuments_OnClick" CommandArgument='<%#Eval("DocumentID")%>'></asp:LinkButton>
                                                           </td>     
                                                       </tr>
                                                   </ItemTemplate>
                                                  </asp:Repeater>
                                            </table>
                                            </div> 


                                        <br />
                                        <div style="text-align:center;padding:5px;">                    
                                            <asp:Button ID="btnCloseDocumentsPopup" runat="server" Text="Close"  CssClass="btn" OnClick="btnCloseDocumentsPopup_Click" />
                                        </div>

                                        <div style="text-align:center;padding:5px; background-color:#e5e5e5">
                                            &nbsp; <asp:Label ID="lblMsgDocuments" runat="server" CssClass="error" style="color:red;font-weight:bold;"></asp:Label>
                                        </div>
                                    
                                    </center>
                                    </div>
                                </center>
                                </div>
                           <%-- </ContentTemplate>
                            <Triggers >
                                <asp:PostBackTrigger ControlID="btnSaveDocuments" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
               </tr>
         </table>  
         
         
    
    </div>
        

   </asp:Content>
