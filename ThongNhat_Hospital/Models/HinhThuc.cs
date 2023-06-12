using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ThongNhat_Hospital.Models
{
    public class HinhThuc
    {
        [Column(TypeName = "varchar")]
        [StringLength(400)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CTDH> CTDH { get; set; }
    }
}
