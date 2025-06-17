namespace estarea;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
    public class Tarea
    {
        public int userId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public bool completed { get; set; }
    }
