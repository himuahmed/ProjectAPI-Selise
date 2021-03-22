using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_Selise.Data;
using ProjectAPI_Selise.Models;

namespace ProjectAPI_Selise.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }
        public async Task<bool> AddBook(BookModel bookModel)
        {

           await _bookContext.Books.AddAsync(bookModel);
           return await _bookContext.SaveChangesAsync() > 0;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _bookContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteBook(int id)
        {
            _bookContext.Books.Remove(await GetBookById(id));
            return await _bookContext.SaveChangesAsync()>0;
        }

        public async Task<List<BookModel>> GetAllRecords()
        {
            return await _bookContext.Books.ToListAsync();
            
        }

        public async Task<bool> EditBook(BookModel bookModel, int id)
        {
            var book = await GetBookById(id);
            if (book != null)
            {
                book.Name = bookModel.Name;
                book.Author = bookModel.Author;
                book.Language = bookModel.Language;
                book.Pages = bookModel.Pages;
                book.PublishDate = bookModel.PublishDate;

                return await _bookContext.SaveChangesAsync() > 0;
            }

            return false;
        }

    }
}
