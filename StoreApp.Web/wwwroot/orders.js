'use strict';

const orderTable = document.getElementById("orderTableBody");
const productTable = document.getElementById("productTableBody");

const emptyTableDesc = document.getElementById("emptyTableDesc");
const loadingTableDesc = document.getElementById("loadingTableDesc");
const errorTableDesc = document.getElementById("failedToLoadTableDesc");

async function loadOrders(fetchUrl) {
    emptyTableDesc.hidden = true;
    errorTableDesc.hidden = true;
    loadingTableDesc.hidden = false;

    const response = await fetch(fetchUrl);

    loadingTableDesc.hidden = true;

    if (!response.ok) {
        errorTableDesc.hidden = false;
        throw new Error(`Unable to download orders from server: (${response.status}) ${response.statusText}`);
    }

    const orders = await response.json();

    for (const order of orders) {
        addOrderRow(order.id, order.orderTime, order.customer, order.location.name, order.totalPrice);
    }

    if (orders.length === 0)
        emptyTableDesc.hidden = false;
}

async function showOrderDetails(orderId) {
    let response = await fetch(`/api/orders/${orderId}`);

    let details = await response.json();

    rebuildProductTable(details);

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

function rebuildProductTable(order) {
    clearOrderLines(productTable);

    for (const line of order.lines)
        addOrderLineRow(productTable, line.product, line.quantity, line.lineTotalPrice);

    updateTotalRow(productTable, order.head.totalPrice);
}

function clearOrderLines(table) {
    let numberOfRowsToDelete = table.rows.length - 1;

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

function addParameterToSearchUrl(parameter, urlParams, previousParams) {
    if (previousParams)
        previousParams += '&';
    if (urlParams.has(parameter))
        return previousParams + `${parameter}=${urlParams.get(parameter)}`;
    else
        return previousParams;
}

function processSearchParameters() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    if (urlParams.has(`customer`) || urlParams.has(`location`)) {
        let url = "/api/orders/search?";
        let params = "";
        params = addParameterToSearchUrl('customer', urlParams, params);
        params = addParameterToSearchUrl('location', urlParams, params);

        loadOrders(url + params);
        showSearchResultTitle(true);
    }
    else {
        loadOrders("/api/orders/getall");
    }
}

document.getElementById("goBackButton").onclick = goBackFromDetails;

processSearchParameters();