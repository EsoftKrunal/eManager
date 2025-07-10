using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class SelecttCountryCode : DataAccessBase
    {
        private int _CountryId;
        public SelecttCountryCode()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCountryCode.ToString();
        }
        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
       
        public string CountryCode()
        {
            DataSet ds;
            CountryCodeParameteres countryParamaters = new CountryCodeParameteres(CountryId);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds=dbhelper.Run(base.ConnectionString, countryParamaters.Parameters);
            if (ds.Tables[0].Rows.Count  == 0)
            {
                return "";
            }
            else
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
    }
    public class CountryCodeParameteres
    {
        private int _CountryId;
        private SqlParameter[] _parameters;
        public CountryCodeParameteres(int param)
        {
            _CountryId = param;
            Build();
        }
        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {   
                new SqlParameter( "@CountryId"		, CountryId   ) ,
            };

            Parameters = parameters;
        }
        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
