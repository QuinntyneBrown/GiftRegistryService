import { ProductAdd, ProductDelete, ProductEdit, productActions } from "./product.actions";
import { Product } from "./product.model";
import { ProductService } from "./product.service";

const template = require("./product-master-detail.component.html");
const styles = require("./product-master-detail.component.scss");

export class ProductMasterDetailComponent extends HTMLElement {
    constructor(
        private _productService: ProductService = ProductService.Instance	
	) {
        super();
        this.onProductAdd = this.onProductAdd.bind(this);
        this.onProductEdit = this.onProductEdit.bind(this);
        this.onProductDelete = this.onProductDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "products"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.products = await this._productService.get();
        this.productListElement.setAttribute("products", JSON.stringify(this.products));
    }

    private _setEventListeners() {
        this.addEventListener(productActions.ADD, this.onProductAdd);
        this.addEventListener(productActions.EDIT, this.onProductEdit);
        this.addEventListener(productActions.DELETE, this.onProductDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(productActions.ADD, this.onProductAdd);
        this.removeEventListener(productActions.EDIT, this.onProductEdit);
        this.removeEventListener(productActions.DELETE, this.onProductDelete);
    }

    public async onProductAdd(e) {

        await this._productService.add(e.detail.product);
        this.products = await this._productService.get();
        
        this.productListElement.setAttribute("products", JSON.stringify(this.products));
        this.productEditElement.setAttribute("product", JSON.stringify(new Product()));
    }

    public onProductEdit(e) {
        this.productEditElement.setAttribute("product", JSON.stringify(e.detail.product));
    }

    public async onProductDelete(e) {

        await this._productService.remove(e.detail.product.id);
        this.products = await this._productService.get();
        
        this.productListElement.setAttribute("products", JSON.stringify(this.products));
        this.productEditElement.setAttribute("product", JSON.stringify(new Product()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "products":
                this.products = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Product> { return this.products; }

    private products: Array<Product> = [];
    public product: Product = <Product>{};
    public get productEditElement(): HTMLElement { return this.querySelector("ce-product-edit-embed") as HTMLElement; }
    public get productListElement(): HTMLElement { return this.querySelector("ce-product-list-embed") as HTMLElement; }
}

customElements.define(`ce-product-master-detail`,ProductMasterDetailComponent);
