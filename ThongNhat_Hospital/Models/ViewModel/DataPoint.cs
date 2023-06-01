using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Runtime.Serialization;

namespace ThongNhat_Hospital.Models.ViewModel
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, int y)
        { 
            this.Label = label;
            this.Y = y;
        }
        [DataMember(Name = "label")]
        public string Label = "";

        [DataMember(Name ="y")]
        public Nullable<int> Y = null;
    }
}
