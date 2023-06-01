using System.Collections.Generic;

namespace ThongNhat_Hospital.Models.ViewModel
{
    public class CTDonViewModel
    {
        public List<CTDH> CTDHs { get; set; }

        public string nguoinhan { get; set; }
        public List<User> Usernhan { get; set; }
    }
}
