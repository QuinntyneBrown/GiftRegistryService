import { Menu } from "./menu.model";

const template = require("./menu-list-embed.component.html");
const styles = require("./menu-list-embed.component.scss");

export class MenuListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "menus"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.menus.length; i++) {
            let el = this._document.createElement(`ce-menu-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.menus[i]));
            this.appendChild(el);
        }    
    }

    menus:Array<Menu> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "menus":
                this.menus = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-menu-list-embed", MenuListEmbedComponent);
