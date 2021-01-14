﻿using ShoppingListApp.Database.Models;
using System.Threading.Tasks;

namespace ShoppingListApp.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingList> GetAsync(int shoppingListId, string userId);
    }
}
