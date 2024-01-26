using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPManagementNewMVVM.Model.ComponentModel
{
    internal class MainWindowItemList
    {
        public string Content { get; set; }
        public object Command { get; set; }
        public MainWindowItemList(string content, object command)
        {
            Content=content;
            Command=command;
        }
    }
}
