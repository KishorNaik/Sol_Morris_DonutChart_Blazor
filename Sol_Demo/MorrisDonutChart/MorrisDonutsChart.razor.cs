using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorrisDonutChart
{
    public partial class MorrisDonutsChart
    {
        #region Declaration

        private Task<IJSObjectReference> _module = null;

        #endregion Declaration

        #region Public Property

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public List<MorrisDonutResultSet> ItemSource { get; set; }

        #endregion Public Property

        #region Private Property

        private ElementReference DivDonutElement { get; set; }

        #endregion Private Property

        #region Private Method

        private void LoadJsModules()
        {
            _module = JSRuntime
                            .InvokeAsync<IJSObjectReference>("import", "./_content/MorrisDonutChart/DonutChart.js")
                            .AsTask();
        }

        private async Task OnLoadDonutChartJs()
        {
            var itemSourceJson = JsonConvert.SerializeObject(this.ItemSource);
            await (await _module).InvokeVoidAsync(identifier: "drawDonutChart", DivDonutElement, itemSourceJson);
        }

        #endregion Private Method

        #region Public & Protected Method

        public async Task SetDonutChartAsync()
        {
            LoadJsModules();

            await OnLoadDonutChartJs();

            base.StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                await (await _module).DisposeAsync();
            }
        }

        #endregion Public & Protected Method
    }
}