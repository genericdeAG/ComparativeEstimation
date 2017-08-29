import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgPlural } from '@angular/common';
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

    isRefreshAllowed: boolean = true;
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
        this.updateTotalWeighting();        
    }

    ngOnDestroy() {
        this.timerSubscription.unsubscribe();
    }

    onRefresh() {
        if (this.isRefreshAllowed) {
            this.updateTotalWeighting();
        }
    }
    
    updateTotalWeighting() {
        this.setConnectionStatus(eConnectionStatus.receiveInProgress);
        if ((this.timerSubscription) && (!this.timerSubscription.closed))
            this.timerSubscription.unsubscribe();
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
        this.timerValue = this.timerValueStart;
        let timer = Observable.timer(1000, 1000);
        this.timerSubscription = timer.subscribe(() => {
            this.timerValue--;
            if (this.timerValue <= 0) {
                this.updateTotalWeighting();
            }
        });
    }

    setIsRefreshAllowed() {
        this.isRefreshAllowed = (this.connStatus == eConnectionStatus.idle)
                             || (this.connStatus == eConnectionStatus.receiveSuccess)
                             || (this.connStatus == eConnectionStatus.receiveError);
    }

    setConnectionStatus(connStatus: eConnectionStatus) {
        this.connStatus = connStatus;
        this.setIsRefreshAllowed();
    }
}