using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public ICollection<CTDH> CTDH { get; set; }



    }
}
