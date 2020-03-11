﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.ToDo.Interfaces;
using AprajitaRetails.Data;
using NodaTime;
using AprajitaRetails.Areas.ToDo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AprajitaRetails.Areas.ToDo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly AprajitaRetailsContext _context;
        private readonly IClock _clock;

        public TodoItemService(AprajitaRetailsContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        public async Task<IEnumerable<TodoItem>> GetItemsByTagAsync(IdentityUser currentUser, string tag)
        {
            return await _context.Todos
                .Where (t => t.Tags.Contains (tag))
                .ToArrayAsync ();
        }

        public async Task<bool> AddItemAsync(TodoItem todo, IdentityUser user)
        {
            todo.Id = Guid.NewGuid ();
            todo.Done = false;
            todo.Added = _clock.GetCurrentInstant ().ToDateTimeUtc();//TODO: Instance is added
            todo.UserId = user.Id;
            todo.File = new Models.FileInfo
            {
                TodoId = todo.Id,
                Path = "",
                Size = 0
            };
            _context.Todos.Add (todo);

            var saved = await _context.SaveChangesAsync ();
            return saved > 0;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync(IdentityUser user)
        {
            return await _context.Todos
                .Where (t => !t.Done && t.UserId == user.Id)
                .ToArrayAsync ();
        }

        public async Task<IEnumerable<TodoItem>> GetCompleteItemsAsync(IdentityUser user)
        {
            return await _context.Todos
                .Where (t => t.Done && t.UserId == user.Id)
                .ToArrayAsync ();
        }

        public bool Exists(Guid id)
        {
            return _context.Todos
                .Any (t => t.Id == id);
        }

        public async Task<bool> UpdateDoneAsync(Guid id, IdentityUser user)
        {
            var todo = await _context.Todos
                .Where (t => t.Id == id && t.UserId == user.Id)
                .SingleOrDefaultAsync ();

            if ( todo == null )
                return false;

            todo.Done = !todo.Done;

            var saved = await _context.SaveChangesAsync ();
            return saved == 1;
        }

        public async Task<bool> UpdateTodoAsync(TodoItem editedTodo, IdentityUser user)
        {
            var todo = await _context.Todos
                .Where (t => t.Id == editedTodo.Id && t.UserId == user.Id)
                .SingleOrDefaultAsync ();

            if ( todo == null )
                return false;

            todo.Title = editedTodo.Title;
            todo.Content = editedTodo.Content;
            todo.Tags = editedTodo.Tags;

            var saved = await _context.SaveChangesAsync ();
            return saved == 1;
        }

        public async Task<TodoItem> GetItemAsync(Guid id)
        {
            return await _context.Todos
                .Include (t => t.File)
                .Where (t => t.Id == id)
                .SingleOrDefaultAsync ();
        }

        public async Task<bool> DeleteTodoAsync(Guid id, IdentityUser currentUser)
        {
            var todo = await _context.Todos
                .Include (t => t.File)
                .Where (t => t.Id == id && t.UserId == currentUser.Id)
                .SingleOrDefaultAsync ();

            _context.Todos.Remove (todo);
            _context.Files.Remove (todo.File);

            var deleted = await _context.SaveChangesAsync ();
            return deleted > 0;
        }

        
        public async Task<IEnumerable<TodoItem>> GetRecentlyAddedItemsAsync(IdentityUser currentUser)
        {
            //return await _context.Todos
            //   .Where (t => t.UserId == currentUser.Id && !t.Done
            //    && DateTime.Compare (DateTime.UtcNow.AddDays (1), t.Added.ToDateTimeUtc ()) <= 0)
            //   .ToArrayAsync ();

           
            return _context.Todos.Where (t => t.UserId == currentUser.Id && !t.Done && DateTime.Compare (DateTime.UtcNow.AddDays (-1), t.Added) <= 0).ToList ();

        }

        public async Task<IEnumerable<TodoItem>> GetDueTo2DaysItems(IdentityUser user)
        {
            return await _context.Todos
                .Where (t => t.UserId == user.Id && !t.Done
                 && DateTime.Compare (DateTime.UtcNow.AddDays (1), t.DueTo/*.ToDateTimeUtc ()*/) >= 0)//TODO: Instance is added
                .ToArrayAsync ();
        }

        public async Task<IEnumerable<TodoItem>> GetMonthlyItems(IdentityUser user, int month)
        {
            return await _context.Todos
                .Where (t => t.UserId == user.Id && !t.Done)
                .Where (t => t.DueTo/*.ToDateTimeUtc ()*/.Month == month)//TODO: Instance is added
                .ToArrayAsync ();
        }

        public async Task<bool> SaveFileAsync(Guid todoId, IdentityUser currentUser, string path, long size)
        {
            var todo = await _context.Todos.Include (t => t.File)
                .Where (t => t.Id == todoId && t.UserId == currentUser.Id)
                .SingleOrDefaultAsync ();

            if ( todo == null )
                return false;

            todo.File.Path = path;
            todo.File.Size = size;
            todo.File.TodoId = todo.Id;

            var changes = await _context.SaveChangesAsync ();
            return changes > 0;
        }
    }
}
