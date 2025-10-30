using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        /// <summary>
        /// Создает новый репозиторий для работы с базой данных
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        /// <param name="tableName">Название таблицы (если не указано, будет использовано имя типа + 's')</param>
        public DapperRepository(string connectionString, string tableName = null)
        {
            _connectionString = connectionString;
            _tableName = tableName ?? typeof(T).Name + "s"; // Например: Worker -> Workers
        }

        /// <summary>
        /// Добавляет новую запись в базу данных
        /// </summary>
        /// <param name="entity">Объект для добавления</param>
        public void Add(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
                var columns = string.Join(", ", properties.Select(p => p.Name));
                var parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

                var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})";
                connection.Execute(sql, entity);
            }
        }

        /// <summary>
        /// Удаляет запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Получает все записи из таблицы
        /// </summary>
        /// <returns>Коллекция всех записей</returns>
        public IEnumerable<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<T>($"SELECT * FROM {_tableName}").ToList();
            }
        }

        /// <summary>
        /// Находит запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор для поиска</param>
        /// <returns>Найденный объект или null</returns>
        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет существующую запись в базе данных
        /// </summary>
        /// <param name="entity">Объект с обновленными данными</param>
        public void Update(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
                var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

                var sql = $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id";
                connection.Execute(sql, entity);
            }
        }
    }
}