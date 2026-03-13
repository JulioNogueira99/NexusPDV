using MediatR;
using NexusPDV.Application.UseCases.Orders.GetById;
using NexusPDV.Application.UseCases.Product.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Product.GetById;

public record class GetProductByIdQuery(int Id) : IRequest<GetProductByIdResponse>;
