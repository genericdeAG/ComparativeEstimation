import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';

import { eConnectionStatus } from "app/eConnectionStatus";
import { TotalWeighting } from "app/rest-provider/TotalWeighting";
import { Observable, BehaviorSubject, Subscription } from "rxjs/Rx";

@Component({
  selector: 'app-sprint-totalweighting',
  templateUrl: './sprint-totalweighting.component.html',
  styleUrls: ['./sprint-totalweighting.component.css']
})
export class SprintTotalweightingComponent implements OnInit, OnDestroy {
    public eConnectionStatus = eConnectionStatus;
    
    sprintId: string;
    subjectTotalWeighting: BehaviorSubject<TotalWeighting> = new BehaviorSubject<TotalWeighting>(new TotalWeighting());
    totalWeighting: TotalWeighting;

    timerValueStart: number = 10;
    timerValue: number = 0;
    timerSubscription: Subscription;

    connStatus: eConnectionStatus = eConnectionStatus.idle;

    constructor(
        private restProvider: RestProviderService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.route.queryParams.subscribe(params => { this.sprintId = params['sprintId']; });
        this.subjectTotalWeighting.asObservable().subscribe(
            (totalWeighting: TotalWeighting) => {
                this.totalWeighting = totalWeighting;
            });
        this.onRefresh();        
    }

    ngOnDestroy() {
        this.timerSubscription.unsubscribe();
    }

    onRefresh() {
        this.timerValue = this.timerValueStart;
        this.updateTotalWeighting();
    }
    
    updateTotalWeighting() {
        this.setConnectionStatus(eConnectionStatus.receiveInProgress);
        this.restProvider.getTotalWeighting(this.sprintId)
            .subscribe(
                // Success
                (totalWeighting: TotalWeighting) => {
                    this.subjectTotalWeighting.next(totalWeighting);
                    this.timerAktivieren();
                    this.setConnectionStatus(eConnectionStatus.receiveSuccess);
                },
                // Error
                () => {
                    this.subjectTotalWeighting.next(new TotalWeighting());
                    this.setConnectionStatus(eConnectionStatus.receiveError);
                });
    }

    timerAktivieren() {
        if ((!this.timerSubscription) || (this.timerSubscription.closed)) {
            let timer = Observable.timer(1000, 1000);
            this.timerSubscription = timer.subscribe(() => {
                this.timerValue--;
                if (this.timerValue <= 0) {
                    this.timerValue = this.timerValueStart;
                    this.timerSubscription.unsubscribe();
                    this.updateTotalWeighting();
                }
            });
        }
    }

    setConnectionStatus(connStatus: eConnectionStatus) {
        this.connStatus = connStatus;
    }
}