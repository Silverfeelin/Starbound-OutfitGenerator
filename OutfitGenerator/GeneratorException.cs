using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutfitGenerator
{
    public class GeneratorException : Exception
    {
        public GeneratorException(string message) : base(message) { }
    }
}
