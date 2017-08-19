import { Directive, OnChanges, Renderer, ElementRef, Input } from '@angular/core';

@Directive({
    selector: '[ceFocus]'
})
export class CeFocusDirective implements OnChanges {

    @Input('ceFocus') isFocused: boolean;

    constructor(private hostElement: ElementRef, private renderer: Renderer) { }

    ngOnChanges() {
        this.renderer.invokeElementMethod(this.hostElement.nativeElement, 'focus');
    }
}