import { Component, OnInit } from '@angular/core';
import { RestProviderService } from './rest-provider/rest-provider.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [RestProviderService]
})
export class AppComponent implements OnInit{
  title = 'app works!';
  result = 'some result';

  constructor(private restProvider: RestProviderService) { }

  ngOnInit() {
    let stories: string[] = ["X", "y", "T"];
    let id = this.restProvider.sprintId;
    this.restProvider.createSprint(stories, "http://localhost:8080/");
    this.result = this.restProvider.sprintId;
  }
}
