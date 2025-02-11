﻿using Microsoft.JSInterop;
using System.Text.Json;

namespace MusicClub.v3.Cms.Services
{
    internal class LocalStorageService(IJSRuntime jsRuntime) 
    {
        public async Task<T?> GetItem<T>(string key)
        {
            var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (json is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetItem<T>(string key, T value)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveItem(string key)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
