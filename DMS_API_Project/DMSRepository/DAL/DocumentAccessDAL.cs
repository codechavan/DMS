using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Model;
using DMS.Repository.SQL;
using DMS.DatabaseConstants;
using System.Data;
using System.Collections;

namespace DMS.Repository.DAL
{
    public abstract class DocumentAccessDAL
    {
        protected string ConnectionStringName = String.Empty;

        public DocumentAccessDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus CreateUpdateDocumentAccess(DocumentAccess paramValue);

        public abstract DocumentAccess GetDocumentAccess(DocumentAccessSearchParameter searchParameters);

    }
}
