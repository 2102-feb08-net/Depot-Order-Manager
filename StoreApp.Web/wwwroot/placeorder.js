'use strict';

const table = document.getElementById("productTableBody");
const orderForm = document.getElementById("orderForm");

async function loadCustomers() {
    const response = await fetch("/api/customers/getall");

    if (!response.ok) {
        throw new Error(`Unable to download customers from server: (${response.status}) ${response.statusText}`);
    }

    const customers = await response.json();

    for (const customer of customers) {
        addToSelect("customerSelect", customer.firstName + " " + customer.lastName + " (ID: " + customer.id + ")", customer.id);
    }
}

async function loadLocations() {
    const response = await fetch("/api/locations/getall");

    if (!response.ok) {
        throw new Error(`Unable to download store locations from server: (${response.status}) ${response.statusText}`);
    }

    const locations = await response.json();

    for (const location of locations) {
        addToSelect("locationSelect", location.name + " (ID: " + location.id + ")", location.id);
    }
}

async function loadProducts() {
    const response = await fetch("/api/products/getall");

    if (!response.ok) {
        throw new Error(`Unable to download store locations from server: (${response.status}) ${response.statusText}`);
    }

    const products = await response.json();

    for (const product of products) {
        addToSelect("productSelect", product.name + " (ID: " + product.id + ")", JSON.stringify(product));
    }
}

function addToSelect(selectElementId, displayName, value) {
    document.getElementById(selectElementId).options.add(new Option(displayName, value));
}

function addProductRow(id, name, category, unitPrice, quantity) {
    let row = table.insertRow(0);
    let idCell = row.insertCell();
    let nameCell = row.insertCell();
    let categoryCell = row.insertCell();
    let priceCell = row.insertCell();
    let quantityCell = row.insertCell();
    let totalPriceCell = row.insertCell();

    let price = truncateToDecimals(unitPrice);

    idCell.innerHTML = id;
    nameCell.innerHTML = name;
    categoryCell.innerHTML = category;
    priceCell.innerHTML = "$" + price;
    quantityCell.innerHTML = quantity;
    totalPriceCell.innerHTML = "$" + (price * quantity);
}

function updateTotalRow(tableToUpdate) {
    const lastRow = tableToUpdate.rows[tableToUpdate.rows.length - 1];
    const totalPriceCell = lastRow.cells[lastRow.cells.length - 1];

    let total = 0;
    for (let product of productPriceViews)
        total += product.totalPrice;
    totalPriceCell.innerHTML = "$" + truncateToDecimals(total);
}

loadCustomers();
loadLocations();
loadProducts();

let productCart = [];
let productPriceViews = [];

let addProductButton = document.getElementById("addProductButton");
let quantityInput = document.getElementById("quantityInput");
addProductButton.onclick = () => {
    let productSelect = document.getElementById("productSelect");
    let productObject = JSON.parse(productSelect.value);
    let quantity = quantityInput.value;

    if (productIsValid(productObject, quantity)) {
        addProductRow(productObject.id, productObject.name, productObject.category, productObject.unitPrice, quantity);
        productPriceViews.push(new ProductPriceView(productObject.id, quantity * truncateToDecimals(productObject.unitPrice)));
        productCart.push(new Product(productObject.id, Number(quantity)));
        updateTotalRow(table);
    }
    else {
        alert("Product and/or quantity is invalid. Failed to add to order.");
    }
};

function productIsValid(product, quantity) {
    return product.id > 0 && quantity > 0;
}

class ProductPriceView {
    constructor(id, totalPrice) {
        this.id = id;
        this.totalPrice = totalPrice;
    }
}

class Product {
    constructor(productId, quantity) {
        this.productId = productId;
        this.quantity = quantity;
    }
}

async function submitOrder(event) {
    event.preventDefault();

    const order = {
        customerId: document.getElementById("customerSelect").value,
        storeLocationId: document.getElementById("locationSelect").value,
        orderLines: productCart,
    }

    const options = {
        method: "POST",
        body: JSON.stringify(order),
        redirect: 'follow',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    let response = await fetch("/api/orders/send-order", options);

    if (!response.ok) {
        alert("Failed to submit order: " + response.message);
    }

    alert(response.status);
}

orderForm.addEventListener('submit', submitOrder);