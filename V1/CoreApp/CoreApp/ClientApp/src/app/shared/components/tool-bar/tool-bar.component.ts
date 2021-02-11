import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';

import { RouteService } from '../../../core/services';
import { takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-tool-bar',
    templateUrl: 'tool-bar.component.html'
})
export class ToolBarComponent implements OnInit {

    @Output() saveEvent = new EventEmitter();
    private destroyed$ = new Subject();

    constructor(private routeService: RouteService, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {

    }

    ngOnDestroy() {
        this.destroyed$.next();
        this.destroyed$.complete();
    }

    save() {
        this.saveEvent.emit();
    }

    back() {
        this.activatedRoute.parent.url.pipe(
            takeUntil(this.destroyed$)
        ).subscribe((urlPath) => {
            const parentSegment = urlPath[urlPath.length - 1].path;
            const parentUrl = this.routeService.currentUrl.substring(0, this.routeService.currentUrl.indexOf(parentSegment) + parentSegment.length);
            this.routeService.navigateByUrl(parentUrl);
        });
    }
}
