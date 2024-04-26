using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VendistaProject.Domain.Dto.Models;
using VendistaProject.Domain.Dto.Models.Interfaces;
using VendistaProject.UI.Services;

namespace VendistaProject.UI.Controllers
{ 
    public class BaseController : Controller 
    {
        protected readonly BaseApiService service = new BaseApiService();
        public IActionResult Index()
        {
            BaseViewModel model = new BaseViewModel(service);
            return View(model);
        }
    }
    public enum TypeAuth
    {
        Client,
        Partner
    }
    public class BaseViewModel
    { 
        public IEnumerable<IHistoryModel> histories { get; set; }
        public Command command { get; set; } 
        public BodyCommand viewCommand { get; set; }
        public BaseViewModel(BaseApiService service)
        {
            
            histories = service.GetHistories().Result;
            command = service.GetCommand(Services.TypeAuth.Partner).Result;
        }
        public List<SelectListItem> GetDropList()
        {
            var list = new List<SelectListItem>();
            foreach(var item in command.items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.name,
                    Value = item.id.ToString()
                });
            }
            return list;
        }

    }
}
