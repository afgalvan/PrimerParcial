using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data.Utils
{
    public class JsonFileChannel : IFileUpdater, IFileContentMapper
    {
        private readonly JsonSerializerSettings _jsonSettings;

        public JsonFileChannel(JsonSerializerSettings jsonSettings)
        {
            _jsonSettings = jsonSettings;
        }

        public async Task UpdateFileWith<TEntity>(UpdateContent<TEntity> updateContent,
            CancellationToken cancellation)
        {
            (string fileName, IEnumerable<TEntity> entities) = updateContent;

            string serializedObject = JsonConvert.SerializeObject(entities, _jsonSettings);
            await File.WriteAllTextAsync(fileName, serializedObject, cancellation);
        }

        public async Task<IEnumerable<TEntity>> MapFileContent<TEntity>(string filePath)
        {
            if (!File.Exists(filePath)) return new List<TEntity>();

            using StreamReader fileReader = File.OpenText(filePath);
            string             content    = await fileReader.ReadToEndAsync();
            return DeserializeSafely<TEntity>(content);
        }

        private IEnumerable<TEntity> DeserializeSafely<TEntity>(string content)
        {
            return (string.IsNullOrEmpty(content)
                ? new List<TEntity>()
                : JsonConvert.DeserializeObject<List<TEntity>>(content, _jsonSettings))!;
        }
    }
}
