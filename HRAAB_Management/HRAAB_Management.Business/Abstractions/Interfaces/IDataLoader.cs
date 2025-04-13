using HRAAB_Management.Business.Entities;

namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface IDataLoader
    {
        /// <summary>
        /// Reads data from files
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> LoadAsync<T>(string filePath) where T : IEntity, new();
    }
}
