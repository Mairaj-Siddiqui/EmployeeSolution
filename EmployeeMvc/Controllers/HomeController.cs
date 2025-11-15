using Microsoft.AspNetCore.Mvc;
using EmployeeMvc.Models;
using System.Net.Http.Json;

namespace EmployeeMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("EmployeeApi");
        }

        //public async Task<IActionResult> Index()
        //{
        //    var employees = await _httpClient.GetFromJsonAsync<List<Employee>>("api/employees");
        //    return View(employees);
        //}

        public async Task<IActionResult> Create()  // GET
        {
            await Task.CompletedTask;  // placeholder await (does nothing)
            // Provide default values for required properties
            return View(new Employee { Name = string.Empty, Email = string.Empty });  // empty form
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)  // POST
        {
            if (!ModelState.IsValid) return View(model);

            // send to API
            var resp = await _httpClient.PostAsJsonAsync("api/employees", model);
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"API error: {(int)resp.StatusCode}");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Home/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
            if (employee == null) return NotFound();
            return View(employee);
        }
        // POST: /Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var resp = await _httpClient.PutAsJsonAsync($"api/employees/{id}", model);
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"API error: {(int)resp.StatusCode}");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Home/Delete/1  -> simple confirm page
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resp = await _httpClient.DeleteAsync($"api/employees/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"API error: {(int)resp.StatusCode}");
                var employee = await _httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
                return View(employee);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            ViewBag.SearchString = searchString;

            var url = string.IsNullOrWhiteSpace(searchString)
                ? "api/employees"
                : $"api/employees?searchString={Uri.EscapeDataString(searchString)}";

            var employees = await _httpClient.GetFromJsonAsync<List<Employee>>(url)
                           ?? new List<Employee>();

            return View(employees);
        }



    }
}
