<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI_Result_Company.aspx.cs" Inherits="emtm_kpi_result_company" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="../JS/jquery-1.10.2.js"></script>
     <script type="text/javascript" src="./jqscript.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
    <style type="text/css">

.fade {
    animation: fadein 2s;
    -moz-animation: fadein 2s; /* Firefox */
    -webkit-animation: fadein 2s; /* Safari and Chrome */
    -o-animation: fadein 2s; /* Opera */
}
@keyframes fadein {
    from {
        opacity:0;
    }
    to {
        opacity:1;
    }
}
@-moz-keyframes fadein { /* Firefox */
    from {
        opacity:0;
    }
    to {
        opacity:1;
    }
}
@-webkit-keyframes fadein { /* Safari and Chrome */
    from {
        opacity:0;
    }
    to {
        opacity:1;
    }
}
@-o-keyframes fadein { /* Opera */
    from {
        opacity:0;
    }
    to {
        opacity: 1;
    }
}

        select,input[type='text'] 
        {
            height:27px;
            padding:3px;
            line-height:27px;
            border:solid 1px #e9e9e9;
            color:#333;
        }
        .kpiname {
            color: #333;
            cursor:pointer;
        }
        .kpiname:hover {
            color: #03467a;
            font-weight: bold;                
        }
        .nolink
        {
            text-decoration:none;
        }
        .panel {
            margin: 10px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            border:none;  
        }
        .panel .header  {
            -moz-border-radius-topleft: 5px;
            -webkit-border-top-left-radius: 5px;
            border-top-left-radius: 5px;

            -moz-border-radius-topright: 5px;
            -webkit-border-top-right-radius: 5px;
            border-top-right-radius: 5px;
            font-size: large;
            border:none;  
            padding:10px;
            color:#ffffff;
        }
            .panel .header .close{
                float:right;
                margin-top:2px;
            }
        .panel .content  {
            padding:1px;
            -moz-border-radius-bottomleft: 5px;
            -webkit-border-bottom-left-radius: 5px;
            border-bottom-left-radius: 5px;

            -moz-border-radius-bottomright: 5px;
            -webkit-border-bottom-right-radius: 5px;
            border-bottom-right-radius: 5px;
            background-color:white;
        }
        td{
            vertical-align:top;
        }
        .alertrow
        {
            margin:7px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            background-color:#e9e9e9;
            position:relative;

        }
        /*.alertrow p {
              line-height: 17px;
                color: #c22e2e;
                padding: 5px 8px 5px 8px;
                margin:0px;
                font-size:12px;
                
        }
        .alertrow .icon {
            color: #f24242;
            float: left;
            margin: 9px 5px 3px 8px;
            line-height: 9px;
        }*/
        .bordered tr td{
            border:solid 1px #e9e9e9;
            padding:5px;

        }
        .bordered tr th{
            border:solid 1px #e9e9e9;
            padding:5px;
            background-color:#e9e9e9;
            color:#383838;
        }

        .alertrow p {
            line-height: 17px;
            color: #c22e2e;
            padding: 5px 8px 5px 8px;
            margin-left: 30px;
            font-size: 12px;
        }
        .alertrow .icon {
          color: #f24242;
            position: absolute;
            top: 50%;
            left: 6px;
            margin-top: -6px;
            line-height: 9px;
        }
        .margin1x
        {
            margin:5px;
        }
        .margin2x
        {
            margin:10px;
        }
        .margin3x
        {
            margin:20px;
        }
        .circle {
            -moz-border-radius: 20px;
            -webkit-border-radius: 20px;
            border-radius: 20px;
            height:40px;
        }
    </style>
