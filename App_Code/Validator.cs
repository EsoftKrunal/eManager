using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text.RegularExpressions;
/// <summary>
/// Summary description for Validator
/// </summary>
public class Validator
{
    public static bool ValidateUserName(string InputText)
    {
        string RegStr = @"^[\w._]{4,30}$";
        return Regex.IsMatch(InputText, RegStr);
    }
    public static bool ValidatePassword(string InputText)
    {
        string RegStr = @"^.{4,30}$";
        return Regex.IsMatch(InputText, RegStr);
    }
    public static bool ValidateName(string InputText)
    {
        string RegStr = @"^[\w ]+$";
        return Regex.IsMatch(InputText, RegStr);
    }
    public static bool ValidateEmail(string InputText)
    {
        string RegStr = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
        return Regex.IsMatch(InputText, RegStr);
    }
}
    
