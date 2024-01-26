using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ozeki.Common;
using Ozeki.VoIP;
using SIPManagementNewMVVM.Model.DataModel;

namespace SIPManagementNewMVVM.Model.DataHandel
{
    static class Export
    {
        public static List<int> CodesNow = new List<int>();
        public static object OBJ = new object();
        public static object OBJ1 = new object();
        public static void ExportAccount(SIPAccount accountModel, string DictName, int statusCode)
        {
            lock (OBJ)
            {
                using (StreamWriter writer = new StreamWriter($"./{DictName}/account_{statusCode}.txt", true))
                {
                    string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}";

                    writer.Write(AccountLine);
                }

            }
        }
        public static void ExportCall(SIPAccount accountModel , string DictName , int statusCode , NumberModel number , string reason)
        {
            lock (OBJ1)
            {
                using (StreamWriter writer = new StreamWriter($"./{DictName}/call_{number.PhoneNumber.Trim()}_{statusCode.ToString().Trim()}.txt", true))
                {
                    string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy.Trim()}|{number.AccountModel.state}|{number.PhoneNumber}|{statusCode}:{reason}\n";

                    writer.Write(AccountLine);
                }

            }
        }
        public static void ExportAttend(ICall numberFrom , ICall numberTo , SIPAccount accountModel) 
        {
            lock (OBJ)
            {
                string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}|{numberFrom.DialInfo.Dialed}:{numberTo.DialInfo.Dialed}:Attend\n";
                if (!File.Exists($"./transfer_calls/{numberFrom.DialInfo.Dialed}_{numberTo.DialInfo.Dialed}_Attend.txt"))
                {
                    File.CreateText($"./transfer_calls/{numberFrom.DialInfo.Dialed}_{numberTo.DialInfo.Dialed}_Attend.txt");
                }
                File.AppendAllText($"./transfer_calls/{numberFrom.DialInfo.Dialed}_{numberTo.DialInfo.Dialed}_Attend.txt", AccountLine);
            }

        }
        public static void ExportBlind(ICall number , string phoneNumber , SIPAccount accountModel)
        {
            lock(OBJ)
            {
                string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}|{number.DialInfo.Dialed}:{phoneNumber}:Blind\n";
                if (!File.Exists($"./transfer_calls/{number.DialInfo.Dialed}_{phoneNumber}_Blind.txt"))
                {
                    File.CreateText($"./transfer_calls/{number.DialInfo.Dialed}_{phoneNumber}_Blind.txt");
                }
                File.AppendAllText($"./transfer_calls/{number.DialInfo.Dialed}_{phoneNumber}_Blind.txt", AccountLine);
            }

        }
    }
}
