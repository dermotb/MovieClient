
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetAMovie().Wait();
            PostAMovie().Wait();
            GetAllMovies().Wait();
            UpdateAMovie().Wait();
            GetAllMovies().Wait();
            DeleteAMovie().Wait();
            GetAllMovies().Wait();

        }

        private static async Task UpdateAMovie()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50873/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PutAsJsonAsync("api/Movies/4", "The Very Loud Man");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie Updated!");
            }
            else
            {
                Console.WriteLine(response.StatusCode + " Reason Phrase: " + response.ReasonPhrase);

            }
        }

        private static async Task DeleteAMovie()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50873/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync("api/Movies/4");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie deleted!");
            }
            else
            {
                Console.WriteLine(response.StatusCode + " Reason Phrase: " + response.ReasonPhrase);

            }
        }


        private static async Task PostAMovie()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50873/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            Movie newMovie = new Movie() { Title = "The Quiet Man", Genre = Genres.action, Cert = Certification.Twelve, AvgRating = 9, ReleaseDate = new DateTime(1952, 10, 19) };
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Movies", newMovie);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Movie added!");
            }
            else
            {
                Console.WriteLine(response.StatusCode + " Reason Phrase: " + response.ReasonPhrase);

            }
        }

        private static async Task GetMoviesByWord()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50873/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Movies/KeyW/The");

            if (response.IsSuccessStatusCode)
            {
                Movie movie = await response.Content.ReadAsAsync<Movie>();
                if (movie != null)
                {
                    Console.WriteLine("Got one movie, it is: " + movie.ToString());
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode + " Reason Phrase: " + response.ReasonPhrase);

            }
        }


        private static async Task GetAMovie()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50873/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Movies/10");

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<String> movies = await response.Content.ReadAsAsync<IEnumerable<String>>();

                foreach (String s in movies)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode + " Reason Phrase: " + response.ReasonPhrase);

            }
        }

        private static async Task GetAllMovies()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50873/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Movies");

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Movie> movies = await response.Content.ReadAsAsync<IEnumerable<Movie>>();

                foreach(Movie mv in movies)
                {
                    Console.WriteLine(mv);
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode+" Reason Phrase: "+response.ReasonPhrase);
            }
        }
    }
}


