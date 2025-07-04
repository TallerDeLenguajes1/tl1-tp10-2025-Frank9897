﻿using System.Net.Http.Json;
using System.Text.Json;
using System.Xml;
using estarea;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Cargando archivo...");
        string url = "https://jsonplaceholder.typicode.com/todos/";
        using HttpClient cliente = new HttpClient();
        
        HttpResponseMessage respuesta = await cliente.GetAsync(url);
        respuesta.EnsureSuccessStatusCode();

        string respuestaBody = await respuesta.Content.ReadAsStringAsync();
        List<Tarea> tareas = JsonSerializer.Deserialize<List<Tarea>>(respuestaBody);

        if (tareas == null)
        {
            Console.WriteLine("No se pudo obtener informacion del json.");
        }else
        {
            Console.WriteLine("\n-----------Tareas Realizadas-----------");
            foreach (var realizadas in tareas)
            {
                if (realizadas.completed)
                {
                    Console.WriteLine($"[X] Titulo: {realizadas.title}");
                }
            }
            Console.WriteLine("\n-----------Tareas Pendientes-----------");
            foreach (var pendientes in tareas)
            {
                if (!pendientes.completed)
                {
                    Console.WriteLine($"[ ] Titulo: {pendientes.title}");
                }
            }
            string guardarJson = JsonSerializer.Serialize(tareas, new JsonSerializerOptions{WriteIndented = true});
            string json = Path.Combine(Directory.GetCurrentDirectory(),"tareas.json");
            await File.WriteAllTextAsync(json,guardarJson);
        }

    }
}