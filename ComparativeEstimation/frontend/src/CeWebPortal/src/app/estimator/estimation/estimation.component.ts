import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';

import { ComparisonPairsDto } from './../../rest-provider/ComparisonPairsDto';
import { ComparisonPair } from './../../rest-provider/ComparisonPair';
import { Weighting } from "app/rest-provider/Weighting";
import { Voting } from "app/rest-provider/Voting";
import { InconsistentVote } from "app/rest-provider/InconsistentVote";

import { eSendStatus } from "app/eSendStatus";

@Component({
    selector: 'app-estimation',
    templateUrl: './estimation.component.html',
    styleUrls: ['./estimation.component.css']
})
export class EstimationComponent implements OnInit {
    public eSendStatus = eSendStatus;
    
    sprintId: string;
    comparisonPairs: ComparisonPair[];
    weighting: Weighting[] = [];

    stringSelectionDefault: string = "";
    stringSelectionA: string = "A";
    stringSelectionB: string = "B";

    isSendAllowed: boolean = false;
    isActionAllowed: boolean = true;
    sendStatus: eSendStatus = eSendStatus.idle; 

    constructor(
        private restProvider: RestProviderService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.route.queryParams.subscribe(params => { this.sprintId = params['sprintId']; });
        this.restProvider.getComparisonPairsFor(this.sprintId)
            .subscribe(comparisonPairsDto => {
                this.comparisonPairs = comparisonPairsDto.Pairs;
                for (let pair of comparisonPairsDto.Pairs) {
                    this.weighting.push(new Weighting(pair.Id, this.stringSelectionDefault));
                }
            });
    }

    onClear() {
        if (this.isActionAllowed) {
            this.weighting.forEach(entry => entry.Selection = this.stringSelectionDefault);
            this.setIsSendAllowed();
        }
    }

    onSelect(index: number, entry: string) {
        if (this.isActionAllowed) {
            this.weighting[index].Selection = entry;
            this.setIsSendAllowed();
        }
    }

    onSend() {
        if (this.isSendAllowed) {
            this.setSendStatus(eSendStatus.sendInProgress);
            let voting: Voting = new Voting((Math.round(Math.random() * 1000)).toString(), this.weighting);
            this.restProvider.submitVoting(this.sprintId, voting)
            .subscribe(
                // Success
                (sucess: boolean) => {
                    this.setSendStatus(eSendStatus.sendSuccess);
                },
                // Error
                (inconsistentVote: InconsistentVote) => {
                    this.setSendStatus(eSendStatus.sendError);
                });
            }
    }

    isChecked(index: number, entry: string) {
        return (this.weighting[index].Selection == entry);
    }

    
    setIsActionAllowed() {
        this.isActionAllowed = ( (this.sendStatus == eSendStatus.idle) || (this.sendStatus == eSendStatus.sendError) );
    }
    
    setIsSendAllowed() {
        this.setIsActionAllowed();
        this.isSendAllowed = this.isActionAllowed 
                             && this.weighting.every(entry => entry.Selection != this.stringSelectionDefault);
    }
    
    setSendStatus(sendStatus: eSendStatus) {
        this.sendStatus = sendStatus;
        this.setIsSendAllowed();
    }
}