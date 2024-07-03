using ClothingStoreApplication.Interface;

namespace Clothing_Store.Controllers
{
    public class ClothController
    {
        private readonly ICloth _iCloth;

        public ClothController(ICloth iCloth)
        {
            _iCloth = iCloth;
        }


        //public IActionResult
    }
}
