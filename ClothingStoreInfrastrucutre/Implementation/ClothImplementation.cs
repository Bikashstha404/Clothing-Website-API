using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using ClothingStoreInfrastrucutre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastrucutre.Implementation
{
    public class ClothImplementation : ICloth
    {
        private readonly ClothDbContext _dbContext;

        public ClothImplementation(ClothDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Cloth AddCloth(Cloth cloth)
        {
            _dbContext.Add(cloth);
            _dbContext.SaveChanges();
            return cloth;
        }

        public List<Cloth> GetAllCloth()
        {
            throw new NotImplementedException();
        }
    }
}
