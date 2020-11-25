using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MorrisDonutChart;

namespace BlazorApp.Pages.Demo
{
    public partial class MorrisDonutChartDemo
    {
        #region Private Property

        private bool IsLoad { get; set; }

        private MorrisDonutsChart MorrisDonutsChartReferanceId { get; set; }

        private List<MorrisDonutResultSet> ItemSource { get; set; }

        #endregion Private Property

        #region Private Method

        private Task<List<SalesSectionModel>> GetSalesSectionDataAsync()
        {
            return Task.Run(() =>
            {
                var salesSectionList = new List<SalesSectionModel>();
                salesSectionList.Add(new SalesSectionModel() { SalesSectionName = "Download Sales", SalesNumber = 12 });
                salesSectionList.Add(new SalesSectionModel() { SalesSectionName = "In-Store Sales", SalesNumber = 30 });
                salesSectionList.Add(new SalesSectionModel() { SalesSectionName = "Mail-Order Sales", SalesNumber = 20 });

                return salesSectionList;
            });
        }

        private async Task BindDonutChartResultAsync()
        {
            var salesSectionList = await this.GetSalesSectionDataAsync();

            this.ItemSource = salesSectionList
                                    .Select((salesSectionModel) => new MorrisDonutResultSet()
                                    {
                                        label = salesSectionModel.SalesSectionName,
                                        value = salesSectionModel.SalesNumber
                                    })
                                    .ToList();
        }

        #endregion Private Method

        #region Ui Event Handler

        private async Task OnClickDisplayDonutChart()
        {
            await BindDonutChartResultAsync();

            base.StateHasChanged();

            await MorrisDonutsChartReferanceId.SetDonutChartAsync();
        }

        #endregion Ui Event Handler

        #region Public & Protected Method

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                IsLoad = true;
                base.StateHasChanged();
            }
        }

        #endregion Public & Protected Method
    }
}