using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentProperties
    {
        
        #region Properties Value

        public long DocumentFileId { get; set; }
        public string Field1Value {get;set;}
        public string Field2Value { get; set; }
        public string Field3Value { get; set; }
        public string Field4Value { get; set; }
        public string Field5Value { get; set; }
        public string Field6Value { get; set; }
        public string Field7Value { get; set; }
        public string Field8Value { get; set; }
        public string Field9Value { get; set; }
        public string Field10Value { get; set; }
        public long PropertyValueCreatedBy { get; set; }
        public DateTime PropertyValueCreatedOn { get; set; }
        public long PropertyValueModifiedBy { get; set; }
        public DateTime? PropertyValueModifiedOn { get; set; }
        public string PropertyValueCreatedByUserName { get; set; }
        public string PropertyValueCreatedByUserFullName { get; set; }
        public string PropertyValueModifiedByUserName { get; set; }
        public string PropertyValueModifiedByUserFullName { get; set; }

        #endregion

        public string FileName {get; set;}
        public string FolderName { get; set; }

        public DocumentPropertiesNames Propertynames { get; set; }
    }
}
