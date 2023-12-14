using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {

        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            OperationResult operation = new();
            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            ProductPicture productPicture = new(command.ProductId,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            OperationResult operation = new();
            ProductPicture productPicture = _productPictureRepository.GetById(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            productPicture.Edit(command.ProductId,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.SaveChanges();
            return operation.Succedded();   
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            OperationResult operation = new();
            ProductPicture productPicture = _productPictureRepository.GetById(id);
            
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            OperationResult operation = new();
            ProductPicture productPicture = _productPictureRepository.GetById(id);

            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
