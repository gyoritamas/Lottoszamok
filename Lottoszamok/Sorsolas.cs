using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottoszamok
{
    class Sorsolas
    {
        private DateTime sorsolasNapja;
        public int MyProperty { get; set; }
        public Sorsolas(DateTime datum)
        {
            this.sorsolasNapja = datum;
        }

    }
}