</head>
<body style="font-family: 'Roboto', sans-serif; margin:0px; background-color:#ebebeb; font-size:12px;">
    <form id="form2" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
    <table width="100%">
                                <tr>
                                    <td style="width:33%">
                                        <div class="panel">
                                            <div class="header" style="background-color:#383838"> KPI Details - 2017
                                                <i class="close fa fa-refresh"></i>
                                            </div>
                                            <div class="content">
                                                <div style="width:95%; margin:0 auto; margin-bottom:20px;">
                                                <div class="margin1x" style="text-align:center;">
                                                    <asp:Label runat="server" ID="lblKPIName" style="font-size:20px;"></asp:Label>
                                                </div>
                                                 <div style="height:26px; overflow-y:scroll;overflow-x:hidden">
                                                 <table width="98%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; margin-left:30px;" class="bordered">
                                                    <thead>
                                                        <tr>                                                        
                                                        <th style="text-align:left">Vessel</th>
                                                        <th style="width:200px;text-align:center">Period</th>
                                                        <th style="width:100px;text-align:center">KPI Requirement</th>
                                                        <th style="width:100px;text-align:center;background-color:#ade1be">+Ve Score</th>
                                                        <th style="width:100px;text-align:center;background-color:#e48383">-Ve Score</th>
                                                        </tr>
                                                    </thead>
                                                     </table>
                                                      </div>
                                                    <div style="height:250px; overflow-y:scroll;overflow-x:hidden" class="ScrollAutoReset" id="fsfafdfaf_02">
                                                  <table width="98%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; margin-left:30px;" class="bordered">
                                                     <tbody>
                                                     <asp:Repeater runat="server" id="rptkpi">
                                                <ItemTemplate>
                                                 <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lnkVessel" OnClick="lnkVessel_Click" Text='<%#Eval("VesselName")%>' vesselid='<%#Eval("vesselid")%>' CssClass='<%#Common.ToDateString(Eval("EffDate"))%>'></asp:LinkButton>
                                                        </td>
                                                        <td style="width:200px;text-align:center"><%#Common.ToDateString(Eval("EffDate"))%> - <%#Common.ToDateString(Eval("LastDate"))%></td>
                                                        <td style="width:100px;text-align:center"><%#Eval("total")%></td>
                                                        <td style="width:100px;text-align:center;background-color:#ade1be"><%#Eval("success")%></td>
                                                        <td style="width:100px;text-align:center;background-color:#e48383"><%#Eval("error")%></td>
                                                        </tr>
                                                </ItemTemplate>
                                                </asp:Repeater> 
                                                   </tbody>
                                                       </table>   
                                                        </div>
                                                 <div style="height:26px; overflow-y:scroll;overflow-x:hidden">
                                                 <table width="98%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; margin-left:30px;" class="bordered">
                                                     <tfoot>
                                                         <tr>
                                                        <th style="text-align:left">Total</th>
                                                        <th style="width:200px;text-align:center"></th>                                                        
                                                        <th style="width:100px;text-align:center"><asp:Label runat="server" ID="lblSumreq"></asp:Label></th>
                                                        <th style="width:100px;text-align:center;background-color:#ade1be"><asp:Label runat="server" ID="lblSumsuccess" ForeColor="#0a7a2f"></asp:Label></th>
                                                        <th style="width:100px;text-align:center;background-color:#e48383"><asp:Label runat="server" ID="lblSumerror" ForeColor="#c22e2e"></asp:Label></th>
                                                        </tr>
                                                     </tfoot>
                                                </table> 
                                                             </div>  
                                                    <div style="padding:5px;text-align:center">
                                                        <asp:Label runat="server" ID="lblVesselName" Font-Size="24px"></asp:Label>
                                                        <asp:Label runat="server" ID="lblperiod"></asp:Label>
                                                    </div>
                                                    <table width="98%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; margin-left:30px;" class="bordered">
                                                    <thead>
                                                        <tr>
                                                        <th style="width:50px;text-align:center">Sr#</th>
                                                        <th style="text-align:left">Report#</th>
                                                        <th style="width:100px;text-align:center">Due Dt.</th>
                                                        <th style="width:100px;text-align:center">Done Dt.</th>
                                                        <th style="width:100px;text-align:center">Result</th>
                                                        <th style="width:100px;text-align:center">Status</th>
                                                        </tr>
                                                    </thead>
                                                     <tbody>
                                                     <asp:Repeater runat="server" id="tptDetails">
                                                <ItemTemplate>
                                                 <tr style='background-color:<%#(Eval("Status").ToString()=="Due")?"#ffff00":((Eval("Result").ToString()=="+Ve")?"#ade1be":"#e48383")%>'>
                                                        <td style="text-align:center"><%#Eval("SNo")%>.</td>
                                                        <td><%#Eval("ReportNo")%></td>
                                                        <td style="text-align:center"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                        <td style="text-align:center"><%#Common.ToDateString(Eval("DoneDate"))%></td>
                                                        <td style="text-align:center"><%#Eval("Result")%></td>
                                                        <td style="text-align:center"><%#Eval("Status")%></td>
                                                        </tr>
                                                </ItemTemplate>
                                                </asp:Repeater> 
                                                   </tbody>
                                                </table>
                                                     
                                                    </div>                                     
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
   
    </div>
    </form>
</body>
</html>
