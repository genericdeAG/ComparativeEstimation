import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { eConnectionStatus } from "app/eConnectionStatus";

@Component({
    selector: 'app-sprint-deletion',
    templateUrl: './sprint-deletion.component.html',
    styleUrls: ['./sprint-deletion.component.css']
})
export class SprintDeletionComponent implements OnInit {
    public eConnectionStatus = eConnectionStatus;
    sprintId: string = "";
    connStatus: eConnectionStatus = eConnectionStatus.idle; 

    constructor(
        private restProvider: RestProviderService,
        private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.sprintId = params['id'];
            this.deleteSprint();
        });
    }  

    deleteSprint() {
        this.connStatus = eConnectionStatus.sendInProgress;
        this.restProvider.deleteSprint(this.sprintId)
        .subscribe(
            // Success
            () => {
                this.connStatus = eConnectionStatus.sendSuccess;
            },
            // Error
            () => {
                this.connStatus = eConnectionStatus.sendError;
            });
    }
}