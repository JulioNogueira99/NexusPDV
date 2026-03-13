using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.GetAll;

public class GetAllProductsQuery : IRequest<IEnumerable<GetAllProductsResponse>>
{
}
