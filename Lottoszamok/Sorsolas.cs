using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottoszamok
{
    class Sorsolas
    {
        public DateTime sorsolasNapja { get; set; }
        public List<int> sorsoltSzamok { get; set; }
        public Sorsolas(DateTime datum)
        {
            this.sorsolasNapja = datum;
        }

    }
}
