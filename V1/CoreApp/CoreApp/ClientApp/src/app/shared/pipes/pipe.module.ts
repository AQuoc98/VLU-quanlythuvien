import { NgModule } from '@angular/core';

import { SafePipe } from './safe.pipe';
import { SubstringPipe } from './substring.pipe';
import { CustomCurrencyPipe } from './customcurrency.pipe'


@NgModule({
    declarations: [
        CustomCurrencyPipe,
        SafePipe,
        SubstringPipe
    ],
    exports: [
        SafePipe,
        SubstringPipe,
        CustomCurrencyPipe,
    ]
})
export class PipeModule { }