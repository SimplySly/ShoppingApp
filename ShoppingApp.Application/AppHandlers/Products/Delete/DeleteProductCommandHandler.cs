using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Products.Delete;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetById(command.Id, cancellationToken);
        if (existingProduct == null)
        {
            return ProductErrors.ProductNotFound(command.Id);
        }

        _productRepository.Delete(existingProduct);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
