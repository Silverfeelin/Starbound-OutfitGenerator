using System;
using System.Collections.Generic;
using System.Drawing;

namespace OutfitGenerator.Generators
{
    public class HatGenerator : ClothingGenerator
    {
        public override string FileName => throw new NotImplementedException();

        public override ISet<Size> SupportedDimensions => throw new NotImplementedException();
        
        public override Bitmap Template => throw new NotImplementedException();

        public override byte[] Config => throw new NotImplementedException();

        public override ItemDescriptor Generate(Bitmap bitmap)
        {
            throw new NotImplementedException();
        }
    }
}
