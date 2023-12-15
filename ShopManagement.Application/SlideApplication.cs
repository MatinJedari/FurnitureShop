using _0_Freamwork.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            var slide = new Slide(command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Heading,
                command.Title,
                command.Text,
                command.BtnText);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetById(command.Id);

            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Edit(command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Heading,
                command.Title,
                command.Text,
                command.BtnText);

            _slideRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _slideRepository.GetSlides();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetById(id);

            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            
            slide.Remove();

            _slideRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetById(id);

            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Restore();

            _slideRepository.SaveChanges();
            return operation.Succedded();
        }
    }
}
