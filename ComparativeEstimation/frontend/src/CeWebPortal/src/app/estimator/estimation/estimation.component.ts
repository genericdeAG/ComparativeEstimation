import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';

import { ComparisonPairsDto } from './../../rest-provider/ComparisonPairsDto';
import { ComparisonPair } from './../../rest-provider/ComparisonPair';
import { Weighting } from "app/rest-provider/Weighting";
import { Voting } from "app/rest-provider/Voting";
import { InconsistentVote } from "app/rest-provider/InconsistentVote";

import { eConnectionStatus } from "app/eConnectionStatus";

@Component({
    selector: 'app-estimation',
    templateUrl: './estimation.component.html',
    styleUrls: ['./estimation.component.css']
})
export class EstimationComponent implements OnInit {
    public eConnectionStatus = eConnectionStatus;
    
    sprintId: string;
    comparisonPairs: ComparisonPair[];
    weighting: Weighting[] = [];

    stringSelectionDefault: string = "";
    stringSelectionA: string = "A";
    stringSelectionB: string = "B";

    isSendAllowed: boolean = false;
    isActionAllowed: boolean = true;
    connStatus: eConnectionStatus = eConnectionStatus.idle; 

    constructor(
        private restProvider: RestProviderService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.setConnectionStatus(eConnectionStatus.receiveInProgress);
        this.route.queryParams.subscribe(params => { this.sprintId = params['sprintId']; });
        this.restProvider.getComparisonPairsFor(this.sprintId)
            .subscribe(
                // Success
                (comparisonPairsDto: ComparisonPairsDto) => {
                    this.comparisonPairs = comparisonPairsDto.Pairs;
                    for (let pair of comparisonPairsDto.Pairs) {
                        this.weighting.push(new Weighting(pair.Id, this.stringSelectionDefault));
                    }
                    this.setConnectionStatus(eConnectionStatus.receiveSuccess);
                },
                // Error
                () => {
                    this.setConnectionStatus(eConnectionStatus.receiveError);
                });
    }

    onClear() {
        if (this.isActionAllowed) {
            this.weighting.forEach(entry => entry.Selection = this.stringSelectionDefault);
            this.setIsActionAllowed_SetIsSendAllowed();
        }
    }

    onSelect(index: number, entry: string) {
        if (this.isActionAllowed) {
            this.weighting[index].Selection = entry;
            this.setIsActionAllowed_SetIsSendAllowed();
        }
    }

    onSend() {
        if (this.isSendAllowed) {
            this.setConnectionStatus(eConnectionStatus.sendInProgress);
            let voting: Voting = new Voting((Math.round(Math.random() * 1000)).toString(), this.weighting);
            this.restProvider.submitVoting(this.sprintId, voting)
            .subscribe(
                // Success
                () => {
                    this.setConnectionStatus(eConnectionStatus.sendSuccess);
                },
                // Error
                (inconsistentVote: InconsistentVote) => {
                    this.setConnectionStatus(eConnectionStatus.sendError);
                });
            }
    }

    isChecked(index: number, entry: string) {
        return (this.weighting[index].Selection == entry);
    }
    
    setIsActionAllowed_SetIsSendAllowed() {
        this.isActionAllowed = this.connStatus == eConnectionStatus.receiveSuccess
                            || this.connStatus == eConnectionStatus.sendError;
        this.isSendAllowed = this.isActionAllowed 
                          && this.weighting.every(entry => entry.Selection != this.stringSelectionDefault);
    }
    
    setConnectionStatus(connStatus: eConnectionStatus) {
        this.connStatus = connStatus;
        this.setIsActionAllowed_SetIsSendAllowed();
    }
}