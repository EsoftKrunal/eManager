<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadVesselCertificate.aspx.cs" Inherits="UploadVesselCertificate" Title="Untitled Page" %>
<%@ Register Src="VesselCertificates.ascx" TagName="VesselCertificates" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Upload Excel File</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript">
    var Cur;
    function ShowPop(caller)
    {
    document.getElementById("dvChild").style.display="block";    
    document.getElementById("dvChild").style.zIndex=100;   
    Cur=caller; 
    return false;
    }
    function ResetAdd(value)
    {
    Cur.value=value;
    Cur.src='../Images/plus.gif';
    Cur.title='< Add This >';
     var txtId=Cur.getAttribute("ID");
    txtId=txtId.substring(0,txtId.indexOf('_img'));
    document.getElementById(txtId+"_txtId").setAttribute("value","-1");
    }
    function SetValue(value,name)
    {
    Cur.value=value;
    Cur.src='../Images/check.gif';
    Cur.title=name;
    var txtId=Cur.getAttribute("ID");
    txtId=txtId.substring(0,txtId.indexOf('_img'));
    document.getElementById(txtId+"_txtId").setAttribute("value",value);
    }
    function HidePop()
    {
    document.getElementById("dvChild").style.display="none";       
    return false;
    }
    </script> 
</head>
<body style="text-align: center;" >
<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" ></ajaxToolkit:ToolkitScriptManager> 
<table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
<tr>
<td align="center" style="background-color:#4371a5; height: 23px;text-align :center" class="text" >Upload Vessel Certificates 
&nbsp; [ <asp:Label runat="server" Text="" ID="lblVessel"></asp:Label> ]
</td>
</tr>
<tr>
<td style="height :470px; vertical-align :top; " >
<div style="width:99%">
<div id="dvChild" style="position:absolute;top:100px;left:500px;width:300px;height:300px;display :none;">
<div style="border:solid 1px black;background-color :White; overflow-x:hidden; overflow-y:scroll;">
<table style="border:solid 0px Gray; border-collapse :collapse" width="94%">
<tr><td>&nbsp;<img style="cursor :pointer" onclick="return ResetAdd('-1');" src="../Images/plus.gif" alt="Plus"/></td><td>&lt; ADD NEW &gt;  <img src="../Images/critical.gif" alt="Close" style ="float :right; cursor:pointer;" onclick="HidePop();" title="Close window." /></td></tr>
<asp:Repeater runat="server" ID="rptCerts" >
<ItemTemplate>
<tr><td>&nbsp;<img style="cursor :pointer" text='<%#Eval("CertName")%>' value='<%#Eval("CertId")%>' onclick='return SetValue(this.value,this.text);' src="../Images/check.gif" alt="Select"/></td><td><%#Eval("CertName")%></td></tr>
</ItemTemplate> 
</asp:Repeater> 
</table> 
</div>
</div>
<div id="dvMain" style="width :100%; position:relative; top:0;left:0; background-color :White" >
<script type="text/javascript" >
function OpenPopup(vslid,recid,mode)         
{
    if(parseInt(recid)>0) 
        window.open('CertPopUp.aspx?VId='+ vslid + "&RecId=" + recid +"&Mode=" + mode,null,'title=no,toolbars=no,scrollbars=yes,width=1000,height=250,left=20,top=20,addressbar=no');
} 
function OpenPopup2(vslid,recid,mode)         
{
    window.open('CertPopUp.aspx?VId='+ vslid + "&RecId=" + recid +"&Mode=" + mode,null,'title=no,toolbars=no,scrollbars=yes,width=1000,height=250,left=20,top=20,addressbar=no');
}
</script>
<div style="height:415px; width:100%;">
<table width="100%" style="background-color:#c2c2c2" >
<tr><td style="width:700px; text-align:left">
<%--<div style="width :100%; height :30px; text-align :center ;border:solid 1px red;" >--%>
Vessel :<asp:DropDownList runat="server" ID="ddlVessel" CssClass="required_box" 
        Width="150px" AutoPostBack="true" 
        onselectedindexchanged="ddlVessel_SelectedIndexChanged"></asp:DropDownList>  
