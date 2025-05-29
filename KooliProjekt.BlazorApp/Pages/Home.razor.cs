
using PublicApi.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PublicApi.Api;


namespace KooliProjekt.BlazorApp.Pages
{
    public partial class Home : ComponentBase // Added inheritance from ComponentBase  
    {
        [Inject]
        protected IApiClient apiClient { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        private List<User> users;

        protected override async Task OnInitializedAsync()
        {
            var result = await apiClient.List();

            users = result.Value;
        }

        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed)
            {
                return;
            }

            await apiClient.Delete(id);

            NavManager.Refresh();
        }
    }
}