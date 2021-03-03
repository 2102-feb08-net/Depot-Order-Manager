'use strict';

const orderTable = document.getElementById("orderTableBody");
const productTable = document.getElementById("productTableBody");

async function loadOrders() {
    const response = await fetch("/api/orders/getall");

    document.getElementById("loadingTable").hidden = true;

    if (!response.ok) {
        document.getElementById("failedToLoadTable").hidden = false;
        throw new Error(`Unable to download orders from server: (${response.status}) ${response.statusText}`);
    }

    const orders = await response.json();

    for (const order of orders) {
        addOrderRow(order.id, order.orderTime, order.customer, order.storeLocation.name, order.totalPrice);
    }
}

async function showOrderDetails(orderId) {
    let response = await fetch(`/api/orders/${orderId}`);

    let details = await response.json();

    for (const line of details.lines)
        addOrderLineRow(productTable, line.product, line.quantity, line.lineTotalPrice);

    updateTotalRow(productTable, details.orderTotalPrice);

    document.getElementById("orderIdTitle").innerHTML = details.Head.id;

    document.getElementById("orderTableContainer").hidden = true;
    document.getElementById("orderDetailsContainer").hidden = false;
}

function addOrderLineRow(table, product, quantity, linePrice) {
    let row = table.insertRow(0);
    let idCell = row.insertCell();
    let nameCell = row.insertCell();
    let categoryCell = row.insertCell();
    let priceCell = row.insertCell();
    let quantityCell = row.insertCell();
    let totalPriceCell = row.insertCell();

    let unitPrice = truncateToDecimals(product.unitPrice);
    linePrice = truncateToDecimals(linePrice);

    idCell.innerHTML = product.id;
    nameCell.innerHTML = product.name;
    categoryCell.innerHTML = product.category;
    priceCell.innerHTML = "$" + unitPrice;
    quantityCell.innerHTML = quantity;
    totalPriceCell.innerHTML = "$" + linePrice;
}

function updateTotalRow(table, totalPrice) {
    const lastRow = table.rows[table.rows.length - 1];
    const totalPriceCell = lastRow.cells[lastRow.cells.length - 1];

    let total = truncateToDecimals(totalPrice);
    totalPriceCell.innerHTML = "$" + truncateToDecimals(total);
}

function goBackFromDetails() {
}

function addOrderRow(id, dateTime, customer, storeLocation, totalPrice) {
    let row = orderTable.insertRow();
    let idCell = row.insertCell();
    let orderDateTimeCell = row.insertCell();
    let customerCell = row.insertCell();
    let storeLocationCell = row.insertCell();
    let totalPriceCell = row.insertCell();

    idCell.innerHTML = id;
    orderDateTimeCell.innerHTML = dateTime;
    customerCell.innerHTML = customer.firstName + ' ' + customer.lastName;
    storeLocationCell.innerHTML = storeLocation;
    totalPriceCell.innerHTML = "$" + totalPrice;

    row.addEventListener("click", () => showOrderDetails(id));
}

loadOrders();