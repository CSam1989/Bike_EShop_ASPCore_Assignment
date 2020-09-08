using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Products.Queries.GetProducts
{
    public class GetProductsQuery: IRequest<ProductsVM>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVM>
        {
            public async Task<ProductsVM> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var vm = new ProductsVM
                {
                    List = new List<ProductsDto>
                    {
                        new ProductsDto
                        {
                            Id = 1,
                            Name= "Bike 1",
                            Price= 1000m
                        },
                        new ProductsDto
                        {
                            Id = 2,
                            Name= "Bike 2",
                            Price= 1250m
                        },
                        new ProductsDto
                        {
                            Id = 3,
                            Name= "Bike 3",
                            Price= 1500m
                        },
                        new ProductsDto
                        {
                            Id = 4,
                            Name= "Bike 4",
                            Price= 750m
                        }
                    }
                };

                return vm;
            }
        }
    }
}
