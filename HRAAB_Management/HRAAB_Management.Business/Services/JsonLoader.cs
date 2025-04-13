using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.Entities;
using Newtonsoft.Json;

namespace HRAAB_Management.Business.Services
{
    public class JsonLoader : IDataLoader
    {
        public async Task<IReadOnlyList<T>> LoadAsync<T>(string filePath) where T : IEntity, new()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            using StreamReader reader = new StreamReader(filePath);
            string json = await reader.ReadToEndAsync();

            try
            {
                IReadOnlyList<T>? data = JsonConvert.DeserializeObject<IReadOnlyList<T>>(json);
                return data ?? [];
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize JSON data from {filePath}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while loading data from {filePath}", ex);
            }
        }
    }
}
