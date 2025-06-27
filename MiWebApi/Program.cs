using System.Text.Json;
using esapi;

class Program
{
    static async Task Main(string[] args)
    {
        HttpClient cliente = new HttpClient();
        string url = "https://catfact.ninja/fact";
        HttpResponseMessage respuesta = await cliente.GetAsync(url);
        respuesta.EnsureSuccessStatusCode();

        string respuestabody = await respuesta.Content.ReadAsStringAsync();
        Root? randomgato = JsonSerializer.Deserialize<Root>(respuestabody);


        if (randomgato != null)
        {
            Console.WriteLine($"Curiosidad: {randomgato.fact}\nLongitud: {randomgato.length}");

            string guardarJson = JsonSerializer.Serialize(randomgato);
            string json = Path.Combine(Directory.GetCurrentDirectory(), "gato.json");
            await File.WriteAllTextAsync(json, guardarJson);
        }
        else
        {
            Console.WriteLine("Error al deserializar la respuesta.");
        }
    }

}