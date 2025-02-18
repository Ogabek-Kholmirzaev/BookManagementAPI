﻿using System.Collections.Immutable;
using BookManagement.Core.DTOs;
using BookManagement.Core.Exceptions;
using BookManagement.Core.Interfaces;
using BookManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Core.Services;

public class BookService(IBookRepository repository) : IBookService
{
    public async Task<int> AddAsync(CreateBookDto dto)
    {
        if (await repository.AnyAsync(book => !book.IsDeleted && book.Title == dto.Title))
        {
            throw new TitleAlreadyExistsException(dto.Title);
        }

        var book = CreateBookDto.ToEntity(dto);
        await repository.AddAsync(book);
        return book.Id;
    }

    public async Task AddRangeAsync(IEnumerable<CreateBookDto> dtos)
    {
        var titles = await repository.GetAll()
            .Where(book => !book.IsDeleted)
            .Select(book => book.Title)
            .ToListAsync();

        var exsistingTitles = new HashSet<string>();

        foreach (var dto in dtos)
        {
            if (titles.Contains(dto.Title))
            {
                exsistingTitles.Add(dto.Title);
            }
        }

        if (exsistingTitles.Count != 0)
        {
            throw new TitleAlreadyExistsException(string.Join(',', exsistingTitles));
        }

        var books = dtos.Select(CreateBookDto.ToEntity);
        await repository.AddRangeAsync(books);
    }
}
