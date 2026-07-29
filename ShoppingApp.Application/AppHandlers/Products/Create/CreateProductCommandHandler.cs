using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Products.Create;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateEntityResponseDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, 
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateEntityResponseDto>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetByName(command.Name, cancellationToken);
        if (existingProduct != null)
        {
            return Result.Failure<CreateEntityResponseDto>(ProductErrors.ProductAlreadyExists(command.Name));
        }

        if (command.Sku < 1)
        {
            return Result.Failure<CreateEntityResponseDto>(ProductErrors.InvalidSku());
        }

        if (command.Price < 0.01)
        {
            return Result.Failure<CreateEntityResponseDto>(ProductErrors.InvalidPrice());
        }

        var newProduct = new Product()
        {
            Name = command.Name,
            Sku = command.Sku,
            Price = command.Price
        };

        _productRepository.Add(newProduct);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success(new CreateEntityResponseDto(newProduct.Id));
    }
}
