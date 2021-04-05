using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectAPI_Selise.Models;
using ProjectAPI_Selise.Repository;

namespace ProjectAPI_Selise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost("addbook")]
        public async Task<IActionResult> AddBook(BookModel[] bookList)
        {
            List<BookModel> errorBookList = new List<BookModel>();
            
            
            foreach (var bookModel in bookList)
            {
                if (ModelState.IsValid)
                {
                    var result = await _bookRepository.AddBook(bookModel);
                    if (result != true)
                    {
                        errorBookList.Add(bookModel);
                    }
                }
                else
                {
                    errorBookList.Add(bookModel);
                }
            }

            if (errorBookList.Count < 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to add record.");
            }
            //throw new Exception("Failed to add some records.");
            //if (ModelState.IsValid)
            //{
            //    var result = await _bookRepository.AddBook(bookModel);
            //    if (result == true)
            //    {
            //        return Ok();
            //    }
            //    return BadRequest("Failed to add record.");
            //}
            //throw new Exception("Failed to add record.");
        }

        [HttpGet("getbook/{id:int}")]
        public async Task<IActionResult> GetBookDetails(int id)
        {
            var book =await _bookRepository.GetBookById(id);

            if (book != null)
            {
                return Ok(book);
            }

            throw new Exception("Couldn't find the book.");

        }

        [HttpDelete("deletebook/{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookRepository.DeleteBook(id);
            if (result == true)
            {
                return Ok();
            }
            throw new Exception("Couldn't delete the record.");
        }

        [HttpGet("getallbooks")]
        public async Task<IActionResult> GetAllBooks()
        {
           var books= await _bookRepository.GetAllRecords();

           if (books != null)
           {
               return Ok(books);
           }
           throw new Exception("No records.");
        }

        [HttpPut("editbook/{id:int}")]
        public async Task<IActionResult> EditBookInfo(int id, BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = await _bookRepository.EditBook(bookModel, id);
                if (isUpdated)
                {
                    return Ok();
                }
                throw new Exception("Failed to update info.");
            }
            throw new Exception("Failed to update info.");
        }
    }
}
