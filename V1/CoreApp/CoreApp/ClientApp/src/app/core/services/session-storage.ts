import { Injectable, Inject } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';

@Injectable()
export class SessionStorage {

    constructor(@Inject(SESSION_STORAGE) private storage: StorageService) { }

    set(key: string, value: any) {
        this.storage.set(key, value);
    }

    get(key: string): any {
        return this.storage.get(key);
    }

    remove(key: string) {
        this.storage.remove(key);
    }
}