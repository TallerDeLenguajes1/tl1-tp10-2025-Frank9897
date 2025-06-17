using System.Text.Json;
using esusuarios;
class Program
{
    static async Task Main(string[] args)
    {
        HttpClient cliente = new HttpClient();
        string url = "https://jsonplaceholder.typicode.com/users";

        HttpResponseMessage respuesta = await cliente.GetAsync(url);
        respuesta.EnsureSuccessStatusCode();

        string respuestaBody = await respuesta.Content.ReadAsStringAsync();
        List<Usuarios.Root> usuarios = JsonSerializer.Deserialize<List<Usuarios.Root>>(respuestaBody);

        foreach (var usuario in usuarios)
        {
            if (usuario.id < 6)
            {
                Console.WriteLine($"\n******Usuario {usuario.id}******\n");
                Console.WriteLine($"Nombre: {usuario.name}\nCorreo: {usuario.email}\nDomicilio: {usuario.address}");
            }
        }
    
        string guardarJson = JsonSerializer.Serialize(usuarios);
        string json = Path.Combine(Directory.GetCurrentDirectory(),"usuarios.json");
        await File.WriteAllTextAsync(json,guardarJson);

        
    }

    
}