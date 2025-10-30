using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Создает новый репозиторий для работы с Entity Framework
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных (необязательный)</param>
        public EntityRepository(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                _context = new AppDbContext(connectionString);
            }
            else
            {
                _context = new AppDbContext();
            }
        }

        /// <summary>
        /// Добавляет новую запись в базу данных
        /// </summary>
        /// <param name="entity">Объект для добавления</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Получает все записи из таблицы
        /// </summary>
        /// <returns>Коллекция всех записей</returns>
        public IEnumerable<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Находит запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор для поиска</param>
        /// <returns>Найденный объект или null</returns>
        public T ReadById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Обновляет существующую запись в базе данных
        /// </summary>
        /// <param name="entity">Объект с обновленными данными</param>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Освобождает ресурсы контекста базы данных
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}