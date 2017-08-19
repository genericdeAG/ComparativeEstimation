import { ComparisonPairsDto } from './../../rest-provider/ComparisonPairsDto';
import { ComparisonPair } from './../../rest-provider/ComparisonPair';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';

@Component({
  selector: 'app-estimation',
  templateUrl: './estimation.component.html',
  styleUrls: ['./estimation.component.css'],
  providers: [RestProviderService]
})
export class EstimationComponent implements OnInit {
  sprintId: string;
  comparisonPairs: ComparisonPair[];

  constructor(
    private restProvider: RestProviderService,
    private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => { this.sprintId = params['sprintId']; });
    this.restProvider.baseUri = "http://localhost:8080";
    this.restProvider.getComparisonPairsFor(this.sprintId).subscribe(comparisonPairsDto => { 
      let cp = comparisonPairsDto;
      this.comparisonPairs = cp.Pairs;
     });
  }

}
