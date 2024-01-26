using Microsoft.Win32;
using SIPManagementNewMVVM.Model.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIPManagementNewMVVM.Model.DataHandel
{
    internal class AccountTextFile
    {
        public string FileName { get; set; }
        public AccountTextFile(string FileName) 
        {
            this.FileName = FileName;
        }

        public List<AccountModel> Handel()
        {
            string DataFile = File.ReadAllText(FileName);
            string[] DataFileByLine = DataFile.Split('\n');
            List<AccountModel> accounts = new List<AccountModel>();
            foreach (string item in DataFileByLine)
            {
                try
                {
                    string[] Account = item.Split('|');
                    Ozeki.VoIP.SIPAccount sip = new Ozeki.VoIP.SIPAccount();
                    sip.DomainServerHost = Account[0];
                    sip.DisplayName = Account[1];
                    sip.UserName = Account[2];
                    sip.RegisterName = Account[3];
                    sip.RegisterPassword = Account[4];
                    sip.OutboundProxy = Account[5];
                    AccountModel accountModel = new AccountModel(sip);
                    accounts.Add(accountModel);
                }
                catch
                {
                    MessageBox.Show("Format isnt Correct");
                    break;
                }
            }
            return accounts;
        }
    }
}
