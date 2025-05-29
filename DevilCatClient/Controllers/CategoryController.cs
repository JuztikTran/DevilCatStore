using DevilCatClient.Models;
using DevilCatClient.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevilCatClient.Controllers
{
    //[IgnoreAntiforgeryToken]
    public class CategoryController : Controller
    {
        private IUseApi _userApi;

        public CategoryController(IUseApi useApi)
        {
            _userApi = useApi;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _userApi.GetData("category");
            //Console.WriteLine(data ?? "empty data");
            var list = JsonConvert.DeserializeObject<OdataState<Category>>(data!);
            return View(list?.Value);
        }
    }
}
