import { Menu } from "./menu.model";
import { EditorComponent } from "../shared";
import {  MenuDelete, MenuEdit, MenuAdd } from "./menu.actions";

const template = require("./menu-edit-embed.component.html");
const styles = require("./menu-edit-embed.component.scss");

export class MenuEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "menu",
            "menu-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.menu ? "Edit Menu": "Create Menu";

        if (this.menu) {                
            this._nameInputElement.value = this.menu.name;  
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
        const menu = {
            id: this.menu != null ? this.menu.id : null,
            name: this._nameInputElement.value
        } as Menu;
        
        this.dispatchEvent(new MenuAdd(menu));            
    }

    public onCreate() {        
        this.dispatchEvent(new MenuEdit(new Menu()));            
    }

    public onDelete() {        
        const menu = {
            id: this.menu != null ? this.menu.id : null,
            name: this._nameInputElement.value
        } as Menu;

        this.dispatchEvent(new MenuDelete(menu));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "menu-id":
                this.menuId = newValue;
                break;
            case "menu":
                this.menu = JSON.parse(newValue);
                if (this.parentNode) {
                    this.menuId = this.menu.id;
                    this._nameInputElement.value = this.menu.name != undefined ? this.menu.name : "";
                    this._titleElement.textContent = this.menuId ? "Edit Menu" : "Create Menu";
                }
                break;
        }           
    }

    public menuId: any;
    
	public menu: Menu;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".menu-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".menu-name") as HTMLInputElement;}
}

customElements.define(`ce-menu-edit-embed`,MenuEditEmbedComponent);
