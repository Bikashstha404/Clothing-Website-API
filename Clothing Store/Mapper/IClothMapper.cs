using Clothing_Store.Models;
using ClothingStoreDomain;

namespace Clothing_Store.Mapper
{
    public interface IClothMapper
    {
        Cloth AddCloth(AddClothViewModel addClothViewModel);
    }
}
