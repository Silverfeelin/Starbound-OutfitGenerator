using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutfitGenerator.Generators
{
    public class MaskedHatGenerator : HatGenerator
    {
        public override byte[] Config => Properties.Resources.MaskedHatConfig;
    }
}
