﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AprajitaRetails.Areas.ToDo.Models;
using Microsoft.AspNetCore.Identity;

namespace AprajitaRetails.Areas.ToDo.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetIncompletePublicItemsAsync();
        Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync(IdentityUser currentUser);
        Task<IEnumerable<TodoItem>> GetCompleteItemsAsync(IdentityUser currentUser);
        Task<IEnumerable<TodoItem>> GetItemsByTagAsync(IdentityUser currentUser, string tag);
        Task<bool> AddItemAsync(TodoItem todo, IdentityUser currentUser);
        Task<bool> UpdateDoneAsync(Guid id, IdentityUser currentUser);
        bool Exists(Guid id);
        Task<bool> UpdateTodoAsync(TodoItem todo, IdentityUser currentUser);
        Task<TodoItem> GetItemAsync(Guid id);
        Task<bool> DeleteTodoAsync(Guid id, IdentityUser currentUser);
        Task<IEnumerable<TodoItem>> GetRecentlyAddedItemsAsync(IdentityUser currentUser);
        Task<IEnumerable<TodoItem>> GetDueTo2DaysItems(IdentityUser user);
        Task<IEnumerable<TodoItem>> GetMonthlyItems(IdentityUser user, int Month);
        Task<bool> SaveFileAsync(Guid todoId, IdentityUser currentUser, string path, long size);
    }
}
