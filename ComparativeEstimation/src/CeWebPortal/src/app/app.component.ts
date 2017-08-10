import { ComparisonPairsDto } from './rest-provider/ComparisonPairsDto';
import { Component, OnInit } from '@angular/core';
import { RestProviderService } from './rest-provider/rest-provider.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [RestProviderService]
})
export class AppComponent implements OnInit {
  title = 'app works!';
  sprintId: string = 'some result';
  comparisonPairs = new ComparisonPairsDto();

  constructor(private restProvider: RestProviderService) { }

  ngOnInit() {
    let stories: string[] = ["X", "y", "T"];
    this.restProvider.baseUri = "http://localhost:8080";

    this.restProvider.createSprint(stories).subscribe(id => {
      this.sprintId = id;
      this.restProvider.getComparisonPairsFor(this.sprintId).subscribe(cp => {
        console.log(cp);
        this.comparisonPairs = cp 
        })
    });



  }
}
