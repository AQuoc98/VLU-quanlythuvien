import { Injectable } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute, NavigationExtras } from '@angular/router';
import { filter } from 'rxjs/operators';

@Injectable()
export class RouteService {

    private storedCurrentUrl: string;
    private storedPreviousUrl: string;

    constructor(private router: Router, private activatedRoute: ActivatedRoute) {
        this.listenEvent();
    }

    /**
     * Back to previous url
     */
    backUrl() {

        //this.router.navigateByUrl(this.storedPreviousUrl);
    }

    navigate(commands: any[], extras?: NavigationExtras): Promise<boolean> {
        return this.router.navigate(commands, extras);
    }

    navigateByUrl(url: string): Promise<boolean> {
        return this.router.navigateByUrl(url);
    }

    /**
     * Current url
     */
    get currentUrl(): string {
        return this.storedCurrentUrl;
    }

    /**
     * Previous url
     */
    get previousUrl(): string {
        return this.storedPreviousUrl;
    }

    private listenEvent() {
        this.router.events.pipe(
            filter(event => event instanceof NavigationEnd)
        ).subscribe((event: NavigationEnd) => {
            this.storedPreviousUrl = this.storedCurrentUrl;
            this.storedCurrentUrl = event.url;
        });
    }

}