<asp:FileUpload runat="server" ID="flp1" Width="300px" CssClass="btn" /> 
<asp:Button runat="server" CssClass="btn" ID="btnUpload" Text="Import Excel File" onclick="btnUpload_Click"/>  
<asp:Button runat="server" CssClass="btn" ID="btnSave" Text="Save" onclick="btnSave_Click"/>  
<%--</div>--%>
</td>
<td style="text-align:left"  >
<asp:Label runat="server" style=" text-align :left" ID="lblMsg" ForeColor="Red" Width="100%" ></asp:Label>
</td>
</tr>
</table>
<div style="width :100%; overflow-x:hidden; overflow-y:scroll; max-height : 430px; padding-top :5px; ">
    <asp:GridView ID="gv_VDoc" runat="server" AutoGenerateColumns="False" Style="text-align: center" Width="100%" GridLines="Horizontal" >
        <HeaderStyle CssClass="headerstylefixedheader" />
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle CssClass="rowstyle" />
        <SelectedRowStyle CssClass="selectedtowstyle" />
        <FooterStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField >
            <ItemTemplate>
                <asp:ImageButton runat="server" ID="btnRemove" ImageUrl="~/Images/delete1.gif" OnClientClick="javascript:return confirm('Are you sure to delete this record?');" OnClick="DeleteRow" CommandArgument='<%#Eval("VesselCertId")%>'/> 
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ME" HeaderText="Main/Extra"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <asp:BoundField DataField="CertCode" HeaderText="Cert. Code"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <asp:TemplateField HeaderText="Certificate Name">
                <ItemTemplate>
                    <asp:Label ID="lbl_Doc_Type" style='<%#(Eval("CertId").ToString()=="-1")?"color:Red":""%>' runat="server" Text='<%# Eval("CertName") %>'></asp:Label>
                    <asp:ImageButton ID="imgBtnAdd" runat="server" ToolTip="< Add This >" value='<%#Eval("CertId")%>' ImageUrl="~/Images/plus.gif" OnClientClick="return ShowPop(this);" AlternateText="Add" Visible='<%#(Eval("CertId").ToString()=="-1")%>'></asp:ImageButton> 
                    <asp:TextBox runat="server" style="display:none" ID="txtId" Value='<%#Eval("CertId")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdME" Value='<%#Eval("ME")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdCertNo" Value='<%#Eval("CertNumber")%>'/>
                    <asp:HiddenField runat="server" ID="hfdCertCode" Value='<%#Eval("CertCode")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdCType" Value='<%#Eval("CertType")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdIdate" Value='<%#Eval("IssueDate1")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdEDate" Value='<%#Eval("ExpiryDate1")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdPlace" Value='<%#Eval("PlaceIssued")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdLastAnn" Value='<%#Eval("LastAnnSurvey1")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdInt" Value='<%#Eval("NextSurveyInterval")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdNSD" Value='<%#Eval("NextSurveyDue1")%>'/> 
                    <asp:HiddenField runat="server" ID="hfdVesselCertId" Value='<%#Eval("VesselCertId")%>'/> 
                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="CertNumber" HeaderText="Certificate #"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <%--<asp:BoundField DataField="CertType" HeaderText="Cert.Type"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>--%>
            <asp:TemplateField HeaderText="Cert.Type">
            <ItemTemplate>
            <asp:Label ID="Label1" Text='<%#Eval("CertType")%>' style="text-align :center ; padding-left:30px;" runat="server"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IssueDate1" HeaderText="Issue Date" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <asp:BoundField DataField="ExpiryDate1" HeaderText="Expiry Date" DataFormatString="{0:D}" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <asp:BoundField DataField="PlaceIssued" HeaderText="Place Issued" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <asp:BoundField DataField="LastAnnSurvey1" HeaderText="Last Ann./Interim" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
            <%--<asp:BoundField DataField="NextSurveyInterval"  ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
            <asp:TemplateField HeaderText="Survey Int.">
            <ItemTemplate>
            <asp:Label Text='<%#Eval("NextSurveyInterval")%>' style="text-align :center ; padding-left:30px;" runat="server"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NextSurveyDue1" HeaderText="Next Survey Due" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
        </Columns>
    </asp:GridView>
</div>
</div>
</div>
</div>
</td> 
</tr> 
</table> 
</form> 
</body> 
</html> 


