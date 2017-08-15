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
  title = 'Comparative Estimation';

  constructor(private restProvider: RestProviderService) { }

  ngOnInit() {
  }
}
