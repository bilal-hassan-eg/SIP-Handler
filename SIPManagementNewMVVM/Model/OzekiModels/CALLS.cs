using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.DataModel;
using Ozeki.VoIP;
namespace SIPManagementNewMVVM.Model.OzekiModels
{
    interface CALLS
    {
        //REGISTER SIPModel { get; set; }
        IPhoneCall PhoneCall { get; set; }
        NumberModel Call();
        void OpenSpeaker();
        void HangUP();
    }
}
