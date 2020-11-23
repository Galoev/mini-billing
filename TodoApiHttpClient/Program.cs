using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TodoApiHttpClient.Models;

namespace TodoApiHttpClient
{
    class Program
    {
        private static HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            CallWebAPIAsync().GetAwaiter().GetResult();
        }

        static async Task<TodoItemDTO> GetTodoItemAsync(string path)
        {
            TodoItemDTO todoItem = null;
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                todoItem = await response.Content.ReadAsAsync<TodoItemDTO>();
            }
            return todoItem;
        }

        static async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            IEnumerable<TodoItemDTO> todoItems = null;
            HttpResponseMessage response = await httpClient.GetAsync("api/TodoItems");
            if (response.IsSuccessStatusCode)
            {
                /*                string json = await response.Content.ReadAsStringAsync();
                                todoItems = JsonConvert.DeserializeObject<List<TodoItemDTO>>(json);*/
                todoItems = await response.Content.ReadAsAsync<IEnumerable<TodoItemDTO>>();
            }
            return todoItems;
        }

        static async Task<Uri> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/TodoItems", todoItemDTO);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<HttpStatusCode> UpdateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                $"api/TodoItems/{todoItemDTO.Id}", todoItemDTO);
            return response.StatusCode;
        }

        static async Task<HttpStatusCode> DeleteTodoItemAsync(long id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(
                $"api/TodoItems/{id}");
            return response.StatusCode;
        }

        static async Task CallWebAPIAsync()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44311/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new todoItem
                TodoItemDTO todoItemDTO = new TodoItemDTO
                {
                    Id = 1,
                    Name = "second algo contest",
                    IsComplete = false
                };

                var url = await CreateTodoItemAsync(todoItemDTO);
                Console.WriteLine($"Created at {url}");

                // Get the todoItem
                todoItemDTO = await GetTodoItemAsync(url.PathAndQuery);
                ShowResult(todoItemDTO);

                // Update the todoItem
                Console.WriteLine("Updating name...");
                todoItemDTO.Name = "xe-xe";
                await UpdateTodoItemAsync(todoItemDTO);

                // Get the todoItems
                IEnumerable<TodoItemDTO> todoItems = await GetTodoItemsAsync();
                foreach (TodoItemDTO item in todoItems)
                {
                    ShowResult(item);
                }

                // Delete the todoItem
                var statusCode = await DeleteTodoItemAsync(todoItemDTO.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        
        static void ShowResult(TodoItemDTO todoItemDTO)
        {
            Console.WriteLine($"Name: {todoItemDTO.Name}\tIsComplete: " +
                $"{todoItemDTO.IsComplete}\tId: {todoItemDTO.Id}");
        }
    }
}
