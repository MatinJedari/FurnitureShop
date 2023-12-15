using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(product => product.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            Product product = new Product(command.Name,
                command.Code,
                command.UnitPrice,
                command.ShortDescription,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.CategoryId,
                command.Slug,
                command.Keywords,
                command.MetaDescription); 

            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetById(command.Id);

            if (product == null) 
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productRepository.Exists(product => product.Name == command.Name && product.Id == command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            string slugifiedSlug = command.Slug.Slugify();
            product.Edit(command.Name,
                command.Code,
                command.UnitPrice,
                command.ShortDescription,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.CategoryId,
                slugifiedSlug,
                command.Keywords,
                command.MetaDescription);

            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProduct GetDetail(long id)
        {
            return _productRepository.GetDetail(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public OperationResult InStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetById(id);

            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            product.InStock();
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetById(id);

            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            product.NotInStock();
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
