using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PjsipDialer
{
    [XmlRoot("PjsipConfig", Namespace = "https://www.pjsip.org/", IsNullable = false)]
    public class PjsipConfig
    {
        [XmlArray("Items")]
        public PjsipAccount[] Accounts;
    }
}
