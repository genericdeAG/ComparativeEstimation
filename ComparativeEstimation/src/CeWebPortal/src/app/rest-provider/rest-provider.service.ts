import { ComparisonPairsDto } from './ComparisonPairsDto';
import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/toPromise';
import "rxjs/add/operator/map";

@Injectable()
export class RestProviderService {
    baseUri: string = "http://localhost:8080";

constructor(private http: Http) { }
   
    createSprint(stories: string[]): Observable<string> {
        return this.http
            .post(this.baseUri + '/api/sprints', JSON.stringify(stories), this.createRequestOptions())
            .map(res => { return res.text()});
    }

    getComparisonPairsFor(sprintId: string): Observable<ComparisonPairsDto> {
        return this.http
            .get(this.baseUri + '/api/sprints/' + sprintId + '/comparisonpairs', this.createRequestOptions())
            .map(res => { return res.json()});
    }

    private createRequestOptions(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    
}