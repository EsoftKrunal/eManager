using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class Authority
    {
        private int _Userid;
        private int _ApplicationModuleId;
        private bool _isAdd;
        private bool _isEdit;
        private bool _isDelete;
        private bool _isPrint;
        private bool _isVerify;
        private bool _isVerify2;
        private bool _isFrstApp;
        private bool _isSndApp;
        public Authority()
        {
        }
        public int UserId
        {
            get { return _Userid; }
            set { _Userid = value; }
        }
        public int ApplicationModuleId
        {
            get { return _ApplicationModuleId; }
            set { _ApplicationModuleId = value; }
        }
        public bool isAdd
        {
            get { return _isAdd; }
            set { _isAdd = value; }
        }
        public bool isEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; }
        }
        public bool isDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }
        public bool isPrint
        {
            get { return _isPrint; }
            set { _isPrint = value; }
        }
        public bool isVerify
        {
            get { return _isVerify; }
            set { _isVerify = value; }
        }
        public bool isVerify2
        {
            get { return _isVerify2; }
            set { _isVerify2 = value; }
        }

        public bool isFirstApproval
        {
            get
            {
                return _isFrstApp;
            }
            set
            {
                _isFrstApp = value;
            }
        }

        public bool isSecondApproval
        {
            get
            {
                return _isSndApp;
            }
            set
            {
                _isSndApp = value;
            }
        }
    }
}
