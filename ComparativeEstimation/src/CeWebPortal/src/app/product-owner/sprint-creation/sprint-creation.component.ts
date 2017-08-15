import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'sprint-creation',
  templateUrl: './sprint-creation.component.html',
  styleUrls: ['./sprint-creation.component.css'],
  providers: [RestProviderService]
})
export class SprintCreationComponent implements OnInit {
  stories: string[] =  [];
  viewModel: string = "story title"

  constructor(
    private restProvider: RestProviderService,
    private router: Router) { }

  ngOnInit() {
  }

  addStory(){
    this.stories.push(this.viewModel);
  }

  createSprintAndNavigateToSprintCreationResult() {
    this.restProvider.baseUri = "http://localhost:8080";
    this.restProvider.createSprint(this.stories).subscribe(id => { 
      this.router.navigate(["/sprint-creation-summary"], {queryParams: {sprintId: id}});
     });
  }

}
