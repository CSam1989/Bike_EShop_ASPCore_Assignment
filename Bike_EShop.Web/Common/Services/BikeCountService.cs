using Bike_EShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bike_EShop.Web.Common.Services
{
    public class BikeCountService : IBikeCountService
    {
        private const string folderUrl = "./wwwroot/images/bikes";
        public int Count()
        {
            return Directory.GetFiles(folderUrl).Length;
        }
    }
}
