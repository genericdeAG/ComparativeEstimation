import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'sprint-creation',
  templateUrl: './sprint-creation.component.html',
  styleUrls: ['./sprint-creation.component.css'],
  providers: [RestProviderService]
})
export class SprintCreationComponent implements OnInit {

  constructor(private restProvider: RestProviderService) { }

  ngOnInit() {
  }

}
