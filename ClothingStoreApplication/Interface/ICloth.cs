﻿using ClothingStoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreApplication.Interface
{
    public interface ICloth
    {
        bool AddCloth(Cloth cloth);

        List<Cloth> GetAllCloth();


    }
}
