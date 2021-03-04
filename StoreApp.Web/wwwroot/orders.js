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
        addOrderRow(order.id, order.orderTime, order.customer, order.location.name, order.totalPrice);
    }
}

async function showOrderDetails(orderId) {
    let response = await fetch(`/api/orders/${orderId}`);

    let details = await response.json();

    clearOrderLines(productTable);

    for (const line of details.lines)
        addOrderLineRow(productTable, line.product, line.quantity, line.lineTotalPrice);

    updateTotalRow(productTable, details.orderTotalPrice);

    buildAddress(details.head.location.name, details.head.location.addressLines);

    document.getElementById("customerName").innerHTML = details.head.customer.firstName + " " + details.head.customer.lastName;
    document.getElementById("orderTime").innerHTML = details.head.orderTime;

    document.getElementById("orderIdTitle").innerHTML = details.head.id;

    setDetailsVisibility(true);
}

function buildAddress(name, addressLines) {
    const NUMBER_OF_ADDRESS_LINES = 4;
    document.getElementById("locationName").innerHTML = name;
    for (let i = 0; i < NUMBER_OF_ADDRESS_LINES; i++) {
        document.getElementById(`addressLine${i}`).innerHTML = i < addressLines.length ? addressLines[i] : "";
    }
}

function setDetailsVisibility(visibility) {
    document.getElementById("orderTableContainer").hidden = visibility;
    document.getElementById("orderDetailsContainer").hidden = !visibility;
}

function clearOrderLines(table) {
    let numberOfRowsToDelete = table.rows.length - 2;

    for (let i = 0; i < numberOfRowsToDelete; i++)
        table.deleteRow(0);
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
    setDetailsVisibility(false);
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
document.getElementById("goBackButton").onclick = goBackFromDetails;