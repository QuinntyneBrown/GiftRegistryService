import { Modal } from "../shared";

const template = require("./features-overlay.component.html");
const styles = require("./features-overlay.component.scss");

export class FeaturesOverlayComponent extends HTMLElement {
    constructor(private _modal: Modal = Modal.Instance) {
        super();
    }

    static get observedAttributes () {
        return [];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();        
    }

    private _bind() {

    }

    private _setEventListeners() {
        this.addEventListener("click", this.onClick.bind(this));
    }

    disconnectedCallback() {
        this.removeEventListener("click", this.onClick.bind(this));
    }

    onClick() {
        this._modal.closeAsync();
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            default:
                break;
        }
    }
}

customElements.define(`ce-features-overlay`,FeaturesOverlayComponent);
