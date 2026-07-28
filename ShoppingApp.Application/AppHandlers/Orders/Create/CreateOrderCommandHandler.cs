using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Orders.Create;

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IAuthRepository _authRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IAuthRepository authRepository, 
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _authRepository = authRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var user = await _authRepository.GetUserById(command.userId, cancellationToken);
        if (user == null)
        {
            return AuthErrors.UserNotFound();
        }

        var newOrder = new Order()
        {
            UserId = user.Id
        };

        foreach (var orderItemDto in command.orderItems)
        {
            var product = await _productRepository.GetById(orderItemDto.ProductId, cancellationToken);

            if (product == null)
            {
                return ProductErrors.ProductNotFound(orderItemDto.ProductId);
            }

            if (product.Sku - orderItemDto.Quantity < 0)
            {
                return OrderErrors.ProductOutOfStock(product.Name);
            }

            newOrder.OrderItems.Add(new OrderItem()
            {
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
            });

            product.Sku -= orderItemDto.Quantity;
        }

        _orderRepository.Add(newOrder);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
