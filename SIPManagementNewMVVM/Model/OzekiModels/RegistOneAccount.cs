using System;
using System.IO;
using Ozeki.VoIP;
using SIPManagementNewMVVM.Model.DataModel;
using System.Diagnostics;
using System.Windows;

namespace SIPManagementNewMVVM.Model.OzekiModels
{
    internal class RegistOneAccount 
    {
        public ISoftPhone SoftPhone { get; set; }
        public IPhoneLine PhoneLine { get; set; }
        public AccountModel AccountModel { get; set; }
        private Stopwatch Stopwatch { get; set; }


        public RegistOneAccount(AccountModel DataRegist)
        {
            AccountModel = DataRegist;
            SoftPhone = SoftPhoneFactory.CreateSoftPhone(20000, 205000, AccountModel.UserAgent);
            Stopwatch = new Stopwatch();
        }
        public void Regist()
        {
            try
            {
                PhoneLineConfiguration configuration = new PhoneLineConfiguration(AccountModel.SIPAccount);
                switch (AccountModel.TransportType)
                {
                    case "TCP":
                        configuration.TransportType = Ozeki.Network.TransportType.Tcp;
                        break;
                    case "UDP":
                        configuration.TransportType = Ozeki.Network.TransportType.Udp;
                        break;
                }
                PhoneLine = SoftPhone.CreatePhoneLine(configuration);
                SIPAccount accountModel = AccountModel.SIPAccount;
                PhoneLine.RegistrationStateChanged += delegate (object sender, RegistrationStateChangedArgs e) {
                    // export accounts to txt file
                    string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}|{e.StatusCode}\n";
                    AccountModel._state = e.StatusCode;
                    File.AppendAllText($"./Home_Page/account_{e.StatusCode}.txt", AccountLine);
                };
                if (PhoneLine.SIPAccount.RegistrationRequired)
                {
                    AccountModel._state = 200;
                }
                SoftPhone.RegisterPhoneLine(PhoneLine);
                if (AccountModel.RegistTime != 0)
                {
                    this.Stopwatch.Start();
                    while (Stopwatch.ElapsedMilliseconds <= AccountModel.RegistTime)
                    {


                    }
                    Stopwatch.Stop();
                    Stopwatch.Reset();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Debug errors
            }
        }
        public void UnRegist()
        {
            try
            {
                SoftPhone.UnregisterPhoneLine(PhoneLine);
                //return true;
            } 
            catch(Exception ex){
                MessageBox.Show(ex.Message); // Debug errors
                //return false;
            }   
        }
    }
}
