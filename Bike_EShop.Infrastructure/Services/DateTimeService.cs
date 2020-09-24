using Bike_EShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Infrastructure.Services
{
    public class DateTimeService: IDateTime
    {
        //Is vooral voor testing purposes
        public DateTime Now => DateTime.Now;
    }
}
