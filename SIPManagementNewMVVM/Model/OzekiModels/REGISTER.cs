using Ozeki.VoIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPManagementNewMVVM.Model.OzekiModels
{
    interface REGISTER
    {
        ISoftPhone SoftPhone { get; set; }
        IPhoneLine PhoneLine { get; set; }
        int Regist();
        bool UnRegist();
    }
}
