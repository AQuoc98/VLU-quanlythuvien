import { ComponentFactoryResolver, Directive, Input, OnInit, ViewContainerRef, OnChanges } from "@angular/core";
import { FormGroup } from "@angular/forms";

import { FieldConfig } from '@infrastructure/models';
import { InputComponent } from '../input/input.component';
import { SelectComponent } from '../select/select.component';
import { TextAreaComponent } from '../text-area/text-area.component';
import { DiRteComponent } from '../di-rte/di-rte.component';
import { ImageComponent } from '../image/image.component';

const componentMapper = {
    input: InputComponent,
    select: SelectComponent,
    textarea: TextAreaComponent,
    rte: DiRteComponent,
    image: ImageComponent
};

@Directive({
    selector: "[dynamicField]"
})
export class DynamicFieldDirective implements OnInit, OnChanges {

    @Input() field: FieldConfig;
    @Input() group: FormGroup;
    componentRef: any;

    constructor(
        private resolver: ComponentFactoryResolver,
        private container: ViewContainerRef) { }

    ngOnInit() {

    }

    ngOnChanges() {
        if (!this.field) return;

        const factory = this.resolver.resolveComponentFactory(
            componentMapper[this.field.type]
        );
        this.componentRef = this.container.createComponent(factory);
        this.componentRef.instance.field = this.field;
        this.componentRef.instance.group = this.group;
    }
}
