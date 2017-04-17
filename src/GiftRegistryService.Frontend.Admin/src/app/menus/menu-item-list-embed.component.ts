import { MenuItem } from "./menu-item.model";

const template = require("./menu-item-list-embed.component.html");
const styles = require("./menu-item-list-embed.component.scss");

export class MenuItemListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "menu-items"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.menuItems.length; i++) {
            let el = this._document.createElement(`ce-menu-item-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.menuItems[i]));
            this.appendChild(el);
        }    
    }

    menuItems:Array<MenuItem> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "menu-items":
                this.menuItems = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-menu-item-list-embed", MenuItemListEmbedComponent);
