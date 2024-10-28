using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient client;

        public MovieController()
        {
            var clientAddress = new Uri("http://ec2-44-202-20-186.compute-1.amazonaws.com");
            this.client = new HttpClient();
            this.client.BaseAddress = clientAddress;
        }

        public async Task<IActionResult> Index()
        {
            var movies = new List<Movie>();
            var response = await client.GetAsync("/api/movies");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<Movie>>(message);
            }

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = new Movie();
            var response = await client.GetAsync($"/api/movies/{id}");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                movie = JsonConvert.DeserializeObject<Movie>(message);
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            return View(new Movie());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var serialized = JsonConvert.SerializeObject(movie);
                var payload = new StringContent(serialized, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/movies", payload);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(movie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = new Movie();
            var response = await client.GetAsync($"/api/movies/{id}");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                movie = JsonConvert.DeserializeObject<Movie>(message);
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Movie movie)
        {
            if (ModelState.IsValid)
            {
                var serialized = JsonConvert.SerializeObject(movie);
                var payload = new StringContent(serialized, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/movies/{id}", payload);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(movie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = new Movie();
            var response = await client.GetAsync($"/api/movies/{id}");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                movie = JsonConvert.DeserializeObject<Movie>(message);
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Movie movie)
        {
            var response = await client.DeleteAsync($"/api/movies/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }
    }
}
