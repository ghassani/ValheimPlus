using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValheimPlus.Utility
{
    public interface IZPackageable
    {
        void Serialize(ZPackage package);

        void Unserialize(ZPackage package);
    }
}
