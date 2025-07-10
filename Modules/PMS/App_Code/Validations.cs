using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Validations
/// </summary>
public class Validations
{
    public Validations()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
    {
        Double result;
        return Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);

    }
    public static bool Isinteger(string str)
    {
        try
        {
            int i = Convert.ToInt32(str);
                return true;
        }
        catch
        {
            return false;
        }
    }
    public bool IsValidDates(string val)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        DateTime result;
        return DateTime.TryParse(val, out result);

    }
    public bool CompareDateBetween(Control cntrl1, Control cntrl2)
    {
        Boolean Is = true;
        TextBox txtbox1 = new TextBox();
        txtbox1 = (TextBox)cntrl1;
        TextBox txtbox2 = new TextBox();
        txtbox2 = (TextBox)cntrl2;
        try
        {
            if (txtbox1.Text != "" && txtbox2.Text != "")
            {
                if (DateTime.Parse(txtbox1.Text.Trim()) <= DateTime.Parse(txtbox2.Text.Trim()))
                {
                    txtbox1.BorderColor = System.Drawing.Color.Green;
                    txtbox2.BorderColor = System.Drawing.Color.Green;
                    return Is = true;
                }
                else
                {
                    txtbox1.BorderColor = System.Drawing.Color.Red;
                    txtbox2.BorderColor = System.Drawing.Color.Red;
                    return Is = false;
                }
            }
            else
            {
                return Is = true;
            }
        }
        catch
        { return Is = false; }
    }
    public static bool Manditory(string data)
    {
        //if (!ScriptValidation(data.Trim()))
        //{
        //    return false;
        //}
        if (data.Trim() != "") return true;
        else return false;
    }
    public static bool ValidateDate(string strToCheck)
    {
        try
        {
            char[] Sep ={ '/' };
            string[] dateparts = strToCheck.Split(Sep);
            if (dateparts.Length != 3)
            {
                return false;
            }
            else
            {
                if (dateparts[0].Trim() == "" || dateparts[1].Trim() == "" || dateparts[2].Trim() == "")
                    return false;
            }
            // For converting dates format mm/dd/yyyy to dd/mm/yyyy
            //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            //dateInfo.ShortDatePattern = "dd/MM/yyyy";
            //DateTime objDate = Convert.ToDateTime(strToCheck, dateInfo);

            DateTime.Parse(strToCheck);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool ValidateLength(object data, Int32 Length)
    {
        if (data.ToString().Trim().Length <= Length) return true;
        else return false;
    }
    public static bool ManditoryDropdown(DropDownList ddl)
    {
        if (ddl.SelectedValue != "0") return true;
        else return false;
    }
    public static bool ValidateEmail(string email)
    {
        Regex pattern = new Regex(MatchEmailPattern);
        if (email.Trim() == "") return false;
        if (!pattern.IsMatch(email)) return false;
        return true;
    }
    public Boolean ValiadationCheck(Control cntrl, string Pass, int TxtMaxlength, int TxtMinlength, string Type)
    {
        Boolean Is = true;
        TextBox txtbox = new TextBox();
        txtbox = (TextBox)cntrl;
        if (Pass == "Numeric")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);
            }
            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                return (Is);
            }
        }
        if (Pass == "Float")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Float);
            }

            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                return (Is);
            }
        }
        if (Pass == "Float")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Float);
            }

            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                return (Is);
            }
        }
        if (Pass == "PhoneNo")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Number);
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                return (Is);
            }
            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                return (Is);
            }
        }
        if (Pass == "NString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9 .-]{1,500}$"));

                if (Val)
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        //txtbox.Focus();
                        Is = false;
                        return (Is);
                    }
                    Is = true;
                    return (Is);
                }
                else
                {
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
        }
        if (Pass == "PString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            if (Type == "0")
            {
                if (txtbox.Text == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    //txtbox.Focus();
                    Is = true;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9+ .-]{1,500}$"));
                if (!Val)
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);

                }
                else
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        //txtbox.Focus();
                        Is = false;
                        return (Is);
                    }
                }
            }
        }
        if (Pass == "Address")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            if (Type == "0")
            {
                if (txtbox.Text == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    //txtbox.Focus();
                    Is = true;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9/,.():-]*${1,500}$"));
                if (!Val)
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);

                }
                else
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        //txtbox.Focus();
                        Is = false;
                        return (Is);
                    }
                }
            }
        }

        // By Manita //
        if (Pass == "StringWithSpecials")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            if (Type == "0")
            {
                if (txtbox.Text == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    //txtbox.Focus();
                    Is = true;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9+ /.-]{1,500}$"));
                if (!Val)
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);

                }
                else
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        //txtbox.Focus();
                        Is = false;
                        return (Is);
                    }
                }
            }
        }
        if (Pass == "String")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9 &.,'-]{1,500}$"));
                string s = txtbox.Text.Trim().Substring(0, 1);
                bool Stringfirst = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);

                if (Stringfirst)
                {
                    Val = false;
                }
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
        }
        if (Pass == "SpacialString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9+',]{1,500}$"));
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
        }
        if (Pass == "Email")
        {

            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            //else
            //{
            bool val = true;
            Is = true;
            if (txtbox.Text != "")
            {
                //bool val = IsValidDates(txtbox.Text.ToString());
                if (IsValidEmailAddress(txtbox.Text))
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        Is = false;
                        return (Is);
                    }
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                return (Is);

            }
        }
        if (Pass == "Date")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = IsValidDates(txtbox.Text.ToString());
            }
            if (val)
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                return (Is);
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                Is = false;
                return (Is);
            }
        }
        return (Is);
    }
    //With Error MSG
    public Boolean ValiadationCheck(Control cntrl, string Pass, int TxtMaxlength, int TxtMinlength, string Type, string EMsg)
    {
        Boolean Is = true;
        TextBox txtbox = new TextBox();
        txtbox = (TextBox)cntrl;
        if (Pass == "Numeric")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);
            }
            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    txtbox.Attributes["title"] = "Fine";
                    Is = true;
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                txtbox.Attributes["title"] = EMsg;
                Is = false;
                return (Is);
            }
        }
        if (Pass == "CountryCode")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$"));
                string s = txtbox.Text.Trim().Substring(0, 1);
                bool Stringfirst = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);

                if (Stringfirst)
                {
                    Val = false;
                }
            }
            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
        }
        if (Pass == "Float")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Float);
            }

            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                txtbox.Attributes["title"] = EMsg;
                Is = false;
                return (Is);
            }
        }
        if (Pass == "Float")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Float);
            }

            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                txtbox.Attributes["title"] = EMsg;
                Is = false;
                return (Is);
            }
        }
        if (Pass == "PhoneNo")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Number);
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                txtbox.Attributes["title"] = "Fine";
                return (Is);
            }
            if (val)
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                txtbox.Attributes["title"] = EMsg;
                Is = false;
                return (Is);
            }
        }
        if (Pass == "NString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9 .]{1,500}$"));

                if (Val)
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        txtbox.Attributes["title"] = "Fine";
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        //txtbox.Focus();
                        txtbox.Attributes["title"] = EMsg;
                        Is = false;
                        return (Is);
                    }
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
        }
        if (Pass == "Char")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString().Replace("\r\n", ""), @"^[a-zA-Z]{1,3}$"));
                
                

                
            }
            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
        }
        if (Pass == "PString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            if (Type == "0")
            {
                if (txtbox.Text == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = "Fine";
                    Is = true;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9+ .-\/,]{1,500}$"));
                if (!Val)
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
                else
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        txtbox.Attributes["title"] = "Fine";
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        txtbox.Attributes["title"] = EMsg;
                        return Is = false;
                    }
                }
            }
        }

        if (Pass == "CHKPhone")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[0-9]+$"));

                if (Val)
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        txtbox.Attributes["title"] = "Fine";
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        //txtbox.Focus();
                        txtbox.Attributes["title"] = EMsg;
                        Is = false;
                        return (Is);
                    }
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
        }
        if (Pass == "String")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString().Replace("\r\n", ""), @"^[a-zA-Z0-9 .,&'-]{1,500}$"));
                string s = txtbox.Text.Trim().Substring(0, 1);
                bool Stringfirst = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);

                if (Stringfirst)
                {
                    Val = false;
                }
            }
            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
        }
        if (Pass == "MandatoryOnly")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                txtbox.Attributes["title"] = "Fine";
                return (Is);
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);
            }
        }
        if (Pass == "SpacialString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                // Val = (Regex.IsMatch(txtbox.Text.ToString(), "^[a-zA-Z0-9+&$'-:/]{1,500}$"));
                Val = (Regex.IsMatch(txtbox.Text.ToString(), "^[A-Za-z0-9,-/&():+]{1,500}$"));
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);
            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
        }

         if (Pass == "Stringonly")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                // Val = (Regex.IsMatch(txtbox.Text.ToString(), "^[a-zA-Z0-9+&$'-:/]{1,500}$"));
                Val = (Regex.IsMatch(txtbox.Text.ToString(), "^[A-Za-z ]{1,500}$"));
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);
            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
        }
        if (Pass == "NewString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            if (Type == "0")
            {
                if (txtbox.Text == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = "Fine";
                    Is = true;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z ]{1,500}$"));
                if (!Val)
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
                else
                {
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        txtbox.Attributes["title"] = "Fine";
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        txtbox.Attributes["title"] = EMsg;
                        return Is = false;
                    }
                }
            }
        }
        if (Pass == "SpacialCharString")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                 //Val = (Regex.IsMatch(txtbox.Text.ToString(), "^[a-zA-Z0-9+&$'-:/]{1,500}$"));
                //"^[A-Za-z ]{1,500}$")
                //Val = (Regex.IsMatch(txtbox.Text.ToString(), "[A-Za-z0-9,-/&@():+]{1,500}$"));
                Val = (Regex.IsMatch(txtbox.Text.ToString(), "^[A-Za-z 0-9,-/&@():+]{1,500}$"));
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);
            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
        }


        if (Pass == "Email")
        {

            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool val = true;
            Is = true;
            if (txtbox.Text != "")
            {
                //bool val = IsValidDates(txtbox.Text.ToString());
                if (IsValidEmailAddress(txtbox.Text))
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                    {
                        txtbox.BorderColor = System.Drawing.Color.Green;
                        Is = true;
                        txtbox.Attributes["title"] = "Fine";
                        return (Is);
                    }
                    else
                    {
                        txtbox.BorderColor = System.Drawing.Color.Red;
                        Is = false;
                        txtbox.Attributes["title"] = EMsg;
                        return (Is);
                    }
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                txtbox.Attributes["title"] = "Fine";
                return (Is);

            }
        }
        if (Pass == "Date")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
            bool val = true;
            if (txtbox.Text != "")
            {
                val = IsValidDates(txtbox.Text.ToString());
            }
            if (val)
            {
                txtbox.BorderColor = System.Drawing.Color.Green;
                Is = true;
                txtbox.Attributes["title"] = "Fine";
                return (Is);
            }
            else
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);
            }
       }

        //******************************

        if (Pass == "NewEmail")
        {
            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text.Trim() != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"));
                string s = txtbox.Text.Trim().Substring(0, 1);
                bool Stringfirst = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);

                if (Stringfirst)
                {
                    Val = false;
                }
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    Is = false;
                    txtbox.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
        }
        //*********************************************
        if (Pass == "FeeString")
        {

            if (Type == "*")
            {
                if (txtbox.Text.Trim() == "")
                {
                    TxtMinlength = 0;
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    txtbox.Attributes["title"] = EMsg;
                    //txtbox.Focus();
                    Is = false;
                    return (Is);
                }
            }
            bool Val = true;
            if (txtbox.Text != "")
            {
                Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[0-9]{1,3}(\.([0-9]{1,2}))?$"));
            }

            if (!Val)
            {
                txtbox.BorderColor = System.Drawing.Color.Red;
                //txtbox.Focus();
                Is = false;
                txtbox.Attributes["title"] = EMsg;
                return (Is);

            }
            else
            {
                if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
                {
                    txtbox.BorderColor = System.Drawing.Color.Green;
                    Is = true;
                    txtbox.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox.BorderColor = System.Drawing.Color.Red;
                    //txtbox.Focus();
                    txtbox.Attributes["title"] = EMsg;
                    Is = false;
                    return (Is);
                }
            }
        }
        return (Is);
    }
    //public Boolean ValiadationCheckForSendMail(Control cntrl, string Pass, int TxtMaxlength, int TxtMinlength, string Type, string EMsg)
    //{
    //    Boolean Is = true;
    //    TextBox txtbox = new TextBox();
    //    txtbox = (TextBox)cntrl;
    //    if (Pass == "String")
    //    {

    //        //if (txtbox.Text == "")
    //        //{
    //        //    txtbox.BorderColor = System.Drawing.Color.Green;
    //        //    txtbox.Focus();
    //        //    Is = false;
    //        //}
    //        //else
    //        //{
    //        //string SpecialChar="!@#$%^&*()_+=-[]{}'?><,./";

    //        if (Type == "*")
    //        {
    //            if (txtbox.Text == "")
    //            {
    //                TxtMinlength = 0;
    //                txtbox.BorderColor = System.Drawing.Color.Red;
    //                //txtbox.Focus();
    //                txtbox.Attributes["title"] = EMsg;
    //                Is = false;
    //                return (Is);
    //            }
    //        }
    //        bool Val = true;
    //        if (txtbox.Text.Trim() != "")
    //        {
    //            Val = (Regex.IsMatch(txtbox.Text.ToString(), @"^[a-zA-Z0-9* .-]{1,500}$"));
    //            string s = txtbox.Text.Trim().Substring(0, 1);
    //            bool Stringfirst = isNumeric(txtbox.Text.ToString(), System.Globalization.NumberStyles.Integer);

    //            if (Stringfirst)
    //            {
    //                Val = false;
    //            }
    //        }

    //        if (!Val)
    //        {
    //            txtbox.BorderColor = System.Drawing.Color.Red;
    //            //txtbox.Focus();
    //            Is = false;
    //            txtbox.Attributes["title"] = EMsg;
    //            return (Is);

    //        }
    //        else
    //        {
    //            if (txtbox.Text.Length <= TxtMaxlength && txtbox.Text.Length >= TxtMinlength)
    //            {
    //                txtbox.BorderColor = System.Drawing.Color.Green;
    //                Is = true;
    //                txtbox.Attributes["title"] = "Fine";
    //                return (Is);
    //            }
    //            else
    //            {
    //                txtbox.BorderColor = System.Drawing.Color.Red;
    //                //txtbox.Focus();
    //                Is = false;
    //                txtbox.Attributes["title"] = EMsg;
    //                return (Is);
    //            }
    //        }
    //    }
    //    return (Is);
    //}
    public static bool IsValidEmailAddress(string sEmail)
    {

        int nFirstAT = sEmail.IndexOf('@');
        int nLastAT = sEmail.LastIndexOf('@');

        if ((nFirstAT > 0) && (nLastAT == nFirstAT) &&
        (nFirstAT < (sEmail.Length - 1)))
        {
            //return(Regex.IsMatch(sEmail,);
            return (Regex.IsMatch(sEmail, @"\w*@*\.\w*((\.\w*)*)?"));

        }
        else
        {
            return false;
        }
    }
    public bool IsValidDate(string Date)
    {
        return (Regex.IsMatch(Date, "^(|(0[1-9])|(1[0-2]))\\/((0[1-9])|(1\\d)|(2\\d)|(3[0-1]))\\/((\\d{4}))$"));
    }
    public bool ComparePassword(Control cntrl1, Control cntrl2, string EMsg)
    {
        Boolean Is = true;
        TextBox txtbox1 = new TextBox();
        txtbox1 = (TextBox)cntrl1;
        TextBox txtbox2 = new TextBox();
        txtbox2 = (TextBox)cntrl2;
        try
        {
            if (txtbox1.Text != "" && txtbox2.Text != "")
            {
                if ((txtbox1.Text.Trim()) == (txtbox2.Text.Trim()))
                {
                    txtbox1.BorderColor = System.Drawing.Color.Green;

                    txtbox2.BorderColor = System.Drawing.Color.Green;
                    return Is = true;
                    txtbox2.Attributes["title"] = "Fine";
                    return (Is);
                }
                else
                {
                    txtbox1.BorderColor = System.Drawing.Color.Red;


                    txtbox2.BorderColor = System.Drawing.Color.Red;

                    Is = false;
                    txtbox2.Attributes["title"] = EMsg;
                    return (Is);
                }
            }
            else
            {
                txtbox2.BorderColor = System.Drawing.Color.Red;
                txtbox2.Attributes["title"] = EMsg;
                return Is = false;
                // txtbox2.Attributes["title"] = EMsg;
            }
        }
        catch
        { return Is = false; }
    }
}
