using _01_FurnitureShopQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Components
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slide = _slideQuery.GetSlides();
            return View(slide);
        }
    }
}
