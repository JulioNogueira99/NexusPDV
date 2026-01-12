using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Orders.GetById;
public record GetOrderByIdQuery(int Id) : IRequest<GetOrderByIdResponse>;
