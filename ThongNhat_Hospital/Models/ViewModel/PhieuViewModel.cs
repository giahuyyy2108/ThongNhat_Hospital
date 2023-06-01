using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Collections.Generic;

namespace ThongNhat_Hospital.Models.ViewModel
{
    public class PhieuViewModel
    {
        public List<CTDH> CTphieu { get; set; }

        public string nguoigui { get; set; }

        public string nguoinhan { get; set; }

        public string email_usergui { get; set; }

        public string chuky_usergui { get; set; }

        public string email_usernhan { get; set; }

        public string chuky_usernhan { get; set; }

        public string ngay_usergui { get; set; }

        public string ngay_usernhan{ get; set; }


    }
}
