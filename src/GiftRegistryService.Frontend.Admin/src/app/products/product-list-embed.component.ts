import { Product } from "./product.model";

const template = require("./product-list-embed.component.html");
const styles = require("./product-list-embed.component.scss");

export class ProductListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "products"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.products.length; i++) {
            let el = this._document.createElement(`ce-product-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.products[i]));
            this.appendChild(el);
        }    
    }

    products:Array<Product> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "products":
                this.products = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-product-list-embed", ProductListEmbedComponent);
