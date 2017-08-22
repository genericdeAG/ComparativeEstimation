import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-sprint-creation-summary',
    templateUrl: './sprint-creation-summary.component.html',
    styleUrls: ['./sprint-creation-summary.component.css']
})
export class SprintCreationSummaryComponent implements OnInit {
    sprintId: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.sprintId = params['sprintId'];
        });
    }
}
