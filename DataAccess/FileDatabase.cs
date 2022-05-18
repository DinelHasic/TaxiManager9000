using Newtonsoft.Json;
using TaxiManager9000.DataAccess.Interface;
using TaxiManager9000.Domain.Entities;

namespace TaxiManager9000.DataAccess
{
    public class FileDatabase<T> : IDatabase<T> where T : BaseEntity
    {
        protected List<T> Items;

        private readonly string _filePath;

        public FileDatabase()
        {
            _filePath = $@"{Directory.GetCurrentDirectory()}\{typeof(T).Name}.json";

            if (!(File.Exists(_filePath)))
            {
                var file = File.Create(_filePath);
                file.Close();
            }

/*
            Items = new List<T>();*/
            Task readFromFile = new(async () => Items = await ReadrFromFileAsync());
            readFromFile.Start();
            readFromFile.Wait();
        }

        public async Task UpdateAsync()
        {
            await SaveToFileAsync(Items);
        }

        private async Task SaveToFileAsync(List<T> items)
        {
            using StreamWriter sw = new(_filePath);

            string json =  JsonConvert.SerializeObject(items, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            sw.Write(json);
            sw.Close();
        }

        private async Task<List<T>> ReadrFromFileAsync()
        {
            string json;

            using (StreamReader read = new(_filePath))
            {
                json = await read.ReadToEndAsync();
                read.Close();
            }

            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public async Task InsertAsync(T data)
        {
            T dataToInsert = AutoIncrementId(data);

            Items.Add(dataToInsert);

            await UpdateAsync();
        }

        public T AutoIncrementId(T data)
        {
            int currentId = 0;

            if (Items.Count > 0)
            {
                currentId = Items.OrderBy(x => x.Id).Last().Id;
            }

            data.Id = ++currentId;

            return data;
        }
    }
}
