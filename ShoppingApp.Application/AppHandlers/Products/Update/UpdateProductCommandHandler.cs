using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Products.Update;

public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetById(command.Id, cancellationToken);
        if (existingProduct == null)
        {
            return ProductErrors.ProductNotFound(command.Id);
        }

        var sameNameProduct = await _productRepository.GetByName(command.Name, cancellationToken);
        if (sameNameProduct != null && sameNameProduct.Id != existingProduct.Id)
        {
            return ProductErrors.ProductAlreadyExists(command.Name);
        }

        if (command.Sku < 1)
        {
            return ProductErrors.InvalidSku();
        }

        if (command.Price < 0.01)
        {
            return ProductErrors.InvalidSku();
        }

        existingProduct.Name = command.Name;
        existingProduct.Sku = command.Sku;
        existingProduct.Price = command.Price;
        _productRepository.Update(existingProduct);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
