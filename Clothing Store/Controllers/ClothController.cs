using Clothing_Store.Mapper;
using Clothing_Store.Models;
using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using Microsoft.AspNetCore.Mvc;

namespace Clothing_Store.Controllers
{
    public class ClothController : Controller
    {
        private readonly ICloth _iCloth;
        private readonly IClothMapper _iClothMapper;

        public ClothController(ICloth iCloth, IClothMapper iClothMapper)
        {
            _iCloth = iCloth;
            _iClothMapper = iClothMapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Message = "Welcome to Add Cloth Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClothViewModel addClothViewModel)
        {
            var cloth = _iClothMapper.AddCloth(addClothViewModel);
            var result = _iCloth.AddCloth(cloth);
            ViewBag.Message = "Cloth added successfully";
            return View();
        }
    }
}
