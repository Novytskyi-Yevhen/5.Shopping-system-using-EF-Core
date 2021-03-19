using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskEFC.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Supermarket> supermarkets { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
