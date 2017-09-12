import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router, UrlSerializer } from '@angular/router';

@Component({
    selector: 'app-sprint-creation-summary',
    templateUrl: './sprint-creation-summary.component.html',
    styleUrls: ['./sprint-creation-summary.component.css']
})
export class SprintCreationSummaryComponent implements OnInit {
    sprintId: string;

    constructor(
        private location: Location,
        private route: ActivatedRoute,
        private router: Router,
        private urlSerializer: UrlSerializer) { }

    ngOnInit() {
        this.route.params.subscribe(params => this.sprintId = params['id']);
    }

    generateExternalUrl(path: string, param: string): string {
        let tree = this.router.createUrlTree([path, param]);
        let url = this.urlSerializer.serialize(tree);
        let fullUrl = window.location.origin + this.location.prepareExternalUrl(url);
        return fullUrl;
    }
}
