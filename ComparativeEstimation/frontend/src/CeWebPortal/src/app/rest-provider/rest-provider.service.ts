import { TotalWeighting } from './TotalWeighting';
import { Voting } from './Voting';
import { InconsistentVote } from './InconsistentVote';
import { ComparisonPairsDto } from './ComparisonPairsDto';
import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from "environments/environment";

@Injectable()
export class RestProviderService {
    baseUri: string = environment.apiEndpoint;

constructor(private http: Http) { }
   
    createSprint(stories: string[]): Observable<string | null> {
        return this.http
            .post(environment.apiEndpoint + '/sprints', JSON.stringify(stories), this.createRequestOptions())
            .map((res: Response) => { return res.text() })
            .catch((error: Response) => Observable.throw(null));
    }

    getComparisonPairsFor(sprintId: string): Observable<ComparisonPairsDto | null> {
        return this.http
            .get(environment.apiEndpoint + '/sprints/' + sprintId + '/comparisonpairs', this.createRequestOptions())
            .map((res: Response) => { return res.json() })
            .catch((error: Response) => Observable.throw(null));
    }

    submitVoting(sprintId: string, voting: Voting): Observable<null | InconsistentVote> {
        return this.http
            .post(environment.apiEndpoint + '/sprints/' + sprintId + '/votings', JSON.stringify(voting), this.createRequestOptions())
            .map((res: Response) => { return null; })
            .catch((error: Response) => Observable.throw(error.json()));
    }

    getTotalWeighting(sprintId: string): Observable<TotalWeighting> {
        return this.http
            .get(environment.apiEndpoint + '/sprints/' + sprintId + '/totalweighting', this.createRequestOptions())
            .map((res: Response) => { return res.json() })
            .catch((error: Response) => Observable.throw(null));
    }

    deleteSprint(sprintId: string) {
        return this.http
            .delete(environment.apiEndpoint + '/sprints/' + sprintId, this.createRequestOptions())
            .map((res: Response) => { return null; })
            .catch((error: Response) => Observable.throw(null));
    }

    private createRequestOptions(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    
}