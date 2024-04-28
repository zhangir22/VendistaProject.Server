using Microsoft.AspNetCore.Mvc;
using VendistaProject.UI.Services;
using VendistaProject.UI.Services.Interfaces;
using VendistaProject.UI.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Dto.Models;

namespace VendistaProject.UI.Controllers
{
    public class UIController : Controller
    {
        protected BaseApiService service = new BaseApiService();
        protected BaseViewModel model;
        protected Builder builder;
        public UIController()
        {
            model = new BaseViewModel(service);
        }

        public IActionResult Index(string? value = null)
        {
            if (value != null)
                model.bodyCommand = model.command.items[int.Parse(value) - 1];
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendDataDB(string userIdTerminals, string lastcommand, string? param1, string? param2, string? param3)
        {
            builder = new Builder(service);
            var command = builder.BuildMultyCommand(
                service.GetCommand(Services.TypeAuth.Partner).Result.items.FirstOrDefault(name => name.name == lastcommand).id,
                param1, param2, param3);

            var result =
                await service.SendCommand(
                Services.TypeAuth.Partner,
                int.Parse(userIdTerminals),
                command,
                lastcommand
                );
            if (result != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
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
        public BodyCommand bodyCommand { get; set; }
        public BaseViewModel(BaseApiService service)
        {

            histories = service.GetHistories().Result;
            command = service.GetCommand(Services.TypeAuth.Partner).Result;

        }
        public List<SelectListItem> GetDropList()
        {
            var list = new List<SelectListItem>();
            foreach (var item in command.items)
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
