using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Operations_
{
    public class TransactionLog
    {
        public string UserName { get; set; }
        public string Action { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
