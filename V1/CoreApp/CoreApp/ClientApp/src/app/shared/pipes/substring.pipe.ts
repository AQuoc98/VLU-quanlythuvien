import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'substring'
})
export class SubstringPipe implements PipeTransform {

    transform(strInput: string, length: number) {
        if (!strInput) return '';
        if (strInput.length <= length) return strInput;
        return `${strInput.substring(0, length)}...`;
    }

}