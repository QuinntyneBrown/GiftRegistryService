import { MenuItem } from "./menu-item.model";
import { EditorComponent } from "../shared";
import {  MenuItemDelete, MenuItemEdit, MenuItemAdd } from "./menu-item.actions";

const template = require("./menu-item-edit-embed.component.html");
const styles = require("./menu-item-edit-embed.component.scss");

export class MenuItemEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "menu-item",
            "menu-item-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.menuItem ? "Edit Menu Item": "Create Menu Item";

        if (this.menuItem) {                
            this._nameInputElement.value = this.menuItem.name;  
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
        const menuItem = {
            id: this.menuItem != null ? this.menuItem.id : null,
            name: this._nameInputElement.value
        } as MenuItem;
        
        this.dispatchEvent(new MenuItemAdd(menuItem));            
    }

    public onCreate() {        
        this.dispatchEvent(new MenuItemEdit(new MenuItem()));            
    }

    public onDelete() {        
        const menuItem = {
            id: this.menuItem != null ? this.menuItem.id : null,
            name: this._nameInputElement.value
        } as MenuItem;

        this.dispatchEvent(new MenuItemDelete(menuItem));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "menu-item-id":
                this.menuItemId = newValue;
                break;
            case "menuItem":
                this.menuItem = JSON.parse(newValue);
                if (this.parentNode) {
                    this.menuItemId = this.menuItem.id;
                    this._nameInputElement.value = this.menuItem.name != undefined ? this.menuItem.name : "";
                    this._titleElement.textContent = this.menuItemId ? "Edit MenuItem" : "Create MenuItem";
                }
                break;
        }           
    }

    public menuItemId: any;
    
	public menuItem: MenuItem;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".menu-item-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".menu-item-name") as HTMLInputElement;}
}

customElements.define(`ce-menu-item-edit-embed`,MenuItemEditEmbedComponent);
