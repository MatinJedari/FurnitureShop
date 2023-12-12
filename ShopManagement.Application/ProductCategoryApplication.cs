using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            OperationResult operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            ProductCategory productCategory = new ProductCategory(
                command.Name,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                command.MetaDescription,
                slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            OperationResult operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetById(command.Id);

            if (productCategory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id == command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            productCategory.Edit(
                command.Name,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                command.MetaDescription,
                slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProductCategory GetDetail(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }


    }
}
