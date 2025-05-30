using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PublicApi.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.BlazorApp.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected IApiClient apiClient { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected List<User> users;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var result = await apiClient.List();
                users = result.Value ?? new List<User>();
                Console.WriteLine($"Loaded {users.Count} users");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading users: {ex.Message}");
                users = new List<User>();
            }
        }

        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed)
            {
                return;
            }

            try
            {
                await apiClient.Delete(id);

                // Reload the users after deletion
                var result = await apiClient.List();
                users = result.Value ?? new List<User>();

                StateHasChanged();  // Refresh UI
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting user: {ex.Message}");
            }
        }
    }
}
