using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salerRecordService;

        public SalesRecordsController(SalesRecordService salesRecord)
        {
            _salerRecordService = salesRecord;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
                minDate = new DateTime(DateTime.Now.Year, 1, 1);   

            if (!maxDate.HasValue)
                maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salerRecordService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
                minDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!maxDate.HasValue)
                maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salerRecordService.FindByDateGroupingAsync(minDate, maxDate);

            return View(result);
        }
}
}
