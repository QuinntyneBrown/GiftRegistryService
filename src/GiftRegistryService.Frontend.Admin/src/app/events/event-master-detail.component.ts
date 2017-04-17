import { EventAdd, EventDelete, EventEdit, eventActions } from "./event.actions";
import { Event } from "./event.model";
import { EventService } from "./event.service";
import { tabsEvents } from "../shared";
import { Router } from "../router";

const template = require("./event-master-detail.component.html");
const styles = require("./event-master-detail.component.scss");

export class EventMasterDetailComponent extends HTMLElement {
    constructor(
        private _eventService: EventService = EventService.Instance,
        private _router: Router = Router.Instance	
	) {
        super();
        this.onEventAdd = this.onEventAdd.bind(this);
        this.onEventEdit = this.onEventEdit.bind(this);
        this.onEventDelete = this.onEventDelete.bind(this);
        this.onSelectedIndexChanged = this.onSelectedIndexChanged.bind(this);
    }

    static get observedAttributes () {
        return [
            "events",
            "tab-index",
            "page-index"
        ];
    }

    onSelectedIndexChanged(e) {
        this._router.navigate(["tab", e.detail.selectedIndex]);
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.events = await this._eventService.get();        
        this.eventEditElement.setAttribute("tab-index", this.customTabIndex);
        this.eventListElement.setAttribute("events", JSON.stringify(this.events));
    }

    private _setEventListeners() {
        this.addEventListener(eventActions.ADD, this.onEventAdd);
        this.addEventListener(eventActions.EDIT, this.onEventEdit);
        this.addEventListener(eventActions.DELETE, this.onEventDelete);
        this.addEventListener(tabsEvents.SELECTED_INDEX_CHANGED, this.onSelectedIndexChanged);
    }

    disconnectedCallback() {
        this.removeEventListener(eventActions.ADD, this.onEventAdd);
        this.removeEventListener(eventActions.EDIT, this.onEventEdit);
        this.removeEventListener(eventActions.DELETE, this.onEventDelete);
    }

    public async onEventAdd(e) {

        await this._eventService.add(e.detail.event);
        this.events = await this._eventService.get();
        
        this.eventListElement.setAttribute("events", JSON.stringify(this.events));
        this.eventEditElement.setAttribute("event", JSON.stringify(new Event()));        
    }

    public onEventEdit(e) {
        this.eventEditElement.setAttribute("event", JSON.stringify(e.detail.event));
    }

    public async onEventDelete(e) {

        await this._eventService.remove(e.detail.event.id);
        this.events = await this._eventService.get();
        
        this.eventListElement.setAttribute("events", JSON.stringify(this.events));
        this.eventEditElement.setAttribute("event", JSON.stringify(new Event()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "events":
                this.events = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;

            case "tab-index":
                this.customTabIndex = newValue;
                break;

            case "page-index":
                this.customPageIndex = newValue;
                break;
        }
    }

    public customTabIndex: any;

    public customPageIndex: any;

    public get value(): Array<Event> { return this.events; }

    private events: Array<Event> = [];

    public event: Event = <Event>{};

    public get eventEditElement(): HTMLElement { return this.querySelector("ce-event-edit-embed") as HTMLElement; }

    public get eventListElement(): HTMLElement { return this.querySelector("ce-event-list-embed") as HTMLElement; }
}

customElements.define(`ce-event-master-detail`,EventMasterDetailComponent);
