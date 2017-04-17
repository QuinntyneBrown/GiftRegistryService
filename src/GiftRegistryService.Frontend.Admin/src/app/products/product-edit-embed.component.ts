import { Product } from "./product.model";
import { EditorComponent } from "../shared";
import {  ProductDelete, ProductEdit, ProductAdd } from "./product.actions";

const template = require("./product-edit-embed.component.html");
const styles = require("./product-edit-embed.component.scss");

export class ProductEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "product",
            "product-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.product ? "Edit Product": "Create Product";

        if (this.product) {                
            this._nameInputElement.value = this.product.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createButtonElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createButtonElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const product = {
            id: this.product != null ? this.product.id : null,
            name: this._nameInputElement.value
        } as Product;
        
        this.dispatchEvent(new ProductAdd(product));            
    }

    public onCreate() {        
        this.dispatchEvent(new ProductEdit(new Product()));            
    }

    public onDelete() {        
        const product = {
            id: this.product != null ? this.product.id : null,
            name: this._nameInputElement.value
        } as Product;

        this.dispatchEvent(new ProductDelete(product));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "product-id":
                this.productId = newValue;
                break;
            case "product":
                this.product = JSON.parse(newValue);
                if (this.parentNode) {
                    this.productId = this.product.id;
                    this._nameInputElement.value = this.product.name != undefined ? this.product.name : "";
                    this._titleElement.textContent = this.productId ? "Edit Product" : "Create Product";
                }
                break;
        }           
    }

    public productId: any;
    
	public product: Product;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".product-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".product-name") as HTMLInputElement;}
}

customElements.define(`ce-product-edit-embed`,ProductEditEmbedComponent);
