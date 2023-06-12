using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ThongNhat_Hospital.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string img { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string hoten { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CTDH> CTDH { get; set; }



    }
}
