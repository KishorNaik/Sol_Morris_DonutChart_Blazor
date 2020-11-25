/// <reference path="morrislib/morris.js" />

export function drawDonutChart(divElementRef, itemSourceJson) {
    var itemsource = JSON.parse(itemSourceJson);
    console.log(itemsource);

    Morris.Donut({
        element: divElementRef,
        data: itemsource
    });

    //Morris.Donut({
    //    element: 'donut-example',
    //    data: [
    //        { label: "Download Sales", value: 12 },
    //        { label: "In-Store Sales", value: 30 },
    //        { label: "Mail-Order Sales", value: 20 }
    //    ]
    //});
}