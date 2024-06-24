let allProductList = [];
let allCustomerList = [];
let orderItemList = [];;
$(() => {
    loadOrder();
    $("#Order").find(".btnModal").on("click", () => {
        loadCustomer();
        loadProduct();
    })
    $("#Order").find(".btnSaveOrder").on("click", () => {
        let customerId = parseInt($("#Order").find("#exampleModal").find("#customername").val());
        let orderNo = $("#Order").find("#exampleModal").find("#orderno").val();
        let orderDate = $("#Order").find("#exampleModal").find("#orderdate").val();
        let orderTotal = parseFloat($("#Order").find("#exampleModal").find("#ordertotal").val());
        var obj = {
            "Id": 0,
            "OrderNo": orderNo,
            "OrderDate": orderDate,
            "CustomerId": customerId,
            "TotalAmount": orderTotal,
            "CustomerName": ""
        }

        fetch('https://localhost:7135/api/API/CreateOrderHeader', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(obj)
        }).then(res => res.json())
            .then(res => {
                console.log(res.data.id)
                for (let item of orderItemList) {
                    item.OrderHeaderId = res.data.id;
                }
                var obj = {
                    "list": orderItemList
                }
                fetch('https://localhost:7135/api/API/CreateOrderItem', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(orderItemList)
                }).then(res => res.json())
                    .then(res => {
                        console.log(res)
                        alert(res.message);
                        $("#Order").find("#exampleModal").modal("hide");
                        loadOrder();
                    });
            });
    })
    $("#Order").find("#exampleModal").find(".btnAddMore").on("click", () => {
        let template = $("#Order").find("#exampleModal").find(".table-order-item").find(".divRow").clone().removeClass("d-none").removeClass("divRow");
        template.find(".btnRemove").on("click", () => {
            template.find(".btnRemove").parent().parent().remove();
            let productId = template.find(".productname").val();
            orderItemList = orderItemList.filter(x => x.ProductId != productId);
            let totalOrder = Object.values(orderItemList).reduce((t, n) => t + n.LineTotal, 0)
            $("#Order").find("#exampleModal").find("#ordertotal").val(totalOrder)
        });
        template.find(".productname").on("change", (e) => {
            let productPrice = 0;
            if (template.find(".productname").val() != "")
                productPrice = allProductList.find(x => x.id == template.find(".productname").val()).productPrice;
            template.find(".orderprice").val(productPrice)
            calculatePrice(template);
        });
        template.find(".orderqty").on("change", (e) => {
            calculatePrice(template);
        });
        template.find(".gst").on("change", (e) => {
            calculatePrice(template);
        });
        $("#Order").find("#exampleModal").find(".table-order-item").find("tbody").append(template);

    })

})

let calculatePrice = (template) => {
    let productId = parseInt(template.find(".productname").val());
    let price = parseInt(template.find(".orderprice").val());
    let qty = parseInt(template.find(".orderqty").val());
    let total = price * qty;
    let gst = parseInt(template.find(".gst").val());
    let gstamount = (total * template.find(".gst").val()) / 100
    let lineTotal = total + gstamount;
    /*template.find(".total").val(total)*/
    template.find(".linetotal").val(lineTotal);
    orderItemList = orderItemList.filter(x => x.ProductId != productId);
    orderItemList.push({
        "Id": 0,
        "OrderHeaderId": 0,
        "ProductId": productId,
        "Rate": price,
        "Qty": qty,
        "GST": gst,
        "Total": total,
        "LineTotal": lineTotal
    });

    let totalOrder = Object.values(orderItemList).reduce((t, n) => t + n.LineTotal, 0)
    $("#Order").find("#exampleModal").find("#ordertotal").val(totalOrder)
    console.log(orderItemList)
}

let loadOrder = async () => {
    let response = await fetch("https://localhost:7135/api/API/GetAllOrder");
    let orderlist = await response.json();

    $("#Order").find(".table-order-details").find("tbody").html("");
    orderlist.data.forEach((item, index) => {

        let template = $("#Order").find(".table-order-details").find(".divRow").clone().removeClass("d-none").removeClass("divRow");
        template.find(".tdCustomerName").html(item.customerName);
        template.find(".tdOrderNo").html(item.orderNo);
        template.find(".tdOrderDate").html(item.orderDate);
        template.find(".tdOrderPrice").html(item.totalAmount);

        $("#Order").find(".table-order-details").find("tbody").append(template);
    })
}

let loadCustomer = async () => {
    let response = await fetch("https://localhost:7135/api/API/GetAllCustomer");
    let customerlist = await response.json();

    $("#Order").find("#exampleModal").find("#customername").html("");
    customerlist.data.forEach((item, index) => {
        console.log(item)
        if (index == 0)
            $("#Order").find("#exampleModal").find("#customername").append('<option value="">-Select Customer-</option>');
        $("#Order").find("#exampleModal").find("#customername").append('<option value="' + item.id + '">' + item.customerName + '</option>');
    })
}

let loadProduct = async () => {
    let response = await fetch("https://localhost:7135/api/API/GetAllProduct");
    let productlist = await response.json();
    allProductList = productlist.data;
    $("#Order").find("#exampleModal").find(".table-order-item").find(".productname").html("");
    productlist.data.forEach((item, index) => {
        if (index == 0)
            $("#Order").find("#exampleModal").find(".table-order-item").find(".productname").append('<option value="">-Select Product-</option>');
        $("#Order").find("#exampleModal").find(".table-order-item").find(".productname").append('<option value="' + item.id + '" data-price="' + item.productPrice + '">' + item.productName + '</option>');
    })
}