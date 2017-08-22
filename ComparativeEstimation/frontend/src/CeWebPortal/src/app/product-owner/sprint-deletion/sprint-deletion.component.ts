import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { eConnectionStatus } from "app/eConnectionStatus";

@Component({
    selector: 'app-sprint-deletion',
    templateUrl: './sprint-deletion.component.html',
    styleUrls: ['./sprint-deletion.component.css']
})
export class SprintDeletionComponent implements OnInit {
    public eConnectionStatus = eConnectionStatus;
    sprintId: string;
    connStatus: eConnectionStatus = eConnectionStatus.idle; 

    constructor(
        private restProvider: RestProviderService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.route.queryParams.subscribe(params => { this.sprintId = params['sprintId']; });
        this.setConnectionStatus(eConnectionStatus.sendInProgress);
        this.restProvider.deleteSprint(this.sprintId)
            .subscribe(
                // Success
                () => {
                    this.setConnectionStatus(eConnectionStatus.sendSuccess);
                },
                // Error
                () => {
                    this.setConnectionStatus(eConnectionStatus.sendError);
                });
        }

    setConnectionStatus(connStatus: eConnectionStatus) {
        this.connStatus = connStatus;
    }
}