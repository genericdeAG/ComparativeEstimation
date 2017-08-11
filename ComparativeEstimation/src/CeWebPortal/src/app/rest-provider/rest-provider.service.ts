import { TotalWeighting } from './TotalWeighting';
import { Voting } from './Voting';
import { InconsistentVote } from './InconsistentVote';
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

    submitVoting(sprintId: string, voting: Voting): Observable<InconsistentVote> {
        return this.http
        .post(this.baseUri + '/api/sprints/' + sprintId + '/votings', JSON.stringify(voting), this.createRequestOptions())
        .map(res => { return res.json()}); //im Normalfall gibt es hier kein JSON - nur bei Inkonsistenzen. Ggf. gibt es hier bessere Lösung?
    }

    getTotalWeighting(sprintId: string): Observable<TotalWeighting> {
        return this.http
        .get(this.baseUri + '/api/sprints/' + sprintId + '/totalweighting', this.createRequestOptions())
        .map(res => { return res.json()});
    }

    deleteSprint(sprintId: string) {
        return this.http.delete(this.baseUri + '/api/sprints/' + sprintId, this.createRequestOptions());
    }

    private createRequestOptions(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    
}