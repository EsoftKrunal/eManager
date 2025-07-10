<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrewMatrixUpload.ascx.cs" Inherits="VesselRecord_CrewMatrixUpload" %>
<style type="text/css">
    input[type=text]
    {
    	width:80px;
    	text-align :center ;
    }
    textarea
    {
    	text-align :left ;
    }
    select
    {
    	font-size:12px; 
    	width:200px; 
    }
</style>
<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />

 <table width="100%" cellpadding="0" cellspacing="0" style=" font-family:Arial;font-size:12px;" > 
            <tr>
                <td colspan="3" style=" text-align :center "  >
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            <tr>
            <td colspan="3">
            <table width="100%" cellpadding ="0" cellspacing ="0" >
            <tr>
            <td style="text-align: right; width :100px;">IMO # : </td>
            <td style="width :100px;" ><asp:Label runat="server" id="lblImo"></asp:Label></td>
            <td style="text-align: right; width :100px;">Crew Matrix :</td>
            <td  style="text-align: right; width :100px;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Text="Officer" Value="OfficerCrew"></asp:ListItem>
                <asp:ListItem Text="Engineer" Value="EngineerCrew">Engineer</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width :100px;">Account Id :</td>
            <td style="text-align: right; width :100px;"><asp:TextBox BackColor="LightYellow" 
                    runat="server" ID="txtAccountId" MaxLength="10"></asp:TextBox> </td>
            <td style="text-align: right; width :100px;">OCIMF User Id :</td>
            <td style="text-align: left; width :100px;"><asp:TextBox BackColor="LightYellow" runat="server" ID="txtUserId" MaxLength="10"></asp:TextBox> </td>
            <td style="text-align: right; width :100px;">Password :</td>
            <td style="text-align: left; width :100px;"><asp:TextBox BackColor="LightYellow" runat="server" ID="txtPassword" MaxLength="10"></asp:TextBox> </td>
            </tr>
            </table>
            </td>
            </tr>
            <tr>
                <td colspan="3" valign="top" style=" border :solid 1px gray" >
                    <div style="width:640px; float :left; " >
                    <div style =" height :215px;">
                    <table cellpadding="2" cellspacing="0" width="640px" style ="background-color : #4371a5; height :19px; color :White">
                <col width="16px"/>
                <col width="50px"/>
                <col width="390px"/>
                <col width="120px"/>
                <col width="120px"/>
                <tr class= "headerstylegrid">
                <td>&nbsp;</td>
                <td>Emp#</td>
                <td>Name</td>
                <td>Rank</td>
                <td>Nationality</td>
                </tr>
                </table>
                    <asp:GridView BorderWidth="1" ShowHeader="false" BorderColor="Black" ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="Horizontal" CellPadding="2" OnSelectedIndexChanged="SelectIndexChanged"  >
                    <Columns>
                        <asp:CommandField ButtonType="Image" ShowSelectButton="true"  SelectImageUrl="~/Modules/HRD/Images/hourglass.gif" ItemStyle-Width="16px"/>    
                        <asp:BoundField DataField="CrewNumber" HeaderText="Emp#" ItemStyle-Width="50px"/>
                        <%--<asp:BoundField DataField="CrewName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="390px"/>--%>
                        <asp:TemplateField HeaderText="Name" >
                        <ItemTemplate>
                            <asp:Label ID="lblCName" runat ="server" Text='<%#Eval("CrewName")%>'></asp:Label>
                            <span style="color:Red" ><%#Eval("Star")%></span>
                        </ItemTemplate>
                        <ItemStyle Width="390px" HorizontalAlign ="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rank" HeaderText="Rank" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px"/>
                        <asp:TemplateField  ItemStyle-Width="120px">
                        <ItemTemplate>
                           <%#Eval("Nationality")%> 
                            <asp:HiddenField runat="server" ID="hfdcrewid" Value='<%# Eval("crewid")%>'/>
                            <asp:HiddenField runat="server" ID="hfdcrewnumber" Value='<%# Eval("crewnumber")%>'/>
                            <asp:HiddenField runat="server" ID="hfdcrewname" Value='<%# Eval("crewname")%>'/>
                            <asp:HiddenField runat="server" ID="hfdrank" Value='<%# Eval("rank")%>'/>
                            <asp:HiddenField runat="server" ID="hfdnationality" Value='<%# Eval("nationality")%>'/>
                            <asp:HiddenField runat="server" ID="hfdcertcomp" Value='<%# Eval("certcomp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdissuingcountry" Value='<%# Eval("issuingcountry")%>'/>
                            <asp:HiddenField runat="server" ID="hfdadminaccept" Value='<%# Eval("adminaccept")%>'/>
                            <asp:HiddenField runat="server" ID="hfdtankercert" Value='<%# Eval("tankercert")%>'/>
                            <asp:HiddenField runat="server" ID="hfdstcw" Value='<%# Eval("stcw")%>'/>
                            <asp:HiddenField runat="server" ID="hfdradioqual" Value='<%# Eval("radioqual")%>'/>
                            <asp:HiddenField runat="server" ID="hfdoperatorexp" Value='<%# Eval("operatorexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdrankexp" Value='<%# Eval("rankexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdtankertypeexp" Value='<%# Eval("tankertypeexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdalltypeexp" Value='<%# Eval("alltypeexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdmonthtour" Value='<%# Eval("monthtour")%>'/>
                            <asp:HiddenField runat="server" ID="hfdsignondate" Value='<%# Eval("signondate")%>'/>
                            <asp:HiddenField runat="server" ID="hfdengprof" Value='<%# Eval("engprof")%>'/>
                            <asp:HiddenField runat="server" ID="hfdUploadBy" Value='<%# Eval("UploadBy")%>'/>
                            <asp:HiddenField runat="server" ID="hfdUploadOn" Value='<%# Eval("UploadOn")%>'/>
                         </ItemTemplate> 
                        </asp:TemplateField> 
                    </Columns> 
                    <HeaderStyle BackColor="#C2C2C2" ForeColor="Black"/>
                    <SelectedRowStyle BackColor="#C2C2C2" /> 
                    <AlternatingRowStyle BackColor ="White" />  
                    </asp:GridView>
                    </div>
                    <table cellpadding ="0" cellspacing ="0" width="100%px" > 
                    <tr><td style=" padding-bottom :2px; padding-left :5px" >
                        <span style="color:Red" >*</span> Crew Members not uploaded to OCIMF Website.</td>
                    <td style=" vertical-align:bottom; padding-bottom :5px" >
                        &nbsp;</td>
                    </tr>
                    <tr><td style=" padding-bottom :5px" >
                    <asp:TextBox runat="server" ID="txtComment" Width="500px" Height="50px" ></asp:TextBox> 
                     
                    </td>
                    <td style=" vertical-align:bottom; padding-bottom :5px" >
                      <asp:Button ID="Button2" CssClass="btn"  runat="server" Width="120px"  Text="Upload Comments" />
                    </td>
                    </tr>
                    <tr>
                    <td style=" padding-bottom :5px;" colspan="2">
                    <table width="100%" cellpadding="0" cellspacing="0" border="1" style=" border:soild 1px black; border-collapse:collapse">
                    <col style =" background-color : #C2C2C2;" />
                    <col style =" text-align :center   " />
                    <col style =" text-align :center   " />
                    <tr  class= "headerstylegrid" style="  font-weight:bold; text-align :center " >
                    <td>Vessel Matrix Status</td>
                    <td>Master + C/O(Yrs)</td>
                    <td>Chief Engineer + 1 A/E(Yrs)</td>
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
                    </td>
                    </tr>
                    </table>
                    </div>
                    <div style="float :right;color :#003366  " >
                    <table cellspacing="4">
                    <tr><td>Certificate of competency</td>
                    <td>
                        <asp:HiddenField runat="server" ID="hfdcrewid" />
                        <asp:HiddenField runat="server" ID="hfdrank" />
                        <asp:HiddenField runat="server" ID="hfdnationality" />
                        
                        <asp:DropDownList style=" float :left" runat="server" ID="ddlCertComp">
                            <asp:ListItem Text="<Select>" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Class 1" Value="Class 1"></asp:ListItem>  
                            <asp:ListItem Text="Class 2" Value="Class 2"></asp:ListItem>  
                            <asp:ListItem Text="OOW" Value="OOW"></asp:ListItem>  
                            <asp:ListItem Text="EOOW" Value="EOOW"></asp:ListItem>  
                            <asp:ListItem Text="Master II/2" Value="Master II/2"></asp:ListItem>  
                            <asp:ListItem Text="Master II/3" Value="Master II/3"></asp:ListItem>  
                            <asp:ListItem Text="Chief Mate II/2" Value="Chief Mate II/2"></asp:ListItem>  
                            <asp:ListItem Text="OOW (Deck) II/1" Value="OOW (Deck) II/1"></asp:ListItem>  
                            <asp:ListItem Text="OOW (Deck) II/3" Value="OOW (Deck) II/3"></asp:ListItem>  
                            <asp:ListItem Text="Chief Eng III/2" Value="Chief Eng III/2"></asp:ListItem>  
                            <asp:ListItem Text="Chief Eng III/3" Value="Chief Eng III/3"></asp:ListItem>  
                            <asp:ListItem Text="Second Eng III/2" Value="Second Eng III/2"></asp:ListItem>  
                            <asp:ListItem Text="Second Eng III/3" Value="Second Eng III/3"></asp:ListItem>  
                            <asp:ListItem Text="OOW (Eng) III/1" Value="OOW (Eng) III/1"></asp:ListItem>  
                        </asp:DropDownList> 
                         <a runat="server" id="ancCertificate" title="Crew Licences Summary">
                        <img src="../Images/help1.png" style="clear:both ;float :right; border :none" alt="Help"/> 
                        </a>               
                      
                    </td></tr>
                    <tr><td>Issuing country</td><td>
                    <asp:DropDownList runat="server" ID="ddlIssuingCountry" ></asp:DropDownList> 
                    </td></tr>
                    <tr><td>Adminstration acceptance</td>
                    <td>
                    <asp:DropDownList runat="server" ID="ddlAdminAccept">
                            <asp:ListItem Text="<Select>" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>  
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>  
                            <asp:ListItem Text="Applied For" Value="Applied For"></asp:ListItem> 
                        </asp:DropDownList> 
                    </td></tr>
                    <tr><td>Tanker certification</td><td>
                    <asp:DropDownList style=" float :left" runat="server" ID="ddlTankerCert">
                            <asp:ListItem Text="<Select>" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Oil" Value="Oil"></asp:ListItem>  
                            <asp:ListItem Text="Chemical" Value="Chemical"></asp:ListItem>  
                            <asp:ListItem Text="Gas" Value="Gas"></asp:ListItem> 
                            <asp:ListItem Text="Oil and Gas" Value="Oil and Gas"></asp:ListItem>  
                            <asp:ListItem Text="Oil and Chemical" Value="Oil and Chemical"></asp:ListItem>  
                            <asp:ListItem Text="Gas and Chemical" Value="Gas and Chemical"></asp:ListItem> 
                            <asp:ListItem Text="Oil, Chemical and Gas" Value="Oil, Chemical and Gas"></asp:ListItem>  
                        </asp:DropDownList>
                          <a runat="server" id="ancDCE" title="Crew DCE Summary">
                        <img src="../Images/help1.png" style=" clear:both ;float :right; border :none" alt="Help"/> 
                        </a>
                    </td></tr>
                    <tr><td>STCW V Para</td><td>
                     <asp:DropDownList runat="server" ID="ddlSTCW">
                            <asp:ListItem Text="<Select>" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Basic" Value="Basic"></asp:ListItem>  
                            <asp:ListItem Text="Advanced" Value="Advanced"></asp:ListItem>  
                            <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                        </asp:DropDownList> 
                    </td></tr>
                    <tr><td>Radio qualification</td><td>
                     <asp:DropDownList runat="server" ID="ddlRadio">
                            <asp:ListItem Text="<Select>" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>  
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>  
                            <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                        </asp:DropDownList>
                        </td></tr>
                    <tr>
                        <td>Years with operator</td>
                        <td><asp:TextBox runat="server" ID="txtOpertor" style="float :left"></asp:TextBox>
                          <a runat="server" id="ancExperience" title="Crew Experience Summary">
                        <img src="../Images/help1.png" style="clear:both ;float :right; border :none" alt="Help"/> 
                        </a>
                        </td>
                    </tr>
                    <tr>
                        <td>Years in rank</td>
                        <td><asp:TextBox runat="server" ID="txtRank"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Years on this type of tanker</td>
                        <td><asp:TextBox runat="server" ID="txtTanker"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Years on all types of tanker</td>
                        <td><asp:TextBox runat="server" ID="txtAllTanker"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Date joined this vesel</td>
                        <td><asp:TextBox runat="server" ID="txtJoinVessel" ReadOnly="true" ></asp:TextBox>
                            <asp:Label runat="server" ID="lblMonth" ></asp:Label></td>
                    </tr>
                    <tr>
                        <td>English proficiency</td>
                        <td><asp:DropDownList runat="server" ID="ddlEngProf">
                            <asp:ListItem Text="Good" Value="Good"></asp:ListItem>  
                            <asp:ListItem Text="Fair" Value="Fair"></asp:ListItem>  
                            <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem> 
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Last Upload On / By</td>
                        <td><asp:Label runat="server" ID="lblLastUpload"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                        <br />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn" onclick="Upload_Click"  />
                        <asp:Button ID="btnShowList" runat="server" Text="Matrix on OCIMF Site" Width="150px" CssClass="btn" onclick="CrewList_Click"  />
                        </td>
                    </tr>
                    </table>
                    </div>
                </td>
            </tr>
 </table>

