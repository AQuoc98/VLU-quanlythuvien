export class Guid {
    private value: string = this.empty;

    constructor(value?: string) {
        if (value) {
            if (Guid.isValid(value)) {
                this.value = value;
            }
        }
    }

    public toString() {
        return this.value;
    }

    public toJSON(): string {
        return this.value;
    }

    public static get empty(): string {
        return '00000000-0000-0000-0000-000000000000';
    }

    get empty(): string {
        return Guid.empty;
    }

    private static isValid(str: string): boolean {
        const validRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
        return validRegex.test(str)
    }
}