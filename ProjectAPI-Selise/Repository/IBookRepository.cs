using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAPI_Selise.Models;

namespace ProjectAPI_Selise.Repository
{
    public interface IBookRepository
    {
        Task<bool> AddBook(BookModel bookModel);
        Task<BookModel> GetBookById(int id);
        Task<bool> EditBook(BookModel bookModel, int id);
        Task<bool> DeleteBook(int id);
        Task<List<BookModel>> GetAllRecords();
    }
}