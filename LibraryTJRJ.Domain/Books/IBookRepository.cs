﻿using ErrorOr;
using LibraryTJRJ.Domain.Common.Models;
using System.Linq.Expressions;

namespace LibraryTJRJ.Domain.Books;

public interface IBookRepository
{
    Task AddAsync(Book book);
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync();
    Task<List<Book>> GetAllByAuthorAsync(Guid authorId);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateBookAsync(Book book);
    Task RemoveBookAsync(Book book);
    Task<PagedResponseOffset<Book>> GetWithOffsetPagination(int pageNumber, int pageSize, string? searchTerm, Expression<Func<Book, object>> sortExpression, string? sortOrder);

}
