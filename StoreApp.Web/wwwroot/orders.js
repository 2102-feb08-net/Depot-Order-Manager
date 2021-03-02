'use strict';

const table = document.getElementById("orderTableBody");

async function loadOrders() {
    const response = await fetch("/api/orders/getall");

    if (!response.ok) {
        throw new Error(`Unable to download orders from server: (${response.status}) ${response.statusText}`);
    }

    const orders = await response.json();

    for (const order of orders) {
        addOrderRow(order.id, order.orderTime, order.customer.id, order.storeLocation.name, order.totalPrice);
    }
}

function addOrderRow(id, dateTime, customer, storeLocation, totalPrice) {
    let row = table.insertRow();
    let idCell = row.insertCell();
    let orderDateTimeCell = row.insertCell();
    let customerCell = row.insertCell();
    let storeLocationCell = row.insertCell();
    let totalPriceCell = row.insertCell();

    idCell.innerHTML = id;
    orderDateTimeCell.innerHTML = dateTime;
    customerCell.innerHTML = customer;
    storeLocationCell.innerHTML = storeLocation;
    totalPriceCell.innerHTML = "$" + totalPrice;
}

loadOrders();

class Product {
    constructor(productId, quantity) {
        this.id = productId;
        this.quantity = quantity;
    }
}