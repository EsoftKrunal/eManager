using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberQualificationDetailsSelectDataById : DataAccessBase
    {
        private QualificationDetails _qualificationdetails;
        public CrewMemberQualificationDetailsSelectDataById()
        {
            StoredProcedureName = StoredProcedure.Name.SelectQualificationDetailsdata.ToString();
        }
        public DataSet Get()
        {
            DataSet ds;
            CrewMemberQualificationDetailsSelectDataParameters qualificationDetailsSelectDataParameters = new CrewMemberQualificationDetailsSelectDataParameters(QualificationDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, qualificationDetailsSelectDataParameters.Parameters);
            return ds;
        }
        public QualificationDetails QualificationDetails
        {
            get { return _qualificationdetails; }
            set { _qualificationdetails = value; }
        }
        public class CrewMemberQualificationDetailsSelectDataParameters
        {
            private QualificationDetails _qualificationdetails;
            private SqlParameter[] _parameters;
            public CrewMemberQualificationDetailsSelectDataParameters(QualificationDetails qualificationdetails)
            {
                QualificationDetails = qualificationdetails;
                Build();
            }
            private void Build()
            {
                SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewQualificationId" , QualificationDetails.QualificationId )
            };

                Parameters = parameters;
            }
            public QualificationDetails QualificationDetails
            {
                get { return _qualificationdetails; }
                set { _qualificationdetails = value; }
            }
            public SqlParameter[] Parameters
            {
                get { return _parameters; }
                set { _parameters = value; }
            }
        }
    }
}
