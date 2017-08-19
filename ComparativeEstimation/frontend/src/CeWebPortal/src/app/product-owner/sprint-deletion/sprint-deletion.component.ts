import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestProviderService } from './../../rest-provider/rest-provider.service';

@Component({
  selector: 'app-sprint-deletion',
  templateUrl: './sprint-deletion.component.html',
  styleUrls: ['./sprint-deletion.component.css'],
  providers: [RestProviderService]
})
export class SprintDeletionComponent implements OnInit {
  sprintId: string;
  success = false;

  constructor(
    private restProvider: RestProviderService,
    private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => { this.sprintId = params['sprintId']; });
    this.restProvider.baseUri = "http://localhost:8080";
    this.restProvider.deleteSprint(this.sprintId).subscribe(response => { 
      console.log(response);
      if(response.ok)
        {
          this.success = true;
        }
     });
  }

}
