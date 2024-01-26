using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPManagementNewMVVM.Model.ComponentModel
{
    internal class RadioBtTranfser
    {
        public string Content { get; set; }
        public bool isChecked { get; set; }
        public RadioBtTranfser(string content, bool isChecked)
        {
            Content=content;
            this.isChecked=isChecked;
        }
    }
}
