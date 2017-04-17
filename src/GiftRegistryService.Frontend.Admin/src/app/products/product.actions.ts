import { Product } from "./product.model";

export const productActions = {
    ADD: "[Product] Add",
    EDIT: "[Product] Edit",
    DELETE: "[Product] Delete",
    PRODUCTS_CHANGED: "[Product] Products Changed"
};

export class ProductEvent extends CustomEvent {
    constructor(eventName:string, product: Product) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { product }
        });
    }
}

export class ProductAdd extends ProductEvent {
    constructor(product: Product) {
        super(productActions.ADD, product);        
    }
}

export class ProductEdit extends ProductEvent {
    constructor(product: Product) {
        super(productActions.EDIT, product);
    }
}

export class ProductDelete extends ProductEvent {
    constructor(product: Product) {
        super(productActions.DELETE, product);
    }
}

export class ProductsChanged extends CustomEvent {
    constructor(products: Array<Product>) {
        super(productActions.PRODUCTS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { products }
        });
    }
}
