using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Mail;    
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics; 

    public enum MyValueType
    {
        Int,Decimal,String,Date,DateTime,Bool
    }
    public class Common
    {

        #region Properties For Email
        private string _Message;
        private string _Subject;
        private string _FromAddress;
        private string _ToAddress;
        private string _ToCCAddress;
        private string _AttachFile;
        private bool _isBodyHTML;
        
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public string FromAddress
        {
            get { return _FromAddress; }
            set { _FromAddress = value; }
        }

        public string ToAddress
        {
            get { return _ToAddress; }
            set { _ToAddress = value; }
        }

        public string ToCCAddress
        {
            get { return _ToCCAddress; }
            set { _ToCCAddress = value; }
        }

        public string AttachFile
        {
            get { return _AttachFile; }
            set { _AttachFile = value; }
        }

        public bool isBodyHTML
        {
            get { return _isBodyHTML; }
            set { _isBodyHTML = value; }
        }
        
        #endregion

        static String[] Procedures;
        static int[] ParameterLength;
        public static string Prefix = "";
        static MyParameter[] Parameters;
        static Parameter_Mappings[] ParameterMapping;
        static SqlTransaction BulkTrans;
        static SqlConnection BulkConn;
        public static string ErrMsg="";
        static String ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();

    
        //------ 
        public static void Set_Procedures(params string[] Dummy)
        {
            Procedures = Dummy;
        }
        public static void Set_ParameterLength(params int[] Dummy)
        {
            ParameterLength = Dummy;
        }
        public static void Set_Parameters(params MyParameter[] Dummy)
        {
            Parameters = Dummy;
        }
        public static void Set_Mappings(params Parameter_Mappings[] Dummy)
        {
            ParameterMapping = Dummy;
        }
        public static void Set_ConnectionString(String Dummy)
        {
            ConnectionString = Dummy;
        }
        public static Parameter_Mappings[] Get_Mapping_For_Procedure(string _ProcedureName)
        {
            int Count = 0;
            try
            {
                for (int i = 0; i <= ParameterMapping.Length - 1; i++)
                {
                    if (ParameterMapping[i].SourceProcedureName.Trim() == _ProcedureName.Trim())
                    {
                        Count = Count + 1;
                    }
                }
            }
            catch { }
            Parameter_Mappings[] Temp = new Parameter_Mappings[Count];
            try
            {
                for (int i = 0; i <= ParameterMapping.Length - 1; i++)
                {
                    if (ParameterMapping[i].SourceProcedureName.Trim() == _ProcedureName.Trim())
                    {
                        Temp[i] = new Parameter_Mappings(ParameterMapping[i].SourceProcedureName, ParameterMapping[i].SourceParameterName, ParameterMapping[i].DestProcedureName, ParameterMapping[i].DestRow, ParameterMapping[i].DestCol);
                    }
                }
            }
            catch { }
            return Temp;
        }

        //------CONSTRUCTOR ---
        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //------COMMON PROCEDURE WHICH IS CALL AT EACH PAGE POST BACK ---
        public static void ServerCommon()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["POS_Demo"].ToString();
        }
        //------COMMON PROCEDURE WHICH IS CALL FOR EXECUTING ANY SQL COMMAND FOR SAVE/UPDATE/DELETE ---
        public static void BulkTransStart()
        {
            BulkConn = new SqlConnection(ConnectionString);
            BulkConn.Open();
            BulkTrans = BulkConn.BeginTransaction();
            
        }
        public static bool BulkTransEnd()
        {
            try
            {
                BulkTrans.Commit();
                BulkConn.Close();
                return true;

                
            }
            catch
            {
                return false;
            }
        }

        public static bool BulkTransRollabck()
        {
            try
            {
                BulkTrans.Rollback();
                BulkConn.Close();
                return true;

                
            }
            catch
            {
                return false;
            }
        }

        public static bool Execute_Procedures_IUDBulk(DataSet ResultBulkTrans)
        {
            int Count = 0;
            SqlCommand[] Commands = new SqlCommand[Procedures.Length];

            // Enlist the command in the current transaction.
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Commands[i] = new SqlCommand();
                Commands[i].Connection = BulkConn;
                Commands[i].CommandType = CommandType.StoredProcedure;
                Commands[i].CommandText = Prefix + Procedures[i].ToString();
                Commands[i].Transaction = BulkTrans;
                for (int j = 0; j <= ParameterLength[i] - 1; j++)
                {
                    Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                    Count++;
                }
            }
            try
            {
                for (int i = 0; i <= Commands.Length - 1; i++)
                {
                    DataTable dt = new DataTable();
                    Parameter_Mappings[] TempMapping = Get_Mapping_For_Procedure(Procedures[i].ToString());
                    for (int j = 0; j <= TempMapping.Length - 1; j++)
                    {
                        Commands[i].Parameters[TempMapping[j].SourceParameterName].Value = ResultBulkTrans.Tables[TempMapping[j].DestProcedureName + "_Result"].Rows[TempMapping[j].DestRow][TempMapping[j].DestCol];
                    }
                    dt.Load(Commands[i].ExecuteReader());
                    dt.TableName = Procedures[i].ToString() + "_Result";
                    ResultBulkTrans.Tables.Add(dt);
                }
                return true;
            }
            catch (Exception exx)
            {
                BulkTransRollabck();
                return false;
                
            }
            finally
            {
            }
        }
        public static bool Execute_Procedures_IUD(DataSet Result)
        {

            SqlConnection myConnection = new SqlConnection(ConnectionString);
            int Count = 0;
            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            SqlCommand[] Commands = new SqlCommand[Procedures.Length];

            // Enlist the command in the current transaction.
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Commands[i] = new SqlCommand();
                Commands[i].Connection = myConnection;
                Commands[i].CommandType = CommandType.StoredProcedure;
                Commands[i].CommandText =Prefix + Procedures[i].ToString();
                Commands[i].Transaction = myTrans;
                Commands[i].CommandTimeout = 300;
                for (int j = 0; j <= ParameterLength[i] - 1; j++)
                {
                    Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name,Parameters[Count].Value));
                    Count++;
                }
            }
            try
            {
                for (int i = 0; i <= Commands.Length - 1; i++)
                {
                    DataTable dt = new DataTable();
                    Parameter_Mappings[] TempMapping = Get_Mapping_For_Procedure(Procedures[i].ToString());
                    for (int j = 0; j <= TempMapping.Length - 1; j++)
                    {
                        Commands[i].Parameters[TempMapping[j].SourceParameterName].Value = Result.Tables[TempMapping[j].DestProcedureName + "_Result"].Rows[TempMapping[j].DestRow][TempMapping[j].DestCol];
                    }
                    dt.Load(Commands[i].ExecuteReader(),LoadOption.OverwriteChanges);
                    dt.TableName = Procedures[i].ToString() + "_Result";
                    Result.Tables.Add(dt);
                }
                myTrans.Commit();
                ErrMsg = "";
                return true;
            }
            catch (Exception exx)
            {
                ErrMsg=exx.Message;
                return false;
            }
            finally
            {
                myConnection.Close();
            }

        }
        //------COMMON PROCEDURE WHICH IS CALL FOR SELECT DATA FROM SQL COMMAND ---
        public static DataSet Execute_Procedures_Select()
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter Adp = new SqlDataAdapter();
            int Count = 0;
            myConnection.Open();
            SqlCommand[] Commands = new SqlCommand[Procedures.Length];
            // Enlist the command in the current transaction.
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Commands[i] = new SqlCommand();
                Commands[i].Connection = myConnection;
                Commands[i].CommandType = CommandType.StoredProcedure;
                Commands[i].CommandText =Prefix + Procedures[i].ToString();
                for (int j = 0; j <= ParameterLength[i] - 1; j++)
                {
                    Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name,Parameters[Count].Value));
                    Count++;
                }
            }
            try
            {
                for (int i = 0; i <= Commands.Length - 1; i++)
                {
                    Adp.SelectCommand = Commands[i];
                    Adp.Fill(RetValue, Procedures[i].ToString() + "_Result");
                }
                ErrMsg = "";
                return RetValue;
                 
            }
            catch (Exception exx)
            {
                ErrMsg = exx.Message;
                return null;
            }
            finally
            {
                myConnection.Close();
            }

        }
        public static void UpdateDataTableById(ref DataTable dt, string ColumnName, object Value)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i][ColumnName] = Value;
            }
        }
        public static DataSet Execute_Procedures_Select(string Message)
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlDataAdapter Adp = new SqlDataAdapter();
            int Count = 0;
            myConnection.Open();
            SqlCommand[] Commands = new SqlCommand[Procedures.Length];
            // Enlist the command in the current transaction.
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Commands[i] = new SqlCommand();
                Commands[i].Connection = myConnection;
                Commands[i].CommandType = CommandType.StoredProcedure;
                Commands[i].CommandText = Prefix + Procedures[i].ToString();
                for (int j = 0; j <= ParameterLength[i] - 1; j++)
                {
                    Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                    Count++;
                }
            }
            try
            {
                for (int i = 0; i <= Commands.Length - 1; i++)
                {
                    Adp.SelectCommand = Commands[i];
                    Adp.Fill(RetValue, Procedures[i].ToString() + "_Result");
                }
                ErrMsg = ""; 
                return RetValue;
            }
            catch (Exception exx)
            {
                ErrMsg = exx.Message;
                return null;
            }
            finally
            {
                myConnection.Close();
            }

        }

        public static void ExportDatatable(HttpResponse Response,DataSet ds)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite =new System.IO.StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg =new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = ds;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public static void ExportDatatable(HttpResponse Response, DataTable dt,String FileName)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "rfq1" + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {

            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;


                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }

                // System.Drawing.Image imgOut =
                //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
                //                              null,IntPtr.Zero);

                // *** This code creates cleaner (though bigger) thumbnails and properly
                // *** and handles GIF files better by generating a white background for
                // *** transparent images (as opposed to black)
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }

            return bmpOut;
        }

        //------COMMON PROCEDURE WHICH IS CALL FOR SELECT DATA FROM SQL COMMAND // Used for Rights management //---
        public static DataSet Execute_Procedures_SelectAdmin()
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Admin_DBConnection"].ToString());
            SqlDataAdapter Adp = new SqlDataAdapter();
            int Count = 0;
            myConnection.Open();
            SqlCommand[] Commands = new SqlCommand[Procedures.Length];
            // Enlist the command in the current transaction.
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Commands[i] = new SqlCommand();
                Commands[i].Connection = myConnection;
                Commands[i].CommandType = CommandType.StoredProcedure;
                Commands[i].CommandText = Procedures[i].ToString();
                for (int j = 0; j <= ParameterLength[i] - 1; j++)
                {
                    Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                    Count++;
                }
            }
            try
            {
                for (int i = 0; i <= Commands.Length - 1; i++)
                {
                    Adp.SelectCommand = Commands[i];
                    Adp.Fill(RetValue, Procedures[i].ToString() + "_Result");
                }
                ErrMsg = "";
                return RetValue;

            }
            catch (Exception exx)
            {
                ErrMsg = exx.Message;
                return null;
            }
            finally
            {
                myConnection.Close();
            }

        }

        public static DataTable Execute_Procedures_Select_ByQuery(string Query)
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
            SqlDataAdapter Adp = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand(Query, myConnection);
            Command.CommandTimeout = 300;
            Adp.SelectCommand = Command;
            Adp.Fill(RetValue, "Result");
            try
            {
                return RetValue.Tables[0];
            }
            catch { return null; }
        }
        public static DataTable Execute_Procedures_Select_ByQueryAdmin(string Query)
        {
            DataSet RetValue = new DataSet();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Admin_DBConnection"].ToString());
            SqlDataAdapter Adp = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand(Query, myConnection);
            Adp.SelectCommand = Command;
            Adp.Fill(RetValue, "Result");
            try
            {
                return RetValue.Tables[0];
            }
            catch { return null; }
        }

        public static object getControlValue(System.Web.UI.Control ctl)
        {
            if (ctl != null)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    return ((TextBox)ctl).Text;
                }
                else if (ctl.GetType() == typeof(DropDownList))
                {
                    return ((DropDownList)ctl).SelectedValue;
                }
                else if (ctl.GetType() == typeof(CheckBox))
                {
                    return ((CheckBox)ctl).Checked;
                }
                else if (ctl.GetType() == typeof(HiddenField))
                {
                    return ((HiddenField)ctl).Value;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static int EnumtoInt(MyValueType DataType) 
        {
            if (DataType == MyValueType.Int)
            {
                return 1;
            }
            if (DataType == MyValueType.Decimal)
            {
                return 2;
            }
            if (DataType == MyValueType.String)
            {
                return 3;
            }
            if (DataType == MyValueType.Date)
            {
                return 4;
            }
            if (DataType == MyValueType.DateTime)
            {
                return 5;
            }
            if (DataType == MyValueType.Bool)
            {
                return 6;
            }
            return 0;
        }
        public static void setControlValueByControlId_DataRow(System.Web.UI.Control ctl, DataRow dr, bool IsTransformed, MyValueType DataType,string ColumnName)
        {
            object Value=dr[ColumnName];
            setControlValueByControlId(ctl, Value, IsTransformed, DataType);
        }
        public static void setControlValueByControlId(System.Web.UI.Control ctl, object Value, bool IsTransformed, MyValueType DataType)
        {
            if (ctl != null)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    switch (EnumtoInt(DataType))
                    {
                        case 4: // date
                            if (IsTransformed)
                            {
                                try
                                {
                                    ((TextBox)ctl).Text = DateTime.Parse(Value.ToString()).ToString("MM/dd/yyyy");
                                }
                                catch { ((TextBox)ctl).Text = ""; }
                            }
                            else
                            {
                                try
                                {
                                    ((TextBox)ctl).Text = Value.ToString();
                                }
                                catch { ((TextBox)ctl).Text = Value.ToString(); }
                            }
                            break;
                        case 5: // datetime
                            if (IsTransformed)
                            {
                                try
                                {
                                    ((TextBox)ctl).Text = DateTime.Parse(Value.ToString()).ToString("MM/dd/yyyy hh:mm:ss");
                                }
                                catch { ((TextBox)ctl).Text = ""; }
                            }
                            else
                            {
                                try
                                {
                                    ((TextBox)ctl).Text = Value.ToString();
                                }
                                catch { ((TextBox)ctl).Text = Value.ToString(); }
                            }
                            break;
                        case 1: // int
                            if (IsTransformed)
                            {
                                try
                                {
                                    Int32 _Value = Int32.Parse(Value.ToString());
                                    if (_Value <= 0)
                                        ((TextBox)ctl).Text = "";
                                    else
                                        ((TextBox)ctl).Text = _Value.ToString();
                                }
                                catch { ((TextBox)ctl).Text = ""; }
                            }
                            else
                            {
                                try
                                {
                                    Int32 _Value = Int32.Parse(Value.ToString());
                                    ((TextBox)ctl).Text = _Value.ToString();
                                }
                                catch { ((TextBox)ctl).Text = ""; }
                            }
                            break;
                        case 2: // decimal
                            if (IsTransformed)
                            {
                                try
                                {
                                    decimal _Value = decimal.Parse(Value.ToString());
                                    if (_Value <= 0)
                                        ((TextBox)ctl).Text = "";
                                    else
                                        ((TextBox)ctl).Text = _Value.ToString();
                                }
                                catch { ((TextBox)ctl).Text = ""; }
                            }
                            else
                            {
                                try
                                {
                                    decimal _Value = decimal.Parse(Value.ToString());
                                    ((TextBox)ctl).Text = _Value.ToString();
                                }
                                catch { ((TextBox)ctl).Text = ""; }
                            }
                            break;
                        case 3: // string
                            try
                            {
                                ((TextBox)ctl).Text = Value.ToString();
                            }
                            catch
                            {
                                ((TextBox)ctl).Text = "";
                            }
                            break;
                    }
                }
                else if (ctl.GetType() == typeof(DropDownList))
                {
                    switch (EnumtoInt(DataType))
                    {
                        case 1: // int
                            try
                            {
                                Int32 _Value = Int32.Parse(Value.ToString());
                                ((DropDownList)ctl).SelectedValue = _Value.ToString();
                            }
                            catch { ((DropDownList)ctl).SelectedValue = "0"; }
                            break;
                        case 3: // string
                            try
                            {
                                ((DropDownList)ctl).SelectedValue = Value.ToString();
                            }
                            catch
                            {
                                ((DropDownList)ctl).SelectedValue = "";
                            }
                            break;
                    }
                }
                else if (ctl.GetType() == typeof(CheckBox))
                {
                    switch ( EnumtoInt(DataType))
                    {
                        case 6: // bool
                            try
                            {
                                bool _Value = bool.Parse(Value.ToString());
                                ((CheckBox)ctl).Checked = _Value;
                            }
                            catch { ((CheckBox)ctl).Checked = false; }
                            break;
                    }

                    ((CheckBox)ctl).Checked = (bool)Value;
                }
                else if (ctl.GetType() == typeof(HiddenField))
                {

                    switch (EnumtoInt(DataType))
                    {
                        case 1: // int
                            try
                            {
                                Int32 _Value = Int32.Parse(Value.ToString());
                                ((HiddenField)ctl).Value = _Value.ToString();
                            }
                            catch { ((HiddenField)ctl).Value = "0"; }
                            break;
                        case 3: // string
                            try
                            {
                                ((HiddenField)ctl).Value = Value.ToString();
                            }
                            catch
                            {
                                ((HiddenField)ctl).Value = "";
                            }
                            break;
                    }
                }
            }
        }
        public static string setControlValueByValue(object Value, bool IsTransformed, MyValueType DataType)
        {
            string retval="";
            switch (DataType)
            {
                case MyValueType.Date:
                    if (IsTransformed)
                    {
                        try
                        {
                            retval = DateTime.Parse(Value.ToString()).ToString("MM/dd/yyyy");
                        }
                        catch { retval = ""; }
                    }
                    else
                    {
                        try
                        {
                            retval  = Value.ToString();
                        }
                        catch { retval = ""; }
                    }
                    break;
                case MyValueType.DateTime:
                    if (IsTransformed)
                    {
                        try
                        {
                            retval = DateTime.Parse(Value.ToString()).ToString("MM/dd/yyyy hh:mm:ss");
                        }
                        catch { retval = ""; }
                    }
                    else
                    {
                        try
                        {
                            retval = Value.ToString();
                        }
                        catch { retval = ""; }
                    }
                    break;
                case MyValueType.Int:
                    if (IsTransformed)
                    {
                        try
                        {
                            Int32 _Value = Int32.Parse(Value.ToString());
                            if (_Value <= 0)
                                retval = "";
                            else
                                retval = _Value.ToString();
                        }
                        catch { retval = ""; }
                    }
                    else
                    {
                        try
                        {
                            Int32 _Value = Int32.Parse(Value.ToString());
                            retval = _Value.ToString();
                        }
                        catch { retval = ""; }
                    }
                    break;
                case MyValueType.Decimal:
                    if (IsTransformed)
                    {
                        try
                        {
                            decimal _Value = decimal.Parse(Value.ToString());
                            if (_Value <= 0)
                                retval = "";
                            else
                                retval = _Value.ToString();
                        }
                        catch { retval = ""; }
                    }
                    else
                    {
                        try
                        {
                            decimal _Value = decimal.Parse(Value.ToString());
                            retval = _Value.ToString();
                        }
                        catch { retval = ""; }
                    }
                    break;
                case MyValueType.String:
                    try
                    {
                        retval = Value.ToString();
                    }
                    catch
                    {
                        retval = "";
                    }
                    break;
            }
            return retval;
        }
        public static void setValuetoDataTable(ref DataRow dr, string ColumnName, string ControlId,RepeaterItem itm)
        {
            object Value = Common.getControlValue(itm.FindControl(ControlId));
            if(dr.Table.Columns[ColumnName].DataType == typeof(Boolean))
            {
                    try
                    {
                        dr[ColumnName] = Convert.ToBoolean(Value);
                    }
                    catch
                    {
                        dr[ColumnName] = false; 
                    }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Byte))
            {
                try
                {
                    dr[ColumnName] = Convert.ToByte(Value);
                }
                catch
                {
                    dr[ColumnName] = "";
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Char))
            {
                try
                {
                    dr[ColumnName] = Convert.ToChar(Value);
                }
                catch
                {
                    dr[ColumnName] = "";
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(DateTime))
            {
                try
                {
                    dr[ColumnName] = Convert.ToDateTime(Value);
                }
                catch
                {
                    dr[ColumnName] = DBNull.Value;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Decimal))
            {
                try
                {
                    dr[ColumnName] = Convert.ToDecimal(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Double))
            {
                try
                {
                    dr[ColumnName] = Convert.ToDouble(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Int16))
            {
                try
                {
                    dr[ColumnName] = Convert.ToInt16(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Int32))
            {
                try
                {
                    dr[ColumnName] = Convert.ToInt32(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Int64))
            {
                try
                {
                    dr[ColumnName] = Convert.ToInt64(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(SByte))
            {
                try
                {
                    dr[ColumnName] = Convert.ToSByte(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(Single))
            {
                try
                {
                    dr[ColumnName] = Convert.ToSingle(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(String))
            {
                try
                {
                    dr[ColumnName] = Convert.ToString(Value);
                }
                catch
                {
                    dr[ColumnName] = "";
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(TimeSpan))
            {
                try
                {
                    dr[ColumnName] = TimeSpan.Parse(Value.ToString());
                }
                catch
                {
                    dr[ColumnName] = DBNull.Value ;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(UInt16))
            {
                try
                {
                    dr[ColumnName] = Convert.ToUInt16(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(UInt32))
            {
                try
                {
                    dr[ColumnName] = Convert.ToUInt32(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
            else if (dr.Table.Columns[ColumnName].DataType == typeof(UInt64))
            {
                try
                {
                    dr[ColumnName] = Convert.ToUInt64(Value);
                }
                catch
                {
                    dr[ColumnName] = 0;
                }
            }
        }
        public static DataTable getTableByFilter(DataTable dt,string WhereClause)
    {
        dt.DefaultView.RowFilter = WhereClause;
        return dt.DefaultView.ToTable();
    }
        public static Int32 CastAsInt32(object Data)
        {
            decimal res = 0;
            try
            {
                res = decimal.Parse(Convert.ToString(Data));
                return Convert.ToInt32(res);  
            }
            catch
            {
                return 0;
            }
        }
        public static decimal CastAsDecimal(object Data)
        {
            decimal res = 0;
            try
            {
                res = decimal.Parse(Data.ToString());
                return Convert.ToDecimal(res);
            }
            catch
            {
                return 0;
            }
        }
        public static object CastAsDate(object Data)
        {
            DateTime res;
            try
            {
                res = DateTime.Parse(Data.ToString());
                return Convert.ToDateTime(res.ToString("MM/dd/yyyy"));
            }
            catch
            {
                return null;
            }
        }
        public static string ToDateString(object inp)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(inp);
                if (dt.ToString("dd-MMM-yyyy") == "01-Jan-1900")
                { return ""; }
                else
                {   return dt.ToString("dd-MMM-yyyy");}
            }
            catch 
            {
                return "";
            } 

        }
        public static string ToDateTimeString(object inp)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(inp);
                if (dt.ToString("dd-MMM-yyyy") == "01-Jan-1900")
                { return ""; }
                else
                { return dt.ToString("dd-MMM-yyyy hh:mm tt"); }
            }
            catch
            {
                return "";
            }

        }

        public static string ConvertToCSV(string[] Data)
        {
            string ret = "";
            for (int i = 0; i <= Data.Length - 1; i++)
            {
                if (i == 0)
                    ret = Data[0];
                else
                    ret = ret + "," + Data[i];  
            }
            return ret;
        }

        //---------------------
        # region MAIL SENDING PROCEDRUES
        //-------------
        #region BASE PROCEDURES
        public void SendEmails(string fromAddress, string toAddress, string tocc, string mailsubject, string msgContent, bool isBodyHTML)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(fromAddress, "Web Admin");
            //objSmtpClient.Host = "relay-hosting.secureserver.net";
            //objSmtpClient.Host = ConfigurationManager.AppSettings["hostname"];
            objSmtpClient.Host = "smtp.gmail.com";
            objSmtpClient.Port = 25;

            objMessage.From = objfromAddress;

            objMessage.To.Add(toAddress);
            objMessage.Subject = mailsubject;
            if (tocc != "")
            {
                objMessage.CC.Add(tocc);
            }
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Credentials = new System.Net.NetworkCredential("tfmesoftech@gmail.com", "test123456");
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.Body = msgContent;
            objSmtpClient.Send(objMessage);
        }
        public void SendEmails(string fromAddress, string toAddress, string tocc, string mailsubject, string msgContent, bool isBodyHTML, string strReplyTo)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(fromAddress, "Web Admin");
            //objSmtpClient.Host = "relay-hosting.secureserver.net";
            objSmtpClient.Host = ConfigurationManager.AppSettings["hostname"];
            objSmtpClient.Port = 25;

            objMessage.From = objfromAddress;
            if (strReplyTo != "")
            {
                MailAddress objReplyToAddress = new MailAddress(strReplyTo);
                objMessage.ReplyTo = objReplyToAddress;
            }

            objMessage.To.Add(toAddress);
            objMessage.Subject = mailsubject;
            if (tocc != "")
            {
                objMessage.CC.Add(tocc);
            }
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.Body = msgContent;
            objSmtpClient.Send(objMessage);
        }
        public void SendEmail(string AttachFileName)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromAddress, "Web Admin");
            
            try
            {
                objSmtpClient.Host = "smtp.gmail.com";
                //objSmtpClient.Host = ConfigurationManager.AppSettings["hostname"];
                objSmtpClient.Port = 25;
                objMessage.From = objfromAddress;
                objMessage.To.Add(ToAddress);
                objMessage.Subject = Subject;
                if (Convert.ToString(ToCCAddress) != "" && ToCCAddress!=null)
                {
                    objMessage.CC.Add(ToCCAddress);
                }
                objSmtpClient.EnableSsl = true;
                objSmtpClient.Credentials = new System.Net.NetworkCredential("tfmesoftech@gmail.com", "test123456");
                objMessage.IsBodyHtml = isBodyHTML;
                objMessage.Body = Message;

                Attachment attachFile = new Attachment(AttachFileName);
                objMessage.Attachments.Add(attachFile);


                objSmtpClient.Send(objMessage);
            }
            catch (Exception e)
            {

            }
        }
        public void SendGEmails(string fromAddress, string toAddress, string tocc, string mailsubject, string msgContent, bool isBodyHTML, string strReplyTo)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(fromAddress, "Web Admin");
            try
            {
                objSmtpClient.Host = "smtp.gmail.com";
                //objSmtpClient.Host = ConfigurationManager.AppSettings["hostname"];
                objSmtpClient.Port = 25;
                objMessage.From = objfromAddress;
                if (strReplyTo != "")
                {
                    MailAddress objReplyToAddress = new MailAddress(strReplyTo);
                    objMessage.ReplyTo = objReplyToAddress;
                }
                objMessage.To.Add(toAddress);
                objMessage.Subject = mailsubject;
                if (tocc != "")
                {
                    objMessage.CC.Add(tocc);
                }
                objSmtpClient.EnableSsl = true;
                objSmtpClient.Credentials = new System.Net.NetworkCredential("manoj@esoftech.com", "pankaj99");
                objMessage.IsBodyHtml = isBodyHTML;
                objMessage.Body = msgContent;
                objSmtpClient.Send(objMessage);
            }
            catch (Exception e)
            {

            }
        }
        public static string GetPageOutput(string strPagePathWithQueryString)
        {
            string strOutput = "";
            // *** Save the current request information
            HttpContext Context = HttpContext.Current;
            // *** Fix up the path to point at the templates directory
            strPagePathWithQueryString = Context.Request.ApplicationPath +
                  "/" + strPagePathWithQueryString;
            // *** Now call the other page and load into StringWriter
            StringWriter sw = new StringWriter();
            try
            {
                // *** IMPORTANT: Child page's FilePath still points at current page
                //                QueryString provided is mapped into new page and then reset
                Context.Server.Execute(strPagePathWithQueryString, sw);
                strOutput = sw.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
                strOutput = null;
            }
            return strOutput;

        }
        public void SendMailWithAttachment(string fromstr, string tostr, string toccstr, string subjectstr, string messagestr, string sAttach)
        {
            //------------------------------Commented Code------------------//
            //MailMessage MyMailMessage = new MailMessage();
            //SmtpClient objSmtpClient = new SmtpClient();
            //MyMailMessage.From = new MailAddress("tfmesoftech@gmail.com", "Web Admin");
            //MyMailMessage.To.Add(tostr);
            //if (toccstr != "")
            //{
            //    MyMailMessage.CC.Add(toccstr);
            //}
            //MyMailMessage.Subject = subjectstr;
            //MyMailMessage.IsBodyHtml = true;
            //MyMailMessage.Body = messagestr;
            //SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
            //if (sAttach.Length > 0)
            //{
            //    Attachment aa = new Attachment(sAttach);
            //    MyMailMessage.Attachments.Add(aa);
            //}
            //SMTPServer.Port = 25;
            //objSmtpClient.EnableSsl = true;
            //// SMTPServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AuthenticatedUser"], ConfigurationManager.AppSettings["AuthenticatedPwd"]);
            //objSmtpClient.Credentials = new System.Net.NetworkCredential("manoj@esoftech.com", "pankaj99");
            //SMTPServer.EnableSsl = true;
            //SMTPServer.Send(MyMailMessage);
            //--------------------------ENDS HERE--------------------//

            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(fromstr, "Web Admin");
            
            objSmtpClient.Host = "smtp.gmail.com";
            //objSmtpClient.Host = ConfigurationManager.AppSettings["hostname"];
            objSmtpClient.Port = 25;
            objMessage.From = objfromAddress;
            objMessage.To.Add(tostr);
            objMessage.Subject = subjectstr;
            if (Convert.ToString(toccstr) != "" && ToCCAddress != null)
            {
                objMessage.CC.Add(toccstr);
            }
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Credentials = new System.Net.NetworkCredential("tfmesoftech@gmail.com", "test123456");
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.Body = messagestr;

            Attachment attachFile = new Attachment(sAttach);
            objMessage.Attachments.Add(attachFile);


            objSmtpClient.Send(objMessage);
        }  

        #endregion
        //-------------
        //public void SendEmail()
        //{
           
        //    if (ConfigurationManager.AppSettings["SendingMail"].Contains("local"))
        //    {
        //        SendLocalEmails();
        //    }
        //    else
        //    {
        //       // for Live mails
        //    }
        //}
        #endregion

        /// <summary>
        /// To set null value in parameter
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static object getObject(object ob)
        {
            Object obs = new object();
            obs = ob;
            return obs;
        }

        //---------- Get Error -----------

        public static string getLastError()
        {
            string Message = Common.ErrMsg.Replace("\"", "`").Replace("'", "`").Replace("\n", " ").Replace("\r", " ");
            return Message;
        }
        // --------------------

        public static void BindYear(DropDownList ddl)
        {

            int i = DateTime.Today.Year;
            for (; i >= 2013; i--)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }
    }

