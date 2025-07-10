<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewUploadPopUp.aspx.cs" Inherits="VesselRecord_CrewUploadPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" >
    function sendvalue()
    {
        var ans=document.getElementById('ddlAns').selectedIndex;
        var crl='<%=Request.QueryString["postback"]%>';
        var win=parent;   
        if(win==null)
            win=parent.frames[1];
        win.document.getElementById(crl).options[ans].selected=true;
        win.tb_remove()
    }
    
     function sendvalue1()
    {
        var ans=document.getElementById('ddlDCE').selectedIndex;
        var crl='<%=Request.QueryString["postback"]%>'; 
       var win=parent;   
        if(win==null)
            win=parent.frames[1];
        win.document.getElementById(crl).options[ans].selected=true;
        win.tb_remove()
    }
    </script> 
    <style type="text/css" >
        #tabcrew
        {
        	width :100%;
        	font-family :Arial;
        	font-size :12px; 
        	border :solid 1px black;
        }
        #tabcrew #trhead
        {
        	height :25px;
        	background-color :Gray ;
        	color:White;
        }
        td
        {
        	text-align:left ; 
        }
        select
        {
        	font-family: Verdana ;
        	font-size :12px; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" width :100%" >
    <table runat="server" id="tbCrew" width="100%" style ="background-color:#e8e8e8;color:#4371a5; font-weight : bold  " cellpadding="0" cellspacing ="0" >
    <tr>
    <td style ="text-align:right">EMP # :&nbsp;&nbsp;</td>
    <td style ="text-align:left"><asp:Label id="lblCrew" runat="server"></asp:Label></td>
    <td style ="text-align:right">Name :&nbsp;&nbsp;</td>
    <td style ="text-align:left"><asp:Label id="lblCrewName" runat="server" ></asp:Label></td>
    </tr>
    </table>
    
    <center style="font-size :smaller;">
    <asp:Label runat="server" ID="lblHeader" Font-Size="Large"></asp:Label> 
    <div runat="server" id="dvExp" visible="false">
        <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdExp" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" >
        <Columns>
        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ItemStyle-HorizontalAlign="Left"/>   
        <asp:BoundField DataField="RankCode" HeaderText="Rank"/>   
        <asp:BoundField DataField="VesselName" HeaderText="Vessel" ItemStyle-HorizontalAlign="Left" />   
        <asp:BoundField DataField="VesselType" HeaderText="Vessel Type" ItemStyle-HorizontalAlign="Left"/>   
        <asp:BoundField DataField="SignOnDate" HeaderText="Sign On Dt." />   
        <asp:BoundField DataField="SignOffDate" HeaderText="Sign Off Dt." />   
        <asp:BoundField DataField="Duration(Month)" HeaderText="Duration(Month)" />   
        <asp:BoundField DataField="GRT" HeaderText="GRT" />   
        <asp:BoundField DataField="BHP" HeaderText="BHP" />   
        
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="25px"  />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
    </div>
    <div runat="server" id="dvLicence" visible="false">
        <div style="height:330px;" >
        <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdLicence" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" >
        <Columns>
        <asp:TemplateField HeaderText="" ItemStyle-Width="40px" >   
            <ItemTemplate>
                <a target="_blank" href='../EMANAGERBLOB/HRD/Documents/Professional/<%#Eval("ImagePath")%>' style="" ><img src="../Images/paperclip.gif" style="<%#(Eval("ImagePath").ToString()=="")?"display:none":""%>;border:none" alt="File" /> </a> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="COC" HeaderText="COC" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px" />   
        <asp:BoundField DataField="LicenseName" HeaderText="Licence Name" ItemStyle-HorizontalAlign="Left" />   
        <asp:BoundField DataField="Grade" HeaderText="Grade" ItemStyle-HorizontalAlign="Left" />   
        <asp:BoundField DataField="Issuing Country" HeaderText="Issuing Country" ItemStyle-HorizontalAlign="Left"/>   
        <asp:BoundField DataField="Number" HeaderText="Number" />   
        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" />   
        <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" />   
        <asp:BoundField DataField="Verified" HeaderText="Verified" />   
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="25px"  />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
        </div>
        <div style=" height:30px; background-color :#c2c2c2; padding-top : 10px;" >
        <select id="ddlAns">
        <option selected="selected" value="">&lt;Select&gt;</option>
	    <option value="Class 1">Class 1</option>
	    <option value="Class 2">Class 2</option>
	    <option value="OOW">OOW</option>
	    <option value="EOOW">EOOW</option>
        </select> 
        &nbsp;&nbsp;
        <button class="btn" onclick="sendvalue();" >Go >> </button>  
        </div>
    </div>
    <div runat="server" id="dvDCE" visible="false">
        <div style="height:330px;" >
        <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdDCE" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" >
        <Columns>
          <asp:TemplateField HeaderText="" ItemStyle-Width="40px" >   
            <ItemTemplate>
                <a target="_blank" href='../EMANAGERBLOB/HRD/Documents/Professional/<%#Eval("ImagePath")%>' style="" ><img src="../Images/paperclip.gif" style="<%#(Eval("ImagePath").ToString()=="")?"display:none":""%>;border:none" alt="File" /> </a> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Endorsement Name" HeaderText="Endorsement Name" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Number" HeaderText="Number" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="PlaceofIssue" HeaderText="Place Of Issue" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="GradeLevel" HeaderText="Grade Level" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" />   
        <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" />  
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="25px"  />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
        </div>
        <div style=" height:30px; background-color :#c2c2c2; padding-top : 10px;" >
        <select id="ddlDCE">
        <option selected="selected" value="">&lt;Select&gt;</option>
	    <option value="Oil">Oil</option>
	    <option value="Chemical">Chemical</option>
	    <option value="Gas">Gas</option>
	    <option value="Oil and Gas">Oil and Gas</option>
	    <option value="Oil and Chemical">Oil and Chemical</option>
	    <option value="Gas and Chemical">Gas and Chemical</option>
	    <option value="Oil, Chemical and Gas">Oil, Chemical and Gas</option>

        </select> 
        &nbsp;&nbsp;
        <button class="btn" onclick="sendvalue1();" >Go >> </button>  
        
        </div>
        
    </div>
    <div runat="server" id="dvLIT" visible="false"> 
        <asp:Literal runat="server" ID="litCrew"></asp:Literal>   
    </div>
    </center>
    </div>
    </form>
</body>
</html>
