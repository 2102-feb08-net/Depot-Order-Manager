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

function addProductRow(index, id, name, category, unitPrice, quantity) {
    let row = table.insertRow(index);
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

function updateTotalRow(tableToUpdate, cart) {
    const lastRow = tableToUpdate.rows[tableToUpdate.rows.length - 1];
    const totalPriceCell = lastRow.cells[lastRow.cells.length - 1];

    let total = 0;
    for (let product of cart)
        total += product.quantity * product.unitPrice;
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
    let quantity = Number(quantityInput.value);

    if (productIsValid(productObject, quantity)) {
        addOrReplaceProductInCart(productObject, quantity);
    }
    else {
        alert("Product and/or quantity is invalid. Failed to add to order.");
    }
};

function productIsValid(product, quantity) {
    return product.id > 0 && quantity >= 0;
}

function addOrReplaceProductInCart(product, quantity) {
    let insertIndex = table.rows.length - 1;
    let replacedProductInCart = false;
    for (let i = productCart.length - 1; i >= 0; i--) {
        if (productCart[i].id == product.id) {
            if (quantity === 0) {
                productCart.splice(i, 1);
            }
            else {
                productCart[i].quantity = quantity;
                replacedProductInCart = true;
            }
            table.deleteRow(i);
            insertIndex = i;
            break;
        }
    }

    if (quantity !== 0) {
        addProductRow(insertIndex, product.id, product.name, product.category, product.unitPrice, quantity);
        if (!replacedProductInCart)
            productCart.push(new CartProduct(product.id, quantity, product.unitPrice));
    }
    updateTotalRow(table, productCart);
}

class CartProduct {
    constructor(id, quantity, unitPrice) {
        this.id = id;
        this.quantity = quantity;
        this.unitPrice = unitPrice;
    }
}

class OrderLine {
    constructor(productId, quantity) {
        this.productId = productId;
        this.quantity = quantity;
    }
}

function convertCartIntoOrderLines(products) {
    let orderLines = [];

    for (let product of productCart) {
        orderLines.push(new OrderLine(product.id, product.quantity));
    }
    return orderLines;
}

async function submitOrder(event) {
    event.preventDefault();

    let orderLines = convertCartIntoOrderLines(productCart);

    if (orderLines.length === 0) {
        alert("You need at least one product in the cart to submit an order.");
        return;
    }

    const order = {
        customerId: document.getElementById("customerSelect").value,
        storeLocationId: document.getElementById("locationSelect").value,
        orderLines: orderLines,
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
        alert("Failed to submit order!");
        return
    }

    alert("Order submitted sucessfully!");

    window.location.reload(false);
}

orderForm.addEventListener('submit', submitOrder);