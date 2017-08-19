import { Component, Input } from '@angular/core';

@Component({
    selector: 'ce-panel',
    templateUrl: './ce-panel.component.html',
    styleUrls: ['./ce-panel.component.css']
})
export class CePanelComponent {
    @Input() title: String;
}
