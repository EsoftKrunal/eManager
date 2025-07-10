<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InspectionHome.aspx.cs" Inherits="Modules_LPSQE_Transactions_InspectionHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="../../JS/Common.js"></script>
    <style  type="text/css" >
    #mask {
      position:absolute;
      left:0;
      top:0;
      z-index:9000;
      background-color:#000;
      display:none;
    }
      
    #boxes .window {
      position:absolute;
      left:0;
      top:0;
      width:440px;
      height:200px;
      display:none;
      z-index:9999;
      padding:20px;
    }

    #boxes #dialog {
      width:375px; 
      height:203px;
      padding:10px;
      background-color:#ffffff;
    }

    #boxes #dialog1 {
      width:375px; 
      height:203px;
    }

    #dialog1 .d-header {
      background:url(images/login-header.png) no-repeat 0 0 transparent; 
      width:375px; 
      height:150px;
    }

    #dialog1 .d-header input {
      position:relative;
      top:60px;
      left:100px;
      border:3px solid #cccccc;
      height:22px;
      width:200px;
      font-size:15px;
      padding:5px;
      margin-top:4px;
    }

    #dialog1 .d-blank {
      float:left;
      background:url(images/login-blank.png) no-repeat 0 0 transparent; 
      width:267px; 
      height:53px;
    }

    #dialog1 .d-login {
      float:left;
      width:108px; 
      height:53px;
    }

    #boxes #dialog2 {
      background:url(../Images/notice.png) no-repeat 0 0 transparent; 
      width:326px; 
      height:229px;
      padding:50px 0 20px 25px;
    }
    </style>
     <style type="text/css">
.selbtn
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;

	
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
      <script language="javascript" type="text/javascript">
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
// prtContent.innerHTML=strOldOne;
}
function uploadError()
    {
    alert('Unable to upload.'); 
    }
    function uploadComplete()
    {
    alert('Uploaded successfully.');
    }

      </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
        <div style="text-align: center">   
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center" >
                   <div style=' font-size:16px; margin-top:5px; font-family:Arial;' class="text headerband" >Inspection Record
                   </div>
               </td>
            </tr>
            <tr runat="server" id="tr_Obsrevations">
                <td>
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr >
                            <td style="padding-right:3px;  padding-left:3px;padding-top:3px;padding-bottom:3px;text-align:left;">
                                <div id="header">
                                 
                                      <asp:Button ID ="btnInsSearch" runat="server" CommandArgument="0" Text="Inspection" OnClick="btnTabs_Click" CssClass="btn1" Visible="false"  />
                                      &nbsp;
                                    <asp:Button ID ="btnInsPlanning" runat="server" CommandArgument="1"  Text="Planning" OnClick="btnTabs_Click" CssClass="btn1" Visible="false"/>
                                    &nbsp;
                                    <asp:Button ID ="btnInsTravelSchedule" runat="server" CommandArgument="2" Text="Execution"  OnClick="btnTabs_Click" CssClass="btn1" Visible="false" />
                                     &nbsp;
                                    <asp:Button ID ="btnInsResponse" runat="server" CommandArgument="3" Text="Observation" OnClick="btnTabs_Click" CssClass="btn1" Visible="false" />
                                     &nbsp;
                                    <asp:Button ID ="btnInsDocs" runat="server" CommandArgument="4" Text="Documents" OnClick="btnTabs_Click" CssClass="btn1" Visible="false" />
                                     &nbsp;
                                    <asp:Button ID ="btnInsExpenses" runat="server" CommandArgument="5" Text="Expenses" OnClick="btnTabs_Click" CssClass="btn1" Visible="false" />
                                     &nbsp;
                                    <asp:Button ID ="btnInsCloser" runat="server" CommandArgument="6" Text="Inspection Closure" OnClick="btnTabs_Click"  CssClass="btn1" Visible="false"/>
                                     &nbsp;
                                </div>
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 10px;">
                            
                          <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/Inspection/InspectionSearch.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>
                           
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td> 
        </tr>
        </table>  

     </div>
</asp:Content>


