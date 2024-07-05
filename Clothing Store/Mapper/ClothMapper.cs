using Clothing_Store.Models;
using ClothingStoreDomain;

namespace Clothing_Store.Mapper
{
    public class ClothMapper : IClothMapper
    {
        public Cloth AddCloth(AddClothViewModel addClothViewModel)
        {
            var cloth = new Cloth
            {
                Name = addClothViewModel.Name,
                Price = addClothViewModel.Price,
                Quantity = addClothViewModel.Quantity,
                ClothImage = addClothViewModel.ClothImage,
            };

            return cloth;
        }
    }
}
